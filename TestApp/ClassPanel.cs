/*
using System;
using ConsoleUI;
using ConsoleUI.Event;
using ConsoleUI.Structs;

namespace TestApp
{
    public class CClassPanel : CControl
    {
        private string[] m_elements;
        private int      m_focusedElement;

        public CClassPanel(string title, ControlTypeCode type, int percentWidth, int percentHeight, CPage parent) : base(title, type, percentWidth, percentHeight, parent)
        {
            m_focusedElement = 0;
            m_elements = new string[]
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
        }

        public CClassPanel(string title, ControlTypeCode type, ConsoleRect rect, CPage parent) : base(title, type, rect, parent)
        {
            m_focusedElement = 0;
            m_elements = new string[]
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
        }

        public override void Initialise()
        {
            throw new NotImplementedException();
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }

        public override void Draw(bool redraw)
        {
            //if(redraw)
            {
                int startX = m_rect.x + 1;
                int startY = m_rect.y + 1;
                int lengthX = m_rect.width - 2;

                DrawBorder(true);
                // Panel content
                for(int row = startY, i = 0; row < m_rect.Bottom && i < m_elements.Length; row++, i++)
                {
                    ConsoleColor background;
                    ConsoleColor foreground;

                    if(m_focusedElement == i && m_isFocused)
                    {
                        foreground = ConsoleColor.White;
                        background = ConsoleColor.Red;
                    }
                    else if(m_focusedElement == i)
                    {
                        foreground = ConsoleColor.White; 
                        background = ConsoleColor.Green;
                    }
                    else
                    {
                        foreground = ConsoleColor.White;
                        background = ConsoleColor.Blue;
                    }

                    // TODO: Fix constants
                    CConsoleDraw.WriteText(m_elements[i].PadRight(lengthX), startX, row, foreground, background);
                }
            }
        }

        public override void OnUpdateData(object sender, GenericEventArgs<string> e)
        {
            throw new NotImplementedException();
        }

        public override void KeyPress(ConsoleKeyInfo keyInfo)
        {
            switch(keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                {
                    m_focusedElement = Math.Max(m_focusedElement - 1, 0);
                    FireDataChangedEvent(m_elements[m_focusedElement]);
                }
                break;

                case ConsoleKey.DownArrow:
                {
                    m_focusedElement = Math.Min(m_focusedElement + 1, m_elements.Length - 1);
                    FireDataChangedEvent(m_elements[m_focusedElement]);
                }
                break;

                default:
                    break;
            }
        }
    }
}
*/