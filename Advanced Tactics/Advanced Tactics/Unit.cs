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

        private Constante var = Game1.cst;
        ContentManager Ctt = Game1.Ctt;
        private Cell[,] map;
        public Sprite spriteOfUnit;
        string path;

        public Viseur Viseur { get; set; }

        public int XofUnit { get; set; }
        public int YofUnit { get; set; }

        public string Classe { get; set; }
        public string Rang { get; set; }
        public List<int> MvtPossibleOfUnit { get; set; }

        #endregion

        // // // // // // // // 

        #region CONSTRUCTEURS

        Action<string, string, ContentManager, Sprite> Sprite2Unit = (p, r, c, s) => s.LC(c, p + r);

        public Unit() { XofUnit = 0; YofUnit = 0; }

        //rang = hq, pvt, tank...
        //clasee = dame, roi , fou...
        public Unit(string rang, string classe, Cell[,] cellArray, int x, int y, List<Unit> ListOfUnit)
        {
            if (cellArray[x, y].unitOfCell == null)
            {
                spriteOfUnit = new Sprite();

                this.Rang = rang;
                this.Classe = classe;
                this.XofUnit = x;
                this.YofUnit = y;
                map = cellArray;

                if (rang == "viseur") path = "Curseur/"; else path = "Unit/";
                Sprite2Unit(path, rang, Ctt, spriteOfUnit);

                MvtPossibleOfUnit = new List<int>();
                if (rang != null && classe != null)
                    ListOfUnit.Add(this);

                map[XofUnit, YofUnit].unitOfCell = this;

                map[this.XofUnit, this.YofUnit].Occupe = ListOfUnit.Contains(this);
            }
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
        public Unit(Unit oldunit, Cell[,] cellArray, Cell newCell, List<Unit> ListOfUnit)   // Constructeur de DESTRUCTION ahahahahahahahahahahahah
        {
            spriteOfUnit = new Sprite();

            this.Rang = oldunit.Rang;
            this.Classe = oldunit.Classe;
            this.XofUnit = newCell.XofCell;
            this.YofUnit = newCell.YofCell;
            map = cellArray;

            if (this.Rang == "viseur") path = "Curseur/"; else path = "Unit/";
            Sprite2Unit(path, this.Rang, Ctt, spriteOfUnit);

            ListOfUnit.Add(this);

            map[oldunit.XofUnit, oldunit.YofUnit].unitOfCell = null; // On mets a null la valeur de lunite dans lancienne case
            map[newCell.XofCell, newCell.YofCell].unitOfCell = this; // On mets a jour la valeur de la nouvelle case

            map[this.XofUnit, this.YofUnit].Occupe = ListOfUnit.Contains(this);

            for (int i = 0; i < ListOfUnit.Count(); i++) // On cherche lancienne unite et on la supprime de la liste des unitees a draw,
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
            spriteOfUnit.Draw(spriteBatch, gameTime, map[XofUnit, YofUnit].positionPixel);
        }
        #endregion
    }
}


