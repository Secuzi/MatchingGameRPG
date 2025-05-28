using Matching_RPG.ClickBehaviors;
using Matching_RPG.Implementation_Classes;
using Matching_RPG.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Matching_RPG
{
	public partial class GameLevels : Form
	{
		DrawText goBackButton, easyButton, normalButton, hardButton;
        const int WIDTH = 1280;
        const int HEIGHT = 720;
        CustomFont customFont;
        GameMenu gameMenu;
        public GameLevels()
		{
			InitializeComponent();
			
		}
        void Init()
        {
            InitializeDisplay();
            InitializeObjects();
		}

		void InitializeObjects()
		{
			this.Cursor = new Cursor(Resources.defaultIcon.Handle);
			gameMenu = new GameMenu(x: 0, y: 0, WIDTH, HEIGHT);
			customFont = new CustomFont(96);

			goBackButton = new DrawText(gameMenu, "GO BACK", customFont, Color.FromArgb(236, 226, 107))
			{
				PositionX = 39,
				PositionY = 26
			};

			goBackButton.ChangeFontSize(64);
			goBackButton.SetTextBox(256, 59);
			goBackButton.AssignClickBehavior(new GoBackBehavior(this, goBackButton));

			easyButton = new DrawText(gameMenu, "EASY", customFont, Color.FromArgb(236, 226, 107))
			{
                PositionX = 522,
                PositionY = 156
            };
			easyButton.ChangeFontSize(96);
			easyButton.SetTextBox(236, 89);
			easyButton.AssignClickBehavior(new ClickEasyGameBehavior(new Game(instanceOfSkulls: 1, numberOfCubies: 9, gameLevelsForm: this), this, easyButton));

            normalButton = new DrawText(gameMenu, "NORMAL", customFont, Color.FromArgb(236, 226, 107))
            {
                PositionX = 462,
                PositionY = 339
            };
            normalButton.ChangeFontSize(96);
            normalButton.SetTextBox(356, 89);
            normalButton.AssignClickBehavior(new ClickNormalGameBehavior(new Game(instanceOfSkulls: 3, numberOfCubies: 9, gameLevelsForm: this), this, normalButton));

            //
            hardButton = new DrawText(gameMenu, "HARD", customFont, Color.FromArgb(236, 226, 107))
            {
                PositionX = 525,
                PositionY = 522
            };
            hardButton.ChangeFontSize(96);
            hardButton.SetTextBox(231, 89);
            hardButton.AssignClickBehavior(new ClickHardGameBehavior(new Game(instanceOfSkulls: 6, numberOfCubies: 9, gameLevelsForm: this), this, hardButton));



            InitializeInputs();
			InitializePaints();



		}
		void InitializeInputs()
		{
			this.MouseUp += goBackButton.Click;
			this.MouseUp += easyButton.Click;
			this.MouseUp += normalButton.Click;
			this.MouseUp += hardButton.Click;
		}
		void InitializePaints()
		{
			this.Paint += gameMenu.Draw;
            this.Paint += goBackButton.OnTextDrawPixels;
			this.Paint += easyButton.OnTextDrawPixels;
			this.Paint += normalButton.OnTextDrawPixels;
			this.Paint += hardButton.OnTextDrawPixels;
        }

        void InitializeDisplay()
        {
            this.MaximizeBox = false;
            this.DoubleBuffered = true;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.ClientSize = new Size(WIDTH, HEIGHT);
        }



		private void GameLevels_Load(object sender, EventArgs e)
		{
			Init();
		}
	}
}
