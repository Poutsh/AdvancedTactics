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

        Players Player1, Player2;
        Vector HQ1;
        Vector HQ2;
        

        public Partie(Data data)
        {
            this.data = data;

            
            
        }

        void StartPoss(Vector _HQ1, Vector HQ2)
        {
            List<Vector> StartPos = new List<Vector>();

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

            //HQ1 = new Vector(StartPos[rrd.Next(0, StartPos.Count)]);
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
