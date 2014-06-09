using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Advanced_Tactics
{
    class Players
    {
        Unit unit;
        List<Unit> UnitsOfPlayer;

        Rectangle Limit;

        /// <summary>
        /// Gestion de Joueur
        /// </summary>
        /// <param name="Number">Nombre de joueurs</param>
        public Players(int Number)
        {
            UnitsOfPlayer = new List<Unit>();

            switch (Number)
            {
                case 2:
                    Limit = new Rectangle(
                default:
                    break;
            }
        }
    }
}
