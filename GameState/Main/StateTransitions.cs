using GameState.States;

namespace GameState.Main
{
    /// <summary>
    /// Class for handling state transitions in the game.
    /// </summary>
    public class StateTransitions
    {
        #region private members

        private GameStateMachine gameStateMachine;
        private GameInformation gameInformation;

        #endregion

        /// <summary>
        /// State transition constructor
        /// </summary>
        /// <param name="gameStateMachine">state machine</param>
        public StateTransitions(GameInformation gameInformation, GameStateMachine gameStateMachine)
        {
            this.gameStateMachine = gameStateMachine;
            this.gameInformation = gameInformation;
            gameStateMachine.SetActionOnTransition(SpringToAutumnTransition, StatesAndCommands.GamePhase.Autumn);
        }

        #region private methods

        private void SpringToAutumnTransition()
        {

        }

        #endregion
    }
}
