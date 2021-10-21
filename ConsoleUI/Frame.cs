﻿using System;
using ConsoleUI.Structs;
using ConsoleUI.Event;

namespace ConsoleUI
{
    /// <summary>
    /// Toplevel UI class responsible for managing other UI elements.
    /// </summary>
    public abstract class CFrame : CComponent
    {
        protected int           m_activePageIndex;
        protected CPage[]       m_pages;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="title">Window title</param>
        /// <param name="rect">Size of the window/buffer</param>
        /// <param name="pageCount">Number of pages</param>
        public CFrame(string title, ConsoleRect rect, int pageCount) : base(title, rect)
        {
            m_activePageIndex = 0;
            m_pages           = new CPage[pageCount];
        }

        /// <summary>
        /// Initialise the console window
        /// </summary>
        public override void Initialise()
        {
            CConsoleWindow.InitialiseWindow(m_rect.width, m_rect.height, m_title);
        }

        /// <summary>
        /// Main event loop for the window class
        /// Get input, despatch to relevant control element and redraw the element
        /// </summary>
        public virtual void WindowMain()
        {
            while(true)
            {
                Console.CursorVisible = false;
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                m_pages[m_activePageIndex].KeyPress(keyInfo);
                m_pages[m_activePageIndex].Draw(false);
            }
        }

        public override void Draw(bool redraw)
        {
            if(redraw)
            {
                CConsoleDraw.DrawColourRect(m_rect, ConsoleColor.Gray);
                DrawHeader();
            }
            for(int i = 0; i < m_pages.Length; i++)
            {
                if(m_pages[i] != null)
                {
                    m_pages[i].Draw(redraw);
                }
            }
        }

        /// <summary>
        /// Draw frame header
        /// </summary>
        protected virtual void DrawHeader()
        {
            CConsoleDraw.WriteText(string.Format(" {0} ", m_title).PadCenter(m_rect.width, '-'), 0, 0, ConsoleColor.White, ConsoleColor.Blue);
            // TODO, draw current page number and title on the right hand side
        }

        public abstract void OnCommand(object sender, GenericEventArgs<string> e);
    }
}
