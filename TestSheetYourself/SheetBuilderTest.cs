using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SheetYourself;

namespace TestSheetYourself
{
    [TestClass]
    public class SheetBuilderTest
    {
        [TestMethod]
        public void ConstructorSinglePadding()
        {
            int expected = 25;
            SheetBuilder builder = new SheetBuilder(expected);
            Assert.IsNotNull(builder);
            int horizontalActual = builder.HorizontalPadding;
            Assert.AreEqual(expected, horizontalActual);
            int verticalActual = builder.VerticalPadding;
            Assert.AreEqual(expected, verticalActual);
        }

        [TestMethod]
        public void ConstructorBothPadding()
        {
            int horizontalExpected = 25;
            int verticalExpected = 50;
            SheetBuilder builder = new SheetBuilder(horizontalExpected, verticalExpected);
            Assert.IsNotNull(builder);
            int horizontalActual = builder.HorizontalPadding;
            Assert.AreEqual(horizontalExpected, horizontalActual);
            int verticalActual = builder.VerticalPadding;
            Assert.AreEqual(verticalExpected, verticalActual);
        }
    }
}
