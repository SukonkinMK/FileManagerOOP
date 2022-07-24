using FileManagerOOP.Base.Comands;
namespace FileManagerOOP.Base;

public class PrintDirectoryFilesCommand : FileManagerComand
{
    private readonly IUserInterface _userInterface;
    private readonly FileManagerLogic _fileManager;

    public PrintDirectoryFilesCommand(IUserInterface userInterface, FileManagerLogic fileManager)
    {
        _userInterface = userInterface;
        _fileManager = fileManager;
    }
    public override void Execute(string[] args)
    {
        var directory = _fileManager.CurrentDirectory;
        _userInterface.WriteLine($"Содержимое директории {directory}");

        int dirs_count = 0;
        foreach(var sub_dir in directory.EnumerateDirectories())
        {
            _userInterface.WriteLine($"\td\t{sub_dir.Name}");
            dirs_count++;
        }

        int files_count = 0;
        long total_length = 0;
        foreach (var file in directory.EnumerateFiles())
        {
            _userInterface.WriteLine($"\tf\t{file.Name}\t{file.Length} B");
            files_count++;
            total_length += file.Length;
        }

        _userInterface.WriteLine("");
        _userInterface.WriteLine($"Директорий {dirs_count} шт., файлов {files_count} шт. (суммарный размер {total_length/1000} kB)");
    }
}
