using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AdvancedLibrary
{
    public class Variable
    {
        private int _WidthMap;
        private int _HeightMap;
        private float tilesize;
        private float scale;
        private float posXinit;
        private int[,] altitudeterrain;
        private string p;

        public int WidthMap { get { return _WidthMap; } }
        public int HeightMap { get { return _HeightMap; } }
        public float TileSize { get { return tilesize; } }
        public float Scale { get { return scale; } }
        public float PosXInit { get { return posXinit; } }
        public int[,] altitudeTerrain { get { return altitudeterrain; } }
        public string fileMap { get { return p; } }

        public Variable(string path, float height, float width)
        {
            p = path;
            StreamReader sReader = new StreamReader("Map /" + path + ".txt");
            _HeightMap = File.ReadLines("Map /" + path + ".txt").Count();
            _WidthMap = sReader.ReadLine().Split(',').Count();

            altitudeterrain = new int[_WidthMap, _HeightMap];

            tilesize = 32f;
            
            if (_HeightMap < _WidthMap)
            {
                scale = (3 * width) / (5 * tilesize * _WidthMap);
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
