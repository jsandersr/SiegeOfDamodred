using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GameObjects
{
    public class Level
    {
        private Rectangle mLevelSize;
        private string mSongName;
        private Texture2D mLevelTexture;
        private static int mCurrentLevel = 0;
        private Song mSong;
        private ContentManager content;
        public bool PlayingMusic;

        public Level(Rectangle mLevelSize, Texture2D mLevelTexture, string mSongName, ContentManager mContent)
        {
            mCurrentLevel++;
            this.mSongName = mSongName;
            this.mLevelSize = mLevelSize;
            this.mLevelTexture = mLevelTexture;
            content = mContent;
            PlayingMusic = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(mLevelTexture, mLevelSize, Color.White);


        }

        public void PlayLevelMusic()
        {

            MediaPlayer.Stop();
            MediaPlayer.IsRepeating = true;
            mSong = content.Load<Song>(mSongName);
            MediaPlayer.Volume = 100;
            MediaPlayer.Play(mSong);
            PlayingMusic = true;


        }


    }
}
