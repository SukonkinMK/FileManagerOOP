namespace FileManagerOOP.Base.Comands;

public abstract class FileManagerComand
{
    public abstract string Description { get; }

    public abstract void Execute(string[] args);
}
