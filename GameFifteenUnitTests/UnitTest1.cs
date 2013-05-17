using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ArrangeTheNumeralPuzzleClassLibrary;

namespace GameFifteenUnitTests
{
    [TestClass]
    public class UnitTest1
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
