using System;

namespace MinesweeperGUI
{
    public class Board
    {
        private int height;
        private int width;
        private int minesCount;
        private Cell[,] cells;
        private bool isFirstReveal;
        private int fieldsToReveal;
        
        public int FieldsToReveal => fieldsToReveal;
        

        public Board(int minesCount, int height, int width)
        {
            fieldsToReveal = height * width - minesCount;
            isFirstReveal = false;
            this.minesCount = minesCount;
            cells = new Cell[height, width];
            this.height = height;
            this.width = width;

            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    cells[row, col] = new Cell();
                }
            }
        }

        private void GenerateMines(int row, int col)
        {
            Random szansa = new Random();
            for (int mines = 0; mines < minesCount; mines++)
            {
                int tempHeight, tempWidth;
                do
                {
                    tempHeight = szansa.Next(0, height);
                    tempWidth = szansa.Next(0, width);
                } while ((tempHeight == row && tempWidth == col) ||
                         (tempHeight == (row - 1) && tempWidth == col) ||
                         (tempHeight == (row + 1) && tempWidth == col) ||
                         (tempHeight == (row) && tempWidth == (col - 1)) ||
                         (tempHeight == (row) && tempWidth == (col + 1)) ||
                         (tempHeight == (row - 1) && tempWidth == (col + 1)) ||
                         (tempHeight == (row + 1) && tempWidth == (col + 1)) ||
                         (tempHeight == (row - 1) && tempWidth == (col - 1)) ||
                         (tempHeight == (row + 1) && tempWidth == (col - 1)) ||
                         hasMine(tempHeight, tempWidth));

                setMine(tempHeight, tempWidth);
            }
        }
        public void RevealCell(int row, int col)
        {
            
            if (isFirstReveal == false)
            {
                GenerateMines(row, col);
                isFirstReveal = true;
            }

            if (row >= 0 && row < height && col >= 0 && col < width)
            {
                if (!cells[row, col].hasMine && !cells[row, col].isRevealed)
                {
                    cells[row, col].isRevealed = true;
                    fieldsToReveal--;
                    int mineCounter = GetMinesAround(row, col);
                    if (mineCounter == 0)
                    {
                        RevealCell(row - 1, col);
                        RevealCell(row + 1, col);
                        RevealCell(row, col - 1);
                        RevealCell(row, col + 1);
                        RevealCell(row - 1, col - 1);
                        RevealCell(row + 1, col + 1);
                        RevealCell(row - 1, col + 1);
                        RevealCell(row + 1, col - 1);
                    }
                }
            }
        }

        public void ToggleFlag(int row, int col)
        {
            if (cells[row, col].hasFlag)
            {
                cells[row, col].hasFlag = false;
            }
            else
                cells[row, col].hasFlag = true;
        }

        public bool hasFlag(int row, int col)
        {
            return cells[row, col].hasFlag;
        }

        public bool hasMine(int row, int col)
        {
            return cells[row, col].hasMine;
        }

        public bool isRevealed(int row, int col)
        {
            return cells[row, col].isRevealed;
        }
        public int GetMinesAround(int row, int col)
        {
            int minesNext = 0;
            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    int nx = row + dx;
                    int ny = col + dy;
                    if (nx >= 0 && nx < height && ny >= 0 && ny < width && hasMine(nx, ny))
                    {
                        minesNext++;
                    }
                }
            }

            return minesNext;
        }

        // public void PrintBoard()
        // {
        //     for (int i = 0; i < width; i++)
        //     {
        //         for (int j = 0; j < height; j++)
        //         {
        //             if ((cells[i, j].isRevealed) && (cells[i, j].hasMine))
        //             {
        //                 Console.Write("M  ");
        //             }
        //             else if ((cells[i, j].isRevealed) && (!cells[i, j].hasMine))
        //             {
        //                 int minesNext = GetMinesAround(i, j);
        //                 Console.Write(minesNext + "  ");
        //             }
        //             else if ((!cells[i, j].isRevealed) && (cells[i, j].hasFlag))
        //             {
        //                 Console.Write("F  ");
        //             }
        //             else
        //             {
        //                 Console.Write("X  ");
        //             }
        //         }
        //
        //         Console.WriteLine("");
        //     }
        // }

        private void setMine(int row, int col)
        {
            cells[row, col].hasMine = true;
        }
    }

    public class Cell
    {
        public Cell()
        {
            hasMine = false;
            hasFlag = false;
            isRevealed = false;
        }

        public bool hasMine = false;
        public bool hasFlag = false;
        public bool isRevealed = false;
    }
}