using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SheetYourself
{
    /// <summary>
    /// Provides extension methods for Bitmap instances.
    /// </summary>
    public static class BitmapExtension
    {
        /// <summary>
        /// Returns a boolean value indicating whether or not the pixel at the specified coordinates
        /// of a bitmap is transparent.
        /// </summary>
        /// <param name="bitmap">A Bitmap instance.</param>
        /// <param name="x">The X-coordinate of the pixel to check.</param>
        /// <param name="y">The Y-coordinate of the pixel to check.</param>
        /// <returns>True if the pixel at the specified coordinates is transparent or false if not.</returns>
        public static bool IsTransparent(this Bitmap bitmap, int x, int y)
        {
            return bitmap.GetPixel(x, y).A == 0;
        }
    }
}
