using System;
using System.Collections.Generic;
using System.Text;

namespace Inlamningsuppgift2
{
    /// <summary>
    /// Contains generic game info such as game version etc.
    /// </summary>
    static class GameInfo
    {
        private static string name = "CyberSnake 2077";
        private static string version = "1.4";
        private static string author = "Christopher Dahlborg";

        public static string Name { get => name; }
        public static string Version { get => version; }
        public static string Author { get => author; }
    }
}
