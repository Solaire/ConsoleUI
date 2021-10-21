﻿using System;
using ConsoleUI.Structs;

namespace ConsoleUI
{
    /// <summary>
    /// Console window management helper class
    /// </summary>
    public static class CConsoleWindow
    {
        /// <summary>
        /// Initialise console window
        /// </summary>
        /// <param name="width">The window width</param>
        /// <param name="height">The window height</param>
        /// <param name="title">The window tititle</param>
        public static void InitialiseWindow(int width, int height, string title)
        {
            Console.Title = title;
            UpdateWindowSize(width, height);
        }

        /// <summary>
        /// Update console window and buffer size and flush the buffer
        /// </summary>
        /// <param name="width">New window/buffer width</param>
        /// <param name="height">New window/buffer height</param>
        private static void UpdateWindowSize(int width, int height)
        {
            Console.CursorVisible = false;

            if(width > Console.BufferWidth)
            {
                Console.BufferWidth = width;
                Console.WindowWidth = width;
            }
            else
            {
                Console.WindowWidth = width;
                Console.BufferWidth = width;
            }

            if(height > Console.BufferHeight)
            {
                Console.BufferHeight = height;
                Console.WindowHeight = height;
            }
            else
            {
                Console.WindowHeight = height;
                Console.BufferHeight = height;
            }

            ConsoleRect rect;
            rect.x = 0;
            rect.y = 0;
            rect.width = width;
            rect.height = height;
            Console.BackgroundColor = ConsoleColor.Gray;

            CConsoleDraw.DrawColourRect(rect, Console.BackgroundColor); // Flush buffer
        }

        /*
        private static void SetupWindow()
        {
            m_windowRect.height = Console.BufferHeight;

            int whereToMove = Console.CursorTop + 1; // One line below the visible window height
            if(whereToMove < Console.WindowHeight)
                whereToMove = Console.WindowHeight + 1;

            if(Console.BufferHeight < whereToMove + Console.WindowHeight) // Fix buffer size
                Console.BufferHeight = whereToMove + Console.WindowHeight;

            Console.MoveBufferArea(0, 0, Console.WindowWidth, Console.WindowHeight, 0, whereToMove);

            Console.CursorVisible = false;
            m_windowRect.x = Console.CursorTop;
            m_windowRect.y = Console.CursorLeft;
            m_defaultColours.foreground = Console.ForegroundColor;
            m_defaultColours.background = Console.BackgroundColor;

            Console.CursorTop  = 0;
            Console.CursorLeft = 0;
        }

        public static void EndWindow()
        {
            Console.ForegroundColor = m_defaultColours.foreground;
            Console.BackgroundColor = m_defaultColours.background;

            int whereToGet = m_windowRect.x; // One line below the visible window height
            if(whereToGet < Console.WindowHeight)
                whereToGet = Console.WindowHeight + 1;

            Console.MoveBufferArea(0, whereToGet, Console.WindowWidth, Console.WindowHeight, 0, 0);

            Console.CursorTop  = m_windowRect.x;
            Console.CursorLeft = m_windowRect.y;

            Console.CursorVisible = true;
            Console.BufferHeight  = m_windowRect.height;
        }
        */
    }
}
