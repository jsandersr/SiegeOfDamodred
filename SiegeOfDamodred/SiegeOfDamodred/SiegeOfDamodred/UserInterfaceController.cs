using System;
using System.Collections.Generic;
using System.Linq;
using ExternalTypes;
using DebugLib;
using GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using SiegeOfDamodred;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;

namespace SiegeOfDamodred
{

	public static class UserInterfaceController
	{
		#region Enum States

		private static GameObject tempObject;
		private static MouseState mouseState;
		private static MouseState prevoiusMouseState;
		public static KeyboardState mKeyboardState;
		private static ObjectType SelectedStructureType = ObjectType.NULL;
		private static HeroActionButtonGroupState HeroActionGroupState = HeroActionButtonGroupState.HERO_ACTION;
		private static RallyFetchTarget RallyTargetState = RallyFetchTarget.INACTIVE;
		private static ObjectColor objectColor = ObjectColor.NULL;
		public static StructureOccupationState BlueOccupationState = StructureOccupationState.UNOCCUPIED;
		public static StructureOccupationState RedOccupationState = StructureOccupationState.UNOCCUPIED;
		public static StructureOccupationState YellowOccupationState = StructureOccupationState.UNOCCUPIED;
		public static StructureOccupationState GreenOccupationState = StructureOccupationState.UNOCCUPIED;
		public static StructureOccupationState PurpleOccupationState = StructureOccupationState.UNOCCUPIED;
		public static PurhcaseStructureDrawState purchaseStructureDrawState = PurhcaseStructureDrawState.INACTIVE;
		private static MainMenuState mainMenuState = MainMenuState.NONE;

		#endregion

		#region SpriteFonts
		public static SpriteFont mSpriteFont;

		#endregion

		#region MenuTexturePaths

		private const string MainMenuTexturePath = "Sprites/Menu/MainMenuIdea1";
		#endregion

		#region WindowTexturePaths

		private const string BottomMenuWindowPath = "Sprites/UserInterface/ButtonGroups/background";
		private const string StatusWindowPath = "Placeholders/Windows/StatusWindow";
		private const string TopMenuBarPath = "Sprites/UserInterface/ButtonGroups/TopMenu";
		private const string MainMenuBannerPath = "Sprites/UserInterface/ButtonGroups/bg_frame";
		private const string TitleSignMenuPath = "Sprites/UserInterface/Buttons/MainMenuButtons/Title-Sign";

		#endregion

		#region Globe Paths
		private static string mManaGlobePath = "Sprites/UserInterface/GlobeTextures/ManaGlobeGlass";
		private static string mHealthGlobePath = "Sprites/UserInterface/GlobeTextures/HealthGlobeGlass";
		private static string mHealthGlobeLiquidPath = "Sprites/UserInterface/GlobeTextures/HealthGlobeLiquid";
		private static string mManaGlobeLiquidPath = "Sprites/UserInterface/GlobeTextures/ManaGlobeLiquid";
		private static string mGlobeBoxPath = "Sprites/UserInterface/GlobeTextures/GlobeBox";

		#endregion

		#region Globe Fields

		public static GlobeFrame mHealthGlobeLiquid;
		public static GlobeFrame mManaGlobeLiquid;
		private static Vector2 mHealthOrbLocation = new Vector2(0, 770);
		private static Vector2 mManaOrbLocation = new Vector2(1470, 770);
		public static Hero mHero;

		#endregion

		#region ButtonGroup Texture Paths

		private const string SaveButtonGroupPath = "Sprites/UserInterface/ButtonGroups/TransparentBG";
		private const string QuitButtonGroupPath = "Sprites/UserInterface/ButtonGroups/TransparentBG";
		private const string SettingsButtonGroupPath = "Sprites/UserInterface/TopMenuTrans";
		private const string UtilityButtonGroupPath = "Placeholders/ButtonGroup/UtilityButtonGroup";
		private const string MainMenuButtonGroupPath = "Sprites/UserInterface/ButtonGroups/ButtonGroupBenny";
		private const string HeroActionButtonGroupPath = "Placeholders/ButtonGroup/HeroActionButtonGroup";
		private const string StructureButtonGroupPath = "Sprites/UserInterface/ButtonGroups/StructureButtonGroup";
		private const string RallyButtonGroupPath = "Placeholders/ButtonGroup/HeroActionButtonGroup";
		private const string PurchaseStructureButtonGroupPath = "Placeholders/ButtonGroup/HeroActionButtonGroup";
		private const string PurchaseStructurePageTwoButtonGroupPath = "Placeholders/ButtonGroup/HeroActionButtonGroup";
		private const string PurchaseUpgradeButtonGroupPath = "Placeholders/ButtonGroup/HeroActionButtonGroup";
		private const string PurchaseUpgradePageTwoButtonGroupPath = "Placeholders/ButtonGroup/HeroActionButtonGroup";
		private const string TutorialNavigationButtonGroupPath = "Sprites/UserInterface/ButtonGroups/TutorialNavigationButtonGroup";

		#endregion

		#region Button Texture Paths

		#region Structure Rune Paths

		// Structure Button Path.
		private const string BlueStructureButtonPath = "Sprites/UserInterface/Buttons/RuneButtons/RunesBlue";
		private const string RedStructureButtonPath = "Sprites/UserInterface/Buttons/RuneButtons/RunesRed";
		private const string GreenStructureButtonPath = "Sprites/UserInterface/Buttons/RuneButtons/RunesGreen";
		private const string YellowStructureButtonPath = "Sprites/UserInterface/Buttons/RuneButtons/RunesYellow";
		private const string PurpleStructureButtonPath = "Sprites/UserInterface/Buttons/RuneButtons/RunesPurple";

		#endregion

		#region Settings Button Path


		private const string UnclickedSettingsPath = "Sprites/UserInterface/settings";
		#endregion

		#region Blank Button Paths

		private const string WaveCompleteWindowPath = "Sprites/UserInterface/Buttons/WaveButtons/waveComplete";
		private const string UnclickedSaveButtonPath = "Sprites/UserInterface/Buttons/WaveButtons/SaveUp";
		private const string ClickedSaveButtonPath = "Sprites/UserInterface/Buttons/WaveButtons/saveDown";

		private const string UnclickedQuitButtonPath1 = "Sprites/UserInterface/Buttons/WaveButtons/quitUp";
		private const string ClickedQuitButtonPath1 = "Sprites/UserInterface/Buttons/WaveButtons/quitDown";

		// Blank Button 1 Path.
		private const string UnclickedBlankButton1Path = "Sprites/UserInterface/Buttons/HeroActionButtons/BlankButton1";
		private const string ClickedBlankButton1Path = "Sprites/UserInterface/Buttons/HeroActionButtons/BlankButton1";

		// Blank Button 2
		private const string UnclickedBlankButton2Path = "Sprites/UserInterface/Buttons/HeroActionButtons/BlankButton2";
		private const string ClickedBlankButton2Path = "Sprites/UserInterface/Buttons/HeroActionButtons/BlankButton2";

		// Blank Button 3
		private const string UnclickedBlankButton3Path = "Sprites/UserInterface/Buttons/HeroActionButtons/BlankButton3";
		private const string ClickedBlankButton3Path = "Sprites/UserInterface/Buttons/HeroActionButtons/BlankButton3";

		// Blank Button 4
		private const string UnclickedBlankButton4Path = "Sprites/UserInterface/Buttons/HeroActionButtons/BlankButton4";
		private const string ClickedBlankButton4Path = "Sprites/UserInterface/Buttons/HeroActionButtons/BlankButton4";

		#endregion

		#region Main Menu Paths

		// New Button Path.
		private const string UnclickedNewGameButtonPath = "Sprites/UserInterface/Buttons/MainMenuButtons/newGame_up_01";
		private const string ClickedNewGameButtonPath = "Sprites/UserInterface/Buttons/MainMenuButtons/newGame_down_01";

		//Load Button Path
		private const string UnClickedLoadGameButtonPath = "Sprites/UserInterface/Buttons/MainMenuButtons/loadGame_up";
		private const string ClickedLoadGameButtonPath = "Sprites/UserInterface/Buttons/MainMenuButtons/loadGame_down";

		// Tutorial Button Path.
		private const string UnclickedTutorialButtonPath = "Sprites/UserInterface/Buttons/MainMenuButtons/tutorial_up";
		private const string ClickedTutorialButtonPath = "Sprites/UserInterface/Buttons/MainMenuButtons/tutorial_down";
		private const string ClickedLeftArrowButtonPath = "Sprites/Tutorial/PreviousArrow";
		private const string ClickedRightArrowButtonPath = "Sprites/Tutorial/NextArrow";

		// Credit Button Path.
		private const string UnclickedCreditButtonPath = "Sprites/UserInterface/Buttons/MainMenuButtons/credits_up";
		private const string ClickedCreditButtonPath = "Sprites/UserInterface/Buttons/MainMenuButtons/credits_down";

		// Quit Button Path.
		private const string UnclickedQuitButtonPath = "Sprites/UserInterface/Buttons/MainMenuButtons/quit_up";
		private const string ClickedQuitButtonPath = "Sprites/UserInterface/Buttons/MainMenuButtons/quit_down";

		#endregion

		#region Hero Action Button Paths

		// Next Button
		private const string UnclickedNextButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Next_up";
		private const string ClickedNextButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Next_down";
		private const string DisabledNextButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Next_no";

		// Back Button
		private const string UnclickedBackButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Return-Arrow_up";
		private const string ClickedBackButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Return-Arrow_down";

		// Purchase Structure Button Path
		private const string UnclickedBuyStructureButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Purchase/Buy_up";
		private const string ClickedBuyStructureButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Purchase/Buy_down";
		private const string DisabledBuyStructureButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Purchase/Buy_no";

		// Spells Button Path.
		private const string UnclickedSpellsButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Spells/Spells_up";
		private const string ClickedSpellsButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Spells/Spells_down";

		// Build Button Path.
		private const string UnclickedBuildButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Build/Build_up";
		private const string ClickedBuildButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Build/Build_down";

		// Rally Button Path. 
		private const string UnclickedRallyButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Rally/Rally_up";
		private const string ClickedRallyButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Rally/Rally_down";




		#endregion

		#region Purchase Button Paths

		// Build Dragon Cave Button Path.
		private const string ClickedPurchaseDragonCaveButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Purchase/Dragons_down";
		private const string UnclickedPurchaseDragonCaveButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Purchase/Dragons_up";
		private const string DisabledPurchaseDragonCaveButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Purchase/Dragons_no";

		// Build Barracks Button Path.
		private const string ClickedPurchaseBarracksButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Purchase/Barracks_down";
		private const string UnclickedPurchaseBarracksButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Purchase/Barracks_up";
		private const string DisabledPurchaseBarracksButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Purchase/Barracks_no";

		// Build Wolf Pen Button Path.
		private const string ClickedPurchaseWolfPenButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Purchase/WolfPen_down";
		private const string UnclickedPurchaseWolfPenButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Purchase/WolfPen_up";
		private const string DisabledPurchaseWolfPenButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Purchase/WolfPen_no";

		// Build Armory Button Path.
		private const string ClickedPurchaseArmoryButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Purchase/Armory_down";
		private const string UnclickedPurchaseArmoryButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Purchase/Armory_up";
		private const string DisabledPurchaseArmoryButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Purchase/Armory_no";

		// Build Library Button Path.
		private const string ClickedPurchaseLibraryButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Purchase/Library_down";
		private const string UnclickedPurchaseLibraryButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Purchase/Library_up";
		private const string DisabledPurchaseLibraryButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Purchase/Library_no";

		// Build Abbey Button Path.
		private const string ClickedPurchaseAbbeyButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Purchase/Abbey_down";
		private const string UnclickedPurchaseAbbeyButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Purchase/Abbey_up";
		private const string DisabledPurchaseAbbeyButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Purchase/Abbey_no";

		// Build Bone Pit Button Path.
		private const string ClickedPurchaseBonePitButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Purchase/BonePit_down";
		private const string UnclickedPurchaseBonePitButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Purchase/BonePit_up";
		private const string DisabledPurchaseBonePitnButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Purchase/BonePit_no";

		// Build Fire Temple Button Path.
		private const string ClickedPurchaseFireTempleButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Purchase/Temple_down";
		private const string UnclickedPurchaseFireTempleButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Purchase/Temple_up";
		private const string DisabledPurchaseFireTempleButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Purchase/Temple_no";

		#endregion

		#region Build Button Paths

		// Build Dragon Cave Button Path.
		private const string ClickedBuildDragonCaveButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Build/Dragons_down";
		private const string UnclickedBuildDragonCaveButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Build/Dragons_up";
		private const string DisabledBuildDragonCaveButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Build/Dragons_no";

		// Build Barracks Button Path.
		private const string ClickedBuildBarracksButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Build/Barracks_down";
		private const string UnclickedBuildBarracksButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Build/Barracks_up";
		private const string DisabledBuildBarracksButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Build/Barracks_no";

		// Build Wolf Pen Button Path.
		private const string ClickedBuildWolfPenButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Build/WolfPen_down";
		private const string UnclickedBuildWolfPenButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Build/WolfPen_up";
		private const string DisabledBuildWolfPenButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Build/WolfPen_no";

		// Build Armory Button Path.
		private const string ClickedBuildArmoryButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Build/Armory_down";
		private const string UnclickedBuildArmoryButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Build/Armory_up";
		private const string DisabledBuildArmoryButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Build/Armory_no";

		// Build Library Button Path.
		private const string ClickedBuildLibraryButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Build/Library_down";
		private const string UnclickedBuildLibraryButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Build/Library_up";
		private const string DisabledBuildLibraryButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Build/Library_no";

		// Build Abbey Button Path.
		private const string ClickedBuildAbbeyButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Build/Abbey_down";
		private const string UnclickedAbbeyButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Build/Abbey_up";
		private const string DisabledBuildAbbeyButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Build/Abbey_no";

		// Build Bone Pit Button Path.
		private const string ClickedBuildBonePitButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Build/BonePit_down";
		private const string UnclickedBuildBonePitButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Build/BonePit_up";
		private const string DisabledBuildBonePitnButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Build/BonePit_no";

		// Build Fire Temple Button Path. 
		private const string ClickedBuildFireTempleButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Build/Temple_down";
		private const string UnclickedBuildFireTempleButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Build/Temple_up";
		private const string DisabledBuildFireTempleButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Build/Temple_no";

		#endregion

		#region Rally Button Paths
		// Rally Blue Button Path.
		private const string UnclickedBlueRallyButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Rally/FlagBlueUp";
		private const string ClickedBlueRallyButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Rally/FlagBlueDown";

		// Rally Red Button Path.
		private const string UnclickedRedRallyButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Rally/FlagRedUp";
		private const string ClickedRedRallyButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Rally/FlagRedDown";

		// Rally Green Button Path.
		private const string UnclickedGreenRallyButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Rally/FlagGreenUp";
		private const string ClickedGreenRallyButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Rally/FlagGreenDown";

		// Rally Yellow Button Path.
		private const string UnclickedYellowRallyButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Rally/FlagYellerUp";
		private const string ClickedYellowRallyButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Rally/FlagYellerDown";

		// Rally Purple Button Path.
		private const string UnclickedPurpleRallyButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Rally/FlagPurpleUp";
		private const string ClickedPurpleRallyButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Rally/FlagPurpleDown";
		#endregion

		#region Upgrade Button Paths

		// Upgrade Attack Button
		private const string UnclickedUpgradeAttackButton = "Sprites/UserInterface/Buttons/HeroActionButtons/Upgrade/UAup";
		private const string ClickedUpgradeAttackButton = "Sprites/UserInterface/Buttons/HeroActionButtons/Upgrade/UAdown";
		private const string DisabledUpgradeAttackButton = "Sprites/UserInterface/Buttons/HeroActionButtons/Upgrade/UAno";

		// Upgrade Defense Button
		private const string UnclickedUpgradeDefenseButton = "Sprites/UserInterface/Buttons/HeroActionButtons/Upgrade/UDup";
		private const string ClickedUpgradeDefenseButton = "Sprites/UserInterface/Buttons/HeroActionButtons/Upgrade/UDdown";
		private const string DisabledUpgradeDefenseButton = "Sprites/UserInterface/Buttons/HeroActionButtons/Upgrade/UDno";

		#endregion

		#region Spell Button Paths

		// Blessing Button
		private const string UnclickedBlessingButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Spells/battleCry_up";
		private const string ClickedBlessingButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Spells/battleCry_down";
		private const string DisabledBlessingButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Spells/battleCry_no";

		// Curse Button
		private const string UnclickedCurseButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Spells/intimidate_up";
		private const string ClickedCurseButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Spells/intimidate_down";
		private const string DisabledCurseButtonPath = "Sprites/UserInterface/Buttons/HeroActionButtons/Spells/intimidate_no";

		#endregion

		#endregion

		#region GameLoop Fields
		public static GameTime gameTime;
		public static ContentManager Content;
		public static SpriteBatch spriteBatch;
		public static GraphicsDevice graphics;
		public static JLinkedList mInPlayObjectList;
		public static Castle mCastle;
		#endregion

		#region Window Fields
		private static WindowFrame BottomMenuWindow;
		private static WindowFrame StatusWindow;
		private static WindowFrame TutorialWindow;
		private static WindowFrame TopMenuBar;
		private static WindowFrame MainMenuBanner;
		private static WindowFrame TitleSignWindow;
		private static WindowFrame WaveComplete;
		#endregion

		#region Menu Fields

		private static Menu MainMenu;
		private static Texture2D[] mTutorialImages = new Texture2D[5];
		private static string[] mImageFilePaths;
		private static int tutorialCounter = 0;

		#endregion

		#region Button Group Fields

		private static ButtonGroup TutorialNavigationButtonGroup;

		private static ButtonGroup SettingsButtonGroup;

		// Button Group to hold the Structure Placement Buttons.
		private static ButtonGroup StructureButtonGroup;

		private static ButtonGroup MainMenuButtonGroup;
		private static ButtonGroup HeroActionButtonGroup;

		// Spell Button Group.
		private static ButtonGroup SpellButtonGroup;

		// RallyButton Button Group
		private static ButtonGroup RallyButtonGroup;

		// BuildButton Button Groups.
		private static ButtonGroup BuildButtonGroup;
		private static ButtonGroup BuildButtonGroupPageTwo;


		// Utility Button Group.
		private static ButtonGroup UtilityButtonGroup;

