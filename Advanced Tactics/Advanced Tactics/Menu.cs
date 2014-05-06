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
using System.Diagnostics;

namespace Advanced_Tactics
{
    class Menu
    {
        #region VARIABLES

        Data data;
        // Main menu
        Texture2D menuJouer, menuOptions, menuQuitter, menuMapEditor;

        //Option menu
        Texture2D optionsRéso, optionsReso2, optionsReso3;
        Texture2D optionsScreen, optionsScreen2;
        Texture2D optionsVolM, optionsVolumeM2, optionsVolumeM3;
        Texture2D optionsVolB, optionsVolumeB2, optionsVolumeB3;
        Texture2D optionsRetour;
        KeyboardState oldKeyboardState, currentKeyboardState;

        //GraphicsDeviceManager graphique; // Pour Résolution/Fullscreen

        float musicVolume = 1.0f;        // Pour Volume Master
        Cue cue;                         // Pour Son off/on

        int position = 1;
        int position2 = 0;
        int position3 = 1;
        int position4 = 1;

        TimeSpan time;

        public bool nothing { get; set; }

        bool inGame = false;
        public bool currentGame { get { return inGame; } set { inGame = value; } }

        bool menuPrincipal = true;
        public bool MenuPrincipal { get { return menuPrincipal; } set { menuPrincipal = value; } }

        bool options = false;
        public bool Options { get { return options; } set { options = value; } }

        bool isExit = false;
        public bool IsExit { get { return isExit; } set { isExit = value; } }

        private bool fullscreen = false;
        public bool Fullscreen { get { return fullscreen; } set { fullscreen = value; } }

        private bool mapssss = false;
        public bool mapgen { get { return mapssss; } set { mapssss = value; } }

        #endregion

        // // // // // // // // 

        #region CONSTRUCTEUR

        public Menu() { }

        public Menu(Data data, bool full, Texture2D img1, Texture2D img2, Texture2D img3, Texture2D img16, Texture2D img4, Texture2D img5, Texture2D img6, Texture2D img7, Texture2D img8, Texture2D img9, Texture2D img10, Texture2D img11, Texture2D img12, Texture2D img13, Texture2D img14, Texture2D img15)
        {
            this.data = data;

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
            menuMapEditor = img16;

            fullscreen = full;
        }

        #endregion

        // // // // // // // // 

        #region DRAW

