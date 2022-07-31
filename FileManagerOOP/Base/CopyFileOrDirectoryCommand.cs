using FileManagerOOP.Base.Comands;
namespace FileManagerOOP.Base;

public class CopyFileOrDirectoryCommand : FileManagerComand
{
    private readonly IUserInterface _userInterface;
    private readonly FileManagerLogic _fileManager;

    public CopyFileOrDirectoryCommand(IUserInterface userInterface, FileManagerLogic fileManager)
    {
        _userInterface = userInterface;
        _fileManager = fileManager;
    }

    public override string Description => "Копирование файла или каталога";

    public override void Execute(string[] args)
    {
        if (args.Length != 3 || string.IsNullOrWhiteSpace(args[1]))
        {
            _userInterface.WriteLine("Для команды копирования файла или каталога необходимо указать два параметра:");
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
            CopyDirectory(startpath, endpath, true);
            _userInterface.WriteLine($"Каталог {startpath} успешно скопирован в каталог {endpath}.");
        }
        else if (File.Exists(startpath))
        {
            File.Copy(startpath, endpath);
            _userInterface.WriteLine($"Файл {startpath} успешно скопирован в {endpath}.");
        }
        else
        {
            _userInterface.WriteLine($"Каталог или файл {startpath} не найден.");
        }
    }

    /// <summary>
    /// Копирование каталога
    /// </summary>
    /// <param name="sourseDir">Копируемый каталог</param>
    /// <param name="targetDir">Скопированый каталог</param>
    /// <param name="recursive">скопировать все вложенные каталоги</param>
    private void CopyDirectory(string sourseDir, string targetDir, bool recursive)
    {
        DirectoryInfo dir = new DirectoryInfo(sourseDir);
        DirectoryInfo[] dirs = dir.GetDirectories();
        Directory.CreateDirectory(targetDir);
        foreach (FileInfo file in dir.GetFiles())
        {
            string targetFilePath = Path.Combine(targetDir, file.Name);
            file.CopyTo(targetFilePath);
        }
        if (recursive)
        {
            foreach (DirectoryInfo subDir in dirs)
            {
                string newTargetDir = Path.Combine(targetDir, subDir.Name);
                CopyDirectory(subDir.FullName, newTargetDir, true);
            }
        }
    }
}
