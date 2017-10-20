using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Hexagon_Reversi
{
    class PictureBoxItem : PictureBox
    {
        private int indexI; // מיקום השחקן - עמודות
        private int indexJ; // מיקום השחקן - שורות
        private int color;  // צבע השחקן (1 / -1) כחול או סגול

        // הפעולה קובעת את מיקום התמונות
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
        // הפעולה אחראית לתמונה
        public void SetPicture(int color)
        {
            this.color = color;
            string imgFile = Images.GetPicture(color);
            this.Image = null;
            Image im = Image.FromFile(imgFile);
            Image = new Bitmap(im, 42, 42);
        }
        // הפעולה מחזירה או קובעת את ערך מיקום השחקן בשורות
        public int IndexI
        {
            get { return this.indexI; }
            set { this.indexI = value; }
        }
        // הפעולה מחזירה או קובעת את ערך מיקום השחקן בעמודות
        public int IndexJ
        {
            get { return this.indexJ; }
            set { this.indexJ = value; }
        }
        // הפעולה מחזירה את צבע השחקן
        public int GetColor()
        {
            return this.color;
        }
    }
}
