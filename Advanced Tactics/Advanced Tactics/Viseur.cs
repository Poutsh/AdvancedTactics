﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdvancedLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Design;

namespace Advanced_Tactics
{
    public class Viseur
    {
        #region VARIABLES

        private Constante var = Game1.cst;
        public ContentManager ctt = Game1.Ctt;
        public KeyboardState oldKeyboardState, currentKeyboardState;
        TimeSpan time;

        public Sprite spviseur, sblinkviseur;
        public Sprite spCaserouge, spCasebleu, Viseurbleu, Viseurrouge, Viseurnormal;
        public List<Sprite> ListSprite;

        public Cell[,] map;
        public Unit viseur;
        public bool blinkviseur;
        public Vector2 coord;

        public Unit UnitInCell { get; set; }
        public bool ViseurOverUnit { get { return map[viseurX, viseurY].Occupe; } }

        public int viseurX { get { return (int)coord.X; } }
        public int viseurY { get { return (int)coord.Y; } }
        public Vector2 coordViseur { get { return coord; } set { coord = value; } }


        public bool depSelec { get; set; }
        public Vector2 depPos { get; set; }
        public bool destSelec { get; set; }
        public Vector2 destPos { get; set; }

        public Sprite spriteViseur { get { return spviseur; } }
        #endregion

        // // // // // // // // 

        #region CONSTRUCTEURS


        public Viseur() { }

        public Viseur(Cell[,] map)
        {
            this.map = map;

            viseur = new Unit();
            Init();

        }

        #endregion

        // // // // // // // // 

        #region INIT & LOAD

        void Init()
        {
            spviseur = new Sprite(); spviseur.LC(ctt, "Curseur/viseur");
            sblinkviseur = new Sprite(); sblinkviseur.LC(ctt, "Curseur/viseurS");
            Viseurbleu = new Sprite(); Viseurbleu.LC(ctt, "Curseur/viseurB");
            Viseurrouge = new Sprite(); Viseurrouge.LC(ctt, "Curseur/viseurR");
            Viseurnormal = new Sprite(); Viseurnormal.LC(ctt, "Curseur/viseur");
            spCaserouge = new Sprite(); spCaserouge.LC(ctt, "Case/rouge");
            spCasebleu = new Sprite(); spCasebleu.LC(ctt, "Case/bleu");
        }

        #endregion

        // // // // // // // // 

        #region FONCTIONS DU VISEUR

        public void OverUnit()
        {
            if (map[viseurX, viseurY].Occupe) UnitInCell = map[viseurX, viseurY].unitOfCell;
        }

        void Reset()
        {
            depSelec = false; depPos = Vector2.Zero;
            destSelec = false; depPos = Vector2.Zero;
        }

        void doMoveUnit(Unit unit, Cell newCell, List<Unit> ListOfUnit)
        {
            unit = new Unit(unit, map, newCell, ListOfUnit);
            Reset();
        }

        void getMovingPath(List<Unit> ListOfUnit, GameTime gameTime, List<int> MvtPossibleOfUnit)
        {
            currentKeyboardState = Keyboard.GetState();

            if (MvtPossibleOfUnit.Contains(var.altitudeTerrain[viseurX, viseurY]) && map[viseurX, viseurY].unitOfCell == null && depSelec && coordViseur != depPos && currentKeyboardState.IsKeyDown(Keys.W))
            {
                destSelec = true;
                destPos = new Vector2(coordViseur.X, coordViseur.Y);
                doMoveUnit(map[(int)depPos.X, (int)depPos.Y].unitOfCell, map[(int)destPos.X, (int)destPos.Y], ListOfUnit);
            }
            else if (ViseurOverUnit && depSelec == false && currentKeyboardState.IsKeyDown(Keys.Q))
            {
                depSelec = true;
                depPos = new Vector2(coordViseur.X, coordViseur.Y);
            }
        }

        void ViseurColor()
        {
            if (var.altitudeTerrain[viseurX, viseurY] != 1 && depSelec)
                spviseur = Viseurrouge;
            else if (!map[viseurX, viseurY].Occupe && !depSelec && !destSelec)
                spviseur = Viseurnormal;
            else if (map[viseurX, viseurY].Occupe && depSelec && !destSelec)
                spviseur = Viseurrouge;
            else if (depSelec || (map[viseurX, viseurY].Occupe && !depSelec))
                spviseur = Viseurbleu;
        }

        void BlinkSprite(GameTime gameTime, bool blinkviseur, SpriteBatch spriteBatch)
        {
            if (depSelec && !destSelec)
            {
                if (map[viseurX, viseurY].VectorOfCell == depPos)
                    sblinkviseur.Position = map[viseurX, viseurY].positionPixel;
                blinkviseur = depSelec;
            }

            sblinkviseur.Draw(spriteBatch, gameTime, sblinkviseur.Position, blinkviseur);
        }

        #endregion

        // // // // // // // // 

        #region UPDATE

        public virtual void Update(GameTime gameTime, List<Unit> ListOfUnit, List<int> MvtPossibleOfUnit)
        {
            currentKeyboardState = Keyboard.GetState();
            float tempo;

            getMovingPath(ListOfUnit, gameTime, MvtPossibleOfUnit);
            ViseurColor();

            #region Gestion des mouvements du viseurs

            if (currentKeyboardState.IsKeyDown(Keys.LeftShift)) tempo = 0.08f; else tempo = 0.15f;

            if (gameTime.TotalGameTime - time > TimeSpan.FromSeconds(tempo) || currentKeyboardState != oldKeyboardState)
            {
                time = gameTime.TotalGameTime;

                if (coordViseur.X == 0 && currentKeyboardState.IsKeyDown(Keys.Left))
                    coord.X = var.WidthMap - 1;
                else
                    if (currentKeyboardState.IsKeyDown(Keys.Left))
                        --coord.X;

                if (coordViseur.X == var.WidthMap - 1 && currentKeyboardState.IsKeyDown(Keys.Right))
                    coord.X = 0;
                else
                    if (currentKeyboardState.IsKeyDown(Keys.Right))
                        ++coord.X;

                if (coordViseur.Y == 0 && currentKeyboardState.IsKeyDown(Keys.Up))
                    coord.Y = var.HeightMap - 1;
                else
                    if (currentKeyboardState.IsKeyDown(Keys.Up))
                        --coord.Y;

                if (coordViseur.Y == var.HeightMap - 1 && currentKeyboardState.IsKeyDown(Keys.Down))
                    coord.Y = 0;
                else
                    if (currentKeyboardState.IsKeyDown(Keys.Down))
                        ++coord.Y;
            }
            #endregion

            oldKeyboardState = currentKeyboardState;
        }

        #endregion

        // // // // // // // // 

        #region DRAW

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            BlinkSprite(gameTime, blinkviseur, spriteBatch);
            spviseur.Draw(spriteBatch, gameTime, map[viseurX, viseurY].positionPixel);
        }

        #endregion
    }
}