		// Upgrade Unit ButtonGroup.
		private static ButtonGroup UpgradeUnitButtonGroup;
		private static ButtonGroup UpgradeUnitButtonGroup1;
		private static ButtonGroup UpgradeUnitButtonGroup2;
		private static ButtonGroup UpgradeUnitButtonGroup3;
		private static ButtonGroup UpgradeUnitButtonGroup4;
		private static ButtonGroup UpgradeUnitButtonGroup5;

		// Purchase Structure Button Group
		private static ButtonGroup PurchaseStructureButtonGroup;
		private static ButtonGroup PurchaseStrctureButtonGroupPageTwo;

		private static ButtonGroup SaveButtonGroup;
		private static ButtonGroup QuitButtonGroup;

		#endregion

		#region Button Fields

		public static Button QuitButton;
		public static Button SaveButton;

		// Settings Button Fields
		public static Button SettingsButton;


		// Structure Placement Buttons.
		public static Button BlueStructureButton;
		public static Button YellowStructureButton;
		public static Button PurpleStructureButton;
		public static Button RedStructureButton;
		public static Button GreenStructureButton;

		// Blank Buttons.
		private static Button BlankButton0;
		private static Button BlankButton1;
		private static Button BlankButton2;
		private static Button BlankButton3;
		private static Button BlankButton4;
		private static Button BlankButton5;
		private static Button BlankButton6;
		private static Button BlankButton7;
		private static Button BlankButton8;
		private static Button BlankButton9;
		private static Button BlankButton10;
		private static Button BlankButton11;
		private static Button BlankButton12;
		private static Button BlankButton13;
		private static Button BlankButton14;
		private static Button BlankButton15;
		private static Button BlankButton16;
		private static Button BlankButton17;
		private static Button BlankButton18;
		private static Button BlankButton19;

		// In-GameController MenuBar.
		private static Button Pause;
		private static Button Quit;


		// Main Menu Buttons.
		private static Button NewGameButton;
		private static Button LoadGameButton;
		private static Button QuitGameButton;
		private static Button TutorialButton;
		private static Button CreditButton;

		// Utility Buttons.
		private static Button BackButton;
		private static Button NextButton;

		// BuildButton Buttons.
		private static Button BuildButton;
		private static Button BuildDragonButton;
		private static Button BuildBerzerkerButton;
		private static Button BuildWolfButton;
		private static Button BuildAxeThrowerButton;
		private static Button BuildArcaneMageButton;
		private static Button BuildFireMageButton;
		private static Button BuildNecromanceButton;
		private static Button BuildClericButton;


		// Upgrade Buttons.
		private static Button PurchaseStructureButton;

		// Which structure would you like to purchase?
		private static Button PurchaseArmoryButton;
		private static Button PurchaseBarracksButton;
		private static Button PurchaseWolfpenButton;
		private static Button PurchaseAbbeyButton;
		private static Button PurchaseDragonCaveButton;
		private static Button PurchaseBonePitButton;
		private static Button PurchaseFireTempleButton;
		private static Button PurchaseLibraryButton;

		// Upgrade Buttons.
		private static Button PurchaseUpgradeButton;

		// What would you like to upgrade?
		private static Button UpgradeAttackStructurePosition1;
		private static Button UpgradeDefenseStructurePosition1;
		private static Button UpgradeAttackStructurePosition2;
		private static Button UpgradeDefenseStructurePosition2;
		private static Button UpgradeAttackStructurePosition3;
		private static Button UpgradeDefenseStructurePosition3;
		private static Button UpgradeAttackStructurePosition4;
		private static Button UpgradeDefenseStructurePosition4;
		private static Button UpgradeAttackStructurePosition5;
		private static Button UpgradeDefenseStructurePosition5;
		// Attack or Defense?
		private static Button UpgradeAttackButton;
		private static Button UpgradeDefenseButton;

		// Stronger walls on your castle?
		private static Button UpgradeCastleButton;

		// Spell Buttons.
		private static Button SpellsButton;
		private static Button BlessingButton;
		private static Button CurseButton;

		// RallyButton Troop Buttons.
		private static Button RallyButton;
		private static Button RallyBlueButton;
		private static Button RallyRedButton;
		private static Button RallyPurpleButton;
		private static Button RallyGreenButton;
		private static Button RallyYellowButton;

		// Tutorial Buttons

		private static Button LeftArrowButton;
		private static Button RightArrowButton;

		#endregion

		#region Timers

		private static float clickTime = 0;
		private static float battleCryTimer = 0;
		private static float curseTimer = 0;

		#endregion

		#region Mouse Fields

		private static Texture2D gauntlet;
		private static Texture2D attack;
		public static Rectangle mouseRectangle;
		public static CursorState cursorState = CursorState.GAUNTLET;

		#endregion

		#region Draw String Fields
		private static StructureAttribute tempStrucutreAttribute;
		private static string tempPurchaseString = " ";

		private static Structure tempStructure;
		private static string tempUnitString = " ";
		private static Friendly tempFriendly;

		#endregion

		// Functions

		#region Controller Functions

		// Load the Content for each object.
		internal static void LoadUserInterface(GameTime gametime, ContentManager contentManager, SpriteBatch spritebatch)
		{
			// Set up Dependant Variables
			gameTime = gametime;
			Content = contentManager;
			spriteBatch = spritebatch;

			InitializeMouseTextures();
			InitializeWindows();
			InitializeMenus();
			InitializeButtonGroups();
			InitializeButtons();
			InitializeSpriteFonts();
			InitializeGlobes();

			LoadMenus();
			LoadWindows();
			LoadButtonGroups();
			LoadGlobes();

			HeroActionGroupState = HeroActionButtonGroupState.HERO_ACTION;
		}

		// Updates positions and states for each button group/menu/button.
		internal static void UpdateUIActiveState(GameTime gameTime)
		{
			if (FriendlyAttribute.BattleCrySpellState == BattleCrySpellState.ACTIVE && HeroAttribute.BattleCrySpellState == BattleCrySpellState.ACTIVE)
			{
				battleCryTimer += gameTime.ElapsedGameTime.Milliseconds;

				if (battleCryTimer >= mHero.HeroAttribute.CalculateSpellTimer())
				{
					FriendlyAttribute.BattleCrySpellState = BattleCrySpellState.INACTIVE;
					HeroAttribute.BattleCrySpellState = BattleCrySpellState.INACTIVE;
					battleCryTimer = 0;
				}
				HeroActionGroupState = HeroActionButtonGroupState.HERO_ACTION;
			}

			if (EnemyAttribute.IntimidateSpellState == IntimidateSpellState.ACTIVE && HeroAttribute.IntimidateSpellState == IntimidateSpellState.ACTIVE)
			{
				curseTimer += gameTime.ElapsedGameTime.Milliseconds;

				if (curseTimer >= mHero.HeroAttribute.CalculateSpellTimer())
				{
					HeroAttribute.IntimidateSpellState = IntimidateSpellState.INACTIVE;
					EnemyAttribute.IntimidateSpellState = IntimidateSpellState.INACTIVE;
					curseTimer = 0;
				}
				HeroActionGroupState = HeroActionButtonGroupState.HERO_ACTION;
			}
			mouseState = Mouse.GetState();
			UpdateMouse(gameTime);

			BottomMenuWindow.Update(gameTime);
			UtilityButtonGroup.Update(gameTime);
			SettingsButtonGroup.Update(gameTime);
			StatusWindow.Update(gameTime);
			TopMenuBar.Update(gameTime);
			mHealthGlobeLiquid.UpdateHp();
			mManaGlobeLiquid.UpdateMana();
			ListenMouseClicks(gameTime);
			ListenMouseover(gameTime);

			if (HeroActionGroupState == HeroActionButtonGroupState.HERO_ACTION)
			{

				HeroActionButtonGroup.Update(gameTime);
			}

			if (HeroActionGroupState == HeroActionButtonGroupState.BUILD_PAGE_ONE)
			{
				BuildButtonGroup.Update(gameTime);
				StructureButtonGroup.Update(gameTime);
			}

			if (HeroActionGroupState == HeroActionButtonGroupState.BUILD_PAGE_TWO)
			{
				BuildButtonGroupPageTwo.Update(gameTime);
				StructureButtonGroup.Update(gameTime);
			}

			if (HeroActionGroupState == HeroActionButtonGroupState.RALLY)
			{
				RallyButtonGroup.Update(gameTime);
				StructureButtonGroup.Update(gameTime);
			}
			if (HeroActionGroupState == HeroActionButtonGroupState.PURCHASE_PAGE_ONE)
			{
				PurchaseStructureButtonGroup.Update(gameTime);
			}
			if (HeroActionGroupState == HeroActionButtonGroupState.PURCHASE_PAGE_TWO)
			{
				PurchaseStrctureButtonGroupPageTwo.Update(gameTime);
			}
			if (HeroActionGroupState == HeroActionButtonGroupState.UPGRADE_ONE)
			{
				UpgradeUnitButtonGroup1.Update(gameTime);
			}
			if (HeroActionGroupState == HeroActionButtonGroupState.UPGRADE_TWO)
			{
				UpgradeUnitButtonGroup2.Update(gameTime);
			}
			if (HeroActionGroupState == HeroActionButtonGroupState.UPGRADE_THREE)
			{
				UpgradeUnitButtonGroup3.Update(gameTime);
			}
			if (HeroActionGroupState == HeroActionButtonGroupState.UPGRADE_FOUR)
			{
				UpgradeUnitButtonGroup4.Update(gameTime);
			}
			if (HeroActionGroupState == HeroActionButtonGroupState.UPGRADE_FIVE)
			{
				UpgradeUnitButtonGroup5.Update(gameTime);
			}
			if (HeroActionGroupState == HeroActionButtonGroupState.SPELLS)
			{
				SpellButtonGroup.Update(gameTime);
			}

			UpdateDisabledButtons();
		}



		internal static void UpdateDisabledButtons()
		{
			//todo: Purchase Structure Buttons
			if (GameController.StructurePool[0][0] != null && mHero.HeroAttribute.Gold < GameController.StructurePool[0][0].StructureAttribute.Cost)
			{
				PurchaseDragonCaveButton.IsDisabled = true;
			}
			else
			{
				PurchaseDragonCaveButton.IsDisabled = false;
			}
			if (GameController.StructurePool[1][0] != null && mHero.HeroAttribute.Gold < GameController.StructurePool[1][0].StructureAttribute.Cost)
			{
				PurchaseBonePitButton.IsDisabled = true;
			}
			else
			{
				PurchaseBonePitButton.IsDisabled = false;
			}
			if (GameController.StructurePool[2][0] != null && mHero.HeroAttribute.Gold < GameController.StructurePool[2][0].StructureAttribute.Cost)
			{
				PurchaseLibraryButton.IsDisabled = true;
			}
			else
			{
				PurchaseLibraryButton.IsDisabled = false;
			}
			if (GameController.StructurePool[3][0] != null && mHero.HeroAttribute.Gold < GameController.StructurePool[3][0].StructureAttribute.Cost)
			{
				PurchaseFireTempleButton.IsDisabled = true;
			}
			else
			{
				PurchaseFireTempleButton.IsDisabled = false;
			}
			if (GameController.StructurePool[4][0] != null && mHero.HeroAttribute.Gold < GameController.StructurePool[4][0].StructureAttribute.Cost)
			{
				PurchaseArmoryButton.IsDisabled = true;
			}
			else
			{
				PurchaseArmoryButton.IsDisabled = false;
			}
			if (GameController.StructurePool[5][0] != null && mHero.HeroAttribute.Gold < GameController.StructurePool[5][0].StructureAttribute.Cost)
			{
				PurchaseBarracksButton.IsDisabled = true;
			}
			else
			{
				PurchaseBarracksButton.IsDisabled = false;
			}
			if (GameController.StructurePool[6][0] != null && mHero.HeroAttribute.Gold < GameController.StructurePool[6][0].StructureAttribute.Cost)
			{
				PurchaseWolfpenButton.IsDisabled = true;
			}
			else
			{
				PurchaseWolfpenButton.IsDisabled = false;
			}
			if (GameController.StructurePool[7][0] != null && mHero.HeroAttribute.Gold < GameController.StructurePool[7][0].StructureAttribute.Cost)
			{
				PurchaseAbbeyButton.IsDisabled = true;
			}
			else
			{
				PurchaseAbbeyButton.IsDisabled = false;
			}

			//todo: Building Global Cooldown
			if (StructureAttribute.CoolDownState == CoolDownState.ONCOOLDOWN)
			{
				BuildAxeThrowerButton.IsDisabled = true;
				BuildArcaneMageButton.IsDisabled = true;
				BuildDragonButton.IsDisabled = true;
				BuildClericButton.IsDisabled = true;
				BuildFireMageButton.IsDisabled = true;
				BuildNecromanceButton.IsDisabled = true;
				BuildWolfButton.IsDisabled = true;
				BuildBerzerkerButton.IsDisabled = true;
			}
			else
			{
				BuildAxeThrowerButton.IsDisabled = false;
				BuildArcaneMageButton.IsDisabled = false;
				BuildDragonButton.IsDisabled = false;
				BuildClericButton.IsDisabled = false;
				BuildFireMageButton.IsDisabled = false;
				BuildNecromanceButton.IsDisabled = false;
				BuildWolfButton.IsDisabled = false;
				BuildBerzerkerButton.IsDisabled = false;
			}

			//todo: Rally Buttons
			if (GameController.InPlayStructures[0] == null)
			{
				RallyBlueButton.IsDisabled = true;
			}
			else
			{
				RallyBlueButton.IsDisabled = false;
			}
			if (GameController.InPlayStructures[1] == null)
			{
				RallyRedButton.IsDisabled = true;
			}
			else
			{
				RallyRedButton.IsDisabled = false;
			}
			if (GameController.InPlayStructures[2] == null)
			{
				RallyYellowButton.IsDisabled = true;
			}
			else
			{
				RallyYellowButton.IsDisabled = false;
			}
			if (GameController.InPlayStructures[3] == null)
			{
				RallyGreenButton.IsDisabled = true;
			}
			else
			{
				RallyGreenButton.IsDisabled = false;
			}
			if (GameController.InPlayStructures[4] == null)
			{
				RallyPurpleButton.IsDisabled = true;
			}
			else
			{
				RallyPurpleButton.IsDisabled = false;
			}


			//todo: Spell Buttons
			if (mHero.HeroAttribute.Mana < 15)
			{
				CurseButton.IsDisabled = true;
				BlessingButton.IsDisabled = true;
			}
			else
			{
				CurseButton.IsDisabled = false;
				BlessingButton.IsDisabled = false;
			}

			//todo: Owned Structures Buttons
			int wolfCount = 0;
			int berserkerCount = 0;
			int axeThrowerCount = 0;
			int arcaneMageCount = 0;
			int clericCount = 0;
			int nercomancerCount = 0;
			int fireMageCount = 0;
			int dragonCount = 0;
			for (int i = 0; i < GameController.InPlayStructures.Length; i++)
			{
				if (GameController.InPlayStructures[i] != null)
				{
					switch (GameController.InPlayStructures[i].StructureType)
					{
						case ObjectType.DRAGON_CAVE:
							dragonCount++;
							break;
						case ObjectType.BONEPIT:
							nercomancerCount++;
							break;
						case ObjectType.LIBRARY:
							arcaneMageCount++;
							break;
						case ObjectType.FIRE_TEMPLE:
							fireMageCount++;
							break;
						case ObjectType.ARMORY:
							axeThrowerCount++;
							break;
						case ObjectType.BARRACKS:
							berserkerCount++;
							break;
						case ObjectType.WOLFPEN:
							wolfCount++;
							break;
						case ObjectType.ABBEY:
							clericCount++;
							break;

						default:
							Console.WriteLine("Problem with disable buttons");
							break;
					}
				}
			}

			if (mHero.HeroAttribute.OwnedAbbey - clericCount <= 0)
			{
				BuildClericButton.IsDisabled = true;
			}
			else
			{
				BuildClericButton.IsDisabled = false;
			}
			if (mHero.HeroAttribute.OwnedArmories - axeThrowerCount <= 0)
			{
				BuildAxeThrowerButton.IsDisabled = true;
			}
			else
			{
				BuildAxeThrowerButton.IsDisabled = false;
			}
			if (mHero.HeroAttribute.OwnedBarracks - berserkerCount <= 0)
			{
				BuildBerzerkerButton.IsDisabled = true;
			}
			else
			{
				BuildBerzerkerButton.IsDisabled = false;
			}
			if (mHero.HeroAttribute.OwnedBonePit - nercomancerCount <= 0)
			{
				BuildNecromanceButton.IsDisabled = true;
			}
			else
			{
				BuildNecromanceButton.IsDisabled = false;
			}
			if (mHero.HeroAttribute.OwnedDragonCaves - dragonCount <= 0)
			{
				BuildDragonButton.IsDisabled = true;
			}
			else
			{
				BuildDragonButton.IsDisabled = false;
			}
			if (mHero.HeroAttribute.OwnedFireTemple - fireMageCount <= 0)
			{
				BuildFireMageButton.IsDisabled = true;
			}
			else
			{
				BuildFireMageButton.IsDisabled = false;
			}
			if (mHero.HeroAttribute.OwnedLibraries - arcaneMageCount <= 0)
			{
				BuildArcaneMageButton.IsDisabled = true;
			}
			else
			{
				BuildArcaneMageButton.IsDisabled = false;
			}
			if (mHero.HeroAttribute.OwnedWolfPens - wolfCount <= 0)
			{
				BuildWolfButton.IsDisabled = true;
			}
			else
			{
				BuildWolfButton.IsDisabled = false;
			}


			//todo: Upgrade Attack Buttons
			if (GameController.InPlayStructures[0] != null && GameController.InPlayStructures[0].StructureAttribute.UpgradeAttackCost > mHero.HeroAttribute.Gold)
			{
				UpgradeAttackStructurePosition1.IsDisabled = true;
			}
			else if (GameController.InPlayStructures[0] != null)
			{
				UpgradeAttackStructurePosition1.IsDisabled = false;
			}
			if (GameController.InPlayStructures[1] != null && GameController.InPlayStructures[1].StructureAttribute.UpgradeAttackCost > mHero.HeroAttribute.Gold)
			{
				UpgradeAttackStructurePosition2.IsDisabled = true;
			}
			else if (GameController.InPlayStructures[1] != null)
			{
				UpgradeAttackStructurePosition2.IsDisabled = false;
			}
			if (GameController.InPlayStructures[2] != null && GameController.InPlayStructures[2].StructureAttribute.UpgradeAttackCost > mHero.HeroAttribute.Gold)
			{
				UpgradeAttackStructurePosition3.IsDisabled = true;
			}
			else if (GameController.InPlayStructures[2] != null)
			{
				UpgradeAttackStructurePosition3.IsDisabled = false;
			}
			if (GameController.InPlayStructures[3] != null && GameController.InPlayStructures[3].StructureAttribute.UpgradeAttackCost > mHero.HeroAttribute.Gold)
			{
				UpgradeAttackStructurePosition4.IsDisabled = true;
			}
			else if (GameController.InPlayStructures[3] != null)
			{
				UpgradeAttackStructurePosition4.IsDisabled = false;
			}
			if (GameController.InPlayStructures[4] != null && GameController.InPlayStructures[4].StructureAttribute.UpgradeAttackCost > mHero.HeroAttribute.Gold)
			{
				UpgradeAttackStructurePosition5.IsDisabled = true;
			}
			else if (GameController.InPlayStructures[4] != null)
			{
				UpgradeAttackStructurePosition5.IsDisabled = false;
			}


			//todo: Upgrade Defense Buttons
			if (GameController.InPlayStructures[0] != null && GameController.InPlayStructures[0].StructureAttribute.UpgradeDefenseCost > mHero.HeroAttribute.Gold)
			{
				UpgradeDefenseStructurePosition1.IsDisabled = true;
			}
			else if (GameController.InPlayStructures[0] != null)
			{
				UpgradeDefenseStructurePosition1.IsDisabled = false;
			}
			if (GameController.InPlayStructures[1] != null && GameController.InPlayStructures[1].StructureAttribute.UpgradeDefenseCost > mHero.HeroAttribute.Gold)
			{
				UpgradeDefenseStructurePosition2.IsDisabled = true;
			}
			else if (GameController.InPlayStructures[1] != null)
			{
				UpgradeDefenseStructurePosition2.IsDisabled = false;
			}
			if (GameController.InPlayStructures[2] != null && GameController.InPlayStructures[2].StructureAttribute.UpgradeDefenseCost > mHero.HeroAttribute.Gold)
			{
				UpgradeDefenseStructurePosition3.IsDisabled = true;
			}
			else if (GameController.InPlayStructures[2] != null)
			{
				UpgradeDefenseStructurePosition3.IsDisabled = false;
			}
			if (GameController.InPlayStructures[3] != null && GameController.InPlayStructures[3].StructureAttribute.UpgradeDefenseCost > mHero.HeroAttribute.Gold)
			{
				UpgradeDefenseStructurePosition4.IsDisabled = true;
			}
			else if (GameController.InPlayStructures[3] != null)
			{
				UpgradeDefenseStructurePosition4.IsDisabled = false;
			}
			if (GameController.InPlayStructures[4] != null && GameController.InPlayStructures[4].StructureAttribute.UpgradeDefenseCost > mHero.HeroAttribute.Gold)
			{
				UpgradeDefenseStructurePosition5.IsDisabled = true;
			}
			else if (GameController.InPlayStructures[4] != null)
			{
				UpgradeDefenseStructurePosition5.IsDisabled = false;
			}
		}

