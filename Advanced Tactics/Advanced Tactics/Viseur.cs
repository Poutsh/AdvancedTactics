using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Advanced_Tactics
{
    class Viseur : Game
    {
        Vector2 viseur = Vector2.Zero;
        Texture2D viseurtex;
        KeyboardState oldKeyboardState, currentKeyboardState;
        TimeSpan time;
        SpriteBatch spriteBatch;

        protected override void Initialize()
        {
            //int viseurx = (Game1.gd.Viewport.Width - Game1.gd.Viewport.Height) / 2;
            currentKeyboardState = new KeyboardState();
            //viseur = new Vector2(viseurx, 0);
            base.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
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

            base.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            Vector2 posinit = new Vector2((Game1.gd.Viewport.Width - Game1.gd.Viewport.Height) / 2, 0);
            spriteBatch.Begin();
            spriteBatch.Draw(viseurtex, (viseur + posinit), Color.White);
            spriteBatch.End();
            

            base.Draw(gameTime);
        }
    }
}
