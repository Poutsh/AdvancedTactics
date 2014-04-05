﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Advanced_Tactics
{
    public class Tank : Unit
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
            _sprite.LoadContent(Game1.Ctt, "Unit/HQ");
        }
    }
}
