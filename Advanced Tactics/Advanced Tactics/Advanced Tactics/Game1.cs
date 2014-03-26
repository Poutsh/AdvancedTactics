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

namespace Advanced_Tactics_Propre
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        ////////////////////////////////////
        //// DEBUT DECLARATION VARIABLES ///
        ////////////////////////////////////

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static GraphicsDevice gd;
        GameTime gameTime;
        TimeSpan time;
        Variable var;

        // Clavier, Souris
        KeyboardState oldKeyboardState, currentKeyboardState, keystateunite;
        MouseState mouseStatePrevious, mouseStateCurrent;

        // Menu
        Menu menu;
        Song musicMenu;
        SoundEffect click;

        // Map
        TileEngine tileMap;
        SpriteFont font;
        Map map;
        RandomSprite random;
        Sprite test2;

        // Sprites
        Sprite test;

        Debug debug;
        //////////////////////////////////////////////////////////////////////////////////////////////////

        // Evenement sortie du jeu
        bool checkExitKey(KeyboardState keyboardState, GamePadState gamePadState)
        {
            if (keyboardState.IsKeyDown(Keys.Escape) || gamePadState.Buttons.Back == ButtonState.Pressed)
            {
                Exit();
                return true;
            }
            return false;
        }
        void Game1_Exiting(object sender, EventArgs e) { }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Gestion de la fenetre
            graphics.PreferredBackBufferWidth = 1600;
            graphics.PreferredBackBufferHeight = 900;
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
            var = new Variable("map1", Game1.gd.Viewport.Height, Game1.gd.Viewport.Width);

            // Menu
            //menu = new Menu(Resources.TitreJouer, Resources.TitreOptions, Resources.TitreQuitter,Resources.OptionReso, Resources.OptionScreen, Resources.OptionVolM, Resources.OptionVolB, Resources.OptionRetour, Resources.OptionReso2, Resources.OptionReso3, Resources.OptionScreen2, Resources.OptionsVolumeB2, Resources.OptionsVolumeB3, Resources.OptionsVolumeM2, Resources.OptionsVolumeM3);
            //click = Resources.sonsouris;
            //musicMenu = Resources.musiquemenu;
            menu = new Menu(Content.Load<Texture2D>("Menu/TitreJouer"), Content.Load<Texture2D>("Menu/TitreOptions"), Content.Load<Texture2D>("Menu/TitreQuitter"), Content.Load<Texture2D>("Menu/OptionsReso"), Content.Load<Texture2D>("Menu/OptionsScreen"), Content.Load<Texture2D>("Menu/OptionsVolM"), Content.Load<Texture2D>("Menu/OptionsVolB"), Content.Load<Texture2D>("Menu/OptionsRetour"), Content.Load<Texture2D>("Menu/OptionsReso2"), Content.Load<Texture2D>("Menu/OptionsReso3"), Content.Load<Texture2D>("Menu/OptionsScreen2"), Content.Load<Texture2D>("Menu/OptionsVolumeB2"), Content.Load<Texture2D>("Menu/OptionsVolumeB3"), Content.Load<Texture2D>("Menu/OptionsVolumeM2"), Content.Load<Texture2D>("Menu/OptionsVolumeM3"));
            click = Content.Load<SoundEffect>("Son/click1");
            musicMenu = Content.Load<Song>("Son/Russian Red Army Choir");
            MediaPlayer.Play(musicMenu);

            // Map
            map = new Map(var);
            tileMap = new TileEngine("map1", Content, var, map);
            //random = new RandomSprite(var, 20);
            this.font = Content.Load<SpriteFont>("font");

            // Sprites
            test.LoadContent(Content, "Unitees/minitank");
            test2.LoadContent(Content, "Unitees/minitank");

            /* DEBUG */
            debug = new Debug(Content, var, map, random); debug.LoadContent();
        }


        protected override void UnloadContent()
        {
            //base.UnloadContent();
        }


        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // Init entrees utilisateur
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
            mouseStateCurrent = Mouse.GetState();
            currentKeyboardState = Keyboard.GetState();

            //Menu
            //menu.Update(gameTime);
            menu.InGame = true;
            menu.MenuPrincipal = false;
            menu.Options = false;
            if (mouseStateCurrent.LeftButton == ButtonState.Pressed && mouseStatePrevious.LeftButton == ButtonState.Released)  //son � chaque clic gauche
                click.Play();
            mouseStatePrevious = mouseStateCurrent;
            if (menu.InGame && !menu.MenuPrincipal)
                MediaPlayer.Stop();


            // Reset entrees utilisateur

            oldKeyboardState = currentKeyboardState;

            if (checkExitKey(currentKeyboardState, gamePadState))
            {
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
                test.Draw(spriteBatch, gameTime, new Vector2(var.PosXInit, 0), var.Scale);
                test.Draw(spriteBatch, gameTime, new Vector2(var.PosXInit, 100), var.Scale);

                /*for (int i = 0; i < random.List_of_position.Count(); i++)
                {
                    //test2.Draw(spriteBatch, gameTime, new Vector2((random.List_of_position[i].X), random.List_of_position[i].Y), 1);
                }*/

                spriteBatch.End();
            }

            base.Draw(gameTime);
        }
    }
}
