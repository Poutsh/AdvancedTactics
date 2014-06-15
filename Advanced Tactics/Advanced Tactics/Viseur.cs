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

        public enum ViseurState { Normal, Attack, AttackCheat, Build, Moving, Reset, CtrlZ }
        public ViseurState currentViseurState;

        public Sprite spviseur, Viseurjaune;
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
        MouseState curmouse, oldmouse;
        Match Match;
        public Sprite spriteViseur { get { return spviseur; } }
        Stats stats;
        #endregion

        // // // // // // // // 

        #region CONSTRUCTEURS


        public Viseur() { }

        public Viseur(Data data, Cell[,] map, Match match)
        {
            this.data = data;
            this.map = map;
            Match = match;
            stats = new Stats(data);
            viseur = new Unit();
            Init();
        }

        #endregion

        // // // // // // // // 

        #region INIT & LOAD

        void Init()
        {
            spviseur = new Sprite(); spviseur.LC(Game1.Ctt, "Curseur/viseur");
            Viseurjaune = new Sprite(); Viseurjaune.LC(Game1.Ctt, "Curseur/viseurJ");
            Viseurbleu = new Sprite(); Viseurbleu.LC(Game1.Ctt, "Curseur/viseurB");
            Viseurrouge = new Sprite(); Viseurrouge.LC(Game1.Ctt, "Curseur/viseurR");
            Viseurnormal = new Sprite(); Viseurnormal.LC(Game1.Ctt, "Curseur/viseur");
            spCaserouge = new Sprite(); spCaserouge.LC(Game1.Ctt, "Case/rouge");
            spCasebleu = new Sprite(); spCasebleu.LC(Game1.Ctt, "Case/bleu");
            currentViseurState = ViseurState.Normal;
            coord.X = data.MapWidth / 2;
            coord.Y = data.MapHeight / 2;
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
            currentViseurState = ViseurState.Normal;
        }

        private void doMoveUnit(Unit unit, Cell newCell, List<Unit> ListOfUnit)
        {
            CtrlZ = new List<Vector>(2);
            CtrlZ.Add(destPos); CtrlZ.Add(depPos);
            if (Contains<Vector>(unit.MvtPossible, new Vector(newCell.XofCell, newCell.YofCell)) || (Contains<Vector>(unit.AttackPossible, new Vector(newCell.XofCell, newCell.YofCell)) && !map[newCell.XofCell, newCell.YofCell].Occupe))
            {
                unit = new Unit(data, unit, map, newCell, Match);
                //if (unit.Classe == "King") Match.PlayerTurn.HQ.HQPossible = stats.HQPoss(Match.PlayerTurn.HQ, map, data);
            }
        }

        private void BlinkSprite(GameTime gameTime, bool blinkviseur, SpriteBatch spriteBatch)
        {
            switch (currentViseurState)
            {
                case ViseurState.Attack:
                    goto case ViseurState.Moving;

                case ViseurState.AttackCheat:
                    goto case ViseurState.Moving;


                case ViseurState.Build:
                    foreach (Vector item in map[depPos.X, depPos.Y].unitOfCell.HQPossible) spCasebleu.Draw(data, spriteBatch, map[item.X, item.Y].positionPixel);
                    break;


                case ViseurState.Moving:
                    foreach (Vector item in map[depPos.X, depPos.Y].unitOfCell.MvtPossible) spCasebleu.Draw(data, spriteBatch, map[item.X, item.Y].positionPixel);
                    foreach (Vector item in map[depPos.X, depPos.Y].unitOfCell.AttackPossible) spCaserouge.Draw(data, spriteBatch, map[item.X, item.Y].positionPixel);
                    break;
            }
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
            curmouse = Mouse.GetState();
            if (Match.canStart)
            {
                if (depSelec && !destSelec && (Inputs.Keyr(Keys.Back) || (curmouse.MiddleButton == ButtonState.Pressed && curmouse != oldmouse))) Reset();

                if (currentViseurState == ViseurState.Normal)
                {
                    if (!map[viseurX, viseurY].Occupe || map[viseurX, viseurY].unitOfCell.Player != Match.PlayerTurn) spviseur = Viseurnormal;
                    else if (map[viseurX, viseurY].Occupe && map[viseurX, viseurY].unitOfCell.Player == Match.PlayerTurn) spviseur = Viseurbleu;

                    if (Inputs.Keyr(Keys.LeftControl) && Inputs.Keyr(Keys.Z) && CtrlZ[1] != Vector.Zero) currentViseurState = ViseurState.CtrlZ;

                    if (map[viseurX, viseurY].unitOfCell == Match.PlayerTurn.HQ && !depSelec && (Inputs.Keyr(Keys.B) || (curmouse.RightButton == ButtonState.Pressed && curmouse != oldmouse)))
                    {
                        depSelec = true;
                        depPos = new Vector(coordViseur.X, coordViseur.Y);
                        UnitTemp = Match.PlayerTurn.HQ;
                        currentViseurState = ViseurState.Build;
                    }
                    else if (map[viseurX, viseurY].unitOfCell != null && Match.canStart && Match.PlayerTurn.PlayerName == map[viseurX, viseurY].unitOfCell.Player.PlayerName && ViseurOverUnit && !depSelec && (Inputs.Keyr(Keys.Enter) || (curmouse.LeftButton == ButtonState.Pressed && curmouse != oldmouse)))
                    {
                        depSelec = true;
                        depPos = new Vector(coordViseur.X, coordViseur.Y);
                        UnitTemp = map[viseurX, viseurY].unitOfCell;
                        currentViseurState = ViseurState.Moving;
                    }
                }
                else if (currentViseurState == ViseurState.AttackCheat)
                {
                    map[viseurX, viseurY].unitOfCell.PV -= 10000;

                    if (map[viseurX, viseurY].unitOfCell.PV <= 0)
                    {
                        Match.PlayerTurn.Score = map[viseurX, viseurY].unitOfCell.Point;
                        map[viseurX, viseurY].unitOfCell.DelUnitofList();
                        destPos = new Vector(coordViseur.X, coordViseur.Y);
                        if (UnitTemp.Classe != "King") doMoveUnit(map[depPos.X, depPos.Y].unitOfCell, map[destPos.X, destPos.Y], ListOfUnit);
                        Explosion();
                    }
                    Match.TurnbyTurn.MvtCount++;
                    Reset();
                }
                else if (currentViseurState == ViseurState.Attack)
                {
                    if (UnitTemp.Classe != "King") map[viseurX, viseurY].unitOfCell.PV -= map[depPos.X, depPos.Y].unitOfCell.Strength;
                    else map[viseurX, viseurY].unitOfCell.PV -= stats.PVUnit(map[viseurX, viseurY].unitOfCell.Rang) / 2;

                    if (map[viseurX, viseurY].unitOfCell.PV <= 0)
                    {
                        Match.PlayerTurn.Score = map[viseurX, viseurY].unitOfCell.Point;
                        map[viseurX, viseurY].unitOfCell.DelUnitofList();
                        destPos = new Vector(coordViseur.X, coordViseur.Y);
                        if (UnitTemp.Classe != "King") doMoveUnit(map[depPos.X, depPos.Y].unitOfCell, map[destPos.X, destPos.Y], ListOfUnit);
                        Explosion();
                    }
                    Match.TurnbyTurn.MvtCount++;
                    Reset();
                }
                else if (currentViseurState == ViseurState.Build)
                {
                    if (map[depPos.X, depPos.Y] == map[viseurX, viseurY]) spviseur = Viseurjaune;
                    else if (Contains<Vector>(map[depPos.X, depPos.Y].unitOfCell.HQPossible, map[viseurX, viseurY].VectorOfCell)) spviseur = Viseurbleu;
                    else spviseur = Viseurrouge;

                    if (depSelec && map[viseurX, viseurY].unitOfCell == null && (Inputs.Keyr(Keys.B) || (curmouse.RightButton == ButtonState.Pressed && curmouse != oldmouse)))
                    {
                        destSelec = true;
                        destPos = new Vector(coordViseur.X, coordViseur.Y);
                        map[viseurX, viseurY].unitOfCell = new Unit(data, "Plane", "Queen", map, destPos.X, destPos.Y, Match.PlayerTurn, Match);
                        Match.TurnbyTurn.MvtCount++;
                        Match.PlayerTurn.HQ.HQPossible = stats.HQPoss(Match.PlayerTurn.HQ, map, data);
                        Reset();
                    }
                }
                else if (currentViseurState == ViseurState.Moving)
                {
                    if (map[depPos.X, depPos.Y] == map[viseurX, viseurY]) spviseur = Viseurjaune;
                    else if (Contains<Vector>(map[depPos.X, depPos.Y].unitOfCell.AttackPossible, map[viseurX, viseurY].VectorOfCell)) spviseur = Viseurjaune;
                    else if (Contains<Vector>(map[depPos.X, depPos.Y].unitOfCell.MvtPossible, map[viseurX, viseurY].VectorOfCell)) spviseur = Viseurbleu;
                    else spviseur = Viseurrouge;

                    if (UnitTemp != null && ViseurOverUnit && Contains<Vector>(UnitTemp.AttackPossible, new Vector(coordViseur.X, coordViseur.Y)))
                    {
                        if (Inputs.Keyr(Keys.C)) currentViseurState = ViseurState.AttackCheat;
                        else if (Inputs.Keyr(Keys.Enter)) currentViseurState = ViseurState.Attack;
                    }
                    else if (depSelec && UnitTemp != null && !ViseurOverUnit && Contains<Vector>(UnitTemp.MvtPossible, new Vector(coordViseur.X, coordViseur.Y)) && (Inputs.Keyr(Keys.Enter) || (curmouse.LeftButton == ButtonState.Pressed && curmouse != oldmouse)))
                    {
                        destSelec = true;
                        destPos = new Vector(coordViseur.X, coordViseur.Y);
                        if (UnitTemp.Classe != "King") doMoveUnit(map[depPos.X, depPos.Y].unitOfCell, map[destPos.X, destPos.Y], ListOfUnit);
                        if (UnitTemp.Classe != "King") Match.TurnbyTurn.MvtCount++;
                        Reset();
                    }
                }
                else if (currentViseurState == ViseurState.Reset)
                {
                    Reset();
                    currentViseurState = ViseurState.Normal;
                }
                else if (currentViseurState == ViseurState.CtrlZ)
                {
                    doMoveUnit(map[CtrlZ[0].X, CtrlZ[0].Y].unitOfCell, map[CtrlZ[1].X, CtrlZ[1].Y], ListOfUnit);
                    currentViseurState = ViseurState.Reset;
                }



                //switch (currentViseurState)
                //{
                //    case ViseurState.Normal:
                //        if (!map[viseurX, viseurY].Occupe || map[viseurX, viseurY].unitOfCell.Player != Match.PlayerTurn) spviseur = Viseurnormal;
                //        else if (map[viseurX, viseurY].Occupe && map[viseurX, viseurY].unitOfCell.Player == Match.PlayerTurn) spviseur = Viseurbleu;

                //        if (Inputs.Keyr(Keys.LeftControl) && Inputs.Keyr(Keys.Z) && CtrlZ[1] != Vector.Zero) currentViseurState = ViseurState.CtrlZ;
                //        if (map[viseurX, viseurY].unitOfCell == Match.PlayerTurn.HQ && !depSelec && (Inputs.Keyr(Keys.V) || (curmouse.RightButton == ButtonState.Pressed && curmouse != oldmouse)))
                //        {
                //            depSelec = true;
                //            depPos = new Vector(coordViseur.X, coordViseur.Y);
                //            UnitTemp = Match.PlayerTurn.HQ;
                //            currentViseurState = ViseurState.Build;
                //        }
                //        else if (map[viseurX, viseurY].unitOfCell != null && Match.canStart && Match.PlayerTurn.PlayerName == map[viseurX, viseurY].unitOfCell.Player.PlayerName && ViseurOverUnit && !depSelec && (Inputs.Keyr(Keys.Enter) || (curmouse.LeftButton == ButtonState.Pressed && curmouse != oldmouse)))
                //        {
                //            depSelec = true;
                //            depPos = new Vector(coordViseur.X, coordViseur.Y);
                //            UnitTemp = map[viseurX, viseurY].unitOfCell;
                //            currentViseurState = ViseurState.Moving;
                //        }
                //        break;

                //    case ViseurState.AttackCheat:
                //        map[viseurX, viseurY].unitOfCell.PV -= 10000;

                //        if (map[viseurX, viseurY].unitOfCell.PV <= 0)
                //        {
                //            Match.PlayerTurn.Score = map[viseurX, viseurY].unitOfCell.Point;
                //            map[viseurX, viseurY].unitOfCell.DelUnitofList();
                //            destPos = new Vector(coordViseur.X, coordViseur.Y);
                //            doMoveUnit(map[depPos.X, depPos.Y].unitOfCell, map[destPos.X, destPos.Y], ListOfUnit);
                //            Explosion();
                //        }
                //        Match.TurnbyTurn.MvtCount++;
                //        currentViseurState = ViseurState.Reset;
                //        break;

                //    case ViseurState.Attack:
                //        map[viseurX, viseurY].unitOfCell.PV -= map[depPos.X, depPos.Y].unitOfCell.Strength;

                //        if (map[viseurX, viseurY].unitOfCell.PV <= 0)
                //        {
                //            Match.PlayerTurn.Score = map[viseurX, viseurY].unitOfCell.Point;
                //            map[viseurX, viseurY].unitOfCell.DelUnitofList();
                //            destPos = new Vector(coordViseur.X, coordViseur.Y);
                //            doMoveUnit(map[depPos.X, depPos.Y].unitOfCell, map[destPos.X, destPos.Y], ListOfUnit);
                //            Explosion();
                //        }
                //        Match.TurnbyTurn.MvtCount++;
                //        currentViseurState = ViseurState.Reset;
                //        break;


                //    case ViseurState.Build:
                //        if (map[depPos.X, depPos.Y] == map[viseurX, viseurY]) spviseur = Viseurjaune;
                //        else if (Contains<Vector>(map[depPos.X, depPos.Y].unitOfCell.HQPossible, map[viseurX, viseurY].VectorOfCell)) spviseur = Viseurbleu;
                //        else spviseur = Viseurrouge;

                //        if (depSelec && map[viseurX, viseurY].unitOfCell == null && (Inputs.Keyr(Keys.B) || (curmouse.RightButton == ButtonState.Pressed && curmouse != oldmouse)))
                //        {
                //            destSelec = true;
                //            destPos = new Vector(coordViseur.X, coordViseur.Y);
                //            map[viseurX, viseurY].unitOfCell = new Unit(data, "Plane", "Queen", map, destPos.X, destPos.Y, Match.PlayerTurn, Match);
                //            Match.TurnbyTurn.MvtCount++;
                //            Reset();
                //        }
                //        break;


                //    case ViseurState.Moving:
                //        if (map[depPos.X, depPos.Y] == map[viseurX, viseurY]) spviseur = Viseurjaune;
                //        else if (Contains<Vector>(map[depPos.X, depPos.Y].unitOfCell.AttackPossible, map[viseurX, viseurY].VectorOfCell)) spviseur = Viseurjaune;
                //        else if (Contains<Vector>(map[depPos.X, depPos.Y].unitOfCell.MvtPossible, map[viseurX, viseurY].VectorOfCell)) spviseur = Viseurbleu;
                //        else spviseur = Viseurrouge;

                //        if (UnitTemp != null && ViseurOverUnit && Contains<Vector>(UnitTemp.AttackPossible, new Vector(coordViseur.X, coordViseur.Y)))
                //        {
                //            if (Inputs.Keyr(Keys.C)) currentViseurState = ViseurState.AttackCheat;
                //            else if (Inputs.Keyr(Keys.Enter)) currentViseurState = ViseurState.Attack;
                //            Reset();
                //        }
                //        else if (depSelec && UnitTemp != null && !ViseurOverUnit && Contains<Vector>(UnitTemp.MvtPossible, new Vector(coordViseur.X, coordViseur.Y)) && (Inputs.Keyr(Keys.Enter) || (curmouse.LeftButton == ButtonState.Pressed && curmouse != oldmouse)))
                //        {
                //            destSelec = true;
                //            destPos = new Vector(coordViseur.X, coordViseur.Y);
                //            doMoveUnit(map[depPos.X, depPos.Y].unitOfCell, map[destPos.X, destPos.Y], ListOfUnit);
                //            if (map[depPos.X, depPos.Y].unitOfCell == Match.PlayerTurn.HQ)
                //            {
                //                Match.PlayerTurn.HQ = map[destPos.X, destPos.Y].unitOfCell;
                //            }


                //            Match.TurnbyTurn.MvtCount++;
                //            Reset();
                //        }
                //        break;


                //    case ViseurState.Reset:
                //        Reset();
                //        currentViseurState = ViseurState.Normal;
                //        break;


                //    case ViseurState.CtrlZ:
                //        doMoveUnit(map[CtrlZ[0].X, CtrlZ[0].Y].unitOfCell, map[CtrlZ[1].X, CtrlZ[1].Y], ListOfUnit);
                //        currentViseurState = ViseurState.Reset;
                //        break;
                //}
            }

            //getMovingPath(ListOfUnit, gameTime, spriteBatch);
            //ViseurColor();

            #region Gestion des mouvements du viseurs
            float tempo;
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
            oldmouse = curmouse;
        }

        #endregion

        // // // // // // // // 

        #region DRAW

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            BlinkSprite(gameTime, blinkviseur, spriteBatch);
            spviseur.Draw(data, spriteBatch, map[viseurX, viseurY].positionPixel);
        }

        #endregion
    }
}
