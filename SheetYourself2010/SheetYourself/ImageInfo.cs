﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SheetYourself
{
    /// <summary>
    /// Stores basic information about an image being used to build a sprite sheet.
    /// </summary>
    internal class ImageInfo
    {
        #region Properties

        /// <summary>
        /// Gets or sets the image file name.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the area of the image to use.
        /// </summary>
        public Rectangle SourceArea { get; set; }

        /// <summary>
        /// Gets or sets the position of the image on the sprite sheet.
        /// </summary>
        public Point Position { get; set; }

        #endregion
    }

    /// <summary>
    /// Compares the area of two <see cref="SheetYourself.ImageInfo"/> instances to one another.
    /// If both images have equal area, then they are compared based on largest dimension.
    /// </summary>
    internal class ImageInfoSizeComparer : IComparer<ImageInfo>
    {
        #region Public methods

        /// <summary>
        /// Compares two <see cref="SheetYourself.ImageInfo"/> instances. Image area is compared,
        /// with largest dimension as a tie-breaker.
        /// </summary>
        /// <param name="x">First image to use.</param>
        /// <param name="y">Second image to use.</param>
        /// <returns>-1 if the area of x is less than the area of y, or if the area of x and y
        /// are equal, but the largest dimension of x is less than the largest dimension of y.
        /// 0 if the both the area of x and y and the largest dimensions of x and y are equal.
        /// 1 if the area of x is greater than the area of y, or if the area of x and y are equal,
        /// but the largest dimension of x is greater than the largest dimension of y.</returns>
        public int Compare(ImageInfo x, ImageInfo y)
        {
            int xArea = x.SourceArea.Width * x.SourceArea.Height;
            int yArea = y.SourceArea.Width * y.SourceArea.Height;

            if (xArea < yArea)
            {
                return -1;
            }
            else if (xArea > yArea)
            {
                return 1;
            }
            else
            {
                int xMax = Math.Max(x.SourceArea.Width, x.SourceArea.Height);
                int yMax = Math.Max(y.SourceArea.Width, y.SourceArea.Height);

                return xMax.CompareTo(yMax);
            }
        }

        #endregion
    }
}
