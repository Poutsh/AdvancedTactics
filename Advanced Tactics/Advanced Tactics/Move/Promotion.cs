using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advanced_Tactics
{
    /// <summary>
    /// Coup Promotion</summary>
    /// <remarks>
    /// Déplacement d'un pion sur la dernière ligne
    /// Il entraîne sa promotion en une pièce de valeur supérieure
    /// </remarks>
    class Promotion : Coup
    {
        /// <summary>
        /// Constructeur de la classe. </summary>
        public Promotion(Position positionDepart, int colonne1, int ligne1, int colonne2, int ligne2)
            : base(positionDepart, colonne1, ligne1, colonne2, ligne2)
        {


            positionResultante.Case[colonne1, ligne1] = new Case();
            positionResultante.Case[colonne2, ligne2] =
                new Dame(positionDepart.Case[colonne1, ligne1].joueur);

        }

        /// <summary>
        /// Notation algébrique UCI d'une promotion </summary>
        override public String notationAlgebriqueUCI
        {
            get
            {
                String notation = this.position1algebrique
                    + this.position2algebrique + 'q'; // promotion en dame

                return notation;
            }
        }

        /// <summary>
        /// Notation algébrique d'une promotion </summary>
        override public String notationAlgebrique
        {

            get
            {
                String notation;

                if (positionInitiale.Case[colonne2, ligne2].lettre == ' ')
                {
                    notation =
                    this.position1algebrique + "-"
                    +
                    this.position2algebrique + "D"; // promotion en dame
                }
                else
                {
                    notation =
                    this.position1algebrique + "+"
                    +
                    this.position2algebrique + "D"; // promotion en dame
                }
                return notation;
            }
        }
    }
}
