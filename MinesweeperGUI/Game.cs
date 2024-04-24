using System;

namespace MinesweeperGUI
{
    public class Game
    {
        public Board board;
        
        public bool EmptyCell(int x, int y)
        {
            if (board.isRevealed(x, y) )
                return true;
            return false;
        }

        public int getMines(int x, int y)
        {
            return board.GetMinesAround(x, y);
        }

        public Game(int size, int mines)
        {
            board = new Board(size, mines);
        }

        public void ExecuteReveal(int x, int y)
        {
            board.RevealCell(x, y);
           // board.PrintBoard();
        }





        


        //click button w xaml.cs
        
    }
}