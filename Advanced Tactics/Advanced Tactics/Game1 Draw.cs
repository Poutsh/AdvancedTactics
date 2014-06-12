using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Advanced_Tactics
{
    public partial class Game1 : Microsoft.Xna.Framework.Game
    {
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            debug = new Debug(Data, map, viseur, ListToDraw); debug.LoadContent();
            Informations = new Informations(Data, map, viseur, ListToDraw); Informations.LoadContent();

            if (menu.InGame) // IN GAME
            {
                //Informations.Draw(spriteBatch, gameTime);

                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
                tileMap.Draw(spriteBatch);

                for (int j = 0; j < Players.Count(); j++)
                    for (int i = 0; i < Players[j].StartZone.Count(); i++)
                        if (Players[j].HQmax < 1) Players[j].spriteStartZone.Draw(Data, spriteBatch, gameTime, map.Carte[Players[j].StartZone[i].X, Players[j].StartZone[i].Y].positionPixel);

                for (int i = 0; i < ListToDraw.Count(); i++) ListToDraw[i].DrawUnit(spriteBatch, gameTime);
                spriteBatch.End();

                spriteBatch.Begin();
                debug.Draw(spriteBatch);
                viseur.Draw(spriteBatch, gameTime);
                spriteBatch.End();
            }
            else // MENU
            {
                menu.Draw(spriteBatch, gameTime);
            }

            //Begin DEBUG
            spriteBatch.Begin();
            //debug.Draw(spriteBatch);
            sppointer.Draw(Data, spriteBatch, gameTime, new Vector2(Mouse.GetState().X, Mouse.GetState().Y));

            spriteBatch.End();
            //End

            base.Draw(gameTime);
        }
    }
}
