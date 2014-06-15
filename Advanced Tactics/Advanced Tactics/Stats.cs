using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advanced_Tactics
{
    public class Stats
    {
        Data Data;
        public Stats(Data Data) { this.Data = Data; }

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


        private List<Vector> MvtPossible;
        private List<Vector> AttackPossible;
        private List<Vector> HQPossible;


        public List<Vector> HQPoss(Unit unit, Cell[,] map, Data data)
        {
            HQPossible = new List<Vector>() { };

            for (int x = unit.XofUnit - 2; x <= unit.XofUnit + 2; x++)
            {
                for (int y = unit.YofUnit - 2; y <= unit.YofUnit + 2; y++)
                {
                    if (x >= 0 && x < data.MapWidth && y >= 0 && y < data.MapHeight)
                    {
                        if (!map[x, y].Occupe) HQPossible.Add(new Vector(x, y));
                    }
                }
            }
            return HQPossible;
        }

        public Tuple<List<Vector>, List<Vector>> Possible(Unit unit, Cell[,] map, Data data, Match match)
        {
            MvtPossible = new List<Vector>() { };
            AttackPossible = new List<Vector>() { };
            switch (unit.Classe)
            {
                case "King":
                    for (int x = unit.XofUnit - 1; x <= unit.XofUnit + 1; x++)
                    {
                        if (x >= 0 && x < data.MapWidth && unit.YofUnit - 1 >= 0)
                        {
                            if (map[x, unit.YofUnit - 1].unitOfCell != null)
                            {
                                if (map[x, unit.YofUnit - 1].unitOfCell.Player != match.PlayerTurn) AttackPossible.Add(new Vector(x, unit.YofUnit - 1));
                            }
                            else MvtPossible.Add(new Vector(x, unit.YofUnit - 1));
                        }
                    }
                    for (int x = unit.XofUnit - 1; x <= unit.XofUnit + 1; x++)
                    {
                        if (x >= 0 && x < data.MapWidth && unit.YofUnit + 1 < data.MapHeight)
                        {
                            if (map[x, unit.YofUnit + 1].unitOfCell != null)
                            {
                                if (map[x, unit.YofUnit + 1].unitOfCell.Player != match.PlayerTurn) AttackPossible.Add(new Vector(x, unit.YofUnit + 1));
                            }
                            else MvtPossible.Add(new Vector(x, unit.YofUnit + 1));
                        }
                    }
                    if (unit.XofUnit - 1 >= 0 && map[unit.XofUnit - 1, unit.YofUnit].unitOfCell != null)
                    {
                        if (map[unit.XofUnit - 1, unit.YofUnit].unitOfCell.Player != match.PlayerTurn) AttackPossible.Add(new Vector(unit.XofUnit - 1, unit.YofUnit));
                    }
                    else MvtPossible.Add(new Vector(unit.XofUnit - 1, unit.YofUnit));

                    if (unit.XofUnit + 1 < data.MapWidth && map[unit.XofUnit + 1, unit.YofUnit].unitOfCell != null)
                    {
                        if (map[unit.XofUnit + 1, unit.YofUnit].unitOfCell.Player != match.PlayerTurn) AttackPossible.Add(new Vector(unit.XofUnit + 1, unit.YofUnit));
                    }
                    else MvtPossible.Add(new Vector(unit.XofUnit + 1, unit.YofUnit));
                    break;


                case "Queen":
                    for (int i = 1; (unit.YofUnit - i >= 0) && (unit.XofUnit - i >= 0); i++)
                    {
                        if (map[unit.XofUnit, unit.YofUnit].unitOfCell.TerrainPossible.Contains(data.altitudeTerrain[unit.XofUnit - i, unit.YofUnit - i]) == false) i = data.MapHeight * data.MapWidth;
                        else if (map[unit.XofUnit - i, unit.YofUnit - i].unitOfCell != null)
                        {
                            if (map[unit.XofUnit - i, unit.YofUnit - i].unitOfCell.Player != match.PlayerTurn) AttackPossible.Add(new Vector(unit.XofUnit - i, unit.YofUnit - i));
                            i = data.MapHeight * data.MapWidth;
                        }
                        else MvtPossible.Add(new Vector(unit.XofUnit - i, unit.YofUnit - i));
                    }
                    for (int i = 1; (unit.YofUnit + i < data.MapHeight) && (unit.XofUnit + i < data.MapWidth); i++)
                    {
                        if (map[unit.XofUnit, unit.YofUnit].unitOfCell.TerrainPossible.Contains(data.altitudeTerrain[unit.XofUnit + i, unit.YofUnit + i]) == false) i = data.MapHeight * data.MapWidth;
                        else if (map[unit.XofUnit + i, unit.YofUnit + i].unitOfCell != null)
                        {
                            if (map[unit.XofUnit + i, unit.YofUnit + i].unitOfCell.Player != match.PlayerTurn) AttackPossible.Add(new Vector(unit.XofUnit + i, unit.YofUnit + i));
                            i = data.MapHeight * data.MapWidth;
                        }
                        else MvtPossible.Add(new Vector(unit.XofUnit + i, unit.YofUnit + i));
                    }
                    for (int i = 1; (unit.YofUnit - i >= 0) && (unit.XofUnit + i < data.MapWidth); i++)
                    {
                        if (map[unit.XofUnit, unit.YofUnit].unitOfCell.TerrainPossible.Contains(data.altitudeTerrain[unit.XofUnit + i, unit.YofUnit - i]) == false) i = data.MapHeight * data.MapWidth;
                        else if (map[unit.XofUnit + i, unit.YofUnit - i].unitOfCell != null)
                        {
                            if (map[unit.XofUnit + i, unit.YofUnit - i].unitOfCell.Player != match.PlayerTurn) AttackPossible.Add(new Vector(unit.XofUnit + i, unit.YofUnit - i));
                            i = data.MapHeight * data.MapWidth;
                        }
                        else MvtPossible.Add(new Vector(unit.XofUnit + i, unit.YofUnit - i));
                    }
                    for (int i = 1; (unit.YofUnit + i < data.MapHeight) && (unit.XofUnit - i >= 0); i++)
                    {
                        if (map[unit.XofUnit, unit.YofUnit].unitOfCell.TerrainPossible.Contains(data.altitudeTerrain[unit.XofUnit - i, unit.YofUnit + i]) == false) i = data.MapHeight * data.MapWidth;
                        else if (map[unit.XofUnit - i, unit.YofUnit + i].unitOfCell != null)
                        {
                            if (map[unit.XofUnit - i, unit.YofUnit + i].unitOfCell.Player != match.PlayerTurn) AttackPossible.Add(new Vector(unit.XofUnit - i, unit.YofUnit + i));
                            i = data.MapHeight * data.MapWidth;
                        }
                        else MvtPossible.Add(new Vector(unit.XofUnit - i, unit.YofUnit + i));
                    }
                    for (int x = unit.XofUnit + 1; x < data.MapWidth; x++)
                    {
                        if (map[unit.XofUnit, unit.YofUnit].unitOfCell.TerrainPossible.Contains(data.altitudeTerrain[x, unit.YofUnit]) == false) x = data.MapWidth + 10;
                        else if (map[x, unit.YofUnit].unitOfCell != null)
                        {
                            if (map[x, unit.YofUnit].unitOfCell.Player != match.PlayerTurn) AttackPossible.Add(new Vector(x, unit.YofUnit));
                            x = data.MapWidth + 10;
                        }
                        else MvtPossible.Add(new Vector(x, unit.YofUnit));
                    }
                    for (int x = unit.XofUnit - 1; x >= 0; x--)
                    {
                        if (map[unit.XofUnit, unit.YofUnit].unitOfCell.TerrainPossible.Contains(data.altitudeTerrain[x, unit.YofUnit]) == false) x = -10;
                        else if (map[x, unit.YofUnit].unitOfCell != null)
                        {
                            if (map[x, unit.YofUnit].unitOfCell.Player != match.PlayerTurn) AttackPossible.Add(new Vector(x, unit.YofUnit));
                            x = -10;
                        }
                        else MvtPossible.Add(new Vector(x, unit.YofUnit));
                    }
                    for (int y = unit.YofUnit + 1; y < data.MapHeight; y++)
                    {
                        if (map[unit.XofUnit, unit.YofUnit].unitOfCell.TerrainPossible.Contains(data.altitudeTerrain[unit.XofUnit, y]) == false) y = data.MapHeight + 10;
                        else if (map[unit.XofUnit, y].unitOfCell != null)
                        {
                            if (map[unit.XofUnit, y].unitOfCell.Player != match.PlayerTurn) AttackPossible.Add(new Vector(unit.XofUnit, y));
                            y = data.MapHeight + 10;
                        }
                        else MvtPossible.Add(new Vector(unit.XofUnit, y));
                    }
                    for (int y = unit.YofUnit - 1; y >= 0; y--)
                    {
                        if (map[unit.XofUnit, unit.YofUnit].unitOfCell.TerrainPossible.Contains(data.altitudeTerrain[unit.XofUnit, y]) == false) y = -10;
                        else if (map[unit.XofUnit, y].unitOfCell != null)
                        {
                            if (map[unit.XofUnit, y].unitOfCell.Player != match.PlayerTurn) AttackPossible.Add(new Vector(unit.XofUnit, y));
                            y = -10;
                        }
                        else MvtPossible.Add(new Vector(unit.XofUnit, y));
                    }
                    break;


                case "Rook":
                    for (int x = unit.XofUnit + 1; x < data.MapWidth; x++)
                    {
                        if (map[unit.XofUnit, unit.YofUnit].unitOfCell.TerrainPossible.Contains(data.altitudeTerrain[x, unit.YofUnit]) == false) x = data.MapWidth + 10;
                        else if (map[x, unit.YofUnit].unitOfCell != null)
                        {
                            if (map[x, unit.YofUnit].unitOfCell.Player != match.PlayerTurn) AttackPossible.Add(new Vector(x, unit.YofUnit));
                            x = data.MapWidth + 10;
                        }
                        else MvtPossible.Add(new Vector(x, unit.YofUnit));
                    }
                    for (int x = unit.XofUnit - 1; x >= 0; x--)
                    {
                        if (map[unit.XofUnit, unit.YofUnit].unitOfCell.TerrainPossible.Contains(data.altitudeTerrain[x, unit.YofUnit]) == false) x = -10;
                        else if (map[x, unit.YofUnit].unitOfCell != null)
                        {
                            if (map[x, unit.YofUnit].unitOfCell.Player != match.PlayerTurn) AttackPossible.Add(new Vector(x, unit.YofUnit));
                            x = -10;
                        }
                        else MvtPossible.Add(new Vector(x, unit.YofUnit));
                    }
                    for (int y = unit.YofUnit + 1; y < data.MapHeight; y++)
                    {
                        if (map[unit.XofUnit, unit.YofUnit].unitOfCell.TerrainPossible.Contains(data.altitudeTerrain[unit.XofUnit, y]) == false) y = data.MapHeight + 10;
                        else if (map[unit.XofUnit, y].unitOfCell != null)
                        {
                            if (map[unit.XofUnit, y].unitOfCell.Player != match.PlayerTurn) AttackPossible.Add(new Vector(unit.XofUnit, y));
                            y = data.MapHeight + 10;
                        }
                        else MvtPossible.Add(new Vector(unit.XofUnit, y));
                    }
                    for (int y = unit.YofUnit - 1; y >= 0; y--)
                    {
                        if (map[unit.XofUnit, unit.YofUnit].unitOfCell.TerrainPossible.Contains(data.altitudeTerrain[unit.XofUnit, y]) == false) y = -10;
                        else if (map[unit.XofUnit, y].unitOfCell != null)
                        {
                            if (map[unit.XofUnit, y].unitOfCell.Player != match.PlayerTurn) AttackPossible.Add(new Vector(unit.XofUnit, y));
                            y = -10;
                        }
                        else MvtPossible.Add(new Vector(unit.XofUnit, y));
                    }
                    break;


                case "Bishop":
                    for (int i = 1; (unit.YofUnit - i >= 0) && (unit.XofUnit - i >= 0); i++)
                    {
                        if (map[unit.XofUnit, unit.YofUnit].unitOfCell.TerrainPossible.Contains(data.altitudeTerrain[unit.XofUnit - i, unit.YofUnit - i]) == false) i = data.MapHeight * data.MapWidth;
                        else if (map[unit.XofUnit - i, unit.YofUnit - i].unitOfCell != null)
                        {
                            if (map[unit.XofUnit - i, unit.YofUnit - i].unitOfCell.Player != match.PlayerTurn) AttackPossible.Add(new Vector(unit.XofUnit - i, unit.YofUnit - i));
                            i = data.MapHeight * data.MapWidth;
                        }
                        else MvtPossible.Add(new Vector(unit.XofUnit - i, unit.YofUnit - i));
                    }
                    for (int i = 1; (unit.YofUnit + i < data.MapHeight) && (unit.XofUnit + i < data.MapWidth); i++)
                    {
                        if (map[unit.XofUnit, unit.YofUnit].unitOfCell.TerrainPossible.Contains(data.altitudeTerrain[unit.XofUnit + i, unit.YofUnit + i]) == false) i = data.MapHeight * data.MapWidth;
                        else if (map[unit.XofUnit + i, unit.YofUnit + i].unitOfCell != null)
                        {
                            if (map[unit.XofUnit + i, unit.YofUnit + i].unitOfCell.Player != match.PlayerTurn) AttackPossible.Add(new Vector(unit.XofUnit + i, unit.YofUnit + i));
                            i = data.MapHeight * data.MapWidth;
                        }
                        else MvtPossible.Add(new Vector(unit.XofUnit + i, unit.YofUnit + i));
                    }
                    for (int i = 1; (unit.YofUnit - i >= 0) && (unit.XofUnit + i < data.MapWidth); i++)
                    {
                        if (map[unit.XofUnit, unit.YofUnit].unitOfCell.TerrainPossible.Contains(data.altitudeTerrain[unit.XofUnit + i, unit.YofUnit - i]) == false) i = data.MapHeight * data.MapWidth;
                        else if (map[unit.XofUnit + i, unit.YofUnit - i].unitOfCell != null)
                        {
                            if (map[unit.XofUnit + i, unit.YofUnit - i].unitOfCell.Player != match.PlayerTurn) AttackPossible.Add(new Vector(unit.XofUnit + i, unit.YofUnit - i));
                            i = data.MapHeight * data.MapWidth;
                        }
                        else MvtPossible.Add(new Vector(unit.XofUnit + i, unit.YofUnit - i));
                    }
                    for (int i = 1; (unit.YofUnit + i < data.MapHeight) && (unit.XofUnit - i >= 0); i++)
                    {
                        if (map[unit.XofUnit, unit.YofUnit].unitOfCell.TerrainPossible.Contains(data.altitudeTerrain[unit.XofUnit - i, unit.YofUnit + i]) == false) i = data.MapHeight * data.MapWidth;
                        else if (map[unit.XofUnit - i, unit.YofUnit + i].unitOfCell != null)
                        {
                            if (map[unit.XofUnit - i, unit.YofUnit + i].unitOfCell.Player != match.PlayerTurn) AttackPossible.Add(new Vector(unit.XofUnit - i, unit.YofUnit + i));
                            i = data.MapHeight * data.MapWidth;
                        }
                        else MvtPossible.Add(new Vector(unit.XofUnit - i, unit.YofUnit + i));
                    }
                    break;


                case "Knight":
                    if ((unit.XofUnit - 1 >= 0) && (unit.YofUnit + 2 < data.MapHeight) && map[unit.XofUnit, unit.YofUnit].unitOfCell.TerrainPossible.Contains(data.altitudeTerrain[unit.XofUnit - 1, unit.YofUnit + 2]) == true)
                    {
                        if (map[unit.XofUnit - 1, unit.YofUnit + 2].unitOfCell != null)
                        {
                            if (map[unit.XofUnit - 1, unit.YofUnit + 2].unitOfCell.Player != match.PlayerTurn) AttackPossible.Add(new Vector(unit.XofUnit - 1, unit.YofUnit + 2));
                        }
                        else MvtPossible.Add(new Vector(unit.XofUnit - 1, unit.YofUnit + 2));
                    }

                    if ((unit.XofUnit + 1 < data.MapWidth) && (unit.YofUnit + 2 < data.MapHeight) && map[unit.XofUnit, unit.YofUnit].unitOfCell.TerrainPossible.Contains(data.altitudeTerrain[unit.XofUnit + 1, unit.YofUnit + 2]) == true)
                    {
                        if (map[unit.XofUnit + 1, unit.YofUnit + 2].unitOfCell != null)
                        {
                            if (map[unit.XofUnit + 1, unit.YofUnit + 2].unitOfCell.Player != match.PlayerTurn) AttackPossible.Add(new Vector(unit.XofUnit + 1, unit.YofUnit + 2));
                        }
                        else MvtPossible.Add(new Vector(unit.XofUnit + 1, unit.YofUnit + 2));
                    }
                    if ((unit.XofUnit + 2 < data.MapWidth) && (unit.YofUnit + 1 < data.MapHeight) && map[unit.XofUnit, unit.YofUnit].unitOfCell.TerrainPossible.Contains(data.altitudeTerrain[unit.XofUnit + 2, unit.YofUnit + 1]) == true)
                    {
                        if (map[unit.XofUnit + 2, unit.YofUnit + 1].unitOfCell != null)
                        {
                            if (map[unit.XofUnit + 2, unit.YofUnit + 1].unitOfCell.Player != match.PlayerTurn) AttackPossible.Add(new Vector(unit.XofUnit + 2, unit.YofUnit + 1));
                        }
                        else MvtPossible.Add(new Vector(unit.XofUnit + 2, unit.YofUnit + 1));
                    }
                    if ((unit.XofUnit + 2 < data.MapWidth) && (unit.YofUnit - 1 >= 0) && map[unit.XofUnit, unit.YofUnit].unitOfCell.TerrainPossible.Contains(data.altitudeTerrain[unit.XofUnit + 2, unit.YofUnit - 1]) == true)
                    {
                        if (map[unit.XofUnit + 2, unit.YofUnit - 1].unitOfCell != null)
                        {
                            if (map[unit.XofUnit + 2, unit.YofUnit - 1].unitOfCell.Player != match.PlayerTurn) AttackPossible.Add(new Vector(unit.XofUnit + 2, unit.YofUnit - 1));
                        }
                        else MvtPossible.Add(new Vector(unit.XofUnit + 2, unit.YofUnit - 1));
                    }
                    if ((unit.XofUnit + 1 < data.MapWidth) && (unit.YofUnit - 2 >= 0) && map[unit.XofUnit, unit.YofUnit].unitOfCell.TerrainPossible.Contains(data.altitudeTerrain[unit.XofUnit + 1, unit.YofUnit - 2]) == true)
                    {
                        if (map[unit.XofUnit + 1, unit.YofUnit - 2].unitOfCell != null)
                        {
                            if (map[unit.XofUnit + 1, unit.YofUnit - 2].unitOfCell.Player != match.PlayerTurn) AttackPossible.Add(new Vector(unit.XofUnit + 1, unit.YofUnit - 2));
                        }
                        else MvtPossible.Add(new Vector(unit.XofUnit + 1, unit.YofUnit - 2));
                    }
                    if ((unit.XofUnit - 1 >= 0) && (unit.YofUnit - 2 >= 0) && map[unit.XofUnit, unit.YofUnit].unitOfCell.TerrainPossible.Contains(data.altitudeTerrain[unit.XofUnit - 1, unit.YofUnit - 2]) == true)
                    {
                        if (map[unit.XofUnit - 1, unit.YofUnit - 2].unitOfCell != null)
                        {
                            if (map[unit.XofUnit - 1, unit.YofUnit - 2].unitOfCell.Player != match.PlayerTurn) AttackPossible.Add(new Vector(unit.XofUnit - 1, unit.YofUnit - 2));
                        }
                        else MvtPossible.Add(new Vector(unit.XofUnit - 1, unit.YofUnit - 2));
                    }
                    if ((unit.XofUnit - 2 >= 0) && (unit.YofUnit - 1 >= 0) && map[unit.XofUnit, unit.YofUnit].unitOfCell.TerrainPossible.Contains(data.altitudeTerrain[unit.XofUnit - 2, unit.YofUnit - 1]) == true)
                    {
                        if (map[unit.XofUnit - 2, unit.YofUnit - 1].unitOfCell != null)
                        {
                            if (map[unit.XofUnit - 2, unit.YofUnit - 1].unitOfCell.Player != match.PlayerTurn) AttackPossible.Add(new Vector(unit.XofUnit - 2, unit.YofUnit - 1));
                        }
                        else MvtPossible.Add(new Vector(unit.XofUnit - 2, unit.YofUnit - 1));
                    }
                    if ((unit.XofUnit - 2 >= 0) && (unit.YofUnit + 1 < data.MapHeight) && map[unit.XofUnit, unit.YofUnit].unitOfCell.TerrainPossible.Contains(data.altitudeTerrain[unit.XofUnit - 2, unit.YofUnit + 1]) == true)
                    {
                        if (map[unit.XofUnit - 2, unit.YofUnit + 1].unitOfCell != null)
                        {
                            if (map[unit.XofUnit - 2, unit.YofUnit + 1].unitOfCell.Player != match.PlayerTurn) AttackPossible.Add(new Vector(unit.XofUnit - 2, unit.YofUnit + 1));
                        }
                        else MvtPossible.Add(new Vector(unit.XofUnit - 2, unit.YofUnit + 1));
                    }
                    break;


                case "Pawn":
                    if (unit.XofUnit + 1 < data.MapWidth)
                    {
                        if (map[unit.XofUnit + 1, unit.YofUnit].unitOfCell != null)
                        {
                            if (map[unit.XofUnit + 1, unit.YofUnit].unitOfCell.Player != match.PlayerTurn) AttackPossible.Add(new Vector(unit.XofUnit + 1, unit.YofUnit));
                        }
                        else MvtPossible.Add(new Vector(unit.XofUnit + 1, unit.YofUnit));

                    }
                    if (unit.YofUnit + 1 < data.MapHeight)
                    {
                        if (map[unit.XofUnit, unit.YofUnit + 1].unitOfCell != null)
                        {
                            if (map[unit.XofUnit, unit.YofUnit + 1].unitOfCell.Player != match.PlayerTurn) AttackPossible.Add(new Vector(unit.XofUnit, unit.YofUnit + 1));
                        }
                        else MvtPossible.Add(new Vector(unit.XofUnit, unit.YofUnit + 1));

                    }
                    if (unit.XofUnit - 1 >= 0)
                    {
                        if (map[unit.XofUnit - 1, unit.YofUnit].unitOfCell != null)
                        {
                            if (map[unit.XofUnit - 1, unit.YofUnit].unitOfCell.Player != match.PlayerTurn) AttackPossible.Add(new Vector(unit.XofUnit - 1, unit.YofUnit));
                        }
                        else MvtPossible.Add(new Vector(unit.XofUnit - 1, unit.YofUnit));

                    }
                    if (unit.YofUnit - 1 >= 0)
                    {
                        if (map[unit.XofUnit, unit.YofUnit - 1].unitOfCell != null)
                        {
                            if (map[unit.XofUnit, unit.YofUnit - 1].unitOfCell.Player != match.PlayerTurn) AttackPossible.Add(new Vector(unit.XofUnit, unit.YofUnit - 1));
                        }
                        else MvtPossible.Add(new Vector(unit.XofUnit, unit.YofUnit - 1));
                    }
                    break;
            }
            return new Tuple<List<Vector>, List<Vector>>(MvtPossible, AttackPossible);
        }
    }
}
