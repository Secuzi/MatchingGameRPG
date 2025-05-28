using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Matching_RPG.Abstract_Classes;

namespace Matching_RPG.Implementation_Classes
{
	public class Tile : SolidObject
	{
		public static readonly int TileSize = 48;

		public int ID { get; set; }
		public Cuby PlacedCuby;
        public Tile()
        {
			
			this.ObjectWidth = TileSize;
			this.ObjectHeight = TileSize;

        }

      

	}
}
