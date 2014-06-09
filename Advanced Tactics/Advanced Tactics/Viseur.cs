using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Design;
using System.Media;

namespace Advanced_Tactics
{
    public class Viseur
    {
        #region VARIABLES

        public enum Key { Q, W, A, Z, LeftControl, LeftShift, R, C }
        Data data;
        private KeyboardState oldKey, curKey;
        private KeyboardState oldKey2, curKey2;
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
        public Vector coordViseur2 { get { return new Vector(coord.X, coord.Y); } }

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
            spviseur = new Sprite(); spviseur.LC(Game1.Ctt, "Curseur/viseur");
            sblinkviseur = new Sprite(); sblinkviseur.LC(Game1.Ctt, "Curseur/viseurS");
            Viseurbleu = new Sprite(); Viseurbleu.LC(Game1.Ctt, "Curseur/viseurB");
            Viseurrouge = new Sprite(); Viseurrouge.LC(Game1.Ctt, "Curseur/viseurR");
            Viseurnormal = new Sprite(); Viseurnormal.LC(Game1.Ctt, "Curseur/viseur");
            spCaserouge = new Sprite(); spCaserouge.LC(Game1.Ctt, "Case/rouge");
            spCasebleu = new Sprite(); spCasebleu.LC(Game1.Ctt, "Case/bleu");
        }

        #endregion

        // // // // // // // // 

        #region FONCTIONS DU VISEUR

        public bool ViseurOverPos(Vector position) { return coordViseur == position; }

        public bool Contains<T>(List<T> List, T tocompare) { if (List != null)return List.Contains(tocompare); else return false; }

        public void OverUnit()
        {
            if (map[viseurX, viseurY].Occupe) UnitInCell = map[viseurX, viseurY].unitOfCell;
        }

        private void Reset()
        {
            depSelec = false; depPos = Vector.Zero;
            destSelec = false; destPos = Vector.Zero;
            UnitTemp = new Unit();
        }

        private void doMoveUnit(Unit unit, Cell newCell, List<Unit> ListOfUnit)
        {
            CtrlZ = new List<Vector>(2);
            CtrlZ.Add(destPos); CtrlZ.Add(depPos);
            if (Contains<Vector>(unit.MvtPossible, new Vector(newCell.XofCell, newCell.YofCell)))
            {
                unit = new Unit(data, unit, map, newCell, ListOfUnit);
            }
            Reset();
        }

        private void getMovingPath(List<Unit> ListOfUnit, GameTime gameTime, SpriteBatch spriteBatch)
        {
            curKey = Keyboard.GetState();

            /// Ctrlz
            if (WasJustPressed(Key.LeftControl) && WasJustPressed(Key.Z) && CtrlZ[1] != Vector.Zero)
                doMoveUnit(map[CtrlZ[0].X, CtrlZ[0].Y].unitOfCell, map[CtrlZ[1].X, CtrlZ[1].Y], ListOfUnit);


            /// Touche Reset
            if (depSelec && !destSelec && WasJustPressed(Key.R)) { Reset(); }


            /// Touche Attack
            if (UnitTemp != null && ViseurOverUnit && Contains<Vector>(UnitTemp.MvtPossible, new Vector(coordViseur.X, coordViseur.Y)) && (WasJustPressed(Key.W) || WasJustPressed(Key.C)))
            {
                if (WasJustPressed(Key.C))
                    map[viseurX, viseurY].unitOfCell.PV -= 1000;
                else
                    map[viseurX, viseurY].unitOfCell.PV -= map[depPos.X, depPos.Y].unitOfCell.Strength;


                if (map[viseurX, viseurY].unitOfCell.PV <= 0)
                {
                    map[viseurX, viseurY].unitOfCell = new Unit(data, map[viseurX, viseurY].unitOfCell, map, ListOfUnit);
                    doMoveUnit(map[depPos.X, depPos.Y].unitOfCell, map[viseurX, viseurY], ListOfUnit);
                    Explosion();
                }
                Reset();
            }


            /// Deplacement
            if (depSelec && !ViseurOverPos(depPos) && WasJustPressed(Key.W))
            {
                if (UnitTemp != null && !ViseurOverUnit && Contains<int>(UnitTemp.TerrainPossible, data.altitudeTerrain[viseurX, viseurY]))
                {
                    destSelec = true;
                    destPos = new Vector(coordViseur.X, coordViseur.Y);
                    doMoveUnit(map[depPos.X, depPos.Y].unitOfCell, map[destPos.X, destPos.Y], ListOfUnit);
                }
            }
            else if (ViseurOverUnit && !depSelec && WasJustPressed(Key.Q))
            {
                depSelec = true;
                depPos = new Vector(coordViseur.X, coordViseur.Y);
                UnitTemp = map[viseurX, viseurY].unitOfCell;
            }
        }

