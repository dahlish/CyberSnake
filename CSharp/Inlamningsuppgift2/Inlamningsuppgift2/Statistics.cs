using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;

namespace Inlamningsuppgift2
{
    public struct Statistics
    {
        private int score;
        private int snakeLength;
        private float timePlayed;
        private Difficulty difficultyPlayed;
        private int highestScore;

        public int Score { get => score; }
        public int SnakeLength { get => snakeLength; }
        public float TimePlayed { get => timePlayed; }
        public Difficulty DifficultyPlayed { get => difficultyPlayed; }
        public int HighestScore { get => highestScore; }

        public Statistics(int score, int snakeLength, Difficulty difficultyPlayed, float timePlayed)
        {
            this.score = score;
            this.snakeLength = snakeLength;
            this.difficultyPlayed = difficultyPlayed;
            this.timePlayed = timePlayed;

            if (File.Exists("score.dat"))
            {
                int.TryParse(File.ReadLines("score.dat").First(), out int scoreInt);
                highestScore = scoreInt;
            }
            else
            {
                File.Create("score.dat").Close();
                highestScore = score;
                File.WriteAllText("score.dat", score.ToString());
            }

            if (score > highestScore)
            {
                File.WriteAllText("score.dat", score.ToString());
                highestScore = score;
            }

        }

        public static int GetHighestScore()
        {
            if (File.Exists("score.dat"))
            {
                int.TryParse(File.ReadLines("score.dat").First(), out int scoreInt);

                return scoreInt;
            }
            else
            {
                return 0;
            }
        }

        public string DifficultyToString()
        {
            switch (difficultyPlayed)
            {
                case Difficulty.Easy:
                    return "Easy";
                case Difficulty.Medium:
                    return "Medium";
                case Difficulty.Hard:
                    return "Hard";
                case Difficulty.VeryHard:
                    return "Very Hard";

                default:
                    return "Unknown";
            }
        }
    }
}
