namespace GameFifteen
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Wintellect.PowerCollections;

    class GameFifteen
    {
        static Random random = new Random();
        public const int MatrixLength = 4;
        static int[,] solvedMatrix = new int[MatrixLength, MatrixLength]
        { 
            { 1, 2, 3, 4 }, 
            { 5, 6, 7, 8 }, 
            { 9, 10, 11, 12 }, 
            { 13, 14, 15, 16 }
        };

        static int emptyRow = 3;
        static int emptyCol = 3;
        static int[,] currentMatrix = new int[MatrixLength, MatrixLength] 
        {
            { 1, 2, 3, 4 }, 
            { 5, 6, 7, 8 }, 
            { 9, 10, 11, 12 }, 
            { 13, 14, 15, 16 }  
        };

        static int[] directionRow = new int[4] { -1, 0, 1, 0 };
        static int[] directionCol = new int[4] { 0, 1, 0, -1 };
        static OrderedMultiDictionary<int, string> scoreboard = new OrderedMultiDictionary<int, string>(true);

        private static void GenerateMatrix()
        {
            int value = 1;
            emptyRow = 3;
            emptyCol = 3;

            for (int i = 0; i < MatrixLength; i++)
            {
                for (int j = 0; j < MatrixLength; j++)
                {
                    currentMatrix[i, j] = value;
                    value++;
                }
            }

            int randomMoves = random.Next(10, 21);

            for (int i = 0; i < randomMoves; i++)
            {
                int randomDirection = random.Next(4);
                int newRow = emptyRow + directionRow[randomDirection];
                int newCol = emptyCol + directionCol[randomDirection];

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

            if (IfEqualMatrix())
            {
                GenerateMatrix();
            }
        }

        private static bool IfOutOfMatrix(int row, int col)
        {
            if (row >= MatrixLength || row < 0 || col < 0 || col >= MatrixLength)
            {
                return true;
            }

            return false;
        }

        private static void MoveEmptyCell(int newRow, int newCol)
        {
            int swapValue = currentMatrix[newRow, newCol];
            currentMatrix[newRow, newCol] = 16;
            currentMatrix[emptyRow, emptyCol] = swapValue;
            emptyRow = newRow;
            emptyCol = newCol;
        }

        private static void PrintMatrix()
        {
            Console.WriteLine(" -------------");
            for (int i = 0; i < MatrixLength; i++)
            {
                Console.Write("|");
                for (int j = 0; j < MatrixLength; j++)
                {
                    if (currentMatrix[i, j] != 16)
                    {
                        Console.Write("{0,3}", currentMatrix[i, j]);
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

        private static void PrintWelcome()
        {
            Console.WriteLine("Welcome to the game “15”.\nPlease try to arrange the numbers sequentially.\n" +
            "Use 'top' to view the top scoreboard, 'restart' to start a new game \nand 'exit' to quit the game.\n\n");
        }

        private static bool IfEqualMatrix()
        {
            for (int i = 0; i < MatrixLength; i++)
            {
                for (int j = 0; j < MatrixLength; j++)
                {
                    if (currentMatrix[i, j] != solvedMatrix[i, j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private static bool IfGoesToBoard(int moves)
        {
            foreach (var score in scoreboard)
            {
                if (moves < score.Key)
                {
                    return true;
                }
            }

            return false;
        }

        private static void RemoveLastScore()
        {
            if (scoreboard.Last().Value.Count > 0)
            {
                string[] values = new string[scoreboard.Last().Value.Count];
                scoreboard.Last().Value.CopyTo(values, 0);
                scoreboard.Last().Value.Remove(values.Last());
            }
            else
            {
                int[] keys = new int[scoreboard.Count];
                scoreboard.Keys.CopyTo(keys, 0);
                scoreboard.Remove(keys.Last());
            }
        }

        private static void GameWon(int moves)
        {
            Console.WriteLine("Congratulations! You won the game in {0} moves.", moves);
            int scorersCount = 0;
            foreach (var scorer in scoreboard)
            {
                scorersCount += scorer.Value.Count;
            }

            if (scorersCount == 5)
            {
                if (IfGoesToBoard(moves))
                {
                    RemoveLastScore();
                    Points(moves);
                }
            }
            else
            {
                Points(moves);
            }
        }

        private static void Points(int moves)
        {
            Console.Write("Please enter your name for the top scoreboard: ");
            string name = Console.ReadLine();
            scoreboard.Add(moves, name);
        }

        private static void PrintScoreBoard()
        {
            if (scoreboard.Count == 0)
            {
                Console.WriteLine("Scoreboard is empty");
                return;
            }

            Console.WriteLine("Scoreboard:");
            int i = 1;
            foreach (var score in scoreboard)
            {
                foreach (var value in score.Value)
                {
                    Console.WriteLine("{0}. {1} --> {2} moves", i, value, score.Key);
                    i++;
                }
            }

            Console.WriteLine();
        }

        static void Main()
        {
            GenerateMatrix();
            PrintWelcome();
            PrintMatrix();
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
                if (IfEqualMatrix())
                {
                    GameWon(moves);
                    PrintScoreBoard();
                    GenerateMatrix();
                    PrintWelcome();
                    PrintMatrix();
                    moves = 0;
                }

                Console.Write("Enter a number to move: ");
                inputString = Console.ReadLine();
            }

            Console.WriteLine("Good bye!");
        }

        private static void ExecuteComand(string inputString, ref int moves)
        {
            switch (inputString)
            {
                case "restart":
                    moves = 0;                    
                    GenerateMatrix();
                    PrintWelcome();
                    PrintMatrix();
                    break;

                case "top":
                    PrintScoreBoard();
                    PrintMatrix();
                    break;

                default:
                    int number = 0;
                    bool isNumber = int.TryParse(inputString, out number);
                    if (!isNumber)
                    {
                        Console.WriteLine("Invalid comand!");
                        break;
                    }

                    if (number < 16 && number > 0)
                    {
                        int newRow = 0;
                        int newCol = 0;
                        for (int i = 0; i < 4; i++)
                        {
                            newRow = emptyRow + directionRow[i];
                            newCol = emptyCol + directionCol[i];

                            if (IfOutOfMatrix(newRow, newCol))
                            {
                                if (i == 3)
                                {
                                    Console.WriteLine("Invalid move");
                                }

                                continue;
                            }

                            if (currentMatrix[newRow, newCol] == number)
                            {
                                MoveEmptyCell(newRow, newCol);
                                moves++;
                                PrintMatrix();
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
                        break;
                    }

                    break;
            }
        }
    }
}