		// Draws each button group/menu/button.
		internal static void DrawUIActiveState(SpriteBatch spriteBatch)
		{
			BottomMenuWindow.Draw(spriteBatch);
			UtilityButtonGroup.Draw(spriteBatch);
			StatusWindow.Draw(spriteBatch);
			TopMenuBar.Draw(spriteBatch);
			mHealthGlobeLiquid.Draw();
			mManaGlobeLiquid.Draw();
			DrawTopMenuBarAttributes();
			SettingsButtonGroup.Draw(spriteBatch);


			
			if (HeroActionGroupState == HeroActionButtonGroupState.HERO_ACTION)
			{

				HeroActionButtonGroup.Draw(spriteBatch);

			}
			if (HeroActionGroupState == HeroActionButtonGroupState.BUILD_PAGE_ONE)
			{
				BuildButtonGroup.Draw(spriteBatch);
				StructureButtonGroup.Draw(spriteBatch);
			}
			if (HeroActionGroupState == HeroActionButtonGroupState.BUILD_PAGE_TWO)
			{
				BuildButtonGroupPageTwo.Draw(spriteBatch);
				StructureButtonGroup.Draw(spriteBatch);
			}

			if (HeroActionGroupState == HeroActionButtonGroupState.RALLY)
			{
				RallyButtonGroup.Draw(spriteBatch);
				StructureButtonGroup.Draw(spriteBatch);
			}
			if (HeroActionGroupState == HeroActionButtonGroupState.PURCHASE_PAGE_ONE)
			{
				PurchaseStructureButtonGroup.Draw(spriteBatch);
			}
			if (HeroActionGroupState == HeroActionButtonGroupState.PURCHASE_PAGE_TWO)
			{
				PurchaseStrctureButtonGroupPageTwo.Draw(spriteBatch);
			}
			if (HeroActionGroupState == HeroActionButtonGroupState.UPGRADE_ONE)
			{
				UpgradeUnitButtonGroup1.Draw(spriteBatch);
				DrawUpgradeUnitAttributes();
			}
			if (HeroActionGroupState == HeroActionButtonGroupState.UPGRADE_TWO)
			{
				UpgradeUnitButtonGroup2.Draw(spriteBatch);
				DrawUpgradeUnitAttributes();
			}
			if (HeroActionGroupState == HeroActionButtonGroupState.UPGRADE_THREE)
			{
				UpgradeUnitButtonGroup3.Draw(spriteBatch);
				DrawUpgradeUnitAttributes();
			}
			if (HeroActionGroupState == HeroActionButtonGroupState.UPGRADE_FOUR)
			{
				UpgradeUnitButtonGroup4.Draw(spriteBatch);
				DrawUpgradeUnitAttributes();
			}
			if (HeroActionGroupState == HeroActionButtonGroupState.UPGRADE_FIVE)
			{
				UpgradeUnitButtonGroup5.Draw(spriteBatch);
				DrawUpgradeUnitAttributes();
			}

			if (purchaseStructureDrawState == PurhcaseStructureDrawState.ACTIVE)
			{
				DrawPurchaseStructureStatus();
			}

			if (HeroActionGroupState == HeroActionButtonGroupState.SPELLS)
			{
				SpellButtonGroup.Draw(spriteBatch);
			}

			DrawMouse(spriteBatch);
		}

		public static void DrawWaveComplete(SpriteBatch spriteBatch)
		{
			WaveComplete.Draw(spriteBatch);
			QuitButtonGroup.Draw(spriteBatch);
			SaveButtonGroup.Draw(spriteBatch);
		}

		public static void UpdateWaveComplete(GameTime gameTime)
		{
			WaveComplete.Update(gameTime);
			QuitButtonGroup.Update(gameTime);
			SaveButtonGroup.Update(gameTime);
		}

		#endregion

		#region Mouseover Listeners

		internal static void ListenMouseover(GameTime gameTime)
		{
			ListenForMouseoverUnit();
			ListenFroMouseoverButtons();
		}

		internal static void ListenFroMouseoverButtons()
		{

			if (HeroActionGroupState == HeroActionButtonGroupState.PURCHASE_PAGE_ONE)
			{
				for (int i = 0; i < PurchaseStructureButtonGroup.ButtonList.Count; i++)
				{
					if (PurchaseStructureButtonGroup.ButtonList[i].ButtonRectangle.Contains(mouseRectangle))
					{
						if (PurchaseStructureButtonGroup.ButtonList[i] == PurchaseWolfpenButton)
						{
							purchaseStructureDrawState = PurhcaseStructureDrawState.ACTIVE;
							tempStrucutreAttribute = GameController.StructurePool[6][0].StructureAttribute;
							tempPurchaseString = "Wolf";
						}
						if (PurchaseStructureButtonGroup.ButtonList[i] == PurchaseBarracksButton)
						{
							purchaseStructureDrawState = PurhcaseStructureDrawState.ACTIVE;
							tempStrucutreAttribute = GameController.StructurePool[5][0].StructureAttribute;
							tempPurchaseString = "Berserker";
						}
						if (PurchaseStructureButtonGroup.ButtonList[i] == PurchaseArmoryButton)
						{
							purchaseStructureDrawState = PurhcaseStructureDrawState.ACTIVE;
							tempStrucutreAttribute = GameController.StructurePool[4][0].StructureAttribute;
							tempPurchaseString = "Axe Thrower";
						}
						if (PurchaseStructureButtonGroup.ButtonList[i] == PurchaseLibraryButton)
						{
							purchaseStructureDrawState = PurhcaseStructureDrawState.ACTIVE;
							tempStrucutreAttribute = GameController.StructurePool[2][0].StructureAttribute;
							tempPurchaseString = "Arcane Mage";
						}
						if (PurchaseStructureButtonGroup.ButtonList[i] == PurchaseAbbeyButton)
						{
							purchaseStructureDrawState = PurhcaseStructureDrawState.ACTIVE;
							tempStrucutreAttribute = GameController.StructurePool[7][0].StructureAttribute;
							tempPurchaseString = "Cleric";
						}

					}
				}
			}


			if (HeroActionGroupState == HeroActionButtonGroupState.PURCHASE_PAGE_TWO)
			{
				for (int i = 0; i < PurchaseStrctureButtonGroupPageTwo.ButtonList.Count; i++)
				{
					if (PurchaseStructureButtonGroup.ButtonList[i].ButtonRectangle.Contains(mouseRectangle))
					{
						if (PurchaseStrctureButtonGroupPageTwo.ButtonList[i] == PurchaseBonePitButton)
						{
							purchaseStructureDrawState = PurhcaseStructureDrawState.ACTIVE;
							tempStrucutreAttribute = GameController.StructurePool[1][0].StructureAttribute;
							tempPurchaseString = "Necromancer";
						}
						if (PurchaseStrctureButtonGroupPageTwo.ButtonList[i] == PurchaseFireTempleButton)
						{
							purchaseStructureDrawState = PurhcaseStructureDrawState.ACTIVE;
							tempStrucutreAttribute = GameController.StructurePool[3][0].StructureAttribute;
							tempPurchaseString = "Fire Mage";
						}
						if (PurchaseStrctureButtonGroupPageTwo.ButtonList[i] == PurchaseDragonCaveButton)
						{
							purchaseStructureDrawState = PurhcaseStructureDrawState.ACTIVE;
							tempStrucutreAttribute = GameController.StructurePool[0][0].StructureAttribute;
							tempPurchaseString = "Dragon";
						}
					}
				}
			}




		}



		internal static void ListenForMouseoverUnit()
		{
			mInPlayObjectList.Reset();

			for (int i = 0; i < mInPlayObjectList.GetCount(); i++)
			{


				if (mInPlayObjectList.GetCurrent().gameObject.Hostility == Hostility.ENEMY &&
					mInPlayObjectList.GetCurrent().gameObject.Sprite.SpriteFrame.Contains(mouseRectangle))
				{
					cursorState = CursorState.ATTACK;
					break;
				}
				else
				{
					cursorState = CursorState.GAUNTLET;
				}

				mInPlayObjectList.NextNode();
			}
		}

		#endregion

		#region Mouse Click Listeners

		internal static void ListenMouseClicks(GameTime gameTime)
		{

			clickTime += gameTime.ElapsedGameTime.Milliseconds;


			if (mouseState.LeftButton == ButtonState.Pressed && clickTime >= 300)
			{

				ListenForSelectedUnit();
				ListenForRallyPoint();


				clickTime = 0;
			}


			prevoiusMouseState = mouseState;
		}

		internal static void ListenForUpgrade()
		{
			for (int i = 0; i < StructureButtonGroup.ButtonList.Count; i++)
			{
				if (StructureButtonGroup.ButtonList[i].ButtonRectangle.Contains(mouseRectangle))
				{

					if (StructureButtonGroup.ButtonList[i] == BlueStructureButton &&
						BlueOccupationState == StructureOccupationState.OCCUPIED)
					{
						tempStructure = (Structure)GameObject.mInPlayObjectList.GetCurrent().gameObject;
						HeroActionGroupState = HeroActionButtonGroupState.UPGRADE_ONE;
						break;
					}
					if (StructureButtonGroup.ButtonList[i] == RedStructureButton &&
						RedOccupationState == StructureOccupationState.OCCUPIED)
					{
						tempStructure = (Structure)GameObject.mInPlayObjectList.GetCurrent().gameObject;
						HeroActionGroupState = HeroActionButtonGroupState.UPGRADE_TWO;
						break;
					}
					if (StructureButtonGroup.ButtonList[i] == YellowStructureButton &&
						YellowOccupationState == StructureOccupationState.OCCUPIED)
					{
						tempStructure = (Structure)GameObject.mInPlayObjectList.GetCurrent().gameObject;
						HeroActionGroupState = HeroActionButtonGroupState.UPGRADE_THREE;
						break;
					}
					if (StructureButtonGroup.ButtonList[i] == GreenStructureButton &&
						GreenOccupationState == StructureOccupationState.OCCUPIED)
					{
						tempStructure = (Structure)GameObject.mInPlayObjectList.GetCurrent().gameObject;
						HeroActionGroupState = HeroActionButtonGroupState.UPGRADE_FOUR;
						break;
					}
					if (StructureButtonGroup.ButtonList[i] == PurpleStructureButton &&
						PurpleOccupationState == StructureOccupationState.OCCUPIED)
					{
						tempStructure = (Structure)GameObject.mInPlayObjectList.GetCurrent().gameObject;
						HeroActionGroupState = HeroActionButtonGroupState.UPGRADE_FIVE;
						break;
					}
				}
			}

		}

		internal static void ListenForSelectedUnit()
		{
			GameObject.mInPlayObjectList.Reset();

			for (int i = 0; i < GameObject.mInPlayObjectList.GetCount(); i++)
			{

				if (GameObject.mInPlayObjectList.GetCurrent().gameObject.Sprite.SpriteFrame.
					Contains(mouseRectangle) ||
					GameObject.mInPlayObjectList.GetCurrent().gameObject.Sprite.SpriteFrame.
					Intersects(mouseRectangle))
				{
					if (GameObject.mInPlayObjectList.GetCurrent().gameObject.Hostility == Hostility.STRUCTURE)
					{
						ListenForUpgrade();
						break;
					}

					else
					{
						tempObject = GameObject.mInPlayObjectList.GetCurrent().gameObject;
						StatusWindow.SetDrawText(DrawSelectedUnit);
						StatusWindow.StatusWindowState = StatusWindowState.ACTIVE;
						break;
					}

				}
				else
				{
					StatusWindow.StatusWindowState = StatusWindowState.INACTIVE;
				}


				GameObject.mInPlayObjectList.NextNode();
			}
		}

		internal static void ListenForRallyPoint()
		{

			if (RallyTargetState == RallyFetchTarget.ACTIVE && GameController.PlayingField.Contains(mouseRectangle))
			{

				switch (objectColor)
				{
					case ObjectColor.BLUE:

						GameObject.mPotentialTargetListList.Reset();
						for (int i = 0; i < GameObject.mPotentialTargetListList.GetCount(); i++)
						{

							if (GameObject.mPotentialTargetListList.GetCurrent().gameObject.Hostility == Hostility.FRIENDLY &&
								!(GameObject.mPotentialTargetListList.GetCurrent().gameObject is Hero))
							{
								Friendly temp = (Friendly)GameObject.mPotentialTargetListList.GetCurrent().gameObject;

								if (temp.Color == ObjectColor.BLUE)
								{

									temp.DefaultTarget = new Vector2(mouseState.X, mouseState.Y);
								}
							}

							GameObject.mPotentialTargetListList.NextNode();
						}
						if (GameController.InPlayStructures[0] != null)
							GameController.InPlayStructures[0].StructureAttribute.DefaultUnitTarget = new Vector2(mouseState.X, mouseState.Y);
						break;

					case ObjectColor.GREEN:

						GameObject.mPotentialTargetListList.Reset();
						for (int i = 0; i < GameObject.mPotentialTargetListList.GetCount(); i++)
						{
							if (GameObject.mPotentialTargetListList.GetCurrent().gameObject.Hostility == Hostility.FRIENDLY &&
								!(GameObject.mPotentialTargetListList.GetCurrent().gameObject is Hero))
							{
								Friendly temp = (Friendly)GameObject.mPotentialTargetListList.GetCurrent().gameObject;

								if (temp.Color == ObjectColor.GREEN)
								{
									temp.DefaultTarget = new Vector2(mouseState.X, mouseState.Y);
								}
							}
							GameObject.mPotentialTargetListList.NextNode();
						}
						if (GameController.InPlayStructures[3] != null)
							GameController.InPlayStructures[3].StructureAttribute.DefaultUnitTarget = new Vector2(mouseState.X, mouseState.Y);

						break;

					case ObjectColor.PURPLE:

						GameObject.mPotentialTargetListList.Reset();
						for (int i = 0; i < GameObject.mPotentialTargetListList.GetCount(); i++)
						{
							if (GameObject.mPotentialTargetListList.GetCurrent().gameObject.Hostility == Hostility.FRIENDLY &&
								!(GameObject.mPotentialTargetListList.GetCurrent().gameObject is Hero))
							{
								Friendly temp = (Friendly)GameObject.mPotentialTargetListList.GetCurrent().gameObject;

								if (temp.Color == ObjectColor.PURPLE)
								{
									temp.DefaultTarget = new Vector2(mouseState.X, mouseState.Y);
								}
							}
							GameObject.mPotentialTargetListList.NextNode();
						}
						if (GameController.InPlayStructures[4] != null)
							GameController.InPlayStructures[4].StructureAttribute.DefaultUnitTarget = new Vector2(mouseState.X, mouseState.Y);

						break;

					case ObjectColor.RED:

						GameObject.mPotentialTargetListList.Reset();
						for (int i = 0; i < GameObject.mPotentialTargetListList.GetCount(); i++)
						{
							if (GameObject.mPotentialTargetListList.GetCurrent().gameObject.Hostility == Hostility.FRIENDLY &&
								!(GameObject.mPotentialTargetListList.GetCurrent().gameObject is Hero))
							{
								Friendly temp = (Friendly)GameObject.mPotentialTargetListList.GetCurrent().gameObject;

								if (temp.Color == ObjectColor.RED)
								{
									temp.DefaultTarget = new Vector2(mouseState.X, mouseState.Y);
								}
							}
							GameObject.mPotentialTargetListList.NextNode();
						}
						if (GameController.InPlayStructures[1] != null)
							GameController.InPlayStructures[1].StructureAttribute.DefaultUnitTarget = new Vector2(mouseState.X, mouseState.Y);

						break;

					case ObjectColor.YELLOW:

						GameObject.mPotentialTargetListList.Reset();
						for (int i = 0; i < GameObject.mPotentialTargetListList.GetCount(); i++)
						{
							if (GameObject.mPotentialTargetListList.GetCurrent().gameObject.Hostility == Hostility.FRIENDLY &&
								!(GameObject.mPotentialTargetListList.GetCurrent().gameObject is Hero))
							{
								Friendly temp = (Friendly)GameObject.mPotentialTargetListList.GetCurrent().gameObject;

								if (temp.Color == ObjectColor.YELLOW)
								{
									temp.DefaultTarget = new Vector2(mouseState.X, mouseState.Y);
								}
							}
							GameObject.mPotentialTargetListList.NextNode();
						}
						if (GameController.InPlayStructures[2] != null)
							GameController.InPlayStructures[2].StructureAttribute.DefaultUnitTarget = new Vector2(mouseState.X, mouseState.Y);

						break;

					default:
						break;
				}

				RallyTargetState = RallyFetchTarget.INACTIVE;
				objectColor = ObjectColor.NULL;
				HeroActionGroupState = HeroActionButtonGroupState.HERO_ACTION;

			}
		}

