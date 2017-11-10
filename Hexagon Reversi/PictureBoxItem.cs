using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Hexagon_Reversi
{
    // Inheritance from PictureBox - this class allowing use of index as individual
    class PictureBoxItem : PictureBox
    {
        private int indexI; // Player index - columns
        private int indexJ; // Player index - rows
        private int color;  // Player color (1 / -1) - Blue / Purple

        // Constructor - Set the images size, location and type
        public PictureBoxItem(int indexI, int indexJ, int color)
        : base() 
        {
            this.indexI = indexI;
            this.indexJ = indexJ;
            int y = indexI * 68 + 28 ;
            int x = indexJ * 79 - 138 + indexI * 40;
            Location = new System.Drawing.Point(x, y);
            Size = new System.Drawing.Size(48, 48);
            BackColor = System.Drawing.Color.Transparent;
            SetPicture(color);
        }
        // Set the correct image
        public void SetPicture(int color)
        {
            this.color = color;
            string imgFile = Images.GetPicture(color);
            this.Image = null;
            Image im = Image.FromFile(imgFile);
            Image = new Bitmap(im, 42, 42);
        }
        // Gets & Sets
        public int IndexI
        {
            get { return this.indexI; }
            set { this.indexI = value; }
        }
        public int IndexJ
        {
            get { return this.indexJ; }
            set { this.indexJ = value; }
        }
        public int GetColor()
        {
            return this.color;
        }
    }
}
