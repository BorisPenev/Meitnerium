namespace ArrangeTheNumeralPuzzleClassLibrary
{
    using System;
    using System.Linq;
    using Wintellect.PowerCollections;

    public static class ScoreBoard
    {
        private static OrderedMultiDictionary<int, string> scoresList = new OrderedMultiDictionary<int, string>(true);

        /// <summary>
        /// Removes the last score in the scoreboard with the least amount of points (moves made).
        /// </summary>
        private static void RemoveLastScore()
        {
            if (scoresList.Last().Value.Count > 0)
            {
                string[] values = new string[scoresList.Last().Value.Count];
                scoresList.Last().Value.CopyTo(values, 0);
                scoresList.Last().Value.Remove(values.Last());
            }
            else
            {
                int[] keys = new int[scoresList.Count];
                scoresList.Keys.CopyTo(keys, 0);
                scoresList.Remove(keys.Last());
            }
        }

        /// <summary>
        /// Here we can save our name or alias for the top scoreboard.
        /// </summary>
        /// <param name="moves">These are the moves we have made.</param>
        private static void SaveScore(int moves)
        {
            Console.Write("Please enter your name for the top scoreboard: ");
            string name = Console.ReadLine();
            scoresList.Add(moves, name);
        }

        /// <summary>
        /// Here we check if our number of moves are less or greater than existing alredy in the top scoreboard.
        /// </summary>
        /// <param name="moves">These are the moves in which we have solved the puzzle.</param>
        /// <returns>True if our moves are less than any of the top scoreboard or true if we can get in.</returns>
        private static bool IfTopScore(int moves)
        {
            foreach (var score in scoresList)
            {
                if (moves < score.Key)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Here we check if we have filled all the 5 spots in the top scoreboard and 
        /// if we can get in somone else's spot or there are free positions.
        /// </summary>
        /// <param name="moves">These are the moves in which we have solved the puzzle.</param>
        public static void IfGoesToScoreboard(int moves)
        {
            int scorersCount = 0;
            foreach (var score in scoresList)
            {
                scorersCount += score.Value.Count;
            }

            if (scorersCount == 5)
            {
                if (IfTopScore(moves))
                {
                    RemoveLastScore();
                    SaveScore(moves);
                }
            }
            else
            {
                SaveScore(moves);
            }
        }

        /// <summary>
        /// Here we print the scoreboard if it's empty and the player has entered the command "top" the message "Scoreboard is empty" is printed,
        /// else is printed  ("{0}. {1} --> {2} moves", index, value, score.Key)
        /// </summary>
        public static void PrintScoreBoard()
        {
            if (scoresList.Count == 0)
            {
                Console.WriteLine("Scoreboard is empty");
                return;
            }

            Console.WriteLine("Scoreboard:");
            int i = 1;
            foreach (var score in scoresList)
            {
                foreach (var value in score.Value)
                {
                    Console.WriteLine("{0}. {1} --> {2} moves", i, value, score.Key);
                    i++;
                }
            }

            Console.WriteLine();
        }
    }
}
