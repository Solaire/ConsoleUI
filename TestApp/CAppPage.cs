using ConsoleUI;
using ConsoleUI.Event;
using ConsoleUI.Structs;
using System;

namespace TestApp
{
    public class CAppPage : CPage
    {
        private enum PanelIndex
        {
            cClass    = 0,
            cSubclass = 1,
            cItemInfo = 2,
        }

        public CAppPage(CWindow parent, ConsoleRect area, string title, int panelCount) : base(parent, area, title, panelCount)
        {

        }

        public override void Initialise(PanelTypeCode[] panelTypes)
        {
            for(int i = 0; i < panelTypes.Length && i < m_panels.Length; i++)
            {
                PanelData data = CConstants.PANEL_DATA[panelTypes[i].Code];

                // C# switch cannot handle readonly fields
                if(panelTypes[i] == PanelType.CLASS_PANEL)
                {
                    m_panels[i] = new CClassPanel("Class", panelTypes[i], data.percentWidth, data.percentHeight, this);
                }
                else if(panelTypes[i] == PanelType.SUBCLASS_PANEL)
                {
                    m_panels[i] = new CSubclassPanel("Subclass", panelTypes[i], data.percentWidth, data.percentHeight, this);
                }
                else if(panelTypes[i] == PanelType.INFO_PANEL)
                {
                    m_panels[i] = new CItemInfoPanel("Information", panelTypes[i], data.percentWidth, data.percentHeight, this);
                }

                if(m_panels[i] != null)
                {
                    FocusChange += m_panels[i].OnSetFocus;
                }
            }

            m_panels[PanelType.CLASS_PANEL.Code].OnDataChangedString    += m_panels[PanelType.SUBCLASS_PANEL.Code].OnUpdateData;
            m_panels[PanelType.SUBCLASS_PANEL.Code].OnDataChangedString += m_panels[PanelType.INFO_PANEL.Code].OnUpdateData;

            FireFocusChangeEvent(PanelType.CLASS_PANEL);
            CalculatePanelLayout();
            Redraw(true);
        }

        public override void Initialise()
        {
            throw new System.NotImplementedException();
        }

        public override void KeyPressed(ConsoleKeyInfo keyInfo)
        {
            if(keyInfo.Key == ConsoleKey.Tab)
            {
                m_focusedPanelIndex = (m_focusedPanelIndex == (int)PanelIndex.cClass) ? (int)PanelIndex.cSubclass : (int)PanelIndex.cClass;
                FireFocusChangeEvent(m_panels[m_focusedPanelIndex].PanelType);
            }
            else
            {
                m_panels[m_focusedPanelIndex].KeyPressed(keyInfo);
            }
        }

        public override void OnResize(object sender, ResizeEventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}
