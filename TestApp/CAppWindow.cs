using ConsoleUI;
using ConsoleUI.Event;
using ConsoleUI.Structs;

namespace TestApp
{
    public class CAppWindow : CWindow
    {
        public CAppWindow(string title, ConsoleRect rect, ColourTheme colours, int pageCount) : base(title, rect, colours, pageCount)
        {
            
        }

        public override void Initialise()
        {
            m_pages[0] = new CAppPage(this, m_area, "App page", 3);
            m_pages[0].Initialise(new PanelTypeCode[] { PanelType.CLASS_PANEL, PanelType.SUBCLASS_PANEL, PanelType.INFO_PANEL });
        }

        public override void OnCommand(object sender, GenericEventArgs<string> e)
        {
            throw new System.NotImplementedException();
        }

        public override void OnResize(object sender, ResizeEventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}
