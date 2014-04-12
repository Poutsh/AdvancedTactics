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
        public static Variable var;
        GraphicsDeviceManager graphics;
        GameTime gameTime;
        SpriteBatch spriteBatch;
        TimeSpan time;


        // Clavier, Souris, Camera
        KeyboardState oldKeyboardState, currentKeyboardState;
        MouseState mouseStatePrevious, mouseStateCurrent;
        Viseur viseur;

        // Menu
        Menu menu;
        Song musicMenu;
        SoundEffect click;
        //Pause pause;
        

        // Map
        TileEngine tileMap;
        SpriteFont font;
        Map map;
        Sprite test2;

        // Unit
        Unit unit, tank, pvt, doc;

        //Resolution
        public int BufferHeight { get { return graphics.PreferredBackBufferHeight; } set { graphics.PreferredBackBufferHeight = value; } }
        public int BufferWidth { get { return graphics.PreferredBackBufferWidth; } set { graphics.PreferredBackBufferWidth = value; } }

        // Sprites
        Sprite test;

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
            IsMouseVisible = true;

            

            this.graphics.ApplyChanges();
        }

        #endregion

        // // // // // // // // 

        #region INIT + LOAD + UNLOAD

        protected override void Initialize()
        {
            currentKeyboardState = new KeyboardState();

            test = new Sprite(); test.Initialize();
            test2 = new Sprite(); test2.Initialize();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            gd = this.GraphicsDevice;
            var = new Variable("map2", BufferHeight, BufferWidth);


            // Menu
            menu = new Menu(false, Content.Load<Texture2D>("Menu/TitreJouer"), Content.Load<Texture2D>("Menu/TitreOptions"), Content.Load<Texture2D>("Menu/TitreQuitter"), Content.Load<Texture2D>("Menu/OptionsReso"), Content.Load<Texture2D>("Menu/OptionsScreen"), Content.Load<Texture2D>("Menu/OptionsVolM"), Content.Load<Texture2D>("Menu/OptionsVolB"), Content.Load<Texture2D>("Menu/OptionsRetour"), Content.Load<Texture2D>("Menu/OptionsReso2"), Content.Load<Texture2D>("Menu/OptionsReso3"), Content.Load<Texture2D>("Menu/OptionsScreen2"), Content.Load<Texture2D>("Menu/OptionsVolumeB2"), Content.Load<Texture2D>("Menu/OptionsVolumeB3"), Content.Load<Texture2D>("Menu/OptionsVolumeM2"), Content.Load<Texture2D>("Menu/OptionsVolumeM3"));
            click = Content.Load<SoundEffect>("Son/click1");
            musicMenu = Content.Load<Song>("Son/Russian Red Army Choir");
            MediaPlayer.Play(musicMenu);

            // Clavier, Souris


            // Map
            map = new Map();
            


            tileMap = new TileEngine(var.fileMap, Content, var, map);
            this.font = Content.Load<SpriteFont>("font");
            //random = new RandomSprite(var, 500);

            // Unit
            pvt = new Unit("pvt", "dame", map.Carte, 1, 1);
            tank = new Unit("hq", "roi", map.Carte, 5, 1);
            unit = new Unit("plane", "fou", map.Carte, 1, 5);
            doc = new Unit("doc", "tour", map.Carte, 5, 5);

            // Sprites
            test.LoadContent(Content, "Case/rouge");
            test2.LoadContent(Content, "Unit/Tank");

            viseur = new Viseur(map.Carte, gameTime);
            /* DEBUG */
            
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

            menu.Update(gameTime);
            viseur.Update(gameTime);

            if (gameTime != null && !menu.currentGame)
            {
                Init(menu.currentGame, menu.MenuPrincipal, menu.Options, false, menu.IsExit);
                Main(menu.currentGame, menu.MenuPrincipal, menu.Options, false, menu.IsExit);
                Option(menu.currentGame, menu.MenuPrincipal, menu.Options, false, menu.IsExit);
                Exit(menu.currentGame, menu.MenuPrincipal, menu.Options, false, menu.IsExit);
                Play(menu.currentGame, menu.MenuPrincipal, menu.Options, false, menu.IsExit);
            }
            

            /*GameState.Init();
            GameState.Main();
            GameState.Option();
            GameState.Exit();
            GameState.Play();*/


            /*// VIDE INTERSIDERAL
            menu.currentGame = true;
            if (!menu.currentGame)
            {
                menu.Update(gameTime);

                if (menu.IsExit) base.Exit();

                if (mouseStateCurrent.LeftButton == ButtonState.Pressed && mouseStatePrevious.LeftButton == ButtonState.Released) click.Play();

                // MENU OPTIONS
                if (!menu.MenuPrincipal && menu.Options)
                {
                    graphics.PreferredBackBufferWidth = (int)var.widthWindow;
                    graphics.PreferredBackBufferHeight = (int)var.heightWindow;
                    graphics.IsFullScreen = menu.Fullscreen;
                    this.graphics.ApplyChanges();
                }

                mouseStatePrevious = mouseStateCurrent;
            }
            else //IN GAME
            {
                menu.Update(gameTime);

                var.GR = true;
                MediaPlayer.Stop();

                viseur.Update(gameTime);

                if (currentKeyboardState.IsKeyDown(Keys.Escape))
                {
                    var.GR = false;
                    Exit();
                    base.Update(gameTime);
                    return;
                }
            }*/


            mouseStatePrevious = mouseStateCurrent;
            oldKeyboardState = currentKeyboardState;

            base.Update(gameTime);
        }

        #endregion

        // // // // // // // // 

        #region MENU STATE

        void Init(bool initState = false, bool mainmenuState = false, bool optionmenuState = false, bool ingameState = false, bool exitState = false)
        {
            initState = true;
        }

        void Main(bool initState = true, bool mainmenuState = true, bool optionmenuState = false, bool ingameState = false, bool exitState = false)
        {
            exitState = menu.IsExit;

            if (mouseStateCurrent.LeftButton == ButtonState.Pressed && mouseStatePrevious.LeftButton == ButtonState.Released)
                click.Play();
        }

        void Option(bool initState = true, bool mainmenuState = false, bool optionmenuState = true, bool ingameState = false, bool exitState = false)
        {
            if (mouseStateCurrent.LeftButton == ButtonState.Pressed && mouseStatePrevious.LeftButton == ButtonState.Released)
                click.Play();

            graphics.PreferredBackBufferWidth = (int)var.widthWindow;
            graphics.PreferredBackBufferHeight = (int)var.heightWindow;
            graphics.IsFullScreen = menu.Fullscreen;
            graphics.ApplyChanges();
        }

        void Exit(bool initState = true, bool mainmenuState = false, bool optionmenuState = false, bool ingameState = false, bool exitState = false)
        {
            exitState = menu.IsExit;
        }

        void Play(bool initState = true, bool mainmenuState = false, bool optionmenuState = false, bool ingameState = false, bool exitState = false)
        {
            MediaPlayer.Stop();

            viseur.Update(gameTime);

            if (currentKeyboardState.IsKeyDown(Keys.Escape))
            {
                exitState = menu.IsExit;
                base.Update(gameTime);
                return;
            }
        }

        #endregion

        // // // // // // // // 

        #region DRAW

        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.CornflowerBlue);
            debug = new Debug(Content, var, map, BufferHeight, BufferWidth, viseur); debug.LoadContent();

            /// Main Menu ///
            if (!menu.currentGame && menu.MenuPrincipal && !menu.Options)
            {
                menu.Draw(spriteBatch, gameTime);
            }

            /// Option menu /// 
            if (!menu.currentGame && !menu.MenuPrincipal && menu.Options)
            {
                menu.Draw(spriteBatch, gameTime);

            }

            /// Jeu ///
            if (menu.currentGame && !menu.MenuPrincipal && !menu.Options)
            {
                //Begin NORMAL
                spriteBatch.Begin();
                tileMap.Draw(spriteBatch);
                //End


                /*for (int i = 0; i < random.List_of_positionx.Count(); i++)
                {
                    //test.Draw(spriteBatch, gameTime, map.map[random.List_of_positionx[i],random.List_of_positiony[i]].positionPixel, var.Scale);
                }*/
                //unit.DrawUnit(spriteBatch, gameTime);

                pvt.DrawUnit(spriteBatch, gameTime);
                tank.DrawUnit(spriteBatch, gameTime);
                unit.DrawUnit(spriteBatch, gameTime);
                doc.DrawUnit(spriteBatch, gameTime);

                //if (currentKeyboardState.IsKeyDown(Keys.L)) doc = doc.
                spriteBatch.End();
                //End

                //Begin VISEUR
                spriteBatch.Begin();
                viseur.Draw(spriteBatch, gameTime);
                spriteBatch.End();
                //End

                //Begin DEBUG
                spriteBatch.Begin();
                debug.Draw(spriteBatch, gameTime);
                spriteBatch.End();
                //End
            }

            base.Draw(gameTime);
        }

        #endregion
    }
}
