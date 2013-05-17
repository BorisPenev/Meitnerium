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


        /// <summary>
        /// Here we initialize the logic for the body of the matrix like game
        /// and we randomize the number in the matrix.
        /// </summary>
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

        /// <summary>
        /// Here we randomize the starting position of the empty cell,
        /// and here we can control the complexity of the game by variable: randomMoves.
        /// </summary>
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

        /// <summary>
        /// Here we move and check the empty cell if its out of the range of the gamefield. 
        /// </summary>
        /// <param name="newRow">This is the position of the new row.</param>
        /// <param name="newCol">This is the position of the new column.</param>
        public void MoveEmptyCell(int newRow, int newCol)
        {
            if (newRow >= MatrixLength || newRow < 0 || newCol >= MatrixLength || newCol < 0)
            {
                throw new IndexOutOfRangeException("Empty cell cannot be moved outside the game field.");                
            }

            int swapValue = this.Body[newRow, newCol];
            this.Body[newRow, newCol] = this.Body[this.EmptyRow, this.EmptyCol];
            this.Body[this.EmptyRow, this.EmptyCol] = swapValue;
            this.EmptyRow = newRow;
            this.EmptyCol = newCol;
        }

        /// <summary>
        /// Here we check if we are out of the boundaries of the gamefield.
        /// </summary>
        /// <param name="row">This is the position of the new row.</param>
        /// <param name="col">This is the position of the new column.</param>
        /// <returns></returns>
        public bool IfOutOfMatrix(int row, int col)
        {
            if (row >= MatrixLength || row < 0 || col < 0 || col >= MatrixLength)
            {               
                return true;
            }

            return false;
        }

        /// <summary>
        /// Here we check if our solution is the correct one.
        /// </summary>
        /// <returns>True if yes, else false</returns>
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

        /// <summary>
        /// Here we print the gamefield.
        /// </summary>
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
    }
}
