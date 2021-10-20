using GameOfLife.Cells;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;

namespace GameOfLife
{
    /// <summary>
    /// The engine responsible for making the game work
    /// </summary>
    public class GameEngine
    {
        #region Fields

        /// <summary>
        /// If true, the game has ended
        /// </summary>
        private bool _gameover = false;

        /// <summary>
        /// The board size
        /// </summary>
        private const int BOARD_SIZE = 20;

        /// <summary>
        /// The game board
        /// </summary>
        private GameBoard _gameBoard;


        /// <summary>
        /// The refresh rate of the board in milliseconds
        /// </summary>
        private int _refreshRate = 250;

        #endregion


        /// <summary>
        /// Starts an instance of the game
        /// </summary>
        public void Start()
        {
            _gameBoard = new GameBoard(BOARD_SIZE);

            PopulateWorld();
            StartGameLoop();
        }

        private void PopulateWorld()
        {
            CellManager.AddOscillator(_gameBoard, CellManager.OscilatorType.Toad, 15, 15);
            CellManager.AddSpaceship(_gameBoard, CellManager.SpaceshipType.Glider, 5, 5);
        }

        /// <summary>
        /// Starts the game loop that keeps the game running
        /// </summary>
        private void StartGameLoop()
        {
            PopulateWorld();
            while (!_gameover)
            {
                //Demo();
                DrawBoard();
                Thread.Sleep(_refreshRate);
                PulseLife();
            }

        }

        /// <summary>
        /// Pulses the life.
        /// </summary>
        private void PulseLife()
        {
            var boardClone = _gameBoard.Clone();

            for (int x = 0; x < BOARD_SIZE; x++)
            {
                for (int y = 0; y < BOARD_SIZE; y++)
                {
                    var neighbors = CellManager.CountNeighbors(boardClone, x, y);
                    var isLiveCell = boardClone.GetField(x, y);

                    //Any live cell with two or three live neighbours survives.
                    if (isLiveCell && (neighbors != 2 && neighbors != 3)) {
                        CellManager.Kill(_gameBoard, x, y);
                    }

                    //Any dead cell with three live neighbours becomes a live cell.
                    if (!isLiveCell && neighbors == 3)
                        CellManager.Create(_gameBoard, x, y);
                }
            }
        }

        /// <summary>
        /// Draws the board
        /// </summary>
        private void DrawBoard()
        {
            Console.Clear();
            Console.WriteLine("Game started!");
            Console.WriteLine();
            for (int x = 0; x < BOARD_SIZE; x++)
            {
                for (int y = 0; y < BOARD_SIZE; y++)
                {
                    if (_gameBoard.GetField(x, y))
                        Console.Write("O");
                    else
                        Console.Write(" ");
                    if (y + 1 == BOARD_SIZE) Console.WriteLine();
                }
            }
        }

        #region Debug

        /// <summary>
        /// Demo to test the board
        /// </summary>
        private void Demo()
        {
            for (int i = 0; i < BOARD_SIZE; i++)
            {
                var x = RandomNumberGenerator.GetInt32(BOARD_SIZE);
                var y = RandomNumberGenerator.GetInt32(BOARD_SIZE);
                var choice = RandomNumberGenerator.GetInt32(2);

                if (choice == 0)
                    _gameBoard.SetField(x, y);
                else
                    _gameBoard.ResetField(x, y);
            }
        }

        #endregion
    }
}
