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
    public class Enemy : GameObject
    {
        private Random random;
        private AttackState mAttackState;
        private int mFieldOfViewSize;
        private Rectangle mFieldOfView;
        private BehaviorEnemy mBehaviorEnemy;
        private JTargetQueue mTargetQueue = new JTargetQueue();
        private Rectangle mDefaultTarget;
        private Vector2 mCurrentTarget;
        private bool isInPlay;
        private EnemyAttribute enemyAttribute;
        private int y;
        private EnemyAggroMode mEnemyAggroMode;


        public Enemy(ObjectType mObjectType, ContentManager content,
            SpriteState defaultState, Castle castle, Vector2 SpritePosition)
            : base(mObjectType, content, defaultState, SpritePosition, Hostility.ENEMY)
        {
            isInPlay = true;
            this.hostility = Hostility.ENEMY;
            mFieldOfViewSize = 200;

            SetUnitAnimation();
            random = new Random();

            y = random.Next(25, 700);
            mDefaultTarget = new Rectangle(1400, y, 200, 775);
            mCurrentTarget = new Vector2(mDefaultTarget.X, mDefaultTarget.Y);

            mBehaviorEnemy = new BehaviorEnemy(1.0f);
            mObjectID = mGlobalID;
            mGlobalID++;


            enemyAttribute = new EnemyAttribute(this, content, castle);
            SetAttributes();
            this.AttackState = AttackState.READY;

            enemyAttribute.UpgradeAttack();
            enemyAttribute.UpgradeDefense();
        }


        public void SetAttributes()
        {
            switch (CreatureType)
            {


                case ObjectType.REAPER:
                    //TODO: Set Base Attributes
                    enemyAttribute.MaxHealthPoints = 500;
                    enemyAttribute.UnitSpeed = .5f;
                    enemyAttribute.BaseMinimumDamage = 200;
                    enemyAttribute.BaseMaximumDamage = 250;
                    enemyAttribute.BaseDamageModifier = (enemyAttribute.BaseMinimumDamage + enemyAttribute.BaseMaximumDamage) / 2.0f;
                    enemyAttribute.AttackSpeed = 400;
                    break;

                case ObjectType.BANSHEE:
                    //TODO: Set Base Attributes
                    enemyAttribute.MaxHealthPoints = 275;
                    enemyAttribute.UnitSpeed = .5f;
                    enemyAttribute.BaseMinimumDamage = 95;
                    enemyAttribute.BaseMaximumDamage = 110;
                    enemyAttribute.BaseDamageModifier = (enemyAttribute.BaseMinimumDamage + enemyAttribute.BaseMaximumDamage) / 2.0f;
                    enemyAttribute.AttackSpeed = 800;
                    break;

                case ObjectType.GOG:
                    //TODO: Set Base Attributes
                    enemyAttribute.MaxHealthPoints = 90;
                    enemyAttribute.UnitSpeed = .5f;
                    enemyAttribute.BaseMinimumDamage = 13;
                    enemyAttribute.BaseMaximumDamage = 20;
                    enemyAttribute.BaseDamageModifier = (enemyAttribute.BaseMinimumDamage + enemyAttribute.BaseMaximumDamage) / 2.0f;
                    enemyAttribute.AttackSpeed = 800;
                    break;

                case ObjectType.DOOM_HOUND:
                    //TODO: Set Base Attributes
                    enemyAttribute.MaxHealthPoints = 60;
                    enemyAttribute.UnitSpeed = .5f;
                    enemyAttribute.BaseMinimumDamage = 8;
                    enemyAttribute.BaseMaximumDamage = 15;
                    enemyAttribute.BaseDamageModifier = (enemyAttribute.BaseMinimumDamage + enemyAttribute.BaseMaximumDamage) / 2.0f;
                    enemyAttribute.AttackSpeed = 2000;
                    break;

                case ObjectType.IMP:
                    //TODO: Set Base Attributes
                    enemyAttribute.MaxHealthPoints = 45;
                    enemyAttribute.UnitSpeed = .5f;
                    enemyAttribute.BaseMinimumDamage = 3;
                    enemyAttribute.BaseMaximumDamage = 10;
                    enemyAttribute.BaseDamageModifier = (enemyAttribute.BaseMinimumDamage + enemyAttribute.BaseMaximumDamage) / 2.0f;
                    enemyAttribute.AttackSpeed = 2000;
                    break;



                default:
                    Console.WriteLine("Enemy.SetAttributes Crash");
                    break;
            }

        }

        public EnemyAttribute EnemyAttribute
        {
            get { return enemyAttribute; }
            set { enemyAttribute = value; }
        }

        public EnemyAggroMode EnemyAggroMode
        {
            get { return mEnemyAggroMode; }
            set { mEnemyAggroMode = value; }
        }

        public Rectangle DefaultTarget
        {
            get { return mDefaultTarget; }
            set { mDefaultTarget = value; }
        }

        public JTargetQueue TargetQueue
        {
            get { return mTargetQueue; }
        }

        public void SetObjectID()
        {
            mObjectID = mGlobalID;
            mGlobalID++;
        }

        public AttackState AttackState
        {
            get { return mAttackState; }
            set { mAttackState = value; }
        }

        public Vector2 CurrentTarget
        {
            get { return mCurrentTarget; }
        }

        public void PostSetSpriteFrame()
        {


            this.mFieldOfView = new Rectangle((int)this.Sprite.WorldPosition.X - mFieldOfViewSize,
                                              (int)this.Sprite.WorldPosition.Y - mFieldOfViewSize,
                                                   this.Sprite.SpriteFrame.Width + mFieldOfViewSize * 2,
                                                   this.Sprite.SpriteFrame.Height + mFieldOfViewSize * 2);
        }

        public Rectangle FieldOfView
        {
            get { return mFieldOfView; }
        }

        public int FieldOfViewSize
        {
            set { this.mFieldOfViewSize = value; }
            get { return mFieldOfViewSize; }
        }

        public void SetTarget()
        {
            mCurrentTarget = this.mTargetQueue.PeekFirst().Sprite.WorldPosition;
        }

        public void SetDefaultTarget()
        {
            mCurrentTarget = new Vector2(mDefaultTarget.X, mDefaultTarget.Y);
        }

        public override void Update(GameTime gameTime)
        {
            if (isInPlay)
            {

                if (!mTargetQueue.IsEmpty())
                {
                    SetTarget();
                }
                else
                {
                    SetDefaultTarget();
                }

                mBehaviorEnemy.Update(this);

                if (this.mDirection.Length() > 0.0f)
                {
                    this.mDirection.Normalize();
                }

                this.mSprite.WorldPosition = this.mSprite.WorldPosition + this.mDirection * enemyAttribute.UnitSpeed;

                // Update position of the field of view
                mFieldOfView.X = (int)this.mSprite.WorldPosition.X - mFieldOfViewSize;
                mFieldOfView.Y = (int)this.mSprite.WorldPosition.Y - mFieldOfViewSize;


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

                        // Update animation.
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

                        // Update animation.
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

                        // Update animation.
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
        }


    }
}
