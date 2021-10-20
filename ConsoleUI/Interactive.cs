namespace ConsoleUI
{
    /// <summary>
    /// Interface containing essential function definitions for interactive elements (controls)
    /// </summary>
    public interface IInteractive
    {
        public abstract void KeyPressed(System.ConsoleKeyInfo keyInfo);
    }
}
