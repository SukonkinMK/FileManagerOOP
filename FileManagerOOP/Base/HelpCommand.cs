using FileManagerOOP.Base.Comands;
namespace FileManagerOOP.Base;

public class HelpCommand : FileManagerComand
{
    private readonly IUserInterface _userInterface;
    private readonly FileManagerLogic _fileManager;

    public override string Description => "Помощь";

    public HelpCommand(IUserInterface userInterface, FileManagerLogic fileManager)
    {
        _userInterface = userInterface;
        _fileManager = fileManager;
    }
    public override void Execute(string[] args)
    {
        _userInterface.WriteLine("Файловый менеджер поддерживает следующие команды:");
        foreach (var (name, command) in _fileManager.Commands)
        {
            _userInterface.WriteLine($"\t{name}\t{command.Description}");
        }
    }

}
