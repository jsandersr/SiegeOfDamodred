using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExternalTypes;
using Microsoft.Xna.Framework.Content;

namespace GameObjects
{

    public class HeroAttribute : Attribute
    {

        #region Spells
        private Spell Curse;
        private Spell Bless;
        #endregion

        private Hero hero;

        public static float AttackSpeed = 400.0f;
        private static int mExperience;
        private static int mGold;
        private static int mLevel;
        private static int mMana;
        private static int mSpeed;
        private static int mDefaultSpeed;
        private static int mSpellLevel;
        private static int mMaxMana;
        private static int mLevelUpExperience;
        private const int mBaseLevelUpExperience = 400;


        private static float mAttackUpgradeLevel;
        private static float mDefenseUpgradeLevel;

        private static int mOwnedDragonCaves;
        private static int mOwnedWolfPens;
        private static int mOwnedBarracks;
        private static int mOwnedArmories;
        private static int mOwnedLibraries;
        private static int mOwnedBonePit;
        private static int mOwnedAbbey;
        private static int mOwnedFireTemple;
        private static BattleCrySpellState mBattleCrySpellState = BattleCrySpellState.INACTIVE;
        private static IntimidateSpellState mIntimidateSpellState = ExternalTypes.IntimidateSpellState.INACTIVE;

        public HeroAttribute(Hero hero, ContentManager content)
            : base(content)
        {
            this.hero = hero;
            
        }

        #region Properties

        public static IntimidateSpellState IntimidateSpellState
        {
            get { return mIntimidateSpellState; }
            set { mIntimidateSpellState = value; }
        }

        public int MaxMana
        {
            get { return mMaxMana; }
            set
            {
                mMaxMana = value;
                mMana = value;
            }
        }

        public static BattleCrySpellState BattleCrySpellState
        {
            get { return mBattleCrySpellState; }
            set { mBattleCrySpellState = value; }
        }


        public int OwnedAbbey
        {
            get { return mOwnedAbbey; }
            set
            {
                if (mOwnedAbbey <= 5)
                    mOwnedAbbey = value;
            }
        }

        public int OwnedArmories
        {
            get { return mOwnedArmories; }
            set
            {
                if (mOwnedArmories <= 5)
                    mOwnedArmories = value;
            }
        }

        public int OwnedBarracks
        {
            get { return mOwnedBarracks; }
            set
            {
                if (mOwnedBarracks <= 5)
                    mOwnedBarracks = value;
            }
        }

        public int OwnedBonePit
        {
            get { return mOwnedBonePit; }
            set
            {
                if (mOwnedBonePit <= 5)
                    mOwnedBonePit = value;
            }
        }

        public int OwnedDragonCaves
        {
            get { return mOwnedDragonCaves; }
            set
            {
                if (mOwnedDragonCaves <= 5)
                    mOwnedDragonCaves = value;
            }
        }

        public int OwnedFireTemple
        {
            get { return mOwnedFireTemple; }
            set
            {
                if (mOwnedFireTemple <= 5)
                    mOwnedFireTemple = value;
            }
        }

        public int OwnedWolfPens
        {
            get { return mOwnedWolfPens; }
            set
            {
                if (mOwnedWolfPens <= 5)
                    mOwnedWolfPens = value;
            }
        }

        public int OwnedLibraries
        {
            get { return mOwnedLibraries; }
            set
            {
                if (mOwnedLibraries <= 5)
                    mOwnedLibraries = value;
            }
        }

        public int Experience
        {
            get { return mExperience; }
            set
            {
                mExperience = value;
                if (mExperience >= CalculateAmountOfExperienceToLevel())
                {
                    LevelUp();
                }
            }

        }

        public int Gold
        {
            get { return mGold; }
            set { mGold = value; }
        }

        public int Level
        {
            get { return mLevel; }
            set { mLevel = value; }
        }

        public int Mana
        {
            get { return mMana; }
            set
            {
                if (value <= mMaxMana && value >= 0)
                    mMana = value;
            }

        }

        public int Speed
        {
            get { return mSpeed; }
            set { mSpeed = value; }
        }

        public int SpellLevel
        {
            get { return mSpellLevel; }
            set { mSpellLevel = value; }
        }

        public float AttackUpgradeLevel
        {
            get { return mAttackUpgradeLevel; }
            set { mAttackUpgradeLevel = value; }
        }

        public float DefenseUpgradeLevel
        {
            get { return mDefenseUpgradeLevel; }
            set { mDefenseUpgradeLevel = value; }
        }

        public int DefaultSpeed
        {
            get { return mDefaultSpeed; }
            set
            {
                mDefaultSpeed = value;
                mSpeed = value;
            }
        }

        #endregion

        public float CalculateAmountOfExperienceToLevel()
        {
            if (mLevel == 1)
                return mBaseLevelUpExperience * (mLevel * .04f) + mBaseLevelUpExperience;
            else
                return mBaseLevelUpExperience * (mLevel * .04f) + mBaseLevelUpExperience * 2 * mLevel;
        }

        public void LevelUp()
        {
            mLevel++;
            int randomModifer = mRandomGenerator.Next(1, 6);
            MaxHealthPoints += MaxHealthPoints * (mLevel * (randomModifer / 100.0f));
            randomModifer = mRandomGenerator.Next(1, 6);
            MaxMana += (40 * (randomModifer / 100));
            UpgradeAttack();
            UpgradeDefense();
            mSpellLevel++;
            Notification.ShowLevelGainNotification();
        }

        // switch on structure type
        // mStructureType++
        public void BuyStructure(ObjectType structureType)
        {

        }

        public float CalculateSpellTimer()
        {
            return 200 + (mSpellLevel * 10);
        }

        public void UpgradeAttack()
        {
            mAttackUpgradeLevel++;
            // Current = baseModifier * (level * .05) + baseDamage
            mCurrentMaximumDamage = BaseDamageModifier * (mAttackUpgradeLevel * .05f) + BaseMaximumDamage;
            mCurrentMinimumDamage = BaseDamageModifier * (mAttackUpgradeLevel * .05f) + BaseMinimumDamage;
        }

        public void UpgradeDefense()
        {
            mDefenseUpgradeLevel++;
            // Current = baseModifier + currentArmor;
            CurrentArmor += BaseArmorModifer;
        }



        public float CalculateDamage()
        {

            if (mBattleCrySpellState == BattleCrySpellState.INACTIVE)
            {
                // calculates the most common output                peak of function = B
                float peak = (mCurrentMaximumDamage + mCurrentMinimumDamage) / 2;

                // must be currentMin < randomModifer < peak          random modifer  = X
                float randomModifer = mRandomGenerator.Next((int)((mCurrentMinimumDamage)), (int)(peak - 1));

                // calculates the range of which the damage can be          width of the function = C
                float width = mCurrentMaximumDamage - mCurrentMinimumDamage;

                // calculates and normalize the damage              F(x)  =  e^- ((X-B)^2) / (2C^2)
                float damage = mCurrentMaximumDamage *
                               (float)Math.Exp(-((randomModifer - peak) * (randomModifer - peak)) / (2 * width * width));

                // Health -= Damage - (Damage * (Armor / 100))
                return damage - (damage * (mCurrentArmor / 100));
            }
            else
            {
                return mCurrentMaximumDamage;
            }
        }


        public void Attack(Enemy target)
        {

            float tempDamage = CalculateDamage();

            Console.WriteLine(tempDamage);

            // Current target's health -= tempDamage. 
            target.EnemyAttribute.CurrentHealthPoints -= tempDamage;
        }




    }
}
