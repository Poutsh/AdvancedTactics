using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace Advanced_Tactics
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static GraphicsDevice gd;

        Texture2D cursor_custom;
        Texture2D map1;
        Vector2 spritePosition = Vector2.Zero;

        Texture2D tank;
        Vector2 tankPosition = new Vector2(320, 320);
        
        SoundEffect click;
        Song musicMenu;

        MouseState mouseStatePrevious, mouseStateCurrent;
        Menu menu;


        bool checkExitKey(KeyboardState keyboardState, GamePadState gamePadState)
        {
            if (keyboardState.IsKeyDown(Keys.Escape) || gamePadState.Buttons.Back == ButtonState.Pressed)
            {
                Exit();
                return true;
            }
            return false;
        }

        void Game1_Exiting(object sender, EventArgs e)
        {
            // Add any code that must execute before the game ends. => Rien
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Gestion de la fenetre
            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            graphics.IsFullScreen = true;

            this.Window.Title = "Advanced Tactics";
            this.graphics.ApplyChanges();


        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            cursor_custom = Content.Load<Texture2D>("Ressources//cursortransp");
            map1 = Content.Load<Texture2D>("Ressources//Map//map1");
            tank = Content.Load<Texture2D>("minitanktrans");
            gd = this.GraphicsDevice;
            menu = new Menu(Content.Load<Texture2D>("TitreJouer"), Content.Load<Texture2D>("TitreOptions"), Content.Load<Texture2D>("Titrequitter"), Content.Load<Texture2D>("OptionsRéso"), Content.Load<Texture2D>("OptionsScreen"), Content.Load<Texture2D>("OptionsVolM"), Content.Load<Texture2D>("OptionsVolB"), Content.Load<Texture2D>("OptionsRetour"));
            click = Content.Load<SoundEffect>("click1");
            musicMenu = Content.Load<Song>("Russian Red Army Choir");
            MediaPlayer.Play(musicMenu);
        }


        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (menu.IsExit)  //quitte le jeu � partir du menu    //sinon appuyer sur �chap
            {
                base.Exit();
            }

            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
            KeyboardState keyboardState = Keyboard.GetState();
            if (checkExitKey(keyboardState, gamePadState))
            {
                base.Update(gameTime);
                return;
            }

            spritePosition.X = Mouse.GetState().X;
            spritePosition.Y = Mouse.GetState().Y;

            menu.Update(gameTime);
            mouseStateCurrent = Mouse.GetState();

            if (mouseStateCurrent.LeftButton == ButtonState.Pressed && mouseStatePrevious.LeftButton == ButtonState.Released)  //son � chaque clic gauche
            {
               click.Play();
            }
            mouseStatePrevious = mouseStateCurrent;

            if (menu.InGame && !menu.MenuPrincipal)
            {
                MediaPlayer.Stop();
            }


            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            if (!menu.InGame && menu.MenuPrincipal && !menu.Options)
            {
                menu.Draw(spriteBatch);
                spriteBatch.Begin();
                spriteBatch.Draw(cursor_custom, new Rectangle((int)spritePosition.X, (int)spritePosition.Y, 24, 24), Color.White);
                spriteBatch.End();
            }
            if (!menu.InGame && !menu.MenuPrincipal && menu.Options)
            {
                menu.Draw(spriteBatch);
                spriteBatch.Begin();
                spriteBatch.Draw(cursor_custom, new Rectangle((int)spritePosition.X, (int)spritePosition.Y, 24, 24), Color.White);
                spriteBatch.End();
            }
            if (menu.InGame && !menu.MenuPrincipal && !menu.Options)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(map1, new Rectangle((Game1.gd.Viewport.Width - Game1.gd.Viewport.Height)/2, 0, Game1.gd.Viewport.Height, Game1.gd.Viewport.Height), Color.White);
                spriteBatch.Draw(tank, tankPosition, Color.White);
                spriteBatch.Draw(cursor_custom, new Rectangle((int)spritePosition.X, (int)spritePosition.Y, 24, 24), Color.White);
                
                spriteBatch.End();
            }

            base.Draw(gameTime);
        }
    }
}
