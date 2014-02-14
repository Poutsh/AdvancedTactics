using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Advanced_Tactics
{
    class Unit
    {
        private string nom;
        private string rang;
        private float niveau;
        private int pos_x;
        private int pos_y;


        public static void unite(string nom, float niveau, string rang)
        {
            float prix = 1;
            float force = 1;
            float pv = 1;
            switch (nom)
            {
                case "troupe" :
                    prix = 100 + 100 * (float)Math.Pow(1.5f, niveau);
                    force = 10 + 10 * (float)Math.Pow(1.2f, niveau);
                    pv = 50 + 50 * (float)Math.Pow(1.2f, niveau);
                    break;
                case "commando" :
                    prix = 500 + 500 * (float)Math.Pow(1.5f, niveau);
                    force = 30 + 30 * (float)Math.Pow(1.2f, niveau);
                    pv = 100 + 100 * (float)Math.Pow(1.2f, niveau);
                    break;
                case "tank":
                    prix = 1200 + 1200 * (float)Math.Pow(1.5f, niveau);
                    force = 90 + 90 * (float)Math.Pow(1.2f, niveau);
                    pv = 300 + 300 * (float)Math.Pow(1.2f, niveau);
                    break;
            }
            switch (rang)
            {
                case "pion":
                    break;
                case "cavalier":
                    prix = prix * 1.5f;
                    break;
                case "fou":
                    prix = prix * 2;
                    break;
                case "tour":
                    prix = prix * 2;
                    break;
                case "dame":
                    prix = prix * 4;
                    break;
            }
            
        }

    }
}
