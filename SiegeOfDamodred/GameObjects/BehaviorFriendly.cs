using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExternalTypes;
using Microsoft.Xna.Framework;

namespace GameObjects
{
    public class BehaviorFriendly
    {
        Vector2 targetDirection;

        private float TimeStamp;
        private float FinalPosition;
        private float InitialPosition;
        private float DeltaX;
        private GameObject currentTarget;
        private bool isInQueue;
        private float weight;
        private GameTime gameTime;
        private float HealTimer;
        private bool isAttacking;

        private float AnimationTimer;
        private float WaitTimer;
        private Random randomNumberGenerator;

        public BehaviorFriendly(float weight)
        {
            randomNumberGenerator = new Random();
            FinalPosition = 0.0f;
            InitialPosition = 0.0f;
            DeltaX = 0.0f;
            TimeStamp = 0.0f;
            this.weight = weight;
            isAttacking = false;

        }

        #region Sound Volume Fields

        private const float mDragonAnimationSoundVolume = 0.3f;
        private const float mWolfAnimationSoundVolume = 0.3f;
        private const float mFireMageAnimationSoundVolume = 0.3f;
        private const float mArcaneMageAnimationSoundVolume = 0.3f;
        private const float mClericAnimationSoundVolume = 0.8f;
        private const float mBerserkerAnimationSoundVolume = 0.3f;
        private const float mAxeThrowerAnimationSoundVolume = 0.3f;

        private const float mMeleeHitSoundVolume = 0.9f;

        private const float mBansheeStruckSoundVolume = 0.3f;
        private const float mReaperStruckSoundVolume = 0.3f;
        private const float mImpStruckSoundVolume = 0.3f;
        private const float mGogStruckSoundVolume = 0.3f;
        private const float mDoomHoundStruckSoundVolume = 0.3f;

        private const float mDragonDeathSoundVolume = 0.3f;
        private const float mWolfDeathSoundVolume = 0.3f;
        private const float mFireMageDeathSoundVolume = 0.3f;
        private const float mArcaneMageDeathSoundVolume = 0.3f;
        private const float mClericDeathSoundVolume = 0.3f;
        private const float mBerserkerDeathSoundVolume = 0.3f;
        private const float mAxeThrowerDeathSoundVolume = 0.3f;
        private const float mNecromancerDeathSoundVolume = 0.3f;

        private bool isDying;
        private float mDeathAnimationTimer;

        #endregion


        public void Update(Friendly friendlyActor, GameTime gameTime)
        {

            this.gameTime = gameTime;

            CheckVitalSigns(friendlyActor);



            if (!isDying)
            {


                // Hmmm.. What type of attacks do I have again?
                if (friendlyActor.CombatType == CombatType.MELEE)
                {
                    HandleMeleeUnitBehavior(friendlyActor);
                }
                else if (friendlyActor.CombatType == CombatType.RANGED)
                {
                    HandleRangedUnitBehavior(friendlyActor);
                }
                else if (friendlyActor.CombatType == CombatType.HEALER)
                {
                    HandleHealingUnitBehavior(friendlyActor);
                }



                targetDirection = friendlyActor.CurrentTarget - friendlyActor.Sprite.WorldPosition;
                targetDirection.Normalize();

                friendlyActor.Direction = friendlyActor.Direction + targetDirection * this.weight;

                if (friendlyActor.CreatureType != ObjectType.CLERIC)
                {
                    ScanGlobalList(friendlyActor);

                    // Check if current target is in our field of view still
                    MaintainTarget(friendlyActor);
                }
            }
        }


        #region Behavior Functions

        #region Melee Behavior

        public void HandleMeleeUnitBehavior(Friendly friendlyActor)
        {

            TimeStamp += friendlyActor.gameTime.ElapsedGameTime.Milliseconds;


            UpdateUnitSpeed(friendlyActor);

            // Always Move To CurrentTarget Unless I'm attacking
            if (!isAttacking)
            {
                friendlyActor.FriendlyAttribute.UnitSpeed = 2.0f;

                MoveTowardsTargetCoordinate(friendlyActor);
            }
            StopWhenAtTargetCoordinate(friendlyActor);


            // If I have a list of targets. And I am melee
            if (!friendlyActor.TargetQueue.IsEmpty() && friendlyActor.CombatType == CombatType.MELEE)
            {
                // Check if I made it to my target's sprite frame.
                CheckMeleeAttackStatus(friendlyActor);


                if (friendlyActor.FriendlyAggroMode == FriendlyAggroMode.ATTACKING)
                {
                    //I'm sitting at my enemy's feet. I'm committed, lock me into attack mode.
                    isAttacking = true;
                    AnimationTimer += friendlyActor.gameTime.ElapsedGameTime.Milliseconds;


                    // If I'm attacking then I should play my Attack animation from start to finish.
                    if (AnimationTimer <= GetAnimationLength(friendlyActor))
                    {
                        PlayAttackAnimation(friendlyActor);

                        //Console.WriteLine("Now I'm playing attacking Animation");
                    }
                    else
                    {
                        // Then I should apply my damage to the current target
                        //Console.WriteLine("Applying Damage");
                        PlayUnitAttackLandedAudio(friendlyActor);
                        PlayUnitStruckAudio(friendlyActor, (Enemy)friendlyActor.TargetQueue.PeekFirst());
                        friendlyActor.FriendlyAttribute.Attack();
                        friendlyActor.FriendlyAggroMode = FriendlyAggroMode.IDLING;
                        WaitTimer = 0;
                    }
                }
                // Now I'm done playing my animation for attack. Now play idle while I cool off
                else if (friendlyActor.FriendlyAggroMode == FriendlyAggroMode.IDLING && WaitTimer <= friendlyActor.FriendlyAttribute.AttackSpeed)
                {
                    WaitTimer += friendlyActor.gameTime.ElapsedGameTime.Milliseconds;
                    PlayIdleAnimationFromAttacking(friendlyActor);
                }
                // After I wait I should play my attack animation again 
                else if (WaitTimer >= friendlyActor.FriendlyAttribute.AttackSpeed)
                {
                    AnimationTimer = 0;
                    friendlyActor.FriendlyAggroMode = FriendlyAggroMode.ATTACKING;
                }
            }
            else
            {
                //If we are done slaughtering our foes.
                friendlyActor.FriendlyAggroMode = FriendlyAggroMode.MOVING;
                isAttacking = false;
                UpdateUnitSpeed(friendlyActor);
                StopWhenAtTargetCoordinate(friendlyActor);
            }
        }
        #endregion

        #region Ranged Behavior

