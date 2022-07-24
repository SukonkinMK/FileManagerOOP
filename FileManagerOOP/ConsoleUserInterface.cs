namespace FileManagerOOP;

public class ConsoleUserInterface : IUserInterface
{
    private void WriteMesage(string? message, bool newLine)
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

    public string ReadLine(string? message, bool newLine)
    {
        WriteMesage(message, newLine);    
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

    public int ReadInt(string? message, bool newLine)
    {
        bool success;
        int result;
        do
        {
            WriteMesage(message, newLine);

            var input = Console.ReadLine();
            success = int.TryParse(input, out result);
            if(!success)
                WriteLine("Строка имела неверный формат");
        }
        while (!success);
        
        return result;
    }

    public double ReadDouble(string? message, bool newLine)
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
}
