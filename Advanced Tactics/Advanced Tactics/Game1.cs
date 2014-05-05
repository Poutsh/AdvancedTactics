using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.IO;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Net;
//using MapGen;


namespace Advanced_Tactics
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        #region VARIABLES

        GraphicsDevice gd;
        ContentManager Ctt;
        Data data;
        GraphicsDeviceManager graphics { get; set; }
        SpriteBatch spriteBatch;

        // Network
        Color color;
        Color clearColor;
        GameState gameState;

        // Clavier, Souris, Camera
        KeyboardState oldKeyboardState, currentKeyboardState;
        MouseState mouseStatePrevious, mouseStateCurrent;
        Viseur viseur;
        Sprite sppointer, flou;

        // Menu
        Menu menu;
        Song musicMenu;
        SoundEffect inGameMusic;
        SoundEffectInstance instance;
        SoundEffect click;
        //Pause pause;

        // Map
        TileEngine tileMap;
        Map map;

        // Unit
        Unit unit;
        List<Unit> ListToDraw;

        //Networking members
        NetworkSession session;
        AvailableNetworkSessionCollection availableSessions;
        int sessionIndex;
        AvailableNetworkSession availableSession;
        PacketWriter packetWriter;
        PacketReader packetReader;
        bool isServer;

        enum GameState { Menu, FindGame, PlayGame }
        enum SessionProperty { GameMode, SkillLevel, ScoreToWin }
        enum GameMode { Practice, Timed, CaptureTheFlag }
        enum SkillLevel { Beginner, Intermediate, Advanced }
        enum PacketType { Enter, Leave, Data }

        //Resolution
        public int BufferHeight { get { return graphics.PreferredBackBufferHeight; } set { graphics.PreferredBackBufferHeight = value; } }
        public int BufferWidth { get { return graphics.PreferredBackBufferWidth; } set { graphics.PreferredBackBufferWidth = value; } }

        Debug debug;
        Informations Informations;

        #endregion

        // // // // // // // // 

        #region CONSTRUCTEUR

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Ctt = Content;
            graphics.IsFullScreen = false;
            this.Window.Title = "Advanced Tactics";

            // Gestion souris
            IsMouseVisible = false;

            this.IsFixedTimeStep = true;
            this.TargetElapsedTime = new TimeSpan(0, 0, 0, 0, 1);

            this.graphics.ApplyChanges();
        }

        #endregion

        // // // // // // // // 

        #region INIT + LOAD + UNLOAD

        protected override void Initialize()
        {
            gd = this.GraphicsDevice;

            // Gestion de la fenetre
            BufferWidth = 1280;
            BufferHeight = 720;
            data = new Data("map2", BufferWidth, BufferHeight, Content, gd);
            ListToDraw = new List<Unit>();

            color = Color.White;
            clearColor = Color.CornflowerBlue;
            gameState = GameState.Menu;
            sessionIndex = 0;
            packetReader = new PacketReader();
            packetWriter = new PacketWriter();

            this.graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Menu
            menu = new Menu(data, false, Content.Load<Texture2D>("Menu/TitreJouer"), Content.Load<Texture2D>("Menu/TitreOptions"), Content.Load<Texture2D>("Menu/TitreMapEditor"), Content.Load<Texture2D>("Menu/TitreQuitter"), Content.Load<Texture2D>("Menu/OptionsReso"), Content.Load<Texture2D>("Menu/OptionsScreen"), Content.Load<Texture2D>("Menu/OptionsVolM"), Content.Load<Texture2D>("Menu/OptionsVolB"), Content.Load<Texture2D>("Menu/OptionsRetour"), Content.Load<Texture2D>("Menu/OptionsReso2"), Content.Load<Texture2D>("Menu/OptionsReso3"), Content.Load<Texture2D>("Menu/OptionsScreen2"), Content.Load<Texture2D>("Menu/OptionsVolumeB2"), Content.Load<Texture2D>("Menu/OptionsVolumeB3"), Content.Load<Texture2D>("Menu/OptionsVolumeM2"), Content.Load<Texture2D>("Menu/OptionsVolumeM3"));
            click = Content.Load<SoundEffect>("Son/click1");
            musicMenu = Content.Load<Song>("Son/Russian Red Army Choir");
            MediaPlayer.Play(musicMenu);

            //Game
            inGameMusic = Content.Load<SoundEffect>("Son/ingamemusic");
            instance = inGameMusic.CreateInstance();
            instance.IsLooped = true;
            instance.Play();


            // Map
            map = new Map(data);
            tileMap = new TileEngine(data.fileMap, data, map);
            flou = new Sprite(); flou.LC(data.Content, "Menu/flou");



            // Clavier, Souris
            viseur = new Viseur(data, map.Carte);
            sppointer = new Sprite(); sppointer.LC(data.Content, "Curseur/pointer");

            // Unit
            unit = new Unit(data, "Plane", "Bishop", map.Carte, 1, 5, ListToDraw);

            string[] arrayrang = new string[] { "AA", "Commando", "Doc", "Engineer", "Plane", "Pvt", "Tank", "Truck" };
            string[] arrayclasse = new string[] { "Queen", "Rook", "Bishop", "Knight", "Pawn" };


            // Fonction anonyme qui permet de faire ce que ferait une methode void sans utiliser de methode, et c'est justement l'avantage
            // http://msdn.microsoft.com/en-us/library/dd267613(v=vs.110).aspx
            // Cette fonction cree tous simplements plusieurs unitees
            Func<Data, string, string, Map, int, int, List<Unit>, Unit, Unit> Rdunit = (d, r, c, m, x, y, l, u) => new Unit(d, r, c, m.Carte, x, y, l);
            Random rrd = new Random();
            //1 23 33 1
            unit = new Unit(data, "HQ", "King", map.Carte, rrd.Next(0, data.WidthMap), rrd.Next(0, data.HeightMap), ListToDraw);
            unit = new Unit(data, "HQ", "King", map.Carte, rrd.Next(0, data.WidthMap), rrd.Next(0, data.HeightMap), ListToDraw);
            // Et ici j'appelle en boucle la dite fonction n fois, n etant le nombre d'unitees voulus
            for (int i = 0; i < rrd.Next(200, 300); i++)
                Rdunit(data, arrayrang[rrd.Next(arrayrang.Count())], arrayclasse[rrd.Next(arrayclasse.Count())], map, rrd.Next(0, data.WidthMap), rrd.Next(0, data.HeightMap), ListToDraw, unit);
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        #endregion

        // // // // // // // // 

        #region UPDATE

        protected override void Update(GameTime gameTime)
        {
            // Init entrees utilisateur
            mouseStateCurrent = Mouse.GetState();
            currentKeyboardState = Keyboard.GetState();
            sppointer.Update(gameTime);


<<<<<<< HEAD

            if (menu.currentGame) // IN GAME
=======
            if (!menu.currentGame) // IN GAME
>>>>>>> 26e6d44bc3e921eadd68d96323876c74f55ead8c
            {
                MediaPlayer.Stop();

                viseur.Update(gameTime, ListToDraw, spriteBatch);
                instance.Volume = 0.4f;

                if (currentKeyboardState.IsKeyDown(Keys.Escape))
                {
                    Exit();
                    base.Update(gameTime);
                    return;
                }
            }
            else // VIDE INTERSIDERAL
            {
                menu.Update(gameTime);

                if (menu.IsExit) { base.EndRun(); base.Exit(); base.Update(gameTime); return; }

                if (mouseStateCurrent.LeftButton == ButtonState.Pressed && mouseStatePrevious.LeftButton == ButtonState.Released) click.Play();

                //// MENU OPTIONS
                //if (!menu.MenuPrincipal && menu.Options)
                //{
                //    graphics.PreferredBackBufferWidth = (int)data.widthWindow;
                //    graphics.PreferredBackBufferHeight = (int)data.heightWindow;
                //    graphics.IsFullScreen = menu.Fullscreen;
                //    this.graphics.ApplyChanges();
                //}

                mouseStatePrevious = mouseStateCurrent;
            }

            mouseStatePrevious = mouseStateCurrent;
            oldKeyboardState = currentKeyboardState;

            base.Update(gameTime);
        }

        #endregion

        // // // // // // // // 

        #region DRAW

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            debug = new Debug(data, map, viseur, ListToDraw); debug.LoadContent();
            Informations = new Informations(data, map, viseur, ListToDraw); Informations.LoadContent();

            if (menu.currentGame) // IN GAME
            {
                Informations.Draw(spriteBatch, gameTime);

                spriteBatch.Begin(); 
                tileMap.Draw(spriteBatch);
                for (int i = 0; i < ListToDraw.Count(); i++) ListToDraw[i].DrawUnit(spriteBatch, gameTime);
                spriteBatch.End(); 

                spriteBatch.Begin();
                debug.Draw(spriteBatch);
                viseur.Draw(spriteBatch, gameTime);
                spriteBatch.End();
            }
            else // MENU
            {
                menu.Draw(spriteBatch, gameTime);
            }

            //Begin DEBUG
            spriteBatch.Begin();
            //debug.Draw(spriteBatch);
            sppointer.Draw(data, spriteBatch, gameTime, new Vector2(Mouse.GetState().X, Mouse.GetState().Y));

            spriteBatch.End();
            //End

            base.Draw(gameTime);
        }

        #endregion
    }
}
