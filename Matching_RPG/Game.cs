using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Matching_RPG.Abstract_Classes;
using Matching_RPG.ClickBehaviors;
using Matching_RPG.Implementation_Classes;
using Matching_RPG.Properties;

namespace Matching_RPG
{
	public partial class Game : Form
	{
		public double InstanceOfSkulls { get; set; }
		public int NumberOfCubies { get; set; }
		GameLevels GameLevels { get; set; }
		Player player;
		int frameStep;
		int frameSpace;
		//By layer so first is the waterfloor, waterrock, waterrock2, then the overworldfloor, fence topleftright,
		//trees (doesn't matter kinsay mag una maybe), chest, player, fence bottom
		GameObject overworldFloor;
		GameObject overworldWaterFloor;
		GameObject overworldFenceBottom;
		GameObject overworldFenceTopLeftRight;
		GameObject overworldTree1;
		GameObject overworldWaterRock;
		GameObject overworldWaterRock2;
		GameObject overworldTree2;
		GameChest chest;
		GameBin trashcan;
		GameLogic gameLogic;
		Vector fromPlayerToChest;
		List<GameObject> gameObjects;
		List<Cuby> cubies;
		List<Cuby> placedCubies;
		List<Tile> tiles;
		List<Tile> completedTiles;
		List<Vector> fromPlayerToTiles;
		int distance = 100;
		EntitiesManager entitiesManager;
		bool isColliding;
		int centerTileDistance;
		Cuby currentCuby;
		int startFrame = 0;
		int endFrame = 0;
		Input playerInput;
		Vector chestVector;
		Vector playerVector;
		Vector mouseVector;
		CountDownTimer countDownTimerObject;
		CustomFont customFont;
		DrawText timerText;
		SettingsView settingsView;
		Icon characterFrame;
		Icon characterTorso;
		Icon homeButton;
		Icon resumeButton;
		Icon closeButton;
		Icon star1;
		Icon star2;
		Icon star3;
		bool isSettingsDraw;
		DrawText settingsText;
		DrawText performanceText;
		List<Icon> clickableIcons;
		bool IsGameOverAsyncOngoing;
		Animate animate;
		DrawText victoryText;
		DrawText defeatText;
		DrawText pressEnterText;
		bool isAnimationGoing;
		bool canPlayerExit;
		const int GameDuration = 120;

		public Game(int instanceOfSkulls, int numberOfCubies, GameLevels gameLevelsForm)
		{
			InitializeComponent();

			this.InstanceOfSkulls = instanceOfSkulls;
			this.NumberOfCubies = numberOfCubies;
			this.GameLevels = gameLevelsForm;
		}
		public void InitializeDisplay()
		{
			this.MaximizeBox = false;
			this.DoubleBuffered = true;
			this.BackgroundImageLayout = ImageLayout.Stretch;
			this.ClientSize = new Size(1280, 768);
			this.Cursor = new Cursor(Resources.mouseIcon.GetHicon());
		}
		public void InitializeGame()
		{
			entitiesManager = new EntitiesManager(this);
			countDownTimerObject = new CountDownTimer(countdownTimer, this);
			countDownTimerObject.CallBackFunction = GameOverAsync;

			InitializeDisplay();
			InitializePlayer();
			Init();
			InitializeLoop();
		}
		public void InitializeLoop()
		{
			gameLoop.Start();
			cursorTimer.Start();
			gameLoop.Interval = 8;
			animationTimer.Start();
			animationTimer.Interval = 1000 / 120;
			countdownTimer.Start();
			countDownTimerObject.Start(GameDuration);
			settingsTimer.Start();
			settingsTimer.Interval = 1000 / 60;
		}
		public void InitializePlayer()
		{
			playerInput = new Input();

			player = new Player
			{
				ObjectHeight = 64,
				ObjectWidth = 64,
				ObjectPositionX = this.ClientSize.Width / 2 - 64,
				ObjectPositionY = 556,
				PlayerName = "Harold",
				PlayerSpeed = 6//6
			};

			player.GetSpriteFrames("Player1Movements");
			player.GetPlayerImage();
			player.SolidHitbox = new Rectangle(player.ObjectPositionX + 20, player.ObjectPositionY + 40, 24, 20);
			player.IsIdle = true;
		}
		
