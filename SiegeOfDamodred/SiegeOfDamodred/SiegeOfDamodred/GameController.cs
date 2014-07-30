using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms.VisualStyles;
using System.Xml.Serialization;
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
using Microsoft.Xna.Framework.Storage;
using SpriteGenerator;
using Debug = DebugLib.Debug;

namespace SiegeOfDamodred
{
	#region Save Data

	public struct SaveGameData
	{
		public int mWaveNumber;
		// HeroAttribute data
		#region Hero Attribute Data
		
		public int mExperience;
		public int mGold;
		public int mLevel;
		public int mDefaultSpeed;
		public int mSpellLevel;
		public float mHeroAttackUpgradeLevel;
		public float mHeroDefenseUpgradeLevel;
		public int mOwnedDragonCaves;
		public int mOwnedWolfPens;
		public int mOwnedBarracks;
		public int mOwnedArmories;
		public int mOwnedLibraries;
		public int mOwnedBonePit;
		public int mOwnedAbbey;
		public int mOwnedFireTemple;

		#endregion

		#region Hero Attack Attribute Data
		// Hero's Attribute data
		public float mHeroMaxHealthPoints;
		public int mHeroMaxMana;
		public float mHeroBaseMinimumDamage;
		public float mHeroBaseMaximumDamage;
		public float mHeroCurrentArmor;

		#endregion

		#region Wolf Data
		// Wolf's  data
		public float mWolfAttackUpgradeLevel;
		public float mWolfDefenseUpgradeLevel;
		public float mWolfAttackSpeed;

		// Wolf's Attribute data
		public float mWolfMaxHealthPoints;
		public float mWolfBaseMinimumDamage;
		public float mWolfBaseMaximumDamage;
		public float mWolfBaseDamageModifier;
		public float mWolfCurrentArmor;


		#endregion

		#region Berserker Data
		// Berserker's  data
		public float mBerserkerAttackUpgradeLevel;
		public float mBerserkerDefenseUpgradeLevel;
		public float mBerserkerAttackSpeed;

		// Berserker's Attribute data
		public float mBerserkerMaxHealthPoints;
		public float mBerserkerBaseMinimumDamage;
		public float mBerserkerBaseMaximumDamage;
		public float mBerserkerBaseDamageModifier;
		public float mBerserkerCurrentArmor;
		#endregion

		#region Axe Thrower Data
		// Axe Thrower's  data
		public float mAxeThrowerAttackUpgradeLevel;
		public float mAxeThrowerDefenseUpgradeLevel;
		public float mAxeThrowerAttackSpeed;

		// Axe Thrower's Attribute data
		public float mAxeThrowerMaxHealthPoints;
		public float mAxeThrowerBaseMinimumDamage;
		public float mAxeThrowerBaseMaximumDamage;
		public float mAxeThrowerBaseDamageModifier;
		public float mAxeThrowerCurrentArmor;

		#endregion


		#region Arcane Mage
		// Arcane Mage's  data
		public float mArcaneMageAttackUpgradeLevel;
		public float mArcaneMageDefenseUpgradeLevel;
		public float mArcaneMageAttackSpeed;

		// Arcane Mage's Attribute data
		public float mArcaneMageMaxHealthPoints;
		public float mArcaneMageBaseMinimumDamage;
		public float mArcaneMageBaseMaximumDamage;
		public float mArcaneMageBaseDamageModifier;
		public float mArcaneMageCurrentArmor;

		#endregion


		#region Cleric Data
		// Cleric's  data
		public float mClericAttackUpgradeLevel;
		public float mClericDefenseUpgradeLevel;
		public float mClericAttackSpeed;

		// Cleric's Attribute data
		public float mClericMaxHealthPoints;
		public float mClericBaseMinimumDamage;
		public float mClericBaseMaximumDamage;
		public float mClericBaseDamageModifier;
		public float mClericCurrentArmor;
		#endregion


		#region Necromancer data
		// Necromancer's  data
		public float mNecromancerAttackUpgradeLevel;
		public float mNecromancerDefenseUpgradeLevel;
		public float mNecromancerAttackSpeed;

		// Necromancer's Attribute data
		public float mNecromancerMaxHealthPoints;
		public float mNecromancerBaseMinimumDamage;
		public float mNecromancerBaseMaximumDamage;
		public float mNecromancerBaseDamageModifier;
		public float mNecromancerCurrentArmor;
		#endregion


		#region Fire Mage Data

		// Fire Mage's  data
		public float mFireMageAttackUpgradeLevel;
		public float mFireMageDefenseUpgradeLevel;
		public float mFireMageAttackSpeed;

		// Fire Mage's Attribute data
		public float mFireMageMaxHealthPoints;
		public float mFireMageBaseMinimumDamage;
		public float mFireMageBaseMaximumDamage;
		public float mFireMageBaseDamageModifier;
		public float mFireMageCurrentArmor;

		#endregion

		#region Dragon data
		// Dragon's  data
		public float mDragonAttackUpgradeLevel;
		public float mDragonDefenseUpgradeLevel;
		public float mDragonAttackSpeed;

		// Dragon's  Attribute data
		public float mDragonMaxHealthPoints;
		public float mDragonBaseMinimumDamage;
		public float mDragonBaseMaximumDamage;
		public float mDragonBaseDamageModifier;
		public float mDragonCurrentArmor;

		#endregion



	}

	#endregion

	class GameController
	{
		#region Save Data Fields

		public static SaveGameData saveGameData;
		StorageDevice storageDevice;
		IAsyncResult asyncResult;
		PlayerIndex playerIndex = PlayerIndex.One;
		static StorageContainer storageContainer;
		static string filename = "savegame.sav";

		#endregion

		#region Fields
		#region Experience Fields

		// Base experience drop amount before modification.
		private const double mBaseExperience = 12;

		// Experience drop before modification.
		private double mInitialExperience;

		// The fractional modifier that changes the amount of experience dropped.
		private const double mExperienceModifier = .15f;

		// The amount of spread after the experience drop has been decided + or -.
		private double mExperienceSpread;

		// The constant modifier that changes the amonut of experience spread
		private const double mExperienceSpreadModifier = .20f;

		#endregion

		#region Gold Fields

		// The base amonut of gold to drop.
		private const double mBaseGold = 12;

		// The inital amount of gold to drop before modification.
		private double mInitialGoldDrop;

		// The amount of spread from the gold that drops + or -.
		private double mGoldSpread;

		// The modifier used to calculate the gold spread.
		private const double mGoldSpreadModifier = .30f;

		// a constant modifier that changes the value of gold dropped.
		private const double mGoldModifier = .05f;

		#endregion

		#region Health Fields

		// The base amonut of gold to drop.
		private const double mBaseHealth = 45;

		// The inital amount of gold to drop before modification.
		private double mInitialHealthDrop;

		// The amount of spread from the gold that drops + or -.
		private double mHealthSpread;

		// The modifier used to calculate the gold spread.
		private const double mHealthSpreadModifier = .10f;

		// a constant modifier that changes the value of gold dropped.
		private const double mHealthModifier = .15f;

		#endregion

		public static FromSaveGame FromSaveGameState = FromSaveGame.NEWGAME;
		public static GodMode mGodMode;
		private float timer;
		private float mRedTimeElapsed;
		private float mGreenTimeElapsed;
		private float mBlueTimeElapsed;
		private float mYellowTimeElapsed;
		private float mPurpleTimeElapsed;
		public DebugGrid mDebugGridState;
		private Texture2D mDebugGridTexture;
		private Texture2D mCastleHealthBarTexture;
		private Rectangle mCastleHealthBarRectangle;
		private Texture2D mRedBar;
		private float mScale;
		private Rectangle source;

		public ContentManager content;
		public SpriteBatch spriteBatch;
		public GameTime gameTime;
		public int screenWidth;
		public int screenHeight;

		public KeyboardState keyboardState;
		public Level mDirtLevel;
		private Texture2D mLevelTexture;
		public int currentLevel;
		private float StartTime;
		private float EnemyTimeToSpawn;
		private float EnemySpawnTimer;
		private float RoundTimer;
		private bool hasAlreadyDropped = false;


		public static GameState gameState = GameState.MAIN_MENU;
		private float waitTimer;


		private Random randomGenerator;

		private Castle mCastle;
		public static Hero mHero;

		public JTargetQueue EnemyWavePool;
		public JLinkedList Graveyard;

		// public List<Friendly> Friendlies;
		private float mSpawnTimer;
		private float mTimer;
		public JLinkedList mInPlayItemDropList;
		public JLinkedList mInPlayObjectList;
		public static JLinkedList mInPlayProjectileList;
		public static Structure[][] StructurePool;
		public static Structure[] InPlayStructures;
		public static Friendly[] FriendlyPool;
		private Song levelComplete;
		public static Rectangle PlayingField;
		private bool isRespawning;
		private float mRespawnTimer;

		#endregion

		#region Initialize Functions

		public GameController(float mScale)
		{
			this.mScale = mScale;
			GameObject.Scale = mScale;
			asyncResult = StorageDevice.BeginShowSelector(playerIndex, null, null);
			storageDevice = StorageDevice.EndShowSelector(asyncResult);

			asyncResult = storageDevice.BeginOpenContainer("Game1StorageContainer", null, null);
			if (asyncResult.IsCompleted)
			{
				storageContainer = storageDevice.EndOpenContainer(asyncResult);
			}
		}

		public GameController()
		{

		}

