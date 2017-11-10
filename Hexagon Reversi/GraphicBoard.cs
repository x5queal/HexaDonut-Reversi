using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace Hexagon_Reversi
{
    // The graphic class - update the visual part
    public partial class GraphicBoard : Form
    {
        private LogicBoard        lb;        // Logicboard Object - 
        private PictureBoxItem[,] colors;    // Image of the player color - cyan or purple
        private bool              option;    // Game mode: Player VS CPU or Player VS Player

        public GraphicBoard(bool option)
        {
            this.option = option;
            InitializeComponent();
            button2.Enabled = false;
        }
        // Button to start the game - build the graphic board
        private void button1_Click_1(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = true;
            lb = new LogicBoard();
            colors = new PictureBoxItem[9,9];
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    if (i + j > 3 && i + j < 13 && (i != 4 || j != 4))
                    {
                        colors[i, j] = new PictureBoxItem(i, j, 0);
                        Controls.Add(colors[i, j]);
                        colors[i, j].Click += new EventHandler(this.HexClick);
                    }
            colors[3, 4].SetPicture(1);
            colors[4, 5].SetPicture(1);
            colors[5, 3].SetPicture(1);
            colors[4, 2].SetPicture(1);
            colors[3, 5].SetPicture(-1);
            colors[4, 3].SetPicture(-1);
            colors[5, 4].SetPicture(-1);
            colors[4, 6].SetPicture(-1);
        }
        // Button to restart the game.
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
        // The function return the Logicboard object
        public LogicBoard GetLogicBoard()
        {
            return lb;
        }
        // The function summoned when the user click on a board cell
        private void HexClick(object sender, EventArgs n)
        {
            PictureBoxItem color = (PictureBoxItem)sender;
            int i = color.IndexI, j = color.IndexJ;
            if (lb.CheckMove(i, j))
            {
                lb.DoMove(i, j);
                colors[i, j].SetPicture(lb.GetBoard()[i, j]);
                UpdateColors(i, j);
                PlayWaterDropSound();
            }
            else
            {
                MessageBox.Show("Illegal Move.");
            }
            if (lb.NoMovesAtAll(i, j))
            {
                if (lb.CheckWin(i, j) == 1)
                    if (option)
                        MessageBox.Show("Blue wins.");
                    else
                        MessageBox.Show("You won the CPU, well done!");
                if (lb.CheckWin(i, j) == -1)
                    if (option)
                        MessageBox.Show("Purple wins.");
                    else
                        MessageBox.Show("The CPU won the game.");
                if (lb.CheckWin(i, j) == 0)
                    MessageBox.Show("Tie!");
            }
            else if (!lb.DoesHaveMoves(i, j))
            {
                if (option)
                    if (lb.GetPlayer() == 1)
                        MessageBox.Show("Purple has no possible moves - another blue turn.");
                    else
                        MessageBox.Show("Blue has no possible moves - another purple turn.");
                else
                    MessageBox.Show("CPU has no possible moves - you get another turn.");
                lb.SetPlayer(lb.GetPlayer() * -1);
            }
            else
            {
                // Label - turn text 
                if (!option)
                {
                    label1.Left = 768;
                    label1.Text = "CPU turn";
                    label1.ForeColor = Color.FromArgb(155, 89, 182);
                    this.Refresh();
                    System.Threading.Thread.Sleep(1300);
                    PlayComputerTurn();
                    label1.Left = 766;
                    label1.Text = "Your turn";
                    label1.ForeColor = Color.FromArgb(0, 220, 225);
                }
                else
                {
                    if (lb.GetPlayer() == 1)
                    {
                        label1.Left = 770;
                        label1.Text = "Blue turn";
                        label1.ForeColor = Color.FromArgb(0, 220, 225);
                    }
                    else
                    {
                        label1.Left = 765;
                        label1.Text = "Purple turn";
                        label1.ForeColor = Color.FromArgb(155, 89, 182);
                    }
                }
            }
            NumberOfBlue.Text = "" + lb.GetCount(1);
            NumberOfPurple.Text = "" + lb.GetCount(-1);


        }
        // The function do the CPU move
        public void PlayComputerTurn()
        {
            if (lb.GetPlayer() == -1)
            {
                Index move = AlphaBeta.BestMove(this.lb);
                lb.DoMove(move.X, move.Y);
                colors[move.X, move.Y].SetPicture(lb.GetBoard()[move.X, move.Y]);
                UpdateColors(move.X, move.Y);
                if (lb.NoMovesAtAll(move.X, move.Y))
                {
                    if (lb.CheckWin(move.X, move.Y) == 1)
                        MessageBox.Show("You won the CPU, well done!");
                    if (lb.CheckWin(move.X, move.Y) == -1)
                        MessageBox.Show("The CPU won the game.");
                    if (lb.CheckWin(move.X, move.Y) == 0)
                        MessageBox.Show("Tie!");
                }
                else if (!lb.DoesHaveMoves(move.X, move.Y))
                {
                    MessageBox.Show("You don't have any possible moves, another CPU turn.");
                    NumberOfBlue.Text = "" + lb.GetCount(1);
                    NumberOfPurple.Text = "" + lb.GetCount(-1);
                    lb.SetPlayer(lb.GetPlayer() * -1);
                    this.Refresh();
                    System.Threading.Thread.Sleep(1300);
                    PlayComputerTurn();
                }
                NumberOfBlue.Text = "" + lb.GetCount(1);
                NumberOfPurple.Text = "" + lb.GetCount(-1);
            }
        }
        // Update grpahic changes
        private void UpdateColors(int x, int y)
        {
            UpdateDirection(x, y, 0, -1);
            UpdateDirection(x, y, 0, 1);
            UpdateDirection(x, y, 1, -1);
            UpdateDirection(x, y, 1, 0);
            UpdateDirection(x, y, -1, 1);
            UpdateDirection(x, y, -1, 0);
        }
        private void UpdateDirection(int x, int y, int dn1, int dn2)
        {
            x += dn1;
            y += dn2;
            while (lb.DoesInBoard(x, y) && lb.GetBoard()[x, y] != colors[x, y].GetColor())
            {
                colors[x, y].SetPicture(lb.GetBoard()[x, y]);
                x += dn1;
                y += dn2;
            }
        }
        // Play audio then the player do a move (require a working 'waterdrop.wav' file)
        private void PlayWaterDropSound()
        {
            SoundPlayer wd = new SoundPlayer(@"..\\..\\Sounds\\waterdrop.wav");
            wd.Play();
        }
        private void label1_Click(object sender, EventArgs e)
        {
           
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
