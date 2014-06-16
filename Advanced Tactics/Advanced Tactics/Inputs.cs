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
        static double ClickTimer;
        const double TimerDelay = 500;
        static int ee = 0;

        static public void Update()
        {
            prevMouseState = curMouseState;
            prevKeyBState = curKeyBState;



            curMouseState = Mouse.GetState();
            curKeyBState = Keyboard.GetState();
        }

        static public bool doubleclick(GameTime gameTime)
        {
            ClickTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (curMouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released && ee == 0)
            {
                if (ClickTimer < TimerDelay) ee = 1; else ee = 0;
                ClickTimer = 0;
            }
            return ee == 1;
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
        static public bool Clickg()
        {
            return curMouseState.LeftButton == ButtonState.Released && prevMouseState.LeftButton == ButtonState.Pressed;
        }
        static public bool Clickd()
        {
            return curMouseState.RightButton == ButtonState.Released && prevMouseState.RightButton == ButtonState.Pressed;
        }
    }
}
