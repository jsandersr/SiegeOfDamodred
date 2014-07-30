using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ExternalTypes;
using GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using SpriteGenerator;
using Keys = Microsoft.Xna.Framework.Input.Keys;

/*
            This class adds a means to debug our game in real time. The following functionalities are provided:
 *          
 *          -   A Console Region that can be toggled to be active or hidden, and displays text.
 *          -   A word wrapper function to allow text to be wrapped properly (sometimes buggy).
 *          -   A Scrolling function to allow text to properly push off screen as new lines are typed.
 *          -   A Write Line function that can be called to display any string to the console.
 *          -   A Write Line At Interval function that can be called even in an update method and will only display data at a given interval.
 *          -   A Command function that holds a list of commands that the console listens for. Operations can be 
 *              triggered when the proper commands are entered.
 *              
             
 *          FUCTIONS THAT MAY BE MODIFIED:
 *          - CheckCommand
 *          
 *          ***IF YOU ARE EXPERIENCING ANY MAJOR PROBLEMS, OR NEED ADDITIONAL FUNCTIONALITIES, PLEASE CONTANT JOSH!! I will respond promptly.***
 *              
 
 */





//TODO: ADD SCROLLBAR

namespace DebugLib
{
    public struct TextBoxString
    {
        public string mStringValue;
        public Vector2 mStringPosition;

    }

    public static class Debug
    {
        #region Fields

        // Enum States.
        public static DebugSpriteID mSpriteIDState = DebugSpriteID.SPRITEID_DISABLED;
        public static DebugState mDebugState = DebugState.HIDDEN;
        public static DebugTargetQueue mTargetQueueState = DebugTargetQueue.TARGET_QUEUE_DISABLED;


        // Fields for our textbox.
        public const int TEXTBOX_Y = 10;
        public const int TEXTBOX_X = 10;
        public const int TEXTBOX_WIDTH = 900;
        public const int TEXTBOX_HEIGHT = 100;
        public static Rectangle mTextBox;
        public static Texture2D mTextBoxTexture;

        // Fields for the sprite font itself.
        public static SpriteFont mSpriteFont;
        public static float mTextScale;
        public static SpriteBatch mSpriteBatch;
        public static ContentManager mContent;

        // Fields needed for grabbing text from the user.
        public static KeyboardState mKeyboardState;
        public static TextBoxString mTypedText;
        public static float mDelayInMilliseconds;
        public static float mStartTime;
        public static List<TextBoxString> mMessageLogList;


        // Fields for CurrentTarget Queue Position.
        private static Vector2 mTargeQueueTextPosition;

        public delegate void CommandAction();

        public static CommandAction EnableGrid;
        public static CommandAction EnableGodModeFunction;


        #endregion

        #region Game Loop Functions

        public static void Load()
        {

            mTextBoxTexture = mContent.Load<Texture2D>("Sprites/PixelTextures/solidwhite");
            mSpriteFont = mContent.Load<SpriteFont>("Sprites/Fonts/ConsoleFont");

        }

        public static void Initialize()
        {
            // This is a lambda expression that calls a delegate. 
            KeyInterceptor.InboundCharEvent += (inboundCharacter) =>
            {
                // Only append characters that exist in the spritefont.
                if (inboundCharacter < 32)
                    return;
                if (inboundCharacter > 126)
                    return;
                // If the debug console is active
                if (mDebugState == DebugState.ACTIVE && inboundCharacter != '`')
                {
                    // Keep adding characters to the string. 
                    mTypedText.mStringValue += inboundCharacter;

                }
            };
        }


