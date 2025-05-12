using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Tools;

namespace Drawing.Common
{
    /// <summary>
    /// Button that performs an action when clicked.
    /// </summary>
    public class ActionButton : ButtonBase
    {
        #region private members

        private Action action;

        #endregion

        /// <summary>
        /// Action Button constructor
        /// </summary>
        /// <param name="label"><inheritdoc/></param>
        /// <param name="action">action to invoke</param>
        /// <param name="graphics"><inheritdoc/></param>
        /// <param name="content"><inheritdoc/></param>
        /// <param name="spriteBatch"><inheritdoc/></param>
        public ActionButton(string label, Action action, GraphicsDeviceManager graphics, ContentManager content, CustomSpriteBatch spriteBatch)
            : base(label, 100, 50, true, content.Load<Texture2D>("GUI/Button"), content.Load<Texture2D>("GUI/ButtonHover"), content, spriteBatch)
        {
            this.action = action;
        }

        /// <summary>
        /// Invoke action
        /// </summary>
        public override void Click()
        {
            action.Invoke();
        }
    }
}
