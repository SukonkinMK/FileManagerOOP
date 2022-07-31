using FileManagerOOP.Base.Comands;
namespace FileManagerOOP.Base;

public class QuitCommand : FileManagerComand
{
    private readonly FileManagerLogic _fileManager;

    public override string Description => "Выход из программы";

    public QuitCommand(FileManagerLogic fileManager)
    {
        _fileManager = fileManager;
    }

    public override void Execute(string[] args)
    {
        _fileManager.Stop();
    }
}
