using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExternalTypes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace GameObjects
{
    public class EnemyAttribute : Attribute
    {
        private static IntimidateSpellState mIntimidateSpellState = ExternalTypes.IntimidateSpellState.INACTIVE;
        private Enemy mEnemyActor;        
        private float mAttackSpeed;
        private int mAttackUpgradeLevel;
        private int mDefenseUpgradeLevel;
        private int mTier;
        private float mUnitSpeed;
        private Castle mCastle;
        private Random randomNumberGenerator;
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


        #region Audio Volume

        private const float mBansheeProjectileSoundVolume = 0.4f;
        private const float mGogProjectileSoundVolume = 0.4f;

        #endregion

        public EnemyAttribute(Enemy enemyActor, ContentManager content, Castle castle)
            : base(content)
        {
            randomNumberGenerator = new Random();
            this.mEnemyActor = enemyActor;
            this.mCastle = castle;

        }

        #region Properties

        public float UnitSpeed
        {
            get { return mUnitSpeed; }
            set { mUnitSpeed = value; }
        }

        public static IntimidateSpellState IntimidateSpellState
        {
            get { return mIntimidateSpellState; }
            set { mIntimidateSpellState = value; }
        }

        public double BaseExperience
        {
            get { return mBaseExperience; }
        }

        public float AttackSpeed // lower is faster
        {
            get { return mAttackSpeed; }
            set { mAttackSpeed = value; }
        }

        public double InitialExperience
        {
            get { return mInitialExperience; }
            set { mInitialExperience = value; }
        }

        public double BaseGold
        {
            get { return mBaseGold; }
        }

        public int AttackUpgradeLevel
        {
            get { return mAttackUpgradeLevel; }
        }

        public int DefenseUpgradeLevel
        {
            get { return mDefenseUpgradeLevel; }
        }

        public Enemy EnemyActor
        {
            get { return mEnemyActor; }
            set { mEnemyActor = value; }
        }


        public int Tier
        {
            get { return mTier; }
            set { mTier = value; }
        }

        #endregion



        public void UpgradeAttack()
        {
            mAttackUpgradeLevel++;
            // Current = baseModifier * (level * .05) + baseDamage
            mEnemyActor.EnemyAttribute.mCurrentMaximumDamage = mEnemyActor.EnemyAttribute.BaseDamageModifier * (mAttackUpgradeLevel * .05f) + mEnemyActor.EnemyAttribute.BaseMaximumDamage;
            mEnemyActor.EnemyAttribute.mCurrentMinimumDamage = mEnemyActor.EnemyAttribute.BaseDamageModifier * (mAttackUpgradeLevel * .05f) + mEnemyActor.EnemyAttribute.BaseMinimumDamage;
        }


        public void UpgradeDefense()
        {
            mDefenseUpgradeLevel++;
            // Current = baseModifier + currentArmor;
            CurrentArmor += BaseArmorModifer;
        }



        // roll a random number between my min and max damage range
        // Check my target's hp
        // Check my target's armor
        // Apply my damage to my target's hp based on armor

        public void Attack()
        {

            if (mEnemyActor.CombatType == CombatType.MELEE)
            {
                MeleeAttack();
            }
            else if (mEnemyActor.CombatType == CombatType.RANGED)
            {
                RangedAttack();
            }


        }



        public void RangedAttack()
        {
            if (mEnemyActor.TargetQueue.PeekFirst() is Structure)
            {
                if (mEnemyActor.SpriteState == SpriteState.ATTACK_LEFT)
                {
                    LaunchProjectileLeftAtStructure();
                }
                else if (mEnemyActor.SpriteState == SpriteState.ATTACK_RIGHT)
                {
                    LaunchProjectileRightAtStructure();
                }
            }

            else if (mEnemyActor.TargetQueue.PeekFirst() is Friendly)
            {

                if (mEnemyActor.SpriteState == SpriteState.ATTACK_LEFT)
                {
                    LaunchProjectileLeftAtFriendly();
                }
                else if (mEnemyActor.SpriteState == SpriteState.ATTACK_RIGHT)
                {
                    LaunchProjectileRightAtFriendly();
                }
            }
            else if (mEnemyActor.TargetQueue.PeekFirst() is Hero)
            {

                if (mEnemyActor.SpriteState == SpriteState.ATTACK_LEFT)
                {
                    LaunchProjectileLeftAtHero();
                }
                else if (mEnemyActor.SpriteState == SpriteState.ATTACK_RIGHT)
                {
                    LaunchProjectileRightAtHero();
                }
            }

            else if(mEnemyActor.TargetQueue.IsEmpty())
            {

                if (mEnemyActor.SpriteState == SpriteState.ATTACK_LEFT)
                {
                    LaunchProjectileLeftAtCastle();
                }
                else if (mEnemyActor.SpriteState == SpriteState.ATTACK_RIGHT)
                {
                    LaunchProjectileRightAtCastle();
                }
            }
        }

        public float CalculateDamage()
        {

            if (mIntimidateSpellState == IntimidateSpellState.INACTIVE)
            {
                // calculates the most common output                peak of function = B
                float peak = (mCurrentMaximumDamage + mCurrentMinimumDamage) / 2;

                // must be  currentMin < randomModifer < peak          random modifer  = X
                float randomModifer = mRandomGenerator.Next((int)((mCurrentMinimumDamage + 1)), (int)(peak - 1));

                // calculates the range of which the damage can be          width of the function = C
                float width = mCurrentMaximumDamage - mCurrentMinimumDamage;

                // calculates and normalize the damage              F(x)  =  e^- ((X-B)^2) / (2C^2)
                float damage = mCurrentMaximumDamage *
                               (float)Math.Exp(-(((randomModifer - peak) * (randomModifer - peak)) / (2 * width * width)));

                // Health -= Damage - (Damage * (Armor / 100))
                return damage - (damage * (mCurrentArmor / 100));
            }
            else
            {
                return mCurrentMinimumDamage;
            }
        }


        public void LaunchProjectileRightAtCastle()
        {
            Structure tempStructure = (Structure)mEnemyActor.TargetQueue.PeekFirst();


            float tempDamage = CalculateDamage();
            PlayProjectileAudio(mEnemyActor);
            Projectile tempProjectile;
            switch (mEnemyActor.CreatureType)
            {

                case ObjectType.BANSHEE:
                    tempProjectile = new Projectile(ObjectType.BANSHEE_PROJECTILE, SpriteState.ATTACK_RIGHT, mEnemyActor.Sprite.WorldPosition, Hostility.PROJECTILE,
                   mCastle, mEnemyActor, tempDamage, this.mContent);
                    break;

                case ObjectType.GOG:
                    tempProjectile = new Projectile(ObjectType.GOG_PROJECTILE, SpriteState.ATTACK_RIGHT, mEnemyActor.Sprite.WorldPosition, Hostility.PROJECTILE,
                  mCastle, mEnemyActor, tempDamage, this.mContent);
                    break;


                default:
                    break;
            }
        }

        public void LaunchProjectileLeftAtCastle()
        {
            Structure tempStructure = (Structure)mEnemyActor.TargetQueue.PeekFirst();


            float tempDamage = CalculateDamage();
            PlayProjectileAudio(mEnemyActor);
            Projectile tempProjectile;
            switch (mEnemyActor.CreatureType)
            {

                case ObjectType.BANSHEE:
                    tempProjectile = new Projectile(ObjectType.BANSHEE_PROJECTILE, SpriteState.ATTACK_RIGHT, mEnemyActor.Sprite.WorldPosition, Hostility.PROJECTILE,
                   mCastle, mEnemyActor, tempDamage, this.mContent);
                    break;

                case ObjectType.GOG:
                    tempProjectile = new Projectile(ObjectType.GOG_PROJECTILE, SpriteState.ATTACK_RIGHT, mEnemyActor.Sprite.WorldPosition, Hostility.PROJECTILE,
                  mCastle, mEnemyActor, tempDamage, this.mContent);
                    break;


                default:
                    break;
            }
        }




        public void LaunchProjectileRightAtStructure()
        {
            Structure tempStructure = (Structure)mEnemyActor.TargetQueue.PeekFirst();


            float tempDamage = CalculateDamage();
            PlayProjectileAudio(mEnemyActor);
            Projectile tempProjectile;
            switch (mEnemyActor.CreatureType)
            {

                case ObjectType.BANSHEE:
                    tempProjectile = new Projectile(ObjectType.BANSHEE_PROJECTILE, SpriteState.ATTACK_RIGHT, mEnemyActor.Sprite.WorldPosition, Hostility.PROJECTILE,
                   mEnemyActor.TargetQueue.PeekFirst(), mEnemyActor, tempDamage, this.mContent);
                    break;

                case ObjectType.GOG:
                    tempProjectile = new Projectile(ObjectType.GOG_PROJECTILE, SpriteState.ATTACK_RIGHT, mEnemyActor.Sprite.WorldPosition, Hostility.PROJECTILE,
                  mEnemyActor.TargetQueue.PeekFirst(), mEnemyActor, tempDamage, this.mContent);
                    break;


                default:
                    break;
            }
        }

        public void LaunchProjectileLeftAtStructure()
        {
            Structure tempStructure = (Structure)mEnemyActor.TargetQueue.PeekFirst();

            float tempDamage = CalculateDamage();

            PlayProjectileAudio(mEnemyActor);
            Projectile tempProjectile;
            switch (mEnemyActor.CreatureType)
            {

                case ObjectType.BANSHEE:
                    tempProjectile = new Projectile(ObjectType.BANSHEE_PROJECTILE, SpriteState.ATTACK_LEFT, mEnemyActor.Sprite.WorldPosition, Hostility.PROJECTILE,
                   mEnemyActor.TargetQueue.PeekFirst(), mEnemyActor, tempDamage, this.mContent);
                    break;

                case ObjectType.GOG:
                    tempProjectile = new Projectile(ObjectType.GOG_PROJECTILE, SpriteState.ATTACK_LEFT, mEnemyActor.Sprite.WorldPosition, Hostility.PROJECTILE,
                  mEnemyActor.TargetQueue.PeekFirst(), mEnemyActor, tempDamage, this.mContent);
                    break;


                default:
                    break;
            }
        }
        public void LaunchProjectileRightAtHero()
        {
            Hero tempHero = (Hero)mEnemyActor.TargetQueue.PeekFirst();

            float tempDamage = CalculateDamage();

            PlayProjectileAudio(mEnemyActor);
            Projectile tempProjectile;
            switch (mEnemyActor.CreatureType)
            {

                case ObjectType.BANSHEE:
                    tempProjectile = new Projectile(ObjectType.BANSHEE_PROJECTILE, SpriteState.ATTACK_RIGHT, mEnemyActor.Sprite.WorldPosition, Hostility.PROJECTILE,
                   mEnemyActor.TargetQueue.PeekFirst(), mEnemyActor, tempDamage, this.mContent);
                    break;

                case ObjectType.GOG:
                    tempProjectile = new Projectile(ObjectType.GOG_PROJECTILE, SpriteState.ATTACK_RIGHT, mEnemyActor.Sprite.WorldPosition, Hostility.PROJECTILE,
                  mEnemyActor.TargetQueue.PeekFirst(), mEnemyActor, tempDamage, this.mContent);
                    break;


                default:
                    break;
            }
        }

        public void LaunchProjectileLeftAtHero()
        {
            Hero tempHero = (Hero)mEnemyActor.TargetQueue.PeekFirst();

            float tempDamage = CalculateDamage();

            PlayProjectileAudio(mEnemyActor);
            Projectile tempProjectile;
            switch (mEnemyActor.CreatureType)
            {

                case ObjectType.BANSHEE:
                    tempProjectile = new Projectile(ObjectType.BANSHEE_PROJECTILE, SpriteState.ATTACK_LEFT, mEnemyActor.Sprite.WorldPosition, Hostility.PROJECTILE,
                   mEnemyActor.TargetQueue.PeekFirst(), mEnemyActor, tempDamage, this.mContent);
                    break;

                case ObjectType.GOG:
                    tempProjectile = new Projectile(ObjectType.GOG_PROJECTILE, SpriteState.ATTACK_LEFT, mEnemyActor.Sprite.WorldPosition, Hostility.PROJECTILE,
                  mEnemyActor.TargetQueue.PeekFirst(), mEnemyActor, tempDamage, this.mContent);
                    break;


                default:
                    break;
            }
        }
        public void LaunchProjectileRightAtFriendly()
        {
            Friendly tempFriendly = (Friendly)mEnemyActor.TargetQueue.PeekFirst();

            float tempDamage = CalculateDamage();

            PlayProjectileAudio(mEnemyActor);
            Projectile tempProjectile;
            switch (mEnemyActor.CreatureType)
            {

                case ObjectType.BANSHEE:
                    tempProjectile = new Projectile(ObjectType.BANSHEE_PROJECTILE, SpriteState.ATTACK_RIGHT, mEnemyActor.Sprite.WorldPosition, Hostility.PROJECTILE,
                   mEnemyActor.TargetQueue.PeekFirst(), mEnemyActor, tempDamage, this.mContent);
                    break;

                case ObjectType.GOG:
                    tempProjectile = new Projectile(ObjectType.GOG_PROJECTILE, SpriteState.ATTACK_RIGHT, mEnemyActor.Sprite.WorldPosition, Hostility.PROJECTILE,
                  mEnemyActor.TargetQueue.PeekFirst(), mEnemyActor, tempDamage, this.mContent);
                    break;


                default:
                    break;
            }
        }

        public void LaunchProjectileLeftAtFriendly()
        {
            Friendly tempFriendly = (Friendly)mEnemyActor.TargetQueue.PeekFirst();

            float tempDamage = CalculateDamage();

            PlayProjectileAudio(mEnemyActor);
            Projectile tempProjectile;
            switch (mEnemyActor.CreatureType)
            {

                case ObjectType.BANSHEE:
                    tempProjectile = new Projectile(ObjectType.BANSHEE_PROJECTILE, SpriteState.ATTACK_LEFT, mEnemyActor.Sprite.WorldPosition, Hostility.PROJECTILE,
                   mEnemyActor.TargetQueue.PeekFirst(), mEnemyActor, tempDamage, this.mContent);
                    break;

                case ObjectType.GOG:
                    tempProjectile = new Projectile(ObjectType.GOG_PROJECTILE, SpriteState.ATTACK_LEFT, mEnemyActor.Sprite.WorldPosition, Hostility.PROJECTILE,
                  mEnemyActor.TargetQueue.PeekFirst(), mEnemyActor, tempDamage, this.mContent);
                    break;


                default:
                    break;
            }
        }


        public void MeleeAttack()
        {
            if (!EnemyActor.TargetQueue.IsEmpty())
            {
                if (mEnemyActor.TargetQueue.PeekFirst() is Structure)
                {
                    Structure tempStructure = (Structure)mEnemyActor.TargetQueue.PeekFirst();

                    float tempDamage = CalculateDamage();

                    // Current target's health -= tempDamage. 
                    tempStructure.StructureAttribute.CurrentHealthPoints -= tempDamage;
                }
                else if (mEnemyActor.TargetQueue.PeekFirst() is Friendly)
                {
                    Friendly tempFriendly = (Friendly)mEnemyActor.TargetQueue.PeekFirst();

                    float tempDamage = CalculateDamage();

                    // Current target's health -= tempDamage. 
                    tempFriendly.FriendlyAttribute.CurrentHealthPoints -= tempDamage;
                }
                else if (mEnemyActor.TargetQueue.PeekFirst() is Hero)
                {
                    Hero tempHero = (Hero)mEnemyActor.TargetQueue.PeekFirst();

                    float tempDamage = CalculateDamage();

                    // Current target's health -= tempDamage. 
                    tempHero.HeroAttribute.CurrentHealthPoints -= tempDamage;
                }
            }
            else
            {
                float tempDamage = CalculateDamage();

                mCastle.CastleAttribute.CurrentHealthPoints -= tempDamage;
            }
        }

        #region Projectile Sounds

        private void PlayProjectileAudio(Enemy enemyActor)
        {
            switch (enemyActor.CreatureType)
            {


                case ObjectType.BANSHEE:

                    PlayBansheeProjectileSound();
                    break;
                case ObjectType.GOG:
                    PlayGogProjectileSound();
                    break;

                default:
                    break;
            }
        }

        #region Banshee Projectile Sounds

        private void PlayBansheeProjectileSound()
        {
            int temp = randomNumberGenerator.Next(0, 2);

            switch (temp)
            {
                case 0:
                    AudioController.BansheeSkullBlastSoundEffectInstance.Volume = mBansheeProjectileSoundVolume;
                    AudioController.BansheeSkullBlastSoundEffectInstance.Play();
                    break;

                default:
                    break;
            }
        }

        #endregion



        #region Gog Projectile Sounds

        private void PlayGogProjectileSound()
        {
            int temp = randomNumberGenerator.Next(0, 1);

            switch (temp)
            {
                case 0:
                    AudioController.GogFireBlastSoundEffectInstance.Volume = mGogProjectileSoundVolume;
                    AudioController.GogFireBlastSoundEffectInstance.Play();
                    break;
                default:
                    break;
            }
        }
        #endregion


        #endregion

    }
}
