using ConsoleUI.Structs;
using ConsoleUI.Event;
using System;

namespace ConsoleUI
{
    /// <summary>
    /// Base class describing UI control elements such as list, message box, etc
    /// </summary>
    public abstract class CControl : CComponent
    {
        protected readonly ControlTypeCode m_type;
        protected readonly CPage           m_parent;
        protected readonly int             m_percentWidth;
        protected readonly int             m_percentHeight;

        protected bool m_isFocused;

        // Getters
        public int PercentWidth          { get { return m_percentWidth; } }
        public int PercentHeight         { get { return m_percentHeight; } }
        public ControlTypeCode PanelType { get { return m_type; } }

        // Event handler in case a different panel causes change of data
        public delegate void OnDataChangedEventHandler(object sender, GenericEventArgs<string> e);
        public event OnDataChangedEventHandler OnDataChangedString;

        /// <summary>
        /// Constructor.
        /// Sets the percentage size values (for dynamic components)
        /// </summary>
        /// <param name="title">The control title</param>
        /// <param name="type">The control type code</param>
        /// <param name="percentWidth">Control width as percentage of parent area</param>
        /// <param name="percentHeight">Control height as percentage of parent area</param>
        /// <param name="parent">Reference to the containing CPage instance</param>
        public CControl(string title, ControlTypeCode type, int percentWidth, int percentHeight, CPage parent) : base(title, new ConsoleRect(0, 0, 0, 0))
        {
            m_type          = type;
            m_parent        = parent;
            m_percentWidth  = percentWidth;
            m_percentHeight = percentHeight;

            m_isFocused = false;
        }

        /// <summary>
        /// Constructor.
        /// Set the control's size and position values (for static components)
        /// </summary>
        /// <param name="title">The control title</param>
        /// <param name="type">The control type code</param>
        /// <param name="rect">Size and position of the control</param>
        /// <param name="parent">Reference to containing CPage instance</param>
        public CControl(string title, ControlTypeCode type, ConsoleRect rect, CPage parent) : base(title, rect)
        {
            m_type          = type;
            m_parent        = parent;
            m_percentWidth  = 0;
            m_percentHeight = 0;

            m_isFocused = false;
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
        protected void FireDataChangedEvent(string eventData)
        {
            OnDataChangedString.Invoke(this, new GenericEventArgs<string>(eventData));
        }

        /// <summary>
        /// Draw the control
        /// </summary>
        /// <param name="redraw">If true, all elements will be redrawn</param>
        /*
        public abstract void Draw(bool redraw);
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
                CConsoleEx.WriteText(m_title, startX, startY, 1 /*CONSTANT*, lengthX, m_parent.GetColour(ColourThemeIndex.cPanelBorderBG), m_parent.GetColour(ColourThemeIndex.cPanelBorderFG));
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
                    CConsoleEx.WriteText(m_panelElements[i], startX, row, 1 /*CONSTANT*, lengthX, m_parent.GetColour(background), m_parent.GetColour(foreground));
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
                CConsoleEx.WriteText(m_panelElements[m_focusedElementIndex], startX, currentItemY, 1 /*CONSTANT*, lengthX, m_parent.GetColour(backgroundSelection), m_parent.GetColour(foregroundSelection));
        
                if(m_focusedElementIndex > 0)
                {
                    int adjecentItemY = startY + m_focusedElementIndex;
                    CConsoleEx.WriteText(m_panelElements[m_focusedElementIndex - 1], startX, adjecentItemY, 1 /*CONSTANT*, lengthX, m_parent.GetColour(ColourThemeIndex.cPanelMainBG), m_parent.GetColour(ColourThemeIndex.cPanelMainFG));
                }
        
                if(m_focusedElementIndex < m_panelElements.Length - 1)
                {
                    int adjecentItemY = startY + m_focusedElementIndex + 2;
                    CConsoleEx.WriteText(m_panelElements[m_focusedElementIndex + 1], startX, adjecentItemY, 1 /*CONSTANT*, lengthX, m_parent.GetColour(ColourThemeIndex.cPanelMainBG), m_parent.GetColour(ColourThemeIndex.cPanelMainFG));
                }
            }
        }
        */

        // Event handler functions
        public abstract void OnUpdateData(object sender, GenericEventArgs<string> e);

        /// <summary>
        /// If sender panel type matches this panel, set focus status to true, otherwise false
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">Event data containing the panel type code</param>
        public void OnSetFocus(object sender, GenericEventArgs<ControlTypeCode> e)
        {
            bool newFocusState = (m_type == e.Data);
            if(newFocusState != m_isFocused)
            {
                Draw(false);
            }
            m_isFocused = newFocusState;
        }

        /// <summary>
        /// Draw border
        /// </summary>
        protected virtual void DrawBorder(bool drawTitle)
        {
            CConsoleDraw.DrawBox(m_rect, ConsoleColor.White, ConsoleColor.Blue, (drawTitle) ? string.Format(" {0} ", m_title) : "");
        }

        // Abstract inferface implementation
        /*
        public abstract void KeyPressed(ConsoleKeyInfo keyInfo);
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
        */

        /// <summary>
        /// Struct holding information about given control
        /// </summary>
        public struct ControlData
        {
            public ControlTypeCode type;
            public string          title;
            public int             percentWidth;
            public int             percentHeight;

            public ControlData(ControlTypeCode type, string title, int percentWidth, int percentHeight)
            {
                this.type           = type;
                this.title          = title;
                this.percentWidth   = percentWidth;
                this.percentHeight  = percentHeight;
            }
        }

        /// <summary>
        /// Abstract class representing a control type
        /// Essentially an inheritable and extensible enumerator
        /// </summary>
        public class ControlTypeCode
        {
            public byte Code { get; private set; }
            public ControlTypeCode(byte code)
            {
                Code = code;
            }

            public static bool operator ==(ControlTypeCode a, ControlTypeCode b)
            {
                return a.Code == b.Code;
            }

            public static bool operator !=(ControlTypeCode a, ControlTypeCode b)
            {
                return a.Code != b.Code;
            }
        }
    }
}
