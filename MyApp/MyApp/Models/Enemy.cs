using System;
using System.Collections.Generic;
using System.Text;


namespace MyApp.Models
{
    public class Enemy
    {
        public int X { get; set; }
        public int Y { get; set; }

       
        
    public void MoveRandomly(char[,] maze, List<Coin> coins)
    {
        var directions = new List<(int dx, int dy)> { (-1,0), (1,0), (0,-1), (0,1) };
        var rand = new Random();
        foreach (var dir in directions.OrderBy(x => rand.Next()))
        {
            int newX = X + dir.dx;
            int newY = Y + dir.dy;
            if (maze[newX, newY] != '#' && !coins.Any(c => c.X == newX && c.Y == newY && !c.Collected))
            {
                X = newX;
                Y = newY;
                break;
            }
        }
    }


    }
}
