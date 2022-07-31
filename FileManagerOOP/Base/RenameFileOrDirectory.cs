using FileManagerOOP.Base.Comands;
namespace FileManagerOOP.Base;

public class RenameFileOrDirectory : FileManagerComand
{
    private readonly char[] restrictedSymbols = { '\\', '/', '?', '*', ':', '"', '>', '<', '|' };
    private readonly IUserInterface _userInterface;
    private readonly FileManagerLogic _fileManager;

    public RenameFileOrDirectory(IUserInterface userInterface, FileManagerLogic fileManager)
    {
        _userInterface = userInterface;
        _fileManager = fileManager;
    }

    public override string Description => "Переименовать файл или каталог";

    public override void Execute(string[] args)
    {
        if (args.Length != 3 || string.IsNullOrWhiteSpace(args[1]))
        {
            _userInterface.WriteLine("Для команды переименования файла или каталога необходимо указать два параметра:");
            _userInterface.WriteLine("1) путь или имя исходного файла или каталога");
            _userInterface.WriteLine("новое имя файла или каталога");
            return;
        }

        var startpath = args[1];
        var endpath = args[2];
        var directory = _fileManager.CurrentDirectory;

        if (!Path.IsPathRooted(startpath))
            startpath = Path.Combine(_fileManager.CurrentDirectory.FullName, startpath);

        for (int i = 0; i < restrictedSymbols.Length; i++)
        {
            if (endpath.Contains(restrictedSymbols[i]))
            {
                _userInterface.WriteLine("В новом имени файла или каталога не должно быть символов '\\', '/', '?', '*', ':', '\"', '>', '<', '|'");
                return;
            }
        }


        if (Directory.Exists(startpath))
        {
            endpath = Path.Combine(_fileManager.CurrentDirectory.FullName, endpath);
            Directory.Move(startpath, endpath);
            _userInterface.WriteLine("Каталог успешно переименован");
        }
        else if (File.Exists(startpath))
        {
            endpath = Path.Combine(_fileManager.CurrentDirectory.FullName, endpath);
            File.Move(startpath, endpath);
            _userInterface.WriteLine("Файл успешно переименован");
        }
        else
        {
            _userInterface.WriteLine($"Каталог или файл {startpath} не найден.");
            return;
        }
    }
}
