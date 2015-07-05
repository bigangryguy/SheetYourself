﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SheetYourself
{
    /// <summary>
    /// Compares the area of two <see cref="SheetYourself.Sprite"/> instances to one another.
    /// If both images have equal area, then they are compared based on largest dimension.
    /// </summary>
    public class SpriteSizeComparer : IComparer<Sprite>
    {
        #region Public methods

        /// <summary>
        /// Compares two <see cref="SheetYourself.Sprite"/> instances. Image area is compared,
        /// with largest dimension as a tie-breaker.
        /// </summary>
        /// <param name="x">First image to use.</param>
        /// <param name="y">Second image to use.</param>
        /// <returns>-1 if the area of x is less than the area of y, or if the area of x and y
        /// are equal, but the largest dimension of x is less than the largest dimension of y.
        /// 0 if the both the area of x and y and the largest dimensions of x and y are equal.
        /// 1 if the area of x is greater than the area of y, or if the area of x and y are equal,
        /// but the largest dimension of x is greater than the largest dimension of y.</returns>
        public int Compare(Sprite x, Sprite y)
        {
            int xArea = x.Width * x.Height;
            int yArea = y.Width * y.Height;

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
                int xMax = Math.Max(x.Width, x.Height);
                int yMax = Math.Max(y.Width, y.Height);

                return xMax.CompareTo(yMax);
            }
        }

        #endregion
    }
}