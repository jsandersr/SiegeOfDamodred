using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using SpriteGenerator;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;

namespace GameObjects
{

    public class Button
    {
        // Dynamic function.
        public delegate void ButtonAction();
        protected ButtonAction mButtonAction;
        private Keys mHotKey;
        KeyboardState keyboardState;
        KeyboardState previousKeyboardState;

        // Game Manager Fields.
        private ContentManager content;
        private GameTime gameTime;
        private MouseState mouseState;
        private MouseState prevoiusMouseState;
        private float StartTime;
        private float pressTimer;

        //Loadable Attributes.
        private SoundEffect mSoundEffect;
        private Texture2D mButtonUnclickedTexture;
        private Texture2D mButtonClickedTexture;
        private Texture2D mButtonDisabledTexture;
        private Texture2D mCurrentTexture;
        private Texture2D mButtonSelectedTexture;

        // Asset Names.
        private string mUnclickedTextureName;
        private string mClickedTextureName;
        private string mDisabledTextureName;
        private string mSoundEffectName;
        private string mButtonSelectedTextureName;

        // Dimensions.
        private Rectangle mButtonRectangle;
        private Vector2 mPosition;
        private float mScale;
        private float mWidth;
        private float mHeight;
        private bool isDisabled;

        // Animation Attributes
        private List<Animation> mAnimationList;
        private Sprite mSprite;
        private int mAnimationIndex;

        public Button(ContentManager content, ButtonAction function, string mUnclickedTextureName = null, string mClickedTextureName = null, string mDisabledTextureName = null, Keys mhotKey = Keys.None)
        {
            this.IsDisabled = false;
            this.mHotKey = mhotKey;
            mAnimationList = new List<Animation>();
            mSprite = new Sprite(content);
            // Set Game Manager Fields.
            this.content = content;

            StartTime = 0.0f;

            // Set Dimensions.
            mScale = 1.0f;

            // Set Asset Names.
            this.mUnclickedTextureName = mUnclickedTextureName;
            this.mClickedTextureName = mClickedTextureName;
            this.mDisabledTextureName = mDisabledTextureName;

            // Set sound effect name.

            //Load Content
            if (mUnclickedTextureName != null)
            {
                mButtonUnclickedTexture = content.Load<Texture2D>(mUnclickedTextureName);
            }
            if (mClickedTextureName != null)
            {
                mButtonClickedTexture = content.Load<Texture2D>(mClickedTextureName);
            }
            if (mDisabledTextureName != null)
            {
                mButtonDisabledTexture = content.Load<Texture2D>(mDisabledTextureName);
            }

            mCurrentTexture = mButtonUnclickedTexture;
            mButtonAction = function;

        }


        #region Properties

        public bool IsDisabled
        {
            get { return isDisabled; }
            set { isDisabled = value; }
        }

        public Sprite Sprite
        {
            get { return mSprite; }
            set { mSprite = value; }
        }


        public GameTime GameTime
        {

            set { gameTime = value; }
        }

        public Rectangle ButtonRectangle
        {
            get { return mButtonRectangle; }
            set
            {
                mButtonRectangle = value;
                Position = new Vector2(ButtonRectangle.X, ButtonRectangle.Y);
                Width = ButtonRectangle.Width;
                Height = ButtonRectangle.Height;
            }
        }

        public Texture2D CurrentTexture
        {
            get { return mCurrentTexture; }
            set { mCurrentTexture = value; }
        }

        public float Height
        {
            get { return mHeight; }
            set { mHeight = value; }
        }

        public Vector2 Position
        {
            get { return mPosition; }
            set { mPosition = value; }
        }

        public float Scale
        {
            get { return mScale; }
            set { mScale = value; }
        }

        public float Width
        {
            get { return mWidth; }
            set { mWidth = value; }
        }

        #endregion


        public void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();
            StartTime += gameTime.ElapsedGameTime.Milliseconds;
            pressTimer += gameTime.ElapsedGameTime.Milliseconds;
            CheckPresses();
            if (mAnimationList.Count != 0)
            {
                mSprite.PlayAnimation(gameTime);
            }

        }

        public void SetSpritePosition()
        {
            mSprite.WorldPosition = new Vector2(this.mButtonRectangle.X + this.mButtonRectangle.Width / 2, this.mButtonRectangle.Y + this.mButtonRectangle.Height / 2);
        }

        public void AddAnimation(Animation animation)
        {
            mAnimationList.Add(animation);
            SetAnimation();
            mSprite.LoadContent();

        }


        private void CheckPresses()
        {
            if (!isDisabled)
            {
                CurrentTexture = mButtonUnclickedTexture;
                if (mouseState.LeftButton == ButtonState.Pressed
                    && mButtonRectangle.Contains((int)(mouseState.X / GameObject.Scale),
                                                 (int)(mouseState.Y / GameObject.Scale))
                    && StartTime >= 200)
                {
                    StartTime = 0.0f;
                    if (mButtonClickedTexture != null)
                    {
                        CurrentTexture = mButtonClickedTexture;
                    }
                }

                if ((keyboardState.IsKeyDown(mHotKey) && previousKeyboardState.IsKeyDown(mHotKey) && pressTimer >= 200) ||
                    (prevoiusMouseState.LeftButton == ButtonState.Pressed &&
                     mouseState.LeftButton == ButtonState.Released
                     && mButtonRectangle.Contains((int)(mouseState.X / GameObject.Scale),
                                                  (int)(mouseState.Y / GameObject.Scale))))
                {
                    if (mButtonUnclickedTexture != null)
                    {
                        CurrentTexture = mButtonUnclickedTexture;
                    }
                    pressTimer = 0.0f;
                    mButtonAction();
                }

                else if (prevoiusMouseState.LeftButton == ButtonState.Pressed &&
                         mouseState.LeftButton == ButtonState.Released)
                {
                    if (mButtonUnclickedTexture != null)
                    {
                        CurrentTexture = mButtonUnclickedTexture;
                    }
                }

                prevoiusMouseState = mouseState;
                previousKeyboardState = keyboardState;
            }

            else
            {
                if (mButtonDisabledTexture != null)
                {
                    CurrentTexture = mButtonDisabledTexture;
                }
            }
        }

        public void SetAnimation()
        {
            this.mSprite.AssetName = mAnimationList.ElementAt(mAnimationIndex).mAnimationName;
            this.mSprite.NumberOfColumns = mAnimationList.ElementAt(mAnimationIndex).mNumberOfCollumns;
            this.mSprite.NumberOfRows = mAnimationList.ElementAt(mAnimationIndex).mNumberOfRows;
            this.mSprite.NumberOfFrames = mAnimationList.ElementAt(mAnimationIndex).mNumberOfFrames;
            this.mSprite.Interval = mAnimationList.ElementAt(mAnimationIndex).mInterval;
        }
    }


}
