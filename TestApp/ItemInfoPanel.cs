using System;
using ConsoleUI;
using ConsoleUI.Event;
using ConsoleUI.Structs;

namespace TestApp
{
    public class CItemInfoPanel : CPanel
    {
        internal struct ItemInfo
        {
            public string itemName;
            public string itemClass;
            public int    itemCounter;
            public bool   isActive;

            public ItemInfo(string itName, string itClass, int itCounter, bool isActive)
            {
                itemName        = itName;
                itemClass       = itClass;
                itemCounter     = itCounter;
                this.isActive   = isActive;
            }

            public void SetInfo(string itName, string itClass, int itCounter, bool isActive)
            {
                itemName = itName;
                itemClass = itClass;
                itemCounter = itCounter;
                this.isActive = isActive;
            }
        }

        private ItemInfo m_currentItem;

        public CItemInfoPanel(string title, PanelTypeCode type, int percentWidth, int percentHeight, CPage parent) : base(title, type, percentWidth, percentHeight, parent)
        {
            m_panelElements = new string[0];
            m_currentItem = new ItemInfo("", "", 0, false);
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
            m_currentItem.itemName = "item name";
            m_currentItem.itemClass = e.Data;
            m_currentItem.itemCounter++;
            m_currentItem.isActive = (m_currentItem.itemCounter % 2 == 0);
            Redraw(true);
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

        public override void Redraw(bool fullRedraw)
        {
            int startX = m_area.x;
            int startY = m_area.y;
            int lengthX = (LeftBorder) ? m_area.width - 1 : m_area.width;

            // Background and borders
            //CConsoleEx.DrawColourRect(m_area, m_parent.GetColour(ColourThemeIndex.cPanelMainBG));
            CConsoleEx.DrawColourRect(m_area, ConsoleColor.Red);
            if(TopBorder)
            {
                CConsoleEx.DrawHorizontalLine(startX, startY, lengthX, m_parent.GetColour(ColourThemeIndex.cPanelBorderBG), m_parent.GetColour(ColourThemeIndex.cPanelBorderFG));
                startY++;
            }
            if(LeftBorder)
            {
                CConsoleEx.DrawVerticalLine(startX, startY, m_area.height, m_parent.GetColour(ColourThemeIndex.cPanelBorderBG), m_parent.GetColour(ColourThemeIndex.cPanelBorderFG));
                startX++;
            }

            // Panel header
            CConsoleEx.WriteText(m_title, startX, startY, 1 /*CONSTANT*/, lengthX, m_parent.GetColour(ColourThemeIndex.cPanelBorderBG), m_parent.GetColour(ColourThemeIndex.cPanelBorderFG));
            startY++;

            // Content
            CConsoleEx.WriteText(m_currentItem.itemClass,                          startX, startY++, 1 /*CONSTANT*/, lengthX, m_parent.GetColour(ColourThemeIndex.cPanelMainBG), m_parent.GetColour(ColourThemeIndex.cPanelMainFG));
            CConsoleEx.WriteText(m_currentItem.itemName,                           startX, startY++, 1 /*CONSTANT*/, lengthX, m_parent.GetColour(ColourThemeIndex.cPanelMainBG), m_parent.GetColour(ColourThemeIndex.cPanelMainFG));
            CConsoleEx.WriteText(m_currentItem.itemCounter.ToString(),             startX, startY++, 1 /*CONSTANT*/, lengthX, m_parent.GetColour(ColourThemeIndex.cPanelMainBG), m_parent.GetColour(ColourThemeIndex.cPanelMainFG));
            CConsoleEx.WriteText((m_currentItem.isActive) ? "active" : "inactive", startX, startY++, 1 /*CONSTANT*/, lengthX, m_parent.GetColour(ColourThemeIndex.cPanelMainBG), m_parent.GetColour(ColourThemeIndex.cPanelMainFG));
        }
    }
}
