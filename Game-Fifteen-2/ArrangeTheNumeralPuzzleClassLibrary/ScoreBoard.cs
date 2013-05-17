namespace ArrangeTheNumeralPuzzleClassLibrary
{
    using System;
    using System.Linq;
    using Wintellect.PowerCollections;

    public static class ScoreBoard
    {
        private static OrderedMultiDictionary<int, string> scoresList = new OrderedMultiDictionary<int, string>(true);

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

        private static void SaveScore(int moves)
        {
            Console.Write("Please enter your name for the top scoreboard: ");
            string name = Console.ReadLine();
            scoresList.Add(moves, name);
        }

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
