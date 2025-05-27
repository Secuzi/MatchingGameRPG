using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Matching_RPG
{
	internal class SettingsView : GameObject
	{
		//3 objects, setting background, bars, 
        public SettingsView()
        {
			this.ObjectWidth = 361;
			this.ObjectHeight = 416;
			this.ObjectImage = Image.FromFile("Settings/settingSkeleton.png");
		}
		public void Draw(object sender, PaintEventArgs e)
		{
			e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
			e.Graphics.DrawImage(this.ObjectImage, this.ObjectPositionX, this.ObjectPositionY, this.ObjectWidth, this.ObjectHeight);
		}

	}
}
