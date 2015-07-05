using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SheetYourself;

namespace TestSheetYourself
{
    [TestClass]
    public class MathHelperTest
    {
        [TestMethod]
        public void LeastPower2GreaterThanXWhenXIsLessThanZero()
        {
            int expected = 0;
            int actual = MathHelper.LeastPower2GreaterThanX(-1);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void LeastPower2GreaterThanXWhenXIsZero()
        {
            int expected = 0;
            int actual = MathHelper.LeastPower2GreaterThanX(0);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void LeastPower2GreaterThanXWhenXIsGreaterThanOne()
        {
            int expected = 1;
            int actual = MathHelper.LeastPower2GreaterThanX(1);
            Assert.AreEqual(expected, actual);

            expected = 2;
            actual = MathHelper.LeastPower2GreaterThanX(2);
            Assert.AreEqual(expected, actual);

            expected = 4;
            actual = MathHelper.LeastPower2GreaterThanX(3);
            Assert.AreEqual(expected, actual);

            expected = 8;
            actual = MathHelper.LeastPower2GreaterThanX(5);
            Assert.AreEqual(expected, actual);

            expected = 512;
            actual = MathHelper.LeastPower2GreaterThanX(384);
            Assert.AreEqual(expected, actual);

            expected = 4096;
            actual = MathHelper.LeastPower2GreaterThanX(4095);
            Assert.AreEqual(expected, actual);

            expected = 8192;
            actual = MathHelper.LeastPower2GreaterThanX(4097);
            Assert.AreEqual(expected, actual);
        }
    }
}
