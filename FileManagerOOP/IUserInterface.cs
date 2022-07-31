using System.Text;

namespace FileManagerOOP;

public interface IUserInterface
{
    void WriteLine(string message);
    void Write(string message);
    void Write(StringBuilder message, string[] args);
    string ReadLine(string? message, bool newLine = true);
    int ReadInt(string? message, bool newLine = true);
    double ReadDouble(string? message, bool newLine = true);
}
