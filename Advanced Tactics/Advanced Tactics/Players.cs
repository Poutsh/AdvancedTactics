using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Advanced_Tactics
{
    class Players
    {
        Data data;
        Unit unit;
        List<Unit> UnitsOfPlayer;
        List<Vector> StartPos;

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

            Random rrd = new Random();
            int x = rrd.Next(0, data.WidthMap);
            int y = rrd.Next(0, data.HeightMap);

            for (int i = 0; i < data.WidthMap; i++)
            {
                for (int j = 0; j < data.HeightMap; j++)
                {
                    if (Vector.Distance(new Vector(x, y), new Vector(i, j)) <= 5)
                        StartPos.Add(new Vector(i, j));
                }
            }
        }

    }
}
