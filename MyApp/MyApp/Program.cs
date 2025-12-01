
using System;
using MyApp.Services;

class Program
{
    static void Main(string[] args)
    {
        IGameService game = new GameService();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Maze Runner ===");
            Console.WriteLine("1. Start Game");
            Console.WriteLine("2. Exit");
            Console.Write("Choose an option: ");
            var choice = Console.ReadLine();

            if (choice == "1")
            {
                game.StartGame();
                PlayGame(game);
            }
            else if (choice == "2")
            {
                Console.WriteLine("Goodbye!");
                break;
            }
        }
    }

    static void PlayGame(IGameService game)
    {
        while (true)
        {
            game.DisplayGameState();
            var key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.Escape) break;
            if (key == ConsoleKey.R)
            {
                game.StartGame();
                continue;
            }

            switch (key)
            {
                case ConsoleKey.UpArrow: game.MovePlayer("up"); break;
                case ConsoleKey.DownArrow: game.MovePlayer("down"); break;
                case ConsoleKey.LeftArrow: game.MovePlayer("left"); break;
                case ConsoleKey.RightArrow: game.MovePlayer("right"); break;
            }
        }
    }
}
