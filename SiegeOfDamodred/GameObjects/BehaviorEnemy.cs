using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExternalTypes;
using GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;

namespace GameObjects
{
    public class BehaviorEnemy
    {
        private Vector2 targetDirection;



        private float WaitTimer;
        private float AnimationTimer;

        private float TimeStamp;
        private float FinalPosition;
        private float InitialPosition;
        private float DeltaX;
        private GameObject currentTarget;
        private bool isInQueue;
        private float weight;
        private int counter;
        private bool isAttacking;
        private Random randomNumberGenerator;
        private bool isAttackingCastle;
        private bool isDying;
        private float mDeathAnimationTimer;

        #region Sound Volumes

        private const float mReaperAnimationSoundVolume = 0.4f;
        private const float mBansheeAnimationSoundVolume = 0.4f;
        private const float mGogAnimationSoundVolume = 0.4f;
        private const float mImpAnimationSoundVolume = 0.4f;
        private const float mDoomHoundAnimationSoundVolume = 0.4f;

        private const float mReaperDeathSoundVolume = 0.4f;
        private const float mGogDeathSoundVolume = 0.4f;
        private const float mBansheeDeathSoundVolume = 0.4f;
        private const float mImpDeathSoundVolume = 0.4f;
        private const float mDoomHoundDeathSoundVolume = 0.4f;


        private const float mMeleeHitSoundVolume = 0.4f;

        #endregion Sound Volumes

        public BehaviorEnemy(float weight)
        {
            randomNumberGenerator = new Random();
            WaitTimer = 0;
            AnimationTimer = 0;
            FinalPosition = 0.0f;
            InitialPosition = 0.0f;
            DeltaX = 0.0f;
            TimeStamp = 0.0f;
            this.weight = weight;
            isAttacking = false;
        }


        public void PrintGlobalList()
        {
            GameObject.mPotentialTargetListList.Reset();
            for (int i = 0; i < GameObject.mPotentialTargetListList.GetCount(); i++)
            {
                Console.Write(GameObject.mPotentialTargetListList.GetCurrent().gameObject.ToString() + "  ");
                Console.WriteLine(" ");
                GameObject.mPotentialTargetListList.NextNode();
            }
        }

        public void Update(Enemy enemyActor)
        {

            CheckVitalSigns(enemyActor);

            if (!isDying)
            {
                if (enemyActor.CombatType == CombatType.RANGED)
                {
                    RangedAttackCastle(enemyActor);
                }
                if (enemyActor.CombatType == CombatType.MELEE)
                {
                    MeleeAttackCastle(enemyActor);
                }
                // Hmmm.. What type of attacks do I have again?
                if (enemyActor.CombatType == CombatType.MELEE && !isAttackingCastle)
                {
                    HandleMeleeUnitBehavior(enemyActor);
                }
                else if (enemyActor.CombatType == CombatType.RANGED && !isAttackingCastle)
                {
                    HandleRangedUnitBehavior(enemyActor);
                }

                targetDirection = enemyActor.CurrentTarget - enemyActor.Sprite.WorldPosition;
                targetDirection.Normalize();

                enemyActor.Direction = enemyActor.Direction + targetDirection * this.weight;


                ScanGlobalList(enemyActor);

                MaintainCurrentTarget(enemyActor);

            }
        }



        #region Behavior Functions

        #region Melee Behavior
        // TODO: Handle Melee Behavior
        public void HandleMeleeUnitBehavior(Enemy enemyActor)
        {

            TimeStamp += enemyActor.gameTime.ElapsedGameTime.Milliseconds;


            UpdateUnitSpeed(enemyActor);

            // Always Move To CurrentTarget Unless I'm attacking
            if (!isAttacking)
            {
                MoveTowardsTargetCoordinate(enemyActor);
            }

            StopWhenAtTargetCoordinate(enemyActor);


            // If I have a list of targets. And I am melee
            if (!enemyActor.TargetQueue.IsEmpty() && enemyActor.CombatType == CombatType.MELEE)
            {
                // Check if I made it to my target's sprite frame.
                CheckMeleeAttackStatus(enemyActor);


                if (enemyActor.EnemyAggroMode == EnemyAggroMode.ATTACKING)
                {
                    //I'm sitting at my enemy's feet. I'm committed, lock me into attack mode.
                    isAttacking = true;
                    AnimationTimer += enemyActor.gameTime.ElapsedGameTime.Milliseconds;


                    // If I'm attacking then I should play my Attack animation from start to finish.
                    if (AnimationTimer <= GetAnimationLength(enemyActor))
                    {
                        PlayAttackAnimation(enemyActor);

                        //    Console.WriteLine("Now I'm playing attacking Animation");
                    }
                    else
                    {
                        // Then I should apply my damage to the current target
                        Console.WriteLine("Applying Damage");
                        //TODO:       PlayUnitAttackLandedAudio(friendlyActor);
                        PlayMeleeUnitAttackLandedAudio(enemyActor);
                        //TODO:   PlayUnitStruckAudio((Friendly)enemyActor.TargetQueue.PeekFirst());


                        if (enemyActor.TargetQueue.PeekFirst() is Hero)
                        {
                            PlayUnitStruckAudio((Hero)enemyActor.TargetQueue.PeekFirst());
                        }
                        else if (enemyActor.TargetQueue.PeekFirst() is Friendly)
                        {
                            PlayUnitStruckAudio((Friendly)enemyActor.TargetQueue.PeekFirst());
                        }


                        enemyActor.EnemyAttribute.Attack();
                        enemyActor.EnemyAggroMode = EnemyAggroMode.IDLING;
                        WaitTimer = 0;
                    }
                }

                // Now I'm done playing my animation for attack. Now play idle while I cool off
                else if (enemyActor.EnemyAggroMode == EnemyAggroMode.IDLING && WaitTimer <= enemyActor.EnemyAttribute.AttackSpeed)
                {
                    WaitTimer += enemyActor.gameTime.ElapsedGameTime.Milliseconds;
                    PlayIdleAnimationFromAttacking(enemyActor);
                }
                // After I wait I should play my attack animation again 
                else if (WaitTimer >= enemyActor.EnemyAttribute.AttackSpeed)
                {
                    AnimationTimer = 0;
                    enemyActor.EnemyAggroMode = EnemyAggroMode.ATTACKING;
                }
            }

            else
            {
                //If we are done slaughtering our foes.
                enemyActor.EnemyAggroMode = EnemyAggroMode.MOVING;
                isAttacking = false;

            }
        }
        #endregion

        #region Ranged Behavior
        // TODO: Handle Melee Behavior

