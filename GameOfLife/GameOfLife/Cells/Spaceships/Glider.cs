using GameOfLife.Board;
using System.Drawing;

namespace GameOfLife.Cells.Spaceships
{
    /// <summary>
    /// Defines the behaviour of the glider cell
    /// </summary>
    public class Glider : AbstractCell, ICell
    {
        public Glider() : base(3, 3) { }

        public void Insert(GameBoard gameboard, Point point)
        {
            point = ValidateCoordinates(gameboard.Length, point);

            gameboard.SetField(point.X, point.Y);
            gameboard.SetField(point.X - 1, point.Y);
            gameboard.SetField(point.X - 2, point.Y);
            gameboard.SetField(point.X, point.Y - 1);
            gameboard.SetField(point.X - 1, point.Y - 2);
        }
    }
}
