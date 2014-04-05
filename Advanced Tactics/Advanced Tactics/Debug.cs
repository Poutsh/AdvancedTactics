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

namespace Advanced_Tactics
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
        private int h, w;

        public Debug(ContentManager content, Variable variable, Map map, int BufferHeight, int BufferWitdh)
        {
            var = variable;
            cartemap = map;
            ctt = content;
            h = BufferHeight; w = BufferWitdh;
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
            string y = "HeightMap : " + Convert.ToString(var.HeightMap);
            spriteBatch.DrawString(this.font, y, new Vector2(20, 400), Color.Black);

            string x = "WidthMap : " + Convert.ToString(var.WidthMap);
            spriteBatch.DrawString(this.font, x, new Vector2(20, 415), Color.Black);

            string scale = "Scale : " + Convert.ToString(var.Scale);
            spriteBatch.DrawString(this.font, scale, new Vector2(20, 430), Color.Black);

            string size = "Tile size : " + Convert.ToString(var.TileSize);
            spriteBatch.DrawString(this.font, size, new Vector2(20, 445), Color.Black);

            string pos = "Pos init : " + Convert.ToString(var.PosXInit);
            spriteBatch.DrawString(this.font, pos, new Vector2(20, 460), Color.Black);

            string mousepos = string.Format("Mouse ({0}, {1})", Mouse.GetState().X, Mouse.GetState().Y);
            spriteBatch.DrawString(this.font, mousepos, new Vector2(20, 475), Color.Black);


            string test = string.Format("est rempli  {0}", cartemap.map[0, 0].Occupe);
            spriteBatch.DrawString(this.font, test, new Vector2(20, 500), Color.Black);

            //posXinit = (width - tilesize * scale * _WidthMap) / 2f;
            string tests = string.Format("{0}", h);
            spriteBatch.DrawString(this.font, tests, new Vector2(20, 850), Color.Black);
        }
    }
}
