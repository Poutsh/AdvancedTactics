﻿using System;
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
    /// Class pour afficher des valeurs textes pour aider aux public
    /// </summary>
    class Debug
    {
        SpriteFont font;
        MouseState mouseStateCurrent;
        KeyboardState currentKeyboardState;
        Constante cst;
        Sprite _r, _b;

        ContentManager ctt;
        List<Unit> _ListOfUnit;
        Map cartemap;
        //RandomSprite rand;
        private Viseur _viseur;

        Menu _menu = new Menu();

        private int h, w;
        List<string> deg = new List<string>();
        List<int> MvtPossible;
        
        public Debug(ContentManager content, Constante variable, Map map, int BufferHeight, int BufferWitdh, Viseur viseur, List<Unit> ListOfUnit, List<int> MvtPossible)
        {
            cst = variable;
            cartemap = map;
            ctt = content;
            _viseur = viseur;
            h = BufferHeight; w = BufferWitdh;
            _ListOfUnit = ListOfUnit;

            _r = new Sprite();
            _b = new Sprite();
            this.MvtPossible = MvtPossible;
        }

        public virtual void LoadContent()
        {
            font = ctt.Load<SpriteFont>("font");
            _r.LC(ctt, "Case/rouge");
            _b.LC(ctt, "Case/bleu");
        }

        public virtual void Update(GameTime gameTime)
        {
            mouseStateCurrent = Mouse.GetState();
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.DrawString(this.font,
                string.Format("Menu  {0}, {1}, {2}, {3}", _menu.currentGame, _menu.MenuPrincipal, _menu.nothing, _menu.Options),
                new Vector2(10, 80), Color.Blue);

            spriteBatch.DrawString(this.font,
                string.Format("Depart         {0}", _viseur.depPos),
                new Vector2(10, 96), Color.Black);

            spriteBatch.DrawString(this.font,
                string.Format("Destination    {0}", _viseur.destPos),
                new Vector2(10, 112), Color.Black);

            spriteBatch.DrawString(this.font,
                string.Format("bool point A   {0}", _viseur.depSelec),
                new Vector2(10, 126), Color.Black);

            spriteBatch.DrawString(this.font,
                string.Format("bool point B   {0}", _viseur.destSelec),
                new Vector2(10, 139), Color.Black);

            spriteBatch.DrawString(this.font,
                "HeightMap : " + Convert.ToString(cst.HeightMap),
                new Vector2(10, 400), Color.Black);

            spriteBatch.DrawString(this.font,
                "WidthMap : " + Convert.ToString(cst.WidthMap),
                new Vector2(10, 415), Color.Black);

            spriteBatch.DrawString(this.font,
                "Scale : " + Convert.ToString(cst.Scale),
                new Vector2(10, 430), Color.Black);

            spriteBatch.DrawString(this.font,
                "Tile size : " + Convert.ToString(cst.TileSize),
                new Vector2(10, 445), Color.Black);

            spriteBatch.DrawString(this.font,
                "Pos init : " + Convert.ToString(cst.PosXInit),
                new Vector2(10, 460), Color.Black);

            spriteBatch.DrawString(this.font,
                string.Format("Mouse ({0}, {1})", Mouse.GetState().X, Mouse.GetState().Y),
                new Vector2(10, 475), Color.Black);
            ////////////

            spriteBatch.DrawString(this.font,
                    string.Format("List {0}", _ListOfUnit.Count()),
                    new Vector2(10, 510), Color.Black);

            
            ///////////
            //Viseur

            spriteBatch.DrawString(this.font, string.Format("Coord. Viseur > {0},{1}", _viseur.viseurX, _viseur.viseurY), new Vector2(20, 175), Color.Indigo);


            //spriteBatch.DrawString(this.font, string.Format("Coord. Viseur > {0},{1}", cartemap.Carte[1,1].unitOfCell.ListUnit[0]), new Vector2(20, 175), Color.Indigo);


            if (cartemap.Carte[_viseur.viseurX, _viseur.viseurY].Occupe)
            {
                spriteBatch.DrawString(this.font,
                    string.Format("Unit : {0}, {1}", cartemap.Carte[_viseur.viseurX, _viseur.viseurY].unitOfCell.Classe, cartemap.Carte[_viseur.viseurX, _viseur.viseurY].unitOfCell.Rang),
                    new Vector2(20, 195), Color.Indigo);
            }
            else
            {
                spriteBatch.DrawString(this.font,
                    string.Format("Unit : {0}", "vide"),
                    new Vector2(20, 195), Color.Indigo);
            }


            spriteBatch.DrawString(this.font,
                 string.Format("OverUnit public  {0}", _viseur.ViseurOverUnit),
                 new Vector2(20, 258), Color.Yellow);
            spriteBatch.DrawString(this.font,
                 string.Format("Altitude {0}", cst.altitudeTerrain[_viseur.viseurX, _viseur.viseurY]),
                 new Vector2(20, 278), Color.Yellow);

            for (int i = 0; i < MvtPossible.Count(); i++)
            {
                spriteBatch.DrawString(this.font,
                 string.Format("Altitude {0}", MvtPossible[i]),
                 new Vector2(20, 298+i*20), Color.Yellow);
            }
        }
    }
}