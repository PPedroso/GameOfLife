namespace GameOfLife.Cells
{
    /// <summary>
    /// Abstract class for base cell functionalities
    /// </summary>
    public abstract class AbstractCell
    {
        private GameBoard _gameBoard;
        private int _initialX;
        private int _initialY;

        public AbstractCell(GameBoard gameBoard, int x, int y)
        {
            this._gameBoard = gameBoard;
            this._initialX = x;
            this._initialY = y;

            _gameBoard.SetField(x, y);
        }
    }
}
