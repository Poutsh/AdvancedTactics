using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advanced_Tactics
{
    /// <summary>
    /// Coup Déplacement Double </summary>
    /// <remarks>
    /// Déplacement de deux cases d'un pion à la condition qu'il n'ait jamais encore bougé.
    /// Il pourra ensuite être pris en passant au coup suivant.
    /// </remarks>
    public class DeplacementDouble : Coup
    {
        /// <summary>
        /// Constructeur de la classe. </summary>
        public DeplacementDouble(Position positionDepart, int colonne1, int ligne1, int colonne2, int ligne2)
            : base(positionDepart, colonne1, ligne1, colonne2, ligne2)
        {


            positionResultante.Case[colonne1, ligne1] = new Case();

            Pion p = (Pion)positionDepart.Case[colonne1, ligne1].positionSuivanteCase(true);
            p.vientDeFaireCoupDouble = true;
            positionResultante.Case[colonne2, ligne2] = p;

        }
    }
}