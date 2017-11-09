using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon_Reversi
{
    // The class store variables and functions that represent the game parts and rules
    public class LogicBoard
    {
        protected int[,]      board;        // Board of the game 
        protected int         player;       // Player value - 1 / 0 
        protected int         countPlayer;  // Counter for the player donuts
        protected int         countOpp;     // Counter for the AI donuts

        // Constructor - build the board and place the donuts in the start position
        public LogicBoard()
        {
            player = 1;
            board = new int[9, 9];
            for (int i = 0; i < board.GetLength(0); i++)
                for (int j = 0; j < board.GetLength(1); j++)
                    if (i + j < 4 || i + j > 12)
                        board[i, j] = -10;
                    else
                        board[i, j] = 0;
            board[3, 4] = 1;
            board[4, 5] = 1;
            board[5, 3] = 1;
            board[4, 2] = 1;
            board[3, 5] = -1;
            board[4, 3] = -1;
            board[5, 4] = -1;
            board[4, 6] = -1;
            board[4, 4] = -10;
            countPlayer = 4;
            countOpp = 4;
        }   
        // Return the player value
        public int GetPlayer()
        {
            return player;
        }
        // Set the player value
        public void SetPlayer(int player)
        {
            this.player = player;
        }
        // Return the board
        public int[,] GetBoard()
        {
            return board;
        }
        // Set value for the index of the board
        public void SetBoard(int x, int y, int color)
        {
            this.board[x, y] = color;
        }
        // Check if the current move is legal (using the CheckDirection function)
        public bool CheckMove(int x, int y)
        {
            int[] direction = { -1, 0, 1 };
            foreach (int d1 in direction)
                foreach (int d2 in direction)
                    if (d1 != d2 && CheckDirection(x, y, d1, d2)) return true;
            return false;

        }
        // Check if the given direction is legal for the current move
        public bool CheckDirection(int x, int y, int dn1, int dn2)
        {
            bool flag = false;
            if (DoesInBoard(x + dn1, y + dn2) && board[x, y] == 0 && board[x + dn1, y + dn2] == player * -1)    {
                x += dn1;
                y += dn2;
                while (DoesInBoard(x, y) && board[x, y] == player * -1) {
                    x += dn1;
                    y += dn2;
                }
                if (DoesInBoard(x, y) && board[x, y] == player)
                    flag = true;
            }
            return flag;
        }
        // Change the index, donuts values, counters
        public void DoMove(int x, int y)
        {
            int count = 0;  

            int tempX, tempY;
            for (int i = -1; i < 2; i++)
                for (int j = -1; j < 2; j++)
                {
                    if (i != j && CheckDirection(x, y, i, j))
                    {
                        tempX = x + i;
                        tempY = y + j;
                        while (DoesInBoard(tempX, tempY) && this.board[tempX, tempY] != player) {
                            this.board[tempX, tempY] = player;
                            tempX += i;
                            tempY += j;
                            if (player == 1)
                            {
                                countPlayer++;
                                countOpp--;
                            }
                            else
                            {
                                countOpp++;
                                countPlayer--;
                            }
                        }
                    }
                }
            this.board[x, y] = this.player;
            count++;
            UpdateCount(player, count);
            UpdateCount(player * -1, count - 1);
            player *= -1;
        }
        private void UpdateCount(int player, int count)
        {
            if (player == 1) countPlayer += count;
            else countOpp += count;
        }
        public int GetCount(int player)
        {
            if (player == 1) return countPlayer;
            else return countOpp;
        }
        // Check if the index is in the game board
        public bool DoesInBoard(int x, int y)
        {
            if (x >= 0 && x < 9 && y >= 0 && y < 9 && this.board[x, y] != -10
               && (x + y > 3 && x + y < 13 && (x != 4 || y != 4) ))
                return true;
            return false;
        }
        // Check if the current player have legal moves
        public bool DoesHaveMoves(int x, int y)
        {
            for (int i = 0; i < board.GetLength(0); i++)
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (CheckMove(i, j))
                        return true;
                }
            return false;
        }
        // Check if there's legal moves in both sides
        public bool NoMovesAtAll(int x, int y)
        {
            if (!DoesHaveMoves(x, y))
            {
                this.player *= -1;
                if(!DoesHaveMoves(x, y))
                {
                    this.player *= -1;
                    return true;
                }
                this.player *= -1;
            }
            return false;
        }
        // Check if there's win / tie
        public int CheckWin(int x, int y)   
        { 
            if (NoMovesAtAll(x, y))
            {
                if (countPlayer > countOpp)
                    return 1;   // Blue win
                if (countPlayer < countOpp)
                    return -1;  // Purple win
                if (countPlayer == countOpp)
                    return 0;   // Tie
            }
            return -10;
        }
    }
}
