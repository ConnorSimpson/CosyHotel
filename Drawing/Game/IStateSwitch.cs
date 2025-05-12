using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using static GameState.States.StatesAndCommands;

namespace Drawing.Game
{
    /// <summary>
    /// State switch interface
    /// </summary>
    public interface IStateSwitch
    {
        /// <summary>
        /// Method to draw based on the game state
        /// </summary>
        /// <param name="state">Game State</param>
        public void DrawBasedOnGameState(GamePhase state);

        /// <summary>
        /// Method to update based on the game state
        /// </summary>
        /// <param name="gameTime">game time</param>
        /// <param name="state">Game State</param>
        /// <param name="mouseState">mouse state</param>
        public void UpdateBasedOnGameState(GameTime gameTime, GamePhase state, MouseState mouseState);
    }
}
