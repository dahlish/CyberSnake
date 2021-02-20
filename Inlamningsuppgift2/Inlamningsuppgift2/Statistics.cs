using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;

namespace Inlamningsuppgift2
{
    /// <summary>
    /// Is used to handle game stats during a game.
    /// </summary>
    public struct Statistics
    {
        private int score;
        private int snakeLength;
        private int highestScore;
        private float timePlayed;
        private Difficulty difficultyPlayed;

        public int Score { get => score; }
        public int SnakeLength { get => snakeLength; }
        public int HighestScore { get => highestScore; }
        public float TimePlayed { get => timePlayed; }
        public Difficulty DifficultyPlayed { get => difficultyPlayed; }

        /// <summary>
        /// Creates a new Statistics value type. With the end game score, snake length, the difficulty played and how long the game lasted.
        /// The highest score is saved to a file score.dat which is later accessed to read the highest score and compare it.
        /// Should a new highest score be achieved, the file will be overwritten with the new highest score.
        /// </summary>
        /// <param name="score">The game score.</param>
        /// <param name="snakeLength">The snake length.</param>
        /// <param name="difficultyPlayed">The difficulty played at.</param>
        /// <param name="timePlayed">How long the game lasted.</param>
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
        
        /// <summary>
        /// Gets the current highest score from the file score.dat. 
        /// </summary>
        /// <returns>The value in score.dat as int. If the file doesnt exist, it returns 0.</returns>
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


        /// <summary>
        /// ranslates the difficulty enum to a more readable string.
        /// </summary>
        /// <param name="difficultyPlayed">The difficulty you wish to translate.</param>
        /// <returns></returns>
        public string DifficultyToString(Difficulty difficultyPlayed)
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
