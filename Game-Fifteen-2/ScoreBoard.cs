using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wintellect.PowerCollections;

namespace GameFifteen
{
    public static class ScoreBoard
    {
        public static OrderedMultiDictionary<int, string> scoreboard = new OrderedMultiDictionary<int, string>(true);

        public static void RemoveLastScore()
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

        public static void Points(int moves)
        {
            Console.Write("Please enter your name for the top scoreboard: ");
            string name = Console.ReadLine();
            scoreboard.Add(moves, name);
        }

        public static bool IfGoesToBoard(int moves)
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

        public static void PrintScoreBoard()
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
    }
}
