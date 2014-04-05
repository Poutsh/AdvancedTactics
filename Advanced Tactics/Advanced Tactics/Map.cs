using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using AdvancedLibrary;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Advanced_Tactics
{
    public class Map
    {
        public Cell[,] map;
        Variable var;
        //Unit unit = new Unit(0, 0);
        public Map(Variable variable)
        {
            var = variable;
            map = new Cell[var.WidthMap, var.HeightMap];

            for (int x = 0; x < var.WidthMap; x++)
            {
                for (int y = 0; y < var.HeightMap; y++)
                {
                    //map[x, y] = new Cell(map, unit, x, y, var);
                    map[x, y] = new Cell(map, x, y, false);
                }
            }
        }
    }

    public class RandomSprite
    {
        public List<int> listposx;
        public List<int> List_of_positionx
        {
            get { return listposx; }
        }

        public List<int> listposy;
        public List<int> List_of_positiony
        {
            get { return listposy; }
        }

        private int nbunit = 0;
        Variable var;
        

        public RandomSprite(Variable variable, int nb_unit_to_create)
        {
            var = variable;
            Random random = new Random();
            listposx = new List<int>(nb_unit_to_create);
            listposy = new List<int>(nb_unit_to_create);

            while (nbunit <= nb_unit_to_create)
            {
                listposx.Add(random.Next(0, 27));
                listposy.Add(random.Next(0, 31));
                ++nbunit;
            }
        }

        
    }
}
