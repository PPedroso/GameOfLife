using GameOfLife.Board;
using System.Drawing;

namespace GameOfLife.Cells.Spaceships
{
    public class MWSS : AbstractCell, ICell
    {
        /// <summary>
        /// Defines the behaviour of the medium weight spaceship cell
        /// </summary>
        public MWSS() : base(4, 3) { }

        public void Insert(GameBoard gameboard, Point point)
        {
            point = ValidateCoordinates(gameboard.Length, point);

            gameboard.SetField(point.X - 2, point.Y + 1);
            gameboard.SetField(point.X - 2, point.Y - 1);
            gameboard.SetField(point.X, point.Y + 2);
            gameboard.SetField(point.X + 2, point.Y + 1);
            gameboard.SetField(point.X + 3, point.Y);
            gameboard.SetField(point.X + 3, point.Y - 1);
            gameboard.SetField(point.X + 3, point.Y - 2);
            gameboard.SetField(point.X + 2, point.Y - 2);
            gameboard.SetField(point.X + 1, point.Y - 2);
            gameboard.SetField(point.X, point.Y - 2);
            gameboard.SetField(point.X - 1, point.Y - 2);
        }
    }
}