		public void Init()
		{
			customFont = new CustomFont(32);
			clickableIcons = new List<Icon>();
			animate = new Animate();
			#region SettingComponents
			settingsView = new SettingsView();
			settingsView.ObjectPositionX = this.ClientSize.Width / 2 - settingsView.ObjectWidth / 2;
			settingsView.ObjectPositionY = this.ClientSize.Height / 2 - settingsView.ObjectHeight / 2;
			characterFrame = new Icon(settingsView, 120, 120, Image.FromFile("CharacterFrame/newFrame.png"));
			characterFrame.ChangeX(36);
			characterFrame.ChangeY(64);
			characterTorso = new Icon(characterFrame, 79, 73, Image.FromFile("icons/characterTorso.png"));
			characterTorso.ChangeX(21);
			characterTorso.ChangeY(24);
			homeButton = new Icon(settingsView, 66, 72, Image.FromFile("icons/homeButton.png"));
			homeButton.ChangeX(36);
			homeButton.ChangeY(244);
			homeButton.AssignClickBehavior(new ClickHomeBehavior(this, homeButton));
			homeButton.Name = "Home";
			clickableIcons.Add(homeButton);

			resumeButton = new Icon(settingsView, 66, 72, Image.FromFile("icons/playButton.png"));
			resumeButton.ChangeX(148);
			resumeButton.ChangeY(244);
			resumeButton.Name = "play";
			resumeButton.AssignClickBehavior(new ClickResumeBehavior(player, resumeButton));
			clickableIcons.Add(resumeButton);

			closeButton = new Icon(settingsView, 66, 72, Image.FromFile("icons/closeButton.png"));
			closeButton.ChangeX(259);
			closeButton.ChangeY(244);
			closeButton.Name = "Close";
			closeButton.AssignClickBehavior(new ClickExitGameBehavior(GameLevels, this, closeButton));
			clickableIcons.Add(closeButton);

			star1 = new Icon(settingsView, 48, 48, Image.FromFile("stars/starShadowEmpty.png"));
			star1.ChangeX(171);
			star1.ChangeY(100);
			star2 = new Icon(settingsView, 48, 48, Image.FromFile("stars/starShadowEmpty.png"));
			star2.ChangeX(227);
			star2.ChangeY(100);
			star3 = new Icon(settingsView, 48, 48, Image.FromFile("stars/starShadowEmpty.png"));
			star3.ChangeX(282);
			star3.ChangeY(100);
			settingsText = new DrawText(settingsView, "Settings", customFont, Color.White);
			settingsText.ChangeFontSize(24);
			settingsText.ChangeX(100);
			settingsText.ChangeY(13);
			performanceText = new DrawText(settingsView, "Performance", customFont, Color.White);
			performanceText.ChangeFontSize(14);
			performanceText.ChangeX(184);
			performanceText.ChangeY(79);
			#endregion


			trashcan = new GameBin(this);
			trashcan.ChestSpawnCallBack = ChestSpawn;
			gameObjects = new List<GameObject>();
			overworldFloor = new GameFloor
			{
				ObjectWidth = 1144,
				ObjectHeight = 640,
				ObjectPositionX = this.ClientSize.Width / 2 - (1144 / 2),
				ObjectPositionY = this.ClientSize.Height / 2 - (640 / 2),
				ObjectImage = Image.FromFile("Background/bg.png")
			};
			fromPlayerToChest = new Vector();

			overworldWaterFloor = new GameFloor
			{
				ObjectWidth = 1280,
				ObjectHeight = 768,
				ObjectPositionX = 0,
				ObjectPositionY = 0,
				ObjectImage = Image.FromFile("Background/water.png")
			};
			overworldFenceBottom = new GameFloor
			{
				ObjectWidth = 1112,
				ObjectHeight = 620,
				ObjectPositionX = overworldFloor.ObjectPositionX + 12,
				ObjectPositionY = overworldFloor.ObjectPositionY,
				ObjectImage = Image.FromFile("Background/fencebottom.png")
			};

			overworldFenceTopLeftRight = new GameFloor
			{
				ObjectWidth = 1112,
				ObjectHeight = 620,
				ObjectPositionX = overworldFloor.ObjectPositionX + 12,
				ObjectPositionY = overworldFloor.ObjectPositionY,
				ObjectImage = Image.FromFile("Background/fencetopleftright.png")
			};
			overworldTree1 = new GameTree(56, 120, overworldFenceTopLeftRight.ObjectWidth - 192, overworldFenceTopLeftRight.ObjectPositionY - 52)
			{
				ObjectImage = Image.FromFile("Trees/tree.png")
			};
			overworldTree1.SolidHitbox = new Rectangle(overworldTree1.ObjectPositionX + 16, overworldTree1.ObjectPositionY + 84, 23, 20);

			overworldWaterRock = new GameFloor()
			{
				ObjectWidth = 64,
				ObjectHeight = 48,
				ObjectImage = Image.FromFile("Background/waterrock1.png"),
				ObjectPositionX = 0,
				ObjectPositionY = this.ClientSize.Height - 48
			};

			overworldTree2 = new GameTree(96, 124, overworldFenceTopLeftRight.ObjectPositionX + 22, overworldFenceTopLeftRight.ObjectPositionY - 52)
			{
				ObjectImage = Image.FromFile("Trees/tree2.png")
			};

			overworldTree2.SolidHitbox = new Rectangle(overworldTree2.ObjectPositionX + 32, overworldTree2.ObjectPositionY + 92, 32, 20);
			overworldWaterRock2 = new GameFloor()
			{
				ObjectWidth = 40,
				ObjectHeight = 32,
				ObjectImage = Image.FromFile("Background/waterrock2.png"),
				ObjectPositionX = 20,
				ObjectPositionY = 128
			};

			chest = new GameChest(64, 56, this)
			{
				ObjectImage = Image.FromFile("Chest/chest.png"),
				player = player
			};
			gameLogic = new GameLogic();
			gameLogic.GetCubyPath(Directory.GetFiles("Cubies", "*.png").ToList());
			cubies = new List<Cuby>();
			cubies = gameLogic.CreateCubies(this.NumberOfCubies);
			currentCuby = new Cuby();
			currentCuby = null;
			placedCubies = new List<Cuby>();

			timerText = new DrawText(overworldWaterFloor, "", customFont, Color.FromArgb(200, 124, 134));
			timerText.ChangeX(overworldWaterFloor.ObjectWidth - 300);
			timerText.ChangeY(10);

			victoryText = new DrawText(overworldWaterFloor, "VICTORY!", customFont, Color.FromArgb(234, 225, 120))
			{
				PositionX = -1000,
				PositionY = this.ClientSize.Height / 2 - 111
			};
			victoryText.ChangeFontSize(128);

			defeatText = new DrawText(overworldWaterFloor, "DEFEAT!", customFont, Color.FromArgb(200, 124, 134))
			{
				PositionX = -1000,
				PositionY = this.ClientSize.Height / 2 - 111
			};
			defeatText.ChangeFontSize(128);

			pressEnterText = new DrawText(overworldWaterFloor, "PRESS THE ENTER KEY TO CONTINUE", customFont, Color.FromArgb(242, 242, 242))
			{
				PositionY = this.ClientSize.Height / 2 + 60
			};
			pressEnterText.ChangeFontSize(24);

			trashcan.player = player;
			gameObjects.Add(overworldWaterFloor);
			gameObjects.Add(overworldFloor);
			gameObjects.Add(overworldFenceTopLeftRight);
			gameObjects.Add(overworldTree2);
			gameObjects.Add(overworldTree1);
			gameObjects.Add(overworldWaterRock);
			gameObjects.Add(overworldWaterRock2);
			gameObjects.Add(trashcan);
			gameObjects.Add(player);
			tiles = new List<Tile>();
			tiles = gameLogic.GetTiles(9, overworldFenceBottom.ObjectWidth - 48 * 2, overworldFenceBottom.ObjectHeight - 48 * 3);
			tiles.ForEach(tile => gameObjects.Add(tile));


			fromPlayerToTiles = new List<Vector>(tiles.Count);
			for (int i = 0; i < tiles.Count; i++)
			{
				fromPlayerToTiles.Add(new Vector());
			}
			gameLogic.SetPuzzle(tiles, cubies, 9);
			//completedTiles = new 
			completedTiles = gameLogic.GetTiles(9, overworldFenceTopLeftRight.ObjectPositionX + 50, overworldFenceTopLeftRight.ObjectPositionY + 150);
		
			SupplyTilesWithCubies();
			completedTiles.ForEach(tile => gameObjects.Add(tile));

			chest.RandomizeChestPosition(gameObjects, overworldFenceTopLeftRight.ObjectPositionX,
				overworldFenceTopLeftRight.ObjectPositionY, overworldFenceTopLeftRight.ObjectWidth, overworldFenceTopLeftRight.ObjectHeight);

			gameObjects.Add(chest);
			this.Events.Dispose();
			this.KeyDown += OnKeyDown;
			this.KeyUp += OnKeyUp;
			this.MouseUp += OnMouseUp;
			this.Paint += OnFormDraw;
			this.Paint += CompletedCubiesDraw;
			this.Paint += trashcan.Draw;
			this.Paint += OnPlayerDraw;
			this.Paint += OnLowerFenceDraw;
			this.Paint += timerText.OnTextDraw;

			
		}

