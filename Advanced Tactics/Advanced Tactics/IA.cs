using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advanced_Tactics
{
    public class IA
    {
        Match match;
        Cell[,] map;
        Data data;
        public Player IAPlayer;
        Stats stats;

        public IA(Data Data, Match Match, Cell[,] Map)
        {
            data = Data;
            match = Match;
            map = Map;
            stats = new Stats(data);
        }

        private void doMoveUnit(Unit unit, Cell newCell, List<Unit> ListOfUnit)
        {
            if (Contains<Vector>(unit.MvtPossible, new Vector(newCell.XofCell, newCell.YofCell)) || (Contains<Vector>(unit.AttackPossible, new Vector(newCell.XofCell, newCell.YofCell)) && !map[newCell.XofCell, newCell.YofCell].Occupe))
                unit = new Unit(data, unit, map, newCell, match);
        }

        public void Attack(Match Match, Viseur vis)
        {
            Random rr = new Random();
            int ss = rr.Next(0, Match.PlayerTurn.UnitOfPlayer.Count);
            if (Match.PlayerTurn.UnitOfPlayer[ss].Classe != "King")
            {

                if (Match.PlayerTurn.UnitOfPlayer[ss].AttackPossible.Count > 0 && Match.PlayerTurn.UnitOfPlayer[ss] != null)
                {
                    int qq = rr.Next(0, Match.PlayerTurn.UnitOfPlayer[ss].AttackPossible.Count);
                    Vector newCell = new Vector(Match.PlayerTurn.UnitOfPlayer[ss].AttackPossible[qq].X, Match.PlayerTurn.UnitOfPlayer[ss].AttackPossible[qq].Y);

                    if (Match.PlayerTurn.UnitOfPlayer[ss].Classe != "King" && map[newCell.X, newCell.Y].unitOfCell != null) map[newCell.X, newCell.Y].unitOfCell.PV -= Match.PlayerTurn.UnitOfPlayer[ss].Strength;
                    else if (map[newCell.X, newCell.Y].unitOfCell != null && Match.PlayerTurn.UnitOfPlayer[ss].Rang == "Doc" && map[newCell.X, newCell.Y].unitOfCell.Player == Match.PlayerTurn) map[newCell.X, newCell.Y].unitOfCell.PV += Match.PlayerTurn.UnitOfPlayer[ss].Strength;
                    else if (map[newCell.X, newCell.Y].unitOfCell != null) map[newCell.X, newCell.Y].unitOfCell.PV -= stats.PVUnit(map[newCell.X, newCell.Y].unitOfCell.Rang) / 2;

                    if (map[newCell.X, newCell.Y].unitOfCell != null && map[newCell.X, newCell.Y].unitOfCell.PV <= 0)
                    {
                        Match.PlayerTurn.Money += map[newCell.X, newCell.Y].unitOfCell.Point;
                        Match.PlayerTurn.Score += map[newCell.X, newCell.Y].unitOfCell.Point;
                        map[newCell.X, newCell.Y].unitOfCell.DelUnitofList();
                        if (Match.PlayerTurn.UnitOfPlayer[ss].Classe != "King") doMoveUnit(Match.PlayerTurn.UnitOfPlayer[ss], map[newCell.X, newCell.Y], match.PlayerTurn.UnitOfPlayer);
                        vis.Explosion();
                    }
                    Match.TurnbyTurn.MvtCount++;
                }
            }
        }

        public bool Contains<T>(List<T> List, T tocompare) { if (List != null)return List.Contains(tocompare); else return false; }
        public void Move(Match Match)
        {
            if (Match.PlayerTurn.UnitOfPlayer.Count > 0)
            {
                Random rr = new Random();
                Vector newCell = new Vector();

                int ss = rr.Next(0, Match.PlayerTurn.UnitOfPlayer.Count);

                if (Match.PlayerTurn.UnitOfPlayer[ss].Classe != "King")
                {
                    if (Match.PlayerTurn.UnitOfPlayer[ss].MvtPossible.Count > 0)
                    {
                        int qq = rr.Next(0, Match.PlayerTurn.UnitOfPlayer[ss].MvtPossible.Count);

                        try
                        {
                            try { Match.PlayerTurn.UnitOfPlayer[ss] = new Unit(data, Match.PlayerTurn.UnitOfPlayer[ss], map, map[Match.PlayerTurn.UnitOfPlayer[ss].MvtPossible[qq].X, Match.PlayerTurn.UnitOfPlayer[ss].MvtPossible[qq].Y], match); }
                            catch (IndexOutOfRangeException) { throw; }
                        }
                        catch (ArgumentOutOfRangeException) { Console.Write(""); }



                        Match.TurnbyTurn.MvtCount++;
                    }
                    //Unit unit = Match.PlayerTurn.UnitOfPlayer[ss];



                }
            }
        }

        public void Build(Match Match)
        {
            string[] arrayrang = new string[] { "AA", "Commando", "Doc", "Truck", "Engineer", "Plane", "Pvt", "Tank", "AA", "Commando", "Doc", "Truck", "Engineer", "Plane", "Pvt", "Tank" };
            string[] arrayclasse = new string[] { "Queen", "Rook", "Bishop", "Knight", "Pawn" };

            Random rr = new Random();
            Random rrr = new Random();
            int rang = rr.Next(0, arrayrang.Count());
            int classe = rrr.Next(0, arrayclasse.Count());

            Vector temp = Match.PlayerTurn.StartZone[rr.Next(0, Match.PlayerTurn.StartZone.Count)];

            if (map[temp.X, temp.Y].unitOfCell == null && Match.PlayerTurn.Money - stats.PointUnit(arrayrang[rang], arrayclasse[classe]) >= 0)
            {
                map[temp.X, temp.Y].unitOfCell = new Unit(data, arrayrang[rang], arrayclasse[classe], map, temp.X, temp.Y, Match.PlayerTurn, Match);
                Match.PlayerTurn.Money -= stats.PointUnit(arrayrang[rang], arrayclasse[classe]);
                Match.TurnbyTurn.MvtCount++;
            }
        }
    }
}
