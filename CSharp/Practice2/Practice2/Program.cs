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
            //IsPalindrome(22223);

            GenerateMultiplicationTable(10);
            //Play();
            Console.WriteLine(Compute(1, 0, "/"));

            bool keepTrying = true;

            do
            {
                Console.WriteLine("Mata in två heltal. Tal 1: ");
                string tal1Str = Console.ReadLine();
                Console.WriteLine("Mata in tal 2: ");
                string tal2Str = Console.ReadLine();
                int tal1, tal2;
                Console.WriteLine("Välj en operator: + - / * : ");
                string op = Console.ReadLine();
                try
                {
                    tal1 = int.Parse(tal1Str);
                    tal2 = int.Parse(tal2Str);
                    keepTrying = false;

                    Console.WriteLine(Compute(tal1, tal2, op));
                }
                catch (FormatException)
                {
                    Console.WriteLine("Du har matat in något annat än ett heltal, eller så har du valt en icke-existerande operatör. Försök igen.\n");
                }
            } while (keepTrying);


            try
            {
                Compute(1, 2, "lol");
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
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


        public static void GenerateMultiplicationTable(int n)
        {
            int offset = (n * n).ToString().Length;

            for (int i = 1; i <= n; i++)
            {
                for (int y = 1; y <= n; y++)
                {
                    int num = Multiply(y, i);

                    Console.Write(num);
                    for (int x = 0; x < offset - num.ToString().Length; x++)
                    {
                        Console.Write(" ");
                    }

                    if (y == n)
                    {
                        Console.Write("\n");
                    }
                }
            }
        }
        public static int Multiply(int n, int multiplication)
        {
            return n * multiplication;
        }


        /// <summary>
        /// Plays a rock paper scissors game. Input: 1 - Rock, 2 - Paper, 3 - Scissors
        /// </summary>
        /// <param name="choice"></param>
        /// <returns>Returns an integer based on the choice. 0 = loss, 1 = win, -1 = tie</returns>
        public static int RockPaperScissors(int choice)
        {
            Random rand = new Random();

            int aiChoice = rand.Next(1, 4);

            switch(choice)
            {
                case 1: //rock
                    if (aiChoice == 2)
                    {
                        return 0;
                    }
                    else if (aiChoice == 3)
                    {
                        return 1;
                    }
                    else
                    {
                        return -1;
                    }
                case 2: //paper
                    if (aiChoice == 1)
                    {
                        return 1;
                    }
                    else if (aiChoice == 3)
                    {
                        return 0;
                    }
                    else
                    {
                        return -1;
                    }
                case 3: //scissors
                    if (aiChoice == 1)
                    {
                        return 0;
                    }
                    else if (aiChoice == 2)
                    {
                        return 1;
                    }
                    else
                    {
                        return -1;
                    }

                default:
                    return -1;
            }
        }

        public static void Play()
        {
            bool keepRunning = true;

            do
            {
                Console.WriteLine("\nPlay Rock Paper Scissors:\n1 - Rock\n2 - Paper\n3 - Scissors\n\nExit: Press any other key\n\n");
                string inputStr = Console.ReadLine();
                int.TryParse(inputStr, out int input);
                if (input > 0 && input <= 3)
                {
                    int result = RockPaperScissors(input);

                    switch (result)
                    {
                        case 1:
                            Console.WriteLine("You won!");
                            break;
                        case 0:
                            Console.WriteLine("You lost!");
                            break;
                        case -1:
                            Console.WriteLine("It's a tie!");
                            break;
                        default:
                            Console.WriteLine("Can't evaluate.");
                            break;
                    }
                }
                else
                {
                    keepRunning = false;
                }

            } while (keepRunning);
        }

        static int Compute(int num1, int num2, string op)
        {
            switch (op)
            {
                case "+":
                    return num1 + num2;
                case "-":
                    return num1 - num2;
                case "*":
                    return num1 * num2;
                case "/":
                    try
                    {
                        return num1 / num2;
                    }
                    catch (DivideByZeroException)
                    {
                        Console.WriteLine("Can't divide by zero!");
                        return 0;
                    }
                default:
                    throw new FormatException("The method requires one of the following operators: +, -, *, /");
            }
        }


    }
}
