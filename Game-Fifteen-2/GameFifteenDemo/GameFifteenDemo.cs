using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArrangeTheNumeralPuzzleClassLibrary;

namespace GameFifteenDemo
{
    class GameFifteenDemo
    {
        static void Main(string[] args)
        {
            Matrix matrix = new Matrix();
            GameFifteen.PrintGameDescription();
            matrix.Print();
            GameFifteen.MainAlgorithm(matrix);
        }
    }
}
