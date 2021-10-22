using System;
using ConsoleUI.Type;

namespace ConsoleUI.Base
{
    /// <summary>
    /// Structure holding initialisation data for a struct
    /// </summary>
    public struct ControlInitData
    {
        public readonly byte        type;
        public readonly string      title;
        public readonly int         percentWidth;
        public readonly int         percentHeight;
        public readonly ConsoleRect rect;

        /// <summary>
        /// Constructor.
        /// Sets the percentage size values (for dynamic components)
        /// </summary>
        /// <param name="type">The control type code</param>
        /// <param name="title">The control title</param>
        /// <param name="percentWidth">Control width as percentage of parent area</param>
        /// <param name="percentHeight">Control height as percentage of parent area</param>
        public ControlInitData(byte type, string title, int percentWidth, int percentHeight)
        {
            this.type          = type;
            this.title         = title;
            this.percentWidth  = percentWidth;
            this.percentHeight = percentHeight;

            rect = new ConsoleRect(0, 0, 0, 0); // Not used
        }

        /// <summary>
        /// Constructor.
        /// Set the control's size and position values (for static components)
        /// </summary>
        /// <param name="type">The control type code</param>
        /// <param name="title">The control title</param>
        /// <param name="rect">Size and position of the control</param>
        public ControlInitData(byte type, string title, ConsoleRect rect)
        {
            this.type  = type;
            this.title = title;
            this.rect  = rect;

            // Not used
            percentWidth  = 0; 
            percentHeight = 0;
        }
    }
    
    /// <summary>
    /// Collection of control type values, represented as bytes
    /// This class is designed as an inheritable enum class, allowing for definint custom types
    /// </summary>
    public abstract class ControlTypeEnumClass
    {
        public const byte LIST      = 0;
        public const byte TEXT_EDIT = 1;
        public const byte INFOBOX   = 2;

        /// <summary>
        /// Class cannot be static because it cannot be inherited
        /// Disallow construction
        /// </summary>
        private ControlTypeEnumClass()
        {

        }
    }
}
