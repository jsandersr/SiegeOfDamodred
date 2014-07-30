using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace GameObjects
{
    public class Attribute
    {

        // Universal unit Attributes
        protected Random mRandomGenerator = new Random();
        protected float mCurrentHealthPoints;
        protected float mMaxHealthPoints;

        // Damage Attributes.
        protected float mBaseMinimumDamage;
        protected float mBaseMaximumDamage;

        // Base damage modifier =  mase min +  base max / 2
        protected float mBaseDamageModifier;
        protected float mCurrentMinimumDamage;
        protected float mCurrentMaximumDamage;

        // Armor Attributes.
        protected const float BaseArmorModifer = 2;
        protected float mCurrentArmor;
        protected ContentManager mContent;

        public Attribute(ContentManager content)
        {
            this.mContent = content;
        }


        #region  Properties

        public float CurrentArmor
        {
            get { return mCurrentArmor; }
            set
            {
                if (value <= 65)
                    mCurrentArmor = value;
            }
        }

        public float BaseDamageModifier
        {
            get { return mBaseDamageModifier; }
            set { mBaseDamageModifier = value; }
        }

        public float BaseMaximumDamage
        {
            get { return mBaseMaximumDamage; }
            set { mBaseMaximumDamage = value; }
        }

        public float BaseMinimumDamage
        {
            get { return mBaseMinimumDamage; }
            set { mBaseMinimumDamage = value; }
        }

        public float CurrentMaximumDamage
        {
            get { return mCurrentMaximumDamage; }
            set { mCurrentMaximumDamage = value; }
        }

        public float CurrentMinimumDamage
        {
            get { return mCurrentMinimumDamage; }
            set { mCurrentMinimumDamage = value; }
        }

        public float MaxHealthPoints
        {
            get { return mMaxHealthPoints; }
            set
            {
                mMaxHealthPoints = value;
                mCurrentHealthPoints = value;
            }
        }


        public float CurrentHealthPoints
        {
            get { return mCurrentHealthPoints; }
            set
            {
                mCurrentHealthPoints = MathHelper.Clamp(value, 0, mMaxHealthPoints);
            }
        }

        #endregion
    }
}