        public virtual void Draw(SpriteBatch sb, GameTime gameTime)
        {
            currentKeyboardState = Keyboard.GetState();

            #region Mainmenu
            if (menuPrincipal)
            {
                sb.Begin();
                if (position == 1) { sb.Draw(menuJouer, new Rectangle(0, 0, data.GraphicsDevice.Viewport.Width, data.GraphicsDevice.Viewport.Height), Color.White); }
                if (position == 2) { sb.Draw(menuMapEditor, new Rectangle(0, 0, data.GraphicsDevice.Viewport.Width, data.GraphicsDevice.Viewport.Height), Color.White); }
                if (position == 3) { sb.Draw(menuOptions, new Rectangle(0, 0, data.GraphicsDevice.Viewport.Width, data.GraphicsDevice.Viewport.Height), Color.White); }
                if (position == 4) { sb.Draw(menuQuitter, new Rectangle(0, 0, data.GraphicsDevice.Viewport.Width, data.GraphicsDevice.Viewport.Height), Color.White); }
                sb.End();
            }
            #endregion

            #region InGame
            if (currentGame)
            {
                sb.Begin();

                if (currentKeyboardState != oldKeyboardState && currentKeyboardState.IsKeyDown(Keys.Escape))
                {
                    menuPrincipal = true;
                }

                sb.End();
            }
            #endregion

            #region Option
            if (options)
            {
                sb.Begin();
                if (position2 == 1)
                {
                    if (position3 == 1) //******** 800*600
                    {
                        sb.Draw(optionsRéso, new Rectangle(0, 0, data.GraphicsDevice.Viewport.Width, data.GraphicsDevice.Viewport.Height), Color.White);

                        if (currentKeyboardState != oldKeyboardState && currentKeyboardState.IsKeyDown(Keys.Enter))
                        {
                            data.widthWindow = 800;
                            data.heightWindow = 600;
                        }
                    }
                    if (position3 == 2) //******** 1280*720
                    {
                        sb.Draw(optionsReso2, new Rectangle(0, 0, data.GraphicsDevice.Viewport.Width, data.GraphicsDevice.Viewport.Height), Color.White);

                        if (currentKeyboardState != oldKeyboardState && currentKeyboardState.IsKeyDown(Keys.Enter))
                        {
                            data.widthWindow = 1280;
                            data.heightWindow = 720;
                        }
                    }
                    if (position3 == 3) //******** 1920*1080
                    {
                        sb.Draw(optionsReso3, new Rectangle(0, 0, data.GraphicsDevice.Viewport.Width, data.GraphicsDevice.Viewport.Height), Color.White);

                        if (currentKeyboardState != oldKeyboardState && currentKeyboardState.IsKeyDown(Keys.Enter))
                        {
                            data.widthWindow = 1920;
                            data.heightWindow = 1080;
                        }
                    }
                }

                if (position2 == 2)
                {
                    if (position4 == 1)
                    {
                        sb.Draw(optionsScreen, new Rectangle(0, 0, data.GraphicsDevice.Viewport.Width, data.GraphicsDevice.Viewport.Height), Color.White);
                        if (currentKeyboardState != oldKeyboardState && currentKeyboardState.IsKeyDown(Keys.Enter))
                        {
                            fullscreen = true;
                        }
                    }
                    if (position4 == 2)
                    {
                        sb.Draw(optionsScreen2, new Rectangle(0, 0, data.GraphicsDevice.Viewport.Width, data.GraphicsDevice.Viewport.Height), Color.White);
                        if (currentKeyboardState != oldKeyboardState && currentKeyboardState.IsKeyDown(Keys.Enter))
                        {
                            fullscreen = false;
                        }
                    }
                }

                if (position2 == 3)
                {
                    if (position3 == 1)
                    {
                        sb.Draw(optionsVolM, new Rectangle(0, 0, data.GraphicsDevice.Viewport.Width, data.GraphicsDevice.Viewport.Height), Color.White);

                        if (currentKeyboardState != oldKeyboardState && currentKeyboardState.IsKeyDown(Keys.Enter))
                            musicVolume = MathHelper.Clamp(musicVolume + 0.01f, 0.0f, 2.0f);
                    }
                    if (position3 == 2)
                    {
                        sb.Draw(optionsVolumeM2, new Rectangle(0, 0, data.GraphicsDevice.Viewport.Width, data.GraphicsDevice.Viewport.Height), Color.White);

                        if (currentKeyboardState != oldKeyboardState && currentKeyboardState.IsKeyDown(Keys.Enter))
                            musicVolume = MathHelper.Clamp(musicVolume - 0.01f, 0.0f, 2.0f);
                    }
                    if (position3 == 3)
                    {
                        sb.Draw(optionsVolumeM3, new Rectangle(0, 0, data.GraphicsDevice.Viewport.Width, data.GraphicsDevice.Viewport.Height), Color.White);

                        if (currentKeyboardState != oldKeyboardState && currentKeyboardState.IsKeyDown(Keys.Enter))
                            if (cue.IsPaused)
                            {
                                cue.Resume();
                            }
                            else if (cue.IsPlaying)
                            {
                                cue.Pause();
                            }
                    }
                }

                if (position2 == 4)
                {
                    if (position3 == 1) { sb.Draw(optionsVolB, new Rectangle(0, 0, data.GraphicsDevice.Viewport.Width, data.GraphicsDevice.Viewport.Height), Color.White); }
                    if (position3 == 2) { sb.Draw(optionsVolumeB2, new Rectangle(0, 0, data.GraphicsDevice.Viewport.Width, data.GraphicsDevice.Viewport.Height), Color.White); }
                    if (position3 == 3) { sb.Draw(optionsVolumeB3, new Rectangle(0, 0, data.GraphicsDevice.Viewport.Width, data.GraphicsDevice.Viewport.Height), Color.White); }
                }

                if (position2 == 5) { sb.Draw(optionsRetour, new Rectangle(0, 0, data.GraphicsDevice.Viewport.Width, data.GraphicsDevice.Viewport.Height), Color.White); }
                sb.End();
            }
            #endregion

            oldKeyboardState = currentKeyboardState;
        }

        #endregion

        // // // // // // // // 

