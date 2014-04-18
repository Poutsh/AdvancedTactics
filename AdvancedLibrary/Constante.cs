using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AdvancedLibrary
{
    public class Constante
    {
        private int _WidthMap;
        private int _HeightMap;
        private float tilesize;
        private float scale;
        private float posXinit;
        private int[,] altitudeterrain;
        private string p;
        private float _widthWindow, _heightWindow;

        public int WidthMap { get { return _WidthMap; } }
        public int HeightMap { get { return _HeightMap; } }
        public float TileSize { get { return tilesize; } }
        public float Scale { get { return scale; } }
        public float PosXInit { get { return posXinit; } }
        public int[,] altitudeTerrain { get { return altitudeterrain; } }
        public string fileMap { get { return p; } }
        public float widthWindow { get { return _widthWindow; } set { _widthWindow = value; } }
        public float heightWindow { get { return _heightWindow; } set { _heightWindow = value; } }
        public bool GR { get; set; }

        public Constante(string path, float height, float width)
        {
            p = "MapEngine /" + path + ".txt";
            _heightWindow = height;
            _widthWindow = width;
            StreamReader sReader = new StreamReader("MapEngine /" + path + ".txt");
            _HeightMap = File.ReadLines("MapEngine /" + path + ".txt").Count();
            _WidthMap = sReader.ReadLine().Split(',').Count();

            altitudeterrain = new int[_WidthMap, _HeightMap];

            tilesize = 32f;
            
            if (_HeightMap < _WidthMap)
            {
                //ale = (3 * width) / (5 * tilesize * _WidthMap);
                scale = height / (tilesize * _HeightMap);
                posXinit = width / 5;
            }
            else
            {
                scale = height / (tilesize * _HeightMap);
                posXinit = (width - tilesize * scale * _WidthMap) / 2f;
            }
        }
    }
}
