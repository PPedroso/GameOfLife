using GameOfLife.Board;
using System.Drawing;

namespace GameOfLife.Cells.Oscillators
{
    /// <summary>
    /// Defines the behaviour of a beacon cell
    /// </summary>
    public class Beacon : AbstractCell, ICell
    {
        static int minx = 3;
        static int miny = 3;

        public Beacon() : base(minx, miny) { }

        public void Insert(GameBoard gameboard, Point point)
        {
            point = ValidateCoordinates(gameboard.Length, point);

            gameboard.SetField(point.X, point.Y);
            gameboard.SetField(point.X, point.Y - 1);
            gameboard.SetField(point.X - 1, point.Y);
            gameboard.SetField(point.X - 1, point.Y - 1);
            gameboard.SetField(point.X + 1, point.Y + 1);
            gameboard.SetField(point.X + 1, point.Y + 2);
            gameboard.SetField(point.X + 2, point.Y + 1);
            gameboard.SetField(point.X + 2, point.Y + 2);
        }
    }
}
