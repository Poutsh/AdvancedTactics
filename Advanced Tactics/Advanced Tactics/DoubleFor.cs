using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advanced_Tactics
{
    struct DoubleFor
    {
        public int I, J;

        public DoubleFor(int value)
        {
            I = value;
            J = value;
        }
        public DoubleFor(float value)
        {
            I = (int)value;
            J = (int)value;
        }
        public DoubleFor(int i, int j)
        {
            I = i;
            J = j;
        }
        public DoubleFor(float i, float j)
        {
            I = (int)i;
            J = (int)j;
        }
        public DoubleFor(int i, float j)
        {
            I = i;
            J = (int)j;
        }
        public DoubleFor(float i, int j)
        {
            I = (int)i;
            J = (int)j;
        }
        
    }
}
