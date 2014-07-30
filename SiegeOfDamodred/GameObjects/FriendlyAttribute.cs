using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExternalTypes;
using Microsoft.Xna.Framework.Content;

namespace GameObjects
{
    public class FriendlyAttribute : Attribute
    {
        private static BattleCrySpellState mBattleCrySpellState = BattleCrySpellState.INACTIVE;
        private Friendly mFriendlyActor;
        private float mUnitSpeed;
        private float mDefaultSpeed;
        private float mAttackUpgradeLevel;
        private float mDefenseUpgradeLevel;
        private float mAttackSpeed;
        private Random randomNumberGenerator;

        #region Audio Volume Fields

        private const float NecromancerProjectileVolume = 0.45f;
        private const float ArcaneMageProjectileVolume = 0.45f;
        private const float AxeThrowerProjectileVolume = 0.45f;
        private const float FireMageProjectileVolume = 0.45f;

        #endregion

        public FriendlyAttribute(Friendly mFriendlyActor, ContentManager content)
            : base(content)
        {
            randomNumberGenerator = new Random();
            this.mFriendlyActor = mFriendlyActor;
        }

        #region Properties

        public static BattleCrySpellState BattleCrySpellState
        {
            get { return mBattleCrySpellState; }
            set { mBattleCrySpellState = value; }
        }

        public float AttackSpeed
        {
            get { return mAttackSpeed; }
            set { mAttackSpeed = value; }
        }

        public float UnitSpeed
        {
            get { return mUnitSpeed; }
            set { mUnitSpeed = value; }
        }

        public float DefaultSpeed
        {
            get { return mDefaultSpeed; }
            set { mDefaultSpeed = value; }
        }

        public float DefenseUpgradeLevel
        {
            get { return mDefenseUpgradeLevel; }
            set { mDefenseUpgradeLevel = value; }
        }

        public float AttackUpgradeLevel
        {
            get { return mAttackUpgradeLevel; }
            set { mAttackUpgradeLevel = value; }
        }

        #endregion


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

        public void Heal()
        {
            GameObject.mPotentialTargetListList.Reset();

            for (int i = 0; i < GameObject.mPotentialTargetListList.GetCount(); i++)
            {
                if (GameObject.mPotentialTargetListList.GetCurrent().gameObject is Friendly)
                {
                    float tempHealing = CalculateDamage();

                    Friendly tempFriendly = (Friendly)GameObject.mPotentialTargetListList.GetCurrent().gameObject;
                    tempFriendly.FriendlyAttribute.CurrentHealthPoints += tempHealing;
                    Notification.SpawnHealingNumber(Math.Round(tempHealing).ToString(),tempFriendly.Sprite.WorldPosition);
                }

                if (GameObject.mPotentialTargetListList.GetCurrent().gameObject is Hero)
                {
                    float tempHealing = CalculateDamage();
                    Hero tempHero = (Hero)GameObject.mPotentialTargetListList.GetCurrent().gameObject;
                    tempHero.HeroAttribute.CurrentHealthPoints += CalculateDamage();


                    Notification.SpawnHealingNumber(Math.Round(tempHealing).ToString(), tempHero.Sprite.WorldPosition);
                }

                GameObject.mPotentialTargetListList.NextNode();
            }
        }

        public void Attack()
        {

            if (mFriendlyActor.CombatType == CombatType.MELEE)
            {
                MeleeAttack();

            }

            else if (mFriendlyActor.CombatType == CombatType.RANGED)
            {
                RangedAttack();
            }

        }

        public void RangedAttack()
        {

            if (mFriendlyActor.SpriteState == SpriteState.ATTACK_RIGHT)
            {
                LaunchProjectileRight();
            }
            else if (mFriendlyActor.SpriteState == SpriteState.ATTACK_LEFT)
            {
                LaunchProjectileLeft();
            }

        }

