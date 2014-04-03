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
    /// <summary>
    /// isEmpty = case vide ou pas
    /// cellTerrain = type de terrain(eau, terre, montagne)/// 
    /// </summary>
    public class Cell
    {
        private Variable var;
        private bool empty;
        private int type;
        private string unitepresente;
        private int x, y;
        private Vector2 pos;

        private List<String> listunit = new List<string> { "tank", "pion" };
        

        public bool isEmpty;
        public int cellType;
        public string Unitepresente;
        public Vector2 positionPixel
        {
            get { return pos; }
        }

        public Cell(Variable variable, bool occupe, int terrain, string unit, int x, int y)
        {
            var = variable;
            isEmpty = occupe;
            cellType = terrain;

            if (listunit.Contains(unit))
                Unitepresente = unit;
            else
                Unitepresente = "toto";

            pos = new Vector2(x * var.Scale * var.TileSize + var.PosXInit, y * var.Scale * var.TileSize);
        }
    }

    public class Map
    {
        public Cell[,] map;
        Random random = new Random();
        Variable var;
        private int nb = 0;

        public Map(Variable variable)
        {
            var = variable;
            map = new Cell[var.WidthMap, var.HeightMap];

            for (int x = 0; x < var.WidthMap; x++)
            {
                for (int y = 0; y < var.HeightMap; y++)
                {
                    //Debug
                    string[] items = { "tank", "pion", "avion", "joueur" };
                    bool state = (random.Next(0, 4) >= 2);
                    int ter = random.Next(0, 4);

                    map[x, y] = new Cell(var, state, ter, items[random.Next(items.Length)], x, y);
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
