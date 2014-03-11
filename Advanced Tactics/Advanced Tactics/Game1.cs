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
        Vector2 spritePosition = Vector2.Zero;

        //Map
        Vector2 mappos;
        private Sprites map;

        //Texture2D tank;
        Vector2 tankPosition;
        private Sprites tank;

        //Cursor
        private Sprites cursor;
        
        SoundEffect click;
        Song musicMenu;


        MouseState mouseStatePrevious, mouseStateCurrent;
        Menu menu;

        Vector2 viseur = Vector2.Zero;
        Texture2D viseurtex;
        TimeSpan time;
        KeyboardState oldKeyboardState, currentKeyboardState;

        

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
            currentKeyboardState = new KeyboardState();

            tank = new Sprites();
            tank.Initialize();

            map = new Sprites();
            map.Initialize();

            cursor = new Sprites();
            cursor.Initialize();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            gd = this.GraphicsDevice;
            menu = new Menu(Content.Load<Texture2D>("TitreJouer"), Content.Load<Texture2D>("TitreOptions"), Content.Load<Texture2D>("Titrequitter"), Content.Load<Texture2D>("OptionsRéso"), Content.Load<Texture2D>("OptionsScreen"), Content.Load<Texture2D>("OptionsVolM"), Content.Load<Texture2D>("OptionsVolB"), Content.Load<Texture2D>("OptionsRetour"));
            click = Content.Load<SoundEffect>("click1");
            musicMenu = Content.Load<Song>("Russian Red Army Choir");
            MediaPlayer.Play(musicMenu);

            viseurtex = this.Content.Load<Texture2D>("viseur");
            tank.LoadContent(Content, "minitanktrans");
            cursor.LoadContent(Content, "Ressources//cursortransp");
            map.LoadContent(Content, "Ressources//Map//map1");
        }


        protected override void UnloadContent()
        {
            
            base.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (menu.IsExit)  //quitte le jeu � partir du menu    //sinon appuyer sur �chap
                base.Exit();

            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
            
            currentKeyboardState = Keyboard.GetState();

            if (gameTime.TotalGameTime - time > TimeSpan.FromSeconds(0.10f))
            {
                time = gameTime.TotalGameTime;
                int upscale = Game1.gd.Viewport.Height / 20;
                if (currentKeyboardState.IsKeyDown(Keys.Right))
                    viseur.X = viseur.X + upscale;
                if (currentKeyboardState.IsKeyDown(Keys.Left))
                    viseur.X = viseur.X - upscale;
                if (currentKeyboardState.IsKeyDown(Keys.Up))
                    viseur.Y = viseur.Y - upscale;
                if (currentKeyboardState.IsKeyDown(Keys.Down))
                    viseur.Y = viseur.Y + upscale;
            }
            

            oldKeyboardState = currentKeyboardState;

            spritePosition.X = Mouse.GetState().X;
            spritePosition.Y = Mouse.GetState().Y;

            menu.Update(gameTime);
            mouseStateCurrent = Mouse.GetState();

            if (mouseStateCurrent.LeftButton == ButtonState.Pressed && mouseStatePrevious.LeftButton == ButtonState.Released)  //son � chaque clic gauche
                click.Play();

            mouseStatePrevious = mouseStateCurrent;

            if (menu.InGame && !menu.MenuPrincipal)
                MediaPlayer.Stop();

            tank.Update(gameTime);
            map.Update(gameTime);
            cursor.Update(gameTime);

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

            KeyboardState keyboardState = Keyboard.GetState();
            int mapx = (Game1.gd.Viewport.Width - Game1.gd.Viewport.Height) / 2;
            float scale = (float)Game1.gd.Viewport.Height / 640f;

            //Position init des sprites
            Vector2 posinit = new Vector2((Game1.gd.Viewport.Width - Game1.gd.Viewport.Height) / 2, 0);
            Vector2 mappos = new Vector2((Game1.gd.Viewport.Width - Game1.gd.Viewport.Height) / 2, 0);
            Vector2 cursorpos = new Vector2((int)spritePosition.X, (int)spritePosition.Y);
            tankPosition = new Vector2((Game1.gd.Viewport.Width - Game1.gd.Viewport.Height) / 2, 32 * scale * 10);
            
            
            if (!menu.InGame && menu.MenuPrincipal && !menu.Options)
            {
                menu.Draw(spriteBatch);
                spriteBatch.Begin();
                cursor.Draw(spriteBatch, gameTime, cursorpos, 1);
                spriteBatch.End();
            }

            if (!menu.InGame && !menu.MenuPrincipal && menu.Options)
            {
                menu.Draw(spriteBatch);
                spriteBatch.Begin();
                cursor.Draw(spriteBatch, gameTime, cursorpos, 1);
                spriteBatch.End();
            }

            if (menu.InGame && !menu.MenuPrincipal && !menu.Options)
            {
                spriteBatch.Begin();
                map.Draw(spriteBatch, gameTime, mappos, scale);
                spriteBatch.Draw(viseurtex, viseur + posinit, null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 1);
                tank.Draw(spriteBatch, gameTime, tankPosition, scale);
                cursor.Draw(spriteBatch, gameTime, cursorpos, 1);
                spriteBatch.End();
            }

            base.Draw(gameTime);
        }
    }
}
