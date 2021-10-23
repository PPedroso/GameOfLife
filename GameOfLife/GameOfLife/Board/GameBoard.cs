using System;
using System.Linq;

namespace GameOfLife.Board
{
    /// <summary>
    /// Represents the game board
    /// </summary>
    [Serializable]
    public class GameBoard
    {
        #region Fields

        private BoardStateField[][] _gameBoard;
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
            _gameBoard = new BoardStateField[_boardSize][];

            for (int y = 0; y < _boardSize; y++)
            {
                _gameBoard[y] = new BoardStateField[_boardSize];

                for (int x = 0; x < _boardSize; x++)
                    _gameBoard[y][x] = new BoardStateField();
            }
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
            if (x < 0 || x >= _gameBoard.Length) return false;
            if (y < 0 || y >= _gameBoard.Length) return false;

            return _gameBoard[x][y].presentState;
        }

        /// <summary>
        /// Sets the field at x,y coordinate
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public void SetField(int x, int y) => _gameBoard[x][y].futureState = true;

        /// <summary>
        /// Resets the field at x,y coordinate
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public void ResetField(int x, int y) => _gameBoard[x][y].futureState = false;

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        public GameBoard Clone()
        {
            var gameBoardClone = (GameBoard)this.MemberwiseClone();
            var boardClone = this._gameBoard;

            return null;
        }

        /// <summary>
        /// Moves the future state to the present
        /// </summary>
        public void PresentToFuture()
        {
            foreach (var boardField in _gameBoard)
            {
                boardField.ToList().ForEach(field =>
                {
                    field.presentState = field.futureState;
                    field.futureState = false;
                });
            }
        }

        #endregion
    }
}
