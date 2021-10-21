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
}
