using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advanced_Tactics
{
    class Case
    {
        private int pos_x;
        private int pos_y;
        private Unit present_unit;
        private int obstacle;
        private bool overline;
        private bool mvt_is_possible;

<<<<<<< HEAD
        public static void Casei(int pos_x, int pos_y, int obstacle)
=======
        //probleme constructeur
        public case_constr(int x, int y, int obs)
>>>>>>> 55
        {
            pos_x = x;
            pos_y = y;
            obstacle = obs;
            //level of obstable (-1 - none)(0 - terre) (1- montagne) (2 - eau)
            
        }
    }
}
