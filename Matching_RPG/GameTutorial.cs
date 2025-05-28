using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Matching_RPG.ClickBehaviors;
using Matching_RPG.Implementation_Classes;
using Matching_RPG.Properties;

namespace Matching_RPG
{
	public partial class GameTutorial : Form
	{
		const int WIDTH = 1280;
		const int HEIGHT = 720;
		DrawText goBack, label1, label2, label3;
		GameBin trashcan;
		Cuby skullCuby;
		CustomFont customFont;
		GameMenu gameMenu;
		public GameTutorial()
		{
			InitializeComponent();
			Init();
		}
		void InitializeDisplay()
		{
			this.MaximizeBox = false;
			this.DoubleBuffered = true;
			this.BackgroundImageLayout = ImageLayout.Stretch;
			this.ClientSize = new Size(WIDTH, HEIGHT);
		}
		void InitializeObjects()
		{
			this.Cursor = new Cursor(Resources.defaultIcon.Handle);
			gameMenu = new GameMenu(x: 0, y: 0, WIDTH, HEIGHT);
			customFont = new CustomFont(96);
			goBack = new DrawText(gameMenu, "GO BACK", customFont, Color.FromArgb(236, 226, 107))
			{
				PositionX = 39,
				PositionY = 26
			};
			goBack.ChangeFontSize(64);
			label1 = new DrawText(gameMenu, "IF YOU SEE THIS GUY", customFont, Color.FromArgb(236, 226, 107))
			{
				PositionX = 97,
				PositionY = 173
			};
			goBack.SetTextBox(256, 59);
			goBack.AssignClickBehavior(new GoBackBehavior(this, goBack));

			label1.ChangeFontSize(96);
			label2 = new DrawText(gameMenu, "PUT HIM IN THE BIN", customFont, Color.FromArgb(236, 226, 107))
			{
				PositionX = 97,
				PositionY = 340
			};
			label2.ChangeFontSize(96);

			label3 = new DrawText(gameMenu, "OTHERWISE MATCH", customFont, Color.FromArgb(236, 226, 107))
			{
				PositionX = 97,
				PositionY = 507
			};
			label3.ChangeFontSize(96);

			skullCuby = new Cuby(1119, 186, 64, 64, "skull-export");

			trashcan = new GameBin(this, 2);
			trashcan.ObjectPositionX = 1023;
			trashcan.ObjectPositionY = 301;

			this.MouseUp += goBack.Click;

			this.Paint += gameMenu.Draw;
			this.Paint += trashcan.Draw;
			this.Paint += skullCuby.Draw;
			this.Paint += label1.OnTextDrawPixels;
			this.Paint += label2.OnTextDrawPixels;
			this.Paint += label3.OnTextDrawPixels;
			this.Paint += goBack.OnTextDrawPixels;
		}
		void Init()
		{
			InitializeDisplay();
			InitializeObjects();
		}



	}
}
