using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matching_RPG
{
	internal class Animate
	{
		/// <summary>
		/// After the delay seconds it will execute the callback function
		/// </summary>
		/// <param name="callBack"></param>
		/// <param name="delay"></param>
		/// <returns></returns>
		public async Task<bool> AnimateAsync(Action callBack, int delay)
		{
			await Task.Delay(delay);
			if (callBack == null)
			{
				return false;
			}
			callBack?.Invoke();
			return true;
		}
	}
}
