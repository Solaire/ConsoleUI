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

        protected CControl[] m_controls;
        protected int        m_focusedComponent;

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
            m_controls = new CControl[componentCount];
            m_parent = parent;
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

            for(int i = 0; i < m_controls.Length; i++)
            {
                int nextWidth  = Math.Min((int)(((float)m_rect.width  / 100f) * m_controls[i].PercentWidth),  m_rect.width  - nextPoint.x);
                int nextHeight = Math.Min((int)(((float)m_rect.height / 100f) * m_controls[i].PercentHeight), m_rect.height - nextPoint.y);

                m_controls[i].SetPosition(nextPoint.x, nextPoint.y);
                m_controls[i].SetSize(nextWidth, nextHeight);

                // Take the next top-right and bottom-left corners of the panel to made use for the next one
                // Prioritise top-right point
                ConsolePoint topRight   = new ConsolePoint(nextPoint.x + nextWidth, nextPoint.y);
                ConsolePoint bottomLeft = new ConsolePoint(nextPoint.x, nextPoint.y + nextHeight + 1);

                if(i == m_controls.Length - 1)
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
        /// Update all valid components
        /// </summary>
        public override void Update()
        {
            foreach(CControl control in m_controls)
            {
                if(control != null)
                {
                    control.Update();
                }
            }
        }

        /// <summary>
        /// Redraw the page
        /// </summary>
        public override void Draw(bool redraw)
        {
            for(int i = 0; i < m_controls.Length; i++)
            {
                if(m_controls[i] != null)
                {
                    m_controls[i].Draw(redraw);
                }
            }
        }

        /// <summary>
        /// Initialise the page and all components
        /// </summary>
        /// <param name="panelTypes"></param>
        public abstract void Initialise(byte[] componentTypes);
    }
}
