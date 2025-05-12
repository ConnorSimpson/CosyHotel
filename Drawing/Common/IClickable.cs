namespace Drawing.Common
{
    /// <summary>
    /// Interface for clickable objects.
    /// </summary>
    public interface IClickable
    {
        /// <summary>
        /// Checks if the given coordinates are within the bounds of the clickable object.
        /// </summary>
        /// <param name="x">x coordinate</param>
        /// <param name="y">y coordinate</param>
        /// <returns></returns>
        bool InBounds(int x, int y);

        /// <summary>
        /// Do something when the mouse hovers over the clickable object.
        /// </summary>
        /// <param name="x">x coordinate</param>
        /// <param name="y">y coordinate</param>
        void Hover(int x, int y);

        /// <summary>
        /// Do something when the mouse is clicked on the clickable object.
        /// </summary>
        void Click();
    }
}
