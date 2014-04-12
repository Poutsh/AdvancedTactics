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
        MouseState mouseStateCurrent;
        Variable var;
        Sprite _r, _b;

        ContentManager ctt;
        Map cartemap;
        //RandomSprite rand;
        private Viseur _viseur;
        private Unit unit;
        private int h, w;

        public Debug(ContentManager content, Variable variable, Map map, int BufferHeight, int BufferWitdh, Viseur viseur)
        {
            var = variable;
            cartemap = map;
            ctt = content;
            _viseur = viseur;
            h = BufferHeight; w = BufferWitdh;

            _r = new Sprite(); _r.Initialize();
            _b = new Sprite(); _b.Initialize();
        }

        public virtual void LoadContent()
        {
            font = ctt.Load<SpriteFont>("font");

            _r.LoadContent(ctt, "Case/rouge");
            _b.LoadContent(ctt, "Case/bleu");
        }

        public virtual void Update(GameTime gameTime)
        {
            mouseStateCurrent = Mouse.GetState();
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.DrawString(this.font,
                "HeightMap : " + Convert.ToString(var.HeightMap),
                new Vector2(20, 400), Color.Black);

            spriteBatch.DrawString(this.font,
                "WidthMap : " + Convert.ToString(var.WidthMap),
                new Vector2(20, 415), Color.Black);

            spriteBatch.DrawString(this.font,
                "Scale : " + Convert.ToString(var.Scale),
                new Vector2(20, 430), Color.Black);

            spriteBatch.DrawString(this.font,
                "Tile size : " + Convert.ToString(var.TileSize), 
                new Vector2(20, 445), Color.Black);

            spriteBatch.DrawString(this.font,
                "Pos init : " + Convert.ToString(var.PosXInit),
                new Vector2(20, 460), Color.Black);

            spriteBatch.DrawString(this.font,
                string.Format("Mouse ({0}, {1})", Mouse.GetState().X, Mouse.GetState().Y),
                new Vector2(20, 475), Color.Black);

            spriteBatch.DrawString(this.font,
                string.Format("est rempli  {0}", cartemap.Carte[1, 1].unitOfCell.Rang),
                new Vector2(20, 500), Color.Black);


            /*spriteBatch.DrawString(this.font,
                string.Format("Carte : ( {0} , {1} )", _viseur.viseurX ,_viseur.viseurY),
                new Vector2(213, 102), Color.Black);*/


            spriteBatch.DrawString(this.font,
                string.Format("Depart          {0}", _viseur.departurePosition),
                new Vector2(22, 96), Color.Black);
            
            spriteBatch.DrawString(this.font,
                string.Format("Destination    {0}", _viseur.destinationPosition),
                new Vector2(20, 112), Color.Black);
            
            spriteBatch.DrawString(this.font,
                string.Format("bool point A   {0}", _viseur.departureSelected),
                new Vector2(20, 126), Color.Black);
            
            spriteBatch.DrawString(this.font,
                string.Format("bool point B   {0}", _viseur.destinationSelected),
                new Vector2(20, 139), Color.Black);


            //Viseur
            spriteBatch.DrawString(this.font, string.Format("Coord. Viseur > {0},{1}", _viseur.viseurX, _viseur.viseurY), new Vector2(213, 102), Color.Indigo);
            //spriteBatch.DrawString(this.font, string.Format("Rang  {0}", cartemap.Carte[_viseur.viseurX, _viseur.viseurY].unitOfCell.Rang), new Vector2(230, 116), Color.Indigo);

            //spriteBatch.DrawString(this.font, string.Format("Rang  {0}", cartemap.Carte[_viseur.viseurX, _viseur.viseurY].unitOfCell.Rang), new Vector2(230, 116), Color.Indigo);

            if (cartemap.Carte[_viseur.viseurX, _viseur.viseurY].Occupe)
            {
                
            }else
            {
                spriteBatch.DrawString(this.font,
                    string.Format("Rang  {0}", "vide"),
                    new Vector2(230, 116), Color.Indigo);
            }
            

            spriteBatch.DrawString(this.font,
                string.Format("OverUnit  {0}", cartemap.Carte[1, 1].Occupe),
                new Vector2(20, 158), Color.Red);

            spriteBatch.DrawString(this.font,
                string.Format("OverUnit public  {0}", _viseur.ViseurOverUnit),
                new Vector2(20, 258), Color.Yellow);


           /* if (_viseur.ViseurOverUnit)
            {
                spriteBatch.DrawString(this.font,
                    string.Format("ViseurOverUnit {0}", _viseur.UnitInCell.Rang),
                    new Vector2(220, 170), Color.Green);
            }*/
        }
    }
}
