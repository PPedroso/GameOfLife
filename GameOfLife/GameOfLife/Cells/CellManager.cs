namespace GameOfLife.Cells
{
    /// <summary>
    /// Abstract class for base cell functionalities
    /// </summary>
    public static class CellManager
    {
        /// <summary>
        /// Kills this cell.
        /// </summary>
        public static void Kill(GameBoard gameBoard, int x, int y)
        {
            gameBoard.ResetField(x, y);
        }

        /// <summary>
        /// Create a cell at the supplied coordinates.
        /// </summary>
        public static void Create(GameBoard gameBoard, int x, int y)
        {
            gameBoard.SetField(x, y);
        }

        /// <summary>
        /// Counts the number neighbors of this cell.
        /// </summary>
        /// <returns></returns>
        public static int CountNeighbors(GameBoard _gameBoard, int x, int y)
        {
            int count = 0;

            if (_gameBoard.GetField(x - 1, y - 1)) count++;
            if (_gameBoard.GetField(x + 0, y - 1)) count++;
            if (_gameBoard.GetField(x + 1, y - 1)) count++;
            if (_gameBoard.GetField(x - 1, y)) count++;
            if (_gameBoard.GetField(x + 1, y)) count++;
            if (_gameBoard.GetField(x - 1, y + 1)) count++;
            if (_gameBoard.GetField(x + 0, y + 1)) count++;
            if (_gameBoard.GetField(x + 1, y + 1)) count++;

            return count;
        }

       
    }
}