		private void SupplyTilesWithCubies()
		{
			for (int i = 0; i < completedTiles.Count; i++)
			{
				Cuby tempCuby = new Cuby()
				{
					ObjectImage = cubies[i].ObjectImage,
					ObjectPositionX = completedTiles[i].ObjectPositionX,
					ObjectPositionY = completedTiles[i].ObjectPositionY,

				};
				completedTiles[i].PlacedCuby = tempCuby;

			}
		}

		private void CompletedCubiesDraw(object sender, PaintEventArgs e)
		{
			completedTiles.ForEach(tile =>
			{
				if (tile != null && tile.ObjectImage != null && tile.PlacedCuby != null && tile.PlacedCuby.ObjectImage != null)
				{
					e.Graphics.DrawImage(tile.PlacedCuby.ObjectImage, tile.ObjectPositionX + tile.PlacedCuby.ObjectWidth / 5, tile.ObjectPositionY + tile.PlacedCuby.ObjectHeight / 5, tile.PlacedCuby.ObjectWidth, tile.PlacedCuby.ObjectHeight);
				}
				
			});
		}
		private void OnPlayerDraw(object sender, PaintEventArgs e)
		{
			if (player != null && player.PlayerBitmapImage != null)
			{
				e.Graphics.DrawImage(player.PlayerBitmapImage, player.ObjectPositionX,
				player.ObjectPositionY, player.ObjectWidth, player.ObjectHeight);
			}
			
		}
		private void OnLowerFenceDraw(object sender, PaintEventArgs e)
		{
			GameObject fenceBottom = overworldFenceBottom;
			if (overworldFenceBottom != null)
			{
				e.Graphics.DrawImage(fenceBottom.ObjectImage, fenceBottom.ObjectPositionX,
				fenceBottom.ObjectPositionY, fenceBottom.ObjectWidth, fenceBottom.ObjectHeight);
			}
			
		}
		private void OnFormDraw(object sender, PaintEventArgs e)
		{
			foreach (GameObject gameObject in gameObjects)
			{
				if (gameObject != null && gameObject.ObjectImage != null)
				{
					if (gameObject is GameChest && gameObject.ObjectImage == null)
					{
						continue;
					}
					
					e.Graphics.DrawImage(gameObject.ObjectImage, gameObject.ObjectPositionX,
					gameObject.ObjectPositionY, gameObject.ObjectWidth, gameObject.ObjectHeight);

				}

			}	
		}
		private void OnSemiTransparentRectangleDraw(object sender, PaintEventArgs e)
		{
			Color transparentColor = Color.FromArgb(128, Color.Black);
			SolidBrush brush = new SolidBrush(transparentColor);
			e.Graphics.FillRectangle(brush, 0, 0, this.ClientSize.Width, this.ClientSize.Height);
		}
		private void GetStars()
		{
			if (placedCubies.Count <= 0)
			{
				return;
			}
			switch (placedCubies.Count % 3)
			{
				case 2:
					if (placedCubies.Count <= 3)
					{
						star1.ObjectImage = Image.FromFile("stars/starShadowHalf.png");
					}
					else if (placedCubies.Count > 3 && placedCubies.Count <= 6)
					{
						star2.ObjectImage = Image.FromFile("stars/starShadowHalf.png");
					}
					else
					{
						star3.ObjectImage = Image.FromFile("stars/starShadowHalf.png");
					}
					break;
				case 0:

					if (placedCubies.Count <= 3)
					{
						star1.ObjectImage = Image.FromFile("stars/starShadowFilled.png");
					}
					else if (placedCubies.Count > 3 && placedCubies.Count <= 6)
					{
						star2.ObjectImage = Image.FromFile("stars/starShadowFilled.png");
					}
					else
					{
						star3.ObjectImage = Image.FromFile("stars/starShadowFilled.png");
					}
					break;
			}
		}
		private void ToggleSetting()
		{
			if (player.isSettingsActivated && isSettingsDraw == false)
			{
				this.Paint += OnSemiTransparentRectangleDraw;
				this.Paint += settingsView.Draw;
				this.Paint += characterFrame.Draw;
				this.Paint += characterTorso.Draw;
				clickableIcons.ForEach(icon =>
				{
					this.Paint += icon.Draw;
					this.MouseClick += icon.Click;
				});
				this.Paint += star1.Draw;
				this.Paint += star2.Draw;
				this.Paint += star3.Draw;
				this.Paint += settingsText.OnTextDraw;
				this.Paint += performanceText.OnTextDraw;
				
				this.MouseMove += OnMouseMove;
				this.countdownTimer.Stop();
				
				isSettingsDraw = true;
			}
			else if (!player.isSettingsActivated && isSettingsDraw == true)
			{
				this.Paint -= OnSemiTransparentRectangleDraw;
				this.Paint -= settingsView.Draw;
				this.Paint -= characterFrame.Draw;
				this.Paint -= characterTorso.Draw;
				clickableIcons.ForEach(icon =>
				{
					this.Paint -= icon.Draw;
					this.MouseClick -= icon.Click;
				});
				this.Paint -= star1.Draw;
				this.Paint -= star2.Draw;
				this.Paint -= star3.Draw;
				this.Paint -= settingsText.OnTextDraw;
				this.Paint -= performanceText.OnTextDraw;
				this.MouseMove -= OnMouseMove;
				this.countdownTimer.Start();

				isSettingsDraw = false;
			
			}
		}
		private void OnSettingsTimerTick(object sender, EventArgs e)
		{
			ToggleSetting();
			GetStars();

		}
		private void UpdateTick()
		{
			
			if (canPlayerExit && player.IsEnterPressed)
			{
				chest = null;
				currentCuby = null;
				gameLoop.Stop();
				cursorTimer.Stop();
				animationTimer.Stop();
				placedCubies.Clear();
				cubies.Clear();
				cubies.ForEach(cuby => cuby.Dispose());
				gameObjects.ForEach(gameObject => gameObject.Dispose());
				player.Dispose();
				tiles.ForEach(tile => tile.Dispose());
				placedCubies.ForEach(placedCuby => placedCuby.Dispose());

				foreach (Control control in Controls)
				{
					control.Dispose();
				}
				this.Dispose();
			}
			//displayTimeLbl.Text = $"{countDownTimerObject.Minute}:{countDownTimerObject.Seconds}";
			if (countDownTimerObject.Seconds >= 10)
			{
				timerText.SetValue($"Time: {countDownTimerObject.Minute}:{countDownTimerObject.Seconds}");
			}
			else
			{
				timerText.SetValue($"Time: {countDownTimerObject.Minute}:0{countDownTimerObject.Seconds}");
			}
			this.Text = $"{player.ObjectPositionX}, {player.ObjectPositionY}";
			if (player.IsPlayerHolding)
			{
				int newCubyPosX = player.ObjectPositionX + player.ObjectWidth / 2 - (currentCuby.ObjectWidth / 2);
				int newCubyPosy = player.ObjectPositionY - currentCuby.ObjectHeight;

				currentCuby.UpdateSolidHitBoxCoordinates(newCubyPosX, newCubyPosy);
			}
	

			if (player.IsIdle)
			{
				startFrame = 0;
				endFrame = 1;
			}

			foreach (var gameObject in gameObjects)
			{
				Player temptPlayer = new Player();
				Rectangle newSolidHitbox = player.SolidHitbox;

				if (player.GoLeft)
				{
					newSolidHitbox.X -= player.PlayerSpeed;
				}
				else if (player.GoRight)
				{
					newSolidHitbox.X += player.PlayerSpeed;
				}
				else if (player.GoDown)
				{
					newSolidHitbox.Y += player.PlayerSpeed;
				}
				else if (player.GoUp)
				{
					newSolidHitbox.Y -= player.PlayerSpeed;
				}
				temptPlayer.SolidHitbox = newSolidHitbox;
				if (gameObject is SolidObject && CollisionChecker.CheckCollision(temptPlayer, gameObject))
				{
					isColliding = true;
					player.IsIdle = true;
					break;
				}

				isColliding = false;
			}
			if (player.TestingPurposes)
			{
				GameOverAsync();
				player.TestingPurposes = false;
			}


			if (!isColliding)
			{
				HandleMovement();
			}
			player.SolidHitbox = new Rectangle(player.ObjectPositionX + 20, player.ObjectPositionY + 40, 24, 20);
			
			playerVector = new Vector(player.SolidHitbox.X + (player.SolidHitbox.Width / 2), player.SolidHitbox.Y + (player.SolidHitbox.Height / 2));
			
			trashcan.Update(currentCuby, playerVector);
			
			if (chest != null)
			{
				chest.player = player;

				chestVector = new Vector(chest.SolidHitbox.X + (chest.SolidHitbox.Width / 2), chest.SolidHitbox.Y + (chest.SolidHitbox.Height / 2));
				fromPlayerToChest = chestVector - playerVector;
				chest.ChestVector = chestVector;
			}
			//Center of the tiles			
			
			Vector centerTileVector = new Vector(tiles[4].SolidHitbox.X + (tiles[4].SolidHitbox.Width / 2), tiles[4].SolidHitbox.Y + (tiles[4].SolidHitbox.Height / 2));
			Vector fromPlayerToCenterTileVector = centerTileVector - playerVector;
			centerTileDistance = (int)fromPlayerToCenterTileVector.Length();

			distance = (int)fromPlayerToChest.Length();
			if (placedCubies.Count == cubies.Count && !IsGameOverAsyncOngoing)
			{
				GameOverAsync();
			}
		}
		private async Task<bool> GameOverLogicAsync()
		{
			IsGameOverAsyncOngoing = true;
			await Task.Delay(500);
			
			chest = null;
			
			bool ifWon = gameLogic.CheckIfPlayerLose(tiles, placedCubies, countDownTimerObject.isGameOver);

			return ifWon;
		}
		private async void GameOverAsync()
		{
			bool isPlayerWin = await GameOverLogicAsync();
			this.Paint += OnSemiTransparentRectangleDraw;
			isAnimationGoing = true;
			playerInput.RemoveKeyUpInput(Keys.Escape);
			if (isPlayerWin)
			{
				await animate.AnimateAsync(VictoryAnimation, 500);
				
			}
			else
			{

				await animate.AnimateAsync(DefeatAnimation, 500);
				
			}
			canPlayerExit = true;

		}
		public async void VictoryAnimation()
		{
			pressEnterText.PositionX = victoryText.PositionX + 120;
			this.Paint += pressEnterText.OnTextDraw;


			this.Paint += victoryText.OnTextDraw;
			while (victoryText.PositionX < (ClientSize.Width / 2) - 793 / 2)
			{
				victoryText.ChangeX(15);
				pressEnterText.ChangeX(15);
				await Task.Delay(1);
			}
		}
		public async void DefeatAnimation()
		{
			pressEnterText.PositionX = defeatText.PositionX + 65;
			this.Paint += pressEnterText.OnTextDraw;

			this.Paint += defeatText.OnTextDraw;
			while(defeatText.PositionX <  ClientSize.Width / 2 - 690 / 2)
			{
				defeatText.ChangeX(15);
				pressEnterText.ChangeX(15);
				await Task.Delay(1);
			}
		}