        public void HandleRangedUnitBehavior(Enemy enemyActor)
        {

            TimeStamp += enemyActor.gameTime.ElapsedGameTime.Milliseconds;


            UpdateUnitSpeed(enemyActor);

            // Always Move To CurrentTarget Unless I'm attacking
            if (!isAttacking)
            {
                enemyActor.EnemyAttribute.UnitSpeed = 1.0f;

                MoveTowardsTargetCoordinate(enemyActor);
            }
            StopWhenAtTargetCoordinate(enemyActor);


            // If I have a list of targets. And I am Ranged
            if (!enemyActor.TargetQueue.IsEmpty() && enemyActor.CombatType == CombatType.RANGED)
            {
                // Check if I made it to my target's sprite frame.
                CheckRangedAttackStatus(enemyActor);


                if (enemyActor.EnemyAggroMode == EnemyAggroMode.ATTACKING)
                {
                    //I'm sitting at my enemy's feet. I'm committed, lock me into attack mode.
                    isAttacking = true;
                    AnimationTimer += enemyActor.gameTime.ElapsedGameTime.Milliseconds;


                    // If I'm attacking then I should play my Attack animation from start to finish.
                    if (AnimationTimer <= GetAnimationLength(enemyActor))
                    {
                        PlayAttackAnimation(enemyActor);

                        Console.WriteLine("Now I'm playing attacking Animation");
                    }
                    else
                    {
                        // Then I should apply my damage to the current target
                        Console.WriteLine("Applying Damage");
                        //TODO:   PlayUnitAttackLandedAudio(friendlyActor);
                        PlayMeleeUnitAttackLandedAudio(enemyActor);
                        enemyActor.EnemyAttribute.Attack();
                        enemyActor.EnemyAggroMode = EnemyAggroMode.IDLING;
                        WaitTimer = 0;
                    }
                }
                // Now I'm done playing my animation for attack. Now play idle while I cool off
                else if (enemyActor.EnemyAggroMode == EnemyAggroMode.IDLING && WaitTimer <= enemyActor.EnemyAttribute.AttackSpeed)
                {
                    WaitTimer += enemyActor.gameTime.ElapsedGameTime.Milliseconds;
                    PlayIdleAnimationFromAttacking(enemyActor);
                }
                // After I wait I should play my attack animation again 
                else if (WaitTimer >= enemyActor.EnemyAttribute.AttackSpeed)
                {
                    AnimationTimer = 0;
                    enemyActor.EnemyAggroMode = EnemyAggroMode.ATTACKING;
                }
            }
            else
            {
                //If we are done slaughtering our foes.
                enemyActor.EnemyAggroMode = EnemyAggroMode.MOVING;
                isAttacking = false;
                UpdateUnitSpeed(enemyActor);
                StopWhenAtTargetCoordinate(enemyActor);
            }
        }

        #endregion


        #region Attack Castle

        private void MeleeAttackCastle(Enemy enemyActor)
        {
            if (enemyActor.TargetQueue.IsEmpty() && enemyActor.Sprite.SpriteFrame.Intersects(enemyActor.DefaultTarget))
            {



                if (isAttackingCastle == false)
                {
                    enemyActor.EnemyAggroMode = EnemyAggroMode.ATTACKING;
                    AnimationTimer = 0;
                    isAttackingCastle = true;
                }


                // Check if I made it to my target's sprite frame.
                CheckMeleeStatusCastle(enemyActor);


                if (enemyActor.EnemyAggroMode == EnemyAggroMode.ATTACKING)
                {
                    AnimationTimer += enemyActor.gameTime.ElapsedGameTime.Milliseconds;
                    // If I'm attacking then I should play my Attack animation from start to finish.
                    if (AnimationTimer <= GetAnimationLength(enemyActor))
                    {
                        PlayAttackAnimation(enemyActor);

                        //    Console.WriteLine("Now I'm playing attacking Animation");
                    }
                    else if (AnimationTimer >= GetAnimationLength(enemyActor))
                    {
                        // Then I should apply my damage to the current target
                        Console.WriteLine("Applying Damage");
                        //TODO:       PlayUnitAttackLandedAudio(friendlyActor);
                        PlayMeleeUnitAttackLandedAudio(enemyActor);

                        enemyActor.EnemyAttribute.Attack();
                        enemyActor.EnemyAggroMode = EnemyAggroMode.IDLING;
                        WaitTimer = 0;
                    }

                }
                // Now I'm done playing my animation for attack. Now play idle while I cool off
                else if (enemyActor.EnemyAggroMode == EnemyAggroMode.IDLING &&
                    WaitTimer <= enemyActor.EnemyAttribute.AttackSpeed)
                {
                    WaitTimer += enemyActor.gameTime.ElapsedGameTime.Milliseconds;
                    PlayIdleAnimationFromAttacking(enemyActor);
                }
                // After I wait I should play my attack animation again 
                else if (WaitTimer >= enemyActor.EnemyAttribute.AttackSpeed)
                {
                    AnimationTimer = 0;
                    enemyActor.EnemyAggroMode = EnemyAggroMode.ATTACKING;
                }

            }
            else if (isAttackingCastle)
            {
                //If we are done slaughtering our foes.
                enemyActor.EnemyAggroMode = EnemyAggroMode.MOVING;

                isAttackingCastle = false;

            }

        }



        public void CheckMeleeStatusCastle(Enemy enemyActor)
        {
            if (enemyActor.Sprite.SpriteFrame.Intersects(enemyActor.DefaultTarget) &&
                 enemyActor.EnemyAggroMode != EnemyAggroMode.IDLING)
            {
                // I should start attacking
                enemyActor.EnemyAggroMode = EnemyAggroMode.ATTACKING;
                UpdateUnitSpeed(enemyActor);
            }
            if (!enemyActor.Sprite.SpriteFrame.Intersects(enemyActor.DefaultTarget))
            {
                // I should be moving :)
                enemyActor.EnemyAggroMode = EnemyAggroMode.MOVING;
                UpdateUnitSpeed(enemyActor);
                MoveTowardsTargetCoordinate(enemyActor);
            }
        }


        public void RangedAttackCastle(Enemy enemyActor)
        {
            if (enemyActor.TargetQueue.IsEmpty() && enemyActor.FieldOfView.Intersects(enemyActor.DefaultTarget))
            {



                if (isAttackingCastle == false)
                {
                    enemyActor.EnemyAggroMode = EnemyAggroMode.ATTACKING;
                    AnimationTimer = 0;
                    isAttackingCastle = true;
                }


                // Check if I made it to my target's sprite frame.
                CheckRangedStatusCastle(enemyActor);


                if (enemyActor.EnemyAggroMode == EnemyAggroMode.ATTACKING)
                {
                    AnimationTimer += enemyActor.gameTime.ElapsedGameTime.Milliseconds;
                    // If I'm attacking then I should play my Attack animation from start to finish.
                    if (AnimationTimer <= GetAnimationLength(enemyActor))
                    {
                        PlayAttackAnimation(enemyActor);

                        //    Console.WriteLine("Now I'm playing attacking Animation");
                    }
                    else if (AnimationTimer >= GetAnimationLength(enemyActor))
                    {
                        // Then I should apply my damage to the current target
                        Console.WriteLine("Applying Damage");
                        //TODO:       PlayUnitAttackLandedAudio(friendlyActor);
                        PlayMeleeUnitAttackLandedAudio(enemyActor);

                        enemyActor.EnemyAttribute.Attack();
                        enemyActor.EnemyAggroMode = EnemyAggroMode.IDLING;
                        WaitTimer = 0;
                    }

                }
                // Now I'm done playing my animation for attack. Now play idle while I cool off
                else if (enemyActor.EnemyAggroMode == EnemyAggroMode.IDLING &&
                    WaitTimer <= enemyActor.EnemyAttribute.AttackSpeed)
                {
                    WaitTimer += enemyActor.gameTime.ElapsedGameTime.Milliseconds;
                    PlayIdleAnimationFromAttacking(enemyActor);
                }
                // After I wait I should play my attack animation again 
                else if (WaitTimer >= enemyActor.EnemyAttribute.AttackSpeed)
                {
                    AnimationTimer = 0;
                    enemyActor.EnemyAggroMode = EnemyAggroMode.ATTACKING;
                }

            }
            else if (isAttackingCastle)
            {
                //If we are done slaughtering our foes.
                enemyActor.EnemyAggroMode = EnemyAggroMode.MOVING;

                isAttackingCastle = false;

            }
        }


