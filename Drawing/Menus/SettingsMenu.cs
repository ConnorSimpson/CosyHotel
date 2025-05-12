using Drawing.Common;
using Microsoft.Xna.Framework;
using Screens.Handling;

namespace Screens.Menus
{
    /// <summary>
    /// SettingsMenu class that implements IScreen.
    /// </summary>
    public class SettingsMenu : ScreenBase
    {
        #region private members

        private Action resizeAction;

        private List<ActionButton> resolutionButtons = new List<ActionButton>();
        private ActionButton fullScreenButton;
        private ActionButton borderlessWindowButton;

        private MenuButton backToMenuButton;
        private GameWindow window;

        #endregion

        public EventHandler Back;

        /// <summary>
        /// SettingsMenu constructor
        /// </summary>
        /// <param name="graphics">graphics</param>
        /// <param name="content">content</param>
        /// <param name="spriteBatch">batch</param>
        public SettingsMenu(Action resizeAction, GameWindow window, ScreenRequirements screenControls) : base(screenControls)
        {
            this.resizeAction = resizeAction;
            this.window = window;
        }

        public void CreateButtons()
        {
            fullScreenButton = new ActionButton("Full Screen", new Action(ToggleFullScreen), graphics, content, spriteBatch);
            borderlessWindowButton = new ActionButton("Borderless Window", new Action(ToggleBorderlessWindow), graphics, content, spriteBatch);
            backToMenuButton = new MenuButton("Back", Back, graphics, content, spriteBatch);

            resolutionButtons.Add(new ActionButton("1920x1080", new Action(() => ChangeResolution(1920, 1080)), graphics, content, spriteBatch));
            resolutionButtons.Add(new ActionButton("1280x720", new Action(() => ChangeResolution(1280, 720)), graphics, content, spriteBatch));
            resolutionButtons.Add(new ActionButton("800x450", new Action(() => ChangeResolution(800, 450)), graphics, content, spriteBatch));

        }

        ///<inheritdoc/>
        public override void Draw()
        {
            fullScreenButton.Draw(100, 100);
            borderlessWindowButton.Draw(100, 200);
            backToMenuButton.Draw(100, 600);

            for (int i = 0; i < resolutionButtons.Count; i++)
            {
                resolutionButtons[i].Draw(100, 300 + i * 50);
            }
        }

        ///<inheritdoc/>
        public override void Hide()
        {
            //AUDIO END?
        }

        ///<inheritdoc/>
        public override void Show()
        {
            //AUDIO things
        }

        ///<inheritdoc/>
        public override void Update(GameTime gameTime)
        {
            var mouseState = Tools.MouseInput.GetInput();

            fullScreenButton.Hover(mouseState.X, mouseState.Y);
            backToMenuButton.Hover(mouseState.X, mouseState.Y);
            borderlessWindowButton.Hover(mouseState.X, mouseState.Y);

            resolutionButtons.ForEach(button => button.Hover(mouseState.X, mouseState.Y));

            if (Tools.MouseInput.HasLeftClickBeenPressed(out var clickLocation))
            {
                //Maybe put all buttons in a list and iterate through them
                if (backToMenuButton.InBounds(mouseState.X, mouseState.Y))
                    backToMenuButton.Click();

                if (fullScreenButton.InBounds(mouseState.X, mouseState.Y))
                    fullScreenButton.Click();

                if (borderlessWindowButton.InBounds(mouseState.X, mouseState.Y))
                    borderlessWindowButton.Click();

                var result = resolutionButtons.FirstOrDefault(button => button.InBounds(clickLocation.x, clickLocation.y));

                if (result is not null)
                    result.Click();
            }
        }

        #region private methods

        private void ChangeResolution(int width, int height)
        {
            graphics.PreferredBackBufferWidth = width;
            graphics.PreferredBackBufferHeight = height;
            graphics.ApplyChanges();

            resizeAction.Invoke();
        }

        private void ToggleFullScreen()
        {
            graphics.IsFullScreen = !graphics.IsFullScreen;
            graphics.ApplyChanges();
            resizeAction.Invoke();
        }

        private void ToggleBorderlessWindow()
        {
            window.IsBorderless = !window.IsBorderless;
            graphics.ApplyChanges();
            resizeAction.Invoke();
        }

        #endregion
    }
}
