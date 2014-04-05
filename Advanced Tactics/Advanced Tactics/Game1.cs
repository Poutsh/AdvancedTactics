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

namespace Advanced_Tactics
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        ////////////////////////////////////
        //// DEBUT DECLARATION VARIABLES ///
        ////////////////////////////////////
        public static ContentManager Ctt;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static GraphicsDevice gd;
        GameTime gameTime;
        TimeSpan time;
        public static Variable var;

        // Clavier, Souris
        KeyboardState oldKeyboardState, currentKeyboardState, keystateunite;
        MouseState mouseStatePrevious, mouseStateCurrent;
        Viseur viseur;

        // Menu
        Menu menu;
        Song musicMenu;
        SoundEffect click;

        // Map
        TileEngine tileMap;
        SpriteFont font;
        Map map;
        //RandomSprite random;
        Sprite test2;

        // Unit
        Unit tank, pvt;

        //Resolution
        public int BufferHeight { get { return graphics.PreferredBackBufferHeight; } set { graphics.PreferredBackBufferHeight = value; } }
        public int BufferWidth { get { return graphics.PreferredBackBufferWidth; } set { graphics.PreferredBackBufferWidth = value; } }

        // Sprites
        Sprite test;

        Debug debug;
        //////////////////////////////////////////////////////////////////////////////////////////////////

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Ctt = Content;
            // Gestion de la fenetre
            graphics.PreferredBackBufferWidth = 1680;
            graphics.PreferredBackBufferHeight = 1050;
            graphics.IsFullScreen = false;
            this.Window.Title = "Advanced Tactics";

            // Gestion souris
            IsMouseVisible = true;
            

            this.graphics.ApplyChanges();
        }


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
            var = new Variable("map1", BufferHeight, BufferWidth);

            // Menu
            menu = new Menu(1680, 1050, false, Content.Load<Texture2D>("Menu/TitreJouer"), Content.Load<Texture2D>("Menu/TitreOptions"), Content.Load<Texture2D>("Menu/TitreQuitter"), Content.Load<Texture2D>("Menu/OptionsReso"), Content.Load<Texture2D>("Menu/OptionsScreen"), Content.Load<Texture2D>("Menu/OptionsVolM"), Content.Load<Texture2D>("Menu/OptionsVolB"), Content.Load<Texture2D>("Menu/OptionsRetour"), Content.Load<Texture2D>("Menu/OptionsReso2"), Content.Load<Texture2D>("Menu/OptionsReso3"), Content.Load<Texture2D>("Menu/OptionsScreen2"), Content.Load<Texture2D>("Menu/OptionsVolumeB2"), Content.Load<Texture2D>("Menu/OptionsVolumeB3"), Content.Load<Texture2D>("Menu/OptionsVolumeM2"), Content.Load<Texture2D>("Menu/OptionsVolumeM3"));
            click = Content.Load<SoundEffect>("Son/click1");
            musicMenu = Content.Load<Song>("Son/Russian Red Army Choir");
            MediaPlayer.Play(musicMenu);

            // Clavier, Souris
            //viseur = new Viseur(map, var);
            
            // Map
            map = new Map(var);
            tileMap = new TileEngine(var.fileMap, Content, var, map);
            this.font = Content.Load<SpriteFont>("font");
            //random = new RandomSprite(var, 500);

            // Unit
            pvt = new Unit("pvt", "dame", map.map, 1, 5);
            tank = new Unit("tank", "dame", map.map, 2, 5);
            
            //pvt = new Unit("pvt", "pion", 1, 0, Content);
            //map.map[10, 12] = new Cell(map.map, tank, 10, 12, var, Content);

            // Sprites
            test.LoadContent(Content, "Case/rouge");
            test2.LoadContent(Content, "Unit/Tank");

            viseur = new Viseur(map.map);

            /* DEBUG */
            debug = new Debug(Content, var, map, BufferHeight, BufferWidth); debug.LoadContent();
        }


        protected override void UnloadContent()
        {
            base.UnloadContent();
        }


        protected override void Update(GameTime gameTime)
        {
            // Init entrees utilisateur
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
            mouseStateCurrent = Mouse.GetState();
            currentKeyboardState = Keyboard.GetState();

            //Menu
            if (true)
            {
                menu.InGame = true;
                menu.MenuPrincipal = false;
                menu.Options = false;
            }
            else
                menu.Update(gameTime);

            if (mouseStateCurrent.LeftButton == ButtonState.Pressed && mouseStatePrevious.LeftButton == ButtonState.Released)  //son � chaque clic gauche
                click.Play();
            mouseStatePrevious = mouseStateCurrent;
            if (menu.InGame && !menu.MenuPrincipal)
                MediaPlayer.Stop();

            //Resolution
            if (!menu.InGame && !menu.MenuPrincipal && menu.Options)
            {
                graphics.PreferredBackBufferWidth = menu.BufferWidth;
                graphics.PreferredBackBufferHeight = menu.BufferHeight;
                graphics.IsFullScreen = menu.Fullscreen;
                this.graphics.ApplyChanges();
            }

            viseur.Update(gameTime);
            
            // Reset entrees utilisateur
            oldKeyboardState = currentKeyboardState;

            // Evenement sortie du jeu
            if (currentKeyboardState.IsKeyDown(Keys.Escape))
            {
                Exit();
                base.Update(gameTime);
                return;
            }

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);


            /// Main Menu ///
            if (!menu.InGame && menu.MenuPrincipal && !menu.Options)
            {
                menu.Draw(spriteBatch);
            }

            /// Option menu /// 
            if (!menu.InGame && !menu.MenuPrincipal && menu.Options)
            {
                menu.Draw(spriteBatch);
            }

            /// Jeu ///
            if (menu.InGame && !menu.MenuPrincipal && !menu.Options)
            {
                spriteBatch.Begin();
                tileMap.Draw(spriteBatch);
                debug.Draw(spriteBatch);


                /*for (int i = 0; i < random.List_of_positionx.Count(); i++)
                {
                    //test.Draw(spriteBatch, gameTime, map.map[random.List_of_positionx[i],random.List_of_positiony[i]].positionPixel, var.Scale);
                }*/

                tank.Draw(spriteBatch, gameTime);
                pvt.Draw(spriteBatch, gameTime);
                spriteBatch.End();

                spriteBatch.Begin();
                viseur.mvtViseur(spriteBatch, gameTime);
                spriteBatch.End();
            }

            base.Draw(gameTime);
        }
    }
}
