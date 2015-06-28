using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using SheetYourself;

namespace TestSheetYourself
{
    [TestClass]
    public class ImageInfoSizeComparerTest
    {
        [TestMethod]
        public void XAreaLessThanYArea()
        {
            ImageInfo x = new ImageInfo() { SourceArea = new Rectangle(0, 0, 10, 20) };
            ImageInfo y = new ImageInfo() { SourceArea = new Rectangle(0, 0, 20, 30) };
            ImageInfoSizeComparer comparer = new ImageInfoSizeComparer();
            int expected = -1;
            int actual = comparer.Compare(x, y);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void XAreaGreaterThanYArea()
        {
            ImageInfo x = new ImageInfo() { SourceArea = new Rectangle(0, 0, 20, 30) };
            ImageInfo y = new ImageInfo() { SourceArea = new Rectangle(0, 0, 10, 20) };
            ImageInfoSizeComparer comparer = new ImageInfoSizeComparer();
            int expected = 1;
            int actual = comparer.Compare(x, y);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void XAreaEqualToYAreaAndXLongestDimensionLessThanY()
        {
            ImageInfo x = new ImageInfo() { SourceArea = new Rectangle(0, 0, 15, 20) };
            ImageInfo y = new ImageInfo() { SourceArea = new Rectangle(0, 0, 10, 30) };
            ImageInfoSizeComparer comparer = new ImageInfoSizeComparer();
            int expected = -1;
            int actual = comparer.Compare(x, y);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void XAreaEqualToYAreaAndXLongestDimensionGreaterThanY()
        {
            ImageInfo x = new ImageInfo() { SourceArea = new Rectangle(0, 0, 10, 30) };
            ImageInfo y = new ImageInfo() { SourceArea = new Rectangle(0, 0, 15, 20) };
            ImageInfoSizeComparer comparer = new ImageInfoSizeComparer();
            int expected = 1;
            int actual = comparer.Compare(x, y);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void XAreaEqualToYAreaAndXLongestDimensionEqualToY()
        {
            ImageInfo x = new ImageInfo() { SourceArea = new Rectangle(0, 0, 10, 30) };
            ImageInfo y = new ImageInfo() { SourceArea = new Rectangle(0, 0, 10, 30) };
            ImageInfoSizeComparer comparer = new ImageInfoSizeComparer();
            int expected = 0;
            int actual = comparer.Compare(x, y);
            Assert.AreEqual(expected, actual);
        }
    }
}
