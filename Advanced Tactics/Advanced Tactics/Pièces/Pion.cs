using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Advanced_Tactics
{
    public class Pion : Piece
    {
       private bool firstMove;

        public bool FirstMove
        {
            get { return firstMove; }
            set { firstMove = value; }
        }

        public Pion(Player couleur) :
            base(couleur, "Pion")
        {
            firstMove = true;
        }

        public override bool IsValidMouvement(ref Case[,] plateau, Point destination, Point currentPosition)
        {
            if (this.Couleur == Player.Player2)
            {
                if (plateau[destination.Y, destination.X].UnePiece == null)
                {
                    if (firstMove)
                    {
                        if (destination.Y == currentPosition.Y + 2 && destination.X == currentPosition.X)
                        {
                            return true;
                        }

                        if (destination.Y == currentPosition.Y + 1 && destination.X == currentPosition.X)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        if (destination.Y == currentPosition.Y + 1 && destination.X == currentPosition.X)
                            return true;
                    }
                }
                else if (plateau[destination.Y, destination.X].UnePiece.Couleur == Player.Player1)
                {
                    if (destination.Y == currentPosition.Y + 1 && (destination.X == currentPosition.X - 1 || destination.X == currentPosition.X + 1))
                        return true;
                }
            }
            else if (this.Couleur == Player.Player1)
            {
                if (plateau[destination.Y, destination.X].UnePiece == null)
                {
                    if (firstMove)
                    {
                        if (destination.Y == currentPosition.Y - 2 && destination.X == currentPosition.X)
                        {
                            return true;
                        }

                        if (destination.Y == currentPosition.Y - 1 && destination.X == currentPosition.X)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        if (destination.Y == currentPosition.Y - 1 && destination.X == currentPosition.X)
                            return true;
                    }
                }
                else if (plateau[destination.Y, destination.X].UnePiece.Couleur == Player.Player2)
                {
                    if (destination.Y == currentPosition.Y - 1 && (destination.X == currentPosition.X + 1 || destination.X == currentPosition.X - 1))
                        return true;
                }
            }

            return false;
        }
    }
}
