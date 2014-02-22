using System;
using System.Collections.Generic;using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Advanced_Tactics
{
    public class Map
    {
        int CaseHeight, CaseWidth;

        Rectangle TailleCarte;

        // Skin de la map
        Texture2D background;
        Texture2D casePoss;

        // Police menus
        SpriteFont font;

        // Selon cas, changer couleur
        Color colorCase;
        bool PieceSelected, check, checkmate;

        Case caseSelected = null;
        Case[,] Carte;


        /*
        
        public static Case[,] map_creator(Case[,] carte, int H, int W, int[,] numero)
        {
            carte = new Case[H, W];

            for (int i = 0; i < H; i++)
            {
                for (int j = 0; i < W; j++)
                {
                    switch (numero[H,W])
                    {
                        case 1 :
                            carte[H, W] = new Case(H, W, 1);
                            break;
                        case 2 :
                            carte[H, W] = new Case(H, W, 2);
                            break;
                        case 3 :
                            carte[H, W] = new Case(H, W, 3);
                            break;
                        case 0 :
                            carte[H, W] = new Case(H, W, 0);
                            break;
                    }

                }
            }
            return carte;
         

        }
        
        */

       /* public Map_20x20()
        {

        }*/

        }
}
