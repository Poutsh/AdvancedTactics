﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace Advanced_Tactics
{
    public class Data
    {
        protected int _WidthMap, _HeightMap;
        protected float tilesize;
        protected float scale;
        protected float posXinit;
        protected int[,] altitudeterrain;
        protected string p;
        protected int _widthWindow, _heightWindow;

        public int MapWidth { get { return _WidthMap; } set { _WidthMap = value; } }
        public int MapHeight { get { return _HeightMap; } set { _HeightMap = value; } }
        public float TileSize { get { return tilesize; } set { tilesize = value; } }
        public float Scale { get { return scale; } set { scale = value; } }
        public float PosXInit { get { return posXinit; } set { posXinit = value; } }
        public int[,] altitudeTerrain { get { return altitudeterrain; } set { altitudeterrain = value; } }
        public string fileMap { get { return p; } set { p = value; } }
        public int WindowWidth { get { return _widthWindow; } set { _widthWindow = value; } }
        public int WindowHeight { get { return _heightWindow; } set { _heightWindow = value; } }

        public Data(string path, int width, int height)
        {
            p = "Map/" + path + ".txt";

            _widthWindow = width;
            _heightWindow = height;

            StreamReader sReader = new StreamReader("Map/" + path + ".txt");
            _HeightMap = File.ReadLines("Map/" + path + ".txt").Count();
            _WidthMap = sReader.ReadLine().Split(',').Count();

            altitudeterrain = new int[_WidthMap, _HeightMap];

            tilesize = 32f;

            if (_HeightMap < _WidthMap)
            {
                scale = _heightWindow / (tilesize * _HeightMap);
                posXinit = _widthWindow / 5;
            }
            else
            {
                scale = _heightWindow / (tilesize * _HeightMap);
                posXinit = (_widthWindow - tilesize * scale * _WidthMap) / 2f;
            }
        }
    }
}
