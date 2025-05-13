using System;
using Drawing.Common;
using Drawing.Drawing;
using GameState;
using GameState.Information;
using MapBuilder;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ScreenHandling;
using Screens.Handling;
using Tools;

namespace CosyHotel.Game
{
    /// <summary>
    /// Main screen for the game.
    /// </summary>
    public class GameMain : ScreenBase
    {
        #region private members

        private GameInformation gameInfo;
        private ScreenRequirements screenControls;

        private UserInterface userInterface;
        private MenuButton backButton;

        private Texture2D fullBackground;
        private Texture2D background;
        private Texture2D miniMapbackground;
        private Texture2D playerPlaceholder;

        private int boardViewX = 0;
        private int boardViewY = 0;

        private Texture2D[,] areaMapTiles;
        private int areaMapWidth;
        private int areaMapHeight;

        private Rectangle worldBounds;
        private Vector2 playerPosition;

        private SpriteBatch gameSpriteBatch;

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
            this.screenControls = screenControls;

            var mm = new MapMaker(screenControls);

            areaMapTiles = mm.ReadAreaMapColors();
            areaMapWidth = areaMapTiles.GetLength(0);
            areaMapHeight = areaMapTiles.GetLength(1);

            gameSpriteBatch = new SpriteBatch(graphics.GraphicsDevice);
            screenControls.Camera.SetZoom(4f);
            screenControls.Camera.WorldBounds = new Rectangle(0, 0, 1024, 1024);
            playerPosition = new Vector2(500, 500);

            fullBackground = content.Load<Texture2D>("Backgrounds/MapBackground");
            background = content.Load<Texture2D>("Backgrounds/MapBackground");
            miniMapbackground = content.Load<Texture2D>("Backgrounds/MiniMapBackground");
            playerPlaceholder = content.Load<Texture2D>("GUI/WhiteBox");

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
            //spriteBatch.Draw(fullBackground, new Rectangle(0, 0, Constants.Native_Width, Constants.Native_Height), Color.White);

            gameSpriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: screenControls.Camera.GetViewMatrix());

            for (int i = 0; i < areaMapWidth; i++)
            {
                for (int j = 0; j < areaMapHeight; j++)
                {
                    gameSpriteBatch.Draw(areaMapTiles[i, j], new Rectangle(i * Constants.TileSize + boardViewX, j * Constants.TileSize + boardViewY, Constants.TileSize, Constants.TileSize), Color.White);
                }
            }

            gameSpriteBatch.Draw(playerPlaceholder, new Rectangle((int)playerPosition.X, (int)playerPosition.Y,16,32), Color.White);

            gameSpriteBatch.End();

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

            if (Tools.KeyboardInput.IsPressed(Keys.D))
                playerPosition.X += 8;
            if (Tools.KeyboardInput.IsPressed(Keys.A))
                playerPosition.X -= 8;
            if (Tools.KeyboardInput.IsPressed(Keys.W))
                playerPosition.Y -= 8;
            if (Tools.KeyboardInput.IsPressed(Keys.S))
                playerPosition.Y += 8;

            backButton.Hover(mouseState.X, mouseState.Y);

            if (Tools.MouseInput.HasLeftClickBeenPressed(out var clickLocation))
            {
                if (backButton.InBounds(clickLocation.x, clickLocation.y))
                    backButton.Click();
            }

            userInterface.Update(gameTime, mouseState);
            screenControls.Camera.Follow(playerPosition);
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
