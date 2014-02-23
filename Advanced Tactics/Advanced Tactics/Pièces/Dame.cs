using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;


namespace Advanced_Tactics
{
    /// La Dame se déplace comme un fou et comme une tour
    public class Dame : Piece
    {
        public Dame(Joueur proprietaire_piece)
            : base(proprietaire_piece)
        {
        }

        // Nom codé de la pièce
        override public char lettre
        {
            get
            {
                return 'D';
            }
        }

        override public void coups(Position position, ArrayList coupsPossibles, int posColonne, int posLigne)
        {
            // les 4 mouvements de Dame en diagonale (comme un fou)
            this.ajouterLigneCoups(position, coupsPossibles, posColonne, posLigne, 1, 1);
            this.ajouterLigneCoups(position, coupsPossibles, posColonne, posLigne, -1, -1);
            this.ajouterLigneCoups(position, coupsPossibles, posColonne, posLigne, -1, 1);
            this.ajouterLigneCoups(position, coupsPossibles, posColonne, posLigne, 1, -1);
            // les 4 mouvements de Dame en ligne droite (comme une tour)
            this.ajouterLigneCoups(position, coupsPossibles, posColonne, posLigne, 0, 1);
            this.ajouterLigneCoups(position, coupsPossibles, posColonne, posLigne, 0, -1);
            this.ajouterLigneCoups(position, coupsPossibles, posColonne, posLigne, -1, 0);
            this.ajouterLigneCoups(position, coupsPossibles, posColonne, posLigne, 1, 0);

        }

        override public Case positionSuivanteCase(Boolean deplacement)
        {
            // clone pour position suivante
            Case posSuivanteCase = new Dame(this.player);
            return posSuivanteCase;
        }



    }
}
