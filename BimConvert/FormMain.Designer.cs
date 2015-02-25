namespace BimConvert
{
    partial class FormMain
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
            this.labelBrowseSource = new System.Windows.Forms.Label();
            this.textBoxSourceFile = new System.Windows.Forms.TextBox();
            this.buttonBrowseSourceFiles = new System.Windows.Forms.Button();
            this.labelDestination = new System.Windows.Forms.Label();
            this.textBoxDestination = new System.Windows.Forms.TextBox();
            this.buttonConvert = new System.Windows.Forms.Button();
            this.openFileDialogSource = new System.Windows.Forms.OpenFileDialog();
            this.buttonBrowseDestinationFolder = new System.Windows.Forms.Button();
            this.folderBrowserDialogDestination = new System.Windows.Forms.FolderBrowserDialog();
            this.listViewSourceFiles = new System.Windows.Forms.ListView();
            this.buttonClear = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelBrowseSource
            // 
            this.labelBrowseSource.AutoSize = true;
            this.labelBrowseSource.Location = new System.Drawing.Point(12, 24);
            this.labelBrowseSource.Name = "labelBrowseSource";
            this.labelBrowseSource.Size = new System.Drawing.Size(57, 13);
            this.labelBrowseSource.TabIndex = 0;
            this.labelBrowseSource.Text = "Source file";
            // 
            // textBoxSourceFile
            // 
            this.textBoxSourceFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSourceFile.Location = new System.Drawing.Point(85, 21);
            this.textBoxSourceFile.Name = "textBoxSourceFile";
            this.textBoxSourceFile.ReadOnly = true;
            this.textBoxSourceFile.Size = new System.Drawing.Size(450, 20);
            this.textBoxSourceFile.TabIndex = 1;
            // 
            // buttonBrowseSourceFiles
            // 
            this.buttonBrowseSourceFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowseSourceFiles.Location = new System.Drawing.Point(541, 15);
            this.buttonBrowseSourceFiles.Name = "buttonBrowseSourceFiles";
            this.buttonBrowseSourceFiles.Size = new System.Drawing.Size(119, 31);
            this.buttonBrowseSourceFiles.TabIndex = 2;
            this.buttonBrowseSourceFiles.Text = "Browse";
            this.buttonBrowseSourceFiles.UseVisualStyleBackColor = true;
            this.buttonBrowseSourceFiles.Click += new System.EventHandler(this.buttonBrowseSourceFiles_Click);
            // 
            // labelDestination
            // 
            this.labelDestination.AutoSize = true;
            this.labelDestination.Location = new System.Drawing.Point(12, 66);
            this.labelDestination.Name = "labelDestination";
            this.labelDestination.Size = new System.Drawing.Size(60, 13);
            this.labelDestination.TabIndex = 3;
            this.labelDestination.Text = "Destination";
            // 
            // textBoxDestination
            // 
            this.textBoxDestination.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDestination.Location = new System.Drawing.Point(85, 63);
            this.textBoxDestination.Name = "textBoxDestination";
            this.textBoxDestination.ReadOnly = true;
            this.textBoxDestination.Size = new System.Drawing.Size(450, 20);
            this.textBoxDestination.TabIndex = 4;
            // 
            // buttonConvert
            // 
            this.buttonConvert.Location = new System.Drawing.Point(12, 101);
            this.buttonConvert.Name = "buttonConvert";
            this.buttonConvert.Size = new System.Drawing.Size(119, 31);
            this.buttonConvert.TabIndex = 5;
            this.buttonConvert.Text = "Convert";
            this.buttonConvert.UseVisualStyleBackColor = true;
            this.buttonConvert.Click += new System.EventHandler(this.buttonConvert_Click);
            // 
            // buttonBrowseDestinationFolder
            // 
            this.buttonBrowseDestinationFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowseDestinationFolder.Location = new System.Drawing.Point(541, 57);
            this.buttonBrowseDestinationFolder.Name = "buttonBrowseDestinationFolder";
            this.buttonBrowseDestinationFolder.Size = new System.Drawing.Size(119, 31);
            this.buttonBrowseDestinationFolder.TabIndex = 6;
            this.buttonBrowseDestinationFolder.Text = "Browse";
            this.buttonBrowseDestinationFolder.UseVisualStyleBackColor = true;
            this.buttonBrowseDestinationFolder.Click += new System.EventHandler(this.buttonBrowseDestinationFolder_Click);
            // 
            // listViewSourceFiles
            // 
            this.listViewSourceFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewSourceFiles.Location = new System.Drawing.Point(12, 138);
            this.listViewSourceFiles.Name = "listViewSourceFiles";
            this.listViewSourceFiles.Size = new System.Drawing.Size(648, 312);
            this.listViewSourceFiles.TabIndex = 8;
            this.listViewSourceFiles.UseCompatibleStateImageBehavior = false;
            this.listViewSourceFiles.SelectedIndexChanged += new System.EventHandler(this.listViewSourceFiles_SelectedIndexChanged);
            // 
            // buttonClear
            // 
            this.buttonClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClear.Location = new System.Drawing.Point(541, 102);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(119, 30);
            this.buttonClear.TabIndex = 9;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(416, 101);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(119, 31);
            this.buttonCancel.TabIndex = 10;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(673, 461);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.listViewSourceFiles);
            this.Controls.Add(this.buttonBrowseDestinationFolder);
            this.Controls.Add(this.buttonConvert);
            this.Controls.Add(this.textBoxDestination);
            this.Controls.Add(this.labelDestination);
            this.Controls.Add(this.buttonBrowseSourceFiles);
            this.Controls.Add(this.textBoxSourceFile);
            this.Controls.Add(this.labelBrowseSource);
            this.MinimumSize = new System.Drawing.Size(400, 200);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bim Convert";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelBrowseSource;
        private System.Windows.Forms.TextBox textBoxSourceFile;
        private System.Windows.Forms.Button buttonBrowseSourceFiles;
        private System.Windows.Forms.Label labelDestination;
        private System.Windows.Forms.TextBox textBoxDestination;
        private System.Windows.Forms.Button buttonConvert;
        private System.Windows.Forms.OpenFileDialog openFileDialogSource;
        private System.Windows.Forms.Button buttonBrowseDestinationFolder;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogDestination;
        private System.Windows.Forms.ListView listViewSourceFiles;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Button buttonCancel;
    }
}

