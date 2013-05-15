using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameFifteen
{
    public static class Matrix
    {
        public static Random random = new Random();
        public const int MatrixLength = 4;
        static int[,] solvedMatrix = new int[MatrixLength, MatrixLength]
        { 
            { 1, 2, 3, 4 }, 
            { 5, 6, 7, 8 }, 
            { 9, 10, 11, 12 }, 
            { 13, 14, 15, 16 }
        };

        public static int emptyRow = 3;
        public static int emptyCol = 3;
        public static int[,] currentMatrix = new int[MatrixLength, MatrixLength] 
        {
            { 1, 2, 3, 4 }, 
            { 5, 6, 7, 8 }, 
            { 9, 10, 11, 12 }, 
            { 13, 14, 15, 16 }  
        };

        public static bool IfOutOfMatrix(int row, int col)
        {
            if (row >= MatrixLength || row < 0 || col < 0 || col >= MatrixLength)
            {
                return true;
            }

            return false;
        }

        public static void Print()
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

        public static bool IfEqualMatrix()
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
    }
}
