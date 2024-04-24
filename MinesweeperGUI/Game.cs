using System;

namespace MinesweeperGUI
{
    public class Game
    {
        public Board board;

        public bool EmptyCell(int row, int col)
        {
            if (board.isRevealed(row, col))
                return true;
            return false;
        }

        public int getMines(int row, int col)
        {
            return board.GetMinesAround(row, col);
        }

        public Game(int mines, int width, int height)
        {
            board = new Board(mines, width, height);
        }

        public void ExecuteReveal(int row, int col)
        {
            board.RevealCell(row, col);
            // board.PrintBoard();
        }
    }
}