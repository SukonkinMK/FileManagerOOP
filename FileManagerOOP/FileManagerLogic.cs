using FileManagerOOP.Base;
using FileManagerOOP.Base.Comands;
namespace FileManagerOOP;

public class FileManagerLogic
{
    private readonly IUserInterface _userInterface;

    public DirectoryInfo CurrentDirectory { get; set; } = new("c:\\");

    public IReadOnlyDictionary<string, FileManagerComand> Commands { get; }
    public FileManagerLogic(IUserInterface userInterface)
    {
        _userInterface = userInterface;
        Commands = new Dictionary<string, FileManagerComand>()
        {
            { "drives", new DrivesListComand(userInterface) },
            { "dir", new PrintDirectoryFilesCommand(userInterface,this) }
        };
    }
    public void Start()
    {
        _userInterface.WriteLine("Файловый менеджер v2.0");

        bool can_work = true;
        do
        {
            var input = _userInterface.ReadLine("> ", false); 

            var args = input.Split(' ');
            var commandName = args[0];

            if (commandName == "quit") //РЕАЛИЗОВАТЬ КОМАНДОЙ
            {
                can_work = false;
                continue;
            }

            if(!Commands.TryGetValue(commandName, out var command))
            {
                _userInterface.WriteLine($"Неизвестная команда {commandName}");
                _userInterface.WriteLine("Для справки введите help, для выхода - quit");
                continue;
            }
            try
            {
                command.Execute(args);
            }
            catch (Exception err)
            {
                _userInterface.WriteLine($"При выполнении команды {commandName} произошла ошибка");
                _userInterface.WriteLine(err.Message);
            }
        }
        while (can_work);
    }
}
