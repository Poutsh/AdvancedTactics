using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdvancedLibrary;

namespace Advanced_Tactics
{
    public class sprite2Unit
    {
        private Variable var = Game1.var;

        public sprite2Unit(string rang, Sprite sprite)
        {
            switch (rang)
            {
                case "viseur":
                    sprite.LoadContent(Game1.Ctt, "Curseur/viseur");
                    break;
                case "hq":
                    sprite.LoadContent(Game1.Ctt, "Unit/HQ");
                    break;
                case "truck":
                    sprite.LoadContent(Game1.Ctt, "Unit/Truck");
                    break;
                case "ing":
                    sprite.LoadContent(Game1.Ctt, "Unit/Ing");
                    break;
                case "doc":
                    sprite.LoadContent(Game1.Ctt, "Unit/Doc");
                    break;
                case "aa":
                    sprite.LoadContent(Game1.Ctt, "Unit/AA");
                    break;
                case "plane":
                    sprite.LoadContent(Game1.Ctt, "Unit/Plane");
                    break;
                case "tank":
                    sprite.LoadContent(Game1.Ctt, "Unit/Tank");
                    break;
                case "com":
                    sprite.LoadContent(Game1.Ctt, "Unit/Com");
                    break;
                case "pvt":
                    sprite.LoadContent(Game1.Ctt, "Unit/Pvt");
                    break;
            }
        }
    }
}
