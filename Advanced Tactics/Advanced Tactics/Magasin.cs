using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advanced_Tactics
{
    class Magasin
    {
        public enum Type { troupe, commando, tank }
        public enum Rang { pion, fou, cavalier, tour, dame, roi }
        private int niveau;

        public void calcul(Type type, float niveau, Rang rg)
        {
            int prix = 1;
            int force = 1;
            int pv = 1;
            switch (type)
            {
                case Type.troupe:
                    prix = 100 + 100 * (int)Math.Pow(1.5f, niveau);
                    force = 10 + 10 * (int)Math.Pow(1.2f, niveau);
                    pv = 50 + 50 * (int)Math.Pow(1.2f, niveau);
                    break;
                case Type.commando:
                    prix = 500 + 500 * (int)Math.Pow(1.5f, niveau);
                    force = 30 + 30 * (int)Math.Pow(1.2f, niveau);
                    pv = 100 + 100 * (int)Math.Pow(1.2f, niveau);
                    break;
                case Type.tank:
                    prix = 1200 + 1200 * (int)Math.Pow(1.5f, niveau);
                    force = 90 + 90 * (int)Math.Pow(1.2f, niveau);
                    pv = 300 + 300 * (int)Math.Pow(1.2f, niveau);
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
                    prix = (int)(prix * 1.5f);
                    break;
                case Rang.tour:
                    prix = prix * 2;
                    break;
                case Rang.dame:
                    prix = prix * 4;
                    break;
            }
        }
    }
}
