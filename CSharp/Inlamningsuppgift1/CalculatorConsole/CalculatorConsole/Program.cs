using System;
using System.Collections.Generic;

namespace CalculatorConsole
{
    public class Program
    {
        static string currentCalculation = "";

        public static void Main(string[] args)
        {
            List<string> calculations = new List<string>();
            bool keepRunning = true;
            do
            {
                Console.WriteLine("Här är dina alternativ:\nAddition: +\nSubtraktion: -\nDivision: /\nMulitplikation: *" +
                    "\nTemperaturomvandling: konvertera\nNewtons andra lag: newton\nBMI-uträkning: bmi\nLista alla uträkningar: lista\nAvsluta: quit");

                string menyVal = Console.ReadLine().ToLower();
                switch (menyVal)
                {
                    case "+":
                        float addNum1 = GetNumber("Mata in det första numret: ");
                        currentCalculation += "+ ";
                        float addNum2 = GetNumber("Mata in det andra numret: ");

                        float addResult = Compute(addNum1, addNum2, "+");
                        Console.WriteLine("Resultat: " + addResult);
                        currentCalculation += $"=> {addResult}";
                        calculations.Add(currentCalculation);
                        currentCalculation = "";
                        break;
                    case "-":
                        float subNum1 = GetNumber("Mata in det första numret: ");
                        currentCalculation += "- ";
                        float subNum2 = GetNumber("Mata in det andra numret: ");

                        float subResult = Compute(subNum1, subNum2, "-");
                        Console.WriteLine("Resultat: " + subResult);
                        currentCalculation += $"=> {subResult}";
                        calculations.Add(currentCalculation);
                        currentCalculation = "";
                        break;
                    case "/":
                        float divNum1 = GetNumber("Mata in det första numret: ");
                        currentCalculation += "/ ";
                        float divNum2 = GetNumber("Mata in det andra numret: ");
                        float divResult;
                        try
                        {
                            divResult = MathF.Round(Compute(divNum1, divNum2, "/"), 2);
                        }
                        catch (DivideByZeroException)
                        {
                            Console.WriteLine("Du försökte dela med 0! Vill du spränga världen eller?");
                            currentCalculation = "";
                            break;
                        }

                        Console.WriteLine("Resultatet är: " + divResult);
                        currentCalculation += $"=> {divResult}";
                        calculations.Add(currentCalculation);
                        currentCalculation = "";
                        break;
                    case "*":
                        float mulNum1 = GetNumber("Mata in det första numret: ");
                        currentCalculation += "* ";
                        float mulNum2 = GetNumber("Mata in det andra numret: ");

                        float mulResult = Compute(mulNum1, mulNum2, "*");
                        Console.WriteLine("Resultat: " + mulResult);
                        currentCalculation += $"=> {mulResult}";
                        calculations.Add(currentCalculation);
                        currentCalculation = "";
                        break;

                    case "konvertera":
                        Console.WriteLine("Vilken enhet vill du konvertera ifrån: C/F?");
                        string tempOp = Console.ReadLine().ToUpper();
                        float temp = GetNumber($"Ange temperatur i {tempOp}: ");
                        float convertResult = default;
                        string newType;

                        //Detta hade jag velat ha gjort redan i konverteringsmetoden, funderade på att använda en class Temperature
                        //istället men kände att det hade frångått för mycket från övriga lösningar vilket kändes opassande och ej enhetligt.
                        //Ett annat alternativ hade varit kanske att lagra den nya enheten i en variabel i klass-scopet som jag gjort med currentCalculation, men jag lät detta vara såhär nu.
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
                            currentCalculation = "";
                            break;
                        }

                        Console.WriteLine($"Resultat: {convertResult} {newType}");
                        currentCalculation += $"{tempOp} => {convertResult} {newType}";
                        calculations.Add(currentCalculation);
                        currentCalculation = "";
                        break;
                    case "newton":
                        float mass = GetNumber("Ange massa (kg): ");
                        currentCalculation += "* ";
                        float acceleration = GetNumber("Ange acceleration (m/s): ");

                        float newtonResult = Compute(mass, acceleration, "*");
                        Console.WriteLine($"Resultat: {newtonResult} N");
                        currentCalculation += $"=> {newtonResult} N";
                        calculations.Add(currentCalculation);
                        currentCalculation = "";
                        break;
                    case "bmi":
                        float weight = GetNumber("Ange vikt (kg): ");
                        currentCalculation += "kg & ";
                        float height = GetNumber("Ange längd (cm): ");
                        currentCalculation += "cm => ";
                        float bmiResult = default;
                        try
                        {
                            bmiResult = CalculateBmi(weight, height);
                        }
                        catch(DivideByZeroException)
                        {
                            Console.WriteLine("Din längd får inte vara 0.");
                            currentCalculation = "";
                            break;
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            Console.WriteLine("Din längd kan inte vara negativ.");
                            currentCalculation = "";
                            break;
                        }
                        Console.WriteLine($"Resultat: {bmiResult} BMI");
                        currentCalculation += $"{bmiResult} BMI";
                        calculations.Add(currentCalculation);
                        currentCalculation = "";
                        break;

                    case "lista":
                        ListAllCalculations(calculations);
                        break;
                    case "quit":
                        keepRunning = false;
                        break;
                    default:
                        Console.WriteLine("Okänt menyval. Försök igen.");
                        break;
                }

                Console.WriteLine();
            } while (keepRunning);
        }

