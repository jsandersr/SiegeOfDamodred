using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExternalTypes;
using GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;



namespace SiegeOfDamodred
{
	public class WaveController
	{
		private static Random randomGenerator;

		// The wave at which that object spawns at maximum rate.
		private static int[] EnemyTypeExpectedWavePeak;

		// The window of wave numbers around peak that the object spawns at minimum rate.
		private static int[] EnemyTypePeakWidth;


		// The wave at which that object spawns at maximum rate.
		private static int[] NumberSpawnedExpectedWavePeak;

		// The window of wave numbers around peak that the object spawns at minimum rate.
		private static int[] NumberSpawnedPeakWidth;


		private static double[] spawnChances;

		private static int randomNumber;


		private static int mCurrentImpSpawnChance;
		private static int mCurrentDoomHoundSpawnChance;
		private static int mCurrentGogSpawnChance;
		private static int mCurrentBansheeSpawnChance;
		private static int mCurrentReaperSpawnChance;

		private static double waveResolution = 1;



		private static int mNumberOfEnemies = 30;
		private static JTargetQueue mWaveEnemies;
		private static int waveNumber = 1;
		private static Castle mCastle;
		private static ContentManager content;

		public static void InitializeWaveController()
		{

			EnemyTypePeakWidth = new int[5];
			EnemyTypeExpectedWavePeak = new int[5];
			spawnChances = new double[5];


			// Increase number to make units appear at later waves.
			EnemyTypeExpectedWavePeak[0] = 0;
			EnemyTypeExpectedWavePeak[1] = 40;
			EnemyTypeExpectedWavePeak[2] = 80;
			EnemyTypeExpectedWavePeak[3] = 80;
			EnemyTypeExpectedWavePeak[4] = 100;

			//  Increase numbers to increase spread.
			EnemyTypePeakWidth[0] = 70;
			EnemyTypePeakWidth[1] = 40;
			EnemyTypePeakWidth[2] = 37;
			EnemyTypePeakWidth[3] = 30;
			EnemyTypePeakWidth[4] = 30;

			// Increase number to make units appear at later waves.


			randomGenerator = new Random();
			mWaveEnemies = new JTargetQueue();
			randomNumber = 0;




			mNumberOfEnemies += (int)(Math.Pow(waveNumber, 0.8));

			PopulateWaveEnemies();

		}

		public static void ReinitializeWaveController()
		{
			waveNumber++;
			randomGenerator = new Random();
			randomNumber = 0;
			mNumberOfEnemies += (int)(Math.Pow(waveNumber, 0.8));

			while (!mWaveEnemies.IsEmpty())
			{
				mWaveEnemies.PopTarget();
			}

			PopulateWaveEnemies();
		}

		private static void PrintWaveStats()
		{
			Console.WriteLine("WaveController Number" + waveNumber + " " + mCurrentImpSpawnChance.ToString() + " " + mCurrentDoomHoundSpawnChance.ToString() + " " + mCurrentGogSpawnChance.ToString() + " " + mCurrentBansheeSpawnChance.ToString() + " " + mCurrentReaperSpawnChance.ToString() + " ");
			//   Console.WriteLine(spawnChances[0]);

		}

		#region Properties

		public static JTargetQueue WaveEnemies
		{
			get { return mWaveEnemies; }
		}

		public static ContentManager Content
		{
			set { content = value; }
		}

		public static Castle Castle
		{
			set { mCastle = value; }
		}

		public static int WaveNumber
		{
			get { return waveNumber; }
			set { waveNumber = value; }
		}

		#endregion

