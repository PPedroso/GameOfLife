namespace GameOfLife
{
    public class GameBoard
    {
        private bool[,] _gameBoard;
        public GameBoard(int boardSize)
        {
            _gameBoard = new bool[boardSize, boardSize];
        }


        public bool GetField(int x, int y) => _gameBoard[x, y];
        public void SetField(int x, int y) => _gameBoard[x, y] = true;
        public void ResetField(int x, int y) => _gameBoard[x, y] = false;
    }
}
