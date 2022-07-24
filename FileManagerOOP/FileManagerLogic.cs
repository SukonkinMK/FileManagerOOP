namespace FileManagerOOP;

public class FileManagerLogic
{
    private readonly IUserInterface _userInterface;

    public FileManagerLogic(IUserInterface userInterface)
    {
        _userInterface = userInterface;
    }
    public void Start()
    {
        _userInterface.WriteLine("Файловый менеджер");
    }
}
