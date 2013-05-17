namespace ArrangeTheNumeralPuzzleClassLibrary
{
    using System;

    public class GameFifteenEngine
    {
        private bool runGame;
        private GameField gameField;
        
        private  void PrintGameDescription()
        {
            Console.WriteLine("Welcome to the game “Game Fifteen”.\nPlease try to arrange the numbers sequentially.\n" +
            "Use 'top' to view the top scoreboard, 'restart' to start a new game \nand 'exit' to quit the game.\n\n");
        }

        private  void GameWon(int moves)
        {
            Console.WriteLine("Congratulations! You won the game in {0} moves.", moves);
            ScoreBoard.IfGoesToScoreboard(moves);
            ScoreBoard.PrintScoreBoard();
        }

        public void MainAlgorithm(int moves)
        {
            while (runGame)
            {
                Console.Write("Enter a number to move: ");
                string inputString = Console.ReadLine();
                this.ExecuteComand(inputString, ref moves);
                if (this.gameField.CheckIfSolved())
                {
                    this.GameWon(moves);
                    this.InitializeGame();
                }
            }
        }

        private  void ExecuteComand(string inputString, ref int moves)
        {
            int inputValue = 0;
            bool isNumber = int.TryParse(inputString, out inputValue);

            if (isNumber && 0 < inputValue && inputValue < 16)
            {
                this.MakeMove(inputValue, ref moves);
            }
            else if (inputString == "restart")
            {
                this.InitializeGame();
            }
            else if (inputString == "top")
            {
                ScoreBoard.PrintScoreBoard();
                this.gameField.Print();
            }
            else if (inputString == "exit")
            {
                Console.WriteLine("Good bye!");
                this.runGame = false;
            }
            else
            {
                Console.WriteLine("Invalid comand!");
                return;
            }
        }

        private void MakeMove(int inputValue, ref int moves)
        {
            int newRow = 0;
            int newCol = 0;
            for (int i = 0; i < 4; i++)
            {
                newRow = this.gameField.EmptyRow + Direction.Row[i];
                newCol = this.gameField.EmptyCol + Direction.Col[i];

                if (this.gameField.IfOutOfMatrix(newRow, newCol))
                {
                    if (i == 3)
                    {
                        Console.WriteLine("Invalid move");
                    }

                    continue;
                }

                if (this.gameField.Body[newRow, newCol] == inputValue)
                {
                    this.gameField.MoveEmptyCell(newRow, newCol);
                    moves++;
                    this.gameField.Print();
                    break;
                }

                if (i == 3)
                {
                    Console.WriteLine("Invalid move");
                }
            }
        }

        public void InitializeGame()
        {
            this.gameField = new GameField();
            this.PrintGameDescription();
            this.gameField.Print();
            this.runGame = true;
            int moves = 0;
            this.MainAlgorithm(moves);
        }
    }
}