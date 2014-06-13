using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Advanced_Tactics
{
    public class Match
    {
        Data Data;

        public Turn TurnState;
        public enum Turn { Player1, Player2 }

        public inTurn inTurnState;
        public enum inTurn { Start, Mid, End }

        public bool canStart;
        public Player PlayerTurn { get { return TurnbyTurn.PlayerTurn; } }

        public int TurnNumber;
        public TurnbyTurn TurnbyTurn;
        public int NumberMvtPerTurn;

        List<Color> ColorSide = new List<Color>(2) { Color.Blue, Color.Red };
        List<string> ColorSideName = new List<string>(2) { "B", "R" };
        public List<Player> Players = new List<Player>();
        Cell[,] Map;

        public Match(Data data, int number, Map map)
        {
            Data = data;
            Map = map.Carte;
            NumberMvtPerTurn = 2;

            Random random = new Random();
            Vector temp = new Vector(random.Next(0, Data.WidthMap + 1), random.Next(0, Data.HeightMap + 1));
            Vector temp2 = new Vector(random.Next(0, Data.WidthMap + 1), random.Next(0, Data.HeightMap + 1));

            while (Vector.Distance(temp, temp2) <= 6) { temp2 = new Vector(random.Next(0, Data.WidthMap + 1), random.Next(0, Data.HeightMap + 1)); }

            List<Vector> Temp = new List<Vector>(2) { temp, temp2 };

            for (int i = 0; i < number; i++)
            {
                Players.Add(new Player(Data));
                Players[i].playernumber = i + 1;
                Players[i].ColorSide = ColorSide[i];
                Players[i].ColorSideN = ColorSideName[i];
                Players[i].StartZoneCenter = Temp[i];
                Players[i].StartZone = DrawStartZone(Players[i]);
                Players[i].ColorStartZoneSprite.LC(Game1.Ctt, "Case/" + ColorSideName[i]);
            }

            TurnNumber = 0;
            TurnbyTurn = new TurnbyTurn(Data, Map, this);
        }

        public List<Vector> DrawStartZone(Player Player)
        {
            for (int i = 0; i < Data.WidthMap; i++)
            {
                for (int j = 0; j < Data.HeightMap; j++)
                {
                    /// Verifie si la distance entre les deux positions de depart des HQ est bien superieur a 6 cases
                    if (Vector.Distance(Player.StartZoneCenter, new Vector(i, j)) <= 5)
                        Player.StartZone.Add(new Vector(i, j));
                }
            }
            return Player.StartZone;
        }

        bool once;
        public void Update(GameTime gameTime, SpriteBatch spriteBatch, Viseur Viseur, List<Unit> ListToDraw)
        {
            if (canStart)
            {
                TurnbyTurn.UpdateTurn(spriteBatch, gameTime, this, Viseur, ListToDraw);
            }
            else
            {
                TurnbyTurn.Message.Update(gameTime);

                if (Players[0].HQ != null && Players[1].HQ != null)
                {
                    canStart = true;
                }
                else if (Players[0].HQ == null)
                {
                    if (!once) TurnbyTurn.Message.Messages.Add(new DisplayMessage(PlayerTurn.PlayerName, TimeSpan.FromSeconds(1.5), new Vector2(Map[Data.WidthMap / 2, Data.HeightMap / 2].positionPixel.X - TurnbyTurn.Message.font.MeasureString("Player 1").X / 2, Map[Data.WidthMap / 2, Data.HeightMap / 2].positionPixel.Y), PlayerTurn.ColorSide));
                    if (!once && (Inputs.Keyd(Keys.Left) || Inputs.Keyd(Keys.Right) || Inputs.Keyd(Keys.Down) || Inputs.Keyd(Keys.Up))) { once = true; }
                    if (TurnbyTurn.PlayerTurn.StartZone.Contains(Viseur.coordViseur2) && Inputs.Keyr(Keys.Enter))
                    {
                        TurnbyTurn.PlayerTurn.HQ = new Unit(Data, TurnbyTurn.PlayerTurn.ColorSideN + "HQ", "King", Map, Viseur.viseurX, Viseur.viseurY, ListToDraw, TurnbyTurn.PlayerTurn);
                        TurnbyTurn.PlayerTurn = Players[1];
                    }
                }
                else if (Players[1].HQ == null)
                {
                    if (once) TurnbyTurn.Message.Messages.Add(new DisplayMessage(PlayerTurn.PlayerName, TimeSpan.FromSeconds(1.5), new Vector2(Map[Data.WidthMap / 2, Data.HeightMap / 2].positionPixel.X - TurnbyTurn.Message.font.MeasureString("Player 1").X / 2, Map[Data.WidthMap / 2, Data.HeightMap / 2].positionPixel.Y), PlayerTurn.ColorSide));
                    if (once && (Inputs.Keyd(Keys.Left) || Inputs.Keyd(Keys.Right) || Inputs.Keyd(Keys.Down) || Inputs.Keyd(Keys.Up))) { once = false; }
                    if (TurnbyTurn.PlayerTurn.StartZone.Contains(Viseur.coordViseur2) && Inputs.Keyr(Keys.Enter))
                    {
                        TurnbyTurn.PlayerTurn.HQ = new Unit(Data, TurnbyTurn.PlayerTurn.ColorSideN + "HQ", "King", Map, Viseur.viseurX, Viseur.viseurY, ListToDraw, TurnbyTurn.PlayerTurn);
                        TurnbyTurn.PlayerTurn = Players[0];
                        TurnbyTurn.Message.Messages.Add(new DisplayMessage(PlayerTurn.PlayerName, TimeSpan.FromSeconds(1.5), new Vector2(Map[Data.WidthMap / 2, Data.HeightMap / 2].positionPixel.X - TurnbyTurn.Message.font.MeasureString("Player 1").X / 2, Map[Data.WidthMap / 2, Data.HeightMap / 2].positionPixel.Y), PlayerTurn.ColorSide));
                    }
                }
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            TurnbyTurn.DrawTurn(gameTime, spriteBatch, this);
        }
    }

    public class TurnbyTurn
    {
        Data Data;
        public Player PlayerTurn;
        public Message Message;
        Cell[,] Map;
        public int MvtCount = 0;

        public TurnbyTurn(Data data, Cell[,] map, Match Match)
        {
            Data = data;
            Map = map;

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
                Message.Messages.Add(new DisplayMessage(PlayerTurn.PlayerName, TimeSpan.FromSeconds(2.0), new Vector2(Map[Data.WidthMap / 2, Data.HeightMap / 2].positionPixel.X - Message.font.MeasureString("Player 1").X / 2, Map[Data.WidthMap / 2, Data.HeightMap / 2].positionPixel.Y), PlayerTurn.ColorSide));
            }
        }

        public void DrawTurn(GameTime gameTime, SpriteBatch spriteBatch, Match Match)
        {
            Message.Draw(spriteBatch);
        }
    }


    public class Player
    {
        //string[] arrayrang = new string[] { "AA", "Commando", "Doc", "Engineer", "Plane", "Pvt", "Tank", "Truck" };
        //string[] arrayclasse = new string[] { "Queen", "Rook", "Bishop", "Knight", "Pawn" };
        //TurnbyTurn.PlayerTurn.HQ = new Unit(Data, TurnbyTurn.PlayerTurn.ColorSideN + "HQ", "King", Map, Viseur.viseurX, Viseur.viseurY, ListToDraw, TurnbyTurn.PlayerTurn);
        Data Data;
        public List<Vector> StartZone { get; set; }
        public Vector StartZoneCenter { get; set; }
        public Unit HQ;
        public Sprite ColorStartZoneSprite;


        public List<Unit> UnitOfPlayer { get; set; }
        public Color ColorSide { get; set; }
        public string ColorSideN { get; set; }
        public string PlayerName { get { return "Player " + playernumber; } set { value = PlayerName; } }
        public int playernumber;

        public Player(Data data)
        {
            Data = data;
            UnitOfPlayer = new List<Unit>();
            StartZone = new List<Vector>();
            ColorStartZoneSprite = new Sprite();
        }
    }
}
