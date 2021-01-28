using System;

namespace Practice
{
    class Program
    {

        static void Main(string[] args)
        {
            WriteVariablesToConsole("Hello World", 10, 5.2f, true);

            for (int i = 1; i <= 10; i++)
            {
                Console.WriteLine("For: " + i);
            }

            Console.WriteLine(NumberCheck(20));

            Console.WriteLine(ReturnLargestNumber(2, 2));
            Console.WriteLine(ReturnLargestNumber(54, 55, 68));


            int[] array = { -1, 2, 4, -5, 5, -7, -2, 2, -3, -5, -6, 2, 110, 52, -10, -150, -20, -35, 10, 56, -35 };
            Console.WriteLine(ReturnSecondSmallestNumberInArray(array));

            Console.WriteLine(GetMostCommonChar("Hello Weeeoorld"));

            string[] strArray = { "Hello", "My", "Friend", "Christopher" };
            WriteArrayInConsoleWithFrame(strArray);

            int[] arrayToReverse = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            int[] reversedArrayCustom = ReverseCustom(arrayToReverse);
            int[] reversedArray = Reverse(arrayToReverse);


            int[] array1 = { 1, 2, 3, 5, 9};
            int[] array2 = { 4, 5, 6, 7, 8 };
            int[] evenElements = MergeArray(array1, array2);

            Console.WriteLine("Compute! Enter number 1: ");
            string num1 = Console.ReadLine();
            Console.WriteLine("Compute! Enter number 2: ");
            string num2 = Console.ReadLine();
            Console.WriteLine("Compute! Enter operator (+, -, /, *): ");
            string op = Console.ReadLine();
            Console.WriteLine("Result: " + Compute(int.Parse(num1), int.Parse(num2), op));


        }

        static void WriteVariablesToConsole(string str, int num, float floatNum, bool boolean)
        {
            Console.WriteLine($"{str}, {num}, {floatNum}, {boolean}");
        }

        static string NumberCheck(int num)
        {
            if (num > 100)
            {
                return "Stort";
            }
            else if (num > 0)
            {
                return "Litet";
            }
            else
            {
                return "Negativt";
            }
        }

        static int ReturnLargestNumber(int num1, int num2)
        {
            return (num1 > num2) ? num1 : num2;
        }
        static int ReturnLargestNumber(int num1, int num2, int num3)
        {
            return (num1 > num2 && num1 > num3) ? num1 : (num2 > num3) ? num2 : num3;
        }
        static int ReturnSecondSmallestNumberInArray(int[] array)
        {
            int smallestNumber = 0;
            int secondSmallestNumber = 0;

            for (int i = 0; i < array.Length; i++)
            {

                if (smallestNumber > array[i])
                {
                    smallestNumber = array[i];
                }
            }

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] > smallestNumber)
                {
                    if (array[i] < secondSmallestNumber)
                    {
                        secondSmallestNumber = array[i];
                    }
                }

            }

            return secondSmallestNumber;
        }

        static char GetMostCommonChar(string str)
        {
            int longestCount = 0;
            char result = '0';
            for (int i = 0; i < str.Length; i++)
            {
                int count = 0;

                for (int y = 0; y < str.Length; y++)
                {
                    if (str[i].ToString().ToLower() == str[y].ToString().ToLower())
                    {
                        count++;
                    }
                }

                if (count > longestCount)
                {
                    longestCount = count;
                    result = str[i];
                }
            }
            return result;   
        }

        static void WriteArrayInConsoleWithFrame(string[] array)
        {
            long longestWord = default;
            int offset = 3;

            for (int i = 0; i < array.Length; i++)
            {
                if (longestWord < array[i].Length)
                {
                    longestWord = array[i].Length;
                }
            }

            for (int i = 0; i < array.Length; i++)
            {
                if (i == 0)
                {
                    for (int y = 0; y <= longestWord + offset; y++)
                    {
                        Console.Write("*");
                    }
                }

                Console.Write("\n* ");
                Console.Write(array[i]);

                for (int y = 0; y <= longestWord - array[i].Length; y++)
                {
                    Console.Write(" ");

                    if (y == longestWord - array[i].Length)
                    {
                        Console.Write("*\n");
                    }
                }

                if (i == array.Length - 1)
                {
                    for (int y = 0; y <= longestWord + offset; y++)
                    {
                        Console.Write("*");
                    }
                }
                
            }
        }

        /// <summary>
        /// Reverses an array.
        /// </summary>
        /// <param name="array"></param>
        /// <returns>A new array that is reversed.</returns>
        static int[] ReverseCustom(int[] array)
        {
            int[] result = new int[array.Length];
            int arrayCounter = 0;

            for(int i = array.Length - 1; i >= 0; i--)
            {
                result[arrayCounter] = array[i];

                arrayCounter++;
            }

            return result;
        }

        static int[] Reverse(int[] array)
        {
            Array.Reverse(array);
            return array;
        }

        ///<summary>
        ///Merges two arrays and places every other element between each other
        ///</summary> 
        static int[] MergeArray(int[] arr1, int[] arr2) //Merge array and place every other element from each array
        {
            int length = arr1.Length + arr2.Length;
            int[] result = new int[length];

            for(int i = 0; i < result.Length; i++)
            {
                if (i % 2 == 0)
                {
                    result[i] = arr1[i / 2];
                }
                else
                {
                    result[i] = arr2[i / 2];
                }
            }

            return result;
        }

        static int Compute(int num1, int num2, string op)
        {
            switch(op)
            {
                case "+":
                    return num1 + num2;
                case "-":
                    return num1 - num2;
                case "*":
                    return num1 * num2;
                case "/":
                    if (num2 == 0)
                    {
                        Console.WriteLine("Can't divide by zero!");
                        return 0;
                    }
                    else
                    {
                        return num1 / num2;
                    }
                default:
                    return num1 + num2;
            }
        }
    }
}
