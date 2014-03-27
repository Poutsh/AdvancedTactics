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
    class Menu
    {
        // Main menu
        Texture2D menuJouer, menuOptions, menuQuitter;

        //Option menu
        Texture2D optionsRéso, optionsReso2, optionsReso3;
        Texture2D optionsScreen, optionsScreen2;
        Texture2D optionsVolM, optionsVolumeM2, optionsVolumeM3;
        Texture2D optionsVolB, optionsVolumeB2, optionsVolumeB3;
        Texture2D optionsRetour;


        GraphicsDeviceManager graphique;



        int position = 1;
        int position2 = 0;
        int position3 = 1;
        int position4 = 1;

        TimeSpan time;

        bool inGame = false;

        public bool InGame
        {
            get { return inGame; }
            set { inGame = value; }
        }
        bool menuPrincipal = true;

        public bool MenuPrincipal
        {
            get { return menuPrincipal; }
            set { menuPrincipal = value; }
        }

        bool options = false;
        public bool Options
        {
            get { return options; }
            set { options = value; }
        }

        bool isExit = false;
        public bool IsExit
        {
            get { return isExit; }
            set { isExit = value; }
        }

        public Menu(Texture2D img1, Texture2D img2, Texture2D img3, Texture2D img4, Texture2D img5, Texture2D img6, Texture2D img7, Texture2D img8, Texture2D img9, Texture2D img10, Texture2D img11, Texture2D img12, Texture2D img13, Texture2D img14, Texture2D img15)
        {

            menuJouer = img1;
            menuOptions = img2;
            menuQuitter = img3;
            optionsRéso = img4;
            optionsScreen = img5;
            optionsVolM = img6;
            optionsVolB = img7;
            optionsRetour = img8;
            optionsReso2 = img9;
            optionsReso3 = img10;
            optionsScreen2 = img11;
            optionsVolumeB2 = img12;
            optionsVolumeB3 = img13;
            optionsVolumeM2 = img14;
            optionsVolumeM3 = img15;

        }

        public void Draw(SpriteBatch sb)
        {
            if (menuPrincipal)
            {
                sb.Begin();
                if (position == 1) { sb.Draw(menuJouer, new Rectangle(0, 0, Game1.gd.Viewport.Width, Game1.gd.Viewport.Height), Color.White); }
                if (position == 2) { sb.Draw(menuOptions, new Rectangle(0, 0, Game1.gd.Viewport.Width, Game1.gd.Viewport.Height), Color.White); }
                if (position == 3) { sb.Draw(menuQuitter, new Rectangle(0, 0, Game1.gd.Viewport.Width, Game1.gd.Viewport.Height), Color.White); }
                sb.End();
            }

            if (options)
            {
                sb.Begin();
                if (position2 == 1)
                {
                    if (position3 == 1) //******** 800*600
                    { 
                        sb.Draw(optionsRéso, new Rectangle(0, 0, Game1.gd.Viewport.Width, Game1.gd.Viewport.Height), Color.White);

                        if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                            graphique.PreferredBackBufferWidth = 800;
                            graphique.PreferredBackBufferHeight = 600;
                    }
                    if (position3 == 2) //******** 1280*720
                    {
                        sb.Draw(optionsReso2, new Rectangle(0, 0, Game1.gd.Viewport.Width, Game1.gd.Viewport.Height), Color.White);

                        if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                            graphique.PreferredBackBufferWidth = 1280;
                            graphique.PreferredBackBufferHeight = 720;
                    }
                    if (position3 == 3) //******** 1920*1080
                    { 
                        sb.Draw(optionsReso3, new Rectangle(0, 0, Game1.gd.Viewport.Width, Game1.gd.Viewport.Height), Color.White);

                        if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                            graphique.PreferredBackBufferWidth = 1920;
                            graphique.PreferredBackBufferHeight = 1080;
                    }
                }

                if (position2 == 2)
                {
                    if (position4 == 1) 
                    { 
                        sb.Draw(optionsScreen, new Rectangle(0, 0, Game1.gd.Viewport.Width, Game1.gd.Viewport.Height), Color.White);
                        if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                        {
                            graphique.IsFullScreen = true;
                        }
                    }
                    if (position4 == 2)
                    {
                        sb.Draw(optionsScreen2, new Rectangle(0, 0, Game1.gd.Viewport.Width, Game1.gd.Viewport.Height), Color.White);
                        if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                        {
                            graphique.IsFullScreen = false;
                        }
                    }
                }

                if (position2 == 3)
                {
                    if (position3 == 1) { sb.Draw(optionsVolM, new Rectangle(0, 0, Game1.gd.Viewport.Width, Game1.gd.Viewport.Height), Color.White); }
                    if (position3 == 2) { sb.Draw(optionsVolumeM2, new Rectangle(0, 0, Game1.gd.Viewport.Width, Game1.gd.Viewport.Height), Color.White); }
                    if (position3 == 3) { sb.Draw(optionsVolumeM3, new Rectangle(0, 0, Game1.gd.Viewport.Width, Game1.gd.Viewport.Height), Color.White); }
                }

                if (position2 == 4)
                {
                    if (position3 == 1) { sb.Draw(optionsVolB, new Rectangle(0, 0, Game1.gd.Viewport.Width, Game1.gd.Viewport.Height), Color.White); }
                    if (position3 == 2) { sb.Draw(optionsVolumeB2, new Rectangle(0, 0, Game1.gd.Viewport.Width, Game1.gd.Viewport.Height), Color.White); }
                    if (position3 == 3) { sb.Draw(optionsVolumeB3, new Rectangle(0, 0, Game1.gd.Viewport.Width, Game1.gd.Viewport.Height), Color.White); }
                }

                if (position2 == 5) { sb.Draw(optionsRetour, new Rectangle(0, 0, Game1.gd.Viewport.Width, Game1.gd.Viewport.Height), Color.White); }
                sb.End();
            }

        }

        public void Update(GameTime gameTime)
        {
            if (gameTime.TotalGameTime - time > TimeSpan.FromSeconds(0.10f))
            {
                time = gameTime.TotalGameTime;
                if (menuPrincipal)
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.Up))
                    {
                        position = position - 1;
                        Console.WriteLine(position);
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.Down))
                    {
                        position = position + 1;
                        Console.WriteLine(position);
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                    {
                        isExit = true;
                    }

                    if (position < 1) { position = 3; }
                    if (position > 3) { position = 1; }
                    if (position == 3 && Keyboard.GetState().IsKeyDown(Keys.Enter)) { isExit = true; }
                    if (position == 1 && Keyboard.GetState().IsKeyDown(Keys.Enter)) { menuPrincipal = false; inGame = true; }
                    if (position == 2 && Keyboard.GetState().IsKeyDown(Keys.Enter))
                    {
                        menuPrincipal = false;
                        position2 = 1;
                        options = true;
                    }
                }
                if (options)
                {

                    if (Keyboard.GetState().IsKeyDown(Keys.Up))
                    {
                        position2 = position2 - 1;
                        position3 = 1;
                        position4 = 1;
                        Console.WriteLine(position2);
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.Down))
                    {
                        position2 = position2 + 1;
                        position3 = 1;
                        position4 = 1;
                        Console.WriteLine(position2);
                    }
                    if (position2 == 3 && position3 == 1)
                    {
                        if (Keyboard.GetState().IsKeyDown(Keys.Enter) && MediaPlayer.Volume <= 0.9f)
                        {
                            if (MediaPlayer.State == MediaState.Paused)
                            {
                                MediaPlayer.Resume(); ;
                            }
                        }
                    }
                    if (position2 == 3 && position3 == 2)
                    {
                        if (Keyboard.GetState().IsKeyDown(Keys.Enter) && MediaPlayer.Volume >= 0.1f)
                        {
                            if (MediaPlayer.State == MediaState.Paused)
                            {
                                MediaPlayer.Resume(); ;
                            }
                        }
                    }
                    if (position2 == 3 && position3 == 3)
                    {
                        if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                        {
                            if (MediaPlayer.State == MediaState.Playing)
                            {
                                MediaPlayer.Pause();
                            }
                        }
                    }

                    if (position2 < 1) { position2 = 5; }
                    if (position2 > 5) { position2 = 1; }
                    if (position2 == 5 && Keyboard.GetState().IsKeyDown(Keys.Enter))
                    {
                        options = false;
                        menuPrincipal = true;
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.Right))
                    {
                        position3 = position3 + 1;
                        Console.WriteLine(position3);
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.Left))
                    {
                        position3 = position3 - 1;
                        Console.WriteLine(position3);
                    }
                    if (position3 < 1) { position3 = 3; }
                    if (position3 > 3) { position3 = 1; }

                    if (Keyboard.GetState().IsKeyDown(Keys.Right))
                    {
                        position4 = position4 + 1;
                        Console.WriteLine(position4);
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.Left))
                    {
                        position4 = position4 - 1;
                        Console.WriteLine(position4);
                    }
                    if (position4 < 1) { position4 = 2; }
                    if (position4 > 2) { position4 = 1; }

                    if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                    {
                        menuPrincipal = true;
                        options = false;
                    }
                }

            }

        }
    }
}
