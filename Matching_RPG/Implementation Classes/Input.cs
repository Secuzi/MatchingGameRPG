using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Matching_RPG.Abstract_Classes;

namespace Matching_RPG.Implementation_Classes
{
	public class Input
	{

		Player _player;
		Dictionary<Keys, Action<Player>> _keyDownInput;
		Dictionary<Keys, Action<Player>> _keyUpInput;

		public Input()
		{
			_keyDownInput = new Dictionary<Keys, Action<Player>>()
			{
				{ Keys.W, player => player.GoUp = true },
				{ Keys.S, player => player.GoDown = true },
				{ Keys.A, player => player.GoLeft = true },
				{ Keys.D, player => player.GoRight = true },
			};

			_keyUpInput = new Dictionary<Keys, Action<Player>>() 
			{
				{ Keys.W, player => player.GoUp = false },
				{ Keys.S, player => player.GoDown = false },
				{ Keys.A, player => player.GoLeft = false },
				{ Keys.D, player => player.GoRight = false },
				{ Keys.Escape, player => { 
					if(player.isSettingsActivated == false)
					{
						player.isSettingsActivated = true;
					}
					else
					{
						player.isSettingsActivated = false;
					}
				
				} },
				{ Keys.G, player => player.TestingPurposes = true},
				{Keys.Enter, player => player.IsEnterPressed = true},
			};
		}
		public Dictionary<Keys, Action<Player>> GetKeyUpInput()
		{
			if (_keyUpInput == null)
			{
				return null;
			}
			return _keyUpInput;
		}
		public Dictionary<Keys, Action<Player>> GetKeyDownInput()
		{
			if (_keyDownInput == null)
			{
				return null;
			}
			return _keyDownInput;
		}

		public void RemoveKeyUpInput(Keys key)
		{
			if (_keyUpInput == null)
			{
				return;
			}
			if (_keyUpInput.ContainsKey(key))
			{
				_keyUpInput.Remove(key);
			}
		}
		public void AddKeyUpInput(Keys key, Action<Player> player)
		{
			if (player != null)
			{
				this._keyUpInput.Add(key, player);

			}
		}
		public void AddKeyDownInput(Keys key, Action<Player> player)
		{
			if (player != null)
			{
				this._keyDownInput.Add(key, player);

			}
		}

	}
}