		#endregion

		#region Initialize Functions

		private static void InitializeGlobes()
		{
			mHealthGlobeLiquid = new GlobeFrame(graphics, spriteBatch, Content, mHealthGlobePath, mGlobeBoxPath,
										  mHealthGlobeLiquidPath, mHealthOrbLocation, mHero);
			mManaGlobeLiquid = new GlobeFrame(graphics, spriteBatch, Content, mManaGlobePath,
										mGlobeBoxPath, mManaGlobeLiquidPath, mManaOrbLocation, mHero);
		}


		private static void InitializeSpriteFonts()
		{
			mSpriteFont = Content.Load<SpriteFont>("Sprites/Fonts/ConsoleFont");

			mImageFilePaths = new string[5] { "Sprites/Tutorial/Tutorial1", "Sprites/Tutorial/Tutorial2", "Sprites/Tutorial/Tutorial3", "Sprites/Tutorial/Tutorial4", "Sprites/Tutorial/Tutorial5" };

			for (int i = 0; i < mTutorialImages.Length; i++)
			{
				mTutorialImages[i] = Content.Load<Texture2D>(mImageFilePaths[i]);
			}
		}

		private static void InitializeMouseTextures()
		{
			gauntlet = Content.Load<Texture2D>("Sprites/Cursors/Hand_pointer");
			attack = Content.Load<Texture2D>("Sprites/Cursors/Sword");
		}


		private static void InitializeWindows()
		{
			BottomMenuWindow = new WindowFrame(BottomMenuWindowPath, new Rectangle(0, 800, 1600, 100));
			StatusWindow = new WindowFrame(StatusWindowPath, new Rectangle(840, 800, 400, 100));
			TopMenuBar = new WindowFrame(TopMenuBarPath, new Rectangle(0, 0, 1600, 25));
			MainMenuBanner = new WindowFrame(MainMenuBannerPath, new Rectangle(0, 0, 1600, 900));
			TitleSignWindow = new WindowFrame(TitleSignMenuPath, new Rectangle(1070, 30, 400, 210));
			WaveComplete = new WindowFrame(WaveCompleteWindowPath, new Rectangle(700,450,510,319));
		}


		private static void InitializeMenus()
		{
			MainMenu = new Menu(new Rectangle(0, 0, 1600, 900), MainMenuTexturePath, "Audio/Songs/Title Theme", Content, spriteBatch);
		}


		private static void InitializeButtonGroups()
		{

			SaveButtonGroup = new ButtonGroup(new Vector2(500,500), SaveButtonGroupPath, 1.0f, Content, 1,1);
			QuitButtonGroup = new ButtonGroup(new Vector2(500, 500), QuitButtonGroupPath, 1.0f, Content, 1, 1);
			MainMenuButtonGroup = new ButtonGroup(new Vector2(1140, 250), MainMenuButtonGroupPath, 1.0f, Content, 1, 5);
			HeroActionButtonGroup = new ButtonGroup(new Vector2(478, 810), HeroActionButtonGroupPath, 1.0f, Content, 3, 2);
			BuildButtonGroup = new ButtonGroup(new Vector2(478, 810), HeroActionButtonGroupPath, 1.0f, Content, 3, 2);
			BuildButtonGroupPageTwo = new ButtonGroup(new Vector2(478, 810), HeroActionButtonGroupPath, 1.0f, Content, 3, 2);
			UtilityButtonGroup = new ButtonGroup(new Vector2(350, 810), UtilityButtonGroupPath, 1.0f, Content, 1, 2);
			RallyButtonGroup = new ButtonGroup(new Vector2(478, 810), RallyButtonGroupPath, 1.0f, Content, 3, 2);
			StructureButtonGroup = new ButtonGroup(new Vector2(1200, 97), StructureButtonGroupPath, 1.0f, Content, 1, 5);
			PurchaseStructureButtonGroup = new ButtonGroup(new Vector2(478, 810), HeroActionButtonGroupPath, 1.0f, Content, 3, 2);
			PurchaseStrctureButtonGroupPageTwo = new ButtonGroup(new Vector2(478, 810), HeroActionButtonGroupPath, 1.0f, Content, 3, 2);
			SpellButtonGroup = new ButtonGroup(new Vector2(478, 810), HeroActionButtonGroupPath, 1.0f, Content, 2, 2);
			UpgradeUnitButtonGroup1 = new ButtonGroup(new Vector2(478, 810), HeroActionButtonGroupPath, 1.0f, Content, 2, 2);
			UpgradeUnitButtonGroup2 = new ButtonGroup(new Vector2(478, 810), HeroActionButtonGroupPath, 1.0f, Content, 2, 2);
			UpgradeUnitButtonGroup3 = new ButtonGroup(new Vector2(478, 810), HeroActionButtonGroupPath, 1.0f, Content, 2, 2);
			UpgradeUnitButtonGroup4 = new ButtonGroup(new Vector2(478, 810), HeroActionButtonGroupPath, 1.0f, Content, 2, 2);
			UpgradeUnitButtonGroup5 = new ButtonGroup(new Vector2(478, 810), HeroActionButtonGroupPath, 1.0f, Content, 2, 2);
			TutorialNavigationButtonGroup = new ButtonGroup(new Vector2(0, 0), TutorialNavigationButtonGroupPath, 1.0f, Content, 2, 1);
			SettingsButtonGroup = new ButtonGroup(new Vector2(0, 0), SettingsButtonGroupPath, 1.0f, Content, 1, 1);
		}


		private static void InitializeButtons()
		{
			//Initialize Settings Button
			SettingsButton = new Button(Content,ButtonSettingsFunction,UnclickedSettingsPath,UnclickedSettingsPath);

			// Initialize Structure Buttons
			BlueStructureButton = new Button(Content, BlueStructureButtonFunction, BlueStructureButtonPath);
			BlueStructureButton.AddAnimation(new Animation("Sprites/UserInterface/Buttons/RuneButtons/RuneAnimationBlue", 18, 2, 18, 175));
			YellowStructureButton = new Button(Content, YellowStructureButtonFunction, YellowStructureButtonPath);
			YellowStructureButton.AddAnimation(new Animation("Sprites/UserInterface/Buttons/RuneButtons/RuneAnimationYeller", 18, 2, 18, 190));
			RedStructureButton = new Button(Content, RedStructureButtonFunction, RedStructureButtonPath);
			RedStructureButton.AddAnimation(new Animation("Sprites/UserInterface/Buttons/RuneButtons/RuneAnimationRed", 18, 2, 18, 200));
			PurpleStructureButton = new Button(Content, PurpleStructureButtonFunction, PurpleStructureButtonPath);
			PurpleStructureButton.AddAnimation(new Animation("Sprites/UserInterface/Buttons/RuneButtons/RuneAnimationPurple", 18, 2, 18, 160));
			GreenStructureButton = new Button(Content, GreenStructureButtonFunction, GreenStructureButtonPath);
			GreenStructureButton.AddAnimation(new Animation("Sprites/UserInterface/Buttons/RuneButtons/RuneAnimationGreen", 18, 2, 18, 155));


			// Initialize Blank Button.
			BlankButton0 = new Button(Content, BlankButtonFunction, UnclickedBlankButton1Path, ClickedBlankButton1Path);
			BlankButton1 = new Button(Content, BlankButtonFunction, UnclickedBlankButton2Path, ClickedBlankButton2Path);
			BlankButton2 = new Button(Content, BlankButtonFunction, UnclickedBlankButton3Path, ClickedBlankButton3Path);
			BlankButton3 = new Button(Content, BlankButtonFunction, UnclickedBlankButton4Path, ClickedBlankButton4Path);
			BlankButton4 = new Button(Content, BlankButtonFunction, UnclickedBlankButton2Path, ClickedBlankButton2Path);
			BlankButton5 = new Button(Content, BlankButtonFunction, UnclickedBlankButton3Path, ClickedBlankButton3Path);
			BlankButton6 = new Button(Content, BlankButtonFunction, UnclickedBlankButton1Path, ClickedBlankButton1Path);
			BlankButton7 = new Button(Content, BlankButtonFunction, UnclickedBlankButton1Path, ClickedBlankButton1Path);
			BlankButton8 = new Button(Content, BlankButtonFunction, UnclickedBlankButton2Path, ClickedBlankButton2Path);
			BlankButton9 = new Button(Content, BlankButtonFunction, UnclickedBlankButton3Path, ClickedBlankButton3Path);
			BlankButton10 = new Button(Content, BlankButtonFunction, UnclickedBlankButton4Path, ClickedBlankButton4Path);
			BlankButton11 = new Button(Content, BlankButtonFunction, UnclickedBlankButton2Path, ClickedBlankButton2Path);
			BlankButton12 = new Button(Content, BlankButtonFunction, UnclickedBlankButton3Path, ClickedBlankButton3Path);
			BlankButton13 = new Button(Content, BlankButtonFunction, UnclickedBlankButton1Path, ClickedBlankButton1Path);
			BlankButton14 = new Button(Content, BlankButtonFunction, UnclickedBlankButton1Path, ClickedBlankButton1Path);
			BlankButton15 = new Button(Content, BlankButtonFunction, UnclickedBlankButton4Path, ClickedBlankButton1Path);
			BlankButton16 = new Button(Content, BlankButtonFunction, UnclickedBlankButton2Path, ClickedBlankButton4Path);
			BlankButton17 = new Button(Content, BlankButtonFunction, UnclickedBlankButton3Path, ClickedBlankButton2Path);
			BlankButton18 = new Button(Content, BlankButtonFunction, UnclickedBlankButton4Path, ClickedBlankButton1Path);
			BlankButton19 = new Button(Content, BlankButtonFunction, UnclickedBlankButton3Path, ClickedBlankButton1Path);

			// Initialize Main Menu Buttons.
			NewGameButton = new Button(Content, NewGame, UnclickedNewGameButtonPath, ClickedNewGameButtonPath);
			LoadGameButton = new Button(Content, LoadGame, UnClickedLoadGameButtonPath, ClickedLoadGameButtonPath);
			CreditButton = new Button(Content, RunCredits, UnclickedCreditButtonPath, ClickedCreditButtonPath);
			QuitGameButton = new Button(Content, QuitGame, UnclickedQuitButtonPath, ClickedQuitButtonPath);
			TutorialButton = new Button(Content, RunTutorial, UnclickedTutorialButtonPath, ClickedTutorialButtonPath);

			// Initialize Hero Action Buttons.

			BuildButton = new Button(Content, BuildButtonFunction, UnclickedBuildButtonPath, ClickedBuildButtonPath, null, Keys.B);
			SpellsButton = new Button(Content, SpellButtonFunction, UnclickedSpellsButtonPath, ClickedSpellsButtonPath, null, Keys.S);
			RallyButton = new Button(Content, RallyButtonFunction, UnclickedRallyButtonPath, ClickedRallyButtonPath, null, Keys.R);
			PurchaseStructureButton = new Button(Content, PurchaseStructureButtonFunction, UnclickedBuyStructureButtonPath, ClickedBuyStructureButtonPath, null, Keys.U);

			// Initialize Build Buttons.
			BuildDragonButton = new Button(Content, BuildDragonButtonFunction, UnclickedBuildDragonCaveButtonPath, ClickedBuildDragonCaveButtonPath, DisabledBuildDragonCaveButtonPath, Keys.D);
			BuildWolfButton = new Button(Content, BuildWolfButtonFunction, UnclickedBuildWolfPenButtonPath, ClickedBuildWolfPenButtonPath, DisabledBuildWolfPenButtonPath, Keys.W);
			BuildBerzerkerButton = new Button(Content, BuildBerzerkerButtonFunction, UnclickedBuildBarracksButtonPath, ClickedBuildBarracksButtonPath, DisabledBuildBarracksButtonPath, Keys.B);
			BuildAxeThrowerButton = new Button(Content, BuildAxeThrowerButtonFunction, UnclickedBuildArmoryButtonPath, ClickedBuildArmoryButtonPath, DisabledBuildArmoryButtonPath, Keys.A);
			BuildArcaneMageButton = new Button(Content, BuildArcaneMageButtonFunction, UnclickedBuildLibraryButtonPath, ClickedBuildLibraryButtonPath, DisabledBuildFireTempleButtonPath, Keys.R);
			BuildFireMageButton = new Button(Content, BuildFireMageButtonFunction, UnclickedBuildFireTempleButtonPath, ClickedBuildFireTempleButtonPath, DisabledBuildFireTempleButtonPath, Keys.F);
			BuildClericButton = new Button(Content, BuildClericButtonFunction, UnclickedAbbeyButtonPath, ClickedBuildAbbeyButtonPath, DisabledBuildAbbeyButtonPath, Keys.C);
			BuildNecromanceButton = new Button(Content, BuildNecromancerButtonFunction, UnclickedBuildBonePitButtonPath, ClickedBuildBonePitButtonPath, DisabledBuildBonePitnButtonPath, Keys.E);

			// Initialize Utility Buttons.
			BackButton = new Button(Content, BackButtonFunction, UnclickedBackButtonPath, ClickedBackButtonPath, null, Keys.Escape);
			NextButton = new Button(Content, NextButtonFunction, UnclickedNextButtonPath, ClickedNextButtonPath, null, Keys.N);

			// Initialize Rally Buttons.
			RallyRedButton = new Button(Content, RedRallyButtonFunction, UnclickedRedRallyButtonPath, ClickedRedRallyButtonPath, null, Keys.R);
			RallyBlueButton = new Button(Content, BlueRallyButtonFunction, UnclickedBlueRallyButtonPath, ClickedBlueRallyButtonPath, null, Keys.B);
			RallyGreenButton = new Button(Content, GreenRallyButtonFunction, UnclickedGreenRallyButtonPath, ClickedGreenRallyButtonPath, null, Keys.G);
			RallyYellowButton = new Button(Content, YellowRallyButtonFunction, UnclickedYellowRallyButtonPath, ClickedYellowRallyButtonPath, null, Keys.Y);
			RallyPurpleButton = new Button(Content, PurpleRallyButtonFunction, UnclickedPurpleRallyButtonPath, ClickedPurpleRallyButtonPath, null, Keys.P);

			// Initialize Purchase Buttons.
			PurchaseDragonCaveButton = new Button(Content, BuyDragonCaveButtonFunction, UnclickedPurchaseDragonCaveButtonPath, ClickedPurchaseDragonCaveButtonPath, DisabledPurchaseDragonCaveButtonPath, Keys.D);
			PurchaseFireTempleButton = new Button(Content, BuyFireTempleButtonFunction, UnclickedPurchaseFireTempleButtonPath, ClickedPurchaseFireTempleButtonPath, DisabledPurchaseFireTempleButtonPath, Keys.F);
			PurchaseLibraryButton = new Button(Content, BuyLibraryButtonFunction, UnclickedPurchaseLibraryButtonPath, ClickedPurchaseLibraryButtonPath, DisabledPurchaseLibraryButtonPath, Keys.R);
			PurchaseBonePitButton = new Button(Content, BuyBonePitButtonFunction, UnclickedPurchaseBonePitButtonPath, ClickedPurchaseBonePitButtonPath, DisabledPurchaseBonePitnButtonPath, Keys.E);
			PurchaseWolfpenButton = new Button(Content, BuyWolfPenButtonFunction, UnclickedPurchaseWolfPenButtonPath, ClickedPurchaseWolfPenButtonPath, DisabledPurchaseWolfPenButtonPath, Keys.W);
			PurchaseAbbeyButton = new Button(Content, BuyAbbeyButtonFunction, UnclickedPurchaseAbbeyButtonPath, ClickedPurchaseAbbeyButtonPath, DisabledPurchaseAbbeyButtonPath, Keys.C);
			PurchaseArmoryButton = new Button(Content, BuyAmroryButtonFunction, UnclickedPurchaseArmoryButtonPath, ClickedPurchaseArmoryButtonPath, DisabledPurchaseArmoryButtonPath, Keys.A);
			PurchaseBarracksButton = new Button(Content, BuyBarracksButtonFunction, UnclickedPurchaseBarracksButtonPath, ClickedPurchaseBarracksButtonPath, DisabledPurchaseBarracksButtonPath, Keys.B);

			// Upgrade Buttons
			UpgradeAttackStructurePosition1 = new Button(Content, UpgradeAttackButtonFunction1, UnclickedUpgradeAttackButton, ClickedUpgradeAttackButton, DisabledUpgradeAttackButton, Keys.A);
			UpgradeAttackStructurePosition2 = new Button(Content, UpgradeAttackButtonFunction2, UnclickedUpgradeAttackButton, ClickedUpgradeAttackButton, DisabledUpgradeAttackButton, Keys.A);
			UpgradeAttackStructurePosition3 = new Button(Content, UpgradeAttackButtonFunction3, UnclickedUpgradeAttackButton, ClickedUpgradeAttackButton, DisabledUpgradeAttackButton, Keys.A);
			UpgradeAttackStructurePosition4 = new Button(Content, UpgradeAttackButtonFunction4, UnclickedUpgradeAttackButton, ClickedUpgradeAttackButton, DisabledUpgradeAttackButton, Keys.A);
			UpgradeAttackStructurePosition5 = new Button(Content, UpgradeAttackButtonFunction5, UnclickedUpgradeAttackButton, ClickedUpgradeAttackButton, DisabledUpgradeAttackButton, Keys.A);
			UpgradeDefenseStructurePosition1 = new Button(Content, UpgradeDefenseButtonFunction1, UnclickedUpgradeDefenseButton, ClickedUpgradeDefenseButton, DisabledUpgradeDefenseButton, Keys.D);
			UpgradeDefenseStructurePosition2 = new Button(Content, UpgradeDefenseButtonFunction2, UnclickedUpgradeDefenseButton, ClickedUpgradeDefenseButton, DisabledUpgradeDefenseButton, Keys.D);
			UpgradeDefenseStructurePosition3 = new Button(Content, UpgradeDefenseButtonFunction3, UnclickedUpgradeDefenseButton, ClickedUpgradeDefenseButton, DisabledUpgradeDefenseButton, Keys.D);
			UpgradeDefenseStructurePosition4 = new Button(Content, UpgradeDefenseButtonFunction4, UnclickedUpgradeDefenseButton, ClickedUpgradeDefenseButton, DisabledUpgradeDefenseButton, Keys.D);
			UpgradeDefenseStructurePosition5 = new Button(Content, UpgradeDefenseButtonFunction5, UnclickedUpgradeDefenseButton, ClickedUpgradeDefenseButton, DisabledUpgradeDefenseButton, Keys.D);

			// Spell Buttons
			CurseButton = new Button(Content, CurseButtonFunction, UnclickedCurseButtonPath, ClickedCurseButtonPath, DisabledCurseButtonPath, Keys.D);
			BlessingButton = new Button(Content, BlessingButtonFunction, UnclickedBlessingButtonPath, ClickedBlessingButtonPath, DisabledBlessingButtonPath, Keys.I);

			// Tutorial Buttons
			LeftArrowButton = new Button(Content, LeftArrowButtonFunction, ClickedLeftArrowButtonPath, ClickedLeftArrowButtonPath, null, Keys.Left);
			RightArrowButton = new Button(Content, RightArrowButtonFunction, ClickedRightArrowButtonPath, ClickedRightArrowButtonPath, null, Keys.Right);
		QuitButton = new Button(Content, QuitGame, UnclickedQuitButtonPath1, ClickedQuitButtonPath1 );
		SaveButton = new Button(Content, SaveButtonFunciton, UnclickedSaveButtonPath, ClickedSaveButtonPath);
		
		}

