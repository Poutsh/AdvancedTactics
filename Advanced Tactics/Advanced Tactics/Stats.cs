using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advanced_Tactics
{
    public class Stats
    {
        public int PVUnit(string rang)
        {
            switch (rang)
            {
                case "AA":
                    return 30;
                case "Commando":
                    return 25;
                case "Doc":
                    return 15;
                case "Engineer":
                    return 15;
                case "Pvt":
                    return 15;
                case "Plane":
                    return 50;
                case "HQ":
                    return 1000;
                case "Tank":
                    return 40;
                case "Truck":
                    return 30;
                default:
                    return 0;
            }
        }

        public int StrengthUnit(string rang)
        {
            switch (rang)
            {
                case "AA":
                    return 10;
                case "Commando":
                    return 10;
                case "Doc":
                    return 10;
                case "Engineer":
                    return 10;
                case "Pvt":
                    return 10;
                case "Plane":
                    return 10;
                case "HQ":
                    return 10;
                case "Tank":
                    return 10;
                case "Truck":
                    return 10;
                default:
                    return 0;
            }
        }

        public List<int> TerrainPossibleUnit(string rang)
        {
            if (new List<string>(6) { "AA", "Commando", "Doc", "Engineer", "Pvt" }.Contains(rang))
            {
                return new List<int>() { 1 };
            }
            else if (new List<string>(2) { "Tank", "Truck" }.Contains(rang))
            {
                return new List<int>() { 1, 2 };
            }
            else return new List<int>() { 0, 1, 2 };
        }

        //cacaDDD

        public List<Vector> MvtPossUnit(string classe, Vector position, Cell[,] map, Data data)
        {
            List<Vector> MvtPossible = new List<Vector>() { };
            //switch (classe)
            //{
            //    case "King":
            //        MvtPossible.Add(new Vector(position.X + 1, position.Y));
            //        MvtPossible.Add(new Vector(position.X, position.Y + 1));
            //        MvtPossible.Add(new Vector(position.X - 1, position.Y));
            //        MvtPossible.Add(new Vector(position.X, position.Y - 1));
            //        MvtPossible.Add(new Vector(position.X + 1, position.Y + 1));
            //        MvtPossible.Add(new Vector(position.X + 1, position.Y - 1));
            //        MvtPossible.Add(new Vector(position.X - 1, position.Y - 1));
            //        MvtPossible.Add(new Vector(position.X - 1, position.Y + 1));
            //        break;
            //    case "Queen":
            //        for (int i = 1; position.Y - i >= 0; i++)
            //            MvtPossible.Add(new Vector(position.X, position.Y - i));
            //        for (int i = 1; position.Y + i != data.HeightMap; i++)
            //            MvtPossible.Add(new Vector(position.X, position.Y + i));
            //        for (int i = 1; position.X - i >= 0; i++)
            //            MvtPossible.Add(new Vector(position.X - i, position.Y));
            //        for (int i = 1; position.X + i != data.WidthMap; i++)
            //            MvtPossible.Add(new Vector(position.X + i, position.Y));
            //        for (int i = 1; (position.Y - i > 0) && (position.X - i > 0); i++)
            //            MvtPossible.Add(new Vector(position.X - i, position.Y - i));
            //        for (int i = 1; (position.Y + i != data.HeightMap) && (position.X + i != data.WidthMap); i++)
            //            MvtPossible.Add(new Vector(position.X + i, position.Y + i));
            //        for (int i = 1; (position.Y - i > 0) && (position.X + i != data.WidthMap); i++)
            //            MvtPossible.Add(new Vector(position.X + i, position.Y - i));
            //        for (int i = 1; (position.Y + i != data.HeightMap) && (position.X - i > 0); i++)
            //            MvtPossible.Add(new Vector(position.X - i, position.Y + i));
            //        break;
            //    case "Rook":
            //        for (int i = 1; position.Y - i >= 0; i++)
            //            MvtPossible.Add(new Vector(position.X, position.Y - i));
            //        for (int i = 1; position.Y + i != data.HeightMap; i++)
            //            MvtPossible.Add(new Vector(position.X, position.Y + i));
            //        for (int i = 1; position.X - i >= 0; i++)
            //            MvtPossible.Add(new Vector(position.X - i, position.Y));
            //        for (int i = 1; position.X + i != data.WidthMap; i++)
            //            MvtPossible.Add(new Vector(position.X + i, position.Y));
            //        break;
            //    case "Bishop":
            //        for (int i = 1; (position.Y - i >= 0) && (position.X - i >= 0); i++)
            //            MvtPossible.Add(new Vector(position.X - i, position.Y - i));
            //        for (int i = 1; (position.Y + i != data.HeightMap) && (position.X + i != data.WidthMap); i++)
            //            MvtPossible.Add(new Vector(position.X + i, position.Y + i));
            //        for (int i = 1; (position.Y - i >= 0) && (position.X + i != data.WidthMap); i++)
            //            MvtPossible.Add(new Vector(position.X + i, position.Y - i));
            //        for (int i = 1; (position.Y + i != data.HeightMap) && (position.X - i >= 0); i++)
            //            MvtPossible.Add(new Vector(position.X - i, position.Y + i));
            //        break;
            //    case "Knight":
            //        MvtPossible.Add(new Vector(position.X - 1, position.Y + 2));
            //        MvtPossible.Add(new Vector(position.X + 1, position.Y + 2));
            //        MvtPossible.Add(new Vector(position.X + 2, position.Y + 1));
            //        MvtPossible.Add(new Vector(position.X + 1, position.Y - 1));
            //        MvtPossible.Add(new Vector(position.X + 1, position.Y - 2));
            //        MvtPossible.Add(new Vector(position.X - 1, position.Y - 2));
            //        MvtPossible.Add(new Vector(position.X - 2, position.Y - 1));
            //        MvtPossible.Add(new Vector(position.X - 2, position.Y + 1));
            //        break;
            //    case "Pawn":
            //        MvtPossible.Add(new Vector(position.X + 1, position.Y));
            //        MvtPossible.Add(new Vector(position.X, position.Y + 1));
            //        MvtPossible.Add(new Vector(position.X - 1, position.Y));
            //        MvtPossible.Add(new Vector(position.X, position.Y - 1));
            //        break;
            //}
            //return MvtPossible;
        }
    }
}
