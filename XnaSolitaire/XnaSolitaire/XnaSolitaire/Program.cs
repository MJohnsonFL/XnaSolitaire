using System;

namespace XnaSolitaire
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (XnaSolitaire game = new XnaSolitaire())
            {
                game.Run();
            }
        }
    }
#endif
}

