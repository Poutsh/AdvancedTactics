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

        Data data;
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
        public bool build;
        List<Vector> CtrlZ;
        MouseState cur, old;
        Match Match;
        public Sprite spriteViseur { get { return spviseur; } }
        #endregion

        // // // // // // // // 

        #region CONSTRUCTEURS


        public Viseur() { }

        public Viseur(Data data, Cell[,] map, Match match)
        {
            this.data = data;
            this.map = map;
            Match = match;
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
            coord.X = map[data.MapWidth / 2, data.MapHeight / 2].XofCell;
            coord.Y = map[data.MapWidth / 2, data.MapHeight / 2].YofCell;
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
                unit = new Unit(data, unit, map, newCell, unit.Player, Match);
            }
            Reset();
            Match.TurnbyTurn.MvtCount++;
        }

        private void getMovingPath(List<Unit> ListOfUnit, GameTime gameTime, SpriteBatch spriteBatch)
        {
            cur = Mouse.GetState();
            /// Ctrlz
            if (Inputs.Keyr(Keys.LeftControl) && Inputs.Keyr(Keys.Z) && CtrlZ[1] != Vector.Zero)
                doMoveUnit(map[CtrlZ[0].X, CtrlZ[0].Y].unitOfCell, map[CtrlZ[1].X, CtrlZ[1].Y], ListOfUnit);

            /// Touche Reset
            if (depSelec && !destSelec && (Inputs.Keyr(Keys.Back) || (cur.MiddleButton == ButtonState.Pressed && cur != old))) { Reset(); }


            /// Touche Attack
            if (UnitTemp != null && ViseurOverUnit && Contains<Vector>(UnitTemp.MvtPossible, new Vector(coordViseur.X, coordViseur.Y)) && (Inputs.Keyr(Keys.Enter) || Inputs.Keyr(Keys.C)))
            {
                if (Inputs.Keyr(Keys.C))
                    map[viseurX, viseurY].unitOfCell.PV -= 10000;
                else
                    map[viseurX, viseurY].unitOfCell.PV -= map[depPos.X, depPos.Y].unitOfCell.Strength;


                if (map[viseurX, viseurY].unitOfCell.PV <= 0)
                {
                    map[viseurX, viseurY].unitOfCell = new Unit(data, map[viseurX, viseurY].unitOfCell, map, ListOfUnit);
                    doMoveUnit(map[depPos.X, depPos.Y].unitOfCell, map[viseurX, viseurY], ListOfUnit);
                    Explosion();
                }
                Reset();
                Match.TurnbyTurn.MvtCount++;
            }

            /// Build
            if (build && depSelec && !ViseurOverPos(depPos) && (Inputs.Keyr(Keys.B) || (cur.RightButton == ButtonState.Pressed && cur != old)))
            {
                destSelec = true;
                destPos = new Vector(coordViseur.X, coordViseur.Y);
                map[viseurX, viseurY].unitOfCell = new Unit(data, Match.PlayerTurn.ColorSideN + "Plane", "Queen", map, destPos.X, destPos.Y, Match.PlayerTurn, Match);
                Match.TurnbyTurn.MvtCount++;
                build = false;
                Reset();
            }
            else if (map[viseurX, viseurY].unitOfCell == Match.PlayerTurn.HQ && !build && !depSelec && (Inputs.Keyr(Keys.B) || (cur.RightButton == ButtonState.Pressed && cur != old)))
            {
                depSelec = true;
                build = true;
                depPos = new Vector(coordViseur.X, coordViseur.Y);
                UnitTemp = Match.PlayerTurn.HQ;
            }


            /// Deplacement
            if (!build && depSelec && !ViseurOverPos(depPos) && (Inputs.Keyr(Keys.Enter) || (cur.LeftButton == ButtonState.Pressed && cur != old)))
            {
                if (UnitTemp != null && !ViseurOverUnit && Contains<int>(UnitTemp.TerrainPossible, data.altitudeTerrain[viseurX, viseurY]))
                {
                    destSelec = true;
                    destPos = new Vector(coordViseur.X, coordViseur.Y);
                    doMoveUnit(map[depPos.X, depPos.Y].unitOfCell, map[destPos.X, destPos.Y], ListOfUnit);
                }
            }
            else if (!build && map[viseurX, viseurY].unitOfCell != null && Match.canStart && Match.PlayerTurn.PlayerName == map[viseurX, viseurY].unitOfCell.Player.PlayerName && ViseurOverUnit && !depSelec && (Inputs.Keyr(Keys.Enter) || (cur.LeftButton == ButtonState.Pressed && cur != old)))
            {
                depSelec = true;
                depPos = new Vector(coordViseur.X, coordViseur.Y);
                UnitTemp = map[viseurX, viseurY].unitOfCell;
            }
            old = cur;
        }

        private void ViseurColor()
        {
            if (build)
                spviseur = Viseurbleu;
            else if (UnitTemp != null && depSelec && (!Contains<int>(UnitTemp.TerrainPossible, data.altitudeTerrain[viseurX, viseurY]) || !Contains<Vector>(UnitTemp.MvtPossible, new Vector(coordViseur.X, coordViseur.Y))))
                spviseur = Viseurrouge;
            else if (!map[viseurX, viseurY].Occupe && !depSelec && !destSelec)
                spviseur = Viseurnormal;
            else if (depSelec || (map[viseurX, viseurY].Occupe && !depSelec))
                spviseur = Viseurbleu;
        }

        private void BlinkSprite(GameTime gameTime, bool blinkviseur, SpriteBatch spriteBatch)
        {
            if (depSelec && !destSelec && build)
            {
                foreach (Vector item in map[depPos.X, depPos.Y].unitOfCell.HQPossible)
                {
                    spCasebleu.Draw(data, spriteBatch, map[item.X, item.Y].positionPixel);
                }
            }
            else if (depSelec && !destSelec && !build)
            {
                foreach (Vector item in map[depPos.X, depPos.Y].unitOfCell.MvtPossible)
                {
                    if (map[depPos.X, depPos.Y].unitOfCell.TerrainPossible.Contains(data.altitudeTerrain[item.X, item.Y]))
                        spCasebleu.Draw(data, spriteBatch, map[item.X, item.Y].positionPixel);
                }
                if (map[viseurX, viseurY].Vector2OfCell == depPos)
                    sblinkviseur.Position = map[viseurX, viseurY].positionPixel;
                blinkviseur = depSelec;
            }

            sblinkviseur.Draw(data, spriteBatch, sblinkviseur.Position, blinkviseur);
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
            float tempo;

            getMovingPath(ListOfUnit, gameTime, spriteBatch);
            ViseurColor();

            #region Gestion des mouvements du viseurs

            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift) || Keyboard.GetState().IsKeyDown(Keys.RightShift)) tempo = 0.07f; else tempo = 0.12f;

            if (gameTime.TotalGameTime - time > TimeSpan.FromSeconds(tempo))
            {
                time = gameTime.TotalGameTime;

                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    if (Mouse.GetState().X >= data.PosXInit && Mouse.GetState().X <= data.PosXInit + data.MapWidth * data.TileSize * data.Scale && Mouse.GetState().Y >= 0 && Mouse.GetState().Y <= data.WindowHeight)
                    {
                        coord.X = (Mouse.GetState().X - data.PosXInit) / (data.TileSize * data.Scale);
                        coord.Y = Mouse.GetState().Y / (data.TileSize * data.Scale);
                    }
                }

                if (coordViseur.X == 0 && Keyboard.GetState().IsKeyDown(Keys.Left))
                    coord.X = data.MapWidth - 1;
                else
                    if (Keyboard.GetState().IsKeyDown(Keys.Left))
                        --coord.X;

                if (coordViseur.X == data.MapWidth - 1 && Keyboard.GetState().IsKeyDown(Keys.Right))
                    coord.X = 0;
                else
                    if (Keyboard.GetState().IsKeyDown(Keys.Right))
                        ++coord.X;

                if (coordViseur.Y == 0 && Keyboard.GetState().IsKeyDown(Keys.Up))
                    coord.Y = data.MapHeight - 1;
                else
                    if (Keyboard.GetState().IsKeyDown(Keys.Up))
                        --coord.Y;

                if (coordViseur.Y == data.MapHeight - 1 && Keyboard.GetState().IsKeyDown(Keys.Down))
                    coord.Y = 0;
                else
                    if (Keyboard.GetState().IsKeyDown(Keys.Down))
                        ++coord.Y;
            }
            #endregion
        }

        #endregion

        // // // // // // // // 

        #region DRAW

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            BlinkSprite(gameTime, blinkviseur, spriteBatch);
            spviseur.Draw(data, spriteBatch, map[viseurX, viseurY].positionPixel);
            if (UnitTemp != null)
            {
                //for (int i = 0; i < UnitTemp.MvtPossible.Count(); i++)
                //{
                //    spCasebleu.Draw(data, spriteBatch, gameTime, new Vector2(UnitTemp.MvtPossible[i].X, UnitTemp.MvtPossible[i].Y));
                //}
            }

        }

        #endregion
    }
}
