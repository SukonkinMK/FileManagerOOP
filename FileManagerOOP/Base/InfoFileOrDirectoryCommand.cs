using FileManagerOOP.Base.Comands;
namespace FileManagerOOP.Base;

public class InfoFileOrDirectoryCommand : FileManagerComand
{
    private readonly IUserInterface _userInterface;
    private readonly FileManagerLogic _fileManager;

    public InfoFileOrDirectoryCommand(IUserInterface userInterface, FileManagerLogic fileManager)
    {
        _userInterface = userInterface;
        _fileManager = fileManager;
    }

    public override string Description => "Вывод информации о файле или директории";

    public override void Execute(string[] args)
    {
        if (args.Length != 2 || string.IsNullOrWhiteSpace(args[1]))
        {
            _userInterface.WriteLine("Для получения информации о файле или каталоге необходимо указать один параметр - путь или имя");
            return;
        }

        var path = args[1];
        var directory = _fileManager.CurrentDirectory;

        if (!Path.IsPathRooted(path))
            path = Path.Combine(_fileManager.CurrentDirectory.FullName, path);

        if (Directory.Exists(path))
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            GetInfo(dir);
        }
        else if (File.Exists(path))
        {
            FileInfo file = new FileInfo(path);
            GetInfo(file);
        }
    }

    private void GetInfo(FileInfo file)
    {
        _userInterface.WriteLine($"Тип файла: {file.Extension}");
        _userInterface.WriteLine($"Размер файла: {file.Length} bytes");
        _userInterface.WriteLine($"Расположение: {file.Directory}");
        _userInterface.WriteLine($"Создан: {file.CreationTime.ToString("dd MMMM yyyy г., HH:mm:ss")}");
        _userInterface.WriteLine($"Изменен: {file.LastWriteTime.ToString("dd MMMM yyyy г., HH:mm:ss")}");
        _userInterface.WriteLine($"Доступен только для чтения: {file.IsReadOnly}");
    }

    private void GetInfo(DirectoryInfo dir)
    {
        _userInterface.WriteLine("Тип: папка с файлами");
        _userInterface.WriteLine($"Размер папки: {DirSize(dir)} bytes");
        _userInterface.WriteLine($"Расположена на диске: {dir.Root}");
        _userInterface.WriteLine($"Созданa: {dir.CreationTime.ToString("dd MMMM yyyy г., HH:mm:ss")}");
        _userInterface.WriteLine($"Измененa: {dir.LastWriteTime.ToString("dd MMMM yyyy г., HH:mm:ss")}");
        _userInterface.WriteLine($"Содержит: {SubDirsQuantuty(dir)} папок, {SubFilesQuantuty(dir)} файлов");
    }

    static int SubDirsQuantuty(DirectoryInfo d)
    {
        int quantity = 0;
        DirectoryInfo[] dirs = d.GetDirectories();
        quantity += dirs.Length;
        foreach (DirectoryInfo dir in dirs)
        {
            quantity += SubDirsQuantuty(dir);
        }
        return quantity;
    }

    /// <summary>
    /// Подсчет общего числа вложенных файлов
    /// </summary>
    /// <param name="d">Целевой каталог</param>
    /// <returns>Число вложенных в целевой каталог файлов</returns>
    static int SubFilesQuantuty(DirectoryInfo d)
    {
        int quantity = 0;
        FileInfo[] files = d.GetFiles();
        quantity += files.Length;
        DirectoryInfo[] dirs = d.GetDirectories();
        foreach (DirectoryInfo dir in dirs)
        {
            quantity += SubFilesQuantuty(dir);
        }
        return quantity;
    }

    /// <summary>
    /// Подсчет размера каталога на диске
    /// </summary>
    /// <param name="d">Целевой каталог</param>
    /// <returns>Размер каталога на диске в байтах</returns>
    static long DirSize(DirectoryInfo d)
    {
        long size = 0;
        FileInfo[] files = d.GetFiles();
        foreach (FileInfo file in files)
        {
            size += file.Length;
        }
        DirectoryInfo[] dirs = d.GetDirectories();
        foreach (DirectoryInfo dir in dirs)
        {
            size += DirSize(dir);
        }
        return size;
    }
}
