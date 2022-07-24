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

            switch (input)
            {
                case "q":
                    can_work = false;
                    break;
                case "int":
                    int int_value  = _userInterface.ReadInt("Введите целое число");
                    _userInterface.WriteLine($"Введено: {int_value}");  
                    break;
                case "double":
                    double double_value = _userInterface.ReadDouble("Введите целое число");
                    _userInterface.WriteLine($"Введено: {double_value}");
                    break;
            }
        }
        while (can_work);
    }
}
