using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using AdvancedLibrary;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Advanced_Tactics
{
    public class Map
    {
        #region VARIABLES

        Variable var = Game1.var;
        private Cell[,] map;
        private int x, y;

        //public Cell[,] Carte { get { return map; } }
        //public int X { get { return x; } }
        //public int Y { get { return y; } }

        public Cell[,] Carte { get { return map; } }
        public int XofCarte { get { return x; } }
        public int YofCarte { get { return y; } }

        #endregion

        // // // // // // // // 

        #region CONSTRUCTEUR

        public Map(List<Unit> ListOfUnit)
        {
            map = new Cell[var.WidthMap, var.HeightMap];

            for (int x = 0; x < var.WidthMap; x++)
                for (int y = 0; y < var.HeightMap; y++)
                    map[x, y] = new Cell(x, y, ListOfUnit);
        }

        #endregion
    }

    public class Cell
    {
        #region VARIABLES

        private Variable var = Game1.var;
        ContentManager ctt = Game1.Ctt;

        private int x, y;
        private Vector2 pos;
        private Unit unit;
        private Cell[,] cell;

        public Unit unitOfCell { get; set; }
        public bool Occupe { get; set; }
        public int XofCell { get { return x; } }
        public int YofCell { get { return y; } }
        public Vector2 VectorOfCell { get { return new Vector2(x, y); } }

        public Vector2 positionPixel
        {
            get { if (x <= var.WidthMap && y <= var.HeightMap && x >= 0 && y >= 0) return pos; else return new Vector2(var.PosXInit, 0); }
        }

        #endregion

        // // // // // // // // 

        #region CONSTRUCTEURS

        // Constructeur d'initialisation de la map

        public Cell(int x, int y, List<Unit> ListOfUnit)
        {
            this.x = x;
            this.y = y;
            pos = new Vector2(this.x * var.Scale * var.TileSize + var.PosXInit, this.y * var.Scale * var.TileSize);
        }

        #endregion
    }
}
