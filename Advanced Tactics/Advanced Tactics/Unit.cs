using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace sprite
{
    class Unit
    {
        private string nom;
        private string rang;
        private float niveau;


        private static void unite(string nom, float niveau, string rang)
        {
            float p = 1;
            float f = 1;
            float pv = 1;
            switch (nom)
            {
                case "troupe" :
                    p = 100 + 100 * (float)Math.Pow(1.5f, niveau);
                    f = 10 + 10 * (float)Math.Pow(1.2f, niveau);
                    pv = 50 + 50 * (float)Math.Pow(1.2f, niveau);
                    break;
                case "commando" :
                    p = 500 + 500 * (float)Math.Pow(1.5f, niveau);
                    f = 30 + 30 * (float)Math.Pow(1.2f, niveau);
                    pv = 100 + 100 * (float)Math.Pow(1.2f, niveau);
                    break;
                case "tank":
                    p = 1200 + 1200 * (float)Math.Pow(1.5f, niveau);
                    f = 90 + 90 * (float)Math.Pow(1.2f, niveau);
                    pv = 300 + 300 * (float)Math.Pow(1.2f, niveau);
                    break;
            }
            switch (rang)
            {
                case "pion":
                    break;
                case "cavalier":
                    p = p*1.5f;
                    break;
                case "fou":
                    p = p*2;
                    break;
                case "tour":
                    p = p*2;
                    break;
                case "dame":
                    p = p*4;
                    break;
            }
            
        }
    


    }
}
