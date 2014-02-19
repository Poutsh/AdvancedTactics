using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Advanced_Tactics
{
    /// <summary>
    /// Classe Case
    /// </summary>
    public class Case
    {
        Piece unePiece = null;
        Point position;

        /// <summary>
        /// Position x,y de la case
        /// </summary>
        public Point Position
        {
            get { return position; }
            set { position = value; }
        }

        /// <summary>
        /// Piéce contenue sur la case
        /// </summary>
        public Piece UnePiece
        {
            get { return unePiece; }
            set { unePiece = value; }
        }

        public Case()
        {
        }

        public Case(Case uneCase)
        {
            this.UnePiece = uneCase.UnePiece;
            this.Position = uneCase.Position;
        }
    }
}
