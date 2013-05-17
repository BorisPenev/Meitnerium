namespace GameFifteenUnitTests
{
    using ArrangeTheNumeralPuzzleClassLibrary;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Text;

    [TestClass]
    public class GameFieldTests
    {
        [TestMethod]
        public void EmptyRowColTest()
        {
            GameField gameField = new GameField();
            SetGameFieldBody(gameField, new int[,]
            { 
                { 1, 2, 3, 4 }, 
                { 5, 6, 7, 8 }, 
                { 9, 10, 11, 16 }, 
                { 13, 14, 15, 12 }
            });

            Assert.AreEqual(gameField.EmptyCol, 3);
        }

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
        public void MoveEmptyCellTest2()
        {
            GameField gameField = new GameField();
            SetGameFieldBody(gameField, new int[,]
            { 
                { 1, 2, 3, 4 }, 
                { 5, 6, 7, 8 }, 
                { 9, 10, 16, 11 }, 
                { 13, 14, 15, 12 }
            });

            gameField.MoveEmptyCell(2, 3);
            int[,] expected = 
            { 
                { 1, 2, 3, 4 }, 
                { 5, 6, 7, 8 }, 
                { 9, 10, 11, 16 }, 
                { 13, 14, 15, 12 }
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
        public void IfOutOfMatrixTest()
        {
            GameField gameField = new GameField();
            Assert.IsTrue(gameField.IfOutOfMatrix(5, 0));
        }

        [TestMethod]
        public void TestGameFieldPrint()
        {
            GameField gameField = new GameField();
            SetGameFieldBody(gameField, new int[,]
            { 
                { 1, 2, 3, 4 }, 
                { 5, 6, 7, 8 }, 
                { 9, 10, 15, 11 }, 
                { 13, 14, 12, 16 }
            });

            string expected = " -------------\r\n|  1  2  3  4 |\r\n|  5  6  7  8 |\r\n|  9 10 15 11 |\r\n| 13 14 12    |\r\n -------------\r\n";

            StringBuilder sb = new StringBuilder();
            Console.SetOut(new System.IO.StringWriter(sb));
            gameField.Print();
            StringAssert.Equals(expected, sb.ToString());
        }

        private GameField SetGameFieldBody(GameField gameField, int[,] matrix)
        {
            typeof(GameField).GetProperty("Body").SetValue(gameField, matrix);
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == 16)
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
