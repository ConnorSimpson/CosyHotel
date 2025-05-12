using static GameState.States.StatesAndCommands;

namespace GameState.States
{
    /// <summary>
    /// This class handles the game state transitions.
    /// </summary>
    public class GameStateProcess
    {
        class StateTransition
        {
            internal readonly GamePhase CurrentState;
            internal readonly Commands Command;
            internal Action transitionAction;

            public StateTransition(GamePhase currentState, Commands command)
            {
                CurrentState = currentState;
                Command = command;
            }

            public override int GetHashCode()
            {
                return 17 + 31 * CurrentState.GetHashCode() + 31 * Command.GetHashCode();
            }

            public override bool Equals(object? obj)
            {
                StateTransition other = obj as StateTransition;
                return other != null && this.CurrentState == other.CurrentState && this.Command == other.Command;
            }
        }

        Dictionary<StateTransition, GamePhase> transitions;
        public GamePhase CurrentState { get; private set; }

        public GameStateProcess()
        {
            CurrentState = GamePhase.LoadIn;

            transitions = new Dictionary<StateTransition, GamePhase>
            {
                { new StateTransition(GamePhase.LoadIn, Commands.StartGame), GamePhase.Spring },

                { new StateTransition(GamePhase.Spring, Commands.ChangeSeason), GamePhase.Summer },
                { new StateTransition(GamePhase.Summer, Commands.ChangeSeason), GamePhase.Autumn },
                { new StateTransition(GamePhase.Autumn, Commands.ChangeSeason), GamePhase.Winter },
                { new StateTransition(GamePhase.Winter, Commands.ChangeSeason), GamePhase.Spring },
            };
        }

        /// <summary>
        /// Set an action to be performed when transitioning to a specific state.
        /// </summary>
        /// <param name="action">Action</param>
        /// <param name="transitioningTo">The state being transitioned to</param>
        public void SetActionOnTransition(Action action, GamePhase transitioningTo)
        {
            foreach (var keyPair in transitions)
            {
                if (keyPair.Value.Equals(transitioningTo))
                {
                    keyPair.Key.transitionAction = action;
                }
            }
        }

        /// <summary>
        /// Get the next state based on the current state and command.
        /// </summary>
        /// <param name="command">transition command</param>
        /// <returns>next Game State</returns>
        /// <exception cref="Exception">Invalid command</exception>
        public GamePhase GetNext(Commands command)
        {
            var transition = Find(CurrentState, command);
            GamePhase nextState;

            if (!transitions.TryGetValue(transition, out nextState))
                throw new Exception("Invalid transition");

            transition.transitionAction?.Invoke();
            return nextState;
        }

        /// <summary>
        /// Move to the next state based on the command.
        /// </summary>
        /// <param name="command">command</param>
        /// <returns>new Game State</returns>
        public GamePhase MoveNext(Commands command)
        {
            CurrentState = GetNext(command);
            return CurrentState;
        }

        private StateTransition Find(GamePhase state, Commands command)
        {
            var result = transitions.Keys.FirstOrDefault(transition => transition.CurrentState.Equals(state) && transition.Command.Equals(command));

            if (result != null)
                return result;
            else
                throw new Exception("Invalid transition");
        }
    }
}
