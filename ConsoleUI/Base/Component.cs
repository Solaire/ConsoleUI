using ConsoleUI.Type;

namespace ConsoleUI.Base
{
    /// <summary>
    /// Base class containing very basic and essential members and function definitions
    /// Should be inherited by all other UI abstract classes
    /// </summary>
    public abstract class CComponent
    {
        protected readonly string m_title;
        protected ConsoleRect     m_rect;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="title">The component title</param>
        /// <param name="rect">The component size and position</param>
        protected CComponent(string title, ConsoleRect rect)
        {
            m_title = title;
            m_rect  = rect;
        }

        public abstract void Draw(bool redraw);
        public abstract void KeyPress(System.ConsoleKeyInfo keyInfo);
    }
}
