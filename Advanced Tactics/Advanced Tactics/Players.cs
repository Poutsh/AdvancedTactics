using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Advanced_Tactics
{
    class Partie
    {
        Data data;
        int x, y;
        Players Player1, Player2;
        public List<Vector> HQ1, HQ2;

        public Partie(Data data)
        {
            this.data = data;
            HQ1 = StartPoss();
            HQ2 = StartPoss2();
        }

        List<Vector> StartPoss()
        {
            List<Vector> StartPos = new List<Vector>();
            Random rrd = new Random();

            x = rrd.Next(0, data.WidthMap);
            y = rrd.Next(0, data.HeightMap);

            for (int i = 0; i < data.WidthMap; i++)
            {
                for (int j = 0; j < data.HeightMap; j++)
                {
                    if (Vector.Distance(new Vector(x, y), new Vector(i, j)) <= 5)
                        StartPos.Add(new Vector(i, j));
                }
            }

            return StartPos;
        }

        List<Vector> StartPoss2()
        {
            List<Vector> StartPos = new List<Vector>();
            int xx = 0, yy = 0;
            Random rrd = new Random();

            while (Vector.Distance(new Vector(x, y), new Vector(xx, yy)) <= 6)
            {
                xx = rrd.Next(0, data.WidthMap);
                yy = rrd.Next(0, data.HeightMap);
            }

            for (int i = 0; i < data.WidthMap; i++)
            {
                for (int j = 0; j < data.HeightMap; j++)
                {
                    if (Vector.Distance(new Vector(xx, yy), new Vector(i, j)) <= 5)
                        StartPos.Add(new Vector(i, j));
                }
            }

            return StartPos;
        }

    }


    class Players
    {
        Data data;
        Unit unit;
        List<Unit> UnitsOfPlayer;



        public Rectangle Limits { get; set; }

        string[] arrayrang = new string[] { "AA", "Commando", "Doc", "Engineer", "Plane", "Pvt", "Tank", "Truck" };
        string[] arrayclasse = new string[] { "Queen", "Rook", "Bishop", "Knight", "Pawn" };


        /// <summary>
        /// Gestion de Joueur
        /// </summary>
        /// <param name="Number">Nombre de joueurs</param>
        public Players(Data data, Map map)
        {
            UnitsOfPlayer = new List<Unit>();


        }

    }
}