        public void CheckRangedStatusCastle(Enemy enemyActor)
        {
            if (enemyActor.FieldOfView.Intersects(enemyActor.DefaultTarget) &&
                 enemyActor.EnemyAggroMode != EnemyAggroMode.IDLING)
            {
                // I should start attacking
                enemyActor.EnemyAggroMode = EnemyAggroMode.ATTACKING;
                UpdateUnitSpeed(enemyActor);
            }
            if (!enemyActor.FieldOfView.Intersects(enemyActor.DefaultTarget))
            {
                // I should be moving :)
                enemyActor.EnemyAggroMode = EnemyAggroMode.MOVING;
                UpdateUnitSpeed(enemyActor);
                MoveTowardsTargetCoordinate(enemyActor);
            }
        }
        #endregion

        #endregion

        #region Unit Status Changing Functions

        private void CheckVitalSigns(Enemy enemyActor)
        {
            if (0 >= enemyActor.EnemyAttribute.CurrentHealthPoints)
            {
                mDeathAnimationTimer += enemyActor.gameTime.ElapsedGameTime.Milliseconds;
                PlayUnitDeathAudio(enemyActor);
                isDying = true;
                BeginDying(enemyActor);
            }
            else
            {
                isDying = false;
            }

        }


        // TODO: Check Melee Attack Status
        public void CheckMeleeAttackStatus(Enemy enemyActor)
        {
            // If I'm colliding with my target
            if (enemyActor.Sprite.SpriteFrame.Intersects(enemyActor.TargetQueue.PeekFirst().Sprite.SpriteFrame) &&
                 enemyActor.EnemyAggroMode != EnemyAggroMode.IDLING)
            {
                // I should start attacking
                enemyActor.EnemyAggroMode = EnemyAggroMode.ATTACKING;
                UpdateUnitSpeed(enemyActor);
            }
            // But if I'm NOT colliding with my target
            else if (!enemyActor.Sprite.SpriteFrame.Intersects(enemyActor.TargetQueue.PeekFirst().Sprite.SpriteFrame))
            {
                // I should be moving :)
                enemyActor.EnemyAggroMode = EnemyAggroMode.MOVING;
                UpdateUnitSpeed(enemyActor);
                MoveTowardsTargetCoordinate(enemyActor);

            }
        }


        // TODO Check Ranged Attack Status

        public void CheckRangedAttackStatus(Enemy enemyActor)
        {
            // If I'm colliding with my target
            if (enemyActor.FieldOfView.Intersects(enemyActor.TargetQueue.PeekFirst().Sprite.SpriteFrame) &&
                 enemyActor.EnemyAggroMode != EnemyAggroMode.IDLING)
            {
                // I should start attacking

                enemyActor.EnemyAggroMode = EnemyAggroMode.ATTACKING;
                UpdateUnitSpeed(enemyActor);
            }
            // But if I'm NOT colliding with my target
            else if (!enemyActor.FieldOfView.Intersects(enemyActor.TargetQueue.PeekFirst().Sprite.SpriteFrame))
            {
                // I should be moving :)
                enemyActor.EnemyAggroMode = EnemyAggroMode.MOVING;
                UpdateUnitSpeed(enemyActor);
                MoveTowardsTargetCoordinate(enemyActor);

            }
        }

        #endregion

        #region Movement Functions

        // Decides if we are moving left or right based on time stamps taken at different frames of reference.
        public void MoveTowardsTargetCoordinate(Enemy enemyActor)
        {


            if (TimeStamp < 100.0f)
            {

                InitialPosition = enemyActor.Sprite.WorldPosition.X;
            }

            if (TimeStamp >= 200.0f)
            {

                FinalPosition = enemyActor.Sprite.WorldPosition.X;
                DeltaX = FinalPosition - InitialPosition;

                TimeStamp = 0.0f;
                if (DeltaX > 0)
                {
                    enemyActor.SpriteState = SpriteState.MOVE_RIGHT;

                }
                if (DeltaX <= 0)
                {
                    enemyActor.SpriteState = SpriteState.MOVE_LEFT;

                }
            }
        }
        // TODO: Stop When At CurrentTarget
        // Will cause the unit to stop moving when target location is reached.
        public void StopWhenAtTargetCoordinate(Enemy enemyActor)
        {


            if ((enemyActor.Sprite.WorldPosition.X >= enemyActor.CurrentTarget.X - 30 &&
                     enemyActor.Sprite.WorldPosition.Y >= enemyActor.CurrentTarget.Y - 30) &&
                     (enemyActor.Sprite.WorldPosition.X <= enemyActor.CurrentTarget.X + 30 &&
                     enemyActor.Sprite.WorldPosition.Y <= enemyActor.CurrentTarget.Y + 30))
            {
                enemyActor.EnemyAttribute.UnitSpeed = 0.0f;
                //TODO:   PlayIdleAnimationFromMotion(friendlyActor);
            }

            //if (enemyActor.Sprite.SpriteFrame.Intersects(enemyActor.TargetQueue.PeekFirst().Sprite.SpriteFrame))
            //{
            //    enemyActor.EnemyAttribute.UnitSpeed = 0.0f;
            //}
        }

        // TODO: Update Unit UnitSpeed
        public void UpdateUnitSpeed(Enemy enemyActor)
        {

            switch (enemyActor.EnemyAggroMode)
            {
                case EnemyAggroMode.ATTACKING:
                    enemyActor.EnemyAttribute.UnitSpeed = 0.0f;
                    break;
                case EnemyAggroMode.IDLING:
                    enemyActor.EnemyAttribute.UnitSpeed = 0.0f;
                    break;
                case EnemyAggroMode.MOVING:
                    enemyActor.EnemyAttribute.UnitSpeed = 1.0f;
                    break;
                default:
                    break;
            }
        }


        #endregion

        #region Animation Functions

