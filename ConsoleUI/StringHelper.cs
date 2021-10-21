using System;

namespace ConsoleUI
{
    /// <summary>
    /// String extension methods
    /// </summary>
    public static class CStringHelper
    {
        /// <summary>
        /// Returns a new string that center aligns the characters in a 
        /// string by padding them on the left and right with a specified 
        /// character, of a specified total length
        /// </summary>
        /// <param name="src">The source string</param>
        /// <param name="totalLength">Total length of output string</param>
        /// <param name="paddingChar">The padding character, with ' ' as default</param>
        /// <returns>Center-aligned string with length of totalWidth, padded with the paddingChar</returns>
        public static string PadCenter(this string src, int totalLength, char paddingChar = ' ')
        {
            int spaces  = totalLength - src.Length;
            int padLeft = spaces / 2 + src.Length;
            return src.PadLeft(padLeft, paddingChar).PadRight(totalLength, paddingChar);
        }

        /// <summary>
        /// Extract the leftmost (count) characters from a string
        /// </summary>
        /// <param name="src">The source string</param>
        /// <param name="count">The number of leftmost characters to extract</param>
        /// <returns>A copy of the source string with the specifiec range of characters. The string might be empty</returns>
        public static string Left(this string src, int count)
        {
            count = Math.Min(src.Length, count);
            return src.Substring(0, count);
        }

        /// <summary>
        /// Extract the rightmost (count) characters from a string
        /// </summary>
        /// <param name="src">The source string</param>
        /// <param name="count">The number of rightmost characters to extract</param>
        /// <returns>A copy of the source string with the specifiec range of characters. The string might be empty</returns>
        public static string Right(this string src, int count)
        {
            count     = Math.Min(src.Length, count);
            int start = Math.Max(src.Length - count, 0);
            return src.Substring(start, count);
        }

        /// <summary>
        /// Return the mid part of the string with specified start and legnth
        /// </summary>
        /// <param name="src">The source string</param>
        /// <param name="start">Starting index of the substring</param>
        /// <param name="count">Number of characters to return</param>
        /// <returns>A copy of the source string with the specifiec range of characters. The string might be empty</returns>
        public static string Mid(this string src, int start, int count)
        {
            start = Math.Min(src.Length, Math.Max(start, 0));
            count = Math.Min(count, Math.Max(src.Length - start, 0));
            return src.Substring(start, count);
        }

        /// <summary>
        /// Return a mid part of the string, starting from the specified index to the end of the string
        /// </summary>
        /// <param name="src">The source string</param>
        /// <param name="start">Starting index of the substring</param>
        /// <returns>A copy of the source string with the specifiec range of characters. The string might be empty</returns>
        public static string Mid(this string src, int start)
        {
            start = Math.Min(src.Length, Math.Max(start, 0));
            return src.Substring(start);
        }
    }
}
