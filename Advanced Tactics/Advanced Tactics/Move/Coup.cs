using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advanced_Tactics
{
    /// Les coups sont une de ces classes héritées:
    /// Grand Roque, Petit Roque, Déplacement, Déplacement double, Prise, Prise en passant, Promotion
    public abstract class Coup
    {
        /// <summary>
        /// Position avant le coup</summary>
        private Position posInitiale;

        /// <summary>
        /// Position après le coup</summary>
        private Position posResultante;

        /// <summary>
        /// Coordonnée X de la case de départ</summary>
        private int posInitialeColonne;

        /// <summary>
        /// Coordonnée X de la cased'arrivée </summary>
        private int posResultanteColonne;

        /// <summary>
        /// Coordonnée Y de la case de départ</summary>
        private int posInitialeLigne;

        /// <summary>
        /// Coordonnée Y de la case d'arrivée </summary>
        private int posResultanteLigne;

        /// <param name="positionDepart"> Position avant le coup </param>
        /// <param name="colonne1"> Coordonnée X de la case de départ</param>
        /// <param name="ligne1"> Coordonnée Y de la case de départ</param>
        /// <param name="colonne2"> Coordonnée X de la case d'arrivée </param>
        /// <param name="ligne2"> Coordonnée Y de la case d'arrivée </param>
        public Coup(Position positionDepart, int colonne1, int ligne1, int colonne2, int ligne2)
        {
            this.posInitiale = positionDepart;
            this.posResultante = positionDepart.positionSuivante(this);
            this.posInitialeColonne = colonne1;
            this.posInitialeLigne = ligne1;
            this.posResultanteColonne = colonne2;
            this.posResultanteLigne = ligne2;
        }

        /// <summary>
        /// Le coup entraîne la prise du roi adverse (impossible dans une position légale) </summary>
        public Boolean régicide
        {
            get
            {
                if (this.positionInitiale.Case[posResultanteColonne, posResultanteLigne].lettre == 'R')
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
        /// Légalité du coup </summary>
        virtual public Boolean légal
        {
            get
            {
                // la légalité dépend de la position après le coup
                return posResultante.légale;
            }
        }

        /// <summary>
        /// Position avant le coup </summary>
        public Position positionInitiale
        {
            get
            {
                return posInitiale;
            }
        }

        /// <summary>
        /// Position après le coup </summary>
        public Position positionResultante
        {
            get
            {
                return posResultante;
            }
        }

        /// <summary>
        /// Coordonnée X de la case de départ </summary>
        public int colonne1
        {
            get
            {
                return posInitialeColonne;
            }
        }

        /// <summary>
        /// Coordonnée X de la case d'arrivée </summary>
        public int colonne2
        {
            get
            {
                return posResultanteColonne;
            }
        }

        /// <summary>
        /// Coordonnée Y de la case de départ </summary>
        public int ligne1
        {
            get
            {
                return posInitialeLigne;
            }
        }

        /// <summary>
        /// Coordonnée Y de la case d'arrivée </summary>
        public int ligne2
        {
            get
            {
                return posResultanteLigne;
            }
        }

        /// <summary>
        /// Code lettre de la case de départ </summary>
        protected String lettrePosition1
        {
            get
            {
                char lettre = positionInitiale.Case[colonne1, ligne1].lettre;
                if (lettre != ' ' && lettre != 'P')
                {
                    return lettre.ToString();
                }
                else
                {
                    return "";
                }
            }
        }

        /// <summary>
        /// Position de départ en algèbre échiquéen </summary>
        protected String position1algebrique
        {
            get
            {
                return Convert.ToChar(colonne1 + 97).ToString() +
                    (ligne1 + 1).ToString();
            }
        }

        /// <summary>
        /// Position d'arrivée en algèbre échiquéen </summary>
        protected String position2algebrique
        {
            get
            {
                return Convert.ToChar(colonne2 + 97).ToString() +
                    (ligne2 + 1).ToString();
            }
        }

        /// <summary>
        /// Notation algébrique formatée pour moteur UCI </summary>
        virtual public String notationAlgebriqueUCI
        {
            get
            {
                String notation = this.position1algebrique + this.position2algebrique;

                return notation;
            }
        }

        /// <summary>
        /// Notation algébrique destinée à la feuille de jeu </summary>
        virtual public String notationAlgebrique
        {
            get
            {
                String notation =
                    this.lettrePosition1 +
                    this.position1algebrique +
                    "-" +
                    this.position2algebrique;

                return notation;
            }
        }

    }
}
