using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advanced_Tactics
{
    /// <summary>
    /// Coup Grand Roque </summary>
    /// <remarks>
    /// Ce coup est effectué par le roi, il implique la tour et n'est légal que sous conditions.
    /// </remarks>
    class GrandRoque : Coup
    {

        /// <summary>
        /// Coup intermédiaire du roque, destiné aux tests</summary>
        private Coup deplacementIntermédiaire;

        /// <summary>
        /// Constructeur de la classe. </summary>
        /// <param name="s"> Emplacement de la description du paramètre de s</param>
        public GrandRoque(Position positionDepart, int colonne1, int ligne1, int colonne2, int ligne2)
            : base(positionDepart, colonne1, ligne1, colonne2, ligne2)
        {

            this.deplacementIntermédiaire = new Deplacement(this.positionInitiale, this.colonne1, this.ligne1, this.colonne1 - 1, this.ligne1);

            positionResultante.Case[colonne1, ligne1] = new Case();
            positionResultante.Case[colonne2, ligne2] =
            positionDepart.Case[colonne1, ligne1].positionSuivanteCase(true);

            // déplacements de la tour
            positionResultante.Case[0, ligne1] = new Case();
            positionResultante.Case[3, ligne1] =
            positionDepart.Case[0, ligne1].positionSuivanteCase(true);

        }

        /// <summary>
        /// Notation algébrique du grand roque </summary>
        override public String notationAlgebrique
        {
            get
            {
                return "0-0-0";
            }
        }

        /// <summary>
        /// Conditions de légalité spécifiques au roque </summary>
        override public Boolean légal
        {
            get
            {
                // en plus de la légalité du déplacement du roi, on vérifie:
                // _que le roi n'est pas en échec
                // _ qu'il pourrait se déplacer sur la case voisine sans être en échec
                if (
                    (base.légal == true) &&
                    (this.positionInitiale.échec == false) &&
                    (this.deplacementIntermédiaire.légal == true)
                    )
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

    }
}
