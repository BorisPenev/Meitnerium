namespace GameFifteen
{
    using System;

    class GameFifteen
    {
        

        static int[] directionRow = new int[4] { -1, 0, 1, 0 };
        static int[] directionCol = new int[4] { 0, 1, 0, -1 };
        

        private static void GenerateMatrix()
        {
            int value = 1;
            Matrix.emptyRow = 3;
            Matrix.emptyCol = 3;

            for (int i = 0; i < Matrix.MatrixLength; i++)
            {
                for (int j = 0; j < Matrix.MatrixLength; j++)
                {
                    Matrix.currentMatrix[i, j] = value;
                    value++;
                }
            }

            int randomMoves = Matrix.random.Next(4, 7);

            for (int i = 0; i < randomMoves; i++)
            {
                int randomDirection = Matrix.random.Next(4);
                int newRow = Matrix.emptyRow + directionRow[randomDirection];
                int newCol = Matrix.emptyCol + directionCol[randomDirection];

                if (Matrix.IfOutOfMatrix(newRow, newCol))
                {
                    i--;
                    continue;
                }
                else
                {
                    MoveEmptyCell(newRow, newCol);
                }
            }

            if (Matrix.IfEqualMatrix())
            {
                GenerateMatrix();
            }
        }

       

        private static void MoveEmptyCell(int newRow, int newCol)
        {
            int swapValue = Matrix.currentMatrix[newRow, newCol];
            Matrix.currentMatrix[newRow, newCol] = 16;
            Matrix.currentMatrix[Matrix.emptyRow, Matrix.emptyCol] = swapValue;
            Matrix.emptyRow = newRow;
            Matrix.emptyCol = newCol;
        }

       

        private static void PrintGameDescription()
        {
            Console.WriteLine("Welcome to the game “Game Fifteen”.\nPlease try to arrange the numbers sequentially.\n" +
            "Use 'top' to view the top scoreboard, 'restart' to start a new game \nand 'exit' to quit the game.\n\n");
        }

       

        private static void GameWon(int moves)
        {
            Console.WriteLine("Congratulations! You won the game in {0} moves.", moves);
            int scorersCount = 0;
            foreach (var scorer in ScoreBoard.scoreboard)
            {
                scorersCount += scorer.Value.Count;
            }

            if (scorersCount == 5)
            {
                if (ScoreBoard.IfGoesToBoard(moves))
                {
                    ScoreBoard.RemoveLastScore();
                    ScoreBoard.Points(moves);
                }
            }
            else
            {
                ScoreBoard.Points(moves);
            }
        }

        static void Main()
        {
            GenerateMatrix();
            PrintGameDescription();
            Matrix.Print();
            MainAlgorithm();
        }

        private static void MainAlgorithm()
        {
            int moves = 0;
            Console.Write("Enter a number to move: ");
            string inputString = Console.ReadLine();
            while (inputString.CompareTo("exit") != 0)
            {
                ExecuteComand(inputString, ref moves);
                if (Matrix.IfEqualMatrix())
                {
                    GameWon(moves);
                    ScoreBoard.PrintScoreBoard();
                    GenerateMatrix();
                    PrintGameDescription();
                    Matrix.Print();
                    moves = 0;
                }

                Console.Write("Enter a number to move: ");
                inputString = Console.ReadLine();
            }

            Console.WriteLine("Good bye!");
        }

        private static void ExecuteComand(string inputString, ref int moves)
        {
            if (inputString == "restart")
	        {
                moves = 0;                    
                GenerateMatrix();
                PrintGameDescription();
                Matrix.Print();               
	        }
            else if(inputString == "top")
            {
                ScoreBoard.PrintScoreBoard();
                Matrix.Print();                    
            }
            else
            {
                 MakeMove(inputString, ref moves);
            }
        }
  
        private static void MakeMove(string inputString, ref int moves)
        {
            int number = 0;
            bool isNumber = int.TryParse(inputString, out number);
            if (!isNumber)
            {
                Console.WriteLine("Invalid comand!");
                return;
            }

            if (number < 16 && number > 0)
            {
                int newRow = 0;
                int newCol = 0;
                for (int i = 0; i < 4; i++)
                {
                    newRow = Matrix.emptyRow + directionRow[i];
                    newCol = Matrix.emptyCol + directionCol[i];

                    if (Matrix.IfOutOfMatrix(newRow, newCol))
                    {
                        if (i == 3)
                        {
                            Console.WriteLine("Invalid move");
                        }

                        continue;
                    }

                    if (Matrix.currentMatrix[newRow, newCol] == number)
                    {
                        MoveEmptyCell(newRow, newCol);
                        moves++;
                        Matrix.Print();
                        break;
                    }

                    if (i == 3)
                    {
                        Console.WriteLine("Invalid move");
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid move");
                return;
            }           
        }
    }
}
