using FileManagerOOP;
using FileManagerOOP.GUI;

Console.BackgroundColor = ConsoleColor.DarkBlue;
Console.Title = "FileManager";
const int WINDOW_WIDTH = 120;
const int WINDOW_HEIGHT = 40;
Console.SetBufferSize(WINDOW_WIDTH, WINDOW_HEIGHT);
Console.SetWindowSize(WINDOW_WIDTH, WINDOW_HEIGHT);

var infoWindow = new Window(0, 0, WINDOW_WIDTH, 36);
var consoleWindow = new ConsoleWindow(0, 36, WINDOW_WIDTH);

infoWindow.DrawWindow();
consoleWindow.DrawWindow();

var console_ui = new ConsoleUserInterface();
//var manager = new FileManagerLogic(console_ui);
var manager = new FileManagerLogic(infoWindow, consoleWindow);


manager.Start();

Console.WriteLine("Завершение программы...");
Console.ReadLine();