using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using AdvancedLibrary;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Advanced_Tactics_Propre
{
    /// <summary>
    /// isEmpty = case vide ou pas
    /// cellTerrain = type de terrain(eau, terre, montagne)/// 
    /// </summary>
    public class Cell
    {
        private bool empty;
        private int type;
        private string unitepresente;
        private List<String> listunit = new List<string> { "tank", "pion" };

        public bool isEmpty;
        public int cellType;
        public string Unitepresente;

        public Cell(bool occupe, int terrain, string unit)
        {
            isEmpty = occupe;
            cellType = terrain;

            if (listunit.Contains(unit))
                Unitepresente = unit;
            else
                Unitepresente = "toto";
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

                    map[x, y] = new Cell(state, ter, items[random.Next(items.Length)]);
                }
            }
        }
    }

    public class RandomSprite
    {
        public List<Vector2> List_of_position;
        private int nbunit;
        Variable var;

        public RandomSprite(Variable variable, int nb_unit_to_create)
        {
            var = variable;
            nbunit = nb_unit_to_create;
            nbunit = nb_unit_to_create;
        }

        public void Position()
        {
            Random random = new Random();
            float x, y;
            List_of_position = new List<Vector2>();

            
            for (int i = 0; i < List_of_position.Count(); i++)
            {
                x = 12 + i;
                y = 15 + i;

                List_of_position.Add(new Vector2(x, y));
            }
        }
    }
}