        public static void Update(GameTime gameTime)
        {
            if (mDebugState == DebugState.ACTIVE)
            {
                mStartTime += gameTime.ElapsedGameTime.Milliseconds;
                mKeyboardState = Keyboard.GetState();

                // Only allow this keypress to count once every delay interval.
                if (mKeyboardState.IsKeyDown(Keys.Enter) && mStartTime >= mDelayInMilliseconds)
                {
                    // Push the message into our list of messages so that we can display a message log.
                    mMessageLogList.Add(mTypedText);

                    // The next message should appear just below the last one.
                    mTypedText.mStringPosition.Y += mSpriteFont.MeasureString(mTypedText.mStringValue).Y * mTextScale;

                    // Scroll the log.
                    ScrollLog(mTypedText);
                    // Check if the user typed a command I must carry out.
                    CheckCommand(mTypedText);

                    // Reset the timer and the string value.
                    mTypedText.mStringValue = " ";
                    mStartTime = 0.0f;

                    if (mMessageLogList.Count > 20)
                    {
                        // Delete old log messages.
                        mMessageLogList.RemoveAt(0);
                    }
                }
                // Make sure the log is scrolling if it needs to be.
                ScrollLog(mTypedText);
            }
        }

        public static void Draw()
        {
            if (mDebugState == DebugState.ACTIVE)
            {
                if (mMessageLogList != null)
                {
                    // Draw Textbox on First.
                    mSpriteBatch.Draw(mTextBoxTexture, mTextBox, new Color(0, 0, 0, 120));

                    // Draw the entire message log.
                    foreach (TextBoxString text in mMessageLogList)
                    {
                        // If the log is within bounds of the textbox.
                        if (text.mStringPosition.Y > 0 && text.mStringPosition.Y < TEXTBOX_HEIGHT)
                        {
                            // Draw the message in log.
                            mSpriteBatch.DrawString(mSpriteFont, ParseText(text.mStringValue), text.mStringPosition, Color.White,
                                                    0.0f,
                                                    new Vector2(0, 0), mTextScale, SpriteEffects.None, 0.0f);
                        }
                    }
                }
                // Only draw typed text if it is inside the textbox.
                if (mTypedText.mStringPosition.Y > -TEXTBOX_HEIGHT && mTypedText.mStringPosition.Y < TEXTBOX_HEIGHT)
                {
                    // Draw the letters the user is currently typing, as he types them.
                    mSpriteBatch.DrawString(mSpriteFont, ParseText(mTypedText.mStringValue), mTypedText.mStringPosition,
                                            Color.White, 0.0f, new Vector2(0, 0), mTextScale, SpriteEffects.None, 0.0f);
                }
            }

        }

