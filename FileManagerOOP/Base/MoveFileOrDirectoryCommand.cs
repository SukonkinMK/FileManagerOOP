using FileManagerOOP.Base.Comands;

namespace FileManagerOOP.Base;

public class MoveFileOrDirectoryCommand : FileManagerComand
{
    private readonly IUserInterface _userInterface;
    private readonly FileManagerLogic _fileManager;

    public MoveFileOrDirectoryCommand(IUserInterface userInterface, FileManagerLogic fileManager)
    {
        _userInterface = userInterface;
        _fileManager = fileManager;
    }

    public override string Description => "Перемещение каталога или файла";

    public override void Execute(string[] args)
    {
        if (args.Length != 3 || string.IsNullOrWhiteSpace(args[1]))
        {
            _userInterface.WriteLine("Для команды перемещения файла или каталога необходимо указать два параметра:");
            _userInterface.WriteLine("1) путь или имя исходного файла или каталога");
            _userInterface.WriteLine("2) путь или имя конечного файла или каталога");
            return;
        }

        var startpath = args[1];
        var endpath = args[2];
        var directory = _fileManager.CurrentDirectory;

        if (!Path.IsPathRooted(startpath))
            startpath = Path.Combine(_fileManager.CurrentDirectory.FullName, startpath);
        if (!Path.IsPathRooted(endpath))
            endpath = Path.Combine(_fileManager.CurrentDirectory.FullName, endpath);

        if (Directory.Exists(startpath))
        {
            var start = new DirectoryInfo(startpath);

            endpath = Path.Combine(endpath, start.Name);
            Directory.Move(startpath, endpath);
        }
        else if (File.Exists(startpath))
        {
            var start = new FileInfo(startpath);
            endpath = Path.Combine(endpath, start.Name);
            File.Move(startpath, endpath);
        }
        else
        {
            _userInterface.WriteLine($"Каталог или файл {startpath} не найден.");
            return;
        }
        
        _userInterface.WriteLine("Перемещение завершено");
    }
}
