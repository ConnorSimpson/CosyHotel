using Drawing.Common;
using GameState;
using GameState.Information;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Tools;

namespace Drawing.Drawing
{
    /// <summary>
    /// UserInterface class while in game.
    /// </summary>
    public class UserInterface
    {
        #region private members

        private ContentManager content;
        private SpriteBatch spriteBatch;
        private GraphicsDeviceManager graphics;
        private GameInformation gameInfo;

        private Texture2D placeholder;
        private SpriteFont font;

        private ActionButton nextStateButton;

        #endregion

        /// <summary>
        /// UserInterface constructor
        /// </summary>
        /// <param name="graphics">graphics</param>
        /// <param name="content">content</param>
        /// <param name="spriteBatch">spriteBatch</param>
        public UserInterface(GameInformation gameInfo, GraphicsDeviceManager graphics, ContentManager content, SpriteBatch spriteBatch)
        {
            this.graphics = graphics;
            this.content = content;
            this.spriteBatch = spriteBatch;
            this.gameInfo = gameInfo;

            font = content.Load<SpriteFont>("GUI/Fonts/BaseFont");
            placeholder = content.Load<Texture2D>("GUI/GameUI");
            nextStateButton = new ActionButton("Next State", gameInfo.GameState.ChangeSeason, graphics, content, spriteBatch);
        }

        /// <summary>
        /// Draw the user interface
        /// </summary>
        public void Draw()
        {
            spriteBatch.Draw(placeholder, new Rectangle(0, 0, Constants.Native_Width, Constants.Native_Height), Color.White);
            spriteBatch.DrawString(font, gameInfo.GameState.CurrentState.ToString(), new Vector2(Constants.Native_Width / 2, 10), Color.White);

            nextStateButton.Draw(10, 5);
        }

        /// <summary>
        /// Update the user interface
        /// </summary>
        /// <param name="gameTime">game time</param>
        /// <param name="mouseState">mouse state</param>
        public void Update(GameTime gameTime, MouseState mouseState)
        {
            nextStateButton.Hover(mouseState.X, mouseState.Y);

            if (Tools.MouseInput.HasLeftClickBeenPressed(out var clickLocation))
            {
                if (nextStateButton.InBounds(clickLocation.x, clickLocation.y))
                    nextStateButton.Click();
            }
        }
    }
}