		#endregion

		#region Load Content Functions

		private static void LoadGlobes()
		{
			mHealthGlobeLiquid.LoadContent();
			mManaGlobeLiquid.LoadContent();
		}


		private static void LoadWindows()
		{
			BottomMenuWindow.LoadContent(Content);
			StatusWindow.LoadContent(Content);
			TopMenuBar.LoadContent(Content);
			MainMenuBanner.LoadContent(Content);
			TitleSignWindow.LoadContent(Content);
		}

		private static void LoadMenus()
		{
			LoadMainMenu();
		}



		private static void LoadButtonGroups()
		{
			LoadHeroActionButtonGroup();
			LoadBuildButtonGroup();
			LoadBuildButtonGroupPageTwo();
			LoadUtilityButtonGroup();
			LoadStructureButtonGroup();
			LoadRallyButtonGroup();
			LoadPurchaseStructureButtonGroup();
			LoadPurchaseStructureButtonGroupPageTwo();
			LoadUpgradeUnitButtonGroup1();
			LoadUpgradeUnitButtonGroup2();
			LoadUpgradeUnitButtonGroup3();
			LoadUpgradeUnitButtonGroup4();
			LoadUpgradeUnitButtonGroup5();
			LoadSpellButtonGroup();
			LoadSettingsButtonGroup();
			LoadTutorialButtonGroup();
			LoadQuitButtonGroup();
			LoadSaveButtonGroup();
		}

		private static void LoadQuitButtonGroup()
		{
			QuitButtonGroup.LoadContent();
			QuitButtonGroup.AddButton(QuitButton);
		}

		private static void LoadSaveButtonGroup()
		{
			SaveButtonGroup.LoadContent();
			SaveButtonGroup.AddButton(SaveButton);
		}

		private static void LoadTutorialButtonGroup()
		{
			TutorialNavigationButtonGroup.LoadContent();
			TutorialNavigationButtonGroup.AddButton(LeftArrowButton);
			TutorialNavigationButtonGroup.AddButton(RightArrowButton);
		}

		private static void LoadSettingsButtonGroup()
		{
			SettingsButtonGroup.LoadContent();
			SettingsButtonGroup.AddButton(SettingsButton);
		}

		private static void LoadUpgradeUnitButtonGroup5()
		{
			UpgradeUnitButtonGroup5.LoadContent();
			UpgradeUnitButtonGroup5.AddLongButton(UpgradeAttackStructurePosition5);
			UpgradeUnitButtonGroup5.AddButton(BlankButton17);
			UpgradeUnitButtonGroup5.AddLongButton(UpgradeDefenseStructurePosition5);
			UpgradeUnitButtonGroup5.AddButton(BlankButton18);
		}
		private static void LoadUpgradeUnitButtonGroup4()
		{
			UpgradeUnitButtonGroup4.LoadContent();
			UpgradeUnitButtonGroup4.AddLongButton(UpgradeAttackStructurePosition4);
			UpgradeUnitButtonGroup4.AddButton(BlankButton15);
			UpgradeUnitButtonGroup4.AddLongButton(UpgradeDefenseStructurePosition4);
			UpgradeUnitButtonGroup4.AddButton(BlankButton16);
		}
		private static void LoadUpgradeUnitButtonGroup3()
		{
			UpgradeUnitButtonGroup3.LoadContent();
			UpgradeUnitButtonGroup3.AddLongButton(UpgradeAttackStructurePosition3);
			UpgradeUnitButtonGroup3.AddButton(BlankButton13);
			UpgradeUnitButtonGroup3.AddLongButton(UpgradeDefenseStructurePosition3);
			UpgradeUnitButtonGroup3.AddButton(BlankButton14);
		}
		private static void LoadUpgradeUnitButtonGroup2()
		{
			UpgradeUnitButtonGroup2.LoadContent();
			UpgradeUnitButtonGroup2.AddLongButton(UpgradeAttackStructurePosition2);
			UpgradeUnitButtonGroup2.AddButton(BlankButton9);
			UpgradeUnitButtonGroup2.AddLongButton(UpgradeDefenseStructurePosition2);
			UpgradeUnitButtonGroup2.AddButton(BlankButton10);
		}
		private static void LoadUpgradeUnitButtonGroup1()
		{
			UpgradeUnitButtonGroup1.LoadContent();
			UpgradeUnitButtonGroup1.AddLongButton(UpgradeAttackStructurePosition1);
			UpgradeUnitButtonGroup1.AddButton(BlankButton7);
			UpgradeUnitButtonGroup1.AddLongButton(UpgradeDefenseStructurePosition1);
			UpgradeUnitButtonGroup1.AddButton(BlankButton8);
		}

		private static void LoadSpellButtonGroup()
		{
			SpellButtonGroup.LoadContent();
			SpellButtonGroup.AddLongButton(BlessingButton);
			SpellButtonGroup.AddButton(BlankButton11);
			SpellButtonGroup.AddLongButton(CurseButton);
			SpellButtonGroup.AddButton(BlankButton12);

		}

		private static void LoadPurchaseStructureButtonGroup()
		{
			PurchaseStructureButtonGroup.LoadContent();
			PurchaseStructureButtonGroup.AddButton(PurchaseWolfpenButton);
			PurchaseStructureButtonGroup.AddButton(PurchaseBarracksButton);
			PurchaseStructureButtonGroup.AddButton(PurchaseArmoryButton);
			PurchaseStructureButtonGroup.AddButton(PurchaseLibraryButton);
			PurchaseStructureButtonGroup.AddButton(PurchaseAbbeyButton);
			PurchaseStructureButtonGroup.AddButton(NextButton);
		}

		private static void LoadPurchaseStructureButtonGroupPageTwo()
		{
			PurchaseStrctureButtonGroupPageTwo.LoadContent();
			PurchaseStrctureButtonGroupPageTwo.AddButton(PurchaseBonePitButton);
			PurchaseStrctureButtonGroupPageTwo.AddButton(PurchaseFireTempleButton);
			PurchaseStrctureButtonGroupPageTwo.AddButton(PurchaseDragonCaveButton);
			PurchaseStrctureButtonGroupPageTwo.AddButton(BlankButton1);
			PurchaseStrctureButtonGroupPageTwo.AddButton(BlankButton2);
			PurchaseStrctureButtonGroupPageTwo.AddButton(NextButton);
		}

		private static void LoadRallyButtonGroup()
		{
			RallyButtonGroup.LoadContent();
			RallyButtonGroup.AddButton(RallyBlueButton);
			RallyButtonGroup.AddButton(RallyRedButton);
			RallyButtonGroup.AddButton(RallyYellowButton);
			RallyButtonGroup.AddButton(RallyGreenButton);
			RallyButtonGroup.AddButton(RallyPurpleButton);
			RallyButtonGroup.AddButton(BlankButton5);
		}

		private static void LoadStructureButtonGroup()
		{
			StructureButtonGroup.LoadContent();
			StructureButtonGroup.AddButton(BlueStructureButton);
			StructureButtonGroup.AddButton(RedStructureButton);
			StructureButtonGroup.AddButton(YellowStructureButton);
			StructureButtonGroup.AddButton(GreenStructureButton);
			StructureButtonGroup.AddButton(PurpleStructureButton);
		}

		private static void LoadUtilityButtonGroup()
		{
			UtilityButtonGroup.LoadContent();
			UtilityButtonGroup.AddButton(BackButton);
			UtilityButtonGroup.AddButton(BlankButton6);
		}

		private static void LoadBuildButtonGroup()
		{
			BuildButtonGroup.LoadContent();
			BuildButtonGroup.AddButton(BuildWolfButton);
			BuildButtonGroup.AddButton(BuildBerzerkerButton);
			BuildButtonGroup.AddButton(BuildAxeThrowerButton);
			BuildButtonGroup.AddButton(BuildArcaneMageButton);
			BuildButtonGroup.AddButton(BuildClericButton);
			BuildButtonGroup.AddButton(NextButton);

		}

		private static void LoadBuildButtonGroupPageTwo()
		{
			BuildButtonGroupPageTwo.LoadContent();
			BuildButtonGroupPageTwo.AddButton(BuildNecromanceButton);
			BuildButtonGroupPageTwo.AddButton(BuildFireMageButton);
			BuildButtonGroupPageTwo.AddButton(BuildDragonButton);
			BuildButtonGroupPageTwo.AddButton(BlankButton1);
			BuildButtonGroupPageTwo.AddButton(BlankButton0);
			BuildButtonGroupPageTwo.AddButton(NextButton);
		}


		public static void LoadHeroActionButtonGroup()
		{
			HeroActionButtonGroup.LoadContent();
			HeroActionButtonGroup.AddButton(SpellsButton);
			HeroActionButtonGroup.AddButton(BuildButton);
			HeroActionButtonGroup.AddButton(RallyButton);
			HeroActionButtonGroup.AddButton(PurchaseStructureButton);
			HeroActionButtonGroup.AddButton(BlankButton3);
			HeroActionButtonGroup.AddButton(BlankButton4);
		}


		// Loads Content and sets up required object attributes.
		private static void LoadMainMenu()
		{
			MainMenu.LoadContent();
			MainMenuButtonGroup.LoadContent();
			MainMenuButtonGroup.AddButton(NewGameButton);
			MainMenuButtonGroup.AddButton(LoadGameButton);
			MainMenuButtonGroup.AddButton(TutorialButton);
			MainMenuButtonGroup.AddButton(CreditButton);
			MainMenuButtonGroup.AddButton(QuitGameButton);
		}

		#endregion

		#region Update Functions


		internal static void UpdateMouse(GameTime gameTime)
		{
			mouseRectangle = new Rectangle((int)(mouseState.X / GameObject.Scale), (int)(mouseState.Y / GameObject.Scale), 25, 25);
		}



		internal static void UpdateMainMenu(GameTime gameTime)
		{
			if (GameController.gameState == GameState.MAIN_MENU)
			{
				MainMenu.Update(gameTime);
				MainMenuButtonGroup.Update(gameTime);
				TitleSignWindow.Update(gameTime);
			}
			if (mainMenuState == MainMenuState.TUTORIAL)
			{
				TutorialNavigationButtonGroup.Update(gameTime);
			}
		}

		#endregion

		#region Draw Functions


		internal static void DrawMouse(SpriteBatch spriteBatch)
		{
			mouseState = Mouse.GetState();
			Vector2 mousePosition = new Vector2((int)(mouseState.X / GameLoop.Scale), (int)(mouseState.Y / GameLoop.Scale));

			if (cursorState == CursorState.GAUNTLET)
			{
				spriteBatch.Draw(gauntlet, mousePosition, null, Color.White, 0.0f,
								 new Vector2(gauntlet.Width / 2, gauntlet.Height / 2), 0.2f, SpriteEffects.None, 0.0f);
			}

			if (cursorState == CursorState.ATTACK)
			{
				spriteBatch.Draw(attack, mousePosition, null, Color.White, 0.0f,
								 new Vector2(attack.Width / 2, attack.Height / 2), 0.2f, SpriteEffects.None, 0.0f);
			}

		}
		//todo
		internal static void DrawTopMenuBarAttributes()
		{
			float textScale = .25f;
			var heroAttribute = new DebugLib.TextBoxString();
			List<TextBoxString> heroAttributeList = new List<TextBoxString>();
			string emptyString = " ";

			heroAttribute.mStringPosition = new Vector2(TopMenuBar.WindowFrameRectangle.X + mSpriteFont.MeasureString(" ").X + 500, TopMenuBar.WindowFrameRectangle.Y + 5);

			emptyString = "Wave Number: " + WaveController.WaveNumber + "    Castle Damodred: " + mCastle.CastleAttribute.CurrentHealthPoints + "/" + mCastle.CastleAttribute.MaxHealthPoints + "      Exp: " + mHero.HeroAttribute.Experience + "/" + mHero.HeroAttribute.CalculateAmountOfExperienceToLevel() + "      Gold: " + mHero.HeroAttribute.Gold;

			WriteLine(ref heroAttribute, textScale, emptyString, heroAttributeList);
			emptyString = " ";

			foreach (var textBoxString in heroAttributeList)
			{
				spriteBatch.DrawString(mSpriteFont, textBoxString.mStringValue,
				textBoxString.mStringPosition, Color.White, 0.0f, Vector2.Zero, textScale,
				SpriteEffects.None, 0.0f);
			}

		}
		internal static void DrawUpgradeUnitAttributes()
		{
			float textScale = .25f;
			var upgradeUnitAttribute = new DebugLib.TextBoxString();
			List<TextBoxString> gameObjectAttributeList = new List<TextBoxString>();
			string emptyString;

			upgradeUnitAttribute.mStringPosition = new Vector2(StatusWindow.WindowFrameRectangle.X + mSpriteFont.MeasureString(" ").X,
																	 StatusWindow.WindowFrameRectangle.Y + 15);

			emptyString = " ";


			PrintUpgradeUnitAttributes(ref textScale, ref upgradeUnitAttribute, ref gameObjectAttributeList, ref emptyString);

			foreach (var textBoxString in gameObjectAttributeList)
			{
				spriteBatch.DrawString(mSpriteFont, textBoxString.mStringValue,
				textBoxString.mStringPosition, Color.White, 0.0f, Vector2.Zero, textScale,
				SpriteEffects.None, 0.0f);
			}
		}

		internal static void DrawPurchaseStructureStatus()
		{
			float textScale = .25f;
			var purchaseStructureAttribute = new DebugLib.TextBoxString();
			List<TextBoxString> gameObjectAttributeList = new List<TextBoxString>();
			string emptyString;

			purchaseStructureAttribute.mStringPosition = new Vector2(StatusWindow.WindowFrameRectangle.X + mSpriteFont.MeasureString(" ").X,
																	 StatusWindow.WindowFrameRectangle.Y + 15);

			emptyString = " ";


			PrintPurchaseAttributes(ref textScale, ref purchaseStructureAttribute, ref gameObjectAttributeList, ref emptyString);

			foreach (var textBoxString in gameObjectAttributeList)
			{
				spriteBatch.DrawString(mSpriteFont, textBoxString.mStringValue,
				textBoxString.mStringPosition, Color.White, 0.0f, Vector2.Zero, textScale,
				SpriteEffects.None, 0.0f);
			}

			purchaseStructureDrawState = PurhcaseStructureDrawState.INACTIVE;
		}

		internal static void DrawSelectedUnit()
		{
			float textScale = .25f;
			var gameObjectAttribute = new DebugLib.TextBoxString();
			List<TextBoxString> gameObjectAttributeList = new List<TextBoxString>();
			string gameObjectAttributeString = " ";

			gameObjectAttribute.mStringPosition = new Vector2(StatusWindow.WindowFrameRectangle.X +
			mSpriteFont.MeasureString(" ").X, StatusWindow.WindowFrameRectangle.Y +
			15);

			gameObjectAttributeString = " ";


			switch (tempObject.Hostility)
			{
				case Hostility.ENEMY:
					Enemy tempCastEnemy = (Enemy)tempObject;

					PrintEnemyAttributes(ref textScale, ref gameObjectAttribute, ref gameObjectAttributeList,
										 ref gameObjectAttributeString, tempCastEnemy);
					break;

				case Hostility.FRIENDLY:

					if (tempObject is Friendly)
					{
						Friendly tempCastFriendly = (Friendly)tempObject;

						PrintFriendlyAttributes(ref textScale, ref gameObjectAttribute, ref gameObjectAttributeList, ref gameObjectAttributeString, tempCastFriendly);

						// Print Color
						gameObjectAttributeString = "Color: " + tempCastFriendly.Color.ToString();
						WriteLine(ref gameObjectAttribute, textScale, gameObjectAttributeString, gameObjectAttributeList);


					}
					else if (tempObject is Hero)
					{
						Hero tempHero = (Hero)tempObject;
						PrintHeroAttributes(ref textScale, ref gameObjectAttribute, ref gameObjectAttributeList, ref gameObjectAttributeString, tempHero);
					}

					break;

				case Hostility.CASTLE:
					Castle tempCastle = (Castle)tempObject;
					PrintCastleAttributes(ref textScale, ref gameObjectAttribute, ref gameObjectAttributeList, ref gameObjectAttributeString, tempCastle);
					break;

				case Hostility.STRUCTURE:
					Structure tempStructure = (Structure)tempObject;
					PrintStructureAttributes(ref textScale, ref gameObjectAttribute, ref gameObjectAttributeList, ref gameObjectAttributeString, tempStructure);
					break;

				default:

					break;

			}



			foreach (var textBoxString in gameObjectAttributeList)
			{
				spriteBatch.DrawString(mSpriteFont, textBoxString.mStringValue,
				textBoxString.mStringPosition, Color.White, 0.0f, Vector2.Zero, textScale,
				SpriteEffects.None, 0.0f);
			}

		}

