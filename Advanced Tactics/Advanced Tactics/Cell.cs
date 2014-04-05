using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using AdvancedLibrary;
using Microsoft.Xna.Framework.Content;

namespace Advanced_Tactics
{
    public class Cell
    {
        private Variable var = Game1.var;
        private int _x, _y;
        private bool _occupe;
        private int altitude;
        private Vector2 pos;

        public bool Occupe { get { return _occupe; } set { _occupe = value; } }


        public Vector2 positionPixel
        {
            get { if (_x <= var.WidthMap && _y <= var.HeightMap && _x >= 0 && _y >= 0) return pos; else return new Vector2(var.PosXInit, 0); }
        }

        public Cell(Cell[,] map, int x, int y)
        {
            _x = x;
            _y = y;
            pos = new Vector2(_x * var.Scale * var.TileSize + var.PosXInit, _y * var.Scale * var.TileSize);
            
        }

        public Cell(Cell[,] map, int x, int y, bool occupe)
        {
            _x = x;
            _y = y;
            _occupe = occupe;
            pos = new Vector2(_x * var.Scale * var.TileSize + var.PosXInit, _y * var.Scale * var.TileSize);
        }
    }
}
