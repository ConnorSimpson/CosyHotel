using System.Drawing;
using GameState.Main;
using GameState.States;
using Player;

namespace GameState
{
    /// <summary>
    /// Game Information class
    /// </summary>
    public class GameInformation
    {
        #region private members

        private StateTransitions stateTransitioned;

        #endregion

        /// <summary>
        /// Game state machine
        /// </summary>
        public GameStateMachine GameState { get; private set; }

        /// <summary>
        /// List of player information
        /// </summary>
        public PlayerInformation Players { get; private set; }

        /// <summary>
        /// Position of the board in the window
        /// </summary>
        public Point BoardPosition;

        /// <summary>
        /// Size of the game board
        /// </summary>
        public (int width, int height) BoardSize;

        /// <summary>
        /// Game Information constructor
        /// </summary>
        /// <param name="players">player count</param>
        /// <exception cref="ArgumentException">invalid player count</exception>
        public GameInformation()
        {
            GameState = new GameStateMachine();
            stateTransitioned = new StateTransitions(this, GameState);
        }
    }
}
