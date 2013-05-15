namespace GameFifteenDemo
{
    using ArrangeTheNumeralPuzzleClassLibrary;

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
