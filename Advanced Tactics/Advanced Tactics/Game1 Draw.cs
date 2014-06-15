using System;
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

            switch (currentGameState)
            {
                case GameState.Menu:
                    spriteBatch.Begin();
                    menu.Draw(spriteBatch, gameTime);
                    spriteBatch.End();
                    break;


                case GameState.Option:
                    goto case GameState.Menu;


                case GameState.Game:
                    Informations.Draw(spriteBatch, gameTime);

                    spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
                    tileMap.Draw(spriteBatch);

                    if (!Match.canStart)
                    {
                        for (int j = 0; j < Match.PlayerTurn.StartZone.Count; j++)
                            Match.PlayerTurn.ColorStartZoneSprite.Draw(Data, spriteBatch, map.Carte[Match.PlayerTurn.StartZone[j].X, Match.PlayerTurn.StartZone[j].Y].positionPixel);
                    }



                    for (int i = 0; i < Match.Players[0].UnitOfPlayer.Count(); i++) Match.Players[0].UnitOfPlayer[i].DrawUnit(spriteBatch, gameTime);
                    for (int i = 0; i < Match.Players[1].UnitOfPlayer.Count(); i++) Match.Players[1].UnitOfPlayer[i].DrawUnit(spriteBatch, gameTime);

                    spriteBatch.End();

                    spriteBatch.Begin();
                    debug.Draw(spriteBatch);
                    viseur.Draw(spriteBatch, gameTime);
                    spriteBatch.End();

                    spriteBatch.Begin();
                    Match.Draw(gameTime, spriteBatch);
                    spriteBatch.End();
                    break;


                case GameState.GameStart:
                    break;


                case GameState.Exit:
                    break;
            }

            spriteBatch.Begin();
            //debug.Draw(spriteBatch);
            sppointer.Draw(Data, spriteBatch, new Vector2(Mouse.GetState().X, Mouse.GetState().Y));

            spriteBatch.End();

            //if (menu.InGame) // IN GAME
            //{
            //    //Informations.Draw(spriteBatch, gameTime);

            //    spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            //    tileMap.Draw(spriteBatch);

            //    //for (int j = 0; j < Players.Count(); j++)
            //    //    for (int i = 0; i < Players[j].StartZone.Count(); i++)
            //    //        if (Players[j].HQmax < 1) Players[j].spriteStartZone.Draw(Data, spriteBatch,  map.Carte[Players[j].StartZone[i].X, Players[j].StartZone[i].Y].positionPixel);

            //    for (int i = 0; i < ListToDraw.Count(); i++) ListToDraw[i].DrawUnit(spriteBatch, gameTime);

            //    spriteBatch.End();

            //    spriteBatch.Begin();
            //    debug.Draw(spriteBatch);
            //    viseur.Draw(spriteBatch, gameTime);
            //    spriteBatch.End();

            //    spriteBatch.Begin();
            //    Match.Draw(spriteBatch);
            //    spriteBatch.End();
            //}
            //else // MENU
            //{
            //    menu.Draw(spriteBatch, gameTime);
            //}

            ////Begin DEBUG
            //spriteBatch.Begin();
            ////debug.Draw(spriteBatch);
            //sppointer.Draw(Data, spriteBatch, new Vector2(Mouse.GetState().X, Mouse.GetState().Y));

            //spriteBatch.End();
            //End

            base.Draw(gameTime);
        }
    }
}
