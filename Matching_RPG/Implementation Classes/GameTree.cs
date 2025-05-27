using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Matching_RPG.Implementation_Classes
{
	internal class GameTree : SolidObject
	{
		
        public GameTree(int width, int height, int positionX, int positionY)
        {
			this.ObjectWidth = width;
			this.ObjectHeight = height;
			this.ObjectPositionX = positionX;
			this.ObjectPositionY = positionY;
        }
		
        public GameTree()
        {
            
        }

		public override void SetSolidHitbox(Action solidCallBack)
		{
			base.SetSolidHitbox(solidCallBack);
		}

		public override void GetImage(Image img)
		{
			this.ObjectImage = img;
		}
	}
}
