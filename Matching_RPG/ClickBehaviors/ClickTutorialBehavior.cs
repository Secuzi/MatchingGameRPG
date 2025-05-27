using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Matching_RPG.Implementation_Classes;
using Matching_RPG.Properties;

namespace Matching_RPG.ClickBehaviors
{
	internal class ClickTutorialBehavior : IClickable
	{
		public bool IsClickable { get; set; }
		DrawText Text { get; set; }
		GameTutorial GameTutorial { get; set; }
        public MainMenu MainMenu { get; set; }
        public ClickTutorialBehavior(DrawText text, GameTutorial tutorial, MainMenu mainMenu)
		{
			this.Text = text;
			this.MainMenu = mainMenu;
			this.GameTutorial = tutorial;
		}

		public void Click(object sender, MouseEventArgs e)
		{
			if (Text.TextBox.IntersectsWith(new System.Drawing.Rectangle(e.X, e.Y, 32, 32)))
			{
				MainMenu.Hide();
				MainMenu.GameTimer.Stop();
				if (GameTutorial.IsDisposed)
				{
					GameTutorial = new GameTutorial();
					GameTutorial.Cursor = new Cursor(Resources.defaultIcon.Handle);
					GameTutorial.StartPosition = FormStartPosition.CenterScreen;
					GameTutorial.ShowDialog(); 
				}
				else
				{
					GameTutorial.ShowDialog();
				}
				if (!MainMenu.IsDisposed)
				{
					MainMenu.Show();
					MainMenu.GameTimer.Start();
				}
			}
		}
	}
}
