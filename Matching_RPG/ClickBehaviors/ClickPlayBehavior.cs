using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Matching_RPG.Implementation_Classes;
using Matching_RPG.Properties;

namespace Matching_RPG.ClickBehaviors
{
	internal class ClickPlayBehavior : IClickable
	{
		public bool IsClickable { get; set; }
		GameLevels GameLevels { get; set; }
		MainMenu MainMenu { get; set; }
		DrawText Text { get; set; }
        public ClickPlayBehavior(DrawText text, GameLevels levels, MainMenu mainMenu)
        {
			this.Text = text;
			this.MainMenu = mainMenu;
			this.GameLevels = levels;
        }
        public void Click(object sender, MouseEventArgs e)
		{
			if (Text == null)
			{
				return;
			}
			if (Text.TextBox.IntersectsWith(new System.Drawing.Rectangle(e.X, e.Y, 32, 32)))
			{
				MainMenu.Hide();
				MainMenu.GameTimer.Stop();
				if (GameLevels.IsDisposed)
				{
					GameLevels = new GameLevels();
					GameLevels.Cursor = new Cursor(Resources.defaultIcon.Handle);
					GameLevels.ShowDialog();
				}
				else
				{
					GameLevels.Cursor = new Cursor(Resources.defaultIcon.Handle);

					GameLevels.ShowDialog();
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
