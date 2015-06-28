using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Xml;

namespace SheetYourself
{
    /// <summary>
    /// Provides methods for compiling multiple source images into a single compact sprite sheet.
    /// </summary>
    public class SheetBuilder
    {
        #region Properties

        /// <summary>
        /// Gets or sets a value indicating the number of empty pixels to add horizontally
        /// between images in a sprite sheet.
        /// </summary>
        public int HorizontalPadding { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the number of empty pixels to add vertically
        /// between images in a sprite sheet.
        /// </summary>
        public int VerticalPadding { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new SheetBuilder instance using the provided value for both
        /// horizontal and vertical padding.
        /// </summary>
        /// <param name="padding">The amount of padding to add both horizontally and
        /// vertically between images in a sprite sheet.</param>
        public SheetBuilder(int padding)
            : this(padding, padding)
        {
        }

        /// <summary>
        /// Creates a new SheetBuilder instance using the provided values for
        /// horizontal and vertical padding.
        /// </summary>
        /// <param name="horizontalPadding">The amount of padding to add horizontally
        /// between images in a sprite sheet.</param>
        /// <param name="verticalPadding">The amount of padding to add vertically
        /// between images in a sprite sheet.</param>
        public SheetBuilder(int horizontalPadding, int verticalPadding)
        {
            HorizontalPadding = horizontalPadding;
            VerticalPadding = verticalPadding;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Gets an <see cref="ImageInfo"/> instance for the image contained in the specified file.
        /// If the <paramref name="cropTransparency"/> parameter is true, then transparent pixels around
        /// all sides of the image are not included when determining the source area of the image.
        /// </summary>
        /// <param name="filename">The file name of the image to use.</param>
        /// <param name="cropTransparency">True if transparent pixels should be removed when determining
        /// the source area of the image, or false if not.</param>
        /// <returns>An <see cref="ImageInfo"/> instance for the provided image file.</returns>
        private ImageInfo GetImageInfo(string filename, bool cropTransparency)
        {
            Bitmap image = (Bitmap)Bitmap.FromFile(filename);
            if (image == null)
            {
                throw new ArgumentException(string.Format("Could not open image file '{0}'", filename), "filename");
            }

            // Lambda method to simplify the process of finding non-transparent pixels below
            Func<int, int, bool> isTransparent = (px, py) =>
                {
                    return (px < image.Width) && (py < image.Height) && (image.GetPixel(px, py).A == 0);
                };

            int x = 0;
            int y = 0;
            int width = image.Width + (2 * HorizontalPadding);
            int height = image.Height + (2 * VerticalPadding);

            if (cropTransparency)
            {
                // Finds the left-most column (X value) containing a non-transparent pixel
                #region Find left opaque pixel
                int column = 0;
                int row = 0;
                while ((column < image.Width) && isTransparent(column, row))
                {
                    while ((row < image.Height) && isTransparent(column, row))
                    {
                        ++row;
                    }

                    if (row == image.Height)
                    {
                        row = 0;
                        ++column;
                    }
                }
                x = column;
                #endregion

                // Finds the right-most column (X value) containing a non-transparent pixel
                #region Find right opaque pixel
                column = image.Width - 1;
                row = 0;
                while ((column > x) && isTransparent(column, row))
                {
                    while ((row < image.Height) && isTransparent(column, row))
                    {
                        ++row;
                    }

                    if (row == image.Height)
                    {
                        row = 0;
                        --column;
                    }
                }
                width = column - x + 1 + (2 * HorizontalPadding);
                #endregion

                // Finds the top-most row (Y value) containing a non-transparent pixel
                #region Find top opaque pixel
                column = 0;
                row = 0;
                while ((row < image.Height) && isTransparent(column, row))
                {
                    while ((column < image.Width) && isTransparent(column, row))
                    {
                        ++column;
                    }

                    if (column == image.Width)
                    {
                        column = 0;
                        ++row;
                    }
                }
                y = row;
                #endregion

                // Finds the bottom-most row (Y value) containing a non-transparent pixel
                #region Find bottom opaque pixel
                column = 0;
                row = image.Height - 1;
                while ((row > y) && isTransparent(column, row))
                {
                    while ((column < image.Width) && isTransparent(column, row))
                    {
                        ++column;
                    }

                    if (column == image.Width)
                    {
                        column = 0;
                        --row;
                    }
                }
                height = row - y + 1 + (2 * VerticalPadding);
                #endregion
            }

            return new ImageInfo() { FileName = filename, SourceArea = new Rectangle(x, y, width, height) };
        }

        /// <summary>
        /// Creates a sprite sheet image file and XML definition file using the list of <see cref="ImageInfo"/>
        /// instances provided.
        /// </summary>
        /// <param name="outputName">Name to use for the sprite sheet (this is not a file name).</param>
        /// <param name="sheetName">Name of the sprite sheet image file.</param>
        /// <param name="xmlName">Name of the sprite sheet XML definition file.</param>
        /// <param name="size">Size of the final output image used in the sprite sheet.</param>
        /// <param name="images">List of <see cref="ImageInfo"/> instances to use when building the sprite sheet.</param>
        private void WriteOutput(string outputName, string sheetName, string xmlName, Size size, List<ImageInfo> images)
        {
            Bitmap sheet = new Bitmap(size.Width, size.Height);
            Graphics graphics = Graphics.FromImage(sheet);

            XmlWriter writer = XmlWriter.Create(xmlName);
            writer.WriteStartDocument(true);
            writer.WriteStartElement("spritesheet");
            writer.WriteElementString("name", outputName);
            writer.WriteElementString("file", Path.GetFileName(sheetName));
            writer.WriteElementString("count", images.Count.ToString());
            writer.WriteElementString("width", size.Width.ToString());
            writer.WriteElementString("height", size.Height.ToString());
            writer.WriteStartElement("sprites");

            foreach (ImageInfo info in images)
            {
                Bitmap image = (Bitmap)Bitmap.FromFile(info.FileName);
                graphics.DrawImage(image, new Rectangle(info.Position, info.SourceArea.Size), info.SourceArea, GraphicsUnit.Pixel);

                string spriteName = Path.GetFileNameWithoutExtension(info.FileName).Replace(' ', '_');

                writer.WriteStartElement("sprite");
                writer.WriteElementString("name", spriteName);
                writer.WriteElementString("x", info.Position.X.ToString());
                writer.WriteElementString("y", info.Position.Y.ToString());
                writer.WriteElementString("width", info.SourceArea.Width.ToString());
                writer.WriteElementString("height", info.SourceArea.Height.ToString());
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.WriteEndDocument();

            sheet.Save(sheetName, ImageFormat.Png);
            graphics.Dispose();
            sheet.Dispose();

            writer.Close();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Uses all images found in the specified source folder to build a compacted sprite sheet.
        /// Images used maintain their original orientation, but may be cropped to remove transparency
        /// if the <paramref name="cropTransparency"/> parameter is true.
        /// </summary>
        /// <param name="sourceFolder">Folder containing images to use in the sprite sheet.
        /// All images in the folder will be used.</param>
        /// <param name="outputName">Name to use for the sprite sheet output image file and XML definition file.</param>
        /// <param name="cropTransparency">True if transparent pixels should be removed when determining
        /// the source area of the image, or false if not.</param>
        public void BuildSheet(string sourceFolder, string outputName, bool cropTransparency)
        {
            if (!Directory.Exists(sourceFolder))
            {
                throw new ArgumentException(string.Format("Source folder '{0}' does not exist", sourceFolder), "sourceFolder");
            }

            if (string.IsNullOrWhiteSpace(outputName))
            {
                throw new ArgumentNullException("outputName");
            }

            DirectoryInfo dirInfo = new DirectoryInfo(sourceFolder);
            FileInfo[] files = dirInfo.GetFiles("*.png");
            if ((files == null) || (files.Length == 0))
            {
                // Not finding image files means nothing can be built, but this should not
                // be treated as an exception
                return;
            }

            // Replace spaces in the name with underscores to help with compatibility
            // with other tools or across platforms.
            outputName = outputName.Replace(' ', '_');
            string sheetName = Path.Combine(sourceFolder, outputName + ".png");
            string xmlName = Path.Combine(sourceFolder, outputName + ".xml");
            if (File.Exists(sheetName))
            {
                File.Delete(sheetName);
            }
            if (File.Exists(xmlName))
            {
                File.Delete(xmlName);
            }

            // Calculate the total area of all images and find the widest.
            // Then create a list of images, sorted from smallest to largest.
            int totalArea = 0;
            int maxWidth = 0;
            List<ImageInfo> images = new List<ImageInfo>();
            foreach (FileInfo file in files)
            {
                if (file.FullName == sheetName)
                {
                    continue;
                }
                ImageInfo info = GetImageInfo(file.FullName, cropTransparency);
                images.Add(info);

                totalArea += info.SourceArea.Width * info.SourceArea.Height;
                maxWidth = (info.SourceArea.Width > maxWidth) ? info.SourceArea.Width : maxWidth;
            }
            images.Sort(new ImageInfoSizeComparer());
            images.Reverse();

            // Target width of the output sprite sheet is the larger of either the square root
            // of the total area of all images, rounded up, or the width of the widest image.
            int targetWidth = Math.Max((int)Math.Ceiling(Math.Sqrt(totalArea)), maxWidth);
            int remainingWidth = targetWidth;
            int nextX = 0;
            int nextY = 0;
            int maxHeight = 0;
            for (int i = 0; i < images.Count; ++i)
            {
                // If the next image being added is wider than the remaining allowed
                // width of the current row of images, start a new row
                if (remainingWidth - images[i].SourceArea.Width <= 0)
                {
                    remainingWidth = targetWidth;
                    nextX = 0;
                    nextY += maxHeight;
                    maxHeight = 0;
                }
                images[i].Position = new Point(nextX, nextY);
                remainingWidth -= images[i].SourceArea.Width;
                nextX += images[i].SourceArea.Width;
                if (images[i].SourceArea.Height > maxHeight)
                {
                    maxHeight = images[i].SourceArea.Height;
                }
            }
            Size size = new Size(targetWidth, nextY + maxHeight);
            WriteOutput(outputName, sheetName, xmlName, size, images);
        }

        #endregion
    }
}
