namespace Matching_RPG
{
	partial class Game
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
			this.components = new System.ComponentModel.Container();
			this.gameLoop = new System.Windows.Forms.Timer(this.components);
			this.cursorTimer = new System.Windows.Forms.Timer(this.components);
			this.animationTimer = new System.Windows.Forms.Timer(this.components);
			this.countdownTimer = new System.Windows.Forms.Timer(this.components);
			this.settingsTimer = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// gameLoop
			// 
			this.gameLoop.Interval = 16;
			this.gameLoop.Tick += new System.EventHandler(this.OnGameLoop);
			// 
			// cursorTimer
			// 
			this.cursorTimer.Enabled = true;
			this.cursorTimer.Tick += new System.EventHandler(this.OnCursorTimer);
			// 
			// animationTimer
			// 
			this.animationTimer.Tick += new System.EventHandler(this.OnAnimationTick);
			// 
			// settingsTimer
			// 
			this.settingsTimer.Interval = 16;
			this.settingsTimer.Tick += new System.EventHandler(this.OnSettingsTimerTick);
			// 
			// Game
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1264, 729);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "Game";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Load += new System.EventHandler(this.Game_Load);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.OnFormDraw);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OnKeyUp);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Timer gameLoop;
		private System.Windows.Forms.Timer cursorTimer;
		private System.Windows.Forms.Timer animationTimer;
		private System.Windows.Forms.Timer countdownTimer;
		private System.Windows.Forms.Timer settingsTimer;
	}
}