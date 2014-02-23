using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Advanced_Tactics
{
    
    /// Le Pion se déplace d'une case en avant, ou une case en diagonale pour les prises.
    /// Il peut également effectuer les coups suivants: Prise en passant, Promotion.
    
    public class Pion : Piece
    {
        private Boolean coupDouble = false;

        // Joueur de même couleur qui peut manipuler la pièce

        public Pion(Joueur proprietaire)
            : base(proprietaire)
        {

        }

        
        // Le pion a fait un déplacement double au coup précédent 

        public Boolean vientDeFaireCoupDouble
        {
            get
            {
                return coupDouble;
            }
            set
            {
                coupDouble = value;
            }
        }


        // Nom codé de la pièce
        override public char lettre
        {
            get
            {
                return 'P';
            }
        }


        override public void coups(Position position, ArrayList coupsPossibles, int posColonne, int posLigne)
        {
            // les coups d'un pion sont orientés différement selon qu'il est blanc ou noir

            if (this.player == Joueur.Player1)
            {

                if (posLigne < 7)
                {
                    if (position.Case[posColonne, posLigne + 1].occupé == false)
                    {
                        if (posLigne == 6)
                        {
                            // promotion
                            coupsPossibles.Add(new Promotion(position, posColonne, posLigne, posColonne + 0, posLigne + 1));
                        }
                        else
                        {
                            // déplacement simple
                            coupsPossibles.Add(new Deplacement(position, posColonne, posLigne, posColonne + 0, posLigne + 1));
                        }
                    }
                    if (posColonne < 7)
                    {
                        if (position.Case[posColonne + 1, posLigne + 1].occupé == true)
                        {
                            if (position.Case[posColonne + 1, posLigne + 1].joueur != this.joueur)
                            {
                                if (posLigne == 6)
                                {
                                    // promotion
                                    coupsPossibles.Add(new Promotion(position, posColonne, posLigne, posColonne + 1, posLigne + 1));
                                }
                                else
                                {
                                    // prise à droite
                                    coupsPossibles.Add(new Prise(position, posColonne, posLigne, posColonne + 1, posLigne + 1));
                                }
                            }
                        }
                    }
                    if (posColonne > 0)
                    {
                        if (position.Case[posColonne - 1, posLigne + 1].occupé == true)
                        {
                            if (position.Case[posColonne - 1, posLigne + 1].joueur != this.joueur)
                            {
                                if (posLigne == 6)
                                {
                                    // promotion
                                    coupsPossibles.Add(new Promotion(position, posColonne, posLigne, posColonne - 1, posLigne + 1));
                                }
                                else
                                {
                                    // prise à gauche
                                    coupsPossibles.Add(new Prise(position, posColonne, posLigne, posColonne - 1, posLigne + 1));
                                }
                            }
                        }
                    }

                }
                // déplacement double
                if (posLigne == 1)
                {
                    if (position.Case[posColonne, 2].occupé == false)
                    {
                        if (position.Case[posColonne, 3].occupé == false)
                        {
                            coupsPossibles.Add(new DeplacementDouble(position, posColonne, posLigne, posColonne, posLigne + 2));
                        }
                    }
                }


                if (posLigne == 4)
                {
                    if (posColonne < 7)
                    {
                        if (position.Case[posColonne + 1, 4].occupé == true)
                        {
                            if (position.Case[posColonne + 1, 4].joueur != this.joueur)
                            {
                                if (position.Case[posColonne + 1, 4].lettre == 'P')
                                {
                                    Pion p = (Pion)position.Case[posColonne + 1, 4];
                                    if (p.vientDeFaireCoupDouble == true)
                                    {
                                        // prise en passant à gauche
                                        coupsPossibles.Add(new PriseEnPassant(position, posColonne, posLigne, posColonne + 1, posLigne + 1));
                                    }
                                }
                            }
                        }
                    }
                    if (posColonne > 0)
                    {
                        if (position.Case[posColonne - 1, 4].occupé == true)
                        {
                            if (position.Case[posColonne - 1, 4].joueur != this.joueur)
                            {
                                if (position.Case[posColonne - 1, 4].lettre == 'P')
                                {
                                    Pion p = (Pion)position.Case[posColonne - 1, 4];
                                    if (p.vientDeFaireCoupDouble == true)
                                    {
                                        // prise en passant à gauche
                                        coupsPossibles.Add(new PriseEnPassant(position, posColonne, posLigne, posColonne - 1, posLigne + 1));
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                if (posLigne > 0)
                {
                    if (position.Case[posColonne, posLigne - 1].occupé == false)
                    {
                        if (posLigne == 1)
                        {
                            // promotion
                            coupsPossibles.Add(new Promotion(position, posColonne, posLigne, posColonne + 0, posLigne - 1));
                        }
                        else
                        {
                            // déplacement simple
                            coupsPossibles.Add(new Deplacement(position, posColonne, posLigne, posColonne + 0, posLigne - 1));
                        }
                    }
                    if (posColonne < 7)
                    {
                        if (position.Case[posColonne + 1, posLigne - 1].occupé == true)
                        {
                            if (position.Case[posColonne + 1, posLigne - 1].joueur != this.joueur)
                            {
                                if (posLigne == 1)
                                {
                                    // promotion
                                    coupsPossibles.Add(new Promotion(position, posColonne, posLigne, posColonne + 1, posLigne - 1));
                                }
                                else
                                {
                                    // prise à droite
                                    coupsPossibles.Add(new Prise(position, posColonne, posLigne, posColonne + 1, posLigne - 1));
                                }
                            }
                        }
                    }
                    if (posColonne > 0)
                    {
                        if (position.Case[posColonne - 1, posLigne - 1].occupé == true)
                        {
                            if (position.Case[posColonne - 1, posLigne - 1].joueur != this.joueur)
                            {
                                if (posLigne == 1)
                                {
                                    // promotion
                                    coupsPossibles.Add(new Promotion(position, posColonne, posLigne, posColonne - 1, posLigne - 1));
                                }
                                else
                                {
                                    // prise à gauche
                                    coupsPossibles.Add(new Prise(position, posColonne, posLigne, posColonne - 1, posLigne - 1));
                                }
                            }
                        }
                    }

                }
                // déplacement double
                if (posLigne == 6)
                {
                    if (position.Case[posColonne, 4].occupé == false)
                    {
                        if (position.Case[posColonne, 5].occupé == false)
                        {
                            coupsPossibles.Add(new DeplacementDouble(position, posColonne, posLigne, posColonne + 0, posLigne - 2));
                        }
                    }
                }
                if (posLigne == 3)
                {
                    if (posColonne < 7)
                    {
                        if (position.Case[posColonne + 1, 3].occupé == true)
                        {
                            if (position.Case[posColonne + 1, 3].joueur != this.joueur)
                            {
                                if (position.Case[posColonne + 1, 3].lettre == 'P')
                                {
                                    Pion p = (Pion)position.Case[posColonne + 1, 3];
                                    if (p.vientDeFaireCoupDouble == true)
                                    {
                                        // prise en passant à gauche
                                        coupsPossibles.Add(new PriseEnPassant(position, posColonne, posLigne, posColonne + 1, posLigne - 1));
                                    }
                                }
                            }
                        }
                    }
                    if (posColonne > 0)
                    {
                        if (position.Case[posColonne - 1, 3].occupé == true)
                        {
                            if (position.Case[posColonne - 1, 3].joueur != this.joueur)
                            {
                                if (position.Case[posColonne - 1, 3].lettre == 'P')
                                {
                                    Pion p = (Pion)position.Case[posColonne - 1, 3];
                                    if (p.vientDeFaireCoupDouble == true)
                                    {
                                        // prise en passant à gauche
                                        coupsPossibles.Add(new PriseEnPassant(position, posColonne, posLigne, posColonne - 1, posLigne - 1));
                                    }
                                }
                            }
                        }
                    }
                }


            }

        }

        // Clône du pion pour une position suivante
       
        override public Case positionSuivanteCase(Boolean deplacement = false)
        {
            // clone pour position suivante
            Pion posSuivanteCase = new Pion(this.player);

            return posSuivanteCase;

        }


    }
}