		void HandleMovement()
		{
			if (isSettingsDraw == true || isAnimationGoing)
			{
				player.IsIdle = true;
				startFrame = 0;
				endFrame = 1;
				return;
			}

			if (player.GoLeft && player.GoRight || player.GoUp && player.GoDown)
			{
				player.IsIdle = true;
				startFrame = 0;
				endFrame = 1;
				return;
			}

			if (CanMoveRight() && player.GoRight)
			{
				player.IsIdle = false;
				player.ObjectPositionX += player.PlayerSpeed;
				startFrame = 14;
				endFrame = 15;
			}
			else if (CanMoveLeft() && player.GoLeft)
			{
				player.IsIdle = false;
				player.ObjectPositionX -= player.PlayerSpeed;
				startFrame = 10;
				endFrame = 11;
			}
			else if (CanMoveUp() && player.GoUp)
			{
				player.IsIdle = false;
				player.ObjectPositionY -= player.PlayerSpeed;
				startFrame = 6;
				endFrame = 7;
			}
			else if (CanMoveDown() && player.GoDown)
			{
				player.IsIdle = false;
				player.ObjectPositionY += player.PlayerSpeed;
				startFrame = 2;
				endFrame = 3;
			}
		}

		bool CanMoveRight()
		{
			return player.ObjectPositionX < overworldFenceBottom.ObjectWidth && player.GoRight;
		}

