using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matching_RPG
{
	public abstract class GameObject : IDisposable
	{
		
	
		internal int ObjectPositionX { get; set; }
		internal int ObjectPositionY { get; set; }
		internal int ObjectWidth { get; set; }
		internal int ObjectHeight { get; set; }
		public Image ObjectImage { get; set; }
		
		public Rectangle SolidHitbox { get; set; }

        public GameObject()
        {
		
        }
        public virtual void GetImage(Image img)
		{
			
			ObjectImage = img;
		}
		public virtual void SetSolidHitbox(Action solidCallBack)
		{
			solidCallBack?.Invoke();
			//Implement this to child classes
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
		~GameObject()
		{
			Dispose(false);
		}
	}
}
