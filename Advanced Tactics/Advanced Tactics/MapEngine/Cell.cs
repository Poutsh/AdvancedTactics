using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Advanced_Tactics
{
    public class Map
    {
        #region VARIABLES

        Data data;
        private Cell[,] map;

        public Cell[,] Carte { get { return map; } }
        

        #endregion

        // // // // // // // // 

        #region CONSTRUCTEUR

        public Map(Data data)
        {
            this.data = data;
            map = new Cell[data.WidthMap, data.HeightMap];

            for (int x = 0; x < data.WidthMap; x++)
                for (int y = 0; y < data.HeightMap; y++)
                    map[x, y] = new Cell(data, x, y);
            
        }

        #endregion
    }


    public class Cell
    {
        #region VARIABLES

        Data data;
        private int x, y;
        private Vector2 pos;

        public Unit unitOfCell { get; set; }
        public bool Occupe { get; set; }
        public int XofCell { get { return x; } }
        public int YofCell { get { return y; } }
        public Vector2 Vector2OfCell { get { return new Vector2(x, y); } }
        public Vector VectorOfCell { get { return new Vector(x, y); } }

        public Vector2 positionPixel
        {
            get { if (x <= data.WidthMap && y <= data.HeightMap && x >= 0 && y >= 0) return pos; else return new Vector2(data.PosXInit, 0); }
        }

        #endregion

        // // // // // // // // 

        #region CONSTRUCTEURS

        // Constructeur d'initialisation de la map
        public Cell() { }

        public Cell(Data data, int x, int y)
        {
            this.data = data;
            this.x = x;
            this.y = y;
            pos = new Vector2(this.x * data.Scale * data.TileSize + data.PosXInit, this.y * data.Scale * data.TileSize);
        }

        #endregion
    }
}
