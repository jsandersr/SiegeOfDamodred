using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace GameObjects
{
    public class ButtonGroup
    {
        private List<Button> mButtonList;
        private Rectangle mButtonGroupRectangle;
        private Texture2D mTexture;
        private Vector2 mPosition;
        private float mScale;
        public GameTime gameTime;
        private ContentManager content;
        private SpriteBatch spriteBatch;
        private string mTextureName;
        private int mTotalButtons;
        private int mMaxButtonsPerRow;
        private int mCurrentButtonsInRow;
        private int mCurrentButtonsInColumn;
        private int mMaxButtonsPerColumn;
        private Color mButtonColor;
        private int mLayerDepth;

        public ButtonGroup(Vector2 mPosition, string mTextureName, float mScale, ContentManager mContent,
                            int mMaxButtonsPerRow, int mMaxButtonsPerColumn)
        {

            this.mButtonColor = Color.White;
            this.mCurrentButtonsInColumn = 0;
            this.mCurrentButtonsInRow = 0;
            mButtonList = new List<Button>();
            this.mTextureName = mTextureName;
            this.mScale = mScale;
            this.mMaxButtonsPerRow = mMaxButtonsPerRow;
            this.mMaxButtonsPerColumn = mMaxButtonsPerColumn;
            this.content = mContent;
            this.mPosition = mPosition;
            this.mLayerDepth = mLayerDepth;

        }

        #region Properties

        public Texture2D Texture
        {
            get { return mTexture; }
            set { mTexture = value; }
        }

        public SpriteBatch SpriteBatch
        {
            get { return spriteBatch; }
            set { spriteBatch = value; }
        }

        public Vector2 Position
        {
            get { return mPosition; }
            set { mPosition = value; }
        }

        public float Scale
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

        public GameTime GameTime
        {
            set { gameTime = value; }
        }

        public Rectangle ButtonGroupRectangle
        {
            get
            {
                return mButtonGroupRectangle;
            }
            set
            {
                mButtonGroupRectangle = value;
            }
        }

        public List<Button> ButtonList
        {
            get { return mButtonList; }
        }

        #endregion

        public void AddLongButton(Button button)
        {
            if (mButtonList.Count == 0)
            {
                button.ButtonRectangle = new Rectangle(mButtonGroupRectangle.X, mButtonGroupRectangle.Y,
                                                       (int)(mButtonGroupRectangle.Width / 1.5f),
                                                       (int)(mButtonGroupRectangle.Height / mMaxButtonsPerColumn));
                if (button.Sprite.SpriteSheet != null)
                {
                    button.SetSpritePosition();
                }
                mButtonList.Add(button);
                mCurrentButtonsInRow++;
                mTotalButtons++;
            }

            else if (mCurrentButtonsInRow < mMaxButtonsPerRow)
            {
                button.ButtonRectangle = new Rectangle(
                                        (int)(mButtonList[mTotalButtons - 1].Position.X + mButtonList[mCurrentButtonsInRow - 1].Width),
                                        (int)(mButtonList[mTotalButtons - 1].Position.Y),
                                        (int)mButtonList[mTotalButtons - 1].Width,
                                        (int)mButtonList[mTotalButtons - 1].Height);
                if (button.Sprite.SpriteSheet != null)
                {
                    button.SetSpritePosition();
                }
                mButtonList.Add(button);
                mCurrentButtonsInRow++;
                mTotalButtons++;
            }

               // We are moving to the next column.
            else if (mCurrentButtonsInColumn < mMaxButtonsPerColumn)
            {

                button.ButtonRectangle = new Rectangle(
                                        (int)(mButtonList[mCurrentButtonsInColumn].Position.X),
                                        (int)(mButtonList[mCurrentButtonsInColumn].Position.Y + mButtonList[mCurrentButtonsInColumn].Height),
                                        (int)mButtonList[0].Width,
                                        (int)mButtonList[0].Height);
                if (button.Sprite.SpriteSheet != null)
                {
                    button.SetSpritePosition();
                }
                mCurrentButtonsInRow = 1;
                mCurrentButtonsInColumn++;
                mTotalButtons++;
                mButtonList.Add(button);

            }


        }


        public void AddButton(Button button)
        {
            // If this is out first button.
            if (mButtonList.Count == 0)
            {
                button.ButtonRectangle = new Rectangle(mButtonGroupRectangle.X, mButtonGroupRectangle.Y,
                                                       mButtonGroupRectangle.Width / mMaxButtonsPerRow,
                                                       mButtonGroupRectangle.Height / mMaxButtonsPerColumn);
                if (button.Sprite.SpriteSheet != null)
                {
                    button.SetSpritePosition();
                }
                mButtonList.Add(button);
                mCurrentButtonsInRow++;
                mTotalButtons++;

            }

                // We are adding more buttons on the same row.
            else if (mCurrentButtonsInRow < mMaxButtonsPerRow)
            {
                button.ButtonRectangle = new Rectangle(
                                        (int)(mButtonList[mTotalButtons - 1].Position.X + mButtonList[mCurrentButtonsInRow - 1].Width),
                                        (int)(mButtonList[mTotalButtons - 1].Position.Y),
                                        (int)mButtonList[mTotalButtons - 1].Width,
                                        (int)mButtonList[mTotalButtons - 1].Height);
                if (button.Sprite.SpriteSheet != null)
                {
                    button.SetSpritePosition();
                }
                mButtonList.Add(button);
                mCurrentButtonsInRow++;
                mTotalButtons++;
            }

               // We are moving to the next column.
            else if (mCurrentButtonsInColumn < mMaxButtonsPerColumn)
            {

                button.ButtonRectangle = new Rectangle(
                                        (int)(mButtonList[mCurrentButtonsInColumn].Position.X),
                                        (int)(mButtonList[mCurrentButtonsInColumn].Position.Y + mButtonList[mCurrentButtonsInColumn].Height),
                                        (int)mButtonList[0].Width,
                                        (int)mButtonList[0].Height);
                if (button.Sprite.SpriteSheet != null)
                {
                    button.SetSpritePosition();
                }
                mCurrentButtonsInRow = 1;
                mCurrentButtonsInColumn++;
                mTotalButtons++;
                mButtonList.Add(button);

            }


        }



        public void LoadContent()
        {
            mTexture = content.Load<Texture2D>(mTextureName);
            mButtonGroupRectangle = new Rectangle((int)mPosition.X, (int)mPosition.Y, mTexture.Width, mTexture.Height);
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(mTexture, mButtonGroupRectangle, Color.White);



            if (mButtonList.Count > 0)
            {
                foreach (var button in mButtonList)
                {
                    if (button.Sprite.SpriteSheet != null)
                    {
                        button.Sprite.Draw(spriteBatch, 1.0f);
                    }
                    spriteBatch.Draw(button.CurrentTexture, button.Position, null, Color.White);
                }
            }

        }

        public void SetButtonColor(Color mButtonColor)
        {
            this.mButtonColor = mButtonColor;
        }


        public void Update(GameTime gameTime)
        {

            foreach (var button in mButtonList)
            {
                button.Update(gameTime);
            }
        }
    }

}