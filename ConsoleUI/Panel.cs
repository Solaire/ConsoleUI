using ConsoleUI.Structs;
using ConsoleUI.Event;
using System;

namespace ConsoleUI
{
    public abstract class CPanel : CElement, IInteractive
    {
        protected const int M_FOCUSED_ELEMENT_NONE = -1;

        protected readonly PanelTypeCode m_panelType;
        protected readonly CPage m_parent;

        protected int  m_focusedElementIndex;
        protected bool m_isFocused;

        protected string[] m_panelElements;

        public int PercentWidth  { get; protected set; }
        public int PercentHeight { get; protected set; }
        public bool LeftBorder { protected get; set; }
        public bool TopBorder { protected get; set; }

        public PanelTypeCode PanelType { get { return m_panelType; } }

        // Event handler in case a different panel causes change of data
        public delegate void OnDataChangedEventHandler(object sender, GenericEventArgs<string> e);
        public event OnDataChangedEventHandler OnDataChangedString;

        public CPanel(string title, PanelTypeCode type, int percentWidth, int percentHeight, CPage parent) : base(new ConsoleRect(0, 0, 0, 0), title)
        {
            m_panelType     = type;
            m_parent        = parent;

            PercentWidth  = percentWidth;
            PercentHeight = percentHeight;
            LeftBorder = true;
            TopBorder  = true;

            m_focusedElementIndex = M_FOCUSED_ELEMENT_NONE;
            m_isFocused           = false;
        }

        /// <summary>
        /// Set panel postion
        /// </summary>
        /// <param name="x">The x coordinate</param>
        /// <param name="y">The y coordinate</param>
        public void SetPosition(int x, int y)
        {
            m_area.x = x;
            m_area.y = y;
        }

        /// <summary>
        /// Set panel size
        /// </summary>
        /// <param name="width">Width in characters</param>
        /// <param name="height">Height in characters</param>
        public void SetSize(int width, int height)
        {
            m_area.width = width;
            m_area.height = height;
        }

        /// <summary>
        /// Fire OnDataChangedString event
        /// </summary>
        /// <param name="eventData">String event data</param>
        protected void FireDataChangedEvent(string eventData)
        {
            OnDataChangedString.Invoke(this, new GenericEventArgs<string>(eventData));
        }

