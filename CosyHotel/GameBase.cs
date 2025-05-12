using System;
using CosyHotel.Game;
using GameState;
using GameState.Information;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Screens.Handling;
using Screens.Menus;
using Tools;

namespace CosyHotel
{
    public class GameBase : Microsoft.Xna.Framework.Game
    {
        #region private members

        private GraphicsDeviceManager graphics;
        private CustomSpriteBatch spriteBatch;
        private MainScreenManager mainScreenHandler;

        private RenderTarget2D renderTarget;

        #endregion

        public GameBase()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Window.AllowUserResizing = true;
            IsMouseVisible = true;
        }

        /// <summary>
        /// Calculates the render destination for the sprite batch.
        /// </summary>
        public void CalculateRenderDestination()
        {
            Point size = GraphicsDevice.Viewport.Bounds.Size;

            float scaleX = (float)size.X / renderTarget.Width;
            float scaleY = (float)size.Y / renderTarget.Height;
            float scale = Math.Min(scaleX, scaleY);

            spriteBatch.RenderDestination.Width = (int)(renderTarget.Width * scale);
            spriteBatch.RenderDestination.Height = (int)(renderTarget.Height * scale);

            spriteBatch.RenderDestination.X = (size.X - spriteBatch.RenderDestination.Width) / 2;
            spriteBatch.RenderDestination.Y = (size.Y - spriteBatch.RenderDestination.Height) / 2;
        }

        #region protected methods

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = Constants.Native_Width;
            graphics.PreferredBackBufferHeight = Constants.Native_Height;
            //graphics.PreferredBackBufferWidth = 640;
            //GameBase.Native_Height = 480;
            graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new CustomSpriteBatch(GraphicsDevice);
            renderTarget = new RenderTarget2D(GraphicsDevice, Constants.Native_Width, Constants.Native_Height);

            CalculateRenderDestination();

            var screenRequirements = new ScreenRequirements(graphics, Content, spriteBatch);
            var mainMenu = new MainMenu(screenRequirements);
            var settingsMenu = new SettingsMenu(new Action(CalculateRenderDestination), Window, screenRequirements);

            //Temp INFO
            var tempGameInfo = new GameInformation();
            var gameScreen = new GameMain(tempGameInfo, screenRequirements);

            mainScreenHandler = new MainScreenManager(mainMenu, settingsMenu, gameScreen);

            mainMenu.CreateButtons();
            settingsMenu.CreateButtons();
            gameScreen.CreateButtons();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            mainScreenHandler.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            GraphicsDevice.SetRenderTarget(renderTarget);

            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            mainScreenHandler.Draw();
            spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);

            spriteBatch.Begin();
            spriteBatch.Draw(renderTarget, spriteBatch.RenderDestination, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        #endregion
    }
}
