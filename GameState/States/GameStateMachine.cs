using static GameState.States.StatesAndCommands;

namespace GameState.States
{
    /// <summary>
    /// Game State Machine
    /// </summary>
    public class GameStateMachine
    {
        #region private members

        private readonly GameStateProcess processor;

        #endregion

        /// <summary>
        /// Current game state
        /// </summary>
        public GamePhase CurrentState => processor.CurrentState;

        /// <summary>
        /// Game state machine constructor
        /// </summary>
        public GameStateMachine()
        {
            processor = new();
        }

        /// <summary>
        /// Set an action to be performed when transitioning to a specific state.
        /// </summary>
        /// <param name="action">Action</param>
        /// <param name="transitioningTo">The state being transitioned to</param>
        public void SetActionOnTransition(Action action, GamePhase transitioningTo) => processor.SetActionOnTransition(action, transitioningTo);

        /// <summary>
        /// Start the game
        /// </summary>
        public void StartGame()
        {
            processor.MoveNext(Commands.StartGame);
        }

        /// <summary>
        /// Change the state of the game
        /// </summary>
        public void ChangeSeason()
        {
            processor.MoveNext(Commands.ChangeSeason);
        }
    }
}
