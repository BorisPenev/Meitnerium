namespace ArrangeTheNumeralPuzzleClassLibrary
{
    using System;

    /// <summary>
    /// This is the class that runs the main logic of the game.
    /// </summary>
    public class GameFifteenEngine
    {
        private bool runGame;
        private GameField gameField;
        
        /// <summary>
        /// Prints the initial game description above the gamefield.
        /// </summary>
        private void PrintGameDescription()
        {
            Console.WriteLine("Welcome to the game “Game Fifteen”.\nPlease try to arrange the numbers sequentially.\n" +
            "Use 'top' to view the top scoreboard, 'restart' to start a new game \nand 'exit' to quit the game.\n\n");
        }

        /// <summary>
        /// This method prints a message with how many moves you have won the game 
        /// if you have a higher score the someone of the top 5 in the scoreboard.
        /// </summary>
        /// <param name="moves">
        /// This parameter is the count of the moves which you made in order to win.
        /// </param>
        private void GameWon(int moves)
        {
            Console.WriteLine("Congratulations! You won the game in {0} moves.", moves);
            ScoreBoard.IfGoesToScoreboard(moves);
            ScoreBoard.PrintScoreBoard();
        }

        /// <summary>
        /// In here we ensure that the game is going to run endlessly, until
        /// you enter a command "exit" to exit the game.
        /// You can endter other commands like "top" to show the current scoreboard,
        /// and command "restart" to generate a new gamefield.
        /// </summary>
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

        /// <summary>
        /// Here we actually check which command we have entered in the console.
        /// </summary>
        /// <param name="inputString">Takes the imput.</param>
        /// <param name="moves">Takes the moves.</param>
        private void ExecuteComand(string inputString, ref int moves)
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

        /// <summary>
        /// Here we swap the picked number if possible with the empty " " position.
        /// </summary>
        /// <param name="inputValue">The imput from the console usually a number.</param>
        /// <param name="moves">\Takes moves and increments it when a move of the empty " " position is made.</param>
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

        /// <summary>
        /// Here we create and print the gameField with its game description along with the main algorithm.
        /// </summary>
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