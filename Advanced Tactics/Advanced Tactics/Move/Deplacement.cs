using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advanced_Tactics
{
    /// <summary>
    /// Coup Déplacement </summary>
    /// <remarks>
    /// Déplacement d'une pièce d'un point A à un point B sans condition particulière. 
    /// </remarks>
    class Deplacement : Coup
    {
        /// <summary>
        /// Constructeur de la classe. </summary>
        public Deplacement(Position positionDepart, int colonne1, int ligne1, int colonne2, int ligne2)
            : base(positionDepart, colonne1, ligne1, colonne2, ligne2)
        {

            this.positionResultante.Case[colonne1, ligne1] = new Case();
            this.positionResultante.Case[colonne2, ligne2] =
                positionDepart.Case[colonne1, ligne1].positionSuivanteCase(true);
        }


    }
}
