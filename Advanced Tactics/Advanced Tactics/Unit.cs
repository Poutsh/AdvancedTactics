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
        SpriteBatch spriteBatch;

        public Viseur Viseur { get; set; }

        public int XofUnit { get; set; }
        public int YofUnit { get; set; }

        public string Classe { get; set; }
        public string Rang { get; set; }
        public List<int> TerrainPossible { get; set; }

        List<int> Mvt1 = new List<int>(1) { 1 };
        List<int> Mvt2 = new List<int>(2) { 1, 2 };
        List<int> Mvt3 = new List<int>(3) { 0, 1, 2 };
        List<int> Mvt4 = new List<int>(0) { };

        private AnimatedTexture SpriteTexture;
        private const float Rotation = 0;
        private const float Scale = 2.0f;
        private const float Depth = 0.5f;
        private Viewport viewport;
        private Vector2 shipPos;
        private const int Frames = 4;
        private const int FramesPerSec = 2;
        public int PV { get; set; }
        public int Strength { get; set; }
        public List<Vector> MvtPossible { get; set; }


        #endregion

        // // // // // // // // 

        #region CONSTRUCTEURS

        Action<string, string, ContentManager, Sprite> Sprite2Unit = (p, r, c, s) => s.LC(c, p + r);

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
                this.MvtPossible = null;
                this.MvtPossible = MvtPoss(Classe, new Vector(this.XofUnit, this.YofUnit), this.MvtPossible, map, data);
                switch (Rang)
                {
                    case "AA":
                        this.PV = 30;
                        this.Strength = 3;
                        break;
                    case "Commando":
                        this.PV = 25;
                        this.Strength = 3;
                        break;
                    case "Doc":
                        this.PV = 15;
                        this.Strength = 1;
                        break;
                    case "Engineer":
                        this.PV = 15;
                        this.Strength = 1;
                        break;
                    case "Pvt":
                        this.PV = 15;
                        this.Strength = 2;
                        break;
                    case "Plane":
                        this.PV = 50;
                        this.Strength = 5;
                        break;
                    case "HQ":
                        this.PV = 1000;
                        this.Strength = 10;
                        break;
                    case "Tank":
                        this.PV = 40;
                        this.Strength = 4;
                        break;
                    case "Truck":
                        this.PV = 30;
                        this.Strength = 1;
                        break;
                }
                

                if (new List<string>(2) { "Tank", "Truck" }.Contains(Rang))
                {
                    TerrainPossible = new List<int>(2);
                    TerrainPossible.Add(1); TerrainPossible.Add(2);
                }
                if (new List<string>(6) { "AA", "Commando", "Doc", "Engineer", "Pvt" }.Contains(Rang))
                {
                    TerrainPossible = new List<int>(1);
                    TerrainPossible.Add(1);
                }
                if (new List<string>(1) { "Plane", "HQ" }.Contains(Rang))
                {
                    TerrainPossible = new List<int>(3);
                    TerrainPossible.Add(0); TerrainPossible.Add(1); TerrainPossible.Add(2);
                }

                if (TerrainPossible.Contains(data.altitudeTerrain[X, Y]))
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
            this.Strength = UnitToMove.Strength;
            this.PV = UnitToMove.PV;
            this.MvtPossible = null;
            this.MvtPossible = MvtPoss(Classe, new Vector(newCell.XofCell, newCell.YofCell), this.MvtPossible, map, data);
            
            map = Map;

            if (new List<string>(2) { "Tank", "Truck" }.Contains(Rang))
            {
                TerrainPossible = new List<int>(2);
                TerrainPossible.Add(1); TerrainPossible.Add(2);
            }
            if (new List<string>(6) { "AA", "Commando", "Doc", "Engineer", "Pvt" }.Contains(Rang))
            {
                TerrainPossible = new List<int>(1);
                TerrainPossible.Add(1);
            }
            if (new List<string>(1) { "Plane", "HQ" }.Contains(Rang))
            {
                TerrainPossible = new List<int>(3);
                TerrainPossible.Add(0); TerrainPossible.Add(1); TerrainPossible.Add(2);
            }

            if (TerrainPossible.Contains(data.altitudeTerrain[XofUnit, YofUnit]))
            {
                if (this.Rang == "viseur") path = "Curseur/"; else path = "Unit/";
                Sprite2Unit(path, this.Rang, data.Content, spriteOfUnit);

                ListOfUnit.Add(this);

                map[UnitToMove.XofUnit, UnitToMove.YofUnit].unitOfCell = null; // On mets a null la valeur de lunite dans lancienne case
                map[newCell.XofCell, newCell.YofCell].unitOfCell = this; // On mets a jour la valeur de la nouvelle case

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
            map[UnitToDestruct.XofUnit, UnitToDestruct.YofUnit].unitOfCell = null; // On mets a null la valeur de lunite dans lancienne case
            map[this.XofUnit, this.YofUnit].unitOfCell = null; // On mets a jour la valeur de la nouvelle case

            //map[UnitToDestruct.XofUnit, UnitToDestruct.YofUnit].unitOfCell = null;
            for (int i = 0; i < ListOfUnit.Count(); i++) // On cherche lancienne unite et on la supprime de la liste des unitees a draw,
                if (ListOfUnit[i] == UnitToDestruct)
                {
                    ListOfUnit.RemoveAt(i);

                    map[UnitToDestruct.XofUnit, UnitToDestruct.YofUnit].Occupe = false;
                }
        }

        #endregion

        // // // // // // // // 

        #region FUNCTIONS
        public List<Vector> MvtPoss(string classe, Vector position, List<Vector> MvtPossible, Cell[,] map, Data data)
        {
            MvtPossible = new List<Vector>() { };
            switch (classe)
            {
                case "King":
                    MvtPossible.Add(new Vector(position.X + 1, position.Y));
                    MvtPossible.Add(new Vector(position.X - 1, position.Y));
                    MvtPossible.Add(new Vector(position.X, position.Y + 1));
                    MvtPossible.Add(new Vector(position.X, position.Y - 1));
                    break;
                case "Queen":
                    for (float x = 0; x < data.WidthMap; ++x)
                    {
                        for (float y = 0; y < data.HeightMap; ++y)
                        {
                            if ((position.Y - position.X) == (y - x))
                                for (int i = 0; i < 5; i++)
                                    MvtPossible.Add(new Vector(x, y));

                            if ((position.X + position.Y) == (x + y))
                                for (int i = 0; i < 5; i++)
                                    MvtPossible.Add(new Vector(x, y));
                        }
                    }
                    for (float x = 0; x < data.WidthMap; ++x)
                        for (float y = 0; y < data.HeightMap; ++y)
                        {
                            MvtPossible.Add(new Vector(x, position.Y));
                            MvtPossible.Add(new Vector(position.X, y));
                        }
                    break;
                case "Rock":
                    for (float x = 0; x < data.WidthMap; ++x)
                        for (float y = 0; y < data.HeightMap; ++y)
                        {
                            MvtPossible.Add(new Vector(x, position.Y));
                            MvtPossible.Add(new Vector(position.X, y));
                        }
                    break;
                case "Bishop":
                    for (float x = 0; x < data.WidthMap; ++x)
                    {
                        for (float y = 0; y < data.HeightMap; ++y)
                        {
                            if ((position.Y - position.X) == (y - x))
                                for (int i = 0; i < 5; i++)
                                    MvtPossible.Add(new Vector(x, y));

                            if ((position.X + position.Y) == (x + y))
                                for (int i = 0; i < 5; i++)
                                    MvtPossible.Add(new Vector(x, y));
                        }
                    }
                    break;
                case "Knight":
                    for (float x = 0; x < data.WidthMap; ++x)
                        for (float y = 0; y < data.HeightMap; ++y)
                        {
                            if ((position.X - 1 == x && y == position.Y - 2 * 1) || (position.X + 1 == x && y == position.Y - 2 * 1) || (position.X - 2 * 1 == x && y == position.Y - 1) || (position.X + 2 * 1 == x && y == position.Y - 1) || (position.X - 2 * 1 == x && y == position.Y + 1) || (position.X + 2 * 1 == x && y == position.Y + 1) || (position.X - 1 == x && y == position.Y + 2 * 1) || (position.X + 1 == x && y == position.Y + 2 * 1))
                                for (int i = 0; i < 5; i++)
                                {
                                    MvtPossible.Add(new Vector(x, y));
                                    MvtPossible.Add(new Vector(position.X, position.Y));
                                }
                        }
                    break;
                case "Pawn":
                    for (float x = 0; x < data.WidthMap; ++x)
                        for (float y = 0; y < data.HeightMap; ++y)
                        {
                            if ((position.X - 1 == x && y == position.Y - 2 * 1) || (position.X + 1 == x && y == position.Y - 2 * 1) || (position.X - 2 * 1 == x && y == position.Y - 1) || (position.X + 2 * 1 == x && y == position.Y - 1) || (position.X - 2 * 1 == x && y == position.Y + 1) || (position.X + 2 * 1 == x && y == position.Y + 1) || (position.X - 1 == x && y == position.Y + 2 * 1) || (position.X + 1 == x && y == position.Y + 2 * 1))
                                for (int i = 0; i < 5; i++)
                                {
                                    MvtPossible.Add(new Vector(x, y));
                                    MvtPossible.Add(new Vector(position.X, position.Y));
                                }
                        }
                    break;
            }
            return MvtPossible;
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


