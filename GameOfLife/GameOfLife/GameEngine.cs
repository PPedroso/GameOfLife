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
        private int _refreshRate = 1 * 1000;

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
            var numberOfCells = 75;

            for (int i = 0; i < numberOfCells; i++)
            {
                var x = RandomNumberGenerator.GetInt32(BOARD_SIZE);
                var y = RandomNumberGenerator.GetInt32(BOARD_SIZE);

                _gameBoard.SetField(x, y);
            }
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
            for (int x = 0; x < BOARD_SIZE; x++)
            {
                for (int y = 0; y < BOARD_SIZE; y++)
                {
                    var neighbors = CellManager.CountNeighbors(_gameBoard, x, y);

                    if (_gameBoard.GetField(x, y) && (neighbors == 2 | neighbors == 3))
                        continue;

                    if (!_gameBoard.GetField(x, y) && neighbors == 3)
                        CellManager.Create(_gameBoard, x, y);
                    else
                        CellManager.Kill(_gameBoard, x, y);
                }
            }
        }

        /// <summary>
        /// Draws the board
        /// </summary>
        private void DrawBoard()
        {

            Console.Clear();
            for (int i = 0; i < BOARD_SIZE; i++)
            {
                for (int j = 0; j < BOARD_SIZE; j++)
                {
                    if (_gameBoard.GetField(i, j))
                        Console.Write("O");
                    else
                        Console.Write("  ");
                    if (j + 1 == BOARD_SIZE) Console.WriteLine();
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
