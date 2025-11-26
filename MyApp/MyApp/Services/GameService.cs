using System;
using System.Collections.Generic;
using System.Text;
//using global::MyApp.Models;
using MyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyApp.Services
{
  

  
        public class GameService : IGameService
        {
            private Player player;
            private Level currentLevel;
            private int timeRemaining;
            private int currentLevelIndex = 1;
            private readonly ScoreService scoreService = new ScoreService();

            public void StartGame()
            {
                player = new Player { X = 1, Y = 1, Score = 0 };
                LoadLevel(currentLevelIndex);
                timeRemaining = currentLevel.TimeLimit;
            }

            private void LoadLevel(int levelNumber)
            {
                currentLevel = new Level
                {
                    LevelNumber = levelNumber,
                    MazeLayout = GetMaze(levelNumber),
                    Coins = GenerateCoins(levelNumber),
                    Enemies = GenerateEnemies(levelNumber),
                    TimeLimit = levelNumber == 1 ? 60 : levelNumber == 2 ? 45 : 30
                };
            }

            public void MovePlayer(string direction)
            {
                int newX = player.X, newY = player.Y;
                switch (direction.ToLower())
                {
                    case "up": newX--; break;
                    case "down": newX++; break;
                    case "left": newY--; break;
                    case "right": newY++; break;
                }

                if (currentLevel.MazeLayout[newX, newY] != '#')
                {
                    player.X = newX;
                    player.Y = newY;
                    CheckCoinCollection();
                    CheckEnemyCollision();
                }
            }

            private void CheckCoinCollection()
            {
                var coin = currentLevel.Coins.FirstOrDefault(c => c.X == player.X && c.Y == player.Y && !c.Collected);
                if (coin != null)
                {
                    coin.Collected = true;
                    scoreService.UpdateScore(10);
                    if (currentLevel.Coins.All(c => c.Collected))
                    {
                        Console.WriteLine("Level Completed!");
                        currentLevelIndex++;
                        if (currentLevelIndex <= 3) StartGame();
                        else Console.WriteLine("Game Won!");
                    }
                }
            }

            private void CheckEnemyCollision()
            {
                if (currentLevel.Enemies.Any(e => e.X == player.X && e.Y == player.Y))
                {
                    Console.WriteLine("Game Over! You touched an enemy.");
                    Environment.Exit(0);
                }
            }

            public void DisplayGameState()
            {
                Console.Clear();
                for (int i = 0; i < currentLevel.MazeLayout.GetLength(0); i++)
                {
                    for (int j = 0; j < currentLevel.MazeLayout.GetLength(1); j++)
                    {
                        if (i == player.X && j == player.Y) Console.Write("P");
                        else if (currentLevel.Coins.Any(c => c.X == i && c.Y == j && !c.Collected)) Console.Write("C");
                        else if (currentLevel.Enemies.Any(e => e.X == i && e.Y == j)) Console.Write("E");
                        else Console.Write(currentLevel.MazeLayout[i, j]);
                    }
                    Console.WriteLine();
                }
                Console.WriteLine($"Score: {scoreService.GetScore()} | Time: {timeRemaining}s");
            }

            private char[,] GetMaze(int levelNumber)
            {
                // Static maze layouts for 3 levels
                if (levelNumber == 1)
                    return new char[,] {
                    { '#','#','#','#','#','#','#' },
                    { '#',' ',' ',' ',' ',' ','#' },
                    { '#',' ','#','#','#',' ','#' },
                    { '#',' ',' ',' ','#',' ','#' },
                    { '#','#','#','#','#','#','#' }
                };
                else if (levelNumber == 2)
                    return new char[,] {
                    { '#','#','#','#','#','#','#','#' },
                    { '#',' ',' ','#',' ',' ',' ','#' },
                    { '#',' ','#','#','#',' ','#','#' },
                    { '#',' ',' ',' ','#',' ',' ','#' },
                    { '#','#','#','#','#','#','#','#' }
                };
                else
                    return new char[,] {
                    { '#','#','#','#','#','#','#','#','#' },
                    { '#',' ',' ','#',' ',' ',' ',' ','#' },
                    { '#',' ','#','#','#',' ','#','#','#' },
                    { '#',' ',' ',' ','#',' ',' ',' ','#' },
                    { '#','#','#','#','#','#','#','#','#' }
                };
            }

            private List<Coin> GenerateCoins(int levelNumber)
            {
                return new List<Coin> {
                new Coin { X = 1, Y = 2 },
                new Coin { X = 3, Y = 3 }
            };
            }

            private List<Enemy> GenerateEnemies(int levelNumber)
            {
                if (levelNumber == 1) return new List<Enemy>();
                if (levelNumber == 2) return new List<Enemy> { new Enemy { X = 2, Y = 4 } };
                return new List<Enemy> { new Enemy { X = 2, Y = 4 }, new Enemy { X = 3, Y = 5 } };
            }
        }
    

}
