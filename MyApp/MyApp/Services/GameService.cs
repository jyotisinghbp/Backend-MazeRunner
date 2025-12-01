
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers; // ✅ Correct namespace for Timer
using MyApp.Models;

namespace MyApp.Services
{
    public class GameService : IGameService
    {
        private Player? player;
        private Level? currentLevel;
        private int currentLevelIndex = 1;
        private int timeRemaining;
        private System.Timers.Timer? gameTimer;
        private System.Timers.Timer? enemyTimer;
        private readonly IScoreService scoreService = new ScoreService();

        public void StartGame()
        {
            player = new Player { X = 1, Y = 1, Score = 0 };
            scoreService.ResetScore(); // ✅ Added in IScoreService and ScoreService
            LoadLevel(currentLevelIndex);
            timeRemaining = currentLevel!.TimeLimit;
            StartTimers();
        }

        private void StartTimers()
        {
            gameTimer?.Stop();
            enemyTimer?.Stop();

            // Countdown Timer
            gameTimer = new System.Timers.Timer(1000);
            gameTimer.Elapsed += (s, e) =>
            {
                timeRemaining--;
                if (timeRemaining <= 0)
                {
                    GameOver("Time's UP!");
                }
            };
            gameTimer.Start();

            // Enemy Movement Timer (speed increases per level)
            int enemySpeed = currentLevelIndex == 1 ? 2000 : currentLevelIndex == 2 ? 1500 : 1000;
            enemyTimer = new System.Timers.Timer(enemySpeed);
            enemyTimer.Elapsed += (s, e) =>
            {
                foreach (var enemy in currentLevel!.Enemies)
                {
                    enemy.MoveRandomly(currentLevel.MazeLayout, currentLevel.Coins);
                }
            };
            enemyTimer.Start();
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
            int newX = player!.X, newY = player.Y;
            switch (direction.ToLower())
            {
                case "up": newX--; break;
                case "down": newX++; break;
                case "left": newY--; break;
                case "right": newY++; break;
            }

            if (currentLevel!.MazeLayout[newX, newY] != '#')
            {
                player.X = newX;
                player.Y = newY;
                CheckCoinCollection();
                CheckEnemyCollision();
            }
        }

        private void CheckCoinCollection()
        {
            var coin = currentLevel!.Coins.FirstOrDefault(c => c.X == player!.X && c.Y == player.Y && !c.Collected);
            if (coin != null)
            {
                coin.Collected = true;
                scoreService.UpdateScore(10);

                if (currentLevel.Coins.All(c => c.Collected))
                {
                    Console.Clear();
                    Console.WriteLine($"Level {currentLevelIndex} Completed!");
                    currentLevelIndex++;
                    if (currentLevelIndex <= 3)
                    {
                        StartGame();
                    }
                    else
                    {
                        Console.WriteLine("🎉 Game Won! 🎉");
                        Environment.Exit(0);
                    }
                }
            }
        }

        private void CheckEnemyCollision()
        {
            if (currentLevel!.Enemies.Any(e => e.X == player!.X && e.Y == player.Y))
            {
                GameOver("caught by enemies");
            }
        }

        private void GameOver(string reason)
        {
            gameTimer?.Stop();
            enemyTimer?.Stop();
            Console.Clear();
            Console.WriteLine($"Game Over: {reason}");
            Console.WriteLine("1. Retry");
            Console.WriteLine("2. Exit");
            var choice = Console.ReadLine();
            if (choice == "1")
            {
                currentLevelIndex = 1;
                StartGame();
            }
            else
            {
                Environment.Exit(0);
            }
        }

        public void DisplayGameState()
        {
            Console.Clear();
            for (int i = 0; i < currentLevel!.MazeLayout.GetLength(0); i++)
            {
                for (int j = 0; j < currentLevel.MazeLayout.GetLength(1); j++)
                {
                    if (i == player!.X && j == player.Y) Console.Write("P");
                    else if (currentLevel.Coins.Any(c => c.X == i && c.Y == j && !c.Collected)) Console.Write("C");
                    else if (currentLevel.Enemies.Any(e => e.X == i && e.Y == j)) Console.Write("E");
                    else Console.Write(currentLevel.MazeLayout[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine($"Score: {scoreService.GetScore()} | Time: {timeRemaining}s");
            Console.WriteLine("Use Arrow Keys to move. Press R to restart, Esc to exit.");
        }

        private char[,] GetMaze(int levelNumber)
        {
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
                    { '#',' ',' ',' ',' ',' ',' ','#' },
                    { '#','#','#','#',' ',' ','#','#' },
                    { '#',' ',' ',' ',' ',' ',' ','#' },
                    { '#','#','#','#','#','#','#','#' }
                };
            else
                return new char[,] {
                    { '#','#','#','#','#','#','#','#','#' },
                    { '#',' ',' ',' ',' ',' ',' ',' ','#' },
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
