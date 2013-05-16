namespace GameFifteenDemo
{
    using ArrangeTheNumeralPuzzleClassLibrary;

    class GameFifteenDemo
    {
        static void Main(string[] args)
        {
            GameField matrix = new GameField();           
            GameFifteen.PrintGameDescription();
            matrix.Print();
            GameFifteen.MainAlgorithm(matrix);
        }
    }
}
