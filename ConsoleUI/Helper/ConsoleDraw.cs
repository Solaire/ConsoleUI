using System;
using System.Text;
using ConsoleUI.Type;

namespace ConsoleUI.Helper
{
    /// <summary>
    /// Console drawing helper class
    /// TODO: buffer expection handling
    /// </summary>
    public static class CConsoleDraw
    {
        private const char BORDER_VERTICAL     = '│';
        private const char BORDER_HORIZONTAL   = '─';
        private const char BORDER_TOP_LEFT     = '┌';
        private const char BORDER_TOP_RIGHT    = '┐';
        private const char BORDER_BOTTOM_LEFT  = '└';
        private const char BORDER_BOTTOM_RIGHT = '┘';
                      

        /// <summary>
        /// Draw rectangle
        /// </summary>
        /// <param name="rect">The rectangle</param>
        /// <param name="colourBG">Backgound colour</param>
        public static void DrawColourRect(ConsoleRect rect, ConsoleColor colourBG)
        {
            Console.BackgroundColor = colourBG;

            // Draw row by row
            for(int row = rect.y; row < rect.y + rect.height; row++)
            {
                Console.CursorLeft = rect.x;
                Console.CursorTop = row;

                Console.WriteLine("".PadLeft(rect.width));
            }
        }

        /// <summary>
        /// Draw a box
        /// </summary>
        /// <param name="rect">The rectangle</param>
        /// <param name="colourFG">Foreground colour</param>
        /// <param name="colourBG">Background colour</param>
        /// <param name="header">Optional header which will be drawn on the top</param>
        public static void DrawBox(ConsoleRect rect, ConsoleColor colourFG, ConsoleColor colourBG, string header = "")
        {
            if(rect.width == 0 || rect.height == 0)
            {
                return;
            }

            // Ensure rect will fit and rescale if needed
            rect.x      = Math.Min(Console.BufferWidth,  Math.Max(0, rect.x));
            rect.y      = Math.Min(Console.BufferHeight, Math.Max(0, rect.y));
            rect.width  = (rect.Right > Console.BufferWidth)   ? Console.BufferWidth  : rect.width;
            rect.height = (rect.Bottom > Console.BufferHeight) ? Console.BufferHeight : rect.height;

            // Create border walls
            StringBuilder top = new StringBuilder(header.PadCenter(rect.width, BORDER_HORIZONTAL));
            top[0] = BORDER_TOP_LEFT;
            top[top.Length - 1] = BORDER_TOP_RIGHT;

            StringBuilder bottom = new StringBuilder("".PadRight(rect.width, BORDER_HORIZONTAL));
            bottom[0] = BORDER_BOTTOM_LEFT;
            bottom[bottom.Length - 1] = BORDER_BOTTOM_RIGHT;

            StringBuilder sides = new StringBuilder("".PadRight(rect.width));
            sides[0] = BORDER_VERTICAL;
            sides[sides.Length - 1] = BORDER_VERTICAL;

            // Draw
            int x   = rect.x;
            int row = rect.y;

            WriteText(top.ToString(), x, row++, colourFG, colourBG);
            while(row < rect.Bottom)
            {
                WriteText(sides.ToString(), x, row++, colourFG, colourBG);
            }
            WriteText(bottom.ToString(), x, row++, colourFG, colourBG);
        }

        /// <summary>
        /// Draw horizontal line
        /// </summary>
        /// <param name="x">Starting x coordinate</param>
        /// <param name="y">Starting y coordinate</param>
        /// <param name="length">Length of the line</param>
        /// /// <param name="colourFG">Foregound colour</param>
        /// <param name="colourBG">Background colour</param>
        public static void DrawHorizontalLine(int x, int y, int length, ConsoleColor colourFG, ConsoleColor colourBG)
        {
            Console.BackgroundColor = colourBG;
            Console.ForegroundColor = colourFG;

            for(int col = x; col < x + length; col++)
            {
                Console.CursorLeft = col;
                Console.CursorTop = y;

                Console.Write("-");
            }
        }

        /// <summary>
        /// Draw horizontal line
        /// </summary>
        /// <param name="x">Starting x coordinate</param>
        /// <param name="y">Starting y coordinate</param>
        /// <param name="length">Length of the line</param>
        /// <param name="colourFG">Foregound colour</param>
        /// <param name="colourBG">Background colour</param>
        public static void DrawVerticalLine(int x, int y, int length, ConsoleColor colourFG, ConsoleColor colourBG)
        {
            Console.BackgroundColor = colourBG;
            Console.ForegroundColor = colourFG;

            x = Math.Min(Console.BufferWidth,  Math.Max(0, x));
            y = Math.Min(Console.BufferHeight, Math.Max(0, y));

            for(int row = y; row < y + length; row++)
            {
                Console.CursorLeft = x;
                Console.CursorTop = row;

                Console.Write("|");
            }
        }

        public static void WriteText(string text, int x, int y, ConsoleColor colourFG, ConsoleColor colourBG)
        {
            if((x < 0 || x > Console.BufferWidth) || (y < 0 || y > Console.BufferHeight))
            {
                return;
            }

            Console.CursorVisible = false;

            Console.CursorLeft = x;
            Console.CursorTop = y;

            Console.BackgroundColor = colourBG;
            Console.ForegroundColor = colourFG;

            Console.Write(text);
        }
    }
}
