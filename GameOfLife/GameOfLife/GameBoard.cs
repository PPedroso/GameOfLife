namespace GameOfLife
{
    /// <summary>
    /// Represents the game board
    /// </summary>
    public class GameBoard
    {
        #region Fields

        private bool[,] _gameBoard;
        private int _boardSize;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="GameBoard"/> class.
        /// </summary>
        /// <param name="boardSize">Size of the board.</param>
        public GameBoard(int boardSize)
        {
            _boardSize = boardSize;
            _gameBoard = new bool[_boardSize, _boardSize];
        }

        #endregion

        #region External implementation

        /// <summary>
        /// Returns the field at x,y coordinate
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public bool GetField(int x, int y)
        {
            if (x < 0 || x >= _boardSize) return false;
            if (y < 0 || y >= _boardSize) return false;

            return _gameBoard[x, y];
        }

        /// <summary>
        /// Sets the field at x,y coordinate
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public void SetField(int x, int y) => _gameBoard[x, y] = true;

        /// <summary>
        /// Resets the field at x,y coordinate
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public void ResetField(int x, int y) => _gameBoard[x, y] = false;

        #endregion
    }
}
