using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;

namespace GameOfLife
{
    /// <summary>
    /// Represents the game board
    /// </summary>
    [Serializable]
    public class GameBoard
    {
        #region Fields

        private bool[][] _gameBoard;
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
            _gameBoard = new bool[_boardSize][];

            for (int y = 0; y < _boardSize; y++)
            {
                _gameBoard[y] = new bool[_boardSize];
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

            return _gameBoard[x][y];
        }

        /// <summary>
        /// Sets the field at x,y coordinate
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public void SetField(int x, int y) => _gameBoard[x][y] = true;

        /// <summary>
        /// Resets the field at x,y coordinate
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public void ResetField(int x, int y) => _gameBoard[x][y] = false;

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        public GameBoard Clone() {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, this);
                ms.Position = 0;

                return (GameBoard)formatter.Deserialize(ms);
            }
        } 

        #endregion
    }
}
