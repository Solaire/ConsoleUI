using System;
using System.Collections;
using ConsoleUI.Base;
using ConsoleUI.Helper;

namespace ConsoleUI.Views
{
    /// <summary>
    /// Data source interface for the list view
    /// </summary>
    public interface IListDataSource
    {
        int Count  { get; } // Number of elements in the list

        /// <summary>
        /// Render multiple elements
        /// </summary>
        /// <param name="startX">Starting x position</param>
        /// <param name="startY">Starting y position</param>
        /// <param name="lengthX">Maximum length of the string</param>
        /// <param name="first">Index of the first item to render</param>
        /// <param name="count">Number of elements to render</param>
        /// <param name="colourFG">foreground colour</param>
        /// <param name="colourBG">Background colour</param>
        void Render(int startX, int startY, int lengthX, int first, int count, ConsoleColor colourFG, ConsoleColor colourBG);

        /// <summary>
        /// Render single element
        /// </summary>
        /// <param name="startX">Starting x position</param>
        /// <param name="startY">Starting y position</param>
        /// <param name="lengthX">Maximum length of the string</param>
        /// <param name="index">Index of the element to render</param>
        /// <param name="colourFG">foreground colour</param>
        /// <param name="colourBG">Background colour</param>
        void Render(int startX, int startY, int lengthX, int index, ConsoleColor colourFG, ConsoleColor colourBG);

        /// <summary>
        /// Check if item is focused
        /// </summary>
        /// <param name="index">The index to check</param>
        /// <returns>True if specified item is focused</returns>
        bool IsFocused(int index);

        /// <summary>
        /// Set item as focused
        /// </summary>
        /// <param name="index">The index of the item to focus</param>
        void SetFocused(int index);

        /// <summary>
        /// Return source as IList
        /// </summary>
        /// <returns>Source as IList</returns>
        IList ToList();
    }

    /// <summary>
    /// Event argument for the list view
    /// </summary>
    public class CListViewItemEventArgs : EventArgs
    {
        public int Item     { get; }
        public object Value { get; }

        public CListViewItemEventArgs(int item, object value)
        {
            Item  = item;
            Value = value;
        }
    }

    /// <summary>
    /// Basic wrapper for the datasource interface
    /// </summary>
    public class CListWrapper : IListDataSource
    {
        private IList m_source;
        private int   m_focusIndex;

        public CListWrapper(IList source)
        {
            if(source != null)
            {
                m_focusIndex = -1;
                m_source = source;
            }
        }

        public int Count { get { return (m_source != null) ? m_source.Count : 0; } }

        public void Render(int startX, int startY, int lengthX, int first, int count, ConsoleColor colourFG, ConsoleColor colourBG)
        {
            if(m_source == null)
            {
                return;
            }

            for(int i = 0; i < count && first + i < Count; i++)
            {
                CConsoleDraw.WriteText(m_source[first + i].ToString().PadRight(lengthX), startX, startY++, colourFG, colourBG);
            }
        }

        public void Render(int startX, int startY, int lengthX, int index, ConsoleColor colourFG, ConsoleColor colourBG)
        {
            if(m_source == null || (index < 0 || index >= Count))
            {
                return;
            }

            CConsoleDraw.WriteText(m_source[index].ToString().PadRight(lengthX), startX, startY, colourFG, colourBG);
        }

        public bool IsFocused(int index)
        {
            if(m_source == null || (index < 0 || index >= Count))
            {
                return false;
            }
            return m_focusIndex == index;
        }

        public void SetFocused(int index)
        {
            if(m_source == null || (index < 0 || index >= Count))
            {
                m_focusIndex = -1;
            }
            m_focusIndex = index;
        }

        public IList ToList()
        {
            return m_source;
        }
    }

    /// <summary>
    /// Basic control type
    /// </summary>
    public class CListView : CView
    {
        protected int   m_focusedIndex;
        protected int   m_lastFocusedIndex;
        IListDataSource m_dataSource;

        public event Action<CListViewItemEventArgs> SelectedItemChanged;
        public event Action<CListViewItemEventArgs> OpenSelectedItem;

        /// <summary>
        /// Constructor.
        /// If both percentage values and rect is valid, prioritise the percentages
        /// </summary>
        /// <param name="initData">Initialisation data</param>
        /// <param name="isFocused">Flag indicating if control is focused</param>
        /// <param name="parent">Reference to the containing CPage instance, with null as default</param>
        public CListView(ViewInitData initData, bool isFocused, CPage parent = null) : base(initData, isFocused, parent)
        {
            m_focusedIndex     = -1;
            m_lastFocusedIndex = -1;
        }

