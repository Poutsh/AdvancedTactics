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
    public class Viseur : Game
    {
        #region VARIABLES

        private Variable var = Game1.var;

        private ContentManager ctt = Game1.Ctt;
        KeyboardState oldKeyboardState, currentKeyboardState;
        GameTime gameTime;
        TimeSpan time;

        private Sprite spviseur, sblinkviseur;
        private Sprite spCaserouge, spCasebleu, Viseurbleu, Viseurrouge, Viseurnormal;
        private Cell[,] map;
        private Unit viseur;
        public bool blinkviseur;
        private Vector2 coord;

        public Unit UnitInCell { get; set; }
        public bool ViseurOverUnit { get { return map[viseurX, viseurY].Occupe; } }

        public int viseurX { get { return (int)coord.X; } }
        public int viseurY { get { return (int)coord.Y; } }
        public Vector2 coordViseur { get { return coord; } set { coord = value; } }

        public bool departureSelected { get; set; }
        public Vector2 departurePosition { get; set; }
        bool inMoving = false;
        public bool destinationSelected { get; set; }
        public Vector2 destinationPosition { get; set; }

        public Sprite spriteViseur { get { return spviseur; } }



        #endregion

        // // // // // // // // 

        #region CONSTRUCTEURS

        public Viseur() { }

        public Viseur(Cell[,] map)
        {
            this.map = map;
            //this.gameTime = gameTime;

            viseur = new Unit();
            Init();
            Load();
        }

        #endregion

        // // // // // // // // 

        #region INIT & LOAD

        void Init()
        {
            viseur.XofUnit = 0;
            viseur.YofUnit = 0;

            spviseur = new Sprite(); spviseur.Initialize();
            sblinkviseur = new Sprite(); sblinkviseur.Initialize();
            spCaserouge = new Sprite(); spCaserouge.Initialize();
            spCasebleu = new Sprite(); spCasebleu.Initialize();
            Viseurbleu = new Sprite(); Viseurbleu.Initialize();
            Viseurrouge = new Sprite(); Viseurrouge.Initialize();
            Viseurnormal = new Sprite(); Viseurnormal.Initialize();

            //blinkviseur = false;
        }

        void Load()
        {
            spviseur.LoadContent(ctt, "Curseur/viseur");
            sblinkviseur.LoadContent(ctt, "Curseur/viseurS");
            spCaserouge.LoadContent(ctt, "Case/rouge");
            spCasebleu.LoadContent(ctt, "Case/bleu");
            Viseurbleu.LoadContent(ctt, "Curseur/viseurB");
            Viseurrouge.LoadContent(ctt, "Curseur/viseurR");
            Viseurnormal.LoadContent(ctt, "Curseur/viseur");
            //Viseurbleu = ctt.Load<Texture2D>("Curseur/viseurB");
            //Viseurrouge = ctt.Load<Texture2D>("Curseur/viseurR");
        }

        #endregion

        // // // // // // // // 

        #region FONCTIONS DU VISEUR

        public void OverUnit()
        {
            if (map[viseurX, viseurY].Occupe) UnitInCell = map[viseurX, viseurY].unitOfCell;
        }

        void Reset()
        {
            departureSelected = false; departurePosition = Vector2.Zero;
            destinationSelected = false; departurePosition = Vector2.Zero;

            //if (ViseurOverUnit && map[viseurX, viseurY].Occupe) map[viseurX, viseurY].unitOfCell = UnitInCell;
        }

        void doMoveUnit(Unit oldUnit, Cell newUnit, List<Unit> ListOfUnit)
        {
            Unit temp;
            temp = new Unit(oldUnit.Rang, oldUnit.Classe, map, newUnit.XofCell, newUnit.YofCell, ListOfUnit, oldUnit, true);
            Reset();
        }

        void getMovingPath(List<Unit> ListOfUnit, GameTime gameTime, bool inMoving)
        {
            currentKeyboardState = Keyboard.GetState();

            if (map[viseurX, viseurY].unitOfCell == null && departureSelected && coordViseur != departurePosition && currentKeyboardState.IsKeyDown(Keys.W))
            {
                destinationSelected = true;
                destinationPosition = new Vector2(coordViseur.X, coordViseur.Y);
                doMoveUnit(map[(int)departurePosition.X, (int)departurePosition.Y].unitOfCell, map[(int)destinationPosition.X, (int)destinationPosition.Y], ListOfUnit);
                inMoving = false;
            }
            else if (ViseurOverUnit && departureSelected == false && currentKeyboardState.IsKeyDown(Keys.Q)/* && gameTime.TotalGameTime - time > TimeSpan.FromSeconds(0.10f)*/)
            {
                //time = gameTime.TotalGameTime;
                departureSelected = true;
                inMoving = true;
                departurePosition = new Vector2(coordViseur.X, coordViseur.Y);
            }
        }

        void ViseurColor()
        {
            if (!map[viseurX, viseurY].Occupe && !departureSelected && !destinationSelected)
                spviseur = Viseurnormal;
            else if (map[viseurX, viseurY].Occupe && departureSelected && !destinationSelected)
                spviseur = Viseurrouge;
            else if (departureSelected || (map[viseurX, viseurY].Occupe && !departureSelected))
                spviseur = Viseurbleu;
        }

        void BlinkSprite(GameTime gameTime, bool blinkviseur, SpriteBatch spriteBatch)
        {
            if (departureSelected && !destinationSelected)
            {
                if (map[viseurX, viseurY].VectorOfCell == departurePosition)
                    sblinkviseur.Position = map[viseurX, viseurY].positionPixel;
                blinkviseur = departureSelected;
            }

            sblinkviseur.Draw(spriteBatch, gameTime, sblinkviseur.Position, blinkviseur);
        }

        #endregion

        // // // // // // // // 

        #region UPDATE

        public void Update(GameTime gameTime, List<Unit> ListOfUnit)
        {
            currentKeyboardState = Keyboard.GetState();
            float tempo;
            getMovingPath(ListOfUnit, gameTime, inMoving);
            ViseurColor();

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

            oldKeyboardState = currentKeyboardState;
        }

        #endregion

        // // // // // // // // 

        #region DRAW

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            BlinkSprite(gameTime, blinkviseur, spriteBatch);
            spviseur.Draw(spriteBatch, gameTime, map[viseurX, viseurY].positionPixel);
            
        }

        #endregion
    }
}
