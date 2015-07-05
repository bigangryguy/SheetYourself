using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace SheetYourself
{
    public partial class MainForm : Form
    {
        #region Constructors

        /// <summary>
        /// Creates a new MainForm instance.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Ensures both a source image folder and output name are specified.
        /// </summary>
        /// <returns>True if both a source image folder and output name are specified,
        /// or false if not.</returns>
        private bool CanBuild()
        {
            return !string.IsNullOrWhiteSpace(uxFolderName.Text) && !string.IsNullOrWhiteSpace(uxOutputName.Text);
        }

        #endregion

        #region Event handlers

        private void uxBrowse_Click(object sender, EventArgs e)
        {
            if (uxFolderBrowser.ShowDialog() == DialogResult.OK)
            {
                uxFolderName.Text = uxFolderBrowser.SelectedPath;
                DirectoryInfo info = new DirectoryInfo(uxFolderName.Text);
                uxOutputName.Text = info.Name;
            }
        }

        private void uxBuild_Click(object sender, EventArgs e)
        {
            if (!CanBuild())
            {
                return;
            }

            SheetBuilder builder = new SheetBuilder((int)uxHPadding.Value, (int)uxVPadding.Value);
            builder.BuildSheet(uxFolderName.Text, uxOutputName.Text, uxCropTransparency.Checked, uxRoundUpPower2.Checked);
        }

        private void uxFolderName_TextChanged(object sender, EventArgs e)
        {
            uxBuild.Enabled = CanBuild();
        }

        private void uxOutputName_TextChanged(object sender, EventArgs e)
        {
            uxBuild.Enabled = CanBuild();
        }

        #endregion
    }
}
