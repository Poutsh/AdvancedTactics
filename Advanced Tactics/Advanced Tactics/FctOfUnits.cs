using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Advanced_Tactics
{
    /*public class FctOfUnits
    {
        Cell[,] map;
        Viseur viseur;
        bool depSelec;
        bool destSelec;
        Vector2 depPos;
        Vector2 destPos;
        Sprite spviseur;
        Sprite Viseurrouge;
        Sprite Viseurnormal;
        Sprite Viseurbleu;
        bool blinkviseur;
        Sprite spblinkviseur;
        KeyboardState currentKeyboardState;

        public FctOfUnits(Cell[,] map, Viseur viseur, bool depSelec, bool destSelec, Vector2 depPos, Vector2 destPos, Sprite spviseur, Sprite Viseurrouge, Sprite Viseurnormal, Sprite Viseurbleu, bool blinkviseur, Sprite spblinkviseur)
        {
            this.map = map;
            this.viseur = viseur;
            this.depSelec = depSelec;
            this.destSelec = destSelec;
            this.depPos = depPos;
            this.destPos = destPos;
            this.spviseur = spviseur;
            this.Viseurbleu = Viseurbleu;
            this.Viseurnormal = Viseurnormal;
            this.Viseurrouge = Viseurrouge;
            this.blinkviseur = blinkviseur;
            this.spblinkviseur = spblinkviseur;
        }

        void Reset()
        {
            depSelec = false; depPos = Vector2.Zero;
            destSelec = false; depPos = Vector2.Zero;
        }

        void doMoveUnit(Unit unit, Cell newUnit, List<Unit> ListOfUnit)
        {
            unit = new Unit(unit.Rang, unit.Classe, map, newUnit.XofCell, newUnit.YofCell, ListOfUnit, unit, true);
            Reset();
        }

        public void MvtOfUnit(List<Unit> ListOfUnit)
        {
            if (map[viseur.viseurX, viseur.viseurY].unitOfCell == null && destSelec && viseur.coordViseur != depPos && currentKeyboardState.IsKeyDown(Keys.W))
            {
                destSelec = true;
                destPos = new Vector2(viseur.coordViseur.X, viseur.coordViseur.Y);
                doMoveUnit(map[(int)depPos.X, (int)depPos.Y].unitOfCell, map[(int)destPos.X, (int)destPos.Y], ListOfUnit);
            }
            else if (viseur.ViseurOverUnit && depSelec == false && currentKeyboardState.IsKeyDown(Keys.Q))
            {
                depSelec = true;
                depPos = new Vector2(viseur.coordViseur.X, viseur.coordViseur.Y);
            }

        }

        public void Blink(bool occupe, GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (occupe)
            {
                if (depSelec && !destSelec)
                    spviseur = Viseurrouge;
                else
                    spviseur = Viseurbleu;
            }
            else if (!destSelec)
            {
                if (!depSelec)
                    spviseur = Viseurnormal;
                else
                {
                    if (map[(int)depPos.X, (int)depPos.Y].VectorOfCell == depPos)
                        spblinkviseur.Position = map[(int)depPos.X, (int)depPos.Y].positionPixel;
                    blinkviseur = depSelec;
                }

                spblinkviseur.Draw(spriteBatch, gameTime, spblinkviseur.Position, blinkviseur);
            }
        }
    }


    /*public class FctOfUnits : Viseur
    {

        #region VARIABLES

        //GameTime gameTime;
        KeyboardState currentKeyboardState;

        Cell[,] m;
        Viseur v;

        bool depSelec;
        Vector2 depPos;
        bool destSelec;
        Vector2 destPos;

        Sprite sp;
        Sprite spn, spb, spr, spj;
        bool blink;

        #endregion

        #region CONSTRUCTEUR

        public FctOfUnits(Cell[,] map, Viseur viseur, Sprite spviseur, Sprite spnormal, Sprite spbleu, Sprite sprouge, Sprite spjaune)
        {
            m = map;
            v = viseur;
            sp = spviseur;
            spn = spnormal;
            spb = spbleu;
            spr = sprouge;
            spj = spjaune;
            GetColorOfViseur(spviseur, map[viseur.viseurX, viseur.viseurY].Occupe);
        }

        #endregion

        #region FONCTIONS
        
        bool InMoving()
        {
            return !depSelec && !destSelec; ;
        }

        public void MvtOfUnit(List<Unit> ListOfUnit, bool ViseurOverUnit)
        {
            if (m[v.viseurX, v.viseurY].unitOfCell == null && depSelec && v.coordViseur != depPos && Keyboard.GetState().IsKeyDown(Keys.W))
            {
                depSelec = true;
                destPos = new Vector2(v.viseurX, v.viseurY);
                doMvt(m[(int)depPos.X, (int)depPos.Y].unitOfCell, m[(int)destPos.X, (int)destPos.Y], ListOfUnit);
            }
            else if (ViseurOverUnit && depSelec == false && Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                depSelec = true;
                depPos = new Vector2(v.viseurX, v.viseurY);
            }
        }

        void Reset()
        {
            depSelec = false; depPos = Vector2.Zero;
            destSelec = false; depPos = Vector2.Zero;
        }
        void doMvt(Unit unit, Cell newCell, List<Unit> ListOfUnit)
        {
            unit = new Unit(unit.Rang, unit.Classe, m, newCell.XofCell, newCell.YofCell, ListOfUnit, unit, true);
            Reset();
        }

        void GetColorOfViseur(Sprite sprite, bool occupe)
        {
            if (occupe)
            {
                if (depSelec && !destSelec) 
                    sprite = spr;
                else
                    sprite = spb;
            }
            else if (!destSelec)
            {
                if (!depSelec)
                    sprite = spn;
                else
                {
                    if (m[v.viseurX, v.viseurY].VectorOfCell == depPos)
                        spj.Position = m[(int)depPos.X, (int)depPos.Y].positionPixel;
                    blink = depSelec;
                }

            }
        }
        #endregion
    }*/
}
