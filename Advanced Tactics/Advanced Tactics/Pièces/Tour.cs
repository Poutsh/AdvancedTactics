using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Advanced_Tactics
{
    /// Cette pièce se déplace en ligne droite.
    /// Le roi peut roquer avec elle, à condition qu'elle ne se soit pas déplacée.
    public class Tour : Piece
    {
        private Boolean déplacé = false;

        public Tour(Joueur proprietaire_piece, Boolean déjaDéplacé = false)
            : base(proprietaire_piece)
        {
            déplacé = déjaDéplacé;
        }

        public Boolean aDéjaEtéDéplacé
        {
            get
            {
                return déplacé;
            }
        }

        // Nom codé de la pièce
        override public char lettre
        {
            get
            {
                return 'T';
            }
        }

        override public void coups(Position position, ArrayList coupsPossibles, int posColonne, int posLigne)
        {
            // la tour se déplace en lignes horizontales et verticales
            this.ajouterLigneCoups(position, coupsPossibles, posColonne, posLigne, 0, 1);
            this.ajouterLigneCoups(position, coupsPossibles, posColonne, posLigne, 0, -1);
            this.ajouterLigneCoups(position, coupsPossibles, posColonne, posLigne, -1, 0);
            this.ajouterLigneCoups(position, coupsPossibles, posColonne, posLigne, 1, 0);
        }


        override public Case positionSuivanteCase(Boolean deplacement = false)
        {
            if (deplacement == true || this.déplacé == true)
            {
                return new Tour(this.player, true);
            }
            else
            {
                return new Tour(this.player, false);
            }
        }


    }
}
