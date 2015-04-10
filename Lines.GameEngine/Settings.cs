using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lines.GameEngine
{
    public class Settings
    {
        public static int Width { get; set; }
        public static int Height { get; set; }
        public static int RecomededConsoleScale { get; set; }
        public static int RecomededFormScale { get; set; }
        public static int Score { get; set; }
        public static bool GameOver { get; set; }

        public static string Messege { get; set; }

        public Settings()
        {
            Width = 10;
            Height = 10;
            RecomededFormScale = 50;
            RecomededConsoleScale = 3;
            Score = 0;
            GameOver = false;
            Messege = "";
        }
    }
}
