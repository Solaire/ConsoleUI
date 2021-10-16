using ConsoleUI.Structs;

namespace ConsoleUI.Event
{
    public class GenericEventArgs<T> : System.EventArgs
    {
        private readonly T m_eventData;

        public GenericEventArgs(T eventData)
        {
            m_eventData = eventData;
        }

        public T Data
        {
            get { return m_eventData; }
        }
    }

    public class ResizeEventArgs : System.EventArgs
    {
        private readonly ConsoleRect m_newSize;

        public ResizeEventArgs(ConsoleRect rect)
        {
            m_newSize = rect;
        }

        public ResizeEventArgs(int x, int y, int w, int h)
        {
            m_newSize = new ConsoleRect(x, y, w, h);
        }

        public ConsoleRect Rect
        {
            get { return m_newSize; }
        }
    }
}
