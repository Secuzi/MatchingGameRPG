using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using Matching_RPG.Abstract_Classes;
namespace Matching_RPG.Implementation_Classes
{
	public class GameChest : SolidObject
	{
		public bool CanOpenChest { get; set; }
		public Form Display { get; set; }
		public List<GameObject> gameObjects;
		Vector MouseVector { get; set; }
		public Vector ChestVector { get; set; }
		public Player player { get; set; }
		public GameChest(int width, int height, Form display) 
        {
            this.ObjectWidth = width;
			this.ObjectHeight = height;
			this.SolidHitbox = new Rectangle(0, 0, width, height);
			this.Display = display;
        }

		public void RandomizeChestPosition(List<GameObject> gameObjects, int positionX, int positionY, int width, int height)
		{
			
			Random rnd = new Random();
			bool isChestPlaced = false;
			while (!isChestPlaced)
			{
				int quadrant = rnd.Next(1, 5);

				int finalX, finalY;
				switch (quadrant)
				{
					case 1:
						finalX = rnd.Next(positionX + 24, width / 2);
						finalY = rnd.Next(positionY, (height - 44) / 2);
						break;
					case 2:
						finalX = rnd.Next(width / 2, width);
						finalY = rnd.Next(positionY, (height - 44) / 2);
						break;
					case 3:
						finalX = rnd.Next(positionX + 24, width / 2);
						finalY = rnd.Next((height - 44) / 2, height - 44);
						break;
					case 4:
						finalX = rnd.Next(width / 2, width);
						finalY = rnd.Next((height - 44) / 2, height - 44);
						break;
					default:
						
						finalX = 0;
						finalY = 0;
						break;
				}

				this.ObjectPositionX = finalX;
				this.ObjectPositionY = finalY;

				this.SolidHitbox = new Rectangle(finalX, finalY, this.ObjectWidth, this.ObjectHeight);
				
				bool collisionDetected = false;
				foreach (var gameObject in gameObjects)
				{
					
					if (gameObject is SolidObject && CollisionChecker.CheckCollision(this, gameObject))
					{
						collisionDetected = true;
						break;
					}
				}
				if (CollisionChecker.CheckCollision(this, this.player))
				{
					collisionDetected = true;
				}
				if (!collisionDetected)
				{
					isChestPlaced = true;
				}
				
			}

		}
	}
}
