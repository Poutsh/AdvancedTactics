using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdvancedLibrary;
using Microsoft.Xna.Framework;

namespace Advanced_Tactics
{
    public class Unit
    {
        /* -- ID -- */
        private string _classe;
        private string _rang;
        private int _lvl;
        private string _up;
        private Sprite _spriteunit;
        //
        public string Classe { get { return _classe; } }
        public string Rang { get { return _rang; } }
        public string Upgrade { get { return _up; } }
        public int Lvl { get { return _lvl; } }
        public Sprite SpriteUnit { get { return _spriteunit; } }


        /*  -- Stats -- */
        private int _pv_max;
        private int _pv_cur;
        private int _force;
        private int _pa;
        //
        public int maxPV { get { return _pv_max; } }
        public int curPV { get { return _pv_cur; } }
        public int Force { get { return _force; } }
        public int PA { get { return _pa; } }

        /* -- Actions -- */
        private List<int> _terrainpossible = new List<int> { 0, 1, 2, 3 };

        enum classeUnit { Roi, Dame, Tour, Fou, Cavalier, Pion }
        enum rangUnit { HQ, Truck, Ing, Doc, AA, Plane, Tank, Com, Pvt }

        public Unit(string classe, string rang, int lvl, string upgrade, int pv_max, int pv_cur, int force, int pa)
        {
            _classe = classe;
            _rang = rang;
            _lvl = lvl;
            _up = upgrade;
            _pv_max = pv_max;
            _pv_cur = pv_cur;
            _pa = pa;
        }

        void Sprite()
        {

        }
    }
}
