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
    public class Map
    {
        #region VARIABLES

        Constante var = Game1.cst;
        private Cell[,] map;

        public Cell[,] Carte { get { return map; } }

        #endregion

        // // // // // // // // 

        #region CONSTRUCTEUR

        public Map()
        {
            map = new Cell[var.WidthMap, var.HeightMap];

            for (int x = 0; x < var.WidthMap; x++)
                for (int y = 0; y < var.HeightMap; y++)
                    map[x, y] = new Cell(x, y);
        }

        #endregion
    }


    public class Cell
    {
        #region VARIABLES

        private Constante cst = Game1.cst;
        ContentManager ctt = Game1.Ctt;

        private int x, y;
        private Vector2 pos;

        public Unit unitOfCell { get; set; }
        public bool Occupe { get; set; }
        public int XofCell { get { return x; } }
        public int YofCell { get { return y; } }
        public Vector2 VectorOfCell { get { return new Vector2(x, y); } }

        public Vector2 positionPixel
        {
            get { if (x <= cst.WidthMap && y <= cst.HeightMap && x >= 0 && y >= 0) return pos; else return new Vector2(cst.PosXInit, 0); }
        }

        #endregion

        // // // // // // // // 

        #region CONSTRUCTEURS

        // Constructeur d'initialisation de la map
        public Cell() { }

        public Cell(int x, int y)
        {
            this.x = x;
            this.y = y;
            pos = new Vector2(this.x * cst.Scale * cst.TileSize + cst.PosXInit, this.y * cst.Scale * cst.TileSize);
        }

        #endregion
    }
}
