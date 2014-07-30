using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace ExternalTypes
{

    public enum CREIDTSTATE
    {
        ROLLING, NOTROLLING
    }
    public enum Race
    {
        ORC
    }

    public enum DebugState
    {
        ACTIVE, HIDDEN
    }

    public enum DebugGrid
    {
        GRID_ENABLED, GRID_DISABLED
    }
    public enum DebugTargetQueue
    {
        TARGET_QUEUE_ENABLED, TARGET_QUEUE_DISABLED
    }

    public enum DebugSpriteID
    {
        SPRITEID_ENABLED, SPRITEID_DISABLED
    }

    public enum GameState
    {
        MAIN_MENU, LOAD, PAUSE, ACTIVE, GAME_OVER, ROUND_OVER
    }

    public enum SpriteState
    {
        IDLE_LEFT, IDLE_RIGHT,
        MOVE_RIGHT, MOVE_LEFT,
        ATTACK_LEFT, ATTACK_RIGHT,
        DEATH_LEFT, DEATH_RIGHT,
        CASTLE, STRUCTURE, ITEMDROP, 
        BATTLECRY_LEFT, BATTLECRY_RIGHT,
        INTIMIDATE_LEFT, INTIMIDATE_RIGHT,
        ABSORB_LEFT, ABSORB_RIGHT,
        CHOMP_LEFT, CHOMP_RIGHT,
        RETURN_LEFT, RETURN_RIGHT


    }

    public enum GodMode
    {
        DEACTIVATED, ACTIVATED
    }

    // DO NOT CHANGE THE ORDER OF THESE!!!!!!
    public enum ObjectColor
    {
        BLUE, RED, YELLOW, GREEN, PURPLE, NULL
    }

    // DO NOT CHANGE THE ORDER OF THESE!!!!!!
    public enum ObjectType
    {
        DRAGON_CAVE, BONEPIT, LIBRARY, FIRE_TEMPLE,
        ARMORY, BARRACKS, WOLFPEN, ABBEY, DRAGON, NECROMANCER, ARCANE_MAGE, FIRE_MAGE,
        WOLF, AXE_THROWER, BERSERKER, BANSHEE, REAPER, DOOM_HOUND, IMP, GOG, CLERIC, HERO, ORC_CASTLE, NULL,
        AXE_THROWER_PROJECTILE, NECROMANCER_PROJECTILE, ARCANE_MAGE_PROJECTILE, FIRE_MAGE_PROJECTILE,
        GOG_PROJECTILE, BANSHEE_PROJECTILE, GOLD, HEALTH
    }

    public enum Hostility
    {
        ENEMY,
        FRIENDLY,
        CASTLE,
        STRUCTURE,
        PROJECTILE,
        ITEMDROP
    }

    public enum ButtonState
    {
        RELEASED, PRESSED
    }

    public enum StatusWindowState
    {
        ACTIVE, INACTIVE
    }

    public enum AttackState
    {
        READY, NOTREADY
    }

    public enum ItemDropType
    {
        GOLD, HEALTH
    }
    public enum HeroActionButtonGroupState
    {
        HERO_ACTION, SPELLS, RALLY, UPGRADE_ONE, UPGRADE_TWO, UPGRADE_THREE, UPGRADE_FOUR, UPGRADE_FIVE, PURCHASE_PAGE_ONE, PURCHASE_PAGE_TWO, BUILD_PAGE_ONE, BUILD_PAGE_TWO,
        
    }

    public enum RallyFetchTarget
    {
        ACTIVE, INACTIVE
    }

    public enum CursorState
    {
        BUILD, ATTACK, GAUNTLET
    }

    public enum CombatType
    {
        RANGED, MELEE, HEALER, LIFESTEAL

    }

    public enum StructureOccupationState
    {
        OCCUPIED, UNOCCUPIED

    }

    public enum PurhcaseStructureDrawState
    {
        ACTIVE, INACTIVE
    }

    public enum BattleCrySpellState
    {
        ACTIVE, INACTIVE
    }

    public enum IntimidateSpellState
    {
        ACTIVE, INACTIVE
    }

    public enum FriendlyAggroMode
    {
        ATTACKING, IDLING, MOVING
    }
    public enum EnemyAggroMode
    {
        ATTACKING, IDLING, MOVING
    }
    public enum CoolDownState
    {
        ONCOOLDOWN, OFFCOOLDOWN
    }

    public enum MainMenuState
    {
        LOADGAME, NEWGAME, TUTORIAL, CREDITS, QUIT, NONE
    }

    public enum FromSaveGame
    {
        SAVEGAME, NEWGAME
    }

    public enum WaveCompleteState
    {
        ACTIVE, INACTIVE
    }
}