        public static void DrawTargetQueue()
        {
            // Start from the beginning of our objects list.
            GameObject.mPotentialTargetListList.Reset();

            // Check every object in the global list.
            for (int i = 0; i < GameObject.mPotentialTargetListList.GetCount(); i++)
            {
                // If the object is an enemy.
                if (GameObject.mPotentialTargetListList.GetCurrent().gameObject.Hostility == Hostility.ENEMY)
                {
                    Enemy tempEnemy;
                    try
                    {
                        // Cast to an enemy.
                        tempEnemy = (Enemy)GameObject.mPotentialTargetListList.GetCurrent().gameObject;
                        tempEnemy.TargetQueue.mTargetList.Reset();

                        // Set position of the TarQ text.
                        Vector2 tempPos = new Vector2(tempEnemy.Sprite.WorldPosition.X + tempEnemy.Sprite.SpriteFrame.Width / 2,
                                                       tempEnemy.Sprite.WorldPosition.Y + tempEnemy.Sprite.SpriteFrame.Width / 2);
                        mSpriteBatch.DrawString(mSpriteFont, "TarQ: ", new Vector2(tempPos.X, tempPos.Y), Color.White, 0.0f, new Vector2(0, 0), mTextScale, SpriteEffects.None, 0f);

                        // Set the position of the sprite id's in queue's text.
                        mTargeQueueTextPosition.X = tempPos.X + mSpriteFont.MeasureString("TarQ: ").X * mTextScale;
                        mTargeQueueTextPosition.Y = tempPos.Y;

                        // Run through the current enemy's target queue.
                        for (int j = 0; j < tempEnemy.TargetQueue.mTargetList.GetCount(); j++)
                        {

                            mTargeQueueTextPosition.X += mSpriteFont.MeasureString(tempEnemy.TargetQueue.mTargetList.GetCurrent().gameObject.ObjectID.ToString()).X * mTextScale;

                            mTargeQueueTextPosition.Y = tempPos.Y;

                            mSpriteBatch.DrawString(mSpriteFont, tempEnemy.TargetQueue.mTargetList.GetCurrent().gameObject.ObjectID.ToString(), mTargeQueueTextPosition, Color.White
                                , 0.0f, new Vector2(0, 0), mTextScale, SpriteEffects.None, 0.0f);

                            tempEnemy.TargetQueue.mTargetList.NextNode();
                        }
                    }
                    catch (Exception e)
                    {

                        Console.WriteLine("stuff");
                    }
                }

                GameObject.mPotentialTargetListList.NextNode();

            }




        }
        public static void DrawSpriteID()
        {
            GameObject.mInPlayObjectList.Reset();
            for (int i = 0; i < GameObject.mInPlayObjectList.GetCount(); i++)
            {

                mSpriteBatch.DrawString(mSpriteFont,
                                 GameObject.mInPlayObjectList.GetCurrent().gameObject.ObjectID.ToString(),
                                 new Vector2(
                                     GameObject.mInPlayObjectList.GetCurrent()
                                               .gameObject.Sprite.WorldPosition
                                               .X +
                                     GameObject.mInPlayObjectList.GetCurrent()
                                               .gameObject.Sprite.SpriteFrame
                                               .Width / 2,
                                     GameObject.mInPlayObjectList.GetCurrent()
                                               .gameObject.Sprite.WorldPosition
                                               .Y +
                                     GameObject.mInPlayObjectList.GetCurrent()
                                               .gameObject.Sprite.SpriteFrame
                                               .Height / 2), Color.White);
                GameObject.mInPlayObjectList.NextNode();
            }
        }



        #endregion

        #region Utility Funcions

        private static void CheckCommand(TextBoxString command)
        {
            var response = new TextBoxString();

            // Strip whitespace from any command we send.
            command.mStringValue = new string(command.mStringValue.Where(c => !char.IsWhiteSpace(c)).ToArray());

            //If we receive this command.
            if (command.mStringValue == "enspriteid")
            {
                //Type a response to the screen.
                response.mStringValue = "Sprite ID Text Enabled.";
                response.mStringPosition = command.mStringPosition;
                // Move our cursor below the response.
                mTypedText.mStringPosition.Y += mSpriteFont.MeasureString(mTypedText.mStringValue).Y *
                                                mTextScale;
                // Add response to the message log so that it may be displayed from the log.
                mMessageLogList.Add(response);

                EnableSpriteID();


            }
            if (command.mStringValue == "entargetq")
            {
                //Type a response to the screen.
                response.mStringValue = "CurrentTarget Queue Enabled.";
                response.mStringPosition = command.mStringPosition;
                // Move our cursor below the response.
                mTypedText.mStringPosition.Y += mSpriteFont.MeasureString(mTypedText.mStringValue).Y *
                                                mTextScale;
                // Add response to the message log so that it may be displayed from the log.
                mMessageLogList.Add(response);

                EnableTargetQueue();


            }
            else if (command.mStringValue == "engrid")
            {
                EnableGrid();
            }
            else if (command.mStringValue == "thereisnospoon")
            {
                response.mStringValue = "CHEATER!!!";
                response.mStringPosition = command.mStringPosition;
                // Move our cursor below the response.
                mTypedText.mStringPosition.Y += mSpriteFont.MeasureString(mTypedText.mStringValue).Y *
                                                mTextScale;
                // Add response to the message log so that it may be displayed from the log.
                mMessageLogList.Add(response);
                EnableGodModeFunction();
            }

        }

