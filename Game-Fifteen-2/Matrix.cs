using System;

namespace GameFifteen
{
    public class Matrix
    {
        private const int MatrixLength = 4;
        private static readonly Random random = new Random();

        public int[,] Body { get; private set; }
        public int EmptyRow { get; set; }
        public int EmptyCol { get; set; }

        public Matrix()
        {
            this.EmptyRow = MatrixLength - 1;
            this.EmptyCol = MatrixLength - 1;
            InitializeBody();
        }

        private void InitializeBody()
        {
            this.Body = new int[MatrixLength, MatrixLength];
            int appendedValue = 1;
            for (int i = 0; i < MatrixLength; i++)
            {
                for (int j = 0; j < MatrixLength; j++)
                {
                    this.Body[i, j] = appendedValue;
                    appendedValue++;
                }
            }

            RandomizeBody();
        }

        private void RandomizeBody()
        {
            int randomMoves = random.Next(4, 5);

            for (int i = 0; i < randomMoves; i++)
            {
                int randomDirection = random.Next(Direction.Row.Length);
                int newRow = this.EmptyRow + Direction.Row[randomDirection];
                int newCol = this.EmptyCol + Direction.Col[randomDirection];

                if (IfOutOfMatrix(newRow, newCol))
                {
                    i--;
                    continue;
                }
                else
                {
                    MoveEmptyCell(newRow, newCol);
                }
            }

            if (CheckIfSolved())
            {
                InitializeBody();
            }
        }

        public void MoveEmptyCell(int newRow, int newCol)
        {
            int swapValue = this.Body[newRow, newCol];
            this.Body[newRow, newCol] = this.Body[this.EmptyRow, this.EmptyCol];
            this.Body[this.EmptyRow, this.EmptyCol] = swapValue;
            this.EmptyRow = newRow;
            this.EmptyCol = newCol;
        }

        public bool IfOutOfMatrix(int row, int col)
        {
            if (row >= MatrixLength || row < 0 || col < 0 || col >= MatrixLength)
            {
                return true;
            }

            return false;
        }

        public void Print()
        {
            Console.WriteLine(" -------------");
            for (int i = 0; i < MatrixLength; i++)
            {
                Console.Write("|");
                for (int j = 0; j < MatrixLength; j++)
                {
                    if (i != this.EmptyRow || j != this.EmptyCol)
                    {
                        Console.Write("{0,3}", this.Body[i, j]);
                    }
                    else
                    {
                        Console.Write("   ");
                    }

                    if (j == MatrixLength - 1)
                    {
                        Console.Write(" |\n");
                    }
                }
            }

            Console.WriteLine(" -------------");
        }

        public bool CheckIfSolved()
        {
            int[,] solvedMatrix = new int[MatrixLength, MatrixLength]
            { 
                { 1, 2, 3, 4 }, 
                { 5, 6, 7, 8 }, 
                { 9, 10, 11, 12 }, 
                { 13, 14, 15, 16 }
            };

            for (int i = 0; i < MatrixLength; i++)
            {
                for (int j = 0; j < MatrixLength; j++)
                {
                    if (this.Body[i, j] != solvedMatrix[i, j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
