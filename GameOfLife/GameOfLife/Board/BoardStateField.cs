namespace GameOfLife.Board
{
    /// <summary>
    /// Represents a field in the board
    /// </summary>
    public class BoardStateField
    {
        public BoardStateField() { presentState = false; futureState = false; }

        /// <summary>
        /// The present state of the field
        /// </summary>
        public bool presentState;
        /// <summary>
        /// The future state of the field
        /// </summary>
        public bool futureState;
    }
}
