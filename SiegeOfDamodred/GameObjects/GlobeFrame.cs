using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GameObjects
{


    public class GlobeFrame
    {




        // Game Loop Fields.
        GraphicsDevice graphics;
        SpriteBatch spriteBatch;
        KeyboardState keyboardState;
        KeyboardState previousKeyboardState;
        private ContentManager Content;

        // Textures.
        public Texture2D mDrawingTexture;
        Texture2D mGlobeLiquidTexture;
        Texture2D mGlobeBoxTexture;
        Texture2D mGlobeGlassTexture;


        // Render Targets.
        RenderTarget2D renderTargetA;
        RenderTarget2D renderTargetB;
        RenderTarget2D activeRenderTarget;
        RenderTarget2D textureRenderTarget;

        AlphaTestEffect alphaTestEffect;
        DepthStencilState stencilAlways;
        DepthStencilState stencilKeepIfZero;

        //Locations
        private Vector2 mDrawingTexturePosition;
        public Vector2 mGlobeBoxPosition; //crater pposition
        public Vector2 mGlobeGlassPosition; //globe
        private int newLocationY;
        private int newLocationX;

        // Teture paths
        private string mGlobeBoxTexturePath;
        private string mGlobeGlassTexturePath;
        private string mGlobeLiquidTexturePath;

        private Hero mHero;
        private float tempAttributePercent;





        public GlobeFrame(GraphicsDevice graphics, SpriteBatch spriteBatch, ContentManager Content, string mGlobeGlassTexturePath, string mGlobeBoxTexturePath,
            string mGlobeLiquidTexturePath, Vector2 mDrawingTexturePosition, Hero mHero)
        {
            this.graphics = graphics;
            this.spriteBatch = spriteBatch;
            this.Content = Content;
            this.mDrawingTexturePosition = mDrawingTexturePosition;
            this.mGlobeBoxTexturePath = mGlobeBoxTexturePath;
            this.mGlobeGlassTexturePath = mGlobeGlassTexturePath;
            this.mGlobeLiquidTexturePath = mGlobeLiquidTexturePath;
            this.mHero = mHero;

            newLocationY = 0;
            newLocationX = 0;
            // initialize keyboard state
            keyboardState = Keyboard.GetState();
            previousKeyboardState = keyboardState;
        }


        public Vector2 GlobeBoxPosition
        {
            get { return mGlobeBoxPosition; }
            set { mGlobeBoxPosition = value; }
        }

        public void ChangeHP(Vector2 position)
        {



            // set up rendering to the active render target
            graphics.SetRenderTarget(activeRenderTarget);

            // clear the render target to opaque black,
            // and initialize the stencil buffer with all zeroes
            graphics.Clear(ClearOptions.Target | ClearOptions.Stencil,
                                 new Color(0, 0, 0, 1.0f), 0, 0);


            // draw the new craters into the stencil buffer
            // stencilAlways makes sure we'll always write a 1
            // to the stencil buffer wherever we draw. the alphaTestEffect
            // is set up to only write a pixel if the alpha value
            // of the source texture is zero

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Opaque,
                              null, stencilAlways, null, alphaTestEffect);

            Vector2 origin = new Vector2(mGlobeBoxTexture.Width * 0.5f,
                                         mGlobeBoxTexture.Height * 0.5f);

            //float rotation = (float)random.NextDouble() * MathHelper.TwoPi; 
            //65                                                                                                                            //- 105
            Rectangle rec = new Rectangle((int)(mGlobeGlassPosition.X - mGlobeLiquidTexture.Width), (int)(mGlobeGlassPosition.Y - mGlobeLiquidTexture.Height - 130), 250, 250);

            rec.Y += newLocationY;
            rec.X += newLocationX;

            spriteBatch.Draw(mGlobeBoxTexture, rec, null, Color.White, 0,
                             origin, SpriteEffects.None, 0);


            spriteBatch.End();


            // now draw the latest planet texture, excluding the stencil
            // buffer, resulting in the new craters being excluded from
            // the drawing. the first time through we don't have a latest
            // planet texture, so draw from the original texture
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Opaque,
                              null, stencilKeepIfZero, null, null);

            //if (firstTime)
            //{
            spriteBatch.Draw(mGlobeLiquidTexture, Vector2.Zero, Color.White);

            // firstTime = false;
            //}
            //else
            // spriteBatch.Draw(textureRenderTarget, Vector2.Zero, Color.White);

            spriteBatch.End();


            // restore main render target - this lets us get at the render target we just drew and use it as a texture
            graphics.SetRenderTarget(null);


            //// save image for testing
            //using (FileStream f = new FileStream("planet.png", FileMode.Create))
            //{
            //  activeRenderTarget.SaveAsPng(f, 256, 256);
            //}


            //firstTime = true;
            // swap render targets, so the next time our source texture is the render target we just drew,
            // and the one we'll be drawing on is the one we just used as our source texture this time
            RenderTarget2D t = activeRenderTarget;
            activeRenderTarget = textureRenderTarget;
            textureRenderTarget = t;

            mDrawingTexture = textureRenderTarget;
        }
        public void ChangeMana(Vector2 position)
        {



            // set up rendering to the active render target
            graphics.SetRenderTarget(activeRenderTarget);

            // clear the render target to opaque black,
            // and initialize the stencil buffer with all zeroes
            graphics.Clear(ClearOptions.Target | ClearOptions.Stencil,
                                 new Color(0, 0, 0, 1.0f), 0, 0);


            // draw the new craters into the stencil buffer
            // stencilAlways makes sure we'll always write a 1
            // to the stencil buffer wherever we draw. the alphaTestEffect
            // is set up to only write a pixel if the alpha value
            // of the source texture is zero

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Opaque,
                              null, stencilAlways, null, alphaTestEffect);

            Vector2 origin = new Vector2(mGlobeBoxTexture.Width * 0.5f,
                                         mGlobeBoxTexture.Height * 0.5f);

            //float rotation = (float)random.NextDouble() * MathHelper.TwoPi; 
            //65                                                             // - 65                                                                  //- 105
            Rectangle rec = new Rectangle((int)(mGlobeGlassPosition.X - mGlobeLiquidTexture.Width), (int)(mGlobeGlassPosition.Y - mGlobeLiquidTexture.Height - 130), 250, 250);

            rec.Y += newLocationY;
            rec.X += newLocationX;

            spriteBatch.Draw(mGlobeBoxTexture, rec, null, Color.White, 0,
                             origin, SpriteEffects.None, 0);


            spriteBatch.End();


            // now draw the latest planet texture, excluding the stencil
            // buffer, resulting in the new craters being excluded from
            // the drawing. the first time through we don't have a latest
            // planet texture, so draw from the original texture
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Opaque,
                              null, stencilKeepIfZero, null, null);

            //if (firstTime)
            //{
            spriteBatch.Draw(mGlobeLiquidTexture, Vector2.Zero, Color.White);

            // firstTime = false;
            //}
            //else
            // spriteBatch.Draw(textureRenderTarget, Vector2.Zero, Color.White);

            spriteBatch.End();


            // restore main render target - this lets us get at the render target we just drew and use it as a texture
            graphics.SetRenderTarget(null);


            //// save image for testing
            //using (FileStream f = new FileStream("planet.png", FileMode.Create))
            //{
            //  activeRenderTarget.SaveAsPng(f, 256, 256);
            //}


            //firstTime = true;
            // swap render targets, so the next time our source texture is the render target we just drew,
            // and the one we'll be drawing on is the one we just used as our source texture this time
            RenderTarget2D t = activeRenderTarget;
            activeRenderTarget = textureRenderTarget;
            textureRenderTarget = t;

            mDrawingTexture = textureRenderTarget;
        }
        public void UpdateHp()
        {
            // update keyboard state
            previousKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();





            // calculate planet position, centered in screen
            //            mGlobeGlassPosition = new Vector2(graphics.PresentationParameters.BackBufferWidth * 0.5f - 130 * 0.4f,
            //                                      graphics.PresentationParameters.BackBufferHeight * 0.5f - 130 * 0.4f);
            mGlobeGlassPosition = new Vector2(150, 150); //300, 140

            if (mHero.HeroAttribute.CurrentHealthPoints != mHero.HeroAttribute.MaxHealthPoints)
            {
                mGlobeBoxPosition = new Vector2(150, 150); //150, 150
                tempAttributePercent = ((mHero.HeroAttribute.CurrentHealthPoints * 100.0f) /
                                        mHero.HeroAttribute.MaxHealthPoints);
                newLocationY = (int)(150 - (tempAttributePercent * 150) / 100);


                // newLocationY = (int)(130*(tempAttributePercent/100));


                // newLocationY = 150;


            }
            else
            {
                newLocationY = (int)(150 - (100 * 150) / 100);
            }


            //if ((keyboardState.IsKeyDown(Keys.Down) && previousKeyboardState.IsKeyUp(Keys.Down)))
            //{
            //    mGlobeBoxPosition = new Vector2(150, 150); //150, 150
            //    newLocationY += 10;

            //}
            //else if ((keyboardState.IsKeyDown(Keys.Up) && previousKeyboardState.IsKeyUp(Keys.Up)))
            //{
            //    mGlobeBoxPosition = new Vector2(150, 150);
            //    newLocationY -= 10;
            //}

            //if ((keyboardState.IsKeyDown(Keys.Right) && previousKeyboardState.IsKeyUp(Keys.Right)))
            //{
            //    mGlobeBoxPosition = new Vector2(150, 150);
            //    newLocationX += 10;
            //}
            //else if ((keyboardState.IsKeyDown(Keys.Left) && previousKeyboardState.IsKeyUp(Keys.Left)))
            //{
            //    mGlobeBoxPosition = new Vector2(150, 150);
            //    newLocationX -= 10;
            //}

        }
        public void UpdateMana()
        {
            // update keyboard state
            previousKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();





            // calculate planet position, centered in screen
            //            mGlobeGlassPosition = new Vector2(graphics.PresentationParameters.BackBufferWidth * 0.5f - 130 * 0.4f,
            //                                      graphics.PresentationParameters.BackBufferHeight * 0.5f - 130 * 0.4f);
            mGlobeGlassPosition = new Vector2(150, 150); //300, 140

            if (mHero.HeroAttribute.Mana != mHero.HeroAttribute.MaxMana)
            {
                mGlobeBoxPosition = new Vector2(150, 150); //150, 150
                tempAttributePercent = ((mHero.HeroAttribute.Mana * 100.0f) /
                                        mHero.HeroAttribute.MaxMana);
                newLocationY = (int)(150 - (tempAttributePercent * 150) / 100);


                // newLocationY = (int)(130*(tempAttributePercent/100));


                // newLocationY = 150;


            }
            else
            {
                newLocationY = (int)(150 - (100 * 150) / 100);
            }


            //if ((keyboardState.IsKeyDown(Keys.Down) && previousKeyboardState.IsKeyUp(Keys.Down)))
            //{
            //    mGlobeBoxPosition = new Vector2(150, 150); //150, 150
            //    newLocationY += 10;

            //}
            //else if ((keyboardState.IsKeyDown(Keys.Up) && previousKeyboardState.IsKeyUp(Keys.Up)))
            //{
            //    mGlobeBoxPosition = new Vector2(150, 150);
            //    newLocationY -= 10;
            //}

            //if ((keyboardState.IsKeyDown(Keys.Right) && previousKeyboardState.IsKeyUp(Keys.Right)))
            //{
            //    mGlobeBoxPosition = new Vector2(150, 150);
            //    newLocationX += 10;
            //}
            //else if ((keyboardState.IsKeyDown(Keys.Left) && previousKeyboardState.IsKeyUp(Keys.Left)))
            //{
            //    mGlobeBoxPosition = new Vector2(150, 150);
            //    newLocationX -= 10;
            //}

        }
        public void LoadContent()
        {
            // background = Content.Load<Texture2D>("Background");
            mGlobeLiquidTexture = Content.Load<Texture2D>(mGlobeLiquidTexturePath);
            mGlobeBoxTexture = Content.Load<Texture2D>(mGlobeBoxTexturePath);
            mGlobeGlassTexture = Content.Load<Texture2D>(mGlobeGlassTexturePath);

            // set up alpha test effect                               //right bottom
            Matrix projection = Matrix.CreateOrthographicOffCenter(0, 150, 150, 0, 0, 1);
            Matrix halfPixelOffset = Matrix.CreateTranslation(-0.5f, -0.5f, 0);

            // The alpha test. This will check to make sure the only
            // parts of a texture that we are overwriting to another texture are
            // the parts with an alpha value of 0.
            alphaTestEffect = new AlphaTestEffect(graphics);
            alphaTestEffect.VertexColorEnabled = true;
            alphaTestEffect.DiffuseColor = Color.White.ToVector3();
            alphaTestEffect.AlphaFunction = CompareFunction.Equal;
            alphaTestEffect.ReferenceAlpha = 0;
            alphaTestEffect.World = Matrix.Identity;
            alphaTestEffect.View = Matrix.Identity;
            alphaTestEffect.Projection = halfPixelOffset * projection;


            // set up stencil state to always replace stencil buffer with 1
            //This is the stencil buffer for writing to the RENDER TARGET
            stencilAlways = new DepthStencilState();
            stencilAlways.StencilEnable = true;
            stencilAlways.StencilFunction = CompareFunction.Always;
            stencilAlways.StencilPass = StencilOperation.Replace;
            stencilAlways.ReferenceStencil = 1;
            stencilAlways.DepthBufferEnable = false;


            // set up stencil state to pass if the stencil value is 0
            // This is the stencil buffer for writing data to the TEXTURE
            stencilKeepIfZero = new DepthStencilState();
            stencilKeepIfZero.StencilEnable = true;
            stencilKeepIfZero.StencilFunction = CompareFunction.Equal;
            stencilKeepIfZero.StencilPass = StencilOperation.Keep;
            stencilKeepIfZero.ReferenceStencil = 0;
            stencilKeepIfZero.DepthBufferEnable = false;


            // create render targets
            renderTargetA = new RenderTarget2D(graphics, 130, 130,
                                               false, SurfaceFormat.Color, DepthFormat.Depth24Stencil8,
                                               0, RenderTargetUsage.DiscardContents);

            renderTargetB = new RenderTarget2D(graphics, 130, 130,
                                               false, SurfaceFormat.Color, DepthFormat.Depth24Stencil8,
                                               0, RenderTargetUsage.DiscardContents);


            // set up the ping-pong texture pointers
            activeRenderTarget = renderTargetA;
            textureRenderTarget = renderTargetB;
            mDrawingTexture = mGlobeLiquidTexture;
        }

        public void Draw()
        {
            spriteBatch.Draw(mDrawingTexture, mDrawingTexturePosition, Color.White);
            spriteBatch.Draw(mGlobeGlassTexture, mDrawingTexturePosition, Color.White);

        }


    }
}
