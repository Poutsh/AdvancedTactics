using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;


namespace Advanced_Tactics
{
    /// <summary>
    /// Position de l'échiquier </summary>
    /// <remarks>
    /// La position est définie par la répartition des pièces sur l'échiquier.
    /// Elle détermine les coups possibles.
    /// Elle peut constituer un échec, un MAT ou un PAT.
    /// </remarks>
    public class Position
    {

        // Tableau des cases (colonne, ligne)
        private Case[,] tableauPositionCase;


        private Joueur joueurTrait;

        // liste des coups permis au joueur pour cette position
        private ArrayList tableauCoupsPermis = null;


        public Position(Joueur trait)
        {

            // initialiser les 400 cases de l'échiquier
            this.tableauPositionCase = new Case[20, 20];

            this.joueurTrait = trait;
        }

        public Position()
        {

            // initialiser les 400 cases de l'échiquier
            this.tableauPositionCase = new Case[20, 20];
            // trait aux blancs
            this.joueurTrait = Joueur.Player1;
            //this.dernierCoupJoué = null;


            // Cases vides à gauche des Rois
            for (int i = 0; i < 8; i++)
            {
                this.Case[i, 0] = new Case();
            }

            // 3 rois du Joueur 2
            this.Case[9, 0] = new Roi(Joueur.Player1);
            this.Case[10, 0] = new Roi(Joueur.Player1);
            this.Case[11, 0] = new Roi(Joueur.Player1);

            // Cases vides à droite des Rois
            for (int i = 12; i < 20; i++)
            {
                this.Case[i, 0] = new Case();
            }


            // Milieu du plateau vide
            for (int i = 0; i < 20; i++)
            {
                for (int j = 1; i < 18; j++)
                {
                    this.Case[i, j] = new Case();
                }
            }

            // Cases vides à gauche des Rois
            for (int i = 0; i < 8; i++)
            {
                this.Case[i, 19] = new Case();
            }

            // 3 rois du Joueur 2
            this.Case[9, 19] = new Roi(Joueur.Player2);
            this.Case[10, 19] = new Roi(Joueur.Player2);
            this.Case[11, 19] = new Roi(Joueur.Player2);

            // Cases vides à droite des Rois
            for (int i = 12; i < 20; i++)
            {
                this.Case[i, 19] = new Case();
            }

        }



        /// <summary>
        /// Joueur qui dispose du trait (lecture seule) </summary>
        public Joueur trait
        {
            get
            {
                return joueurTrait;
            }
        }

        /// <summary>
        /// Tableau des cases de l'échiquier </summary>
        public Case[,] Case
        {
            get
            {
                return tableauPositionCase;
            }
        }

        /// <summary>
        /// Liste des coups permis par la position (lecture seule) </summary>
        public ArrayList coupsPermis
        {
            get
            {
                if (this.tableauCoupsPermis == null)
                {
                    this.tableauCoupsPermis = new ArrayList();
                    int x = 0;
                    int y = 0;
                    for (x = 0; x < 20; x++)
                    {
                        for (y = 0; y < 20; y++)
                        {
                            // la case est elle occupée par une pièce ?
                            if (this.Case[x, y].occupé == true)
                            {
                                // la pièce appartient elle au joueur qui a le trait ?
                                if (this.Case[x, y].joueur == this.trait)
                                {
                                    // ajouter les coups de la pièce aux coups permis
                                    this.Case[x, y].coups(this, this.coupsPermis, x, y);
                                }
                            }
                        }
                    }
                }
                return this.tableauCoupsPermis;

            }
        }

        /// <summary>
        /// Liste des coups permis formellement légaux (lecture seule) </summary>
        public ArrayList coupsPermisLégaux
        {
            get
            {
                ArrayList tableauCoupsPermisLégaux = new ArrayList();
                foreach (Coup c in this.coupsPermis)
                {
                    if (c.légal == true)
                    {
                        tableauCoupsPermisLégaux.Add(c);
                    }
                }
                return tableauCoupsPermisLégaux;
            }
        }

        /// <summary>
        /// La légalité de la position (lecture seule) </summary>
        public Boolean légale
        {
            get
            {
                foreach (Coup c in this.coupsPermis)
                {
                    if (c.régicide == true)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        /// <summary>
        /// Le roi est il en échec ? (lecture seule) </summary>
        public Boolean échec
        {
            get
            {
                // on teste s'il y a échec immédiat
                Position posTest = this.positionSuivante(null);
                // simuler les coups comme si on passait son tour sans jouer

                if (posTest.légale == true)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// Y a t-il échec et MAT ? (lecture seule) </summary>
        public Boolean MAT
        {
            get
            {
                if (this.coupsPermisLégaux.Count == 0 && this.échec == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Y a t-il PAT ? (lecture seule) </summary>
        public Boolean PAT
        {
            get
            {

                if (this.coupsPermisLégaux.Count == 0 && this.échec == false)
                {
                    return true;
                }
                else
                {
                    return false;
                }


            }
        }

        public Position positionSuivante(Coup coup)
        {
            Position posSuivante;

            // inverser le trait
            if (this.trait == Joueur.Player1)
            {
                posSuivante = new Position(Joueur.Player2);
            }
            else
            {
                posSuivante = new Position(Joueur.Player1);
            }

            // créer les cases de la nouvelle position
            int x;
            int y;
            for (x = 0; x < 20; x++)
            {
                for (y = 0; y < 20; y++)
                {
                    // le clone de la case peut être sensiblement différent
                    // ex: pion qui pouvait être pris en passant et ne le peut plus
                    posSuivante.Case[x, y] = this.Case[x, y].positionSuivanteCase(false);
                }
            }

            return posSuivante;

        }


    }
}
