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
            this.pbxGameBoard = new System.Windows.Forms.PictureBox();
            this.lblScore = new System.Windows.Forms.Label();
            this.lblTurn = new System.Windows.Forms.Label();
            this.btnStop = new System.Windows.Forms.Button();
            this.lblPath = new System.Windows.Forms.Label();
            this.btnStepBack = new System.Windows.Forms.Button();
            this.btnNewGame = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbxGameBoard)).BeginInit();
            this.SuspendLayout();
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
            // lblScore
            // 
            this.lblScore.AutoSize = true;
            this.lblScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblScore.Location = new System.Drawing.Point(47, 6);
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
            this.btnStop.Location = new System.Drawing.Point(318, 445);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(100, 70);
            this.btnStop.TabIndex = 7;
            this.btnStop.Text = "End Game";
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // lblPath
            // 
            this.lblPath.AutoSize = true;
            this.lblPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblPath.Location = new System.Drawing.Point(59, 590);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(0, 31);
            this.lblPath.TabIndex = 8;
            // 
            // btnStepBack
            // 
            this.btnStepBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStepBack.BackColor = System.Drawing.Color.IndianRed;
            this.btnStepBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStepBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnStepBack.ForeColor = System.Drawing.SystemColors.Desktop;
            this.btnStepBack.Location = new System.Drawing.Point(212, 445);
            this.btnStepBack.Name = "btnStepBack";
            this.btnStepBack.Size = new System.Drawing.Size(100, 70);
            this.btnStepBack.TabIndex = 11;
            this.btnStepBack.Text = "Cancel Move";
            this.btnStepBack.UseVisualStyleBackColor = true;
            this.btnStepBack.Click += new System.EventHandler(this.btnStepBack_Click);
            // 
            // btnNewGame
            // 
            this.btnNewGame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNewGame.BackColor = System.Drawing.Color.IndianRed;
            this.btnNewGame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnNewGame.ForeColor = System.Drawing.SystemColors.Desktop;
            this.btnNewGame.Location = new System.Drawing.Point(17, 445);
            this.btnNewGame.Name = "btnNewGame";
            this.btnNewGame.Size = new System.Drawing.Size(100, 67);
            this.btnNewGame.TabIndex = 12;
            this.btnNewGame.Text = "New Game";
            this.btnNewGame.UseVisualStyleBackColor = true;
            this.btnNewGame.Click += new System.EventHandler(this.btnNewGame_Click);
            // 
            // Lines
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lavender;
            this.ClientSize = new System.Drawing.Size(430, 526);
            this.Controls.Add(this.btnNewGame);
            this.Controls.Add(this.btnStepBack);
            this.Controls.Add(this.lblPath);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.lblTurn);
            this.Controls.Add(this.lblScore);
            this.Controls.Add(this.pbxGameBoard);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "Lines";
            this.Text = "Lines";
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
        private System.Windows.Forms.Button btnStepBack;
        private System.Windows.Forms.Button btnNewGame;
    }
}

