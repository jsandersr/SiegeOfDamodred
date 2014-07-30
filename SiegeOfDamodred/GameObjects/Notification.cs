using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameObjects
{

    internal class TextBoxString
    {
        public string mStringValue;
        public Vector2 mStringPosition;
        public float TimeToLive;

    }


    public static class Notification
    {
        private static SpriteFont mSpriteFont;
        private static float changingY;
        private static List<TextBoxString> ListOfDamageNumbers = new List<TextBoxString>();
        private static List<TextBoxString> ListOfHealingNumbers = new List<TextBoxString>();
        private static float MaxLife = 500;
        private static float VerticalSpeed = 5;
        private static float lifeTimer;
        private static float moveTimer;
        private static float LevelRibbonNotificationLife = 1500.0f;
        private static Texture2D mLevelRibbonTexture;
        private static bool isDrawingLevelBanner;
        private static float mAnimationTimer;


        #region Healing Number Notifications

        public static void SpawnHealingNumber(string numberValueString, Vector2 mStringPosition)
        {
            var tempTextBoxString = new TextBoxString();
            tempTextBoxString.TimeToLive = 0;
            tempTextBoxString.mStringPosition = mStringPosition;
            tempTextBoxString.mStringValue = numberValueString.ToString();

            ListOfHealingNumbers.Add(tempTextBoxString);

        }

        public static void UpdateHealingNumbers(GameTime gameTime)
        {

            moveTimer += gameTime.ElapsedGameTime.Milliseconds;

            if (moveTimer >= 500)
            {
                for (int i = 0; i < ListOfHealingNumbers.Count; i++)
                {
                    ListOfHealingNumbers[i].mStringPosition.Y -= 5f;

                }


            }


            for (int index = 0; index < ListOfHealingNumbers.Count; index++)
            {
                var textBoxString = ListOfHealingNumbers[index];
                textBoxString.TimeToLive += gameTime.ElapsedGameTime.Milliseconds;

                if (textBoxString.TimeToLive >= MaxLife)
                {
                    ListOfHealingNumbers.Remove(textBoxString);

                }
            }
        }

        public static void DrawHealingNumbers(SpriteBatch spriteBatch)
        {
            float textScale = .25f;


            foreach (var textBoxString in ListOfHealingNumbers)
            {
                spriteBatch.DrawString(mSpriteFont, textBoxString.mStringValue,
                                       new Vector2(textBoxString.mStringPosition.X, textBoxString.mStringPosition.Y),
                                       Color.GreenYellow, 0.0f, Vector2.Zero, .5f,
                                       SpriteEffects.None, 0.0f);
            }
        }

        #endregion

        #region Damage Number Notifications

        public static void SpawnDamageNumber(string numberValueString, Vector2 mStringPosition)
        {
            var tempTextBoxString = new TextBoxString();
            tempTextBoxString.TimeToLive = 0;
            tempTextBoxString.mStringPosition = mStringPosition;
            tempTextBoxString.mStringValue = numberValueString.ToString();

            ListOfDamageNumbers.Add(tempTextBoxString);

        }

        public static void UpdateDamageNumbers(GameTime gameTime)
        {

            moveTimer += gameTime.ElapsedGameTime.Milliseconds;

            if (moveTimer >= 500)
            {
                for (int i = 0; i < ListOfDamageNumbers.Count; i++)
                {
                    ListOfDamageNumbers[i].mStringPosition.Y -= 5f;

                }
            }

            for (int index = 0; index < ListOfDamageNumbers.Count; index++)
            {
                var textBoxString = ListOfDamageNumbers[index];
                textBoxString.TimeToLive += gameTime.ElapsedGameTime.Milliseconds;

                if (textBoxString.TimeToLive >= MaxLife)
                {
                    ListOfDamageNumbers.Remove(textBoxString);

                }
            }
        }


        public static void DrawDamageNumbers(SpriteBatch spriteBatch)
        {
            float textScale = .25f;


            foreach (var textBoxString in ListOfDamageNumbers)
            {
                spriteBatch.DrawString(mSpriteFont, textBoxString.mStringValue,
                                       new Vector2(textBoxString.mStringPosition.X, textBoxString.mStringPosition.Y),
                                       Color.Red, 0.0f, Vector2.Zero, .5f,
                                       SpriteEffects.None, 0.0f);
            }
        }

        #endregion

        #region Load Notifications Functins

        public static void LoadNotificationFont(ContentManager mContent)
        {
            mSpriteFont = mContent.Load<SpriteFont>("Sprites/Fonts/ConsoleFont");
        }
        public static void LoadLevelUpRibbon(ContentManager mContent)
        {
            mLevelRibbonTexture = mContent.Load<Texture2D>("Sprites/Hero/LevelBanner");
        }

        #endregion 

        #region Level Banner Functions

        public static void ShowLevelGainNotification()
        {
            if (AudioController.HeroLevelGainSoundEffectInstance != null)
            {
                AudioController.HeroLevelGainSoundEffectInstance.Play(); 
                isDrawingLevelBanner = true;
            }
           
            

        }

        public static void UpdateLevelBanner(GameTime gameTime)
        {
            if (!isDrawingLevelBanner)
            {
                mAnimationTimer = 0;
                
            }

            if (isDrawingLevelBanner)
            {
                mAnimationTimer += gameTime.ElapsedGameTime.Milliseconds;
               
                if (mAnimationTimer >= LevelRibbonNotificationLife)
                {
                    isDrawingLevelBanner = false;
                }

            }

        }


        public static void DrawLevelBanner(SpriteBatch spriteBatch)
        {
            if (isDrawingLevelBanner)
            {
                spriteBatch.Draw(mLevelRibbonTexture, new Vector2(400, 70), null, Color.White
                                 , 0.0f, Vector2.Zero, 0.7f, SpriteEffects.None, 0);
            }
        }

        #endregion

    }

}


      

      
