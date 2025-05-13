using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Tools;

namespace Drawing.Common
{
    /// <summary>
    /// MenuButton class that inherits from ButtonBase.
    /// </summary>
    public class MenuButton : ButtonBase
    {
        #region private members

        private EventHandler clickEvent;

        #endregion

        /// <summary>
        /// Constructor for the MenuButton class.
        /// </summary>
        /// <param name="label"><inheritdoc/></param>
        /// <param name="clickEvent">eventHandler</param>
        /// <param name="graphics"><inheritdoc/></param>
        /// <param name="content"><inheritdoc/></param>
        /// <param name="spriteBatch"><inheritdoc/></param>
        public MenuButton(string label, EventHandler clickEvent, GraphicsDeviceManager graphics, ContentManager content, SpriteBatch spriteBatch)
            : base(label, 100, 50, true, content.Load<Texture2D>("GUI/Button"), content.Load<Texture2D>("GUI/ButtonHover"), content, spriteBatch)
        {
            this.clickEvent = clickEvent;
        }

        /// <summary>
        /// Invoke the event
        /// </summary>
        public override void Click()
        {
            clickEvent.Invoke(null, EventArgs.Empty);
        }
    }
}
