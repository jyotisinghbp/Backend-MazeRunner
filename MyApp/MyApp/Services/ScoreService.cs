using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Services
{

    
        public class ScoreService : IScoreService
        {
            private int score = 0;

            public void UpdateScore(int points)
            {
                score += points;
        }

        public int GetScore() => score;

        public void ResetScore()
        {
            score = 0;
        }
        


    }

}
