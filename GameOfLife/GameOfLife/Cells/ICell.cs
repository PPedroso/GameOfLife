using GameOfLife.Board;
using System.Drawing;

namespace GameOfLife.Cells
{
    /// <summary>
    /// Defines how a cell should behave
    /// </summary>
    public interface ICell
    {
        /// <summary>
        /// Inserts the cell into the board
        /// </summary>
        void Insert(GameBoard gameboard, Point point);
        /// <summary>
        /// Checks if coordinates are valid
        /// </summary>
        void ValidateCoordinates(int boardLength, Point point);
    }
}
