using Microsoft.VisualStudio.TestTools.UnitTesting;
using Practice2;
using System;
using System.Collections.Generic;
using System.Text;

namespace Practice2.Tests
{
    [TestClass()]
    public class ProgramTests
    {
        #region PalindromeTests
        [TestMethod()]
        public void IsPalindromeTest()
        {
            string word = "tillit";
            bool expected = true;

            bool actual = Program.IsPalindrome(word);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsPalindromeTest2()
        {
            string word = "is";
            bool expected = false;

            bool actual = Program.IsPalindrome(word);

            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void IsPalindromeTest3()
        {
            string word = "ö";
            bool expected = true;

            bool actual = Program.IsPalindrome(word);

            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void IsPalindromeTest4()
        {
            string word = "kajak";
            bool expected = true;

            bool actual = Program.IsPalindrome(word);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsPalindromeTest5()
        {
            string word = "apa";
            bool expected = true;

            bool actual = Program.IsPalindrome(word);

            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void IsPalindromeTest6()
        {
            string word = "monster";
            bool expected = false;

            bool actual = Program.IsPalindrome(word);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsPalindromeTest7()
        {
            int number = 22222;
            bool expected = true;

            bool actual = Program.IsPalindrome(number);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsPalindromeTest8()
        {
            int number = 22322;
            bool expected = true;

            bool actual = Program.IsPalindrome(number);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsPalindromeTest9()
        {
            int number = 22323;
            bool expected = false;

            bool actual = Program.IsPalindrome(number);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsPalindromeTest10()
        {
            int number = 62616367;
            bool expected = false;

            bool actual = Program.IsPalindrome(number);

            Assert.AreEqual(expected, actual);
        }
        #endregion

        #region GuessingGameTests
        [TestMethod()]
        public void GuessingGameTest()
        {
            Random rand = new Random();
            Program.GuessingGame(0);
            int guess = rand.Next(1, 100);
            string expected = "Rätt!";
            if (guess == Program.correctAnswer)
            {
                expected = "Rätt!";
            }
            else if (guess > Program.correctAnswer)
            {
                expected = "För högt!";
            }
            else
            {
                expected = "För lågt!";
            }

            string actual = Program.GuessingGame(guess);

            Assert.AreEqual(expected, actual);
        }
        #endregion


    }

}