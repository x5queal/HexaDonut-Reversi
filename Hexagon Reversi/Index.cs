using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon_Reversi
{
    // המחלקה אחראית למיקום הלוח הנוכחי של המחשב
    public class Index
    {
        private int x;
        private int y;

        // פעולה בונה
        public Index(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        // גט וסט של איקס
        public int X
        {
            get { return x; }
            set { x = value; }
        }
        // גט וסט של ויי
        public int Y
        {
            get { return y; }
            set { y = value; }
        }
    }
}