        private void ViseurColor()
        {
            if (UnitTemp != null && depSelec && (!Contains<int>(UnitTemp.TerrainPossible, data.altitudeTerrain[viseurX, viseurY]) || !Contains<Vector>(UnitTemp.MvtPossible, new Vector(coordViseur.X, coordViseur.Y))))
                spviseur = Viseurrouge;
            else if (!map[viseurX, viseurY].Occupe && !depSelec && !destSelec)
                spviseur = Viseurnormal;
            else if (depSelec || (map[viseurX, viseurY].Occupe && !depSelec))
                spviseur = Viseurbleu;
        }

        private void BlinkSprite(GameTime gameTime, bool blinkviseur, SpriteBatch spriteBatch)
        {
            if (depSelec && !destSelec)
            {
                foreach (Vector item in map[depPos.X, depPos.Y].unitOfCell.MvtPossible)
                {
                    if (map[depPos.X, depPos.Y].unitOfCell.TerrainPossible.Contains(data.altitudeTerrain[item.X, item.Y]))
                        spCasebleu.Draw(data, spriteBatch, gameTime, map[item.X, item.Y].positionPixel);
                }
                if (map[viseurX, viseurY].Vector2OfCell == depPos)
                    sblinkviseur.Position = map[viseurX, viseurY].positionPixel;
                blinkviseur = depSelec;
            }

            sblinkviseur.Draw(data, spriteBatch, gameTime, sblinkviseur.Position, blinkviseur);
        }

        private void Explosion()
        {
            SoundPlayer player = new SoundPlayer(Resource.explosound);
            player.Play();
        }

        #endregion

        // // // // // // // // 

        #region UPDATE

        public virtual void Update(GameTime gameTime, List<Unit> ListOfUnit, SpriteBatch spriteBatch)
        {
            curKey2 = Keyboard.GetState();
            float tempo;

            getMovingPath(ListOfUnit, gameTime, spriteBatch);
            ViseurColor();

            #region Gestion des mouvements du viseurs

            if (WasJustPressed(Key.LeftShift)) tempo = 0.08f; else tempo = 0.15f;

            if (gameTime.TotalGameTime - time > TimeSpan.FromSeconds(tempo) || curKey2 != oldKey2)
            {
                time = gameTime.TotalGameTime;

                if (coordViseur.X == 0 && curKey2.IsKeyDown(Keys.Left))
                    coord.X = data.WidthMap - 1;
                else
                    if (curKey2.IsKeyDown(Keys.Left))
                        --coord.X;

                if (coordViseur.X == data.WidthMap - 1 && curKey2.IsKeyDown(Keys.Right))
                    coord.X = 0;
                else
                    if (curKey2.IsKeyDown(Keys.Right))
                        ++coord.X;

                if (coordViseur.Y == 0 && curKey2.IsKeyDown(Keys.Up))
                    coord.Y = data.HeightMap - 1;
                else
                    if (curKey2.IsKeyDown(Keys.Up))
                        --coord.Y;

                if (coordViseur.Y == data.HeightMap - 1 && curKey2.IsKeyDown(Keys.Down))
                    coord.Y = 0;
                else
                    if (curKey2.IsKeyDown(Keys.Down))
                        ++coord.Y;
            }
            #endregion

            oldKey2 = curKey2;
        }

        #endregion

        // // // // // // // // 

        #region DRAW

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            BlinkSprite(gameTime, blinkviseur, spriteBatch);
            spviseur.Draw(data, spriteBatch, gameTime, map[viseurX, viseurY].positionPixel);
            if (UnitTemp != null)
            {
                //for (int i = 0; i < UnitTemp.MvtPossible.Count(); i++)
                //{
                //    spCasebleu.Draw(data, spriteBatch, gameTime, new Vector2(UnitTemp.MvtPossible[i].X, UnitTemp.MvtPossible[i].Y));
                //}
            }

        }

        #endregion

        // // // // // // // //

        #region HELPER
        private bool WasJustPressed(Key button)
        {
            curKey = Keyboard.GetState();
            switch (button)
            {
                case Key.Q:
                    return curKey.IsKeyDown(Keys.Q) && oldKey != curKey;

                case Key.C:
                    return curKey.IsKeyDown(Keys.C) && oldKey != curKey;

                case Key.W:
                    return curKey.IsKeyDown(Keys.W) && oldKey != curKey;

                case Key.A:
                    return curKey.IsKeyDown(Keys.A) && oldKey != curKey;

                case Key.Z:
                    return curKey.IsKeyDown(Keys.Z) && oldKey != curKey;

                case Key.LeftControl:
                    return curKey.IsKeyDown(Keys.LeftControl) && oldKey != curKey;

                case Key.R:
                    return curKey.IsKeyDown(Keys.R) && oldKey != curKey;
            }
            oldKey = curKey;
            return false;
        }
        #endregion
    }
}
