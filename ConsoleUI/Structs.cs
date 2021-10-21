namespace ConsoleUI.Structs
{
    /// <summary>
    /// A rectangle structure used for drawing and creating areas
    /// </summary>
    public struct ConsoleRect
    {
        public int x;
        public int y;
        public int width;
        public int height;

        public int Right
        {
            get
            {
                return x + width;
            }
        }

        public int Bottom
        {
            get
            {
                return y + height;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public ConsoleRect(int x, int y, int w, int h)
        {
            this.x = x;
            this.y = y;
            width = w;
            height = h;
        }
    }

    /// <summary>
    /// Represents an x/y coordinate
    /// </summary>
    public struct ConsolePoint
    {
        public int x;
        public int y;

        /// <summary>
        /// Constructor
        /// </summary>
        public ConsolePoint(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public bool IsWithinArea(ConsoleRect area)
        {
            bool isValidWidth  = (x >= area.x && x < area.Right);
            bool isValidHeight = (y >= area.y && y < area.Bottom);
            return (isValidWidth && isValidHeight);
        }
    }

    /// <summary>
    /// Represents a colour theme, containing an array of colours accessed via a type/index
    /// </summary>
    public struct ColourTheme
    {
        public System.ConsoleColor[] m_colours;

        public System.ConsoleColor this[ColourThemeIndex i]
        {
            get
            {
                if((int)i >= m_colours.Length)
                {
                    return (((int)i & 1) == 0) ? m_colours[(int)ColourThemeIndex.cDefaultBG] : m_colours[(int)ColourThemeIndex.cDefaultFG];
                }
                return m_colours[(int)i];
            }
            set
            {
                if((int)i < m_colours.Length)
                {
                    m_colours[(int)i] = value;
                }
            }
        }

        public int Length
        {
            get { return m_colours.Length; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public ColourTheme(int colourCount)
        {
            if(colourCount < 2)
            {
                throw new System.ArgumentException("Colour theme must have at least two (2) colours - default background and foreground");
            }
            m_colours = new System.ConsoleColor[colourCount];
        }

        public ColourTheme(System.ConsoleColor[] colours)
        {
            if(colours.Length < 2)
            {
                throw new System.ArgumentException("Colour theme must have at least two (2) colours - default background and foreground");
            }
            m_colours = colours;
        }
    }

    /// <summary>
    /// Definitions of the purposes of each colour in the schema
    /// The colours should alternate - background are even and foreground are odd
    /// </summary>
    public enum ColourThemeIndex
    {
        // Default colour pair.
        cDefaultBG = 0,
        cDefaultFG = 1,

        // Colour pair for the status bar
        // At the moment the minibuffer will use the default
        cStatusBG = 2,
        cStatusFG = 3,

        // Colours for the right/bottom borders as well as the headers on top
        cPanelBorderBG = 4,
        cPanelBorderFG = 5,

        // Main colours used to draw the panel content
        cPanelMainBG = 6,
        cPanelMainFG = 7,

        // Panel colours for highlighted items when the panel is not in focus
        cPanelSelectBG = 8,
        cPanelSelectFG = 9,

        // Panel colours for highlighted items when the panel is in focus
        cPanelSelectFocusBG = 10,
        cPanelSelectFocusFG = 11,
    }
}
