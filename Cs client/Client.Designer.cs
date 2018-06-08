using System.Windows.Forms;

namespace TIN
{
    partial class Client
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Client));
            this.imageBox = new System.Windows.Forms.PictureBox();
            this.openButton = new System.Windows.Forms.Button();
            this.sendButton = new System.Windows.Forms.Button();
            this.sendPictureLabel = new System.Windows.Forms.Label();
            this.pictureNameLabel = new System.Windows.Forms.Label();
            this.connLabel = new System.Windows.Forms.Label();
            this.IPLabel = new System.Windows.Forms.Label();
            this.PortLabel = new System.Windows.Forms.Label();
            this.disconnectButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).BeginInit();
            this.SuspendLayout();
            // 
            // imageBox
            // 
            this.imageBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.imageBox.ErrorImage = ((System.Drawing.Image)(resources.GetObject("imageBox.ErrorImage")));
            this.imageBox.InitialImage = null;
            this.imageBox.Location = new System.Drawing.Point(30, 30);
            this.imageBox.Name = "imageBox";
            this.imageBox.Size = new System.Drawing.Size(512, 512);
            this.imageBox.TabIndex = 0;
            this.imageBox.TabStop = false;
            this.imageBox.Paint += new System.Windows.Forms.PaintEventHandler(this.imageBox_Paint);
            // 
            // openButton
            // 
            this.openButton.Location = new System.Drawing.Point(588, 478);
            this.openButton.Name = "openButton";
            this.openButton.Size = new System.Drawing.Size(75, 25);
            this.openButton.TabIndex = 1;
            this.openButton.Text = "open";
            this.openButton.UseVisualStyleBackColor = true;
            this.openButton.Click += new System.EventHandler(this.openButton_Click);
            // 
            // sendButton
            // 
            this.sendButton.Location = new System.Drawing.Point(700, 478);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(75, 25);
            this.sendButton.TabIndex = 2;
            this.sendButton.Text = "send";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // sendPictureLabel
            // 
            this.sendPictureLabel.AutoSize = true;
            this.sendPictureLabel.Location = new System.Drawing.Point(630, 447);
            this.sendPictureLabel.Name = "sendPictureLabel";
            this.sendPictureLabel.Size = new System.Drawing.Size(86, 17);
            this.sendPictureLabel.TabIndex = 3;
            this.sendPictureLabel.Text = "send picture";
            // 
            // pictureNameLabel
            // 
            this.pictureNameLabel.AutoSize = true;
            this.pictureNameLabel.Location = new System.Drawing.Point(585, 525);
            this.pictureNameLabel.Name = "pictureNameLabel";
            this.pictureNameLabel.Size = new System.Drawing.Size(81, 17);
            this.pictureNameLabel.TabIndex = 4;
            this.pictureNameLabel.Text = "*no picture*";
            // 
            // connLabel
            // 
            this.connLabel.AutoSize = true;
            this.connLabel.Location = new System.Drawing.Point(630, 30);
            this.connLabel.Name = "connLabel";
            this.connLabel.Size = new System.Drawing.Size(94, 17);
            this.connLabel.TabIndex = 5;
            this.connLabel.Text = "connected to:";
            // 
            // IPLabel
            // 
            this.IPLabel.AutoSize = true;
            this.IPLabel.Location = new System.Drawing.Point(585, 64);
            this.IPLabel.Name = "IPLabel";
            this.IPLabel.Size = new System.Drawing.Size(30, 17);
            this.IPLabel.TabIndex = 6;
            this.IPLabel.Text = "*IP*";
            // 
            // PortLabel
            // 
            this.PortLabel.AutoSize = true;
            this.PortLabel.Location = new System.Drawing.Point(585, 96);
            this.PortLabel.Name = "PortLabel";
            this.PortLabel.Size = new System.Drawing.Size(43, 17);
            this.PortLabel.TabIndex = 7;
            this.PortLabel.Text = "*port*";
            // 
            // disconnectButton
            // 
            this.disconnectButton.Location = new System.Drawing.Point(690, 64);
            this.disconnectButton.Name = "disconnectButton";
            this.disconnectButton.Size = new System.Drawing.Size(85, 25);
            this.disconnectButton.TabIndex = 11;
            this.disconnectButton.Text = "disconnect";
            this.disconnectButton.UseVisualStyleBackColor = true;
            this.disconnectButton.Click += new System.EventHandler(this.disconnectButton_Click);
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(821, 570);
            this.Controls.Add(this.disconnectButton);
            this.Controls.Add(this.PortLabel);
            this.Controls.Add(this.IPLabel);
            this.Controls.Add(this.connLabel);
            this.Controls.Add(this.pictureNameLabel);
            this.Controls.Add(this.sendPictureLabel);
            this.Controls.Add(this.sendButton);
            this.Controls.Add(this.openButton);
            this.Controls.Add(this.imageBox);
            this.Name = "Client";
            this.Text = "Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Client_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox imageBox;
        private System.Windows.Forms.Button openButton;
        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.Label sendPictureLabel;
        private System.Windows.Forms.Label pictureNameLabel;
        private System.Windows.Forms.Label connLabel;
        private System.Windows.Forms.Label IPLabel;
        private System.Windows.Forms.Label PortLabel;
        private System.Windows.Forms.Button disconnectButton;
    }
}