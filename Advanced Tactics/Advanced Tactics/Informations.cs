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
        Map cartemap;
        private Viseur _viseur;
        Menu _menu = new Menu();

        private int h, w;
        List<string> deg = new List<string>();

        public Informations(Data data, Map map, Viseur viseur)
        {
            this.data = data;
            cartemap = map;
            ctt = Game1.Ctt;
            _viseur = viseur;
            unit = new Sprite();
        }

        public virtual void LoadContent()
        {
            font = ctt.Load<SpriteFont>("info");
            font2 = ctt.Load<SpriteFont>("info2");
            fondmetal = new Sprite(); fondmetal.LC(Game1.Ctt, "Menu/Metal");
            magasin = new Sprite(); magasin.LC(Game1.Ctt, "Menu/Magasin");
            info = new Sprite(); info.LC(Game1.Ctt, "Menu/Informations");
        }

        public virtual void Update(GameTime gameTime)
        {
            mouseStateCurrent = Mouse.GetState();
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
            info.Draw(data, spriteBatch, new Vector2((data.PosXInit / 2) - info.Texture.Width / 2, 0));
            magasin.Draw(data, spriteBatch, new Vector2((data.PosXInit / 2) - magasin.Texture.Width / 2, 4 * data.WindowHeight / 7));
            fondmetal.Draw(data, spriteBatch, new Vector2(0, 0));
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
            spriteBatch.DrawString(this.font, string.Format("Viewfinder {0},{1}", _viseur.viseurX, _viseur.viseurY), new Vector2(1 * (data.PosXInit / 8), info.Texture.Height + 30 * data.Scale), Color.Black);

            spriteBatch.DrawString(this.font, string.Format("Unit : "), new Vector2(1 * (data.PosXInit / 8), info.Texture.Height + 100 * data.Scale), Color.Black);

            if (_viseur.viseurX >= 0 && _viseur.viseurX <= data.MapWidth && _viseur.viseurY >= 0 && _viseur.viseurY <= data.MapHeight && cartemap.Carte[_viseur.viseurX, _viseur.viseurY].Occupe)
            {
                spriteBatch.DrawString(this.font, string.Format("{0}", cartemap.Carte[_viseur.viseurX, _viseur.viseurY].unitOfCell.Classe), new Vector2(1 * (data.PosXInit / 8) + 100 * data.Scale, info.Texture.Height + 100 * data.Scale), Color.Black);
                unit.LC(Game1.Ctt, "Unit/" + cartemap.Carte[_viseur.viseurX, _viseur.viseurY].unitOfCell.Player.ColorSideN + cartemap.Carte[_viseur.viseurX, _viseur.viseurY].unitOfCell.Rang);
                unit.Draw(data, spriteBatch, new Vector2(1 * (data.PosXInit / 8) + 220 * data.Scale, info.Texture.Height + 90 * data.Scale), 1.2f);
                spriteBatch.DrawString(this.font, string.Format("PV :"), new Vector2(1 * (data.PosXInit / 8) + 10 * data.Scale, info.Texture.Height + 130 * data.Scale), Color.Black);
                spriteBatch.DrawString(this.font2, string.Format("{0}", cartemap.Carte[_viseur.viseurX, _viseur.viseurY].unitOfCell.PV), new Vector2(1 * (data.PosXInit / 8) + 90 * data.Scale, info.Texture.Height + 120 * data.Scale), Color.Red);
                spriteBatch.DrawString(this.font, string.Format("Strength :"), new Vector2(1 * (data.PosXInit / 8) + 10 * data.Scale, info.Texture.Height + 160 * data.Scale), Color.Black);
                spriteBatch.DrawString(this.font2, string.Format("{0}", cartemap.Carte[_viseur.viseurX, _viseur.viseurY].unitOfCell.Strength), new Vector2(1 * (data.PosXInit / 8) + 230 * data.Scale, info.Texture.Height + 155 * data.Scale), Color.Red);
            }

            if (_viseur.depSelec)
            {

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
