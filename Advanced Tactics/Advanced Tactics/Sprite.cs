using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Advanced_Tactics
{
    public class Sprite
    {
        #region VARIABLES

        public Texture2D Texture { get { return texture; } set { texture = value; } }
        private Texture2D texture;

        public Vector2 Position { get { return position; } set { position = value; } }
        private Vector2 position;

        public Vector2 Direction { get { return direction; } set { direction = Vector2.Normalize(value); } }
        private Vector2 direction;

        public float Speed { get { return speed; } set { speed = value; } }
        private float speed;

        #endregion

        // // // // // // // // 

        #region INITIALISATION + LOAD + UPDATE

        public Sprite()
        {
            position = Vector2.Zero;
            direction = Vector2.Zero;
            speed = 0;
        }

        public virtual void LC(ContentManager content, string assetName)
        {
            texture = content.Load<Texture2D>(assetName);
        }

        public virtual void Update(GameTime gameTime)
        {
            position += direction * speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        }

        public virtual void HandleInput(KeyboardState keyboardState, MouseState mouseState)
        {
        }

        #endregion

        // // // // // // // // 

        #region DRAW(S)

        /// <summary>
        /// Draw normal
        /// </summary>
        /// <param name="data"></param>
        /// <param name="spriteBatch"></param>
        /// <param name="gameTime"></param>
        /// <param name="posinit"></param>
        public virtual void Draw(Data data, SpriteBatch spriteBatch, GameTime gameTime, Vector2 posinit)
        {
            spriteBatch.Draw(texture, posinit, null, Color.White, 0, Vector2.Zero, data.Scale, SpriteEffects.None, 1);
        }
        /// <summary>
        /// Draw with an origin spec
        /// </summary>
        /// <param name="data"></param>
        /// <param name="spriteBatch"></param>
        /// <param name="gameTime"></param>
        /// <param name="posinit"></param>
        /// <param name="origin"></param>
        public virtual void Draw(Data data, SpriteBatch spriteBatch, GameTime gameTime, Vector2 posinit, Vector2 origin)
        {
            spriteBatch.Draw(texture, posinit, null, Color.White, 0, origin, data.Scale, SpriteEffects.None, 1);
        }
        /// <summary>
        /// Draw and specify a scale
        /// </summary>
        /// <param name="data"></param>
        /// <param name="spriteBatch"></param>
        /// <param name="gameTime"></param>
        /// <param name="posinit"></param>
        /// <param name="Scale"></param>
        public virtual void Draw(Data data, SpriteBatch spriteBatch, GameTime gameTime, Vector2 posinit, float Scale)
        {
            spriteBatch.Draw(texture, posinit, null, Color.White, 0, Vector2.Zero, Scale, SpriteEffects.None, 1);
        }
        /// <summary>
        /// Draw with bool for blink
        /// </summary>
        /// <param name="data"></param>
        /// <param name="spriteBatch"></param>
        /// <param name="gameTime"></param>
        /// <param name="posinit"></param>
        /// <param name="blink"></param>
        public virtual void Draw(Data data, SpriteBatch spriteBatch, GameTime gameTime, Vector2 posinit, bool blink)
        {
            if (blink)
                spriteBatch.Draw(texture, posinit, null, Color.White, 0, Vector2.Zero, data.Scale, SpriteEffects.None, 1);
        }

        #endregion
    }
}