		public void LoadGame()
		{

			

			
			//Load UserInterface.
			//Load Debugger.
			//PopulateLists
			//gameObject.GetSprite.LoadContent.
			//gameObject.PostSetSpriteFram().
			//Set Starting gamestate
			// pass gametime.

			PlayingField = new Rectangle(0, 25, 1380, 775);


			randomGenerator = new Random();

			// EnemyTimeToSpawn = randomGenerator.Next(7000, 15000);


			

			mRedTimeElapsed = 0f;
			mBlueTimeElapsed = 0f;
			mGreenTimeElapsed = 0f;
			mYellowTimeElapsed = 0f;
			mPurpleTimeElapsed = 0f;

			Graveyard = new JLinkedList();
			mInPlayItemDropList = new JLinkedList();
			mInPlayObjectList = new JLinkedList();
			mInPlayProjectileList = new JLinkedList();



			mDebugGridState = DebugGrid.GRID_DISABLED;
			Debug.EnableGrid = DisplayGrid;
			Debug.EnableGodModeFunction = EnableGodMode;
			LoadDebugger();

			PopulateLists();

			UserInterfaceController.mCastle = mCastle;
			UserInterfaceController.mHero = mHero;
			UserInterfaceController.LoadUserInterface(gameTime, content, spriteBatch);
			InitializeInPlayStructures(ref  InPlayStructures);
			InitializeStructurePool(ref StructurePool);
			InitializeFriendlyPool(ref FriendlyPool);

			// Create Castle
			mCastle.Sprite.LoadContent();
			mCastle.PostSetSpriteFrame();
			//mCastleHealthBarTexture = content.Load<Texture2D>("Sprites/Castle/HealthBar");
			mRedBar = content.Load<Texture2D>("Sprites/PixelTextures/solidred");
			source = new Rectangle(1565, 450, 50, 300);
			mCastleHealthBarRectangle = new Rectangle(1565, 450, 50, 300);
			// Populate Enemies Menus, and Levels.
			mHero.Sprite.LoadContent();

			// Create HERO
			mHero.PostSetSpriteFrame();
			mHero.PlayingField = PlayingField;

			// Create Level
			mDirtLevel = new Level(new Rectangle(0, 0, screenWidth, screenHeight), mLevelTexture, "Audio/Songs/Battle Music", content);
			WaveController.Castle = mCastle;
			WaveController.Content = content;
			WaveController.InitializeWaveController();

			PassGameTime();

			Notification.LoadNotificationFont(content);
			Notification.LoadLevelUpRibbon(content);
			
			ChangeGameState(GameState.LOAD);
		}

		private void ResetVariablesForNextWave()
		{
			RoundTimer = 0;
			mCastle.CastleAttribute.CurrentHealthPoints = mCastle.CastleAttribute.MaxHealthPoints;
			mHero.HeroAttribute.CurrentHealthPoints = mHero.HeroAttribute.MaxHealthPoints;
			mHero.HeroAttribute.Mana = mHero.HeroAttribute.MaxMana;

			for (int i = 0; i < InPlayStructures.Length; i++)
			{
				if (InPlayStructures[i] != null)
				{
					InPlayStructures[i].StructureAttribute.CurrentHealthPoints = InPlayStructures[i].StructureAttribute.MaxHealthPoints;
					InPlayStructures[i].StructureAttribute.CurrentAmountArcaneMages = 0;
					InPlayStructures[i].StructureAttribute.CurrentAmountAxeThrowers = 0;
					InPlayStructures[i].StructureAttribute.CurrentAmountBerserkers = 0;
					InPlayStructures[i].StructureAttribute.CurrentAmountClerics = 0;
					InPlayStructures[i].StructureAttribute.CurrentAmountDragons = 0;
					InPlayStructures[i].StructureAttribute.CurrentAmountFireMages = 0;
					InPlayStructures[i].StructureAttribute.CurrentAmountNecromancers = 0;
					InPlayStructures[i].StructureAttribute.CurrentAmountWolfs = 0;
				}
			}

			WaveController.ReinitializeWaveController();
		}

		public static void LoadSaveGame()
		{
			if (!storageContainer.FileExists(filename))
			{
				FromSaveGameState = FromSaveGame.NEWGAME;

				#region Default Save Game Data

				saveGameData = new SaveGameData()
				{
					mWaveNumber = 1,
					// HeroAttribute data
					mExperience = 0,
					mGold = 1000,
					mLevel = 1,
					mDefaultSpeed = 4,
					mSpellLevel = 1,
					mHeroAttackUpgradeLevel = 1,
					mHeroDefenseUpgradeLevel = 1,
					mOwnedDragonCaves = 0,
					mOwnedWolfPens = 0,
					mOwnedBarracks = 0,
					mOwnedArmories = 0,
					mOwnedLibraries = 0,
					mOwnedBonePit = 0,
					mOwnedAbbey = 0,
					mOwnedFireTemple = 0,
					// Hero's Attribute data
					mHeroMaxHealthPoints = 150,
					mHeroMaxMana = 40,
					mHeroBaseMinimumDamage = 55,
					mHeroBaseMaximumDamage = 86,
					mHeroCurrentArmor = 4,

					// Wolf's  data
					mWolfAttackUpgradeLevel = 1,
					mWolfDefenseUpgradeLevel = 1,
					mWolfAttackSpeed = 1000,
					// Wolf's Attribute data
					mWolfMaxHealthPoints = 60,
					mWolfBaseMinimumDamage = 10,
					mWolfBaseMaximumDamage = 15,
					mWolfBaseDamageModifier = (10 + 15) / 2.0f,
					mWolfCurrentArmor = 1,

					// Berserker's  data
					mBerserkerAttackUpgradeLevel = 1,
					mBerserkerDefenseUpgradeLevel = 1,
					mBerserkerAttackSpeed = 1100,
					// Berserker's Attribute data
					mBerserkerMaxHealthPoints = 120,
					mBerserkerBaseMinimumDamage = 30,
					mBerserkerBaseMaximumDamage = 55,
					mBerserkerBaseDamageModifier = (33 + 55) / 2.0f,
					mBerserkerCurrentArmor = 1,

					// Axe Thrower's  data
					mAxeThrowerAttackUpgradeLevel = 1,
					mAxeThrowerDefenseUpgradeLevel = 1,
					mAxeThrowerAttackSpeed = 1200,
					// Axe Thrower's Attribute data
					mAxeThrowerMaxHealthPoints = 230,
					mAxeThrowerBaseMinimumDamage = 3,
					mAxeThrowerBaseMaximumDamage = 10,
					mAxeThrowerBaseDamageModifier = (13 / 2.0f),
					mAxeThrowerCurrentArmor = 1,

					// Arcane Mage's  data
					mArcaneMageAttackUpgradeLevel = 1,
					mArcaneMageDefenseUpgradeLevel = 1,
					mArcaneMageAttackSpeed = 2300,
					// Arcane Mage's Attribute data
					mArcaneMageMaxHealthPoints = 180,
					mArcaneMageBaseMinimumDamage = 60,
					mArcaneMageBaseMaximumDamage = 80,
					mArcaneMageBaseDamageModifier = 140 / 2.0f,
					mArcaneMageCurrentArmor = 1,

					// Cleric's  data
					mClericAttackUpgradeLevel = 1,
					mClericDefenseUpgradeLevel = 1,
					mClericAttackSpeed = 4000,
					// Cleric's Attribute data
					mClericMaxHealthPoints = 300,
					mClericBaseMinimumDamage = 10,
					mClericBaseMaximumDamage = 30,
					mClericBaseDamageModifier = 40 / 2.0f,
					mClericCurrentArmor = 1,

					// Necromancer's  data
					mNecromancerAttackUpgradeLevel = 1,
					mNecromancerDefenseUpgradeLevel = 1,
					mNecromancerAttackSpeed = 2800,
					// Necromancer's Attribute data
					mNecromancerMaxHealthPoints = 300,
					mNecromancerBaseMinimumDamage = 15,
					mNecromancerBaseMaximumDamage = 35,
					mNecromancerBaseDamageModifier = 50 / 2.0f,
					mNecromancerCurrentArmor = 1,

					// Fire Mage's  data
					mFireMageAttackUpgradeLevel = 1,
					mFireMageDefenseUpgradeLevel = 1,
					mFireMageAttackSpeed = 2300,
					// Fire Mage's Attribute data
					mFireMageMaxHealthPoints = 180,
					mFireMageBaseMinimumDamage = 60,
					mFireMageBaseMaximumDamage = 80,
					mFireMageBaseDamageModifier = 140 / 2.0f,
					mFireMageCurrentArmor = 1,

					// Dragon's  data
					mDragonAttackUpgradeLevel = 1,
					mDragonDefenseUpgradeLevel = 1,
					mDragonAttackSpeed = 40000,
					// Fire Mage's Attribute data
					mDragonMaxHealthPoints = 700,
					mDragonBaseMinimumDamage = 275,
					mDragonBaseMaximumDamage = 300,
					mDragonBaseDamageModifier = (275 + 300) / 2.0f,
					mDragonCurrentArmor = 1
				};

				#endregion

			}

			else
			{

				Stream stream = storageContainer.OpenFile(filename, FileMode.Open);
				XmlSerializer serializer = new XmlSerializer(typeof(SaveGameData));
				saveGameData = (SaveGameData)serializer.Deserialize(stream);
				stream.Close();
				storageContainer.Dispose();
				storageContainer = null;

				FromSaveGameState = FromSaveGame.SAVEGAME;


				#region Load Save Game Data

				// simplfying data
				HeroAttribute tempHeroAttribute = mHero.HeroAttribute;
				FriendlyAttribute tempWolfAttribute = FriendlyPool[0].FriendlyAttribute;
				FriendlyAttribute tempBerserkerAttribute = FriendlyPool[1].FriendlyAttribute;
				FriendlyAttribute tempArcaneMageAttribute = FriendlyPool[2].FriendlyAttribute;
				FriendlyAttribute tempAxeThrowerAttribute = FriendlyPool[3].FriendlyAttribute;
				FriendlyAttribute tempClericAttribute = FriendlyPool[4].FriendlyAttribute;
				FriendlyAttribute tempNecromancerAttribute = FriendlyPool[5].FriendlyAttribute;
				FriendlyAttribute tempFireMageAttribute = FriendlyPool[6].FriendlyAttribute;
				FriendlyAttribute tempDragonAttribute = FriendlyPool[7].FriendlyAttribute;

				WaveController.WaveNumber = saveGameData.mWaveNumber;

				// HeroAttribute data
				tempHeroAttribute.Experience = saveGameData.mExperience;
				tempHeroAttribute.Gold = saveGameData.mGold;
				tempHeroAttribute.Level = saveGameData.mLevel;
				tempHeroAttribute.MaxMana = saveGameData.mHeroMaxMana;
				tempHeroAttribute.DefaultSpeed = saveGameData.mDefaultSpeed;
				tempHeroAttribute.SpellLevel = saveGameData.mSpellLevel;
				tempHeroAttribute.AttackUpgradeLevel = saveGameData.mHeroAttackUpgradeLevel;
				tempHeroAttribute.DefenseUpgradeLevel = saveGameData.mHeroDefenseUpgradeLevel;
				tempHeroAttribute.OwnedDragonCaves = saveGameData.mOwnedDragonCaves;
				tempHeroAttribute.OwnedWolfPens = saveGameData.mOwnedWolfPens;
				tempHeroAttribute.OwnedBarracks = saveGameData.mOwnedBarracks;
				tempHeroAttribute.OwnedArmories = saveGameData.mOwnedArmories;
				tempHeroAttribute.OwnedLibraries = saveGameData.mOwnedLibraries;
				tempHeroAttribute.OwnedBonePit = saveGameData.mOwnedBonePit;
				tempHeroAttribute.OwnedAbbey = saveGameData.mOwnedAbbey;
				tempHeroAttribute.OwnedFireTemple = saveGameData.mOwnedFireTemple;
				// Hero's Attribute data
				tempHeroAttribute.MaxHealthPoints = saveGameData.mHeroMaxHealthPoints;
				tempHeroAttribute.BaseMinimumDamage = saveGameData.mHeroBaseMinimumDamage;
				tempHeroAttribute.BaseMaximumDamage = saveGameData.mHeroBaseMaximumDamage;
				tempHeroAttribute.CurrentArmor = saveGameData.mHeroCurrentArmor;

				// Wolf's data
				tempWolfAttribute.AttackSpeed = saveGameData.mWolfAttackSpeed;
				tempWolfAttribute.AttackUpgradeLevel = saveGameData.mWolfAttackUpgradeLevel;
				tempWolfAttribute.DefenseUpgradeLevel = saveGameData.mWolfDefenseUpgradeLevel;
				// Wolf's Attribute data
				tempWolfAttribute.MaxHealthPoints = saveGameData.mWolfMaxHealthPoints;
				tempWolfAttribute.BaseMinimumDamage = saveGameData.mWolfBaseMinimumDamage;
				tempWolfAttribute.BaseMaximumDamage = saveGameData.mWolfBaseMaximumDamage;
				tempWolfAttribute.CurrentArmor = saveGameData.mWolfCurrentArmor;

				//Berserker's data
				tempBerserkerAttribute.AttackSpeed = saveGameData.mBerserkerAttackSpeed;
				tempBerserkerAttribute.AttackUpgradeLevel = saveGameData.mBerserkerAttackUpgradeLevel;
				tempBerserkerAttribute.DefenseUpgradeLevel = saveGameData.mBerserkerDefenseUpgradeLevel;
				//Berserker's Attribute data
				tempBerserkerAttribute.MaxHealthPoints = saveGameData.mBerserkerMaxHealthPoints;
				tempBerserkerAttribute.BaseMinimumDamage = saveGameData.mBerserkerBaseMinimumDamage;
				tempBerserkerAttribute.BaseMaximumDamage = saveGameData.mBerserkerBaseMaximumDamage;
				tempBerserkerAttribute.CurrentArmor = saveGameData.mBerserkerCurrentArmor;

				//Axe Thrower's Data
				tempAxeThrowerAttribute.AttackSpeed = saveGameData.mAxeThrowerAttackSpeed;
				tempAxeThrowerAttribute.AttackUpgradeLevel = saveGameData.mAxeThrowerAttackUpgradeLevel;
				tempAxeThrowerAttribute.DefenseUpgradeLevel = saveGameData.mAxeThrowerDefenseUpgradeLevel;
				//Axe Thrower's Attribute data
				tempAxeThrowerAttribute.MaxHealthPoints = saveGameData.mAxeThrowerMaxHealthPoints;
				tempAxeThrowerAttribute.BaseMinimumDamage = saveGameData.mAxeThrowerBaseMinimumDamage;
				tempAxeThrowerAttribute.BaseMaximumDamage = saveGameData.mAxeThrowerBaseMaximumDamage;
				tempAxeThrowerAttribute.CurrentArmor = saveGameData.mAxeThrowerCurrentArmor;

				//Arcane Mage's data
				tempArcaneMageAttribute.AttackSpeed = saveGameData.mArcaneMageAttackSpeed;
				tempArcaneMageAttribute.AttackUpgradeLevel = saveGameData.mArcaneMageAttackUpgradeLevel;
				tempArcaneMageAttribute.DefenseUpgradeLevel = saveGameData.mArcaneMageDefenseUpgradeLevel;
				//Arcane Mage's Attribute data
				tempArcaneMageAttribute.MaxHealthPoints = saveGameData.mArcaneMageMaxHealthPoints;
				tempArcaneMageAttribute.BaseMinimumDamage = saveGameData.mArcaneMageBaseMinimumDamage;
				tempArcaneMageAttribute.BaseMaximumDamage = saveGameData.mArcaneMageBaseMaximumDamage;
				tempArcaneMageAttribute.CurrentArmor = saveGameData.mArcaneMageCurrentArmor;

				//Cleric's data
				tempClericAttribute.AttackSpeed = saveGameData.mClericAttackSpeed;
				tempClericAttribute.AttackUpgradeLevel = saveGameData.mClericAttackUpgradeLevel;
				tempClericAttribute.DefenseUpgradeLevel = saveGameData.mClericDefenseUpgradeLevel;
				//Cleric's Attributes data
				tempClericAttribute.MaxHealthPoints = saveGameData.mClericMaxHealthPoints;
				tempClericAttribute.BaseMinimumDamage = saveGameData.mClericBaseMinimumDamage;
				tempClericAttribute.BaseMaximumDamage = saveGameData.mClericBaseMaximumDamage;
				tempClericAttribute.CurrentArmor = saveGameData.mClericCurrentArmor;

				//Necromancer's data
				tempNecromancerAttribute.AttackSpeed = saveGameData.mNecromancerAttackSpeed;
				tempNecromancerAttribute.AttackUpgradeLevel = saveGameData.mNecromancerAttackUpgradeLevel;
				tempNecromancerAttribute.DefenseUpgradeLevel = saveGameData.mNecromancerDefenseUpgradeLevel;
				//Necromancer's Attribute data
				tempNecromancerAttribute.MaxHealthPoints = saveGameData.mNecromancerMaxHealthPoints;
				tempNecromancerAttribute.BaseMinimumDamage = saveGameData.mNecromancerBaseMinimumDamage;
				tempNecromancerAttribute.BaseMaximumDamage = saveGameData.mNecromancerBaseMaximumDamage;
				tempNecromancerAttribute.CurrentArmor = saveGameData.mNecromancerCurrentArmor;

				//Fire Mage's data
				tempFireMageAttribute.AttackSpeed = saveGameData.mFireMageAttackSpeed;
				tempFireMageAttribute.AttackUpgradeLevel = saveGameData.mFireMageAttackUpgradeLevel;
				tempFireMageAttribute.DefenseUpgradeLevel = saveGameData.mFireMageDefenseUpgradeLevel;
				//Fire Mage's Attribute data
				tempFireMageAttribute.MaxHealthPoints = saveGameData.mFireMageMaxHealthPoints;
				tempFireMageAttribute.BaseMinimumDamage = saveGameData.mFireMageBaseMinimumDamage;
				tempFireMageAttribute.BaseMaximumDamage = saveGameData.mFireMageBaseMaximumDamage;
				tempFireMageAttribute.CurrentArmor = saveGameData.mFireMageCurrentArmor;

				//Dragon's data
				tempDragonAttribute.AttackSpeed = saveGameData.mDragonAttackSpeed;
				tempDragonAttribute.AttackUpgradeLevel = saveGameData.mDragonAttackUpgradeLevel;
				tempDragonAttribute.DefenseUpgradeLevel = saveGameData.mDragonDefenseUpgradeLevel;
				//Dragon's Attribute data
				tempDragonAttribute.MaxHealthPoints = saveGameData.mDragonMaxHealthPoints;
				tempDragonAttribute.BaseMinimumDamage = saveGameData.mDragonBaseMinimumDamage;
				tempDragonAttribute.BaseMaximumDamage = saveGameData.mDragonBaseMaximumDamage;
				tempDragonAttribute.CurrentArmor = saveGameData.mDragonCurrentArmor;

				#endregion


			}
		}

