using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdvancedLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Advanced_Tactics
{
    /*public class Pause
    {
        Variable var;
        KeyboardState oldKeyboardState, currentKeyboardState;
        private bool paused = false;
        private bool pauseKeyDown = false;
        private bool pausedForGuide = false;

        public Pause(KeyboardState keyboardState)
        {
            checkPauseKey(keyboardState);

            //checkPauseGuide();
        }

        #region FONCTIONS

        private void BeginPause(bool UserInitiated)
        {
            paused = true;
            pausedForGuide = !UserInitiated;
            //TODO: Pause audio playback
            //TODO: Pause controller vibration
        }

        private void EndPause()
        {
            //TODO: Resume audio
            //TODO: Resume controller vibration
            pausedForGuide = false;
            paused = false;
        }

        private void checkPauseKey(KeyboardState keyboardState)
        {
            bool pauseKeyDownThisFrame = (keyboardState.IsKeyDown(Keys.P));

            if (!pauseKeyDown && pauseKeyDownThisFrame)
            {
                if (!paused)
                    BeginPause(true);
                else
                    EndPause();
            }
            pauseKeyDown = pauseKeyDownThisFrame;
        }

        #endregion
    }*/
}
