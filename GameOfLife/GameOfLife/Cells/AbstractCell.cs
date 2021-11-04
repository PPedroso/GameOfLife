using System.Drawing;

namespace GameOfLife.Cells
{
    /// <summary>
    /// Base cell functionalities
    /// </summary>
    public abstract class AbstractCell
    {
        int minx;
        int miny;

        public AbstractCell(int minx, int miny)
        {
            this.minx = minx;
            this.miny = miny;
        }

        /// <summary>
        /// Validate coordinates to make sure they're in valid range
        /// </summary>
        /// <param name="boardLength"></param>
        /// <param name="point"></param>
        public void ValidateCoordinates(int boardLength, Point point)
        {
            if (point.X < minx) point.X = minx;
            if (point.Y < miny) point.Y = miny;
            if (point.X > boardLength - minx) point.X = boardLength - minx;
            if (point.Y > boardLength - miny) point.Y = boardLength - miny;
        }
    }
}