        public void HandleRangedUnitBehavior(Friendly friendlyActor)
        {

            TimeStamp += friendlyActor.gameTime.ElapsedGameTime.Milliseconds;


            UpdateUnitSpeed(friendlyActor);

            // Always Move To CurrentTarget Unless I'm attacking
            if (!isAttacking)
            {
                friendlyActor.FriendlyAttribute.UnitSpeed = 2.0f;

                MoveTowardsTargetCoordinate(friendlyActor);
            }
            StopWhenAtTargetCoordinate(friendlyActor);


            // If I have a list of targets. And I am Ranged
            if (!friendlyActor.TargetQueue.IsEmpty() && friendlyActor.CombatType == CombatType.RANGED)
            {
                // Check if I made it to my target's sprite frame.
                CheckRangedAttackStatus(friendlyActor);


                if (friendlyActor.FriendlyAggroMode == FriendlyAggroMode.ATTACKING)
                {
                    //I'm sitting at my enemy's feet. I'm committed, lock me into attack mode.
                    isAttacking = true;
                    AnimationTimer += friendlyActor.gameTime.ElapsedGameTime.Milliseconds;


                    // If I'm attacking then I should play my Attack animation from start to finish.
                    if (AnimationTimer <= GetAnimationLength(friendlyActor))
                    {
                        PlayAttackAnimation(friendlyActor);

                        //Console.WriteLine("Now I'm playing attacking Animation");
                    }
                    else
                    {
                        // Then I should apply my damage to the current target
                        //Console.WriteLine("Applying Damage");
                        PlayUnitAttackLandedAudio(friendlyActor);
                        friendlyActor.FriendlyAttribute.Attack();
                        friendlyActor.FriendlyAggroMode = FriendlyAggroMode.IDLING;
                        WaitTimer = 0;
                    }
                }
                // Now I'm done playing my animation for attack. Now play idle while I cool off
                else if (friendlyActor.FriendlyAggroMode == FriendlyAggroMode.IDLING && WaitTimer <= friendlyActor.FriendlyAttribute.AttackSpeed)
                {
                    WaitTimer += friendlyActor.gameTime.ElapsedGameTime.Milliseconds;
                    PlayIdleAnimationFromAttacking(friendlyActor);
                }
                // After I wait I should play my attack animation again 
                else if (WaitTimer >= friendlyActor.FriendlyAttribute.AttackSpeed)
                {
                    AnimationTimer = 0;
                    friendlyActor.FriendlyAggroMode = FriendlyAggroMode.ATTACKING;
                }
            }
            else
            {
                //If we are done slaughtering our foes.
                friendlyActor.FriendlyAggroMode = FriendlyAggroMode.MOVING;
                isAttacking = false;
                UpdateUnitSpeed(friendlyActor);
                StopWhenAtTargetCoordinate(friendlyActor);
            }
        }

        #endregion

        #region Healing Behavior

        private void HandleHealingUnitBehavior(Friendly friendlyActor)
        {
            TimeStamp += friendlyActor.gameTime.ElapsedGameTime.Milliseconds;


            UpdateUnitSpeed(friendlyActor);

            // Always Move To CurrentTarget Unless I'm attacking
            if (!isAttacking)
            {
                friendlyActor.FriendlyAttribute.UnitSpeed = 2.0f;

                MoveTowardsTargetCoordinate(friendlyActor);
            }
            StopWhenAtTargetCoordinate(friendlyActor);

            HealTimer += friendlyActor.gameTime.ElapsedGameTime.Milliseconds;

            if (HealTimer >= friendlyActor.FriendlyAttribute.AttackSpeed)
            {
                // Time to play heal animation
                AnimationTimer += friendlyActor.gameTime.ElapsedGameTime.Milliseconds;
                if (AnimationTimer <= GetAnimationLength(friendlyActor))
                {
                    PlayAttackAnimation(friendlyActor);      
                }
                else
                {
                    PlayClericAnimationSound();
                    friendlyActor.FriendlyAttribute.Heal();
                    HealTimer = 0;
                    AnimationTimer = 0;
                }


            }

        }
        #endregion

        #endregion

        #region Unit Status Changing Functions

        private void CheckVitalSigns(Friendly friendlyActor)
        {
            if (0 >= friendlyActor.FriendlyAttribute.CurrentHealthPoints)
            {
                mDeathAnimationTimer += friendlyActor.gameTime.ElapsedGameTime.Milliseconds;
                PlayUnitDeathAudio(friendlyActor);
                isDying = true;
                BeginDying(friendlyActor);
            }
            else
            {
                isDying = false;
            }
        }


        public void CheckMeleeAttackStatus(Friendly friendlyActor)
        {
            // If I'm colliding with my target
            if (friendlyActor.Sprite.SpriteFrame.Intersects(friendlyActor.TargetQueue.PeekFirst().Sprite.SpriteFrame) &&
                 friendlyActor.FriendlyAggroMode != FriendlyAggroMode.IDLING)
            {
                // I should start attacking
                UpdateUnitSpeed(friendlyActor);
                friendlyActor.FriendlyAggroMode = FriendlyAggroMode.ATTACKING;
            }
            // But if I'm NOT colliding with my target
            else if (!friendlyActor.Sprite.SpriteFrame.Intersects(friendlyActor.TargetQueue.PeekFirst().Sprite.SpriteFrame))
            {
                // I should be moving :)
                friendlyActor.FriendlyAggroMode = FriendlyAggroMode.MOVING;
                UpdateUnitSpeed(friendlyActor);
                MoveTowardsTargetCoordinate(friendlyActor);

            }
        }

        public void CheckRangedAttackStatus(Friendly friendlyActor)
        {
            // If I'm colliding with my target
            if (friendlyActor.FieldOfView.Intersects(friendlyActor.TargetQueue.PeekFirst().Sprite.SpriteFrame) &&
                 friendlyActor.FriendlyAggroMode != FriendlyAggroMode.IDLING)
            {
                // I should start attacking
                UpdateUnitSpeed(friendlyActor);
                friendlyActor.FriendlyAggroMode = FriendlyAggroMode.ATTACKING;
            }
            // But if I'm NOT colliding with my target
            else if (!friendlyActor.FieldOfView.Intersects(friendlyActor.TargetQueue.PeekFirst().Sprite.SpriteFrame))
            {
                // I should be moving :)
                friendlyActor.FriendlyAggroMode = FriendlyAggroMode.MOVING;
                UpdateUnitSpeed(friendlyActor);
                MoveTowardsTargetCoordinate(friendlyActor);

            }
        }
        #endregion

        #region Movement Functions

