
using MyApp.Services;
using System;

class Program
{
    static void Main(string[] args)
    {
        IGameService game = new GameService();
        game.StartGame();

        while (true)
        {
            game.DisplayGameState();
            Console.WriteLine("Enter command (up/down/left/right/restart/exit):");
            string command = Console.ReadLine();
            if (command == "exit") break;
            if (command == "restart") game.StartGame();
            else game.MovePlayer(command);
        }
    }
}
