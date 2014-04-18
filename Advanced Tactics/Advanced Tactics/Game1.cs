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
        SpriteFont font;
        Map map;

        // Unit
        Unit unit, tank, pvt, doc;
        List<Unit> ListToDraw;

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

            // Clavier, Souris
            viseur = new Viseur(map.Carte);
            sppointer = new Sprite(); sppointer.Initialize();
            sppointer.LoadContent(Content, "Curseur/pointer");

            tileMap = new TileEngine(cst.fileMap, Content, cst, map);

            // Unit
            pvt = new Unit("pvt", "dame", map.Carte, 1, 1, ListToDraw);
            tank = new Unit("hq", "roi", map.Carte, 5, 1, ListToDraw);
            unit = new Unit("plane", "fou", map.Carte, 1, 5, ListToDraw);
            doc = new Unit("doc", "tour", map.Carte, 5, 5, ListToDraw);
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

                viseur.Update(gameTime, ListToDraw);

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

                if (menu.IsExit) base.Exit();

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
            debug = new Debug(Content, cst, map, BufferHeight, BufferWidth, viseur, ListToDraw); debug.LoadContent();

            if (!menu.currentGame) // IN GAME
            {
                spriteBatch.Begin();    // Begin NORMAL

                tileMap.Draw(spriteBatch);

                for (int i = 0; i < ListToDraw.Count(); i++)    ListToDraw[i].DrawUnit(spriteBatch, gameTime);

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
