﻿using System;
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
        Texture2D single, multi;

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

        int position = 1;             //Main menu
        int position2 = 0;            //Options Vertical
        int position3 = 1;            //Options Horizontal
        int position4 = 1;            //Options Horizontal Fullscreen
        int position5 = 1;            //Nombre de joueur

        TimeSpan time;

        public bool changeres;
        public bool changeres2;

        bool loadscreen = true;
        public bool Loadscreen { get { return loadscreen; } set { loadscreen = value; } }

        bool inGame = false;
        public bool InGame { get { return inGame; } set { inGame = value; } }

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

        bool nbJoueur = false;
        public bool NbJoueur { get { return nbJoueur; } set { nbJoueur = value; } }

        #endregion

        // // // // // // // // 

        #region CONSTRUCTEUR

        public Menu() { }

        public Menu(Game1.GameState current, Data data, bool full, Texture2D img1, Texture2D img2, Texture2D img3, Texture2D img16, Texture2D img4, Texture2D img5, Texture2D img6, Texture2D img7, Texture2D img8, Texture2D img9, Texture2D img10, Texture2D img11, Texture2D img12, Texture2D img13, Texture2D img14, Texture2D img15, Texture2D img17, Texture2D img18)
        {
            this.data = data;

            changeres = false;

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
            single = img17;
            multi = img18;

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
                if (position == 1) { sb.Draw(menuJouer, new Rectangle(0, 0, Game1.gd.Viewport.Width, Game1.gd.Viewport.Height), Color.White); }
                if (position == 2) { sb.Draw(menuMapEditor, new Rectangle(0, 0, Game1.gd.Viewport.Width, Game1.gd.Viewport.Height), Color.White); }
                if (position == 3) { sb.Draw(menuOptions, new Rectangle(0, 0, Game1.gd.Viewport.Width, Game1.gd.Viewport.Height), Color.White); }
                if (position == 4) { sb.Draw(menuQuitter, new Rectangle(0, 0, Game1.gd.Viewport.Width, Game1.gd.Viewport.Height), Color.White); }
                sb.End();
            }
            #endregion

            #region Nombre de Joueur
            if (nbJoueur)
            {
                sb.Begin();
                if (position5 == 1) { sb.Draw(single, new Rectangle(0, 0, Game1.gd.Viewport.Width, Game1.gd.Viewport.Height), Color.White); }
                if (position5 == 2) { sb.Draw(multi, new Rectangle(0, 0, Game1.gd.Viewport.Width, Game1.gd.Viewport.Height), Color.White); }
                sb.End();
            }

            #endregion

            #region InGame
            if (InGame)
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
                        sb.Draw(optionsRéso, new Rectangle(0, 0, Game1.gd.Viewport.Width, Game1.gd.Viewport.Height), Color.White);

                        if (currentKeyboardState.IsKeyDown(Keys.Enter))
                        {
                            data.WindowWidth = 800;
                            data.WindowHeight = 600;
                            changeres = true;
                        }
                        
                    }
                    if (position3 == 2) //******** 1280*720
                    {
                        sb.Draw(optionsReso2, new Rectangle(0, 0, Game1.gd.Viewport.Width, Game1.gd.Viewport.Height), Color.White);

                        if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                        {
                            data.WindowWidth = 1280;
                            data.WindowHeight = 720;
                            changeres = true;
                        }
                    }
                    if (position3 == 3) //******** 1920*1080
                    {
                        sb.Draw(optionsReso3, new Rectangle(0, 0, Game1.gd.Viewport.Width, Game1.gd.Viewport.Height), Color.White);

                        if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                        {
                            data.WindowWidth = 1920;
                            data.WindowHeight = 1080;
                            changeres = true;
                        }
                    }
                }
                else if (position2 == 2)
                {
                    if (position4 == 1)
                    {
                        sb.Draw(optionsScreen, new Rectangle(0, 0, Game1.gd.Viewport.Width, Game1.gd.Viewport.Height), Color.White);
                        if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                        {
                            fullscreen = true;
                            changeres2 = true;
                        }
                    }
                    if (position4 == 2)
                    {
                        sb.Draw(optionsScreen2, new Rectangle(0, 0, Game1.gd.Viewport.Width, Game1.gd.Viewport.Height), Color.White);
                        if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                        {
                            fullscreen = false;
                            changeres2 = true;

                        }
                    }
                }
                else if (position2 == 3)
                {
                    if (position3 == 1)
                    {
                        sb.Draw(optionsVolM, new Rectangle(0, 0, Game1.gd.Viewport.Width, Game1.gd.Viewport.Height), Color.White);

                        if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                            musicVolume = MathHelper.Clamp(musicVolume + 0.01f, 0.0f, 2.0f);
                    }
                    if (position3 == 2)
                    {
                        sb.Draw(optionsVolumeM2, new Rectangle(0, 0, Game1.gd.Viewport.Width, Game1.gd.Viewport.Height), Color.White);

                        if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                            musicVolume = MathHelper.Clamp(musicVolume - 0.01f, 0.0f, 2.0f);
                    }
                    if (position3 == 3)
                    {
                        sb.Draw(optionsVolumeM3, new Rectangle(0, 0, Game1.gd.Viewport.Width, Game1.gd.Viewport.Height), Color.White);

                        if (Keyboard.GetState().IsKeyDown(Keys.Enter))
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
                else if (position2 == 4)
                {
                    if (position3 == 1) { sb.Draw(optionsVolB, new Rectangle(0, 0, Game1.gd.Viewport.Width, Game1.gd.Viewport.Height), Color.White); }
                    if (position3 == 2) { sb.Draw(optionsVolumeB2, new Rectangle(0, 0, Game1.gd.Viewport.Width, Game1.gd.Viewport.Height), Color.White); }
                    if (position3 == 3) { sb.Draw(optionsVolumeB3, new Rectangle(0, 0, Game1.gd.Viewport.Width, Game1.gd.Viewport.Height), Color.White); }
                }
                else if (position2 == 5) { sb.Draw(optionsRetour, new Rectangle(0, 0, Game1.gd.Viewport.Width, Game1.gd.Viewport.Height), Color.White); }
                sb.End();
            }
            #endregion

            oldKeyboardState = currentKeyboardState;
        }

        #endregion

        // // // // // // // // 

        #region UPDATE
        public void Update(GameTime gameTime)
        {
            currentKeyboardState = Keyboard.GetState();

            #region Mainmenu
            if (menuPrincipal)
            {
                if (currentKeyboardState != oldKeyboardState && currentKeyboardState.IsKeyDown(Keys.Up))
                {
                    position = position + 1;
                    //Console.WriteLine(position);
                }
                if (currentKeyboardState != oldKeyboardState && currentKeyboardState.IsKeyDown(Keys.Down))
                {
                    position = position - 1;
                    //Console.WriteLine(position);
                }
                if (currentKeyboardState != oldKeyboardState && currentKeyboardState.IsKeyDown(Keys.Escape))
                {
                    isExit = true;
                    Game1.currentGameState = Game1.GameState.Exit;
                }

                if (position < 1) { position = 4; }
                if (position > 4) { position = 1; }
                if (position == 2 && currentKeyboardState != oldKeyboardState && currentKeyboardState.IsKeyDown(Keys.Enter) || currentKeyboardState != oldKeyboardState && currentKeyboardState.IsKeyDown(Keys.Escape)) { isExit = true; Game1.currentGameState = Game1.GameState.Exit; }

                if (position == 4 && currentKeyboardState != oldKeyboardState && currentKeyboardState.IsKeyDown(Keys.Enter))
                {
                    mapssss = true;
                }

                if (position == 3 && currentKeyboardState != oldKeyboardState && currentKeyboardState.IsKeyDown(Keys.Enter))
                {
                    menuPrincipal = false;
                    position2 = 1;
                    if (data.WindowWidth == 800) position3 = 1;
                    else if (data.WindowWidth == 1280) position3 = 2;
                    else if (data.WindowWidth == 1920) position3 = 3;
                    options = true;
                    Game1.currentGameState = Game1.GameState.Option;
                }
                
                if (position == 1 && currentKeyboardState.IsKeyDown(Keys.Enter))
                {
                    //this.Loadscreen = false; menuPrincipal = false; inGame = true;
                    //Game1.time = gameTime.TotalGameTime;
                    //Game1.currentGameState = Game1.GameState.Loading;
                    nbJoueur = true;
                    position5 = 1;
                    menuPrincipal = false;
                }
            }
            #endregion

            #region Nombre de Joueur
            if (nbJoueur)
            {
                if (currentKeyboardState != oldKeyboardState && currentKeyboardState.IsKeyDown(Keys.Up))
                {
                    position5 = position5 + 1;
                    //Console.WriteLine(position);
                }
                if (currentKeyboardState != oldKeyboardState && currentKeyboardState.IsKeyDown(Keys.Down))
                {
                    position5 = position5 - 1;
                    //Console.WriteLine(position);
                }
                if (currentKeyboardState != oldKeyboardState && currentKeyboardState.IsKeyDown(Keys.Escape))
                {
                    menuPrincipal = true;
                    position = 1;
                    nbJoueur = false;
                    Game1.currentGameState = Game1.GameState.Menu;
                }

                if (position5 < 1) { position5 = 2; }
                if (position5 > 2) { position5 = 1; }

                if (position5 == 1 && currentKeyboardState.IsKeyDown(Keys.Enter))
                {
                    //lancer single player
                }
                if (position5 == 2 && currentKeyboardState.IsKeyDown(Keys.Enter))
                {
                    //lancer multiplayer
                }
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
                    Game1.currentGameState = Game1.GameState.Menu;
                }
                else if (currentKeyboardState != oldKeyboardState && currentKeyboardState.IsKeyDown(Keys.Up))
                {
                    position2 = position2 - 1;
                    position3 = 1;
                    position4 = 1;
                    Console.WriteLine(position2);
                }
                else if (currentKeyboardState != oldKeyboardState && currentKeyboardState.IsKeyDown(Keys.Down))
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
                else if (position2 == 3 && position3 == 2)
                {
                    if (currentKeyboardState != oldKeyboardState && currentKeyboardState.IsKeyDown(Keys.Enter) && MediaPlayer.Volume >= 0.1f)
                    {
                        if (MediaPlayer.State == MediaState.Paused)
                        {
                            MediaPlayer.Resume(); ;
                        }
                    }
                }
                else if (position2 == 3 && position3 == 3)
                {
                    if (currentKeyboardState != oldKeyboardState && currentKeyboardState.IsKeyDown(Keys.Enter))
                    {
                        if (MediaPlayer.State == MediaState.Playing)
                        {
                            MediaPlayer.Pause();
                        }
                    }
                }
                else if (position2 < 1) { position2 = 5; }
                else if (position2 > 5) { position2 = 1; }
                else if (position2 == 5 && currentKeyboardState != oldKeyboardState && currentKeyboardState.IsKeyDown(Keys.Enter) || currentKeyboardState != oldKeyboardState && currentKeyboardState.IsKeyDown(Keys.Escape))
                {
                    options = false;
                    menuPrincipal = true;
                    Game1.currentGameState = Game1.GameState.Menu;
                }
                else if (currentKeyboardState != oldKeyboardState && currentKeyboardState.IsKeyDown(Keys.Right))
                {
                    position3 = position3 + 1;
                    Console.WriteLine(position3);
                }
                else if (currentKeyboardState != oldKeyboardState && currentKeyboardState.IsKeyDown(Keys.Left))
                {
                    position3 = position3 - 1;
                    Console.WriteLine(position3);
                }
                else if (position3 < 1) { position3 = 3; }
                else if (position3 > 3) { position3 = 1; }
                else if (currentKeyboardState != oldKeyboardState && currentKeyboardState.IsKeyDown(Keys.Right))
                {
                    position4 = position4 + 1;
                    Console.WriteLine(position4);
                }
                else if (currentKeyboardState != oldKeyboardState && currentKeyboardState.IsKeyDown(Keys.Left))
                {
                    position4 = position4 - 1;
                    Console.WriteLine(position4);
                }
                else if (position4 < 1) { position4 = 2; }
                else if (position4 > 2) { position4 = 1; }
                else if (currentKeyboardState != oldKeyboardState && currentKeyboardState.IsKeyDown(Keys.Escape))
                {
                    menuPrincipal = true;
                    options = false;
                    Game1.currentGameState = Game1.GameState.Menu;
                }

            }
            #endregion

            oldKeyboardState = currentKeyboardState;
        }
    }
        #endregion
}

