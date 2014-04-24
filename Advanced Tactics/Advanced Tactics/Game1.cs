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
using AdvancedLibrary;
//using MapGen;


namespace Advanced_Tactics
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        #region VARIABLES

        public static GraphicsDevice gd;
        public static ContentManager Ctt;
        public static Constante cst;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;


        // Clavier, Souris, Camera
        KeyboardState oldKeyboardState, currentKeyboardState;
        MouseState mouseStatePrevious, mouseStateCurrent;
        Viseur viseur;
        Sprite sppointer;

        // Menu
        Menu menu;
        Song musicMenu;
        SoundEffect click;
        //Pause pause;

        // Map
        TileEngine tileMap;
        Map map;

        // Unit
        Unit unit;
        List<Unit> ListToDraw;
        List<int> MvtPossible;


        //Resolution
        public int BufferHeight { get { return graphics.PreferredBackBufferHeight; } set { graphics.PreferredBackBufferHeight = value; } }
        public int BufferWidth { get { return graphics.PreferredBackBufferWidth; } set { graphics.PreferredBackBufferWidth = value; } }

        Debug debug;

        #endregion

        // // // // // // // // 

        #region CONSTRUCTEUR

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Ctt = Content;
            // Gestion de la fenetre
            graphics.PreferredBackBufferWidth = 1600;
            graphics.PreferredBackBufferHeight = 900;
            graphics.IsFullScreen = false;
            this.Window.Title = "Advanced Tactics";

            this.IsFixedTimeStep = true;
            this.TargetElapsedTime = new TimeSpan(0, 0, 0, 0, 5);

            // Gestion souris
            IsMouseVisible = false;

            this.graphics.ApplyChanges();
        }

        #endregion

        // // // // // // // // 

        #region INIT + LOAD + UNLOAD

        protected override void Initialize()
        {
            currentKeyboardState = new KeyboardState();

            ListToDraw = new List<Unit>();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            gd = this.GraphicsDevice;
            cst = new Constante("map2", BufferHeight, BufferWidth);

            // Menu
            menu = new Menu(false, Content.Load<Texture2D>("Menu/TitreJouer"), Content.Load<Texture2D>("Menu/TitreOptions"), Content.Load<Texture2D>("Menu/TitreQuitter"), Content.Load<Texture2D>("Menu/OptionsReso"), Content.Load<Texture2D>("Menu/OptionsScreen"), Content.Load<Texture2D>("Menu/OptionsVolM"), Content.Load<Texture2D>("Menu/OptionsVolB"), Content.Load<Texture2D>("Menu/OptionsRetour"), Content.Load<Texture2D>("Menu/OptionsReso2"), Content.Load<Texture2D>("Menu/OptionsReso3"), Content.Load<Texture2D>("Menu/OptionsScreen2"), Content.Load<Texture2D>("Menu/OptionsVolumeB2"), Content.Load<Texture2D>("Menu/OptionsVolumeB3"), Content.Load<Texture2D>("Menu/OptionsVolumeM2"), Content.Load<Texture2D>("Menu/OptionsVolumeM3"));
            click = Content.Load<SoundEffect>("Son/click1");
            musicMenu = Content.Load<Song>("Son/Russian Red Army Choir");
            MediaPlayer.Play(musicMenu);

            // Map
            map = new Map();
            tileMap = new TileEngine(cst.fileMap, Content, cst, map);

            // Clavier, Souris
            viseur = new Viseur(map.Carte);
            sppointer = new Sprite(); sppointer.LC(Content, "Curseur/pointer");

            // Unit
            unit = new Unit("plane", "fou", map.Carte, 1, 5, ListToDraw);
            MvtPossible = unit.MvtPossibleOfUnit; MvtPossible.Add(1);
            string[] arrayrang = new string[] { "aa", "com", "doc", "hq", "ing", "plane", "pvt", "tank", "truck" };
            string[] arrayclasse = new string[] { "roi", "dame", "tour", "fou", "cavalier", "pion" };

            Func<string, string, Map, int, int, List<Unit>, Unit, Unit> Rdunit = (r, c, m, x, y, l, u) => new Unit(r, c, m.Carte, x, y, l);
            Random rrd = new Random();

            for (int i = 0; i < rrd.Next(20, 50); i++)
                Rdunit(arrayrang[rrd.Next(arrayrang.Count())], arrayclasse[rrd.Next(arrayclasse.Count())], map, rrd.Next(0, cst.WidthMap), rrd.Next(0, cst.HeightMap), ListToDraw, unit);
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



            if (!menu.currentGame) // IN GAME
            {
                MediaPlayer.Stop();

                viseur.Update(gameTime, ListToDraw, MvtPossible);

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

                if (menu.IsExit) base.EndRun(); base.Exit(); base.Update(gameTime); return;

                if (mouseStateCurrent.LeftButton == ButtonState.Pressed && mouseStatePrevious.LeftButton == ButtonState.Released) click.Play();

                // MENU OPTIONS
                if (!menu.MenuPrincipal && menu.Options)
                {
                    graphics.PreferredBackBufferWidth = (int)cst.widthWindow;
                    graphics.PreferredBackBufferHeight = (int)cst.heightWindow;
                    graphics.IsFullScreen = menu.Fullscreen;
                    this.graphics.ApplyChanges();
                }

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
            debug = new Debug(Content, cst, map, BufferHeight, BufferWidth, viseur, ListToDraw, MvtPossible); debug.LoadContent();

            if (!menu.currentGame) // IN GAME
            {
                spriteBatch.Begin();    // Begin NORMAL

                tileMap.Draw(spriteBatch);

                for (int i = 0; i < ListToDraw.Count(); i++) ListToDraw[i].DrawUnit(spriteBatch, gameTime);

                spriteBatch.End();      // End

                /// /// /// ///

                spriteBatch.Begin();    // Begin VISEUR
                viseur.Draw(spriteBatch, gameTime);
                spriteBatch.End();      // End

            }
            else // MENU
            {
                menu.Draw(spriteBatch, gameTime);
            }

            //Begin DEBUG
            spriteBatch.Begin();
            debug.Draw(spriteBatch);
            sppointer.Draw(spriteBatch, gameTime, new Vector2(Mouse.GetState().X, Mouse.GetState().Y));
            spriteBatch.End();
            //End

            base.Draw(gameTime);
        }

        #endregion
    }
}
