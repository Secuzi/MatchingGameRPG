using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Matching_RPG.Implementation_Classes;

namespace Matching_RPG.ClickBehaviors
{
	internal class GoBackBehavior : IClickable
	{
		Form Display { get; set; }
		public bool IsClickable { get; set; }
		DrawText Text { get; set; }
		public GoBackBehavior(Form display, DrawText text)
        {
            Display = display;
			Text = text;
        }

		public void Click(object sender, MouseEventArgs e)
		{
			if (Text == null)
			{
				return;
			}
			if (Text.TextBox.IntersectsWith(new System.Drawing.Rectangle(e.X, e.Y, 32, 32)))
			{
				Display.Dispose();
			}


		}
	}
}
