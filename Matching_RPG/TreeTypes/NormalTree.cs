using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Matching_RPG.Implementation_Classes;

namespace Matching_RPG.TreeTypes
{
	internal class NormalTree : GameTree
	{
        readonly string path = "Trees/tree2.png";
        readonly int _defaultWidth = 96;
        readonly int _defaultHeight = 124;

		public NormalTree(int positionX, int positionY) : base(0, 0, positionX, positionY)
        {
            this.ObjectImage = Image.FromFile(path);
            this.ObjectHeight = _defaultHeight;
            this.ObjectWidth = _defaultWidth;
            this.SolidHitbox = new Rectangle(this.ObjectPositionX + 32, this.ObjectPositionY + 92, 32, 20);
		}
    }
}
