using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Services
{
    public interface IGameService
    {
        void StartGame();
        void MovePlayer(string direction);
        void DisplayGameState();
    }


}
