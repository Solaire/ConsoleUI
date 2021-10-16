using ConsoleUI.Structs;
using ConsoleUI.Event;
using System;

namespace ConsoleUI
{
    public abstract class CPage : CElement, IInteractive
    {
        protected CWindow  m_parent;
        protected CPanel[] m_panels;
        protected int      m_focusedPanelIndex;

        protected delegate void FocusChangeEventHandler(object sender, GenericEventArgs<PanelTypeCode> e);
        protected event FocusChangeEventHandler FocusChange;

        public CPage(CWindow parent, ConsoleRect area, string title, int panelCount) : base(area, title)
        {
            m_parent = parent;
            m_panels = new CPanel[panelCount];
            m_focusedPanelIndex = 0;
        }

        protected virtual void FireFocusChangeEvent(PanelTypeCode focusedPanel)
        {
            if(FocusChange != null)
            {
                FocusChange.Invoke(this, new GenericEventArgs<PanelTypeCode>(focusedPanel));
            }
        }

        public abstract void Initialise(PanelTypeCode[] panelTypes);

        public virtual void Update()
        {
            foreach(CPanel panel in m_panels)
            {
                if(panel != null)
                {
                    panel.Update();
                }
            }
        }

        public void CalculatePanelLayout()
        {
            ConsolePoint nextPoint = new ConsolePoint(0, 0);

            for(int i = 0; i < m_panels.Length; i++)
            {
                int nextWidth  = Math.Min((int)(((float)m_area.width  / 100f) * m_panels[i].PercentWidth),  m_area.width  - nextPoint.x);
                int nextHeight = Math.Min((int)(((float)m_area.height / 100f) * m_panels[i].PercentHeight), m_area.height - nextPoint.y);

                m_panels[i].SetPosition(nextPoint.x, nextPoint.y);
                m_panels[i].SetSize(nextWidth, nextHeight);

                // Take the next top-right and bottom-left corners of the panel to made use for the next one
                // Prioritise top-right point
                ConsolePoint topRight   = new ConsolePoint(nextPoint.x + nextWidth, nextPoint.y);
                ConsolePoint bottomLeft = new ConsolePoint(nextPoint.x, nextPoint.y + nextHeight);

                m_panels[i].RightBorder  = topRight.IsWithinArea(m_area);
                m_panels[i].BottomBorder = bottomLeft.IsWithinArea(m_area);

                if(i == m_panels.Length - 1)
                {
                    m_panels[i].RightBorder  = false;
                    m_panels[i].BottomBorder = false;
                    break; // Last panel, don't bother with borders
                }
                else if(topRight.IsWithinArea(m_area)) // Next panel to the right
                {
                    nextPoint = topRight;
                }
                else if(bottomLeft.IsWithinArea(m_area)) // Next panel below
                {
                    nextPoint = bottomLeft;
                }
                else
                {
                    break; // Point not valid
                }
            }
        }

        public ConsoleColor GetColour(ColourThemeIndex i)
        {
            if(m_parent == null)
            {
                return (((int)i & 1) == 0) ? ConsoleColor.Black : ConsoleColor.White;
            }
            return m_parent.m_colours[i];
        }

        /// <summary>
        /// Redraw the page
        /// </summary>
        public void Redraw(bool fullRedraw)
        {
            // TODO: draw title
            for(int i = 0; i < m_panels.Length; i++)
            {
                if(m_panels[i] != null)
                {
                    m_panels[i].Redraw(fullRedraw);
                }
            }
        }

        public abstract void KeyPressed(ConsoleKey keyEvent);
    }
}
