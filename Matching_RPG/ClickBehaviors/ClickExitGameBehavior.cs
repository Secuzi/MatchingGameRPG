using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Matching_RPG.ClickBehaviors
{
	internal class ClickExitGameBehavior : IClickable
	{
		public bool IsClickable { get; set; }
		GameLevels GameLevels { get; set; }
		Game GamePanel { get; set; }
		Icon Icon { get; set; }

		public ClickExitGameBehavior(GameLevels gameLevels, Game gamePanel, Icon icon)
		{
			GameLevels = gameLevels;
			GamePanel = gamePanel;
			Icon = icon;
		}

		public void Click(object sender, MouseEventArgs e)
		{
			if (Icon.Length < 35)
			{
				GameLevels.Dispose();
				GamePanel.Dispose();
			}
		}
	}
}
