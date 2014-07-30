using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ExternalTypes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SpriteGenerator;

namespace GameObjects
{
    public class Projectile : GameObject
    {



        private bool mHasLanded;
        private GameObject mProjectileTargetObject;
        private GameObject mProjectileCaster;
        private float mProjectileDamage;
        private Vector2 mTargetDirection;
        private Vector2 mCurrentTarget;
        private float mSpeed;
        private int mTrackingNumber;
        private Random randomNumberGenerator;

        private const float mDragonStruckSoundVolume = 0.4f;
        private const float mArcaneMageStruckSoundVolume = 0.4f;
        private const float mFireMageStruckSoundVolume = 0.4f;
        private const float mClericStruckSoundVolume = 0.4f;
        private const float mWolfStruckSoundVolume = 0.4f;
        private const float mAxeThrowerStruckSoundVolume = 0.4f;
        private const float mBerserkerStruckSoundVolume = 0.4f;
        private const float mNecromancerStruckSoundVolume = 0.4f;




        private const float mBansheeStruckSoundVolume = 0.4f;
        private const float mReaperStruckSoundVolume = 0.4f;
        private const float mGogStruckSoundVolume = 0.4f;
        private const float mImpStruckSoundVolume = 0.4f;
        private const float mDoomHoundStruckSoundVolume = 0.4f;

        private const float mMeleeHitSoundVolume = 0.4f;


        public Projectile(ObjectType mObjectType, SpriteState defaultState, Vector2 SpritePosition, Hostility hostility,
                          GameObject mProjectileTargetObject, GameObject mProjectileCaster, float mProjectileDamage,
                          ContentManager content)
            : base(mObjectType, content, defaultState, SpritePosition, hostility)
        {
            mObjectID = mGlobalID;
            mGlobalID++;
            mSpeed = 5.0f;


            randomNumberGenerator = new Random();
            this.mProjectileDamage = mProjectileDamage;
            this.mProjectileCaster = mProjectileCaster;
            this.mProjectileTargetObject = mProjectileTargetObject;
            this.mCurrentTarget = mProjectileTargetObject.Sprite.WorldPosition;
            this.mTrackingNumber = this.mProjectileTargetObject.mObjectID;
            mHasLanded = false;



        }

        public float Speed
        {
            get { return mSpeed; }
            set { mSpeed = value; }
        }

        public bool HasLanded
        {
            get { return mHasLanded; }
            set { mHasLanded = value; }
        }

