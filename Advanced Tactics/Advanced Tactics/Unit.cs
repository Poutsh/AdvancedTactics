using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Advanced_Tactics;
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

        //
        public Stats Stats { get; set; }
        public string Classe { get; set; }
        public string Rang { get; set; }
        public int PV { get; set; }
        public int Strength { get; set; }
        public List<int> TerrainPossible { get; set; }
        public List<Vector> MvtPossible { get; set; }
        public Player Player { get; set; }

        public Viseur Viseur { get; set; }

        public int XofUnit { get; set; }
        public int YofUnit { get; set; }


        #endregion

        // // // // // // // // 

        #region CONSTRUCTEURS

        Action<string, string, ContentManager, Sprite> Sprite2Unit = (p, r, c, s) => s.LC(c, p + r);
        Func<string, string> ColorSide = c => new StringBuilder(c).Remove(0, 1).ToString();

        public Unit() { XofUnit = 0; YofUnit = 0; TerrainPossible = new List<int>(0); }

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
        public Unit(Data data, string Rang, string Classe, Cell[,] Map, int X, int Y, List<Unit> ListOfUnit, Player Player)
        {
            this.data = data;

            Stats = new Stats();

            if (Map[X, Y].unitOfCell == null)
            {
                spriteOfUnit = new Sprite();

                this.XofUnit = X;
                this.YofUnit = Y;
                this.Player = Player;
                this.Rang = Rang;
                this.Classe = Classe;
                this.PV = Stats.PVUnit(ColorSide(Rang));
                this.Strength = Stats.StrengthUnit(ColorSide(Rang));
                this.TerrainPossible = Stats.TerrainPossibleUnit(ColorSide(Rang));
                this.MvtPossible = Stats.MvtPossUnit(Classe, new Vector(this.XofUnit, this.YofUnit), map, data);

                map = Map;

                if (TerrainPossible.Contains(data.altitudeTerrain[X, Y]))
                {
                    Sprite2Unit(
                        ((Rang == "viseur") ? "Curseur/" : "Unit/"),
                        Rang, Game1.Ctt, spriteOfUnit);

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
        public Unit(Data data, Unit UnitToMove, Cell[,] Map, Cell newCell, List<Unit> ListOfUnit, Player Player)   // Constructeur de DESTRUCTION ahahahahahahahahahahahah
        {
            this.data = data;
            spriteOfUnit = new Sprite();
            Stats = new Stats();
            this.Player = UnitToMove.Player;
            this.Rang = UnitToMove.Rang;
            this.Classe = UnitToMove.Classe;
            this.Strength = UnitToMove.Strength;
            this.PV = UnitToMove.PV;
            this.TerrainPossible = Stats.TerrainPossibleUnit(Rang);
            this.MvtPossible = Stats.MvtPossUnit(this.Classe, new Vector(newCell.XofCell, newCell.YofCell), map, data);

            this.XofUnit = newCell.XofCell;
            this.YofUnit = newCell.YofCell;

            map = Map;



            if (TerrainPossible.Contains(data.altitudeTerrain[XofUnit, YofUnit]))
            {
                Sprite2Unit(
                        ((Rang == "viseur") ? "Curseur/" : "Unit/"),
                        Rang, Game1.Ctt, spriteOfUnit);

                ListOfUnit.Add(this);

                map[UnitToMove.XofUnit, UnitToMove.YofUnit].unitOfCell = null; // On mets a null la valeur de lunite dans lancienne case
                map[newCell.XofCell, newCell.YofCell].unitOfCell = this; // On met a jour la valeur de la nouvelle case

                map[this.XofUnit, this.YofUnit].Occupe = ListOfUnit.Contains(this);

                for (int i = 0; i < ListOfUnit.Count(); i++)    // On cherche lancienne unite et on la supprime de la liste des unitees a draw,
                {
                    if (ListOfUnit[i] == UnitToMove)
                    {
                        ListOfUnit.RemoveAt(i);
                        map[UnitToMove.XofUnit, UnitToMove.YofUnit].Occupe = false;
                    }
                }
            }
        }


        /// <summary>
        /// Destruction Unite
        /// </summary>
        /// <param name="data"></param>
        /// <param name="UnitToDestruct"></param>
        /// <param name="ListOfUnit"></param>
        public Unit(Data data, Unit UnitToDestruct, Cell[,] Map, List<Unit> ListOfUnit)
        {
            this.data = data;
            spriteOfUnit = new Sprite();

            this.XofUnit = UnitToDestruct.XofUnit;
            this.YofUnit = UnitToDestruct.YofUnit;

            map = Map;

            DelUnitofList(UnitToDestruct, ListOfUnit);
        }

        #endregion

        // // // // // // // // 

        #region FUNCTIONS



        void DelUnitofList(Unit UnitToDestruct, List<Unit> ListOfUnit)
        {
            for (int i = 0; i < ListOfUnit.Count(); i++) // On cherche lancienne unite et on la supprime de la liste des unitees a draw,
                if (ListOfUnit[i] == UnitToDestruct)
                {
                    ListOfUnit.RemoveAt(i);

                    map[UnitToDestruct.XofUnit, UnitToDestruct.YofUnit].Occupe = false;
                }

            map[UnitToDestruct.XofUnit, UnitToDestruct.YofUnit].unitOfCell = null; // On mets a null la valeur de lunite dans lancienne case

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


