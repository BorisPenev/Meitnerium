using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ArrangeTheNumeralPuzzleClassLibrary;
using System.Text;

namespace GameFifteenUnitTests
{
    [TestClass]
    public class GameFieldTests
    {
        [TestMethod]
        public void MoveEmptyCellTest()
        {
            GameField gameField = new GameField();
            SetGameFieldBody(gameField, new int[,]
            { 
                { 1, 2, 3, 4 }, 
                { 5, 6, 7, 8 }, 
                { 9, 10, 11, 16 }, 
                { 13, 14, 15, 12 }
            });            

            gameField.MoveEmptyCell(3, 3);
            int[,] expected = 
            { 
                { 1, 2, 3, 4 }, 
                { 5, 6, 7, 8 }, 
                { 9, 10, 11, 12 }, 
                { 13, 14, 15, 16 }
            };

            CollectionAssert.AreEqual(gameField.Body, expected);
        }

        [TestMethod]
        public void CheckSolvedGameField()
        {
            GameField gameField = new GameField();
            SetGameFieldBody(gameField, new int[,]
            { 
                { 1, 2, 3, 4 }, 
                { 5, 6, 7, 8 }, 
                { 9, 10, 11, 12 }, 
                { 13, 14, 15, 16 }
            });

            Assert.IsTrue(gameField.CheckIfSolved());            
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void InvalidMoveEmptyCellTest()
        {
            GameField gameField = new GameField();
            SetGameFieldBody(gameField, new int[,]
            { 
                { 1, 2, 3, 4 }, 
                { 5, 6, 7, 8 }, 
                { 9, 10, 11, 12 }, 
                { 13, 14, 15, 16 }
            });

            gameField.MoveEmptyCell(4, 3);            
        }

        [TestMethod]
        public void TestPrintMatrix()
        {
            GameField gameField = new GameField();
            SetGameFieldBody(gameField, new int[,]
            { 
                { 1, 2, 3, 4 }, 
                { 5, 9, 7, 8 }, 
                { 6, 10, 15, 12 }, 
                { 16, 14, 11, 13 }
            });

            string expected = " -------------\r\n|  1  2  3  4 |\r\n|  5  9  7  8 |\r\n|  6 10 15 12 |\r\n|    14 11 13 |\r\n -------------\r\n";
            StringBuilder sb = new StringBuilder();
            Console.SetOut(new System.IO.StringWriter(sb));
            gameField.Print();
            Assert.AreEqual(expected, sb.ToString(), "PringMatrix not working.");
        }
        
        private GameField SetGameFieldBody (GameField gameField, int[,] matrix)
        {
            typeof(GameField).GetProperty("Body").SetValue(gameField, matrix);
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i,j] == 16)
                    {
                        typeof(GameField).GetProperty("EmptyRow").SetValue(gameField, i);
                        typeof(GameField).GetProperty("EmptyCol").SetValue(gameField, j);
                    }
                }
            }
            return gameField;
        }
    }
}