		bool CanMoveLeft()
		{
			return player.ObjectPositionX > overworldFenceBottom.ObjectPositionX + 10 && player.GoLeft;
		}

		bool CanMoveUp()
		{
			return player.ObjectPositionY > overworldFenceBottom.ObjectPositionY && player.GoUp;
		}

		bool CanMoveDown()
		{
			return player.ObjectPositionY < overworldFenceBottom.ObjectHeight && player.GoDown;
		}

		private void AnimatePlayer(int startFrame, int endFrame)
		{
			
			frameSpace++;
			if (frameSpace == 15)
			{
				frameStep++;
				frameSpace = 0;
			}
			if(frameStep > endFrame || frameStep < startFrame)
			{
				frameStep = startFrame;
			}
		

			// Load the next frame from preloaded images
			player.PlayerBitmapImage = new Bitmap(player.PlayerMovements[frameStep]);
		}
	
		private void OnGameLoop(object sender, EventArgs e)
		{
			//Update
			UpdateTick();
			//Draw
			this.Invalidate();


		}


		private void OnKeyDown(object sender, KeyEventArgs e)
		{
			var keyDownInputs = playerInput.GetKeyDownInput();
			if (keyDownInputs.ContainsKey(e.KeyCode))
			{
				keyDownInputs[e.KeyCode].Invoke(player);
				player.IsIdle = false;
			}

		}

