using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExternalTypes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace GameObjects
{
    public class StructureAttribute : Attribute
    {
        private float mSpawnTimer;
        private int mUpgradeSpeedLevel; // This is the current level of speed.
        private int mTier;
        private Structure mStructure;
        private int mCost;
        private Vector2 mDefaultUnitTarget;
        private int mUpgradeAttackCost;
        private int mUpgradeDefenseCost;



        private static int mMaxWolfAmount = 4;
        private static int mMaxBerserkerAmount = 4;
        private static int mMaxAxeThrowerAmount = 4;
        private static int mMaxArcaneMageAmount = 4;
        private static int mMaxClericAmount = 2;
        private static int mMaxNecromancerAmount = 2;
        private static int mMaxFireMageAmount = 3;
        private static int mMaxDragonAmount = 2;

        private int mCurrentAmountWolfs;
        private int mCurrentAmountBerserkers;
        private int mCurrentAmountAxeThrowers;
        private int mCurrentAmountArcaneMages;
        private int mCurrentAmountClerics;
        private int mCurrentAmountNecromancers;
        private int mCurrentAmountFireMages;
        private int mCurrentAmountDragons;

        private static float mCoolDownTimer = 30000;
        private static CoolDownState mCoolDownState = CoolDownState.OFFCOOLDOWN;

        private int mAttackLevel;
        private int mDefenseLevel;

        public StructureAttribute(ContentManager content, Structure structure)
            : base(content)
        {
            this.mStructure = structure;
            this.mContent = content;
            mDefenseLevel = 1;
            mAttackLevel = 1;
            SetStructureDefaultTarget();
        }

        #region Properties

        public int UpgradeAttackCost
        {
            get { return mUpgradeAttackCost; }
            set { mUpgradeAttackCost = value; }
        }

        public int UpgradeDefenseCost
        {
            get { return mUpgradeDefenseCost; }
            set { mUpgradeDefenseCost = value; }
        }

        public int DefenseLevel
        {
            get { return mDefenseLevel; }
            set { mDefenseLevel = value; }
        }

        public int AttackLevel
        {
            get { return mAttackLevel; }
            set { mAttackLevel = value; }
        }

        public static float CoolDownTimer
        {
            get { return mCoolDownTimer; }
            set { mCoolDownTimer = value; }
        }

        public static CoolDownState CoolDownState
        {
            get { return mCoolDownState; }
            set { mCoolDownState = value; }
        }

        public int MaxWolfAmount
        {
            get { return mMaxWolfAmount; }
        }

        public int MaxDragonAmount
        {
            get { return mMaxDragonAmount; }
        }

        public int MaxFireMageAmount
        {
            get { return mMaxFireMageAmount; }
        }

        public int MaxNecromancerAmount
        {
            get { return mMaxNecromancerAmount; }
        }

        public int MaxClericAmount
        {
            get { return mMaxClericAmount; }
        }

        public int MaxArcaneMageAmount
        {
            get { return mMaxArcaneMageAmount; }
        }

        public int MaxAxeThrowerAmount
        {
            get { return mMaxAxeThrowerAmount; }
        }

        public int MaxBerserkerAmount
        {
            get { return mMaxBerserkerAmount; }
        }

        public int CurrentAmountWolfs
        {
            get { return mCurrentAmountWolfs; }
            set
            {
                if (mCurrentAmountWolfs <= mMaxWolfAmount)
                    mCurrentAmountWolfs = value;
            }
        }

        public int CurrentAmountBerserkers
        {
            get { return mCurrentAmountBerserkers; }
            set
            {
                if (mCurrentAmountBerserkers <= mMaxBerserkerAmount)
                    mCurrentAmountBerserkers = value;
            }
        }

        public int CurrentAmountAxeThrowers
        {
            get { return mCurrentAmountAxeThrowers; }
            set
            {
                if (mCurrentAmountAxeThrowers <= mMaxAxeThrowerAmount)
                    mCurrentAmountAxeThrowers = value;
            }
        }

        public int CurrentAmountArcaneMages
        {
            get { return mCurrentAmountArcaneMages; }
            set
            {
                if (mCurrentAmountArcaneMages <= mMaxArcaneMageAmount)
                    mCurrentAmountArcaneMages = value;
            }
        }

        public int CurrentAmountClerics
        {
            get { return mCurrentAmountClerics; }
            set
            {
                if (mCurrentAmountClerics <= mMaxClericAmount)
                    mCurrentAmountClerics = value;
            }
        }

        public int CurrentAmountFireMages
        {
            get { return mCurrentAmountFireMages; }
            set
            {
                if (mCurrentAmountFireMages <= mMaxFireMageAmount)
                    mCurrentAmountFireMages = value;
            }
        }

        public int CurrentAmountDragons
        {
            get { return mCurrentAmountDragons; }
            set
            {
                if (mCurrentAmountDragons <= mMaxDragonAmount)
                    mCurrentAmountDragons = value;
            }
        }

        public int CurrentAmountNecromancers
        {
            get { return mCurrentAmountNecromancers; }
            set
            {
                if (mCurrentAmountNecromancers <= mMaxNecromancerAmount)
                    mCurrentAmountNecromancers = value;
            }
        }

        public Structure Structure
        {
            get { return mStructure; }
            set { mStructure = value; }
        }

        public Vector2 DefaultUnitTarget
        {
            get { return mDefaultUnitTarget; }
            set { mDefaultUnitTarget = value; }
        }

        public int Cost
        {
            get { return mCost; }
            set { mCost = value; }
        }

        public int Tier
        {
            get { return mTier; }
            set { mTier = value; }
        }

        public float SpawnTimer
        {
            get { return mSpawnTimer; }
            set { mSpawnTimer = value; }
        }

        public int UpgradeSpeedLevel
        {
            get { return mUpgradeSpeedLevel; }
            set { mUpgradeSpeedLevel = value; }
        }

        #endregion

        // mUpgradeSpeedLevel++
        // Apply level : spawnTimer ratio to upgrade speed level.
        public void UpgradeSpeed()
        {
            mUpgradeSpeedLevel++;
            mSpawnTimer -= mUpgradeSpeedLevel;
        }

        public void SetStructureDefaultTarget()
        {


            switch (mStructure.StructureColor)
            {
                case ObjectColor.BLUE:
                    mDefaultUnitTarget = new Vector2(800, mStructure.ButtonPosition.Y);
                    break;
                case ObjectColor.RED:
                    mDefaultUnitTarget = new Vector2(800, mStructure.ButtonPosition.Y);
                    break;
                case ObjectColor.YELLOW:
                    mDefaultUnitTarget = new Vector2(800, mStructure.ButtonPosition.Y);
                    break;
                case ObjectColor.GREEN:
                    mDefaultUnitTarget = new Vector2(800, mStructure.ButtonPosition.Y);
                    break;
                case ObjectColor.PURPLE:
                    mDefaultUnitTarget = new Vector2(800, mStructure.ButtonPosition.Y);
                    break;
                case ObjectColor.NULL:
                    break;

                default:
                    Console.WriteLine("Crashed in Structure Attribute");
                    break;
            }

        }

    }
}
