using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using AdvancedLibrary;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Advanced_Tactics
{
    public class GameState
    {
        Menu menu;
        KeyboardState oldKeyboardState, currentKeyboardState;
        MouseState mouseStatePrevious, mouseStateCurrent;
        GraphicsDeviceManager graphics;
        Variable var;
        SoundEffect click;
        Viseur viseur;

        public GameState()
        {
            /// A METTRE DANS LE GAME UPDATE
            /// 
            /*Init(menu.currentGame, menu.MenuPrincipal, menu.Options, false, menu.IsExit);
            Main(menu.currentGame, menu.MenuPrincipal, menu.Options, false, menu.IsExit);
            Option(menu.currentGame, menu.MenuPrincipal, menu.Options, false, menu.IsExit);
            Exit(menu.currentGame, menu.MenuPrincipal, menu.Options, false, menu.IsExit);
            Play(gameTime, menu.currentGame, menu.MenuPrincipal, menu.Options, false, exit);*/
        }

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

       /*void Play(GameTime gameTime, bool initState = true, bool mainmenuState = false, bool optionmenuState = false, bool ingameState = false, bool exitState = false)
       {
           MediaPlayer.Stop();

           viseur.Update(gameTime, ListOfUnit);

           if (Keyboard.GetState().IsKeyDown(Keys.Escape))
           {
               
           }
       }*/

       #endregion
    }
}
