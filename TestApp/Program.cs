using ConsoleUI;
using ConsoleUI.Structs;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleRect rect = new ConsoleRect(0, 0, CConstants.DEFAULT_WINDOW_WIDTH, CConstants.DEFAULT_WINDOW_HEIGHT);
            const int PAGE_COUNT = 1;

            CAppWindow window = new CAppWindow("ConsoleUI TestAPP", rect, CConstants.DEFAULT_THEME, PAGE_COUNT);
            window.Initialise();
            window.WindowMain();
        }
    }
}
