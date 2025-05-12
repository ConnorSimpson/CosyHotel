using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Tools;

namespace Drawing.Common
{
    public class SelectableButton : ButtonBase
    {
        #region private members

        private bool selected = false;

        private readonly Texture2D buttonSelect;
        private readonly CustomSpriteBatch spriteBatch;

        private readonly int width;
        private readonly int height;

        #endregion

        /// <summary>
        /// Is this button selected
        /// </summary>
        public bool IsSelected => selected;

        public SelectableButton(int width, int height, Texture2D button, Texture2D buttonHover, Texture2D buttonSelect, GraphicsDeviceManager graphics, ContentManager content, CustomSpriteBatch spriteBatch)
            : base("", width, height, false, button, buttonHover, content, spriteBatch)
        {
            this.buttonSelect = buttonSelect;
            this.spriteBatch = spriteBatch;

            this.width = width;
            this.height = height;
        }

        public void Draw(int x, int y)
        {
            if (!selected)
                base.Draw(x, y);
            else
                spriteBatch.Draw(buttonSelect, new Rectangle(x, y, width, height), Color.White);
        }

        public void UnSelect()
        {
            selected = false;
        }

        public override void Click()
        {
            selected = true;
        }
    }
}
