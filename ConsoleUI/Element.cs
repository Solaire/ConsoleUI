using ConsoleUI.Structs;
using ConsoleUI.Event;

namespace ConsoleUI
{
    /// <summary>
    /// Base class for providing mandatory members and functions to UI elements
    /// </summary>
    public abstract class CElement
    {
        protected ConsoleRect m_area;
        protected string      m_title;

        protected CElement(ConsoleRect area, string title = "")
        {
            m_area  = area;
            m_title = title;
        }

        public abstract void Initialise();
        public abstract void OnResize(object sender, ResizeEventArgs e);
    }
}
