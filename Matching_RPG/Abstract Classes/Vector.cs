using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Matching_RPG.Implementation_Classes;

namespace Matching_RPG.Abstract_Classes
{
	public class Vector
	{
		public float X { get; set; }
		public float Y { get; set; }

        public int VectorLength { get; set; }

        public Vector(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }
        public Vector()
        {
            
        }


        public Point AddVector(GameObject gameObject)
        {
            return new Point(gameObject.ObjectPositionX + (int)this.X, gameObject.ObjectPositionY + (int)this.Y);
        }
        public static Vector operator -(Vector object1, Vector object2)
        {
            return new Vector(object1.X - object2.X, object1.Y - object2.Y);
        }

        public double Length()
        {
            double result = Math.Sqrt(X * X + Y * Y);

			VectorLength = (int)result;

			return result;
        }

    }
}
