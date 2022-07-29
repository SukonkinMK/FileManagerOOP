using FileManagerOOP.Base.Comands;
namespace FileManagerOOP.Base;

public class CreateDirectoryCommand : FileManagerComand
{
    private readonly IUserInterface _userInterface;
    private readonly FileManagerLogic _fileManager;

    public CreateDirectoryCommand(IUserInterface userInterface, FileManagerLogic fileManager)
    {
        _userInterface = userInterface;
        _fileManager = fileManager;
    }

    public override string Description => "Cоздает все каталоги и подкаталоги по указанному пути";

    public override void Execute(string[] args)
    {
        if (args.Length != 2 || string.IsNullOrWhiteSpace(args[1]))
        {
            _userInterface.WriteLine("Для команды создания каталога необходимо указать один параметр - путь или имя");
            return;
        }

        var path = args[1];
        var directory = _fileManager.CurrentDirectory;

        if (!Path.IsPathRooted(path))
            path = Path.Combine(_fileManager.CurrentDirectory.FullName, path);

        if (Directory.Exists(path))
        {
            _userInterface.WriteLine("Каталог уже существует.");
            return;
        }

        Directory.CreateDirectory(path);
        _userInterface.WriteLine($"Каталог успешно создан. Путь к каталогу {path}");
    }
}
