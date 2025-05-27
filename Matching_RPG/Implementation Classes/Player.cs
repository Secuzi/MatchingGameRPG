using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Matching_RPG.Implementation_Classes;

namespace Matching_RPG.Abstract_Classes
{
	public class Player : GameObject
	{
	
		public bool GoLeft { get; set; }
		public bool GoRight { get; set; }
		public bool GoDown { get; set; }
		public bool GoUp { get; set; }
		public bool IsIdle { get; set; }
		public int PlayerSpeed { get; set; }
		public string PlayerName { get; set; }
		public Bitmap PlayerBitmapImage { get; set; }
		public bool IsPlayerHolding { get; set; }
		public bool CanPlayerPlace { get; set; }
		public List<string> PlayerMovements { get; set; }
		public bool TestingPurposes { get; set; }
        public bool IsEnterPressed { get; set; }

        public bool isSettingsActivated { get; set; }
        public Player()
        {
			isSettingsActivated = false;
        }

        //Violating single responsibility principle, might move this to the form itself...
        /// <summary>
        /// Get the sprite frames from a relative path
        /// </summary>
        /// <param name="path"></param>
        /// 
        public void GetSpriteFrames(string path)
		{
			this.PlayerMovements = Directory.GetFiles(path, "*.png").ToList();
		}

		public void GetPlayerImage()
		{
			if (PlayerMovements is null || PlayerMovements.Count == 0)
			{
				//For debugging purposes
				MessageBox.Show("Frames cannot be found");
				return;
			}
			//this.ObjectImage = Image.FromFile(this.PlayerMovements[0]);
			this.PlayerBitmapImage = new Bitmap(this.PlayerMovements[0]);
		}

	}
}