        private void BeginDying(Enemy enemyActor)
        {
            switch (enemyActor.CreatureType)
            {

                case ObjectType.BANSHEE:
                    if (mDeathAnimationTimer <=
                        enemyActor.BansheeDeathAnimationFrames * enemyActor.BansheeDeathAnimationInterval
                        &&
                        (enemyActor.SpriteState == SpriteState.ATTACK_LEFT ||
                         enemyActor.SpriteState == SpriteState.MOVE_LEFT
                         || enemyActor.SpriteState == SpriteState.IDLE_LEFT))
                    {
                        enemyActor.SpriteState = SpriteState.DEATH_LEFT;

                    }

                    else if (mDeathAnimationTimer <= enemyActor.BansheeDeathAnimationFrames * enemyActor.BansheeDeathAnimationInterval
                             &&
                             (enemyActor.SpriteState == SpriteState.ATTACK_RIGHT ||
                              enemyActor.SpriteState == SpriteState.MOVE_RIGHT
                         || enemyActor.SpriteState == SpriteState.IDLE_RIGHT))
                    {
                        enemyActor.SpriteState = SpriteState.DEATH_RIGHT;
                    }
                    else if (mDeathAnimationTimer >=
                             enemyActor.BansheeDeathAnimationFrames *
                             enemyActor.BansheeDeathAnimationInterval)
                    {
                        enemyActor.HasDied = true;
                    }


                    break;
                case ObjectType.GOG:

                    if (mDeathAnimationTimer <=
                        enemyActor.GogDeathAnimationFrames * enemyActor.GogDeathAnimationInterval
                        &&
                        (enemyActor.SpriteState == SpriteState.ATTACK_LEFT ||
                         enemyActor.SpriteState == SpriteState.MOVE_LEFT
                          || enemyActor.SpriteState == SpriteState.IDLE_LEFT))
                    {
                        enemyActor.SpriteState = SpriteState.DEATH_LEFT;
                    }

                    else if (mDeathAnimationTimer <=
                             enemyActor.GogDeathAnimationFrames *
                             enemyActor.GogDeathAnimationInterval
                             &&
                             (enemyActor.SpriteState == SpriteState.ATTACK_RIGHT ||
                              enemyActor.SpriteState == SpriteState.MOVE_RIGHT
                         || enemyActor.SpriteState == SpriteState.IDLE_RIGHT))
                    {
                        enemyActor.SpriteState = SpriteState.DEATH_RIGHT;
                    }
                    else if (mDeathAnimationTimer >=
                             enemyActor.GogDeathAnimationFrames *
                             enemyActor.GogDeathAnimationInterval)
                    {
                        enemyActor.HasDied = true;
                    }

                    break;
                case ObjectType.REAPER:
                    if (mDeathAnimationTimer <=
                        enemyActor.ReaperDeathAnimationFrames * enemyActor.ReaperDeathAnimationInterval
                        &&
                        (enemyActor.SpriteState == SpriteState.ATTACK_LEFT ||
                         enemyActor.SpriteState == SpriteState.MOVE_LEFT
                          || enemyActor.SpriteState == SpriteState.IDLE_LEFT))
                    {
                        enemyActor.SpriteState = SpriteState.DEATH_LEFT;
                    }

                    else if (mDeathAnimationTimer <=
                             enemyActor.ReaperDeathAnimationFrames * enemyActor.ReaperDeathAnimationInterval
                             &&
                             (enemyActor.SpriteState == SpriteState.ATTACK_RIGHT ||
                              enemyActor.SpriteState == SpriteState.MOVE_RIGHT
                         || enemyActor.SpriteState == SpriteState.IDLE_RIGHT))
                    {
                        enemyActor.SpriteState = SpriteState.DEATH_RIGHT;
                    }
                    else if (mDeathAnimationTimer >=
                             enemyActor.ReaperDeathAnimationFrames *
                             enemyActor.ReaperDeathAnimationInterval)
                    {
                        enemyActor.HasDied = true;
                    }

                    break;
                case ObjectType.IMP:

                    if (mDeathAnimationTimer <=
                        enemyActor.ImpDeathAnimationFrames * enemyActor.ImpDeathAnimationInterval
                        &&
                        (enemyActor.SpriteState == SpriteState.ATTACK_LEFT ||
                         enemyActor.SpriteState == SpriteState.MOVE_LEFT
                          || enemyActor.SpriteState == SpriteState.IDLE_LEFT))
                    {
                        enemyActor.SpriteState = SpriteState.DEATH_LEFT;
                    }

                    else if (mDeathAnimationTimer <=
                             enemyActor.ImpDeathAnimationFrames * enemyActor.ImpDeathAnimationInterval
                             &&
                             (enemyActor.SpriteState == SpriteState.ATTACK_RIGHT ||
                              enemyActor.SpriteState == SpriteState.MOVE_RIGHT
                         || enemyActor.SpriteState == SpriteState.IDLE_RIGHT))
                    {
                        enemyActor.SpriteState = SpriteState.DEATH_RIGHT;
                    }
                    else if (mDeathAnimationTimer >=
                             enemyActor.ImpDeathAnimationFrames *
                             enemyActor.ImpDeathAnimationInterval)
                    {
                        enemyActor.HasDied = true;
                    }

                    break;
                case ObjectType.DOOM_HOUND:

                    if (mDeathAnimationTimer <=
                        enemyActor.DoomHoundDeathAnimationFrames * enemyActor.DoomHoundDeathAnimationInterval
                        &&
                        (enemyActor.SpriteState == SpriteState.ATTACK_LEFT ||
                         enemyActor.SpriteState == SpriteState.MOVE_LEFT
                          || enemyActor.SpriteState == SpriteState.IDLE_LEFT))
                    {
                        enemyActor.SpriteState = SpriteState.DEATH_LEFT;
                    }

                    else if (mDeathAnimationTimer <=
                             enemyActor.DoomHoundDeathAnimationFrames * enemyActor.DoomHoundDeathAnimationInterval
                             &&
                             (enemyActor.SpriteState == SpriteState.ATTACK_RIGHT ||
                              enemyActor.SpriteState == SpriteState.MOVE_RIGHT
                         || enemyActor.SpriteState == SpriteState.IDLE_RIGHT))
                    {
                        enemyActor.SpriteState = SpriteState.DEATH_RIGHT;
                    }
                    else if (mDeathAnimationTimer >=
                             enemyActor.DoomHoundDeathAnimationFrames *
                             enemyActor.DoomHoundDeathAnimationInterval)
                    {
                        enemyActor.HasDied = true;
                    }

                    break;

                default:
                    break;
            }
        }