		private static void RollAttributes()
		{
			CalculateSpawnChances();
			double total = 0;
			for (int i = 0; i < spawnChances.Length; i++)
			{
				total += spawnChances[i];

			}


			mCurrentImpSpawnChance = (int)(Math.Round(spawnChances[0] / total * mNumberOfEnemies));
			mCurrentDoomHoundSpawnChance = (int)(Math.Round(spawnChances[1] / total * mNumberOfEnemies));
			mCurrentGogSpawnChance = (int)(Math.Round(spawnChances[2] / total * mNumberOfEnemies));
			mCurrentBansheeSpawnChance = (int)(Math.Round(spawnChances[3] / total * mNumberOfEnemies));
			mCurrentReaperSpawnChance = (int)(Math.Round(spawnChances[4] / total * mNumberOfEnemies));

			mCurrentImpSpawnChance = mCurrentImpSpawnChance * 100 / mNumberOfEnemies;
			mCurrentDoomHoundSpawnChance = mCurrentDoomHoundSpawnChance * 100 / mNumberOfEnemies;
			mCurrentGogSpawnChance = mCurrentGogSpawnChance * 100 / mNumberOfEnemies;
			mCurrentBansheeSpawnChance = mCurrentBansheeSpawnChance * 100 / mNumberOfEnemies;
			mCurrentReaperSpawnChance = mCurrentReaperSpawnChance * 100 / mNumberOfEnemies;
			PrintWaveStats();

		}

		public static void CalculateSpawnChances()
		{

			for (int i = 0; i < spawnChances.Length; i++)
			{
				spawnChances[i] = (Math.Exp(-(double)(((waveNumber / waveResolution) - EnemyTypeExpectedWavePeak[i]) * ((waveNumber / waveResolution) - EnemyTypeExpectedWavePeak[i])) /
										   ((EnemyTypePeakWidth[i] * EnemyTypePeakWidth[i]))));
			}

		}




		//private static void InitializeAttributes()
		//{
		//    mImpSpawnLimit = 15;
		//    mDoomHoundSpawnLimit = 20;
		//    mGogSpawnLimit = 20;
		//    mBansheeSpawnLimit = 25;
		//    mReaperSpawnLimit = 20;

		//    mImpStartingHealth = 20;
		//    mImpCurrentHealth = mImpStartingHealth;
		//    mImpStartingArmor = 1;
		//    mImpCurrentArmor = mImpStartingArmor;
		//    mImpStartingDamage = 10;
		//    mImpCurrentDamage = mImpStartingDamage;

		//    mDoomHoundStartingHealth = 45;
		//    mDoomHoundCurrentHealth = mDoomHoundStartingHealth;
		//    mDoomHoundStartingArmor = 2;
		//    mDoomHoundCurrentArmor = mDoomHoundStartingArmor;
		//    mDoomHoundStartingDamage = 20;
		//    mDoomHoundCurrentDamage = mDoomHoundStartingDamage;

		//    mGogStartingHealth = 100;
		//    mGogCurrentHealth = mGogStartingHealth;
		//    mGogStartingArmor = 2;
		//    mGogCurrentArmor = mGogStartingArmor;
		//    mGogStartingDamage = 20;
		//    mGogCurrentDamage = mGogStartingDamage;

		//    mBansheeCurrentHealth = 250;
		//    mBansheeCurrentHealth = mBansheeStartingHealth;
		//    mBansheeCurrentArmor = 5;
		//    mBansheeCurrentArmor = mBansheeStartingArmor;
		//    mBansheeStartingDamage = 35;
		//    mBansheeCurrentDamage = mBansheeStartingDamage;

		//    mReaperStartingHealth = 500;
		//    mReaperCurrentHealth = mReaperStartingHealth;
		//    mReaperStartingArmor = 25;
		//    mReaperCurrentArmor = mReaperStartingArmor;
		//    mReaperStartingDamage = 50;
		//    mReaperCurrentDamage = mReaperStartingDamage;


		//}


