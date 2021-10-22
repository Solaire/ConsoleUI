using System;
using ConsoleUI.Helper;
using ConsoleUI.Type;

namespace ConsoleUI.Base
{
    /// <summary>
    /// Base class describing UI control elements such as list, message box, etc
    /// </summary>
    public abstract class CControl : CComponent
    {
        protected readonly byte   m_type;
        protected readonly CPage  m_parent;
        protected readonly int    m_percentWidth;
        protected readonly int    m_percentHeight;

        protected bool m_isFocused;
        protected bool m_isDirty;

        // Getters
        public int  PercentWidth    { get { return m_percentWidth; } }
        public int  PercentHeight   { get { return m_percentHeight; } }
        public byte PanelType       { get { return m_type; } }
        public bool Focused         { get { return m_isFocused; } set { m_isFocused = value; } }

        // Event handler in case a different panel causes change of data
        public delegate void OnDataChangedEventHandler(object sender, GenericEventArgs<string> e);
        public event OnDataChangedEventHandler OnDataChangedString;

        /// <summary>
        /// Constructor.
        /// If both percentage values and rect is valid, prioritise the percentages
        /// </summary>
        /// <param name="initData">Initialisation data</param>
        /// <param name="isFocused">Flag indicating if control is focused</param>
        /// <param name="parent">Reference to the containing CPage instance, with null as default</param>
        public CControl(ControlInitData initData, bool isFocused, CPage parent = null) : base(initData.title, new ConsoleRect(0, 0, 0, 0))
        {
            m_type = initData.type;
            if(initData.percentWidth > 0 && initData.percentHeight > 0)
            {
                m_percentWidth  = initData.percentWidth;
                m_percentHeight = initData.percentHeight;
            }
            else if(m_rect.height > 0 && m_rect.width > 0)
            {
                m_percentWidth  = 0;
                m_percentHeight = 0;
                m_rect = initData.rect;
            }
            else
            {
                throw new ArgumentNullException("CControl.CControl() - Percentage or rect value must be set!");
            }

            m_parent = parent;

            m_isFocused = isFocused;
            m_isDirty   = false;
        }

        /// <summary>
        /// Set control's postion
        /// </summary>
        /// <param name="x">The x coordinate</param>
        /// <param name="y">The y coordinate</param>
        public void SetPosition(int x, int y)
        {
            m_rect.x = x;
            m_rect.y = y;
        }

        /// <summary>
        /// Set control's size
        /// </summary>
        /// <param name="width">Width in characters</param>
        /// <param name="height">Height in characters</param>
        public void SetSize(int width, int height)
        {
            m_rect.width = width;
            m_rect.height = height;
        }

        /// <summary>
        /// Set control's size and position
        /// </summary>
        /// <param name="rect">The console rectangle</param>
        public void SetRect(ConsoleRect rect)
        {
            m_rect = rect;
        }

        /// <summary>
        /// Fire DataChanged event to all subscribed instances
        /// TODO
        /// </summary>
        /// <param name="eventData">The event data as string</param>
        protected void RaiseDataChangedEvent(string eventData)
        {
            OnDataChangedString.Invoke(this, new GenericEventArgs<string>(eventData));
        }

        /// <summary>
        /// Draw border
        /// </summary>
        protected virtual void DrawBorder(bool drawTitle)
        {
            CConsoleDraw.DrawBox(m_rect, ConsoleColor.White, ConsoleColor.Blue, (drawTitle) ? string.Format(" {0} ", m_title) : "");
        }

        public abstract void SetData<T>(T[] data);
        protected abstract void LoadData<U>(U updateInfo);
    }
}
