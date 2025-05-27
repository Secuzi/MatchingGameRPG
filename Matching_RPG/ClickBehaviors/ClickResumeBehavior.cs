using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Matching_RPG.Abstract_Classes;

namespace Matching_RPG.ClickBehaviors
{
	internal class ClickResumeBehavior : IClickable
	{
		public bool IsClickable { get; set; }
		Player Player{ get; set; }
		Icon Icon { get; set; }
		public ClickResumeBehavior(Player player, Icon icon)
		{
			Player = player;
			Icon = icon;
		}
		public void Click(object sender, MouseEventArgs e)
		{
			if (Icon.Length < 35)
			{
				Player.isSettingsActivated = false;

			}

		}
	}
}