		private static Enemy CreateEnemy()
		{
			randomNumber = randomGenerator.Next(0, 100);

			if (randomNumber >= 0 && randomNumber <= mCurrentImpSpawnChance - 1)
			{
				//Spawn Imp

				//                Console.WriteLine("Initializing Imp");

				Enemy tempImp = new Enemy(ObjectType.IMP, content,
										  SpriteState.MOVE_RIGHT, mCastle,
										  Vector2.Zero);

				//tempImp.Damage = mImpCurrentDamage;
				tempImp.Sprite.LoadContent();
				tempImp.PostSetSpriteFrame();
				return tempImp;
			}

			else if (randomNumber >= mCurrentImpSpawnChance && randomNumber <= mCurrentDoomHoundSpawnChance + mCurrentImpSpawnChance - 1)
			{
				//spawn doom hound
				//    Console.WriteLine("Initializing DoomHound");

				Enemy tempDoomHound = new Enemy(ObjectType.DOOM_HOUND,
					content, SpriteState.MOVE_RIGHT, mCastle, Vector2.Zero);

				// tempDoomHound.Damage = mDoomHoundCurrentDamage;
				tempDoomHound.Sprite.LoadContent();
				tempDoomHound.PostSetSpriteFrame();
				return tempDoomHound;
			}
			else if (randomNumber >= mCurrentDoomHoundSpawnChance + mCurrentImpSpawnChance &&
					 randomNumber <= mCurrentDoomHoundSpawnChance + mCurrentImpSpawnChance + mCurrentGogSpawnChance - 1)
			{
				//spawn gog
				//       Console.WriteLine("Initializing Gog");

				Enemy tempGog = new Enemy(ObjectType.GOG, content,
										  SpriteState.MOVE_RIGHT, mCastle, Vector2.Zero);

				// tempGog.Damage = mGogCurrentDamage;
				tempGog.Sprite.LoadContent();
				tempGog.PostSetSpriteFrame();

				return tempGog;
			}

			else if (randomNumber >= mCurrentDoomHoundSpawnChance + mCurrentImpSpawnChance + mCurrentGogSpawnChance &&
					 randomNumber <= mCurrentDoomHoundSpawnChance + mCurrentImpSpawnChance + mCurrentGogSpawnChance + mCurrentBansheeSpawnChance - 1)
			{
				//spawn banshee
				//    Console.WriteLine("Initializing Banshee");

				Enemy tempBanshee = new Enemy(ObjectType.BANSHEE, content,
											  SpriteState.MOVE_RIGHT, mCastle, Vector2.Zero);

				// tempBanshee.Damage = mBansheeCurrentDamage;
				tempBanshee.Sprite.LoadContent();
				tempBanshee.PostSetSpriteFrame();

				return tempBanshee;
			}
			else if (randomNumber >= mCurrentDoomHoundSpawnChance + mCurrentImpSpawnChance + mCurrentGogSpawnChance + mCurrentBansheeSpawnChance &&
					 randomNumber <=
					 mCurrentDoomHoundSpawnChance + mCurrentImpSpawnChance + mCurrentGogSpawnChance + mCurrentBansheeSpawnChance +
					 mCurrentReaperSpawnChance - 1)
			{
				//spawn reaper
				//       Console.WriteLine("Initializing Reaper");

				Enemy tempReaper = new Enemy(ObjectType.REAPER, content,
											 SpriteState.MOVE_RIGHT, mCastle, Vector2.Zero);

				//  tempReaper.Damage = mReaperCurrentDamage;
				tempReaper.Sprite.LoadContent();
				tempReaper.PostSetSpriteFrame();

				return tempReaper;
			}
			else
			{
				Enemy tempImp = new Enemy(ObjectType.IMP, content,
											SpriteState.MOVE_RIGHT, mCastle,
											Vector2.Zero);

				// tempImp.Damage = mImpCurrentDamage;
				tempImp.Sprite.LoadContent();
				tempImp.PostSetSpriteFrame();
				return tempImp;
			}
		}

		private static void PopulateWaveEnemies()
		{
			RollAttributes();

			for (int i = 0; i < mNumberOfEnemies; i++)
			{
				mWaveEnemies.InsertTarget((Enemy)CreateEnemy());
			}

		}



	}
}
