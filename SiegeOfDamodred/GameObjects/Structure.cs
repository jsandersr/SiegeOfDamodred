using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExternalTypes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
namespace GameObjects
{
    public class Structure : GameObject
    {



        private ObjectType mStructureType;
        private ObjectType mUnitType;
        private ContentManager content;
        private SpriteState defaultState;
        private Vector2 mSpritePosition;
        private ObjectColor mStructureColor;
        private StructureAttribute mStructureAttribute;
        private float mCoolDownTimer;
        private Vector2 mButtonPosition;

        public Structure(ObjectType mStructureType, ContentManager content,
                         SpriteState defaultState, Vector2 SpritePosition, ObjectColor mStructureColor, Vector2 ButtonPosition)
            : base(mStructureType, content, defaultState, SpritePosition, Hostility.STRUCTURE)
        {

            this.mStructureType = mStructureType;
            this.mStructureColor = mStructureColor;
            this.content = content;
            this.defaultState = defaultState;
            this.mSpritePosition = SpritePosition;
            this.mButtonPosition = ButtonPosition;

            mStructureAttribute = new StructureAttribute(content, this);
            SetAttributes();
            SetUnitAnimation();

        }

        #region Properties

        public Vector2 ButtonPosition
        {
            get { return mButtonPosition; }
            set { mButtonPosition = value; }
        }


        public void SetObjectID()
        {
            mObjectID = mGlobalID;
            mGlobalID++;
        }

        public ObjectType UnitType
        {
            get { return mUnitType; }
        }

        public ObjectType StructureType
        {
            get { return mStructureType; }
        }

        public ObjectColor StructureColor
        {
            get { return mStructureColor; }
            set { mStructureColor = value; }
        }

        public Vector2 SpritePosition
        {
            get { return mSpritePosition; }
            set { mSpritePosition = value; }
        }

        public Hostility Hostility
        {
            get { return hostility; }

        }



        public StructureAttribute StructureAttribute
        {
            get { return mStructureAttribute; }
            set { mStructureAttribute = value; }
        }

        #endregion

        public void SetAttributes()
        {
            switch (mStructureType)
            {
                case ObjectType.DRAGON_CAVE:

                    mUnitType = ObjectType.DRAGON;

                    StructureAttribute.SpawnTimer = 7000f;
                    StructureAttribute.MaxHealthPoints = 250;
                    StructureAttribute.Cost = 50000;
                    StructureAttribute.UpgradeDefenseCost = 1500;
                    StructureAttribute.UpgradeAttackCost = 2300;
                    //TODO: Set Base Attributes

                    break;

                case ObjectType.BONEPIT:
                    mUnitType = ObjectType.NECROMANCER;
                    StructureAttribute.MaxHealthPoints = 250;
                    StructureAttribute.SpawnTimer = 7000f;
                    StructureAttribute.Cost = 20000;
                    StructureAttribute.UpgradeDefenseCost = 1500;
                    StructureAttribute.UpgradeAttackCost = 2300;
                    //TODO: Set Base Attributes
                    break;

                case ObjectType.LIBRARY:
                    mUnitType = ObjectType.ARCANE_MAGE;
                    StructureAttribute.MaxHealthPoints = 250;
                    StructureAttribute.SpawnTimer = 7000f;
                    StructureAttribute.Cost = 10000;
                    StructureAttribute.UpgradeDefenseCost = 1500;
                    StructureAttribute.UpgradeAttackCost = 2300;
                    //TODO: Set Base Attributes
                    break;

                case ObjectType.FIRE_TEMPLE:
                    mUnitType = ObjectType.FIRE_MAGE;
                    StructureAttribute.SpawnTimer = 7000f;
                    StructureAttribute.MaxHealthPoints = 250;
                    StructureAttribute.UpgradeDefenseCost = 1500;
                    StructureAttribute.UpgradeAttackCost = 2300;
                    StructureAttribute.Cost = 20000;
                    //TODO: Set Base Attributes
                    break;

                case ObjectType.ARMORY:
                    mUnitType = ObjectType.AXE_THROWER;
                    StructureAttribute.SpawnTimer = 3000f;
                    StructureAttribute.MaxHealthPoints = 250;
                    StructureAttribute.Cost = 5000;
                    StructureAttribute.UpgradeDefenseCost = 1500;
                    StructureAttribute.UpgradeAttackCost = 2300;
                    //TODO: Set Base Attributes
                    break;

                case ObjectType.BARRACKS:
                    mUnitType = ObjectType.BERSERKER;
                    StructureAttribute.SpawnTimer = 7000f;
                    StructureAttribute.MaxHealthPoints = 250;
                    StructureAttribute.UpgradeDefenseCost = 1500;
                    StructureAttribute.UpgradeAttackCost = 2300;

                    StructureAttribute.Cost = 5000;
                    //TODO: Set Base Attributes
                    break;

                case ObjectType.WOLFPEN:
                    mUnitType = ObjectType.WOLF;
                    StructureAttribute.SpawnTimer = 5000f;
                    StructureAttribute.MaxHealthPoints = 250;
                    StructureAttribute.UpgradeDefenseCost = 1500;
                    StructureAttribute.UpgradeAttackCost = 2300;
                    StructureAttribute.Cost = 1000;
                    //TODO: Set Base Attributes
                    break;

                case ObjectType.ABBEY:
                    mUnitType = ObjectType.CLERIC;
                    StructureAttribute.SpawnTimer = 5000f;
                    StructureAttribute.MaxHealthPoints = 250;
                    StructureAttribute.UpgradeDefenseCost = 1500;
                    StructureAttribute.UpgradeAttackCost = 2300;
                    StructureAttribute.Cost = 20000;
                    //TODO: Set Base Attributes
                    break;

                default:
                    Console.WriteLine("Structure problems");
                    break;
            }
        }


        public override void Update(GameTime gameTime)
        {

            if (this.StructureAttribute.CurrentHealthPoints <= 0)
            {
                HasDied = true;
            }

            if (StructureAttribute.CoolDownState == CoolDownState.ONCOOLDOWN)
            {
                mCoolDownTimer += gameTime.ElapsedGameTime.Milliseconds;

                if (mCoolDownTimer >= StructureAttribute.CoolDownTimer)
                {
                    StructureAttribute.CoolDownState = CoolDownState.OFFCOOLDOWN;
                }
            }


        }
    }
}
