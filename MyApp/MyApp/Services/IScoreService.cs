using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Services
{
    public interface IScoreService
    {
        void UpdateScore(int points);
        int GetScore();
        void ResetScore(); 
    }


}
