using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Matching_RPG.Abstract_Classes;
using System.Xml.Linq;

namespace Matching_RPG
{
	internal class GameMenuCat : GameObject
	{
		int frameSpace;
		int frameStep;
		List<Image> images;
		public GameMenuCat(int x, int y)
		{
			ObjectImage = Image.FromFile("MenuCat/EMOT1.png");
			
			images = new List<Image>()
			{
				Image.FromFile("MenuCat/EMOT1.png"),
				Image.FromFile("MenuCat/EMOT2.png")

			};
			ObjectPositionX = x;
			ObjectPositionY = y;
			ObjectWidth = 159;
			ObjectHeight = 159;
		}

		public void Draw(object sender, PaintEventArgs e)
		{
			e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
			e.Graphics.DrawImage(ObjectImage, ObjectPositionX, ObjectPositionY, ObjectWidth, ObjectHeight);
		}

		public void AnimatePlayer(int startFrame, int endFrame)
		{

			frameSpace++;
			if (frameSpace == 15)
			{
				frameStep++;
				frameSpace = 0;
			}
			if (frameStep > endFrame || frameStep < startFrame)
			{
				frameStep = startFrame;
			}
			// Load the next frame from preloaded images
			ObjectImage = new Bitmap(images[frameStep]);
		}
		
	}
}
