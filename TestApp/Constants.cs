using ConsoleUI;
using ConsoleUI.Structs;

namespace TestApp
{
    public static class CConstants
    {
        public static int DEFAULT_WINDOW_WIDTH  = 100;
        public static int DEFAULT_WINDOW_HEIGHT = 48;

        public static int TEXT_PADDING_LEFT  = 1;
        public static int TEXT_PADDING_RIGHT = 1;

        // Initialise the panel array and each individual panel
        public static readonly CControl.ControlData[] CONTROL_DATA =
        {
            new CControl.ControlData(ControlType.CLASS_PANEL,    "Class",     50, 100),
            new CControl.ControlData(ControlType.SUBCLASS_PANEL, "Subclass",  50, 70),
            new CControl.ControlData(ControlType.INFO_PANEL,     "Item info", 50, 30),
        };

        private static readonly System.ConsoleColor[] DEFAULT_COLOUR_ARRAY =
        {
            // Default background/foreground
            System.ConsoleColor.Black,
            System.ConsoleColor.White,
            // Status background/foreground
            System.ConsoleColor.White,
            System.ConsoleColor.Black,
            // Panel border background/foreground
            System.ConsoleColor.DarkGreen,
            System.ConsoleColor.White,
            // Panel mian background/foreground
            System.ConsoleColor.Black,
            System.ConsoleColor.White,
            // Panel highlight background/foreground
            System.ConsoleColor.DarkGray,
            System.ConsoleColor.Red,
            // Focused panel highlight background/foreground
            System.ConsoleColor.Blue,
            System.ConsoleColor.Red,
        };

        public static readonly ColourTheme DEFAULT_THEME = new ColourTheme(DEFAULT_COLOUR_ARRAY);
    }
}
