namespace Lines.DesktopUI
{
    partial class Lines
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Lines));
            this.lblScore = new System.Windows.Forms.Label();
            this.lblTurn = new System.Windows.Forms.Label();
            this.btnStop = new System.Windows.Forms.Button();
            this.lblPath = new System.Windows.Forms.Label();
            this.btnUndo = new System.Windows.Forms.Button();
            this.btnRestartGame = new System.Windows.Forms.Button();
            this.pbxSound = new System.Windows.Forms.PictureBox();
            this.pbxGameBoard = new System.Windows.Forms.PictureBox();
            this.lblAllowedUndos = new System.Windows.Forms.Label();
            this.btnNewGame = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbxSound)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxGameBoard)).BeginInit();
            this.SuspendLayout();
            // 
            // lblScore
            // 
            this.lblScore.AutoSize = true;
            this.lblScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblScore.Location = new System.Drawing.Point(66, 6);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(122, 31);
            this.lblScore.TabIndex = 3;
            this.lblScore.Text = "Score : 0";
            // 
            // lblTurn
            // 
            this.lblTurn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTurn.AutoSize = true;
            this.lblTurn.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblTurn.Location = new System.Drawing.Point(288, 6);
            this.lblTurn.Name = "lblTurn";
            this.lblTurn.Size = new System.Drawing.Size(107, 31);
            this.lblTurn.TabIndex = 5;
            this.lblTurn.Text = "Turn : 0";
            // 
            // btnStop
            // 
            this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStop.BackColor = System.Drawing.Color.IndianRed;
            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnStop.ForeColor = System.Drawing.SystemColors.Desktop;
            this.btnStop.Location = new System.Drawing.Point(318, 474);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(100, 70);
            this.btnStop.TabIndex = 7;
            this.btnStop.Text = "End Game";
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // lblPath
            // 
            this.lblPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblPath.AutoSize = true;
            this.lblPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblPath.Location = new System.Drawing.Point(14, 443);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(0, 17);
            this.lblPath.TabIndex = 8;
            // 
            // btnUndo
            // 
            this.btnUndo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUndo.BackColor = System.Drawing.Color.IndianRed;
            this.btnUndo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUndo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnUndo.ForeColor = System.Drawing.SystemColors.Desktop;
            this.btnUndo.Location = new System.Drawing.Point(212, 474);
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(100, 70);
            this.btnUndo.TabIndex = 11;
            this.btnUndo.Text = "Undo";
            this.btnUndo.UseVisualStyleBackColor = true;
            this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);
            // 
            // btnRestartGame
            // 
            this.btnRestartGame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRestartGame.BackColor = System.Drawing.Color.IndianRed;
            this.btnRestartGame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestartGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnRestartGame.ForeColor = System.Drawing.SystemColors.Desktop;
            this.btnRestartGame.Location = new System.Drawing.Point(17, 474);
            this.btnRestartGame.Name = "btnRestartGame";
            this.btnRestartGame.Size = new System.Drawing.Size(120, 33);
            this.btnRestartGame.TabIndex = 12;
            this.btnRestartGame.Text = "Restart";
            this.btnRestartGame.UseVisualStyleBackColor = true;
            this.btnRestartGame.Click += new System.EventHandler(this.btnRestartGame_Click);
            // 
            // pbxSound
            // 
            this.pbxSound.Image = global::Lines.DesktopUI.Properties.Resources.sound;
            this.pbxSound.Location = new System.Drawing.Point(17, 4);
            this.pbxSound.Name = "pbxSound";
            this.pbxSound.Size = new System.Drawing.Size(43, 33);
            this.pbxSound.TabIndex = 13;
            this.pbxSound.TabStop = false;
            this.pbxSound.Click += new System.EventHandler(this.pbxSound_Click);
            // 
            // pbxGameBoard
            // 
            this.pbxGameBoard.BackColor = System.Drawing.Color.IndianRed;
            this.pbxGameBoard.Location = new System.Drawing.Point(17, 40);
            this.pbxGameBoard.Name = "pbxGameBoard";
            this.pbxGameBoard.Size = new System.Drawing.Size(400, 400);
            this.pbxGameBoard.TabIndex = 1;
            this.pbxGameBoard.TabStop = false;
            this.pbxGameBoard.Paint += new System.Windows.Forms.PaintEventHandler(this.pbxGameBoard_Paint);
            this.pbxGameBoard.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbxGameBoard_MouseClick);
            // 
            // lblAllowedUndos
            // 
            this.lblAllowedUndos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAllowedUndos.AutoSize = true;
            this.lblAllowedUndos.BackColor = System.Drawing.Color.Transparent;
            this.lblAllowedUndos.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblAllowedUndos.ForeColor = System.Drawing.Color.Black;
            this.lblAllowedUndos.Location = new System.Drawing.Point(215, 545);
            this.lblAllowedUndos.Name = "lblAllowedUndos";
            this.lblAllowedUndos.Size = new System.Drawing.Size(59, 13);
            this.lblAllowedUndos.TabIndex = 14;
            this.lblAllowedUndos.Text = "Allowed : 0";
            // 
            // btnNewGame
            // 
            this.btnNewGame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNewGame.BackColor = System.Drawing.Color.IndianRed;
            this.btnNewGame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnNewGame.ForeColor = System.Drawing.SystemColors.Desktop;
            this.btnNewGame.Location = new System.Drawing.Point(17, 511);
            this.btnNewGame.Name = "btnNewGame";
            this.btnNewGame.Size = new System.Drawing.Size(120, 33);
            this.btnNewGame.TabIndex = 15;
            this.btnNewGame.Text = "Main Menu";
            this.btnNewGame.UseVisualStyleBackColor = true;
            this.btnNewGame.Click += new System.EventHandler(this.btnNewGame_Click);
            // 
            // Lines
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(430, 560);
            this.Controls.Add(this.btnNewGame);
            this.Controls.Add(this.lblAllowedUndos);
            this.Controls.Add(this.pbxSound);
            this.Controls.Add(this.btnRestartGame);
            this.Controls.Add(this.btnUndo);
            this.Controls.Add(this.lblPath);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.lblTurn);
            this.Controls.Add(this.lblScore);
            this.Controls.Add(this.pbxGameBoard);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Lines";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lines";
            ((System.ComponentModel.ISupportInitialize)(this.pbxSound)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxGameBoard)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbxGameBoard;
        private System.Windows.Forms.Label lblScore;
        private System.Windows.Forms.Label lblTurn;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label lblPath;
        private System.Windows.Forms.Button btnUndo;
        private System.Windows.Forms.Button btnRestartGame;
        private System.Windows.Forms.PictureBox pbxSound;
        private System.Windows.Forms.Label lblAllowedUndos;
        private System.Windows.Forms.Button btnNewGame;
    }
}

