namespace ArrangeTheNumeralPuzzleClassLibrary
{
    using System;

    public class GameFifteen
    {
        public static void PrintGameDescription()
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

        public static void MainAlgorithm(Matrix matrix)
        {
            int moves = 0;
            Console.Write("Enter a number to move: ");
            string inputString = Console.ReadLine();

            while (inputString.CompareTo("exit") != 0)
            {
                ExecuteComand(matrix, inputString, ref moves);
                if (matrix.CheckIfSolved())
                {
                    GameWon(moves);
                    ScoreBoard.PrintScoreBoard();
                    matrix = new Matrix();
                    PrintGameDescription();
                    matrix.Print();
                    moves = 0;
                }

                Console.Write("Enter a number to move: ");
                inputString = Console.ReadLine();
            }

            Console.WriteLine("Good bye!");
        }

        private static void ExecuteComand(Matrix matrix, string inputString, ref int moves)
        {
            if (inputString == "restart")
            {
                moves = 0;
                matrix = new Matrix();
                PrintGameDescription();
                matrix.Print();
            }
            else if (inputString == "top")
            {
                ScoreBoard.PrintScoreBoard();
                matrix.Print();
            }
            else
            {
                MakeMove(matrix, inputString, ref moves);
            }
        }

        private static void MakeMove(Matrix matrix, string inputString, ref int moves)
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
                    newRow = matrix.EmptyRow + Direction.Row[i];
                    newCol = matrix.EmptyCol + Direction.Col[i];

                    if (matrix.IfOutOfMatrix(newRow, newCol))
                    {
                        if (i == 3)
                        {
                            Console.WriteLine("Invalid move");
                        }

                        continue;
                    }

                    if (matrix.Body[newRow, newCol] == number)
                    {
                        matrix.MoveEmptyCell(newRow, newCol);
                        moves++;
                        matrix.Print();
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
