using System;
using Drawing.Common;
using Drawing.Drawing;
using GameState;
using GameState.Information;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Screens.Handling;

namespace CosyHotel.Game
{
    /// <summary>
    /// Main screen for the game.
    /// </summary>
    public class GameMain : ScreenBase
    {
        #region private members

        private GameInformation gameInfo;

        private UserInterface userInterface;
        private MenuButton backButton;

        private Texture2D fullBackground;
        private Texture2D background;
        private Texture2D miniMapbackground;

        private int boardViewX = 0;
        private int boardViewY = 0;

        #endregion

        /// <summary>
        /// Back button event handler.
        /// </summary>
        public EventHandler Back;

        /// <summary>
        /// GameMain constructor
        /// </summary>
        /// <param name="graphics">graphic</param>
        /// <param name="content">content</param>
        /// <param name="spriteBatch">content</param>
        public GameMain(GameInformation gameInfo, ScreenRequirements screenControls) : base(screenControls)
        {
            this.gameInfo = gameInfo;

            fullBackground = content.Load<Texture2D>("Backgrounds/MapBackground");
            background = content.Load<Texture2D>("Backgrounds/MapBackground");
            miniMapbackground = content.Load<Texture2D>("Backgrounds/MiniMapBackground");

            userInterface = new UserInterface(gameInfo, graphics, content, spriteBatch);


            ScaleAtStart();

            //Temp start game
            gameInfo.GameState.StartGame();
        }

        /// <summary>
        /// Create the buttons for the game screen.
        /// </summary>
        public void CreateButtons()
        {
            backButton = new MenuButton("Back", Back, graphics, content, spriteBatch);
        }

        ///<inheritdoc/>
        public override void Draw()
        {
            spriteBatch.Draw(fullBackground, new Rectangle(0, 0, Constants.Native_Width, Constants.Native_Height), Color.White);

            userInterface.Draw();
            backButton.Draw(1800, 5);

            spriteBatch.Draw(miniMapbackground, new Rectangle(1555, 840, 355, 230), Color.White);
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
            Tools.KeyboardInput.GetInput();
            var mouseState = Tools.MouseInput.GetInput();

            if (Tools.KeyboardInput.HasBeenPressed(Keys.D))
                boardViewX += 20;
            if (Tools.KeyboardInput.HasBeenPressed(Keys.A))
                boardViewX -= 20;
            if (Tools.KeyboardInput.HasBeenPressed(Keys.W))
                boardViewY -= 20;
            if (Tools.KeyboardInput.HasBeenPressed(Keys.S))
                boardViewY += 20;

            if ((Tools.KeyboardInput.HasBeenPressed(Keys.Q) || Tools.MouseInput.IsMiddleClickScrollUp()) && spriteBatch.BoardViewScale < 0.7)
            {
                MouseZoom(mouseState, true);
            }
            if ((Tools.KeyboardInput.HasBeenPressed(Keys.E) || Tools.MouseInput.IsMiddleClickScrollDown()) && spriteBatch.BoardViewScale > 0.3)
            {
                MouseZoom(mouseState, false);
            }

            backButton.Hover(mouseState.X, mouseState.Y);

            if (Tools.MouseInput.HasLeftClickBeenPressed(out var clickLocation))
            {
                if (backButton.InBounds(clickLocation.x, clickLocation.y))
                    backButton.Click();
            }

            userInterface.Update(gameTime, mouseState);
        }

        #region private methods

        private void MouseZoom(MouseState ms, bool zoomIn)
        {

        }

        private void ScaleAtStart()
        {

        }

        #endregion
    }
}
