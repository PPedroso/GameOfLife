using GameOfLife.Board;
using GameOfLife.Cells;
using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

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

        /// <summary>
        /// If true, the game is paused
        /// </summary>
        private bool _pause = false;

        #endregion


        /// <summary>
        /// Starts an instance of the game
        /// </summary>
        public void Start()
        {
            _gameBoard = new GameBoard(BOARD_SIZE);

            PopulateWorld();
            StartInputCapture();
            StartGameLoop();
        }

        private void PopulateWorld()
        {
            //CellManager.AddSpaceship(_gameBoard, CellManager.SpaceshipType.Glider, 4, 4);
            //CellManager.AddOscillator(_gameBoard, CellManager.OscilatorType.Blinker, 15, 15);
            //CellManager.AddOscillator(_gameBoard, CellManager.OscilatorType.Blinker, 1, 1);
        }

        /// <summary>
        /// Starts the game loop that keeps the game running
        /// </summary>
        private void StartGameLoop()
        {
            PopulateWorld();
            while (!_gameover)
            {
                DrawBoard();
                Thread.Sleep(_refreshRate);
                if (!_pause)
                    CellManager.PulseLife(_gameBoard);
            }

        }

        /// <summary>
        /// Starts a task to capture user input
        /// </summary>
        private void StartInputCapture()
        {
            new Task(() =>
            {
                while (true)
                {
                    var key = Console.ReadKey().Key;

                    switch (key)
                    {
                        case ConsoleKey.P:
                            _pause = !_pause;
                            break;

                        case ConsoleKey.B:
                            CellManager.AddOscillator(
                                _gameBoard,
                                CellManager.OscilatorType.Blinker,
                                RandomNumberGenerator.GetInt32(BOARD_SIZE),
                                RandomNumberGenerator.GetInt32(BOARD_SIZE));
                            break;


                        case ConsoleKey.G:
                            CellManager.AddSpaceship(
                                _gameBoard,
                                CellManager.SpaceshipType.Glider,
                                RandomNumberGenerator.GetInt32(BOARD_SIZE),
                                RandomNumberGenerator.GetInt32(BOARD_SIZE));
                            break;

                    }
                }

            }).Start();
        }

        /// <summary>
        /// Draws the board
        /// </summary>
        private void DrawBoard()
        {
            Console.Clear();
            ShowMenu();
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

        /// <summary>
        /// Shows the user menu
        /// </summary>
        private void ShowMenu()
        {
            Console.WriteLine("Game started!");
            Console.WriteLine("P - Pause game");
            Console.WriteLine("B - Create blinker");
            Console.WriteLine("G - Create glider");
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
