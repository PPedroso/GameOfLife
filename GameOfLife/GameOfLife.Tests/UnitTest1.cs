using NUnit.Framework;
using GameOfLife.Board;
using GameOfLife.Cells;

namespace GameOfLife.Tests
{
    public class Tests
    {
        GameBoard board;

        [SetUp]
        public void Setup()
        {
            int maxBoard = 10;
            board = new GameBoard(maxBoard);
        }

        [Test]
        public void LoneCellShouldDie()
        {
            board.SetField(1, 1);
            CellManager.PulseLife(board);
            CellManager.PulseLife(board);

            Assert.IsFalse(board.GetField(1, 1));
        }

        [Test]
        public void SurroundedDeadCellShouldRessurrect()
        {
            board.SetField(0, 0);
            board.SetField(0, 1);
            board.SetField(0, 2);

            CellManager.PulseLife(board);
            CellManager.PulseLife(board);

            Assert.IsTrue(board.GetField(1, 1));

        }

        [Test]
        public void SurroundedLiveCellsShouldStayAlive()
        {
            board.SetField(0, 0);
            board.SetField(0, 1);
            board.SetField(1, 0);
            board.SetField(1, 1);

            CellManager.PulseLife(board);
            CellManager.PulseLife(board);

            Assert.IsTrue(board.GetField(0, 0));
            Assert.IsTrue(board.GetField(0, 1));
            Assert.IsTrue(board.GetField(1, 0));
            Assert.IsTrue(board.GetField(1, 1));

        }

        /// <summary>
        /// Oscilators the type blinker should transition to next phase.
        /// 
        ///        *
        /// *** -> *
        ///        *
        /// </summary>
        [Test]
        public void OscilatorTypeBlinkerShouldTransitionToNextPhase()
        {
            CellManager.AddOscillator(board, CellManager.OscilatorType.Blinker, 1, 1);

            Assert.IsTrue(board.GetField(1, 0));
            Assert.IsTrue(board.GetField(1, 1));
            Assert.IsTrue(board.GetField(1, 2));

            CellManager.PulseLife(board);
            CellManager.PulseLife(board);

            Assert.IsFalse(board.GetField(0, 1));
            Assert.IsFalse(board.GetField(2, 1));
            Assert.IsTrue(board.GetField(1, 0));
            Assert.IsTrue(board.GetField(1, 1));
            Assert.IsTrue(board.GetField(1, 2));
        }

    }
}