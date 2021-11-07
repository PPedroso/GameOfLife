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
        #region Board Settings

        /// <summary>
        /// The board size
        /// </summary>
        private const int BOARD_SIZE = 200;

        /// <summary>
        /// The game board
        /// </summary>
        private GameBoard _gameBoard;

        #endregion

        #region Game Settings

        /// <summary>
        /// The settings for the game window
        /// </summary>
        private static GameWindowSettings _gameWindowSettings = new GameWindowSettings
        {
            RenderFrequency = 20,
            UpdateFrequency = 20
        };

        /// <summary>
        /// The settings for the native window
        /// </summary>
        private static NativeWindowSettings _nativeWindowSettings = new NativeWindowSettings
        {
            Size = new Vector2i(BOARD_SIZE * 4, BOARD_SIZE * 4),
            Title = "Game of Life",
        };

        #endregion

        #region Constructor

        public GameEngine() : base(_gameWindowSettings, _nativeWindowSettings)
        {
        }

        #endregion

        #region External Implementation

        /// <summary>
        /// Starts an instance of the game
        /// </summary>
        public void Start()
        {
            this.CenterWindow();
            ShowMenu();
            ResetBoard();
            PopulateWorld();
            this.Run();
        }

        #endregion

        #region Internal Implementation
        #region Render functionality

        #region Fields

        private int _vertexBufferObject;
        private int _vertexArrayObject;
        private int _elementBufferObject;
        static float symbolSize = 0.005f;

        #endregion

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            ProcessInput();

            base.OnUpdateFrame(args);
        }


        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(new Color4(0.3f, 0.4f, 0.5f, 1f));

            _vertexBufferObject = GL.GenBuffer();
            _vertexArrayObject = GL.GenVertexArray();
            _elementBufferObject = GL.GenBuffer();
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
            GL.DeleteBuffer(_elementBufferObject);

            base.OnUnload();
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit);


            CellManager.PulseLife(_gameBoard);
            DrawBoard();

            SwapBuffers();
        }

        #endregion

        /// <summary>
        /// Shows the user menu
        /// </summary>
        private void ShowMenu()
        {
            Console.WriteLine("Create oscilators");
            Console.WriteLine("B - Blinker");
            Console.WriteLine("T - Toad");
            Console.WriteLine("E - Beacon");
            Console.WriteLine("P - Pulsar");
            Console.WriteLine("D - Pentadecathlon");
            Console.WriteLine();
            Console.WriteLine("Create spaceships");
            Console.WriteLine("G - Glider");
            Console.WriteLine("L - Light weight spaceship");
            Console.WriteLine("M - Medium weight spaceship");
            Console.WriteLine("H - Heavy weight spaceship");
            Console.WriteLine();
            Console.WriteLine("R - Reset board");
            Console.WriteLine("X - Close game");
        }

        /// <summary>
        /// Draws the board
        /// </summary>
        private void DrawBoard()
        {
            for (int x = 0; x < BOARD_SIZE; x++)
            {
                for (int y = 0; y < BOARD_SIZE; y++)
                {
                    if (_gameBoard.GetField(x, y))
                        DrawSquareAt((x - 99f) / 100f, (99f - y) / 100f);
                }
            }
        }

        private void PopulateWorld()
        {
            int totalDivisions = BOARD_SIZE / 25;

            for (int x = 1; x < totalDivisions; x++)
                for (int y = 1; y < totalDivisions; y++)
                    CellManager.AddOscillator(_gameBoard, CellManager.OscilatorType.Pulsar, x * 25, y * 25);
        }

        /// <summary>
        /// Draws a square form at x,y
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void DrawSquareAt(float x, float y)
        {
            float[] _vertices = new float[] {
                x + symbolSize *  1f,y + symbolSize *  1f, 0.0f, // top right
                x + symbolSize *  1f,y + symbolSize * -1f, 0.0f, // bottom right
                x + symbolSize * -1f,y + symbolSize * -1f, 0.0f,  // bottom left
                x + symbolSize * -1f,y + symbolSize *  1f, 0.0f,  // top left
            };

            uint[] _indices =
            {
                3, 2, 0, // The first triangle will be the bottom-right half of the triangle
                2, 0, 1  // Then the second will be the top-right half of the triangle
            };


            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);


            GL.BindVertexArray(_vertexArrayObject);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Length * sizeof(uint), _indices, BufferUsageHint.StaticDraw);

            GL.BindVertexArray(this._vertexArrayObject);
            GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);
        }

        /// <summary>
        /// Processes the user input
        /// </summary>
        private void ProcessInput()
        {

            /* Oscilators*/

            if (KeyboardState.IsKeyDown(Keys.B))
                AddRandomOscilator(_gameBoard, CellManager.OscilatorType.Blinker);

            if (KeyboardState.IsKeyDown(Keys.T))
                AddRandomOscilator(_gameBoard, CellManager.OscilatorType.Toad);

            if (KeyboardState.IsKeyDown(Keys.E))
                AddRandomOscilator(_gameBoard, CellManager.OscilatorType.Beacon);

            if (KeyboardState.IsKeyDown(Keys.P))
                AddRandomOscilator(_gameBoard, CellManager.OscilatorType.Pulsar);

            if (KeyboardState.IsKeyDown(Keys.D))
                AddRandomOscilator(_gameBoard, CellManager.OscilatorType.Pentadecathlon);

            /* Spaceships */

            if (KeyboardState.IsKeyDown(Keys.G))
                AddRandomSpaceship(_gameBoard, CellManager.SpaceshipType.Glider);

            if (KeyboardState.IsKeyDown(Keys.L))
                AddRandomSpaceship(_gameBoard, CellManager.SpaceshipType.LWSS);

            if (KeyboardState.IsKeyDown(Keys.M))
                AddRandomSpaceship(_gameBoard, CellManager.SpaceshipType.MWSS);

            if (KeyboardState.IsKeyDown(Keys.H))
                AddRandomSpaceship(_gameBoard, CellManager.SpaceshipType.HWSS);

            /*  Special commands */

            if (KeyboardState.IsKeyDown(Keys.R))
                ResetBoard();

            if (KeyboardState.IsKeyDown(Keys.X) || KeyboardState.IsKeyDown(Keys.Escape))
                this.Close();
        }

        /// <summary>
        /// Resets the board to its initial state
        /// </summary>
        private void ResetBoard()
        {
            _gameBoard = new GameBoard(BOARD_SIZE);
        }

        /// <summary>
        /// Adds the supplied oscilator type cell at a random position on the board
        /// </summary>
        /// <param name="gameboard">The gameboard to add the cell to</param>
        /// <param name="type">The type of oscilator</param>
        private void AddRandomOscilator(GameBoard gameboard, CellManager.OscilatorType type)
        {
            var x = RandomNumberGenerator.GetInt32(BOARD_SIZE);
            var y = RandomNumberGenerator.GetInt32(BOARD_SIZE);

            CellManager.AddOscillator(gameboard, type, x, y);
        }

        /// <summary>
        /// Adds teh supplied spaceship type cell at a random position on the board
        /// </summary>
        /// <param name="gameboard">The gameboard to add the cell to</param>
        /// <param name="type">The type of spaceship to add</param>
        private void AddRandomSpaceship(GameBoard gameboard, CellManager.SpaceshipType type)
        {
            var x = RandomNumberGenerator.GetInt32(BOARD_SIZE);
            var y = RandomNumberGenerator.GetInt32(BOARD_SIZE);

            CellManager.AddSpaceship(gameboard, type, x, y);
        }


        #endregion
    }
}
