using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advanced_Tactics
{
    public class Case
    {
        private int pos_x;
        public int x_case
        {
            get { return pos_x; }
            set { pos_x = value; }
        }
        private int pos_y;
        public int y_case
        {
            get { return pos_y; }
            set { pos_y = value; }
        }
        private Unit present_unit;
        private int obstacle;
        public int obs
        {
            get { return obstacle; }
            set { obstacle = value; }
        }
        private bool overline;
        private bool mvt_is_possible;
        public bool mvt
        {
            get { return mvt_is_possible; }
            set { mvt_is_possible = value; }
        }


        
        public Case(int x, int y, int obs)
        {
            pos_x = x;
            pos_y = y;
            obstacle = obs;
            //level of obstable (-1 - none)(0 - terre) (1- montagne) (2 - eau)
        }

        public void over(bool mvt_is_possible)
        {
            if (mvt_is_possible)
                overline = true;
        }
    }
}
