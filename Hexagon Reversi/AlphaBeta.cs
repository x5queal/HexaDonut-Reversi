using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon_Reversi
{
    // The static class store the BestMove & Iterate functions
    public static class AlphaBeta
    {
        const int MAXPLAYER = -1;
        const int MINPLAYER = 1;

        // Choosing the best move
        public static Index BestMove(LogicBoard lb)
        {
            AlphaBetaBoard b = new AlphaBetaBoard(lb);
            AlphaBetaBoard move = null;
            List<AlphaBetaBoard> children = b.Children();

            foreach (AlphaBetaBoard child in children)
            {
                child.Val = Iterate(child, child.Depth, -999999, 999999);
                if (move == null || child.Val > move.Val)
                    move = child;
            }

            return move.SelectedIndex;
        }
        // Call evaluate funtion if depth = 0 or if someone win
        private static int Iterate(AlphaBetaBoard node, int depth, int alpha, int beta)
        {
            if (depth == 0 || node.CheckWin(node.SelectedIndex.X, node.SelectedIndex.Y) != -10)
            {
                node.Evaluate();
                return node.Val;
            }
            if (node.GetPlayer() == MAXPLAYER)
            {
                foreach (AlphaBetaBoard child in node.Children())
                {
                    alpha = Math.Max(alpha, Iterate(child, depth - 1, alpha, beta));
                    if (beta < alpha)
                        break;  // Alpha Beta Pruning
                }
                return alpha;
            }
            else
            {
                foreach (AlphaBetaBoard child in node.Children())
                {
                    beta = Math.Min(beta, Iterate(child, depth - 1, alpha, beta));
                    if (beta < alpha)
                        break; // Alpha Beta Pruning
                }
                return beta;
            }
        }
    }
}
