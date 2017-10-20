using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon_Reversi
{
    // המחלקה אחראית על ניהול תמונות המשחק
    public static class Images
    {
        private static string begStr = "..\\..\\Images\\";
        private static string endStr = ".png";
        private static string blue = "Blue Donut";
        private static string purple = "Purple Donut";
        private static string empty = "Empty";

        // הפעולה תחזיר את התמונה המתאימה
        public static string GetPicture(int color)
        {
            string str = begStr;
            if (color == 0)
                str += empty;
          
            if (color == 1)
                str += blue;
            else
                if (color == -1)
                str += purple;
            str += endStr;
            return str;
        }
    }
}
