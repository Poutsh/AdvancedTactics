using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Advanced_Tactics
{
    public class Unit
    {
        #region VARIABLES

        Data data;
        private Cell[,] map;
        public Sprite spriteOfUnit;
        string path;

        public Viseur Viseur { get; set; }

        public int XofUnit { get; set; }
        public int YofUnit { get; set; }

        public string Classe { get; set; }
        public string Rang { get; set; }
        public List<int> Mvt { get; set; }

        List<int> Mvt1 = new List<int>(1) { 1 };
        List<int> Mvt2 = new List<int>(2) { 1, 2 };
        List<int> Mvt3 = new List<int>(3) { 0, 1, 2 };
        List<int> Mvt4 = new List<int>(0) { };

        #endregion

        // // // // // // // // 

        #region CONSTRUCTEURS

        Action<string, string, ContentManager, Sprite> Sprite2Unit = (p, r, c, s) => s.LC(c, p + r);

        public Unit() { XofUnit = 0; YofUnit = 0; Mvt = new List<int>(0); }

        //rang = hq, pvt, tank...
        //clasee = dame, roi , fou...

        /// <summary>
        /// Creation d'unite
        /// </summary>
        /// <param name="Rang">Rang de l'unite (HQ, PVT, TANK...)</param>
        /// <param name="Classe">Classe de l'unite (ROI, DAME, FOU, TOUR...)</param>
        /// <param name="Map">Map</param>
        /// <param name="X">Position X de la nouvelle unite</param>
        /// <param name="Y">Position Y de la nouvelle unite</param>
        /// <param name="ListOfUnit">List qui contient toute les unitee a DRAW</param>
        public Unit(Data data, string Rang, string Classe, Cell[,] Map, int X, int Y, List<Unit> ListOfUnit)
        {
            this.data = data;
            if (Map[X, Y].unitOfCell == null)
            {
                spriteOfUnit = new Sprite();

                this.Rang = Rang;
                this.Classe = Classe;
                this.XofUnit = X;
                this.YofUnit = Y;
                map = Map;

                if (new List<string>(2) { "tank", "truck" }.Contains(Rang))
                {
                    Mvt = new List<int>(2);
                    Mvt.Add(1); Mvt.Add(2);
                }
                if (new List<string>(6) { "aa", "com", "doc", "ing", "pvt", "hq" }.Contains(Rang))
                {
                    Mvt = new List<int>(1);
                    Mvt.Add(1);
                }
                if (new List<string>(1) { "plane" }.Contains(Rang))
                {
                    Mvt = new List<int>(3);
                    Mvt.Add(1); Mvt.Add(2); Mvt.Add(0);
                }

                if (Mvt.Contains(data.altitudeTerrain[X, Y]))
                {
                    if (Rang == "viseur") path = "Curseur/"; else path = "Unit/";
                    Sprite2Unit(path, Rang, data.Content, spriteOfUnit);

                    if (Rang != null && Classe != null)
                        ListOfUnit.Add(this);

                    map[XofUnit, YofUnit].unitOfCell = this;

                    map[this.XofUnit, this.YofUnit].Occupe = ListOfUnit.Contains(this);
                }
            }
        }

        /// <summary>
        /// Deplacement d'unitee
        /// </summary>
        /// <param name="UnitToMove">Unite que l'on veut faire bouger</param>
        /// <param name="Map">Map sur laquelle est pose l'unite</param>
        /// <param name="newCell">Case ou l'on veut deplacer l'unite</param>
        /// <param name="ListOfUnit">List qui contient toute les unitee a DRAW</param>
        public Unit(Data data, Unit UnitToMove, Cell[,] Map, Cell newCell, List<Unit> ListOfUnit)   // Constructeur de DESTRUCTION ahahahahahahahahahahahah
        {
            this.data = data;
            spriteOfUnit = new Sprite();

            this.Rang = UnitToMove.Rang;
            this.Classe = UnitToMove.Classe;
            this.XofUnit = newCell.XofCell;
            this.YofUnit = newCell.YofCell;
            map = Map;

            if (new List<string>(2) { "tank", "truck" }.Contains(Rang))
            {
                Mvt = new List<int>(2);
                Mvt.Add(1); Mvt.Add(2);
            }
            if (new List<string>(6) { "aa", "com", "doc", "ing", "pvt", "hq" }.Contains(Rang))
            {
                Mvt = new List<int>(1);
                Mvt.Add(1);
            }
            if (new List<string>(1) { "plane" }.Contains(Rang))
            {
                Mvt = new List<int>(3);
                Mvt.Add(0); Mvt.Add(1); Mvt.Add(2);
            }

            if (Mvt.Contains(data.altitudeTerrain[XofUnit, YofUnit]))
            {
                if (this.Rang == "viseur") path = "Curseur/"; else path = "Unit/";
                Sprite2Unit(path, this.Rang, data.Content, spriteOfUnit);

                ListOfUnit.Add(this);

                map[UnitToMove.XofUnit, UnitToMove.YofUnit].unitOfCell = null; // On mets a null la valeur de lunite dans lancienne case
                map[newCell.XofCell, newCell.YofCell].unitOfCell = this; // On mets a jour la valeur de la nouvelle case

                map[this.XofUnit, this.YofUnit].Occupe = ListOfUnit.Contains(this);

                for (int i = 0; i < ListOfUnit.Count(); i++) // On cherche lancienne unite et on la supprime de la liste des unitees a draw,
                    if (ListOfUnit[i] == UnitToMove)
                    {
                        ListOfUnit.RemoveAt(i);
                        map[UnitToMove.XofUnit, UnitToMove.YofUnit].Occupe = false;
                    }
            }
        }

        #endregion

        // // // // // // // // 

        #region DRAW

        public void DrawUnit(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteOfUnit.Draw(data, spriteBatch, gameTime, map[XofUnit, YofUnit].positionPixel);
        }
        #endregion
    }
}


