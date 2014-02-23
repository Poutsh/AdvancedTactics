using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Advanced_Tactics
{

    /// Cette classe peut être héritée en : Roi, Dame, Cavalier, Fou, Tour, Pion
    
    public class Piece : Case
    {
        public Piece(Joueur proprietaire_p)
            : base()
        {
            player = proprietaire_p;

        }

        /// Case occupée par une pièce (lecture seule)
        
        override public Boolean occupé
        {
            get
            {
                return true;
            }
        }

        /// Fonction facilitante pour les lignes droites de déplacements
        /// Les pièces concernées sont : le fou, la tour, la dame
        
        protected void ajouterLigneCoups(Position position, ArrayList coupsPossibles, int colonne, int ligne, int incrementX, int incrementY)
        {
            int colonnes;
            int lignes;
            Case c;
            Boolean obstacle;

            colonnes = 0;
            lignes = 0;
            obstacle = false;
            while (obstacle == false)
            {
                colonnes += incrementX;
                lignes += incrementY;

                if ((colonne + colonnes) >= 0 && (colonne + colonnes) <= 7 && (ligne + lignes) >= 0 && (ligne + lignes) <= 7)
                {
                    c = position.Case[colonne + colonnes, ligne + lignes];
                    if (c.occupé == false)
                    {
                        // possibilité de se déplacer 
                        coupsPossibles.Add(new Deplacement(position, colonne, ligne, colonne + colonnes, ligne + lignes));
                    }
                    else
                    {
                        // on a rencontré une pièce donc impossible d'aller plus loin
                        obstacle = true;
                        if (c.joueur != this.joueur)
                        {
                            // possibilité de prendre la pièce adverse
                            coupsPossibles.Add(new Prise(position, colonne, ligne, colonne + colonnes, ligne + lignes));
                        }
                    }
                }
                else
                {
                    // sortie de l'échiquier : impossible d'aller plus loin
                    obstacle = true;
                }
            }

        }
    }
}