		public static void SaveGame()
		{

			FromSaveGameState = FromSaveGame.NEWGAME;

			#region Save Game Data

			// simplfying data
			HeroAttribute tempHeroAttribute = mHero.HeroAttribute;
			FriendlyAttribute tempWolfAttribute = FriendlyPool[0].FriendlyAttribute;
			FriendlyAttribute tempBerserkerAttribute = FriendlyPool[1].FriendlyAttribute;
			FriendlyAttribute tempArcaneMageAttribute = FriendlyPool[2].FriendlyAttribute;
			FriendlyAttribute tempAxeThrowerAttribute = FriendlyPool[3].FriendlyAttribute;
			FriendlyAttribute tempClericAttribute = FriendlyPool[4].FriendlyAttribute;
			FriendlyAttribute tempNecromancerAttribute = FriendlyPool[5].FriendlyAttribute;
			FriendlyAttribute tempFireMageAttribute = FriendlyPool[6].FriendlyAttribute;
			FriendlyAttribute tempDragonAttribute = FriendlyPool[7].FriendlyAttribute;


			saveGameData = new SaveGameData()
			{
				mWaveNumber = WaveController.WaveNumber,

				// HeroAttribute data
				mExperience = tempHeroAttribute.Experience,
				mGold = tempHeroAttribute.Gold,
				mLevel = tempHeroAttribute.Level,
				mHeroMaxMana = tempHeroAttribute.Mana,
				mDefaultSpeed = tempHeroAttribute.DefaultSpeed,
				mSpellLevel = tempHeroAttribute.SpellLevel,
				mHeroAttackUpgradeLevel = tempHeroAttribute.AttackUpgradeLevel,
				mHeroDefenseUpgradeLevel = tempHeroAttribute.DefenseUpgradeLevel,
				mOwnedDragonCaves = tempHeroAttribute.OwnedDragonCaves,
				mOwnedWolfPens = tempHeroAttribute.OwnedWolfPens,
				mOwnedBarracks = tempHeroAttribute.OwnedBarracks,
				mOwnedArmories = tempHeroAttribute.OwnedArmories,
				mOwnedLibraries = tempHeroAttribute.OwnedLibraries,
				mOwnedBonePit = tempHeroAttribute.OwnedBonePit,
				mOwnedAbbey = tempHeroAttribute.OwnedAbbey,
				mOwnedFireTemple = tempHeroAttribute.OwnedFireTemple,
				// Hero's Attribute data
				mHeroMaxHealthPoints = tempHeroAttribute.MaxHealthPoints,
				mHeroBaseMinimumDamage = tempHeroAttribute.BaseMinimumDamage,
				mHeroBaseMaximumDamage = tempHeroAttribute.BaseMaximumDamage,
				mHeroCurrentArmor = tempHeroAttribute.CurrentArmor,

				// Wolf's  data
				mWolfAttackSpeed = tempWolfAttribute.AttackSpeed,
				mWolfAttackUpgradeLevel = tempWolfAttribute.AttackUpgradeLevel,
				mWolfDefenseUpgradeLevel = tempWolfAttribute.DefenseUpgradeLevel,
				// Wolf's Attribute data
				mWolfMaxHealthPoints = tempWolfAttribute.MaxHealthPoints,
				mWolfBaseMinimumDamage = tempWolfAttribute.BaseMinimumDamage,
				mWolfBaseMaximumDamage = tempWolfAttribute.BaseMaximumDamage,
				mWolfCurrentArmor = tempWolfAttribute.CurrentArmor,

				// Berserker's  data
				mBerserkerAttackSpeed = tempBerserkerAttribute.AttackSpeed,
				mBerserkerAttackUpgradeLevel = tempBerserkerAttribute.AttackUpgradeLevel,
				mBerserkerDefenseUpgradeLevel = tempBerserkerAttribute.DefenseUpgradeLevel,
				// Berserker's Attribute data
				mBerserkerMaxHealthPoints = tempBerserkerAttribute.MaxHealthPoints,
				mBerserkerBaseMinimumDamage = tempBerserkerAttribute.BaseMinimumDamage,
				mBerserkerBaseMaximumDamage = tempBerserkerAttribute.BaseMaximumDamage,
				mBerserkerCurrentArmor = tempBerserkerAttribute.CurrentArmor,

				// Axe Thrower's  data
				mAxeThrowerAttackSpeed = tempAxeThrowerAttribute.AttackSpeed,
				mAxeThrowerAttackUpgradeLevel = tempArcaneMageAttribute.AttackUpgradeLevel,
				mAxeThrowerDefenseUpgradeLevel = tempArcaneMageAttribute.DefenseUpgradeLevel,
				// Axe Thrower's Attribute data
				mAxeThrowerMaxHealthPoints = tempArcaneMageAttribute.MaxHealthPoints,
				mAxeThrowerBaseMinimumDamage = tempArcaneMageAttribute.BaseMinimumDamage,
				mAxeThrowerBaseMaximumDamage = tempArcaneMageAttribute.BaseMaximumDamage,
				mAxeThrowerCurrentArmor = tempArcaneMageAttribute.CurrentArmor,

				// Arcane Mage's  data
				mArcaneMageAttackSpeed = tempArcaneMageAttribute.AttackSpeed,
				mArcaneMageAttackUpgradeLevel = tempAxeThrowerAttribute.AttackUpgradeLevel,
				mArcaneMageDefenseUpgradeLevel = tempAxeThrowerAttribute.DefenseUpgradeLevel,
				// Arcane Mage's Attribute data
				mArcaneMageMaxHealthPoints = tempAxeThrowerAttribute.MaxHealthPoints,
				mArcaneMageBaseMinimumDamage = tempAxeThrowerAttribute.BaseMinimumDamage,
				mArcaneMageBaseMaximumDamage = tempAxeThrowerAttribute.BaseMaximumDamage,
				mArcaneMageCurrentArmor = tempAxeThrowerAttribute.CurrentArmor,

				// Cleric's  data
				mClericAttackSpeed = tempClericAttribute.AttackSpeed,
				mClericAttackUpgradeLevel = tempClericAttribute.AttackUpgradeLevel,
				mClericDefenseUpgradeLevel = tempClericAttribute.DefenseUpgradeLevel,
				// Cleric's Attribute data
				mClericBaseMinimumDamage = tempClericAttribute.CurrentMinimumDamage,
				mClericBaseMaximumDamage = tempClericAttribute.CurrentMaximumDamage,
				mClericCurrentArmor = tempClericAttribute.CurrentArmor,

				// Necromancer's  data
				mNecromancerAttackSpeed = tempNecromancerAttribute.AttackSpeed,
				mNecromancerAttackUpgradeLevel = tempNecromancerAttribute.AttackUpgradeLevel,
				mNecromancerDefenseUpgradeLevel = tempNecromancerAttribute.DefenseUpgradeLevel,
				// Necromancer's Attribute data
				mNecromancerMaxHealthPoints = tempNecromancerAttribute.MaxHealthPoints,
				mNecromancerBaseMinimumDamage = tempNecromancerAttribute.BaseMinimumDamage,
				mNecromancerBaseMaximumDamage = tempNecromancerAttribute.BaseMaximumDamage,
				mNecromancerCurrentArmor = tempNecromancerAttribute.CurrentArmor,

				// Fire Mage's  data
				mFireMageAttackSpeed = tempFireMageAttribute.AttackSpeed,
				mFireMageAttackUpgradeLevel = tempFireMageAttribute.AttackUpgradeLevel,
				mFireMageDefenseUpgradeLevel = tempFireMageAttribute.DefenseUpgradeLevel,
				// Fire Mage's Attribute data
				mFireMageMaxHealthPoints = tempFireMageAttribute.MaxHealthPoints,
				mFireMageBaseMinimumDamage = tempFireMageAttribute.BaseMinimumDamage,
				mFireMageBaseMaximumDamage = tempFireMageAttribute.BaseMaximumDamage,
				mFireMageCurrentArmor = tempFireMageAttribute.CurrentArmor,

				// Dragon's  data
				mDragonAttackSpeed = tempDragonAttribute.AttackSpeed,
				mDragonAttackUpgradeLevel = tempDragonAttribute.AttackUpgradeLevel,
				mDragonDefenseUpgradeLevel = tempDragonAttribute.DefenseUpgradeLevel,
				// Fire Mage's Attribute data
				mDragonMaxHealthPoints = tempDragonAttribute.MaxHealthPoints,
				mDragonBaseMinimumDamage = tempDragonAttribute.BaseMinimumDamage,
				mDragonBaseMaximumDamage = tempDragonAttribute.BaseMaximumDamage,
				mDragonCurrentArmor = tempDragonAttribute.CurrentArmor
			};

			#endregion


			using (Stream stream = storageContainer.CreateFile(filename))
			{
				XmlSerializer serializer = new XmlSerializer(typeof(SaveGameData));
				serializer.Serialize(stream, saveGameData);
			}
		}

