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
        private Cell[,] map;
        public Sprite spriteOfUnit;
        private sprite2Unit sprite2unit;

        public Viseur Viseur { get; set; }

        public Unit UnitofUnit { get; set; }
        public int XofUnit { get; set; }
        public int YofUnit { get; set; }

        public string Classe { get; set; }
        public string Rang { get; set; }
        #endregion

        // // // // // // // // 

        #region CONSTRUCTEURS

        public Unit() { UnitofUnit = null; }

        //rang = hq, pvt, tank...
        //clasee = dame, roi , fou...
        public Unit(string rang, string classe, Cell[,] cellArray, int x, int y, List<Unit> ListOfUnit)
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


            if (rang != null && classe != null)
                ListOfUnit.Add(UnitofUnit);

            map[XofUnit, YofUnit].unitOfCell = UnitofUnit;

            map[this.XofUnit, this.YofUnit].Occupe = ListOfUnit.Contains(this);

        }

        /// <summary>
        /// Constructeur de deplacement d'unite
        /// </summary>
        /// <param name="rang">rang de l'ancienne unite</param>
        /// <param name="classe">classe de l'ancienne unite</param>
        /// <param name="cellArray">map</param>
        /// <param name="x">X de l'ancienne unite</param>
        /// <param name="y">Y de l'ancienne unite</param>
        /// <param name="ListOfUnit"></param>
        /// <param name="oldunit">ancienne unite</param>
        /// <param name="destruction">TRUE</param>
        public Unit(string rang, string classe, Cell[,] cellArray, int x, int y, List<Unit> ListOfUnit, Unit oldunit, bool destruction = true)
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

            ListOfUnit.Add(UnitofUnit);

            map[XofUnit, YofUnit].unitOfCell = UnitofUnit;

            map[this.XofUnit, this.YofUnit].Occupe = ListOfUnit.Contains(this);

            for (int i = 0; i < ListOfUnit.Count(); i++)
                if (ListOfUnit[i] == oldunit)
                {
                    ListOfUnit.RemoveAt(i);
                    map[oldunit.XofUnit, oldunit.YofUnit].Occupe = false;
                }
        }

        #endregion

        // // // // // // // // 

        #region DRAW

        public void DrawUnit(SpriteBatch spriteBatch, GameTime gameTime)
        {
            /*for (int ddd = 0; ddd < ListUnit.Count(); ddd++)
            {
                ListUnit[ddd].spriteOfUnit.Draw(spriteBatch, gameTime, map[XofUnit, YofUnit].positionPixel);
            }*/
            spriteOfUnit.Draw(spriteBatch, gameTime, map[XofUnit, YofUnit].positionPixel);
        }
        #endregion
    }
}


