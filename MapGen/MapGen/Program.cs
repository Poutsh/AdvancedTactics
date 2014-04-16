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
                game.Run();
            }
        }
    }
#endif
}

