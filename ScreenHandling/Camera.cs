using System.Drawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ScreenHandling
{
    public class Camera
    {
        #region private members

        private Vector2 position;
        private float zoom;
        private float rotation;

        private Viewport viewport;

        #endregion

        public Microsoft.Xna.Framework.Rectangle WorldBounds { get; set; }
        public Vector2 Position => position;

        public Camera(Viewport viewport)
        {
            zoom = 1f;
            rotation = 0;
            this.viewport = viewport;
        }

        public void SetZoom(float newZoom)
        {
            zoom = MathHelper.Clamp(newZoom, 1f, 5f);
        }

        public Matrix GetViewMatrix()
        {
            return Matrix.CreateTranslation(new Vector3(-position, 0f)) *
                   Matrix.CreateRotationZ(rotation) *
                   Matrix.CreateScale(zoom, zoom, 1f) *
                   Matrix.CreateTranslation(new Vector3(viewport.Width * 0.5f, viewport.Height * 0.5f, 0f));
        }

        public void Follow(Vector2 targetPosition)
        {
            position = targetPosition;

            // Clamp the camera to stay within the bounds of the world
            var halfScreenWidth = viewport.Width / 2f / zoom;
            var halfScreenHeight = viewport.Height / 2f / zoom;

            float minX = WorldBounds.Left + halfScreenWidth;
            float maxX = WorldBounds.Right - halfScreenWidth;
            float minY = WorldBounds.Top + halfScreenHeight;
            float maxY = WorldBounds.Bottom - halfScreenHeight;

            position.X = MathHelper.Clamp(position.X, minX, maxX);
            position.Y = MathHelper.Clamp(position.Y, minY, maxY);
        }
    }
}
