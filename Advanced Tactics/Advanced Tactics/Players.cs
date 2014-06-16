using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Advanced_Tactics
{
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

        public int Score;

        public List<Unit> UnitOfPlayer { get; set; }
        public Color ColorSide { get; set; }
        public string ColorSideN { get; set; }
        public string PlayerName { get { return "Player " + playernumber; } set { value = PlayerName; } }
        public int playernumber;
        public int Money;

        public Player(Data data)
        {
            Data = data;
            Money = 40;
            Score = 0;
            UnitOfPlayer = new List<Unit>();
            StartZone = new List<Vector>();
            ColorStartZoneSprite = new Sprite();
        }
    }
}
