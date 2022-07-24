using FileManagerOOP.Base.Comands;
namespace FileManagerOOP.Base;

public class ChangeDirectoryCommand : FileManagerComand
{
    private readonly IUserInterface _userInterface;
    private readonly FileManagerLogic _fileManager;

    public override string Description => "Смена текущей директории";

    public ChangeDirectoryCommand(IUserInterface userInterface, FileManagerLogic fileManager)
    {
        _userInterface = userInterface;
        _fileManager = fileManager;
    }

    public override void Execute(string[] args)
    {
        if (args.Length != 2 || string.IsNullOrWhiteSpace(args[1]))
        {
            _userInterface.WriteLine("Для команды смены каталога необходимо указать один параметр - целевой каталог");
            return;
        }

        var dir_path = args[1];

        DirectoryInfo? directory;
        /*if (Path.IsPathRooted(dir_path))
        {
            directory = new DirectoryInfo(dir_path);
        }
        else
        {
            directory = new DirectoryInfo(Path.Combine(_fileManager.CurrentDirectory.FullName, dir_path));
        }*/

        if (dir_path == "..")
        {
            directory = _fileManager.CurrentDirectory.Parent;
            if(directory is null)
            {
                _userInterface.WriteLine("Невозможно подняться выше по дереву каталогов");
                return;
            }
        }
        else if (!Path.IsPathRooted(dir_path))
            dir_path = Path.Combine(_fileManager.CurrentDirectory.FullName, dir_path);
        directory = new DirectoryInfo(dir_path);

        if (!directory.Exists)
        {
            _userInterface.WriteLine($"Целевая директория {directory} не существует");
            return;
        }

        _fileManager.CurrentDirectory = directory;
        _userInterface.WriteLine($"Текущая директория изменена на {directory.FullName}");
        Directory.SetCurrentDirectory(directory.FullName);

    }
}
