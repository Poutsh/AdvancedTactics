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
        Data data;

        Sprite unit;
        Sprite fondmetal, magasin, info;
        ContentManager ctt;
        Cell[,] map;
        Menu _menu = new Menu();

        private int h, w;
        List<string> deg = new List<string>();

        public Informations()
        {
            ctt = Game1.Ctt;
            unit = new Sprite();
        }

        public void Update(Data data, Cell[,] Map)
        {
            this.data = data;
            map = Map;
        }

        public void LoadContent()
        {
            font = ctt.Load<SpriteFont>("info");
            font2 = ctt.Load<SpriteFont>("info2");
            fondmetal = new Sprite(); fondmetal.LC(Game1.Ctt, "Menu/Metal");
            magasin = new Sprite(); magasin.LC(Game1.Ctt, "Menu/Magasin");
            info = new Sprite(); info.LC(Game1.Ctt, "Menu/Informations");
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, Match match, Viseur viseur)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
            info.Draw(data, spriteBatch, new Vector2((data.PosXInit / 2) - info.Texture.Width / 2, 0));
            magasin.Draw(data, spriteBatch, new Vector2((data.PosXInit / 2) - magasin.Texture.Width / 2, 4 * data.WindowHeight / 7));
            fondmetal.Draw(data, spriteBatch, new Vector2(0, 0));
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
            spriteBatch.DrawString(this.font, string.Format("Turn"), new Vector2(17, 120), Color.Black);
            spriteBatch.DrawString(this.font, string.Format("{0}", match.PlayerTurn.PlayerName), new Vector2(17 + this.font.MeasureString("Turn  ").X, 120), match.PlayerTurn.ColorSide);
            spriteBatch.DrawString(this.font, string.Format("Viewfinder {0},{1}", viseur.viseurX, viseur.viseurY), new Vector2(17, 160), Color.Black);

            spriteBatch.DrawString(this.font, string.Format("Unit : "), new Vector2(17, 200), Color.Black);

            if (map[viseur.viseurX, viseur.viseurY].unitOfCell != null)
            {
                spriteBatch.DrawString(this.font, string.Format("{0}", map[viseur.viseurX, viseur.viseurY].unitOfCell.Classe), new Vector2(17 + this.font.MeasureString("Unit :  ").X, 200), Color.Black);
                unit.LC(Game1.Ctt, "Unit/" + map[viseur.viseurX, viseur.viseurY].unitOfCell.Player.ColorSideN + map[viseur.viseurX, viseur.viseurY].unitOfCell.Rang);
                unit.Draw(data, spriteBatch, new Vector2(this.font.MeasureString("Unit :  " + map[viseur.viseurX, viseur.viseurY].unitOfCell.Classe + "    ").X, 185), 1.2f);
                spriteBatch.DrawString(this.font, string.Format("PV : "), new Vector2(17 + this.font.MeasureString("Unit :").X - this.font.MeasureString("PV :").X, 225), Color.Black);
                spriteBatch.DrawString(this.font2, string.Format("{0}", map[viseur.viseurX, viseur.viseurY].unitOfCell.PV), new Vector2(17 + this.font.MeasureString("Unit :").X - this.font.MeasureString("PV :").X + this.font.MeasureString("PV :  ").X, 225), Color.Red);
                spriteBatch.DrawString(this.font, string.Format("Strength : "), new Vector2(35, 245), Color.Black);
                spriteBatch.DrawString(this.font2, string.Format("{0}", map[viseur.viseurX, viseur.viseurY].unitOfCell.Strength), new Vector2(35 + this.font.MeasureString("Strength :  ").X, 245), Color.Red);
            }

            string field = "";
            Color color = Color.Green;
            switch (data.altitudeTerrain[viseur.viseurX, viseur.viseurY])
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
            spriteBatch.DrawString(this.font, string.Format("Field : "), new Vector2(17, 300), Color.Black);
            spriteBatch.DrawString(this.font, string.Format("{0}", field), new Vector2(17 + this.font.MeasureString("Field :  ").X, 300), color);
            spriteBatch.End();
        }
    }
}