        // TODO: Get Animation Length
        //Gets how long my attack animation lasts
        public float GetAnimationLength(Enemy enemyActor)
        {
            switch (enemyActor.CreatureType)
            {

                case ObjectType.BANSHEE:
                    return enemyActor.BansheeAttackAnimationFrames * enemyActor.BansheeAttackAnimationInterval;
                    break;
                case ObjectType.REAPER:
                    return enemyActor.ReaperAttackAnimationFrames * enemyActor.ReaperAttackAnimationInterval;
                    break;
                case ObjectType.GOG:
                    return enemyActor.GogAttackAnimationFrames * enemyActor.GogAttackAnimationInterval;
                    break;
                case ObjectType.IMP:
                    return enemyActor.ImpAttackAnimationFrames * enemyActor.ImpAttackAnimationInterval;
                    break;
                case ObjectType.DOOM_HOUND:
                    return enemyActor.DoomHoundAttackAnimationFrames * enemyActor.DoomHoundAttackAnimationInterval;
                    break;

                default:
                    return 0.0f;
                    break;
            }
        }
        // TODO: Play Attack Animation
        // Will play Idle Animation for duration of animation
        public void PlayAttackAnimation(Enemy enemyActor)
        {
            switch (enemyActor.CreatureType)
            {

                case ObjectType.BANSHEE:
                    if (enemyActor.SpriteState == SpriteState.IDLE_LEFT)
                    {
                        enemyActor.SpriteState = SpriteState.ATTACK_LEFT;
                    }
                    else if (enemyActor.SpriteState == SpriteState.IDLE_RIGHT)
                    {
                        enemyActor.SpriteState = SpriteState.ATTACK_RIGHT;
                    }
                    else if (enemyActor.SpriteState == SpriteState.MOVE_LEFT)
                    {
                        enemyActor.SpriteState = SpriteState.ATTACK_LEFT;
                    }
                    else if (enemyActor.SpriteState == SpriteState.MOVE_RIGHT)
                    {
                        enemyActor.SpriteState = SpriteState.ATTACK_RIGHT;
                    }
                    // TODO:   PlayUnitAnimationAudio(friendlyActor);
                    break;
                case ObjectType.REAPER:
                    if (enemyActor.SpriteState == SpriteState.IDLE_LEFT)
                    {
                        enemyActor.SpriteState = SpriteState.ATTACK_LEFT;
                    }
                    else if (enemyActor.SpriteState == SpriteState.IDLE_RIGHT)
                    {
                        enemyActor.SpriteState = SpriteState.ATTACK_RIGHT;
                    }
                    else if (enemyActor.SpriteState == SpriteState.MOVE_LEFT)
                    {
                        enemyActor.SpriteState = SpriteState.ATTACK_LEFT;
                    }
                    else if (enemyActor.SpriteState == SpriteState.MOVE_RIGHT)
                    {
                        enemyActor.SpriteState = SpriteState.ATTACK_RIGHT;
                    }
                    // TODO:   PlayUnitAnimationAudio(friendlyActor);
                    break;
                case ObjectType.GOG:
                    if (enemyActor.SpriteState == SpriteState.IDLE_LEFT)
                    {
                        enemyActor.SpriteState = SpriteState.ATTACK_LEFT;
                    }
                    else if (enemyActor.SpriteState == SpriteState.IDLE_RIGHT)
                    {
                        enemyActor.SpriteState = SpriteState.ATTACK_RIGHT;
                    }
                    else if (enemyActor.SpriteState == SpriteState.MOVE_LEFT)
                    {
                        enemyActor.SpriteState = SpriteState.ATTACK_LEFT;
                    }
                    else if (enemyActor.SpriteState == SpriteState.MOVE_RIGHT)
                    {
                        enemyActor.SpriteState = SpriteState.ATTACK_RIGHT;
                    }
                    // TODO:       PlayUnitAnimationAudio(friendlyActor);
                    break;
                case ObjectType.IMP:
                    if (enemyActor.SpriteState == SpriteState.IDLE_LEFT)
                    {
                        enemyActor.SpriteState = SpriteState.ATTACK_LEFT;
                    }
                    else if (enemyActor.SpriteState == SpriteState.IDLE_RIGHT)
                    {
                        enemyActor.SpriteState = SpriteState.ATTACK_RIGHT;
                    }
                    else if (enemyActor.SpriteState == SpriteState.MOVE_LEFT)
                    {
                        enemyActor.SpriteState = SpriteState.ATTACK_LEFT;
                    }
                    else if (enemyActor.SpriteState == SpriteState.MOVE_RIGHT)
                    {
                        enemyActor.SpriteState = SpriteState.ATTACK_RIGHT;
                    }
                    // TODO:     PlayUnitAnimationAudio(friendlyActor);
                    break;
                case ObjectType.DOOM_HOUND:
                    if (enemyActor.SpriteState == SpriteState.IDLE_LEFT)
                    {
                        enemyActor.SpriteState = SpriteState.ATTACK_LEFT;
                    }
                    else if (enemyActor.SpriteState == SpriteState.IDLE_RIGHT)
                    {
                        enemyActor.SpriteState = SpriteState.ATTACK_RIGHT;
                    }
                    else if (enemyActor.SpriteState == SpriteState.MOVE_LEFT)
                    {
                        enemyActor.SpriteState = SpriteState.ATTACK_LEFT;
                    }
                    else if (enemyActor.SpriteState == SpriteState.MOVE_RIGHT)
                    {
                        enemyActor.SpriteState = SpriteState.ATTACK_RIGHT;
                    }
                    // TODO:         PlayUnitAnimationAudio(friendlyActor);
                    break;

                default:
                    break;
            }
            PlayUnitAnimationAudio(enemyActor);
        }

        // TODO: Play Idle Animation From Attacking

        // WIll play the idle animation if we were attacking before
        public void PlayIdleAnimationFromAttacking(Enemy enemyActor)
        {
            if (enemyActor.SpriteState == SpriteState.ATTACK_LEFT)
            {
                enemyActor.SpriteState = SpriteState.IDLE_LEFT;
            }

            if (enemyActor.SpriteState == SpriteState.ATTACK_RIGHT)
            {
                enemyActor.SpriteState = SpriteState.IDLE_RIGHT;
            }
        }
        // TODO: Play Idle Animation From Motion

        // Will play idle animation if we were moving prior to calling
        public void PlayIdleAnimationFromMotion(Enemy enemyActor)
        {

            if (enemyActor.SpriteState == SpriteState.MOVE_LEFT)
            {
                enemyActor.SpriteState = SpriteState.IDLE_LEFT;
            }

            if (enemyActor.SpriteState == SpriteState.MOVE_RIGHT)
            {
                enemyActor.SpriteState = SpriteState.IDLE_RIGHT;
            }

        }
        //TODO: Begin Dying

        #endregion



        #region Handle CurrentTarget Functions

        public void ScanGlobalList(Enemy enemyActor)
        {

            GameObject.mPotentialTargetListList.Reset();
            // Check the global list to see if another object's sprite frame intersects our FOV.
            // If so
            // InsertTargetIntoQueue

            for (int i = 0; i < GameObject.mPotentialTargetListList.GetCount(); i++)
            {
                currentTarget = GameObject.mPotentialTargetListList.GetCurrent().gameObject;

                // If my field of view intersects another object's frame.
                // And if the other object is not an enemyActor.
                // And if the other object is not a castle.
                if (enemyActor.FieldOfView.Intersects(currentTarget.Sprite.SpriteFrame)
                    && currentTarget.Hostility != Hostility.ENEMY
                    && currentTarget.Hostility != Hostility.CASTLE)
                {
                    // Reset current node back to first node.
                    enemyActor.TargetQueue.mTargetList.Reset();

                    // Check to make sure he is not already in my queue.
                    isInQueue = false;
                    for (int j = 0; j < enemyActor.TargetQueue.mTargetList.GetCount(); j++)
                    {
                        if (currentTarget.ObjectID == enemyActor.TargetQueue.mTargetList.GetCurrent().gameObject.ObjectID)
                        {
                            isInQueue = true;

                        }
                        // Go to next node in Queue
                        enemyActor.TargetQueue.mTargetList.NextNode();
                    }
                    if (!isInQueue)
                    {
                        InsertTargetIntoQueue(enemyActor);
                    }



                }

                GameObject.mPotentialTargetListList.NextNode();

            }

        }

