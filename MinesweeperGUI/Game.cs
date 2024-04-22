using System;

namespace MinesweeperGUI
{
    public class Game
    {
        public Board board;

        public bool EmptyCell(int x, int y)
        {
            if (board.isRevealed(x, y) && (board.GetMinesAround(x, y) == 0))
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
            board.PrintBoard();
        }


        public bool gameOver()
        {
            return board.clickedMine;
        }

        public void Move()
        {
            board.PrintBoard();
            (int y, int x, int action) = PlayerInput();

            if (IsActionReveal(action))
            {
                if (board.hasMine(x, y))
                {
                    board.clickedMine = true;
                }

                board.RevealCell(x, y);
            }
            else if (IsActionToggle(action))
            {
                board.ToggleFlag(x, y);
            }

            Console.WriteLine("");
        }

        private (int y, int x, int action) PlayerInput()
        {
            return ConsoleTypingMove();
        }

        private (int y, int x, int action) ConsoleTypingMove()
        {
            Console.WriteLine("Choose the x coordinate to alter (starting with 1)");
            int y = (Convert.ToInt32(Console.ReadLine()) - 1);
            Console.WriteLine("Choose the y coordinate to alter (starting with 1)");
            int x = (Convert.ToInt32(Console.ReadLine()) - 1);
            Console.WriteLine("Choose the action to perform: 1. Reveal 2. Flag");
            int action = Convert.ToInt32(Console.ReadLine());

            return (y, x, action);
        }


        // private int ConsoleTypingFirstMove()
        // {
        //     Console.WriteLine("It's your first move, so it'd be silly to make a flag!. Type 1 to reveal.");
        //     int action = Convert.ToInt32(Console.ReadLine());
        //     return action;
        //
        // }

        // private int PlayerInputFirstMove()
        // {
        //     return ConsoleTypingFirstMove();
        // }

        private int PlayerFirstInputCheck()
        {
            return ConsoleTypingFirstMoveCheck();
        }

        private int ConsoleTypingFirstMoveCheck()
        {
            Console.WriteLine(":( It won't work until you type 1)");
            int action = Convert.ToInt32(Console.ReadLine());
            return action;
        }

        private bool IsActionToggle(int action)
        {
            if (action == 2)
            {
                return true;
            }

            return false;
        }

        private bool IsActionReveal(int action)
        {
            if (action == 1)
            {
                return true;
            }

            return false;
        }


        private void GameGoing()
        {
            while (!gameOver() && !board.gameWon)
            {
                Move();
                board.allNonMinesRevealed();
            }

            if (gameOver())
            {
                board.RevealAllCells();
                board.PrintBoard();
                Console.WriteLine("You detonated a mine! Game over!");
                return;
            }
            else if (board.gameWon)
                board.PrintBoard();
        }

        private void FirstBoard(int x)
        {
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    Console.Write("X  ");
                }

                Console.WriteLine("");
            }
        }

        public (int x, int y) MakeSize()
        {
            (int x, int y) = PlayerInputSize();
            return (x, y);
        }

        public (int x, int y) PlayerInputSize()
        {
            (int x, int y) = ConsoleTypingSize();
            return (x, y);
        }

        public (int x, int y) ConsoleTypingSize()
        {
            Console.WriteLine("Choose the size of your board)");
            int x = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Choose the number of mines)");
            int y = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Game has started");
            return (x, y);
        }


        //click button w xaml.cs

        public (int, int) MakeAFirstMove()
        {
            Console.WriteLine("Choose the x coordinate to alter (starting with 1)");
            int b = (Convert.ToInt32(Console.ReadLine()) - 1);
            Console.WriteLine("Choose the y coordinate to alter (starting with 1)");
            int a = (Convert.ToInt32(Console.ReadLine()) - 1);
            return (a, b);
        }
    }
}