        #region UPDATE
        int once = 1;
        public void Update(GameTime gameTime)
        {
            currentKeyboardState = Keyboard.GetState();

            #region Mainmenu
            if (menuPrincipal)
            {
                if (currentKeyboardState != oldKeyboardState && currentKeyboardState.IsKeyDown(Keys.Up))
                {
                    position = position + 1;
                    Console.WriteLine(position);
                }
                if (currentKeyboardState != oldKeyboardState && currentKeyboardState.IsKeyDown(Keys.Down))
                {
                    position = position - 1;
                    Console.WriteLine(position);
                }
                if (currentKeyboardState != oldKeyboardState && currentKeyboardState.IsKeyDown(Keys.Escape))
                {
                    isExit = true;
                }

                if (position < 1) { position = 4; }
                if (position > 4) { position = 1; }
                if (position == 4 && currentKeyboardState != oldKeyboardState && currentKeyboardState.IsKeyDown(Keys.Enter) || currentKeyboardState != oldKeyboardState && currentKeyboardState.IsKeyDown(Keys.Escape)) { isExit = true; }
                if (position == 3 && currentKeyboardState != oldKeyboardState && currentKeyboardState.IsKeyDown(Keys.Enter))
                {
                    menuPrincipal = false;
                    position2 = 1;
                    options = true;
                }
                if (position == 2 && currentKeyboardState != oldKeyboardState && currentKeyboardState.IsKeyDown(Keys.Enter))
                {
                    if (once == 1)
                    {
                        System.Diagnostics.Process process = new System.Diagnostics.Process();
                        System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                        startInfo.FileName = "MapGen";
                        process.StartInfo = startInfo;
                        process.Start();
                        once--;
                    }
                }
                if (position == 1 && currentKeyboardState != oldKeyboardState && currentKeyboardState.IsKeyDown(Keys.Enter)) { this.nothing = false; menuPrincipal = false; inGame = true; }
            }
            #endregion

            #region Option
            if (options)
            {
                if (currentKeyboardState != oldKeyboardState && currentKeyboardState.IsKeyDown(Keys.Escape))
                {
                    menuPrincipal = true;
                    position = 1;
                    options = false;
                }
                if (currentKeyboardState != oldKeyboardState && currentKeyboardState.IsKeyDown(Keys.Up))
                {
                    position2 = position2 - 1;
                    position3 = 1;
                    position4 = 1;
                    Console.WriteLine(position2);
                }
                if (currentKeyboardState != oldKeyboardState && currentKeyboardState.IsKeyDown(Keys.Down))
                {
                    position2 = position2 + 1;
                    position3 = 1;
                    position4 = 1;
                    Console.WriteLine(position2);
                }
                if (position2 == 3 && position3 == 1)
                {
                    if (currentKeyboardState != oldKeyboardState && currentKeyboardState.IsKeyDown(Keys.Enter) && MediaPlayer.Volume <= 0.9f)
                    {
                        if (MediaPlayer.State == MediaState.Paused)
                        {
                            MediaPlayer.Resume(); ;
                        }
                    }
                }
                if (position2 == 3 && position3 == 2)
                {
                    if (currentKeyboardState != oldKeyboardState && currentKeyboardState.IsKeyDown(Keys.Enter) && MediaPlayer.Volume >= 0.1f)
                    {
                        if (MediaPlayer.State == MediaState.Paused)
                        {
                            MediaPlayer.Resume(); ;
                        }
                    }
                }
                if (position2 == 3 && position3 == 3)
                {
                    if (currentKeyboardState != oldKeyboardState && currentKeyboardState.IsKeyDown(Keys.Enter))
                    {
                        if (MediaPlayer.State == MediaState.Playing)
                        {
                            MediaPlayer.Pause();
                        }
                    }
                }

                if (position2 < 1) { position2 = 5; }
                if (position2 > 5) { position2 = 1; }
                if (position2 == 5 && currentKeyboardState != oldKeyboardState && currentKeyboardState.IsKeyDown(Keys.Enter) || currentKeyboardState != oldKeyboardState && currentKeyboardState.IsKeyDown(Keys.Escape))
                {
                    options = false;
                    menuPrincipal = true;
                }
                if (currentKeyboardState != oldKeyboardState && currentKeyboardState.IsKeyDown(Keys.Right))
                {
                    position3 = position3 + 1;
                    Console.WriteLine(position3);
                }
                if (currentKeyboardState != oldKeyboardState && currentKeyboardState.IsKeyDown(Keys.Left))
                {
                    position3 = position3 - 1;
                    Console.WriteLine(position3);
                }
                if (position3 < 1) { position3 = 3; }
                if (position3 > 3) { position3 = 1; }

                if (currentKeyboardState != oldKeyboardState && currentKeyboardState.IsKeyDown(Keys.Right))
                {
                    position4 = position4 + 1;
                    Console.WriteLine(position4);
                }
                if (currentKeyboardState != oldKeyboardState && currentKeyboardState.IsKeyDown(Keys.Left))
                {
                    position4 = position4 - 1;
                    Console.WriteLine(position4);
                }
                if (position4 < 1) { position4 = 2; }
                if (position4 > 2) { position4 = 1; }

                if (currentKeyboardState != oldKeyboardState && currentKeyboardState.IsKeyDown(Keys.Escape))
                {
                    menuPrincipal = true;
                    options = false;
                }

            }
            #endregion

            oldKeyboardState = currentKeyboardState;
        }
    }
        #endregion
}

