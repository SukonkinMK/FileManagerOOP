﻿using FileManagerOOP;

var console_ui = new ConsoleUserInterface();
var manager = new FileManagerLogic(console_ui);

manager.Start(); 

Console.WriteLine("Завершение программы...");
Console.ReadLine();