        private static void EnableTargetQueue()
        {

            if (mTargetQueueState == DebugTargetQueue.TARGET_QUEUE_DISABLED)
            {
                mTargetQueueState = DebugTargetQueue.TARGET_QUEUE_ENABLED;
            }

            else if (mTargetQueueState == DebugTargetQueue.TARGET_QUEUE_ENABLED)
            {
                mTargetQueueState = DebugTargetQueue.TARGET_QUEUE_DISABLED;
            }
        }


        private static void EnableSpriteID()
        {

            if (mSpriteIDState == DebugSpriteID.SPRITEID_DISABLED)
            {
                mSpriteIDState = DebugSpriteID.SPRITEID_ENABLED;
            }

            else if (mSpriteIDState == DebugSpriteID.SPRITEID_ENABLED)
            {
                mSpriteIDState = DebugSpriteID.SPRITEID_DISABLED;
            }

        }




        // Allows to print debug information even inside of a loop at a set interval.
        public static void WriteLineAtInterval(string debugText, GameTime gameTime, float Interval)
        {
            // Check if we should print another message yet.
            if (mStartTime >= Interval)
            {
                var debugString = new TextBoxString();

                // Retain proper formatting and wordwrapping.
                debugString.mStringValue = ParseText(debugText);
                try
                {
                    debugString.mStringPosition = mTypedText.mStringPosition;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                // Push our typed text position down below our debug string.
                mTypedText.mStringPosition.Y += mSpriteFont.MeasureString(mTypedText.mStringValue).Y *
                                                  mTextScale;
                // Add the debug string to the message log list.
                mMessageLogList.Add(debugString);

                // Assure that we are scrolling properly.
                ScrollLog(debugString);

                // Reset Timer.
                mStartTime = 0.0f;
            }

        }

        public static void WriteLine(string debugString)
        {
            var debugText = new TextBoxString();
            // Retain proper formatting and wordwrapping.
            debugText.mStringValue = ParseText(debugString);

            try
            {
                debugText.mStringPosition = mTypedText.mStringPosition;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }
            // Move our typed text position to below the debug text.
            mTypedText.mStringPosition.Y += mSpriteFont.MeasureString(mTypedText.mStringValue).Y *
                                              mTextScale;
            // Add the debug text to the message log.
            mMessageLogList.Add(debugText);
            // Assure that we are properly scrolling.
            ScrollLog(debugText);
        }



        #endregion

        #region Formatting Functions


        private static void ScrollLog(TextBoxString textBoxString)
        {
            // If we get to the bottom of the console - the height of our font after scaling.
            if (textBoxString.mStringPosition.Y > TEXTBOX_HEIGHT - (mSpriteFont.MeasureString(mTypedText.mStringValue).Y * mTextScale))
            {
                // Take every message in the log and move it up one slot.
                for (int i = 0; i < mMessageLogList.Count; i++)
                {
                    TextBoxString temp = mMessageLogList[i];
                    temp.mStringPosition.Y -= mSpriteFont.MeasureString(mTypedText.mStringValue).Y * mTextScale;
                    mMessageLogList[i] = temp;
                }
                // Move our typed text up as well.
                mTypedText.mStringPosition.Y -= mSpriteFont.MeasureString(mTypedText.mStringValue).Y *
                           mTextScale;
            }
        }
        // This function provides word wrapping.
        private static String ParseText(String text)
        {
            String line = String.Empty;
            String returnString = String.Empty;
            String[] wordArray = text.Split(' ');

            foreach (String word in wordArray)
            {
                if (mSpriteFont.MeasureString(line + word).X * mTextScale > mTextBox.Width)
                {
                    returnString += line + '\n';
                    line = String.Empty;
                }
                line = line + word + ' ';
            }
            return returnString + line;
        }

        #endregion

        #region Debug State
        // Is the debug console active or not?
        public static void ToggleDebugState()
        {
            if (mDebugState == DebugState.ACTIVE)
            {
                mDebugState = DebugState.HIDDEN;
            }
            else if (mDebugState == DebugState.HIDDEN)
            {
                mDebugState = DebugState.ACTIVE;
            }

        }
        #endregion
    }
}