        public virtual void Redraw(bool fullRedraw)
        {
            if(fullRedraw)
            {
                int startX = m_area.x;
                int startY = m_area.y;
                int lengthX = m_area.width;

                // Background and borders
                CConsoleEx.DrawColourRect(m_area, m_parent.GetColour(ColourThemeIndex.cPanelMainBG));
                if(TopBorder)
                {
                    CConsoleEx.DrawHorizontalLine(startX, startY, lengthX, m_parent.GetColour(ColourThemeIndex.cPanelBorderBG), m_parent.GetColour(ColourThemeIndex.cPanelBorderFG));
                    startY++;
                }
                if(LeftBorder)
                {
                    CConsoleEx.DrawVerticalLine(startX, startY, m_area.height, m_parent.GetColour(ColourThemeIndex.cPanelBorderBG), m_parent.GetColour(ColourThemeIndex.cPanelBorderFG));
                    startX++;
                    lengthX--;
                }

                // Panel header
                CConsoleEx.WriteText(m_title, startX, startY, 1 /*CONSTANT*/, lengthX, m_parent.GetColour(ColourThemeIndex.cPanelBorderBG), m_parent.GetColour(ColourThemeIndex.cPanelBorderFG));
                startY++;

                // Panel content
                for(int row = startY, i = 0; row < m_area.Bottom && i < m_panelElements.Length; row++, i++)
                {
                    ColourThemeIndex background;
                    ColourThemeIndex foreground;

                    if(m_focusedElementIndex == i)
                    {
                       background = (m_isFocused) ? ColourThemeIndex.cPanelSelectFocusBG : ColourThemeIndex.cPanelSelectBG;
                       foreground = (m_isFocused) ? ColourThemeIndex.cPanelSelectFocusFG : ColourThemeIndex.cPanelSelectFG;
                    }
                    else
                    {
                        background = ColourThemeIndex.cPanelMainBG;
                        foreground = ColourThemeIndex.cPanelMainFG;
                    } 
                    
                    // TODO: Fix constants
                    CConsoleEx.WriteText(m_panelElements[i], startX, row, 1 /*CONSTANT*/, lengthX, m_parent.GetColour(background), m_parent.GetColour(foreground));
                }
            }
            else if(m_focusedElementIndex != M_FOCUSED_ELEMENT_NONE)
            {
                int startX = (LeftBorder) ? m_area.x + 1 : m_area.x;
                int startY = (TopBorder)  ? m_area.y + 1 : m_area.y;
                int lengthX = (LeftBorder) ? m_area.width - 1 : m_area.width;

                ColourThemeIndex backgroundSelection = (m_isFocused) ? ColourThemeIndex.cPanelSelectFocusBG : ColourThemeIndex.cPanelSelectBG;
                ColourThemeIndex foregroundSelection = (m_isFocused) ? ColourThemeIndex.cPanelSelectFocusFG : ColourThemeIndex.cPanelSelectFG;

                int currentItemY = startY + m_focusedElementIndex + 1;
                CConsoleEx.WriteText(m_panelElements[m_focusedElementIndex], startX, currentItemY, 1 /*CONSTANT*/, lengthX, m_parent.GetColour(backgroundSelection), m_parent.GetColour(foregroundSelection));

                if(m_focusedElementIndex > 0)
                {
                    int adjecentItemY = startY + m_focusedElementIndex;
                    CConsoleEx.WriteText(m_panelElements[m_focusedElementIndex - 1], startX, adjecentItemY, 1 /*CONSTANT*/, lengthX, m_parent.GetColour(ColourThemeIndex.cPanelMainBG), m_parent.GetColour(ColourThemeIndex.cPanelMainFG));
                }

                if(m_focusedElementIndex < m_panelElements.Length - 1)
                {
                    int adjecentItemY = startY + m_focusedElementIndex + 2;
                    CConsoleEx.WriteText(m_panelElements[m_focusedElementIndex + 1], startX, adjecentItemY, 1 /*CONSTANT*/, lengthX, m_parent.GetColour(ColourThemeIndex.cPanelMainBG), m_parent.GetColour(ColourThemeIndex.cPanelMainFG));
                }
            }
        }

        // Abstract classes
        protected abstract bool LoadContent();
        protected abstract void ReloadContent();
        protected abstract void DrawHighlighted(bool isFocused);
        public abstract void Update();

        // Event handler functions
        public abstract void OnUpdateData(object sender, GenericEventArgs<string> e);

        /// <summary>
        /// If sender panel type matches this panel, set focus status to true, otherwise false
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">Event data containing the panel type code</param>
        public void OnSetFocus(object sender, GenericEventArgs<PanelTypeCode> e)
        {
            bool newFocusState = (m_panelType == e.Data);
            if(newFocusState != m_isFocused)
            {
                Redraw(false);
            }
            m_isFocused = newFocusState;
        }

        // Abstract inferface implementation
        public void KeyPressed(ConsoleKeyInfo keyInfo)
        {
            switch(keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                {
                    m_focusedElementIndex = Math.Max(m_focusedElementIndex - 1, 0);
                    FireDataChangedEvent(m_panelElements[m_focusedElementIndex]);
                }
                break;

                case ConsoleKey.DownArrow:
                {
                    m_focusedElementIndex = Math.Min(m_focusedElementIndex + 1, m_panelElements.Length - 1);
                    FireDataChangedEvent(m_panelElements[m_focusedElementIndex]);
                }
                break;

                default:
                    break;
            }
        }
    }
}
