using System;

namespace CreateDBackClone
{
    public static class Program
    {
        private const int SCREEN_WIDTH = 1000;
        private const int SCREEN_HEIGHT = 600;

        [STAThread]
        static void Main()
        {
            using (var game = new Game1(SCREEN_WIDTH, SCREEN_HEIGHT))
                game.Run();
        }
    }
}
