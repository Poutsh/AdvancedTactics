using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Advanced_Tactics
{
    /// Le Fou se déplace en diagonale.
    public class Fou : Piece
    {
        public Fou(Joueur proprietaire)
            : base(proprietaire)
        {
        }

        // Nom codé de la pièce
        override public char lettre
        {
            get
            {
                return 'F';
            }
        }

        override public void coups(Position position, ArrayList coupsPossibles, int posColonne, int posLigne)
        {
            // les 4 mouvements en diagonale du fou
            this.ajouterLigneCoups(position, coupsPossibles, posColonne, posLigne, 1, 1);
            this.ajouterLigneCoups(position, coupsPossibles, posColonne, posLigne, -1, -1);
            this.ajouterLigneCoups(position, coupsPossibles, posColonne, posLigne, -1, 1);
            this.ajouterLigneCoups(position, coupsPossibles, posColonne, posLigne, 1, -1);

        }

        override public Case positionSuivanteCase(Boolean deplacement = false)
        {
            // clone pour position suivante
            Case posSuivanteCase = new Fou(this.player);
            return posSuivanteCase;
        }


    }
}
