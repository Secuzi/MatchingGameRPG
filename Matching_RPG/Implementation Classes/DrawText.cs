using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Matching_RPG.Implementation_Classes
{
	internal class DrawText
	{
		public string Value { get; set; }
		public GameObject ParentRect { get; set; }
		public CustomFont Font { get; set; }
		public Color FontColor { get; set; }
		public float PositionX { get; set; }
		public float PositionY { get; set; }
        public int InitialPositionX { get; set; }
        public int InitialPositionY { get; set; }
		public Rectangle TextBox { get; set; }
		IClickable ClickBehavior { get; set; }
		public DrawText(GameObject parentObject, string value, CustomFont font, Color fontColor)
		{
			this.ParentRect = parentObject;
			this.Value = value;
			this.Font = font;
			this.FontColor = fontColor;
			this.PositionX = parentObject.ObjectPositionX;
			this.PositionY = parentObject.ObjectPositionY;
			this.InitialPositionX = parentObject.ObjectPositionX;
			this.InitialPositionY = parentObject.ObjectPositionY;
	
		}
		public void SetTextBox(int width, int height)
		{
			TextBox = new Rectangle((int)PositionX, (int)PositionY, width, height);
		}
		public void AssignClickBehavior(IClickable clickBehavior)
		{
			this.ClickBehavior = clickBehavior;
		}
		public void ChangeX(int x)
		{
			if (x <= 0)
			{
				this.PositionX = this.InitialPositionX;
			}
			this.PositionX += x;
		}

		public void ChangeY(int y)
		{
			if (y <= 0)
			{
				this.PositionY = this.InitialPositionY;
			}
			this.PositionY += y;
		}
		
		public void ChangeFontSize(int fontSize)
		{
			this.Font = new CustomFont(fontSize);
		}
		public void OnTextDrawPixels(object sender, PaintEventArgs e)
		{
			if (ParentRect == null)
			{
				return;
			}
			if (Font == null) { return; }
			SolidBrush solidBrush = new SolidBrush(FontColor);
			e.Graphics.DrawString(Value, Font.GetCustomFontInPixelUnit(), solidBrush, PositionX, PositionY);
		}

		public void OnTextDraw(object sender, PaintEventArgs e)
		{
			if (ParentRect == null)
			{
				return;
			}
			if (Font == null) { return; }
			SolidBrush solidBrush = new SolidBrush(FontColor);
			e.Graphics.DrawString(Value, Font.GetCustomFont(), solidBrush, PositionX, PositionY);
			
		}

		public void SetValue(string value)
		{
			this.Value = value;
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
	}
}
