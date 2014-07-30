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
    public class ItemDrop : GameObject
    {






        // Health value, Gold value, etc.
        private int mValue;

        // Flips to true is touched by hero.
        private bool mIsTagged;
        // Send to graveyard if true;
        private bool mIsDead;

        private float mTimer;
        private float mTimeToLive;
        private Hero mHero;
        private Vector2 mSpawnLocation;
        private ObjectType mObjectType;

        public ItemDrop(int value, ObjectType mObjectType, ContentManager content, SpriteState defaultState, Vector2 SpritePosition, Hostility hostility)
            : base(mObjectType, content, defaultState, SpritePosition, hostility)
        {


            this.mObjectType = mObjectType;
            mIsTagged = false;
            mTimeToLive = 10000;
            mTimer = 0;
            mSpawnLocation = SpritePosition;
            this.mValue = value;
            mSprite = new Sprite(content);
            SetItemDropAnimation();
            mSprite.LoadContent();
            mSprite.WorldPosition = mSpawnLocation;
        }

        #region Properties

        public Hero Hero
        {
            set { mHero = value; }
        }

        public bool IsDead
        {
            get { return mIsDead; }
            set { mIsDead = value; }
        }

        public bool IsTagged
        {
            get { return mIsTagged; }
            set { mIsTagged = value; }
        }

        public Sprite Sprite
        {
            get { return mSprite; }
            set { mSprite = value; }
        }

        public int Value
        {
            get { return mValue; }
            set { mValue = value; }
        }

        #endregion


        public void Draw(SpriteBatch spriteBatch)
        {
            if (!mIsDead)
            {

                Sprite.Draw(spriteBatch, 1.0f);
            }

        }

        public void Update(GameTime gameTime)
        {
            // Disappear when my sprite frame intersects with the hero's or my time runs out
            // Transfer my benefits to the hero
            // be marked for deletion
            mSprite.Update(gameTime);
            mTimer += gameTime.ElapsedGameTime.Milliseconds;

            if (mTimer >= mTimeToLive)
            {
                mIsDead = true;
                mTimer = 0.0f;
            }


            switch (mObjectType)
            {
                case ObjectType.GOLD:
                    if (mIsTagged)
                    {
                        if (mHero != null)
                        {
                            mHero.HeroAttribute.Gold += mValue;
                            mIsDead = true;
                        }
                        else
                        {
                            Console.WriteLine("Send HealthDrop a hero please.");
                        }

                    }
                    break;

                case ObjectType.HEALTH:
                    if (mIsTagged)
                    {
                        if (mHero != null)
                        {
                            mHero.HeroAttribute.CurrentHealthPoints += mValue;
                            mIsDead = true;
                        }
                        else
                        {
                            Console.WriteLine("Send HealthDrop a hero please.");
                        }

                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }


        }



    }
}
