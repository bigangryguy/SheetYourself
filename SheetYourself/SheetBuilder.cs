using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Xml;
using System.Threading.Tasks;

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
        /// Creates a sprite sheet image file and XML definition file using the list of <see cref="ImageInfo"/>
        /// instances provided.
        /// </summary>
        /// <param name="outputName">Name to use for the sprite sheet (this is not a file name).</param>
        /// <param name="sheetName">Name of the sprite sheet image file.</param>
        /// <param name="xmlName">Name of the sprite sheet XML definition file.</param>
        /// <param name="size">Size of the final output image used in the sprite sheet.</param>
        /// <param name="sprites">List of <see cref="ImageInfo"/> instances to use when building the sprite sheet.</param>
        private void WriteOutput(string outputName, string sheetName, string xmlName, Size size, List<Sprite> sprites)
        {
            Bitmap sheet = new Bitmap(size.Width, size.Height);
            Graphics graphics = Graphics.FromImage(sheet);

            XmlWriter writer = XmlWriter.Create(xmlName);
            writer.WriteStartDocument(true);
            writer.WriteStartElement("spritesheet");
            writer.WriteElementString("name", outputName);
            writer.WriteElementString("file", Path.GetFileName(sheetName));
            writer.WriteElementString("count", sprites.Count.ToString());
            writer.WriteElementString("width", size.Width.ToString());
            writer.WriteElementString("height", size.Height.ToString());
            writer.WriteStartElement("sprites");

            foreach (Sprite sprite in sprites)
            {
                Bitmap image = sprite.Image;
                graphics.DrawImage(image, new Rectangle(sprite.SheetPosition.X, sprite.SheetPosition.Y, sprite.Width, sprite.Height));

                writer.WriteStartElement("sprite");
                writer.WriteElementString("name", sprite.Name);
                writer.WriteElementString("x", sprite.SheetPosition.X.ToString());
                writer.WriteElementString("y", sprite.SheetPosition.Y.ToString());
                writer.WriteElementString("width", sprite.Width.ToString());
                writer.WriteElementString("height", sprite.Height.ToString());
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
        public void BuildSheet(string sourceFolder, string outputName, bool cropTransparency, bool roundUpPower2Size)
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
            List<Sprite> sprites = new List<Sprite>();
            foreach (FileInfo file in files)
            {
                if (file.FullName != sheetName)
                {
                    Bitmap image = new Bitmap(file.FullName);
                    Sprite sprite = new Sprite(Path.GetFileNameWithoutExtension(file.FullName).Replace(' ', '_'), image, HorizontalPadding, VerticalPadding, cropTransparency);
                    sprites.Add(sprite);

                    totalArea += sprite.Width * sprite.Height;
                    maxWidth = (sprite.Width > maxWidth) ? sprite.Width : maxWidth;
                }
            }
            sprites.Sort(new SpriteSizeComparer());
            sprites.Reverse();

            // Target width of the output sprite sheet is the larger of either the square root
            // of the total area of all images, rounded up, or the width of the widest image.
            int targetWidth = Math.Max((int)Math.Ceiling(Math.Sqrt(totalArea)), maxWidth);
            if (roundUpPower2Size)
            {
                targetWidth = MathHelper.LeastPower2GreaterThanX(targetWidth);
            }
            int remainingWidth = targetWidth;
            int nextX = 0;
            int nextY = 0;
            int maxHeightInRow = 0;
            for (int i = 0; i < sprites.Count; ++i)
            {
                // If the next image being added is wider than the remaining allowed
                // width of the current row of images, start a new row
                if (remainingWidth - sprites[i].Width <= 0)
                {
                    remainingWidth = targetWidth;
                    nextX = 0;
                    nextY += maxHeightInRow;
                    maxHeightInRow = 0;
                }
                sprites[i].SheetPosition = new Point(nextX, nextY);
                remainingWidth -= sprites[i].Width;
                nextX += sprites[i].Width;
                if (sprites[i].Height > maxHeightInRow)
                {
                    maxHeightInRow = sprites[i].Height;
                }
            }
            int targetHeight = nextY + maxHeightInRow;
            if (roundUpPower2Size)
            {
                targetHeight = MathHelper.LeastPower2GreaterThanX(targetHeight);
            }
            Size size = new Size(targetWidth, targetHeight);
            WriteOutput(outputName, sheetName, xmlName, size, sprites);
        }

        #endregion
    }
}
