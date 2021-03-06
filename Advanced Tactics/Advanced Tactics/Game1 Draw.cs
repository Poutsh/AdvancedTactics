﻿using System;
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
            GraphicsDevice.Clear(Color.White);
            switch (currentGameState)
            {
                case GameState.Menu:
                    menu.Draw(spriteBatch, gameTime);
                    break;


                case GameState.Option:
                    menu.Draw(spriteBatch, gameTime);
                    break;


                case GameState.Loading:
                    GraphicsDevice.Clear(Color.Black);
                    spriteBatch.Begin();
                    message.Draw(spriteBatch);
                    spriteBatch.End();
                    return;


                case GameState.GameMulti:
                    Info.Draw(spriteBatch, gameTime, Match, viseur);
                    spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
                    tileMap.Draw(spriteBatch);

                    if (!Match.canStart)
                    {
                        for (int j = 0; j < Match.PlayerTurn.StartZone.Count; j++)
                            Match.PlayerTurn.ColorStartZoneSprite.Draw(Data, spriteBatch, map.Carte[Match.PlayerTurn.StartZone[j].X, Match.PlayerTurn.StartZone[j].Y].positionPixel);
                    }


                    for (int i = 0; i < Match.Players[0].UnitOfPlayer.Count(); i++) Match.Players[0].UnitOfPlayer[i].DrawUnit(spriteBatch, gameTime);
                    for (int i = 0; i < Match.Players[1].UnitOfPlayer.Count(); i++) Match.Players[1].UnitOfPlayer[i].DrawUnit(spriteBatch, gameTime);

                    Match.Shop(spriteBatch, viseur);
                    spriteBatch.End();

                    spriteBatch.Begin();
                    //debug.Draw(spriteBatch);
                    viseur.Draw(spriteBatch, gameTime);
                    spriteBatch.End();

                    spriteBatch.Begin();
                    Match.Draw(gameTime, spriteBatch);
                    spriteBatch.End();

                    spriteBatch.Begin();
                    message.Draw(spriteBatch);
                    spriteBatch.End();
                    break;


                case GameState.Winner:
                    GraphicsDevice.Clear(Color.Black);
                    spriteBatch.Begin();
                    message.Draw(spriteBatch);
                    spriteBatch.End();
                    break;

                case GameState.ExitConfirm:
                    GraphicsDevice.Clear(Color.Black);
                    spriteBatch.Begin();
                    message.Draw(spriteBatch);
                    spriteBatch.End();
                    break;
            }

            spriteBatch.Begin();
            sppointer.Draw(Data, spriteBatch, new Vector2(Mouse.GetState().X, Mouse.GetState().Y));
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
