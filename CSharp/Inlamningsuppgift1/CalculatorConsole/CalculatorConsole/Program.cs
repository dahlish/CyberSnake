using System;
using System.Collections.Generic;

namespace CalculatorConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            List<string> calculations = new List<string>();
            bool keepRunning = true;

            do
            {
                Console.WriteLine("Här är dina alternativ:\nAddition: +\nSubtraktion: -\nDivision: /\nMulitplikation: *" +
                    "\nTemperaturomvandling: konvertera\nLista alla uträkningar: lista");

                string menyVal = Console.ReadLine().ToLower();
                switch (menyVal)
                {
                    case "+":
                        int addNum1 = GetNumber("Mata in det första numret: ");
                        int addNum2 = GetNumber("Mata in det andra numret: ");

                        float addResult = Compute(addNum1, addNum2, "+");
                        Console.WriteLine("Resultat: " + addResult);
                        calculations.Add($"{addNum1} + {addNum2} => {addResult}");
                        break;
                    case "-":
                        int subNum1 = GetNumber("Mata in det första numret: ");
                        int subNum2 = GetNumber("Mata in det andra numret: ");

                        float subResult = Compute(subNum1, subNum2, "-");
                        Console.WriteLine("Resultat: " + subResult);
                        calculations.Add($"{subNum1} - {subNum2} => {subResult}");
                        break;
                    case "/":
                        int divNum1 = GetNumber("Mata in det första numret: ");
                        int divNum2 = GetNumber("Mata in det andra numret: ");
                        float divResult;
                        try
                        {
                            divResult = MathF.Round(Compute(divNum1, divNum2, "/"), 2);
                        }
                        catch (DivideByZeroException)
                        {
                            Console.WriteLine("Du försökte dela med 0! Vill du spränga världen eller?");
                            break;
                        }

                        Console.WriteLine("Resultatet är: " + divResult);
                        calculations.Add($"{divNum1} / {divNum2} => {divResult}");
                        break;
                    case "*":
                        int mulNum1 = GetNumber("Mata in det första numret: ");
                        int mulNum2 = GetNumber("Mata in det andra numret: ");

                        float mulResult = Compute(mulNum1, mulNum2, "*");
                        Console.WriteLine("Resultat: " + mulResult);
                        calculations.Add($"{mulNum1} * {mulNum2} => {mulResult}");
                        break;

                    case "konvertera":
                        Console.WriteLine("Vilken enhet vill du konvertera ifrån: C/F?");
                        string tempOp = Console.ReadLine().ToUpper();
                        float temp = GetFloat($"Ange temperatur i {tempOp}: ");
                        float convertResult = default;
                        string newType;
                        //Detta hade jag velat ha gjort redan i konverteringsmetoden, funderade på att använda en class Temperature
                        //istället men kände att det hade frångått för mycket från övriga lösningar vilket kändes opassande och ej enhetligt.
                        //Ett annat alternativ hade varit kanske att lagra den nya enheten i en variabel i klass-scopet men det känns
                        //som bad practice i och med att jag kanske skulle vilja använda denna metod i ett annat program någon gång.
                        if (tempOp == "C")
                        {
                            newType = "F";
                        }
                        else
                        {
                            newType = "C";
                        }
                        try
                        {
                            convertResult = MathF.Round(ConvertTemperature(temp, tempOp), 1);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Du kan inte konvertera från detta.");
                        }

                        Console.WriteLine($"Resultat: {convertResult} {newType}");
                        calculations.Add($"{temp} {tempOp} => {convertResult} {newType}");
                        break;
                    case "lista":
                        ListAllCalculations(calculations);
                        break;
                    default:
                        Console.WriteLine("Okänt menyval. Försök igen.");
                        break;
                }

                Console.WriteLine("\n");
            } while (keepRunning);
        }

        /// <summary>
        /// Asks the user to enter a number value and then returns it as an int. If the user enters a different
        /// value than a number, the method will be executed again and a message will be shown in the console.
        /// The message parameter is optional and will be output to the console should it else than null or empty.
        /// </summary>
        /// <param name="message">Add an optional message to be shown to the console before the input is asked for.</param>
        /// <returns>An int with the input value.</returns>
        public static int GetNumber(string message = "")
        {
            int result = default;
            if (!string.IsNullOrEmpty(message))
            {
                Console.WriteLine(message);
            }
            string inputString = Console.ReadLine();

            if (inputString.ToLower() == "marcus")
            {
                inputString = "42";
            }

            try
            {
                result = int.Parse(inputString);
            }
            catch (FormatException)
            {
                Console.WriteLine("Du har inte matat in ett nummer. Vänligen försök igen.");
                GetNumber();
            }

            return result;
        }

        /// <summary>
        /// Asks the user to enter a floating point value and then returns it as a float. If the user enters a different
        /// value than a number, the method will be executed again and a message will be shown in the console.
        /// The message parameter is optional and will be output to the console should it else than null or empty.
        /// </summary>
        /// <param name="message">Add an optional message to be shown to the console before the input is asked for.</param>
        /// <returns>A float with the input value.</returns>
        public static float GetFloat(string message = "")
        {
            float result = default;

            if (!string.IsNullOrEmpty(message))
            {
                Console.WriteLine(message);
            }

            string inputString = Console.ReadLine();

            if (inputString.ToLower() == "marcus")
            {
                inputString = "42";
            }

            try
            {
                inputString = inputString.Replace('.', ',');
                result = float.Parse(inputString);
            }
            catch (FormatException)
            {
                Console.WriteLine("Du har inte matat in ett nummer. Vänligen försök igen.");
                GetFloat();
            }

            return result;
        }

        /// <summary>
        /// Outputs the contents of the parameter List into the console window.
        /// </summary>
        /// <param name="calculations">The list you want to output to the console.</param>
        public static void ListAllCalculations(List<string> calculations)
        {
            foreach(var calculation in calculations)
            {
                Console.WriteLine(calculation);
            }
        }

        /// <summary>
        /// Calculates simple mathematics such as addition, subtraction, division and multiplication of two numbers and one operator.
        /// </summary>
        /// <param name="num1">The first number to calculate.</param>
        /// <param name="num2">The seocnd number to calculate.</param>
        /// <param name="op">The operator to use. Valid operators: +, -, /, *</param>
        /// <exception cref="DivideByZeroException">If an attempt to divide by zero is made, this exception will be thrown.</exception>
        /// <exception cref="FormatException">If an unknown operator is used, this exception is thrown.</exception>
        /// <returns></returns>
        public static float Compute(int num1, int num2, string op)
        {
            switch (op)
            {
                case "+":
                    return num1 + num2;
                case "-":
                    return num1 - num2;
                case "/":
                    if (num2 == 0)
                    {
                        throw new DivideByZeroException("Can't divide by zero.");
                    }
                    else
                    {
                        return (float)num1 / (float)num2;
                    }
                case "*":
                    return num1 * num2;

                default:
                    throw new FormatException("An invalid operator was used. Please use +, -, * or /");
            }
        }

        /// <summary>
        /// Converts temperature from celsius to fahrenheit and vice versa.
        /// </summary>
        /// <param name="temperature">The temperature in the old unit type.</param>
        /// <param name="convertFrom">The unit type you are converting from.</param>
        /// <returns>A new float containing the converted temperature.</returns>
        /// <exception cref="FormatException">If a unit other than C or F is sent in the convertFrom parameter, this exception is thrown.</exception>
        public static float ConvertTemperature(float temperature, string convertFrom)
        {
            switch (convertFrom.ToUpper())
            {
                case "C":
                    return (temperature * 9) / 5 + 32;
                case "F":
                    return (temperature - 32) * 5 / 9;
                default:
                    throw new FormatException("You have to convert from either C or F.");
            }
        }
    }
}
