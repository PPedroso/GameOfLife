
using GameOfLife.Board;
using System.Drawing;

namespace GameOfLife.Cells.Oscillators
{
    /// <summary>
    /// Defines the behaviour of a toad cell
    /// </summary>
    public class Toad : AbstractCell, ICell
    {
        static int minx = 2;
        static int miny = 3;

        public Toad() : base(minx, miny) { }

        public void Insert(GameBoard gameboard, Point point)
        {
            ValidateCoordinates(gameboard.Length, point);

            gameboard.SetField(point.X, point.Y);
            gameboard.SetField(point.X, point.Y - 1);
            gameboard.SetField(point.X, point.Y - 2);
            gameboard.SetField(point.X - 1, point.Y);
            gameboard.SetField(point.X - 1, point.Y - 1);
            gameboard.SetField(point.X - 1, point.Y + 1);
        }
    }
}