		public static void PrintUpgradeUnitAttributes(ref float textScale, ref TextBoxString gameObjectAttribute,
												ref List<TextBoxString> gameObjectAttributeList, ref string gameObjectAttributeString)
		{


			//TODO: unit name, HP, armor, damage


			switch (tempStructure.UnitType)
			{
				case ObjectType.DRAGON:
					tempFriendly = GameController.FriendlyPool[7];
					break;

				case ObjectType.NECROMANCER:
					tempFriendly = GameController.FriendlyPool[5];
					break;

				case ObjectType.ARCANE_MAGE:
					tempFriendly = GameController.FriendlyPool[2];
					break;

				case ObjectType.FIRE_MAGE:
					tempFriendly = GameController.FriendlyPool[6];
					break;

				case ObjectType.AXE_THROWER:
					tempFriendly = GameController.FriendlyPool[3];
					break;

				case ObjectType.BERSERKER:
					tempFriendly = GameController.FriendlyPool[1];
					break;

				case ObjectType.WOLF:
					tempFriendly = GameController.FriendlyPool[0];
					break;

				case ObjectType.CLERIC:
					tempFriendly = GameController.FriendlyPool[4];
					break;

				default:
					throw new ArgumentOutOfRangeException();
			}

			// Print Upgrade Stats
			gameObjectAttributeString = "Unit Type: " + tempStructure.UnitType + "\nHealth Points: " + tempFriendly.FriendlyAttribute.MaxHealthPoints +
										"\nArmor: " + tempFriendly.FriendlyAttribute.CurrentArmor + "\nDamge: "
									   + tempFriendly.FriendlyAttribute.CurrentMinimumDamage + " - " + tempFriendly.FriendlyAttribute.CurrentMaximumDamage;
			WriteLine(ref gameObjectAttribute, textScale, gameObjectAttributeString, gameObjectAttributeList);

			gameObjectAttributeString = " ";

		}

		public static void PrintPurchaseAttributes(ref float textScale, ref TextBoxString gameObjectAttribute,
												ref List<TextBoxString> gameObjectAttributeList, ref string gameObjectAttributeString)
		{
			// Print Armor
			gameObjectAttributeString = "Unit Type: " + tempPurchaseString + "\nGold Cost: " + tempStrucutreAttribute.Cost;
			WriteLine(ref gameObjectAttribute, textScale, gameObjectAttributeString, gameObjectAttributeList);

			gameObjectAttributeString = " ";
		}

		public static void PrintHeroAttributes(ref float textScale, ref TextBoxString gameObjectAttribute,
												ref List<TextBoxString> gameObjectAttributeList, ref string gameObjectAttributeString,
												 Hero tempHero)
		{



			// Print Name
			gameObjectAttributeString = "The Hero";
			WriteLine(ref gameObjectAttribute, textScale, gameObjectAttributeString, gameObjectAttributeList);

			// Print Armor
			gameObjectAttributeString = "Armor: " + tempHero.HeroAttribute.CurrentArmor;
			WriteLine(ref gameObjectAttribute, textScale, gameObjectAttributeString, gameObjectAttributeList);

			//Print Level 
			gameObjectAttributeString = "Level: " + tempHero.HeroAttribute.Level;
			WriteLine(ref gameObjectAttribute, textScale, gameObjectAttributeString, gameObjectAttributeList);

			//Print Damage 
			gameObjectAttributeString = "Damage: " + tempHero.HeroAttribute.CurrentMinimumDamage + " - " + tempHero.HeroAttribute.CurrentMaximumDamage;
			WriteLine(ref gameObjectAttribute, textScale, gameObjectAttributeString, gameObjectAttributeList);
			gameObjectAttributeString = " ";

		}


		public static void PrintStructureAttributes(ref float textScale, ref TextBoxString gameObjectAttribute,
												ref List<TextBoxString> gameObjectAttributeList, ref string gameObjectAttributeString,
												 Structure tempStructure)
		{
			//Print Strucutre Name 
			gameObjectAttributeString = tempStructure.StructureType.ToString();
			WriteLine(ref gameObjectAttribute, textScale, gameObjectAttributeString, gameObjectAttributeList);

			// Print Health
			gameObjectAttributeString = "Health: " + tempStructure.StructureAttribute.CurrentHealthPoints.ToString();
			WriteLine(ref gameObjectAttribute, textScale, gameObjectAttributeString, gameObjectAttributeList);

			// Print Armor
			gameObjectAttributeString = "Armor: " + tempStructure.StructureAttribute.CurrentArmor;
			WriteLine(ref gameObjectAttribute, textScale, gameObjectAttributeString, gameObjectAttributeList);



			gameObjectAttributeString = " ";
		}


		public static void PrintFriendlyAttributes(ref float textScale, ref TextBoxString gameObjectAttribute,
												ref List<TextBoxString> gameObjectAttributeList, ref string gameObjectAttributeString,
												 Friendly tempFriendly)
		{
			// Print Health
			gameObjectAttributeString = "Health: " + tempFriendly.FriendlyAttribute.CurrentHealthPoints.ToString();
			WriteLine(ref gameObjectAttribute, textScale, gameObjectAttributeString, gameObjectAttributeList);

			// Print Armor
			gameObjectAttributeString = "Armor: " + tempFriendly.FriendlyAttribute.CurrentArmor;
			WriteLine(ref gameObjectAttribute, textScale, gameObjectAttributeString, gameObjectAttributeList);

			//Print Sprite Frame 
			gameObjectAttributeString = "Damage: " + tempFriendly.FriendlyAttribute.CurrentMinimumDamage + " - " + tempFriendly.FriendlyAttribute.CurrentMaximumDamage;
			WriteLine(ref gameObjectAttribute, textScale, gameObjectAttributeString, gameObjectAttributeList);

			gameObjectAttributeString = " ";
		}


		public static void PrintCastleAttributes(ref float textScale, ref TextBoxString gameObjectAttribute,
												ref List<TextBoxString> gameObjectAttributeList, ref string gameObjectAttributeString,
												 Castle tempCastle)
		{
			textScale = .5f;
			//Print Name
			gameObjectAttributeString = "Castle Damodred";
			WriteLine(ref gameObjectAttribute, textScale, gameObjectAttributeString, gameObjectAttributeList);

			// Print Health
			gameObjectAttributeString = "Health: " + tempCastle.CastleAttribute.CurrentHealthPoints.ToString();
			WriteLine(ref gameObjectAttribute, textScale, gameObjectAttributeString, gameObjectAttributeList);

			gameObjectAttributeString = " ";
		}


		public static void PrintEnemyAttributes(ref float textScale, ref TextBoxString gameObjectAttribute,
												ref List<TextBoxString> gameObjectAttributeList, ref string gameObjectAttributeString,
												 Enemy tempEnemy)
		{
			// Print Name
			gameObjectAttributeString = tempEnemy.CreatureType.ToString();
			WriteLine(ref gameObjectAttribute, textScale, gameObjectAttributeString, gameObjectAttributeList);

			// Print Health
			gameObjectAttributeString = "Health: " + tempEnemy.EnemyAttribute.CurrentHealthPoints.ToString();
			WriteLine(ref gameObjectAttribute, textScale, gameObjectAttributeString, gameObjectAttributeList);

			// Print Armor
			gameObjectAttributeString = "Armor: " + tempEnemy.EnemyAttribute.CurrentArmor;
			WriteLine(ref gameObjectAttribute, textScale, gameObjectAttributeString, gameObjectAttributeList);


			//Print Damage 
			gameObjectAttributeString = "Damage: " + tempEnemy.EnemyAttribute.BaseMinimumDamage.ToString() + " - " + tempEnemy.EnemyAttribute.BaseMaximumDamage.ToString();
			WriteLine(ref gameObjectAttribute, textScale, gameObjectAttributeString, gameObjectAttributeList);
			gameObjectAttributeString = " ";

		}

		public static void WriteLine(ref TextBoxString gameAttributeText, float mTextScale, string gameAttribute, List<TextBoxString> attributeList)
		{

			try
			{
				gameAttributeText.mStringValue = gameAttribute;
				attributeList.Add(gameAttributeText);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);

			}
			// Move our typed text position to below the debug text.
			gameAttributeText.mStringPosition.Y += mSpriteFont.MeasureString(gameAttributeText.mStringValue).Y *
											mTextScale;
		}

		internal static void DrawMainMenu(SpriteBatch spriteBatch)
		{
			if (GameController.gameState == GameState.MAIN_MENU)
			{

				MainMenu.Draw(spriteBatch);
				MainMenuBanner.Draw(spriteBatch);
				MainMenuButtonGroup.Draw(spriteBatch);
				TitleSignWindow.Draw(spriteBatch);
			}
			if (mainMenuState == MainMenuState.TUTORIAL)
			{
				spriteBatch.Draw(mTutorialImages[tutorialCounter], new Rectangle(0, 0, 1600, 900), Color.White);
				TutorialNavigationButtonGroup.Draw(spriteBatch);
			}
		}

		#endregion

		#region Button Action Functions

		#region Settings Button Function

		internal static void ButtonSettingsFunction()
		{
			
		}

		#endregion


		#region Rally Button Functions

		internal static void RallyButtonFunction()
		{
			AudioController.ButtonClick3SoundEffectInstance.Play();
			HeroActionGroupState = HeroActionButtonGroupState.RALLY;
		}

		internal static void RedRallyButtonFunction()
		{
			AudioController.ButtonClick3SoundEffectInstance.Play();
			RallyTargetState = RallyFetchTarget.ACTIVE;
			objectColor = ObjectColor.RED;
		}

		internal static void BlueRallyButtonFunction()
		{
			AudioController.ButtonClick3SoundEffectInstance.Play();
			RallyTargetState = RallyFetchTarget.ACTIVE;
			objectColor = ObjectColor.BLUE;
		}

		internal static void YellowRallyButtonFunction()
		{
			AudioController.ButtonClick3SoundEffectInstance.Play();
			RallyTargetState = RallyFetchTarget.ACTIVE;
			objectColor = ObjectColor.YELLOW;
		}

		internal static void PurpleRallyButtonFunction()
		{
			AudioController.ButtonClick3SoundEffectInstance.Play();
			RallyTargetState = RallyFetchTarget.ACTIVE;
			objectColor = ObjectColor.PURPLE;
		}

		internal static void GreenRallyButtonFunction()
		{
			AudioController.ButtonClick3SoundEffectInstance.Play();
			RallyTargetState = RallyFetchTarget.ACTIVE;
			objectColor = ObjectColor.GREEN;
		}

		#endregion

		#region Structure Color Button Functions

		internal static void BlueStructureButtonFunction()
		{
			AudioController.BuildingSpawnSoundEffectInstance.Play();
			// Blue   index = 0
			if (SelectedStructureType != ObjectType.NULL && BlueOccupationState != StructureOccupationState.OCCUPIED)
			{
				GameController.InPlayStructures[0] = GameController.StructurePool[(int)SelectedStructureType][0];
				GameController.InPlayStructures[0].Sprite.WorldPosition = BlueStructureButton.Position;
				GameController.InPlayStructures[0].SetObjectID();

				GameObject.mPotentialTargetListList.InsertLast(GameController.InPlayStructures[0]);
				GameObject.mInPlayObjectList.InsertLast(GameController.InPlayStructures[0]);
				GameObject.mPotentialTargetListList.Reset();
				HeroActionGroupState = HeroActionButtonGroupState.HERO_ACTION;
				SelectedStructureType = ObjectType.NULL;
				BlueOccupationState = StructureOccupationState.OCCUPIED;

			}

		}

		internal static void YellowStructureButtonFunction()
		{
			AudioController.BuildingSpawnSoundEffectInstance.Play();
			// Yellow index = 2
			if (SelectedStructureType != ObjectType.NULL && YellowOccupationState != StructureOccupationState.OCCUPIED)
			{
				GameController.InPlayStructures[2] = GameController.StructurePool[(int)SelectedStructureType][2];
				GameController.InPlayStructures[2].Sprite.WorldPosition = YellowStructureButton.Position;
				GameController.InPlayStructures[2].SetObjectID();
				GameObject.mPotentialTargetListList.InsertLast(GameController.InPlayStructures[2]);
				GameObject.mInPlayObjectList.InsertLast(GameController.InPlayStructures[2]);
				GameObject.mPotentialTargetListList.Reset();
				HeroActionGroupState = HeroActionButtonGroupState.HERO_ACTION;
				SelectedStructureType = ObjectType.NULL;
				YellowOccupationState = StructureOccupationState.OCCUPIED;
			}
		}

		internal static void RedStructureButtonFunction()
		{
			AudioController.BuildingSpawnSoundEffectInstance.Play();
			// Red    index = 1
			if (SelectedStructureType != ObjectType.NULL && RedOccupationState != StructureOccupationState.OCCUPIED)
			{
				GameController.InPlayStructures[1] = GameController.StructurePool[(int)SelectedStructureType][1];
				GameController.InPlayStructures[1].Sprite.WorldPosition = RedStructureButton.Position;
				GameObject.mPotentialTargetListList.InsertLast(GameController.InPlayStructures[1]);
				GameObject.mInPlayObjectList.InsertLast(GameController.InPlayStructures[1]);
				GameController.InPlayStructures[1].SetObjectID();
				GameObject.mPotentialTargetListList.Reset();
				HeroActionGroupState = HeroActionButtonGroupState.HERO_ACTION;
				SelectedStructureType = ObjectType.NULL;
				RedOccupationState = StructureOccupationState.OCCUPIED;
			}
		}

		internal static void GreenStructureButtonFunction()
		{
			AudioController.BuildingSpawnSoundEffectInstance.Play();
			// Green  index = 3
			if (SelectedStructureType != ObjectType.NULL && GreenOccupationState != StructureOccupationState.OCCUPIED)
			{
				GameController.InPlayStructures[3] = GameController.StructurePool[(int)SelectedStructureType][3];
				GameController.InPlayStructures[3].Sprite.WorldPosition = GreenStructureButton.Position;
				GameObject.mPotentialTargetListList.InsertLast(GameController.InPlayStructures[3]);
				GameObject.mInPlayObjectList.InsertLast(GameController.InPlayStructures[3]);
				GameController.InPlayStructures[3].SetObjectID();
				GameObject.mPotentialTargetListList.Reset();
				HeroActionGroupState = HeroActionButtonGroupState.HERO_ACTION;
				SelectedStructureType = ObjectType.NULL;
				GreenOccupationState = StructureOccupationState.OCCUPIED;
			}
		}

		internal static void PurpleStructureButtonFunction()
		{
			AudioController.BuildingSpawnSoundEffectInstance.Play();
			// Purple index = 4
			if (SelectedStructureType != ObjectType.NULL && PurpleOccupationState != StructureOccupationState.OCCUPIED)
			{
				GameController.InPlayStructures[4] = GameController.StructurePool[(int)SelectedStructureType][4];
				GameController.InPlayStructures[4].Sprite.WorldPosition = PurpleStructureButton.Position;
				GameController.InPlayStructures[4].SetObjectID();
				GameObject.mPotentialTargetListList.InsertLast(GameController.InPlayStructures[4]);
				GameObject.mInPlayObjectList.InsertLast(GameController.InPlayStructures[4]);
				GameObject.mPotentialTargetListList.Reset();
				HeroActionGroupState = HeroActionButtonGroupState.HERO_ACTION;
				SelectedStructureType = ObjectType.NULL;
				PurpleOccupationState = StructureOccupationState.OCCUPIED;

			}

		}

		#endregion

		#region Utility Button Functions

		internal static void BlankButtonFunction()
		{

		}


		internal static void BackButtonFunction()
		{
			AudioController.ButtonClick3SoundEffectInstance.Play();
			HeroActionGroupState = HeroActionButtonGroupState.HERO_ACTION;
			SelectedStructureType = ObjectType.NULL;
			RallyTargetState = RallyFetchTarget.INACTIVE;
		}

		internal static void NextButtonFunction()
		{
			AudioController.ButtonClick3SoundEffectInstance.Play();
			switch (HeroActionGroupState)
			{
				case HeroActionButtonGroupState.PURCHASE_PAGE_ONE:
					HeroActionGroupState = HeroActionButtonGroupState.PURCHASE_PAGE_TWO;
					break;

				case HeroActionButtonGroupState.PURCHASE_PAGE_TWO:
					HeroActionGroupState = HeroActionButtonGroupState.PURCHASE_PAGE_ONE;
					break;

				case HeroActionButtonGroupState.BUILD_PAGE_ONE:
					HeroActionGroupState = HeroActionButtonGroupState.BUILD_PAGE_TWO;
					break;

				case HeroActionButtonGroupState.BUILD_PAGE_TWO:
					HeroActionGroupState = HeroActionButtonGroupState.BUILD_PAGE_ONE;
					break;

				default:
					break;
			}
		}

		#endregion

		#region Main Menu Button Functions

		internal static void LoadGame()
		{
			GameController.LoadSaveGame();
			AudioController.ButtonClick3SoundEffectInstance.Play();
			GameController.gameState = GameState.ACTIVE;
		}

		internal static void NewGame()
		{
			GameController.gameState = GameState.ACTIVE;
			AudioController.ButtonClick3SoundEffectInstance.Play();
		}


		internal static void RunCredits()
		{
			AudioController.ButtonClick3SoundEffectInstance.Play();
			GameLoop.isRollingCredits = true;
			MainMenu.isRolling = true;
			if (!GameLoop.isRollingCredits)
			{
				MainMenu.isRolling = false;
			}
		}


		internal static void QuitGame()
		{
			GameController.gameState = GameState.GAME_OVER;
			AudioController.ButtonClick3SoundEffectInstance.Play();
		}


		internal static void RunTutorial()
		{
			AudioController.ButtonClick3SoundEffectInstance.Play();
			mainMenuState = MainMenuState.TUTORIAL;
		}

		#endregion

		#region Build Button Functions

		internal static void BuildButtonFunction()
		{
			AudioController.ButtonClick3SoundEffectInstance.Play();
			HeroActionGroupState = HeroActionButtonGroupState.BUILD_PAGE_ONE;
		}

		internal static void BuildClericButtonFunction()
		{
			AudioController.ButtonClick3SoundEffectInstance.Play();
			int counter = 0;
			for (int i = 0; i < GameController.InPlayStructures.Length; i++)
			{
				if (GameController.InPlayStructures[i] != null &&
					GameController.InPlayStructures[i].StructureType == ObjectType.ABBEY)
				{
					counter++;
				}
			}

			// Cleric Struct           index = 7
			if (mHero.HeroAttribute.OwnedAbbey - counter >= 0 && StructureAttribute.CoolDownState == CoolDownState.OFFCOOLDOWN)
			{
				SelectedStructureType = ObjectType.ABBEY;
				StructureAttribute.CoolDownState = CoolDownState.ONCOOLDOWN;
			}
		}

