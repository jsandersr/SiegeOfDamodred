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

namespace GameObjects
{

    public class Menu
    {

        private List<ButtonGroup> mButtonGroupList;
        private Rectangle mMenuRectangle;
        private Texture2D mTexture;
        private Vector2 mPosition;
        private float mScale;
        public GameTime gameTime;
        private ContentManager content;
        private SpriteBatch spriteBatch;
        private string mTextureName;
        private string mSongName;
        private Song mSong;
        public bool isRolling;


        public Menu(Rectangle mMenuRectangle, string mTextureName, string mSongName, ContentManager mContent, SpriteBatch mSpriteBatch)
        {
            mButtonGroupList = new List<ButtonGroup>();
            this.mMenuRectangle = mMenuRectangle;
            this.mTextureName = mTextureName;
            this.content = mContent;
            this.spriteBatch = mSpriteBatch;
            this.mSongName = mSongName;
            isRolling = false;
        }


        public GameTime GameTime
        {
            set { gameTime = value; }
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

        public Rectangle MenuRectangle
        {
            get { return mMenuRectangle; }
            set { mMenuRectangle = value; }
        }

        public void AddButtonGroup(ButtonGroup buttonGroup)
        {
            mButtonGroupList.Add(buttonGroup);
        }

        
        public void LoadContent()
        {

            mTexture = content.Load<Texture2D>(mTextureName);
            MediaPlayer.Stop();
            mSong = content.Load<Song>(mSongName);
            if (!isRolling)
            {
                MediaPlayer.Play(mSong);
                MediaPlayer.Volume = 100;
                MediaPlayer.IsRepeating = true;
            }
            else
            {
                MediaPlayer.Stop();

            }
           
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(mTexture, mMenuRectangle, Color.White);
            foreach (var buttonGroup in mButtonGroupList)
            {
                spriteBatch.Draw(buttonGroup.Texture, buttonGroup.Position, Color.White);
            }
        }



        public void Update(GameTime gameTime)
        {
            foreach (var buttonGroup in mButtonGroupList)
            {
                buttonGroup.Update(gameTime);
            }
        }





    }





}
