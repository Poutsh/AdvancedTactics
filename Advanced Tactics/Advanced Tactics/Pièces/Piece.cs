using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Advanced_Tactics
{
    public abstract class Piece : Sprites
    {
        Player couleur;
        String nom;

        public Player Couleur
        {
            get { return couleur; }
            set { couleur = value; }
        }

        public Piece(Player couleur, String nom)
        {
            if (couleur == Player.Player1)
                this.nom = nom + "B";
            else
                this.nom = nom + "N";

            this.couleur = couleur;
        }

        //Mvt piece valide ?

        public abstract bool IsValidMouvement(ref Case[,] plateau, Point destination, Point currentPosition);

        public virtual void Move()
        {

        }

        public void LoadContent(ContentManager Content)
        {
            this.Texture = Content.Load<Texture2D>(nom);
        }
    }
}
