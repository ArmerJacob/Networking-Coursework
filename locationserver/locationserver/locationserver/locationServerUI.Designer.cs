namespace locationserver
{
    partial class locationServerUI
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
            this.runServerButton = new System.Windows.Forms.Button();
            this.portTextBox = new System.Windows.Forms.TextBox();
            this.portLabel = new System.Windows.Forms.Label();
            this.timeoutTextBox = new System.Windows.Forms.TextBox();
            this.timeoutLabel = new System.Windows.Forms.Label();
            this.portInfoButton = new System.Windows.Forms.Button();
            this.timeoutInfoButton = new System.Windows.Forms.Button();
            this.quitButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // runServerButton
            // 
            this.runServerButton.Location = new System.Drawing.Point(180, 15);
            this.runServerButton.Name = "runServerButton";
            this.runServerButton.Size = new System.Drawing.Size(130, 55);
            this.runServerButton.TabIndex = 0;
            this.runServerButton.Text = "Run server";
            this.runServerButton.UseVisualStyleBackColor = true;
            this.runServerButton.Click += new System.EventHandler(this.runServerButton_Click);
            // 
            // portTextBox
            // 
            this.portTextBox.Location = new System.Drawing.Point(13, 33);
            this.portTextBox.Name = "portTextBox";
            this.portTextBox.Size = new System.Drawing.Size(118, 20);
            this.portTextBox.TabIndex = 1;
            // 
            // portLabel
            // 
            this.portLabel.AutoSize = true;
            this.portLabel.Location = new System.Drawing.Point(13, 13);
            this.portLabel.Name = "portLabel";
            this.portLabel.Size = new System.Drawing.Size(26, 13);
            this.portLabel.TabIndex = 2;
            this.portLabel.Text = "Port";
            // 
            // timeoutTextBox
            // 
            this.timeoutTextBox.Location = new System.Drawing.Point(13, 101);
            this.timeoutTextBox.Name = "timeoutTextBox";
            this.timeoutTextBox.Size = new System.Drawing.Size(118, 20);
            this.timeoutTextBox.TabIndex = 3;
            // 
            // timeoutLabel
            // 
            this.timeoutLabel.AutoSize = true;
            this.timeoutLabel.Location = new System.Drawing.Point(13, 82);
            this.timeoutLabel.Name = "timeoutLabel";
            this.timeoutLabel.Size = new System.Drawing.Size(45, 13);
            this.timeoutLabel.TabIndex = 4;
            this.timeoutLabel.Text = "Timeout";
            // 
            // portInfoButton
            // 
            this.portInfoButton.Location = new System.Drawing.Point(138, 33);
            this.portInfoButton.Name = "portInfoButton";
            this.portInfoButton.Size = new System.Drawing.Size(16, 19);
            this.portInfoButton.TabIndex = 5;
            this.portInfoButton.Text = "i";
            this.portInfoButton.UseVisualStyleBackColor = true;
            this.portInfoButton.Click += new System.EventHandler(this.portInfoButton_Click);
            // 
            // timeoutInfoButton
            // 
            this.timeoutInfoButton.Location = new System.Drawing.Point(138, 101);
            this.timeoutInfoButton.Name = "timeoutInfoButton";
            this.timeoutInfoButton.Size = new System.Drawing.Size(15, 19);
            this.timeoutInfoButton.TabIndex = 6;
            this.timeoutInfoButton.Text = "i";
            this.timeoutInfoButton.UseVisualStyleBackColor = true;
            this.timeoutInfoButton.Click += new System.EventHandler(this.timeoutInfoButton_Click);
            // 
            // quitButton
            // 
            this.quitButton.Location = new System.Drawing.Point(180, 91);
            this.quitButton.Name = "quitButton";
            this.quitButton.Size = new System.Drawing.Size(130, 39);
            this.quitButton.TabIndex = 7;
            this.quitButton.Text = "Quit";
            this.quitButton.UseVisualStyleBackColor = true;
            this.quitButton.Click += new System.EventHandler(this.quitButton_Click);
            // 
            // locationServerUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(328, 140);
            this.Controls.Add(this.quitButton);
            this.Controls.Add(this.timeoutInfoButton);
            this.Controls.Add(this.portInfoButton);
            this.Controls.Add(this.timeoutLabel);
            this.Controls.Add(this.timeoutTextBox);
            this.Controls.Add(this.portLabel);
            this.Controls.Add(this.portTextBox);
            this.Controls.Add(this.runServerButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "locationServerUI";
            this.Text = "Location Server";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button runServerButton;
        private System.Windows.Forms.TextBox portTextBox;
        private System.Windows.Forms.Label portLabel;
        private System.Windows.Forms.TextBox timeoutTextBox;
        private System.Windows.Forms.Label timeoutLabel;
        private System.Windows.Forms.Button portInfoButton;
        private System.Windows.Forms.Button timeoutInfoButton;
        private System.Windows.Forms.Button quitButton;
    }
}