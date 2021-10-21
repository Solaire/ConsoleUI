using System;
using ConsoleUI;
using ConsoleUI.Event;
using ConsoleUI.Structs;

namespace TestApp
{
    public class CItemInfoPanel : CControl
    {
        internal struct ItemInfo
        {
            public string itemName;
            public string itemClass;
            public int    itemCounter;
            public bool   isActive;

            public ItemInfo(string itName, string itClass, int itCounter, bool isActive)
            {
                itemName        = itName;
                itemClass       = itClass;
                itemCounter     = itCounter;
                this.isActive   = isActive;
            }

            public void SetInfo(string itName, string itClass, int itCounter, bool isActive)
            {
                itemName = itName;
                itemClass = itClass;
                itemCounter = itCounter;
                this.isActive = isActive;
            }
        }

        private ItemInfo m_currentItem;

        public CItemInfoPanel(string title, ControlTypeCode type, int percentWidth, int percentHeight, CPage parent) : base(title, type, percentWidth, percentHeight, parent)
        {
            m_currentItem = new ItemInfo("", "", 0, false);
        }

        public CItemInfoPanel(string title, ControlTypeCode type, ConsoleRect rect, CPage parent) : base(title, type, rect, parent)
        {
            m_currentItem = new ItemInfo("", "", 0, false);
        }

        public override void Initialise()
        {
            throw new NotImplementedException();
        }

        public override void OnUpdateData(object sender, GenericEventArgs<string> e)
        {
            m_currentItem.itemName = "item name";
            m_currentItem.itemClass = e.Data;
            m_currentItem.itemCounter++;
            m_currentItem.isActive = (m_currentItem.itemCounter % 2 == 0);
            Draw(true);
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }

        public override void Draw(bool fullRedraw)
        {
            //if(redraw)
            {
                int startX = m_rect.x + 1;
                int startY = m_rect.y + 1;
                int lengthX = m_rect.width - 2;

                DrawBorder(true);

                // Content
                CConsoleDraw.WriteText(m_currentItem.itemClass.PadRight(lengthX),               startX, startY++, ConsoleColor.White, ConsoleColor.Blue);
                CConsoleDraw.WriteText(m_currentItem.itemName.PadRight(lengthX),                startX, startY++, ConsoleColor.White, ConsoleColor.Blue);
                CConsoleDraw.WriteText(m_currentItem.itemCounter.ToString().PadRight(lengthX),  startX, startY++, ConsoleColor.White, ConsoleColor.Blue);
                CConsoleDraw.WriteText((m_currentItem.isActive) ? "active".PadRight(lengthX) : "inactive".PadRight(lengthX), startX, startY++, ConsoleColor.White, ConsoleColor.Blue);
            }
        }

        public override void KeyPress(ConsoleKeyInfo keyInfo)
        {
            throw new NotImplementedException();
        }
    }
}
