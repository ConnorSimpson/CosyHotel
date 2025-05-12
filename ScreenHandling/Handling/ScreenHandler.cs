using Microsoft.Xna.Framework;

namespace Screens.Handling
{
    /// <summary>
    /// Base class for handling screens.
    /// </summary>
    public abstract class ScreenHandler
    {
        /// <summary>
        /// Current screen.
        /// </summary>
        public IScreen CurrentScreen { get; private set; }

        public void Update(GameTime gameTime)
        {
            CurrentScreen?.Update(gameTime);
        }

        public void Draw()
        {
            CurrentScreen?.Draw();
        }

        protected void ChangeScreen(IScreen newScreen)
        {
            CurrentScreen?.Hide();
            CurrentScreen = newScreen;
            CurrentScreen.Show();
        }
    }
}
