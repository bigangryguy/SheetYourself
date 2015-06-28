using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SheetYourself
{
    /// <summary>
    /// Stores basic information about an image being used to build a sprite sheet.
    /// </summary>
    public class ImageInfo
    {
        #region Properties

        /// <summary>
        /// Gets or sets the image file name.
        /// </summary>
        private string _fileName;
        public string FileName
        {
            get { return _fileName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("FileName cannot be null, empty or all whitespace.");
                }
                _fileName = value;
            }
        }

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
}
