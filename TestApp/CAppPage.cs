using System;
using System.Collections.Generic;
using ConsoleUI.Base;
using ConsoleUI.Type;
using ConsoleUI.Controls;

namespace TestApp
{
    public class CAppPage : CPage
    {
        private List<string> m_categories;
        private List<string> m_items;

        public CAppPage(string title, ConsoleRect rect, int componentCount, CFrame parent) : base(title, rect, componentCount, parent)
        {
            m_categories = new List<string>();
            m_items      = new List<string>();
        }

        public override void Initialise()
        {
            for(int i = 0; i < 15; i++)
            {
                m_categories.Add(string.Format("Category {0}", i));
            }
            
            // Create the page views
            CListView categoryView = new CListView(CConstants.CONTROL_DATA[0], new CListWrapper(m_categories), true , this);
            CListView itemsView    = new CListView(CConstants.CONTROL_DATA[1], new CListWrapper(m_items)     , false, this);

            // Add to array
            m_views[0] = categoryView;
            m_views[1] = itemsView;

            // Add any event handlers
            categoryView.SelectedItemChanged += CategoryListView_SelectedChange;

            // Set the view sizes
            CalculateComponentPositions();
        }

        public override void KeyPress(ConsoleKeyInfo keyInfo)
        {
            if(keyInfo.Key == ConsoleKey.Tab)
            {
                m_views[m_focusedComponent].Focused = false;
                m_focusedComponent = (m_focusedComponent == 0) ? 1 : 0;
                m_views[m_focusedComponent].Focused = true;

            }
            else
            {
                m_views[m_focusedComponent].KeyPress(keyInfo);
            }
        }

        public void CategoryListView_SelectedChange(CListViewItemEventArgs e)
        {
            m_items.Clear();
            for(int i = 0; i < 15; i++)
            {
                m_items.Add(string.Format("Category {0}: Item {1}", e.Item, i));
            }
            m_views[1].Draw(true);
        }
    }
}
