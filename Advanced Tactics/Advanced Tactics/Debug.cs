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
    /// <summary>
    /// Class pour afficher des valeurs textes pour aider aux public
    /// </summary>
    class Debug
    {
        SpriteFont font;
        MouseState mouseStateCurrent;
        Data data;

        Sprite _r, _b;
        ContentManager ctt;
        List<Unit> _ListOfUnit;
        Map cartemap;
        private Viseur _viseur;

        Match Match;

        Menu _menu = new Menu();

        private int h, w;
        List<string> deg = new List<string>();

        public Debug()
        {
            ctt = Game1.Ctt;
            _r = new Sprite();
            _b = new Sprite();
        }

        public void LoadContent()
        {
            font = ctt.Load<SpriteFont>("font");
            _r.LC(ctt, "Case/rouge");
            _b.LC(ctt, "Case/bleu");
        }

        public void Update(Data data, Map map, Viseur viseur, List<Unit> ListOfUnit, Match Match)
        {
            this.data = data;
            this.Match = Match;
            cartemap = map;
            _viseur = viseur;
            _ListOfUnit = ListOfUnit;
            mouseStateCurrent = Mouse.GetState();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(this.font,
                string.Format("Menu  {0}, {1}, {2}, {3}", _menu.InGame, _menu.MenuPrincipal, _menu.Loadscreen, _menu.Options),
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
                "HeightMap : " + Convert.ToString(data.MapHeight),
                new Vector2(10, 400), Color.Black);

            spriteBatch.DrawString(this.font,
                "WidthMap : " + Convert.ToString(data.MapWidth),
                new Vector2(10, 415), Color.Black);

            spriteBatch.DrawString(this.font,
                "Scale : " + Convert.ToString(data.Scale),
                new Vector2(10, 430), Color.Black);

            spriteBatch.DrawString(this.font,
                "Tile size : " + Convert.ToString(data.TileSize),
                new Vector2(10, 445), Color.Black);

            spriteBatch.DrawString(this.font,
                "Pos init : " + Convert.ToString(data.PosXInit),
                new Vector2(10, 460), Color.Black);

            spriteBatch.DrawString(this.font,
                string.Format("Mouse ({0}, {1})", Mouse.GetState().X, Mouse.GetState().Y),
                new Vector2(10, 475), Color.Black);
            ////////////

            spriteBatch.DrawString(this.font,
                    string.Format("Units Player1 {0}", Match.Players[0].UnitOfPlayer.Count()),
                    new Vector2(10, 510), Match.Players[0].ColorSide);
            spriteBatch.DrawString(this.font,
                    string.Format("Units Player2 {0}", Match.Players[1].UnitOfPlayer.Count()),
                    new Vector2(10, 535), Match.Players[1].ColorSide);

            spriteBatch.DrawString(this.font,
                    string.Format("Match {0}", Match.TurnState),
                    new Vector2(10, 600), Color.Black);

            spriteBatch.DrawString(this.font,
                    string.Format("Match {0}", Match.canStart),
                    new Vector2(10, 625), Color.Black);

            spriteBatch.DrawString(this.font,
                    string.Format("Match {0}", Match.inTurnState),
                    new Vector2(10, 650), Color.Black);

            spriteBatch.DrawString(this.font,
                    string.Format("GameState {0}", Game1.currentGameState),
                    new Vector2(10, 675), Color.Orchid);

            spriteBatch.DrawString(this.font,
                    string.Format("ViseurState {0}", _viseur.currentViseurState),
                    new Vector2(10, 700), Color.Orchid);


            ///////////
            //Viseur

            spriteBatch.DrawString(this.font, string.Format("Coord. Viseur > {0},{1}", _viseur.viseurX, _viseur.viseurY), new Vector2(20, 175), Color.Indigo);


            //spriteBatch.DrawString(this.font, string.Format("Coord. Viseur > {0},{1}", cartemap.Carte[1,1].unitOfCell.ListUnit[0]), new Vector2(20, 175), Color.Indigo);


            if (cartemap.Carte[_viseur.viseurX, _viseur.viseurY].Occupe)
            {
                spriteBatch.DrawString(this.font,
                    string.Format("Unit : {0}, {1}", cartemap.Carte[_viseur.viseurX, _viseur.viseurY].unitOfCell.Classe, cartemap.Carte[_viseur.viseurX, _viseur.viseurY].unitOfCell.Rang),
                    new Vector2(20, 195), Color.Indigo);
                spriteBatch.DrawString(this.font,
                    string.Format("Unit : {0}", cartemap.Carte[_viseur.viseurX, _viseur.viseurY].unitOfCell.Strength),
                    new Vector2(20, 215), Color.Indigo);
                for (int i = 0; i < cartemap.Carte[_viseur.viseurX, _viseur.viseurY].unitOfCell.MvtPossible.Count; i++)
                {
                    spriteBatch.DrawString(this.font,
                    string.Format("Unit : {0}", cartemap.Carte[_viseur.viseurX, _viseur.viseurY].unitOfCell.MvtPossible[i]),
                    new Vector2(20, 240 + i * 10), Color.Pink);
                }

                if (cartemap.Carte[_viseur.viseurX, _viseur.viseurY].unitOfCell.Player != null)
                {
                    spriteBatch.DrawString(this.font,
                        string.Format("Match {0}", cartemap.Carte[_viseur.viseurX, _viseur.viseurY].unitOfCell.Player.PlayerName),
                        new Vector2(10, 675), Color.Black);
                }


            }
            else
            {
                spriteBatch.DrawString(this.font,
                    string.Format("Unit : {0}", "vide"),
                    new Vector2(20, 195), Color.Indigo);
            }

            //spriteBatch.DrawString(this.font,
            //     string.Format("OverUnit public  {0}", _viseur.ViseurOverUnit),
            //     new Vector2(20, 258), Color.Yellow);
            //spriteBatch.DrawString(this.font,
            //     string.Format("Altitude sous le viseur {0}", data.altitudeTerrain[_viseur.viseurX, _viseur.viseurY]),
            //     new Vector2(20, 278), Color.Yellow);

            if (_viseur.UnitTemp != null)
            {
                for (int i = 0; i < _viseur.UnitTemp.TerrainPossible.Count(); i++)
                {

                    spriteBatch.DrawString(this.font,
                     string.Join(",", _viseur.UnitTemp.TerrainPossible.ToArray()),
                     new Vector2(20, 298), Color.Brown);
                }
            }
        }
    }
}