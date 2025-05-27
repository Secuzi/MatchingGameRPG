using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Matching_RPG.Implementation_Classes
{
	internal class GameTable : SolidObject
	{

		public GameTable(int x, int y, int width, int height)
		{
			ObjectImage = Image.FromFile("Table/table.png");

			ObjectPositionX = x;
			ObjectPositionY = y;
			ObjectWidth = width;
			ObjectHeight = height;
		}

		public void Draw(object sender, PaintEventArgs e)
		{
			e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
			e.Graphics.DrawImage(ObjectImage, ObjectPositionX, ObjectPositionY, ObjectWidth, ObjectHeight);
		}
	}
}