		private void OnKeyUp(object sender, KeyEventArgs e)
		{
			var keyUpInputs = playerInput.GetKeyUpInput();
			if (keyUpInputs.ContainsKey(e.KeyCode))
			{
				keyUpInputs[e.KeyCode].Invoke(player);
				player.IsIdle = true;
			}


			//player.IsIdle = true;
			
		}

	
		private void OnCursorTimer(object sender, EventArgs e)
		{
			if (chest is null)
			{

				return;

			}
			if (distance < 60 && !player.IsPlayerHolding)
			{
				this.Cursor = new Cursor(Resources.interactableIcon.Handle);

				chest.CanOpenChest = true;
			}
			else if (centerTileDistance < 120 && player.IsPlayerHolding && currentCuby.IsSkull == false)
			{
				//Make a delegate for this mo return ug cursor and mo 
				this.Cursor = new Cursor(Resources.interactableIcon.Handle);
				player.CanPlayerPlace = true;
			}
			else if (trashcan.fromPlayerTobin < 60 && player.IsPlayerHolding)
			{
				this.Cursor = new Cursor(Resources.interactableIcon.Handle);

				player.CanPlayerPlace = true;
			}
			else 
			{
				this.Cursor = new Cursor(Resources.defaultIcon.Handle);
				player.CanPlayerPlace = false;
			}
		}
		private void OnTileClick(object sender, MouseEventArgs e)
		{
			if (!player.CanPlayerPlace || isSettingsDraw)
			{
				return;
			}
			for (int i = 0; i < tiles.Count; i++)
			{

				int tileX = tiles[i].SolidHitbox.X + (tiles[i].SolidHitbox.Width / 2);
				int tileY = tiles[i].SolidHitbox.Y + (tiles[i].SolidHitbox.Height / 2);
				fromPlayerToTiles[i].X = tileX - e.X;
				fromPlayerToTiles[i].Y = tileY - e.Y;
				fromPlayerToTiles[i].Length();

				if (fromPlayerToTiles[i].VectorLength > 40)
				{
					continue;
				}

				if (tiles[i].ID == currentCuby.ID)
				{
					mouseVector.X = tiles[i].ObjectPositionX + (tiles[i].SolidHitbox.Width / 2) - (currentCuby.ObjectWidth / 2);
					mouseVector.Y = tiles[i].ObjectPositionY + (tiles[i].SolidHitbox.Height / 2) - currentCuby.ObjectHeight / 2;
					currentCuby.UpdateSolidHitBoxCoordinates((int)mouseVector.X, (int)mouseVector.Y);
					currentCuby.ObjectPositionX = currentCuby.SolidHitbox.X;
					currentCuby.ObjectPositionY = currentCuby.SolidHitbox.Y;
					placedCubies.Add(currentCuby);
					tiles[i].PlacedCuby = currentCuby;
					player.IsPlayerHolding = false;
					this.MouseUp -= OnTileClick;
					this.Paint -= OnPlayerHold;
					gameObjects.Insert(gameObjects.Count - 1, currentCuby);

					if (placedCubies.Count < 9)
					{
						ChestSpawn();
					}



				}
				break;

			}
		}
		public void OnPlayerHold(object senderObject, PaintEventArgs paint)
		{

			if (currentCuby != null && currentCuby.ObjectImage != null)
			{
				paint.Graphics.DrawImage(currentCuby.ObjectImage, currentCuby.SolidHitbox.X,
			currentCuby.SolidHitbox.Y, currentCuby.ObjectWidth, currentCuby.ObjectHeight);

			}

		}

