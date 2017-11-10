using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hexagon_Reversi
{
    // The class create a tree sturcture of game boards ; Inheritance from LogicBoard.cs
    public class AlphaBetaBoard : LogicBoard
    {
        private LogicBoard parent;
        private int depth;
        private int val;
        private Index selectedIndex;
        // Matrix with number in every index that represent the value of the index (based on reversi game strategy)
        static int[,] posValue = {  {0,0,0,0,9,2,6,2,9},
                                    {0,0,0,2,1,2,2,1,2},
                                    {0,0,6,2,4,3,4,2,6},
                                    {0,2,2,3,3,3,3,2,2},
                                    {9,1,4,3,3,3,4,1,9},
                                    {2,2,3,3,3,3,2,2,0},
                                    {6,2,4,3,4,2,6,0,0},
                                    {2,1,2,2,1,2,0,0,0},
                                    {9,2,6,2,9,0,0,0,0}};

        // Constructor - Build the root of the tree
        public AlphaBetaBoard(LogicBoard b) : base()
        {
            this.parent = null;
            this.depth = 5;
            this.player = b.GetPlayer();
            this.CopyBoardData(b.GetBoard());
        }
        // Constructor(2) - Build the tree nodes
        public AlphaBetaBoard(AlphaBetaBoard b) : base()
        {
            this.parent = b;
            this.depth = b.depth -1;
            this.player = b.GetPlayer();
            this.CopyBoardData(b.GetBoard());
        }
        // Copy the given board information (number of players & their type)
        public void CopyBoardData(int[,] b)
        {
            if (this.parent != null)
            {
                int countB, countP;
                countB = this.parent.GetCount(1);
                countP = this.parent.GetCount(-1);
            }
            UpdateCurrentBoard(b);
        }
        // Update the given board
        public void UpdateCurrentBoard(int[,] board)
        {
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    this.SetBoard(i, j, board[i, j]);
        }
        // Create child node
        public List<AlphaBetaBoard> Children()
        {
            List<AlphaBetaBoard> lst = new List<AlphaBetaBoard>();
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    if (this.DoesInBoard(i,j) && this.CheckMove(i,j)) {
                        AlphaBetaBoard son = new AlphaBetaBoard(this);
                        son.DoMove(i, j);
                        son.selectedIndex = new Index(i, j);
                        lst.Add(son);
                        son.player *= -1;
                    }
            return lst;
        }
        // Evaluate funtion of the game - (the number of availbe enemy moves) / (value of the current board index)
        public void Evaluate()
        {
            double val = 1000*
                ((double)CountOppMoves() / posValue[this.SelectedIndex.X, this.SelectedIndex.Y]);
            this.val = (int)(Math.Round(val)) * this.parent.GetPlayer();
        }
        // Return the number of possible moves
        public int CountOppMoves()
        {
            int count = 0;
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    if (this.DoesInBoard(i,j) && this.CheckMove(i, j))
                        count++;
            return count;
        }
        // Gets & Sets
        public LogicBoard Parent
        {
            get { return parent; }
            set { parent = value; }
        }
        public int Depth
        {
            get { return depth; }
            set { depth = value; }
        }
        public int Val
        {
            get { return val; }
            set { val = value; }
        }
        public Index SelectedIndex
        {
            get { return selectedIndex; }
            set { selectedIndex = value; }
        }
    }
}
