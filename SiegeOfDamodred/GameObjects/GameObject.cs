/*
            This class will hold all of our data for each individual game object. A game object
            may be a creature, structure, or even a hero. 
 
            Animations - This class holds the definitions for every single animation available in the game. It
                         will need to be updated by me everytime a new character/animation set is added to the game.
                         When an object is instantiated from this class, it's object type is set. This object
                         type dictates which animations are available for it. Having access to different sprite animations and
                         models is as simple as changing it's ObjectType.
            
            Attributes - Health, Armor, Experience, Level, Damage, or any number of attributes may be stored here. We can then
                         use these attributes to determine winners in battle, power, and calculate other game mechanics. As the game
                         logic progresses, more attributes will be added.
        
              The Idea - Most of the sprite animation functions have been hidden outside of this class. This is because you should
                         not need to mess with those. I have set this up so that when you make a creature, the animation should just
                         work, provided it is instantiated correctly. The only things we should concern ourselves with outside of this class, 
                         are this class's attributes. That said:
 
                                            ** DO NOT MODIFY THIS CLASS WITHOUT PERMISSION**
 
 
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ExternalTypes;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.ComponentModel;
using SpriteGenerator;

// @Joshua


// Superclass for GameObjects Library.
// Shared fields will go here.
namespace GameObjects
{

    // Store all data neccessary to constitute an animation. 
    // Each type of GameObject will have a list of these.

    public abstract class GameObject
    {
        private static float mScale;
        #region Animation Time Fields

        //Projectile intervals
        /*
         *                         Animation ChompLeft = new Animation("Sprites/Units/Necro/NecroSkullChompLeft", 8, 1, 8, 200);
                        Animation ChompRight = new Animation("Sprites/Units/Necro/NecroSkullChompRight", 8, 1, 8, 200);

                        Animation ReturnLeft = new Animation("Sprites/Units/Necro/NecroSkullReturnLeft", 7, 4, 7, 200);
                        Animation ReturnRight = new Animation("Sprites/Units/Necro/NecroSkullReturnRight", 7, 4, 7, 200);

         * 
         */
        private int mNecromancerChompAnimationFrames = 8;
        private int mNecromancerChompAnimationInterval  = 200;
        private int mNecromancerReturnAnimationFrames = 7;
        private int mNecromancerReturnAnimationInterval = 200;



        // Attack Intervals
        private int mHeroAttackAnimationInterval = 200;
        private int mHeroAttackAnimationFrames = 4;

        private int mDragonAttackAnimationInterval = 200;
        private int mDragonAttackAnimationFrames = 10;

        private int mClericAttackAnimationInterval = 200;
        private int mClericAttackAnimationFrames = 3;

        private int mBerserkerAttackAnimationInterval = 200;
        private int mBerserkerAttackAnimationFrames = 1;

        private int mAxeThrowerAttackAnimationInterval = 200;
        private int mAxeThrowerAttackAnimationFrames = 1;

        private int mFireMageAttackAnimationInterval = 200;
        private int mFireMageAttackAnimationFrames = 1;

        private int mNecromancerAttackAnimationInterval = 200;
        private int mNecromancerAttackAnimationFrames = 4;

        private int mWolfAttackAnimationInterval = 200;
        private int mWolfAttackAnimationFrames = 4;

        private int mArcaneMageAttackAnimationInterval = 200;
        private int mArcaneMageAttackAnimationFrames = 4;

        private int mImpAttackAnimationInterval = 200;
        private int mImpAttackAnimationFrames = 4;

        private int mGogAttackAnimationInterval = 100;
        private int mGogAttackAnimationFrames = 3;

        private int mBansheeAttackAnimationInterval = 100;
        private int mBansheeAttackAnimationFrames = 2;

        private int mReaperAttackAnimationInterval = 200;
        private int mReaperAttackAnimationFrames = 6;

        private int mDoomHoundAttackAnimationInterval = 200;
        private int mDoomHoundAttackAnimationFrames = 8;


        // Death Interval
        private bool hasDied = false;
        private int mHeroDeathAnimationInterval = 150;
        private int mHeroDeathAnimationFrames = 9;

        private int mDragonDeathAnimationInterval = 200;
        private int mDragonDeathAnimationFrames = 10;

        private int mClericDeathAnimationInterval = 200;
        private int mClericDeathAnimationFrames = 14;

        private int mBerserkerDeathAnimationInterval = 200;
        private int mBerserkerDeathAnimationFrames = 6;

        private int mAxeThrowerDeathAnimationInterval = 200;
        private int mAxeThrowerDeathAnimationFrames = 3;

        private int mFireMageDeathAnimationInterval = 200;
        private int mFireMageDeathAnimationFrames = 1;

        private int mNecromancerDeathAnimationInterval = 100;
        private int mNecromancerDeathAnimationFrames = 8;

        private int mWolfDeathAnimationInterval = 150;
        private int mWolfDeathAnimationFrames = 4;

        private int mArcaneMageDeathAnimationInterval = 100;
        private int mArcaneMageDeathAnimationFrames = 9;

        private int mImpDeathAnimationInterval = 150;
        private int mImpDeathAnimationFrames = 13;

        private int mGogDeathAnimationInterval = 100;
        private int mGogDeathAnimationFrames = 15;

        private int mBansheeDeathAnimationInterval = 100;
        private int mBansheeDeathAnimationFrames = 5;

        private int mReaperDeathAnimationInterval = 200;
        private int mReaperDeathAnimationFrames = 32;

        private int mDoomHoundDeathAnimationInterval = 150;
        private int mDoomHoundDeathAnimationFrames = 4;

        // War Cry Intervals
        private int mIntimitadeAnimationInterval = 200;
        private int mIntimidateAnimationFrames = 11;
        private int mBattleCryAnimationInterval = 200;
        private int mBattleCryAnimationFrames = 11;




        #endregion

        #region Animation Time Properties

        // Necromancer Projectile Animation Properties

        public int NecromancerChompAnimationFrames
        {
            get { return mNecromancerChompAnimationFrames; }
            set { mNecromancerChompAnimationFrames = value; }
        }

        public int NecromancerChompAnimationInterval
        {
            get { return mNecromancerChompAnimationInterval; }
            set { mNecromancerChompAnimationInterval = value; }
        }

        public int NecromancerReturnAnimationInterval
        {
            get { return mNecromancerReturnAnimationInterval; }
            set { mNecromancerReturnAnimationInterval = value; }
        }

        public int NecromancerReturnAnimationFrames
        {
            get { return mNecromancerReturnAnimationFrames; }
            set { mNecromancerReturnAnimationFrames = value; }
        }

        // War Cry Interval Properties

        public int IntimidateAnimationFrames
        {
            get { return mIntimidateAnimationFrames; }
            set { mIntimidateAnimationFrames = value; }
        }

        public int BattleCryAnimationFrames
        {
            get { return mBattleCryAnimationFrames; }
            set { mBattleCryAnimationFrames = value; }
        }

        public int BattleCryAnimationInterval
        {
            get { return mBattleCryAnimationInterval; }
            set { mBattleCryAnimationInterval = value; }
        }

        public int IntimitadeAnimationInterval
        {
            get { return mIntimitadeAnimationInterval; }
            set { mIntimitadeAnimationInterval = value; }
        }

        // Attack Interval Properties
        public int HeroAttackAnimationInterval
        {
            get { return mHeroAttackAnimationInterval; }
        }

        public int HeroAttackAnimationFrames
        {
            get { return mHeroAttackAnimationFrames; }
        }

        public int ArcaneMageAttackAnimationFrames
        {
            get { return mArcaneMageAttackAnimationFrames; }
            set { mArcaneMageAttackAnimationFrames = value; }
        }

        public int ArcaneMageAttackAnimationInterval
        {
            get { return mArcaneMageAttackAnimationInterval; }
            set { mArcaneMageAttackAnimationInterval = value; }
        }

        public int AxeThrowerAttackAnimationFrames
        {
            get { return mAxeThrowerAttackAnimationFrames; }
            set { mAxeThrowerAttackAnimationFrames = value; }
        }

        public int AxeThrowerAttackAnimationInterval
        {
            get { return mAxeThrowerAttackAnimationInterval; }
            set { mAxeThrowerAttackAnimationInterval = value; }
        }

        public int BansheeAttackAnimationFrames
        {
            get { return mBansheeAttackAnimationFrames; }
            set { mBansheeAttackAnimationFrames = value; }
        }

        public int BansheeAttackAnimationInterval
        {
            get { return mBansheeAttackAnimationInterval; }
            set { mBansheeAttackAnimationInterval = value; }
        }

        public int BerserkerAttackAnimationFrames
        {
            get { return mBerserkerAttackAnimationFrames; }
            set { mBerserkerAttackAnimationFrames = value; }
        }

        public int BerserkerAttackAnimationInterval
        {
            get { return mBerserkerAttackAnimationInterval; }
            set { mBerserkerAttackAnimationInterval = value; }
        }

        public int ClericAttackAnimationFrames
        {
            get { return mClericAttackAnimationFrames; }
            set { mClericAttackAnimationFrames = value; }
        }

        public int ClericAttackAnimationInterval
        {
            get { return mClericAttackAnimationInterval; }
            set { mClericAttackAnimationInterval = value; }
        }

        public int DoomHoundAttackAnimationFrames
        {
            get { return mDoomHoundAttackAnimationFrames; }
            set { mDoomHoundAttackAnimationFrames = value; }
        }

        public int DoomHoundAttackAnimationInterval
        {
            get { return mDoomHoundAttackAnimationInterval; }
            set { mDoomHoundAttackAnimationInterval = value; }
        }

        public int DragonAttackAnimationFrames
        {
            get { return mDragonAttackAnimationFrames; }
            set { mDragonAttackAnimationFrames = value; }
        }

        public int DragonAttackAnimationInterval
        {
            get { return mDragonAttackAnimationInterval; }
            set { mDragonAttackAnimationInterval = value; }
        }

        public int FireMageAttackAnimationFrames
        {
            get { return mFireMageAttackAnimationFrames; }
            set { mFireMageAttackAnimationFrames = value; }
        }

        public int FireMageAttackAnimationInterval
        {
            get { return mFireMageAttackAnimationInterval; }
            set { mFireMageAttackAnimationInterval = value; }
        }

        public int GogAttackAnimationFrames
        {
            get { return mGogAttackAnimationFrames; }
            set { mGogAttackAnimationFrames = value; }
        }

        public int GogAttackAnimationInterval
        {
            get { return mGogAttackAnimationInterval; }
            set { mGogAttackAnimationInterval = value; }
        }

        public int ImpAttackAnimationFrames
        {
            get { return mImpAttackAnimationFrames; }
            set { mImpAttackAnimationFrames = value; }
        }

        public int ImpAttackAnimationInterval
        {
            get { return mImpAttackAnimationInterval; }
            set { mImpAttackAnimationInterval = value; }
        }

        public int NecromancerAttackAnimationFrames
        {
            get { return mNecromancerAttackAnimationFrames; }
            set { mNecromancerAttackAnimationFrames = value; }
        }

        public int NecromancerAttackAnimationInterval
        {
            get { return mNecromancerAttackAnimationInterval; }
            set { mNecromancerAttackAnimationInterval = value; }
        }

        public int ReaperAttackAnimationFrames
        {
            get { return mReaperAttackAnimationFrames; }
            set { mReaperAttackAnimationFrames = value; }
        }

        public int ReaperAttackAnimationInterval
        {
            get { return mReaperAttackAnimationInterval; }
            set { mReaperAttackAnimationInterval = value; }
        }

        public int WolfAttackAnimationInterval
        {
            get { return mWolfAttackAnimationInterval; }
            set { mWolfAttackAnimationInterval = value; }
        }

        public int WolfAttackAnimationFrames
        {
            get { return mWolfAttackAnimationFrames; }
            set { mWolfAttackAnimationFrames = value; }
        }


        // Death Interval Properties
        public bool HasDied
        {
            get { return hasDied; }
            set { hasDied = value; }
        }

        public int HeroDeathAnimationInterval
        {
            get { return mHeroDeathAnimationInterval; }
            set { mHeroDeathAnimationInterval = value; }
        }

        public int HeroDeathAnimationFrames
        {
            get { return mHeroDeathAnimationFrames; }
            set { mHeroDeathAnimationFrames = value; }
        }

        public int DragonDeathAnimationInterval
        {
            get { return mDragonDeathAnimationInterval; }
            set { mDragonDeathAnimationInterval = value; }
        }

        public int ClericDeathAnimationInterval
        {
            get { return mClericDeathAnimationInterval; }
            set { mClericDeathAnimationInterval = value; }
        }

        public int DragonDeathAnimationFrames
        {
            get { return mDragonDeathAnimationFrames; }
            set { mDragonDeathAnimationFrames = value; }
        }

        public int BerserkerDeathAnimationInterval
        {
            get { return mBerserkerDeathAnimationInterval; }
            set { mBerserkerDeathAnimationInterval = value; }
        }

        public int ClericDeathAnimationFrames
        {
            get { return mClericDeathAnimationFrames; }
            set { mClericDeathAnimationFrames = value; }
        }

        public int BerserkerDeathAnimationFrames
        {
            get { return mBerserkerDeathAnimationFrames; }
            set { mBerserkerDeathAnimationFrames = value; }
        }

        public int AxeThrowerDeathAnimationFrames
        {
            get { return mAxeThrowerDeathAnimationFrames; }
            set { mAxeThrowerDeathAnimationFrames = value; }
        }

        public int AxeThrowerDeathAnimationInterval
        {
            get { return mAxeThrowerDeathAnimationInterval; }
            set { mAxeThrowerDeathAnimationInterval = value; }
        }

        public int FireMageDeathAnimationInterval
        {
            get { return mFireMageDeathAnimationInterval; }
            set { mFireMageDeathAnimationInterval = value; }
        }

        public int FireMageDeathAnimationFrames
        {
            get { return mFireMageDeathAnimationFrames; }
            set { mFireMageDeathAnimationFrames = value; }
        }

        public int NecromancerDeathAnimationInterval
        {
            get { return mNecromancerDeathAnimationInterval; }
            set { mNecromancerDeathAnimationInterval = value; }
        }

        public int NecromancerDeathAnimationFrames
        {
            get { return mNecromancerDeathAnimationFrames; }
            set { mNecromancerDeathAnimationFrames = value; }
        }

        public int WolfDeathAnimationInterval
        {
            get { return mWolfDeathAnimationInterval; }
            set { mWolfDeathAnimationInterval = value; }
        }

        public int WolfDeathAnimationFrames
        {
            get { return mWolfDeathAnimationFrames; }
            set { mWolfDeathAnimationFrames = value; }
        }

        public int ArcaneMageDeathAnimationInterval
        {
            get { return mArcaneMageDeathAnimationInterval; }
            set { mArcaneMageDeathAnimationInterval = value; }
        }

        public int ArcaneMageDeathAnimationFrames
        {
            get { return mArcaneMageDeathAnimationFrames; }
            set { mArcaneMageDeathAnimationFrames = value; }
        }

        public int ImpDeathAnimationInterval
        {
            get { return mImpDeathAnimationInterval; }
            set { mImpDeathAnimationInterval = value; }
        }

        public int ImpDeathAnimationFrames
        {
            get { return mImpDeathAnimationFrames; }
            set { mImpDeathAnimationFrames = value; }
        }

        public int GogDeathAnimationInterval
        {
            get { return mGogDeathAnimationInterval; }
            set { mGogDeathAnimationInterval = value; }
        }

        public int GogDeathAnimationFrames
        {
            get { return mGogDeathAnimationFrames; }
            set { mGogDeathAnimationFrames = value; }
        }

        public int BansheeDeathAnimationInterval
        {
            get { return mBansheeDeathAnimationInterval; }
            set { mBansheeDeathAnimationInterval = value; }
        }

        public int BansheeDeathAnimationFrames
        {
            get { return mBansheeDeathAnimationFrames; }
            set { mBansheeDeathAnimationFrames = value; }
        }

        public int ReaperDeathAnimationInterval
        {
            get { return mReaperDeathAnimationInterval; }
            set { mReaperDeathAnimationInterval = value; }
        }

        public int ReaperDeathAnimationFrames
        {
            get { return mReaperDeathAnimationFrames; }
            set { mReaperDeathAnimationFrames = value; }
        }

        public int DoomHoundDeathAnimationFrames
        {
            get { return mDoomHoundDeathAnimationFrames; }
            set { mDoomHoundDeathAnimationFrames = value; }
        }

        public int DoomHoundDeathAnimationInterval
        {
            get { return mDoomHoundDeathAnimationInterval; }
            set { mDoomHoundDeathAnimationInterval = value; }
        }



        #endregion

        public static float Scale
        {
            get
            {
                return mScale;
            }
            set
            {
                mScale = value;
            }
        }

        // Please add fields for every unit. Use the below hero fields as an example:

        #region Animation Fields

        public GameTime gameTime;
        protected Hostility hostility;

        public static FromSaveGame FromSaveGameState;

        protected Vector2 mDirection;


        // Which animation to play.
        protected int mAnimationIndex;

        private ObjectType mObjectType;
        private Race mRace;
        protected Sprite mSprite;
        protected SpriteState mSpriteState;
        protected float mSpriteScale;



        public static JLinkedList mPotentialTargetListList = new JLinkedList();
        public static JLinkedList mInPlayObjectList = new JLinkedList();
        public static JLinkedList mInPlayProjectileList = new JLinkedList();
        public static JLinkedList mInPlayItemDropList;
        protected CombatType mCombatType;
        private List<Animation> mUnitAnimationList;
        private List<Animation> mProjectileAnimationList;
        private Animation mItemDropAnimation;


        public static int mGlobalID = 0;
        public int mObjectID;

        #endregion

        #region Constructor

        public GameObject(ObjectType mObjectType, ContentManager content, SpriteState defaultState, Vector2 SpritePosition, Hostility hostility)
        {


            this.mDirection = Vector2.Zero;

            this.mObjectType = mObjectType;
            this.mRace = mRace;
            this.mSpriteState = defaultState;
            this.mSprite = new Sprite(content);
            this.hostility = hostility;

            mInPlayItemDropList = new JLinkedList();
            mProjectileAnimationList = new List<Animation>();
            mUnitAnimationList = new List<Animation>();
            this.mSprite.WorldPosition = SpritePosition;

            if (this is Hero)
            {
                mPotentialTargetListList.InsertLast(this);
            }

            if (this.hostility == Hostility.CASTLE || this is Hero)
            {
                mInPlayObjectList.InsertLast(this);
            }

            if (this.hostility == Hostility.PROJECTILE)
            {
                mInPlayProjectileList.InsertLast(this);
            }

            // I know what type of game object I am now, so populate
            // my list of available animations.
            PopulateLists();
            // Initialize the default animation.
            if (this.mSprite.AssetName == null && this.hostility != Hostility.PROJECTILE && this.hostility != Hostility.ITEMDROP)
            {
                this.mSprite.AssetName = mUnitAnimationList.ElementAt(0).mAnimationName;
            }
        }





        #endregion

        #region Properties

        public CombatType CombatType
        {
            get { return mCombatType; }
            set { mCombatType = value; }
        }


        public Hostility Hostility
        {
            get { return hostility; }
        }

        public int ObjectID
        {
            get { return mObjectID; }
            set { this.mObjectID = value; }
        }

        public Vector2 Direction
        {
            set { mDirection = value; }
            get { return mDirection; }
        }

        public SpriteState SpriteState
        {
            get { return mSpriteState; }
            set { mSpriteState = value; }
        }


        public Sprite Sprite
        {
            get { return mSprite; }
            set { mSprite = value; }
        }


        public ObjectType CreatureType
        {
            get { return mObjectType; }
            set { mObjectType = value; }
        }


        public Race Race
        {
            get { return mRace; }
        }

        # endregion

        #region Core Functions

        public virtual void Update(GameTime gameTime)
        {

        }


        // Sets all parameters needed to begin a new animation.
        public void SetUnitAnimation()
        {
            this.mSprite.AssetName = mUnitAnimationList.ElementAt(mAnimationIndex).mAnimationName;
            this.mSprite.NumberOfColumns = mUnitAnimationList.ElementAt(mAnimationIndex).mNumberOfCollumns;
            this.mSprite.NumberOfRows = mUnitAnimationList.ElementAt(mAnimationIndex).mNumberOfRows;
            this.mSprite.NumberOfFrames = mUnitAnimationList.ElementAt(mAnimationIndex).mNumberOfFrames;
            this.mSprite.Interval = mUnitAnimationList.ElementAt(mAnimationIndex).mInterval;
        }

        public void SetProjectileAnimation()
        {
            this.mSprite.AssetName = mProjectileAnimationList.ElementAt(mAnimationIndex).mAnimationName;
            this.mSprite.NumberOfColumns = mProjectileAnimationList.ElementAt(mAnimationIndex).mNumberOfCollumns;
            this.mSprite.NumberOfRows = mProjectileAnimationList.ElementAt(mAnimationIndex).mNumberOfRows;
            this.mSprite.NumberOfFrames = mProjectileAnimationList.ElementAt(mAnimationIndex).mNumberOfFrames;
            this.mSprite.Interval = mProjectileAnimationList.ElementAt(mAnimationIndex).mInterval;
        }
        public void SetItemDropAnimation()
        {


            this.mSprite.AssetName = mItemDropAnimation.mAnimationName;
            this.mSprite.NumberOfColumns = mItemDropAnimation.mNumberOfCollumns;
            this.mSprite.NumberOfRows = mItemDropAnimation.mNumberOfRows;
            this.mSprite.NumberOfFrames = mItemDropAnimation.mNumberOfFrames;
            this.mSprite.Interval = mItemDropAnimation.mInterval;


        }

        // Contains definitions of all animations for every character in the game.
        private void PopulateLists()
        {
            // What type of game object am I?
            switch (this.mObjectType)
            {
                case ObjectType.DRAGON:
                    {

                        //I'm a DRAGON, so define my DRAGON animations.
                        Animation MoveLeft = new Animation("Sprites/Units/Dragon/DragonFlyingLeft", 4, 1, 4, 200);
                        Animation MoveRight = new Animation("Sprites/Units/Dragon/DragonFlyingRight", 4, 1, 4, 200);
                        Animation IdleLeft = new Animation("Sprites/Units/Dragon/DragonIdleLeft", 20, 1, 20, 200);
                        Animation IdleRight = new Animation("Sprites/Units/Dragon/DragonIdleRight", 20, 1, 20, 200);
                        Animation AttackLeft = new Animation("Sprites/Units/Dragon/DragonAttackLeft", 10, 1, 10, 200);
                        Animation AttackRight = new Animation("Sprites/Units/Dragon/DragonAttackRight", 10, 1, 10, 200);
                        Animation DeathLeft = new Animation("Sprites/Units/Dragon/DragonDeathLeft", 10, 1, 10, 200);
                        Animation DeathRight = new Animation("Sprites/Units/Dragon/DragonDeathRight", 10, 1, 10, 200);


                        // Populate my list of animations. All game objects will populate animations in a consistant order.
                        // Move will always be at index 0. IDLE_LEFT will always be at index 1, etc.
                        mUnitAnimationList.Add(MoveLeft);
                        mUnitAnimationList.Add(MoveRight);
                        mUnitAnimationList.Add(IdleLeft);
                        mUnitAnimationList.Add(IdleRight);
                        mUnitAnimationList.Add(AttackLeft);
                        mUnitAnimationList.Add(AttackRight);
                        mUnitAnimationList.Add(DeathLeft);
                        mUnitAnimationList.Add(DeathRight);

                        this.mCombatType = CombatType.MELEE;
                    }
                    break;
                case ObjectType.CLERIC:
                    {

                        //I'm a CLERIC, so define my CLERIC animations.
                        Animation MoveLeft = new Animation("Sprites/Units/Cleric/ClericWalkingLeft", 4, 1, 4, 200);
                        Animation MoveRight = new Animation("Sprites/Units/Cleric/ClericWalkingRight", 4, 1, 4, 200);
                        Animation IdleLeft = new Animation("Sprites/Units/Cleric/ClericIdleLeft", 1, 1, 1, 200);
                        Animation IdleRight = new Animation("Sprites/Units/Cleric/ClericIdleRight", 1, 1, 1, 200);
                        Animation AttackLeft = new Animation("Sprites/Units/Cleric/ClericHealLeft", 3, 1, 3, 200);
                        Animation AttackRight = new Animation("Sprites/Units/Cleric/ClericHealRight", 3, 1, 3, 200);
                        Animation DeathLeft = new Animation("Sprites/Units/Cleric/ClericDeathLeft", 15, 1, 15, 200);
                        Animation DeathRight = new Animation("Sprites/Units/Cleric/ClericDeathRight", 15, 1, 15, 200);



                        // Populate my list of animations. All game objects will populate animations in a consistant order.
                        // Move will always be at index 0. IDLE_LEFT will always be at index 1, etc.
                        mUnitAnimationList.Add(MoveLeft);
                        mUnitAnimationList.Add(MoveRight);
                        mUnitAnimationList.Add(IdleLeft);
                        mUnitAnimationList.Add(IdleRight);
                        mUnitAnimationList.Add(AttackLeft);
                        mUnitAnimationList.Add(AttackRight);
                        mUnitAnimationList.Add(DeathLeft);
                        mUnitAnimationList.Add(DeathRight);

                        mCombatType = CombatType.HEALER;

                    }
                    break;

                case ObjectType.BERSERKER:
                    {

                        //I'm a BERSERKER, so define my BERSERKER animations.
                        Animation MoveLeft = new Animation("Sprites/Units/Berzerker/BerzerkerWalkingLeft", 7, 1, 7, 200);
                        Animation MoveRight = new Animation("Sprites/Units/Berzerker/BerzerkerWalkingRight", 7, 1, 7, 200);
                        Animation IdleLeft = new Animation("Sprites/Units/Berzerker/BerzerkerIdleLeft", 6, 1, 6, 200);
                        Animation IdleRight = new Animation("Sprites/Units/Berzerker/BerzerkerIdleRight", 6, 1, 6, 200);
                        Animation AttackLeft = new Animation("Sprites/Units/Berzerker/BerzerkerAttackLeft", 5, 1, 5, 200);
                        Animation AttackRight = new Animation("Sprites/Units/Berzerker/BerzerkerAttackRight", 5, 1, 5, 200);
                        Animation DeathLeft = new Animation("Sprites/Units/Berzerker/BerzerkerDyingLeft", 6, 1, 6, 200);
                        Animation DeathRight = new Animation("Sprites/Units/Berzerker/BerzerkerDyingRight", 6, 1, 6, 200);


                        // Populate my list of animations. All game objects will populate animations in a consistant order.
                        // Move will always be at index 0. IDLE_LEFT will always be at index 1, etc.
                        mUnitAnimationList.Add(MoveLeft);
                        mUnitAnimationList.Add(MoveRight);
                        mUnitAnimationList.Add(IdleLeft);
                        mUnitAnimationList.Add(IdleRight);
                        mUnitAnimationList.Add(AttackLeft);
                        mUnitAnimationList.Add(AttackRight);
                        mUnitAnimationList.Add(DeathLeft);
                        mUnitAnimationList.Add(DeathRight);

                        mCombatType = CombatType.MELEE;
                    }
                    break;
                // THIS IS AXE THROWER
                case ObjectType.AXE_THROWER:
                    {

                        //I'm a AXE_THROWER, so define my AXE_THROWER animations.
                        Animation MoveLeft = new Animation("Sprites/Units/AxeThrower/AxeThrowerWalkingLeft", 7, 1, 7, 200);
                        Animation MoveRight = new Animation("Sprites/Units/AxeThrower/AxeThrowerWalkingRight", 7, 1, 7, 200);
                        Animation IdleLeft = new Animation("Sprites/Units/AxeThrower/AxeThrowerIdleLeft", 3, 1, 3, 200);
                        Animation IdleRight = new Animation("Sprites/Units/AxeThrower/AxeThrowerIdleRight", 3, 1, 3, 200);
                        Animation AttackLeft = new Animation("Sprites/Units/AxeThrower/AxeThrowerAttackLeft", 6, 1, 6, 200);
                        Animation AttackRight = new Animation("Sprites/Units/AxeThrower/AxeThrowerAttackRight", 6, 1, 6, 200);
                        Animation DeathLeft = new Animation("Sprites/Units/AxeThrower/AxeThrowerDyingLeft", 3, 1, 3, 200);
                        Animation DeathRight = new Animation("Sprites/Units/AxeThrower/AxeThrowerDyingRight", 3, 1, 3, 200);



                        // Populate my list of animations. All game objects will populate animations in a consistant order.
                        // Move will always be at index 0. IDLE_LEFT will always be at index 1, etc.
                        mUnitAnimationList.Add(MoveLeft);
                        mUnitAnimationList.Add(MoveRight);
                        mUnitAnimationList.Add(IdleLeft);
                        mUnitAnimationList.Add(IdleRight);
                        mUnitAnimationList.Add(AttackLeft);
                        mUnitAnimationList.Add(AttackRight);
                        mUnitAnimationList.Add(DeathLeft);
                        mUnitAnimationList.Add(DeathRight);


                        mCombatType = CombatType.RANGED;

                    }
                    break;
                case ObjectType.FIRE_MAGE:
                    {

                        //I'm a FIRE_MAGE, so define my FIRE_MAGE animations.
                        Animation MoveLeft = new Animation("Sprites/Units/FireMage/FireWalkingLeft", 14, 1, 14, 200);
                        Animation MoveRight = new Animation("Sprites/Units/FireMage/FireWalkingRight", 14, 1, 14, 200);
                        Animation IdleLeft = new Animation("Sprites/Units/FireMage/FireIdleLeft", 15, 1, 15, 200);
                        Animation IdleRight = new Animation("Sprites/Units/FireMage/FireIdleRight", 15, 1, 15, 200);
                        Animation AttackLeft = new Animation("Sprites/Units/FireMage/FireAttackLeft", 6, 1, 6, 200);
                        Animation AttackRight = new Animation("Sprites/Units/FireMage/FireAttackRight", 6, 1, 6, 200);
                        Animation DeathLeft = new Animation("Sprites/Units/FireMage/FireDyingLeft", 1, 1, 1, 200);
                        Animation DeathRight = new Animation("Sprites/Units/FireMage/FireDyingRight", 1, 1, 1, 200);


                        // Populate my list of animations. All game objects will populate animations in a consistant order.
                        // Move will always be at index 0. IDLE_LEFT will always be at index 1, etc.
                        mUnitAnimationList.Add(MoveLeft);
                        mUnitAnimationList.Add(MoveRight);
                        mUnitAnimationList.Add(IdleLeft);
                        mUnitAnimationList.Add(IdleRight);
                        mUnitAnimationList.Add(AttackLeft);
                        mUnitAnimationList.Add(AttackRight);
                        mUnitAnimationList.Add(DeathLeft);
                        mUnitAnimationList.Add(DeathRight);

                        mCombatType = CombatType.RANGED;

                    }
                    break;
                case ObjectType.NECROMANCER:
                    {

                        //I'm a NECROMANCER, so define my NECROMANCER animations.
                        Animation MoveLeft = new Animation("Sprites/Units/Necro/NecroWalkingLeft", 6, 1, 6, 200);
                        Animation MoveRight = new Animation("Sprites/Units/Necro/NecroWalkingRight", 6, 1, 6, 200);
                        Animation IdleLeft = new Animation("Sprites/Units/Necro/NecroIdleLeft", 1, 1, 1, 200);
                        Animation IdleRight = new Animation("Sprites/Units/Necro/NecroIdleRight", 1, 1, 1, 200);
                        Animation AttackLeft = new Animation("Sprites/Units/Necro/NecroAttackingLeft", 4, 1, 4, 200);
                        Animation AttackRight = new Animation("Sprites/Units/Necro/NecroAttackingRight", 4, 1, 4, 200);
                        Animation DeathLeft = new Animation("Sprites/Units/Necro/NecroDyingLeft", 8, 1, 8, 100);
                        Animation DeathRight = new Animation("Sprites/Units/Necro/NecroDyingRight", 8, 1, 8, 100);


                        // Populate my list of animations. All game objects will populate animations in a consistant order.
                        // Move will always be at index 0. IDLE_LEFT will always be at index 1, etc.
                        mUnitAnimationList.Add(MoveLeft);
                        mUnitAnimationList.Add(MoveRight);
                        mUnitAnimationList.Add(IdleLeft);
                        mUnitAnimationList.Add(IdleRight);
                        mUnitAnimationList.Add(AttackLeft);
                        mUnitAnimationList.Add(AttackRight);
                        mUnitAnimationList.Add(DeathLeft);
                        mUnitAnimationList.Add(DeathRight);


                        mCombatType = CombatType.RANGED;
                    }
                    break;

                case ObjectType.WOLF:
                    {

                        //I'm a WOLF, so define my WOLF animations.
                        Animation MoveLeft = new Animation("Sprites/Units/Wolf/WolfWalkingLeft", 8, 1, 8, 200);
                        Animation MoveRight = new Animation("Sprites/Units/Wolf/WolfWalkingRight", 8, 1, 8, 200);
                        Animation IdleLeft = new Animation("Sprites/Units/Wolf/WolfIdleLeft", 14, 1, 14, 200);
                        Animation IdleRight = new Animation("Sprites/Units/Wolf/WolfIdleRight", 14, 1, 14, 200);
                        Animation AttackLeft = new Animation("Sprites/Units/Wolf/WolfAttackLeft", 4, 1, 4, 200);
                        Animation AttackRight = new Animation("Sprites/Units/Wolf/WolfAttackRight", 4, 1, 4, 200);
                        Animation DeathLeft = new Animation("Sprites/Units/Wolf/WolfDyingLeft", 4, 1, 4, 200);
                        Animation DeathRight = new Animation("Sprites/Units/Wolf/WolfDyingRight", 4, 1, 4, 200);


                        // Populate my list of animations. All game objects will populate animations in a consistant order.
                        // Move will always be at index 0. IDLE_LEFT will always be at index 1, etc.
                        mUnitAnimationList.Add(MoveLeft);
                        mUnitAnimationList.Add(MoveRight);
                        mUnitAnimationList.Add(IdleLeft);
                        mUnitAnimationList.Add(IdleRight);
                        mUnitAnimationList.Add(AttackLeft);
                        mUnitAnimationList.Add(AttackRight);
                        mUnitAnimationList.Add(DeathLeft);
                        mUnitAnimationList.Add(DeathRight);

                        mCombatType = CombatType.MELEE;
                    }
                    break;
                case ObjectType.ARCANE_MAGE:
                    {

                        //I'm a ARCANE_MAGE, so define my ARCANE_MAGE animations.
                        Animation MoveLeft = new Animation("Sprites/Units/ArcaneMage/ArcaneWalkingLeft", 6, 1, 6, 200);
                        Animation MoveRight = new Animation("Sprites/Units/ArcaneMage/ArcaneWalkingRight", 6, 1, 6, 200);
                        Animation IdleLeft = new Animation("Sprites/Units/ArcaneMage/ArcaneIdleLeft", 1, 1, 1, 200);
                        Animation IdleRight = new Animation("Sprites/Units/ArcaneMage/ArcaneIdleRight", 1, 1, 1, 200);
                        Animation AttackLeft = new Animation("Sprites/Units/ArcaneMage/ArcaneAttackLeft", 4, 1, 4, 200);
                        Animation AttackRight = new Animation("Sprites/Units/ArcaneMage/ArcaneAttackRight", 4, 1, 4, 200);
                        Animation DeathLeft = new Animation("Sprites/Units/ArcaneMage/ArcaneMageDeathLeft", 9, 1, 9, 200);
                        Animation DeathRight = new Animation("Sprites/Units/ArcaneMage/ArcaneMageDeathRight", 9, 1, 9, 200);


                        // Populate my list of animations. All game objects will populate animations in a consistant order.
                        // Move will always be at index 0. IDLE_LEFT will always be at index 1, etc.
                        mUnitAnimationList.Add(MoveLeft);
                        mUnitAnimationList.Add(MoveRight);
                        mUnitAnimationList.Add(IdleLeft);
                        mUnitAnimationList.Add(IdleRight);
                        mUnitAnimationList.Add(AttackLeft);
                        mUnitAnimationList.Add(AttackRight);
                        mUnitAnimationList.Add(DeathLeft);
                        mUnitAnimationList.Add(DeathRight);


                        mCombatType = CombatType.RANGED;
                    }
                    break;

                //TODO: THIS IS IMP
                case ObjectType.IMP:
                    {

                        //I'm a IMP, so define my IMP animations.
                        Animation MoveLeft = new Animation("Sprites/Units/Imp/ImpWalkingLeft", 4, 1, 4, 200);
                        Animation MoveRight = new Animation("Sprites/Units/Imp/ImpWalkingRight", 4, 1, 4, 200);
                        Animation AttackLeft = new Animation("Sprites/Units/Imp/ImpAttackingLeft", 4, 1, 4, 200);
                        Animation AttackRight = new Animation("Sprites/Units/Imp/ImpAttackingRight", 4, 1, 4, 200);
                        Animation DeathLeft = new Animation("Sprites/Units/Imp/ImpDyingLeft", 14, 1, 14, 150);
                        Animation DeathRight = new Animation("Sprites/Units/Imp/ImpDyingRight", 14, 1, 14, 150);
                        Animation IdleLeft = new Animation("Sprites/Units/Imp/ImpIdleLeft", 1, 1, 1, 200);
                        Animation IdleRight = new Animation("Sprites/Units/Imp/ImpIdleRight", 1, 1, 1, 200);

                        // Populate my list of animations. All game objects will populate animations in a consistant order.
                        // Move will always be at index 0. IDLE_LEFT will always be at index 1, etc.
                        mUnitAnimationList.Add(MoveLeft);
                        mUnitAnimationList.Add(MoveRight);
                        mUnitAnimationList.Add(IdleLeft);
                        mUnitAnimationList.Add(IdleRight);
                        mUnitAnimationList.Add(AttackLeft);
                        mUnitAnimationList.Add(AttackRight);
                        mUnitAnimationList.Add(DeathLeft);
                        mUnitAnimationList.Add(DeathRight);

                        mCombatType = CombatType.MELEE;
                    }
                    break;
                case ObjectType.GOG:
                    {

                        //I'm a GOG, so define my GOG animations.
                        Animation MoveLeft = new Animation("Sprites/Units/Gog/GogWalkingLeft", 5, 1, 5, 200);
                        Animation MoveRight = new Animation("Sprites/Units/Gog/GogWalkingRight", 5, 1, 5, 200);
                        Animation AttackLeft = new Animation("Sprites/Units/Gog/GogAttackingLeft", 3, 1, 3, 200);
                        Animation AttackRight = new Animation("Sprites/Units/Gog/GogAttackingRight", 3, 1, 3, 200);
                        Animation DeathLeft = new Animation("Sprites/Units/Gog/GogDyingLeft", 15, 1, 15, 200);
                        Animation DeathRight = new Animation("Sprites/Units/Gog/GogDyingRight", 15, 1, 15, 200);
                        Animation IdleLeft = new Animation("Sprites/Units/Gog/GogWalkingLeft", 0, 1, 1, 200);
                        Animation IdleRight = new Animation("Sprites/Units/Gog/GogWalkingRight", 0, 1, 1, 200);

                        // Populate my list of animations. All game objects will populate animations in a consistant order.
                        // Move will always be at index 0. IDLE_LEFT will always be at index 1, etc.
                        mUnitAnimationList.Add(MoveLeft);
                        mUnitAnimationList.Add(MoveRight);
                        mUnitAnimationList.Add(IdleLeft);
                        mUnitAnimationList.Add(IdleRight);
                        mUnitAnimationList.Add(AttackLeft);
                        mUnitAnimationList.Add(AttackRight);
                        mUnitAnimationList.Add(DeathLeft);
                        mUnitAnimationList.Add(DeathRight);



                        mCombatType = CombatType.RANGED;

                    }
                    break;

                // THIS IS BANSHEE
                case ObjectType.BANSHEE:
                    {

                        //I'm a BANSHEE, so define my BANSHEE animations.
                        Animation MoveLeft = new Animation("Sprites/Units/Banshee/BansheeWalkingLeft", 6, 1, 6, 200);
                        Animation MoveRight = new Animation("Sprites/Units/Banshee/BansheeWalkingRight", 6, 1, 6, 200);
                        Animation AttackLeft = new Animation("Sprites/Units/Banshee/BansheeAttackingLeft", 2, 1, 2, 100);
                        Animation AttackRight = new Animation("Sprites/Units/Banshee/BansheeAttackingLeft", 2, 1, 2, 100);
                        Animation DeathLeft = new Animation("Sprites/Units/Banshee/BansheeDeathLeft", 5, 1, 5, 200);
                        Animation DeathRight = new Animation("Sprites/Units/Banshee/BansheeDeathRight", 5, 1, 5, 200);
                        Animation IdleLeft = new Animation("Sprites/Units/Banshee/BansheeAttackingLeft", 0, 1, 1, 200);
                        Animation IdleRight = new Animation("Sprites/Units/Banshee/BansheeAttackingRight", 0, 1, 1, 200);



                        // Populate my list of animations. All game objects will populate animations in a consistant order.
                        // Move will always be at index 0. IDLE_LEFT will always be at index 1, etc.
                        mUnitAnimationList.Add(MoveLeft);
                        mUnitAnimationList.Add(MoveRight);
                        mUnitAnimationList.Add(IdleLeft);
                        mUnitAnimationList.Add(IdleRight);
                        mUnitAnimationList.Add(AttackLeft);
                        mUnitAnimationList.Add(AttackRight);
                        mUnitAnimationList.Add(DeathLeft);
                        mUnitAnimationList.Add(DeathRight);


                        mCombatType = CombatType.RANGED;
                    }
                    break;
                // TODO: THIS IS REAPER
                case ObjectType.REAPER:
                    {

                        //I'm a REAPER, so define my REAPER animations.
                        Animation MoveLeft = new Animation("Sprites/Units/Reaper/ReaperWalkingLeft", 9, 1, 9, 200);
                        Animation MoveRight = new Animation("Sprites/Units/Reaper/ReaperWalkingRight", 9, 1, 9, 200);
                        Animation AttackLeft = new Animation("Sprites/Units/Reaper/ReaperAttackingLeft", 6, 1, 6, 200);
                        Animation AttackRight = new Animation("Sprites/Units/Reaper/ReaperAttackingRight", 6, 1, 6, 200);
                        Animation DeathLeft = new Animation("Sprites/Units/Reaper/ReaperDyingLeft", 16, 2, 16, 200);
                        Animation DeathRight = new Animation("Sprites/Units/Reaper/ReaperDyingRight", 16, 2, 16, 200);
                        Animation IdleLeft = new Animation("Sprites/Units/Reaper/ReaperWalkingLeft", 9, 1, 9, 200);
                        Animation IdleRight = new Animation("Sprites/Units/Reaper/ReaperWalkingRight", 9, 1, 9, 200);

                        // Populate my list of animations. All game objects will populate animations in a consistant order.
                        // Move will always be at index 0. IDLE_LEFT will always be at index 1, etc.
                        mUnitAnimationList.Add(MoveLeft);
                        mUnitAnimationList.Add(MoveRight);
                        mUnitAnimationList.Add(IdleLeft);
                        mUnitAnimationList.Add(IdleRight);
                        mUnitAnimationList.Add(AttackLeft);
                        mUnitAnimationList.Add(AttackRight);
                        mUnitAnimationList.Add(DeathLeft);
                        mUnitAnimationList.Add(DeathRight);
                        mCombatType = CombatType.MELEE;
                    }
                    break;
                case ObjectType.DOOM_HOUND:
                    {

                        //I'm a DOOM_HOUND, so define my DOOM_HOUND animations.
                        Animation MoveLeft = new Animation("Sprites/Units/DoomHound/DoomHoundWalkLeft", 10, 1, 10, 200);
                        Animation MoveRight = new Animation("Sprites/Units/DoomHound/DoomHoundWalkRight", 10, 1, 10, 200);
                        Animation AttackLeft = new Animation("Sprites/Units/DoomHound/DoomHoundAttackLeft", 8, 1, 8, 200);
                        Animation AttackRight = new Animation("Sprites/Units/DoomHound/DoomHoundAttackRight", 8, 1, 8, 200);
                        Animation DeathLeft = new Animation("Sprites/Units/DoomHound/DoomHoundDeathLeft", 6, 1, 6, 150);
                        Animation DeathRight = new Animation("Sprites/Units/DoomHound/DoomHoundDeathRight", 6, 1, 6, 150);
                        Animation IdleLeft = new Animation("Sprites/Units/DoomHound/DoomHoundWalkLeft", 10, 1, 10, 200);
                        Animation IdleRight = new Animation("Sprites/Units/DoomHound/DoomHoundWalkRight", 10, 1, 10, 200);

                        // Populate my list of animations. All game objects will populate animations in a consistant order.
                        // Move will always be at index 0. IDLE_LEFT will always be at index 1, etc.
                        mUnitAnimationList.Add(MoveLeft);
                        mUnitAnimationList.Add(MoveRight);
                        mUnitAnimationList.Add(IdleLeft);
                        mUnitAnimationList.Add(IdleRight);
                        mUnitAnimationList.Add(AttackLeft);
                        mUnitAnimationList.Add(AttackRight);
                        mUnitAnimationList.Add(DeathLeft);
                        mUnitAnimationList.Add(DeathRight);


                        mCombatType = CombatType.MELEE;
                    }
                    break;
                case ObjectType.HERO:
                    {

                        //I'm a HERO, so define my HERO animations.
                        Animation MoveLeft = new Animation("Sprites/Hero/HeroWalkingLeft", 4, 1, 4, 200);
                        Animation MoveRight = new Animation("Sprites/Hero/HeroWalkingRight", 4, 1, 4, 200);
                        Animation IdleLeft = new Animation("Sprites/Hero/HeroIdleLeft", 12, 1, 12, 200);
                        Animation IdleRight = new Animation("Sprites/Hero/HeroIdleRight", 12, 1, 12, 200);
                        Animation AttackLeft = new Animation("Sprites/Hero/HeroAttackingLeft", 4, 1, 4, 200);
                        Animation AttackRight = new Animation("Sprites/Hero/HeroAttackingRight", 4, 1, 4, 200);
                        Animation DeathLeft = new Animation("Sprites/Hero/HeroDyingLeft", 10, 1, 10, 150);
                        Animation DeathRight = new Animation("Sprites/Hero/HeroDyingRight", 10, 1, 10, 150);
                        Animation CastBattleCryLeft = new Animation("Sprites/Hero/HeroCastBattleCryLeft", 11, 0, 11, 200);
                        Animation CastBattleCryRight = new Animation("Sprites/Hero/HeroCastBattleCryRight", 11, 0, 11, 200);
                        Animation CastIntimidateLeft = new Animation("Sprites/Hero/HeroCastIntimidateLeft", 11, 0, 11, 200);
                        Animation CastIntimidateRight = new Animation("Sprites/Hero/HeroCastIntimidateRight", 11, 0, 11, 200);


                        // Populate my list of animations. All game objects will populate animations in a consistant order.
                        // Move will always be at index 0. IDLE_LEFT will always be at index 1, etc.
                        mUnitAnimationList.Add(MoveLeft);
                        mUnitAnimationList.Add(MoveRight);
                        mUnitAnimationList.Add(IdleLeft);
                        mUnitAnimationList.Add(IdleRight);
                        mUnitAnimationList.Add(AttackLeft);
                        mUnitAnimationList.Add(AttackRight);
                        mUnitAnimationList.Add(DeathLeft);
                        mUnitAnimationList.Add(DeathRight);
                        mUnitAnimationList.Add(CastBattleCryLeft);
                        mUnitAnimationList.Add(CastBattleCryRight);
                        mUnitAnimationList.Add(CastIntimidateLeft);
                        mUnitAnimationList.Add(CastIntimidateRight);

                        mCombatType = CombatType.MELEE;
                    }
                    break;
                case ObjectType.ORC_CASTLE:
                    {
                        Animation Castle = new Animation("Sprites/Structures/OrcCastle2", 1, 1, 1, 100000);
                        // Animation Castle = new Animation("Sprites/Castle/NecromancerStructure", 10, 1, 10, 500);
                        mUnitAnimationList.Add(Castle);

                    }
                    break;
                case ObjectType.DRAGON_CAVE:
                    {
                        Animation Structure = new Animation("Sprites/Structures/DragonCave", 9, 1, 9, 500);

                        mUnitAnimationList.Add(Structure);

                    }
                    break;
                case ObjectType.ABBEY:
                    {
                        Animation Structure = new Animation("Sprites/Structures/Abbey", 20, 1, 20, 200);

                        mUnitAnimationList.Add(Structure);

                    }
                    break;
                case ObjectType.BONEPIT:
                    {
                        Animation Structure = new Animation("Sprites/Structures/BonePit", 10, 1, 10, 200);

                        mUnitAnimationList.Add(Structure);

                    }
                    break;
                case ObjectType.FIRE_TEMPLE:
                    {
                        Animation Structure = new Animation("Sprites/Structures/FireTemple", 9, 1, 9, 200);

                        mUnitAnimationList.Add(Structure);

                    }
                    break;
                case ObjectType.WOLFPEN:
                    {
                        Animation Structure = new Animation("Sprites/Structures/WolfPen", 1, 1, 1, 100000);

                        mUnitAnimationList.Add(Structure);

                    }
                    break;
                case ObjectType.LIBRARY:
                    {
                        Animation Structure = new Animation("Sprites/Structures/Library", 21, 1, 21, 100000);

                        mUnitAnimationList.Add(Structure);

                    }
                    break;
                case ObjectType.ARMORY:
                    {
                        Animation Structure = new Animation("Sprites/Structures/Armory", 1, 1, 1, 100000);

                        mUnitAnimationList.Add(Structure);

                    }
                    break;
                case ObjectType.BARRACKS:
                    {
                        Animation Structure = new Animation("Sprites/Structures/Barracks", 1, 1, 1, 100000);

                        mUnitAnimationList.Add(Structure);

                    }
                    break;
                case ObjectType.FIRE_MAGE_PROJECTILE:
                    {

                        //Give me my axe I can throw at my foes!
                        Animation CastLeft = new Animation("Sprites/Units/FireMage/FireballLeft", 5, 1, 5, 200);
                        Animation CastRight = new Animation("Sprites/Units/FireMage/FireballRight", 5, 1, 5, 200);

                        mProjectileAnimationList.Add(CastLeft);
                        mProjectileAnimationList.Add(CastRight);
                    }
                    break;
                case ObjectType.GOG_PROJECTILE:
                    {

                        //Give me my axe I can throw at my foes!
                        Animation CastLeft = new Animation("Sprites/Units/Gog/FireballLeft", 5, 1, 5, 100);
                        Animation CastRight = new Animation("Sprites/Units/Gog/FireballRight", 5, 1, 5, 100);

                        mProjectileAnimationList.Add(CastLeft);
                        mProjectileAnimationList.Add(CastRight);
                    }
                    break;
                case ObjectType.NECROMANCER_PROJECTILE:
                    {

                        //Give me my axe I can throw at my foes!
                        Animation CastLeft = new Animation("Sprites/Units/Necro/SkullChompLeft", 8, 1, 8, 200);
                        Animation CastRight = new Animation("Sprites/Units/Necro/SkullChompRight", 8, 1, 8, 200);

                        mProjectileAnimationList.Add(CastLeft);
                        mProjectileAnimationList.Add(CastRight);
                    }
                    break;
                //TODO: THIS IS A BANSHEE PROJECTILE
                case ObjectType.BANSHEE_PROJECTILE:
                    {
                        //Give me my axe I can throw at my foes!
                        Animation CastLeft = new Animation("Sprites/Units/Banshee/SkullLeft", 4, 1, 4, 150);
                        Animation CastRight = new Animation("Sprites/Units/Banshee/SkullRight", 4, 1, 4, 150);
                        mProjectileAnimationList.Add(CastLeft);
                        mProjectileAnimationList.Add(CastRight);
                    }
                    break;
                case ObjectType.ARCANE_MAGE_PROJECTILE:
                    {

                        //Give me my axe I can throw at my foes!
                        Animation CastLeft = new Animation("Sprites/Units/ArcaneMage/ArcaneOrbLeft", 5, 1, 5, 200);
                        Animation CastRight = new Animation("Sprites/Units/ArcaneMage/ArcaneOrbRight", 5, 1, 5, 200);
                        mProjectileAnimationList.Add(CastLeft);
                        mProjectileAnimationList.Add(CastRight);
                    }
                    break;
                // THIS IS AN AXE THROWER PROJECTILE
                case ObjectType.AXE_THROWER_PROJECTILE:
                    {

                        //Give me my axe I can throw at my foes!
                        Animation CastLeft = new Animation("Sprites/Units/AxeThrower/AxeThrowerSpellLeft", 12, 1, 12, 100);
                        Animation CastRight = new Animation("Sprites/Units/AxeThrower/AxeThrowerSpellRight", 12, 1, 12, 100);

                        mProjectileAnimationList.Add(CastLeft);
                        mProjectileAnimationList.Add(CastRight);
                    }
                    break;
                case ObjectType.GOLD:
                    //TODO:
                    Animation GoldAnimation = new Animation("Sprites/ItemDrops/COIN", 59, 1, 59, 50);

                    mItemDropAnimation = GoldAnimation;

                    break;
                case ObjectType.HEALTH:
                    Animation HealthDropAnimation = new Animation("Sprites/ItemDrops/HealthPickupAnimation", 15, 1, 15, 200);

                    mItemDropAnimation = HealthDropAnimation;

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
                    break;
            }
        }

        #endregion

        #region Combat Functions



        #endregion


    }
}
