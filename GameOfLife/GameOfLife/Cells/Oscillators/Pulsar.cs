using GameOfLife.Board;
using System.Drawing;

namespace GameOfLife.Cells.Oscillators
{
    /// <summary>
    /// Defines the behaviour of the pulsar cell
    /// </summary>
    public class Pulsar : AbstractCell, ICell
    {
        public Pulsar() : base(7, 7) { }

        public void Insert(GameBoard gameboard, Point point)
        {
            point = ValidateCoordinates(gameboard.Length, point);

            gameboard.SetField(point.X - 2, point.Y - 1);
            gameboard.SetField(point.X - 3, point.Y - 1);
            gameboard.SetField(point.X - 4, point.Y - 1);
            gameboard.SetField(point.X - 1, point.Y - 2);
            gameboard.SetField(point.X - 1, point.Y - 3);
            gameboard.SetField(point.X - 1, point.Y - 4);
            gameboard.SetField(point.X - 6, point.Y - 2);
            gameboard.SetField(point.X - 6, point.Y - 3);
            gameboard.SetField(point.X - 6, point.Y - 4);
            gameboard.SetField(point.X - 2, point.Y - 6);
            gameboard.SetField(point.X - 3, point.Y - 6);
            gameboard.SetField(point.X - 4, point.Y - 6);

            gameboard.SetField(point.X + 2, point.Y + 1);
            gameboard.SetField(point.X + 3, point.Y + 1);
            gameboard.SetField(point.X + 4, point.Y + 1);
            gameboard.SetField(point.X + 1, point.Y + 2);
            gameboard.SetField(point.X + 1, point.Y + 3);
            gameboard.SetField(point.X + 1, point.Y + 4);
            gameboard.SetField(point.X + 6, point.Y + 2);
            gameboard.SetField(point.X + 6, point.Y + 3);
            gameboard.SetField(point.X + 6, point.Y + 4);
            gameboard.SetField(point.X + 2, point.Y + 6);
            gameboard.SetField(point.X + 3, point.Y + 6);
            gameboard.SetField(point.X + 4, point.Y + 6);

            gameboard.SetField(point.X - 2, point.Y + 1);
            gameboard.SetField(point.X - 3, point.Y + 1);
            gameboard.SetField(point.X - 4, point.Y + 1);
            gameboard.SetField(point.X - 1, point.Y + 2);
            gameboard.SetField(point.X - 1, point.Y + 3);
            gameboard.SetField(point.X - 1, point.Y + 4);
            gameboard.SetField(point.X - 6, point.Y + 2);
            gameboard.SetField(point.X - 6, point.Y + 3);
            gameboard.SetField(point.X - 6, point.Y + 4);
            gameboard.SetField(point.X - 2, point.Y + 6);
            gameboard.SetField(point.X - 3, point.Y + 6);
            gameboard.SetField(point.X - 4, point.Y + 6);

            gameboard.SetField(point.X + 2, point.Y - 1);
            gameboard.SetField(point.X + 3, point.Y - 1);
            gameboard.SetField(point.X + 4, point.Y - 1);
            gameboard.SetField(point.X + 1, point.Y - 2);
            gameboard.SetField(point.X + 1, point.Y - 3);
            gameboard.SetField(point.X + 1, point.Y - 4);
            gameboard.SetField(point.X + 6, point.Y - 2);
            gameboard.SetField(point.X + 6, point.Y - 3);
            gameboard.SetField(point.X + 6, point.Y - 4);
            gameboard.SetField(point.X + 2, point.Y - 6);
            gameboard.SetField(point.X + 3, point.Y - 6);
            gameboard.SetField(point.X + 4, point.Y - 6);
        }
    }
}
