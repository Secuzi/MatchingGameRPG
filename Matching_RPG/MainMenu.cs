using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Matching_RPG.Abstract_Classes;
using Matching_RPG.ClickBehaviors;
using Matching_RPG.Implementation_Classes;
using Matching_RPG.Properties;

namespace Matching_RPG
{
	public partial class MainMenu : Form
	{
		DrawText titleButton1, titleButton2, playButton, tutorialButton, exitButton;
		GameMenu gameMenu;
		CustomFont customFont;
		GameMenuCat cat;
		GameTable table;
		Niglet greenNiglet, orangeNiglet, yellowNiglet, redNiglet;
		//public List<Action<object, PaintEventArgs>> paints { get; set; }
		public List<MouseEventHandler> Clicks { get; set; }
		public Timer GameTimer { get; set; }

		const int WIDTH = 1280;
		const int HEIGHT = 768;
		public MainMenu()
		{
			InitializeComponent();
			
		}
		void InitializeTimer()
		{
			GameTimer = gameTimer;
			gameTimer.Enabled = true;
			gameTimer.Start();
		}

		private void MainMenu_Load(object sender, EventArgs e)
		{
			Init();
		}

		void InitializeObjects()
		{
			this.Cursor = new Cursor(Resources.defaultIcon.Handle);
			gameMenu = new GameMenu(x: 0, y: 0, WIDTH, HEIGHT);
			customFont = new CustomFont(96);
			table = new GameTable(629, 365, 535, 260);
			titleButton1 = new DrawText(gameMenu, "FINDING", customFont, Color.FromArgb(236, 226, 107));
			titleButton1.ChangeFontSize(96);
			titleButton1.ChangeX(93);
			titleButton1.ChangeY(47);
			titleButton2 = new DrawText(gameMenu, "NIGLETS", customFont, Color.FromArgb(236, 226, 107));
			titleButton2.ChangeFontSize(96);
			titleButton2.ChangeX(93);
			titleButton2.ChangeY(136);
			playButton = new DrawText(gameMenu, "PLAY", customFont, Color.FromArgb(236, 226, 107));
			playButton.ChangeFontSize(64);
			playButton.ChangeX(93);
			playButton.ChangeY(354);
			playButton.SetTextBox(157, 59);
			//Initializes game levels
			playButton.AssignClickBehavior(new ClickPlayBehavior(playButton, new GameLevels(), this));
			tutorialButton = new DrawText(gameMenu, "TUTORIAL", customFont, Color.FromArgb(236, 226, 107));
			tutorialButton.ChangeFontSize(64);

			tutorialButton.ChangeX(93);
			tutorialButton.ChangeY(461);
			tutorialButton.SetTextBox(298, 59);

			tutorialButton.AssignClickBehavior(new ClickTutorialBehavior(tutorialButton, new GameTutorial(), this));

			exitButton = new DrawText(gameMenu, "EXIT", customFont, Color.FromArgb(236, 226, 107));
			exitButton.ChangeFontSize(64);
			exitButton.ChangeX(93);
			exitButton.ChangeY(568);
			exitButton.SetTextBox(148, 59);
			exitButton.AssignClickBehavior(new GoBackBehavior(this, exitButton));
			cat = new GameMenuCat(798, 209);
			greenNiglet = new Niglet(685, 456, 64, 64, "lightgreen-export");
			orangeNiglet = new Niglet(805, 456, 64, 64, "orange-export");
			yellowNiglet = new Niglet(925, 456, 64, 64, "ryellow-export");
			redNiglet = new Niglet(1045, 456, 64, 64, "red-export");
			Clicks = new List<MouseEventHandler>()
			{
				playButton.Click,
				tutorialButton.Click,
				exitButton.Click
			};
		
			AssignClicks();
			AssignPaints();	
		}
		void AssignClicks()
		{
			
			Clicks.ForEach(click => MouseUp += click);

		}
		void AssignPaints()
		{
			this.Paint += gameMenu.Draw;
			this.Paint += titleButton1.OnTextDrawPixels;
			this.Paint += titleButton2.OnTextDrawPixels;
			this.Paint += playButton.OnTextDrawPixels;
			this.Paint += tutorialButton.OnTextDrawPixels;
			this.Paint += exitButton.OnTextDrawPixels;
			this.Paint += cat.Draw;
			this.Paint += table.Draw;
			this.Paint += greenNiglet.Draw;
			this.Paint += orangeNiglet.Draw;
			this.Paint += yellowNiglet.Draw;
			this.Paint += redNiglet.Draw;
		}
		private void gameTimer_Tick(object sender, EventArgs e)
		{
			UpdateTick();
			this.Invalidate();
		}
		void UpdateTick()
		{
			Debug.WriteLine("Main menu update tick");
			cat.AnimatePlayer(0, 1);
		}
		void InitializeDisplay()
		{
			this.MaximizeBox = false;
			this.DoubleBuffered = true;
			this.BackgroundImageLayout = ImageLayout.Stretch;
			this.ClientSize = new Size(WIDTH, HEIGHT);
		}

		void Init()
		{
			InitializeDisplay();
			InitializeObjects();
			InitializeTimer();

		}
		
	}
}
