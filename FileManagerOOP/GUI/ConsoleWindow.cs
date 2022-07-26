namespace FileManagerOOP.GUI;

public class ConsoleWindow : Window
{
    public const int _consoleHeight = 3;

    public ConsoleWindow(int positionX, int positionY, int width) : base(positionX, positionY, width, _consoleHeight)
    {
    }

    public override string ReadLine(string? message, bool newLine = true)
    {
        Write(message!);
        return Console.ReadLine()!;
    }

    public override void Write(string message)
    {
        SetCursorPosition();

        Console.Write(message);
    }

    public override void WriteLine(string message)
    {
        return;
    }

    private void SetCursorPosition()
    {
        (int left, int top) = GetCursorPosition();
        if (top > PositionY + Height - 1 || top < PositionY)
        {
            Console.SetCursorPosition(PositionX, PositionY + 1);
            (left, top) = GetCursorPosition();
        }
        if (left == 0)
            Console.SetCursorPosition(left + 1, top);
    }
    private (int left, int top) GetCursorPosition()
    {
        return (Console.CursorLeft, Console.CursorTop);
    }
}
