namespace ArrangeTheNumeralPuzzleClassLibrary
{
    using System;

    public class GameFifteen
    {
        private static bool runGame;
        private static void PrintGameDescription()
        {
            Console.WriteLine("Welcome to the game “Game Fifteen”.\nPlease try to arrange the numbers sequentially.\n" +
            "Use 'top' to view the top scoreboard, 'restart' to start a new game \nand 'exit' to quit the game.\n\n");
        }

        private static void GameWon(int moves)
        {
            Console.WriteLine("Congratulations! You won the game in {0} moves.", moves);
            ScoreBoard.IfGoesToScoreboard(moves);
            ScoreBoard.PrintScoreBoard();
        }

        public static void MainAlgorithm(GameField gameField, int moves)
        {
            while (runGame)
            {
                Console.Write("Enter a number to move: ");
                string inputString = Console.ReadLine();
                ExecuteComand(gameField, inputString, ref moves);
                if (gameField.CheckIfSolved())
                {
                    GameWon(moves);
                    InitializeGame();
                }
            }
        }

        private static void ExecuteComand(GameField matrix, string inputString, ref int moves)
        {
            int inputValue = 0;
            bool isNumber = int.TryParse(inputString, out inputValue);

            if (isNumber && 0 < inputValue && inputValue < 16)
            {
                MakeMove(matrix, inputValue, ref moves);
            }
            else if (inputString == "restart")
            {
                InitializeGame();
            }
            else if (inputString == "top")
            {
                ScoreBoard.PrintScoreBoard();
                matrix.Print();
            }
            else if (inputString == "exit")
            {
                Console.WriteLine("Good bye!");
                runGame = false;
            }
            else
            {
                Console.WriteLine("Invalid comand!");
                return;
            }
        }

        private static void MakeMove(GameField matrix, int inputValue, ref int moves)
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

                if (matrix.Body[newRow, newCol] == inputValue)
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

        public static void InitializeGame()
        {
            GameField gameField = new GameField();
            GameFifteen.PrintGameDescription();
            gameField.Print();
            int moves = 0;
            runGame = true;
            MainAlgorithm(gameField, moves);
        }
    }
}