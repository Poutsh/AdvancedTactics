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
        TimeSpan time;

        public TurnbyTurn(Data data, Cell[,] map, Match Match)
        {
            Data = data;
            Map = map;
            Stats = new Stats(Data);

            Message = new Message();
            Match.TurnState = Match.Turn.Player1;
            Match.inTurnState = Match.inTurn.Start;
            PlayerTurn = Match.Players[0];
            Match.IA.IAPlayer = Match.Players[0];
        }

        public void UpdateTurn(SpriteBatch spriteBatch, GameTime gameTime, Match Match, Viseur Viseur, List<Unit> ListToDraw)
        {
            Message.Update(gameTime);
            float tempo = 1f;
            
            if ((Inputs.Keyr(Keys.Space) || MvtCount >= Match.NumberMvtPerTurn) && gameTime.TotalGameTime - time > TimeSpan.FromSeconds(tempo))
            {
                time = gameTime.TotalGameTime;
                MvtCount = 0;
                if (Match.TurnState == Match.Turn.Player1)
                {
                    Match.TurnState = Match.Turn.Player2;
                    PlayerTurn.Money += 10;
                    PlayerTurn = Match.Players[1];
                    Match.IA.IAPlayer = Match.Players[1];
                }
                else
                {
                    PlayerTurn.Money += 10;
                    Match.TurnState = Match.Turn.Player1;
                    PlayerTurn = Match.Players[0];
                    Match.IA.IAPlayer = Match.Players[0];
                }

                for (int i = 0; i < Match.Players.Count; i++)
                {
                    for (int j = 0; j < Match.Players[i].UnitOfPlayer.Count; j++)
                    {
                        Match.Players[i].UnitOfPlayer[j].MvtPossible = Stats.Possible(Match.Players[i].UnitOfPlayer[j], Map, Data, Match).Item1;
                        Match.Players[i].UnitOfPlayer[j].AttackPossible = Stats.Possible(Match.Players[i].UnitOfPlayer[j], Map, Data, Match).Item2;
                    }
                }

                Message.Messages.Add(new DisplayMessage(PlayerTurn.PlayerName, TimeSpan.FromSeconds(0.9), new Vector2(Map[Data.MapWidth / 2, Data.MapHeight / 2].positionPixel.X - Message.font.MeasureString(PlayerTurn.PlayerName).X / 2, Map[Data.MapWidth / 2, Data.MapHeight / 2].positionPixel.Y), PlayerTurn.ColorSide));
                Match.LoadShop();
            }
        }

        public void DrawTurn(GameTime gameTime, SpriteBatch spriteBatch, Match Match)
        {
            Message.Draw(spriteBatch);
        }
    }
}
