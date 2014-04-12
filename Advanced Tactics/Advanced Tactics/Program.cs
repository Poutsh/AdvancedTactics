using System;
using System.Linq;
using System.IO;
using Microsoft.Xna.Framework;

namespace Advanced_Tactics
{
#if WINDOWS
    static class Program
    {
       static void Main(string[] args)
        {
            using (Game1 game = new Game1())
            {
                game.Run();
            }
        }
    }
#endif
}

