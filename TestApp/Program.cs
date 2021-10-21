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

            CTestFrame frame = new CTestFrame("ConsoleUI TestAPP", rect, PAGE_COUNT);
            frame.Initialise();
            frame.WindowMain();
        }
    }
}
