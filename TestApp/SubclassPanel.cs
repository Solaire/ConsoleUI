using ConsoleUI;
using ConsoleUI.Structs;
using ConsoleUI.Event;
using System;

namespace TestApp
{
    class CSubclassPanel : CPanel
    {
        public CSubclassPanel(string title, PanelTypeCode type, int percentWidth, int percentHeight, CPage parent) : base(title, type, percentWidth, percentHeight, parent)
        {
            m_panelElements = new string[]
            {
                "Element 0",
                "Element 1",
                "Element 2",
                "Element 3",
                "Element 4",
                "Element 5",
                "Element 6",
                "Element 7",
                "Element 8",
                "Element 9",
            };
        }

        public override void Initialise()
        {
            throw new NotImplementedException();
        }

        public override void OnResize(object sender, ResizeEventArgs e)
        {
            throw new NotImplementedException();
        }

        public override void OnUpdateData(object sender, GenericEventArgs<string> e)
        {
            for(int i = 0; i < m_panelElements.Length; i++)
            {
                m_panelElements[i] = e.Data + " - Element " + i.ToString();
            }
            Redraw(true);
            FireDataChangedEvent(e.Data); // Will update the item info panel
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }

        protected override void DrawHighlighted(bool isFocused)
        {
            throw new NotImplementedException();
        }

        protected override bool LoadContent()
        {
            throw new NotImplementedException();
        }

        protected override void ReloadContent()
        {
            throw new NotImplementedException();
        }
    }
}