        public void InsertTargetIntoQueue(Enemy enemyActor)
        {

            // current object = THIS object.
            // hostile object = object that ran into my field of view.

            // If my target queue is empty 
            if (enemyActor.TargetQueue.IsEmpty())
            {
                //Push onto queue.
                enemyActor.TargetQueue.InsertTarget(currentTarget);
            }

            // Else If the hostile object is a structure.
            else if (currentTarget.Hostility == Hostility.STRUCTURE)
            {
                // Push the structure at the end of the queue.
                enemyActor.TargetQueue.mTargetList.InsertLast(currentTarget);
            }

                    // If the hostile object is a creature.
            else if (currentTarget.Hostility == Hostility.FRIENDLY)
            {
                // Reset the current object's target queue's current node to = first node.
                enemyActor.TargetQueue.mTargetList.Reset();

                // Iterate through the current object's target queue to determine where to put the new target.
                for (int i = 0; i < enemyActor.TargetQueue.mTargetList.GetCount(); i++)
                {

                    // If first target is a structure
                    if (enemyActor.TargetQueue.mTargetList.GetCurrent().gameObject.Hostility ==
                        Hostility.STRUCTURE)
                    {
                        // Push creatue to front of the line.
                        enemyActor.TargetQueue.mTargetList.InsertFirst(currentTarget);
                        // We placed the target and now we're done.
                        break;
                    }


                    // If the target queue current.next == structure.
                    else if (enemyActor.TargetQueue.mTargetList.GetCurrent().nextNode != null
                        && enemyActor.TargetQueue.mTargetList.GetCurrent().nextNode.gameObject.Hostility ==
                        Hostility.STRUCTURE)
                    {
                        // Push the creature at the current target queue node.
                        enemyActor.TargetQueue.mTargetList.CreateNewNode(currentTarget,
                                                                              enemyActor.TargetQueue
                                                                                        .mTargetList.GetCurrent());
                        // We placed the target and now we're done.
                        break;
                    }
                    // Else  If we are at the end of the queue 
                    else if (enemyActor.TargetQueue.mTargetList.AtEnd())
                    {
                        // Push the creature at the current target queue node.
                        enemyActor.TargetQueue.mTargetList.InsertLast(currentTarget);

                        // We placed the target and now we're done.
                        break;
                    }
                    enemyActor.TargetQueue.mTargetList.NextNode();
                }
            }
        }


        public void MaintainCurrentTarget(Enemy enemyActor)
        {

            // If my target queue is not empty.
            // And my FOV is no longer intersecting my first target's sprite frame.
            //  enemyActor.GetTargetQueue().mTargetList.Reset();
            if (!enemyActor.TargetQueue.IsEmpty() &&
                !(enemyActor.FieldOfView.Intersects(enemyActor.TargetQueue.PeekFirst().Sprite.SpriteFrame)))
            {
                enemyActor.TargetQueue.PopTarget();
                // PrintGlobalList();

            }

        }


        #endregion

        #region Audio Functions

        #region Unit Struck Audio

        public void PlayUnitStruckAudio(Hero friendlyActor)
        {

            int temp = randomNumberGenerator.Next(0, 2);

            switch (temp)
            {
                case 0:
                    AudioController.HeroStruck1SoundEffectInstance.Volume = mMeleeHitSoundVolume;
                    AudioController.HeroStruck1SoundEffectInstance.Play();

                    break;
                case 1:
                    AudioController.HeroStruck2SoundEffectInstance.Volume = mMeleeHitSoundVolume;
                    AudioController.HeroStruck2SoundEffectInstance.Play();
                    break;
                default:
                    break;

            }


        }

        public void PlayUnitStruckAudio(Friendly friendlyActor)
        {
            //TODO: place holder
            switch (friendlyActor.CreatureType)
            {

                case ObjectType.DRAGON:
                    PlayDragonStruckSound();
                    break;
                case ObjectType.NECROMANCER:
                    PlayNecromancerStruckSound();
                    break;
                case ObjectType.ARCANE_MAGE:
                    PlayArcaneMageStruckSound();
                    break;
                case ObjectType.FIRE_MAGE:
                    PlayFireMageStruckSound();
                    break;
                case ObjectType.WOLF:
                    PlayWolfStruckSound();
                    break;
                case ObjectType.AXE_THROWER:
                    PlayAxeThrowerStruckSound();
                    break;
                case ObjectType.BERSERKER:
                    PlayBerserkerStruckSound();
                    break;

                case ObjectType.CLERIC:
                    PlayClericStruckSound();
                    break;

                default:
                    break;
            }


        }

        #region Arcane Mage

        private void PlayArcaneMageStruckSound()
        {
            int temp = randomNumberGenerator.Next(0, 3);
            switch (temp)
            {
                case 0:
                    AudioController.ArcaneMageStruck1SoundEffectInstance.Play();

                    break;
                case 1:
                    AudioController.ArcaneMageStruck2SoundEffectInstance.Play();
                    break;

                default:
                    break;
            }
        }

        #endregion Arcane Mage Struck

        #region Axe Thrower Struck

        private void PlayAxeThrowerStruckSound()
        {
            int temp = randomNumberGenerator.Next(0, 3);
            switch (temp)
            {

                case 0:
                    AudioController.AxeThrowerStruck1SoundEffectInstance.Play();
                    break;

                case 1:
                    AudioController.AxeThrowerStruck2SoundEffectInstance.Play();
                    break;

                case 2:
                    AudioController.AxeThrowerStruck3SoundEffectInstance.Play();
                    break;

                default:
                    break;
            }
        }

        #endregion Axe Thrower Struck

        #region Wolf Struck

        private void PlayWolfStruckSound()
        {
            int temp = randomNumberGenerator.Next(0, 1);
            switch (temp)
            {


                case 0:
                    AudioController.WolfStruckSoundEffectInstance.Play();
                    break;

                default:
                    break;
            }
        }

        #endregion Wolf Struck

        #region Necromancer Struck

        private void PlayNecromancerStruckSound()
        {
            int temp = randomNumberGenerator.Next(0, 2);
            switch (temp)
            {
                case 0:
                    AudioController.NecromancerStruck1SoundEffectInstance.Play();
                    break;
                case 1:
                    AudioController.NecromancerStruck2SoundEffectInstance.Play();
                    break;

                default:
                    break;
            }
        }
        #endregion Necromancer Struck

        #region Cleric Struck
        private void PlayClericStruckSound()
        {
            int temp = randomNumberGenerator.Next(0, 2);
            switch (temp)
            {
                case 0:
                    AudioController.ClericStruck1SoundEffectInstance.Play();
                    break;
                case 1:
                    AudioController.ClericStruck2SoundEffectInstance.Play();
                    break;
                default:
                    break;
            }
        }
        #endregion Cleric Struck

        #region Dragon Struck
        private void PlayDragonStruckSound()
        {

            int temp = randomNumberGenerator.Next(0, 2);

            switch (temp)
            {

                case 0:
                    AudioController.DragonStruck1SoundEffectInstance.Play();
                    break;
                case 1:
                    AudioController.DragonStruck2SoundEffectInstance.Play();
                    break;
                default:
                    break;
            }
        }

        #endregion Dragon Struck

        #region Fire Mage Struck

