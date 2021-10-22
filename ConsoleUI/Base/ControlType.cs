using ConsoleUI.Type;

namespace ConsoleUI.Base
{
    /// <summary>
    /// Structure holding initialisation data for a view object
    /// </summary>
    public struct ViewInitData
    {
        public readonly string      title;
        public readonly int         percentWidth;
        public readonly int         percentHeight;
        public readonly ConsoleRect rect;

        /// <summary>
        /// Constructor.
        /// Sets the percentage size values (for dynamic components)
        /// </summary>
        /// <param name="title">The control title</param>
        /// <param name="percentWidth">Control width as percentage of parent area</param>
        /// <param name="percentHeight">Control height as percentage of parent area</param>
        public ViewInitData(string title, int percentWidth, int percentHeight)
        {
            this.title         = title;
            this.percentWidth  = percentWidth;
            this.percentHeight = percentHeight;

            rect = new ConsoleRect(0, 0, 0, 0); // Not used
        }

        /// <summary>
        /// Constructor.
        /// Set the control's size and position values (for static components)
        /// </summary>
        /// <param name="title">The control title</param>
        /// <param name="rect">Size and position of the control</param>
        public ViewInitData(string title, ConsoleRect rect)
        {
            this.title = title;
            this.rect  = rect;

            // Not used
            percentWidth  = 0; 
            percentHeight = 0;
        }
    }
}
