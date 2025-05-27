using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matching_RPG.Implementation_Classes
{
	internal class CollisionChecker
	{
		public static bool CheckCollision(GameObject gameObject, GameObject otherGameObject)
		{

			//Computes the current player's position and factoring the width if it doesn't intersect with the other object's position whether the 
			//object is coming from the left, right, top, or bottom of the object.
			//If it doesn't intersect the object's position then it will return false
			if (gameObject.SolidHitbox.X + gameObject.SolidHitbox.Width <= otherGameObject.SolidHitbox.X ||
				gameObject.SolidHitbox.X >= otherGameObject.SolidHitbox.X + otherGameObject.SolidHitbox.Width ||
				gameObject.SolidHitbox.Y + gameObject.SolidHitbox.Height <= otherGameObject.SolidHitbox.Y ||
				gameObject.SolidHitbox.Y >= otherGameObject.SolidHitbox.Y + otherGameObject.SolidHitbox.Height)
			{
				//There is no collision
				return false;
			}
			return true;

		}
	}
}
