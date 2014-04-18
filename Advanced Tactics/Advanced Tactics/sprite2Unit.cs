using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using AdvancedLibrary;
using Microsoft.Xna.Framework.Content;

namespace Advanced_Tactics
{
    public class sprite2Unit
    {
        public sprite2Unit(ContentManager content, string rang, Sprite sprite)
        {
            if (rang == "viseur")
                sprite.LoadContent(content, "Curseur/viseur");
            else
                sprite.LoadContent(content, "Unit/" + rang);

        }
    }
}
