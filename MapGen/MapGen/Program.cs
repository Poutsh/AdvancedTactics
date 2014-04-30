using System;

namespace MapGenerator
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (MapGen game = new MapGen())
            {
                System.Windows.Forms.Control control = System.Windows.Forms.Form.FromHandle(game.Window.Handle);
                System.Windows.Forms.Form form = control.FindForm();
                form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                game.Run();
            }
        }
    }
#endif
}