        /// <summary>
        /// Asks the user to enter a numeric value and then returns it as a float. If the user enters a different
        /// value than a numeric value, the method will be executed again and a message will be shown in the console.
        /// The message parameter is optional and will be output to the console should it else than null or empty.
        /// </summary>
        /// <param name="message">Add an optional message to be shown to the console before the input is asked for.</param>
        /// <returns>A float with the input value.</returns>
        public static float GetNumber(string message = "")
        {
            float result = default;
            if (!string.IsNullOrEmpty(message))
            {
                Console.WriteLine(message);
            }
            string inputString = Console.ReadLine();
            inputString = inputString.Replace('.', ',');
            currentCalculation += $"{inputString} ";

            if (inputString.ToLower() == "marcus")
            {
                inputString = "42";
            }

            try
            {
                result = float.Parse(inputString);
            }
            catch (FormatException)
            {
                currentCalculation = currentCalculation.Replace($"{inputString} ", "");
                result = GetNumber("Du har inte matat in ett nummer. Vänligen försök igen.");
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
        public static float Compute(float num1, float num2, string op)
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
                        return num1 / num2;
                    }
                case "*":
                    return num1 * num2;
                default:
                    throw new FormatException("An invalid operator was used. Please use +, -, * or /");
            }
        }

        /// <summary>
        /// Converts temperature from Celsius to Fahrenheit and vice versa.
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

        /// <summary>
        /// Calculates the body mass index based on the input weight and length. Both parameters must be positive.
        /// </summary>
        /// <param name="weight">The weight in kilograms. Must be positive.</param>
        /// <param name="height">The length in cm. Must be positive.</param>
        /// <returns>The calculated BMI as a float.</returns>
        /// <exception cref="ArgumentOutOfRangeException">If any of the parameter values are negative, this exception is thrown.</exception>
        /// <exception cref="DivideByZeroException">If the height is 0, this exception is thrown.</exception>
        public static float CalculateBmi(float weight, float height)
        {
            float bmi = 0;

            if (height < 0 || weight < 0)
            {
                throw new ArgumentOutOfRangeException("The parameter values must be a positive number.");
            }

            height = height / 100;
            bmi = weight / MathF.Pow(height, 2);

            if (float.IsInfinity(bmi)) //Här upptäckte jag att dela en float på 0 resulterar i infinity istället för DivideByZeroException. Finns det ett bättre sätt att jobba runt detta eller är detta "rätt" sätt? :)
            {
                throw new DivideByZeroException("Can't divide by zero.");
            }
            
            return MathF.Round(bmi, 1);
        }
    }
}
