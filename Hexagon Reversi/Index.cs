using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon_Reversi
{
    // The class store and manage the CPU current index on the board
    public class Index
    {
        private int x;
        private int y;

        // Constructor
        public Index(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        // Gets & Sets
        public int X
        {
            get { return x; }
            set { x = value; }
        }
        public int Y
        {
            get { return y; }
            set { y = value; }
        }
    }
}
