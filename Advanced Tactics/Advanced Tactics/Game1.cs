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

        public static GraphicsDevice gd;
        public static ContentManager Ctt;
        public Data Data { get; set; }
        public static GraphicsDeviceManager graphics { get; set; }
        SpriteBatch spriteBatch;

        // Clavier, Souris, Camera
        KeyboardState oldKey, curKey;
        MouseState mouseStatePrevious, mouseStateCurrent;
        Viseur viseur;
        Sprite sppointer, flou;
        public enum Key { Q, W, A, Z, LeftControl, LeftShift, R, C }

        // Menu
        Menu menu;
        Song musicMenu;
        SoundEffect inGameMusic;
        SoundEffectInstance instance;
        SoundEffect click;
        List<Texture2D> ListMenu;


        // Map
        TileEngine tileMap;
        Map map;

        // Unit
        Player Player1, Player2;
        List<Player> Players;
        Partie Partie;

        Unit unit;
        List<Unit> ListToDraw;

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

            Game1.graphics.ApplyChanges();
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
            Data = new Data("map2", BufferWidth, BufferHeight);
            ListToDraw = new List<Unit>();
            ListMenu = new List<Texture2D>();

            Game1.graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            gd = this.GraphicsDevice;
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Menu
            menu = new Menu(Data, false, Content.Load<Texture2D>("Menu/TitreJouer"), Content.Load<Texture2D>("Menu/TitreOptions"), Content.Load<Texture2D>("Menu/TitreMapEditor"), Content.Load<Texture2D>("Menu/TitreQuitter"), Content.Load<Texture2D>("Menu/OptionsReso"), Content.Load<Texture2D>("Menu/OptionsScreen"), Content.Load<Texture2D>("Menu/OptionsVolM"), Content.Load<Texture2D>("Menu/OptionsVolB"), Content.Load<Texture2D>("Menu/OptionsRetour"), Content.Load<Texture2D>("Menu/OptionsReso2"), Content.Load<Texture2D>("Menu/OptionsReso3"), Content.Load<Texture2D>("Menu/OptionsScreen2"), Content.Load<Texture2D>("Menu/OptionsVolumeB2"), Content.Load<Texture2D>("Menu/OptionsVolumeB3"), Content.Load<Texture2D>("Menu/OptionsVolumeM2"), Content.Load<Texture2D>("Menu/OptionsVolumeM3"));
            click = Content.Load<SoundEffect>("Son/click1");
            musicMenu = Content.Load<Song>("Son/Russian Red Army Choir");
            MediaPlayer.Play(musicMenu);

            //Game
            inGameMusic = Content.Load<SoundEffect>("Son/ingamemusic");
            instance = inGameMusic.CreateInstance();
            instance.IsLooped = true;
            instance.Play();


            // Map
            map = new Map(Data);
            tileMap = new TileEngine(Data.fileMap, Data, map);
            flou = new Sprite(); flou.LC(Game1.Ctt, "Menu/flou");


            // Clavier, Souris
            viseur = new Viseur(Data, map.Carte);
            sppointer = new Sprite(); sppointer.LC(Game1.Ctt, "Curseur/pointer");

            // Unit
            Players = new List<Player>(2) { Player1, Player2 };
            Partie = new Partie(Data, viseur, map, Players);
            //unit = new Unit(data, "Plane", "Bishop", map.Carte, 1, 5, ListToDraw);

            //string[] arrayrang = new string[] { "AA", "Commando", "Doc", "Engineer", "Plane", "Pvt", "Tank", "Truck" };
            //string[] arrayclasse = new string[] { "Queen", "Rook", "Bishop", "Knight", "Pawn" };


            //// Fonction anonyme qui permet de faire ce que ferait une methode void sans utiliser de methode, et c'est justement l'avantage
            //// http://msdn.microsoft.com/en-us/library/dd267613(v=vs.110).aspx
            //// Cette fonction cree tous simplements plusieurs unitees
            //Func<Data, string, string, Map, int, int, List<Unit>, Unit, Unit> Rdunit = (d, r, c, m, x, y, l, u) => new Unit(d, r, c, m.Carte, x, y, l);
            //Random rrd = new Random();
            ////1 23 33 1
            //unit = new Unit(data, "HQ", "King", map.Carte, rrd.Next(0, data.WidthMap), rrd.Next(0, data.HeightMap), ListToDraw);
            //unit = new Unit(data, "HQ", "King", map.Carte, rrd.Next(0, data.WidthMap), rrd.Next(0, data.HeightMap), ListToDraw);
            //// Et ici j'appelle en boucle la dite fonction n fois, n etant le nombre d'unitees voulus
            //for (int i = 0; i < rrd.Next(200, 300); i++)
            //    Rdunit(data, arrayrang[rrd.Next(arrayrang.Count())], arrayclasse[rrd.Next(arrayclasse.Count())], map, rrd.Next(0, data.WidthMap), rrd.Next(0, data.HeightMap), ListToDraw, unit);
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
            curKey = Keyboard.GetState();
            sppointer.Update(gameTime);

            menu.InGame = true;

            if (menu.InGame)
            {
                MediaPlayer.Stop();

                viseur.Update(gameTime, ListToDraw, spriteBatch);
                instance.Volume = 0.4f;

                foreach (Player Player in Players)
                {
                    Player.PosHQ(viseur, map, Data, ListToDraw);
                }

                if (curKey.IsKeyDown(Keys.Escape))
                {
                    Exit();
                    base.Update(gameTime);
                    return;
                }
            }//System.Diagnostics.Process.Start("MapGen.exe", Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
            else // VIDE INTERSIDERAL
            {
                menu.Update(gameTime);

                //if (menu.IsExit) { base.EndRun(); base.Exit(); base.Update(gameTime); return; }
                //checkExitKey(curKey);


                if (mouseStateCurrent.LeftButton == ButtonState.Pressed && mouseStatePrevious.LeftButton == ButtonState.Released) click.Play();

                mouseStatePrevious = mouseStateCurrent;
            }

            if (!menu.InGame && !menu.MenuPrincipal && menu.Options)
            {
                graphics.PreferredBackBufferWidth = (int)Data.widthWindow;
                graphics.PreferredBackBufferHeight = (int)Data.heightWindow;
                graphics.IsFullScreen = menu.Fullscreen;
                Game1.graphics.ApplyChanges();
            }


            mouseStatePrevious = mouseStateCurrent;
            oldKey = curKey;

            base.Update(gameTime);
        }

        int once = 1;
        bool checkExitKey(KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                Exit();
                if (once == 1)
                {
                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                    startInfo.FileName = "MapGenerator";
                    process.StartInfo = startInfo;
                    process.Start();
                    once--;
                }

                return true;
            }
            return false;
        }


        #endregion

        // // // // // // // // 

        #region DRAW

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            debug = new Debug(Data, map, viseur, ListToDraw); debug.LoadContent();
            Informations = new Informations(Data, map, viseur, ListToDraw); Informations.LoadContent();

            if (menu.InGame) // IN GAME
            {
                Informations.Draw(spriteBatch, gameTime);

                spriteBatch.Begin();
                tileMap.Draw(spriteBatch);
                for (int i = 0; i < ListToDraw.Count(); i++) ListToDraw[i].DrawUnit(spriteBatch, gameTime);
                //for (int i = 0; i < Partie.HQ1.Count(); i++) spCaserouge.Draw(data, spriteBatch, gameTime, map.Carte[Partie.HQ1[i].X, Partie.HQ1[i].Y].positionPixel);
                //for (int i = 0; i < Partie.HQ2.Count(); i++) spCaserouge.Draw(data, spriteBatch, gameTime, map.Carte[Partie.HQ2[i].X, Partie.HQ2[i].Y].positionPixel);
                for (int j = 0; j < Players.Count(); j++)
                {
                    for (int i = 0; i < Players[j].StartZone.Count(); i++)
                    {
                        if (Players[j].HQmax < 1) Players[j].spriteStartZone.Draw(Data, spriteBatch, gameTime, map.Carte[Players[j].StartZone[i].X, Players[j].StartZone[i].Y].positionPixel);
                    }
                }

                //if (Players[i].HQmax < 1) { for (int j = 0; j < Players[j].StartZone.Count(); j++) spCaserouge.Draw(data, spriteBatch, gameTime, map.Carte[Players[j].StartZone[j].X, Players[j].StartZone[j].Y].positionPixel); }
                spriteBatch.End();

                spriteBatch.Begin();
                //debug.Draw(spriteBatch);
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
            sppointer.Draw(Data, spriteBatch, gameTime, new Vector2(Mouse.GetState().X, Mouse.GetState().Y));

            spriteBatch.End();
            //End

            base.Draw(gameTime);
        }

        #endregion

        #region HELPER
        private bool WasJustPressed(Key button)
        {
            curKey = Keyboard.GetState();
            switch (button)
            {
                case Key.Q:
                    return curKey.IsKeyDown(Keys.Q) && oldKey != curKey;

                case Key.C:
                    return curKey.IsKeyDown(Keys.C) && oldKey != curKey;

                case Key.W:
                    return curKey.IsKeyDown(Keys.W) && oldKey != curKey;

                case Key.A:
                    return curKey.IsKeyDown(Keys.A) && oldKey != curKey;

                case Key.Z:
                    return curKey.IsKeyDown(Keys.Z) && oldKey != curKey;

                case Key.LeftControl:
                    return curKey.IsKeyDown(Keys.LeftControl) && oldKey != curKey;

                case Key.R:
                    return curKey.IsKeyDown(Keys.R) && oldKey != curKey;
            }
            oldKey = curKey;
            return false;
        }
        #endregion
    }
}
