using System;
using ConsoleUI.Type;
using ConsoleUI.Helper;

namespace ConsoleUI.Base
{
    public abstract class CDialogBox : CComponent
    {
        public CDialogBox(string title, ConsoleRect rect) : base(title, rect)
        {

        }

        protected virtual void DrawBorder(bool drawTitle)
        {
            CConsoleDraw.DrawBox(m_rect, ConsoleColor.White, ConsoleColor.Blue, (drawTitle) ? string.Format(" {0} ", m_title) : "");
        }

        public abstract int DoModal();

        public override void Draw(bool redraw)
        {
            if(redraw)
            {
                DrawBorder(true);
            }
        }
    }
}
