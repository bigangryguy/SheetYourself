namespace SheetYourself
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.uxFolderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.uxFolderName = new System.Windows.Forms.TextBox();
            this.uxImageFolderLabel = new System.Windows.Forms.Label();
            this.uxBrowse = new System.Windows.Forms.Button();
            this.uxBuild = new System.Windows.Forms.Button();
            this.uxOutputNameLabel = new System.Windows.Forms.Label();
            this.uxOutputName = new System.Windows.Forms.TextBox();
            this.uxCropTransparency = new System.Windows.Forms.CheckBox();
            this.uxHPadding = new System.Windows.Forms.NumericUpDown();
            this.uxHPaddingLabel = new System.Windows.Forms.Label();
            this.uxVPaddingLabel = new System.Windows.Forms.Label();
            this.uxVPadding = new System.Windows.Forms.NumericUpDown();
            this.uxRoundUpPower2 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.uxHPadding)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uxVPadding)).BeginInit();
            this.SuspendLayout();
            // 
            // uxFolderName
            // 
            this.uxFolderName.Location = new System.Drawing.Point(86, 6);
            this.uxFolderName.Name = "uxFolderName";
            this.uxFolderName.Size = new System.Drawing.Size(320, 20);
            this.uxFolderName.TabIndex = 0;
            this.uxFolderName.TextChanged += new System.EventHandler(this.uxFolderName_TextChanged);
            // 
            // uxImageFolderLabel
            // 
            this.uxImageFolderLabel.AutoSize = true;
            this.uxImageFolderLabel.Location = new System.Drawing.Point(12, 9);
            this.uxImageFolderLabel.Name = "uxImageFolderLabel";
            this.uxImageFolderLabel.Size = new System.Drawing.Size(68, 13);
            this.uxImageFolderLabel.TabIndex = 1;
            this.uxImageFolderLabel.Text = "Image Folder";
            // 
            // uxBrowse
            // 
            this.uxBrowse.Location = new System.Drawing.Point(412, 4);
            this.uxBrowse.Name = "uxBrowse";
            this.uxBrowse.Size = new System.Drawing.Size(35, 23);
            this.uxBrowse.TabIndex = 2;
            this.uxBrowse.TabStop = false;
            this.uxBrowse.Text = "...";
            this.uxBrowse.UseVisualStyleBackColor = true;
            this.uxBrowse.Click += new System.EventHandler(this.uxBrowse_Click);
            // 
            // uxBuild
            // 
            this.uxBuild.Enabled = false;
            this.uxBuild.Location = new System.Drawing.Point(332, 110);
            this.uxBuild.Name = "uxBuild";
            this.uxBuild.Size = new System.Drawing.Size(115, 23);
            this.uxBuild.TabIndex = 0;
            this.uxBuild.Text = "Build Sprite Sheet";
            this.uxBuild.UseVisualStyleBackColor = true;
            this.uxBuild.Click += new System.EventHandler(this.uxBuild_Click);
            // 
            // uxOutputNameLabel
            // 
            this.uxOutputNameLabel.AutoSize = true;
            this.uxOutputNameLabel.Location = new System.Drawing.Point(12, 35);
            this.uxOutputNameLabel.Name = "uxOutputNameLabel";
            this.uxOutputNameLabel.Size = new System.Drawing.Size(70, 13);
            this.uxOutputNameLabel.TabIndex = 4;
            this.uxOutputNameLabel.Text = "Output Name";
            // 
            // uxOutputName
            // 
            this.uxOutputName.Location = new System.Drawing.Point(86, 32);
            this.uxOutputName.Name = "uxOutputName";
            this.uxOutputName.Size = new System.Drawing.Size(320, 20);
            this.uxOutputName.TabIndex = 0;
            this.uxOutputName.TextChanged += new System.EventHandler(this.uxOutputName_TextChanged);
            // 
            // uxCropTransparency
            // 
            this.uxCropTransparency.AutoSize = true;
            this.uxCropTransparency.Checked = true;
            this.uxCropTransparency.CheckState = System.Windows.Forms.CheckState.Checked;
            this.uxCropTransparency.Location = new System.Drawing.Point(290, 59);
            this.uxCropTransparency.Name = "uxCropTransparency";
            this.uxCropTransparency.Size = new System.Drawing.Size(112, 17);
            this.uxCropTransparency.TabIndex = 0;
            this.uxCropTransparency.Text = "Crop transparency";
            this.uxCropTransparency.UseVisualStyleBackColor = true;
            // 
            // uxHPadding
            // 
            this.uxHPadding.Location = new System.Drawing.Point(114, 58);
            this.uxHPadding.Maximum = new decimal(new int[] {
            128,
            0,
            0,
            0});
            this.uxHPadding.Name = "uxHPadding";
            this.uxHPadding.Size = new System.Drawing.Size(79, 20);
            this.uxHPadding.TabIndex = 0;
            this.uxHPadding.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // uxHPaddingLabel
            // 
            this.uxHPaddingLabel.AutoSize = true;
            this.uxHPaddingLabel.Location = new System.Drawing.Point(12, 60);
            this.uxHPaddingLabel.Name = "uxHPaddingLabel";
            this.uxHPaddingLabel.Size = new System.Drawing.Size(96, 13);
            this.uxHPaddingLabel.TabIndex = 8;
            this.uxHPaddingLabel.Text = "Horizontal Padding";
            // 
            // uxVPaddingLabel
            // 
            this.uxVPaddingLabel.AutoSize = true;
            this.uxVPaddingLabel.Location = new System.Drawing.Point(12, 86);
            this.uxVPaddingLabel.Name = "uxVPaddingLabel";
            this.uxVPaddingLabel.Size = new System.Drawing.Size(84, 13);
            this.uxVPaddingLabel.TabIndex = 9;
            this.uxVPaddingLabel.Text = "Vertical Padding";
            // 
            // uxVPadding
            // 
            this.uxVPadding.Location = new System.Drawing.Point(114, 84);
            this.uxVPadding.Maximum = new decimal(new int[] {
            128,
            0,
            0,
            0});
            this.uxVPadding.Name = "uxVPadding";
            this.uxVPadding.Size = new System.Drawing.Size(79, 20);
            this.uxVPadding.TabIndex = 0;
            this.uxVPadding.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // uxRoundUpPower2
            // 
            this.uxRoundUpPower2.AutoSize = true;
            this.uxRoundUpPower2.Checked = true;
            this.uxRoundUpPower2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.uxRoundUpPower2.Location = new System.Drawing.Point(290, 85);
            this.uxRoundUpPower2.Name = "uxRoundUpPower2";
            this.uxRoundUpPower2.Size = new System.Drawing.Size(159, 17);
            this.uxRoundUpPower2.TabIndex = 10;
            this.uxRoundUpPower2.Text = "Round up to power of 2 size";
            this.uxRoundUpPower2.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(456, 145);
            this.Controls.Add(this.uxRoundUpPower2);
            this.Controls.Add(this.uxVPadding);
            this.Controls.Add(this.uxVPaddingLabel);
            this.Controls.Add(this.uxHPaddingLabel);
            this.Controls.Add(this.uxHPadding);
            this.Controls.Add(this.uxCropTransparency);
            this.Controls.Add(this.uxOutputName);
            this.Controls.Add(this.uxOutputNameLabel);
            this.Controls.Add(this.uxBuild);
            this.Controls.Add(this.uxBrowse);
            this.Controls.Add(this.uxImageFolderLabel);
            this.Controls.Add(this.uxFolderName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Sheet Yourself";
            ((System.ComponentModel.ISupportInitialize)(this.uxHPadding)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uxVPadding)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog uxFolderBrowser;
        private System.Windows.Forms.TextBox uxFolderName;
        private System.Windows.Forms.Label uxImageFolderLabel;
        private System.Windows.Forms.Button uxBrowse;
        private System.Windows.Forms.Button uxBuild;
        private System.Windows.Forms.Label uxOutputNameLabel;
        private System.Windows.Forms.TextBox uxOutputName;
        private System.Windows.Forms.CheckBox uxCropTransparency;
        private System.Windows.Forms.NumericUpDown uxHPadding;
        private System.Windows.Forms.Label uxHPaddingLabel;
        private System.Windows.Forms.Label uxVPaddingLabel;
        private System.Windows.Forms.NumericUpDown uxVPadding;
        private System.Windows.Forms.CheckBox uxRoundUpPower2;

    }
}

