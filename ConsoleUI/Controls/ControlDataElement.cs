using System;
using ConsoleUI.Helper;

namespace ConsoleUI.Controls
{
    /// <summary>
    /// Base class for user-defined data for list controls
    /// </summary>
    public abstract class CListControlData
    {
        public abstract int Length { get; }

        public CListControlData(int elementCount)
        {

        }

        public abstract void SetData<T>(T[] data);
        public abstract void LoadList<U>(U updateInfo);
        public abstract void DrawElement(int startX, int startY, int lengthX, int index, ConsoleColor colourFG, ConsoleColor colourBG);
        public abstract void DrawElements(int startX, int startY, int lengthX, int startIndex, int endIndex, ConsoleColor colourFG, ConsoleColor colourBG);
    }

    /// <summary>
    /// String implementation of CListControlData
    /// </summary>
    public class CListControlData_String : CListControlData
    {
        string[] m_data;

        public override int Length { get { return m_data.Length; } }

        public CListControlData_String(int elementCount) : base(elementCount)
        {
            m_data = new string[elementCount];
        }

        public CListControlData_String(string[] data) : base(data.Length)
        {
            m_data = data;
        }

        public override void SetData<T>(T[] data)
        {
            if(typeof(string) != data.GetType())
            {
                throw new ArgumentException("CListControlData_String.Must be a type of string");
            }
            m_data = data as string[];
        }

        public override void LoadList<U>(U updateInfo)
        {
            // Do nothing
            // This class must rely on the SetData function
        }

        public override void DrawElement(int startX, int startY, int lengthX, int index, ConsoleColor colourFG, ConsoleColor colourBG)
        {
            if(index < 0 || index >= Length)
            {
                return;
            }
            CConsoleDraw.WriteText(m_data[index].PadRight(lengthX), startX, startY, colourFG, colourBG);
        }

        public override void DrawElements(int startX, int startY, int lengthX, int startIndex, int endIndex, ConsoleColor colourFG, ConsoleColor colourBG)
        {
            while(startIndex < endIndex)
            {
                if(startIndex < 0 || startIndex >= Length)
                {
                    return;
                }
                CConsoleDraw.WriteText(m_data[startIndex++].PadRight(lengthX), startX, startY++, colourFG, colourBG);
            }
        }
    }
}
