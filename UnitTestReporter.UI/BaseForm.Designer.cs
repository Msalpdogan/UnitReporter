namespace UnitTestReporter
{
    partial class BaseForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.inputFile = new System.Windows.Forms.TextBox();
            this.outputFolder = new System.Windows.Forms.TextBox();
            this.templateFile = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.reportButton = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // inputFile
            // 
            this.inputFile.Location = new System.Drawing.Point(151, 7);
            this.inputFile.Multiline = true;
            this.inputFile.Name = "inputFile";
            this.inputFile.ReadOnly = true;
            this.inputFile.Size = new System.Drawing.Size(365, 31);
            this.inputFile.TabIndex = 0;
            // 
            // outputFolder
            // 
            this.outputFolder.Location = new System.Drawing.Point(151, 40);
            this.outputFolder.Multiline = true;
            this.outputFolder.Name = "outputFolder";
            this.outputFolder.ReadOnly = true;
            this.outputFolder.Size = new System.Drawing.Size(365, 31);
            this.outputFolder.TabIndex = 2;
            // 
            // templateFile
            // 
            this.templateFile.Location = new System.Drawing.Point(151, 77);
            this.templateFile.Multiline = true;
            this.templateFile.Name = "templateFile";
            this.templateFile.ReadOnly = true;
            this.templateFile.Size = new System.Drawing.Size(365, 31);
            this.templateFile.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(133, 31);
            this.button1.TabIndex = 6;
            this.button1.Text = "Select Input File ";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 40);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(133, 31);
            this.button2.TabIndex = 7;
            this.button2.Text = "Select Output Folder ";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(12, 77);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(133, 31);
            this.button3.TabIndex = 8;
            this.button3.Text = "Select Template File";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // reportButton
            // 
            this.reportButton.Enabled = false;
            this.reportButton.Location = new System.Drawing.Point(12, 114);
            this.reportButton.Name = "reportButton";
            this.reportButton.Size = new System.Drawing.Size(504, 32);
            this.reportButton.TabIndex = 9;
            this.reportButton.Text = "Create Report (Please Select Files)";
            this.reportButton.UseVisualStyleBackColor = true;
            this.reportButton.Click += new System.EventHandler(this.reportButton_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(13, 153);
            this.progressBar.Maximum = 80;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(503, 23);
            this.progressBar.TabIndex = 10;
            // 
            // BaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 195);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.reportButton);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.templateFile);
            this.Controls.Add(this.outputFolder);
            this.Controls.Add(this.inputFile);
            this.Name = "BaseForm";
            this.Tag = "";
            this.Text = "Muki Reporter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox inputFile;
        private System.Windows.Forms.TextBox outputFolder;
        private System.Windows.Forms.TextBox templateFile;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button reportButton;
        private System.Windows.Forms.ProgressBar progressBar;
    }
}

