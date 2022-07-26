namespace FileManagerOOP.GUI;

public class Window : IUserInterface
{
    public int PositionX { get; }

    public int PositionY { get; }

    public int Width { get; }

    public int Height{ get; }

    public Window(int positionX, int positionY, int width, int height)
    {
        this.PositionX = positionX;
        this.PositionY = positionY;
        this.Width = width;
        this.Height = height;
    }

    public void DrawWindow()
    {
        Console.SetCursorPosition(PositionX, PositionY);
        Console.Write("\u2554");
        for (int i = 0; i < Width - 2; i++)
        {
            Console.Write("\u2550");
        }
        Console.Write("\u2557");
        //body
        Console.SetCursorPosition(PositionX, PositionY + 1);
        for (int j = 0; j < Height - 2; j++)
        {
            Console.Write("\u2551");
            for (int i = 0; i < Width - 2; i++)
            {
                Console.Write(" ");
            }
            Console.Write("\u2551");
        }
        //footer
        Console.SetCursorPosition(PositionX, PositionY + Height - 1);
        Console.Write("\u255A");
        for (int i = 0; i < Width - 2; i++)
        {
            Console.Write("\u2550");
        }
        Console.Write("\u255D");
    }

    public void WriteMesage(string? message, bool newLine)
    {
        //if(message != null && message.Length > 0)
        if (message is { Length: > 0 })
        {
            if (newLine)
            {
                WriteLine(message);
            }
            else
            {
                Write(message);
            }
        }
    }

    public double ReadDouble(string? message, bool newLine = true)
    {
        bool success;
        double result;
        do
        {
            WriteMesage(message, newLine);

            var input = Console.ReadLine();
            success = double.TryParse(input, out result);
            if (!success)
                WriteLine("Строка имела неверный формат");
        }
        while (!success);

        return result;
    }

    public int ReadInt(string? message, bool newLine = true)
    {
        bool success;
        int result;
        do
        {
            WriteMesage(message, newLine);

            var input = Console.ReadLine();
            success = int.TryParse(input, out result);
            if (!success)
                WriteLine("Строка имела неверный формат");
        }
        while (!success);

        return result;
    }

    public virtual string ReadLine(string? message, bool newLine = true) { return ""; }

    public virtual void Write(string message) 
    {
        SetCursorPosition();

        Console.Write(message);
    }

    public virtual void WriteLine(string message)
    {
        SetCursorPosition();

        Console.WriteLine(message);
    }

    private void SetCursorPosition()
    {
        (int left, int top) = GetCursorPosition();
        if (top > PositionY + Height - 2 )
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
