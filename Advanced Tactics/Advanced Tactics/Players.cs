using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Advanced_Tactics
{
    public class Partie
    {
        Data data;
        int x, y;

        public Partie(Data data, Viseur viseur, Map map, List<Player> Players)
        {
            this.data = data;

            Random rrd = new Random();

            x = rrd.Next(0, data.WidthMap);
            y = rrd.Next(0, data.HeightMap);

            for (int i = 0; i < Players.Count(); i++) Players[i] = new Player();
            Players[0].Color = "B";
            Players[1].Color = "R";
            for (int i = 0; i < Players.Count(); i++)
            {
                Players[i].StartZone = StartPoss(Players, rrd.Next(0, data.WidthMap), rrd.Next(0, data.HeightMap)).Item1;
                Players[i].CenterZone = StartPoss(Players, rrd.Next(0, data.WidthMap), rrd.Next(0, data.HeightMap)).Item2;
            }

        }

        Tuple<List<Vector>, Vector> StartPoss(List<Player> Players, int x, int y)
        {
            List<Vector> StartPos = new List<Vector>();

            Vector Center = new Vector(x, y);

            for (int i = 0; i < data.WidthMap; i++)
            {
                for (int j = 0; j < data.HeightMap; j++)
                {
                    /// Verifie si la distance entre les deux positions de depart des HQ est bien superieur a 6 cases
                    if (Vector.Distance(new Vector(x, y), new Vector(i, j)) <= 5)
                        StartPos.Add(new Vector(i, j));
                }
            }

            return Tuple.Create(StartPos, Center);
        }
    }


    public class Player
    {
        public Sprite spriteStartZone;
        public List<Vector> StartZone;
        public Vector CenterZone;
        public Unit HQ;
        public int HQmax;
        public string Color;
        public bool Create { get { return HQ != null; } set { value = HQ != null; } }


        public Rectangle Limits { get; set; }

        string[] arrayrang = new string[] { "AA", "Commando", "Doc", "Engineer", "Plane", "Pvt", "Tank", "Truck" };
        string[] arrayclasse = new string[] { "Queen", "Rook", "Bishop", "Knight", "Pawn" };


        public Player() { spriteStartZone = new Sprite(); spriteStartZone.LC(Game1.Ctt, "Case/bleu"); }


        public void PosHQ(Viseur viseur, Map map, Data data, List<Unit> ListToDraw)
        {
            if (StartZone.Contains(viseur.coordViseur2) && Inputs.Keyr(Keys.Enter) && HQmax < 1)
            {
                HQ = new Unit(data, Color + "HQ", "King", map.Carte, viseur.viseurX, viseur.viseurY, ListToDraw, this);
                ++HQmax;
            }
        }
    }
}
