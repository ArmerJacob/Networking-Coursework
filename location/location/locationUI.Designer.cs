namespace location
{
    partial class locationUI
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ipTextBox = new System.Windows.Forms.TextBox();
            this.portTextBox = new System.Windows.Forms.TextBox();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.locationTextBox = new System.Windows.Forms.TextBox();
            this.protocolComboBox = new System.Windows.Forms.ComboBox();
            this.sendButton = new System.Windows.Forms.Button();
            this.ipInfoButton = new System.Windows.Forms.Button();
            this.portInfoButton = new System.Windows.Forms.Button();
            this.nameInfoButton = new System.Windows.Forms.Button();
            this.locationInfoButton = new System.Windows.Forms.Button();
            this.protocolInfoButton = new System.Windows.Forms.Button();
            this.clientOutput = new System.Windows.Forms.RichTextBox();
            this.ClearLogButton = new System.Windows.Forms.Button();
            this.quitButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Port";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 163);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Name";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 238);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Location";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 326);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Protocol";
            // 
            // ipTextBox
            // 
            this.ipTextBox.Location = new System.Drawing.Point(15, 39);
            this.ipTextBox.Name = "ipTextBox";
            this.ipTextBox.Size = new System.Drawing.Size(162, 20);
            this.ipTextBox.TabIndex = 5;
            // 
            // portTextBox
            // 
            this.portTextBox.Location = new System.Drawing.Point(15, 123);
            this.portTextBox.Name = "portTextBox";
            this.portTextBox.Size = new System.Drawing.Size(162, 20);
            this.portTextBox.TabIndex = 6;
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(15, 198);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(162, 20);
            this.nameTextBox.TabIndex = 7;
            // 
            // locationTextBox
            // 
            this.locationTextBox.Location = new System.Drawing.Point(15, 279);
            this.locationTextBox.Name = "locationTextBox";
            this.locationTextBox.Size = new System.Drawing.Size(162, 20);
            this.locationTextBox.TabIndex = 8;
            // 
            // protocolComboBox
            // 
            this.protocolComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.protocolComboBox.FormattingEnabled = true;
            this.protocolComboBox.Items.AddRange(new object[] {
            "HTTP 0.9",
            "HTTP 1.0",
            "HTTP 1.1",
            "WhoIs"});
            this.protocolComboBox.Location = new System.Drawing.Point(18, 371);
            this.protocolComboBox.Name = "protocolComboBox";
            this.protocolComboBox.Size = new System.Drawing.Size(159, 21);
            this.protocolComboBox.TabIndex = 9;
            // 
            // sendButton
            // 
            this.sendButton.Location = new System.Drawing.Point(318, 371);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(155, 67);
            this.sendButton.TabIndex = 10;
            this.sendButton.Text = "Send to server";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // ipInfoButton
            // 
            this.ipInfoButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ipInfoButton.Location = new System.Drawing.Point(184, 39);
            this.ipInfoButton.Name = "ipInfoButton";
            this.ipInfoButton.Size = new System.Drawing.Size(18, 20);
            this.ipInfoButton.TabIndex = 12;
            this.ipInfoButton.Text = "i";
            this.ipInfoButton.UseVisualStyleBackColor = true;
            this.ipInfoButton.Click += new System.EventHandler(this.ipInfoButton_Click);
            // 
            // portInfoButton
            // 
            this.portInfoButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.portInfoButton.Location = new System.Drawing.Point(184, 123);
            this.portInfoButton.Name = "portInfoButton";
            this.portInfoButton.Size = new System.Drawing.Size(18, 19);
            this.portInfoButton.TabIndex = 13;
            this.portInfoButton.Text = "i";
            this.portInfoButton.UseVisualStyleBackColor = true;
            this.portInfoButton.Click += new System.EventHandler(this.portInfoButton_Click);
            // 
            // nameInfoButton
            // 
            this.nameInfoButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.nameInfoButton.Location = new System.Drawing.Point(183, 198);
            this.nameInfoButton.Name = "nameInfoButton";
            this.nameInfoButton.Size = new System.Drawing.Size(18, 19);
            this.nameInfoButton.TabIndex = 14;
            this.nameInfoButton.Text = "i";
            this.nameInfoButton.UseVisualStyleBackColor = true;
            this.nameInfoButton.Click += new System.EventHandler(this.nameInfoButton_Click);
            // 
            // locationInfoButton
            // 
            this.locationInfoButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.locationInfoButton.Location = new System.Drawing.Point(183, 279);
            this.locationInfoButton.Name = "locationInfoButton";
            this.locationInfoButton.Size = new System.Drawing.Size(19, 19);
            this.locationInfoButton.TabIndex = 15;
            this.locationInfoButton.Text = "i";
            this.locationInfoButton.UseVisualStyleBackColor = true;
            this.locationInfoButton.Click += new System.EventHandler(this.locationInfoButton_Click);
            // 
            // protocolInfoButton
            // 
            this.protocolInfoButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.protocolInfoButton.Location = new System.Drawing.Point(184, 371);
            this.protocolInfoButton.Name = "protocolInfoButton";
            this.protocolInfoButton.Size = new System.Drawing.Size(17, 20);
            this.protocolInfoButton.TabIndex = 16;
            this.protocolInfoButton.Text = "i";
            this.protocolInfoButton.UseVisualStyleBackColor = true;
            this.protocolInfoButton.Click += new System.EventHandler(this.protocolInfoButton_Click);
            // 
            // clientOutput
            // 
            this.clientOutput.Location = new System.Drawing.Point(229, 12);
            this.clientOutput.Name = "clientOutput";
            this.clientOutput.ReadOnly = true;
            this.clientOutput.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.clientOutput.Size = new System.Drawing.Size(235, 353);
            this.clientOutput.TabIndex = 17;
            this.clientOutput.Text = "";
            // 
            // ClearLogButton
            // 
            this.ClearLogButton.Location = new System.Drawing.Point(229, 368);
            this.ClearLogButton.Name = "ClearLogButton";
            this.ClearLogButton.Size = new System.Drawing.Size(75, 23);
            this.ClearLogButton.TabIndex = 18;
            this.ClearLogButton.Text = "Clear log";
            this.ClearLogButton.UseVisualStyleBackColor = true;
            this.ClearLogButton.Click += new System.EventHandler(this.ClearLogButton_Click);
            // 
            // quitButton
            // 
            this.quitButton.Location = new System.Drawing.Point(172, 403);
            this.quitButton.Name = "quitButton";
            this.quitButton.Size = new System.Drawing.Size(132, 35);
            this.quitButton.TabIndex = 19;
            this.quitButton.Text = "Quit";
            this.quitButton.UseVisualStyleBackColor = true;
            this.quitButton.Click += new System.EventHandler(this.quitButton_Click);
            // 
            // locationUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(476, 438);
            this.Controls.Add(this.quitButton);
            this.Controls.Add(this.ClearLogButton);
            this.Controls.Add(this.clientOutput);
            this.Controls.Add(this.protocolInfoButton);
            this.Controls.Add(this.locationInfoButton);
            this.Controls.Add(this.nameInfoButton);
            this.Controls.Add(this.portInfoButton);
            this.Controls.Add(this.ipInfoButton);
            this.Controls.Add(this.sendButton);
            this.Controls.Add(this.protocolComboBox);
            this.Controls.Add(this.locationTextBox);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.portTextBox);
            this.Controls.Add(this.ipTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "locationUI";
            this.Text = "Location Client";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox ipTextBox;
        private System.Windows.Forms.TextBox portTextBox;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.TextBox locationTextBox;
        private System.Windows.Forms.ComboBox protocolComboBox;
        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.Button ipInfoButton;
        private System.Windows.Forms.Button portInfoButton;
        private System.Windows.Forms.Button nameInfoButton;
        private System.Windows.Forms.Button locationInfoButton;
        private System.Windows.Forms.Button protocolInfoButton;
        public System.Windows.Forms.RichTextBox clientOutput;
        private System.Windows.Forms.Button ClearLogButton;
        private System.Windows.Forms.Button quitButton;
    }
}