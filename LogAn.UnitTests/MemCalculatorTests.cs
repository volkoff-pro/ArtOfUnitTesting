using System;
using NUnit.Framework;

namespace LogAn.UnitTests
{
    [TestFixture]
    public class MemCalculatorTests
    {
        private static MemCalculator MakeCalc()
        {
            return new MemCalculator();
        }

        [Test]
        public void Sum_ByDefault_ReturnsZero()
        {
            MemCalculator calc = new MemCalculator();
            int lastSum = calc.Sum();
            Assert.AreEqual(0, lastSum);
        }
        
        [Test]
        public void Add_WhenCalled_ChangesSum()
        {
            MemCalculator calc = MakeCalc();

            calc.Add(1);
            int sum = calc.Sum();

            Assert.AreEqual(1, sum);
        }
    }
}