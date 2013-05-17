namespace ArrangeTheNumeralPuzzleClassLibrary
{
    using System;

    public class GameField
    {
        private const int MatrixLength = 4;
        private static readonly Random random = new Random();

        public int[,] Body { get; private set; }

        public int EmptyRow { get; private set; }

        public int EmptyCol { get; private set; }

        public GameField()
        {           
            this.InitializeBody();
        }

        private void InitializeBody()
        {
            this.Body = new int[MatrixLength, MatrixLength];
            this.EmptyRow = MatrixLength - 1;
            this.EmptyCol = MatrixLength - 1;

            int appendedValue = 1;
            for (int i = 0; i < MatrixLength; i++)
            {
                for (int j = 0; j < MatrixLength; j++)
                {
                    this.Body[i, j] = appendedValue;
                    appendedValue++;
                }
            }
            
            this.RandomizeBody();
        }

        private void RandomizeBody()
        {
            int randomMoves = random.Next(4, 5);

            for (int i = 0; i < randomMoves; i++)
            {
                int randomDirection = random.Next(Direction.Row.Length);
                int newRow = this.EmptyRow + Direction.Row[randomDirection];
                int newCol = this.EmptyCol + Direction.Col[randomDirection];

                if (this.IfOutOfMatrix(newRow, newCol))
                {
                    i--;
                    continue;
                }
                else
                {
                    this.MoveEmptyCell(newRow, newCol);
                }
            }

            if (this.CheckIfSolved())
            {
                this.InitializeBody();
            }
        }

        public void MoveEmptyCell(int newRow, int newCol)
        {
            if (newRow > this.EmptyRow)
            {
                throw new ArgumentOutOfRangeException("Invalid movement of the row for the empty cell!");
            }
            else if (newCol > this.EmptyCol)
            {
                throw new ArgumentOutOfRangeException("Invalid movement of the column for the empty cell!");
            }

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
            for (int row = 0; row < MatrixLength; row++)
            {
                Console.Write("|");
                for (int col = 0; col < MatrixLength; col++)
                {
                    if (row != this.EmptyRow || col != this.EmptyCol)
                    {
                        Console.Write("{0,3}", this.Body[row, col]);
                    }
                    else
                    {
                        Console.Write("   ");
                    }

                    if (col == MatrixLength - 1)
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

            for (int row = 0; row < MatrixLength; row++)
            {
                for (int col = 0; col < MatrixLength; col++)
                {
                    if (this.Body[row, col] != solvedMatrix[row, col])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
