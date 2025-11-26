using System;
using System.Collections.Generic;
using System.Text;


namespace MyApp.Models
{
    public class Enemy
    {
        public int X { get; set; }
        public int Y { get; set; }

        public void MoveRandomly(char[,] maze)
        {
            // Simple random movement logic
            Random rnd = new Random();
            int direction = rnd.Next(4);
            switch (direction)
            {
                case 0: if (maze[X - 1, Y] != '#') X--; break; // Up
                case 1: if (maze[X + 1, Y] != '#') X++; break; // Down
                case 2: if (maze[X, Y - 1] != '#') Y--; break; // Left
                case 3: if (maze[X, Y + 1] != '#') Y++; break; // Right
            }
        }
    }
}