        // Update is called from Game Controller as this object is passed in the projectile list
        public void Update(GameTime gameTime)
        {


            mTargetDirection = -this.Sprite.WorldPosition;

            mTargetDirection = this.mCurrentTarget - this.Sprite.WorldPosition;
            mTargetDirection.Normalize();

            this.Direction = this.Direction + mTargetDirection * mSpeed;

            if (this.mDirection.Length() > 0.0f)
            {
                this.mDirection.Normalize();
            }
            this.mSprite.WorldPosition = this.mSprite.WorldPosition + this.mDirection * mSpeed;


            if ((this.Sprite.WorldPosition.X >= mCurrentTarget.X - 30 &&
                       this.Sprite.WorldPosition.Y >= mCurrentTarget.Y - 30) &&
                      (this.Sprite.WorldPosition.X <= mCurrentTarget.X + 30 &&
                       this.Sprite.WorldPosition.Y <= mCurrentTarget.Y + 30))
            {
                mHasLanded = true;
                if (mProjectileTargetObject is Enemy)
                {
                    Enemy tempEnemyTarget = (Enemy)mProjectileTargetObject;
                    tempEnemyTarget.EnemyAttribute.CurrentHealthPoints -= mProjectileDamage;
                    PlayUnitStruckAudio(tempEnemyTarget);
                    if (mProjectileCaster.CreatureType == ObjectType.NECROMANCER)
                    {
                        int counter = 0;
                        JTargetQueue HealList = new JTargetQueue();
                        Friendly tempNecromancer = (Friendly)mProjectileCaster;
                        mPotentialTargetListList.Reset();
                        for (int i = 0; i < mPotentialTargetListList.GetCount(); i++)
                        {
                            if ((mPotentialTargetListList.GetCurrent().gameObject.Hostility == Hostility.FRIENDLY) &&
                                mPotentialTargetListList.GetCurrent().gameObject.Sprite.SpriteFrame.Contains(tempNecromancer.FieldOfView))
                            {
                                HealList.InsertTarget(mPotentialTargetListList.GetCurrent().gameObject);
                                counter++;
                            }

                            mPotentialTargetListList.NextNode();
                        }

                        while (!HealList.IsEmpty())
                        {
                            if (HealList.PeekFirst() is Friendly)
                            {
                                Friendly tempFriendly = (Friendly)HealList.PopTarget();
                                tempFriendly.FriendlyAttribute.CurrentHealthPoints += mProjectileDamage / counter;
                            }
                            if (HealList.PeekFirst() is Hero)
                            {
                                Hero tempHero = (Hero)HealList.PopTarget();
                                tempHero.HeroAttribute.CurrentHealthPoints += mProjectileDamage / counter;
                            }
                        }

                    }
                }
                else if (mProjectileTargetObject is Friendly)
                {
                    Friendly tempFriendlyTarget = (Friendly)mProjectileTargetObject;
                    PlayUnitStruckAudio(tempFriendlyTarget);
                    tempFriendlyTarget.FriendlyAttribute.CurrentHealthPoints -= mProjectileDamage;
                }
                else if (mProjectileTargetObject is Castle)
                {
                    Castle tempCastleTarget = (Castle)mProjectileTargetObject;
                    tempCastleTarget.CastleAttribute.CurrentHealthPoints -= mProjectileDamage;
                }
                else if (mProjectileTargetObject is Structure)
                {
                    Structure tempStructureTarget = (Structure)mProjectileTargetObject;
                    tempStructureTarget.StructureAttribute.CurrentHealthPoints -= mProjectileDamage;
                }
                else if (mProjectileTargetObject is Hero)
                {
                    Hero tempHeroTarget = (Hero)mProjectileTargetObject;
                    tempHeroTarget.HeroAttribute.CurrentHealthPoints -= mProjectileDamage;
                    PlayUnitStruckAudio(tempHeroTarget);
                }

            }




            CheckSpriteAnimation();
        }



        // Draw is called from Game Controller as this object is passed in the projectile list
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (this.Sprite.AssetName != null)
            {
                this.Sprite.Draw(spriteBatch, 1.0f);
            }
        }

        #region Unit Struck Sound Decision Functions

        public void PlayUnitStruckAudio(Enemy EnemyActor)
        {
            //TODO: place holder
            switch (EnemyActor.CreatureType)
            {

                case ObjectType.BANSHEE:
                    PlayBansheeStruckSound();
                    break;
                case ObjectType.REAPER:
                    PlayReaperStruckSound();
                    break;
                case ObjectType.DOOM_HOUND:
                    PlayDoomHoundStruckSound();
                    break;
                case ObjectType.IMP:
                    PlayImpStruckSound();
                    break;
                case ObjectType.GOG:
                    PlayGogStruckSound();
                    break;

                default:
                    break;
            }
        }

        #region Banshee Struck Sounds
        private void PlayBansheeStruckSound()
        {

            int temp = randomNumberGenerator.Next(0, 2);

            switch (temp)
            {

                case 0:
                    AudioController.BansheeStruck1SoundEffectInstance.Volume = mBansheeStruckSoundVolume;
                    AudioController.BansheeStruck1SoundEffectInstance.Play();
                    break;
                case 1:
                    AudioController.BansheeStruck2SoundEffectInstance.Volume = mBansheeStruckSoundVolume;
                    AudioController.BansheeStruck2SoundEffectInstance.Play();
                    break;

                case 2:

                    break;

                default:
                    break;
            }
        }

        #endregion

        #region Reaper Struck Sounds

