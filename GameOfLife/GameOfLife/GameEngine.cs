using GameOfLife.Board;
using GameOfLife.Cells;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace GameOfLife
{
    /// <summary>
    /// The engine responsible for making the game work
    /// </summary>
    public class GameEngine : GameWindow
    {
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

        #region GameSettings

        /// <summary>
        /// The settings for the game window
        /// </summary>
        private static GameWindowSettings _gameWindowSettings = new GameWindowSettings
        {

        };

        /// <summary>
        /// The settings for the native window
        /// </summary>
        private static NativeWindowSettings _nativeWindowSettings = new NativeWindowSettings
        {
            Size = new Vector2i(800, 800),
            Title = "Game of Life",
        };

        #endregion

        #region Constructor

        public GameEngine() : base(GameWindowSettings.Default, _nativeWindowSettings)
        {
        }

        #endregion

        #region Render functionality

        private int _vertexBufferObject;
        private int _vertexArrayObject;
        private int _elementBufferObject;

        protected override void OnUpdateFrame(FrameEventArgs args)
        {

            if (KeyboardState.IsKeyDown(Keys.Escape))
                Close();

            base.OnUpdateFrame(args);
        }


        static float symbolSize = 0.01f;


        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(new Color4(0.3f, 0.4f, 0.5f, 1f));

        }

        protected override void OnUnload()
        {
            // Unbind all the resources by binding the targets to 0/null.
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.UseProgram(0);

            // Delete all the resources.
            GL.DeleteBuffer(_vertexBufferObject);
            GL.DeleteVertexArray(_vertexArrayObject);

            base.OnUnload();
        }

        /// <summary>
        /// Draws a square form at x,y
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void DrawSquareAt(float x, float y)
        {
            float[] _vertices = new float[] {
                x + symbolSize *  1f,  y + symbolSize *  1f, 0.0f, // top right
                x + symbolSize *  1f,  y + symbolSize * -1f, 0.0f, // bottom right
                x + symbolSize * -1f,  y + symbolSize * -1f, 0.0f,  // bottom left
                x + symbolSize * -1f,  y + symbolSize *  1f, 0.0f,  // top left
            };

            uint[] _indices =
            {
                3, 2, 0, // The first triangle will be the bottom-right half of the triangle
                2, 0, 1  // Then the second will be the top-right half of the triangle
            };

            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);

            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            _elementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Length * sizeof(uint), _indices, BufferUsageHint.StaticDraw);

            GL.BindVertexArray(this._vertexArrayObject);
            GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);
        }


        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            float x = ((float)RandomNumberGenerator.GetInt32(101)) / 100f;
            float y = ((float)RandomNumberGenerator.GetInt32(101)) / 100f;

            DrawSquareAt(0, 0);
            SwapBuffers();
        }

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
            //PopulateWorld();

            this.Run();
            //while (!_gameover)
            //{
            //    DrawBoard();
            //    Thread.Sleep(_refreshRate);
            //    if (!_pause)
            //        CellManager.PulseLife(_gameBoard);
            //}
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
