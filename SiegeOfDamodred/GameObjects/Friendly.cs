using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExternalTypes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using SpriteGenerator;

namespace GameObjects
{


    public class Friendly : GameObject
    {
        private int mFieldOfViewSize;
        private Rectangle mFieldOfView;
        private BehaviorFriendly mBehaviorFriendly;
        private JTargetQueue mTargetQueue = new JTargetQueue();
        private Vector2 mDefaultTarget;
        private Vector2 mCurrentTarget;
        private ObjectColor mColor;
        private FriendlyAttribute mFriendlyAttribute;
        private FriendlyAggroMode mFriendlyAggroMode;


        public Friendly(ObjectType mObjectType, ContentManager content,
                        SpriteState defaultState, Vector2 SpritePosition, Vector2 mDefaultTarget, int mAttackLevel, int mDefenseLevel)
            : base(mObjectType, content, defaultState, SpritePosition, Hostility.FRIENDLY)
        {
            this.DefaultTarget = mDefaultTarget;
            this.mCurrentTarget = mDefaultTarget;
            this.hostility = Hostility.FRIENDLY;
            mBehaviorFriendly = new BehaviorFriendly(1.0f);
            mFieldOfViewSize = 300;

            mObjectID = mGlobalID;
            mGlobalID++;
            SetUnitAnimation();

            mFriendlyAttribute = new FriendlyAttribute(this, content);
            if (GameObject.FromSaveGameState == FromSaveGame.NEWGAME)
            {
                SetAttributes();
            }
            this.FriendlyAggroMode = FriendlyAggroMode.MOVING;

            for (int i = 0; i < mAttackLevel; i++)
            {
                mFriendlyAttribute.UpgradeAttack();
            }
            for (int i = 0; i < mDefenseLevel; i++)
            {
                mFriendlyAttribute.UpgradeDefense();
            }

        }



        public void SetAttributes()
        {
            switch (CreatureType)
            {
                case ObjectType.DRAGON:
                    //TODO: Set Base Attributes
                    mFriendlyAttribute.MaxHealthPoints = 700;
                    mFriendlyAttribute.BaseMinimumDamage = 275;
                    mFriendlyAttribute.BaseMaximumDamage = 300;
                    mFriendlyAttribute.BaseDamageModifier = (mFriendlyAttribute.BaseMinimumDamage + mFriendlyAttribute.BaseMaximumDamage) / 2;
                    mFriendlyAttribute.DefaultSpeed = 2.0f;
                    mFriendlyAttribute.AttackSpeed = 2500;
                    break;

                case ObjectType.FIRE_MAGE:
                    //TODO: Set Base Attributes
                    mFriendlyAttribute.MaxHealthPoints = 180;
                    mFriendlyAttribute.BaseMinimumDamage = 60;
                    mFriendlyAttribute.BaseMaximumDamage = 80;
                    mFriendlyAttribute.BaseDamageModifier = (mFriendlyAttribute.BaseMinimumDamage + mFriendlyAttribute.BaseMaximumDamage) / 2;
                    mFriendlyAttribute.DefaultSpeed = 2.0f;
                    mFriendlyAttribute.AttackSpeed = 2500;
                    break;

                case ObjectType.NECROMANCER:
                    //TODO: Set Base Attributes
                    mFriendlyAttribute.MaxHealthPoints = 300;
                    mFriendlyAttribute.BaseMinimumDamage = 15;
                    mFriendlyAttribute.BaseMaximumDamage = 35;
                    mFriendlyAttribute.BaseDamageModifier = (mFriendlyAttribute.BaseMinimumDamage + mFriendlyAttribute.BaseMaximumDamage) / 2;
                    mFriendlyAttribute.DefaultSpeed = 2.0f;
                    mFriendlyAttribute.AttackSpeed = 2000.0f;
                    break;

                case ObjectType.CLERIC:
                    //TODO: Set Base Attributes
                    mFriendlyAttribute.MaxHealthPoints = 300;
                    mFriendlyAttribute.BaseMinimumDamage = 10;
                    mFriendlyAttribute.BaseMaximumDamage = 30;
                    mFriendlyAttribute.BaseDamageModifier = (mFriendlyAttribute.BaseMinimumDamage + mFriendlyAttribute.BaseMaximumDamage) / 2;
                    mFriendlyAttribute.DefaultSpeed = 2.0f;
                    mFriendlyAttribute.AttackSpeed = 4000;
                    break;

                case ObjectType.ARCANE_MAGE:
                    //TODO: Set Base Attributes
                    mFriendlyAttribute.MaxHealthPoints = 180;
                    mFriendlyAttribute.BaseMinimumDamage = 60;
                    mFriendlyAttribute.BaseMaximumDamage = 80;
                    mFriendlyAttribute.BaseDamageModifier = (mFriendlyAttribute.BaseMinimumDamage + mFriendlyAttribute.BaseMaximumDamage) / 2;
                    mFriendlyAttribute.DefaultSpeed = 2.0f;
                    mFriendlyAttribute.AttackSpeed = 2500;
                    break;

                case ObjectType.BERSERKER:
                    //TODO: Set Base Attributes
                    mFriendlyAttribute.MaxHealthPoints = 120;
                    mFriendlyAttribute.BaseMinimumDamage = 30;
                    mFriendlyAttribute.BaseMaximumDamage = 55;
                    mFriendlyAttribute.BaseDamageModifier = (mFriendlyAttribute.BaseMinimumDamage + mFriendlyAttribute.BaseMaximumDamage) / 2;
                    mFriendlyAttribute.DefaultSpeed = 2.0f;
                    mFriendlyAttribute.AttackSpeed = 2500;
                    break;

                case ObjectType.AXE_THROWER:
                    //TODO: Set Base Attributes
                    mFriendlyAttribute.MaxHealthPoints = 90;
                    mFriendlyAttribute.BaseMinimumDamage = 3;
                    mFriendlyAttribute.BaseMaximumDamage = 10;
                    mFriendlyAttribute.BaseDamageModifier = (mFriendlyAttribute.BaseMinimumDamage + mFriendlyAttribute.BaseMaximumDamage) / 2;
                    mFriendlyAttribute.DefaultSpeed = 2.0f;
                    mFriendlyAttribute.AttackSpeed = 800.0f;
                    break;

                case ObjectType.WOLF:
                    //TODO: Set Base Attributes
                    mFriendlyAttribute.MaxHealthPoints = 60;
                    mFriendlyAttribute.BaseMinimumDamage = 3;
                    mFriendlyAttribute.BaseMaximumDamage = 15;
                    mFriendlyAttribute.BaseDamageModifier = (mFriendlyAttribute.BaseMinimumDamage + mFriendlyAttribute.BaseMaximumDamage) / 2;
                    mFriendlyAttribute.DefaultSpeed = 2.0f;
                    mFriendlyAttribute.AttackSpeed = 2000;
                    break;

                default:

                    Console.WriteLine("Friendly problems");
                    break;
            }
        }