		internal static void BuildNecromancerButtonFunction()
		{
			AudioController.ButtonClick3SoundEffectInstance.Play();
			int counter = 0;
			for (int i = 0; i < GameController.InPlayStructures.Length; i++)
			{
				if (GameController.InPlayStructures[i] != null &&
					GameController.InPlayStructures[i].StructureType == ObjectType.BONEPIT)
				{
					counter++;
				}
			}


			if (mHero.HeroAttribute.OwnedBonePit - counter >= 0 && StructureAttribute.CoolDownState == CoolDownState.OFFCOOLDOWN)
			{
				// Necromancer Shack     index = 1
				SelectedStructureType = ObjectType.BONEPIT;
				StructureAttribute.CoolDownState = CoolDownState.ONCOOLDOWN;
			}
		}


		internal static void BuildFireMageButtonFunction()
		{
			AudioController.ButtonClick3SoundEffectInstance.Play();
			int counter = 0;
			for (int i = 0; i < GameController.InPlayStructures.Length; i++)
			{
				if (GameController.InPlayStructures[i] != null &&
					GameController.InPlayStructures[i].StructureType == ObjectType.FIRE_TEMPLE)
				{
					counter++;
				}
			}

			if (mHero.HeroAttribute.OwnedFireTemple - counter >= 0 && StructureAttribute.CoolDownState == CoolDownState.OFFCOOLDOWN)
			{
				// Fire Mage Struct      index = 3
				SelectedStructureType = ObjectType.FIRE_TEMPLE;
				StructureAttribute.CoolDownState = CoolDownState.ONCOOLDOWN;
			}
		}

		internal static void BuildDragonButtonFunction()
		{
			AudioController.ButtonClick3SoundEffectInstance.Play();
			int counter = 0;
			for (int i = 0; i < GameController.InPlayStructures.Length; i++)
			{
				if (GameController.InPlayStructures[i] != null &&
					GameController.InPlayStructures[i].StructureType == ObjectType.DRAGON_CAVE)
				{
					counter++;
				}
			}

			if (mHero.HeroAttribute.OwnedDragonCaves - counter >= 0 && StructureAttribute.CoolDownState == CoolDownState.OFFCOOLDOWN)
			{
				// Dragon Cave 	      index = 0
				SelectedStructureType = ObjectType.DRAGON_CAVE;
				StructureAttribute.CoolDownState = CoolDownState.ONCOOLDOWN;
			}
		}


		internal static void BuildBerzerkerButtonFunction()
		{
			AudioController.ButtonClick3SoundEffectInstance.Play();
			int counter = 0;
			for (int i = 0; i < GameController.InPlayStructures.Length; i++)
			{
				if (GameController.InPlayStructures[i] != null &&
					GameController.InPlayStructures[i].StructureType == ObjectType.BARRACKS)
				{
					counter++;
				}
			}

			if (mHero.HeroAttribute.OwnedBarracks - counter >= 0 && StructureAttribute.CoolDownState == CoolDownState.OFFCOOLDOWN)
			{
				// Barracks              index = 5
				SelectedStructureType = ObjectType.BARRACKS;
				StructureAttribute.CoolDownState = CoolDownState.ONCOOLDOWN;
			}
		}

		internal static void BuildWolfButtonFunction()
		{
			AudioController.ButtonClick3SoundEffectInstance.Play();
			int counter = 0;
			for (int i = 0; i < GameController.InPlayStructures.Length; i++)
			{
				if (GameController.InPlayStructures[i] != null &&
					GameController.InPlayStructures[i].StructureType == ObjectType.WOLFPEN)
				{
					counter++;
				}
			}

			if (mHero.HeroAttribute.OwnedWolfPens - counter >= 0 && StructureAttribute.CoolDownState == CoolDownState.OFFCOOLDOWN)
			{
				// WolfPen 	             index = 6
				SelectedStructureType = ObjectType.WOLFPEN;
				StructureAttribute.CoolDownState = CoolDownState.ONCOOLDOWN;
			}
		}

		internal static void BuildAxeThrowerButtonFunction()
		{
			AudioController.ButtonClick3SoundEffectInstance.Play();
			int counter = 0;
			for (int i = 0; i < GameController.InPlayStructures.Length; i++)
			{
				if (GameController.InPlayStructures[i] != null &&
					GameController.InPlayStructures[i].StructureType == ObjectType.ARMORY)
				{
					counter++;
				}
			}

			if (mHero.HeroAttribute.OwnedArmories - counter >= 0 && StructureAttribute.CoolDownState == CoolDownState.OFFCOOLDOWN)
			{
				// AxeThrower Struct     index = 4
				SelectedStructureType = ObjectType.ARMORY;
				StructureAttribute.CoolDownState = CoolDownState.ONCOOLDOWN;
			}
		}

		internal static void BuildArcaneMageButtonFunction()
		{
			AudioController.ButtonClick3SoundEffectInstance.Play();
			int counter = 0;
			for (int i = 0; i < GameController.InPlayStructures.Length; i++)
			{
				if (GameController.InPlayStructures[i] != null &&
					GameController.InPlayStructures[i].StructureType == ObjectType.LIBRARY)
				{
					counter++;
				}
			}

			if (mHero.HeroAttribute.OwnedLibraries - counter > 0 && StructureAttribute.CoolDownState == CoolDownState.OFFCOOLDOWN)
			{
				// Arcane Mage Struct    index = 2
				SelectedStructureType = ObjectType.LIBRARY;
				StructureAttribute.CoolDownState = CoolDownState.ONCOOLDOWN;
			}
		}

		#endregion

		#region Buy Button Functions

		internal static void PurchaseStructureButtonFunction()
		{
			AudioController.ButtonClick3SoundEffectInstance.Play();
			HeroActionGroupState = HeroActionButtonGroupState.PURCHASE_PAGE_ONE;
		}

		internal static void BuyDragonCaveButtonFunction()
		{
			AudioController.ButtonClick3SoundEffectInstance.Play();
			if (mHero.HeroAttribute.Gold >= GameController.StructurePool[0][0].StructureAttribute.Cost && mHero.HeroAttribute.OwnedDragonCaves < 6)
			{
				mHero.HeroAttribute.OwnedDragonCaves++;
				mHero.HeroAttribute.Gold -= GameController.StructurePool[0][0].StructureAttribute.Cost;
				HeroActionGroupState = HeroActionButtonGroupState.HERO_ACTION;
			}
		}

		internal static void BuyWolfPenButtonFunction()
		{
			AudioController.ButtonClick3SoundEffectInstance.Play();
			if (mHero.HeroAttribute.Gold >= GameController.StructurePool[6][0].StructureAttribute.Cost && mHero.HeroAttribute.OwnedWolfPens < 6)
			{
				mHero.HeroAttribute.OwnedWolfPens++;
				mHero.HeroAttribute.Gold -= GameController.StructurePool[6][0].StructureAttribute.Cost;
				HeroActionGroupState = HeroActionButtonGroupState.HERO_ACTION;
			}
		}

		internal static void BuyAbbeyButtonFunction()
		{
			AudioController.ButtonClick3SoundEffectInstance.Play();
			if (mHero.HeroAttribute.Gold >= GameController.StructurePool[7][0].StructureAttribute.Cost && mHero.HeroAttribute.OwnedAbbey < 6)
			{
				mHero.HeroAttribute.OwnedAbbey++;
				mHero.HeroAttribute.Gold -= GameController.StructurePool[7][0].StructureAttribute.Cost;
				HeroActionGroupState = HeroActionButtonGroupState.HERO_ACTION;
			}
		}

		internal static void BuyBonePitButtonFunction()
		{
			AudioController.ButtonClick3SoundEffectInstance.Play();
			if (mHero.HeroAttribute.Gold >= GameController.StructurePool[1][0].StructureAttribute.Cost && mHero.HeroAttribute.OwnedAbbey < 6)
			{
				mHero.HeroAttribute.OwnedBonePit++;
				mHero.HeroAttribute.Gold -= GameController.StructurePool[1][0].StructureAttribute.Cost;
				HeroActionGroupState = HeroActionButtonGroupState.HERO_ACTION;
			}
		}

		internal static void BuyFireTempleButtonFunction()
		{
			AudioController.ButtonClick3SoundEffectInstance.Play();
			if (mHero.HeroAttribute.Gold >= GameController.StructurePool[3][0].StructureAttribute.Cost && mHero.HeroAttribute.OwnedFireTemple < 6)
			{
				mHero.HeroAttribute.OwnedFireTemple++;
				mHero.HeroAttribute.Gold -= GameController.StructurePool[3][0].StructureAttribute.Cost;
				HeroActionGroupState = HeroActionButtonGroupState.HERO_ACTION;
			}
		}

		internal static void BuyLibraryButtonFunction()
		{
			AudioController.ButtonClick3SoundEffectInstance.Play();
			if (mHero.HeroAttribute.Gold >= GameController.StructurePool[2][0].StructureAttribute.Cost && mHero.HeroAttribute.OwnedLibraries < 6)
			{
				mHero.HeroAttribute.OwnedLibraries++;
				mHero.HeroAttribute.Gold -= GameController.StructurePool[2][0].StructureAttribute.Cost;
				HeroActionGroupState = HeroActionButtonGroupState.HERO_ACTION;
			}
		}

		internal static void BuyBarracksButtonFunction()
		{
			AudioController.ButtonClick3SoundEffectInstance.Play();
			if (mHero.HeroAttribute.Gold >= GameController.StructurePool[5][0].StructureAttribute.Cost && mHero.HeroAttribute.OwnedBarracks < 6)
			{
				mHero.HeroAttribute.OwnedBarracks++;
				mHero.HeroAttribute.Gold -= GameController.StructurePool[5][0].StructureAttribute.Cost;
				HeroActionGroupState = HeroActionButtonGroupState.HERO_ACTION;
			}
		}

		internal static void BuyAmroryButtonFunction()
		{
			AudioController.ButtonClick3SoundEffectInstance.Play();
			if (mHero.HeroAttribute.Gold >= GameController.StructurePool[4][0].StructureAttribute.Cost && mHero.HeroAttribute.OwnedArmories < 6)
			{
				mHero.HeroAttribute.OwnedArmories++;
				mHero.HeroAttribute.Gold -= GameController.StructurePool[4][0].StructureAttribute.Cost;
				HeroActionGroupState = HeroActionButtonGroupState.HERO_ACTION;
			}
		}

		#endregion

		#region Upgrade Button Functions

		internal static void UpgradeAttackButtonFunction5()
		{
			if (GameController.InPlayStructures[4] != null && GameController.InPlayStructures[4].StructureAttribute.UpgradeAttackCost < mHero.HeroAttribute.Gold)
			{

				AudioController.ButtonClick3SoundEffectInstance.Play();
				GameObject.mPotentialTargetListList.Reset();
				for (int i = 0; i < GameObject.mPotentialTargetListList.GetCount(); i++)
				{
					if (GameObject.mPotentialTargetListList.GetCurrent().gameObject is Friendly)
					{
						if (tempFriendly.CreatureType ==
							GameObject.mPotentialTargetListList.GetCurrent().gameObject.CreatureType)
						{
							Friendly temp = (Friendly)GameObject.mPotentialTargetListList.GetCurrent().gameObject;
							temp.FriendlyAttribute.UpgradeAttack();
						}
					}

					GameObject.mPotentialTargetListList.NextNode();
				}

				GameController.InPlayStructures[4].StructureAttribute.AttackLevel++;

				switch (tempFriendly.CreatureType)
				{

					case ObjectType.DRAGON:
						GameController.FriendlyPool[7].FriendlyAttribute.UpgradeAttack();
						break;
					case ObjectType.NECROMANCER:
						GameController.FriendlyPool[5].FriendlyAttribute.UpgradeAttack();
						break;
					case ObjectType.ARCANE_MAGE:
						GameController.FriendlyPool[2].FriendlyAttribute.UpgradeAttack();
						break;
					case ObjectType.FIRE_MAGE:
						GameController.FriendlyPool[6].FriendlyAttribute.UpgradeAttack();
						break;
					case ObjectType.WOLF:
						GameController.FriendlyPool[0].FriendlyAttribute.UpgradeAttack();
						break;
					case ObjectType.AXE_THROWER:
						GameController.FriendlyPool[3].FriendlyAttribute.UpgradeAttack();
						break;
					case ObjectType.BERSERKER:
						GameController.FriendlyPool[1].FriendlyAttribute.UpgradeAttack();
						break;
					case ObjectType.CLERIC:
						GameController.FriendlyPool[4].FriendlyAttribute.UpgradeAttack();
						break;

					default:
						Console.WriteLine("Upgrade Attack incorrect case");
						break;
				}
				mHero.HeroAttribute.Gold -= GameController.InPlayStructures[4].StructureAttribute.UpgradeAttackCost;
				HeroActionGroupState = HeroActionButtonGroupState.HERO_ACTION;

			}
		}

		internal static void UpgradeAttackButtonFunction4()
		{
			if (GameController.InPlayStructures[3] != null && GameController.InPlayStructures[3].StructureAttribute.UpgradeAttackCost < mHero.HeroAttribute.Gold)
			{

				AudioController.ButtonClick3SoundEffectInstance.Play();
				GameObject.mPotentialTargetListList.Reset();
				for (int i = 0; i < GameObject.mPotentialTargetListList.GetCount(); i++)
				{
					if (GameObject.mPotentialTargetListList.GetCurrent().gameObject is Friendly)
					{
						if (tempFriendly.CreatureType ==
							GameObject.mPotentialTargetListList.GetCurrent().gameObject.CreatureType)
						{
							Friendly temp = (Friendly)GameObject.mPotentialTargetListList.GetCurrent().gameObject;
							temp.FriendlyAttribute.UpgradeAttack();
						}
					}

					GameObject.mPotentialTargetListList.NextNode();
				}

				GameController.InPlayStructures[3].StructureAttribute.AttackLevel++;

				switch (tempFriendly.CreatureType)
				{

					case ObjectType.DRAGON:
						GameController.FriendlyPool[7].FriendlyAttribute.UpgradeAttack();
						break;
					case ObjectType.NECROMANCER:
						GameController.FriendlyPool[5].FriendlyAttribute.UpgradeAttack();
						break;
					case ObjectType.ARCANE_MAGE:
						GameController.FriendlyPool[2].FriendlyAttribute.UpgradeAttack();
						break;
					case ObjectType.FIRE_MAGE:
						GameController.FriendlyPool[6].FriendlyAttribute.UpgradeAttack();
						break;
					case ObjectType.WOLF:
						GameController.FriendlyPool[0].FriendlyAttribute.UpgradeAttack();
						break;
					case ObjectType.AXE_THROWER:
						GameController.FriendlyPool[3].FriendlyAttribute.UpgradeAttack();
						break;
					case ObjectType.BERSERKER:
						GameController.FriendlyPool[1].FriendlyAttribute.UpgradeAttack();
						break;
					case ObjectType.CLERIC:
						GameController.FriendlyPool[4].FriendlyAttribute.UpgradeAttack();
						break;

					default:
						Console.WriteLine("Upgrade Attack incorrect case");
						break;
				}
				mHero.HeroAttribute.Gold -= GameController.InPlayStructures[3].StructureAttribute.UpgradeAttackCost;
				HeroActionGroupState = HeroActionButtonGroupState.HERO_ACTION;

			}
		}

		internal static void UpgradeAttackButtonFunction3()
		{
			if (GameController.InPlayStructures[2] != null && GameController.InPlayStructures[2].StructureAttribute.UpgradeAttackCost < mHero.HeroAttribute.Gold)
			{

				AudioController.ButtonClick3SoundEffectInstance.Play();
				GameObject.mPotentialTargetListList.Reset();
				for (int i = 0; i < GameObject.mPotentialTargetListList.GetCount(); i++)
				{
					if (GameObject.mPotentialTargetListList.GetCurrent().gameObject is Friendly)
					{
						if (tempFriendly.CreatureType ==
							GameObject.mPotentialTargetListList.GetCurrent().gameObject.CreatureType)
						{
							Friendly temp = (Friendly)GameObject.mPotentialTargetListList.GetCurrent().gameObject;
							temp.FriendlyAttribute.UpgradeAttack();
						}
					}

					GameObject.mPotentialTargetListList.NextNode();
				}

				GameController.InPlayStructures[2].StructureAttribute.AttackLevel++;

				switch (tempFriendly.CreatureType)
				{

					case ObjectType.DRAGON:
						GameController.FriendlyPool[7].FriendlyAttribute.UpgradeAttack();
						break;
					case ObjectType.NECROMANCER:
						GameController.FriendlyPool[5].FriendlyAttribute.UpgradeAttack();
						break;
					case ObjectType.ARCANE_MAGE:
						GameController.FriendlyPool[2].FriendlyAttribute.UpgradeAttack();
						break;
					case ObjectType.FIRE_MAGE:
						GameController.FriendlyPool[6].FriendlyAttribute.UpgradeAttack();
						break;
					case ObjectType.WOLF:
						GameController.FriendlyPool[0].FriendlyAttribute.UpgradeAttack();
						break;
					case ObjectType.AXE_THROWER:
						GameController.FriendlyPool[3].FriendlyAttribute.UpgradeAttack();
						break;
					case ObjectType.BERSERKER:
						GameController.FriendlyPool[1].FriendlyAttribute.UpgradeAttack();
						break;
					case ObjectType.CLERIC:
						GameController.FriendlyPool[4].FriendlyAttribute.UpgradeAttack();
						break;

					default:
						Console.WriteLine("Upgrade Attack incorrect case");
						break;
				}
				mHero.HeroAttribute.Gold -= GameController.InPlayStructures[2].StructureAttribute.UpgradeAttackCost;
				HeroActionGroupState = HeroActionButtonGroupState.HERO_ACTION;

			}
		}

