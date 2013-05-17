namespace GameFifteenUnitTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ArrangeTheNumeralPuzzleClassLibrary;
    using System.Text;

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
    }
}
