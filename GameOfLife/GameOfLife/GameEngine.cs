using GameOfLife.Board;
using GameOfLife.Cells;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
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
    public class GameEngine : GameWindow
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

        #region Constructor

        public GameEngine() : base(GameWindowSettings.Default, NativeWindowSettings.Default)
        {
            this.CenterWindow(new Vector2i(1200, 768));
        }

        #endregion

        #region Render functionality

        private int vertexBufferHandle;
        private int shaderProgramHandle;
        private int vertexArrayHandle;

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
        }


        protected override void OnLoad()
        {
            GL.ClearColor(new Color4(0.3f, 0.4f, 0.5f, 1f));

            float[] vertices = new float[] {
                0.0f, 0.5f, 0f,   //vertex0
                0.5f, -0.5f, 0f,  //vertex1
                -0.5f, -0.5f, 0f  //vertex2
            };

            this.vertexBufferHandle = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, this.vertexBufferHandle);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            this.vertexArrayHandle = GL.GenVertexArray();
            GL.BindVertexArray(this.vertexArrayHandle);

            GL.BindBuffer(BufferTarget.ArrayBuffer, this.vertexBufferHandle);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            GL.BindVertexArray(0);

            string vertexShaderCode =
                @"
                #version 330 core

                layout (location = 0) in vec3 aPosition
                    void main()
                {
                    gl_Position = vec4(aPosition,1f);
                }
                ";

            string pixelShaderCode =
                @"
                #version 330 core

                out vec4 pixelColor;                

                void main()
                {
                    pixelColor = vec4(0.8f,0.1f,1f);
                }
                ";

            int vertexShaderHandle = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShaderHandle, vertexShaderCode);
            GL.CompileShader(vertexShaderHandle);

            int pixelShaderHandle = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(pixelShaderHandle, pixelShaderCode);
            GL.CompileShader(pixelShaderHandle);

            this.shaderProgramHandle = GL.CreateProgram();

            GL.AttachShader(this.shaderProgramHandle, vertexShaderHandle);
            GL.AttachShader(this.shaderProgramHandle, pixelShaderHandle);

            GL.LinkProgram(this.shaderProgramHandle);

            GL.DetachShader(this.shaderProgramHandle, vertexShaderHandle);
            GL.DetachShader(this.shaderProgramHandle, pixelShaderHandle);

            GL.DeleteShader(vertexShaderHandle);
            GL.DeleteShader(pixelShaderHandle);

            base.OnLoad();
        }

        

        protected override void OnRenderFrame(FrameEventArgs args)
        {

            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.UseProgram(this.shaderProgramHandle);
            GL.BindVertexArray(this.vertexArrayHandle);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);

            this.Context.SwapBuffers();
            base.OnRenderFrame(args);
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
            PopulateWorld();

            this.Run();
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
