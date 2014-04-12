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
        #region VARIABLES

        private Variable var = Game1.var;
        
        private ContentManager ctt = Game1.Ctt;
        KeyboardState oldKeyboardState, currentKeyboardState;
        GameTime gameTime;
        TimeSpan time;

        private Sprite spviseur;
        private Sprite spCaserouge, spCasebleu, Viseurbleu, Viseurrouge;
        private Texture2D Viseurnormal;
        //private int x = 0, y = 0;
        private Cell[,] map;
        private Unit viseur;
        //private bool overunit;
        private bool blinkviseur;
        private Vector2 coord;

        public Unit UnitInCell { get; set; }
        //public bool ViseurOverUnit { get { return overunit; } }
        public bool ViseurOverUnit { get { return map[viseurX, viseurY].Occupe; } }

        public int viseurX { get { return (int)coord.X; } }
        public int viseurY { get { return (int)coord.Y; } }
        public Vector2 coordViseur { get { return coord; } set { coord = value; } }

        public bool departureSelected { get; set; }
        public Vector2 departurePosition { get; set; }

        public bool destinationSelected { get; set; }
        public Vector2 destinationPosition { get; set; }

        public Sprite spriteViseur { get { return spviseur; } }
        //private Unit this.unit;
        //public Unit unit { get { return this.unit; } set { this.unit = value; } }



        #endregion

        // // // // // // // // 

        #region CONSTRUCTEURS

        public Viseur() { }

        public Viseur(Cell[,] map, GameTime gameTime)
        {
            this.map = map;
            this.gameTime = gameTime;

            viseur = new Unit();
            Init();
            Load();
            //OverUnit();
            ViseurColor();
            Reset();

        }

        #endregion

        // // // // // // // // 

        #region INIT & LOAD

        void Init()
        {
            //viseur = new Unit("viseur", "viseur", this.map, 0, 0);
            viseur.XofUnit = 0;
            viseur.YofUnit = 0;

            spviseur = new Sprite(); spviseur.Initialize();
            spCaserouge = new Sprite(); spCaserouge.Initialize();
            spCasebleu = new Sprite(); spCasebleu.Initialize();
            Viseurbleu = new Sprite(); Viseurbleu.Initialize();
            Viseurrouge = new Sprite(); Viseurrouge.Initialize();
        }

        void Load()
        {
            spviseur.LoadContent(ctt, "Curseur/viseur");
            spCaserouge.LoadContent(ctt, "Case/rouge");
            spCasebleu.LoadContent(ctt, "Case/bleu");
            Viseurbleu.LoadContent(ctt, "Curseur/viseurB");
            Viseurrouge.LoadContent(ctt, "Curseur/viseurR");
            //Viseurnormal = ctt.Load<Texture2D>("Curseur/viseur");
            //Viseurbleu = ctt.Load<Texture2D>("Curseur/viseurB");
            //Viseurrouge = ctt.Load<Texture2D>("Curseur/viseurR");
        }

        #endregion

        // // // // // // // // 

        #region FONCTIONS DU VISEUR

        public void OverUnit()
        {
            //this.overunit = this.map[(int)this.coordViseur.X, (int)this.coordViseur.Y].Occupe;
            if (map[viseurX, viseurY].Occupe) UnitInCell = map[viseurX, viseurY].unitOfCell;
        }

        void Reset()
        {
            departureSelected = false; departurePosition = Vector2.Zero;
            destinationSelected = false; departurePosition = Vector2.Zero;
            //spviseur.Texture = Viseurbleu;
            if (ViseurOverUnit && map[viseurX, viseurY].Occupe) map[viseurX, viseurY].unitOfCell = UnitInCell;
        }

        void doMoveUnit(Unit oldUnit, Cell newUnit)
        {
            oldUnit.XofUnit = newUnit.XofCell;
            oldUnit.YofUnit = newUnit.YofCell;
            //newUnit.unitOfCell = new Unit(oldUnit);
            //oldUnit.UnitofUnit = newUnit.unitOfCell;
        }

        void getMovingPath()
        {
            currentKeyboardState = Keyboard.GetState();



            if (map[viseurX, viseurY].unitOfCell == null && departureSelected && coordViseur != departurePosition && currentKeyboardState.IsKeyDown(Keys.Enter))
            {
                destinationSelected = true;
                destinationPosition = new Vector2(coordViseur.X, coordViseur.Y);
                doMoveUnit(map[(int)departurePosition.X, (int)departurePosition.Y].unitOfCell, map[(int)destinationPosition.X, (int)destinationPosition.Y]);
                //Reset();
            }
            else if (ViseurOverUnit && departureSelected == false && currentKeyboardState.IsKeyDown(Keys.Enter))
            {
                departureSelected = true;
                departurePosition = new Vector2(coordViseur.X, coordViseur.Y);
            }
        }

        void ViseurColor()
        {
            switch (map[viseurX, viseurY].Occupe)
            {
                case true:
                    spviseur = Viseurrouge;
                    if (!departureSelected)
                        spviseur = Viseurbleu;
                    break;

                case false:
                    if (!departureSelected)
                        spviseur = Viseurbleu;
                    break;
            }
        }

        void BlinkSprite(GameTime gameTime)
        {
            if (gameTime.TotalGameTime - time > TimeSpan.FromSeconds(0.1f))
            {
                blinkviseur = !blinkviseur;
                time = gameTime.TotalGameTime;
            }
        }

        #endregion

        // // // // // // // // 

        #region UPDATE

        public void Update(GameTime gameTime)
        {
            currentKeyboardState = Keyboard.GetState();
            float tempo;
            getMovingPath();
            
            /*if (_map[_viseur.X, _viseur.Y].unit != null)
                overunit = true;*/

            #region Gestion des mouvements du viseurs

            if (currentKeyboardState.IsKeyDown(Keys.LeftShift)) tempo = 0.08f; else tempo = 0.15f;

            if (gameTime.TotalGameTime - time > TimeSpan.FromSeconds(tempo) || currentKeyboardState != oldKeyboardState)
            {
                time = gameTime.TotalGameTime;

                if (coordViseur.X == 0 && currentKeyboardState.IsKeyDown(Keys.Left))
                    coord.X = var.WidthMap - 1;
                else
                    if (currentKeyboardState.IsKeyDown(Keys.Left))
                        --coord.X;

                if (coordViseur.X == var.WidthMap - 1 && currentKeyboardState.IsKeyDown(Keys.Right))
                    coord.X = 0;
                else
                    if (currentKeyboardState.IsKeyDown(Keys.Right))
                        ++coord.X;

                if (coordViseur.Y == 0 && currentKeyboardState.IsKeyDown(Keys.Up))
                    coord.Y = var.HeightMap - 1;
                else
                    if (currentKeyboardState.IsKeyDown(Keys.Up))
                        --coord.Y;

                if (coordViseur.Y == var.HeightMap - 1 && currentKeyboardState.IsKeyDown(Keys.Down))
                    coord.Y = 0;
                else
                    if (currentKeyboardState.IsKeyDown(Keys.Down))
                        ++coord.Y;
            }
            #endregion

            /*if (currentKeyboardState.IsKeyDown(Keys.Delete))
                Reset();*/
            /*else
            {
                if (!pointA && !pointB)
                {
                    getDeparture(gameTime);
                }
                if (pointA && !pointB)
                {
                    DeplacementB(gameTime);
                }

            }*/

            oldKeyboardState = currentKeyboardState;
        }

        #endregion

        // // // // // // // // 

        #region DRAW

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            //BlinkSprite(gameTime);
            spviseur.Draw(spriteBatch, gameTime, map[viseurX, viseurY].positionPixel);
            /*if (overunit && !pointA && !pointB)
                _viseura.Draw(spriteBatch, gameTime, _map[_viseur.X, _viseur.Y].positionPixel);
            else
                if (overunit && pointA && !pointB)
                    _viseurb.Draw(spriteBatch, gameTime, _map[_viseur.X, _viseur.Y].positionPixel);
                else
                    if (!overunit)
                    {
                        _viseur.Draw(spriteBatch, gameTime);
                        overunit = false;
                    }
            */
        }

        #endregion
    }
}
