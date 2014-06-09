using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace Advanced_Tactics
{
    public struct WasJust
    {
        public enum Key { Q, W, A, Z, LeftControl, LeftShift, R, C }
        public enum MouseButtons { Left, Right }
        public KeyboardState curKey;
        public MouseState curMouse;

        public WasJust(KeyboardState key) { curKey = key; }
        public WasJust(MouseState mouse) { curMouse = mouse; }

        public bool WasJustClicked(MouseButtons button)
        {
            switch (button)
            {
                case MouseButtons.Left:
                    return curMouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Released;

                case MouseButtons.Right:
                    return curMouse.RightButton == ButtonState.Pressed && oldMouse.RightButton == ButtonState.Released;
            }

            return false;
        }

        public bool WasJustPressed(Key button)
        {
            curKey = Keyboard.GetState();
            switch (button)
            {
                case Key.Q:
                    return curKey.IsKeyDown(Keys.Q) && oldKey != curKey;

                case Key.C:
                    return curKey.IsKeyDown(Keys.C) && oldKey != curKey;

                case Key.W:
                    return curKey.IsKeyDown(Keys.W) && oldKey != curKey;

                case Key.A:
                    return curKey.IsKeyDown(Keys.A) && oldKey != curKey;

                case Key.Z:
                    return curKey.IsKeyDown(Keys.Z) && oldKey != curKey;

                case Key.LeftControl:
                    return curKey.IsKeyDown(Keys.LeftControl) && oldKey != curKey;

                case Key.R:
                    return curKey.IsKeyDown(Keys.R) && oldKey != curKey;
            }
            oldKey = curKey;
            return false;
        }
    }
}
