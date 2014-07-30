using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ExternalTypes;
using GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace GameObjects
{
    public class Hero : GameObject
    {

        private BehaviorHero mBehaviorHero;
        public Vector2 mTarget;
        private Rectangle mPlayingField;
        private HeroAttribute mheroAttribute;
        private const float mRegenManaTime = 3000;
        private float mRegenManaTimer;

        public Hero(ObjectType mObjectType, ContentManager content,
            SpriteState defaultState, Vector2 SpritePosition)
            : base(mObjectType, content, defaultState, SpritePosition, Hostility.FRIENDLY)
        {

            mSpriteScale = 0.8f;
            this.mSprite.SpriteScale = mSpriteScale;
            this.hostility = Hostility.FRIENDLY;
            SetUnitAnimation();
            mTarget = SpritePosition;
            mBehaviorHero = new BehaviorHero(1.0f, mTarget);
            mObjectID = mGlobalID;
            mGlobalID++;
            mheroAttribute = new HeroAttribute(this, content);
            SetAttributes();

            int mAttackLevel = (int)HeroAttribute.AttackUpgradeLevel;
            int mDefenseLevel = (int)HeroAttribute.DefenseUpgradeLevel;

            for (int i = 0; i < mAttackLevel; i++)
            {
                mheroAttribute.UpgradeAttack();
            }

            for (int i = 0; i < mDefenseLevel; i++)
            {
                mheroAttribute.UpgradeDefense();
            }
            
        }

        public void SetAttributes()
        {
            //TODO: set Hero's attributes

            mheroAttribute.MaxHealthPoints = 150;
            mheroAttribute.MaxMana = 40;
            mheroAttribute.DefaultSpeed = 4;
            mheroAttribute.Gold = 1000;
            mheroAttribute.Experience = 0;
            mheroAttribute.BaseMaximumDamage = 80;
            mheroAttribute.BaseMinimumDamage = 50;
            mheroAttribute.BaseDamageModifier = (mheroAttribute.BaseMaximumDamage + mheroAttribute.BaseMinimumDamage) / 2;
        }


        public void PostSetSpriteFrame()
        {
            // this.GetSprite().SetSpriteFrame((int)this.GetSprite().GetWorldPosition().X, (int)this.GetSprite().GetWorldPosition().Y);

        }

        public BehaviorHero BehaviorHero
        {
            get { return mBehaviorHero; }
            set { mBehaviorHero = value; }
        }

        public HeroAttribute HeroAttribute
        {
            get { return mheroAttribute; }
        }

        public Rectangle PlayingField
        {
            get { return mPlayingField; }
            set { mPlayingField = value; }
        }

        public void SetHeroTarget(Vector2 mTarget)
        {
            this.mTarget = mTarget;
        }


        public Vector2 GetTarget()
        {
            return mTarget;
        }



        public override void Update(GameTime gameTime)
        {
            mRegenManaTimer += gameTime.ElapsedGameTime.Milliseconds;

            if (mRegenManaTimer >= mRegenManaTime)
            {
                this.HeroAttribute.Mana += 1;
                mRegenManaTimer = 0;
            }

            if (this.Sprite.SpriteFrame.Y + this.Sprite.SpriteFrame.Height >= 800)
            {

                this.mTarget = new Vector2(this.Sprite.WorldPosition.X, this.Sprite.WorldPosition.Y - 11);
            }

            if (this.Sprite.SpriteFrame.Y <= 25)
            {

                this.mTarget = new Vector2(this.Sprite.WorldPosition.X, this.Sprite.WorldPosition.Y + 11);
            }

            if (this.Sprite.SpriteFrame.X + this.Sprite.SpriteFrame.Width >= 1380)
            {

                this.mTarget = new Vector2(this.Sprite.WorldPosition.X - 11, this.Sprite.WorldPosition.Y);
            }

            if (this.Sprite.SpriteFrame.X <= 0)
            {
                this.mTarget = new Vector2(this.Sprite.WorldPosition.X + 11, this.Sprite.WorldPosition.Y);
            }



            mBehaviorHero.Update(this);



            if (this.mDirection.Length() > 0.0f)
            {
                this.mDirection.Normalize();
            }

            this.mSprite.WorldPosition = this.mSprite.WorldPosition + this.mDirection * mheroAttribute.Speed;

            // Check current sprite state.
            switch (mSpriteState)
            {

                case SpriteState.MOVE_LEFT:

                    mAnimationIndex = 0;
                    try
                    {
                        SetUnitAnimation();

                        mSprite.LoadContent();
                    }
                    catch (Exception e)
                    {

                    }
                    break;

                case SpriteState.MOVE_RIGHT:

                    mAnimationIndex = 1;

                    // UpdateHP animation.
                    try
                    {
                        SetUnitAnimation();
                        mSprite.LoadContent();
                    }
                    catch (Exception e)
                    {

                    }
                    break;



                case SpriteState.IDLE_LEFT:
                    mAnimationIndex = 2;
                    try
                    {
                        SetUnitAnimation();
                        mSprite.LoadContent();
                    }
                    catch (Exception e)
                    {

                    }
                    break;

                case SpriteState.IDLE_RIGHT:
                    mAnimationIndex = 3;
                    try
                    {
                        SetUnitAnimation();
                        mSprite.LoadContent();
                    }
                    catch (Exception e)
                    {

                    }
                    break;


                case SpriteState.ATTACK_LEFT:
                    mAnimationIndex = 4;
                    try
                    {
                        SetUnitAnimation();
                        mSprite.LoadContent();
                    }
                    catch (Exception e)
                    {

                    }
                    break;

                case SpriteState.ATTACK_RIGHT:
                    mAnimationIndex = 5;
                    try
                    {
                        SetUnitAnimation();
                        mSprite.LoadContent();
                    }
                    catch (Exception e)
                    {

                    }
                    break;
                case SpriteState.DEATH_LEFT:
                    mAnimationIndex = 6;
                    try
                    {
                        SetUnitAnimation();
                        mSprite.LoadContent();
                    }
                    catch (Exception e)
                    {

                    }
                    break;
                case SpriteState.DEATH_RIGHT:
                    mAnimationIndex = 7;
                    try
                    {
                        SetUnitAnimation();
                        mSprite.LoadContent();
                    }
                    catch (Exception e)
                    {

                    }
                    break;
                case SpriteState.BATTLECRY_LEFT:
                    mAnimationIndex = 8;
                    try
                    {
                        SetUnitAnimation();
                        mSprite.LoadContent();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Not Working");
                    }
                    break;
                case SpriteState.BATTLECRY_RIGHT:
                    mAnimationIndex = 9;
                    try
                    {
                        SetUnitAnimation();
                        mSprite.LoadContent();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Not Working");
                    }
                    break;
                case SpriteState.INTIMIDATE_LEFT:
                    mAnimationIndex = 10;
                    try
                    {
                        SetUnitAnimation();
                        mSprite.LoadContent();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Not Working");
                    }
                    break;
                case SpriteState.INTIMIDATE_RIGHT:
                    mAnimationIndex = 11;
                    try
                    {
                        SetUnitAnimation();
                        mSprite.LoadContent();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Not Working");
                    }
                    break;
                default:

                    break;
            }

        }
    }
}
