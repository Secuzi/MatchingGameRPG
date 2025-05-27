using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Matching_RPG.Abstract_Classes;
using Matching_RPG.FloorTypes;
using Matching_RPG.Implementation_Classes;

namespace Matching_RPG
{
	internal class EntitiesManager
	{
		List<GameObject> _gameObjects;
        public Form Display { get; set; }
		public EntitiesManager(Form display)
        {
            this.Display = display;
			_gameObjects = new List<GameObject>();
        }


        public void Init()
		{
			if (_gameObjects == null || _gameObjects.Count > 0)
			{
				MessageBox.Show("Game Objects property is already initialized!");
				return;
			}
			if (Display == null) 
			{

				MessageBox.Show("Display form is null!");
				return;
			}
			GameObject overworldWaterFloor = new GameFloor
			{
				ObjectWidth = 1280,
				ObjectHeight = 768,
				ObjectPositionX = 0,
				ObjectPositionY = 0,
				ObjectImage = Image.FromFile("Background/water.png")
				//1
			};
			GameObject overworldFloor = new GameFloor
			{
				ObjectWidth = 1144,
				ObjectHeight = 640,
				ObjectPositionX = Display.ClientSize.Width / 2 - (1144 / 2),
				ObjectPositionY = Display.ClientSize.Height / 2 - (640 / 2),
				ObjectImage = Image.FromFile("Background/bg.png")
				//2
			};
			GameObject overworldFenceTopLeftRight = new GameFloor
			{
				ObjectWidth = 1112,
				ObjectHeight = 620,
				ObjectPositionX = overworldFloor.ObjectPositionX + 12,
				ObjectPositionY = overworldFloor.ObjectPositionY,
				ObjectImage = Image.FromFile("Background/fencetopleftright.png")
				//3
			};

			GameObject overworldWaterRock = new GameFloor()
			{
				ObjectWidth = 64,
				ObjectHeight = 48,
				ObjectImage = Image.FromFile("Background/waterrock1.png"),
				ObjectPositionX = 0,
				ObjectPositionY = Display.ClientSize.Height - 48
				//4
			};

			GameObject overworldWaterRock2 = new GameFloor()
			{
				ObjectWidth = 40,
				ObjectHeight = 32,
				ObjectImage = Image.FromFile("Background/waterrock2.png"),
				ObjectPositionX = 20,
				ObjectPositionY = 128
				//5
			};
			GameObject overworldFenceBottom = new GameFloor
			{
				ObjectWidth = 1112,
				ObjectHeight = 620,
				ObjectPositionX = overworldFloor.ObjectPositionX + 12,
				ObjectPositionY = overworldFloor.ObjectPositionY,
				ObjectImage = Image.FromFile("Background/fencebottom.png")
				//6
			};
			Player player = new Player
			{
				ObjectHeight = 64,
				ObjectWidth = 64,
				ObjectPositionX = Display.ClientSize.Width / 2 - 64,
				ObjectPositionY = 556,
				PlayerName = "Harold",
				PlayerSpeed = 4
				//7
			};

			_gameObjects = new List<GameObject>
			{
				overworldWaterFloor,
				overworldWaterRock,
				overworldWaterRock2,
				overworldFloor,
				overworldFenceTopLeftRight,
				player,
				overworldFenceBottom
			};
			

		}
		
		public List<GameObject> GetGameObjects()
		{
			if ( _gameObjects == null )
			{
				MessageBox.Show("game objects property is null");
				return null;
			}
			return this._gameObjects;
		}
	}
}