        public float CalculateDamage()
        {

            if (mBattleCrySpellState == BattleCrySpellState.INACTIVE)
            {
                // calculates the most common output                peak of function = B
                float peak = (mCurrentMaximumDamage + mCurrentMinimumDamage) / 2;

                // must be currentMin < randomModifer < peak          random modifer  = X
                float randomModifer = mRandomGenerator.Next((int)((CurrentMinimumDamage)), (int)(peak - 1));

                // calculates the range of which the damage can be          width of the function = C
                float width = mCurrentMaximumDamage - mCurrentMinimumDamage;

                // calculates and normalize the damage              F(x)  =  e^- ((X-B)^2) / (2C^2)
                float damage = mCurrentMaximumDamage *
                               (float)Math.Exp(-((randomModifer - peak) * (randomModifer - peak)) / (2 * width * width));

           //     float randomNumber = mRandomGenerator.Next(0, 5);
               // damage += randomNumber;
                //Damage - (Damage * (Armor / 100))
                return damage - (damage * (mCurrentArmor / 100));
            }
            else
            {
                return mCurrentMaximumDamage;
            }
        }

        public void LaunchProjectileRight()
        {
            Enemy tempEnemy = (Enemy)mFriendlyActor.TargetQueue.PeekFirst();

            float tempDamage = CalculateDamage();
            PlayProjectileAudio(mFriendlyActor);
            Projectile tempProjectile;
            switch (tempEnemy.CreatureType)
            {
                case ObjectType.NECROMANCER:
                    tempProjectile = new Projectile(ObjectType.NECROMANCER_PROJECTILE, SpriteState.ATTACK_RIGHT, mFriendlyActor.Sprite.WorldPosition, Hostility.PROJECTILE,
                     mFriendlyActor.TargetQueue.PeekFirst(), mFriendlyActor, tempDamage, this.mContent);
                    break;
                case ObjectType.ARCANE_MAGE:
                    tempProjectile = new Projectile(ObjectType.ARCANE_MAGE_PROJECTILE, SpriteState.ATTACK_RIGHT, mFriendlyActor.Sprite.WorldPosition, Hostility.PROJECTILE,
                     mFriendlyActor.TargetQueue.PeekFirst(), mFriendlyActor, tempDamage, this.mContent);
                    break;
                case ObjectType.FIRE_MAGE:
                    tempProjectile = new Projectile(ObjectType.FIRE_MAGE_PROJECTILE, SpriteState.ATTACK_RIGHT, mFriendlyActor.Sprite.WorldPosition, Hostility.PROJECTILE,
                     mFriendlyActor.TargetQueue.PeekFirst(), mFriendlyActor, tempDamage, this.mContent);
                    break;

                case ObjectType.AXE_THROWER:
                    tempProjectile = new Projectile(ObjectType.AXE_THROWER_PROJECTILE, SpriteState.ATTACK_RIGHT, mFriendlyActor.Sprite.WorldPosition, Hostility.PROJECTILE,
                     mFriendlyActor.TargetQueue.PeekFirst(), mFriendlyActor, tempDamage, this.mContent);
                    break;

                default:
                    break;
            }
        }

        public void LaunchProjectileLeft()
        {
            Enemy tempEnemy = (Enemy)mFriendlyActor.TargetQueue.PeekFirst();

            float tempDamage = CalculateDamage();
            PlayProjectileAudio(mFriendlyActor);
            Projectile tempProjectile;
            switch (mFriendlyActor.CreatureType)
            {
                case ObjectType.NECROMANCER:
                    tempProjectile = new Projectile(ObjectType.NECROMANCER_PROJECTILE, SpriteState.ATTACK_LEFT, mFriendlyActor.Sprite.WorldPosition, Hostility.PROJECTILE,
                     mFriendlyActor.TargetQueue.PeekFirst(), mFriendlyActor, tempDamage, this.mContent);
                    break;
                case ObjectType.ARCANE_MAGE:
                    tempProjectile = new Projectile(ObjectType.ARCANE_MAGE_PROJECTILE, SpriteState.ATTACK_LEFT, mFriendlyActor.Sprite.WorldPosition, Hostility.PROJECTILE,
                     mFriendlyActor.TargetQueue.PeekFirst(), mFriendlyActor, tempDamage, this.mContent);
                    break;
                case ObjectType.FIRE_MAGE:
                    tempProjectile = new Projectile(ObjectType.FIRE_MAGE_PROJECTILE, SpriteState.ATTACK_LEFT, mFriendlyActor.Sprite.WorldPosition, Hostility.PROJECTILE,
                     mFriendlyActor.TargetQueue.PeekFirst(), mFriendlyActor, tempDamage, this.mContent);
                    break;

                case ObjectType.AXE_THROWER:
                    tempProjectile = new Projectile(ObjectType.AXE_THROWER_PROJECTILE, SpriteState.ATTACK_LEFT, mFriendlyActor.Sprite.WorldPosition, Hostility.PROJECTILE,
                     mFriendlyActor.TargetQueue.PeekFirst(), mFriendlyActor, tempDamage, this.mContent);
                    break;

                default:
                    break;
            }
        }


