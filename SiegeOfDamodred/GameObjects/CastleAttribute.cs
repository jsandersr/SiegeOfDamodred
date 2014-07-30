using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace GameObjects
{
    public class CastleAttribute : Attribute
    {
        private Castle mCastle;
        private const string mCastleName = "Castle Damodred";

        public CastleAttribute(Castle castle, ContentManager content)
            : base(content)
        {
            this.mCastle = castle;
        }
    }
}
