using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ArrangeTheNumeralPuzzleClassLibrary;
using System.Text;

namespace GameFifteenUnitTests
{
    [TestClass]
    public class GameFifteenEngineTests
    {
        [TestMethod]
        public void TestInvalidCommandMessage()
        {
            string expected = "Invalid comand!\r\n";

            StringBuilder sb = new StringBuilder();
            Console.SetOut(new System.IO.StringWriter(sb));
            typeof(GameFifteenEngine).GetMethod("ExecuteComand(\"dggg\", 5)");
            //Assert.AreEqual(expected, sb.ToString(), "Scoreboard.Print not working.");
            StringAssert.Equals(expected, sb.ToString());
        }
    }
}
