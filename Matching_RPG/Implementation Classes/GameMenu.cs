using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Matching_RPG.Implementation_Classes
{
	internal class GameMenu : GameObject
	{

        public GameMenu(int x, int y, int width, int height)
        {
            ObjectImage = Image.FromFile("Background/menubg.png");

			ObjectPositionX = x;
            ObjectPositionY = y;
            ObjectWidth = width;
            ObjectHeight = height;
        }

        public void Draw(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(ObjectImage, ObjectPositionX, ObjectPositionY, ObjectWidth, ObjectHeight);
        }

    }
}
