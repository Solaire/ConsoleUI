using System;
using ConsoleUI;
using ConsoleUI.Structs;

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

        public CAppPage(string title, ConsoleRect rect, int componentCount, CFrame parent) : base(title, rect, componentCount, parent)
        {

        }

        public override void Initialise(CControl.ControlTypeCode[] controlTypes)
        {
            for(int i = 0; i < controlTypes.Length && i < m_controls.Length; i++)
            {
                CControl.ControlData data = CConstants.CONTROL_DATA[controlTypes[i].Code];

                // C# switch cannot handle readonly fields
                if(controlTypes[i] == ControlType.CLASS_PANEL)
                {
                    m_controls[i] = new CClassPanel("Class", controlTypes[i], data.percentWidth, data.percentHeight, this);
                }
                else if(controlTypes[i] == ControlType.SUBCLASS_PANEL)
                {
                    m_controls[i] = new CSubclassPanel("Subclass", controlTypes[i], data.percentWidth, data.percentHeight, this);
                }
                else if(controlTypes[i] == ControlType.INFO_PANEL)
                {
                    m_controls[i] = new CItemInfoPanel("Information", controlTypes[i], data.percentWidth, data.percentHeight, this);
                }

                if(m_controls[i] != null)
                {
                    FocusChange += m_controls[i].OnSetFocus;
                }
            }

            m_controls[ControlType.CLASS_PANEL.Code].OnDataChangedString    += m_controls[ControlType.SUBCLASS_PANEL.Code].OnUpdateData;
            m_controls[ControlType.SUBCLASS_PANEL.Code].OnDataChangedString += m_controls[ControlType.INFO_PANEL.Code].OnUpdateData;

            FireFocusChangeEvent(ControlType.CLASS_PANEL);
            CalculateComponentPositions();
            //Draw(true);
        }

        public override void Initialise()
        {
            throw new System.NotImplementedException();
        }

        public override void KeyPress(ConsoleKeyInfo keyInfo)
        {
            if(keyInfo.Key == ConsoleKey.Tab)
            {
                m_focusedComponent = (m_focusedComponent == (int)PanelIndex.cClass) ? (int)PanelIndex.cSubclass : (int)PanelIndex.cClass;
                FireFocusChangeEvent(m_controls[m_focusedComponent].PanelType);
            }
            else
            {
                m_controls[m_focusedComponent].KeyPress(keyInfo);
            }
        }
    }
}
