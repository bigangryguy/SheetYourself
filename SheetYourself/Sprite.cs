using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SheetYourself
{
    /// <summary>
    /// Contains information for individual sprites on a sprite sheet.
    /// </summary>
    public class Sprite
    {
        #region Properties

        /// <summary>
        /// Gets the name of the sprite.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the <see cref="System.Drawing.Bitmap"/> image for the sprite.
        /// </summary>
        public Bitmap Image { get; private set; }

        /// <summary>
        /// Gets or sets the upper-left coordinate of the sprite on the sprite sheet.
        /// </summary>
        public Point SheetPosition { get; set; }

        /// <summary>
        /// Gets the sprite width.
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// Gets the sprite height.
        /// </summary>
        public int Height { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new <see cref="SheetYourself.Sprite"/> instance with the specified name, image and padding
        /// and crops image transparency if necessary.
        /// </summary>
        /// <param name="name">Name of the sprite.</param>
        /// <param name="image"><see cref="System.Drawing.Bitmap"/> instance to use for the sprite image.</param>
        /// <param name="padding">Number of pixels of padding to add to each side of the sprite.</param>
        /// <param name="cropTransparency">True if the image should be cropped to the minimum size possible without removing non-transparent
        /// pixels, or false if not.</param>
        public Sprite(string name, Bitmap image, int padding, bool cropTransparency)
            : this(name, image, padding, padding, cropTransparency)
        {
        }

        /// <summary>
        /// Creates a new <see cref="SheetYourself.Sprite"/> instance with the specified name, image and padding
        /// and crops image transparency if necessary.
        /// </summary>
        /// <param name="name">Name of the sprite.</param>
        /// <param name="image"><see cref="System.Drawing.Bitmap"/> instance to use for the sprite image.</param>
        /// <param name="horizontalPadding">Number of pixels of padding to add to both the left and right sides of the sprite.</param>
        /// <param name="verticalPadding">Number of pixels of padding to add to both the top and bottom sides of the sprite.</param>
        /// <param name="cropTransparency">True if the image should be cropped to the minimum size possible without removing non-transparent
        public Sprite(string name, Bitmap image, int horizontalPadding, int verticalPadding, bool cropTransparency)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("name");
            }

            if (image == null)
            {
                throw new ArgumentNullException("image");
            }

            if (horizontalPadding < 0)
            {
                throw new ArgumentOutOfRangeException("horizontalPadding", "Padding must be greater than or equal to zero.");
            }

            if (verticalPadding < 0)
            {
                throw new ArgumentOutOfRangeException("verticalPadding", "Padding must be greater than or equal to zero.");
            }

            Name = name;

            int left = 0;
            int right = image.Width;
            int top = 0;
            int bottom = image.Height;

            if (cropTransparency)
            {
                // Finds the left-most column (X value) containing a non-transparent pixel
                left = GetNonTransparentColumn(image, true);

                // Finds the right-most column (X value) containing a non-transparent pixel
                right = GetNonTransparentColumn(image, false);

                // Finds the top-most row (Y value) containing a non-transparent pixel
                top = GetNonTransparentRow(image, true);

                // Finds the bottom-most row (Y value) containing a non-transparent pixel
                bottom = GetNonTransparentRow(image, false);
            }
            int width = cropTransparency ? right - left + 1 : right - left;
            int height = cropTransparency ? bottom - top + 1 : bottom - top;

            Bitmap croppedImage = image.Clone(new Rectangle(left, top, width, height), image.PixelFormat);

            // Add padding to original image if necessary.
            if ((horizontalPadding > 0) || (verticalPadding > 0))
            {
                Bitmap paddedImage = new Bitmap(croppedImage.Width + (2 * horizontalPadding), croppedImage.Height + (2 * verticalPadding));
                
                Graphics graphics = Graphics.FromImage(paddedImage);
                graphics.Clear(Color.Transparent);
                graphics.DrawImageUnscaled(croppedImage, horizontalPadding, verticalPadding);

                Image = paddedImage;
            }
            else
            {
                Image = croppedImage;
            }

            Width = Image.Width;
            Height = Image.Height;
        }

        #endregion

        #region Private methods

        private static int GetNonTransparentColumn(Bitmap bitmap, bool leftToRight)
        {
            int column = leftToRight ? 0 : bitmap.Width - 1;
            int row = 0;
            int increment = leftToRight ? 1 : -1;
            int limit = leftToRight ? bitmap.Width : 0;
            while ((column != limit) && bitmap.IsTransparent(column, row))
            {
                while ((row < bitmap.Height) && bitmap.IsTransparent(column, row))
                {
                    ++row;
                }

                if (row == bitmap.Height)
                {
                    row = 0;
                    column += increment;
                }
            }

            return column;
        }

        private static int GetNonTransparentRow(Bitmap bitmap, bool topToBottom)
        {
            int column = 0;
            int row = topToBottom ? 0 : bitmap.Height - 1;
            int increment = topToBottom ? 1 : -1;
            int limit = topToBottom ? bitmap.Height : 0;
            while ((row != limit) && bitmap.IsTransparent(column, row))
            {
                while ((column < bitmap.Width) && bitmap.IsTransparent(column, row))
                {
                    ++column;
                }

                if (column == bitmap.Width)
                {
                    column = 0;
                    row += increment;
                }
            }
            return row;
        }

        #endregion
    }
}