		private void PopulateLists()
		{
			// Friendlies = new List<Friendly>();

			// DRAGON, NECROMANCER, ARCANE_MAGE, FIRE_MAGE,
			//WOLF, AXE_THROWER, TROLL, BERSERKER   

			mLevelTexture = content.Load<Texture2D>("Sprites/Levels/background_composite");

			Vector2 position = new Vector2(1250, 159);

			float x = position.X;
			float y = position.Y;



			//Create Hero
			mHero = new Hero(ObjectType.HERO, content, SpriteState.MOVE_LEFT, new Vector2(900, 450));

			mCastle = new Castle(ObjectType.ORC_CASTLE, content, SpriteState.CASTLE, new Vector2(1450, 450));
			//mCastle.Speed = 0.5f;

		}

		public void InitializeFriendlyPool(ref Friendly[] FriendlyPool)
		{
			FriendlyPool = new Friendly[8];

			FriendlyPool[0] = new Friendly(ObjectType.WOLF, content, SpriteState.MOVE_LEFT,
				Vector2.Zero, Vector2.Zero, 1, 1);

			FriendlyPool[1] = new Friendly(ObjectType.BERSERKER, content, SpriteState.MOVE_LEFT,
				Vector2.Zero, Vector2.Zero, 1, 1);

			FriendlyPool[2] = new Friendly(ObjectType.ARCANE_MAGE, content, SpriteState.MOVE_LEFT,
				Vector2.Zero, Vector2.Zero, 1, 1);

			FriendlyPool[3] = new Friendly(ObjectType.AXE_THROWER, content, SpriteState.MOVE_LEFT,
				Vector2.Zero, Vector2.Zero, 1, 1);

			FriendlyPool[4] = new Friendly(ObjectType.CLERIC, content, SpriteState.MOVE_LEFT,
				Vector2.Zero, Vector2.Zero, 1, 1);

			FriendlyPool[5] = new Friendly(ObjectType.NECROMANCER, content, SpriteState.MOVE_LEFT,
				Vector2.Zero, Vector2.Zero, 1, 1);

			FriendlyPool[6] = new Friendly(ObjectType.FIRE_MAGE, content, SpriteState.MOVE_LEFT,
				Vector2.Zero, Vector2.Zero, 1, 1);

			FriendlyPool[7] = new Friendly(ObjectType.DRAGON, content, SpriteState.MOVE_LEFT,
				Vector2.Zero, Vector2.Zero, 1, 1);


		}

		public void InitializeInPlayStructures(ref  Structure[] InPlayStructures)
		{
			InPlayStructures = new Structure[5];

			for (int i = 0; i < InPlayStructures.Length; i++)
			{
				InPlayStructures[i] = null;
			}

		}

		public void InitializeStructurePool(ref Structure[][] StructurePool)
		{


			StructurePool = new Structure[8][];
			for (int i = 0; i < 8; i++)
			{
				StructurePool[i] = new Structure[5];
			}

			for (int objectType = 0; objectType < 8; objectType++)
			{
				for (int objectColor = 0; objectColor < 5; objectColor++)
				{
					switch ((ObjectColor)objectColor)
					{
						case ObjectColor.BLUE:
							StructurePool[objectType][objectColor] = new Structure((ObjectType)objectType, content,
																		  SpriteState.STRUCTURE, new Vector2(0, -3000),
																		  (ObjectColor)objectColor, UserInterfaceController.BlueStructureButton.Sprite.WorldPosition);
							StructurePool[objectType][objectColor].Sprite.LoadContent();
							break;
						case ObjectColor.RED:
							StructurePool[objectType][objectColor] = new Structure((ObjectType)objectType, content,
																		  SpriteState.STRUCTURE, new Vector2(0, -3000),
																		  (ObjectColor)objectColor, UserInterfaceController.RedStructureButton.Sprite.WorldPosition);
							StructurePool[objectType][objectColor].Sprite.LoadContent();
							break;
						case ObjectColor.YELLOW:
							StructurePool[objectType][objectColor] = new Structure((ObjectType)objectType, content,
																		  SpriteState.STRUCTURE, new Vector2(0, -3000),
																		  (ObjectColor)objectColor, UserInterfaceController.YellowStructureButton.Sprite.WorldPosition);
							StructurePool[objectType][objectColor].Sprite.LoadContent();
							break;
						case ObjectColor.GREEN:
							StructurePool[objectType][objectColor] = new Structure((ObjectType)objectType, content,
																		  SpriteState.STRUCTURE, new Vector2(0, -3000),
																		  (ObjectColor)objectColor, UserInterfaceController.GreenStructureButton.Sprite.WorldPosition);
							StructurePool[objectType][objectColor].Sprite.LoadContent();
							break;
						case ObjectColor.PURPLE:
							StructurePool[objectType][objectColor] = new Structure((ObjectType)objectType, content,
																		  SpriteState.STRUCTURE, new Vector2(0, -3000),
																		  (ObjectColor)objectColor, UserInterfaceController.PurpleStructureButton.Sprite.WorldPosition);
							StructurePool[objectType][objectColor].Sprite.LoadContent();
							break;
						case ObjectColor.NULL:
							break;
						default:
							break;
					}

				}

			}


		}


