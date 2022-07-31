using FileManagerOOP.Base.Comands;
using System.Text;

namespace FileManagerOOP.Base;

public class PrintDirectoryFilesCommand : FileManagerComand
{
    private readonly IUserInterface _userInterface;
    private readonly FileManagerLogic _fileManager;

    public override string Description => "Вывод содержимого директории по странично с аргументом {-p n}, где n - номер страницы";

    public PrintDirectoryFilesCommand(IUserInterface userInterface, FileManagerLogic fileManager)
    {
        _userInterface = userInterface;
        _fileManager = fileManager;
    }
    public override void Execute(string[] args)
    {
        var directory = _fileManager.CurrentDirectory;

        StringBuilder tree = new StringBuilder();
        tree.Append($"{directory}\n");

        DirectoryInfo[] sub_dirs = directory.GetDirectories();
        for (int i = 0; i < sub_dirs.Length; i++)
        {
            if (i == sub_dirs.Length - 1)
                tree.Append($"  └─{sub_dirs[i].Name}\n");
            else
                tree.Append($"  ├─{sub_dirs[i].Name}\n");
        }

        FileInfo[] files = directory.GetFiles();
        long total_length = 0;

        for (int i = 0; i < files.Length; i++)
        {
            if (i == files.Length - 1)
            {
                tree.Append($"  └─{files[i].Name}\n");
            }
            else
            {
                tree.Append($"  ├─{files[i].Name}\n");
            }
            total_length += files[i].Length;
        }
        _userInterface.Write(tree, args);

        #region
        /*_userInterface.WriteLine($"{directory}");

        
        DirectoryInfo[] sub_dirs1 = directory.GetDirectories();
        for (int i = 0; i < sub_dirs1.Length; i++)
        {
            if (i == sub_dirs.Length - 1)
                _userInterface.WriteLine($"  └─{sub_dirs1[i].Name}");
            else
                _userInterface.WriteLine($"  ├─{sub_dirs1[i].Name}");
        }

        FileInfo[] files1 = directory.GetFiles();
        long total_length1 = 0;

        for (int i = 0; i < files1.Length; i++)
        {
            if (i == files1.Length - 1)
            {
                _userInterface.WriteLine($"  └─{files1[i].Name}");
            }
            else
            {
                _userInterface.WriteLine($"  ├─{files1[i].Name}");
            }
            total_length += files1[i].Length;
        }

        //_userInterface.WriteLine("");
        //_userInterface.WriteLine($"Директорий {dirs_count} шт., файлов {files_count} шт. (суммарный размер {total_length/1000} kB)");*/
        #endregion
    }
}
