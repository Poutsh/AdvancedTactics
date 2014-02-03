using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advanced_Tactics
{
    class Map
    {
        static int[][] Map_map()
        {
            int H = 100;
            int W = 100;
            int[][] Map_map = new int[H][];
            for (int i = 0; i < H; i++)
            {
                Map_map[i] = new int[W];
            }

            return Map_map;
       }
    }
}
