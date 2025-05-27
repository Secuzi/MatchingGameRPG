using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matching_RPG.Implementation_Classes
{
	public class SolidObject : GameObject
	{
		public void UpdateSolidHitBoxCoordinates(int positionX, int positionY)
		{
			this.SolidHitbox = new System.Drawing.Rectangle(positionX, positionY, this.SolidHitbox.Width, this.SolidHitbox.Height);
		}

	}
}
