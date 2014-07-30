/*
     This class will handle any sprite created in the GameObject class. It contains
     functions necessary to draw, animate, and update the worldPosition of a sprite.
     Sprites will only be instantiated as a member of a GameObject. 
 
     This class is designed to handle setting, playing, and updating animations automatically
     based on a Game Object's creature type. It is set up and configured to work directly with 
     the Game Object class.
 
 
                     *******DO NOT MODIFY THIS CLASS WITHOUT PERMISSION***********
 */





using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

// @Josh

//Sprite superclass
namespace SpriteGenerator
{
    public class Sprite
    {
        #region Fields

        private ContentManager Content;

        // Sprite Sheet Fields
        private Texture2D mSpriteSheet;
        private string mAssetName;

        // Sprite Frame Fields
        private Rectangle mSpriteFrame;

        private Vector2 mWorldPosition;
        private Vector2 mFrameOrigin;
        private Rectangle mAnimationFrame;
        private int mSpriteFrameWidth;
        private int mSpriteFrameHeight;
        private int mNumberOfColumns;
        private int mNumberOfRows;
        private int mCurrentFrame;
        private int mNumberOfFrames;
        private float mTimer;
        private float mInterval;
        private float mSpriteScale;

        #endregion

        #region Constructors

        public Sprite(ContentManager Content)
        {

            this.Content = Content;

            mTimer = 0.0f;
            mCurrentFrame = 0;
            mInterval = 100;
            mNumberOfColumns = 1;
            mNumberOfRows = 1;
            mSpriteScale = 1.0f;
        }

        #endregion

        #region Accessor and Mutator Functions

        public Texture2D SpriteSheet
        {
            get { return mSpriteSheet; }
        }

        public float SpriteScale
        {
            set { mSpriteScale = value; }
        }


        public string AssetName
        {
            set { mAssetName = value; }
            get { return mAssetName; }
        }


        public int NumberOfColumns
        {
            set { mNumberOfColumns = value; }
        }


        public int NumberOfRows
        {
            set { mNumberOfRows = value; }
        }


        public int NumberOfFrames
        {
            set { mNumberOfFrames = value; }
        }

        public int Interval
        {
            set { mInterval = value; }
        }


        public Vector2 WorldPosition
        {
            set
            {
                mWorldPosition = value;
                mSpriteFrame.X = (int)value.X;
                mSpriteFrame.Y = (int)value.Y;
            }
            get { return mWorldPosition; }
        }

        public Rectangle SpriteFrame
        {
            get
            {
                mSpriteFrame.Width = mSpriteFrameWidth;
                mSpriteFrame.Height = mSpriteFrameHeight;
                mSpriteFrame.X = (int)mWorldPosition.X - mSpriteFrameWidth / 2;
                mSpriteFrame.Y = (int)mWorldPosition.Y - mSpriteFrameHeight / 2;

                return mSpriteFrame;
            }
        }


        public Vector2 GetSpriteOrigin()
        {
            return mFrameOrigin;
        }


        public void SetOriginOfSpriteRectangle(Rectangle spriteFrame)
        {
            mFrameOrigin = new Vector2((float)spriteFrame.Width / 2, (float)spriteFrame.Height / 2);

        }

        #endregion

        #region Core Functions

        public void PlayAnimation(GameTime gameTime)
        {

            // Increase the mTimer by the number of milliseconds since update was last called.
            mTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            // Check if the mTimer is more than the chosen interval.
            if (mTimer > mInterval)
            {
                // Show the next frame.
                mCurrentFrame++;
                // Reset the Timer.
                mTimer = 0f;
            }

            // If we are on last frame.
            if (mCurrentFrame >= mNumberOfFrames)
            {
                // Reset animation.
                mCurrentFrame = 0;
            }

            // Update Sprite Frame.
            mAnimationFrame = new Rectangle(mCurrentFrame * mSpriteFrameWidth, 0, mSpriteFrameWidth, mSpriteFrameHeight);
            mFrameOrigin = new Vector2((float)mAnimationFrame.Width / 2, (float)mAnimationFrame.Height / 2);
        }


        public void Update(GameTime gameTime)
        {
            this.PlayAnimation(gameTime);
        }


        public void LoadContent()
        {
            mSpriteSheet = Content.Load<Texture2D>(mAssetName);
            mSpriteFrameWidth = mSpriteSheet.Width / mNumberOfColumns;
            mSpriteFrameHeight = mSpriteSheet.Height / mNumberOfRows;
        }

        public void SetSpriteFrame(int xPosition, int yPosition)
        {
            mSpriteFrame = new Rectangle(xPosition, yPosition, mSpriteFrameWidth, mSpriteFrameHeight);
        }

        public Vector2 GetCenterOfObject()
        {
            return new Vector2(this.mSpriteFrameWidth / 2 + mWorldPosition.X, this.mSpriteFrameHeight / 2 + mWorldPosition.Y);
        }

        public void Draw(SpriteBatch spriteBatch, float scale)
        {
            spriteBatch.Draw(mSpriteSheet, mWorldPosition, mAnimationFrame, Color.White, 0f, mFrameOrigin, scale, SpriteEffects.None, 0);

        }


        #endregion

        #region Utility Functions

        public void DisplaySpriteAttributes()
        {
            Console.WriteLine("Asset Name: " + this.mAssetName);
            Console.WriteLine("NumberOfCollumns: " + this.mNumberOfColumns);
            Console.WriteLine("NumberOfRows: " + this.mNumberOfRows);
            Console.WriteLine("NumberOfFrames: " + this.mNumberOfFrames);
            Console.WriteLine("Position X, Y: " + this.mWorldPosition.X.ToString() + " , " + this.mWorldPosition.Y.ToString());
            Console.WriteLine("Frame Width: " + this.mAnimationFrame.Width);
            Console.WriteLine("Frame Height: " + this.mAnimationFrame.Height);
            Console.WriteLine("Sprite Origin: " + mFrameOrigin);
            Console.WriteLine("SpriteSheet Width: " + mSpriteSheet.Width);
            Console.WriteLine("SpriteSheet Height: " + mSpriteSheet.Height);
            Console.WriteLine("Sprite Height: " + mSpriteFrameHeight);
            Console.WriteLine("Sprite Width: " + mSpriteFrameWidth);
            Console.WriteLine("Current Frame: " + mCurrentFrame);
            Console.WriteLine("Interval: " + mInterval);
            Console.WriteLine("Timer" + mTimer);

        }

        #endregion
    }
}
