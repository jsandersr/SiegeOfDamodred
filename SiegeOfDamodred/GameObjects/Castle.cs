using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ExternalTypes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace GameObjects
{
    public class Castle : GameObject
    {
        private CastleAttribute mCastleAttribute;

        public Castle(ObjectType mObjectType,
                      ContentManager content, SpriteState defaultState, Vector2 SpritePosition)
            : base(mObjectType, content, defaultState, SpritePosition, Hostility.CASTLE)
        {
            mObjectID = -1;
            mCastleAttribute = new CastleAttribute(this, content);
            SetAttributes();
        }

        public void SetAttributes()
        {
            CastleAttribute.MaxHealthPoints = 500;
            CastleAttribute.CurrentHealthPoints = 500;

        }

        public CastleAttribute CastleAttribute
        {
            get { return mCastleAttribute; }
            set { mCastleAttribute = value; }
        }

        public void PostSetSpriteFrame()
        {
            this.Sprite.SetSpriteFrame(1450, 450);

        }

        public override void Update(GameTime gameTime)
        {

            if (this.CastleAttribute.CurrentHealthPoints <= 0)
            {
                HasDied = true;
            }

            // Check current sprite state.
            switch (mSpriteState)
            {
                case SpriteState.MOVE_RIGHT:

                    mAnimationIndex = 0;

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

                case SpriteState.MOVE_LEFT:

                    mAnimationIndex = 1;
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

                default:

                    break;
            }
        }
    }

}
