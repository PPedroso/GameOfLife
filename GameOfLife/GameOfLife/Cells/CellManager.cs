using GameOfLife.Board;
using GameOfLife.Cells.Oscillators;
using GameOfLife.Cells.Spaceships;
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
        public static int CountNeighbors(GameBoard gameboard, int x, int y)
        {
            int count = 0;

            if (gameboard.GetField(x - 1, y - 1)) count++;
            if (gameboard.GetField(x + 0, y - 1)) count++;
            if (gameboard.GetField(x + 1, y - 1)) count++;
            if (gameboard.GetField(x - 1, y)) count++;
            if (gameboard.GetField(x + 1, y)) count++;
            if (gameboard.GetField(x - 1, y + 1)) count++;
            if (gameboard.GetField(x + 0, y + 1)) count++;
            if (gameboard.GetField(x + 1, y + 1)) count++;

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

                    Pulsar pulsar = new();
                    pulsar.Insert(gameBoard, new Point(x, y));
                    break;

                case OscilatorType.Pentadecathlon:

                    Pentadecathlon pentadecathlon = new();
                    pentadecathlon.Insert(gameBoard, new Point(x, y));
                    break;
            }
            PulseLife(gameBoard);
        }

        /// <summary>
        /// Adds the spaceship type cell at the coordinates x and y.
        /// </summary>
        /// <param name="gameboard">The game board.</param>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate</param>
        public static void AddSpaceship(GameBoard gameboard, SpaceshipType type, int x, int y)
        {
            switch (type)
            {
                case SpaceshipType.Glider:

                    Glider glider = new();
                    glider.Insert(gameboard, new Point(x, y));
                    break;

                case SpaceshipType.LWSS:

                    LWSS lwss = new();
                    lwss.Insert(gameboard, new Point(x, y));
                    break;

                case SpaceshipType.MWSS:

                    MWSS mwss = new();
                    mwss.Insert(gameboard, new Point(x, y));
                    break;

                case SpaceshipType.HWSS:

                    HWSS hwss = new();
                    hwss.Insert(gameboard, new Point(x, y));
                    break;
            }

            PulseLife(gameboard);
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
