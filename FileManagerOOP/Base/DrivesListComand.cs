using FileManagerOOP.Base.Comands;
namespace FileManagerOOP.Base;

public class DrivesListComand : FileManagerComand
{
    private readonly IUserInterface _userInterface;

    public DrivesListComand(IUserInterface userInterface)
    {
        _userInterface = userInterface;
    }
    public override void Execute(string[] args)
    {
        var drives = DriveInfo.GetDrives();
        _userInterface.WriteLine($"В файловой системе существует {drives.Length} дисков");
        foreach(var drive in drives)
        {
            _userInterface.WriteLine($"\t{drive.Name}");
        }
    }
}
