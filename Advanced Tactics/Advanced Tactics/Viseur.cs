using System;
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

        public Unit UnitTemp { get; set; }
        public bool depSelec { get; set; }
        public Vector depPos { get; set; }
        public bool destSelec { get; set; }
        public Vector destPos { get; set; }
        List<Vector> CtrlZ;

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
            depSelec = false; depPos = Vector.Zero;
            destSelec = false; depPos = Vector.Zero;
            UnitTemp = new Unit();
        }

        void doMoveUnit(Unit unit, Cell newCell, List<Unit> ListOfUnit)
        {
            unit = new Unit(unit, map, newCell, ListOfUnit);
            CtrlZ = new List<Vector>(2);
            CtrlZ.Add(destPos); CtrlZ.Add(depPos);
            Reset();
        }

        //Func<string, string, Map, int, int, List<Unit>, Unit, Unit> Rdunit = (r, c, m, x, y, l, u) => new Unit(r, c, m.Carte, x, y, l);

        void getMovingPath(List<Unit> ListOfUnit, GameTime gameTime)
        {
            currentKeyboardState = Keyboard.GetState();

            if (currentKeyboardState.IsKeyDown(Keys.LeftControl) && currentKeyboardState.IsKeyDown(Keys.Z) && CtrlZ[1] != Vector.Zero)
            {
                doMoveUnit(map[CtrlZ[1].X, CtrlZ[1].Y].unitOfCell, map[CtrlZ[2].X, CtrlZ[2].Y], ListOfUnit);
            }

            if (depSelec && !destSelec && currentKeyboardState.IsKeyDown(Keys.R))
            {
                Reset();
            }
            else if (map[viseurX, viseurY].unitOfCell == null && depSelec && coordViseur != depPos && currentKeyboardState.IsKeyDown(Keys.W))
            {
                if (UnitTemp.Mvt.Contains(var.altitudeTerrain[viseurX, viseurY]))
                {
                    destSelec = true;
                    destPos = new Vector(coordViseur.X, coordViseur.Y);
                    doMoveUnit(map[depPos.X, depPos.Y].unitOfCell, map[destPos.X, destPos.Y], ListOfUnit);
                }
            }
            else if (ViseurOverUnit && !depSelec && currentKeyboardState.IsKeyDown(Keys.Q))
            {
                depSelec = true;
                //depPos = new Vector(coordViseur.X, coordViseur.Y);
                depPos = new Vector(coordViseur.X, coordViseur.Y);
                UnitTemp = map[viseurX, viseurY].unitOfCell;
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

        public virtual void Update(GameTime gameTime, List<Unit> ListOfUnit)
        {
            currentKeyboardState = Keyboard.GetState();
            float tempo;

            getMovingPath(ListOfUnit, gameTime);
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
