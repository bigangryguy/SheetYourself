using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SheetYourself;
using System.Drawing;

namespace TestSheetYourself
{
    [TestClass]
    public class ImageInfoTest
    {
        [TestMethod]
        public void FileNameNormalString()
        {
            string expected = @"C:\Temp\Example.png";
            ImageInfo info = new ImageInfo() { FileName = expected };
            Assert.IsNotNull(info);
            string actual = info.FileName;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FileNameCannotBeNull()
        {
            try
            {
                ImageInfo info = new ImageInfo() { FileName = null };
            }
            catch (ArgumentNullException)
            {
                return;
            }

            Assert.Fail("ArgumentNullException was expected but not thrown.");
        }

        [TestMethod]
        public void FileNameCannotBeEmpty()
        {
            try
            {
                ImageInfo info = new ImageInfo() { FileName = string.Empty };
            }
            catch (ArgumentNullException)
            {
                return;
            }

            Assert.Fail("ArgumentNullException was expected but not thrown.");
        }

        [TestMethod]
        public void FileNameCannotBeAllWhitespace()
        {
            try
            {
                ImageInfo info = new ImageInfo() { FileName = "    " };
            }
            catch (ArgumentNullException)
            {
                return;
            }

            Assert.Fail("ArgumentNullException was expected but not thrown.");
        }

        [TestMethod]
        public void SourceArea()
        {
            Rectangle expected = new Rectangle(0, 0, 640, 480);
            ImageInfo info = new ImageInfo() { SourceArea = expected };
            Assert.IsNotNull(info);
            Rectangle actual = info.SourceArea;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Position()
        {
            Point expected = new Point(24, 48);
            ImageInfo info = new ImageInfo() { Position = expected };
            Assert.IsNotNull(info);
            Point actual = info.Position;
            Assert.AreEqual(expected, actual);
        }
    }
}
