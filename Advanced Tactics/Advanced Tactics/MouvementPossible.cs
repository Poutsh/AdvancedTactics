using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Advanced_Tactics
{
    class MouvementPossible : Game
    {
        public enum towerunit { roi, fou, cavalier, tour, pion, dame };
        

        public virtual void Draw(SpriteBatch spriteBatch, towerunit piece, Vector2 pieceinit, GameTime gameTime, Sprites unitee)
        {
            float mapx = (float)(Game1.gd.Viewport.Width - Game1.gd.Viewport.Height) / 2f;
            float scale = (float)Game1.gd.Viewport.Height / 640f;
            float scalei = 32f * scale;

            switch (piece)
            {
                case towerunit.roi:
                    for (int i = 0; i < 4; i++)
                    {
                        spriteBatch.Draw(unitee.Texture, new Vector2(pieceinit.X + scalei, pieceinit.Y), null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 1);
                        spriteBatch.Draw(unitee.Texture, new Vector2(pieceinit.X - scalei, pieceinit.Y), null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 1);
                        spriteBatch.Draw(unitee.Texture, new Vector2(pieceinit.X, pieceinit.Y + scalei), null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 1);
                        spriteBatch.Draw(unitee.Texture, new Vector2(pieceinit.X, pieceinit.Y - scalei), null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 1);
                    }
                    
                    break;

                case towerunit.fou:
                    for (float x = mapx; x < (float)(Game1.gd.Viewport.Width - mapx); x = x + scalei)
                    {
                        for (float y = 0; y < (float)Game1.gd.Viewport.Height; y = y + scalei)
                        {
                            if ((pieceinit.Y-pieceinit.X) == (y-x)) 
                                for (int i = 0; i < 5; i++)
                                    spriteBatch.Draw(unitee.Texture, new Vector2(x, y), null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 1);

                            if((pieceinit.X + pieceinit.Y) == (x+y))
                                for (int i = 0; i < 5; i++)
                                    spriteBatch.Draw(unitee.Texture, new Vector2(x, y), null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 1);
                        }
                    }
                    break;

                case towerunit.cavalier:
                    for (float x = mapx; x < (float)(Game1.gd.Viewport.Width - mapx); x = x + scalei)
                        for (float y = 0; y < (float)Game1.gd.Viewport.Height; y = y + scalei)
                        {
                            if ((pieceinit.X - scalei == x && y == pieceinit.Y - 2 * scalei) || (pieceinit.X + scalei == x && y == pieceinit.Y - 2 * scalei) || (pieceinit.X - 2 * scalei == x && y == pieceinit.Y - scalei) || (pieceinit.X + 2 * scalei == x && y == pieceinit.Y - scalei) || (pieceinit.X - 2 * scalei == x && y == pieceinit.Y + scalei) || (pieceinit.X + 2 * scalei == x && y == pieceinit.Y + scalei) || (pieceinit.X - scalei == x && y == pieceinit.Y + 2 * scalei) || (pieceinit.X + scalei == x && y == pieceinit.Y + 2 * scalei))
                                for (int i = 0; i < 5; i++)
                                {
                                    spriteBatch.Draw(unitee.Texture, new Vector2(x, y), null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 1);
                                    spriteBatch.Draw(unitee.Texture, new Vector2(pieceinit.X, pieceinit.Y), null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 1);
                                }
                        }
                    break;

                case towerunit.tour:
                    for (float x = mapx; x < (float)(Game1.gd.Viewport.Width - mapx); x = x + scalei)
                        for (float y = 0; y < (float)Game1.gd.Viewport.Height; y = y + scalei)
                        {
                            spriteBatch.Draw(unitee.Texture, new Vector2(x, pieceinit.Y), null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 1);
                            spriteBatch.Draw(unitee.Texture, new Vector2(pieceinit.X, y), null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 1);
                        }
                    
                    break;

                case towerunit.pion:
                    for (int i = 0; i < 5; i++)
                        spriteBatch.Draw(unitee.Texture, new Vector2(pieceinit.X, pieceinit.Y - scalei), null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 1);
                    
                    break;

                case towerunit.dame:
                    for (float x = mapx; x < (float)(Game1.gd.Viewport.Width - mapx); x = x + scalei)
                    {
                        for (float y = 0; y < (float)Game1.gd.Viewport.Height; y = y + scalei)
                        {
                            if ((pieceinit.Y - pieceinit.X) == (y - x))
                                for (int i = 0; i < 5; i++)
                                    spriteBatch.Draw(unitee.Texture, new Vector2(x, y), null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 1);

                            if ((pieceinit.X + pieceinit.Y) == (x + y))
                                for (int i = 0; i < 5; i++)
                                    spriteBatch.Draw(unitee.Texture, new Vector2(x, y), null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 1);
                        }
                    }
                    for (float x = mapx; x < (float)(Game1.gd.Viewport.Width - mapx); x = x + scalei)
                        for (float y = 0; y < (float)Game1.gd.Viewport.Height; y = y + scalei)
                        {
                            spriteBatch.Draw(unitee.Texture, new Vector2(x, pieceinit.Y), null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 1);
                            spriteBatch.Draw(unitee.Texture, new Vector2(pieceinit.X, y), null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 1);
                        }
                    break;
            }
            base.Draw(gameTime);

        }
    }
}