		private void LoadDebugger()
		{


			mDebugGridTexture = content.Load<Texture2D>("Sprites/Debugging/gridOverlay");
			StartTime = 0.0f;
			Debug.mContent = content;
			Debug.mSpriteBatch = spriteBatch;
			Debug.mTextBox = new Rectangle(10, 10, Debug.TEXTBOX_WIDTH, Debug.TEXTBOX_HEIGHT);
			Debug.mDebugState = DebugState.HIDDEN;
			Debug.mMessageLogList = new List<TextBoxString>();
			Debug.mTypedText.mStringValue = " ";
			Debug.mTypedText.mStringPosition = new Vector2(Debug.TEXTBOX_X, Debug.TEXTBOX_Y);
			Debug.mTextScale = 0.28f;
			Debug.mDelayInMilliseconds = 250;
			Debug.mMessageLogList.Add(Debug.mTypedText);
			Debug.Load();
			Debug.Initialize();
		}

		#endregion

		#region Update Functions

		public void Update()
		{
			PassGameTime();
			UpdateDebugger();
			//gameObject.Update
			//gameObject.GetSprite.Update



			switch (gameState)
			{
				case GameState.MAIN_MENU:

					UserInterfaceController.UpdateMainMenu(gameTime);
					UserInterfaceController.UpdateMouse(gameTime);
					break;

				case GameState.ACTIVE:

					GameObject.FromSaveGameState = FromSaveGameState;
					UserInterfaceController.mInPlayObjectList = mInPlayObjectList;
					RoundTimer += gameTime.ElapsedGameTime.Milliseconds;
					mInPlayObjectList = GameObject.mInPlayObjectList;
					mInPlayProjectileList = GameObject.mInPlayProjectileList;
					UpdateStructures();
					UpdateInPlayObjects();

					
					UpdateProjectiles();
					UpdateInPlayItemDrops();
					CheckItemDropCollision();
					Notification.UpdateDamageNumbers(gameTime);
					Notification.UpdateHealingNumbers(gameTime);
					Notification.UpdateLevelBanner(gameTime);
					UserInterfaceController.UpdateUIActiveState(gameTime);

					if (mGodMode == GodMode.ACTIVATED)
					{
						mHero.HeroAttribute.MaxHealthPoints = 9999;
						mHero.HeroAttribute.MaxMana = 9999;
						mHero.HeroAttribute.Gold = 999999;
						mHero.HeroAttribute.Level = 98;
						mHero.HeroAttribute.OwnedArmories = 5;
						mHero.HeroAttribute.OwnedBarracks = 5;
						mHero.HeroAttribute.OwnedBonePit = 5;
						mHero.HeroAttribute.OwnedDragonCaves = 5;
						mHero.HeroAttribute.OwnedFireTemple = 5;
						mHero.HeroAttribute.OwnedLibraries = 5;
						mHero.HeroAttribute.OwnedWolfPens = 5;
						mHero.HeroAttribute.OwnedAbbey = 5;
						mHero.HeroAttribute.LevelUp();
						WaveController.WaveNumber = 60;
					}

					SpawnEnemy();

					break;

				case GameState.PAUSE:
					break;

				case GameState.ROUND_OVER:
					UserInterfaceController.UpdateWaveComplete(gameTime);
					break;

				case GameState.GAME_OVER:


					break;
			}
		}





		public void UpdateProjectiles()
		{

			if (!mInPlayProjectileList.IsEmpty())
			{
				mInPlayProjectileList.Reset();

				for (int i = 0; i < mInPlayProjectileList.GetCount(); i++)
				{
					Projectile currentProjectile = (Projectile)mInPlayProjectileList.GetCurrent().gameObject;
					currentProjectile.Sprite.Update(gameTime);
					currentProjectile.Update(gameTime);

					if (currentProjectile.HasLanded)
					{
						DestroyProjectile();
					}


					mInPlayProjectileList.NextNode();
				}

			}

		}

		public void UpdateInPlayItemDrops()
		{

			if (!mInPlayItemDropList.IsEmpty())
			{


				mInPlayItemDropList.Reset();
				for (int i = 0; i < mInPlayItemDropList.GetCount(); i++)
				{

					ItemDrop tempItemDrop = (ItemDrop) mInPlayItemDropList.GetCurrent().gameObject;
					if (tempItemDrop != null)
					{
						tempItemDrop.Sprite.Update(gameTime);
						tempItemDrop.Update(gameTime);

						if (tempItemDrop.IsDead)
						{
							DestroyItemDrop();
						}
					}


					mInPlayItemDropList.NextNode();
				}

			}

		}

		public void CheckItemDropCollision()
		{

				if (!mInPlayItemDropList.IsEmpty())
				{
					mInPlayItemDropList.Reset();

					for (int i = 0; i < mInPlayItemDropList.GetCount(); i++)
					{
						ItemDrop tempItemDrop = (ItemDrop) mInPlayItemDropList.GetCurrent().gameObject;

						if (tempItemDrop != null)
						{
							tempItemDrop.Hero = mHero;
							if (mInPlayItemDropList.GetCurrent().gameObject != null &&
								mInPlayItemDropList.GetCurrent()
												   .gameObject.Sprite.SpriteFrame.Intersects(mHero.Sprite.SpriteFrame))
							{
								if (mInPlayItemDropList.GetCurrent().gameObject.CreatureType == ObjectType.GOLD)
								{

									int temp = randomGenerator.Next(0, 3);
									switch (temp)
									{
										case 0:
											AudioController.GoldDropCoin1SoundEffectInstance.Play();
											break;
										case 1:
											AudioController.GoldDropCoin2SoundEffectInstance.Play();
											break;
										case 2:
											AudioController.GoldDropCoin3SoundEffectInstance.Play();
											break;
									}


								}

								else if (mInPlayItemDropList.GetCurrent().gameObject.CreatureType == ObjectType.HEALTH)
								{
									AudioController.HealthDropSoundEffectInstance.Play();
								}
								tempItemDrop.IsTagged = true;
							}

							mInPlayItemDropList.NextNode();
						}
					}
				}
		   
		 
		}

