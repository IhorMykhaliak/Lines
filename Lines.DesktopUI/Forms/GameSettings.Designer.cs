namespace Lines.DesktopUI
{
    partial class GameSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameSettings));
            this.gbxDifficulty = new System.Windows.Forms.GroupBox();
            this.rbtnHard = new System.Windows.Forms.RadioButton();
            this.rbtnMedium = new System.Windows.Forms.RadioButton();
            this.rbtnEasy = new System.Windows.Forms.RadioButton();
            this.gbxSize = new System.Windows.Forms.GroupBox();
            this.rbtnExtraLargeSize = new System.Windows.Forms.RadioButton();
            this.rbtnLargeSize = new System.Windows.Forms.RadioButton();
            this.rbtnMediumSize = new System.Windows.Forms.RadioButton();
            this.rbtnSmallSize = new System.Windows.Forms.RadioButton();
            this.btnSaveSettings = new System.Windows.Forms.Button();
            this.gbxDifficulty.SuspendLayout();
            this.gbxSize.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxDifficulty
            // 
            this.gbxDifficulty.Controls.Add(this.rbtnHard);
            this.gbxDifficulty.Controls.Add(this.rbtnMedium);
            this.gbxDifficulty.Controls.Add(this.rbtnEasy);
            this.gbxDifficulty.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gbxDifficulty.Location = new System.Drawing.Point(12, 12);
            this.gbxDifficulty.Name = "gbxDifficulty";
            this.gbxDifficulty.Size = new System.Drawing.Size(209, 55);
            this.gbxDifficulty.TabIndex = 0;
            this.gbxDifficulty.TabStop = false;
            this.gbxDifficulty.Text = "Game difficulty";
            // 
            // rbtnHard
            // 
            this.rbtnHard.AutoSize = true;
            this.rbtnHard.Location = new System.Drawing.Point(142, 20);
            this.rbtnHard.Name = "rbtnHard";
            this.rbtnHard.Size = new System.Drawing.Size(36, 24);
            this.rbtnHard.TabIndex = 2;
            this.rbtnHard.Text = "5";
            this.rbtnHard.UseVisualStyleBackColor = true;
            // 
            // rbtnMedium
            // 
            this.rbtnMedium.AutoSize = true;
            this.rbtnMedium.Location = new System.Drawing.Point(76, 20);
            this.rbtnMedium.Name = "rbtnMedium";
            this.rbtnMedium.Size = new System.Drawing.Size(36, 24);
            this.rbtnMedium.TabIndex = 1;
            this.rbtnMedium.Text = "4";
            this.rbtnMedium.UseVisualStyleBackColor = true;
            // 
            // rbtnEasy
            // 
            this.rbtnEasy.AutoSize = true;
            this.rbtnEasy.Checked = true;
            this.rbtnEasy.Location = new System.Drawing.Point(7, 20);
            this.rbtnEasy.Name = "rbtnEasy";
            this.rbtnEasy.Size = new System.Drawing.Size(36, 24);
            this.rbtnEasy.TabIndex = 0;
            this.rbtnEasy.TabStop = true;
            this.rbtnEasy.Text = "3";
            this.rbtnEasy.UseVisualStyleBackColor = true;
            // 
            // gbxSize
            // 
            this.gbxSize.Controls.Add(this.rbtnExtraLargeSize);
            this.gbxSize.Controls.Add(this.rbtnLargeSize);
            this.gbxSize.Controls.Add(this.rbtnMediumSize);
            this.gbxSize.Controls.Add(this.rbtnSmallSize);
            this.gbxSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gbxSize.Location = new System.Drawing.Point(12, 86);
            this.gbxSize.Name = "gbxSize";
            this.gbxSize.Size = new System.Drawing.Size(209, 52);
            this.gbxSize.TabIndex = 2;
            this.gbxSize.TabStop = false;
            this.gbxSize.Text = "Size of field";
            // 
            // rbtnExtraLargeSize
            // 
            this.rbtnExtraLargeSize.AutoSize = true;
            this.rbtnExtraLargeSize.Location = new System.Drawing.Point(157, 20);
            this.rbtnExtraLargeSize.Name = "rbtnExtraLargeSize";
            this.rbtnExtraLargeSize.Size = new System.Drawing.Size(45, 24);
            this.rbtnExtraLargeSize.TabIndex = 3;
            this.rbtnExtraLargeSize.Text = "10";
            this.rbtnExtraLargeSize.UseVisualStyleBackColor = true;
            // 
            // rbtnLargeSize
            // 
            this.rbtnLargeSize.AutoSize = true;
            this.rbtnLargeSize.Location = new System.Drawing.Point(111, 20);
            this.rbtnLargeSize.Name = "rbtnLargeSize";
            this.rbtnLargeSize.Size = new System.Drawing.Size(36, 24);
            this.rbtnLargeSize.TabIndex = 2;
            this.rbtnLargeSize.Text = "9";
            this.rbtnLargeSize.UseVisualStyleBackColor = true;
            // 
            // rbtnMediumSize
            // 
            this.rbtnMediumSize.AutoSize = true;
            this.rbtnMediumSize.Location = new System.Drawing.Point(57, 20);
            this.rbtnMediumSize.Name = "rbtnMediumSize";
            this.rbtnMediumSize.Size = new System.Drawing.Size(36, 24);
            this.rbtnMediumSize.TabIndex = 1;
            this.rbtnMediumSize.Text = "8";
            this.rbtnMediumSize.UseVisualStyleBackColor = true;
            // 
            // rbtnSmallSize
            // 
            this.rbtnSmallSize.AutoSize = true;
            this.rbtnSmallSize.Checked = true;
            this.rbtnSmallSize.Location = new System.Drawing.Point(7, 20);
            this.rbtnSmallSize.Name = "rbtnSmallSize";
            this.rbtnSmallSize.Size = new System.Drawing.Size(36, 24);
            this.rbtnSmallSize.TabIndex = 0;
            this.rbtnSmallSize.TabStop = true;
            this.rbtnSmallSize.Text = "7";
            this.rbtnSmallSize.UseVisualStyleBackColor = true;
            // 
            // btnSaveSettings
            // 
            this.btnSaveSettings.BackColor = System.Drawing.Color.IndianRed;
            this.btnSaveSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSaveSettings.ForeColor = System.Drawing.SystemColors.Desktop;
            this.btnSaveSettings.Location = new System.Drawing.Point(69, 144);
            this.btnSaveSettings.Name = "btnSaveSettings";
            this.btnSaveSettings.Size = new System.Drawing.Size(104, 44);
            this.btnSaveSettings.TabIndex = 3;
            this.btnSaveSettings.Text = "Save changes";
            this.btnSaveSettings.UseVisualStyleBackColor = true;
            this.btnSaveSettings.Click += new System.EventHandler(this.btnSaveSettings_Click);
            // 
            // GameSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(235, 196);
            this.Controls.Add(this.btnSaveSettings);
            this.Controls.Add(this.gbxSize);
            this.Controls.Add(this.gbxDifficulty);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GameSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lines";
            this.gbxDifficulty.ResumeLayout(false);
            this.gbxDifficulty.PerformLayout();
            this.gbxSize.ResumeLayout(false);
            this.gbxSize.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxDifficulty;
        private System.Windows.Forms.RadioButton rbtnHard;
        private System.Windows.Forms.RadioButton rbtnMedium;
        private System.Windows.Forms.RadioButton rbtnEasy;
        private System.Windows.Forms.GroupBox gbxSize;
        private System.Windows.Forms.RadioButton rbtnExtraLargeSize;
        private System.Windows.Forms.RadioButton rbtnLargeSize;
        private System.Windows.Forms.RadioButton rbtnMediumSize;
        private System.Windows.Forms.RadioButton rbtnSmallSize;
        private System.Windows.Forms.Button btnSaveSettings;
    }
}