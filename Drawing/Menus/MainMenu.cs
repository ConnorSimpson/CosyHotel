using Drawing.Common;
using GameState.Information;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Screens.Handling;

namespace Screens.Menus
{
    public class MainMenu : ScreenBase
    {
        #region private members

        private SpriteFont font;
        private Texture2D background;
        private Texture2D menuUnderlay;
        private List<MenuButton> menuButtons = new List<MenuButton>();

        public EventHandler Play;
        public EventHandler Create;

        public EventHandler Settings;
        public EventHandler Armies;
        public EventHandler Exit;

        private enum MenuSelection
        {
            Play,
            Create,
            Settings,
            Exit
        }

        #endregion

        /// <summary>
        /// MainMenu constructor
        /// </summary>
        /// <param name="graphics">graphics</param>
        /// <param name="content">content</param>
        /// <param name="spriteBatch">batch</param>
        public MainMenu(ScreenRequirements screenControls) : base(screenControls)
        {
            font = content.Load<SpriteFont>("GUI/Fonts/BaseFont");
            background = content.Load<Texture2D>("Backgrounds/SpaceBackground");
            menuUnderlay = content.Load<Texture2D>("GUI/MainMenuUI");
            //Need multiple buttons
            var menuItems = Enum.GetValues(typeof(MenuSelection)).Cast<MenuSelection>();
        }

        /// <summary>
        /// Creates the navigation buttons for the menu.
        /// </summary>
        public void CreateButtons()
        {
            menuButtons.Add(new MenuButton(MenuSelection.Play.ToString(), Play, graphics, content, spriteBatch));
            menuButtons.Add(new MenuButton(MenuSelection.Create.ToString(), Create, graphics, content, spriteBatch));
            menuButtons.Add(new MenuButton(MenuSelection.Settings.ToString(), Settings, graphics, content, spriteBatch));
            menuButtons.Add(new MenuButton(MenuSelection.Exit.ToString(), Exit, graphics, content, spriteBatch));
        }

        /// <inheritdoc/>
        public override void Draw()
        {
            spriteBatch.Draw(background, new Rectangle(0, 0, Constants.Native_Width, Constants.Native_Height), Color.White);
            spriteBatch.Draw(menuUnderlay, new Rectangle((Constants.Native_Width / 2) - 128, 200, 256, 512), Color.White);
            spriteBatch.DrawString(font, "Celestial War", new Vector2(Constants.Native_Width / 2, 150), Color.White);
            for (int i = 0; i < menuButtons.Count; i++)
            {
                menuButtons[i].Draw((Constants.Native_Width / 2) - 50, (Constants.Native_Height / 4 + (70 * i)) - 15);
            }
        }

        /// <inheritdoc/>
        public override void Hide()
        {
            //AUDIO END?
        }

        /// <inheritdoc/>
        public override void Show()
        {
            //AUDIO things
        }

        /// <inheritdoc/>
        public override void Update(GameTime gameTime)
        {
            Tools.KeyboardInput.GetInput();
            var mouseState = Tools.MouseInput.GetInput();

            menuButtons.ForEach(button => button.Hover(mouseState.X, mouseState.Y));

            if (Tools.MouseInput.HasLeftClickBeenPressed(out var clickLocation))
            {
                var result = menuButtons.FirstOrDefault(button => button.InBounds(clickLocation.x, clickLocation.y));
                if (result is not null)
                    result.Click();
            }

            if (Tools.KeyboardInput.HasBeenPressed(Keys.Space) || Tools.KeyboardInput.HasBeenPressed(Keys.Enter))
                Settings.Invoke(this, EventArgs.Empty);
        }
    }
}
