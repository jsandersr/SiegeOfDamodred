using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExternalTypes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameObjects
{
    public class WindowFrame
    {
        // Dynamic function.
        public delegate void DrawText();
        protected DrawText mDrawText;

        private GameObject temp;

        private Rectangle mWindowFrameRectangle;
        private Texture2D mWindowFrameTexture;
        private string mAssetName;
        private StatusWindowState statusWindowState = StatusWindowState.INACTIVE;

        public StatusWindowState StatusWindowState
        {
            get { return statusWindowState; }
            set { statusWindowState = value; }
        }


        public Rectangle WindowFrameRectangle
        {
            get { return mWindowFrameRectangle; }
            set { mWindowFrameRectangle = value; }
        }

        public GameObject Temp
        {
            get { return temp; }
            set { temp = value; }
        }

        public WindowFrame(string mAssetName, Rectangle mRectangle)
        {
            this.mAssetName = mAssetName;
            mWindowFrameRectangle = mRectangle;
        }

        public void LoadContent(ContentManager content)
        {
            mWindowFrameTexture = content.Load<Texture2D>(mAssetName);
        }

        public void Update(GameTime gameTime)
        {

        }
        public void SetDrawText(DrawText function)
        {
            mDrawText = function;
        }


        public void Draw(SpriteBatch spritBatch)
        {
            spritBatch.Draw(mWindowFrameTexture, mWindowFrameRectangle, Color.White);

            // If I'm a status frame and I have a selected object to draw
            // The draw the object's parameters
            if (StatusWindowState == StatusWindowState.ACTIVE)
            {
                mDrawText();
            }


        }



    }
}
