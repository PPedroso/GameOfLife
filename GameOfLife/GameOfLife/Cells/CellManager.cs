namespace GameOfLife.Cells
{
    /// <summary>
    /// Abstract class for base cell functionalities
    /// </summary>
    public static class CellManager
    {
        public enum OscilatorType
        {
            Blinker,
            Toad,
            Beacon,
            Pulsar,
            Pentadecathlon
        }

        public enum SpaceshipType
        {
            Glider,
            LWSS,
            MWSS,
            HWSS
        }

        /// <summary>
        /// Kills the cell at coordinates x and y
        /// </summary>
        public static void Kill(GameBoard gameBoard, int x, int y)
        {
            gameBoard.ResetField(x, y);
        }

        /// <summary>
        /// Create a cell at the coordinates x and y.
        /// </summary>
        public static void Create(GameBoard gameBoard, int x, int y)
        {
            gameBoard.SetField(x, y);
        }

        /// <summary>
        /// Counts the number neighbors of this cell.
        /// </summary>
        /// <returns>The number of neighbors</returns>
        public static int CountNeighbors(GameBoard gameBoard, int x, int y)
        {
            int count = 0;

            if (gameBoard.GetField(x - 1, y - 1)) count++;
            if (gameBoard.GetField(x + 0, y - 1)) count++;
            if (gameBoard.GetField(x + 1, y - 1)) count++;
            if (gameBoard.GetField(x - 1, y)) count++;
            if (gameBoard.GetField(x + 1, y)) count++;
            if (gameBoard.GetField(x - 1, y + 1)) count++;
            if (gameBoard.GetField(x + 0, y + 1)) count++;
            if (gameBoard.GetField(x + 1, y + 1)) count++;

            return count;
        }

        /// <summary>
        /// Adds the oscillator type cell at the coordinates x and y.
        /// </summary>
        /// <param name="gameBoard">The game board.</param>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate</param>
        public static void AddOscillator(GameBoard gameBoard, OscilatorType type, int x, int y)
        {
            switch (type)
            {
                case OscilatorType.Blinker:
                    gameBoard.SetField(x, y);
                    gameBoard.SetField(x, y - 1);
                    gameBoard.SetField(x, y + 1);
                    break;

                case OscilatorType.Toad:
                    gameBoard.SetField(x, y);
                    gameBoard.SetField(x, y - 1);
                    gameBoard.SetField(x, y - 2);
                    gameBoard.SetField(x - 1, y);
                    gameBoard.SetField(x - 1, y - 1);
                    gameBoard.SetField(x - 1, y + 1);
                    break;
                case OscilatorType.Beacon:
                    gameBoard.SetField(x, y);
                    gameBoard.SetField(x, y - 1);
                    gameBoard.SetField(x - 1, y);
                    gameBoard.SetField(x - 1, y - 1);
                    gameBoard.SetField(x + 1, y + 1);
                    gameBoard.SetField(x + 1, y + 2);
                    gameBoard.SetField(x + 2, y + 1);
                    gameBoard.SetField(x + 2, y + 2);
                    break;

                case OscilatorType.Pulsar:
                    gameBoard.SetField(x - 2, y - 1);
                    gameBoard.SetField(x - 3, y - 1);
                    gameBoard.SetField(x - 4, y - 1);
                    gameBoard.SetField(x - 1, y - 2);
                    gameBoard.SetField(x - 1, y - 3);
                    gameBoard.SetField(x - 1, y - 4);
                    gameBoard.SetField(x - 6, y - 2);
                    gameBoard.SetField(x - 6, y - 3);
                    gameBoard.SetField(x - 6, y - 4);
                    gameBoard.SetField(x - 2, y - 6);
                    gameBoard.SetField(x - 3, y - 6);
                    gameBoard.SetField(x - 4, y - 6);

                    gameBoard.SetField(x + 2, y + 1);
                    gameBoard.SetField(x + 3, y + 1);
                    gameBoard.SetField(x + 4, y + 1);
                    gameBoard.SetField(x + 1, y + 2);
                    gameBoard.SetField(x + 1, y + 3);
                    gameBoard.SetField(x + 1, y + 4);
                    gameBoard.SetField(x + 6, y + 2);
                    gameBoard.SetField(x + 6, y + 3);
                    gameBoard.SetField(x + 6, y + 4);
                    gameBoard.SetField(x + 2, y + 6);
                    gameBoard.SetField(x + 3, y + 6);
                    gameBoard.SetField(x + 4, y + 6);

                    gameBoard.SetField(x - 2, y + 1);
                    gameBoard.SetField(x - 3, y + 1);
                    gameBoard.SetField(x - 4, y + 1);
                    gameBoard.SetField(x - 1, y + 2);
                    gameBoard.SetField(x - 1, y + 3);
                    gameBoard.SetField(x - 1, y + 4);
                    gameBoard.SetField(x - 6, y + 2);
                    gameBoard.SetField(x - 6, y + 3);
                    gameBoard.SetField(x - 6, y + 4);
                    gameBoard.SetField(x - 2, y + 6);
                    gameBoard.SetField(x - 3, y + 6);
                    gameBoard.SetField(x - 4, y + 6);

                    gameBoard.SetField(x + 2, y - 1);
                    gameBoard.SetField(x + 3, y - 1);
                    gameBoard.SetField(x + 4, y - 1);
                    gameBoard.SetField(x + 1, y - 2);
                    gameBoard.SetField(x + 1, y - 3);
                    gameBoard.SetField(x + 1, y - 4);
                    gameBoard.SetField(x + 6, y - 2);
                    gameBoard.SetField(x + 6, y - 3);
                    gameBoard.SetField(x + 6, y - 4);
                    gameBoard.SetField(x + 2, y - 6);
                    gameBoard.SetField(x + 3, y - 6);
                    gameBoard.SetField(x + 4, y - 6);
                    break;

                case OscilatorType.Pentadecathlon:
                    gameBoard.SetField(x, y);
                    gameBoard.SetField(x - 4, y);
                    gameBoard.SetField(x - 3, y);
                    gameBoard.SetField(x - 2, y - 1);
                    gameBoard.SetField(x - 2, y + 1);
                    gameBoard.SetField(x - 1, y);
                    gameBoard.SetField(x + 1, y);
                    gameBoard.SetField(x + 2, y);
                    gameBoard.SetField(x + 3, y - 1);
                    gameBoard.SetField(x + 3, y + 1);
                    gameBoard.SetField(x + 4, y);
                    gameBoard.SetField(x + 5, y);



                    break;

            }
        }

        /// <summary>
        /// Adds the spaceship type cell at the coordinates x and y.
        /// </summary>
        /// <param name="gameBoard">The game board.</param>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate</param>
        public static void AddSpaceship(GameBoard gameBoard, SpaceshipType type, int x, int y)
        {
            switch (type)
            {
                case SpaceshipType.Glider:
                    gameBoard.SetField(x, y);
                    gameBoard.SetField(x - 1, y);
                    gameBoard.SetField(x - 2, y);
                    gameBoard.SetField(x, y - 1);
                    gameBoard.SetField(x - 1, y - 2);
                    break;
            }
        }
    }
}
