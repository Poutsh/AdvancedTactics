using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;


namespace Advanced_Tactics
{
    // Le cavalier se déplace en sautant deux case dans une direction et une case dans l'autre
    public class Cavalier : Piece
    {
        public Cavalier(Joueur proprietaire)
            : base(proprietaire)
        {
        }

        // Nom codé de la pièce
        override public char lettre
        {
            get
            {
                return 'C';
            }
        }

        override public void coups(Position position, ArrayList coupsPossibles, int posColonne, int posLigne)
        {

            // test des 8 déplacements (ou prises) possibles pour un cavalier 

            if (posColonne > 0 && posLigne > 1)
            {
                if (position.Case[posColonne - 1, posLigne - 2].occupé == false)
                {
                    // deplacement 
                    coupsPossibles.Add(new Deplacement(position, posColonne, posLigne, posColonne - 1, posLigne - 2));
                }
                else
                {
                    if (position.Case[posColonne - 1, posLigne - 2].joueur != this.joueur)
                    {
                        // prise
                        coupsPossibles.Add(new Prise(position, posColonne, posLigne, posColonne - 1, posLigne - 2));
                    }
                }
            }
            if (posColonne > 1 && posLigne > 0)
            {
                if (position.Case[posColonne - 2, posLigne - 1].occupé == false)
                {
                    // deplacement 
                    coupsPossibles.Add(new Deplacement(position, posColonne, posLigne, posColonne - 2, posLigne - 1));
                }
                else
                {
                    if (position.Case[posColonne - 2, posLigne - 1].joueur != this.joueur)
                    {
                        // prise
                        coupsPossibles.Add(new Prise(position, posColonne, posLigne, posColonne - 2, posLigne - 1));
                    }
                }
            }
            if (posColonne < 6 && posLigne > 0)
            {
                if (position.Case[posColonne + 2, posLigne - 1].occupé == false)
                {
                    // deplacement 
                    coupsPossibles.Add(new Deplacement(position, posColonne, posLigne, posColonne + 2, posLigne - 1));
                }
                else
                {
                    if (position.Case[posColonne + 2, posLigne - 1].joueur != this.joueur)
                    {
                        // prise
                        coupsPossibles.Add(new Prise(position, posColonne, posLigne, posColonne + 2, posLigne - 1));
                    }
                }
            }
            if (posColonne < 6 && posLigne < 7)
            {
                if (position.Case[posColonne + 2, posLigne + 1].occupé == false)
                {
                    // deplacement 
                    coupsPossibles.Add(new Deplacement(position, posColonne, posLigne, posColonne + 2, posLigne + 1));
                }
                else
                {
                    if (position.Case[posColonne + 2, posLigne + 1].joueur != this.joueur)
                    {
                        // prise
                        coupsPossibles.Add(new Prise(position, posColonne, posLigne, posColonne + 2, posLigne + 1));
                    }
                }
            }
            if (posColonne > 1 && posLigne < 7)
            {
                if (position.Case[posColonne - 2, posLigne + 1].occupé == false)
                {
                    // deplacement 
                    coupsPossibles.Add(new Deplacement(position, posColonne, posLigne, posColonne - 2, posLigne + 1));
                }
                else
                {
                    if (position.Case[posColonne - 2, posLigne + 1].joueur != this.joueur)
                    {
                        // prise
                        coupsPossibles.Add(new Prise(position, posColonne, posLigne, posColonne - 2, posLigne + 1));
                    }
                }
            }
            if (posColonne < 7 && posLigne < 6)
            {
                if (position.Case[posColonne + 1, posLigne + 2].occupé == false)
                {
                    // deplacement 
                    coupsPossibles.Add(new Deplacement(position, posColonne, posLigne, posColonne + 1, posLigne + 2));
                }
                else
                {
                    if (position.Case[posColonne + 1, posLigne + 2].joueur != this.joueur)
                    {
                        // prise
                        coupsPossibles.Add(new Prise(position, posColonne, posLigne, posColonne + 1, posLigne + 2));
                    }
                }
            }
            if (posColonne < 7 && posLigne > 1)
            {
                if (position.Case[posColonne + 1, posLigne - 2].occupé == false)
                {
                    // deplacement 
                    coupsPossibles.Add(new Deplacement(position, posColonne, posLigne, posColonne + 1, posLigne - 2));
                }
                else
                {
                    if (position.Case[posColonne + 1, posLigne - 2].joueur != this.joueur)
                    {
                        // prise
                        coupsPossibles.Add(new Prise(position, posColonne, posLigne, posColonne + 1, posLigne - 2));
                    }
                }
            }
            if (posColonne > 0 && posLigne < 6)
            {
                if (position.Case[posColonne - 1, posLigne + 2].occupé == false)
                {
                    // deplacement 
                    coupsPossibles.Add(new Deplacement(position, posColonne, posLigne, posColonne - 1, posLigne + 2));
                }
                else
                {
                    if (position.Case[posColonne - 1, posLigne + 2].joueur != this.joueur)
                    {
                        // prise
                        coupsPossibles.Add(new Prise(position, posColonne, posLigne, posColonne - 1, posLigne + 2));
                    }
                }
            }

        }

        override public Case positionSuivanteCase(Boolean deplacement = false)
        {
            // clone pour position suivante
            Case posSuivanteCase = new Cavalier(this.player);
            return posSuivanteCase;
        }



    }
}
