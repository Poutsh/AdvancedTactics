using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Advanced_Tactics
{
    public class TurnbyTurn
    {
        Data Data;
        public Player PlayerTurn;
        public Message Message;
        Cell[,] Map;
        Stats Stats;
        public int MvtCount = 0;

        public TurnbyTurn(Data data, Cell[,] map, Match Match)
        {
            Data = data;
            Map = map;
            Stats = new Stats(Data);

            Message = new Message();
            Match.TurnState = Match.Turn.Player1;
            Match.inTurnState = Match.inTurn.Start;
            PlayerTurn = Match.Players[0];

        }

        public void UpdateTurn(SpriteBatch spriteBatch, GameTime gameTime, Match Match, Viseur Viseur, List<Unit> ListToDraw)
        {
            Message.Update(gameTime);

            if (Inputs.Keyr(Keys.Space) || Match.NumberMvtPerTurn == MvtCount)
            {
                MvtCount = 0;
                if (Match.TurnState == Match.Turn.Player1)
                {
                    Match.TurnState = Match.Turn.Player2;
                    PlayerTurn = Match.Players[1];
                }
                else
                {
                    Match.TurnState = Match.Turn.Player1;
                    PlayerTurn = Match.Players[0];
                }
                Message.Messages.Add(new DisplayMessage(PlayerTurn.PlayerName, TimeSpan.FromSeconds(2.0), new Vector2(Map[Data.MapWidth / 2, Data.MapHeight / 2].positionPixel.X - Message.font.MeasureString("Player 1").X / 2, Map[Data.MapWidth / 2, Data.MapHeight / 2].positionPixel.Y), PlayerTurn.ColorSide));
            }
        }

        public void DrawTurn(GameTime gameTime, SpriteBatch spriteBatch, Match Match)
        {
            Message.Draw(spriteBatch);
        }
    }
}
