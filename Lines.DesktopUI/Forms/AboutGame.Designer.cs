namespace Lines.DesktopUI
{
    partial class AboutGame
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutGame));
            this.lblHowToPlay = new System.Windows.Forms.Label();
            this.rtxtHowToPlay = new System.Windows.Forms.RichTextBox();
            this.lblAuthor = new System.Windows.Forms.Label();
            this.lblPlace = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblHowToPlay
            // 
            this.lblHowToPlay.AutoSize = true;
            this.lblHowToPlay.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblHowToPlay.Location = new System.Drawing.Point(12, 18);
            this.lblHowToPlay.Name = "lblHowToPlay";
            this.lblHowToPlay.Size = new System.Drawing.Size(99, 20);
            this.lblHowToPlay.TabIndex = 0;
            this.lblHowToPlay.Text = "How to play :";
            // 
            // rtxtHowToPlay
            // 
            this.rtxtHowToPlay.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rtxtHowToPlay.Location = new System.Drawing.Point(12, 41);
            this.rtxtHowToPlay.Name = "rtxtHowToPlay";
            this.rtxtHowToPlay.ReadOnly = true;
            this.rtxtHowToPlay.Size = new System.Drawing.Size(400, 392);
            this.rtxtHowToPlay.TabIndex = 1;
            this.rtxtHowToPlay.Text = resources.GetString("rtxtHowToPlay.Text");
            // 
            // lblAuthor
            // 
            this.lblAuthor.AutoSize = true;
            this.lblAuthor.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblAuthor.Location = new System.Drawing.Point(213, 436);
            this.lblAuthor.Name = "lblAuthor";
            this.lblAuthor.Size = new System.Drawing.Size(199, 20);
            this.lblAuthor.TabIndex = 2;
            this.lblAuthor.Text = "Created by : Ihor Mykhaliak";
            // 
            // lblPlace
            // 
            this.lblPlace.AutoSize = true;
            this.lblPlace.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblPlace.Location = new System.Drawing.Point(282, 462);
            this.lblPlace.Name = "lblPlace";
            this.lblPlace.Size = new System.Drawing.Size(130, 18);
            this.lblPlace.TabIndex = 3;
            this.lblPlace.Text = "EPAM IT Lab 2015";
            // 
            // AboutGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 489);
            this.Controls.Add(this.lblPlace);
            this.Controls.Add(this.lblAuthor);
            this.Controls.Add(this.rtxtHowToPlay);
            this.Controls.Add(this.lblHowToPlay);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutGame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lines";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHowToPlay;
        private System.Windows.Forms.RichTextBox rtxtHowToPlay;
        private System.Windows.Forms.Label lblAuthor;
        private System.Windows.Forms.Label lblPlace;
    }
}