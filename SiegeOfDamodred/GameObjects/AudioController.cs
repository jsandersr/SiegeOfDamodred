using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace GameObjects
{
    public static class AudioController
    {

        #region GameLoop Functions

        public static ContentManager Content;

        #endregion

        #region Sound Effect Paths



        #region RoundOverIndicators
        


        #endregion
        #region Item Drop Sound Effect Paths

        private const string GoldDropCoin1SoundEffectPath = "Audio/ItemDrops/GoldDrop/Coin 1";
        private const string GoldDropCoin2SoundEffectPath = "Audio/ItemDrops/GoldDrop/Coin 2";
        private const string GoldDropCoin3SoundEffectPath = "Audio/ItemDrops/GoldDrop/Coin 3";

        private const string HealthDropSoundEffectPath = "Audio/ItemDrops/HealthDrop/Health Drop";

        #endregion


        #region Dragon Sound Effect Paths

        private const string DragonDeath1SoundEffectPath = "Audio/Dragon/Dragon Death 1";
        private const string DragonDeath2SoundEffectPath = "Audio/Dragon/Dragon Death 2";
        private const string DragonFireAttackSoundEffectPath = "Audio/Dragon/Dragon Fire Attack";
        private const string DragonFireOnlySoundEffectPath = "Audio/Dragon/Dragon Fire Only";
        private const string DragonStruck1SoundEffectPath = "Audio/Dragon/Dragon Struck 1";
        private const string DragonStruck2SoundEffectPath = "Audio/Dragon/Dragon Struck 2";

        #endregion

        #region Arcane Mage Sound Effect Paths

        private const string ArcaneMageAttackSoundEffectPath = "Audio/ArcaneMage/Mage - Arcane Attack"; // Sound effect for launching projectile.
        private const string ArcaneMageBlastSoundEffectPath = "Audio/ArcaneMage/Mage - Arcane Blast"; // Sound effect for impacting projectile.
        private const string ArcaneMageAttackGruntSoundEffectPath = "Audio/ArcaneMage/Mage Attack Grunt";
        private const string ArcaneMageDeath1SoundEffectPath = "Audio/ArcaneMage/Mage Death 1";
        private const string ArcaneMageDeath2SoundEffectPath = "Audio/ArcaneMage/Mage Death 2";
        private const string ArcaneMageStruck1SoundEffectPath = "Audio/ArcaneMage/Mage Struck 1";
        private const string ArcaneMageStruck2SoundEffectPath = "Audio/ArcaneMage/Mage Struck 2";

        #endregion

        #region Axe Thrower Sound Effect Paths

        private const string AxeThrowSoundEffectPath = "Audio/AxeThrower/Axe Throw"; // Sound effect for launching projectile.
        private const string AxeWhooshLoopSoundEffectPath = "Audio/AxeThrower/Axe Whoosh Loop"; // Sound effect for impacting projectile.
        private const string AxeThrowerAttack1SoundEffectPath = "Audio/AxeThrower/AxeThrower Attack 1";
        private const string AxeThrowerAttack2SoundEffectPath = "Audio/AxeThrower/AxeThrower Attack 2";
        private const string AxeThrowerDeath1SoundEffectPath = "Audio/AxeThrower/AxeThrower Death 1";
        private const string AxeThrowerDeath2SoundEffectPath = "Audio/AxeThrower/AxeThrower Death 2";
        private const string AxeThroweStruck1SoundEffectPath = "Audio/AxeThrower/AxeThrower Struck 1";
        private const string AxeThroweStruck2SoundEffectPath = "Audio/AxeThrower/AxeThrower Struck 2";
        private const string AxeThroweStruck3SoundEffectPath = "Audio/AxeThrower/AxeThrower Struck 3";

        #endregion

        #region Banshee Sound Effect Paths

        private const string BansheeSkullBlastSoundEffectPath = "Audio/Banshee/Banshee Skull Blast";
        private static string BansheeAttackSoundEffectPath = "Audio/Banshee/Banshee Attack";
        private static string BansheeStruck1SoundEffectPath = "Audio/Banshee/Banshee Struck 1";
        private static string BansheeStruck2SoundEffectPath = "Audio/Banshee/Banshee Struck 2";
        private static string BansheeDeathSoundEffectPath = "Audio/Banshee/Banshee Death";

        #endregion

        #region Berserker Sound Effect Paths

        private const string BerserkerAttack1SoundEffectPath = "Audio/Berserker/Berserker Attack 1";
        private const string BerserkerAttack2SoundEffectPath = "Audio/Berserker/Berserker Attack 2";
        private const string BerserkerAttack3SoundEffectPath = "Audio/Berserker/Berserker Attack 3";
        private const string BerserkerDeath1SoundEffectPath = "Audio/Berserker/Berserker Death 1";
        private const string BerserkerDeath2SoundEffectPath = "Audio/Berserker/Berserker Death 2";
        private const string BerserkerDeath3SoundEffectPath = "Audio/Berserker/Berserker Death 3";
        private const string BerserkerStruck1SoundEffectPath = "Audio/Berserker/Berserker Struck 1";
        private const string BerserkerStruck2SoundEffectPath = "Audio/Berserker/Berserker Struck 2";
        private const string BerserkerStruck3SoundEffectPath = "Audio/Berserker/Berserker Struck 3";
        private const string BerserkerStruck4SoundEffectPath = "Audio/Berserker/Berserker Struck 4";
        private const string BerserkerStruck5SoundEffectPath = "Audio/Berserker/Berserker Struck 5";

        #endregion

        #region BuildingSpawn Sound Effect Paths

        private const string BuildingSpawnSoundEffectPath = "Audio/Buildings/Building Spawn";

        #endregion

        #region Button CLicks Sound Effect Paths

        private const string ButtonClick1SoundEffectPath = "Audio/ButtonClicks/Click 1";
        private const string ButtonClick2SoundEffectPath = "Audio/ButtonClicks/Click 2";
        private const string ButtonClick3SoundEffectPath = "Audio/ButtonClicks/Click 3";

        #endregion

        #region Cleric Sound Effect Paths

        private const string ClericHealSpellSoundEffectPath = "Audio/Cleric/Cleric Heal Spell";
        private const string ClericDeath1SoundEffectPath = "Audio/Cleric/Mage Death 1";
        private const string ClericDeath2SoundEffectPath = "Audio/Cleric/Mage Death 2";
        private const string ClericStruck1SoundEffectPath = "Audio/Cleric/Mage Struck 1";
        private const string ClericStruck2SoundEffectPath = "Audio/Cleric/Mage Struck 2";

        #endregion

        #region Doom Hound Sound Effect Paths

        private const string DoomHoundAttack1SoundEffectPath = "Audio/DoomHound/Doom Hound Attack 1";
        private const string DoomHoundAttack2SoundEffectPath = "Audio/DoomHound/Doom Hound Attack 2";
        private const string DoomHoundDeath1SoundEffectPath = "Audio/DoomHound/Doom Hound Death 1";
        private const string DoomHoundDeath2SoundEffectPath = "Audio/DoomHound/Doom Hound Death 2";
        private const string DoomHoundStruckSoundEffectPath = "Audio/DoomHound/Doom Hound Struck 1";

        #endregion

        #region Fire Mage Sound Effect Paths

        private const string FireMageFireBlastSoundEffectPath = "Audio/FireMage/Mage - Fire Blast";
        private const string FireMageAttackGruntSoundEffectPath = "Audio/FireMage/Mage Attack Grunt";
        private const string FireMageDeath1SoundEffectPath = "Audio/FireMage/Mage Death 1";
        private const string FireMageDeath2SoundEffectPath = "Audio/FireMage/Mage Death 2";
        private const string FireMageStruck1SoundEffectPath = "Audio/FireMage/Mage Struck 1";
        private const string FireMageStruck2SoundEffectPath = "Audio/FireMage/Mage Struck 2";

        #endregion

        #region Gog Sound Effect Paths

        private const string GogAttack1SoundEffectPath = "Audio/Gog/Gog Attack 1";
        private const string GogAttack2SoundEffectPath = "Audio/Gog/Gog Attack 2";
        private const string GogDeath1SoundEffectPath = "Audio/Gog/Gog Death 1";
        private const string GogDeath2SoundEffectPath = "Audio/Gog/Gog Death 2";
        private const string GogFireBlastSoundEffectPath = "Audio/Gog/Gog Fire Blast";
        private const string GogStruck1SoundEffectPath = "Audio/Gog/Gog Struck 1";
        private const string GogStruck2SoundEffectPath = "Audio/Gog/Gog Struck 2";

        #endregion

        #region Hero Sound Effect Paths

        private const string HeroAttack1SoundEffectPath = "Audio/Hero/Hero Attack 1";
        private const string HeroAttack2SoundEffectPath = "Audio/Hero/Hero Attack 2";
        private const string HeroBlessSpellSoundEffectPath = "Audio/Hero/Hero Bless Spell";
        private const string HeroCurseSpellSoundEffectPath = "Audio/Hero/Hero Curse Spell";
        private const string HeroDeathSoundEffectPath = "Audio/Hero/Hero Death";
        private const string HeroStruck1SoundEffectPath = "Audio/Hero/Hero Struck 1";
        private const string HeroStruck2SoundEffectPath = "Audio/Hero/Hero Struck 2";
        private const string HeroWarCrySoundEffectPath = "Audio/Hero/Hero War Cry";
        private const string HeroLevelGainSoundEffectPath = "Audio/Hero/Level Gain";

        #endregion

        #region Imp Sound Effect Paths

        private const string ImpAttack1SoundEffectPath = "Audio/Imp/Imp Attack 1";
        private const string ImpAttack2SoundEffectPath = "Audio/Imp/Imp Attack 2";
        private const string ImpDeathSoundEffectPath = "Audio/Imp/Imp Death";
        private const string ImpSpawnSoundEffectPath = "Audio/Imp/Imp Spawn - Kill Them";
        private const string ImpStruck1SoundEffectPath = "Audio/Imp/Imp Struck 1";
        private const string ImpStruck2SoundEffectPath = "Audio/Imp/Imp Struck 2";

        #endregion

        #region Necromancer Sound Effect Paths

        private const string NecromancerLifeDrain1SoundEffectPath = "Audio/Necromancer/Necro Life Drain 1";
        private const string NecromancerLifeDrain2SoundEffectPath = "Audio/Necromancer/Necro Life Drain 1";
        private const string NecromancerDeathSoundEffectPath = "Audio/Necromancer/Necro Mage Death";
        private const string NecromancerStruck1SoundEffectPath = "Audio/Necromancer/Necro Struck 1";
        private const string NecromancerStruck2SoundEffectPath = "Audio/Necromancer/Necro Struck 2";

        #endregion


        #region Reaper Sound Effect Paths

        private const string ReaperStruck1SoundEffectPath = "Audio/Reaper/Reaper Struck 1";
        private const string ReaperStruck2SoundEffectPath = "Audio/Reaper/Reaper Struck 2";
        private const string ReaperDeathSoundEffectPath = "Audio/Reaper/Reaper Death";
        private const string ReaperAttackSoundEffectPath = "Audio/Reaper/Reaper Attack";
        #endregion


        #region Unit Melee Struck Sound Effect Paths

        private const string MeleeBoneSnapSoundEffectPath = "Audio/UnitStruckSounds/Hit - Bone Snap";
        private const string MeleePunch1SoundEffectPath = "Audio/UnitStruckSounds/Hit - Punch 1";
        private const string MeleePunch2SoundEffectPath = "Audio/UnitStruckSounds/Hit - Punch 2";
        private const string MeleePunch3SoundEffectPath = "Audio/UnitStruckSounds/Hit - Punch 3";
        private const string MeleePunch4SoundEffectPath = "Audio/UnitStruckSounds/Hit - Punch 4";
        private const string MeleePunchSoundEffectPath = "Audio/UnitStruckSounds/Hit - Punch 5";
        private const string MeleeWetStab1SoundEffectPath = "Audio/UnitStruckSounds/Hit - Wet Stab 1";
        private const string MeleeWetStab2SoundEffectPath = "Audio/UnitStruckSounds/Hit - Wet Stab 2";
        private const string MeleeWetStab3SoundEffectPath = "Audio/UnitStruckSounds/Hit - Wet Stab 3";

        #endregion

        #region Wolf Sound Effect Paths

        private const string WolfAttack1SoundEffectPath = "Audio/Wolf/Wolf Attack 1";
        private const string WolfAttack2SoundEffectPath = "Audio/Wolf/Wolf Attack 2";
        private const string WolfDeath1SoundEffectPath = "Audio/Wolf/Wolf Death 1";
        private const string WolfDeath2SoundEffectPath = "Audio/Wolf/Wolf Death 2";
        private const string WolfStruckSoundEffectPath = "Audio/Wolf/Wolf Struck";

        #endregion

        #endregion

        #region Sound Effect Fields


        #region Item Drop Sound Effect Paths

        private static SoundEffect GoldDropCoin1SoundEffect;
        private static SoundEffect GoldDropCoin2SoundEffect;
        private static SoundEffect GoldDropCoin3SoundEffect;

        private static SoundEffect HealthDropSoundEffect;

        #endregion

        #region Dragon Sound Effects

        private static SoundEffect DragonDeath1SoundEffect;
        private static SoundEffect DragonDeath2SoundEffect;
        private static SoundEffect DragonFireAttackSoundEffect;
        private static SoundEffect DragonFireOnlySoundEffect;
        private static SoundEffect DragonStruck1SoundEffect;
        private static SoundEffect DragonStruck2SoundEffect;

        #endregion

        #region Arcane Mage Sound Effects

        private static SoundEffect ArcaneMageAttackSoundEffect; // Sound effect for launching projectile.
        private static SoundEffect ArcaneMageBlastSoundEffect; // Sound effect for impacting projectile.
        private static SoundEffect ArcaneMageAttackGruntSoundEffect;
        private static SoundEffect ArcaneMageDeath1SoundEffect;
        private static SoundEffect ArcaneMageDeath2SoundEffect;
        private static SoundEffect ArcaneMageStruck1SoundEffect;
        private static SoundEffect ArcaneMageStruck2SoundEffect;

        #endregion

        #region Axe Thrower Sound Effects

        private static SoundEffect AxeThrowSoundEffect; // Sound effect for launching projectile.
        private static SoundEffect AxeWhooshLoopSoundEffect; // Sound effect for impacting projectile.
        private static SoundEffect AxeThrowerAttack1SoundEffect;
        private static SoundEffect AxeThrowerAttack2SoundEffect;
        private static SoundEffect AxeThrowerDeath1SoundEffect;
        private static SoundEffect AxeThrowerDeath2SoundEffect;
        private static SoundEffect AxeThroweStruck1SoundEffect;
        private static SoundEffect AxeThroweStruck2SoundEffect;
        private static SoundEffect AxeThroweStruck3SoundEffect;

        #endregion

        #region Banshee Sound Effects

        private static SoundEffect BansheeSkullBlastSoundEffect;
        private static SoundEffect BansheeAttackSoundEffect;
        private static SoundEffect BansheeStruck1SoundEffect;
        private static SoundEffect BansheeStruck2SoundEffect;
        private static SoundEffect BansheeDeathSoundEffect;


        #endregion

        #region Berserker Sound Effects

        private static SoundEffect BerserkerAttack1SoundEffect;
        private static SoundEffect BerserkerAttack2SoundEffect;
        private static SoundEffect BerserkerAttack3SoundEffect;
        private static SoundEffect BerserkerDeath1SoundEffect;
        private static SoundEffect BerserkerDeath2SoundEffect;
        private static SoundEffect BerserkerDeath3SoundEffect;
        private static SoundEffect BerserkerStruck1SoundEffect;
        private static SoundEffect BerserkerStruck2SoundEffect;
        private static SoundEffect BerserkerStruck3SoundEffect;
        private static SoundEffect BerserkerStruck4SoundEffect;
        private static SoundEffect BerserkerStruck5SoundEffect;

        #endregion

        #region BuildingSpawn Sound Effects

        private static SoundEffect BuildingSpawnSoundEffect;

        #endregion

        #region Button CLicks Sound Effects

        private static SoundEffect ButtonClick1SoundEffect;
        private static SoundEffect ButtonClick2SoundEffect;
        private static SoundEffect ButtonClick3SoundEffect;

        #endregion

        #region Cleric Sound Effects

        private static SoundEffect ClericHealSpellSoundEffect;
        private static SoundEffect ClericDeath1SoundEffect;
        private static SoundEffect ClericDeath2SoundEffect;
        private static SoundEffect ClericStruck1SoundEffect;
        private static SoundEffect ClericStruck2SoundEffect;

        #endregion

        #region Doom Hound Sound Effects

        private static SoundEffect DoomHoundAttack1SoundEffect;
        private static SoundEffect DoomHoundAttack2SoundEffect;
        private static SoundEffect DoomHoundDeath1SoundEffect;
        private static SoundEffect DoomHoundDeath2SoundEffect;
        private static SoundEffect DoomHoundStruckSoundEffect;

        #endregion

        #region Fire Mage Sound Effects

        private static SoundEffect FireMageFireBlastSoundEffect;
        private static SoundEffect FireMageAttackGruntSoundEffect;
        private static SoundEffect FireMageDeath1SoundEffect;
        private static SoundEffect FireMageDeath2SoundEffect;
        private static SoundEffect FireMageStruck1SoundEffect;
        private static SoundEffect FireMageStruck2SoundEffect;

        #endregion

        #region Gog Sound Effects

        private static SoundEffect GogAttack1SoundEffect;
        private static SoundEffect GogAttack2SoundEffect;
        private static SoundEffect GogDeath1SoundEffect;
        private static SoundEffect GogDeath2SoundEffect;
        private static SoundEffect GogFireBlastSoundEffect;
        private static SoundEffect GogStruck1SoundEffect;
        private static SoundEffect GogStruck2SoundEffect;


        #endregion

        #region Hero Sound Effects

        private static SoundEffect HeroAttack1SoundEffect;
        private static SoundEffect HeroAttack2SoundEffect;
        private static SoundEffect HeroBlessSpellSoundEffect;
        private static SoundEffect HeroCurseSpellSoundEffect;
        private static SoundEffect HeroDeathSoundEffect;
        private static SoundEffect HeroStruck1SoundEffect;
        private static SoundEffect HeroStruck2SoundEffect;
        private static SoundEffect HeroWarCrySoundEffect;
        private static SoundEffect HeroLevelGainSoundEffect;

        #endregion

        #region Imp Sound Effects

        private static SoundEffect ImpAttack1SoundEffect;
        private static SoundEffect ImpAttack2SoundEffect;
        private static SoundEffect ImpDeathSoundEffect;
        private static SoundEffect ImpSpawnSoundEffect;
        private static SoundEffect ImpStruck1SoundEffect;
        private static SoundEffect ImpStruck2SoundEffect;

        #endregion

        #region Necromancer Sound Effects

        private static SoundEffect NecromancerLifeDrain1SoundEffect;
        private static SoundEffect NecromancerLifeDrain2SoundEffect;
        private static SoundEffect NecromancerDeathSoundEffect;
        private static SoundEffect NecromancerStruck1SoundEffect;
        private static SoundEffect NecromancerStruck2SoundEffect;

        #endregion


        #region Reaper Sound Effects

        private static SoundEffect ReaperStruck1SoundEffect;
        private static SoundEffect ReaperStruck2SoundEffect;
        private static SoundEffect ReaperDeathSoundEffect;
        private static SoundEffect ReaperAttackSoundEffect;

        #endregion


        #region Unit Melee Struck Sound Effects

        private static SoundEffect MeleeBoneSnapSoundEffect;
        private static SoundEffect MeleePunch1SoundEffect;
        private static SoundEffect MeleePunch2SoundEffect;
        private static SoundEffect MeleePunch3SoundEffect;
        private static SoundEffect MeleePunch4SoundEffect;
        private static SoundEffect MeleePunchSoundEffect;
        private static SoundEffect MeleeWetStab1SoundEffect;
        private static SoundEffect MeleeWetStab2SoundEffect;
        private static SoundEffect MeleeWetStab3SoundEffect;

        #endregion

        #region Wolf Sound Effects

        private static SoundEffect WolfAttack1SoundEffect;
        private static SoundEffect WolfAttack2SoundEffect;
        private static SoundEffect WolfDeath1SoundEffect;
        private static SoundEffect WolfDeath2SoundEffect;
        private static SoundEffect WolfStruckSoundEffect;

        #endregion

        #endregion

        #region  Sound Effect Instances



        #region Item Drop Sound Effect Instance Paths

        private static SoundEffectInstance mGoldDropCoin1SoundEffectInstance;
        private static SoundEffectInstance mGoldDropCoin2SoundEffectInstance;
        private static SoundEffectInstance mGoldDropCoin3SoundEffectInstance;


        private static SoundEffectInstance mHealthDropSoundEffectInstance;
        #endregion


        #region Dragon Sound Instances

        private static SoundEffectInstance mDragonDeath1SoundEffectInstance;
        private static SoundEffectInstance mDragonDeath2SoundEffectInstance;
        private static SoundEffectInstance mDragonFireAttackSoundEffectInstance;
        private static SoundEffectInstance mDragonFireOnlySoundEffecttInstance;
        private static SoundEffectInstance mDragonStruck1SoundEffectInstance;
        private static SoundEffectInstance mDragonStruck2SoundEffectInstance;

        #endregion

        #region Arcane Mage Sound Effect Instances

        private static SoundEffectInstance mArcaneMageAttackSoundEffectInstance; // Sound effect for launching projectile.
        private static SoundEffectInstance mArcaneMageBlastSoundEffectInstance; // Sound effect for impacting projectile.
        private static SoundEffectInstance mArcaneMageAttackGruntSoundEffectInstance;
        private static SoundEffectInstance mArcaneMageDeath1SoundEffectInstance;
        private static SoundEffectInstance mArcaneMageDeath2SoundEffectInstance;
        private static SoundEffectInstance mArcaneMageStruck1SoundEffectInstance;
        private static SoundEffectInstance mArcaneMageStruck2SoundEffectInstance;

        #endregion

        #region Axe Thrower Sound Effect Instance

        private static SoundEffectInstance mAxeThrowSoundEffectInstance; // Sound effect for launching projectile.
        private static SoundEffectInstance mAxeWhooshLoopSoundEffectInstance; // Sound effect for impacting projectile.
        private static SoundEffectInstance mAxeThrowerAttack1SoundEffectInstance;
        private static SoundEffectInstance mAxeThrowerAttack2SoundEffectInstance;
        private static SoundEffectInstance mAxeThrowerDeath1SoundEffectInstance;
        private static SoundEffectInstance mAxeThrowerDeath2SoundEffectInstance;
        private static SoundEffectInstance mAxeThrowerStruck1SoundEffectInstance;
        private static SoundEffectInstance mAxeThrowerStruck2SoundEffectInstance;
        private static SoundEffectInstance mAxeThrowerStruck3SoundEffectInstance;

        #endregion

        #region Banshee Sound Effect Instance

        private static SoundEffectInstance mBansheeSkullBlastSoundEffectInstance;
        private static SoundEffectInstance mBansheeAttackSoundEffectInstance;
        private static SoundEffectInstance mBansheeStruck1SoundEffectInstance;
        private static SoundEffectInstance mBansheeStruck2SoundEffectInstance;
        private static SoundEffectInstance mBansheeDeathSoundEffectInstance;

        #endregion

        #region Berserker Sound Effect Instance

        private static SoundEffectInstance mBerserkerAttack1SoundEffectInstance;
        private static SoundEffectInstance mBerserkerAttack2SoundEffectInstance;
        private static SoundEffectInstance mBerserkerAttack3SoundEffectInstance;
        private static SoundEffectInstance mBerserkerDeath1SoundEffectInstance;
        private static SoundEffectInstance mBerserkerDeath2SoundEffectInstance;
        private static SoundEffectInstance mBerserkerDeath3SoundEffectInstance;
        private static SoundEffectInstance mBerserkerStruck1SoundEffectInstance;
        private static SoundEffectInstance mBerserkerStruck2SoundEffectInstance;
        private static SoundEffectInstance mBerserkerStruck3SoundEffectInstance;
        private static SoundEffectInstance mBerserkerStruck4SoundEffectInstance;
        private static SoundEffectInstance mBerserkerStruck5SoundEffectInstance;

        #endregion

        #region BuildingSpawn Sound Effect Instance

        private static SoundEffectInstance mBuildingSpawnSoundEffectInstance;

        #endregion

        #region Button CLicks Sound Effect Instance

        private static SoundEffectInstance mButtonClick1SoundEffectInstance;
        private static SoundEffectInstance mButtonClick2SoundEffectInstance;
        private static SoundEffectInstance mButtonClick3SoundEffectInstance;

        #endregion

        #region Cleric Sound Effect Instance

        private static SoundEffectInstance mClericHealSpellSoundEffectInstance;
        private static SoundEffectInstance mClericDeath1SoundEffectInstance;
        private static SoundEffectInstance mClericDeath2SoundEffectInstance;
        private static SoundEffectInstance mClericStruck1SoundEffectInstance;
        private static SoundEffectInstance mClericStruck2SoundEffectInstance;

        #endregion

        #region Doom Hound Sound Effect Instance

        private static SoundEffectInstance mDoomHoundAttack1SoundEffectInstance;
        private static SoundEffectInstance mDoomHoundAttack2SoundEffectInstance;
        private static SoundEffectInstance mDoomHoundDeath1SoundEffectInstance;
        private static SoundEffectInstance mDoomHoundDeath2SoundEffectInstance;
        private static SoundEffectInstance mDoomHoundStruckSoundEffectInstance;

        #endregion

        #region Fire Mage Sound Effect Instance

        private static SoundEffectInstance mFireMageFireBlastSoundEffectInstance;
        private static SoundEffectInstance mFireMageAttackGruntSoundEffectInstance;
        private static SoundEffectInstance mFireMageDeath1SoundEffectInstance;
        private static SoundEffectInstance mFireMageDeath2SoundEffectInstance;
        private static SoundEffectInstance mFireMageStruck1SoundEffectInstance;
        private static SoundEffectInstance mFireMageStruck2SoundEffectInstance;

        #endregion

        #region Gog Sound Effect Instance

        private static SoundEffectInstance mGogAttack1SoundEffectInstance;
        private static SoundEffectInstance mGogAttack2SoundEffectInstance;
        private static SoundEffectInstance mGogDeath1SoundEffectInstance;
        private static SoundEffectInstance mGogDeath2SoundEffectInstance;
        private static SoundEffectInstance mGogFireBlastSoundEffectInstance;
        private static SoundEffectInstance mGogStruck1SoundEffectInstance;
        private static SoundEffectInstance mGogStruck2SoundEffectInstance;


        #endregion

        #region Hero Sound Effect Instance

        private static SoundEffectInstance mHeroAttack1SoundEffectInstance;
        private static SoundEffectInstance mHeroAttack2SoundEffectInstance;
        private static SoundEffectInstance mHeroBlessSpellSoundEffectInstance;
        private static SoundEffectInstance mHeroCurseSpellSoundEffectInstance;
        private static SoundEffectInstance mHeroDeathSoundEffectInstance;
        private static SoundEffectInstance mHeroStruck1SoundEffectInstance;
        private static SoundEffectInstance mHeroStruck2SoundEffectInstance;
        private static SoundEffectInstance mHeroWarCrySoundEffectInstance;
        private static SoundEffectInstance mHeroLevelGainSoundEffectInstance;

        #endregion

        #region Imp Sound Effect Instance

        private static SoundEffectInstance mImpAttack1SoundEffectInstance;
        private static SoundEffectInstance mImpAttack2SoundEffectInstance;
        private static SoundEffectInstance mImpDeathSoundEffectInstance;
        private static SoundEffectInstance mImpSpawnSoundEffectInstance;
        private static SoundEffectInstance mImpStruck1SoundEffectInstance;
        private static SoundEffectInstance mImpStruck2SoundEffectInstance;

        #endregion

        #region Necromancer Sound Effect Instance

        private static SoundEffectInstance mNecromancerLifeDrain1SoundEffectInstance;
        private static SoundEffectInstance mNecromancerLifeDrain2SoundEffectInstance;
        private static SoundEffectInstance mNecromancerDeathSoundEffectInstance;
        private static SoundEffectInstance mNecromancerStruck1SoundEffectInstance;
        private static SoundEffectInstance mNecromancerStruck2SoundEffectInstance;

        #endregion


        #region Reaper Sound Effect Instance

        private static SoundEffectInstance mReaperStruck1SoundEffectInstance;
        private static SoundEffectInstance mReaperStruck2SoundEffectInstance;
        private static SoundEffectInstance mReaperDeathSoundEffectInstance;
        private static SoundEffectInstance mReaperAttackSoundEffectInstance;

        #endregion


        #region Unit Melee Struck Sound Effect Instance

        private static SoundEffectInstance mMeleeBoneSnapSoundEffectInstance;
        private static SoundEffectInstance mMeleePunch1SoundEffectInstance;
        private static SoundEffectInstance mMeleePunch2SoundEffectInstance;
        private static SoundEffectInstance mMeleePunch3SoundEffectInstance;
        private static SoundEffectInstance mMeleePunch4SoundEffectInstance;
        private static SoundEffectInstance mMeleePunchSoundEffectInstance;
        private static SoundEffectInstance mMeleeWetStab1SoundEffectInstance;
        private static SoundEffectInstance mMeleeWetStab2SoundEffectInstance;
        private static SoundEffectInstance mMeleeWetStab3SoundEffectInstance;

        #endregion

        #region Wolf Sound Effects

        private static SoundEffectInstance mWolfAttack1SoundEffectInstance;
        private static SoundEffectInstance mWolfAttack2SoundEffectInstance;
        private static SoundEffectInstance mWolfDeath1SoundEffectInstance;
        private static SoundEffectInstance mWolfDeath2SoundEffectInstance;
        private static SoundEffectInstance mWolfStruckSoundEffectInstance;

        #endregion


        #endregion

        #region Properties

        public static SoundEffectInstance HealthDropSoundEffectInstance
        {
            get { return mHealthDropSoundEffectInstance; }
            set { mHealthDropSoundEffectInstance = value; }
        }

        public static SoundEffectInstance ReaperAttackSoundEffectInstance
        {
            get { return mReaperAttackSoundEffectInstance; }
            set { mReaperAttackSoundEffectInstance = value; }
        }

        public static SoundEffectInstance GoldDropCoin1SoundEffectInstance
        {
            get { return mGoldDropCoin1SoundEffectInstance; }
            set { mGoldDropCoin1SoundEffectInstance = value; }
        }

        public static SoundEffectInstance GoldDropCoin2SoundEffectInstance
        {
            get { return mGoldDropCoin2SoundEffectInstance; }
            set { mGoldDropCoin2SoundEffectInstance = value; }
        }

        public static SoundEffectInstance GoldDropCoin3SoundEffectInstance
        {
            get { return mGoldDropCoin3SoundEffectInstance; }
            set { mGoldDropCoin3SoundEffectInstance = value; }
        }

        public static SoundEffectInstance WolfStruckSoundEffectInstance
        {
            get { return mWolfStruckSoundEffectInstance; }
            set { mWolfStruckSoundEffectInstance = value; }
        }

        public static SoundEffectInstance WolfDeath2SoundEffectInstance
        {
            get { return mWolfDeath2SoundEffectInstance; }
            set { mWolfDeath2SoundEffectInstance = value; }
        }

        public static SoundEffectInstance WolfDeath1SoundEffectInstance
        {
            get { return mWolfDeath1SoundEffectInstance; }
            set { mWolfDeath1SoundEffectInstance = value; }
        }

        public static SoundEffectInstance WolfAttack2SoundEffectInstance
        {
            get { return mWolfAttack2SoundEffectInstance; }
            set { mWolfAttack2SoundEffectInstance = value; }
        }

        public static SoundEffectInstance WolfAttack1SoundEffectInstance
        {
            get { return mWolfAttack1SoundEffectInstance; }
            set { mWolfAttack1SoundEffectInstance = value; }
        }

        public static SoundEffectInstance MeleeWetStab3SoundEffectInstance
        {
            get { return mMeleeWetStab3SoundEffectInstance; }
            set { mMeleeWetStab3SoundEffectInstance = value; }
        }

        public static SoundEffectInstance MeleeWetStab2SoundEffectInstance
        {
            get { return mMeleeWetStab2SoundEffectInstance; }
            set { mMeleeWetStab2SoundEffectInstance = value; }
        }

        public static SoundEffectInstance MeleeWetStab1SoundEffectInstance
        {
            get { return mMeleeWetStab1SoundEffectInstance; }
            set { mMeleeWetStab1SoundEffectInstance = value; }
        }

        public static SoundEffectInstance MeleePunchSoundEffectInstance
        {
            get { return mMeleePunchSoundEffectInstance; }
            set { mMeleePunchSoundEffectInstance = value; }
        }

        public static SoundEffectInstance MeleePunch4SoundEffectInstance
        {
            get { return mMeleePunch4SoundEffectInstance; }
            set { mMeleePunch4SoundEffectInstance = value; }
        }

        public static SoundEffectInstance MeleePunch3SoundEffectInstance
        {
            get { return mMeleePunch3SoundEffectInstance; }
            set { mMeleePunch3SoundEffectInstance = value; }
        }

        public static SoundEffectInstance MeleePunch2SoundEffectInstance
        {
            get { return mMeleePunch2SoundEffectInstance; }
            set { mMeleePunch2SoundEffectInstance = value; }
        }

        public static SoundEffectInstance MeleePunch1SoundEffectInstance
        {
            get { return mMeleePunch1SoundEffectInstance; }
            set { mMeleePunch1SoundEffectInstance = value; }
        }

        public static SoundEffectInstance MeleeBoneSnapSoundEffectInstance
        {
            get { return mMeleeBoneSnapSoundEffectInstance; }
            set { mMeleeBoneSnapSoundEffectInstance = value; }
        }

        public static SoundEffectInstance ReaperDeathSoundEffectInstance
        {
            get { return mReaperDeathSoundEffectInstance; }
            set { mReaperDeathSoundEffectInstance = value; }
        }
        public static SoundEffectInstance ReaperStruck2SoundEffectInstance
        {
            get { return mReaperStruck2SoundEffectInstance; }
            set { mReaperStruck2SoundEffectInstance = value; }
        }

        public static SoundEffectInstance ReaperStruck1SoundEffectInstance
        {
            get { return mReaperStruck1SoundEffectInstance; }
            set { mReaperStruck1SoundEffectInstance = value; }
        }

        public static SoundEffectInstance NecromancerStruck2SoundEffectInstance
        {
            get { return mNecromancerStruck2SoundEffectInstance; }
            set { mNecromancerStruck2SoundEffectInstance = value; }
        }

        public static SoundEffectInstance NecromancerStruck1SoundEffectInstance
        {
            get { return mNecromancerStruck1SoundEffectInstance; }
            set { mNecromancerStruck1SoundEffectInstance = value; }
        }

        public static SoundEffectInstance NecromancerDeathSoundEffectInstance
        {
            get { return mNecromancerDeathSoundEffectInstance; }
            set { mNecromancerDeathSoundEffectInstance = value; }
        }

        public static SoundEffectInstance NecromancerLifeDrain2SoundEffectInstance
        {
            get { return mNecromancerLifeDrain2SoundEffectInstance; }
            set { mNecromancerLifeDrain2SoundEffectInstance = value; }
        }

        public static SoundEffectInstance NecromancerLifeDrain1SoundEffectInstance
        {
            get { return mNecromancerLifeDrain1SoundEffectInstance; }
            set { mNecromancerLifeDrain1SoundEffectInstance = value; }
        }

        public static SoundEffectInstance ImpStruck2SoundEffectInstance
        {
            get { return mImpStruck2SoundEffectInstance; }
            set { mImpStruck2SoundEffectInstance = value; }
        }

        public static SoundEffectInstance ImpStruck1SoundEffectInstance
        {
            get { return mImpStruck1SoundEffectInstance; }
            set { mImpStruck1SoundEffectInstance = value; }
        }

        public static SoundEffectInstance ImpSpawnSoundEffectInstance
        {
            get { return mImpSpawnSoundEffectInstance; }
            set { mImpSpawnSoundEffectInstance = value; }
        }

        public static SoundEffectInstance ImpDeathSoundEffectInstance
        {
            get { return mImpDeathSoundEffectInstance; }
            set { mImpDeathSoundEffectInstance = value; }
        }

        public static SoundEffectInstance ImpAttack2SoundEffectInstance
        {
            get { return mImpAttack2SoundEffectInstance; }
            set { mImpAttack2SoundEffectInstance = value; }
        }

        public static SoundEffectInstance HeroLevelGainSoundEffectInstance
        {
            get { return mHeroLevelGainSoundEffectInstance; }
            set { mHeroLevelGainSoundEffectInstance = value; }
        }

        public static SoundEffectInstance ImpAttack1SoundEffectInstance
        {
            get { return mImpAttack1SoundEffectInstance; }
            set { mImpAttack1SoundEffectInstance = value; }
        }

        public static SoundEffectInstance HeroWarCrySoundEffectInstance
        {
            get { return mHeroWarCrySoundEffectInstance; }
            set { mHeroWarCrySoundEffectInstance = value; }
        }

        public static SoundEffectInstance HeroStruck2SoundEffectInstance
        {
            get { return mHeroStruck2SoundEffectInstance; }
            set { mHeroStruck2SoundEffectInstance = value; }
        }

        public static SoundEffectInstance HeroStruck1SoundEffectInstance
        {
            get { return mHeroStruck1SoundEffectInstance; }
            set { mHeroStruck1SoundEffectInstance = value; }
        }

        public static SoundEffectInstance HeroDeathSoundEffectInstance
        {
            get { return mHeroDeathSoundEffectInstance; }
            set { mHeroDeathSoundEffectInstance = value; }
        }

        public static SoundEffectInstance HeroBlessSpellSoundEffectInstance
        {
            get { return mHeroBlessSpellSoundEffectInstance; }
            set { mHeroBlessSpellSoundEffectInstance = value; }
        }

        public static SoundEffectInstance HeroCurseSpellSoundEffectInstance
        {
            get { return mHeroCurseSpellSoundEffectInstance; }
            set { mHeroCurseSpellSoundEffectInstance = value; }
        }

        public static SoundEffectInstance HeroAttack2SoundEffectInstance
        {
            get { return mHeroAttack2SoundEffectInstance; }
            set { mHeroAttack2SoundEffectInstance = value; }
        }

        public static SoundEffectInstance HeroAttack1SoundEffectInstance
        {
            get { return mHeroAttack1SoundEffectInstance; }
            set { mHeroAttack1SoundEffectInstance = value; }
        }

        public static SoundEffectInstance GogStruck2SoundEffectInstance
        {
            get { return mGogStruck2SoundEffectInstance; }
            set { mGogStruck2SoundEffectInstance = value; }
        }
        public static SoundEffectInstance GogStruck1SoundEffectInstance
        {
            get { return mGogStruck1SoundEffectInstance; }
            set { mGogStruck1SoundEffectInstance = value; }
        }

        public static SoundEffectInstance GogFireBlastSoundEffectInstance
        {
            get { return mGogFireBlastSoundEffectInstance; }
            set { mGogFireBlastSoundEffectInstance = value; }
        }

        public static SoundEffectInstance GogDeath2SoundEffectInstance
        {
            get { return mGogDeath2SoundEffectInstance; }
            set { mGogDeath2SoundEffectInstance = value; }
        }

        public static SoundEffectInstance GogDeath1SoundEffectInstance
        {
            get { return mGogDeath1SoundEffectInstance; }
            set { mGogDeath1SoundEffectInstance = value; }
        }

        public static SoundEffectInstance GogAttack2SoundEffectInstance
        {
            get { return mGogAttack2SoundEffectInstance; }
            set { mGogAttack2SoundEffectInstance = value; }
        }

        public static SoundEffectInstance GogAttack1SoundEffectInstance
        {
            get { return mGogAttack1SoundEffectInstance; }
            set { mGogAttack1SoundEffectInstance = value; }
        }

        public static SoundEffectInstance FireMageStruck2SoundEffectInstance
        {
            get { return mFireMageStruck2SoundEffectInstance; }
            set { mFireMageStruck2SoundEffectInstance = value; }
        }

        public static SoundEffectInstance FireMageStruck1SoundEffectInstance
        {
            get { return mFireMageStruck1SoundEffectInstance; }
            set { mFireMageStruck1SoundEffectInstance = value; }
        }

        public static SoundEffectInstance FireMageDeath2SoundEffectInstance
        {
            get { return mFireMageDeath2SoundEffectInstance; }
            set { mFireMageDeath2SoundEffectInstance = value; }
        }

        public static SoundEffectInstance FireMageAttackGruntSoundEffectInstance
        {
            get { return mFireMageAttackGruntSoundEffectInstance; }
            set { mFireMageAttackGruntSoundEffectInstance = value; }
        }

        public static SoundEffectInstance FireMageFireBlastSoundEffectInstance
        {
            get { return mFireMageFireBlastSoundEffectInstance; }
            set { mFireMageFireBlastSoundEffectInstance = value; }
        }

        public static SoundEffectInstance DoomHoundStruckSoundEffectInstance
        {
            get { return mDoomHoundStruckSoundEffectInstance; }
            set { mDoomHoundStruckSoundEffectInstance = value; }
        }

        public static SoundEffectInstance DoomHoundDeath2SoundEffectInstance
        {
            get { return mDoomHoundDeath2SoundEffectInstance; }
            set { mDoomHoundDeath2SoundEffectInstance = value; }
        }

        public static SoundEffectInstance DoomHoundDeath1SoundEffectInstance
        {
            get { return mDoomHoundDeath1SoundEffectInstance; }
            set { mDoomHoundDeath1SoundEffectInstance = value; }
        }

        public static SoundEffectInstance FireMageDeath1SoundEffectInstance
        {
            get { return mFireMageDeath1SoundEffectInstance; }
            set { mFireMageDeath1SoundEffectInstance = value; }
        }

        public static SoundEffectInstance DoomHoundAttack2SoundEffectInstance
        {
            get { return mDoomHoundAttack2SoundEffectInstance; }
            set { mDoomHoundAttack2SoundEffectInstance = value; }
        }

        public static SoundEffectInstance DoomHoundAttack1SoundEffectInstance
        {
            get { return mDoomHoundAttack1SoundEffectInstance; }
            set { mDoomHoundAttack1SoundEffectInstance = value; }
        }

        public static SoundEffectInstance ClericStruck2SoundEffectInstance
        {
            get { return mClericStruck2SoundEffectInstance; }
            set { mClericStruck2SoundEffectInstance = value; }
        }

        public static SoundEffectInstance ClericStruck1SoundEffectInstance
        {
            get { return mClericStruck1SoundEffectInstance; }
            set { mClericStruck1SoundEffectInstance = value; }
        }

        public static SoundEffectInstance ClericDeath2SoundEffectInstance
        {
            get { return mClericDeath2SoundEffectInstance; }
            set { mClericDeath2SoundEffectInstance = value; }
        }

        public static SoundEffectInstance ClericDeath1SoundEffectInstance
        {
            get { return mClericDeath1SoundEffectInstance; }
            set { mClericDeath1SoundEffectInstance = value; }
        }

        public static SoundEffectInstance ClericHealSpellSoundEffectInstance
        {
            get { return mClericHealSpellSoundEffectInstance; }
            set { mClericHealSpellSoundEffectInstance = value; }
        }

        public static SoundEffectInstance ButtonClick3SoundEffectInstance
        {
            get { return mButtonClick3SoundEffectInstance; }
            set { mButtonClick3SoundEffectInstance = value; }
        }

        public static SoundEffectInstance ButtonClick2SoundEffectInstance
        {
            get { return mButtonClick2SoundEffectInstance; }
            set { mButtonClick2SoundEffectInstance = value; }
        }

        public static SoundEffectInstance ButtonClick1SoundEffectInstance
        {
            get { return mButtonClick1SoundEffectInstance; }
            set { mButtonClick1SoundEffectInstance = value; }
        }

        public static SoundEffectInstance BuildingSpawnSoundEffectInstance
        {
            get { return mBuildingSpawnSoundEffectInstance; }
            set { mBuildingSpawnSoundEffectInstance = value; }
        }

        public static SoundEffectInstance BerserkerStruck5SoundEffectInstance
        {
            get { return mBerserkerStruck5SoundEffectInstance; }
            set { mBerserkerStruck5SoundEffectInstance = value; }
        }

        public static SoundEffectInstance BerserkerStruck4SoundEffectInstance
        {
            get { return mBerserkerStruck4SoundEffectInstance; }
            set { mBerserkerStruck4SoundEffectInstance = value; }
        }

        public static SoundEffectInstance BerserkerStruck3SoundEffectInstance
        {
            get { return mBerserkerStruck3SoundEffectInstance; }
            set { mBerserkerStruck3SoundEffectInstance = value; }
        }

        public static SoundEffectInstance BerserkerStruck2SoundEffectInstance
        {
            get { return mBerserkerStruck2SoundEffectInstance; }
            set { mBerserkerStruck2SoundEffectInstance = value; }
        }

        public static SoundEffectInstance BerserkerStruck1SoundEffectInstance
        {
            get { return mBerserkerStruck1SoundEffectInstance; }
            set { mBerserkerStruck1SoundEffectInstance = value; }
        }

        public static SoundEffectInstance BerserkerDeath3SoundEffectInstance
        {
            get { return mBerserkerDeath3SoundEffectInstance; }
            set { mBerserkerDeath3SoundEffectInstance = value; }
        }

        public static SoundEffectInstance BerserkerDeath2SoundEffectInstance
        {
            get { return mBerserkerDeath2SoundEffectInstance; }
            set { mBerserkerDeath2SoundEffectInstance = value; }
        }

        public static SoundEffectInstance BerserkerDeath1SoundEffectInstance
        {
            get { return mBerserkerDeath1SoundEffectInstance; }
            set { mBerserkerDeath1SoundEffectInstance = value; }
        }

        public static SoundEffectInstance BerserkerAttack3SoundEffectInstance
        {
            get { return mBerserkerAttack3SoundEffectInstance; }
            set { mBerserkerAttack3SoundEffectInstance = value; }
        }

        public static SoundEffectInstance BerserkerAttack2SoundEffectInstance
        {
            get { return mBerserkerAttack2SoundEffectInstance; }
            set { mBerserkerAttack2SoundEffectInstance = value; }
        }

        public static SoundEffectInstance BerserkerAttack1SoundEffectInstance
        {
            get { return mBerserkerAttack1SoundEffectInstance; }
            set { mBerserkerAttack1SoundEffectInstance = value; }
        }

        public static SoundEffectInstance BansheeAttackSoundEffectInstance
        {
            get { return mBansheeAttackSoundEffectInstance; }
            set { mBansheeAttackSoundEffectInstance = value; }
        }

        public static SoundEffectInstance BansheeDeathSoundEffectInstance
        {
            get { return mBansheeDeathSoundEffectInstance; }
            set { mBansheeDeathSoundEffectInstance = value; }
        }

        public static SoundEffectInstance BansheeStruck1SoundEffectInstance
        {
            get { return mBansheeStruck1SoundEffectInstance; }
            set { mBansheeStruck1SoundEffectInstance = value; }
        }

        public static SoundEffectInstance BansheeStruck2SoundEffectInstance
        {
            get { return mBansheeStruck2SoundEffectInstance; }
            set { mBansheeStruck2SoundEffectInstance = value; }
        }

        public static SoundEffectInstance BansheeSkullBlastSoundEffectInstance
        {
            get { return mBansheeSkullBlastSoundEffectInstance; }
            set { mBansheeSkullBlastSoundEffectInstance = value; }
        }

        public static SoundEffectInstance AxeThrowerStruck3SoundEffectInstance
        {
            get { return mAxeThrowerStruck3SoundEffectInstance; }
            set { mAxeThrowerStruck3SoundEffectInstance = value; }
        }

        public static SoundEffectInstance AxeThrowerStruck2SoundEffectInstance
        {
            get { return mAxeThrowerStruck2SoundEffectInstance; }
            set { mAxeThrowerStruck2SoundEffectInstance = value; }
        }

        public static SoundEffectInstance AxeThrowerStruck1SoundEffectInstance
        {
            get { return mAxeThrowerStruck1SoundEffectInstance; }
            set { mAxeThrowerStruck1SoundEffectInstance = value; }
        }

        public static SoundEffectInstance AxeThrowerDeath2SoundEffectInstance
        {
            get { return mAxeThrowerDeath2SoundEffectInstance; }
            set { mAxeThrowerDeath2SoundEffectInstance = value; }
        }

        public static SoundEffectInstance AxeThrowerDeath1SoundEffectInstance
        {
            get { return mAxeThrowerDeath1SoundEffectInstance; }
            set { mAxeThrowerDeath1SoundEffectInstance = value; }
        }

        public static SoundEffectInstance AxeThrowerAttack2SoundEffectInstance
        {
            get { return mAxeThrowerAttack2SoundEffectInstance; }
            set { mAxeThrowerAttack2SoundEffectInstance = value; }
        }
        public static SoundEffectInstance AxeThrowerAttack1SoundEffectInstance
        {
            get { return mAxeThrowerAttack1SoundEffectInstance; }
            set { mAxeThrowerAttack1SoundEffectInstance = value; }
        }

        public static SoundEffectInstance AxeWhooshLoopSoundEffectInstance
        {
            get { return mAxeWhooshLoopSoundEffectInstance; }
            set { mAxeWhooshLoopSoundEffectInstance = value; }
        }

        public static SoundEffectInstance AxeThrowSoundEffectInstance
        {
            get { return mAxeThrowSoundEffectInstance; }
            set { mAxeThrowSoundEffectInstance = value; }
        }

        public static SoundEffectInstance ArcaneMageStruck2SoundEffectInstance
        {
            get { return mArcaneMageStruck2SoundEffectInstance; }
            set { mArcaneMageStruck2SoundEffectInstance = value; }
        }

        public static SoundEffectInstance ArcaneMageStruck1SoundEffectInstance
        {
            get { return mArcaneMageStruck1SoundEffectInstance; }
            set { mArcaneMageStruck1SoundEffectInstance = value; }
        }

        public static SoundEffectInstance ArcaneMageDeath1SoundEffectInstance
        {
            get { return mArcaneMageDeath1SoundEffectInstance; }
            set { mArcaneMageDeath1SoundEffectInstance = value; }
        }

        public static SoundEffectInstance ArcaneMageDeath2SoundEffectInstance
        {
            get { return mArcaneMageDeath2SoundEffectInstance; }
            set { mArcaneMageDeath2SoundEffectInstance = value; }
        }

        public static SoundEffectInstance ArcaneMageAttackGruntSoundEffectInstance
        {
            get { return mArcaneMageAttackGruntSoundEffectInstance; }
            set { mArcaneMageAttackGruntSoundEffectInstance = value; }
        }

        public static SoundEffectInstance ArcaneMageBlastSoundEffectInstance
        {
            get { return mArcaneMageBlastSoundEffectInstance; }
            set { mArcaneMageBlastSoundEffectInstance = value; }
        }

        public static SoundEffectInstance ArcaneMageAttackSoundEffectInstance
        {
            get { return mArcaneMageAttackSoundEffectInstance; }
            set { mArcaneMageAttackSoundEffectInstance = value; }
        }

        public static SoundEffectInstance DragonStruck2SoundEffectInstance
        {
            get { return mDragonStruck2SoundEffectInstance; }
            set { mDragonStruck2SoundEffectInstance = value; }
        }

        public static SoundEffectInstance DragonStruck1SoundEffectInstance
        {
            get { return mDragonStruck1SoundEffectInstance; }
            set { mDragonStruck1SoundEffectInstance = value; }
        }

        public static SoundEffectInstance DragonFireOnlySoundEffecttInstance
        {
            get { return mDragonFireOnlySoundEffecttInstance; }
            set { mDragonFireOnlySoundEffecttInstance = value; }
        }

        public static SoundEffectInstance DragonFireAttackSoundEffectInstance
        {
            get { return mDragonFireAttackSoundEffectInstance; }
            set { mDragonFireAttackSoundEffectInstance = value; }
        }

        public static SoundEffectInstance DragonDeath2SoundEffectInstance
        {
            get { return mDragonDeath2SoundEffectInstance; }
            set { mDragonDeath2SoundEffectInstance = value; }
        }

        public static SoundEffectInstance DragonDeath1SoundEffectInstance
        {
            get { return mDragonDeath1SoundEffectInstance; }
            set { mDragonDeath1SoundEffectInstance = value; }
        }

        #endregion

        #region Controller Functions

        public static void InitializeAudioController()
        {

        }

        public static void LoadAudioController()
        {
            LoadSoundEffects();
            LoadSoundEffectInstances();
        }

        #endregion

        #region Initialize Functions

        #endregion

        #region Load Functions


        #region Load Sound Effect Functions

        public static void LoadSoundEffects()
        {

            LoadItemDropSoundEffects();
            LoadArcaneMageSoundEffects();
            LoadAxeThrowerSoundEffects();
            LoadBansheeSoundEffects();
            LoadBerserkerSoundEffects();
            LoadBuildingSpawnSoundEffects();
            LoadButtonClicksSoundEffects();
            LoadClericSoundEffects();
            LoadDoomHoundSoundEffects();
            LoadDragonSoundEffects();
            LoadFireMageSoundEffects();
            LoadGogSoundEffects();
            LoadHeroSoundEffects();
            LoadImpSoundEffects();
            LoadNecromancerSoundEffects();
            LoadReaperSoundEffects();
            LoadUnitStruckSoundEffects();
            LoadWolfSoundEffects();

        }

        private static void LoadItemDropSoundEffects()
        {
            GoldDropCoin1SoundEffect = Content.Load<SoundEffect>(GoldDropCoin1SoundEffectPath);
            GoldDropCoin2SoundEffect = Content.Load<SoundEffect>(GoldDropCoin2SoundEffectPath);
            GoldDropCoin3SoundEffect = Content.Load<SoundEffect>(GoldDropCoin3SoundEffectPath);
            HealthDropSoundEffect = Content.Load<SoundEffect>(HealthDropSoundEffectPath);
        }

        public static void LoadArcaneMageSoundEffects()
        {
            ArcaneMageAttackSoundEffect = Content.Load<SoundEffect>(ArcaneMageAttackSoundEffectPath);
            ArcaneMageBlastSoundEffect = Content.Load<SoundEffect>(ArcaneMageBlastSoundEffectPath);
            ArcaneMageAttackGruntSoundEffect = Content.Load<SoundEffect>(ArcaneMageAttackGruntSoundEffectPath);
            ArcaneMageDeath1SoundEffect = Content.Load<SoundEffect>(ArcaneMageDeath1SoundEffectPath);
            ArcaneMageDeath2SoundEffect = Content.Load<SoundEffect>(ArcaneMageDeath2SoundEffectPath);
            ArcaneMageStruck1SoundEffect = Content.Load<SoundEffect>(ArcaneMageStruck1SoundEffectPath);
            ArcaneMageStruck2SoundEffect = Content.Load<SoundEffect>(ArcaneMageStruck2SoundEffectPath);
        }

        public static void LoadAxeThrowerSoundEffects()
        {

            AxeThrowSoundEffect = Content.Load<SoundEffect>(AxeThrowSoundEffectPath);
            AxeWhooshLoopSoundEffect = Content.Load<SoundEffect>(AxeWhooshLoopSoundEffectPath);
            AxeThrowerAttack1SoundEffect = Content.Load<SoundEffect>(AxeThrowerAttack1SoundEffectPath);
            AxeThrowerAttack2SoundEffect = Content.Load<SoundEffect>(AxeThrowerAttack2SoundEffectPath);
            AxeThrowerDeath1SoundEffect = Content.Load<SoundEffect>(AxeThrowerDeath1SoundEffectPath);
            AxeThrowerDeath2SoundEffect = Content.Load<SoundEffect>(AxeThrowerDeath2SoundEffectPath);
            AxeThroweStruck1SoundEffect = Content.Load<SoundEffect>(AxeThroweStruck1SoundEffectPath);
            AxeThroweStruck2SoundEffect = Content.Load<SoundEffect>(AxeThroweStruck2SoundEffectPath);
            AxeThroweStruck3SoundEffect = Content.Load<SoundEffect>(AxeThroweStruck3SoundEffectPath);
        }
        public static void LoadBansheeSoundEffects()
        {
            BansheeSkullBlastSoundEffect = Content.Load<SoundEffect>(BansheeSkullBlastSoundEffectPath);
            BansheeAttackSoundEffect = Content.Load<SoundEffect>(BansheeAttackSoundEffectPath);
            BansheeStruck1SoundEffect = Content.Load<SoundEffect>(BansheeStruck1SoundEffectPath);
            BansheeStruck2SoundEffect = Content.Load<SoundEffect>(BansheeStruck2SoundEffectPath);
            BansheeDeathSoundEffect = Content.Load<SoundEffect>(BansheeDeathSoundEffectPath);

        }
        public static void LoadBerserkerSoundEffects()
        {

            BerserkerAttack1SoundEffect = Content.Load<SoundEffect>(BerserkerAttack1SoundEffectPath);
            BerserkerAttack2SoundEffect = Content.Load<SoundEffect>(BerserkerAttack2SoundEffectPath);
            BerserkerAttack3SoundEffect = Content.Load<SoundEffect>(BerserkerAttack3SoundEffectPath);
            BerserkerDeath1SoundEffect = Content.Load<SoundEffect>(BerserkerDeath1SoundEffectPath);
            BerserkerDeath2SoundEffect = Content.Load<SoundEffect>(BerserkerDeath2SoundEffectPath);
            BerserkerDeath3SoundEffect = Content.Load<SoundEffect>(BerserkerDeath3SoundEffectPath);
            BerserkerStruck1SoundEffect = Content.Load<SoundEffect>(BerserkerStruck1SoundEffectPath);
            BerserkerStruck2SoundEffect = Content.Load<SoundEffect>(BerserkerStruck2SoundEffectPath);
            BerserkerStruck3SoundEffect = Content.Load<SoundEffect>(BerserkerStruck3SoundEffectPath);
            BerserkerStruck4SoundEffect = Content.Load<SoundEffect>(BerserkerStruck4SoundEffectPath);
            BerserkerStruck5SoundEffect = Content.Load<SoundEffect>(BerserkerStruck5SoundEffectPath);

        }
        public static void LoadBuildingSpawnSoundEffects()
        {

            BuildingSpawnSoundEffect = Content.Load<SoundEffect>(BuildingSpawnSoundEffectPath);

        }
        public static void LoadButtonClicksSoundEffects()
        {

            ButtonClick1SoundEffect = Content.Load<SoundEffect>(ButtonClick1SoundEffectPath);
            ButtonClick2SoundEffect = Content.Load<SoundEffect>(ButtonClick2SoundEffectPath);
            ButtonClick3SoundEffect = Content.Load<SoundEffect>(ButtonClick3SoundEffectPath);

        }
        public static void LoadClericSoundEffects()
        {

            ClericHealSpellSoundEffect = Content.Load<SoundEffect>(ClericHealSpellSoundEffectPath);
            ClericDeath1SoundEffect = Content.Load<SoundEffect>(ClericDeath1SoundEffectPath);
            ClericDeath2SoundEffect = Content.Load<SoundEffect>(ClericDeath2SoundEffectPath);
            ClericStruck1SoundEffect = Content.Load<SoundEffect>(ClericStruck1SoundEffectPath);
            ClericStruck2SoundEffect = Content.Load<SoundEffect>(ClericStruck2SoundEffectPath);

        }
        public static void LoadDoomHoundSoundEffects()
        {

            DoomHoundAttack1SoundEffect = Content.Load<SoundEffect>(DoomHoundAttack1SoundEffectPath);
            DoomHoundAttack2SoundEffect = Content.Load<SoundEffect>(DoomHoundAttack2SoundEffectPath);
            DoomHoundDeath1SoundEffect = Content.Load<SoundEffect>(DoomHoundDeath1SoundEffectPath);
            DoomHoundDeath2SoundEffect = Content.Load<SoundEffect>(DoomHoundDeath2SoundEffectPath);
            DoomHoundStruckSoundEffect = Content.Load<SoundEffect>(DoomHoundStruckSoundEffectPath);

        }
        public static void LoadDragonSoundEffects()
        {


            DragonDeath1SoundEffect = Content.Load<SoundEffect>(DragonDeath1SoundEffectPath);
            DragonDeath2SoundEffect = Content.Load<SoundEffect>(DragonDeath2SoundEffectPath);
            DragonFireAttackSoundEffect = Content.Load<SoundEffect>(DragonFireAttackSoundEffectPath);
            DragonFireOnlySoundEffect = Content.Load<SoundEffect>(DragonFireOnlySoundEffectPath);
            DragonStruck1SoundEffect = Content.Load<SoundEffect>(DragonStruck1SoundEffectPath);
            DragonStruck2SoundEffect = Content.Load<SoundEffect>(DragonStruck2SoundEffectPath);

        }
        public static void LoadFireMageSoundEffects()
        {

            FireMageFireBlastSoundEffect = Content.Load<SoundEffect>(FireMageFireBlastSoundEffectPath);
            FireMageAttackGruntSoundEffect = Content.Load<SoundEffect>(FireMageAttackGruntSoundEffectPath);
            FireMageDeath1SoundEffect = Content.Load<SoundEffect>(FireMageDeath1SoundEffectPath);
            FireMageDeath2SoundEffect = Content.Load<SoundEffect>(FireMageDeath2SoundEffectPath);
            FireMageStruck1SoundEffect = Content.Load<SoundEffect>(FireMageStruck1SoundEffectPath);
            FireMageStruck2SoundEffect = Content.Load<SoundEffect>(FireMageStruck2SoundEffectPath);

        }
        public static void LoadGogSoundEffects()
        {

            GogAttack1SoundEffect = Content.Load<SoundEffect>(GogAttack1SoundEffectPath);
            GogAttack2SoundEffect = Content.Load<SoundEffect>(GogAttack2SoundEffectPath);
            GogDeath1SoundEffect = Content.Load<SoundEffect>(GogDeath1SoundEffectPath);
            GogDeath2SoundEffect = Content.Load<SoundEffect>(GogDeath2SoundEffectPath);
            GogFireBlastSoundEffect = Content.Load<SoundEffect>(GogFireBlastSoundEffectPath);
            GogStruck1SoundEffect = Content.Load<SoundEffect>(GogStruck1SoundEffectPath);
            GogStruck2SoundEffect = Content.Load<SoundEffect>(GogStruck2SoundEffectPath);


        }
        public static void LoadHeroSoundEffects()
        {
            HeroAttack1SoundEffect = Content.Load<SoundEffect>(HeroAttack1SoundEffectPath);
            HeroAttack2SoundEffect = Content.Load<SoundEffect>(HeroAttack2SoundEffectPath);
            HeroBlessSpellSoundEffect = Content.Load<SoundEffect>(HeroBlessSpellSoundEffectPath);
            HeroCurseSpellSoundEffect = Content.Load<SoundEffect>(HeroCurseSpellSoundEffectPath);
            HeroDeathSoundEffect = Content.Load<SoundEffect>(HeroDeathSoundEffectPath);
            HeroStruck1SoundEffect = Content.Load<SoundEffect>(HeroStruck1SoundEffectPath);
            HeroStruck2SoundEffect = Content.Load<SoundEffect>(HeroStruck2SoundEffectPath);
            HeroWarCrySoundEffect = Content.Load<SoundEffect>(HeroWarCrySoundEffectPath);
            HeroLevelGainSoundEffect = Content.Load<SoundEffect>(HeroLevelGainSoundEffectPath);

        }
        public static void LoadImpSoundEffects()
        {
            ImpAttack1SoundEffect = Content.Load<SoundEffect>(ImpAttack1SoundEffectPath);
            ImpAttack2SoundEffect = Content.Load<SoundEffect>(ImpAttack2SoundEffectPath);
            ImpDeathSoundEffect = Content.Load<SoundEffect>(ImpDeathSoundEffectPath);
            ImpSpawnSoundEffect = Content.Load<SoundEffect>(ImpSpawnSoundEffectPath);
            ImpStruck1SoundEffect = Content.Load<SoundEffect>(ImpStruck1SoundEffectPath);
            ImpStruck2SoundEffect = Content.Load<SoundEffect>(ImpStruck2SoundEffectPath);
        }
        public static void LoadNecromancerSoundEffects()
        {

            NecromancerLifeDrain1SoundEffect = Content.Load<SoundEffect>(NecromancerLifeDrain1SoundEffectPath);
            NecromancerLifeDrain2SoundEffect = Content.Load<SoundEffect>(NecromancerLifeDrain2SoundEffectPath);
            NecromancerDeathSoundEffect = Content.Load<SoundEffect>(NecromancerDeathSoundEffectPath);
            NecromancerStruck1SoundEffect = Content.Load<SoundEffect>(NecromancerStruck1SoundEffectPath);
            NecromancerStruck2SoundEffect = Content.Load<SoundEffect>(NecromancerStruck2SoundEffectPath);

        }
        public static void LoadReaperSoundEffects()
        {
            ReaperStruck1SoundEffect = Content.Load<SoundEffect>(ReaperStruck1SoundEffectPath);
            ReaperStruck2SoundEffect = Content.Load<SoundEffect>(ReaperStruck2SoundEffectPath);
            ReaperDeathSoundEffect = Content.Load<SoundEffect>(ReaperDeathSoundEffectPath);
            ReaperAttackSoundEffect = Content.Load<SoundEffect>(ReaperAttackSoundEffectPath);

        }
        public static void LoadUnitStruckSoundEffects()
        {

            MeleeBoneSnapSoundEffect = Content.Load<SoundEffect>(MeleeBoneSnapSoundEffectPath);
            MeleePunch1SoundEffect = Content.Load<SoundEffect>(MeleePunch1SoundEffectPath);
            MeleePunch2SoundEffect = Content.Load<SoundEffect>(MeleePunch2SoundEffectPath);
            MeleePunch3SoundEffect = Content.Load<SoundEffect>(MeleePunch3SoundEffectPath);
            MeleePunch4SoundEffect = Content.Load<SoundEffect>(MeleePunch4SoundEffectPath);
            MeleePunchSoundEffect = Content.Load<SoundEffect>(MeleePunchSoundEffectPath);
            MeleeWetStab1SoundEffect = Content.Load<SoundEffect>(MeleeWetStab1SoundEffectPath);
            MeleeWetStab2SoundEffect = Content.Load<SoundEffect>(MeleeWetStab2SoundEffectPath);
            MeleeWetStab3SoundEffect = Content.Load<SoundEffect>(MeleeWetStab3SoundEffectPath);

        }
        public static void LoadWolfSoundEffects()
        {

            WolfAttack1SoundEffect = Content.Load<SoundEffect>(WolfAttack1SoundEffectPath);
            WolfAttack2SoundEffect = Content.Load<SoundEffect>(WolfAttack2SoundEffectPath);
            WolfDeath1SoundEffect = Content.Load<SoundEffect>(WolfDeath1SoundEffectPath);
            WolfDeath2SoundEffect = Content.Load<SoundEffect>(WolfDeath2SoundEffectPath);
            WolfStruckSoundEffect = Content.Load<SoundEffect>(WolfStruckSoundEffectPath);

        }








        #endregion


        #region Load Sound Effect Instances



        public static void LoadSoundEffectInstances()
        {

            LoadItemDropSoundEffectInstances();
            LoadArcaneMageSoundEffectInstances();
            LoadAxeThrowerSoundEffectInstances();
            LoadBansheeSoundEffectInstances();
            LoadBerserkerSoundEffectInstances();
            LoadBuildingSpawnSoundEffectInstances();
            LoadButtonClicksSoundEffectInstances();
            LoadClericSoundEffectInstances();
            LoadDoomHoundSoundEffectInstances();
            LoadDragonSoundEffectInstances();
            LoadFireMageSoundEffectInstances();
            LoadGogSoundEffectInstances();
            LoadHeroSoundEffectInstances();
            LoadImpSoundEffectInstances();
            LoadNecromancerSoundEffectInstances();
            LoadReaperSoundEffectInstances();
            LoadUnitStruckSoundEffectInstances();
            LoadWolfSoundEffectInstances();

        }

        private static void LoadItemDropSoundEffectInstances()
        {
            mGoldDropCoin1SoundEffectInstance = GoldDropCoin1SoundEffect.CreateInstance();
            mGoldDropCoin2SoundEffectInstance = GoldDropCoin2SoundEffect.CreateInstance();
            mGoldDropCoin3SoundEffectInstance = GoldDropCoin3SoundEffect.CreateInstance();
            mHealthDropSoundEffectInstance = HealthDropSoundEffect.CreateInstance();
        }


        public static void LoadArcaneMageSoundEffectInstances()
        {
            mArcaneMageAttackSoundEffectInstance = ArcaneMageAttackSoundEffect.CreateInstance();
            mArcaneMageBlastSoundEffectInstance = ArcaneMageBlastSoundEffect.CreateInstance();
            mArcaneMageAttackGruntSoundEffectInstance = ArcaneMageAttackGruntSoundEffect.CreateInstance();
            mArcaneMageDeath1SoundEffectInstance = ArcaneMageDeath1SoundEffect.CreateInstance();
            mArcaneMageDeath2SoundEffectInstance = ArcaneMageDeath2SoundEffect.CreateInstance();
            mArcaneMageStruck1SoundEffectInstance = ArcaneMageStruck1SoundEffect.CreateInstance();
            mArcaneMageStruck2SoundEffectInstance = ArcaneMageStruck2SoundEffect.CreateInstance();

        }

        public static void LoadAxeThrowerSoundEffectInstances()
        {
            mAxeThrowSoundEffectInstance = AxeThrowSoundEffect.CreateInstance();
            mAxeWhooshLoopSoundEffectInstance = AxeWhooshLoopSoundEffect.CreateInstance();
            mAxeThrowerAttack1SoundEffectInstance = AxeThrowerAttack1SoundEffect.CreateInstance();
            mAxeThrowerAttack2SoundEffectInstance = AxeThrowerAttack2SoundEffect.CreateInstance();
            mAxeThrowerDeath1SoundEffectInstance = AxeThrowerDeath1SoundEffect.CreateInstance();
            mAxeThrowerDeath2SoundEffectInstance = AxeThrowerDeath2SoundEffect.CreateInstance();
            mAxeThrowerStruck1SoundEffectInstance = AxeThroweStruck1SoundEffect.CreateInstance();
            mAxeThrowerStruck2SoundEffectInstance = AxeThroweStruck2SoundEffect.CreateInstance();
            mAxeThrowerStruck3SoundEffectInstance = AxeThroweStruck3SoundEffect.CreateInstance();


        }

        public static void LoadBansheeSoundEffectInstances()
        {
            mBansheeSkullBlastSoundEffectInstance = BansheeSkullBlastSoundEffect.CreateInstance();
            mBansheeAttackSoundEffectInstance = BansheeAttackSoundEffect.CreateInstance();
            mBansheeStruck1SoundEffectInstance = BansheeStruck1SoundEffect.CreateInstance();
            mBansheeStruck2SoundEffectInstance = BansheeStruck2SoundEffect.CreateInstance();
            mBansheeDeathSoundEffectInstance = BansheeDeathSoundEffect.CreateInstance();
        }

        public static void LoadBerserkerSoundEffectInstances()
        {

            mBerserkerAttack1SoundEffectInstance = BerserkerAttack1SoundEffect.CreateInstance();
            mBerserkerAttack2SoundEffectInstance = BerserkerAttack2SoundEffect.CreateInstance();
            mBerserkerAttack3SoundEffectInstance = BerserkerAttack3SoundEffect.CreateInstance();
            mBerserkerDeath1SoundEffectInstance = BerserkerDeath1SoundEffect.CreateInstance();
            mBerserkerDeath2SoundEffectInstance = BerserkerDeath2SoundEffect.CreateInstance();
            mBerserkerDeath3SoundEffectInstance = BerserkerDeath3SoundEffect.CreateInstance();
            mBerserkerStruck1SoundEffectInstance = BerserkerStruck1SoundEffect.CreateInstance();
            mBerserkerStruck2SoundEffectInstance = BerserkerStruck2SoundEffect.CreateInstance();
            mBerserkerStruck3SoundEffectInstance = BerserkerStruck3SoundEffect.CreateInstance();
            mBerserkerStruck4SoundEffectInstance = BerserkerStruck4SoundEffect.CreateInstance();
            mBerserkerStruck5SoundEffectInstance = BerserkerStruck5SoundEffect.CreateInstance();


        }
        public static void LoadBuildingSpawnSoundEffectInstances()
        {
            mBuildingSpawnSoundEffectInstance = BuildingSpawnSoundEffect.CreateInstance();

        }
        public static void LoadButtonClicksSoundEffectInstances()
        {

            mButtonClick1SoundEffectInstance = ButtonClick1SoundEffect.CreateInstance();
            mButtonClick2SoundEffectInstance = ButtonClick2SoundEffect.CreateInstance();
            mButtonClick3SoundEffectInstance = ButtonClick3SoundEffect.CreateInstance();
        }
        public static void LoadClericSoundEffectInstances()
        {
            mClericHealSpellSoundEffectInstance = ClericHealSpellSoundEffect.CreateInstance();
            mClericDeath1SoundEffectInstance = ClericDeath1SoundEffect.CreateInstance();
            mClericDeath1SoundEffectInstance = ClericDeath1SoundEffect.CreateInstance();
            mClericStruck1SoundEffectInstance = ClericStruck1SoundEffect.CreateInstance();
            mClericStruck2SoundEffectInstance = ClericStruck2SoundEffect.CreateInstance();

        }
        public static void LoadDoomHoundSoundEffectInstances()
        {

            mDoomHoundAttack1SoundEffectInstance = DoomHoundAttack1SoundEffect.CreateInstance();
            mDoomHoundAttack2SoundEffectInstance = DoomHoundAttack2SoundEffect.CreateInstance();
            mDoomHoundDeath1SoundEffectInstance = DoomHoundDeath1SoundEffect.CreateInstance();
            mDoomHoundDeath2SoundEffectInstance = DoomHoundDeath2SoundEffect.CreateInstance();
            mDoomHoundStruckSoundEffectInstance = DoomHoundStruckSoundEffect.CreateInstance();

        }
        public static void LoadDragonSoundEffectInstances()
        {

            mDragonDeath1SoundEffectInstance = DragonDeath1SoundEffect.CreateInstance();
            mDragonDeath2SoundEffectInstance = DragonDeath2SoundEffect.CreateInstance();
            mDragonFireAttackSoundEffectInstance = DragonFireAttackSoundEffect.CreateInstance();
            mDragonFireOnlySoundEffecttInstance = DragonFireOnlySoundEffect.CreateInstance();
            mDragonStruck1SoundEffectInstance = DragonStruck1SoundEffect.CreateInstance();
            mDragonStruck2SoundEffectInstance = DragonStruck2SoundEffect.CreateInstance();


        }
        public static void LoadFireMageSoundEffectInstances()
        {

            mFireMageFireBlastSoundEffectInstance = FireMageFireBlastSoundEffect.CreateInstance();
            mFireMageAttackGruntSoundEffectInstance = FireMageAttackGruntSoundEffect.CreateInstance();
            mFireMageDeath1SoundEffectInstance = FireMageDeath1SoundEffect.CreateInstance();
            mFireMageDeath2SoundEffectInstance = FireMageDeath2SoundEffect.CreateInstance();
            mFireMageStruck1SoundEffectInstance = FireMageStruck1SoundEffect.CreateInstance();
            mFireMageStruck2SoundEffectInstance = FireMageStruck2SoundEffect.CreateInstance();
        }
        public static void LoadGogSoundEffectInstances()
        {

            mGogAttack1SoundEffectInstance = GogAttack1SoundEffect.CreateInstance();
            mGogAttack2SoundEffectInstance = GogAttack2SoundEffect.CreateInstance();
            mGogDeath1SoundEffectInstance = GogDeath1SoundEffect.CreateInstance();
            mGogDeath2SoundEffectInstance = GogDeath2SoundEffect.CreateInstance();
            mGogFireBlastSoundEffectInstance = GogFireBlastSoundEffect.CreateInstance();
            mGogStruck1SoundEffectInstance = GogStruck1SoundEffect.CreateInstance();
            mGogStruck2SoundEffectInstance = GogStruck2SoundEffect.CreateInstance();

        }
        public static void LoadHeroSoundEffectInstances()
        {
            mHeroAttack1SoundEffectInstance = HeroAttack1SoundEffect.CreateInstance();
            mHeroAttack2SoundEffectInstance = HeroAttack2SoundEffect.CreateInstance();
            mHeroBlessSpellSoundEffectInstance = HeroBlessSpellSoundEffect.CreateInstance();
            mHeroCurseSpellSoundEffectInstance = HeroCurseSpellSoundEffect.CreateInstance();
            mHeroDeathSoundEffectInstance = HeroDeathSoundEffect.CreateInstance();
            mHeroStruck1SoundEffectInstance = HeroStruck1SoundEffect.CreateInstance();
            mHeroStruck2SoundEffectInstance = HeroStruck2SoundEffect.CreateInstance();
            mHeroWarCrySoundEffectInstance = HeroWarCrySoundEffect.CreateInstance();
            mHeroLevelGainSoundEffectInstance = HeroLevelGainSoundEffect.CreateInstance();

        }
        public static void LoadImpSoundEffectInstances()
        {
            mImpAttack1SoundEffectInstance = ImpAttack1SoundEffect.CreateInstance();
            mImpAttack2SoundEffectInstance = ImpAttack2SoundEffect.CreateInstance();
            mImpDeathSoundEffectInstance = ImpDeathSoundEffect.CreateInstance();
            mImpSpawnSoundEffectInstance = ImpSpawnSoundEffect.CreateInstance();
            mImpStruck1SoundEffectInstance = ImpStruck1SoundEffect.CreateInstance();
            mImpStruck2SoundEffectInstance = ImpStruck2SoundEffect.CreateInstance();

        }
        public static void LoadNecromancerSoundEffectInstances()
        {

            mNecromancerLifeDrain1SoundEffectInstance = NecromancerLifeDrain1SoundEffect.CreateInstance();
            mNecromancerLifeDrain2SoundEffectInstance = NecromancerLifeDrain2SoundEffect.CreateInstance();
            mNecromancerDeathSoundEffectInstance = NecromancerDeathSoundEffect.CreateInstance();
            mNecromancerStruck1SoundEffectInstance = NecromancerStruck1SoundEffect.CreateInstance();
            mNecromancerStruck2SoundEffectInstance = NecromancerStruck2SoundEffect.CreateInstance();


        }
        public static void LoadReaperSoundEffectInstances()
        {
            mReaperStruck1SoundEffectInstance = ReaperStruck1SoundEffect.CreateInstance();
            mReaperStruck2SoundEffectInstance = ReaperStruck2SoundEffect.CreateInstance();
            mReaperDeathSoundEffectInstance = ReaperDeathSoundEffect.CreateInstance();
            mReaperAttackSoundEffectInstance = ReaperAttackSoundEffect.CreateInstance();

        }
        public static void LoadUnitStruckSoundEffectInstances()
        {

            mMeleeBoneSnapSoundEffectInstance = MeleeBoneSnapSoundEffect.CreateInstance();
            mMeleePunchSoundEffectInstance = MeleePunchSoundEffect.CreateInstance();
            mMeleePunch1SoundEffectInstance = MeleePunch1SoundEffect.CreateInstance();
            mMeleePunch2SoundEffectInstance = MeleePunch2SoundEffect.CreateInstance();
            mMeleePunch3SoundEffectInstance = MeleePunch3SoundEffect.CreateInstance();
            mMeleePunch4SoundEffectInstance = MeleePunch4SoundEffect.CreateInstance();
            mMeleeWetStab1SoundEffectInstance = MeleeWetStab1SoundEffect.CreateInstance();
            mMeleeWetStab2SoundEffectInstance = MeleeWetStab2SoundEffect.CreateInstance();
            mMeleeWetStab3SoundEffectInstance = MeleeWetStab3SoundEffect.CreateInstance();

        }
        public static void LoadWolfSoundEffectInstances()
        {
            mWolfAttack1SoundEffectInstance = WolfAttack1SoundEffect.CreateInstance();
            mWolfAttack2SoundEffectInstance = WolfAttack2SoundEffect.CreateInstance();
            mWolfDeath1SoundEffectInstance = WolfDeath1SoundEffect.CreateInstance();
            mWolfDeath2SoundEffectInstance = WolfDeath2SoundEffect.CreateInstance();
            mWolfStruckSoundEffectInstance = WolfStruckSoundEffect.CreateInstance();

        }
        #endregion

        #endregion

        #region Dispose Sound Effect Instances

        public static void DisposeSoundEffectInstances()
        {

            DisposeArcaneMageSoundEffectInstances();
            DisposeAxeThrowerSoundEffectInstances();
            DisposeBansheeSoundEffectInstances();
            DisposeBerserkerSoundEffectInstances();
            DisposeBuildingSpawnSoundEffectInstances();
            DisposeButtonClicksSoundEffectInstances();
            DisposeClericSoundEffectInstances();
            DisposeDoomHoundSoundEffectInstances();
            DisposeDragonSoundEffectInstances();
            DisposeFireMageSoundEffectInstances();
            DisposeGogSoundEffectInstances();
            DisposeHeroSoundEffectInstances();
            DisposeImpSoundEffectInstances();
            DisposeNecromancerSoundEffectInstances();
            DisposeReaperSoundEffectInstances();
            DisposeSoundEffectInstances();
            DisposeUnitStruckSoundEffectInstances();
            DisposeWolfSoundEffectInstances();

        }


        public static void DisposeArcaneMageSoundEffectInstances()
        {
            mArcaneMageAttackSoundEffectInstance.Dispose();
            mArcaneMageBlastSoundEffectInstance.Dispose();
            mArcaneMageAttackGruntSoundEffectInstance.Dispose();
            mArcaneMageDeath1SoundEffectInstance.Dispose();
            mArcaneMageDeath2SoundEffectInstance.Dispose();
            mArcaneMageStruck1SoundEffectInstance.Dispose();
            mArcaneMageStruck2SoundEffectInstance.Dispose();

        }

        public static void DisposeAxeThrowerSoundEffectInstances()
        {
            mAxeThrowSoundEffectInstance.Dispose();
            mAxeWhooshLoopSoundEffectInstance.Dispose();
            mAxeThrowerAttack1SoundEffectInstance.Dispose();
            mAxeThrowerAttack2SoundEffectInstance.Dispose();
            mAxeThrowerDeath1SoundEffectInstance.Dispose();
            mAxeThrowerDeath2SoundEffectInstance.Dispose();
            mAxeThrowerStruck1SoundEffectInstance.Dispose();
            mAxeThrowerStruck2SoundEffectInstance.Dispose();
            mAxeThrowerStruck3SoundEffectInstance.Dispose();


        }
        public static void DisposeBansheeSoundEffectInstances()
        {
            mBansheeSkullBlastSoundEffectInstance.Dispose();

        }
        public static void DisposeBerserkerSoundEffectInstances()
        {

            mBerserkerAttack1SoundEffectInstance.Dispose();
            mBerserkerAttack2SoundEffectInstance.Dispose();
            mBerserkerAttack3SoundEffectInstance.Dispose();
            mBerserkerDeath1SoundEffectInstance.Dispose();
            mBerserkerDeath2SoundEffectInstance.Dispose();
            mBerserkerDeath3SoundEffectInstance.Dispose();
            mBerserkerStruck1SoundEffectInstance.Dispose();
            mBerserkerStruck2SoundEffectInstance.Dispose();
            mBerserkerStruck3SoundEffectInstance.Dispose();
            mBerserkerStruck4SoundEffectInstance.Dispose();
            mBerserkerStruck5SoundEffectInstance.Dispose();


        }
        public static void DisposeBuildingSpawnSoundEffectInstances()
        {
            mBuildingSpawnSoundEffectInstance.Dispose();

        }
        public static void DisposeButtonClicksSoundEffectInstances()
        {

            mButtonClick1SoundEffectInstance.Dispose();
            mButtonClick2SoundEffectInstance.Dispose();
            mButtonClick3SoundEffectInstance.Dispose();
        }
        public static void DisposeClericSoundEffectInstances()
        {
            mClericHealSpellSoundEffectInstance.Dispose();
            mClericDeath1SoundEffectInstance.Dispose();
            mClericDeath1SoundEffectInstance.Dispose();
            mClericStruck1SoundEffectInstance.Dispose();
            mClericStruck2SoundEffectInstance.Dispose();

        }
        public static void DisposeDoomHoundSoundEffectInstances()
        {

            mDoomHoundAttack1SoundEffectInstance.Dispose();
            mDoomHoundDeath1SoundEffectInstance.Dispose();
            mDoomHoundDeath2SoundEffectInstance.Dispose();
            mDoomHoundStruckSoundEffectInstance.Dispose();

        }
        public static void DisposeDragonSoundEffectInstances()
        {

            mDragonDeath1SoundEffectInstance.Dispose();
            mDragonDeath2SoundEffectInstance.Dispose();
            mDragonFireAttackSoundEffectInstance.Dispose();
            mDragonFireOnlySoundEffecttInstance.Dispose();
            mDragonStruck1SoundEffectInstance.Dispose();
            mDragonStruck2SoundEffectInstance.Dispose();


        }
        public static void DisposeFireMageSoundEffectInstances()
        {

            mFireMageFireBlastSoundEffectInstance.Dispose();
            mFireMageAttackGruntSoundEffectInstance.Dispose();
            mFireMageDeath1SoundEffectInstance.Dispose();
            mFireMageDeath2SoundEffectInstance.Dispose();
            mFireMageStruck1SoundEffectInstance.Dispose();
            mFireMageStruck2SoundEffectInstance.Dispose();
        }
        public static void DisposeGogSoundEffectInstances()
        {

            mGogAttack1SoundEffectInstance.Dispose();
            mGogAttack2SoundEffectInstance.Dispose();
            mGogDeath1SoundEffectInstance.Dispose();
            mGogDeath2SoundEffectInstance.Dispose();
            mGogFireBlastSoundEffectInstance.Dispose();
            mGogStruck1SoundEffectInstance.Dispose();
            mGogStruck2SoundEffectInstance.Dispose();

        }
        public static void DisposeHeroSoundEffectInstances()
        {
            mHeroAttack1SoundEffectInstance.Dispose();
            mHeroAttack2SoundEffectInstance.Dispose();
            mHeroBlessSpellSoundEffectInstance.Dispose();
            mHeroCurseSpellSoundEffectInstance.Dispose();
            mHeroDeathSoundEffectInstance.Dispose();
            mHeroStruck1SoundEffectInstance.Dispose();
            mHeroStruck2SoundEffectInstance.Dispose();
            mHeroWarCrySoundEffectInstance.Dispose();
            mHeroLevelGainSoundEffectInstance.Dispose();

        }
        public static void DisposeImpSoundEffectInstances()
        {
            mImpAttack1SoundEffectInstance.Dispose();
            mImpAttack2SoundEffectInstance.Dispose();
            mImpDeathSoundEffectInstance.Dispose();
            mImpSpawnSoundEffectInstance.Dispose();
            mImpStruck1SoundEffectInstance.Dispose();
            mImpStruck2SoundEffectInstance.Dispose();

        }
        public static void DisposeNecromancerSoundEffectInstances()
        {

            mNecromancerLifeDrain1SoundEffectInstance.Dispose();
            mNecromancerLifeDrain2SoundEffectInstance.Dispose();
            mNecromancerDeathSoundEffectInstance.Dispose();
            mNecromancerStruck1SoundEffectInstance.Dispose();
            mNecromancerStruck2SoundEffectInstance.Dispose();


        }
        public static void DisposeReaperSoundEffectInstances()
        {
            mReaperStruck1SoundEffectInstance.Dispose();
            mReaperStruck2SoundEffectInstance.Dispose();
            mReaperDeathSoundEffectInstance.Dispose();

        }
        public static void DisposeUnitStruckSoundEffectInstances()
        {

            mMeleeBoneSnapSoundEffectInstance.Dispose();
            mMeleePunchSoundEffectInstance.Dispose();
            mMeleePunch1SoundEffectInstance.Dispose();
            mMeleePunch2SoundEffectInstance.Dispose();
            mMeleePunch3SoundEffectInstance.Dispose();
            mMeleePunch4SoundEffectInstance.Dispose();
            mMeleeWetStab1SoundEffectInstance.Dispose();
            mMeleeWetStab2SoundEffectInstance.Dispose();
            mMeleeWetStab3SoundEffectInstance.Dispose();

        }
        public static void DisposeWolfSoundEffectInstances()
        {
            mWolfAttack1SoundEffectInstance.Dispose();
            mWolfAttack2SoundEffectInstance.Dispose();
            mWolfDeath1SoundEffectInstance.Dispose();
            mWolfDeath2SoundEffectInstance.Dispose();
            mWolfStruckSoundEffectInstance.Dispose();

        }

        #endregion


    }
}
