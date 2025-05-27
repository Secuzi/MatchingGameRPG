using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Matching_RPG.Abstract_Classes;

namespace Matching_RPG
{
	internal class Icon : GameObject, IClickable
	{
        public int InitialPositionX { get; set; }
		public int InitialPositionY { get; set; }
		public bool IsClickable { get; set; }
        public double? Length { get; set; }
		public IClickable ClickBehavior { get; set; }
        public string Name { get; set; }
        public Icon(GameObject parentObject, int objectWidth, int objectHeight, Image iconImage)
        {
			this.ObjectImage = iconImage;
			this.ObjectWidth = objectWidth;
			this.ObjectHeight = objectHeight;
			this.ObjectPositionX = parentObject.ObjectPositionX;
			this.ObjectPositionY = parentObject.ObjectPositionY;
			this.InitialPositionX = parentObject.ObjectPositionX;
			this.InitialPositionY = parentObject.ObjectPositionY;
			Length = null;
		}
		public void AssignClickBehavior(IClickable clickBehavior)
		{
			this.ClickBehavior = clickBehavior;
		}
		public void ChangeX(int x)
		{
			if (x <= 0)
			{
				this.ObjectPositionX = this.InitialPositionX;
			}
			this.ObjectPositionX += x;			
		}

		public void ChangeY(int y)
		{
			if (y <= 0)
			{
				this.ObjectPositionY = this.InitialPositionY;
			}
			this.ObjectPositionY += y;
		}
		public void Draw(object sender, PaintEventArgs e)
		{
			e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
			e.Graphics.DrawImage(this.ObjectImage, this.ObjectPositionX, this.ObjectPositionY, this.ObjectWidth, this.ObjectHeight);
		}

		public void Click(object sender, MouseEventArgs e)
		{
			//Put tag
			if (ClickBehavior == null)
			{
				return;
			}
			ClickBehavior.Click(sender, e);

		}

		public Vector GetCenterCoordinateOfAnIcon()
		{
			return new Vector(this.ObjectPositionX + (this.ObjectWidth / 2), this.ObjectPositionY +  (this.ObjectHeight / 2));
		}

	}
}
