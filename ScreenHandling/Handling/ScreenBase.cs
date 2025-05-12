using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Tools;

namespace Screens.Handling
{
    /// <summary>
    /// Screen base class that implements IScreen interface.
    /// </summary>
    public abstract class ScreenBase : IScreen
    {
        #region private methods

        protected ContentManager content;
        protected GraphicsDeviceManager graphics;
        protected CustomSpriteBatch spriteBatch;

        #endregion

        /// <summary>
        /// Constructor for the ScreenBase class.
        /// </summary>
        /// <param name="graphics">graphics</param>
        /// <param name="content">content</param>
        /// <param name="spriteBatch">spriteBatch</param>
        public ScreenBase(ScreenRequirements screenControls)
        {
            this.graphics = screenControls.graphics;
            this.content = screenControls.content;
            this.spriteBatch = screenControls.spriteBatch;
        }

        ///<inheritdoc/>
        public abstract void Draw();

        ///<inheritdoc/>
        public abstract void Hide();

        ///<inheritdoc/>
        public abstract void Show();

        ///<inheritdoc/>
        public abstract void Update(GameTime gameTime);
    }

    public class ScreenRequirements
    {
        public GraphicsDeviceManager graphics;

        public ContentManager content;

        public CustomSpriteBatch spriteBatch;

        public ScreenRequirements(GraphicsDeviceManager graphics, ContentManager content, CustomSpriteBatch spriteBatch)
        {
            this.graphics = graphics;
            this.content = content;
            this.spriteBatch = spriteBatch;
        }
    }
}