        private void PlayFireMageStruckSound()
        {
            int temp = randomNumberGenerator.Next(0, 2);
            switch (temp)
            {
                case 0:
                    AudioController.FireMageStruck1SoundEffectInstance.Play();

                    break;
                case 1:
                    AudioController.FireMageStruck2SoundEffectInstance.Play();
                    break;

                default:
                    break;
            }
        }
        #endregion Fire Mage Struck

        #region Berserker  Struck

        private void PlayBerserkerStruckSound()
        {
            int temp = randomNumberGenerator.Next(0, 5);
            switch (temp)
            {


                case 0:
                    AudioController.BerserkerStruck1SoundEffectInstance.Play();

                    break;
                case 1:
                    AudioController.BerserkerStruck2SoundEffectInstance.Play();
                    break;

                case 2:
                    AudioController.BerserkerStruck3SoundEffectInstance.Play();
                    break;
                case 3:
                    AudioController.BerserkerStruck4SoundEffectInstance.Play();
                    break;
                case 4:
                    AudioController.BerserkerStruck5SoundEffectInstance.Play();
                    break;

                default:
                    break;
            }
        }
        #endregion Berserker Mage Struck






        #endregion Unit Struck Audio


        #region Unit Attack Landed Audio

        public void PlayMeleeUnitAttackLandedAudio(Enemy enemyActor)
        {
            //TODO: place holder
            switch (enemyActor.CreatureType)
            {

                case ObjectType.REAPER:
                    PlayReaperAttackLandedSound();
                    break;
                case ObjectType.DOOM_HOUND:
                    PlayDoomHoundAttackLandedSound();
                    break;
                case ObjectType.IMP:
                    PlayImpAttackLandedSound();
                    break;

                default:
                    break;

            }
        }


        #endregion  Unit Attack Landed Audio


        #region Unit Animation Audio

        public void PlayUnitAnimationAudio(Enemy enemyActor)
        {
            //TODO: place holder
            switch (enemyActor.CreatureType)
            {


                case ObjectType.BANSHEE:
                    PlayBansheeAnimationSound();
                    break;
                case ObjectType.REAPER:
                    PlayReaperAnimationSound();
                    break;
                case ObjectType.DOOM_HOUND:
                    PlayDoomHoundAnimationSound();
                    break;
                case ObjectType.IMP:
                    PlayImpAnimationSound();
                    break;
                case ObjectType.GOG:
                    PlayGogAnimationSound();
                    break;

                default:
                    break;
            }
        }

        #endregion Unit Animation Audio


        #region Unit Death Audio

        private void PlayUnitDeathAudio(Enemy enemyActor)
        {
            switch (enemyActor.CreatureType)
            {


                case ObjectType.BANSHEE:
                    PlayBansheeDeathSound();

                    break;
                case ObjectType.REAPER:
                    PlayReaperDeathSound();
                    break;
                case ObjectType.DOOM_HOUND:
                    PlayDoomHoundDeathSound();
                    break;
                case ObjectType.IMP:
                    PlayImpDeathSound();
                    break;
                case ObjectType.GOG:
                    PlayGogDeathSound();
                    break;

                default:
                    break;
            }
        }

        #endregion




        #region Unit Possible Audio

        #region Reaper Audio

        public void PlayReaperDeathSound()
        {
            int temp = randomNumberGenerator.Next(0, 1);

            switch (temp)
            {
                case 0:
                    AudioController.ReaperDeathSoundEffectInstance.Volume = mReaperDeathSoundVolume;
                    AudioController.ReaperDeathSoundEffectInstance.Play();
                    break;

                default:

                    break;
            }
        }

        public void PlayReaperAttackLandedSound()
        {
            int temp = randomNumberGenerator.Next(0, 8);



            switch (temp)
            {
                case 0:
                    AudioController.MeleePunch1SoundEffectInstance.Volume = mMeleeHitSoundVolume;
                    AudioController.MeleePunch1SoundEffectInstance.Play();
                    break;

                case 1:
                    AudioController.MeleePunch2SoundEffectInstance.Volume = mMeleeHitSoundVolume;
                    AudioController.MeleePunch2SoundEffectInstance.Play();
                    break;

                case 2:
                    AudioController.MeleePunch3SoundEffectInstance.Volume = mMeleeHitSoundVolume;
                    AudioController.MeleePunch3SoundEffectInstance.Play();
                    break;
                case 3:
                    AudioController.MeleePunch4SoundEffectInstance.Volume = mMeleeHitSoundVolume;
                    AudioController.MeleePunch4SoundEffectInstance.Play();
                    break;
                case 4:
                    AudioController.MeleePunchSoundEffectInstance.Volume = mMeleeHitSoundVolume;
                    AudioController.MeleePunchSoundEffectInstance.Play();
                    break;
                case 5:
                    AudioController.MeleeWetStab1SoundEffectInstance.Volume = mMeleeHitSoundVolume;
                    AudioController.MeleeWetStab1SoundEffectInstance.Play();
                    break;
                case 6:
                    AudioController.MeleeWetStab2SoundEffectInstance.Volume = mMeleeHitSoundVolume;
                    AudioController.MeleeWetStab2SoundEffectInstance.Play();
                    break;
                case 7:
                    AudioController.MeleeWetStab3SoundEffectInstance.Volume = mMeleeHitSoundVolume;
                    AudioController.MeleeWetStab3SoundEffectInstance.Play();
                    break;


                default:

                    break;
            }
        }

        public void PlayReaperAnimationSound()
        {
            int temp = randomNumberGenerator.Next(0, 2);

            switch (temp)
            {
                case 1:
                    AudioController.ReaperStruck1SoundEffectInstance.Volume = mReaperAnimationSoundVolume;
                    AudioController.ReaperStruck1SoundEffectInstance.Play();
                    break;

                case 2:

                default:
                    break;
            }
        }

        #endregion Reaper Audio

        #region Gog Audio

        public void PlayGogDeathSound()
        {

            int temp = randomNumberGenerator.Next(0, 2);

            switch (temp)
            {
                case 0:
                    AudioController.GogDeath1SoundEffectInstance.Volume = mGogDeathSoundVolume;
                    AudioController.GogDeath1SoundEffectInstance.Play();
                    break;

                case 1:
                    AudioController.GogDeath2SoundEffectInstance.Volume = mGogDeathSoundVolume;
                    AudioController.GogDeath2SoundEffectInstance.Play();
                    break;

                default:

                    break;
            }
        
        }

        public void PlayGogAttackLandedSound()
        {
            int temp = randomNumberGenerator.Next(0, 2);

            switch (temp)
            {
                case 0:
                    AudioController.GogAttack1SoundEffectInstance.Volume = mGogAnimationSoundVolume;
                    AudioController.GogAttack1SoundEffectInstance.Play();
                    break;
                case 1:
                    AudioController.GogAttack2SoundEffectInstance.Volume = mGogAnimationSoundVolume;
                    AudioController.GogAttack2SoundEffectInstance.Play();
                    break;

                default:
                    break;
            }
        }

        public void PlayGogAnimationSound()
        {
            int temp = randomNumberGenerator.Next(0, 2);

            switch (temp)
            {
                case 0:
                    break;

                case 1:
                default:
                    break;
            }
        }
        #endregion Gog Audio

        #region Banshee Audio