        private void PlayReaperStruckSound()
        {
            int temp = randomNumberGenerator.Next(0, 2);
            switch (temp)
            {

                case 0:
                    AudioController.ReaperStruck1SoundEffectInstance.Volume = mReaperStruckSoundVolume;
                    AudioController.ReaperStruck1SoundEffectInstance.Play();
                    break;
                case 1:
                    AudioController.ReaperStruck2SoundEffectInstance.Volume = mReaperStruckSoundVolume;
                    AudioController.ReaperStruck2SoundEffectInstance.Play();
                    break;

                case 2:

                    break;

                default:
                    break;
            }
        }
        #endregion

        #region Gog Struck Sounds

        private void PlayGogStruckSound()
        {
            int temp = randomNumberGenerator.Next(0, 2);
            switch (temp)
            {



                case 0:
                    AudioController.GogStruck2SoundEffectInstance.Volume = mGogStruckSoundVolume;
                    AudioController.GogStruck2SoundEffectInstance.Play();
                    break;
                case 1:
                    AudioController.GogStruck2SoundEffectInstance.Volume = mGogStruckSoundVolume;
                    AudioController.GogStruck2SoundEffectInstance.Play();
                    break;

                default:
                    break;
            }
        }

        #endregion

        #region Imp Struck Sounds

        private void PlayImpStruckSound()
        {
            int temp = randomNumberGenerator.Next(0, 2);
            switch (temp)
            {
                case 0:
                    AudioController.ImpStruck1SoundEffectInstance.Volume = mImpStruckSoundVolume;
                    AudioController.ImpStruck1SoundEffectInstance.Play();

                    break;
                case 1:
                    AudioController.ImpStruck2SoundEffectInstance.Volume = mImpStruckSoundVolume;
                    AudioController.ImpStruck2SoundEffectInstance.Play();
                    break;

                case 2:

                    break;

                default:
                    break;
            }
        }

        #endregion

        #region DoomHound Struck Sounds

        private void PlayDoomHoundStruckSound()
        {
            int temp = randomNumberGenerator.Next(0, 1);
            switch (temp)
            {


                case 0:
                    AudioController.DoomHoundStruckSoundEffectInstance.Volume = mDoomHoundStruckSoundVolume;
                    AudioController.DoomHoundStruckSoundEffectInstance.Play();
                    break;

                default:
                    break;
            }
        }

        #endregion

        #region Unit Struck Audio

        public void PlayUnitStruckAudio(Hero friendlyActor)
        {

            int temp = randomNumberGenerator.Next(0, 2);

            switch (temp)
            {
                case 0:
                    AudioController.HeroStruck1SoundEffectInstance.Volume = mMeleeHitSoundVolume;
                    AudioController.HeroStruck1SoundEffectInstance.Play();

                    break;
                case 1:
                    AudioController.HeroStruck2SoundEffectInstance.Volume = mMeleeHitSoundVolume;
                    AudioController.HeroStruck2SoundEffectInstance.Play();
                    break;
                default:
                    break;

            }


        }

        public void PlayUnitStruckAudio(Friendly friendlyActor)
        {
            //TODO: place holder
            switch (friendlyActor.CreatureType)
            {

                case ObjectType.DRAGON:
                    PlayDragonStruckSound();
                    break;
                case ObjectType.NECROMANCER:
                    PlayNecromancerStruckSound();
                    break;
                case ObjectType.ARCANE_MAGE:
                    PlayArcaneMageStruckSound();
                    break;
                case ObjectType.FIRE_MAGE:
                    PlayFireMageStruckSound();
                    break;
                case ObjectType.WOLF:
                    PlayWolfStruckSound();
                    break;
                case ObjectType.AXE_THROWER:
                    PlayAxeThrowerStruckSound();
                    break;
                case ObjectType.BERSERKER:
                    PlayBerserkerStruckSound();
                    break;

                case ObjectType.CLERIC:
                    PlayClericStruckSound();
                    break;

                default:
                    break;
            }


        }

        #region Arcane Mage

