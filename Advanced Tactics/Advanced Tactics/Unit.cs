using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Advanced_Tactics
{
    public class Unit
    {
        public enum Type { troupe, commando, tank }
        public enum Rang { pion, fou, cavalier, tour, dame, roi }
        private float niveau;
        private int pos_x;
        public int x_unit
        {
            get { return pos_x; }
            set { pos_x = value; }
        }
        private int pos_y;
        public int y_unit
        {
            get { return pos_y; }
            set { pos_y = value; }
        }
        private bool valid_deplac;
        


        public static void unite(Type type, float niveau, Rang rg)
        {
            float prix = 1;
            float force = 1;
            float pv = 1;
            switch (type)
            {
                case Type.troupe :
                    prix = 100 + 100 * (float)Math.Pow(1.5f, niveau);
                    force = 10 + 10 * (float)Math.Pow(1.2f, niveau);
                    pv = 50 + 50 * (float)Math.Pow(1.2f, niveau);
                    break;
                case Type.commando :
                    prix = 500 + 500 * (float)Math.Pow(1.5f, niveau);
                    force = 30 + 30 * (float)Math.Pow(1.2f, niveau);
                    pv = 100 + 100 * (float)Math.Pow(1.2f, niveau);
                    break;
                case Type.tank:
                    prix = 1200 + 1200 * (float)Math.Pow(1.5f, niveau);
                    force = 90 + 90 * (float)Math.Pow(1.2f, niveau);
                    pv = 300 + 300 * (float)Math.Pow(1.2f, niveau);
                    break;
            }
            switch (rg)
            {
                case Rang.pion:
                   
                    break;
                case Rang.fou:
                    prix = prix * 2;
                    break;
                case Rang.cavalier:
                    prix = prix * 1.5f;
                    break;
                case Rang.tour:
                    prix = prix * 2;
                    break;
                case Rang.dame:
                    prix = prix * 4;
                    break;
            }
        }

        public void deplacement(int pos_x, int pos_y, Rang rg, Type type)
        {
            switch (rg)
            {
                case Rang.pion:
                    // utiliser mvt_is_possible et obstacle de case et carte de map pour utiliser sur case du tableau

                   
                    break;
                case Rang.fou:
                    break;
                case Rang.cavalier:
                    break;
                case Rang.tour:
                    break;
                case Rang.dame:
                    break;
                case Rang.roi:
                    break;
            }
        }


    }
}
