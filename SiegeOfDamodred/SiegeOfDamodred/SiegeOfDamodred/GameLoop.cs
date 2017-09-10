using System;
using System.Collections.Generic;
using System.Linq;
using DebugLib;
using ExternalTypes;
using GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using SiegeOfDamodred;
using SpriteGenerator;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
namespace SiegeOfDamodred
{
	public class GameLoop : Microsoft.Xna.Framework.Game
	{

		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		private KeyboardState keyboard;
		private GameController game;
		public static bool isRollingCredits;
		private static float mScale;
		Video video;
		VideoPlayer player;
		Texture2D videoTexture;

		public static float Scale
		{
			get { return mScale; }
		}

		public GameLoop()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			Window.AllowUserResizing = true;
			graphics.PreferredBackBufferHeight = 900;
			graphics.PreferredBackBufferWidth = 1600;
			this.graphics.IsFullScreen = false;
			graphics.ApplyChanges();

		}

		protected override void Initialize()
		{
			game = new GameController();
			spriteBatch = new SpriteBatch(this.GraphicsDevice);
			UserInterfaceController.graphics = GraphicsDevice;

			game.content = this.Content;
			game.spriteBatch = this.spriteBatch;
			game.screenHeight = 900;
			game.screenWidth = 1600;

			base.Initialize();
		}

		protected override void LoadContent()
		{
			game.LoadGame();

			AudioController.Content = this.Content;
			AudioController.LoadAudioController();
			video = Content.Load<Video>("Credits");
			player = new VideoPlayer();  
		}

		protected override void UnloadContent()
		{
			AudioController.DisposeUnitStruckSoundEffectInstances();
		}

		protected override void Update(GameTime gameTime)
		{
			keyboard = Keyboard.GetState();
			game.gameTime = gameTime;

			game.Update();

			if (keyboard.IsKeyDown(Keys.Escape))
			{
				isRollingCredits = false;
			}

			if (isRollingCredits)
			{
				if (player.State == MediaState.Stopped)
				{
					player.IsLooped = true;
					player.Play(video);
				}
			}

			if (GameController.gameState == GameState.GAME_OVER || keyboard.IsKeyDown(Keys.F10))
			{
				Exit();
			}

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);
			GetFps(gameTime);

			UserInterfaceController.mHealthGlobeLiquid.ChangeHP(UserInterfaceController.mHealthGlobeLiquid.GlobeBoxPosition);
			UserInterfaceController.mHealthGlobeLiquid.GlobeBoxPosition = Vector2.Zero;
			UserInterfaceController.mManaGlobeLiquid.ChangeHP(UserInterfaceController.mManaGlobeLiquid.GlobeBoxPosition);
			UserInterfaceController.mManaGlobeLiquid.GlobeBoxPosition = Vector2.Zero;

			mScale = GraphicsDevice.PresentationParameters.BackBufferWidth / 1600f;
			spriteBatch.Begin(SpriteSortMode.Deferred, null, null, DepthStencilState.Default,
								RasterizerState.CullNone, null, Matrix.CreateScale(mScale, mScale, 1.0f));

			game.Draw();

			spriteBatch.End();

			if (isRollingCredits)
			{
				MediaPlayer.Stop();
				
				// Only call GetTexture if a video is playing or paused
				if (player.State != MediaState.Stopped)
					videoTexture = player.GetTexture();

				// Drawing to the rectangle will stretch the 
				// video to fill the screen
				Rectangle screen = new Rectangle(GraphicsDevice.Viewport.X,
					GraphicsDevice.Viewport.Y,
					GraphicsDevice.Viewport.Width,
					GraphicsDevice.Viewport.Height);

				// Draw the video, if we have a texture to draw.
				if (videoTexture != null)
				{
					spriteBatch.Begin();
					spriteBatch.Draw(videoTexture, screen, Color.White);
					spriteBatch.End();
				}
			}

			base.Draw(gameTime);

		}

		private void GetFps(GameTime gameTime)
		{
			float fps = 1 / (float)gameTime.ElapsedGameTime.TotalSeconds;

			this.Window.Title = fps.ToString() + " Siege of Damodred";
		}

	}

}