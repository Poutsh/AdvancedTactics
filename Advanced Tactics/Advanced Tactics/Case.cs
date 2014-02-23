using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Advanced_Tactics
{
    /// <summary>
    /// Case d'une position sur l'échiquier </summary>
    /// <remarks>
    /// Une position comprend les 400 cases de l'échiquier
    /// Cette case peut être soit libre, soit occupée par une pièce (héritage en Piece)</remarks>
    public class Case
    {
        /// <summary>
        /// La couleur de la pièce (appatient au joueur blanc ou au joueur noir) </summary>
        protected Joueur player = Joueur.Player1;

        /// <summary>
        /// Code de la pièce : Caractère blanc par défaut (lecture seule) </summary>
        virtual public char lettre
        {
            get
            {
                return ' ';
            }
        }

        /// <summary>
        /// Couleur de la pièce (lecture seule) </summary>
        public Joueur joueur
        {
            get
            {
                return player;
            }
        }

        /// <summary>
        /// Par défaut, la case est innocppée (lecture seule) </summary>
        virtual public Boolean occupé
        {
            get
            {
                return false;
            }
        }

        /// Calculer les coups possibles en démarrant de cette case. </summary>
        
        virtual public void coups(Position position, ArrayList coupsPossibles, int posColonne, int posLigne)
        {
            // aucun coup ne peut démarrer d'une case vide
        }


        /// Obtenir un clône de la case pour les positions de jeu suivantes</summary>
        
        virtual public Case positionSuivanteCase(Boolean deplacement = false)
        {
            // clone pour position suivante
            Case posSuivanteCase = new Case();
            return posSuivanteCase;
        }



    }
}