        public void MeleeAttack()
        {
            Enemy tempEnemy = (Enemy)mFriendlyActor.TargetQueue.PeekFirst();

            float tempDamage = CalculateDamage();
            int a = (int) (Math.Round(tempDamage));
            Notification.SpawnDamageNumber(((int)Math.Round(tempDamage)).ToString(), tempEnemy.Sprite.WorldPosition);
            // Current target's health -= tempDamage. 
            tempEnemy.EnemyAttribute.CurrentHealthPoints -= tempDamage;

        }



        #region Projectile Sounds

        private void PlayProjectileAudio(Friendly friendlyActor)
        {
            switch (friendlyActor.CreatureType)
            {


                case ObjectType.NECROMANCER:

                    PlayNecromancerProjectileSound();
                    break;
                case ObjectType.ARCANE_MAGE:
                    PlayArcaneMageProjectileSound();
                    break;
                case ObjectType.FIRE_MAGE:
                    PlayFireMageProjectileSound();
                    break;
                case ObjectType.AXE_THROWER:
                    PlayAxeThrowerProjectileSound();
                    break;


                default:
                    break;
            }
        }

        #region Necromancer Projectile Sounds

        private void PlayNecromancerProjectileSound()
        {
            int temp = randomNumberGenerator.Next(0, 2);

            switch (temp)
            {
                case 0:
                    AudioController.NecromancerLifeDrain1SoundEffectInstance.Volume = NecromancerProjectileVolume;
                    AudioController.NecromancerLifeDrain1SoundEffectInstance.Play();
                    break;

                case 1:
                    AudioController.NecromancerLifeDrain2SoundEffectInstance.Volume = NecromancerProjectileVolume;
                    AudioController.NecromancerLifeDrain2SoundEffectInstance.Play();
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region Arcane Mage Projectile Sounds

        private void PlayArcaneMageProjectileSound()
        {
            int temp = randomNumberGenerator.Next(0, 1);

            switch (temp)
            {
                case 0:
                    AudioController.ArcaneMageBlastSoundEffectInstance.Volume = NecromancerProjectileVolume;
                    AudioController.ArcaneMageBlastSoundEffectInstance.Play();
                    break;

                default:
                    break;
            }
        }
        #endregion

        #region Axe Thrower Projectile Sounds

        private void PlayAxeThrowerProjectileSound()
        {
            int temp = randomNumberGenerator.Next(0, 2);

            switch (temp)
            {
                case 0:

                    AudioController.AxeWhooshLoopSoundEffectInstance.Volume = AxeThrowerProjectileVolume;
                    AudioController.AxeWhooshLoopSoundEffectInstance.Play();
                    break;

                case 1:
                    AudioController.AxeThrowSoundEffectInstance.Volume = AxeThrowerProjectileVolume;
                    AudioController.AxeThrowSoundEffectInstance.Play();
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region Fire Mage Projectile Sounds

        private void PlayFireMageProjectileSound()
        {
            int temp = randomNumberGenerator.Next(0, 1);

            switch (temp)
            {
                case 0:
                    AudioController.FireMageFireBlastSoundEffectInstance.Volume = FireMageProjectileVolume;
                    AudioController.FireMageFireBlastSoundEffectInstance.Play();
                    break;
                default:
                    break;
            }
        }
        #endregion #endregion

        #endregion
    }
}
