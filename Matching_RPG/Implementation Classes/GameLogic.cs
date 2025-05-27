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
		List<string> NigletPath { get; set; }
		GameObject World { get; set; }
		List<Tile> CompletedTiles { get; set; }
		List<Niglet> Niglets { get; set; }
        public GameLogic()
        {
            
        }
        public List<Niglet> CreateNiglets(int maxNiglets)
		{
			Random rnd = new Random();
			var niglets = new List<Niglet>();
			var usedIds = new HashSet<int>(); // Set to store used IDs
		
			

			if (NigletPath is null)
			{
				return null;
			}

			for (int i = 0; i < maxNiglets; i++)
			{
				int id;
				do
				{
					id = rnd.Next(0, 100); // Generate a random ID
				} while (usedIds.Contains(id)); // Check if ID is already used

				int skin = rnd.Next(0, 4);

				niglets.Add(new Niglet
				{
					ID = id,
					ObjectImage = Image.FromFile(NigletPath[skin]),
				});

				usedIds.Add(id); // Add the ID to the used IDs set
			}

			return niglets;

		}

		public void GetNigletPath(IEnumerable<string> nigletPath)
		{
			this.NigletPath = nigletPath.ToList() ;
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
		public Niglet GetNilget(List<Niglet> niglets, Player player, double instanceOfSkull)
		{
			Random rnd = new Random();
			Niglet niglet;
			if (niglets.Count == 0)
			{
				return niglets[0];
			}

			double skullChance = instanceOfSkull / niglets.Count;

			double generatedRandomValue = rnd.NextDouble();

			if (generatedRandomValue <= skullChance)
			{
				niglet = new Niglet()
				{
					ObjectImage = Image.FromFile(NigletPath[4])
				};
				player.IsPlayerHolding = true;
				niglet.IsPicked = true;
				niglet.IsSkull = true;
				return niglet;
			}


			do
			{
				//Create a function or something na mo get sa percentage sa skull niglet
				niglet = niglets[rnd.Next(0, niglets.Count)];
				if (niglet.IsPicked == false)
				{
					niglet.IsPicked = true;
					player.IsPlayerHolding = true;
				
					break;
				}
				else
				{
					continue;
				}

			} while (niglet.IsPicked == true);
			return niglet;
		}
		public void SetPuzzle(List<Tile> tiles, List<Niglet> niglets, int numberOfTiles)
		{
			var rnd = new Random();
			for (int i = 0; i < numberOfTiles; i++)
			{
				tiles[i].ID = niglets[i].ID;
			}
		}

		public bool CheckIfPlayerLose(List<Tile> tiles, List<Niglet> placedNiglets, bool isGameOver)
		{
			bool isPlacedCorrectly = false;
			if (tiles is null) { MessageBox.Show("tiles is null"); return false; }
			if (placedNiglets is null) { MessageBox.Show("Placed niglets is false"); return false; }
			if (isGameOver) return false;
			for(int i = 0; i < tiles.Count; i++)
			{
				if (tiles[i].ID == tiles[i].PlacedNiglet.ID)
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
