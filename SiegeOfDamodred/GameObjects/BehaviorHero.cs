using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExternalTypes;
using GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;

namespace GameObjects
{
    public class BehaviorHero
    {
        private float AttackTimer;
        private float WaitTimer;
        Vector2 targetDirection;
        private MouseState mouseState;
        private Vector2 targetPosition;
        private Rectangle mouseRectangle;
        private Enemy targetEnemy;
        private bool attackingEnemy;
        private bool playingAttackAnimation;
        private bool isWaitingToAttack;
        private const float mHeroDeathSoundVolume = 0.5f;
        private const float mHeroWarCrySoundVolume = 0.5f;
        private bool isDying;
        private float mDeathAnimationTimer;
        private float mWarCryAnimationTimer;
       
        private bool isCasting;

        public BehaviorHero(float weight, Vector2 StartPosition)
        {
            targetPosition = StartPosition;
            attackingEnemy = false;
            isDying = false;
        }

        public bool IsCasting
        {
            get { return isCasting; }
            set { isCasting = value; }
        }


        public void Update(Hero actor)
        {
            

            Hero heroActor = (Hero)actor;  
            mWarCryAnimationTimer += heroActor.gameTime.ElapsedGameTime.Milliseconds;
           
            CheckVitalSigns(heroActor);
            HandleWarCries(heroActor);
            if (!isDying && !isCasting)
            {


                mouseState = Mouse.GetState();
                mouseRectangle = new Rectangle((int) (mouseState.X/GameObject.Scale),
                                               (int) (mouseState.Y/GameObject.Scale), 30, 30);


                ChangeSpriteAnimation(actor);



                if (mouseState.RightButton == ButtonState.Pressed)
                {


                    GameObject.mPotentialTargetListList.Reset();
                    for (int i = 0; i < GameObject.mPotentialTargetListList.GetCount(); i++)
                    {
                        GameObject temp = GameObject.mPotentialTargetListList.GetCurrent().gameObject;

                        if (temp is Enemy)
                        {
                            if (temp.Sprite.SpriteFrame.Contains(mouseRectangle))
                            {
                                targetPosition = new Vector2(temp.Sprite.WorldPosition.X/GameObject.Scale,
                                                             temp.Sprite.WorldPosition.Y/GameObject.Scale);
                                attackingEnemy = true;
                                targetEnemy = (Enemy) temp;
                                break;
                            }
                            else
                            {
                                attackingEnemy = false;
                            }
                        }

                        GameObject.mPotentialTargetListList.NextNode();
                    }
                    if (attackingEnemy == false)
                    {
                        targetPosition = new Vector2(mouseState.X/GameObject.Scale,
                                                     mouseState.Y/GameObject.Scale);
                    }
                }

                if (attackingEnemy && targetEnemy.Sprite.SpriteFrame.Intersects(heroActor.Sprite.SpriteFrame))
                {

                    Attack(heroActor, targetEnemy);
                }



                heroActor.SetHeroTarget(targetPosition);

            }

        }

        private void HandleWarCries(Hero heroActor)
        {
 
                PlayBattleCryAnimation(heroActor);
            

                 PlayIntmidateAnimation(heroActor);
         
            
           

        }

        private void CheckVitalSigns(Hero heroActor)
        {
            if (0 >= heroActor.HeroAttribute.CurrentHealthPoints)
            {
                mDeathAnimationTimer += heroActor.gameTime.ElapsedGameTime.Milliseconds;
                PlayHeroDeathSound(heroActor);
                isDying = true;
                BeginDying(heroActor);
            }
            else
            {
                isDying = false;
            }
        }

