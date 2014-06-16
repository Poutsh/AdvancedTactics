using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Advanced_Tactics
{
    public class Viseur
    {
        #region VARIABLES

        Data data;
        TimeSpan time, time2, time3, time4;

        public enum ViseurState { Normal, Attack, AttackCheat, Build, Moving, Reset, CtrlZ }
        public ViseurState currentViseurState;

        public Sprite spviseur, Viseurjaune;
        public Sprite spCaserouge, spCasebleu, Viseurbleu, Viseurrouge, Viseurnormal, spCase3bleu, casevert, Viseurvert;
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
        public bool Build;
        List<Vector> CtrlZ;
        MouseState curmouse, oldmouse;
        Match Match;
        public Sprite spriteViseur { get { return spviseur; } }
        Stats stats;
        Message2 message, message2;

        public Rectangle AA, Commando, Doc, Engineer, Plane, Pvt, Tank, Truck, Queen, Rook, Bishop, Knight, Pawn;
        Rectangle[] test;
        String[] test2;
        bool build = true, move = true, attack = true;
        public int cost { get { return stats.PointUnit(test2[0], test2[1]); } }
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
            Viseurvert = new Sprite(); Viseurvert.LC(Game1.Ctt, "Curseur/viseurV");
            Viseurbleu = new Sprite(); Viseurbleu.LC(Game1.Ctt, "Curseur/viseurB");
            Viseurrouge = new Sprite(); Viseurrouge.LC(Game1.Ctt, "Curseur/viseurR");
            Viseurnormal = new Sprite(); Viseurnormal.LC(Game1.Ctt, "Curseur/viseur");
            spCaserouge = new Sprite(); spCaserouge.LC(Game1.Ctt, "Case/rouge");
            spCasebleu = new Sprite(); spCasebleu.LC(Game1.Ctt, "Case/bleu");
            casevert = new Sprite(); casevert.LC(Game1.Ctt, "Case/vert");
            spCase3bleu = new Sprite(); spCase3bleu.LC(Game1.Ctt, "Case/bleu");
            currentViseurState = ViseurState.Normal;
            coord.X = data.MapWidth / 2;
            coord.Y = data.MapHeight / 2;
            message = new Message2();
            message2 = new Message2();
            test = new Rectangle[2];
            test2 = new String[2];
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
            test = new Rectangle[2];
            test2 = new String[2];
            currentViseurState = ViseurState.Normal;
        }

        private void doMoveUnit(Unit unit, Cell newCell, List<Unit> ListOfUnit)
        {
            CtrlZ = new List<Vector>(2);
            CtrlZ.Add(destPos); CtrlZ.Add(depPos);
            if (Contains<Vector>(unit.MvtPossible, new Vector(newCell.XofCell, newCell.YofCell)) || (Contains<Vector>(unit.AttackPossible, new Vector(newCell.XofCell, newCell.YofCell)) && !map[newCell.XofCell, newCell.YofCell].Occupe))
                unit = new Unit(data, unit, map, newCell, Match);
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
                    if (map[depPos.X, depPos.Y].unitOfCell.Rang == "Doc") foreach (Vector item in map[depPos.X, depPos.Y].unitOfCell.HealPossible) casevert.Draw(data, spriteBatch, map[item.X, item.Y].positionPixel);
                    break;
            }
        }

        public void Explosion()
        {
            SoundPlayer player = new SoundPlayer(Resource.explosound);
            player.Play();
        }

        #endregion

        // // // // // // // // 

        #region UPDATE

        public void Update(GameTime gameTime, List<Unit> ListOfUnit, SpriteBatch spriteBatch)
        {
            curmouse = Mouse.GetState();
            if (Match.canStart && !Game1.test)
            {
                if (depSelec && !destSelec && (Inputs.Keyr(Keys.Back) || (curmouse.MiddleButton == ButtonState.Pressed && curmouse != oldmouse))) Reset();


                /////////////////////////
                /// ViseurState.Normal
                ////////////////////////
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
                        Match.PlayerTurn.HQ.HQPossible = stats.HQPoss(Match.PlayerTurn.HQ, map, data);
                        currentViseurState = ViseurState.Build;
                    }
                    else if (map[viseurX, viseurY].unitOfCell != null && Match.canStart && Match.PlayerTurn.PlayerName == map[viseurX, viseurY].unitOfCell.Player.PlayerName && ViseurOverUnit && !depSelec && (Inputs.Keyr(Keys.Enter) || (Inputs.Clickd() && Inputs.Clickg())))
                    {
                        depSelec = true;
                        depPos = new Vector(coordViseur.X, coordViseur.Y);
                        UnitTemp = map[viseurX, viseurY].unitOfCell;
                        currentViseurState = ViseurState.Moving;
                    }
                }
                /////////////////////////
                /// ViseurState.AttackCheat
                ////////////////////////
                else if (currentViseurState == ViseurState.AttackCheat)
                {
                    if (UnitTemp.Rang == "Doc" && map[viseurX, viseurY].unitOfCell.Player == Match.PlayerTurn) map[viseurX, viseurY].unitOfCell.PV += stats.PVUnit(map[viseurX, viseurY].unitOfCell.Rang);
                    else if (UnitTemp.Rang == "Doc" && map[viseurX, viseurY].unitOfCell.Player == Match.PlayerTurn) map[viseurX, viseurY].unitOfCell.PV += map[depPos.X, depPos.Y].unitOfCell.Strength;
                    else map[viseurX, viseurY].unitOfCell.PV -= 10000;

                    destPos = new Vector(coordViseur.X, coordViseur.Y);
                    if (map[viseurX, viseurY].unitOfCell.PV <= 0)
                    {
                        Match.PlayerTurn.Money += map[viseurX, viseurY].unitOfCell.Point;
                        Match.PlayerTurn.Score += map[viseurX, viseurY].unitOfCell.Point;
                        map[viseurX, viseurY].unitOfCell.DelUnitofList();
                        if (UnitTemp.Classe != "King") doMoveUnit(map[depPos.X, depPos.Y].unitOfCell, map[destPos.X, destPos.Y], ListOfUnit);
                        Explosion();
                    }
                    Match.TurnbyTurn.MvtCount++;
                    Reset();
                }
                /////////////////////////
                /// ViseurState.Attack
                ////////////////////////
                else if (currentViseurState == ViseurState.Attack)
                {
                    if (UnitTemp.Classe != "King") map[viseurX, viseurY].unitOfCell.PV -= map[depPos.X, depPos.Y].unitOfCell.Strength;
                    else if (UnitTemp.Rang == "Doc" && map[viseurX, viseurY].unitOfCell.Player == Match.PlayerTurn) map[viseurX, viseurY].unitOfCell.PV += map[depPos.X, depPos.Y].unitOfCell.Strength;
                    else map[viseurX, viseurY].unitOfCell.PV -= stats.PVUnit(map[viseurX, viseurY].unitOfCell.Rang) / 2;

                    destPos = new Vector(coordViseur.X, coordViseur.Y);
                    if (map[viseurX, viseurY].unitOfCell.PV <= 0)
                    {
                        Match.PlayerTurn.Money += map[viseurX, viseurY].unitOfCell.Point;
                        Match.PlayerTurn.Score += map[viseurX, viseurY].unitOfCell.Point;
                        map[viseurX, viseurY].unitOfCell.DelUnitofList();
                        if (UnitTemp.Classe != "King") doMoveUnit(map[depPos.X, depPos.Y].unitOfCell, map[destPos.X, destPos.Y], ListOfUnit);
                        Explosion();
                    }
                    Match.TurnbyTurn.MvtCount++;
                    Reset();
                }
                /////////////////////////
                /// ViseurState.Build
                ////////////////////////
                else if (currentViseurState == ViseurState.Build)
                {
                    float temp = 1f;

                    if (map[depPos.X, depPos.Y] == map[viseurX, viseurY]) spviseur = Viseurjaune;
                    else if (Contains<Vector>(map[depPos.X, depPos.Y].unitOfCell.HQPossible, map[viseurX, viseurY].VectorOfCell)) spviseur = Viseurbleu;
                    else spviseur = Viseurrouge;

                    string[] arrayrang = new string[] { "AA", "Commando", "Doc", "Engineer", "Plane", "Pvt", "Tank", "Truck" };
                    string[] arrayclasse = new string[] { "Queen", "Rook", "Bishop", "Knight", "Pawn" };

                    if (Inputs.Keyr(Keys.C)) Match.PlayerTurn.Money += 10000;

                    if (currentViseurState == ViseurState.Build)
                    {
                        if (curmouse.LeftButton == ButtonState.Pressed && curmouse != oldmouse)
                        {
                            if (AA.Contains(Mouse.GetState().X, Mouse.GetState().Y)) { test[0] = AA; test2[0] = "AA"; }
                            else if (Commando.Contains(Mouse.GetState().X, Mouse.GetState().Y)) { test[0] = Commando; test2[0] = "Commando"; }
                            else if (Doc.Contains(Mouse.GetState().X, Mouse.GetState().Y)) { test[0] = Doc; test2[0] = "Doc"; }
                            else if (Engineer.Contains(Mouse.GetState().X, Mouse.GetState().Y)) { test[0] = Engineer; test2[0] = "Engineer"; }
                            else if (Plane.Contains(Mouse.GetState().X, Mouse.GetState().Y)) { test[0] = Plane; test2[0] = "Plane"; }
                            else if (Pvt.Contains(Mouse.GetState().X, Mouse.GetState().Y)) { test[0] = Pvt; test2[0] = "Pvt"; }
                            else if (Tank.Contains(Mouse.GetState().X, Mouse.GetState().Y)) { test[0] = Tank; test2[0] = "Tank"; }
                            else if (Truck.Contains(Mouse.GetState().X, Mouse.GetState().Y)) { test[0] = Truck; test2[0] = "Truck"; }
                        }

                        if (curmouse.LeftButton == ButtonState.Pressed && curmouse != oldmouse)
                        {
                            if (Queen.Contains(Mouse.GetState().X, Mouse.GetState().Y)) { test[1] = Queen; test2[1] = "Queen"; }
                            else if (Rook.Contains(Mouse.GetState().X, Mouse.GetState().Y)) { test[1] = Rook; test2[1] = "Rook"; }
                            else if (Bishop.Contains(Mouse.GetState().X, Mouse.GetState().Y)) { test[1] = Bishop; test2[1] = "Bishop"; }
                            else if (Knight.Contains(Mouse.GetState().X, Mouse.GetState().Y)) { test[1] = Knight; test2[1] = "Knight"; }
                            else if (Pawn.Contains(Mouse.GetState().X, Mouse.GetState().Y)) { test[1] = Pawn; test2[1] = "Pawn"; }
                        }
                    }

                    if (arrayrang.Contains(test2[0]) && arrayclasse.Contains(test2[1]) && depSelec && map[viseurX, viseurY].unitOfCell == null && (Inputs.Keyr(Keys.B) || (curmouse.RightButton == ButtonState.Pressed && curmouse != oldmouse)))
                    {
                        destSelec = true;
                        destPos = new Vector(coordViseur.X, coordViseur.Y);

                        if (Match.PlayerTurn.Money - stats.PointUnit(test2[0], test2[1]) >= 0)
                        {
                            map[viseurX, viseurY].unitOfCell = new Unit(data, test2[0], test2[1], map, destPos.X, destPos.Y, Match.PlayerTurn, Match);
                            Match.PlayerTurn.Money -= stats.PointUnit(test2[0], test2[1]);
                            Match.TurnbyTurn.MvtCount++;
                        }
                        else
                        {
                            message2.Messages.Add(new DisplayMessage("Not enough money", TimeSpan.FromSeconds(0.9), new Vector2(map[data.MapWidth / 2, data.MapHeight / 2].positionPixel.X - message.font.MeasureString("Not enough money").X / 2, map[data.MapWidth / 2, data.MapHeight / 2].positionPixel.Y), Color.Gold));
                        }
                        Reset();
                    }
                }
                /////////////////////////
                /// ViseurState.Moving
                ////////////////////////
                else if (currentViseurState == ViseurState.Moving)
                {
                    if (map[depPos.X, depPos.Y] == map[viseurX, viseurY]) spviseur = Viseurjaune;
                    else if (Contains<Vector>(map[depPos.X, depPos.Y].unitOfCell.HealPossible, map[viseurX, viseurY].VectorOfCell) && map[depPos.X, depPos.Y].unitOfCell.Rang == "Doc") spviseur = Viseurvert;
                    else if (Contains<Vector>(map[depPos.X, depPos.Y].unitOfCell.AttackPossible, map[viseurX, viseurY].VectorOfCell)) spviseur = Viseurjaune;
                    else if (Contains<Vector>(map[depPos.X, depPos.Y].unitOfCell.MvtPossible, map[viseurX, viseurY].VectorOfCell)) spviseur = Viseurbleu;
                    else spviseur = Viseurrouge;

                    if (UnitTemp != null && ViseurOverUnit && (Contains<Vector>(UnitTemp.AttackPossible, new Vector(coordViseur.X, coordViseur.Y)) || (Contains<Vector>(map[depPos.X, depPos.Y].unitOfCell.HealPossible, map[viseurX, viseurY].VectorOfCell) && map[depPos.X, depPos.Y].unitOfCell.Rang == "Doc")))
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
                /////////////////////////
                /// ViseurState.Reset
                ////////////////////////
                else if (currentViseurState == ViseurState.Reset)
                {
                    Reset();
                    currentViseurState = ViseurState.Normal;
                }
                /////////////////////////
                /// ViseurState.CtrlZ
                ////////////////////////
                else if (currentViseurState == ViseurState.CtrlZ)
                {
                    doMoveUnit(map[CtrlZ[0].X, CtrlZ[0].Y].unitOfCell, map[CtrlZ[1].X, CtrlZ[1].Y], ListOfUnit);
                    currentViseurState = ViseurState.Reset;
                }
            }
            else if (Match.canStart && Game1.test)
            {
                float temp = 0.0f;
                float temp2 = 15f;
                Match.Players[0].HQ.HQPossible = stats.HQPoss(Match.Players[0].HQ, map, data);
                Match.Players[1].HQ.HQPossible = stats.HQPoss(Match.Players[1].HQ, map, data);
                List<bool> action = new List<bool>(8) { build, move, move, attack, move, attack, attack, build };

                Random rr = new Random();

                if (Match.TurnbyTurn.MvtCount < Match.NumberMvtPerTurn && gameTime.TotalGameTime - time4 > TimeSpan.FromSeconds(temp))
                    Match.TurnbyTurn.MvtCount++;

                if (Match.TurnbyTurn.MvtCount < Match.NumberMvtPerTurn && gameTime.TotalGameTime - time3 > TimeSpan.FromSeconds(temp))
                {
                    time3 = gameTime.TotalGameTime;

                    Match.IA.Build(Match);
                    Match.IA.Move(Match);
                    Match.IA.Attack(Match, this);
                    //if (action[rr.Next(0, 8)] == build) Match.IA.Build(Match);
                    //if (action[rr.Next(0, 8)] == move) Match.IA.Move(Match);
                    //if (action[rr.Next(0, 8)] == attack) Match.IA.Attack(Match, this);
                }
                //if (Match.PlayerTurn.PlayerName == "IA")
                //{
                //    float temp = 0.5f;
                //    if (Match.TurnbyTurn.MvtCount < Match.NumberMvtPerTurn && gameTime.TotalGameTime - time3 > TimeSpan.FromSeconds(temp))
                //    {
                //        time3 = gameTime.TotalGameTime;
                //        //Match.TurnbyTurn.MvtCount -= 10;
                //        Match.PlayerTurn.HQ.HQPossible = stats.HQPoss(Match.PlayerTurn.HQ, map, data);
                //        if (Match.Players[1].Money >= 0) Match.IA.Build(Match);
                //        if (Match.Players[1].UnitOfPlayer.Count > 0) Match.IA.Move(Match);
                //        if (Match.Players[1].UnitOfPlayer.Count > 0) Match.IA.Attack(Match, this);
                //    }
                //}
                //else
                //{
                //    float temp = 0.5f;
                //    if (Match.TurnbyTurn.MvtCount < Match.NumberMvtPerTurn && gameTime.TotalGameTime - time3 > TimeSpan.FromSeconds(temp))
                //    {
                //        time3 = gameTime.TotalGameTime;
                //        //if (Match.Players[0].Money <= 0) Match.TurnbyTurn.MvtCount += 10;
                //        Match.PlayerTurn.HQ.HQPossible = stats.HQPoss(Match.PlayerTurn.HQ, map, data);
                //        if (Match.Players[0].Money >= 0) Match.IA.Build(Match);
                //        if (Match.Players[0].UnitOfPlayer.Count > 0) Match.IA.Move(Match);
                //        if (Match.Players[0].UnitOfPlayer.Count > 0) Match.IA.Attack(Match, this);
                //    }
                //}
            }

            //getMovingPath(ListOfUnit, gameTime, spriteBatch);
            //ViseurColor();

            #region Gestion des mouvements du viseurs
            float tempo;
            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift) || Keyboard.GetState().IsKeyDown(Keys.RightShift)) tempo = 0.15f; else tempo = 0.07f;

            if (gameTime.TotalGameTime - time > TimeSpan.FromSeconds(tempo) && Match.PlayerTurn.PlayerName != "IA")
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
            message.Draw(spriteBatch);

            foreach (Rectangle item in test) Viseurjaune.Draw(spriteBatch, item);
        }

        #endregion
    }
}
