using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Matching_RPG.Implementation_Classes
{
	public class Cuby : SolidObject, IDisposable
	{
        public int ID { get; set; }
        public bool IsPicked { get; set; }

		public bool IsSkull { get; set; }


		public Cuby(int x, int y, int width, int height, string path)
		{
			ObjectImage = Image.FromFile($"Cubies/{path}.png");
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
		public Cuby()
        {
			int cubySize = 36;
			this.ObjectHeight = cubySize;
			this.ObjectWidth = cubySize;
            
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				// Dispose managed resources
				if (ObjectImage != null)
				{
					ObjectImage.Dispose();
					ObjectImage = null;
				}
			}
			// Dispose unmanaged resources
		}

		// Finalizer
		~Cuby()
		{
			Dispose(false);
		}
	}
}