      public void  BeginDying(Hero heroActor)
        {

          if (mDeathAnimationTimer <= heroActor.HeroDeathAnimationFrames * heroActor.HeroDeathAnimationInterval 
              && (heroActor.SpriteState == SpriteState.ATTACK_LEFT || heroActor.SpriteState == SpriteState.MOVE_LEFT ||
              heroActor.SpriteState == SpriteState.IDLE_LEFT ))
          {
              heroActor.SpriteState = SpriteState.DEATH_LEFT;
          }
          else if (mDeathAnimationTimer <= heroActor.HeroDeathAnimationFrames * heroActor.HeroDeathAnimationInterval
              && (heroActor.SpriteState == SpriteState.ATTACK_RIGHT || heroActor.SpriteState == SpriteState.MOVE_RIGHT ||
              heroActor.SpriteState == SpriteState.IDLE_RIGHT))
          {
              heroActor.SpriteState = SpriteState.DEATH_RIGHT;
          }
          else if (mDeathAnimationTimer >= heroActor.HeroDeathAnimationFrames * heroActor.HeroDeathAnimationInterval)
          {
              heroActor.HasDied = true;
          }
        }


        public void Attack(Hero heroActor, Enemy target)
        {

            WaitTimer += heroActor.gameTime.ElapsedGameTime.Milliseconds;

            if (WaitTimer <= HeroAttribute.AttackSpeed)
            {
                playingAttackAnimation = false;
                if (heroActor.SpriteState == SpriteState.IDLE_LEFT)
                {
                    heroActor.SpriteState = SpriteState.ATTACK_LEFT;
                }
                else
                {
                    heroActor.SpriteState = SpriteState.ATTACK_RIGHT;
                }
                
                AttackTimer = 0;
            }
            else
            {
                playingAttackAnimation = true;
                AttackTimer += heroActor.gameTime.ElapsedGameTime.Milliseconds;
                if (AttackTimer >= heroActor.HeroAttackAnimationInterval * heroActor.HeroAttackAnimationFrames)
                {
                    if (playingAttackAnimation && (heroActor.SpriteState == SpriteState.ATTACK_LEFT ||
             heroActor.SpriteState == SpriteState.ATTACK_RIGHT))
                    {
                        heroActor.HeroAttribute.Attack(target);
                        AudioController.HeroAttack1SoundEffectInstance.Play();
                        WaitTimer = 0;
                    }




                }
            }


        }

        public void ChangeSpriteAnimation(Hero actor)
        {
            if (actor is Hero)
            {
                Hero heroActor = (Hero)actor;

                targetDirection = heroActor.GetTarget() - heroActor.Sprite.WorldPosition;
                heroActor.Direction = heroActor.Direction + targetDirection;

                if (heroActor.GetTarget().X - heroActor.Sprite.WorldPosition.X >= 0)
                {
                    heroActor.SpriteState = SpriteState.MOVE_RIGHT;
                }
                else if (heroActor.GetTarget().X - heroActor.Sprite.WorldPosition.X <= 0)
                {
                    heroActor.SpriteState = SpriteState.MOVE_LEFT;
                }
                if (heroActor.Sprite.WorldPosition.Y <= heroActor.GetTarget().Y + 10 &&
                heroActor.Sprite.WorldPosition.Y >= heroActor.GetTarget().Y - 10 &&
                heroActor.Sprite.WorldPosition.X <= heroActor.GetTarget().X + 10 &&
                heroActor.Sprite.WorldPosition.X >= heroActor.GetTarget().X - 10)
                {
                    if (heroActor.SpriteState == SpriteState.MOVE_LEFT)
                    {
                        heroActor.SpriteState = SpriteState.IDLE_LEFT;
                    }
                    else if (heroActor.SpriteState == SpriteState.MOVE_RIGHT)
                    {
                        heroActor.SpriteState = SpriteState.IDLE_RIGHT;
                    }
                    heroActor.HeroAttribute.Speed = 0;
                }
                if (attackingEnemy == true && targetEnemy.Sprite.SpriteFrame.Intersects(heroActor.Sprite.SpriteFrame))
                {
                    if (heroActor.SpriteState == SpriteState.MOVE_LEFT)
                    {
                        heroActor.SpriteState = SpriteState.ATTACK_LEFT;
                        heroActor.HeroAttribute.Speed = 0;
                    }
                    else if (heroActor.SpriteState == SpriteState.MOVE_RIGHT)
                    {
                        heroActor.SpriteState = SpriteState.ATTACK_RIGHT;
                        heroActor.HeroAttribute.Speed = 0;
                    }
                }
                else if (heroActor.SpriteState == SpriteState.MOVE_LEFT || heroActor.SpriteState == SpriteState.MOVE_RIGHT)
                {
                    //we are moving

                    heroActor.HeroAttribute.Speed = heroActor.HeroAttribute.DefaultSpeed;
                }

            }

        }

