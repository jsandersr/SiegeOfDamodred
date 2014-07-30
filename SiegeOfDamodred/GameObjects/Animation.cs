using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameObjects
{
    public class Animation
    {

        public string mAnimationName;
        public int mNumberOfCollumns;
        public int mNumberOfRows;
        public int mNumberOfFrames;
        public int mInterval;



        public Animation(string mAnimationName, int mNumberOfCollumns, int mNumberOfRows, int mNumberOfFrames,
                         int mInterval)
        {
            this.mAnimationName = mAnimationName;
            this.mNumberOfCollumns = mNumberOfCollumns;
            this.mNumberOfRows = mNumberOfRows;
            this.mInterval = mInterval;
            this.mNumberOfFrames = mNumberOfFrames;

        }


    }
}
