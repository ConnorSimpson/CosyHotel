using Microsoft.Xna.Framework;

namespace Screens.Handling
{
    /// <summary>
    /// Interface for a screens.
    /// </summary>
    public interface IScreen
    {
        /// <summary>
        /// Calls when window first shows
        /// </summary>
        void Show();

        /// <summary>
        /// Calls when window is hidden
        /// </summary>
        void Hide();

        /// <summary>
        /// Updates game logic.
        /// </summary>
        /// <param name="gameTime">game time</param>
        void Update(GameTime gameTime);

        /// <summary>
        /// Draw on the screen.
        /// </summary>
        void Draw();
    }
}
