using Microsoft.VisualStudio.TestTools.UnitTesting;
using Practice3;
using System;
using System.Collections.Generic;
using System.Text;

namespace Practice3.Tests
{
    [TestClass()]
    public class RectangleTests
    {
        [TestMethod()]
        public void GetAreaTest()
        {
            Rectangle rect1 = new Rectangle(10, 15);
            float rect1Expected = 150f;
            float rect1Actual = rect1.GetArea();
            Assert.AreEqual(rect1Expected, rect1Actual);

            Rectangle rect2 = new Rectangle(10.5f, 10.2f);
            float rect2Expected = 107.1f;
            float rect2Actual = rect2.GetArea();
            Assert.AreEqual(rect2Expected, rect2Actual);

            Rectangle kvadratTest1 = new Rectangle(10);
            float kvadratTest1Expected = 100;
            float kvadratTest1Actual = kvadratTest1.GetArea();
            Assert.AreEqual(kvadratTest1Expected, kvadratTest1Actual);
        }

        [TestMethod()]
        public void GetCircumferenceTest()
        {
            Rectangle rect1 = new Rectangle(10, 15);
            float rect1Expected = 50;
            float rect1Actual = rect1.GetCircumference();
            Assert.AreEqual(rect1Expected, rect1Actual);

            Rectangle rect2 = new Rectangle(10.5f, 10.2f);
            float rect2Expected = 41.4f;
            float rect2Actual = rect2.GetCircumference();
            Assert.AreEqual(rect2Expected, rect2Actual);

            Rectangle kvadratTest1 = new Rectangle(10);
            float kvadratTest1Expected = 40;
            float kvadratTest1Actual = kvadratTest1.GetCircumference();
            Assert.AreEqual(kvadratTest1Expected, kvadratTest1Actual);
        }
    }
}