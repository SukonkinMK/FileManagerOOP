namespace FileManagerOOP;

public interface IUserInterface
{
    void WriteLine(string message);
    void Write(string message);
    string ReadLine(string? message, bool newLine = true);
    int ReadInt(string? message, bool newLine = true);
    double ReadDouble(string? message, bool newLine = true);
}