        private void PlayBattleCryAnimation(Hero heroActor)
        {

            if (HeroAttribute.BattleCrySpellState == BattleCrySpellState.ACTIVE && !isCasting)
            {
                mWarCryAnimationTimer = 0;
                PlayHeroWarCrySound();
                isCasting = true;
            }

            if (mWarCryAnimationTimer <= heroActor.BattleCryAnimationFrames * heroActor.BattleCryAnimationInterval && isCasting )
            {
                if (heroActor.SpriteState == SpriteState.IDLE_LEFT
                    || heroActor.SpriteState == SpriteState.MOVE_LEFT
                    || heroActor.SpriteState == SpriteState.ATTACK_LEFT)
                {
                    heroActor.SpriteState = SpriteState.BATTLECRY_LEFT;
                  
                }
                else if (heroActor.SpriteState == SpriteState.IDLE_RIGHT
                    || heroActor.SpriteState == SpriteState.MOVE_RIGHT
                    || heroActor.SpriteState == SpriteState.ATTACK_RIGHT)
                {
                    heroActor.SpriteState = SpriteState.BATTLECRY_RIGHT;
                   
                }

            }
            else if (mWarCryAnimationTimer >= (heroActor.BattleCryAnimationFrames * heroActor.BattleCryAnimationInterval) && isCasting)
            {
                isCasting = false;
            }

        }
                
            
        

        private void PlayIntmidateAnimation(Hero heroActor)
        {
            if (HeroAttribute.IntimidateSpellState == IntimidateSpellState.ACTIVE && !isCasting)
            {
                mWarCryAnimationTimer = 0;
                PlayHeroWarCrySound();
                isCasting = true;
            }

            if (mWarCryAnimationTimer <= heroActor.IntimidateAnimationFrames * heroActor.IntimitadeAnimationInterval && isCasting)
            {
                if (heroActor.SpriteState == SpriteState.IDLE_LEFT
                    || heroActor.SpriteState == SpriteState.MOVE_LEFT
                    || heroActor.SpriteState == SpriteState.ATTACK_LEFT)
                {
                    heroActor.SpriteState = SpriteState.INTIMIDATE_LEFT;

                }
                else if (heroActor.SpriteState == SpriteState.IDLE_RIGHT
                    || heroActor.SpriteState == SpriteState.MOVE_RIGHT
                    || heroActor.SpriteState == SpriteState.ATTACK_RIGHT)
                {
                    heroActor.SpriteState = SpriteState.INTIMIDATE_RIGHT;

                }

            }
            else if (mWarCryAnimationTimer >= (heroActor.IntimidateAnimationFrames * heroActor.IntimitadeAnimationInterval) && isCasting)
            {
                isCasting = false;
            }
        }

        public void PlayHeroWarCrySound()
        {
            AudioController.HeroWarCrySoundEffectInstance.Volume = mHeroWarCrySoundVolume;
            AudioController.HeroWarCrySoundEffectInstance.Play();
        }


        public void PlayHeroDeathSound(Hero heroActor)
        {

            AudioController.HeroDeathSoundEffectInstance.Volume = mHeroDeathSoundVolume;
            AudioController.HeroDeathSoundEffectInstance.Play();

        }

    }
}