        public void UpdateUnitSpeed(Friendly friendlyActor)
        {

            switch (friendlyActor.FriendlyAggroMode)
            {
                case FriendlyAggroMode.ATTACKING:
                    friendlyActor.FriendlyAttribute.UnitSpeed = 0.0f;
                    break;
                case FriendlyAggroMode.IDLING:
                    friendlyActor.FriendlyAttribute.UnitSpeed = 0.0f;
                    break;
                case FriendlyAggroMode.MOVING:
                    friendlyActor.FriendlyAttribute.UnitSpeed = 2.0f;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        // Decides if we are moving left or right based on time stamps taken at different frames of reference.
        public void MoveTowardsTargetCoordinate(Friendly friendlyActor)
        {


            if (TimeStamp < 100.0f)
            {

                InitialPosition = friendlyActor.Sprite.WorldPosition.X;
            }

            if (TimeStamp >= 200.0f)
            {

                FinalPosition = friendlyActor.Sprite.WorldPosition.X;
                DeltaX = FinalPosition - InitialPosition;

                TimeStamp = 0.0f;
                if (DeltaX > 0)
                {
                    friendlyActor.SpriteState = SpriteState.MOVE_RIGHT;

                }
                if (DeltaX <= 0)
                {
                    friendlyActor.SpriteState = SpriteState.MOVE_LEFT;

                }
            }
        }


        // Will cause the unit to stop moving when target location is reached.
        public void StopWhenAtTargetCoordinate(Friendly friendlyActor)
        {


            if ((friendlyActor.Sprite.WorldPosition.X >= friendlyActor.CurrentTarget.X - 30 &&
                     friendlyActor.Sprite.WorldPosition.Y >= friendlyActor.CurrentTarget.Y - 30) &&
                     (friendlyActor.Sprite.WorldPosition.X <= friendlyActor.CurrentTarget.X + 30 &&
                     friendlyActor.Sprite.WorldPosition.Y <= friendlyActor.CurrentTarget.Y + 30))
            {
                friendlyActor.FriendlyAttribute.UnitSpeed = 0.0f;
                PlayIdleAnimationFromMotion(friendlyActor);
            }
        }

        #endregion

        #region Animation Functions

        //Gets how long my attack animation lasts
        public float GetAnimationLength(Friendly friendlyActor)
        {
            switch (friendlyActor.CreatureType)
            {

                case ObjectType.DRAGON:
                    PlayDragonAnimationSound();
                    return friendlyActor.DragonAttackAnimationFrames * friendlyActor.DragonAttackAnimationInterval;
                    break;
                case ObjectType.NECROMANCER:
                    return friendlyActor.NecromancerAttackAnimationFrames * friendlyActor.NecromancerAttackAnimationInterval;
                    break;
                case ObjectType.ARCANE_MAGE:
                    return friendlyActor.ArcaneMageAttackAnimationFrames * friendlyActor.ArcaneMageAttackAnimationInterval;
                    break;
                case ObjectType.FIRE_MAGE:
                    return friendlyActor.FireMageAttackAnimationFrames * friendlyActor.FireMageAttackAnimationInterval;
                    break;
                case ObjectType.WOLF:
                    return friendlyActor.WolfAttackAnimationFrames * friendlyActor.WolfAttackAnimationInterval;
                    break;
                case ObjectType.AXE_THROWER:
                    return friendlyActor.AxeThrowerAttackAnimationFrames * friendlyActor.AxeThrowerAttackAnimationInterval;
                    break;
                case ObjectType.BERSERKER:
                    return friendlyActor.BerserkerAttackAnimationFrames * friendlyActor.BerserkerAttackAnimationInterval;
                    break;

                case ObjectType.CLERIC:
                    return friendlyActor.ClericAttackAnimationFrames * friendlyActor.ClericAttackAnimationInterval;
                    break;

                default:
                    return 0.0f;
                    break;
            }
        }

        // Play the death animation after our HP has dropped below 0.
        private void BeginDying(Friendly friendlyActor)
        {
            switch (friendlyActor.CreatureType)
            {

                case ObjectType.DRAGON:
                    if (mDeathAnimationTimer <= friendlyActor.DragonDeathAnimationFrames * friendlyActor.DragonDeathAnimationInterval
                        && (friendlyActor.SpriteState == SpriteState.ATTACK_LEFT || friendlyActor.SpriteState == SpriteState.MOVE_LEFT
                        || friendlyActor.SpriteState == SpriteState.IDLE_LEFT))
                    {
                        friendlyActor.SpriteState = SpriteState.DEATH_LEFT;

                    }

                    else if (mDeathAnimationTimer <= friendlyActor.DragonDeathAnimationFrames * friendlyActor.DragonDeathAnimationInterval
                        && (friendlyActor.SpriteState == SpriteState.ATTACK_RIGHT || friendlyActor.SpriteState == SpriteState.MOVE_RIGHT
                        || friendlyActor.SpriteState == SpriteState.IDLE_RIGHT))
                    {
                        friendlyActor.SpriteState = SpriteState.DEATH_RIGHT;
                    }
                    else if (mDeathAnimationTimer >= friendlyActor.DragonDeathAnimationFrames * friendlyActor.DragonDeathAnimationInterval)
                    {
                        friendlyActor.HasDied = true;
                    }


                    break;
                case ObjectType.NECROMANCER:

                    if (mDeathAnimationTimer <= friendlyActor.NecromancerDeathAnimationFrames * friendlyActor.NecromancerDeathAnimationInterval
    && (friendlyActor.SpriteState == SpriteState.ATTACK_LEFT || friendlyActor.SpriteState == SpriteState.MOVE_LEFT
    || friendlyActor.SpriteState == SpriteState.IDLE_LEFT))
                    {
                        friendlyActor.SpriteState = SpriteState.DEATH_LEFT;
                    }

                    else if (mDeathAnimationTimer <= friendlyActor.NecromancerDeathAnimationFrames * friendlyActor.NecromancerDeathAnimationInterval
                        && (friendlyActor.SpriteState == SpriteState.ATTACK_RIGHT || friendlyActor.SpriteState == SpriteState.MOVE_RIGHT
                        || friendlyActor.SpriteState == SpriteState.IDLE_RIGHT))
                    {
                        friendlyActor.SpriteState = SpriteState.DEATH_RIGHT;
                    }
                    else if (mDeathAnimationTimer >= friendlyActor.NecromancerDeathAnimationFrames * friendlyActor.NecromancerDeathAnimationInterval)
                    {
                        friendlyActor.HasDied = true;
                    }

                    break;
                case ObjectType.ARCANE_MAGE:
                    if (mDeathAnimationTimer <= friendlyActor.ArcaneMageDeathAnimationFrames * friendlyActor.ArcaneMageDeathAnimationInterval
    && (friendlyActor.SpriteState == SpriteState.ATTACK_LEFT || friendlyActor.SpriteState == SpriteState.MOVE_LEFT
    || friendlyActor.SpriteState == SpriteState.IDLE_LEFT))
                    {
                        friendlyActor.SpriteState = SpriteState.DEATH_LEFT;
                    }

                    else if (mDeathAnimationTimer <= friendlyActor.ArcaneMageDeathAnimationFrames * friendlyActor.ArcaneMageDeathAnimationInterval
                        && (friendlyActor.SpriteState == SpriteState.ATTACK_RIGHT || friendlyActor.SpriteState == SpriteState.MOVE_RIGHT
                        || friendlyActor.SpriteState == SpriteState.IDLE_RIGHT))
                    {
                        friendlyActor.SpriteState = SpriteState.DEATH_RIGHT;
                    }
                    else if (mDeathAnimationTimer >= friendlyActor.ArcaneMageDeathAnimationFrames * friendlyActor.ArcaneMageDeathAnimationInterval)
                    {
                        friendlyActor.HasDied = true;
                    }

                    break;
                case ObjectType.FIRE_MAGE:

                    if (mDeathAnimationTimer <= friendlyActor.FireMageDeathAnimationFrames * friendlyActor.FireMageDeathAnimationInterval
    && (friendlyActor.SpriteState == SpriteState.ATTACK_LEFT || friendlyActor.SpriteState == SpriteState.MOVE_LEFT
    || friendlyActor.SpriteState == SpriteState.IDLE_LEFT))
                    {
                        friendlyActor.SpriteState = SpriteState.DEATH_LEFT;
                    }

                    else if (mDeathAnimationTimer <= friendlyActor.FireMageDeathAnimationFrames * friendlyActor.FireMageDeathAnimationInterval
                        && (friendlyActor.SpriteState == SpriteState.ATTACK_RIGHT || friendlyActor.SpriteState == SpriteState.MOVE_RIGHT
                        || friendlyActor.SpriteState == SpriteState.IDLE_RIGHT))
                    {
                        friendlyActor.SpriteState = SpriteState.DEATH_RIGHT;
                    }
                    else if (mDeathAnimationTimer >= friendlyActor.FireMageDeathAnimationFrames * friendlyActor.FireMageDeathAnimationInterval)
                    {
                        friendlyActor.HasDied = true;
                    }

                    break;
                case ObjectType.WOLF:

                    if (mDeathAnimationTimer <= friendlyActor.WolfDeathAnimationFrames * friendlyActor.WolfDeathAnimationInterval
    && (friendlyActor.SpriteState == SpriteState.ATTACK_LEFT || friendlyActor.SpriteState == SpriteState.MOVE_LEFT
    || friendlyActor.SpriteState == SpriteState.IDLE_LEFT))
                    {
                        friendlyActor.SpriteState = SpriteState.DEATH_LEFT;
                    }

                    else if (mDeathAnimationTimer <= friendlyActor.WolfDeathAnimationFrames * friendlyActor.WolfDeathAnimationInterval
                        && (friendlyActor.SpriteState == SpriteState.ATTACK_RIGHT || friendlyActor.SpriteState == SpriteState.MOVE_RIGHT
                        || friendlyActor.SpriteState == SpriteState.IDLE_RIGHT))
                    {
                        friendlyActor.SpriteState = SpriteState.DEATH_RIGHT;
                    }
                    else if (mDeathAnimationTimer >= friendlyActor.WolfDeathAnimationFrames * friendlyActor.WolfDeathAnimationInterval)
                    {
                        friendlyActor.HasDied = true;
                    }

                    break;
                case ObjectType.AXE_THROWER:
                    if (mDeathAnimationTimer <= friendlyActor.AxeThrowerDeathAnimationFrames * friendlyActor.AxeThrowerDeathAnimationInterval
                         && (friendlyActor.SpriteState == SpriteState.ATTACK_LEFT || friendlyActor.SpriteState == SpriteState.MOVE_LEFT ||
                         friendlyActor.SpriteState == SpriteState.IDLE_LEFT))
                    {
                        friendlyActor.SpriteState = SpriteState.DEATH_LEFT;
                    }

                    else if (mDeathAnimationTimer <= friendlyActor.AxeThrowerDeathAnimationFrames * friendlyActor.AxeThrowerDeathAnimationInterval
                        && (friendlyActor.SpriteState == SpriteState.ATTACK_RIGHT || friendlyActor.SpriteState == SpriteState.MOVE_RIGHT
                         || friendlyActor.SpriteState == SpriteState.IDLE_RIGHT))
                    {
                        friendlyActor.SpriteState = SpriteState.DEATH_RIGHT;
                    }
                    else if (mDeathAnimationTimer >= friendlyActor.AxeThrowerDeathAnimationFrames * friendlyActor.AxeThrowerDeathAnimationInterval)
                    {
                        friendlyActor.HasDied = true;
                    }


                    break;
                case ObjectType.BERSERKER:
                    if (mDeathAnimationTimer <= friendlyActor.BerserkerDeathAnimationFrames * friendlyActor.BerserkerDeathAnimationInterval
                        && (friendlyActor.SpriteState == SpriteState.ATTACK_LEFT || friendlyActor.SpriteState == SpriteState.MOVE_LEFT
                        || friendlyActor.SpriteState == SpriteState.IDLE_LEFT))
                    {
                        friendlyActor.SpriteState = SpriteState.DEATH_LEFT;
                    }

                    else if (mDeathAnimationTimer <= friendlyActor.BerserkerDeathAnimationFrames * friendlyActor.BerserkerDeathAnimationInterval
                        && (friendlyActor.SpriteState == SpriteState.ATTACK_RIGHT || friendlyActor.SpriteState == SpriteState.MOVE_RIGHT
                        || friendlyActor.SpriteState == SpriteState.IDLE_RIGHT))
                    {
                        friendlyActor.SpriteState = SpriteState.DEATH_RIGHT;
                    }
                    else if (mDeathAnimationTimer >= friendlyActor.BerserkerDeathAnimationFrames * friendlyActor.BerserkerDeathAnimationInterval)
                    {
                        friendlyActor.HasDied = true;
                    }

                    break;
                case ObjectType.CLERIC:
                    if (mDeathAnimationTimer <= friendlyActor.ClericDeathAnimationFrames * friendlyActor.ClericDeathAnimationInterval
    && (friendlyActor.SpriteState == SpriteState.ATTACK_LEFT || friendlyActor.SpriteState == SpriteState.MOVE_LEFT))
                    {
                        friendlyActor.SpriteState = SpriteState.DEATH_LEFT;
                    }

                    else if (mDeathAnimationTimer <= friendlyActor.ClericDeathAnimationFrames * friendlyActor.ClericDeathAnimationInterval
                        && (friendlyActor.SpriteState == SpriteState.ATTACK_RIGHT || friendlyActor.SpriteState == SpriteState.MOVE_RIGHT))
                    {
                        friendlyActor.SpriteState = SpriteState.DEATH_RIGHT;
                    }
                    else if (mDeathAnimationTimer >= friendlyActor.ClericDeathAnimationFrames * friendlyActor.ClericDeathAnimationInterval)
                    {
                        friendlyActor.HasDied = true;
                    }

                    break;

                default:
                    break;
            }
        }

        // Will play Idle Animation for duration of animation
        public void PlayAttackAnimation(Friendly friendlyActor)
        {
            switch (friendlyActor.CreatureType)
            {

                case ObjectType.DRAGON:
                    if (friendlyActor.SpriteState == SpriteState.IDLE_LEFT)
                    {
                        friendlyActor.SpriteState = SpriteState.ATTACK_LEFT;
                    }
                    else if (friendlyActor.SpriteState == SpriteState.IDLE_RIGHT)
                    {
                        friendlyActor.SpriteState = SpriteState.ATTACK_RIGHT;
                    }
                    else if (friendlyActor.SpriteState == SpriteState.MOVE_LEFT)
                    {
                        friendlyActor.SpriteState = SpriteState.ATTACK_LEFT;
                    }
                    else if (friendlyActor.SpriteState == SpriteState.MOVE_RIGHT)
                    {
                        friendlyActor.SpriteState = SpriteState.ATTACK_RIGHT;
                    }
                    PlayUnitAnimationAudio(friendlyActor);
                    break;
                case ObjectType.NECROMANCER:
                    if (friendlyActor.SpriteState == SpriteState.IDLE_LEFT)
                    {
                        friendlyActor.SpriteState = SpriteState.ATTACK_LEFT;
                    }
                    else if (friendlyActor.SpriteState == SpriteState.IDLE_RIGHT)
                    {
                        friendlyActor.SpriteState = SpriteState.ATTACK_RIGHT;
                    }
                    else if (friendlyActor.SpriteState == SpriteState.MOVE_LEFT)
                    {
                        friendlyActor.SpriteState = SpriteState.ATTACK_LEFT;
                    }
                    else if (friendlyActor.SpriteState == SpriteState.MOVE_RIGHT)
                    {
                        friendlyActor.SpriteState = SpriteState.ATTACK_RIGHT;
                    }
                    PlayUnitAnimationAudio(friendlyActor);
                    break;
                case ObjectType.ARCANE_MAGE:
                    if (friendlyActor.SpriteState == SpriteState.IDLE_LEFT)
                    {
                        friendlyActor.SpriteState = SpriteState.ATTACK_LEFT;
                    }
                    else if (friendlyActor.SpriteState == SpriteState.IDLE_RIGHT)
                    {
                        friendlyActor.SpriteState = SpriteState.ATTACK_RIGHT;
                    }
                    else if (friendlyActor.SpriteState == SpriteState.MOVE_LEFT)
                    {
                        friendlyActor.SpriteState = SpriteState.ATTACK_LEFT;
                    }
                    else if (friendlyActor.SpriteState == SpriteState.MOVE_RIGHT)
                    {
                        friendlyActor.SpriteState = SpriteState.ATTACK_RIGHT;
                    }
                    PlayUnitAnimationAudio(friendlyActor);
                    break;
                case ObjectType.FIRE_MAGE:
                    if (friendlyActor.SpriteState == SpriteState.IDLE_LEFT)
                    {
                        friendlyActor.SpriteState = SpriteState.ATTACK_LEFT;
                    }
                    else if (friendlyActor.SpriteState == SpriteState.IDLE_RIGHT)
                    {
                        friendlyActor.SpriteState = SpriteState.ATTACK_RIGHT;
                    }
                    else if (friendlyActor.SpriteState == SpriteState.MOVE_LEFT)
                    {
                        friendlyActor.SpriteState = SpriteState.ATTACK_LEFT;
                    }
                    else if (friendlyActor.SpriteState == SpriteState.MOVE_RIGHT)
                    {
                        friendlyActor.SpriteState = SpriteState.ATTACK_RIGHT;
                    }
                    PlayUnitAnimationAudio(friendlyActor);
                    break;
                case ObjectType.WOLF:
                    if (friendlyActor.SpriteState == SpriteState.IDLE_LEFT)
                    {
                        friendlyActor.SpriteState = SpriteState.ATTACK_LEFT;
                    }
                    else if (friendlyActor.SpriteState == SpriteState.IDLE_RIGHT)
                    {
                        friendlyActor.SpriteState = SpriteState.ATTACK_RIGHT;
                    }
                    else if (friendlyActor.SpriteState == SpriteState.MOVE_LEFT)
                    {
                        friendlyActor.SpriteState = SpriteState.ATTACK_LEFT;
                    }
                    else if (friendlyActor.SpriteState == SpriteState.MOVE_RIGHT)
                    {
                        friendlyActor.SpriteState = SpriteState.ATTACK_RIGHT;
                    }
                    PlayUnitAnimationAudio(friendlyActor);
                    break;
                case ObjectType.AXE_THROWER:
                    if (friendlyActor.SpriteState == SpriteState.IDLE_LEFT)
                    {
                        friendlyActor.SpriteState = SpriteState.ATTACK_LEFT;
                    }
                    else if (friendlyActor.SpriteState == SpriteState.IDLE_RIGHT)
                    {
                        friendlyActor.SpriteState = SpriteState.ATTACK_RIGHT;
                    }
                    else if (friendlyActor.SpriteState == SpriteState.MOVE_LEFT)
                    {
                        friendlyActor.SpriteState = SpriteState.ATTACK_LEFT;
                    }
                    else if (friendlyActor.SpriteState == SpriteState.MOVE_RIGHT)
                    {
                        friendlyActor.SpriteState = SpriteState.ATTACK_RIGHT;
                    }
                    PlayUnitAnimationAudio(friendlyActor);
                    break;
                case ObjectType.BERSERKER:
                    if (friendlyActor.SpriteState == SpriteState.IDLE_LEFT)
                    {
                        friendlyActor.SpriteState = SpriteState.ATTACK_LEFT;
                    }
                    else if (friendlyActor.SpriteState == SpriteState.IDLE_RIGHT)
                    {
                        friendlyActor.SpriteState = SpriteState.ATTACK_RIGHT;
                    }
                    else if (friendlyActor.SpriteState == SpriteState.MOVE_LEFT)
                    {
                        friendlyActor.SpriteState = SpriteState.ATTACK_LEFT;
                    }
                    else if (friendlyActor.SpriteState == SpriteState.MOVE_RIGHT)
                    {
                        friendlyActor.SpriteState = SpriteState.ATTACK_RIGHT;
                    }
                    PlayUnitAnimationAudio(friendlyActor);
                    break;
                case ObjectType.CLERIC:
                    if (friendlyActor.SpriteState == SpriteState.IDLE_LEFT)
                    {
                        friendlyActor.SpriteState = SpriteState.ATTACK_LEFT;
                    }
                    else if (friendlyActor.SpriteState == SpriteState.IDLE_RIGHT)
                    {
                        friendlyActor.SpriteState = SpriteState.ATTACK_RIGHT;
                    }
                    else if (friendlyActor.SpriteState == SpriteState.MOVE_LEFT)
                    {
                        friendlyActor.SpriteState = SpriteState.ATTACK_LEFT;
                    }
                    else if (friendlyActor.SpriteState == SpriteState.MOVE_RIGHT)
                    {
                        friendlyActor.SpriteState = SpriteState.ATTACK_RIGHT;
                    }
                    PlayUnitAnimationAudio(friendlyActor);
                    break;


                default:
                    break;
            }
        }

        // WIll play the idle animation if we were attacking before
        public void PlayIdleAnimationFromAttacking(Friendly friendlyActor)
        {
            if (friendlyActor.SpriteState == SpriteState.ATTACK_LEFT)
            {
                friendlyActor.SpriteState = SpriteState.IDLE_LEFT;
            }

            if (friendlyActor.SpriteState == SpriteState.ATTACK_RIGHT)
            {
                friendlyActor.SpriteState = SpriteState.IDLE_RIGHT;
            }
        }

        // Will play idle animation if we were moving prior to calling
        public void PlayIdleAnimationFromMotion(Friendly friendlyActor)
        {

            if (friendlyActor.SpriteState == SpriteState.MOVE_LEFT)
            {
                friendlyActor.SpriteState = SpriteState.IDLE_LEFT;
            }

            if (friendlyActor.SpriteState == SpriteState.MOVE_RIGHT)
            {
                friendlyActor.SpriteState = SpriteState.IDLE_RIGHT;
            }

        }

        #endregion

        #region Audio Functions


        #region Unit Dying Sound Decision Functions

        public void PlayUnitDeathAudio(Friendly friendlyActor)
        {
            switch (friendlyActor.CreatureType)
            {

                case ObjectType.DRAGON:
                    PlayDragonDeathSounds();
                    break;

                case ObjectType.NECROMANCER:
                    PlayNecromancerDeathSounds();
                    break;

                case ObjectType.ARCANE_MAGE:
                    PlayArcaneMageDeathSounds();
                    break;

                case ObjectType.FIRE_MAGE:
                    PlayFireMageDeathSounds();
                    break;

                case ObjectType.WOLF:
                    PlayWolfDeathSounds();
                    break;

                case ObjectType.AXE_THROWER:
                    PlayAxeThrowerDeathSounds();
                    break;

                case ObjectType.BERSERKER:
                    PlayBerserkerDeathSounds();
                    break;

                case ObjectType.CLERIC:
                    PlayClericDeathSound();
                    break;

                default:
                    break;
            }
        }


        #region Cleric Death Sounds

        private void PlayClericDeathSound()
        {

            int temp = randomNumberGenerator.Next(0, 1);

            switch (temp)
            {
                case 0:
                    AudioController.ClericDeath1SoundEffectInstance.Volume = mClericDeathSoundVolume;
                    AudioController.ClericDeath1SoundEffectInstance.Play();
                    break;

                //case 1:
                //    AudioController.ClericDeath2SoundEffectInstance.Volume = mClericDeathSoundVolume;
                //    AudioController.ClericDeath2SoundEffectInstance.Play();
                //    break;

                default:
                    break;
            }

        }


        #endregion

        #region Dragon Death Sounds

        private void PlayDragonDeathSounds()
        {
            int temp = randomNumberGenerator.Next(0, 2);

            switch (temp)
            {
                case 0:
                    AudioController.DragonDeath1SoundEffectInstance.Volume = mDragonDeathSoundVolume;
                    AudioController.DragonDeath1SoundEffectInstance.Play();
                    break;
                case 1:
                    AudioController.DragonDeath2SoundEffectInstance.Volume = mDragonDeathSoundVolume;
                    AudioController.DragonDeath2SoundEffectInstance.Play();
                    break;

                default:
                    break;
            }
        }
        #endregion

        #region Arcane Mage Death Sounds

        private void PlayArcaneMageDeathSounds()
        {
            int temp = randomNumberGenerator.Next(0, 2);

            switch (temp)
            {
                case 0:
                    AudioController.ArcaneMageDeath1SoundEffectInstance.Volume = mArcaneMageDeathSoundVolume;
                    AudioController.ArcaneMageDeath1SoundEffectInstance.Play();
                    break;
                case 1:
                    AudioController.ArcaneMageDeath2SoundEffectInstance.Volume = mArcaneMageDeathSoundVolume;
                    AudioController.ArcaneMageDeath2SoundEffectInstance.Play();
                    break;

                default:
                    break;
            }
        }
        #endregion

        #region Necromancer Death Sounds
        private void PlayNecromancerDeathSounds()
        {
            int temp = randomNumberGenerator.Next(0, 1);

            switch (temp)
            {
                case 0:
                    AudioController.NecromancerDeathSoundEffectInstance.Volume = mNecromancerDeathSoundVolume;
                    AudioController.NecromancerDeathSoundEffectInstance.Play();
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region Wolf Death Sounds

        private void PlayWolfDeathSounds()
        {
            int temp = randomNumberGenerator.Next(0, 2);

            switch (temp)
            {
                case 0:
                    AudioController.WolfDeath1SoundEffectInstance.Volume = mWolfDeathSoundVolume;
                    AudioController.WolfDeath1SoundEffectInstance.Play();
                    break;
                case 1:
                    AudioController.WolfDeath2SoundEffectInstance.Volume = mWolfDeathSoundVolume;
                    AudioController.WolfDeath2SoundEffectInstance.Play();
                    break;

                default:
                    break;
            }
        }
        #endregion

        #region Berserker Death Sounds

        private void PlayBerserkerDeathSounds()
        {
            int temp = randomNumberGenerator.Next(0, 3);

            switch (temp)
            {
                case 0:
                    AudioController.BerserkerDeath1SoundEffectInstance.Volume = mBerserkerDeathSoundVolume;
                    AudioController.BerserkerDeath1SoundEffectInstance.Play();
                    break;

                case 1:
                    AudioController.BerserkerDeath2SoundEffectInstance.Volume = mBerserkerDeathSoundVolume;
                    AudioController.BerserkerDeath2SoundEffectInstance.Play();
                    break;

                case 2:
                    AudioController.BerserkerDeath3SoundEffectInstance.Volume = mBerserkerDeathSoundVolume;
                    AudioController.BerserkerDeath3SoundEffectInstance.Play();
                    break;

                default:
                    break;
            }
        }
        #endregion

        #region Axe Thrower Death Sounds

        private void PlayAxeThrowerDeathSounds()
        {
            int temp = randomNumberGenerator.Next(0, 2);

            switch (temp)
            {
                case 0:
                    AudioController.AxeThrowerDeath1SoundEffectInstance.Volume = mAxeThrowerDeathSoundVolume;
                    AudioController.AxeThrowerDeath1SoundEffectInstance.Play();
                    break;

                case 1:
                    AudioController.AxeThrowerDeath2SoundEffectInstance.Volume = mAxeThrowerDeathSoundVolume;
                    AudioController.AxeThrowerDeath2SoundEffectInstance.Play();
                    break;

                default:
                    break;
            }
        }

        #endregion

        #region Fire Mage Death Sounds
        private void PlayFireMageDeathSounds()
        {
            int temp = randomNumberGenerator.Next(0, 2);

            switch (temp)
            {
                case 0:
                    AudioController.FireMageDeath1SoundEffectInstance.Volume = mFireMageDeathSoundVolume;
                    AudioController.FireMageDeath1SoundEffectInstance.Play();
                    break;

                case 1:
                    AudioController.FireMageDeath2SoundEffectInstance.Volume = mFireMageDeathSoundVolume;
                    AudioController.FireMageDeath2SoundEffectInstance.Play();
                    break;

                default:
                    break;
            }
        }
        #endregion





        #endregion

        #region Unit Struck Sound Decision Functions

        public void PlayUnitStruckAudio(Friendly friendlyActor, Enemy EnemyActor)
        {
            //TODO: place holder
            switch (friendlyActor.TargetQueue.PeekFirst().CreatureType)
            {

                case ObjectType.BANSHEE:
                    PlayBansheeStruckSound();
                    break;
                case ObjectType.REAPER:
                    PlayReaperStruckSound();
                    break;
                case ObjectType.DOOM_HOUND:
                    PlayDoomHoundStruckSound();
                    break;
                case ObjectType.IMP:
                    PlayImpStruckSound();
                    break;
                case ObjectType.GOG:
                    PlayGogStruckSound();
                    break;

                default:
                    break;
            }
        }

        #region Banshee Struck Sounds
        private void PlayBansheeStruckSound()
        {

            int temp = randomNumberGenerator.Next(0, 2);

            switch (temp)
            {

                case 0:
                    AudioController.BansheeStruck1SoundEffectInstance.Volume = mBansheeStruckSoundVolume;
                    AudioController.BansheeStruck1SoundEffectInstance.Play();
                    break;
                case 1:
                    AudioController.BansheeStruck2SoundEffectInstance.Volume = mBansheeStruckSoundVolume;
                    AudioController.BansheeStruck2SoundEffectInstance.Play();
                    break;

                case 2:

                    break;

                default:
                    break;
            }
        }

        #endregion

        #region Reaper Struck Sounds

        private void PlayReaperStruckSound()
        {
            int temp = randomNumberGenerator.Next(0, 2);
            switch (temp)
            {

                case 0:
                    AudioController.ReaperStruck1SoundEffectInstance.Volume = mReaperStruckSoundVolume;
                    AudioController.ReaperStruck1SoundEffectInstance.Play();
                    break;
                case 1:
                    AudioController.ReaperStruck2SoundEffectInstance.Volume = mReaperStruckSoundVolume;
                    AudioController.ReaperStruck2SoundEffectInstance.Play();
                    break;

                case 2:

                    break;

                default:
                    break;
            }
        }
        #endregion

        #region Gog Struck Sounds

        private void PlayGogStruckSound()
        {
            int temp = randomNumberGenerator.Next(0, 2);
            switch (temp)
            {



                case 0:
                    AudioController.GogStruck2SoundEffectInstance.Volume = mGogStruckSoundVolume;
                    AudioController.GogStruck2SoundEffectInstance.Play();
                    break;
                case 1:
                    AudioController.GogStruck2SoundEffectInstance.Volume = mGogStruckSoundVolume;
                    AudioController.GogStruck2SoundEffectInstance.Play();
                    break;

                default:
                    break;
            }
        }

        #endregion

        #region Imp Struck Sounds

        private void PlayImpStruckSound()
        {
            int temp = randomNumberGenerator.Next(0, 2);
            switch (temp)
            {
                case 0:
                    AudioController.ImpStruck1SoundEffectInstance.Volume = mImpStruckSoundVolume;
                    AudioController.ImpStruck1SoundEffectInstance.Play();

                    break;
                case 1:
                    AudioController.ImpStruck2SoundEffectInstance.Volume = mImpStruckSoundVolume;
                    AudioController.ImpStruck2SoundEffectInstance.Play();
                    break;

                case 2:

                    break;

                default:
                    break;
            }
        }

        #endregion

        #region DoomHound Struck Sounds

        private void PlayDoomHoundStruckSound()
        {
            int temp = randomNumberGenerator.Next(0, 1);
            switch (temp)
            {


                case 0:
                    AudioController.DoomHoundStruckSoundEffectInstance.Volume = mDoomHoundStruckSoundVolume;
                    AudioController.DoomHoundStruckSoundEffectInstance.Play();
                    break;

                default:
                    break;
            }
        }

        #endregion



        #endregion

        #region Unit Attack Sound Decision Functions

        public void PlayUnitAnimationAudio(Friendly friendlyActor)
        {

            // Decide what type of unit I am.
            // Pick a random number and use that to select which sound effect I make this time.

            switch (friendlyActor.CreatureType)
            {

                case ObjectType.DRAGON:
                    PlayDragonAnimationSound();
                    break;
                case ObjectType.NECROMANCER:

                    break;
                case ObjectType.ARCANE_MAGE:
                    PlayArcaneMageAnimationSound();
                    break;
                case ObjectType.FIRE_MAGE:
                    PlayFireMageAnimationSound();
                    break;
                case ObjectType.WOLF:
                    PlayWolfAnimationSound();
                    break;
                case ObjectType.AXE_THROWER:
                    PlayAxeThrowerAnimationSound();

                    break;
                case ObjectType.BERSERKER:
                    PlayBerserkerAnimationSound();
                    break;

                case ObjectType.CLERIC:

                    break;

                default:
                    break;
            }
        }


        public void PlayUnitAttackLandedAudio(Friendly friendlyActor)
        {

            // Decide what type of unit I am.
            // Pick a random number and use that to select which sound effect I make this time.

            switch (friendlyActor.CreatureType)
            {
                case ObjectType.WOLF:
                    PlayWolfAttackLandedSound();
                    break;

                case ObjectType.BERSERKER:
                    PlayBerserkerAttackLandedSound();
                    break;

                default:
                    break;
            }
        }

        #region Dragon Sounds

        private void PlayDragonAnimationSound()
        {
            int temp = randomNumberGenerator.Next(0, 2);

            switch (temp)
            {
                case 0:
                    AudioController.DragonFireAttackSoundEffectInstance.Volume = mDragonAnimationSoundVolume;
                    AudioController.DragonFireAttackSoundEffectInstance.Play();
                    break;

                case 1:
                    AudioController.DragonFireOnlySoundEffecttInstance.Volume = mDragonAnimationSoundVolume;
                    AudioController.DragonFireOnlySoundEffecttInstance.Play();
                    break;

                default:
                    break;
            }
        }
        #endregion

        #region Axe Thrower Sounds

        private void PlayAxeThrowerAnimationSound()
        {
            int temp = randomNumberGenerator.Next(0, 2);

            switch (temp)
            {
                case 0:
                    AudioController.AxeThrowerAttack1SoundEffectInstance.Volume = mAxeThrowerAnimationSoundVolume;
                    AudioController.AxeThrowerAttack1SoundEffectInstance.Play();
                    break;
                case 1:
                    AudioController.AxeThrowerAttack2SoundEffectInstance.Volume = mAxeThrowerAnimationSoundVolume;
                    AudioController.AxeThrowerAttack2SoundEffectInstance.Play();
                    break;

                default:
                    break;
            }

        }
        #endregion

        #region Berserker Sounds

        private void PlayBerserkerAnimationSound()
        {
            int temp = randomNumberGenerator.Next(0, 3);

            switch (temp)
            {
                case 0:
                    AudioController.BerserkerAttack1SoundEffectInstance.Volume = mBerserkerAnimationSoundVolume;
                    AudioController.BerserkerAttack1SoundEffectInstance.Play();
                    break;

                case 1:
                    AudioController.BerserkerAttack2SoundEffectInstance.Volume = mBerserkerAnimationSoundVolume;
                    AudioController.BerserkerAttack2SoundEffectInstance.Play();
                    break;

                case 2:
                    AudioController.BerserkerAttack2SoundEffectInstance.Volume = mBerserkerAnimationSoundVolume;
                    AudioController.BerserkerAttack3SoundEffectInstance.Play();
                    break;

                default:

                    break;
            }
        }


        private void PlayBerserkerAttackLandedSound()
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
        #endregion

        #region Wolf Sounds
        private void PlayWolfAnimationSound()
        {
            int temp = randomNumberGenerator.Next(0, 2);
            switch (temp)
            {
                case 0:
                    AudioController.WolfAttack1SoundEffectInstance.Volume = mWolfAnimationSoundVolume;
                    AudioController.WolfAttack1SoundEffectInstance.Play();

                    break;
                case 1:
                    AudioController.WolfAttack2SoundEffectInstance.Volume = mWolfAnimationSoundVolume;
                    AudioController.WolfAttack2SoundEffectInstance.Play();

                    break;
                default:
                    break;

            }

        }

        private void PlayWolfAttackLandedSound()
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

                    break;

                default:
                    break;
            }

        }
        #endregion

        #region Necromancer Sounds
        #endregion

        #region Cleric Sounds
        private void PlayClericAnimationSound()
        {
            int temp = randomNumberGenerator.Next(0, 1);

            switch (temp)
            {
                case 0:
                    AudioController.ClericHealSpellSoundEffectInstance.Volume = mClericAnimationSoundVolume;
                    AudioController.ClericHealSpellSoundEffectInstance.Play();
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region FireMage Sounds

        private void PlayFireMageAnimationSound()
        {
            int temp = randomNumberGenerator.Next(0, 1);

            switch (temp)
            {
                case 0:
                    AudioController.FireMageAttackGruntSoundEffectInstance.Volume = mFireMageAnimationSoundVolume;
                    AudioController.FireMageAttackGruntSoundEffectInstance.Play();
                    break;

                default:
                    break;
            }

        }
        #endregion

        #region Arcane Mage Sounds
        private void PlayArcaneMageAnimationSound()
        {
            int temp = randomNumberGenerator.Next(0, 3);

            switch (temp)
            {
                case 0:
                    AudioController.ArcaneMageAttackSoundEffectInstance.Volume = mArcaneMageAnimationSoundVolume;
                    AudioController.ArcaneMageAttackSoundEffectInstance.Play();


                    break;
                case 1:
                    AudioController.ArcaneMageAttackGruntSoundEffectInstance.Volume = mArcaneMageAnimationSoundVolume;
                    AudioController.ArcaneMageAttackGruntSoundEffectInstance.Play();
                    break;

                case 2:

                    break;

                default:
                    break;
            }

        }
        #endregion

        #endregion

        #endregion

        #region Handle CurrentTarget Functions

        public void ScanGlobalList(Friendly friendlyActor)
        {

            GameObject.mPotentialTargetListList.Reset();

            // Check the global list to see if another object's sprite frame intersects our FOV.
            // If so
            // InsertTargetIntoQueue

            for (int i = 0; i < GameObject.mPotentialTargetListList.GetCount(); i++)
            {
                currentTarget = GameObject.mPotentialTargetListList.GetCurrent().gameObject;

                // If my field of view intersects another object's frame.
                // And if the other object is not an friendlyActor.
                // And if the other object is not a castle.
                // And if the other object is not a structure.
                if (friendlyActor.FieldOfView.Intersects(currentTarget.Sprite.SpriteFrame)
                    && currentTarget.Hostility != Hostility.FRIENDLY
                    && currentTarget.Hostility != Hostility.CASTLE
                    && currentTarget.Hostility != Hostility.STRUCTURE)
                {
                    // Reset current node back to first node.
                    friendlyActor.TargetQueue.mTargetList.Reset();

                    // Check to make sure he is not already in my queue.
                    isInQueue = false;
                    for (int j = 0; j < friendlyActor.TargetQueue.mTargetList.GetCount(); j++)
                    {
                        if (currentTarget.ObjectID == friendlyActor.TargetQueue.mTargetList.GetCurrent().gameObject.ObjectID)
                        {
                            isInQueue = true;

                        }
                        // Go to next node in Queue
                        friendlyActor.TargetQueue.mTargetList.NextNode();
                    }
                    if (!isInQueue)
                    {
                        InsertTargetIntoQueue(friendlyActor);
                    }

                }

                GameObject.mPotentialTargetListList.NextNode();

            }

        }

        public void InsertTargetIntoQueue(Friendly friendlyActor)
        {

            // current object = THIS object.
            // hostile object = object that ran into my field of view.

            // If my target queue is empty 
            if (friendlyActor.TargetQueue.IsEmpty())
            {
                //Push onto queue.
                friendlyActor.TargetQueue.InsertTarget(currentTarget);
            }

                    // If the hostile object is a creature.
            else if (currentTarget.Hostility == Hostility.ENEMY)
            {

                // Reset the current object's target queue's current node to = first node.
                friendlyActor.TargetQueue.mTargetList.Reset();

                // Iterate through the current object's target queue to determine where to put the new target.
                for (int i = 0; i < friendlyActor.TargetQueue.mTargetList.GetCount(); i++)
                {

                    // If first target is a senemy
                    if (friendlyActor.TargetQueue.mTargetList.GetCurrent().gameObject.Hostility ==
                        Hostility.ENEMY)
                    {
                        // Push creatue to front of the line.
                        friendlyActor.TargetQueue.mTargetList.InsertLast(currentTarget);
                        // We placed the target and now we're done.
                        break;
                    }

                    friendlyActor.TargetQueue.mTargetList.NextNode();
                }
            }



        }

        public void MaintainTarget(Friendly friendlyActor)
        {

            // If my target queue is not empty.
            // And my FOV is no longer intersecting my first target's sprite frame.
            if (!(friendlyActor.TargetQueue.IsEmpty()) &&
                !(friendlyActor.FieldOfView.Intersects(friendlyActor.TargetQueue.PeekFirst().Sprite.SpriteFrame)))
            {
                friendlyActor.TargetQueue.PopTarget();
            }

        }


        #endregion Handle CurrentTarget Functions

    }
}
