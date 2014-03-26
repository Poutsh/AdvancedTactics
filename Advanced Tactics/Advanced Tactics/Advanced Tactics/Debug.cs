using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System.IO;
using Microsoft.Xna.Framework.Input;
using AdvancedLibrary;

namespace Advanced_Tactics_Propre
{
    /// <summary>
    /// Class pour afficher des valeurs textes pour aider aux debugs
    /// </summary>
    public class Debug
    {
        SpriteFont font;
        MouseState mouseStatePrevious, mouseStateCurrent;
        Variable var;
        ContentManager ctt;
        Map cartemap;
        RandomSprite rand;

        public Debug(ContentManager content, Variable variable, Map map, RandomSprite randd)
        {
            var = variable;
            cartemap = map;
            ctt = content;
            rand = randd;
        }

        public virtual void LoadContent()
        {
            font = ctt.Load<SpriteFont>("font");
        }

        public virtual void Update(GameTime gameTime)
        {
            mouseStateCurrent = Mouse.GetState();
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            string y = "Y : " + Convert.ToString(var.HeightMap);
            spriteBatch.DrawString(this.font, y, new Vector2(20, 400), Color.Black);

            string x = "X : " + Convert.ToString(var.WidthMap);
            spriteBatch.DrawString(this.font, x, new Vector2(20, 415), Color.Black);

            string scale = "Scale : " + Convert.ToString(var.Scale);
            spriteBatch.DrawString(this.font, scale, new Vector2(20, 430), Color.Black);

            string size = "Tile size : " + Convert.ToString(var.TileSize);
            spriteBatch.DrawString(this.font, size, new Vector2(20, 445), Color.Black);

            string pos = "Pos init : " + Convert.ToString(var.PosXInit);
            spriteBatch.DrawString(this.font, pos, new Vector2(20, 460), Color.Black);

            string mousepos = string.Format("Mouse ({0}, {0})", Mouse.GetState().X, Mouse.GetState().Y);
            spriteBatch.DrawString(this.font, mousepos, new Vector2(20, 475), Color.Black);

            //string test = Convert.ToString(cartemap.map[2, 3].nombre);
            //spriteBatch.DrawString(this.font, test, new Vector2(20, 500), Color.Black);


        }
    }
}
