using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advanced_Tactics
{
    /// <summary>
    /// Coup Prise</summary>
    /// <remarks>
    /// Similaire à Déplacement, mais en engageant une perte de matériel pour l'adversaire.
    /// </remarks>
    class Prise : Coup
    {

        /// <summary>
        /// Constructeur de la classe. </summary>
        public Prise(Position positionDepart, int colonne1, int ligne1, int colonne2, int ligne2)
            : base(positionDepart, colonne1, ligne1, colonne2, ligne2)
        {


            positionResultante.Case[colonne1, ligne1] = new Case();
            positionResultante.Case[colonne2, ligne2] =
                positionDepart.Case[colonne1, ligne1].positionSuivanteCase(true);

        }

        /// <summary>
        /// Notation algébrique d'une prise </summary>
        override public String notationAlgebrique
        {
            get
            {
                String notation =
                    this.lettrePosition1
                    +
                    this.position1algebrique + "x"
                    +
                    this.position2algebrique;

                return notation;
            }
        }
    }
}
