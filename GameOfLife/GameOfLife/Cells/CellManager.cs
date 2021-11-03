using GameOfLife.Board;
using System.Drawing;

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

                    Blinker blinker = new();
                    blinker.Insert(gameBoard, new Point(x, y));
                    break;

                case OscilatorType.Toad:

                    Toad toad = new();
                    toad.Insert(gameBoard, new Point(x, y));
                    break;

                case OscilatorType.Beacon:

                    Beacon beacon = new();
                    beacon.Insert(gameBoard, new Point(x, y));
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
            PulseLife(gameBoard);
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

            PulseLife(gameBoard);
        }

        /// <summary>
        /// Pulses the life.
        /// </summary>
        public static void PulseLife(GameBoard board)
        {

            for (int x = 0; x < board.Length; x++)
            {
                for (int y = 0; y < board.Length; y++)
                {
                    var neighbors = CellManager.CountNeighbors(board, x, y);
                    var isLiveCell = board.GetField(x, y);

                    //Any live cell with two or three live neighbours survives.
                    if (isLiveCell)
                    {
                        if (neighbors != 2 && neighbors != 3)
                            CellManager.Kill(board, x, y);
                        else
                            CellManager.Create(board, x, y);
                    }


                    //Any dead cell with three live neighbours becomes a live cell.
                    if (!isLiveCell && neighbors == 3)
                        CellManager.Create(board, x, y);
                }
            }

            board.PresentToFuture();
        }
    }
}
