using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lines.GameEngine
{
    public static class Settings
    {
        public static int RecomededConsoleScale;
        public static int RecomededFormScale;

        public static string Messege;

        static Settings()
        {
            RecomededFormScale = 50;
            RecomededConsoleScale = 3;
            Messege = "";
        }
    }
}
