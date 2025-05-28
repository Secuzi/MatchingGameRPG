using System;
using System.Drawing;
using System.Windows.Forms;
using Matching_RPG.Abstract_Classes;

namespace Matching_RPG.Implementation_Classes
{
	internal class GameBin : SolidObject
	{
		public readonly int Width = 64;
		public readonly int Height = 64;
		public readonly string FilePath = @"trashbin.png";
		public readonly int PositionX = 1104;
		public readonly int PositionY = 392;
		public Cuby Cuby { get; set; }
		public Vector BinVector { get; set; }
		public Vector MouseVector { get; set; }
		public Player player { get; set; }
		public Vector playerVector { get; set; }
		public Game Display { get; set; }
		public GameTutorial TutorialDisplay { get; set; }

		public int fromPlayerTobin { get; set; }
		public Action ChestSpawnCallBack { get; set; }
		public int Scale { get; set; } = 1;
        public GameBin(Game display) 
        {
			const int xOffset = 12;
			const int yOffset = 8;
			const int widthOffset = 24;
			const int heightOffset = 12;
			this.ObjectImage = Image.FromFile(this.FilePath);
			this.SolidHitbox = new Rectangle(this.PositionX + xOffset, PositionY + yOffset, this.Width - widthOffset,  this.Height - heightOffset);
			this.ObjectWidth = Width;
			this.ObjectHeight = Height;
			this.ObjectPositionX = PositionX;
			this.ObjectPositionY = PositionY;
			this.Display = display;
			BinVector = new Vector(this.SolidHitbox.X + (SolidHitbox.Width / 2), SolidHitbox.Y + (SolidHitbox.Height / 2));

		}
		public GameBin(GameTutorial display, int scale)
		{
			Scale = scale;
		    int xOffset = 12 * scale;
			int yOffset = 8 * scale;
			int widthOffset = 24 * scale;
			int heightOffset = 12 * scale;
			this.ObjectImage = Image.FromFile(this.FilePath);
			this.SolidHitbox = new Rectangle(this.PositionX + xOffset, PositionY + yOffset, this.Width - widthOffset, this.Height - heightOffset);
			this.ObjectWidth = Width * scale;
			this.ObjectHeight = Height * scale;
			this.ObjectPositionX = PositionX;
			this.ObjectPositionY = PositionY;
			this.TutorialDisplay = display;
			BinVector = new Vector(this.SolidHitbox.X + (SolidHitbox.Width / 2), SolidHitbox.Y + (SolidHitbox.Height / 2));

		}
		public void Update(Cuby cuby,Vector playerVector)
		{
			this.playerVector = playerVector;
			fromPlayerTobin = (int)CalculateDistanceFromPlayerToTrashCan();

			if (cuby is null || playerVector is null)
			{
				return;
			}

			if (cuby.IsSkull)
			{
				this.Cuby = cuby;
			}

		}

		public double CalculateDistanceFromPlayerToTrashCan()
		{
			Vector fromPlayerToBinVector = BinVector - playerVector;
			return fromPlayerToBinVector.Length();
		}

		public void OnTrashClick(object sender, MouseEventArgs e)
		{
			
		
			MouseVector = new Vector(e.X, e.Y);
			var fromMouseToBinVector = BinVector - MouseVector;
			int length = (int)fromMouseToBinVector.Length();
			if (length > 50 || !player.CanPlayerPlace)
			{
				return;
			}
			if (ChestSpawnCallBack == null)
			{
				return;
			}
			if (Cuby.IsSkull)
			{
				Cuby = null;
			}
			player.IsPlayerHolding = false;
			ChestSpawnCallBack?.Invoke();
			Display.MouseUp -= OnTrashClick;
			Display.Paint -= Display.OnPlayerHold;

		}

		public void Draw(object sender, PaintEventArgs g)
		{
			if (this.ObjectImage != null )
			{
				g.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
				g.Graphics.DrawImage(this.ObjectImage, this.ObjectPositionX, ObjectPositionY, this.Width * Scale, this.Height * Scale);
			}
		}
	}
}
