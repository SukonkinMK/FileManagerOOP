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
        _userInterface.WriteLine("Файловый менеджер v2.0");

        bool can_work = true;
        do
        {
            var input = _userInterface.ReadLine("> ", false);

            if (input == "q")
                can_work = false;
            else
                _userInterface.WriteLine($"Введена команда {input}");
        }
        while (can_work);
    }
}
