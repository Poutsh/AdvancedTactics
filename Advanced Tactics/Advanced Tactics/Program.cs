using System;
using System.Linq;
using System.IO;
using Microsoft.Xna.Framework;
using System.Windows.Forms;

namespace Advanced_Tactics
{
#if WINDOWS
    static class Program
    {
       static void Main(string[] args)
        {
            using (Game1 game = new Game1())
            {
                Control control = Form.FromHandle(game.Window.Handle);
                Form form = control.FindForm();
                form.MinimizeBox = false;
                form.MaximizeBox = false;
                game.Run();
            }
        }
    }
#endif
}

