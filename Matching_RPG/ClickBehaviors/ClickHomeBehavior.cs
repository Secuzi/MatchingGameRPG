using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Matching_RPG
{
	internal class ClickHomeBehavior : IClickable
	{
		public bool IsClickable { get; set; }
		Game GamePanel { get; set; }
		Icon Icon { get; set; }
		public ClickHomeBehavior(Game gamePanel, Icon icon)
		{
			GamePanel = gamePanel;
			Icon = icon;
		}

		public void Click(object sender, MouseEventArgs e)
		{
			if (Icon.Length < 35)
			{
				GamePanel.Dispose();
			}
		}
	}
}
