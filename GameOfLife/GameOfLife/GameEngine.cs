using GameOfLife.Board;
using GameOfLife.Cells;
using System;
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
        private int _refreshRate = 50;

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
            //CellManager.AddSpaceship(_gameBoard, CellManager.SpaceshipType.Glider, 4, 4);
            CellManager.AddSpaceship(_gameBoard, CellManager.SpaceshipType.Glider, 5, 5);

            CellManager.AddOscillator(_gameBoard, CellManager.OscilatorType.Blinker,15,15);

            //CellManager.AddOscillator(_gameBoard, CellManager.OscilatorType.Blinker, 1, 1);
        }

        /// <summary>
        /// Starts the game loop that keeps the game running
        /// </summary>
        private void StartGameLoop()
        {
            int count = 1;

            PopulateWorld();
            while (!_gameover)
            {
                //Demo();
                DrawBoard();
                Thread.Sleep(_refreshRate);
                CellManager.PulseLife(_gameBoard);
                ++count;

                //if (count % 20 == 0)
                //    CellManager.AddSpaceship(_gameBoard, CellManager.SpaceshipType.Glider, 4, 4);
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
