using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ArrangeTheNumeralPuzzleClassLibrary;
using System.Text;

namespace GameFifteenUnitTests
{
    [TestClass]
    public class ScoreBoardTests
    {
        [TestMethod]
        public void TestEmptyScoreBoard()
        {           
            string expected = "Scoreboard is empty\r\n";

            StringBuilder sb = new StringBuilder();            
            Console.SetOut(new System.IO.StringWriter(sb));
            ScoreBoard.PrintScoreBoard();
            Assert.AreEqual(expected, sb.ToString(), "Scoreboard.Print not working.");
        }

        [TestMethod]
        public void TestScoreBoardWithScores()
        {            
            ScoreBoard.IfGoesToScoreboard(2);
            StringBuilder name = new StringBuilder();
            name.Append("ceko");
            Console.SetIn(new System.IO.StringReader(name[0].ToString()));

            string expected = "Scoreboard:\r\n1. ceko --> 2 moves\r\n";           

            StringBuilder sb = new StringBuilder();
            Console.SetOut(new System.IO.StringWriter(sb));
            ScoreBoard.PrintScoreBoard();
            Assert.AreEqual(expected, sb.ToString(), "PringMatrix not working.");
        }
    }
}