        private void PlayArcaneMageStruckSound()
        {
            int temp = randomNumberGenerator.Next(0, 3);
            switch (temp)
            {
                case 0:

                    AudioController.ArcaneMageStruck1SoundEffectInstance.Volume = mArcaneMageStruckSoundVolume;
                    AudioController.ArcaneMageStruck1SoundEffectInstance.Play();

                    break;
                case 1:
                    AudioController.ArcaneMageStruck2SoundEffectInstance.Volume = mArcaneMageStruckSoundVolume;
                    AudioController.ArcaneMageStruck2SoundEffectInstance.Play();
                    break;

                default:
                    break;
            }
        }

        #endregion Arcane Mage Struck

        #region Axe Thrower Struck

        private void PlayAxeThrowerStruckSound()
        {
            int temp = randomNumberGenerator.Next(0, 3);
            switch (temp)
            {

                case 0:
                    AudioController.AxeThrowerStruck1SoundEffectInstance.Volume = mAxeThrowerStruckSoundVolume;
                    AudioController.AxeThrowerStruck1SoundEffectInstance.Play();
                    break;

                case 1:
                    AudioController.AxeThrowerStruck2SoundEffectInstance.Volume = mAxeThrowerStruckSoundVolume;
                    AudioController.AxeThrowerStruck2SoundEffectInstance.Play();
                    break;

                case 2:
                    AudioController.AxeThrowerStruck3SoundEffectInstance.Volume = mAxeThrowerStruckSoundVolume;
                    AudioController.AxeThrowerStruck3SoundEffectInstance.Play();
                    break;

                default:
                    break;
            }
        }

        #endregion Axe Thrower Struck

        #region Wolf Struck

        private void PlayWolfStruckSound()
        {
            int temp = randomNumberGenerator.Next(0, 1);
            switch (temp)
            {


                case 0:
                    AudioController.WolfStruckSoundEffectInstance.Volume = mWolfStruckSoundVolume;
                    AudioController.WolfStruckSoundEffectInstance.Play();
                    break;

                default:
                    break;
            }
        }

        #endregion Wolf Struck

        #region Necromancer Struck

        private void PlayNecromancerStruckSound()
        {
            int temp = randomNumberGenerator.Next(0, 2);
            switch (temp)
            {
                case 0:
                    AudioController.NecromancerStruck1SoundEffectInstance.Volume = mNecromancerStruckSoundVolume;
                    AudioController.NecromancerStruck1SoundEffectInstance.Play();
                    break;
                case 1:
                    AudioController.NecromancerStruck2SoundEffectInstance.Volume = mNecromancerStruckSoundVolume;
                    AudioController.NecromancerStruck2SoundEffectInstance.Play();
                    break;

                default:
                    break;
            }
        }
        #endregion Necromancer Struck

        #region Cleric Struck
        private void PlayClericStruckSound()
        {
            int temp = randomNumberGenerator.Next(0, 2);
            switch (temp)
            {
                case 0:
                    AudioController.ClericStruck1SoundEffectInstance.Volume = mClericStruckSoundVolume;
                    AudioController.ClericStruck1SoundEffectInstance.Play();
                    break;
                case 1:
                    AudioController.ClericStruck2SoundEffectInstance.Volume = mClericStruckSoundVolume;
                    AudioController.ClericStruck2SoundEffectInstance.Play();
                    break;
                default:
                    break;
            }
        }
        #endregion Cleric Struck

        #region Dragon Struck
        private void PlayDragonStruckSound()
        {

            int temp = randomNumberGenerator.Next(0, 2);

            switch (temp)
            {

                case 0:
                    AudioController.DragonStruck1SoundEffectInstance.Volume = mDragonStruckSoundVolume;
                    AudioController.DragonStruck1SoundEffectInstance.Play();
                    break;
                case 1:
                    AudioController.DragonStruck2SoundEffectInstance.Volume = mDragonStruckSoundVolume;
                    AudioController.DragonStruck2SoundEffectInstance.Play();
                    break;
                default:
                    break;
            }
        }

        #endregion Dragon Struck

        #region Fire Mage Struck

