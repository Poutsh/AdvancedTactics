using System;
using System.Collections.Generic;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Advanced_Tactics
{
    public class Map
    {
        int CaseHeight, CaseWidth;

        private Case[,] TailleCarte;
        private Joueur joueur;

        // Coups permis au joueur pour cette position
        private ArrayList CoupsPermis = null;

        public Map(Joueur player)
        {
            this.TailleCarte = new Case[20, 20];
            this.joueur = player;
        }

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

        /* BUGGY
         
        public Map_20x20()
        {
            // Taille de la map initialisée

            this.TailleCarte = new Case[20, 20];

            // Joueur 1

            this.joueur = Player.Player1;
            Pion pion = new Pion(joueur);
            pion = new Pion(joueur);

            Convert.ChangeType(pion, );

            // Pièces installées

            this.TailleCarte[0, 0] = new Tour(Player.Player1);
            this.TailleCarte[1, 0] = new Cavalier(Player.Player1);
            this.TailleCarte[2, 0] = new Fou(Player.Player1);
            this.TailleCarte[3, 0] = new Dame(Player.Player1);
            this.TailleCarte[4, 0] = new Roi(Player.Player1);
            this.TailleCarte[5, 0] = new Fou(Player.Player1);
            this.TailleCarte[6, 0] = new Cavalier(Player.Player1);
            this.TailleCarte[7, 0] = new Tour(Player.Player1);
            this.TailleCarte[0, 1] = new Pion(Player.Player1);
            this.TailleCarte[1, 1] = new Case(pion);
            this.TailleCarte[2, 1] = new Case(pion);
            this.TailleCarte[3, 1] = new Case(pion);
            this.TailleCarte[4, 1] = new Case(pion);
            this.TailleCarte[5, 1] = new Case(pion);
            this.TailleCarte[6, 1] = new Case(pion);
            this.TailleCarte[7, 1] = new Case(pion);
            
         */


        /* TEST
         * 
         * 
        public class Plateau
        {
            int CaseHeight, CaseWidth;

            Rectangle plateauRectangle;

            Texture2D background;
            Texture2D casePoss;

            SpriteFont fontText;
            Color couleurCase = Color.Blue;
            bool pieceIsSelected, isEchec, isEchecEtMat;

            Case caseSelected = null;

            Case[,] plateauValeur;

            Selection rectangleSelection;

            Player playerTour;

            List<Point> positionPossibilities;

        
            public bool IsEchecEtMat
            {
                get { return isEchecEtMat; }
                set { isEchecEtMat = value; }
            }

            public Rectangle PlateauRectangle
            {
                get { return plateauRectangle; }
                set { plateauRectangle = value; }
            }

            public bool PieceIsSelected
            {
                get { return pieceIsSelected; }
            }

            public Plateau()
            {
                rectangleSelection = new Selection();
                plateauRectangle = new Rectangle(0, 0, 640, 480);
                plateauValeur = new Case[20, 20];
                positionPossibilities = new List<Point>();
                playerTour = Player.Player1;

                pieceIsSelected = false;
                isEchec = false;
                isEchecEtMat = false;

                CaseHeight = 32;
                CaseWidth = 32;
            }

            public void Initialize()
            {
                positionPossibilities.Clear();

                for (int y = 0; y < 20; y++)
                {
                    for (int x = 0; x < 20; x++)
                    {
                        plateauValeur[y, x] = new Case();
                        plateauValeur[y, x].Position = new Point(x, y);
                    }
                }

                for (int x = 0; x < 20; x++)
                {
                    Pion pionNoir = new Pion(Player.Player2);
                    plateauValeur[1, x].UnePiece = pionNoir;

                    Pion pionBlanc = new Pion(Player.Player1);
                    plateauValeur[6, x].UnePiece = pionBlanc;
                }

                for (int y = 0; y < 2; y++)
                {
                    Player player;

                    if (y % 2 == 1)
                        player = Player.Player1;
                    else
                        player = Player.Player2;

                    plateauValeur[y * 7, 0].UnePiece = new Tour(player);
                    plateauValeur[y * 7, 7].UnePiece = new Tour(player);

                    plateauValeur[y * 7, 1].UnePiece = new Cavalier(player);
                    plateauValeur[y * 7, 6].UnePiece = new Cavalier(player);

                    plateauValeur[y * 7, 2].UnePiece = new Fou(player);
                    plateauValeur[y * 7, 5].UnePiece = new Fou(player);

                    plateauValeur[y * 7, 3].UnePiece = new Reine(player);
                    plateauValeur[y * 7, 4].UnePiece = new Roi(player);
                }
            }

            /// <summary>
            /// Charge les différentes images / polices
            /// </summary>
            /// <param name="Content">Gestionnaire de contenue</param>
            public void LoadContent(ContentManager Content)
            {
                /* rectangleSelection.LoadContent(Content, "selection");

                background = Content.Load<Texture2D>("bg");

                /* Police font
                 * fontText = Content.Load<SpriteFont>();

                casePoss = Content.Load<Texture2D>("case"); *

                for (int y = 0; y < 8; y++)
                {
                    for (int x = 0; x < 8; x++)
                    {
                        Piece piece = plateauValeur[y, x].UnePiece;

                        if (piece != null)
                        {
                            piece.LoadContent(Content);
                        }
                    }
                }
            }

            /// Méthode de dessin

            public void Draw(SpriteBatch spriteBatch)
            {
                DrawBoard(spriteBatch);
                DrawPiece(spriteBatch);
                DrawPossibilites(spriteBatch);
                DrawText(spriteBatch);
            }

            /// Dessine le plateau
            
            private void DrawBoard(SpriteBatch spriteBatch)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(background, plateauRectangle, Color.White);
                spriteBatch.End();
            }

            
            /// Dessine les textes
            private void DrawText(SpriteBatch spriteBatch)
            {
                spriteBatch.Begin();
                String message;
                if (playerTour == Player.Player1)
                    message = "Au joueur 1 de jouer";
                else
                    message = "Au joueur 2 de jouer";

                spriteBatch.DrawString(fontText, message, new Vector2(0, plateauRectangle.Height), Color.Black);

                if (isEchec)
                {
                    spriteBatch.DrawString(fontText, "ECHEC", new Vector2(0, plateauRectangle.Height + 20), Color.Red);
                }
                else if (pieceIsSelected && !isEchec)
                {
                    spriteBatch.DrawString(fontText, "Selectionnez une destination", new Vector2(0, plateauRectangle.Height + 20), Color.Black);
                }

                spriteBatch.End();
            }

            /// Dessine les pièces du plateau
            
            private void DrawPiece(SpriteBatch spriteBatch)
            {
                spriteBatch.Begin();
                for (int y = 0; y < 20; y++)
                {
                    for (int x = 0; x < 20; x++)
                    {
                        Piece piece = plateauValeur[y, x].UnePiece;

                        if (piece != null)
                        {
                            spriteBatch.Draw(piece.Texture, new Rectangle(x * CaseWidth, y * CaseHeight, CaseWidth, CaseHeight), Color.White);
                        }
                    }
                }
                if (!rectangleSelection.IsHide)
                    spriteBatch.Draw(rectangleSelection.Texture, rectangleSelection.Rectangle, Color.White);

                spriteBatch.End();
            }


            /// Retourne la case
            
            private Case getCase(int x, int y)
            {
                return plateauValeur[y, x];
            }


            /// Retourne une piece
            
            private Piece getPiece(int x, int y)
            {
                return plateauValeur[y, x].UnePiece;
            }


            /// Sélectionne la pièce sous le curseur
            
            public void selectPiece(Point mouseCoord)
            {
                int x = mouseCoord.X - (mouseCoord.X % CaseWidth);
                int y = mouseCoord.Y - (mouseCoord.Y % CaseHeight);

                if (new Rectangle(x, y, 1, 1).Intersects(plateauRectangle))
                {
                    caseSelected = getCase(x / CaseWidth, y / CaseHeight);



                    if (caseSelected.UnePiece != null && caseSelected.UnePiece.Couleur == playerTour)
                    {
                        if (isEchec)
                        {
                            if (!caseSelected.Equals(getKingCase(ref plateauValeur)))
                            {
                                ReleaseSelection();
                                return;
                            }
                        }

                        rectangleSelection.IsHide = false;
                        rectangleSelection.MoveTo(new Point(x, y));
                        pieceIsSelected = true;
                        TestPossibilities(ref plateauValeur, caseSelected, positionPossibilities);
                    }
                }
            }


            /// Déplace la pièce
            
            public void movePiece(Point mouseCoord)
            {
                int x = mouseCoord.X - (mouseCoord.X % CaseWidth);
                int y = mouseCoord.Y - (mouseCoord.Y % CaseHeight);

                if (new Rectangle(x, y, 1, 1).Intersects(plateauRectangle))
                {

                    int caseX = x / CaseWidth;
                    int caseY = y / CaseHeight;

                    if (getCase(caseX, caseY).UnePiece == null || getCase(caseX, caseY).UnePiece.Couleur != playerTour)
                    {
                        if (caseSelected.UnePiece.IsValidMouvement(ref plateauValeur, new Point(caseX, caseY), caseSelected.Position))
                        {
                            plateauValeur[caseY, caseX].UnePiece = caseSelected.UnePiece;

                            if (caseSelected.UnePiece.GetType() == typeof(Pion))
                            {
                                ((Pion)plateauValeur[caseY, caseX].UnePiece).FirstMove = false;
                            }

                            caseSelected.UnePiece = null;

                            if (playerTour == Player.Player1)
                                playerTour = Player.Player2;
                            else
                                playerTour = Player.Player1;

                            if (CheckEchec(ref plateauValeur))
                            {
                                isEchec = true;
                                isEchecEtMat = CheckEchecEtMat();
                            }

                            ReleaseSelection();
                        }
                    }
                }
            }

            /// Enlève la sélection
            
            public void ReleaseSelection()
            {
                rectangleSelection.IsHide = true;
                pieceIsSelected = false;
                caseSelected = null;
                positionPossibilities.Clear();
            }


            /// Teste les possibilités
            
            public void TestPossibilities(ref Case[,] plateau, Case caseSelectionnee, List<Point> possibilites)
            {
                for (int y = 0; y < 8; y++)
                {
                    for (int x = 0; x < 8; x++)
                    {
                        if (caseSelectionnee.UnePiece.IsValidMouvement(ref plateau, new Point(x, y), caseSelectionnee.Position))
                            possibilites.Add(new Point(x, y));
                    }
                }
            }

            /// Dessine les possibilités
            
            public void DrawPossibilites(SpriteBatch spriteBatch)
            {
                if (positionPossibilities.Count > 0)
                {
                    couleurCase.A = 50;
                    spriteBatch.Begin();
                    foreach (Point point in positionPossibilities)
                    {
                        spriteBatch.Draw(casePoss, new Rectangle(point.X * casePoss.Width, point.Y * casePoss.Height, casePoss.Width, casePoss.Height), couleurCase);
                    }
                    spriteBatch.End();
                }
            }
         
            /// Retourne la case ou se trouve le roi courant
         
            private Case getKingCase(ref Case[,] plateau)
            {
                for (int y = 0; y < 8; y++)
                {
                    for (int x = 0; x < 8; x++)
                    {
                        if (plateau[y, x].UnePiece != null)
                        {
                            if (plateau[y, x].UnePiece.GetType().Equals(typeof(Roi)) && plateau[y, x].UnePiece.Couleur == playerTour)
                            {
                                return plateau[y, x];
                            }
                        }
                    }
                }

                return null;
            }

            /// Vérifie l'état d'échec de la partie
            
            private bool CheckEchec(ref Case[,] plateau)
            {
                Case caseRoi = getKingCase(ref plateau);

                if (caseRoi != null)
                {
                    for (int y = 0; y < 8; y++)
                    {
                        for (int x = 0; x < 8; x++)
                        {
                            if (plateau[y, x].UnePiece != null && plateau[y, x].UnePiece.Couleur != playerTour)
                                if (plateau[y, x].UnePiece.IsValidMouvement(ref plateau, caseRoi.Position, plateau[y, x].Position))
                                    return true;
                        }
                    }
                }

                return false;
            }

            /// Vérifie l'état d'échec et mat et termine la partie
            
            private bool CheckEchecEtMat()
            {
                Case[,] plateauTmp = new Case[20, 20];

                for (int y = 0; y < 20; y++)
                {
                    for (int x = 0; x < 20; x++)
                    {
                        plateauTmp[y, x] = new Case(plateauValeur[y, x]);
                    }
                }

                List<Point> possibilites = new List<Point>();

                Case king = getKingCase(ref plateauTmp);
                TestPossibilities(ref plateauTmp, king, possibilites);

                foreach (Point position in possibilites)
                {
                    king.Position = position;

                    if (!CheckEchec(ref plateauTmp))
                    {
                        return false;
                    }

                }

                return true;
            }
        }

        */
    }
}
