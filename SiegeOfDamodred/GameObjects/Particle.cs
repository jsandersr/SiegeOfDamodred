using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameObjects
{
    public class Particle
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public float Angle { get; set; }
        public float AngularVelocity { get; set; }
        public Color Color { get; set; }
        public float Size { get; set; }
        public float TTL { get; set; }
        public bool IsDead { get; set; }
        public float ParticleLifeTimer;

        public Particle(Texture2D texture, Vector2 position, Vector2 velocity,
            float angle, float angularVelocity, Color color, float size, int ttl)
        {
            Texture = texture;
            Position = position;
            Velocity = velocity;
            Angle = angle;
            AngularVelocity = angularVelocity;
            Color = color;
            Size = size;
            TTL = ttl;
            IsDead = false;
            ParticleLifeTimer = 0;
        }

        public void Update(GameTime gameTime)
        {

            ParticleLifeTimer += gameTime.ElapsedGameTime.Milliseconds;

            if (TTL <= ParticleLifeTimer)
            {
                IsDead = true;
                ParticleLifeTimer = 0;
            }

            Position += Velocity;
            Angle += AngularVelocity;
        }

        public void DrawString()
        {

        }


        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle sourceRectangle = new Rectangle(0, 0, Texture.Width, Texture.Height);
            Vector2 origin = new Vector2(Texture.Width / 2, Texture.Height / 2);

            spriteBatch.Draw(Texture, Position, sourceRectangle, Color,
                Angle, origin, Size, SpriteEffects.None, 0f);
        }
    }
}