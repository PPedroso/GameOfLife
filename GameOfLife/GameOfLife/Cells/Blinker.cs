
using GameOfLife.Board;
using System.Drawing;

namespace GameOfLife.Cells
{
    /// <summary>
    /// Defines the behaviour of the blinker cell
    /// </summary>
    public class Blinker : AbstractCell, ICell
    {
        static int minx = 2;
        static int miny = 2;

        public Blinker() : base(minx, miny) { }

        public void Insert(GameBoard gameboard, Point point)
        {

            ValidateCoordinates(gameboard.Length, point);

            gameboard.SetField(point.X, point.Y);
            gameboard.SetField(point.X, point.Y - 1);
            gameboard.SetField(point.X, point.Y + 1);
        }
    }
}
