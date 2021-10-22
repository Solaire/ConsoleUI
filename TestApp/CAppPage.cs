using System;
using ConsoleUI.Base;
using ConsoleUI.Type;
using ConsoleUI.Controls;

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

        public override void Initialise(byte[] controlTypes)
        {
            /*
            for(int i = 0; i < controlTypes.Length && i < m_controls.Length; i++)
            {
                ControlInitData data = CConstants.CONTROL_DATA[controlTypes[i]];

                // C# switch cannot handle readonly fields
                if(controlTypes[i] == ControlTypeEnumClass.CLASS_PANEL)
                {
                    //m_controls[i] = new CClassPanel("Class", controlTypes[i], data.percentWidth, data.percentHeight, this);
                    m_controls[i] = new CListControl<string, string>("Class", data.percentWidth, data.percentHeight, ControlTypeEnumClass.LIST, this);
                }
                else if(controlTypes[i] == ControlType.SUBCLASS_PANEL)
                {
                    //m_controls[i] = new CSubclassPanel("Subclass", controlTypes[i], data.percentWidth, data.percentHeight, this);
                    m_controls[i] = new CListControl<string, string>("Subclass", data.percentWidth, data.percentHeight, ControlTypeEnumClass.LIST, this);
                }
                else if(controlTypes[i] == ControlType.INFO_PANEL)
                {
                    //m_controls[i] = new CItemInfoPanel("Information", controlTypes[i], data.percentWidth, data.percentHeight, this);
                }

                if(m_controls[i] != null)
                {
                    FocusChange += m_controls[i].OnSetFocus;
                }
            }
            */

            string[] first  = new string[]
            {
                "Class 0",
                "Class 1",
                "Class 2",
                "Class 3",
                "Class 4",
                "Class 5",
                "Class 6",
                "Class 7",
                "Class 8",
                "Class 9",
            };

            string[] second  = new string[]
            {
                "Subclass 0",
                "Subclass 1",
                "Subclass 2",
                "Subclass 3",
                "Subclass 4",
                "Subclass 5",
                "Subclass 6",
                "Subclass 7",
                "Subclass 8",
                "Subclass 9",
            };

            CListControlData_String dataClass = new CListControlData_String(first);
            CListControlData_String dataSubclass = new CListControlData_String(second);

            m_controls[0] = new CListControl(CConstants.CONTROL_DATA[controlTypes[0]], dataClass,    true , this);
            m_controls[1] = new CListControl(CConstants.CONTROL_DATA[controlTypes[1]], dataSubclass, false, this);

            //m_controls[ControlType.CLASS_PANEL.Code].OnDataChangedString    += m_controls[ControlType.SUBCLASS_PANEL.Code].OnUpdateData;
            //m_controls[ControlType.SUBCLASS_PANEL.Code].OnDataChangedString += m_controls[ControlType.INFO_PANEL.Code].OnUpdateData;

            CalculateComponentPositions();
            //Draw(true);
        }

        public override void KeyPress(ConsoleKeyInfo keyInfo)
        {
            if(keyInfo.Key == ConsoleKey.Tab)
            {
                m_controls[m_focusedComponent].Focused = false;
                m_focusedComponent = (m_focusedComponent == (int)PanelIndex.cClass) ? (int)PanelIndex.cSubclass : (int)PanelIndex.cClass;
                m_controls[m_focusedComponent].Focused = true;

            }
            else
            {
                m_controls[m_focusedComponent].KeyPress(keyInfo);
            }
        }
    }
}
