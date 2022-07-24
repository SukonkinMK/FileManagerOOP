using FileManagerOOP.Base;
using FileManagerOOP.Base.Comands;
namespace FileManagerOOP;

public class FileManagerLogic
{
    private readonly IUserInterface _userInterface;
    private bool _canWork = true;

    public DirectoryInfo CurrentDirectory { get; set; } = new("c:\\");

    public IReadOnlyDictionary<string, FileManagerComand> Commands { get; }
    public FileManagerLogic(IUserInterface userInterface)
    {
        _userInterface = userInterface;
        Commands = new Dictionary<string, FileManagerComand>()
        {
            { "drives", new DrivesListComand(userInterface) },
            { "dir", new PrintDirectoryFilesCommand(userInterface,this) },
            { "help", new HelpCommand(userInterface, this) },
            { "quit", new QuitCommand(this) },
            { "cd", new ChangeDirectoryCommand(userInterface, this) },
        };
    }
    public void Start()
    {
        _userInterface.WriteLine("Файловый менеджер v2.0");

        do
        {
            var input = _userInterface.ReadLine("> ", false); 

            var args = input.Split(' ');
            var commandName = args[0];

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
        while (_canWork);
    }

    public void Stop()
    {
        _canWork = false;
    }
}