		public void UpdateInPlayObjects()
		{
			mInPlayObjectList.Reset();


			for (int i = 0; i < mInPlayObjectList.GetCount(); i++)
			{
				mInPlayObjectList.GetCurrent().gameObject.Sprite.Update(gameTime);
				mInPlayObjectList.GetCurrent().gameObject.Update(gameTime);

				if (mInPlayObjectList.GetCurrent().gameObject is Enemy)
				{
					Enemy tempEnemy = (Enemy)mInPlayObjectList.GetCurrent().gameObject;

					if (tempEnemy.EnemyAttribute.CurrentHealthPoints <= 0)
					{


						if (tempEnemy.HasDied)
						{
							DropHealthOrb(WaveController.WaveNumber, tempEnemy);
							mHero.HeroAttribute.Experience += DropExperience(WaveController.WaveNumber, tempEnemy);
							DropGold(WaveController.WaveNumber, tempEnemy);
							KillGameObject();
						}

					}
				}
				else if (mInPlayObjectList.GetCurrent().gameObject is Friendly)
				{
					Friendly tempFriendly = (Friendly)mInPlayObjectList.GetCurrent().gameObject;

					if (tempFriendly.HasDied)
					{

						#region Subtracting From Structure Count

						switch (tempFriendly.CreatureType)
						{
							case ObjectType.DRAGON:
								switch (tempFriendly.Color)
								{
									case ObjectColor.BLUE:
										if (InPlayStructures[0] != null)
											InPlayStructures[0].StructureAttribute.CurrentAmountDragons--;
										break;
									case ObjectColor.RED:
										if (InPlayStructures[1] != null)
											InPlayStructures[1].StructureAttribute.CurrentAmountDragons--;
										break;
									case ObjectColor.YELLOW:
										if (InPlayStructures[2] != null)
											InPlayStructures[2].StructureAttribute.CurrentAmountDragons--;
										break;
									case ObjectColor.GREEN:
										if (InPlayStructures[3] != null)
											InPlayStructures[3].StructureAttribute.CurrentAmountDragons--;
										break;
									case ObjectColor.PURPLE:
										if (InPlayStructures[4] != null)
											InPlayStructures[4].StructureAttribute.CurrentAmountDragons--;
										break;

									default:
										Console.WriteLine("Problems in Update Object List (Friendly)");
										break;
								}
								break;
							case ObjectType.NECROMANCER:
								switch (tempFriendly.Color)
								{
									case ObjectColor.BLUE:
										if (InPlayStructures[0] != null)
											InPlayStructures[0].StructureAttribute.CurrentAmountNecromancers--;
										break;
									case ObjectColor.RED:
										if (InPlayStructures[1] != null)
											InPlayStructures[1].StructureAttribute.CurrentAmountNecromancers--;
										break;
									case ObjectColor.YELLOW:
										if (InPlayStructures[2] != null)
											InPlayStructures[2].StructureAttribute.CurrentAmountNecromancers--;
										break;
									case ObjectColor.GREEN:
										if (InPlayStructures[3] != null)
											InPlayStructures[3].StructureAttribute.CurrentAmountNecromancers--;
										break;
									case ObjectColor.PURPLE:
										if (InPlayStructures[4] != null)
											InPlayStructures[4].StructureAttribute.CurrentAmountNecromancers--;
										break;

									default:
										Console.WriteLine("Problems in Update Object List (Friendly)");

										break;
								}

								break;
							case ObjectType.ARCANE_MAGE:
								switch (tempFriendly.Color)
								{
									case ObjectColor.BLUE:
										if (InPlayStructures[0] != null)
											InPlayStructures[0].StructureAttribute.CurrentAmountArcaneMages--;
										break;
									case ObjectColor.RED:
										if (InPlayStructures[1] != null)
											InPlayStructures[1].StructureAttribute.CurrentAmountArcaneMages--;
										break;
									case ObjectColor.YELLOW:
										if (InPlayStructures[2] != null)
											InPlayStructures[2].StructureAttribute.CurrentAmountArcaneMages--;
										break;
									case ObjectColor.GREEN:
										if (InPlayStructures[3] != null)
											InPlayStructures[3].StructureAttribute.CurrentAmountArcaneMages--;
										break;
									case ObjectColor.PURPLE:
										if (InPlayStructures[4] != null)
											InPlayStructures[4].StructureAttribute.CurrentAmountArcaneMages--;
										break;

									default:
										Console.WriteLine("Problems in Update Object List (Friendly)");

										break;
								}
								break;
							case ObjectType.FIRE_MAGE:
								switch (tempFriendly.Color)
								{
									case ObjectColor.BLUE:
										if (InPlayStructures[0] != null)
											InPlayStructures[0].StructureAttribute.CurrentAmountFireMages--;
										break;
									case ObjectColor.RED:
										if (InPlayStructures[1] != null)
											InPlayStructures[1].StructureAttribute.CurrentAmountFireMages--;
										break;
									case ObjectColor.YELLOW:
										if (InPlayStructures[2] != null)
											InPlayStructures[2].StructureAttribute.CurrentAmountFireMages--;
										break;
									case ObjectColor.GREEN:
										if (InPlayStructures[3] != null)
											InPlayStructures[3].StructureAttribute.CurrentAmountFireMages--;
										break;
									case ObjectColor.PURPLE:
										if (InPlayStructures[4] != null)
											InPlayStructures[4].StructureAttribute.CurrentAmountFireMages--;
										break;

									default:
										Console.WriteLine("Problems in Update Object List (Friendly)");

										break;
								}
								break;
							case ObjectType.WOLF:
								switch (tempFriendly.Color)
								{
									case ObjectColor.BLUE:
										if (InPlayStructures[0] != null)
											InPlayStructures[0].StructureAttribute.CurrentAmountWolfs--;
										break;
									case ObjectColor.RED:
										if (InPlayStructures[1] != null)
											InPlayStructures[1].StructureAttribute.CurrentAmountWolfs--;
										break;
									case ObjectColor.YELLOW:
										if (InPlayStructures[2] != null)
											InPlayStructures[2].StructureAttribute.CurrentAmountWolfs--;
										break;
									case ObjectColor.GREEN:
										if (InPlayStructures[3] != null)
											InPlayStructures[3].StructureAttribute.CurrentAmountWolfs--;
										break;
									case ObjectColor.PURPLE:
										if (InPlayStructures[4] != null)
											InPlayStructures[4].StructureAttribute.CurrentAmountWolfs--;
										break;

									default:
										Console.WriteLine("Problems in Update Object List (Friendly)");

										break;
								}
								break;
							case ObjectType.AXE_THROWER:
								switch (tempFriendly.Color)
								{
									case ObjectColor.BLUE:
										if (InPlayStructures[0] != null)
											InPlayStructures[0].StructureAttribute.CurrentAmountAxeThrowers--;
										break;
									case ObjectColor.RED:
										if (InPlayStructures[1] != null)
											InPlayStructures[1].StructureAttribute.CurrentAmountAxeThrowers--;
										break;
									case ObjectColor.YELLOW:
										if (InPlayStructures[2] != null)
											InPlayStructures[2].StructureAttribute.CurrentAmountAxeThrowers--;
										break;
									case ObjectColor.GREEN:
										if (InPlayStructures[3] != null)
											InPlayStructures[3].StructureAttribute.CurrentAmountAxeThrowers--;
										break;
									case ObjectColor.PURPLE:
										if (InPlayStructures[4] != null)
											InPlayStructures[4].StructureAttribute.CurrentAmountAxeThrowers--;
										break;

									default:
										Console.WriteLine("Problems in Update Object List (Friendly)");

										break;
								}
								break;
							case ObjectType.BERSERKER:
								switch (tempFriendly.Color)
								{
									case ObjectColor.BLUE:
										if (InPlayStructures[0] != null)
											InPlayStructures[0].StructureAttribute.CurrentAmountBerserkers--;
										break;
									case ObjectColor.RED:
										if (InPlayStructures[1] != null)
											InPlayStructures[1].StructureAttribute.CurrentAmountBerserkers--;
										break;
									case ObjectColor.YELLOW:
										if (InPlayStructures[2] != null)
											InPlayStructures[2].StructureAttribute.CurrentAmountBerserkers--;
										break;
									case ObjectColor.GREEN:
										if (InPlayStructures[3] != null)
											InPlayStructures[3].StructureAttribute.CurrentAmountBerserkers--;
										break;
									case ObjectColor.PURPLE:
										if (InPlayStructures[4] != null)
											InPlayStructures[4].StructureAttribute.CurrentAmountBerserkers--;
										break;

									default:
										Console.WriteLine("Problems in Update Object List (Friendly)");

										break;
								}
								break;
							case ObjectType.CLERIC:
								switch (tempFriendly.Color)
								{
									case ObjectColor.BLUE:
										if (InPlayStructures[0] != null)
											InPlayStructures[0].StructureAttribute.CurrentAmountClerics--;
										break;
									case ObjectColor.RED:
										if (InPlayStructures[1] != null)
											InPlayStructures[1].StructureAttribute.CurrentAmountClerics--;
										break;
									case ObjectColor.YELLOW:
										if (InPlayStructures[2] != null)
											InPlayStructures[2].StructureAttribute.CurrentAmountClerics--;
										break;
									case ObjectColor.GREEN:
										if (InPlayStructures[3] != null)
											InPlayStructures[3].StructureAttribute.CurrentAmountClerics--;
										break;
									case ObjectColor.PURPLE:
										if (InPlayStructures[4] != null)
											InPlayStructures[4].StructureAttribute.CurrentAmountClerics--;
										break;

									default:
										Console.WriteLine("Problems in Update Object List (Friendly)");

										break;
								}
								break;
							default:
								Console.WriteLine("Problems in Update Object List");
								break;
						}

						#endregion

						KillGameObject();
					}
				}
				else if (mInPlayObjectList.GetCurrent().gameObject is Castle)
				{
					Castle tempCastle = (Castle)mInPlayObjectList.GetCurrent().gameObject;
					if (tempCastle.HasDied)
					{
						KillGameObject();
						gameState = GameState.MAIN_MENU;
					}
				}

				else if (mInPlayObjectList.GetCurrent().gameObject is Structure)
				{
					Structure tempStructure = (Structure)mInPlayObjectList.GetCurrent().gameObject;
					if (tempStructure.HasDied)
					{
						KillGameObject();
					}
				}

				else if (mInPlayObjectList.GetCurrent().gameObject is Hero)
				{
					Hero tempHero = (Hero)mInPlayObjectList.GetCurrent().gameObject;
					if (tempHero.HasDied)
					{
						isRespawning = true;
						while (isRespawning)
						{
							mRespawnTimer += gameTime.ElapsedGameTime.Milliseconds;
							if (mRespawnTimer >= 500000)
							{
								mHero.HeroAttribute.CurrentHealthPoints = mHero.HeroAttribute.MaxHealthPoints;
								mHero.Sprite.WorldPosition = new Vector2(800,800);
								isRespawning = false;
								mHero.HasDied = false;
							}

						}
					   
						// KillGameObject();
					}
				}

				mInPlayObjectList.NextNode();
			}


		}

		public void DestroyProjectile()
		{
			Graveyard.InsertLast(mInPlayProjectileList.GetCurrent().gameObject);
			mInPlayProjectileList.GetCurrent().gameObject.Sprite.WorldPosition = new Vector2(3000, 3000);
			mInPlayProjectileList.DeleteAt(GameObject.mInPlayProjectileList.GetCurrent().gameObject.ObjectID);
		}


		private void DestroyItemDrop()
		{
			Graveyard.InsertLast(mInPlayItemDropList.GetCurrent().gameObject);
			mInPlayItemDropList.GetCurrent().gameObject.Sprite.WorldPosition = new Vector2(3000, 3000);
			mInPlayItemDropList.DeleteAt(mInPlayItemDropList.GetCurrent().gameObject.ObjectID);
		}

		public void KillGameObject()
		{

			if (mInPlayObjectList.GetCurrent().gameObject.Hostility == Hostility.STRUCTURE)
			{


				//TODO: RESET THE STRUCTURE'S HP AFTER IT DIES SINCE WE ARE COPYING DATA FROM 
				// THE SAME THING
				Structure tempStructure = (Structure)mInPlayObjectList.GetCurrent().gameObject;
				switch (tempStructure.StructureColor)
				{
					case ObjectColor.BLUE:
						UserInterfaceController.BlueOccupationState = StructureOccupationState.UNOCCUPIED;
						break;
					case ObjectColor.YELLOW:
						UserInterfaceController.YellowOccupationState = StructureOccupationState.UNOCCUPIED;
						break;
					case ObjectColor.GREEN:
						UserInterfaceController.GreenOccupationState = StructureOccupationState.UNOCCUPIED;
						break;
					case ObjectColor.RED:
						UserInterfaceController.RedOccupationState = StructureOccupationState.UNOCCUPIED;
						break;
					case ObjectColor.PURPLE:
						UserInterfaceController.PurpleOccupationState = StructureOccupationState.UNOCCUPIED;
						break;

					default:
						break;
				}
			}

			Graveyard.InsertFirst(mInPlayObjectList.GetCurrent().gameObject);
			mInPlayObjectList.GetCurrent().gameObject.Sprite.WorldPosition = new Vector2(3000, 3000);
			GameObject.mPotentialTargetListList.DeleteAt(GameObject.mInPlayObjectList.GetCurrent().gameObject.ObjectID);

			mInPlayObjectList.DeleteAt(GameObject.mInPlayObjectList.GetCurrent().gameObject.ObjectID);


		}

		public void UpdateStructures()
		{


			if (InPlayStructures[0] != null)
			{
				mBlueTimeElapsed += gameTime.ElapsedGameTime.Milliseconds;

				InPlayStructures[0].Update(gameTime);
				InPlayStructures[0].Sprite.Update(gameTime);

				mSpawnTimer = InPlayStructures[0].StructureAttribute.SpawnTimer;


				if (mBlueTimeElapsed >= mSpawnTimer)
				{
					SpawnFriendly(0);
					mBlueTimeElapsed = 0f;
				}

			}

			if (InPlayStructures[1] != null)
			{
				mRedTimeElapsed += gameTime.ElapsedGameTime.Milliseconds;

				InPlayStructures[1].Update(gameTime);
				InPlayStructures[1].Sprite.Update(gameTime);

				mSpawnTimer = InPlayStructures[1].StructureAttribute.SpawnTimer;


				if (mRedTimeElapsed >= mSpawnTimer)
				{
					SpawnFriendly(1);
					mRedTimeElapsed = 0f;
				}

			}

			if (InPlayStructures[2] != null)
			{
				mYellowTimeElapsed += gameTime.ElapsedGameTime.Milliseconds;

				InPlayStructures[2].Update(gameTime);
				InPlayStructures[2].Sprite.Update(gameTime);

				mSpawnTimer = InPlayStructures[2].StructureAttribute.SpawnTimer;


				if (mYellowTimeElapsed >= mSpawnTimer)
				{
					SpawnFriendly(2);
					mYellowTimeElapsed = 0f;
				}

			}

			if (InPlayStructures[3] != null)
			{
				mGreenTimeElapsed += gameTime.ElapsedGameTime.Milliseconds;

				InPlayStructures[3].Update(gameTime);
				InPlayStructures[3].Sprite.Update(gameTime);

				mSpawnTimer = InPlayStructures[3].StructureAttribute.SpawnTimer;


				if (mGreenTimeElapsed >= mSpawnTimer)
				{
					SpawnFriendly(3);
					mGreenTimeElapsed = 0f;
				}

			}
			if (InPlayStructures[4] != null)
			{
				mPurpleTimeElapsed += gameTime.ElapsedGameTime.Milliseconds;

				InPlayStructures[4].Update(gameTime);
				InPlayStructures[4].Sprite.Update(gameTime);

				mSpawnTimer = InPlayStructures[4].StructureAttribute.SpawnTimer;


				if (mPurpleTimeElapsed >= mSpawnTimer)
				{
					SpawnFriendly(4);
					mPurpleTimeElapsed = 0f;
				}

			}
		}

