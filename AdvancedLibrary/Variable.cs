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

        public int WidthMap
        {
            get { return _WidthMap; }
            set { _WidthMap = value; }
        }
        public int HeightMap
        {
            get { return _HeightMap; }
            set { _HeightMap = value; }
        }
        public float TileSize
        {
            get { return tilesize; }
            set { tilesize = value; }
        }
        public float Scale
        {
            get { return scale; }
            set { scale = value; }
        }
        public float PosXInit
        {
            get { return posXinit; }
            set { posXinit = value; }
        }

        public Variable(string path, float height, float width)
        {
            StreamReader sReader = new StreamReader("Map /" + path + ".txt");
            _HeightMap = File.ReadLines("Map /" + path + ".txt").Count();
            _WidthMap = sReader.ReadLine().Split(',').Count();
            

            tilesize = 32f;
            scale = height / (tilesize * _HeightMap);
            posXinit = (width - tilesize * scale * _WidthMap) / 2f;
        }
    }
}
