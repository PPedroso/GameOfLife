﻿using GameOfLife.Board;
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

        #region GameSettings

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
            _gameBoard = new GameBoard(BOARD_SIZE);
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

            if (KeyboardState.IsKeyDown(Keys.Escape))
                Close();

            if (KeyboardState.IsKeyDown(Keys.B))
                CellManager.AddOscillator(
                                 _gameBoard,
                                 CellManager.OscilatorType.Blinker,
                                 RandomNumberGenerator.GetInt32(BOARD_SIZE),
                                 RandomNumberGenerator.GetInt32(BOARD_SIZE));

            if (KeyboardState.IsKeyDown(Keys.G))
                CellManager.AddSpaceship(
                                _gameBoard,
                                CellManager.SpaceshipType.Glider,
                                RandomNumberGenerator.GetInt32(BOARD_SIZE),
                                RandomNumberGenerator.GetInt32(BOARD_SIZE));

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
            Console.WriteLine("Game started!");
            Console.WriteLine("P - Pause game");
            Console.WriteLine("B - Create blinker");
            Console.WriteLine("G - Create glider");
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
                    CellManager.AddOscillator(_gameBoard, CellManager.OscilatorType.Pulsar, x*25, y*25);
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


        #endregion
    }
}
