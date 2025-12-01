using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Models
{
  
        public class Level
        {
            public int LevelNumber { get; set; }
            public char[,] MazeLayout { get; set; } = new char[0,0];
            public List<Coin> Coins { get; set; } = new List<Coin>();
            public List<Enemy> Enemies { get; set; } = new List<Enemy>();
            public int TimeLimit { get; set; }
            
        }
 }