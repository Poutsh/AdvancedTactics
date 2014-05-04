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
        }

        public virtual void Update(GameTime gameTime)
        {
            mouseStateCurrent = Mouse.GetState();
        }//61 164

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.DrawString(this.font, string.Format("Viewfinder {0},{1}", _viseur.viseurX, _viseur.viseurY), new Vector2(60, 140), Color.Black);

            spriteBatch.DrawString(this.font, string.Format("Unit : "), new Vector2(60, 200), Color.Black);
            if (cartemap.Carte[_viseur.viseurX, _viseur.viseurY].Occupe)
            {
                spriteBatch.DrawString(this.font, string.Format("{0}", cartemap.Carte[_viseur.viseurX, _viseur.viseurY].unitOfCell.Classe), new Vector2(138, 200), Color.Black);
                unit.LC(data.Content, "Unit/" + cartemap.Carte[_viseur.viseurX, _viseur.viseurY].unitOfCell.Rang);
                unit.Draw(data, spriteBatch, gameTime, new Vector2(220, 185), 1.2f);
                spriteBatch.DrawString(this.font, string.Format("PV :"), new Vector2(60, 230), Color.Black);
                spriteBatch.DrawString(this.font2, string.Format("{0}", cartemap.Carte[_viseur.viseurX, _viseur.viseurY].unitOfCell.PV), new Vector2(120, 223), Color.Red);
                spriteBatch.DrawString(this.font, string.Format("Strength :"), new Vector2(60, 250), Color.Black);
                spriteBatch.DrawString(this.font2, string.Format("{0}", cartemap.Carte[_viseur.viseurX, _viseur.viseurY].unitOfCell.Strength), new Vector2(210, 243), Color.Red);
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
            spriteBatch.DrawString(this.font, string.Format("Field : "), new Vector2(60, 320), Color.Black);
            spriteBatch.DrawString(this.font, string.Format("{0}", field), new Vector2(150, 320), color);
        }
    }
}
