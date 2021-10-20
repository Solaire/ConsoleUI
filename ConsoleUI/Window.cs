using System;
using ConsoleUI.Structs;
using ConsoleUI.Event;

namespace ConsoleUI
{
    /// <summary>
    /// Manage groups of static elements and interactive controls
    /// </summary>
    public abstract class CWindow : CElement
    {
        public ColourTheme  m_colours { get; private set; }

        protected int         m_activePageIndex;
        protected CPage[]     m_pages;
        //private CMinibuffer m_minibuffer;
        //private bool        m_isMinibufferFocused;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="title">Window title</param>
        /// <param name="rect">Size of the window/buffer</param>
        /// <param name="defaultColours">Default background and foreground colour pair</param>
        public CWindow(string title, ConsoleRect rect, ColourTheme colours, int pageCount) : base(rect, title)
        {
            m_colours = colours;

            m_activePageIndex = 0;
            m_pages           = new CPage[pageCount];

            //m_minibuffer          = new CMinibuffer(this);
            //m_isMinibufferFocused = true;

            CConsoleEx.InitialiseWindow(m_area.width, m_area.height, m_title);
            //m_minibuffer.Initialise();
        }

        /// <summary>
        /// Main event loop for the window class
        /// Get input, despatch to relevant control element and redraw the element
        /// </summary>
        public virtual void WindowMain()
        {
            while(true)
            {
                Console.CursorVisible = false; // m_isMinibufferFocused;
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                /*
                if(m_isMinibufferFocused)
                {
                    m_minibuffer.SetCursorPosition();
                    m_minibuffer.KeyPressed(keyInfo.Key);
                    m_minibuffer.Redraw(false);
                }
                else
                {
                    m_pages[m_activePageIndex].KeyPressed(keyInfo.Key);
                    m_pages[m_activePageIndex].Redraw(false);  
                }
                */
                m_pages[m_activePageIndex].KeyPressed(keyInfo);
                m_pages[m_activePageIndex].Redraw(false);
            }
        }

        public abstract void OnCommand(object sender, GenericEventArgs<string> e);
    }
}
