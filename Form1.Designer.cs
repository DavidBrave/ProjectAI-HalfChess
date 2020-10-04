namespace Project_AI_Half_Chess
{
    partial class Form1
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
            this.panel_game = new System.Windows.Forms.Panel();
            this.pictureBox_startGame = new System.Windows.Forms.PictureBox();
            this.panel_whiteTaken = new System.Windows.Forms.Panel();
            this.panel_blackTaken = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_startGame)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_game
            // 
            this.panel_game.BackColor = System.Drawing.Color.Silver;
            this.panel_game.Location = new System.Drawing.Point(12, 12);
            this.panel_game.Name = "panel_game";
            this.panel_game.Size = new System.Drawing.Size(320, 640);
            this.panel_game.TabIndex = 0;
            // 
            // pictureBox_startGame
            // 
            this.pictureBox_startGame.Image = global::Project_AI_Half_Chess.Properties.Resources.start_button_passive;
            this.pictureBox_startGame.Location = new System.Drawing.Point(387, 301);
            this.pictureBox_startGame.Name = "pictureBox_startGame";
            this.pictureBox_startGame.Size = new System.Drawing.Size(113, 59);
            this.pictureBox_startGame.TabIndex = 1;
            this.pictureBox_startGame.TabStop = false;
            // 
            // panel_whiteTaken
            // 
            this.panel_whiteTaken.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel_whiteTaken.Location = new System.Drawing.Point(387, 23);
            this.panel_whiteTaken.Name = "panel_whiteTaken";
            this.panel_whiteTaken.Size = new System.Drawing.Size(200, 100);
            this.panel_whiteTaken.TabIndex = 2;
            // 
            // panel_blackTaken
            // 
            this.panel_blackTaken.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel_blackTaken.Location = new System.Drawing.Point(387, 496);
            this.panel_blackTaken.Name = "panel_blackTaken";
            this.panel_blackTaken.Size = new System.Drawing.Size(200, 100);
            this.panel_blackTaken.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1182, 853);
            this.Controls.Add(this.panel_blackTaken);
            this.Controls.Add(this.panel_whiteTaken);
            this.Controls.Add(this.pictureBox_startGame);
            this.Controls.Add(this.panel_game);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_startGame)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_game;
        private System.Windows.Forms.PictureBox pictureBox_startGame;
        private System.Windows.Forms.Panel panel_whiteTaken;
        private System.Windows.Forms.Panel panel_blackTaken;
    }
}

