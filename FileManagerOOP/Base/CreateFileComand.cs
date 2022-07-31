using FileManagerOOP.Base.Comands;
namespace FileManagerOOP.Base;

public class CreateFileComand : FileManagerComand
{
    private readonly IUserInterface _userInterface;
    private readonly FileManagerLogic _fileManager;

    public CreateFileComand(IUserInterface userInterface, FileManagerLogic fileManager)
    {
        _userInterface = userInterface;
        _fileManager = fileManager;
    }

    public override string Description => "Создание файла по умолчанию в текущей директории с именем {filename} или по заданному пути";

    public override void Execute(string[] args)
    {
        if (args.Length != 2 || string.IsNullOrWhiteSpace(args[1]))
        {
            _userInterface.WriteLine("Для команды создания файла необходимо указать один параметр - путь или имя файла");
            return;
        }

        var filename = args[1];
        var directory = _fileManager.CurrentDirectory;

        if (!Path.IsPathRooted(filename))
            filename = Path.Combine(_fileManager.CurrentDirectory.FullName, filename);

        using (File.Create(filename)) ;
        _userInterface.WriteLine($"Файл успешно создан. Путь к файлу {filename}");
    }
}
