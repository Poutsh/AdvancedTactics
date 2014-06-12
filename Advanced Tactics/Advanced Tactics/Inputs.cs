using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Advanced_Tactics
{
    class Inputs
    {
        static private MouseState curMouseState = new MouseState();
        static private MouseState prevMouseState = new MouseState();

        static private KeyboardState curKeyBState = new KeyboardState();
        static private KeyboardState prevKeyBState = new KeyboardState();

        static public void Update()
        {
            prevMouseState = curMouseState;
            prevKeyBState = curKeyBState;

            curMouseState = Mouse.GetState();
            curKeyBState = Keyboard.GetState();
        }

        static public Vector2 getMousePos()
        {
            return new Vector2(curMouseState.X, curMouseState.Y);
        }
        static public bool Keyd(Keys k)
        {
            return curKeyBState.IsKeyDown(k);
        }
        static public bool Keyr(Keys k)
        {
            return curKeyBState.IsKeyUp(k) && prevKeyBState.IsKeyDown(k);
        }
        static public bool isLMBClick()
        {
            return curMouseState.LeftButton == ButtonState.Released && prevMouseState.LeftButton == ButtonState.Pressed;
        }
    }
}
