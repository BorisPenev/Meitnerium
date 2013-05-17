namespace ArrangeTheNumeralPuzzleClassLibrary
{
    using System;
    using System.Linq;
    using Wintellect.PowerCollections;

    public static class ScoreBoard
    {
        private static OrderedMultiDictionary<int, string> scores = new OrderedMultiDictionary<int, string>(true);

        private static void RemoveLastScore()
        {
            if (scores.Last().Value.Count > 0)
            {
                string[] values = new string[scores.Last().Value.Count];
                scores.Last().Value.CopyTo(values, 0);
                scores.Last().Value.Remove(values.Last());
            }
            else
            {
                int[] keys = new int[scores.Count];
                scores.Keys.CopyTo(keys, 0);
                scores.Remove(keys.Last());
            }
        }

        private static void SaveScore(int moves)
        {
            Console.Write("Please enter your name for the top scoreboard: ");
            string name = Console.ReadLine();
            scores.Add(moves, name);
        }

        private static bool IfTopScore(int moves)
        {
            foreach (var score in scores)
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
            foreach (var score in scores)
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
            if (scores.Count == 0)
            {
                Console.WriteLine("Scoreboard is empty");
                return;
            }

            Console.WriteLine("Scoreboard:");
            int i = 1;
            foreach (var score in scores)
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