        public void PlayBansheeDeathSound()
        {

            int temp = randomNumberGenerator.Next(0, 1);

            switch (temp)
            {
                case 0:
                    AudioController.BansheeDeathSoundEffectInstance.Volume = mBansheeDeathSoundVolume;
                    AudioController.BansheeDeathSoundEffectInstance.Play();
                    break;


                default:

                    break;
            }
        }

        public void PlayBansheeAttackLandedSound()
        {
            int temp = randomNumberGenerator.Next(0, 2);

            switch (temp)
            {
                case 0:
                    break;

                case 1:
                default:
                    break;
            }
        }

        public void PlayBansheeAnimationSound()
        {
            int temp = randomNumberGenerator.Next(0, 2);

            switch (temp)
            {
                case 0:
                    AudioController.BansheeAttackSoundEffectInstance.Volume = mBansheeAnimationSoundVolume;
                    AudioController.BansheeAttackSoundEffectInstance.Play();
                    break;


                default:
                    break;
            }
        }
        #endregion Banshee Audio

        #region Imp Audio


        public void PlayImpDeathSound()
        {

            int temp = randomNumberGenerator.Next(0, 1);

            switch (temp)
            {
                case 0:

                    AudioController.ImpDeathSoundEffectInstance.Volume = mImpDeathSoundVolume;
                    AudioController.ImpDeathSoundEffectInstance.Play();
                    break;

                default:

                    break;
            }
        }


        public void PlayImpAttackLandedSound()
        {
            int temp = randomNumberGenerator.Next(0, 8);

            switch (temp)
            {
                case 0:
                    AudioController.MeleePunch1SoundEffectInstance.Volume = mMeleeHitSoundVolume;
                    AudioController.MeleePunch1SoundEffectInstance.Play();
                    break;

                case 1:
                    AudioController.MeleePunch2SoundEffectInstance.Volume = mMeleeHitSoundVolume;
                    AudioController.MeleePunch2SoundEffectInstance.Play();
                    break;

                case 2:
                    AudioController.MeleePunch3SoundEffectInstance.Volume = mMeleeHitSoundVolume;
                    AudioController.MeleePunch3SoundEffectInstance.Play();
                    break;
                case 3:
                    AudioController.MeleePunch4SoundEffectInstance.Volume = mMeleeHitSoundVolume;
                    AudioController.MeleePunch4SoundEffectInstance.Play();
                    break;
                case 4:
                    AudioController.MeleePunchSoundEffectInstance.Volume = mMeleeHitSoundVolume;
                    AudioController.MeleePunchSoundEffectInstance.Play();
                    break;
                case 5:
                    AudioController.MeleeWetStab1SoundEffectInstance.Volume = mMeleeHitSoundVolume;
                    AudioController.MeleeWetStab1SoundEffectInstance.Play();
                    break;
                case 6:
                    AudioController.MeleeWetStab2SoundEffectInstance.Volume = mMeleeHitSoundVolume;
                    AudioController.MeleeWetStab2SoundEffectInstance.Play();
                    break;
                case 7:
                    AudioController.MeleeWetStab3SoundEffectInstance.Volume = mMeleeHitSoundVolume;
                    AudioController.MeleeWetStab3SoundEffectInstance.Play();
                    break;


                default:

                    break;
            }
        }

        public void PlayImpAnimationSound()
        {
            int temp = randomNumberGenerator.Next(0, 2);

            switch (temp)
            {
                //case 0:
                //    AudioController.ImpSpawnSoundEffectInstance.Volume = mImpAnimationSoundVolume;
                //    AudioController.ImpSpawnSoundEffectInstance.Play();
                //    break;

                case 0:
                    AudioController.ImpAttack1SoundEffectInstance.Volume = mImpAnimationSoundVolume;
                    AudioController.ImpAttack1SoundEffectInstance.Play();

                    break;
                case 1:
                    AudioController.ImpAttack2SoundEffectInstance.Volume = mImpAnimationSoundVolume;
                    AudioController.ImpAttack2SoundEffectInstance.Play();
                    break;
                default:

                    break;


            }








        }
        #endregion Imp Audio

        #region Doom Hound Audio

        public void PlayDoomHoundDeathSound()
        {

            int temp = randomNumberGenerator.Next(0, 2);

            switch (temp)
            {
                case 0:

                    AudioController.DoomHoundDeath1SoundEffectInstance.Volume = mDoomHoundDeathSoundVolume;
                    AudioController.DoomHoundDeath1SoundEffectInstance.Play();
                    break;
                case 1:

                    AudioController.DoomHoundDeath2SoundEffectInstance.Volume = mDoomHoundDeathSoundVolume;
                    AudioController.DoomHoundDeath2SoundEffectInstance.Play();
                    break;


                default:

                    break;
            }
        }


        public void PlayDoomHoundAttackLandedSound()
        {
            int temp = randomNumberGenerator.Next(0, 8);



            switch (temp)
            {
                case 0:
                    AudioController.MeleePunch1SoundEffectInstance.Volume = mMeleeHitSoundVolume;
                    AudioController.MeleePunch1SoundEffectInstance.Play();
                    break;

                case 1:
                    AudioController.MeleePunch2SoundEffectInstance.Volume = mMeleeHitSoundVolume;
                    AudioController.MeleePunch2SoundEffectInstance.Play();
                    break;

                case 2:
                    AudioController.MeleePunch3SoundEffectInstance.Volume = mMeleeHitSoundVolume;
                    AudioController.MeleePunch3SoundEffectInstance.Play();
                    break;
                case 3:
                    AudioController.MeleePunch4SoundEffectInstance.Volume = mMeleeHitSoundVolume;
                    AudioController.MeleePunch4SoundEffectInstance.Play();
                    break;
                case 4:
                    AudioController.MeleePunchSoundEffectInstance.Volume = mMeleeHitSoundVolume;
                    AudioController.MeleePunchSoundEffectInstance.Play();
                    break;
                case 5:
                    AudioController.MeleeWetStab1SoundEffectInstance.Volume = mMeleeHitSoundVolume;
                    AudioController.MeleeWetStab1SoundEffectInstance.Play();
                    break;
                case 6:
                    AudioController.MeleeWetStab2SoundEffectInstance.Volume = mMeleeHitSoundVolume;
                    AudioController.MeleeWetStab2SoundEffectInstance.Play();
                    break;
                case 7:
                    AudioController.MeleeWetStab3SoundEffectInstance.Volume = mMeleeHitSoundVolume;
                    AudioController.MeleeWetStab3SoundEffectInstance.Play();
                    break;


                default:

                    break;
            }
        }

        public void PlayDoomHoundAnimationSound()
        {
            int temp = randomNumberGenerator.Next(0, 2);

            switch (temp)
            {
                case 0:
                    AudioController.DoomHoundAttack1SoundEffectInstance.Volume = mDoomHoundAnimationSoundVolume;
                    AudioController.DoomHoundAttack1SoundEffectInstance.Play();
                    break;

                case 1:
                    AudioController.DoomHoundAttack2SoundEffectInstance.Volume = mDoomHoundAnimationSoundVolume;
                    AudioController.DoomHoundAttack2SoundEffectInstance.Play();
                    break;
                default:
                    break;
            }
        }
        #endregion Doom Hound Audio


        #endregion Unit Possible Audio

        #endregion Unit Audio




    }
}
