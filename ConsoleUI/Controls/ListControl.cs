using ConsoleUI.Base;
using ConsoleUI.Type;
using System;

namespace ConsoleUI.Controls
{
    /// <summary>
    /// Basic control type
    /// </summary>
    public class CListControl : CControl
    {
        protected CListControlData m_data;
        protected int m_focusedElement;
        protected int m_lastFocusedElement;

        /// <summary>
        /// Constructor.
        /// If both percentage values and rect is valid, prioritise the percentages
        /// </summary>
        /// <param name="initData">Initialisation data</param>
        /// <param name="data">List control data</param>
        /// <param name="isFocused">Flag indicating if control is focused</param>
        /// <param name="parent">Reference to the containing CPage instance, with null as default</param>
        public CListControl(ControlInitData initData, CListControlData data, bool isFocused, CPage parent = null) : base(initData, isFocused, parent)
        {
            m_data               = data;
            m_focusedElement     = -1;
            m_lastFocusedElement = -1;
        }

        public override void SetData<T>(T[] data)
        {
            if(m_data != null)
            {
                m_data.SetData(data);
            }
        }
        
        protected override void LoadData<U>(U updateInfo)
        {
            if(m_data != null)
            {
                m_data.LoadList(updateInfo);
            }
        }

        public override void Draw(bool redraw)
        {
            int startX  = m_rect.x + 1;
            int startY  = m_rect.y + 1;
            int lengthX = m_rect.width - 2;

            if(redraw)
            {
                DrawBorder(true);

                // Panel content
                for(int row = startY, i = 0; row < m_rect.Bottom && i < m_data.Length; row++, i++)
                {
                    ConsoleColor background;
                    ConsoleColor foreground;

                    if(m_focusedElement == i && m_isFocused)
                    {
                        foreground = ConsoleColor.Black;
                        background = ConsoleColor.White;
                    }
                    else if(m_focusedElement == i)
                    {
                        foreground = ConsoleColor.Black;
                        background = ConsoleColor.Gray;
                    }
                    else
                    {
                        foreground = ConsoleColor.Black;
                        background = ConsoleColor.Blue;
                    }

                    m_data.DrawElement(startX, row, lengthX, i, foreground, background);
                    //CConsoleDraw.WriteText(m_elements[i].PadRight(lengthX), startX, row, foreground, background);
                }
            }
            else if(m_isDirty)
            {
                //CConsoleDraw.WriteText(m_elements[m_focusedElement].PadRight(lengthX)    , startX, startY + m_focusedElement    , ConsoleColor.Black, (m_isFocused) ? ConsoleColor.White : ConsoleColor.Gray); // Current selection
                //CConsoleDraw.WriteText(m_elements[m_lastFocusedElement].PadRight(lengthX), startX, startY + m_lastFocusedElement, ConsoleColor.Black, ConsoleColor.Blue); // Last selection

                m_data.DrawElement(startX, startY + m_focusedElement    , lengthX, m_focusedElement    , ConsoleColor.Black, (m_isFocused) ? ConsoleColor.White : ConsoleColor.Gray); // Current selection
                m_data.DrawElement(startX, startY + m_lastFocusedElement, lengthX, m_lastFocusedElement, ConsoleColor.Black, ConsoleColor.Blue); // Last selection
            }
        }

        public override void KeyPress(ConsoleKeyInfo keyInfo)
        {
            if(!m_isFocused)
            {
                return;
            }

            m_lastFocusedElement = m_focusedElement;

            switch(keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                {
                    m_focusedElement = Math.Max(m_focusedElement - 1, 0);
                    //RaiseDataChangedEvent(m_elements[m_focusedElement]);
                }
                break;

                case ConsoleKey.DownArrow:
                {
                    m_focusedElement = Math.Min(m_focusedElement + 1, m_data.Length - 1);
                    //RaiseDataChangedEvent(m_elements[m_focusedElement]);
                }
                break;

                default:
                    break;
            }
            m_isDirty = (m_focusedElement != m_lastFocusedElement);
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }
    }
}
