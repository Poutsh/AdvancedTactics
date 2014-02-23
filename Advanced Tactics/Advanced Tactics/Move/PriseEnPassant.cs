using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advanced_Tactics
{
    /// <summary>
    /// Coup Prise En Passant</summary>
    /// <remarks>
    /// Coup effectué exclusivement par un Pion 
    /// Il prend "en passant" un Pion qui vient de faire un Déplacement Double
    /// </remarks>
    class PriseEnPassant : Coup
    {

        /// <summary>
        /// Constructeur de la classe. </summary>
        public PriseEnPassant(Position positionDepart, int colonne1, int ligne1, int colonne2, int ligne2)
            : base(positionDepart, colonne1, ligne1, colonne2, ligne2)
        {


            positionResultante.Case[colonne1, ligne1] = new Case();
            positionResultante.Case[colonne2, ligne1] = new Case();
            positionResultante.Case[colonne2, ligne2] =
                positionDepart.Case[colonne1, ligne1].positionSuivanteCase(true);

        }

        /// <summary>
        /// Notation algébrique d'une prise en passant </summary>
        override public String notationAlgebrique
        {
            get
            {
                String notation =
                    this.position1algebrique + "x"
                    +
                    this.position2algebrique + "e.p.";

                return notation;
            }
        }

    }
}