		private void UpdateDebugger()
		{
			Debug.Update(gameTime);
			keyboardState = Keyboard.GetState();

			StartTime += gameTime.ElapsedGameTime.Milliseconds;
			if (keyboardState.IsKeyDown(Keys.OemTilde) && StartTime >= Debug.mDelayInMilliseconds)
			{

				Debug.ToggleDebugState();
				StartTime = 0.0f;
			}

		}

		#endregion

		#region Draw Functions

		public void Draw()
		{


			//GameObject.GetSprite.Draw
			switch (gameState)
			{
				case GameState.MAIN_MENU:
					UserInterfaceController.DrawMainMenu(spriteBatch);

					UserInterfaceController.DrawMouse(spriteBatch);
					break;

				case GameState.ACTIVE:


					mInPlayObjectList = GameObject.mInPlayObjectList;
					mDirtLevel.Draw(spriteBatch);


					DrawInPlayObjects();


					//spriteBatch.Draw(mRedBar, mCastleHealthBarRectangle, source, Color.White, MathHelper.ToRadians(180), new Vector2(source.Width/2,source.Height/2), SpriteEffects.None, 0);

					spriteBatch.Draw(mRedBar, new Rectangle(1560, 280, 50, (int)(400 * (mCastle.CastleAttribute.CurrentHealthPoints / mCastle.CastleAttribute.MaxHealthPoints))), new Rectangle(1560, 280, 50, 400), Color.Red);


					DrawInPlayProjectiles();
					DrawInPlayItemDrops();
					if (!mDirtLevel.PlayingMusic)
						mDirtLevel.PlayLevelMusic();


					
					UserInterfaceController.DrawUIActiveState(spriteBatch);
					Notification.DrawDamageNumbers(spriteBatch);
					Notification.DrawHealingNumbers(spriteBatch);
					Notification.DrawLevelBanner(spriteBatch);
					break;

				case GameState.PAUSE:
					break;

				case GameState.ROUND_OVER:
					UserInterfaceController.DrawWaveComplete(spriteBatch);
					break;

				case GameState.GAME_OVER:
					break;
			}

			DrawDebugInterface();

		}



		public void DrawInPlayItemDrops()
		{
			mInPlayItemDropList.Reset();
			for (int i = 0; i < mInPlayItemDropList.GetCount(); i++)
			{
				ItemDrop tempItemDrop = (ItemDrop)mInPlayItemDropList.GetCurrent().gameObject;

				if (tempItemDrop != null)
					tempItemDrop.Draw(spriteBatch);

				mInPlayItemDropList.NextNode();
			}

		}
		public void DrawInPlayProjectiles()
		{
			if (!mInPlayProjectileList.IsEmpty())
			{
				mInPlayProjectileList.Reset();

				for (int i = 0; i < mInPlayProjectileList.GetCount(); i++)
				{
					Projectile currentProjectile = (Projectile)mInPlayProjectileList.GetCurrent().gameObject;


					currentProjectile.Draw(gameTime, spriteBatch);

					mInPlayProjectileList.NextNode();
				}

			}
		}


		public void DrawInPlayObjects()
		{
			mInPlayObjectList.Reset();

			for (int i = 0; i < mInPlayObjectList.GetCount(); i++)
			{
				mInPlayObjectList.GetCurrent().gameObject.Sprite.Draw(spriteBatch, 1.0f);

				mInPlayObjectList.NextNode();

			}
		}





		public void DrawStructures()
		{
			for (int i = 0; i < InPlayStructures.Length; i++)
			{
				if (InPlayStructures[i] != null)
				{
					InPlayStructures[i].Sprite.Draw(spriteBatch, .6f);
				}
			}
		}


		public void DrawDebugInterface()
		{
			Debug.Draw();

			if (Debug.mSpriteIDState == DebugSpriteID.SPRITEID_ENABLED)
			{
				Debug.DrawSpriteID();
			}

			if (Debug.mTargetQueueState == DebugTargetQueue.TARGET_QUEUE_ENABLED)
			{
				Debug.DrawTargetQueue();
			}

			if (mDebugGridState == DebugGrid.GRID_ENABLED)
			{
				spriteBatch.Draw(mDebugGridTexture, new Vector2(0, 0), Color.White);
			}
		}

		#endregion

		#region Utility Functions

		public void GarbageCollectTheLists()
		{
			Graveyard.Reset();
			for (int i = 0; i < Graveyard.GetCount(); i++)
			{
				Graveyard.GetCurrent().gameObject = null;
				Graveyard.NextNode();
			}

			mInPlayItemDropList.Reset();
			for (int i = 0; i < mInPlayItemDropList.GetCount(); i++)
			{
				mInPlayItemDropList.GetCurrent().gameObject = null;
				mInPlayItemDropList.NextNode();
			}

			mInPlayProjectileList.Reset();
			for (int i = 0; i < mInPlayProjectileList.GetCount(); i++)
			{
				mInPlayProjectileList.GetCurrent().gameObject = null;
				mInPlayProjectileList.NextNode();
			}

			mInPlayObjectList.Reset();

			for (int i = 0; i < mInPlayObjectList.GetCount(); i++)
			{
				if (mInPlayObjectList.GetCurrent().gameObject is Friendly)
				{
					mInPlayObjectList.DeleteAt(mInPlayObjectList.GetCurrent().gameObject.ObjectID);
				}
				if (mInPlayObjectList.GetCurrent().gameObject is Projectile)
				{
					mInPlayObjectList.DeleteAt(mInPlayObjectList.GetCurrent().gameObject.ObjectID);
				}
				if (mInPlayObjectList.GetCurrent().gameObject is ItemDrop)
				{
					mInPlayObjectList.DeleteAt(mInPlayObjectList.GetCurrent().gameObject.ObjectID);
				}

				mInPlayObjectList.NextNode();

			}

			GameObject.mPotentialTargetListList.Reset();
			for (int i = 0; i < GameObject.mPotentialTargetListList.GetCount(); i++)
			{



				if (GameObject.mPotentialTargetListList.GetCurrent().gameObject is Friendly)
				{
					GameObject.mPotentialTargetListList.DeleteAt(GameObject.mPotentialTargetListList.GetCurrent().gameObject.ObjectID);
				}
				if (GameObject.mPotentialTargetListList.GetCurrent().gameObject is Enemy)
				{
					GameObject.mPotentialTargetListList.DeleteAt(GameObject.mPotentialTargetListList.GetCurrent().gameObject.ObjectID);
				}

				GameObject.mPotentialTargetListList.NextNode();
			}

			GameObject.mInPlayItemDropList = mInPlayItemDropList;
			GameObject.mInPlayObjectList = mInPlayObjectList;
			GameObject.mInPlayProjectileList = mInPlayProjectileList;



			GC.Collect();
		}


		public void SpawnFriendly(int i)
		{
			bool notCapped = false;

			switch (InPlayStructures[i].UnitType)
			{
				case ObjectType.DRAGON:
					if (InPlayStructures[i].StructureAttribute.CurrentAmountDragons <
						InPlayStructures[i].StructureAttribute.MaxDragonAmount)
						notCapped = true;
					break;

				case ObjectType.NECROMANCER:
					if (InPlayStructures[i].StructureAttribute.CurrentAmountNecromancers <
						InPlayStructures[i].StructureAttribute.MaxNecromancerAmount)
						notCapped = true;
					break;

				case ObjectType.ARCANE_MAGE:
					if (InPlayStructures[i].StructureAttribute.CurrentAmountArcaneMages <
						InPlayStructures[i].StructureAttribute.MaxArcaneMageAmount)
						notCapped = true;
					break;

				case ObjectType.FIRE_MAGE:
					if (InPlayStructures[i].StructureAttribute.CurrentAmountFireMages <
						InPlayStructures[i].StructureAttribute.MaxFireMageAmount)
						notCapped = true;
					break;

				case ObjectType.AXE_THROWER:
					if (InPlayStructures[i].StructureAttribute.CurrentAmountAxeThrowers <
						InPlayStructures[i].StructureAttribute.MaxAxeThrowerAmount)
						notCapped = true;
					break;

				case ObjectType.BERSERKER:
					if (InPlayStructures[i].StructureAttribute.CurrentAmountBerserkers <
						InPlayStructures[i].StructureAttribute.MaxBerserkerAmount)
						notCapped = true;
					break;

				case ObjectType.WOLF:
					if (InPlayStructures[i].StructureAttribute.CurrentAmountWolfs <
						InPlayStructures[i].StructureAttribute.MaxWolfAmount)
						notCapped = true;
					break;

				case ObjectType.CLERIC:
					if (InPlayStructures[i].StructureAttribute.CurrentAmountClerics <
						InPlayStructures[i].StructureAttribute.MaxClericAmount)
						notCapped = true;
					break;

				default:
					Console.WriteLine("Spawn Friendly problems");
					break;
			}


			if (notCapped)
			{
				Friendly tempFriendly = new Friendly(InPlayStructures[i].UnitType, content, SpriteState.MOVE_LEFT,
													 Vector2.Zero, InPlayStructures[i].StructureAttribute.DefaultUnitTarget, InPlayStructures[i].StructureAttribute.AttackLevel, InPlayStructures[i].StructureAttribute.DefenseLevel);

				mInPlayObjectList.InsertLast(tempFriendly);
				GameObject.mPotentialTargetListList.InsertLast(tempFriendly);

				tempFriendly.Sprite.LoadContent();
				tempFriendly.Sprite.WorldPosition =
					new Vector2(InPlayStructures[i].Sprite.WorldPosition.X - tempFriendly.Sprite.SpriteFrame.Width,
								InPlayStructures[i].Sprite.WorldPosition.Y);
				tempFriendly.Color = InPlayStructures[i].StructureColor;
				tempFriendly.PostSetFOV();

				PassGameTime();

				switch (InPlayStructures[i].UnitType)
				{
					case ObjectType.DRAGON:
						InPlayStructures[i].StructureAttribute.CurrentAmountDragons++;
						break;

					case ObjectType.NECROMANCER:
						InPlayStructures[i].StructureAttribute.CurrentAmountNecromancers++;
						break;

					case ObjectType.ARCANE_MAGE:
						InPlayStructures[i].StructureAttribute.CurrentAmountArcaneMages++;
						break;

					case ObjectType.FIRE_MAGE:
						InPlayStructures[i].StructureAttribute.CurrentAmountFireMages++;
						break;

					case ObjectType.AXE_THROWER:
						InPlayStructures[i].StructureAttribute.CurrentAmountAxeThrowers++;
						break;

					case ObjectType.BERSERKER:
						InPlayStructures[i].StructureAttribute.CurrentAmountBerserkers++;
						break;

					case ObjectType.WOLF:
						InPlayStructures[i].StructureAttribute.CurrentAmountWolfs++;
						break;

					case ObjectType.CLERIC:
						InPlayStructures[i].StructureAttribute.CurrentAmountClerics++;
						break;

					default:
						Console.WriteLine("Spawn Friendly problems");
						break;
				}


					notCapped = false;
				
			}


		}

