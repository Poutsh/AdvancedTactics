using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Advanced_Tactics
{
    public class Tank : Game
    {
        public string rang = "tank";
        public int lvl = 100;
        public string up = null;
        public int pvmax = 100;
        public int pvcur = 80;
        public int force = 60;
        public int pa = 1;
        public Sprite _sprite;
        

        public Tank(Sprite sprite)
            :base()
        {
            _sprite = sprite;
            _sprite.LoadContent(Content, "Unit/Tank");
        }
    }
}
