using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Matching_RPG.Abstract_Classes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Matching_RPG.Implementation_Classes
{
	public class GameLogic
	{
		List<string> cubyPath { get; set; }
		GameObject World { get; set; }
		List<Tile> CompletedTiles { get; set; }
		List<Cuby> Cubies { get; set; }
        public GameLogic()
        {
            
        }
        public List<Cuby> CreateCubies(int maxCubies)
		{
			Random rnd = new Random();
			var cubies = new List<Cuby>();
			var usedIds = new HashSet<int>(); // Set to store used IDs
		
			

			if (cubyPath is null)
			{
				return null;
			}

			for (int i = 0; i < maxCubies; i++)
			{
				int id;
				do
				{
					id = rnd.Next(0, 100); // Generate a random ID
				} while (usedIds.Contains(id)); // Check if ID is already used

				int skin = rnd.Next(0, 4);

				cubies.Add(new Cuby
				{
					ID = id,
					ObjectImage = Image.FromFile(cubyPath[skin]),
				});

				usedIds.Add(id); // Add the ID to the used IDs set
			}

			return cubies;

		}

		public void GetCubyPath(IEnumerable<string> cubyPath)
		{
			this.cubyPath = cubyPath.ToList() ;
		}
		public List<Tile> GetTiles(int numberOfTiles, int positionX, int positionY)
		{
			List<Tile> tiles = new List<Tile>();
			
			for (int i = 0; i < numberOfTiles / 3; i++)
			{
				for(int j = 0; j < numberOfTiles / 3; j++)
				{

					//Make the width and height constant
					tiles.Add(new Tile
					{
						ObjectPositionX = positionX,
						ObjectPositionY = positionY,
						SolidHitbox = new Rectangle(positionX, positionY, Tile.TileSize, Tile.TileSize),
						ObjectImage = Image.FromFile("Tile/tile.png")
					});
					positionX += Tile.TileSize;
				}
				positionX -= Tile.TileSize * 3;
				positionY += Tile.TileSize;
			}

			return tiles;
		}
		public Cuby GetNilget(List<Cuby> cubies, Player player, double instanceOfSkull)
		{
			Random rnd = new Random();
			Cuby cuby;
			if (cubies.Count == 0)
			{
				return cubies[0];
			}

			double skullChance = instanceOfSkull / cubies.Count;

			double generatedRandomValue = rnd.NextDouble();

			if (generatedRandomValue <= skullChance)
			{
				cuby = new Cuby()
				{
					ObjectImage = Image.FromFile(cubyPath[4])
				};
				player.IsPlayerHolding = true;
				cuby.IsPicked = true;
				cuby.IsSkull = true;
				return cuby;
			}


			do
			{
				cuby = cubies[rnd.Next(0, cubies.Count)];
				if (cuby.IsPicked == false)
				{
					cuby.IsPicked = true;
					player.IsPlayerHolding = true;
				
					break;
				}
				else
				{
					continue;
				}

			} while (cuby.IsPicked == true);
			return cuby;
		}
		public void SetPuzzle(List<Tile> tiles, List<Cuby> cubies, int numberOfTiles)
		{
			var rnd = new Random();
			for (int i = 0; i < numberOfTiles; i++)
			{
				tiles[i].ID = cubies[i].ID;
			}
		}

		public bool CheckIfPlayerLose(List<Tile> tiles, List<Cuby> placedCubies, bool isGameOver)
		{
			bool isPlacedCorrectly = false;
			if (tiles is null) { MessageBox.Show("tiles is null"); return false; }
			if (placedCubies is null) { MessageBox.Show("Placed cubies is false"); return false; }
			if (isGameOver) return false;
			for(int i = 0; i < tiles.Count; i++)
			{
				if (tiles[i].ID == tiles[i].PlacedCuby.ID)
				{
					isPlacedCorrectly = true;
					continue;
				}
				isPlacedCorrectly = false;
			}

			return isPlacedCorrectly;

		}

		
	}
}
