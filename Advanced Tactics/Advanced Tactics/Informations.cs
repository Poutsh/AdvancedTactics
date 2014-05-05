using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System.IO;
using Microsoft.Xna.Framework.Input;
using System.Xml;

namespace Advanced_Tactics
{
    class Informations
    {
        SpriteFont font, font2;
        MouseState mouseStateCurrent;
        Data data;

        Sprite unit;
        Sprite fondmetal, magasin, info;
        ContentManager ctt;
        List<Unit> _ListOfUnit;
        Map cartemap;
        private Viseur _viseur;

        Menu _menu = new Menu();

        private int h, w;
        List<string> deg = new List<string>();

        public Informations(Data data, Map map, Viseur viseur, List<Unit> ListOfUnit)
        {
            this.data = data;

            cartemap = map;
            ctt = data.Content;
            _viseur = viseur;
            _ListOfUnit = ListOfUnit;
            unit = new Sprite();
        }

        public virtual void LoadContent()
        {
            font = ctt.Load<SpriteFont>("info");
            font2 = ctt.Load<SpriteFont>("info2");
            fondmetal = new Sprite(); fondmetal.LC(data.Content, "Menu/Metal");
            magasin = new Sprite(); magasin.LC(data.Content, "Menu/Magasin");
            info = new Sprite(); info.LC(data.Content, "Menu/Informations");
        }

        public virtual void Update(GameTime gameTime)
        {
            mouseStateCurrent = Mouse.GetState();
        }//61 164

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
            info.Draw(data, spriteBatch, gameTime, new Vector2((data.PosXInit / 2) - info.Texture.Width / 2, 0));
            magasin.Draw(data, spriteBatch, gameTime, new Vector2((data.PosXInit / 2) - magasin.Texture.Width / 2, 4 * data.heightWindow / 7));
            fondmetal.Draw(data, spriteBatch, gameTime, new Vector2(0, 0));
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
            spriteBatch.DrawString(this.font, string.Format("Viewfinder {0},{1}", _viseur.viseurX, _viseur.viseurY), new Vector2(1 * (data.PosXInit / 8), info.Texture.Height + 30 * data.Scale), Color.Black);

            spriteBatch.DrawString(this.font, string.Format("Unit : "), new Vector2(1 * (data.PosXInit / 8), info.Texture.Height + 100 * data.Scale), Color.Black);
            if (cartemap.Carte[_viseur.viseurX, _viseur.viseurY].Occupe)
            {
                spriteBatch.DrawString(this.font, string.Format("{0}", cartemap.Carte[_viseur.viseurX, _viseur.viseurY].unitOfCell.Classe), new Vector2(1 * (data.PosXInit / 8) + 100 * data.Scale, info.Texture.Height + 100 * data.Scale), Color.Black);
                unit.LC(data.Content, "Unit/" + cartemap.Carte[_viseur.viseurX, _viseur.viseurY].unitOfCell.Rang);
                unit.Draw(data, spriteBatch, gameTime, new Vector2(1 * (data.PosXInit / 8) + 220 * data.Scale, info.Texture.Height + 90 * data.Scale), 1.2f);
                spriteBatch.DrawString(this.font, string.Format("PV :"), new Vector2(1 * (data.PosXInit / 8) + 10 * data.Scale, info.Texture.Height + 130 * data.Scale), Color.Black);
                spriteBatch.DrawString(this.font2, string.Format("{0}", cartemap.Carte[_viseur.viseurX, _viseur.viseurY].unitOfCell.PV), new Vector2(1 * (data.PosXInit / 8) + 90 * data.Scale, info.Texture.Height + 120 * data.Scale), Color.Red);
                spriteBatch.DrawString(this.font, string.Format("Strength :"), new Vector2(1 * (data.PosXInit / 8) + 10 * data.Scale, info.Texture.Height + 160 * data.Scale), Color.Black);
                spriteBatch.DrawString(this.font2, string.Format("{0}", cartemap.Carte[_viseur.viseurX, _viseur.viseurY].unitOfCell.Strength), new Vector2(1 * (data.PosXInit / 8) + 230 * data.Scale, info.Texture.Height + 155 * data.Scale), Color.Red);
            }

            string field = "";
            Color color = Color.Green;
            switch (data.altitudeTerrain[_viseur.viseurX, _viseur.viseurY])
            {
                case 0:
                    field = "Sea";
                    color = Color.Blue;
                    break;
                case 1:
                    field = "Land";
                    color = Color.GreenYellow;
                    break;
                case 2:
                    field = "Mountain";
                    color = Color.DarkGray;
                    break;
            }
            spriteBatch.DrawString(this.font, string.Format("Field : "), new Vector2(1 * (data.PosXInit / 8), info.Texture.Height + 230 * data.Scale), Color.Black);
            spriteBatch.DrawString(this.font, string.Format("{0}", field), new Vector2(1 * (data.PosXInit / 8) + 120 * data.Scale, info.Texture.Height + 230 * data.Scale), color);
            spriteBatch.End();
        }
    }
}
