using GameOfLife.Board;
using System.Drawing;

namespace GameOfLife.Cells.Oscillators
{
    /// <summary>
    /// Defines the behaviour of a pentadecathlon cell
    /// </summary>
    public class Pentadecathlon : AbstractCell, ICell
    {
        public Pentadecathlon() : base(6, 2) { }

        public void Insert(GameBoard gameboard, Point point)
        {
            ValidateCoordinates(gameboard.Length, point);

            gameboard.SetField(point.X, point.Y);
            gameboard.SetField(point.X - 4, point.Y);
            gameboard.SetField(point.X - 3, point.Y);
            gameboard.SetField(point.X - 2, point.Y - 1);
            gameboard.SetField(point.X - 2, point.Y + 1);
            gameboard.SetField(point.X - 1, point.Y);
            gameboard.SetField(point.X + 1, point.Y);
            gameboard.SetField(point.X + 2, point.Y);
            gameboard.SetField(point.X + 3, point.Y - 1);
            gameboard.SetField(point.X + 3, point.Y + 1);
            gameboard.SetField(point.X + 4, point.Y);
            gameboard.SetField(point.X + 5, point.Y);
        }
    }
}
