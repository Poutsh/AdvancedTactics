using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Advanced_Tactics
{
    class Partie
    {


        Data data;
        int x, y;

        Viseur viseur;

        public Partie(Data data, Viseur viseur, Map map, List<Player> Players)
        {
            this.data = data;

            Random rrd = new Random();

            x = rrd.Next(0, data.WidthMap);
            y = rrd.Next(0, data.HeightMap);

            for (int i = 0; i < Players.Count(); i++) Players[i] = new Player();
            for (int i = 0; i < Players.Count(); i++)
            {
                Players[i].couleur = i.ToString();
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
                    if (Vector.Distance(new Vector(x, y), new Vector(i, j)) <= 5)
                        StartPos.Add(new Vector(i, j));
                }
            }

            return Tuple.Create(StartPos, Center);
        }





    }


    class Player
    {
        public enum Key { Q, W, A, Z, LeftControl, LeftShift, R, C, Enter }
        private KeyboardState oldKey, curKey;
        Data data;
        public List<Vector> StartZone;
        public Vector CenterZone;
        public Unit HQ;
        public int HQmax;
        public string couleur;
        public bool Create { get { return HQ != null; } set { value = HQ != null; } }


        public Rectangle Limits { get; set; }

        string[] arrayrang = new string[] { "AA", "Commando", "Doc", "Engineer", "Plane", "Pvt", "Tank", "Truck" };
        string[] arrayclasse = new string[] { "Queen", "Rook", "Bishop", "Knight", "Pawn" };


        public void PosHQ(Viseur viseur, Map map, Data data, List<Unit> ListToDraw)
        {
            if (StartZone.Contains(viseur.coordViseur2) && WasJustPressed(Key.Enter) &&  HQmax < 1)
            {
                HQ = new Unit(data, couleur+"HQ", "King", map.Carte, viseur.viseurX, viseur.viseurY, ListToDraw, couleur);
                ++HQmax;
            }
        }
        private bool WasJustPressed(Key button)
        {
            curKey = Keyboard.GetState();
            switch (button)
            {
                case Key.Enter:
                    return curKey.IsKeyDown(Keys.Enter) && oldKey != curKey;

                case Key.Q:
                    return curKey.IsKeyDown(Keys.Q) && oldKey != curKey;

                case Key.C:
                    return curKey.IsKeyDown(Keys.C) && oldKey != curKey;

                case Key.W:
                    return curKey.IsKeyDown(Keys.W) && oldKey != curKey;

                case Key.A:
                    return curKey.IsKeyDown(Keys.A) && oldKey != curKey;

                case Key.Z:
                    return curKey.IsKeyDown(Keys.Z) && oldKey != curKey;

                case Key.LeftControl:
                    return curKey.IsKeyDown(Keys.LeftControl) && oldKey != curKey;

                case Key.R:
                    return curKey.IsKeyDown(Keys.R) && oldKey != curKey;
            }
            oldKey = curKey;
            return false;
        }

    }
}
