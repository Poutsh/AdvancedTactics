using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdvancedLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Advanced_Tactics
{
    public class Unit
    {
        private Variable var = Game1.var;
        private Cell[,] _map;

        private int _x, _y;
        private Sprite spriteunit;
        private string _rang, _classe;
        private Viseur viseur;
        private spriteUnit spp;
        private Tank tank;

        private int _lvl;
        private int _pvmax;
        private int _force;
        private int _pa;

        public int X { get { return _x; } set { _x = value; } }
        public int Y { get { return _y; } set { _y = value; } }

        /* classe { Roi, Dame, Tour, Fou, Cavalier, Pion }
           rang { HQ, Truck, Ing, Doc, AA, Plane, Tank, Com, Pvt }*/

        public Unit() { }

        public Unit(string rang, string classe, Cell[,] map, int x, int y)
        {
            spriteunit = new Sprite(); spriteunit.Initialize();
            tank = new Tank(spriteunit);

            _rang = rang; _classe = classe;
            _x = x; _y = y;
            _map = map;

            if (rang != "viseur")
                map[x, y].Occupe = true;

            spp = new spriteUnit(_rang, spriteunit);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (_rang == "tank")
                tank._sprite.Draw(spriteBatch, gameTime, _map[_x, _y].positionPixel);
            else
                spriteunit.Draw(spriteBatch, gameTime, _map[_x, _y].positionPixel);
        }
    }
}
