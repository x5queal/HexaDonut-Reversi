using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hexagon_Reversi
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Menu menu = new Menu();
            Application.Run(menu);
            Application.Run(new GraphicBoard(menu.GetPvP()));
        }
    }
}
