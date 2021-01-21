using System;
using System.Collections.Generic;

namespace Practice2
{
    public class Program
    {
        public static int correctAnswer = 0;
        public static bool gameActive = false;
        static void Main(string[] args)
        {
            IsPalindrome(22223);
        }

        public static bool IsPalindrome(string word)
        {
            string reversedWord = "";
            
            for (int i = word.Length - 1; i >= 0; i--)
            {
                reversedWord += word[i];
            }

            return (word == reversedWord);
        }

        public static bool IsPalindrome(int numbers)
        {
            List<int> numberList = new List<int>();
            int numberCounter = numbers;
            int reversedInt = 0;
            while (numberCounter > 0)
            {
                numberList.Add(numberCounter % 10);
                numberCounter /= 10;
            }

            foreach (var number in numberList)
            {
                reversedInt = 10 * reversedInt + number;
            }

            return (numbers == reversedInt);
            
        }

        public static string GuessingGame(int guess)
        {
            if (!gameActive)
            {
                gameActive = true;
                Random rand = new Random();
                correctAnswer = rand.Next(1, 100);
            }

            if (guess == correctAnswer)
            {
                return "Rätt!";
            }
            else if (guess > correctAnswer)
            {
                return "För högt!";
            }
            else
            {
                return "För lågt!";
            }
        }
    }
}
