using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdvancedLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Advanced_Tactics
{
    public class Unit
    {
        #region VARIABLES

        private Variable var = Game1.var;
        //private TimeSpan time;

        //private SpriteBatch spriteBatch;
        //private GameTime gameTime;

        private Cell[,] map;
        private Sprite spriteOfUnit;
        private sprite2Unit sprite2unit;
        //private Tank tank;

        //public Unit unit { get { return _unit; } set { unit = value; } }
        //public int X { get { return _x; } set { _x = value; } }
        //public int Y { get { return _y; } set { _y = value; } }

        public SpriteBatch SpriteBatch { get; set; }
        public GameTime GameTime { get; set; }

        public Viseur Viseur { get; set; }

        public Unit UnitofUnit { get; set; }
        public int XofUnit { get; set; }
        public int YofUnit { get; set; }

        public string Classe { get; set; }
        public string Rang { get; set; }
        //public Vector2 pos { get { return new Vector2(_x, _y); } }
        

        #endregion

        // // // // // // // // 

        #region CONSTRUCTEURS

        public Unit() { UnitofUnit = null; }

        public Unit(string rang, string classe, Cell[,] cellArray, int x, int y)
        {
            spriteOfUnit = new Sprite();
            spriteOfUnit.Initialize();
            
            this.Rang = rang;
            this.Classe = classe;
            this.XofUnit = x;
            this.YofUnit = y;
            map = cellArray;

            sprite2unit = new sprite2Unit(this.Rang, spriteOfUnit);

            UnitofUnit = this;
            
            //Exist();

            if (rang != "viseur")
            {
                map[XofUnit, YofUnit].unitOfCell = UnitofUnit;
                map[this.XofUnit, this.YofUnit].Occupe = true;
            }
            else if(rang!="viseur")
            {
                map[XofUnit, YofUnit].unitOfCell = new Unit(); ;
                map[this.XofUnit, this.YofUnit].Occupe = false;
            }

            //map[XofUnit, YofUnit].unitOfCell = UnitofUnit;

            /*if (rang != "viseur" && rang != null) _map[_x, _y].Occupe = true;
            if (rang != "viseur" && rang == null) _map[_x, _y].Occupe = false;*/
        }

        #endregion

        // // // // // // // // 

        #region FONCTIONS

        /*void Exist()
        {
            if (Rang != "viseur")
            {
                map[XofUnit, YofUnit].unitOfCell = UnitofUnit;
                map[XofUnit, YofUnit].Occupe = true;
            }
        }*/

        /*Unit Mvt(Unit oldUnit, Cell newUnit)
        {
            oldUnit.XofUnit = newUnit.XofCell;
            oldUnit.YofUnit = newUnit.YofCell;

            return oldUnit;

            int unitX = oldUnit.X;
            int unitY = oldUnit.Y;

            if (new Vector2(unitX, unitY) != new Vector2(newUnit.X, newUnit.Y))
            {
                #region CAS DOUBLES ( 2 COORDONNEES CHANGENT )
                
                float tempo = 0.15f;

                if ((unitX < newUnit.X && unitY < newUnit.Y) || (unitX < newUnit.X && unitY > newUnit.Y) ||
                    (unitX > newUnit.X && unitY > newUnit.Y) || (unitX < newUnit.X && unitY < newUnit.Y))
                {
                    if ((unitX < newUnit.X && unitY < newUnit.Y))//HG vers BD
                    {
                        if (gameTime.TotalGameTime - time > TimeSpan.FromSeconds(tempo))
                        {
                            _map[unitX, unitY].unit.unit._x += 1;
                            time = gameTime.TotalGameTime;
                        }

                        if (gameTime.TotalGameTime - time > TimeSpan.FromSeconds(tempo))
                        {
                            _map[unitX, unitY].unit.unit._y += 1;
                            time = gameTime.TotalGameTime;
                        }
                    }
                    if ((unitX < newUnit.X && unitY > newUnit.Y))//BG vers HD
                    {
                        if (gameTime.TotalGameTime - time > TimeSpan.FromSeconds(tempo))
                        {
                            unitX += 1; time = gameTime.TotalGameTime;
                        }

                        if (gameTime.TotalGameTime - time > TimeSpan.FromSeconds(tempo))
                        {
                            unitY += 1; time = gameTime.TotalGameTime;
                        }
                    }
                    if ((unitX > newUnit.X && unitY > newUnit.Y))//HG vers BD
                    {
                        if (gameTime.TotalGameTime - time > TimeSpan.FromSeconds(tempo))
                        {
                            unitX -= 1; time = gameTime.TotalGameTime;
                        }

                        if (gameTime.TotalGameTime - time > TimeSpan.FromSeconds(tempo))
                        {
                            unitY += 1; time = gameTime.TotalGameTime;
                        }
                    }
                    if ((unitX > newUnit.X && unitY < newUnit.Y))//BD vers HG
                    {
                        if (gameTime.TotalGameTime - time > TimeSpan.FromSeconds(tempo))
                        {
                            unitX -= 1; time = gameTime.TotalGameTime;
                        }

                        if (gameTime.TotalGameTime - time > TimeSpan.FromSeconds(tempo))
                        {
                            unitY -= 1; time = gameTime.TotalGameTime;
                        }
                    }
                }

                #endregion

                #region CAS SIMPLES ( 1 COORDONNEE CHANGE )

                else
                {
                    if (unitX < newUnit.X)
                    {
                        if (gameTime.TotalGameTime - time > TimeSpan.FromSeconds(tempo))
                        {
                            time = gameTime.TotalGameTime;
                            unitX += 1;
                        }

                    }
                    else
                    {
                        if (unitX > newUnit.X)
                        {
                            if (gameTime.TotalGameTime - time > TimeSpan.FromSeconds(tempo))
                            {
                                time = gameTime.TotalGameTime;
                                unitX -= 1;
                            }
                        }
                    }

                    if (unitY < newUnit.Y)
                    {
                        if (gameTime.TotalGameTime - time > TimeSpan.FromSeconds(tempo))
                        {
                            time = gameTime.TotalGameTime;
                            unitY += 1;
                        }
                    }
                    else
                    {
                        if (unitY > newUnit.Y)
                        {
                            if (gameTime.TotalGameTime - time > TimeSpan.FromSeconds(tempo))
                            {
                                time = gameTime.TotalGameTime;
                                unitY -= 1;
                            }
                        }
                    }
                }

                #endregion
            }
            oldUnit.X = unitX;
            oldUnit.Y = unitY;
            return oldUnit;*/


        /*public void MvtUnit(Viseur viseur)
        {
            if (viseur.destinationSelected)
            {
                Unit a;
                Cell b;
                a = this.map[(int)viseur.A.X, (int)viseur.A.Y].unitOfCell;
                b = this.map[(int)viseur.B.X, (int)viseur.B.Y];

                viseur.unit = Mvt(a, b);
            }

        }*/

        #endregion

        // // // // // // // // 

        #region DRAW

        public void DrawUnit(SpriteBatch spriteBatch, GameTime gameTime)
        {
            /*if (this.rang == "tank")
                tank._sprite.Draw(_spriteBatch, _gameTime, _map[_x, _y].positionPixel);
            else*/
            //sprite2unit.Draw(_spriteBatch, _gameTime, _map[_x, _y].positionPixel);
            spriteOfUnit.Draw(spriteBatch, gameTime, map[XofUnit, YofUnit].positionPixel);

        }

        public void DrawUnit(Sprite spriteunit, SpriteBatch spriteBatch, GameTime gameTime, Vector2 pospixel)
        {
            spriteunit.Draw(spriteBatch, gameTime, map[XofUnit, YofUnit].positionPixel);
        }


        #endregion
    }
}


