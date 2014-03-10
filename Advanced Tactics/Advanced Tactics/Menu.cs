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

namespace Advanced_Tactics
{
    class Menu
    {
        Texture2D menuJouer;
        Texture2D menuOptions;
        Texture2D menuQuitter;
        Texture2D optionsRéso;
        Texture2D optionsScreen;
        Texture2D optionsVolM;
        Texture2D optionsVolB;
        Texture2D optionsRetour;
        int position = 1;
        int position2 = 0;
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

        public Menu(Texture2D img1, Texture2D img2, Texture2D img3, Texture2D img4, Texture2D img5, Texture2D img6, Texture2D img7, Texture2D img8)
        {
            menuJouer = img1;
            menuOptions = img2;
            menuQuitter = img3;
            optionsRéso = img4;
            optionsScreen = img5;
            optionsVolM = img6;
            optionsVolB = img7;
            optionsRetour = img8;

        }

        public void Draw(SpriteBatch sb)
        {
            if (MenuPrincipal)
            {
                sb.Begin();


                if (position == 1)
                {
                    sb.Draw(menuJouer, new Rectangle(0, 0, Game1.gd.Viewport.Width, Game1.gd.Viewport.Height), Color.White);
                }
                if (position == 2)
                {
                    sb.Draw(menuOptions, new Rectangle(0, 0, Game1.gd.Viewport.Width, Game1.gd.Viewport.Height), Color.White);
                }
                if (position == 3)
                {
                    sb.Draw(menuQuitter, new Rectangle(0, 0, Game1.gd.Viewport.Width, Game1.gd.Viewport.Height), Color.White);
                }
                sb.End();
            }
            if (Options)
            {
                sb.Begin();
                if (position2 == 1)
                {
                    sb.Draw(optionsRéso, new Rectangle(0, 0, Game1.gd.Viewport.Width, Game1.gd.Viewport.Height), Color.White);
                }
                if (position2 == 2)
                {
                    sb.Draw(optionsScreen, new Rectangle(0, 0, Game1.gd.Viewport.Width, Game1.gd.Viewport.Height), Color.White);
                }
                if (position2 == 3)
                {
                    sb.Draw(optionsVolM, new Rectangle(0, 0, Game1.gd.Viewport.Width, Game1.gd.Viewport.Height), Color.White);
                }
                if (position2 == 4)
                {
                    sb.Draw(optionsVolB, new Rectangle(0, 0, Game1.gd.Viewport.Width, Game1.gd.Viewport.Height), Color.White);
                }
                if (position2 == 5)
                {
                    sb.Draw(optionsRetour, new Rectangle(0, 0, Game1.gd.Viewport.Width, Game1.gd.Viewport.Height), Color.White);
                }
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

                    if (position < 1)
                    {
                        position = 3;
                    }
                    if (position > 3)
                    {
                        position = 1;
                    }
                    if (position == 3 && Keyboard.GetState().IsKeyDown(Keys.Enter))
                    {
                        isExit = true;
                    }
                    if (position == 1 && Keyboard.GetState().IsKeyDown(Keys.Enter))
                    {
                        menuPrincipal = false;
                        inGame = true;
                    }
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
                        Console.WriteLine(position2);
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.Down))
                    {
                        position2 = position2 + 1;
                        Console.WriteLine(position2);
                    }
                    if (position2 < 1)
                    {
                        position2 = 5;
                    }
                    if (position2 > 5)
                    {
                        position2 = 1;
                    }
                    if (position2 == 5 && Keyboard.GetState().IsKeyDown(Keys.Enter))
                    {
                        options = false;
                        menuPrincipal = true;
                    }



                }

            }

        }
    }
}
