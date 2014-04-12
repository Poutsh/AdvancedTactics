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

        private Variable var = Game1.var;
        ContentManager ctt = Game1.Ctt;

        private int x, y;
        //private bool occupe;
        //private int altitude;
        private Vector2 pos;
        private Unit unit;

        private Cell[,] cell;

        //public Map CellMap { get { return _map; } set { _map = value; } }
        public Unit unitOfCell { get; set; }
        public bool Occupe { get; set; }
        public int XofCell { get { return x; } }
        public int YofCell { get { return y; } }

        public Vector2 positionPixel
        {
            get { if (x <= var.WidthMap && y <= var.HeightMap && x >= 0 && y >= 0) return pos; else return new Vector2(var.PosXInit, 0); }
        }

        #endregion

        // // // // // // // // 

        #region CONSTRUCTEURS

        /*public Cell(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (_map[_x, _y].unit != null && _map[_x, _y].unit._rang != "viseur")
            if (_unit != null && _unit.Rang != "viseur")
                _occupe = true;
            if (_unit == null)
                _occupe = false;
        }*/

        // Constructeur d'initialisation de la map

        public Cell(int x, int y)
        {
            this.x = x;
            this.y = y;
            pos = new Vector2(this.x * var.Scale * var.TileSize + var.PosXInit, this.y * var.Scale * var.TileSize);

            //Occupe = (unitOfCell != null); 
        }

        #endregion

        public void Occupation()
        {
            //Occupe = (unitOfCell != null); 
        }
    }
}