		private void OnMouseUp(object sender, MouseEventArgs e)
		{
			Random rnd = new Random();


			mouseVector = new Vector(e.X, e.Y);
			Vector fromChestToMouseVector = chestVector - mouseVector;
			if (chest is null)
			{
				return;
			}

			int length = (int)fromChestToMouseVector.Length();
			if (!chest.CanOpenChest || length > 50)
			{
				
				return;
			}
			if (player.IsPlayerHolding)
			{
				return;
			}
			if (placedCubies.Count < cubies.Count)
			{
				currentCuby = gameLogic.GetNilget(cubies, player, this.InstanceOfSkulls);
				
			}
			else
			{
				gameObjects.Remove(chest);
				chest = null;
				return;
			}
			chest.ObjectImage = null;
			chest.SolidHitbox = new Rectangle();
			if (currentCuby.IsSkull)
			{
				this.MouseUp += trashcan.OnTrashClick;
			}
			else
			{
				this.MouseUp += OnTileClick;
			}
			this.Paint += OnPlayerHold;
		}

		private void ChestSpawn()
		{
			int chestIndex = gameObjects.IndexOf(chest);
			gameObjects.Remove(chest);
			chest = new GameChest(64, 56, this)
			{
				ObjectImage = Image.FromFile("Chest/chest.png"),
				player = player
			};
			chest.RandomizeChestPosition(gameObjects, overworldFenceTopLeftRight.ObjectPositionX,
				overworldFenceTopLeftRight.ObjectPositionY, overworldFenceTopLeftRight.ObjectWidth, overworldFenceTopLeftRight.ObjectHeight);
			gameObjects.Insert(chestIndex, chest);
		}

		private void OnAnimationTick(object sender, EventArgs e)
		{
			AnimatePlayer(startFrame, endFrame);
		}
		//Assign this event when IsSettings is true to reduce performance issues
		private void OnMouseMove(object sender, MouseEventArgs e)
		{
			//calculate the lengths for each icon
			clickableIcons.ForEach(icon =>
			{
				Vector mousePosition = new Vector(e.X, e.Y);
				Vector objectPosition = icon.GetCenterCoordinateOfAnIcon();
				Vector fromMouseToObject = objectPosition - mousePosition;
				icon.Length = fromMouseToObject.Length();
			});
		}

		private void Game_Load(object sender, EventArgs e)
		{
			InitializeGame();
		}
	}
}
