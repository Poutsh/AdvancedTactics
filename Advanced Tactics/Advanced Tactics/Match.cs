using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Advanced_Tactics
{
    public class Match
    {
        Data Data;
        TimeSpan time, time2;
        public Turn TurnState;
        public enum Turn { Player1, Player2 }

        public inTurn inTurnState;
        public enum inTurn { Start, Mid, End }

        public bool canStart;
        public Player PlayerTurn { get { return TurnbyTurn.PlayerTurn; } }
        public Player Winner;

        public int TurnNumber;
        public TurnbyTurn TurnbyTurn;
        public int NumberMvtPerTurn;

        List<Color> ColorSide = new List<Color>(2) { Color.Blue, Color.Red };
        List<string> ColorSideName = new List<string>(2) { "B", "R" };
        public List<Player> Players = new List<Player>();
        Cell[,] Map;
        Stats stats;

        SpriteFont font;
        public Sprite AA, Commando, Doc, Engineer, Plane, Pvt, Tank, Truck, Viseurjaune;
        public Sprite Queen, Rook, Bishop, Knight, Pawn;



        public Match(Data data, int number, Map map)
        {
            Data = data;
            Map = map.Carte;
            NumberMvtPerTurn = 2;
            stats = new Stats(Data);
            font = Game1.Ctt.Load<SpriteFont>("info");

            AA = new Sprite(); Commando = new Sprite(); Doc = new Sprite(); Engineer = new Sprite(); Plane = new Sprite(); Pvt = new Sprite(); Tank = new Sprite(); Truck = new Sprite();
            Queen = new Sprite(); Rook = new Sprite(); Bishop = new Sprite(); Knight = new Sprite(); Pawn = new Sprite();

            Random random = new Random();
            Vector temp = new Vector(random.Next(0, Data.MapWidth), random.Next(0, Data.MapHeight));
            Vector temp2 = new Vector(random.Next(0, Data.MapWidth), random.Next(0, Data.MapHeight));

            while (Vector.Distance(temp, temp2) <= 6) { temp2 = new Vector(random.Next(0, Data.MapWidth), random.Next(0, Data.MapHeight)); }

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
            for (int i = 0; i < Data.MapWidth; i++)
            {
                for (int j = 0; j < Data.MapHeight; j++)
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
                if (Players[0].Score >= 100000)
                {
                    Winner = Players[0];
                }
                else if (Players[1].Score >= 100000)
                {
                    Winner = Players[1];
                }
                else
                {
                    TurnbyTurn.UpdateTurn(spriteBatch, gameTime, this, Viseur, ListToDraw);
                }
            }
            else
            {
                TurnbyTurn.Message.Update(gameTime);

                float tempo = 0.5f;
                float tempo2 = 1f;


                if (Players[0].HQ != null && Players[1].HQ != null)
                {
                    canStart = true;
                }
                else if (Players[0].HQ == null)
                {
                    if (!once && gameTime.TotalGameTime - time > TimeSpan.FromSeconds(tempo2)) { time = gameTime.TotalGameTime; Viseur.coordViseur = PlayerTurn.StartZoneCenter.ToVector2(); TurnbyTurn.Message.Messages.Add(new DisplayMessage(PlayerTurn.PlayerName, TimeSpan.FromSeconds(0.9), new Vector2(Map[Data.MapWidth / 2, Data.MapHeight / 2].positionPixel.X - TurnbyTurn.Message.font.MeasureString("Player 1").X / 2, Map[Data.MapWidth / 2, Data.MapHeight / 2].positionPixel.Y), PlayerTurn.ColorSide)); }
                    if (!once && gameTime.TotalGameTime - time > TimeSpan.FromSeconds(tempo)) { time = gameTime.TotalGameTime; once = true; }
                    if (TurnbyTurn.PlayerTurn.StartZone.Contains(Viseur.coordViseur2) && (Inputs.Keyr(Keys.Enter) || Inputs.doubleclick(gameTime)))
                    {
                        TurnbyTurn.PlayerTurn.HQ = new Unit(Data, "HQ", "King", Map, Viseur.viseurX, Viseur.viseurY, TurnbyTurn.PlayerTurn, this);
                        TurnbyTurn.PlayerTurn = Players[1];
                    }
                }
                else if (Players[1].HQ == null)
                {
                    if (once && gameTime.TotalGameTime - time > TimeSpan.FromSeconds(tempo2)) { time = gameTime.TotalGameTime; Viseur.coordViseur = PlayerTurn.StartZoneCenter.ToVector2(); TurnbyTurn.Message.Messages.Add(new DisplayMessage(PlayerTurn.PlayerName, TimeSpan.FromSeconds(0.9), new Vector2(Map[Data.MapWidth / 2, Data.MapHeight / 2].positionPixel.X - TurnbyTurn.Message.font.MeasureString("Player 1").X / 2, Map[Data.MapWidth / 2, Data.MapHeight / 2].positionPixel.Y), PlayerTurn.ColorSide)); }
                    if (once && gameTime.TotalGameTime - time > TimeSpan.FromSeconds(tempo)) { time = gameTime.TotalGameTime; once = false; }
                    if (TurnbyTurn.PlayerTurn.StartZone.Contains(Viseur.coordViseur2) && (Inputs.Keyr(Keys.Enter) || Inputs.doubleclick(gameTime)))
                    {
                        TurnbyTurn.PlayerTurn.HQ = new Unit(Data, "HQ", "King", Map, Viseur.viseurX, Viseur.viseurY, TurnbyTurn.PlayerTurn, this);
                        TurnbyTurn.PlayerTurn = Players[0];
                        TurnbyTurn.Message.Messages.Add(new DisplayMessage(PlayerTurn.PlayerName, TimeSpan.FromSeconds(0.9), new Vector2(Map[Data.MapWidth / 2, Data.MapHeight / 2].positionPixel.X - TurnbyTurn.Message.font.MeasureString("Player 1").X / 2, Map[Data.MapWidth / 2, Data.MapHeight / 2].positionPixel.Y), PlayerTurn.ColorSide));
                    }
                }
            }

            for (int i = 0; i < Players.Count; i++)
            {
                for (int j = 0; j < Players[i].UnitOfPlayer.Count; j++)
                {
                    Players[i].UnitOfPlayer[j].MvtPossible = stats.Possible(Players[i].UnitOfPlayer[j], Map, Data, this).Item1;
                    Players[i].UnitOfPlayer[j].AttackPossible = stats.Possible(Players[i].UnitOfPlayer[j], Map, Data, this).Item2;
                }
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            TurnbyTurn.DrawTurn(gameTime, spriteBatch, this);
        }

        public void LoadShop()
        {
            AA.LC(Game1.Ctt, "Unit/" + PlayerTurn.ColorSideN + "AA");
            Commando.LC(Game1.Ctt, "Unit/" + PlayerTurn.ColorSideN + "Commando");
            Doc.LC(Game1.Ctt, "Unit/" + PlayerTurn.ColorSideN + "Doc");
            Engineer.LC(Game1.Ctt, "Unit/" + PlayerTurn.ColorSideN + "Engineer");
            Plane.LC(Game1.Ctt, "Unit/" + PlayerTurn.ColorSideN + "Plane");
            Pvt.LC(Game1.Ctt, "Unit/" + PlayerTurn.ColorSideN + "Pvt");
            Tank.LC(Game1.Ctt, "Unit/" + PlayerTurn.ColorSideN + "Tank");
            Truck.LC(Game1.Ctt, "Unit/" + PlayerTurn.ColorSideN + "Truck");

            Queen.LC(Game1.Ctt, "Unit/pdame");
            Rook.LC(Game1.Ctt, "Unit/ptour");
            Bishop.LC(Game1.Ctt, "Unit/pfou");
            Knight.LC(Game1.Ctt, "Unit/pcav");
            Pawn.LC(Game1.Ctt, "Unit/ppion");
            
        }

        public void Shop(SpriteBatch spriteBatch, Viseur viseur)
        {
            AA.Draw(Data, spriteBatch, new Vector2(17, 550), 1.2f);
            Commando.Draw(Data, spriteBatch, new Vector2(17, 590), 1.2f);
            Doc.Draw(Data, spriteBatch, new Vector2(17, 630), 1.2f);
            Engineer.Draw(Data, spriteBatch, new Vector2(17, 670), 1.2f);
            Plane.Draw(Data, spriteBatch, new Vector2(80, 550), 1.2f);
            Pvt.Draw(Data, spriteBatch, new Vector2(80, 590), 1.2f);
            Tank.Draw(Data, spriteBatch, new Vector2(80, 630), 1.2f);
            Truck.Draw(Data, spriteBatch, new Vector2(80, 670), 1.2f);

            Queen.Draw(Data, spriteBatch, new Vector2(150, 555));
            Rook.Draw(Data, spriteBatch, new Vector2(150, 595));
            Bishop.Draw(Data, spriteBatch, new Vector2(150, 635));
            Knight.Draw(Data, spriteBatch, new Vector2(150, 675));
            Pawn.Draw(Data, spriteBatch, new Vector2(200, 555));

            viseur.AA = new Rectangle(AA.Position.X, AA.Position.Y, (int)(AA.Texture.Width * 1.2f), (int)(AA.Texture.Height * 1.2f));
            viseur.Commando = new Rectangle(Commando.Position.X, Commando.Position.Y, (int)(Commando.Texture.Width * 1.2f), (int)(Commando.Texture.Height * 1.2f));
            viseur.Doc = new Rectangle(Doc.Position.X, Doc.Position.Y, (int)(Doc.Texture.Width * 1.2f), (int)(Doc.Texture.Height * 1.2f));
            viseur.Engineer = new Rectangle(Engineer.Position.X, Engineer.Position.Y, (int)(Engineer.Texture.Width * 1.2f), (int)(Engineer.Texture.Height * 1.2f));
            viseur.Plane = new Rectangle(Plane.Position.X, Plane.Position.Y, (int)(Plane.Texture.Width * 1.2f), (int)(Plane.Texture.Height * 1.2f));
            viseur.Pvt = new Rectangle(Pvt.Position.X, Pvt.Position.Y, (int)(Pvt.Texture.Width * 1.2f), (int)(Pvt.Texture.Height * 1.2f));
            viseur.Tank = new Rectangle(Tank.Position.X, Tank.Position.Y, (int)(Tank.Texture.Width * 1.2f), (int)(Tank.Texture.Height * 1.2f));
            viseur.Truck = new Rectangle(Truck.Position.X, Truck.Position.Y, (int)(Truck.Texture.Width * 1.2f), (int)(Truck.Texture.Height * 1.2f));


            viseur.Queen = new Rectangle(Queen.Position.X, Queen.Position.Y, (int)(Queen.Texture.Width), (int)(Queen.Texture.Height));
            viseur.Rook = new Rectangle(Rook.Position.X, Rook.Position.Y, (int)(Rook.Texture.Width), (int)(Rook.Texture.Height));
            viseur.Bishop = new Rectangle(Bishop.Position.X, Bishop.Position.Y, (int)(Bishop.Texture.Width), (int)(Bishop.Texture.Height));
            viseur.Knight = new Rectangle(Knight.Position.X, Knight.Position.Y, (int)(Knight.Texture.Width), (int)(Knight.Texture.Height));
            viseur.Pawn = new Rectangle(Pawn.Position.X, Pawn.Position.Y, (int)(Pawn.Texture.Width), (int)(Pawn.Texture.Height));
        }
    }
}
