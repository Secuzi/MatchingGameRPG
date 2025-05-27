using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Matching_RPG
{
	internal interface IClickable
	{
		bool IsClickable { get; set; }
		void Click(object sender, MouseEventArgs e);


	}
}
