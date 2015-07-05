using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using SheetYourself;

namespace TestSheetYourself
{
    [TestClass]
    public class SpriteSizeComparerTest
    {
        [TestMethod]
        public void XAreaLessThanYArea()
        {
            Sprite x = new Sprite("X", new Bitmap(10, 20), 0, false);
            Sprite y = new Sprite("Y", new Bitmap(20, 30), 0, false);
            SpriteSizeComparer comparer = new SpriteSizeComparer();
            int expected = -1;
            int actual = comparer.Compare(x, y);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void XAreaGreaterThanYArea()
        {
            Sprite x = new Sprite("X", new Bitmap(20, 30), 0, false);
            Sprite y = new Sprite("Y", new Bitmap(10, 20), 0, false);
            SpriteSizeComparer comparer = new SpriteSizeComparer();
            int expected = 1;
            int actual = comparer.Compare(x, y);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void XAreaEqualToYAreaAndXLongestDimensionLessThanY()
        {
            Sprite x = new Sprite("X", new Bitmap(15, 20), 0, false);
            Sprite y = new Sprite("Y", new Bitmap(10, 30), 0, false);
            SpriteSizeComparer comparer = new SpriteSizeComparer();
            int expected = -1;
            int actual = comparer.Compare(x, y);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void XAreaEqualToYAreaAndXLongestDimensionGreaterThanY()
        {
            Sprite x = new Sprite("X", new Bitmap(10, 30), 0, false);
            Sprite y = new Sprite("Y", new Bitmap(15, 20), 0, false);
            SpriteSizeComparer comparer = new SpriteSizeComparer();
            int expected = 1;
            int actual = comparer.Compare(x, y);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void XAreaEqualToYAreaAndXLongestDimensionEqualToY()
        {
            Sprite x = new Sprite("X", new Bitmap(10, 20), 0, false);
            Sprite y = new Sprite("Y", new Bitmap(10, 20), 0, false);
            SpriteSizeComparer comparer = new SpriteSizeComparer();
            int expected = 0;
            int actual = comparer.Compare(x, y);
            Assert.AreEqual(expected, actual);
        }
    }
}