		internal static void UpgradeAttackButtonFunction2()
		{
			if (GameController.InPlayStructures[1] != null && GameController.InPlayStructures[1].StructureAttribute.UpgradeAttackCost < mHero.HeroAttribute.Gold)
			{

				AudioController.ButtonClick3SoundEffectInstance.Play();
				GameObject.mPotentialTargetListList.Reset();
				for (int i = 0; i < GameObject.mPotentialTargetListList.GetCount(); i++)
				{
					if (GameObject.mPotentialTargetListList.GetCurrent().gameObject is Friendly)
					{
						if (tempFriendly.CreatureType ==
							GameObject.mPotentialTargetListList.GetCurrent().gameObject.CreatureType)
						{
							Friendly temp = (Friendly)GameObject.mPotentialTargetListList.GetCurrent().gameObject;
							temp.FriendlyAttribute.UpgradeAttack();
						}
					}

					GameObject.mPotentialTargetListList.NextNode();
				}

				GameController.InPlayStructures[1].StructureAttribute.AttackLevel++;

				switch (tempFriendly.CreatureType)
				{

					case ObjectType.DRAGON:
						GameController.FriendlyPool[7].FriendlyAttribute.UpgradeAttack();
						break;
					case ObjectType.NECROMANCER:
						GameController.FriendlyPool[5].FriendlyAttribute.UpgradeAttack();
						break;
					case ObjectType.ARCANE_MAGE:
						GameController.FriendlyPool[2].FriendlyAttribute.UpgradeAttack();
						break;
					case ObjectType.FIRE_MAGE:
						GameController.FriendlyPool[6].FriendlyAttribute.UpgradeAttack();
						break;
					case ObjectType.WOLF:
						GameController.FriendlyPool[0].FriendlyAttribute.UpgradeAttack();
						break;
					case ObjectType.AXE_THROWER:
						GameController.FriendlyPool[3].FriendlyAttribute.UpgradeAttack();
						break;
					case ObjectType.BERSERKER:
						GameController.FriendlyPool[1].FriendlyAttribute.UpgradeAttack();
						break;
					case ObjectType.CLERIC:
						GameController.FriendlyPool[4].FriendlyAttribute.UpgradeAttack();
						break;

					default:
						Console.WriteLine("Upgrade Attack incorrect case");
						break;
				}
				mHero.HeroAttribute.Gold -= GameController.InPlayStructures[1].StructureAttribute.UpgradeAttackCost;
				HeroActionGroupState = HeroActionButtonGroupState.HERO_ACTION;

			}
		}

		internal static void UpgradeAttackButtonFunction1()
		{
			if (GameController.InPlayStructures[0] != null && GameController.InPlayStructures[0].StructureAttribute.UpgradeAttackCost < mHero.HeroAttribute.Gold)
			{

				AudioController.ButtonClick3SoundEffectInstance.Play();
				GameObject.mPotentialTargetListList.Reset();
				for (int i = 0; i < GameObject.mPotentialTargetListList.GetCount(); i++)
				{
					if (GameObject.mPotentialTargetListList.GetCurrent().gameObject is Friendly)
					{
						if (tempFriendly.CreatureType ==
							GameObject.mPotentialTargetListList.GetCurrent().gameObject.CreatureType)
						{
							Friendly temp = (Friendly)GameObject.mPotentialTargetListList.GetCurrent().gameObject;
							temp.FriendlyAttribute.UpgradeAttack();
						}
					}

					GameObject.mPotentialTargetListList.NextNode();
				}

				GameController.InPlayStructures[0].StructureAttribute.AttackLevel++;

				switch (tempFriendly.CreatureType)
				{

					case ObjectType.DRAGON:
						GameController.FriendlyPool[7].FriendlyAttribute.UpgradeAttack();
						break;
					case ObjectType.NECROMANCER:
						GameController.FriendlyPool[5].FriendlyAttribute.UpgradeAttack();
						break;
					case ObjectType.ARCANE_MAGE:
						GameController.FriendlyPool[2].FriendlyAttribute.UpgradeAttack();
						break;
					case ObjectType.FIRE_MAGE:
						GameController.FriendlyPool[6].FriendlyAttribute.UpgradeAttack();
						break;
					case ObjectType.WOLF:
						GameController.FriendlyPool[0].FriendlyAttribute.UpgradeAttack();
						break;
					case ObjectType.AXE_THROWER:
						GameController.FriendlyPool[3].FriendlyAttribute.UpgradeAttack();
						break;
					case ObjectType.BERSERKER:
						GameController.FriendlyPool[1].FriendlyAttribute.UpgradeAttack();
						break;
					case ObjectType.CLERIC:
						GameController.FriendlyPool[4].FriendlyAttribute.UpgradeAttack();
						break;

					default:
						Console.WriteLine("Upgrade Attack incorrect case");
						break;
				}
				mHero.HeroAttribute.Gold -= GameController.InPlayStructures[0].StructureAttribute.UpgradeAttackCost;
				HeroActionGroupState = HeroActionButtonGroupState.HERO_ACTION;

			}
		}


		internal static void UpgradeDefenseButtonFunction5()
		{
			if (GameController.InPlayStructures[4] != null && GameController.InPlayStructures[4].StructureAttribute.UpgradeDefenseCost < mHero.HeroAttribute.Gold)
			{

				AudioController.ButtonClick3SoundEffectInstance.Play();
				GameObject.mPotentialTargetListList.Reset();
				for (int i = 0; i < GameObject.mPotentialTargetListList.GetCount(); i++)
				{
					if (GameObject.mPotentialTargetListList.GetCurrent().gameObject is Friendly)
					{
						if (tempFriendly.CreatureType ==
							GameObject.mPotentialTargetListList.GetCurrent().gameObject.CreatureType)
						{
							Friendly temp = (Friendly)GameObject.mPotentialTargetListList.GetCurrent().gameObject;
							temp.FriendlyAttribute.UpgradeDefense();
						}
					}

					GameObject.mPotentialTargetListList.NextNode();
				}

				GameController.InPlayStructures[4].StructureAttribute.DefenseLevel++;

				switch (tempFriendly.CreatureType)
				{

					case ObjectType.DRAGON:
						GameController.FriendlyPool[7].FriendlyAttribute.UpgradeDefense();
						break;
					case ObjectType.NECROMANCER:
						GameController.FriendlyPool[5].FriendlyAttribute.UpgradeDefense();
						break;
					case ObjectType.ARCANE_MAGE:
						GameController.FriendlyPool[2].FriendlyAttribute.UpgradeDefense();
						break;
					case ObjectType.FIRE_MAGE:
						GameController.FriendlyPool[6].FriendlyAttribute.UpgradeDefense();
						break;
					case ObjectType.WOLF:
						GameController.FriendlyPool[0].FriendlyAttribute.UpgradeDefense();
						break;
					case ObjectType.AXE_THROWER:
						GameController.FriendlyPool[3].FriendlyAttribute.UpgradeDefense();
						break;
					case ObjectType.BERSERKER:
						GameController.FriendlyPool[1].FriendlyAttribute.UpgradeDefense();
						break;
					case ObjectType.CLERIC:
						GameController.FriendlyPool[4].FriendlyAttribute.UpgradeDefense();
						break;

					default:
						Console.WriteLine("Upgrade Attack incorrect case");
						break;
				}
				mHero.HeroAttribute.Gold -= GameController.InPlayStructures[4].StructureAttribute.UpgradeDefenseCost;
				HeroActionGroupState = HeroActionButtonGroupState.HERO_ACTION;

			}
		}

		internal static void UpgradeDefenseButtonFunction4()
		{
			if (GameController.InPlayStructures[3] != null && GameController.InPlayStructures[3].StructureAttribute.UpgradeDefenseCost < mHero.HeroAttribute.Gold)
			{

				AudioController.ButtonClick3SoundEffectInstance.Play();
				GameObject.mPotentialTargetListList.Reset();
				for (int i = 0; i < GameObject.mPotentialTargetListList.GetCount(); i++)
				{
					if (GameObject.mPotentialTargetListList.GetCurrent().gameObject is Friendly)
					{
						if (tempFriendly.CreatureType ==
							GameObject.mPotentialTargetListList.GetCurrent().gameObject.CreatureType)
						{
							Friendly temp = (Friendly)GameObject.mPotentialTargetListList.GetCurrent().gameObject;
							temp.FriendlyAttribute.UpgradeDefense();
						}
					}

					GameObject.mPotentialTargetListList.NextNode();
				}

				GameController.InPlayStructures[3].StructureAttribute.DefenseLevel++;

				switch (tempFriendly.CreatureType)
				{

					case ObjectType.DRAGON:
						GameController.FriendlyPool[7].FriendlyAttribute.UpgradeDefense();
						break;
					case ObjectType.NECROMANCER:
						GameController.FriendlyPool[5].FriendlyAttribute.UpgradeDefense();
						break;
					case ObjectType.ARCANE_MAGE:
						GameController.FriendlyPool[2].FriendlyAttribute.UpgradeDefense();
						break;
					case ObjectType.FIRE_MAGE:
						GameController.FriendlyPool[6].FriendlyAttribute.UpgradeDefense();
						break;
					case ObjectType.WOLF:
						GameController.FriendlyPool[0].FriendlyAttribute.UpgradeDefense();
						break;
					case ObjectType.AXE_THROWER:
						GameController.FriendlyPool[3].FriendlyAttribute.UpgradeDefense();
						break;
					case ObjectType.BERSERKER:
						GameController.FriendlyPool[1].FriendlyAttribute.UpgradeDefense();
						break;
					case ObjectType.CLERIC:
						GameController.FriendlyPool[4].FriendlyAttribute.UpgradeDefense();
						break;

					default:
						Console.WriteLine("Upgrade Attack incorrect case");
						break;
				}
				mHero.HeroAttribute.Gold -= GameController.InPlayStructures[3].StructureAttribute.UpgradeDefenseCost;
				HeroActionGroupState = HeroActionButtonGroupState.HERO_ACTION;

			}
		}

		internal static void UpgradeDefenseButtonFunction3()
		{
			if (GameController.InPlayStructures[2] != null && GameController.InPlayStructures[2].StructureAttribute.UpgradeDefenseCost < mHero.HeroAttribute.Gold)
			{

				AudioController.ButtonClick3SoundEffectInstance.Play();
				GameObject.mPotentialTargetListList.Reset();
				for (int i = 0; i < GameObject.mPotentialTargetListList.GetCount(); i++)
				{
					if (GameObject.mPotentialTargetListList.GetCurrent().gameObject is Friendly)
					{
						if (tempFriendly.CreatureType ==
							GameObject.mPotentialTargetListList.GetCurrent().gameObject.CreatureType)
						{
							Friendly temp = (Friendly)GameObject.mPotentialTargetListList.GetCurrent().gameObject;
							temp.FriendlyAttribute.UpgradeDefense();
						}
					}

					GameObject.mPotentialTargetListList.NextNode();
				}

				GameController.InPlayStructures[2].StructureAttribute.DefenseLevel++;

				switch (tempFriendly.CreatureType)
				{

					case ObjectType.DRAGON:
						GameController.FriendlyPool[7].FriendlyAttribute.UpgradeDefense();
						break;
					case ObjectType.NECROMANCER:
						GameController.FriendlyPool[5].FriendlyAttribute.UpgradeDefense();
						break;
					case ObjectType.ARCANE_MAGE:
						GameController.FriendlyPool[2].FriendlyAttribute.UpgradeDefense();
						break;
					case ObjectType.FIRE_MAGE:
						GameController.FriendlyPool[6].FriendlyAttribute.UpgradeDefense();
						break;
					case ObjectType.WOLF:
						GameController.FriendlyPool[0].FriendlyAttribute.UpgradeDefense();
						break;
					case ObjectType.AXE_THROWER:
						GameController.FriendlyPool[3].FriendlyAttribute.UpgradeDefense();
						break;
					case ObjectType.BERSERKER:
						GameController.FriendlyPool[1].FriendlyAttribute.UpgradeDefense();
						break;
					case ObjectType.CLERIC:
						GameController.FriendlyPool[4].FriendlyAttribute.UpgradeDefense();
						break;

					default:
						Console.WriteLine("Upgrade Attack incorrect case");
						break;
				}
				mHero.HeroAttribute.Gold -= GameController.InPlayStructures[2].StructureAttribute.UpgradeDefenseCost;
				HeroActionGroupState = HeroActionButtonGroupState.HERO_ACTION;

			}
		}

		internal static void UpgradeDefenseButtonFunction2()
		{
			if (GameController.InPlayStructures[1] != null && GameController.InPlayStructures[1].StructureAttribute.UpgradeDefenseCost < mHero.HeroAttribute.Gold)
			{

				AudioController.ButtonClick3SoundEffectInstance.Play();
				GameObject.mPotentialTargetListList.Reset();
				for (int i = 0; i < GameObject.mPotentialTargetListList.GetCount(); i++)
				{
					if (GameObject.mPotentialTargetListList.GetCurrent().gameObject is Friendly)
					{
						if (tempFriendly.CreatureType ==
							GameObject.mPotentialTargetListList.GetCurrent().gameObject.CreatureType)
						{
							Friendly temp = (Friendly)GameObject.mPotentialTargetListList.GetCurrent().gameObject;
							temp.FriendlyAttribute.UpgradeDefense();
						}
					}

					GameObject.mPotentialTargetListList.NextNode();
				}

				GameController.InPlayStructures[1].StructureAttribute.DefenseLevel++;

				switch (tempFriendly.CreatureType)
				{

					case ObjectType.DRAGON:
						GameController.FriendlyPool[7].FriendlyAttribute.UpgradeDefense();
						break;
					case ObjectType.NECROMANCER:
						GameController.FriendlyPool[5].FriendlyAttribute.UpgradeDefense();
						break;
					case ObjectType.ARCANE_MAGE:
						GameController.FriendlyPool[2].FriendlyAttribute.UpgradeDefense();
						break;
					case ObjectType.FIRE_MAGE:
						GameController.FriendlyPool[6].FriendlyAttribute.UpgradeDefense();
						break;
					case ObjectType.WOLF:
						GameController.FriendlyPool[0].FriendlyAttribute.UpgradeDefense();
						break;
					case ObjectType.AXE_THROWER:
						GameController.FriendlyPool[3].FriendlyAttribute.UpgradeDefense();
						break;
					case ObjectType.BERSERKER:
						GameController.FriendlyPool[1].FriendlyAttribute.UpgradeDefense();
						break;
					case ObjectType.CLERIC:
						GameController.FriendlyPool[4].FriendlyAttribute.UpgradeDefense();
						break;

					default:
						Console.WriteLine("Upgrade Attack incorrect case");
						break;
				}
				mHero.HeroAttribute.Gold -= GameController.InPlayStructures[1].StructureAttribute.UpgradeDefenseCost;
				HeroActionGroupState = HeroActionButtonGroupState.HERO_ACTION;

			}
		}

		internal static void UpgradeDefenseButtonFunction1()
		{
			if (GameController.InPlayStructures[0] != null && GameController.InPlayStructures[0].StructureAttribute.UpgradeDefenseCost < mHero.HeroAttribute.Gold)
			{

				AudioController.ButtonClick3SoundEffectInstance.Play();
				GameObject.mPotentialTargetListList.Reset();
				for (int i = 0; i < GameObject.mPotentialTargetListList.GetCount(); i++)
				{
					if (GameObject.mPotentialTargetListList.GetCurrent().gameObject is Friendly)
					{
						if (tempFriendly.CreatureType ==
							GameObject.mPotentialTargetListList.GetCurrent().gameObject.CreatureType)
						{
							Friendly temp = (Friendly)GameObject.mPotentialTargetListList.GetCurrent().gameObject;
							temp.FriendlyAttribute.UpgradeDefense();
						}
					}

					GameObject.mPotentialTargetListList.NextNode();
				}

				GameController.InPlayStructures[0].StructureAttribute.DefenseLevel++;

				switch (tempFriendly.CreatureType)
				{

					case ObjectType.DRAGON:
						GameController.FriendlyPool[7].FriendlyAttribute.UpgradeDefense();
						break;
					case ObjectType.NECROMANCER:
						GameController.FriendlyPool[5].FriendlyAttribute.UpgradeDefense();
						break;
					case ObjectType.ARCANE_MAGE:
						GameController.FriendlyPool[2].FriendlyAttribute.UpgradeDefense();
						break;
					case ObjectType.FIRE_MAGE:
						GameController.FriendlyPool[6].FriendlyAttribute.UpgradeDefense();
						break;
					case ObjectType.WOLF:
						GameController.FriendlyPool[0].FriendlyAttribute.UpgradeDefense();
						break;
					case ObjectType.AXE_THROWER:
						GameController.FriendlyPool[3].FriendlyAttribute.UpgradeDefense();
						break;
					case ObjectType.BERSERKER:
						GameController.FriendlyPool[1].FriendlyAttribute.UpgradeDefense();
						break;
					case ObjectType.CLERIC:
						GameController.FriendlyPool[4].FriendlyAttribute.UpgradeDefense();
						break;

					default:
						Console.WriteLine("Upgrade Attack incorrect case");
						break;
				}
				mHero.HeroAttribute.Gold -= GameController.InPlayStructures[0].StructureAttribute.UpgradeDefenseCost;
				HeroActionGroupState = HeroActionButtonGroupState.HERO_ACTION;

			}
		}


		#endregion

		#region Spell Button Functions

		internal static void SpellButtonFunction()
		{
			AudioController.ButtonClick3SoundEffectInstance.Play();
			HeroActionGroupState = HeroActionButtonGroupState.SPELLS;
		}

		internal static void CurseButtonFunction()
		{
			AudioController.ButtonClick3SoundEffectInstance.Play();
			if (mHero.HeroAttribute.Mana >= 15 && !mHero.BehaviorHero.IsCasting)
			{
				HeroAttribute.IntimidateSpellState = IntimidateSpellState.ACTIVE;
				EnemyAttribute.IntimidateSpellState = IntimidateSpellState.ACTIVE;
				mHero.HeroAttribute.Mana -= 15;
			}

		}

		internal static void BlessingButtonFunction()
		{
			AudioController.ButtonClick3SoundEffectInstance.Play();
			if (mHero.HeroAttribute.Mana >= 15 && !mHero.BehaviorHero.IsCasting)
			{
				HeroAttribute.BattleCrySpellState = BattleCrySpellState.ACTIVE;
				FriendlyAttribute.BattleCrySpellState = BattleCrySpellState.ACTIVE;
				mHero.HeroAttribute.Mana -= 15;
			}

		}

		#endregion

		#region Tutorial Button Functions

		internal static void LeftArrowButtonFunction()
		{
			AudioController.ButtonClick3SoundEffectInstance.Play();
			if (tutorialCounter > 0)
			{
				tutorialCounter--;
			}
		}

		internal static void RightArrowButtonFunction()
		{
			AudioController.ButtonClick3SoundEffectInstance.Play();
			if (tutorialCounter == mTutorialImages.Length - 1)
			{
				GameController.gameState = GameState.MAIN_MENU;
				mainMenuState = MainMenuState.NONE;
				tutorialCounter = 0;
			}

			else
			{
				tutorialCounter++;
			}
		}

		#endregion

		internal static void SaveButtonFunciton()
		{
			AudioController.ButtonClick3SoundEffectInstance.Play();
			GameController.SaveGame();
		}



		#endregion



	}
}