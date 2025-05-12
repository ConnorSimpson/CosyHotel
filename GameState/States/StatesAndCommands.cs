namespace GameState.States
{
    /// <summary>
    /// This class contains the different game states and commands.
    /// </summary>
    public class StatesAndCommands
    {
        /// <summary>
        /// This enum represents the different game states.
        /// </summary>
        public enum GamePhase
        {
            LoadIn,
            Spring,
            Summer,
            Autumn,
            Winter,
            GameOver,
        }

        /// <summary>
        /// This enum represents the different commands that can be issued in the game.
        /// </summary>
        public enum Commands
        {
            StartGame,
            ChangeSeason,
            EndGame,
        }
    }
}
