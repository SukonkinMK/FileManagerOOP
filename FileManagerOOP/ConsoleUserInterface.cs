namespace FileManagerOOP;

public class ConsoleUserInterface : IUserInterface
{
    public string ReadLine(string? message)
    {
        //if(message != null && message.Length > 0)
        if (message is { Length: > 0 })
            WriteLine(message);

        return Console.ReadLine()!;
    }

    public void WriteLine(string message)
    {
        Console.WriteLine(message);
    }
}
