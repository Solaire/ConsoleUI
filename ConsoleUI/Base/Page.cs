using System;
using ConsoleUI.Type;

namespace ConsoleUI.Base
{
    /// <summary>
    /// Container class for multiple UI components
    /// </summary>
    public abstract class CPage : CComponent
    {
        protected readonly CFrame m_parent;

        protected CView[] m_views;
        protected int     m_focusedComponent;

        public string Title { get { return m_title; } }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="title">The page title</param>
        /// <param name="rect">The page size</param>
        /// <param name="componentCount">Number of components</param>
        /// <param name="parent">Reference to a parent frame</param>
        public CPage(string title, ConsoleRect rect, int componentCount, CFrame parent) : base(title, rect)
        {
            m_focusedComponent = 0;
            m_views  = new CView[componentCount];
            m_parent = parent;
        }

        public virtual void Initialise()
        {
            CalculateComponentPositions();
        }

        /// <summary>
        /// Calculate and set the size/positioning of each component
        /// The size values of each component is the percentage of the page they take
        /// Each component is layed out right -> left, top -> bottom (comic book style),
        /// starting from the first element.
        /// </summary>
        protected void CalculateComponentPositions()
        {
            ConsolePoint nextPoint = new ConsolePoint(m_rect.x, m_rect.y);

            for(int i = 0; i < m_views.Length; i++)
            {
                int nextWidth  = Math.Min((int)(((float)m_rect.width  / 100f) * m_views[i].PercentWidth),  m_rect.width  - nextPoint.x);
                int nextHeight = Math.Min((int)(((float)m_rect.height / 100f) * m_views[i].PercentHeight), m_rect.height - nextPoint.y);

                m_views[i].SetPosition(nextPoint.x, nextPoint.y);
                m_views[i].SetSize(nextWidth, nextHeight);

                // Take the next top-right and bottom-left corners of the panel to made use for the next one
                // Prioritise top-right point
                ConsolePoint topRight   = new ConsolePoint(nextPoint.x + nextWidth, nextPoint.y);
                ConsolePoint bottomLeft = new ConsolePoint(nextPoint.x, nextPoint.y + nextHeight + 1);

                if(i == m_views.Length - 1)
                {
                    break; // Last panel, don't bother with borders
                }
                else if(topRight.IsWithinArea(m_rect)) // Next panel to the right
                {
                    nextPoint = topRight;
                }
                else if(bottomLeft.IsWithinArea(m_rect)) // Next panel below
                {
                    nextPoint = bottomLeft;
                }
                else
                {
                    break; // Point not valid
                }
            }
        }

        /// <summary>
        /// Redraw the page
        /// </summary>
        public override void Draw(bool redraw)
        {
            for(int i = 0; i < m_views.Length; i++)
            {
                if(m_views[i] != null)
                {
                    m_views[i].Draw(redraw);
                }
            }
        }
    }
}
