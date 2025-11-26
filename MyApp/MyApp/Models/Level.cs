using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Models
{
  
        public class Level
        {
            public int LevelNumber { get; set; }
            public char[,] MazeLayout { get; set; }
            public List<Coin> Coins { get; set; }
            public List<Enemy> Enemies { get; set; }
            public int TimeLimit { get; set; }
        }
 }