        // TODO: Make sure this is being called on construction.
        public void PostSetFOV()
        {
            //   this.GetSprite().SetSpriteFrame((int)this.GetSprite().GetWorldPosition().X, (int)this.GetSprite().GetWorldPosition().Y);
            this.mFieldOfView = new Rectangle((int)this.Sprite.WorldPosition.X - mFieldOfViewSize,
                                              (int)this.Sprite.WorldPosition.Y - mFieldOfViewSize,
                                                   this.Sprite.SpriteFrame.Width + mFieldOfViewSize * 2,
                                                   this.Sprite.SpriteFrame.Height + mFieldOfViewSize * 2);
        }

        #region Properties

        public FriendlyAggroMode FriendlyAggroMode
        {
            get { return mFriendlyAggroMode; }
            set { mFriendlyAggroMode = value; }
        }

        public FriendlyAttribute FriendlyAttribute
        {
            get { return mFriendlyAttribute; }
            set { mFriendlyAttribute = value; }
        }

        public ObjectColor Color
        {
            get { return mColor; }
            set { mColor = value; }
        }


        public Vector2 CurrentTarget
        {
            get { return mCurrentTarget; }
            set { mCurrentTarget = value; }
        }

        public Vector2 DefaultTarget
        {
            get { return mDefaultTarget; }
            set { mDefaultTarget = value; }
        }

        public Rectangle FieldOfView
        {
            get { return mFieldOfView; }
            set { mFieldOfView = value; }
        }

        public JTargetQueue TargetQueue
        {
            get { return mTargetQueue; }
            set { mTargetQueue = value; }
        }

        new public Hostility Hostility
        {
            get { return hostility; }
            set { hostility = value; }
        }

        public Vector2 Direction
        {
            get { return mDirection; }
            set { mDirection = value; }
        }

        public int ObjectId
        {
            get { return mObjectID; }
            set { mObjectID = value; }
        }

        public float SpriteScale
        {
            get { return mSpriteScale; }
            set { mSpriteScale = value; }
        }

        public Sprite Sprite
        {
            get { return mSprite; }
            set { mSprite = value; }
        }

        public SpriteState SpriteState
        {
            get { return mSpriteState; }
            set { mSpriteState = value; }
        }

        public BehaviorFriendly BehaviorFriendly
        {
            get { return mBehaviorFriendly; }
            set { mBehaviorFriendly = value; }
        }
        #endregion


        public override void Update(GameTime gameTime)
        {

            if (!mTargetQueue.IsEmpty())
            {
                SetTargetCoordinate();
            }
            else
            {
                SetDefaultTarget();
            }

            mBehaviorFriendly.Update(this, gameTime);


            if (this.mDirection.Length() > 0.0f)
            {
                this.mDirection.Normalize();
            }
            this.mSprite.WorldPosition = this.mSprite.WorldPosition +
                                         this.mDirection * this.FriendlyAttribute.UnitSpeed;

            mFieldOfView.X = (int)this.mSprite.WorldPosition.X - mFieldOfViewSize;
            mFieldOfView.Y = (int)this.mSprite.WorldPosition.Y - mFieldOfViewSize;

            CheckSpriteAnimation();


        }

        private void SetDefaultTarget()
        {
            CurrentTarget = mDefaultTarget;
        }

        public void CheckSpriteAnimation()
        {
            // Check current sprite state.
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
                default:

                    break;
            }
        }


        public void SetTargetCoordinate()
        {
            mCurrentTarget = this.mTargetQueue.PeekFirst().Sprite.WorldPosition;
        }




    }

}
