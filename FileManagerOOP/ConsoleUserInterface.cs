namespace FileManagerOOP;

public class ConsoleUserInterface : IUserInterface
{
    public string ReadLine(string? message, bool newLine)
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

        return Console.ReadLine()!;
    }

    public void WriteLine(string message)
    {
        Console.WriteLine(message);
    }
    public void Write(string message)
    {
        Console.Write(message);
    }
}
