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


        //probleme constructeur
        public void case_constr(int x, int y, int obs)

        {
            pos_x = x;
            pos_y = y;
            obstacle = obs;
            //level of obstable (-1 - none)(0 - terre) (1- montagne) (2 - eau)
            
        }
    }
}
