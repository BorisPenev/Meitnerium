namespace ArrangeTheNumeralPuzzleClassLibrary
{
    using System;
    using System.Linq;
    using Wintellect.PowerCollections;

    public static class ScoreBoard
    {
        public static OrderedMultiDictionary<int, string> Scoreboard = new OrderedMultiDictionary<int, string>(true);

        public static void RemoveLastScore()
        {
            if (Scoreboard.Last().Value.Count > 0)
            {
                string[] values = new string[Scoreboard.Last().Value.Count];
                Scoreboard.Last().Value.CopyTo(values, 0);
                Scoreboard.Last().Value.Remove(values.Last());
            }
            else
            {
                int[] keys = new int[Scoreboard.Count];
                Scoreboard.Keys.CopyTo(keys, 0);
                Scoreboard.Remove(keys.Last());
            }
        }

        public static void Points(int moves)
        {
            Console.Write("Please enter your name for the top scoreboard: ");
            string name = Console.ReadLine();
            Scoreboard.Add(moves, name);
        }

        public static bool IfGoesToBoard(int moves)
        {
            foreach (var score in Scoreboard)
            {
                if (moves < score.Key)
                {
                    return true;
                }
            }

            return false;
        }

        public static void PrintScoreBoard()
        {
            if (Scoreboard.Count == 0)
            {
                Console.WriteLine("Scoreboard is empty");
                return;
            }

            Console.WriteLine("Scoreboard:");
            int i = 1;
            foreach (var score in Scoreboard)
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