		public void SpawnEnemy()
		{
			EnemySpawnTimer += gameTime.ElapsedGameTime.Milliseconds;
			//TODO: change spawn timer
			if (WaveController.WaveNumber == 1)
			{
				waitTimer = 10000000;
			}
			else
			{
				waitTimer = 500000000;
			}

			if (RoundTimer >= waitTimer)
			{

				bool killedWave = false;



				mInPlayObjectList.Reset();
				for (int i = 0; i < mInPlayObjectList.GetCount(); i++)
				{
					if (mInPlayObjectList.GetCurrent().gameObject.Hostility == Hostility.ENEMY)
					{
						break;
					}
					else if (mInPlayObjectList.AtEnd())
					{
						killedWave = true;
					}
					mInPlayObjectList.NextNode();
				}


				if (!WaveController.WaveEnemies.IsEmpty())
				{


					if (EnemySpawnTimer >= EnemyTimeToSpawn || killedWave)
					{
					   
						EnemyTimeToSpawn = randomGenerator.Next(7000, 15000);
						EnemySpawnTimer = 0;


						int randomNumberOfEnemiesRoll = randomGenerator.Next(1, 5);
						int enemySpawnNumber = 0;

						switch (randomNumberOfEnemiesRoll)
						{
							case 1:
								enemySpawnNumber = 1;
								break;

							case 2:
								enemySpawnNumber = 2;
								break;

							case 3:
								enemySpawnNumber = 3;
								break;

							case 4:
								enemySpawnNumber = 4;
								break;

							case 5:
								enemySpawnNumber = 5;
								break;

							default:
								Console.WriteLine("Problem in Spawn Enemy");
								break;
						}


						for (int i = 0; i < enemySpawnNumber; i++)
						{

							if (!WaveController.WaveEnemies.IsEmpty())
							{

								float x = 0;
								float y = randomGenerator.Next(100, 700);

								Enemy tempEnemy = (Enemy)WaveController.WaveEnemies.PeekFirst();

								y = randomGenerator.Next(75, 700);

								tempEnemy.DefaultTarget = new Rectangle(1400, (int)y, 200, 775);
								GameObject.mInPlayObjectList.InsertLast(WaveController.WaveEnemies.PeekFirst());
								GameObject.mPotentialTargetListList.InsertLast(WaveController.WaveEnemies.PeekFirst());
								WaveController.WaveEnemies.PeekFirst().Sprite.WorldPosition = new Vector2(x, y);
								tempEnemy.PostSetSpriteFrame();
								WaveController.WaveEnemies.PopTarget();

							}
							else
							{
								break;
							}
						}
					}
				}

				timer += gameTime.ElapsedGameTime.Milliseconds;

				if (WaveController.WaveEnemies.IsEmpty() && timer >= 200)
				{
					bool allEnemiesDead = true;

					mInPlayObjectList.Reset();

					for (int i = 0; i < mInPlayObjectList.GetCount(); i++)
					{
						if (mInPlayObjectList.GetCurrent().gameObject is Enemy)
						{
							allEnemiesDead = false;
							break;
						}

						mInPlayObjectList.NextNode();
					}

					timer = 0;

					if (allEnemiesDead)
					{
						ChangeGameState(GameState.ROUND_OVER);
					}

				}

			}

		}


		public void EnableGodMode()
		{
			mGodMode = GodMode.ACTIVATED;
		}

		public void DisplayGrid()
		{
			if (mDebugGridState == DebugGrid.GRID_ENABLED)
			{
				mDebugGridState = DebugGrid.GRID_DISABLED;
			}
			else if (mDebugGridState == DebugGrid.GRID_DISABLED)
			{
				mDebugGridState = DebugGrid.GRID_ENABLED;
			}
		}

		public void PassGameTime()
		{

			mInPlayObjectList.Reset();
			for (int i = 0; i < mInPlayObjectList.GetCount(); i++)
			{
				mInPlayObjectList.GetCurrent().gameObject.gameTime = gameTime;
				mInPlayObjectList.NextNode();
			}
		}

		public void ChangeGameState(GameState state)
		{

			switch (state)
			{

				case GameState.LOAD:
					gameState = GameState.MAIN_MENU;
					break;

				case GameState.MAIN_MENU:
					gameState = GameState.ACTIVE;
					RoundTimer = 0;
					break;

				case GameState.ACTIVE:

					break;

				case GameState.PAUSE:
					break;

				case GameState.ROUND_OVER:
				   
			MediaPlayer.Stop();
			MediaPlayer.IsRepeating = false;
			levelComplete = content.Load<Song>("Audio/Songs/Level Complete Music");
			MediaPlayer.Volume = 100;
			MediaPlayer.Play(levelComplete);
			
			gameState = GameState.ACTIVE;
					GarbageCollectTheLists();
					ResetVariablesForNextWave();

					break;

				case GameState.GAME_OVER:
					break;
			}

		}

		#endregion

		#region ItemDrop Functions



		public void DropHealthOrb(int WaveNumber, Enemy mEnemyActor)
		{
			// Base gold at 12.
			// Gold Spread Modifier = 20%
			// Gold Drop Modifier = 5%
			// Wave 10 
			// Tier 1
			int dropChance = randomGenerator.Next(0, 101);

			//               20% chance to get in
			if (dropChance <= 20)
			{
				// initial gold drop calculation formula.
				mInitialHealthDrop = mBaseHealth * (1 + mHealthModifier * (WaveNumber * mEnemyActor.EnemyAttribute.Tier));

				// calculate a range +- that we can add or subtract from our final gold drop.
				int negativeTempHealthSpread = (int)Math.Round(-1 * (mHealthSpreadModifier * mInitialHealthDrop));

				int positiveTempHealthSpread = (int)Math.Round(mHealthSpreadModifier * mInitialHealthDrop);

				// Gold spread will be a random number +- between the gold drop amount and the spread modifier.
				mHealthSpread = randomGenerator.Next(negativeTempHealthSpread, positiveTempHealthSpread);

				// Add the random spread to the gold drop amount.
				int finalHealthDrop = (int)(Math.Round(mInitialHealthDrop + mHealthSpread));


				// Create the health drop with animation.
				mInPlayItemDropList.InsertLast(new ItemDrop(finalHealthDrop, ObjectType.HEALTH, content,
															SpriteState.ITEMDROP,
															mEnemyActor.Sprite.WorldPosition, Hostility.ITEMDROP));
				hasAlreadyDropped = true;
			}


		}

		public int DropExperience(int WaveNumber, Enemy mEnemyActor)
		{
			// Base XP at 12.
			// Experience Spread Modifier = 20%
			// Experience Modifier = 15%
			// Wave 10 
			// Tier 1

			// XP = (Base XP) * (1 + XPModifier * (WaveNumber * Tier) )
			mInitialExperience = mBaseExperience * (1 + mExperienceModifier * (WaveNumber * mEnemyActor.EnemyAttribute.Tier));

			// calculate a range +- that we can add or subtract from our final experience drop.
			int negativeTempExperienceSpread = (int)(Math.Round(-1 * (mExperienceSpreadModifier * mInitialExperience)));
			int positiveTempExperienceSpread = (int)(Math.Round(mExperienceSpreadModifier * mInitialExperience));

			// Experience spread will be a random number +- between the gold drop amount and the spread modifier.
			mExperienceSpread = randomGenerator.Next(negativeTempExperienceSpread, positiveTempExperienceSpread);

			int finalExperienceDrop = (int)(Math.Round(mInitialExperience + mExperienceSpread));

			return finalExperienceDrop;
		}



		public void DropGold(int WaveNumber, Enemy mEnemyActor)
		{

			// Base gold at 12.
			// Gold Spread Modifier = 20%
			// Gold Drop Modifier = 5%
			// Wave 10 
			// Tier 1
			int dropChance = randomGenerator.Next(0, 101);

			if (dropChance <= 75 && !hasAlreadyDropped)
			{

				// initial gold drop calculation formula.
				mInitialGoldDrop = mBaseGold * (1 + mGoldModifier * (WaveNumber * mEnemyActor.EnemyAttribute.Tier));

				// calculate a range +- that we can add or subtract from our final gold drop.
				int negativeTempGoldSpread = (int)Math.Round(-1 * (mGoldSpreadModifier * mInitialGoldDrop));

				int positiveTempGoldSpread = (int)Math.Round(mGoldSpreadModifier * mInitialGoldDrop);

				// Gold spread will be a random number +- between the gold drop amount and the spread modifier.
				mGoldSpread = randomGenerator.Next(negativeTempGoldSpread, positiveTempGoldSpread);

				// Add the random spread to the gold drop amount.
				int finalGoldDrop = (int)(Math.Round(mInitialGoldDrop + mGoldSpread));


				// Create the gold drop with animation.
				mInPlayItemDropList.InsertLast(new ItemDrop(finalGoldDrop, ObjectType.GOLD, content,
															SpriteState.ITEMDROP, mEnemyActor.Sprite.WorldPosition,
															Hostility.ITEMDROP));
			}
			hasAlreadyDropped = false;
		}

		#endregion


	}

}