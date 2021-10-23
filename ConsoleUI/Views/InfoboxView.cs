using System;
using System.Collections;
using ConsoleUI.Base;
using ConsoleUI.Helper;

namespace ConsoleUI.Views
{
    public interface IInfoboxDataSource
    {
        void Render(int startX, int startY, int lengthX, ConsoleColor colourFG, ConsoleColor colourBG);
    }

    public class CInfoboxItemEventArgs : EventArgs
    {
        public object Value { get; set; }

        public CInfoboxItemEventArgs(object value)
        {
            Value = value;
        }
    }

    public class CInfoboxView : CView
    {
        IInfoboxDataSource m_dataSource;

        public event Action<CInfoboxItemEventArgs> SelectedItemChanged;

        public CInfoboxView(ViewInitData initData, bool isFocused, CPage parent = null) : base(initData, isFocused, parent)
        {
            
        }

        public CInfoboxView(ViewInitData initData, IInfoboxDataSource source, bool isFocused, CPage parent = null) : base(initData, isFocused, parent)
        {
            m_dataSource = source;
        }

        public virtual void SetSource(IInfoboxDataSource newSource)
        {
            m_dataSource = newSource;
        }

        /// <summary>
        /// Draw the view
        /// </summary>
        /// <param name="redraw">If true, redraw the border and all data elements, otherwise only draw dirty components</param>
        public override void Draw(bool redraw)
        {
            int startX  = m_rect.x + 1;
            int startY  = m_rect.y + 1;
            int lengthX = m_rect.width - 2;

            if(redraw)
            {
                DrawBorder(true);
                m_dataSource?.Render(startX, startY, lengthX, ConsoleColor.Black, ConsoleColor.Blue);
            }
            else if(m_isDirty)
            {
                m_dataSource?.Render(startX, startY, lengthX, ConsoleColor.Black, ConsoleColor.Blue);
            }
        }

        public override void KeyPress(ConsoleKeyInfo keyInfo)
        {
            // Do nothing
        }
    }
}