        private void PlayFireMageStruckSound()
        {
            int temp = randomNumberGenerator.Next(0, 2);
            switch (temp)
            {
                case 0:

                    AudioController.FireMageStruck1SoundEffectInstance.Volume = mFireMageStruckSoundVolume;
                    AudioController.FireMageStruck1SoundEffectInstance.Play();

                    break;
                case 1:
                    AudioController.FireMageStruck2SoundEffectInstance.Volume = mFireMageStruckSoundVolume;
                    AudioController.FireMageStruck2SoundEffectInstance.Play();
                    break;

                default:
                    break;
            }
        }
        #endregion Fire Mage Struck

        #region Berserker  Struck

        private void PlayBerserkerStruckSound()
        {
            int temp = randomNumberGenerator.Next(0, 5);
            switch (temp)
            {


                case 0:

                    AudioController.BerserkerStruck1SoundEffectInstance.Volume = mBerserkerStruckSoundVolume;
                    AudioController.BerserkerStruck1SoundEffectInstance.Play();

                    break;
                case 1:
                    AudioController.BerserkerStruck2SoundEffectInstance.Volume = mBerserkerStruckSoundVolume;
                    AudioController.BerserkerStruck2SoundEffectInstance.Play();
                    break;

                case 2:
                    AudioController.BerserkerStruck3SoundEffectInstance.Volume = mBerserkerStruckSoundVolume;
                    AudioController.BerserkerStruck3SoundEffectInstance.Play();
                    break;
                case 3:
                    AudioController.BerserkerStruck4SoundEffectInstance.Volume = mBerserkerStruckSoundVolume;
                    AudioController.BerserkerStruck4SoundEffectInstance.Play();
                    break;
                case 4:
                    AudioController.BerserkerStruck5SoundEffectInstance.Volume = mBerserkerStruckSoundVolume;
                    AudioController.BerserkerStruck5SoundEffectInstance.Play();
                    break;

                default:
                    break;
            }
        }
        #endregion Berserker Mage Struck






        #endregion Unit Struck Audio


        #endregion

        public void CheckSpriteAnimation()
        {
            switch (mSpriteState)
            {

                case SpriteState.ATTACK_LEFT:
                    mAnimationIndex = 0;
                    try
                    {
                        SetProjectileAnimation();

                        mSprite.LoadContent();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("No Animation for Projectile");
                    }

                    break;
                case SpriteState.ATTACK_RIGHT:
                    mAnimationIndex = 1;
                    try
                    {
                        SetProjectileAnimation();

                        mSprite.LoadContent();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("No Animation for Projectile");
                    }

                    break;
                case SpriteState.CHOMP_LEFT:
                    mAnimationIndex = 2;
                    try
                    {
                        SetProjectileAnimation();

                        mSprite.LoadContent();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("No Animation for Projectile");
                    }

                    break;
                case SpriteState.CHOMP_RIGHT:
                    mAnimationIndex = 3;
                    try
                    {
                        SetProjectileAnimation();

                        mSprite.LoadContent();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("No Animation for Projectile");
                    }

                    break;
                case SpriteState.RETURN_LEFT:
                    mAnimationIndex = 4;
                    try
                    {
                        SetProjectileAnimation();

                        mSprite.LoadContent();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("No Animation for Projectile");
                    }

                    break;
                case SpriteState.RETURN_RIGHT:
                    mAnimationIndex = 5;
                    try
                    {
                        SetProjectileAnimation();

                        mSprite.LoadContent();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("No Animation for Projectile");
                    }

                    break;
                case SpriteState.ABSORB_LEFT:
                    mAnimationIndex = 6;
                    try
                    {
                        SetProjectileAnimation();

                        mSprite.LoadContent();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("No Animation for Projectile");
                    }

                    break;
                case SpriteState.ABSORB_RIGHT:
                    mAnimationIndex = 7;
                    try
                    {
                        SetProjectileAnimation();

                        mSprite.LoadContent();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("No Animation for Projectile");
                    }

                    break;

                default:
                    break;
            }
        }
    }
}
