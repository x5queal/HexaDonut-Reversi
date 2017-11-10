using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hexagon_Reversi
{
    // The start menu - let the user choose game mode (Player VS Player or Player VS CPU)
    public partial class Menu : Form
    {
        private bool pvp;
        
        public Menu()
        {
            InitializeComponent();
        }

        // Player VS CPU button
        private void button1_Click(object sender, EventArgs e)
        {
            pvp = false;
            this.Close();
        }
        // Player VS Player button
        private void button2_Click(object sender, EventArgs e)
        {
            pvp = true;
            this.Close();
        }
        // Get
        public bool GetPvP()
        {
            return pvp;
        }
    }
}
