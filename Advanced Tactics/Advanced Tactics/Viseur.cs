using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdvancedLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Advanced_Tactics
{
    public class Viseur
    {
        private Variable var = Game1.var;
        private ContentManager ctt = Game1.Ctt;
        KeyboardState oldKeyboardState, currentKeyboardState;
        TimeSpan time;

        private Sprite _sviseur;
        private Sprite _stest;
        private int _x = 0, _y = 0;
        private Cell[,] _map;
        private Unit _viseur;
        private bool overunit = false;


        public Sprite spriteViseur { get { return _sviseur; } }

        public Viseur(Cell[,] map)
        {
            _map = map;

            _sviseur = new Sprite(); _sviseur.Initialize();
            _stest = new Sprite(); _stest.Initialize();
            _viseur = new Unit("viseur", "viseur", _map, 0, 0);

            _stest.LoadContent(ctt, "Case/bleu");
        }

        public void Update(GameTime gameTime)
        {
            currentKeyboardState = Keyboard.GetState();

            if (_map[_viseur.X, _viseur.Y].Occupe == true )
            {
                overunit = true;
            }

            if (gameTime.TotalGameTime - time > TimeSpan.FromSeconds(0.15f) || currentKeyboardState != oldKeyboardState)
            {
                time = gameTime.TotalGameTime;

                if (_viseur.X == 0 && currentKeyboardState.IsKeyDown(Keys.Left))
                    _viseur.X = var.WidthMap - 1;
                else
                    if (currentKeyboardState.IsKeyDown(Keys.Left))
                        --_viseur.X;

                if (_viseur.X == var.WidthMap - 1 && currentKeyboardState.IsKeyDown(Keys.Right))
                    _viseur.X = 0;
                else
                    if (currentKeyboardState.IsKeyDown(Keys.Right))
                        ++_viseur.X;

                if (_viseur.Y == 0 && currentKeyboardState.IsKeyDown(Keys.Up))
                    _viseur.Y = var.HeightMap - 1;
                else
                    if (currentKeyboardState.IsKeyDown(Keys.Up))
                        --_viseur.Y;

                if (_viseur.Y == var.HeightMap - 1 && currentKeyboardState.IsKeyDown(Keys.Down))
                    _viseur.Y = 0;
                else
                    if (currentKeyboardState.IsKeyDown(Keys.Down))
                        ++_viseur.Y;
            }

            oldKeyboardState = currentKeyboardState;
        }

        public void mvtViseur(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _viseur.Draw(spriteBatch, gameTime);

            if (overunit)
            {
                _stest.Draw(spriteBatch, gameTime, _map[_viseur.X+1, 1+_viseur.Y].positionPixel);
                overunit = false;
            }
        }
    }
}
