using FileManagerOOP.Base;
using FileManagerOOP.Base.Comands;
using FileManagerOOP.GUI;

namespace FileManagerOOP;

public class FileManagerLogic
{
    private readonly IUserInterface _outUserInterface;
    private readonly IUserInterface _inUserInterface;
    private bool _canWork = true;

    public DirectoryInfo CurrentDirectory { get; set; } = new("c:\\");

    public IReadOnlyDictionary<string, FileManagerComand> Commands { get; }
    public FileManagerLogic(IUserInterface outUserInterface, IUserInterface inUserInterface)
    {
        _outUserInterface = outUserInterface;
        _inUserInterface = inUserInterface;

        var dirCommand = new PrintDirectoryFilesCommand(_outUserInterface, this);
        Commands = new Dictionary<string, FileManagerComand>()
        {
            { "drives", new DrivesListComand(_outUserInterface) },
            { "dir", dirCommand },
            { "dir -p n", dirCommand },
            { "help", new HelpCommand(_outUserInterface, this) },
            { "quit", new QuitCommand(this) },
            { "cd", new ChangeDirectoryCommand(_outUserInterface, this) },
        };
    }
    public void Start()
    {
        _outUserInterface.WriteLine("Файловый менеджер v2.0");

        do
        {
            var input = _inUserInterface.ReadLine("> ", false);
            var w = (Window)_inUserInterface;
            w.DrawWindow();
            w = (Window)_outUserInterface;
            w.DrawWindow();
            var args = input.Split(' ');
            var commandName = args[0];

            if(!Commands.TryGetValue(commandName, out var command))
            {
                _outUserInterface.WriteLine($"Неизвестная команда {commandName}");
                _outUserInterface.WriteLine("Для справки введите help, для выхода - quit");
                continue;
            }
            try
            {
                command.Execute(args);
            }
            catch (Exception err)
            {
                _outUserInterface.WriteLine($"При выполнении команды {commandName} произошла ошибка");
                _outUserInterface.WriteLine(err.Message);
            }
        }
        while (_canWork);
    }

    public void Stop()
    {
        _canWork = false;
    }
}
