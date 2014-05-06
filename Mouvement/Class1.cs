using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System.Threading.Tasks;


namespace Mouvement
{
    public class Mouvement
    {
        public enum Key { Q, W, A, Z, LeftControl, LeftShift, R }

        KeyboardState oldKey, curKey;
        public bool Occupe { get; set; }
        public bool depSelec { get; set; }
        public bool destSelec { get; set; }
        public bool Altitude { get; set; }

        public Vector coordViseur { get; set; }
        public Vector depPos { get; set; }
        public Vector destPos { get; set; }
        public List<Vector> MvtPossible { get; set; }

        public List<Object> ListOfUnit { get; set; }
        public Object UnitTemp { get; set; }

        object Unit;

        public bool ViseurOverPos(Vector position) { return this.coordViseur == position; }

        //public bool Contains(List<Object> List, Object objecttocompare) { return List.Contains(objecttocompare); }
        public bool Contains<T>(List<T> List, T objecttocompare) where T : struct { return List.Contains(objecttocompare); }









        public void Reset()
        {
            depSelec = false; depPos = Vector.Zero;
            destSelec = false; destPos = Vector.Zero;
        }

        void Moving(bool depSelec = true, bool ViseurOverPos = false, bool Occupe = false, bool Atlitude = true)
        {
            if (WasJustPressed(Key.W))
            {
                destSelec = true;
                destPos = new Vector(coordViseur.X, coordViseur.Y);
                //doMoveUnit(map[depPos.X, depPos.Y].unitOfCell, map[destPos.X, destPos.Y], ListOfUnit);
                UnitTemp = new Object();
            }
        }

        void doMoveUnit(Object unit, int XCell, int YCell)
        {
            if (Contains(MvtPossible, (new Vector(XCell, YCell))))
            {
                
            }
            Reset();
        }

        private bool WasJustPressed(Key button)
        {
            curKey = Keyboard.GetState();
            switch (button)
            {
                case Key.Q:
                    return curKey.IsKeyDown(Keys.Q) && oldKey != curKey;

                case Key.W:
                    return curKey.IsKeyDown(Keys.W) && oldKey != curKey;

                case Key.A:
                    return curKey.IsKeyDown(Keys.A) && oldKey != curKey;

                case Key.Z:
                    return curKey.IsKeyDown(Keys.Z) && oldKey != curKey;

                case Key.LeftControl:
                    return curKey.IsKeyDown(Keys.LeftControl) && oldKey != curKey;

                case Key.R:
                    return curKey.IsKeyDown(Keys.R) && oldKey != curKey;
            }
            oldKey = curKey;
            return false;
        }
    }
}
