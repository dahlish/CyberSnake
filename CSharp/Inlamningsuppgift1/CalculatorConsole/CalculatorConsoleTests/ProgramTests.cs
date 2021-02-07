using Microsoft.VisualStudio.TestTools.UnitTesting;
using CalculatorConsole;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace CalculatorConsole.Tests
{
    [TestClass()]
    public class ProgramTests
    {
        [TestMethod()]
        public void GetNumberTest()
        {
            string test1Num1 = "30";
            float test1Expected = 30;
            StringReader input = new StringReader(test1Num1);
            Console.SetIn(input);
            float test1Actual = Program.GetNumber();
            Assert.AreEqual(test1Expected, test1Actual);

            string test2Num1 = "-20";
            float test2Expected = -20;
            input = new StringReader(test2Num1);
            Console.SetIn(input);
            float test2Actual = Program.GetNumber();
            Assert.AreEqual(test2Expected, test2Actual);


            string test3Num1 = "MarCus";
            float test3Expected = 42;
            input = new StringReader(test3Num1);
            Console.SetIn(input);
            float test3Actual = Program.GetNumber();
            Assert.AreEqual(test3Expected, test3Actual);


            //Eftersom GetNumber() är recursive vid fel input så fastnade jag på hur jag unit testar något som är recursive. Tar jättegärna en förklaring i detta så jag vet
            //hur jag testar något som är recursive och samtidigt kräver input :)

            //string test4Num1 = "Arthas";
            //input = new StringReader(test4Num1);
            //Console.SetIn(input);
            //Assert.ThrowsException<FormatException>(() => Program.GetNumber());

            string test4Num1 = "42.1";
            float test4Expected = 42.1f;
            input = new StringReader(test4Num1);
            Console.SetIn(input);
            float test4Actual = Program.GetNumber();
            Assert.AreEqual(test4Expected, test4Actual);

            string test5Num1 = "42,1";
            float test5Expected = 42.1f;
            input = new StringReader(test5Num1);
            Console.SetIn(input);
            float test5Actual = Program.GetNumber();
            Assert.AreEqual(test5Expected, test5Actual);
        }

        [TestMethod()]
        public void ComputeTest()
        {
            float test1Num1 = 10.2f;
            float test1Num2 = 0.8f;
            string test1Op = "+";
            float test1Expected = 11f;
            float test1Actual = Program.Compute(test1Num1, test1Num2, test1Op);
            Assert.AreEqual(test1Expected, test1Actual);

            float test2Num1 = 91f;
            float test2Num2 = 100f;
            string test2Op = "%";
            Assert.ThrowsException<FormatException>(() => Program.Compute(test2Num1, test2Num2, test2Op));

            float test3Num1 = -12.3f;
            float test3Num2 = 0;
            string test3Op = "/";
            Assert.ThrowsException<DivideByZeroException>(() => Program.Compute(test3Num1, test3Num2, test3Op));

            float test4Num1 = -5f;
            float test4Num2 = -5f;
            string test4Op = "-";
            float test4Expected = 0f;
            float test4Actual = Program.Compute(test4Num1, test4Num2, test4Op);
            Assert.AreEqual(test4Expected, test4Actual);
        }

        [TestMethod()]
        public void ConvertTemperatureTest()
        {
            float test1Num1 = 32f;
            string test1Op1 = "F";
            float test1Expected = 0f;
            float test1Actual = Program.ConvertTemperature(test1Num1, test1Op1);
            Assert.AreEqual(test1Expected, test1Actual);

            float test2Num1 = 100f;
            string test2Op = "C";
            float test2Expected = 212f;
            float test2Actual = Program.ConvertTemperature(test2Num1, test2Op);
            Assert.AreEqual(test2Expected, test2Actual);

            float test3Num1 = 32.5f;
            string test3Op = "C";
            float test3Expected = 90.5f;
            float test3Actual = Program.ConvertTemperature(test3Num1, test3Op);
            Assert.AreEqual(test3Expected, test3Actual);

            float test4Num1 = 0f;
            string test4Op = "K";
            Assert.ThrowsException<FormatException>(() => Program.ConvertTemperature(test4Num1, test4Op));
        }

        [TestMethod()]
        public void CalculateBmiTest()
        {
            float test1Num1 = 100f;
            float test1Num2 = 180f;
            float test1Expected = 30.9f;
            float test1Actual = Program.CalculateBmi(test1Num1, test1Num2);
            Assert.AreEqual(test1Expected, test1Actual);

            float test2Num1 = 71.34f;
            float test2Num2 = 172.2f;
            float test2Expected = 24.1f;
            float test2Actual = Program.CalculateBmi(test2Num1, test2Num2);
            Assert.AreEqual(test2Expected, test2Actual);

            float test3Num1 = -0.02f;
            float test3Num2 = 30f;
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Program.CalculateBmi(test3Num1, test3Num2));

            float test4Num1 = 67.3f;
            float test4Num2 = -0.42f;
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Program.CalculateBmi(test4Num1, test4Num2));

            float test5Num1 = -32f;
            float test5Num2 = -132f;
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Program.CalculateBmi(test5Num1, test5Num2));

            float test6Num1 = 95f;
            float test6Num2 = 0f;
            Assert.ThrowsException<DivideByZeroException>(() => Program.CalculateBmi(test6Num1, test6Num2));
        }
    }
}