        /// <summary>
        /// Constructor.
        /// Set datasource
        /// </summary>
        /// <param name="initData">Initialisation data</param>
        /// <param name="source">Data source</param>
        /// <param name="isFocused">Flag indicating if control is focused</param>
        /// <param name="parent">Reference to the containing CPage instance, with null as default</param>
        public CListView(ViewInitData initData, IListDataSource source, bool isFocused, CPage parent = null) : base(initData, isFocused, parent)
        {
            m_dataSource        = source;
            m_focusedIndex      = -1;
            m_lastFocusedIndex  = -1;
        }

        /// <summary>
        /// Set view's data source.
        /// </summary>
        /// <param name="newSource">New data source</param>
        public virtual void SetSource(IListDataSource newSource)
        {
            m_dataSource        = newSource;
            m_focusedIndex      = -1;
            m_lastFocusedIndex  = -1;
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
                m_dataSource.Render(startX, startY, lengthX, 0, m_dataSource.Count, ConsoleColor.Black, ConsoleColor.Blue); // Draw all list items

                if(m_focusedIndex >= 0)
                {
                    startY += m_focusedIndex;
                    m_dataSource.Render(startX, startY, lengthX, m_focusedIndex, ConsoleColor.Black, (m_isFocused) ? ConsoleColor.Yellow : ConsoleColor.Gray); // Draw selected/highlighted item
                }
            }
            else if(m_isDirty)
            {
                m_dataSource.Render(startX, startY + m_lastFocusedIndex, lengthX, m_lastFocusedIndex, 1, ConsoleColor.Black, ConsoleColor.Blue);
                m_dataSource.Render(startX, startY + m_focusedIndex, lengthX, m_focusedIndex, 1, ConsoleColor.Black, (m_isFocused) ? ConsoleColor.Yellow : ConsoleColor.Gray);
            }
        }

        /// <summary>
        /// Handle key press
        /// </summary>
        /// <param name="keyInfo">The key info</param>
        public override void KeyPress(ConsoleKeyInfo keyInfo)
        {
            if(!m_isFocused)
            {
                return;
            }

            switch(keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                {
                    m_focusedIndex = Math.Max(m_focusedIndex - 1, 0);
                    OnSelectedItemChanged();
                }
                break;

                case ConsoleKey.DownArrow:
                {
                    m_focusedIndex = Math.Min(m_focusedIndex + 1, m_dataSource.Count - 1);
                    OnSelectedItemChanged();
                }
                break;

                case ConsoleKey.Enter:
                {
                    OnOpenSelectedItem();
                }
                break;

                default:
                    break;
            }
        }

        /// <summary>
        /// Raise a SelectedItemChanged event
        /// </summary>
        /// <returns>Bool if event was raised</returns>
        public virtual bool OnSelectedItemChanged()
        {
            if(m_focusedIndex != m_lastFocusedIndex)
            {
                m_isDirty = true;
                var value = (m_dataSource.Count > 0) ? m_dataSource.ToList()[m_focusedIndex] : null;
                SelectedItemChanged?.Invoke(new CListViewItemEventArgs(m_focusedIndex, value));
                DrawSelectionChange();
            }
            return false;
        }

        /// <summary>
        /// Raise a OpenSelectedItem event
        /// </summary>
        /// <returns>Bool if event was raised</returns>
        public virtual bool OnOpenSelectedItem()
        {
            if(m_focusedIndex < 0 || m_focusedIndex > m_dataSource.Count)
            {
                return false;
            }

            var value = m_dataSource.ToList()[m_focusedIndex];
            OpenSelectedItem?.Invoke(new CListViewItemEventArgs(m_focusedIndex, value));
            return true;
        }

        private void DrawSelectionChange()
        {
            int startX  = m_rect.x + 1;
            int startY  = m_rect.y + 1;
            int lengthX = m_rect.width - 2;

            if(m_isDirty || m_focusedIndex != m_lastFocusedIndex)
            {
                if(m_lastFocusedIndex >= 0)
                {
                    m_dataSource.Render(startX, startY + m_lastFocusedIndex, lengthX, m_lastFocusedIndex, 1, ConsoleColor.Black, ConsoleColor.Blue);
                }
                if(m_focusedIndex >= 0)
                {
                    m_dataSource.Render(startX, startY + m_focusedIndex, lengthX, m_focusedIndex, 1, ConsoleColor.Black, (m_isFocused) ? ConsoleColor.Yellow : ConsoleColor.Gray);
                }

                m_lastFocusedIndex = m_focusedIndex;
                m_isDirty = false;
            }
        }
    }
}
