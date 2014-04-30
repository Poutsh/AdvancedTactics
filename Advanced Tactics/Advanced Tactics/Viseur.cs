using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        Data data;
        public KeyboardState oldKey, curKey;
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

        public Viseur(Data data, Cell[,] map)
        {
            this.data = data;
            this.map = map;
            viseur = new Unit();
            Init();

        }

        #endregion

        // // // // // // // // 

        #region INIT & LOAD

        void Init()
        {
            spviseur = new Sprite(); spviseur.LC(data.Content, "Curseur/viseur");
            sblinkviseur = new Sprite(); sblinkviseur.LC(data.Content, "Curseur/viseurS");
            Viseurbleu = new Sprite(); Viseurbleu.LC(data.Content, "Curseur/viseurB");
            Viseurrouge = new Sprite(); Viseurrouge.LC(data.Content, "Curseur/viseurR");
            Viseurnormal = new Sprite(); Viseurnormal.LC(data.Content, "Curseur/viseur");
            spCaserouge = new Sprite(); spCaserouge.LC(data.Content, "Case/rouge");
            spCasebleu = new Sprite(); spCasebleu.LC(data.Content, "Case/bleu");
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
            destSelec = false; destPos = Vector.Zero;
            UnitTemp = new Unit();
        }

        void doMoveUnit(Unit unit, Cell newCell, List<Unit> ListOfUnit)
        {
            CtrlZ = new List<Vector>(2);
            CtrlZ.Add(destPos); CtrlZ.Add(depPos);
            unit = new Unit(data, unit, map, newCell, ListOfUnit);
            Reset();
        }

        void getMovingPath(List<Unit> ListOfUnit, GameTime gameTime)
        {
            curKey = Keyboard.GetState();

            if ((curKey.IsKeyDown(Keys.LeftControl) || curKey.IsKeyDown(Keys.RightControl)) && curKey.IsKeyDown(Keys.Z) && CtrlZ[1] != Vector.Zero)
            {
                doMoveUnit(map[CtrlZ[0].X, CtrlZ[0].Y].unitOfCell, map[CtrlZ[1].X, CtrlZ[1].Y], ListOfUnit);
            }

            if (depSelec && !destSelec && curKey.IsKeyDown(Keys.R)) { Reset(); }

            if (map[viseurX, viseurY].unitOfCell == null && depSelec && coordViseur != depPos && curKey.IsKeyDown(Keys.W))
            {
                if (UnitTemp.Mvt.Contains(data.altitudeTerrain[viseurX, viseurY]))
                {
                    destSelec = true;
                    destPos = new Vector(coordViseur.X, coordViseur.Y);
                    doMoveUnit(map[depPos.X, depPos.Y].unitOfCell, map[destPos.X, destPos.Y], ListOfUnit);
                }
            }
            else if (ViseurOverUnit && !depSelec && curKey.IsKeyDown(Keys.Q))
            {
                depSelec = true;
                depPos = new Vector(coordViseur.X, coordViseur.Y);
                UnitTemp = map[viseurX, viseurY].unitOfCell;
            }
        }

        void ViseurColor()
        {
            if (UnitTemp != null && !UnitTemp.Mvt.Contains(data.altitudeTerrain[viseurX, viseurY]) && depSelec)
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

            sblinkviseur.Draw(data, spriteBatch, gameTime, sblinkviseur.Position, blinkviseur);
        }

        #endregion

        // // // // // // // // 

        #region UPDATE

        public virtual void Update(GameTime gameTime, List<Unit> ListOfUnit)
        {
            curKey = Keyboard.GetState();
            float tempo;

            getMovingPath(ListOfUnit, gameTime);
            ViseurColor();

            #region Gestion des mouvements du viseurs

            if (curKey.IsKeyDown(Keys.LeftShift)) tempo = 0.08f; else tempo = 0.15f;

            if (gameTime.TotalGameTime - time > TimeSpan.FromSeconds(tempo) || curKey != oldKey)
            {
                time = gameTime.TotalGameTime;

                if (coordViseur.X == 0 && curKey.IsKeyDown(Keys.Left))
                    coord.X = data.WidthMap - 1;
                else
                    if (curKey.IsKeyDown(Keys.Left))
                        --coord.X;

                if (coordViseur.X == data.WidthMap - 1 && curKey.IsKeyDown(Keys.Right))
                    coord.X = 0;
                else
                    if (curKey.IsKeyDown(Keys.Right))
                        ++coord.X;

                if (coordViseur.Y == 0 && curKey.IsKeyDown(Keys.Up))
                    coord.Y = data.HeightMap - 1;
                else
                    if (curKey.IsKeyDown(Keys.Up))
                        --coord.Y;

                if (coordViseur.Y == data.HeightMap - 1 && curKey.IsKeyDown(Keys.Down))
                    coord.Y = 0;
                else
                    if (curKey.IsKeyDown(Keys.Down))
                        ++coord.Y;
            }
            #endregion

            oldKey = curKey;
        }

        #endregion

        // // // // // // // // 

        #region DRAW

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            BlinkSprite(gameTime, blinkviseur, spriteBatch);
            spviseur.Draw(data, spriteBatch, gameTime, map[viseurX, viseurY].positionPixel);
        }

        #endregion
    }
}
