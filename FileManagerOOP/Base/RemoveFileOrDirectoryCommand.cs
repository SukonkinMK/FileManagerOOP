using FileManagerOOP.Base.Comands;
namespace FileManagerOOP.Base;

public class RemoveFileOrDirectoryCommand : FileManagerComand
{
    private readonly IUserInterface _userInterface;
    private readonly FileManagerLogic _fileManager;

    public RemoveFileOrDirectoryCommand(IUserInterface userInterface, FileManagerLogic fileManager)
    {
        _userInterface = userInterface;
        _fileManager = fileManager;
    }

    public override string Description => "Удаление файла или каталога";

    public override void Execute(string[] args)
    {
        if (args.Length != 2 || string.IsNullOrWhiteSpace(args[1]))
        {
            _userInterface.WriteLine("Для команды удаления файла или каталога необходимо указать один параметр - путь или имя");
            return;
        }

        var path = args[1];
        var directory = _fileManager.CurrentDirectory;

        if (!Path.IsPathRooted(path))
            path = Path.Combine(_fileManager.CurrentDirectory.FullName, path);

        if (Directory.Exists(path))
        {
            Directory.Delete(path, true);
            _userInterface.WriteLine($"Каталог {path} успешно удален.");
        }
        else if (File.Exists(path))
        {
            File.Delete(path);
            _userInterface.WriteLine($"Файл {path} успешно удален.");
        }
    }
}
