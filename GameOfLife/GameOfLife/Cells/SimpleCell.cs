
namespace GameOfLife.Cells
{
    /// <summary>
    /// A simple cell
    /// </summary>
    /// <seealso cref="GameOfLife.Cells.ICell" />
    public class SimpleCell : AbstractCell, ICell
    {
        public SimpleCell(GameBoard gameBoard, int initalX, int initialY) : base(gameBoard, initalX, initialY)
        { }

        public void Pusle()
        {
            throw new System.NotImplementedException();
        }
    }
}
