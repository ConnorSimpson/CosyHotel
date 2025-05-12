using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Tools;

namespace Drawing.Common
{
    /// <summary>
    /// Base class for buttons in the GUI.
    /// </summary>
    public abstract class ButtonBase : IClickable
    {
        #region private members

        private ContentManager content;
        private CustomSpriteBatch spriteBatch;
        private SpriteFont font;

        private Texture2D button;
        private Texture2D buttonHover;

        private int width;
        private int height;

        private int locationX;
        private int locationY;

        private string label;
        private bool isHovered = false;
        private bool surroundHover;

        #endregion

        /// <summary>
        /// Constructor for the button base class.
        /// </summary>
        /// <param name="label">string the button will display</param>
        /// <param name="graphics">graphics</param>
        /// <param name="content">content</param>
        /// <param name="spriteBatch">batch</param>
        public ButtonBase(string label, int width, int height, bool surroundHover, Texture2D button, Texture2D hover, ContentManager content, CustomSpriteBatch spriteBatch)
        {
            this.content = content;
            this.spriteBatch = spriteBatch;
            this.label = label;

            this.width = width;
            this.height = height;

            this.button = button;
            this.buttonHover = hover;

            this.surroundHover = surroundHover;

            font = content.Load<SpriteFont>("GUI/Fonts/BaseFont");
        }

        /// <summary>
        /// Draw the button to the screen.
        /// </summary>
        /// <param name="x">x start point</param>
        /// <param name="y">y start point</param>
        public void Draw(int x, int y)
        {
            spriteBatch.Draw(button, new Rectangle(x, y, width, height), Color.White);
            //Centre the text
            var textLengthPx = font.MeasureString(label);
            var textXLocation = x + width / 2 - textLengthPx.X / 2;
            var textYLocation = y + height / 2 - textLengthPx.Y / 2;
            spriteBatch.DrawString(font, label, new Vector2(textXLocation, textYLocation), Color.Green);

            locationX = x;
            locationY = y;

            if (isHovered)
            {
                if (surroundHover)
                    spriteBatch.Draw(buttonHover, new Rectangle(x - 2, y - 2, width + 4, height + 4), Color.White);
                else
                    spriteBatch.Draw(buttonHover, new Rectangle(x, y, width, height), Color.White);
            }
        }

        ///<inheritdoc/>
        public bool InBounds(int x, int y)
        {
            float scaleX = (float)spriteBatch.RenderDestination.Width / spriteBatch.GraphicsDevice.DisplayMode.Width;
            float scaleY = (float)spriteBatch.RenderDestination.Height / spriteBatch.GraphicsDevice.DisplayMode.Height;
            float scale = Math.Min(scaleX, scaleY);

            var xAddional = spriteBatch.RenderDestination.X;
            var yAddional = spriteBatch.RenderDestination.Y;

            return x - xAddional >= (locationX * scaleX) && x - xAddional <= (locationX * scaleX) + (width * scaleX) && y - yAddional >= (locationY * scaleY) && y - yAddional <= (locationY * scaleY) + (height * scaleY);
        }

        ///<inheritdoc/>
        public void Hover(int x, int y)
        {
            if (InBounds(x, y))
                isHovered = true;
            else
                isHovered = false;
        }

        ///<inheritdoc/>
        public abstract void Click();
    }
}
