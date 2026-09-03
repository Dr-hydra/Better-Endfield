local UI_ASSETS_PATH = "Assets/Beyond/DynamicAssets/Gameplay/UI/"
local UI_INIT_ASSETS_PATH = "Assets/Beyond/InitialAssets/UI/"
local UI_ASSETS_DEV_PATH = "Assets/BeyondDev/DynamicAssets/Gameplay/UI/"
local UI_PANEL_PREFAB_FORMAT = "Prefabs/%s/%sPanel.prefab"
local UI_PC_PANEL_PREFAB_FORMAT = "Prefabs/%s/%sPanel_PC.prefab"
local UI_CONTROLLER_PANEL_PREFAB_FORMAT = "Prefabs/%s/%sPanel_Controller.prefab"

UI_NODE_PREFAB_PATH = UI_ASSETS_PATH .. "Prefabs/UINode.prefab"
UI_RED_DOT_ANIMATION_PATH = UI_ASSETS_PATH .. "Prefabs/RedDotAnimation.prefab"
UI_BUSINESS_CARD_PREFAB_PATH = UI_ASSETS_PATH .. "Prefabs/Friend/FriendBusinessCard/%s.prefab"
UI_WATCH_BUSINESS_CARD_PREFAB_PATH = UI_ASSETS_PATH .. "Prefabs/Common/Widgets/BusinessCard/%s.prefab"
UI_ACTIVITY_CHAR_GUIDE_LINE_PREFAB_PATH = UI_ASSETS_PATH .. "Prefabs/Activity/CharacterGuideLine/%s.prefab"
UI_ACTIVITY_VERSION_GUIDE_PREFAB_PATH = UI_ASSETS_PATH .. "Prefabs/Activity/VersionGuide/%s.prefab"
UI_ACTIVITY_CHECK_IN_PREFAB_PATH = UI_ASSETS_PATH .. "Prefabs/Activity/Checkin/%s.prefab"
UI_ACTIVITY_HIGH_DIFFICULTY_BG_PREFAB_PATH = UI_ASSETS_PATH .. "Prefabs/Activity/HighDifficultyBg/%s.prefab"
UI_ACTIVITY_CHALLENGE_DUNGEON_BG_PREFAB_PATH = UI_ASSETS_PATH .. "Prefabs/Activity/ChallengeDungeonBg/%s.prefab"
UI_ACTIVITY_ARKNIGHTS_BIRTH_PREFAB_PATH = UI_ASSETS_PATH .. "Prefabs/Activity/ArknightsBirth/%s.prefab"
UI_DUMMY_NAVI_LAYER_PREFAB_PATH = UI_ASSETS_PATH .. "Prefabs/UIDummyNaviLayer.prefab"

UI_PANEL_PREFAB_PATH = UI_ASSETS_PATH .. UI_PANEL_PREFAB_FORMAT
UI_PC_PANEL_PREFAB_PATH = UI_ASSETS_PATH .. UI_PC_PANEL_PREFAB_FORMAT
UI_CONTROLLER_PANEL_PREFAB_PATH = UI_ASSETS_PATH .. UI_CONTROLLER_PANEL_PREFAB_FORMAT

UI_COMMON_SHARE_LOGO_SPRITE_PATH = "Snapshot"

UI_PANEL_PREFAB_DEV_PATH = UI_ASSETS_DEV_PATH .. UI_PANEL_PREFAB_FORMAT
UI_PC_PANEL_PREFAB_DEV_PATH = UI_ASSETS_DEV_PATH .. UI_PC_PANEL_PREFAB_FORMAT
UI_CONTROLLER_PANEL_PREFAB_DEV_PATH = UI_ASSETS_DEV_PATH .. UI_CONTROLLER_PANEL_PREFAB_FORMAT

UI_ROOT_PREFAB_PATH = UI_ASSETS_PATH .. "Prefabs/%s/%sRoot.prefab"
UI_PANEL_CTRL_FILE_PATH = "UI/Panels/%s/%sCtrl"
UI_PANEL_MODEL_FILE_PATH = "UI/Panels/%s/%sModel"
UI_BACKGROUND_MESSAGE_PATH = "Common/Core/BackgroundMessage"
UI_SNS_DIALOG_CONTENT_WIDGETS_PATH = UI_ASSETS_PATH .. "Prefabs/SNS/Widgets/SNSContent%s.prefab"
UI_SNS_FRIEND_CHAT_WIDGETS_PATH = UI_ASSETS_PATH .. "Prefabs/SNS/Widgets/%s.prefab"
UI_DEFAULT_I18N_FONT_ASSET_PATH = UI_INIT_ASSETS_PATH .. "Fonts/DefaultFont_I18N.asset"
UI_COMMON_TASK_TRACK_TOAST_WIDGETS_PATH = UI_ASSETS_PATH .. "Prefabs/CommonTaskTrack/Widgets/%s.prefab"
UI_CASH_SHOP_DYNAMIC_GIFT_PANEL_WIDGETS_PATH = UI_ASSETS_PATH .. "Prefabs/CashShop/Widgets/SeasonalGiftpackNode/%s.prefab"
UI_CASH_SHOP_CUSTOM_GIFT_PANEL_WIDGETS_PATH = UI_ASSETS_PATH .. "Prefabs/CashShop/Widgets/UniversalGiftpackNode/%s.prefab"


CANVAS_DEFAULT_WIDTH = CS.Beyond.UI.CUR_STANDARD_HORIZONTAL_RESOLUTION
CANVAS_DEFAULT_HEIGHT = CS.Beyond.UI.CUR_STANDARD_VERTICAL_RESOLUTION

UI_SPRITE_PATH = "Assets/Beyond/DynamicAssets/Gameplay/UI/Sprites/%s.png"
UI_SPRITE_DEV_PATH = "Assets/BeyondDev/DynamicAssets/Gameplay/UI/Sprites/%s.png"
UI_SPRITE_DEVELOP_RETURN_TAB_PATH = "Activity/DevelopReturn"
UI_SPRITE_COMMON = "Common"
UI_SPRITE_ITEM = "ItemIcon"
UI_SPRITE_ITEM_BIG = "ItemIconBig"
UI_SPRITE_ITEM_COMPOSITE_DECO = "ItemIconCompositeDeco"
UI_SPRITE_ITEM_COMPOSITE_DECO_BIG = "ItemIconCompositeDecoBig"
UI_SPRITE_ITEM_TIPS = "ItemTips"
UI_SPRITE_CHECK_IN_DATE_IMAGE_PATH = "Activity/ActivityCheckBG"
UI_SPRITE_EQUIPMENT_LOGO = "EquipmentLogo"
UI_SPRITE_EQUIPMENT_LOGO_BIG = "EquipmentLogoBig"
UI_SPRITE_EQUIPMENT_LOGO_BIG_WHITE = "EquipmentLogoBigWhite"
UI_SPRITE_SKILL_ICON = "SkillIcon"
UI_SPRITE_CHAR_HEAD = "CharRoundIcon"
UI_SPRITE_CHAR_HEAD_RECTANGLE = "CharIcon"
UI_SPRITE_CHAR_PORTRAIT = "CharacterPortrait"
UI_SPRITE_ITEM_DEFAULT_ICON = "item_default"
UI_SPRITE_ACTIVITY = "Activity"
UI_SPRITE_ACTIVITY_BENEFITS = "Activity/RewardShow"
UI_SPRITE_ACTIVITY_HIGH_DIFFICULTY = "Activity/HighDifficulty"
UI_SPRITE_ACTIVITY_PUSH_BG = "Activity/PushBg"
UI_SPRITE_HIGH_DIFFICULTY = "HighDifficulty"
UI_CHAR_TALENT_ICON = "CharTalent"
UI_SPRITE_MINIGAME_BALLOON = "BalloonMinigame"

UI_SPRITE_GACHA = "Gacha"
UI_SPRITE_GACHA_CHAR_SHADOW = "GachaShadow"
UI_SPRITE_GACHA_CHAR = "CharGacha"
UI_SPRITE_GACHA_POOL = "GachaPool"
UI_SPRITE_GACHA_WEAPON = "GachaWeapon"

UI_VIDEO_ACTIVITY = "Activity/"
UI_VIDEO_SKIP_CHAPTER = "SkipChapter/"
UI_VIDEO_GACHA = "Gacha/"
UI_VIDEO_SNAPSHOT = "Snapshot/"

UI_SPRITE_AI_BARK_CHAR_HEAD = "AiBark/AiBarkCharicon"
UI_SPRITE_VIDEO_COVER = "VideoCover"
UI_SPRITE_REMOTE_COMM_BG = "RemoteComm/BG"
UI_SPRITE_CHAR_PROFESSION = "CharProfessionIcon"
UI_SPRITE_CHAR_ELEMENT = "ElementIcon"
UI_SPRITE_CHAR_INFO = "CharInfo"
UI_SPRITE_WEAPON_EXHIBIT = "WeaponExhibit"
UI_SPRITE_WIKI_WEAPON_IMAGE = "Wiki/WeaponImage"
UI_SPRITE_PRTS = "PRTS"
UI_SPRITE_PRTS_ICON = "PRTS/Icon"
UI_SPRITE_SKETCH = "Sketch"
UI_SPRITE_WIKI_MONSTER = "Wiki/MonsterImage"
UI_SPRITE_CROP = "Factory/Crop"
UI_SPRITE_FAC_GAS = "Factory/Gas"
UI_SPRITE_SS_SKILL_ICON = "Spaceship/SpaceshipSkillIcon"
UI_SPRITE_SS_CLUE_ICON = "Spaceship/SpaceshipGuestroom"
UI_SPRITE_SS_COMMON = "Spaceship/SpaceshipCommon"
UI_SPRITE_SNS = "SNS"
UI_SPRITE_SNS_VIDEO_PREVIEW = "SNS/VideoPreview"
UI_SPRITE_SNS_STICKER = "SNS/Sticker"
UI_SPRITE_SNS_PRTS_PREVIEW = "SNS/PRTSPreview"
UI_SPRITE_SNS_PICTURE = "SNS/Picture"
UI_SPRITE_SNS_EMOJI = "SNS/Emoji"
UI_SPRITE_GENERAL_ABILITY = "GeneralAbility"
UI_SPRITE_MINI_POWER = "Factory/MiniPower"
UI_SPRITE_FAC_TRANS = "Factory/DomainItemTransfer"
UI_SPRITE_COMMON_TASK_TRACK = "CommonTaskTrack"
UI_SPRITE_COMMON_POI_UPGRADE_TOAST = "POI/Toast"

UI_MINIGAME_PUZZLE_GREY_BLOCK_SUFFIX = "_block"

UI_SPRITE_DUNGEON = "Dungeon"
UI_SPRITE_SEASONTOWER = "SeasonTower"

UI_SPRITE_BLUEPRINT = "FacBlueprint"

UI_SPRITE_DOMAIN_DEPOT_UPGRADE = "Shop/ShopMarketTabIconSmall"
UI_SPRITE_DOMAIN_DEPOT_INST = "DomainDepot/DepotImage"
UI_SPRITE_DOMAIN_DEPOT_BG_MONEY_ICON = "DomainDepot/DomainBgMoneyIcon"

UI_SPRITE_HOR_CHAR_HEAD = "CharHorHeadIcon"
UI_SPRITE_SQUARE_CHAR_HEAD = "CharBattleIcon"
UI_SPRITE_ROUND_CHAR_HEAD = "CharRoundIcon"
UI_SPRITE_ATTRIBUTE_ICON = "AttributeIcon"
UI_SPRITE_EQUIP = "Equip"
UI_SPRITE_EQUIP_SUIT = "Suit"
UI_CHAR_HEAD_PREFIX = "icon_round_"
UI_CHAR_HEAD_SQUARE_PREFIX = "icon_"
UI_CHAR_REMOTE_ICON_PREFIX = "icon_"

UI_CHAR_PROFESSION_SMALL_SUFFIX = "_s"
UI_CHAR_ELEMENT_PREFIX = "icon_element_"
UI_CHAR_ELEMENT_WHITE_PREFIX = "icon_element_white_"

UI_AI_BARK_CHAR_HEAD_PREFIX = "aibark"

UI_ROUND_CHAR_HEAD_PREFIX = "icon_round_"
UI_ATTRIBUTE_ICON_PREFIX = "icon_attribute_"
UI_ATTRIBUTE_ICON_BIG_PREFIX = "icon_attribute_big_"

UI_EQUIP_PART_ICON_PREFIX = "icon_equipmenttype_0"
UI_CHAR_INFO_CHAR_BG_PREFIX = "bg_charinfo_"
UI_SPRITE_MAP_ICON = "Map"
UI_SPRITE_MAP_MARK_ICON = "Map/MarkIcon"
UI_SPRITE_MAP_MARK_ICON_SMALL = "Map/MarkIconSmall"
UI_SPRITE_MAP_MARK_ICON_CUSTOM = "Map/MarkIconCustom"
UI_SPRITE_MAP_DETAIL_BTN_ICON = "Map/BtnIcon"
UI_SPRITE_GUIDE = "Guide"
UI_SPRITE_LIMITED_GUIDE = "LimitedGuide"
UI_SPRITE_HEAD_LABEL_ICON = "HeadLabelIcon"
UI_SPRITE_CURRENCY_ICON = "MoneyIcon"
UI_SPRITE_WIKI_ICON = "Wiki"
UI_SPRITE_LEVEL_COLLECTION = "LevelCollection"
UI_SPRITE_REWARDS = "Rewards"
UI_SPRITE_INTERACT_OPTION_ICON = "InteractOptionIcon"

UI_SPRITE_MISSION_ICON = "MissionIcon"
UI_SPRITE_MISSION_TYPE_ICON = "Mission/TypeIcon"
UI_SPRITE_MISSION_TITLE_BACKGROUND = "Mission/TitleBackground"

UI_SPRITE_GAME_SETTING = "GameSetting"

UI_SPRITE_CHAR_IMAGE_510 = "CharImg510"
UI_SPRITE_CHAR_REMOTE_ICON = "CharRemoteIcon"
UI_SPRITE_CHAR_REMOTE_ICON_700 = "CharRemoteIcon700"

UI_SPRITE_HIDE_LABEL_STATE_ICON = "HeadLabelIcon"

UI_SPRITE_SETTLEMENT_DEFENSE_MAP = "Settlement/DefenseMap"
UI_SPRITE_SETTLEMENT_DEFENSE_DETAIL = "Settlement/DefenseDetail"
UI_SPRITE_SETTLEMENT_DETAIL_LEVEL = "Settlement/SettlementPic"
UI_SPRITE_SETTLEMENT_ICON_BIG = "Settlement/IconBig"
UI_SPRITE_SETTLEMENT = "Settlement"
UI_SPRITE_SETTLEMENT_KITE_STATION = "Settlement/KiteStation"

UI_SPRITE_DOMAIN = "Domain"

UI_SPRITE_HEAD_FRAME = "HeadFrameIcon"
UI_SPRITE_HEAD = UI_SPRITE_CHAR_REMOTE_ICON

UI_SPRITE_WIKI_GROUP = "Wiki/GroupIcon"

PLAYER_NAME_FORMATTER = "{player}"

UI_MANUALCRAFT_ICON_ID = "icon_tips_manualcraft"

UI_LOADING_BG = "Loading"

UI_READING_POPUP_LOGO = "ReadingPopLogo"

UI_SPRITE_BATTLE_PASS = "BattlePass"

UI_SPRITE_BATTLE_PASS_PLAN = "BattlePass/BattlePassPlan"


UI_SPRITE_SNAPSHOT = "Snapshot"
UI_SPRITE_SNAPSHOT_FILTER = "Snapshot/Filter"
UI_SPRITE_SNAPSHOT_STICKER = "Snapshot/Sticker"
UI_SPRITE_SNAPSHOT_CHALLENGE = "Snapshot/Challenge"
UI_SPRITE_SNAPSHOT_ACTION = "Snapshot/Action"
SNAPSHOT_FILTER_VOLUME_PATH = "Assets/Beyond/DynamicAssets/Gameplay/Snapshot/Filter/Volume/%s.prefab"


POSTER_TEXTURE_PATH = "Assets/Beyond/DynamicAssets/Gameplay/UI/Textures/SpaceShip/ImagePoster/LargeSize/%s.png"
POSTER_TEXTURE_SUB_SIZE_PATH = "Assets/Beyond/DynamicAssets/Gameplay/UI/Textures/SpaceShip/ImagePoster/SubSize/%s.png"

local MISSION_TYPE = CS.Beyond.Gameplay.MissionSystem.MissionType
MISSION_TYPE_CONFIG = {
    [MISSION_TYPE.Main] = {
        missionIcon = "main_mission_icon",
    },
    [MISSION_TYPE.Char] = {
        missionIcon = "char_mission_icon",
    },
    [MISSION_TYPE.Factory] = {
        missionIcon = "fac_mission_icon",
    },
    [MISSION_TYPE.Misc] = {
        missionIcon = "misc_mission_icon",
    },
    [MISSION_TYPE.World] = {
        missionIcon = "world_mission_icon",
    },
}

local MISSION_VIEW_TYPE = GEnums.MissionViewType
MISSION_VIEW_TYPE_CONFIG = {
    [MISSION_VIEW_TYPE.MissionViewMain] = {
        missionIcon = "main_mission_icon_gray",
    },
    [MISSION_VIEW_TYPE.MissionViewDiscovery] = {
        missionIcon = "fac_mission_icon_gray",
    },
    [MISSION_VIEW_TYPE.MissionViewSide] = {
        missionIcon = "char_mission_icon_gray",
    },
    [MISSION_VIEW_TYPE.MissionViewActivity] = {
        missionIcon = "activity_mission_icon_gray",
    },
    [MISSION_VIEW_TYPE.MissionViewOther] = {
        missionIcon = "misc_mission_icon_gray",
    },
}

ITEM_DEPOT_TYPE = {
    Factory = 1,
    Equip = 2,
    ExpCard = 4,

    MissionItem = 5,
}

FACTORY_DEPOT_SHOWING_TYPES = {
    GEnums.ItemShowingType.Ore,
    GEnums.ItemShowingType.Plant,
    GEnums.ItemShowingType.Product,
    GEnums.ItemShowingType.Doodad,
    GEnums.ItemShowingType.Nurturance,
    GEnums.ItemShowingType.Usable,
    GEnums.ItemShowingType.Producer,
    GEnums.ItemShowingType.PortableDevice,
}

do

    UI_DRAG_DROP_SOURCE_TYPE = {
        Storage = 1,
        Repository = 2,
        QuickBar = 3,
        ItemBag = 4,
        FactoryDepot = 5,
        UseItemBar = 6,
        BuildModeSelect = 7,
    }


    UI_CONTROLLER_DRAG_DROP_SOURCE_PRIORITY = {
        [UI_DRAG_DROP_SOURCE_TYPE.ItemBag] = 1,
        [UI_DRAG_DROP_SOURCE_TYPE.FactoryDepot] = 1,
        [UI_DRAG_DROP_SOURCE_TYPE.Repository] = 2,
        [UI_DRAG_DROP_SOURCE_TYPE.Storage] = 2,
    }
    UI_CONTROLLER_DRAG_DROP_SOURCE_PRIORITY_MAX = 100


    FACTORY_REPO_DROP_ACCEPT_INFO = {
        sources = {
            UI_DRAG_DROP_SOURCE_TYPE.Storage,

            UI_DRAG_DROP_SOURCE_TYPE.ItemBag,
            UI_DRAG_DROP_SOURCE_TYPE.FactoryDepot,
        },
        types = {
            GEnums.ItemType.Material,
            GEnums.ItemType.TacticalItem,
            GEnums.ItemType.ConsumableItem,
        },
    }

    FACTORY_STORAGER_DROP_ACCEPT_INFO = {
        sources = {
            UI_DRAG_DROP_SOURCE_TYPE.FactoryDepot,
            UI_DRAG_DROP_SOURCE_TYPE.Storage,
            UI_DRAG_DROP_SOURCE_TYPE.ItemBag,
        },
    }

    FACTORY_LIQUID_STORAGER_DROP_ACCEPT_INFO = {
        sources = {
            UI_DRAG_DROP_SOURCE_TYPE.FactoryDepot,
            UI_DRAG_DROP_SOURCE_TYPE.ItemBag,
        },
    }

    FACTORY_QUICK_BAR_CLEAR_AREA_DROP_ACCEPT_INFO = {
        sources = {
            UI_DRAG_DROP_SOURCE_TYPE.QuickBar,
        },
    }

    FACTORY_QUICK_BAR_DROP_ACCEPT_INFO = {
        sources = {
            UI_DRAG_DROP_SOURCE_TYPE.Storage,
            UI_DRAG_DROP_SOURCE_TYPE.Repository,
            UI_DRAG_DROP_SOURCE_TYPE.ItemBag,
            UI_DRAG_DROP_SOURCE_TYPE.FactoryDepot,
            UI_DRAG_DROP_SOURCE_TYPE.QuickBar,
            UI_DRAG_DROP_SOURCE_TYPE.BuildModeSelect,
        },
        types = {
            GEnums.ItemType.NormalBuilding,
            GEnums.ItemType.FuncBuilding,
            GEnums.ItemType.SpecialBuilding,
            GEnums.ItemType.TDBuilding,
            GEnums.ItemType.Logistics,
            GEnums.ItemType.DecorateBuildingItem,
        },
    }

    ITEM_BAG_DROP_ACCEPT_INFO = {
        sources = {
            UI_DRAG_DROP_SOURCE_TYPE.Storage,
            UI_DRAG_DROP_SOURCE_TYPE.Repository,
            UI_DRAG_DROP_SOURCE_TYPE.ItemBag,
            UI_DRAG_DROP_SOURCE_TYPE.FactoryDepot,
        },
    }

    INVENTORY_AREA_ITEM_BAG_DROP_ACCEPT_INFO = {
        sources = {
            UI_DRAG_DROP_SOURCE_TYPE.Storage,
            UI_DRAG_DROP_SOURCE_TYPE.Repository,
            UI_DRAG_DROP_SOURCE_TYPE.ItemBag,
            UI_DRAG_DROP_SOURCE_TYPE.FactoryDepot,
        },
    }

    RPG_EQUIP_SLOT_DROP_ACCEPT_INFO = {
        sources = {
            UI_DRAG_DROP_SOURCE_TYPE.ItemBag,
        },
        types = {
            GEnums.ItemType.RPGDgEquip,
        },
    }

    ITEM_BAG_DROP_MASK_ACCEPT_INFO = {
        sources = {
            UI_DRAG_DROP_SOURCE_TYPE.FactoryDepot,
            UI_DRAG_DROP_SOURCE_TYPE.Storage,
            UI_DRAG_DROP_SOURCE_TYPE.Repository,
        },
    }

    USE_ITEM_BAR_DROP_ACCEPT_INFO = {
        sources = {
            UI_DRAG_DROP_SOURCE_TYPE.ItemBag,
            UI_DRAG_DROP_SOURCE_TYPE.UseItemBar,
        },
    }

    FACTORY_DEPOT_DROP_ACCEPT_INFO = {
        sources = {
            UI_DRAG_DROP_SOURCE_TYPE.Storage,
            UI_DRAG_DROP_SOURCE_TYPE.Repository,
            UI_DRAG_DROP_SOURCE_TYPE.ItemBag,
        }
    }

    INVENTORY_AREA_FACTORY_DEPOT_DROP_ACCEPT_INFO = {
        sources = {
            UI_DRAG_DROP_SOURCE_TYPE.Storage,
            UI_DRAG_DROP_SOURCE_TYPE.Repository,
            UI_DRAG_DROP_SOURCE_TYPE.ItemBag,
            UI_DRAG_DROP_SOURCE_TYPE.FactoryDepot,
        }
    }

    ABANDON_ITEM_DROP_ACCEPT_INFO = {
        sources = {
            UI_DRAG_DROP_SOURCE_TYPE.ItemBag,
        }
    }

    ITEM_MOVE_TARGET = {
        ItemBag = 1,
        Depot = 2,
        FacMachine = 3,
    }
end

UI_SPRITE_MAIN_HUD = "MainHud"
UI_SPRITE_FAC_COMMON = "Factory/Common"
UI_SPRITE_FAC_BUILDING_COMMON = "Factory/BuildingCommon"
UI_SPRITE_INVENTORY = "Inventory"
UI_SPRITE_FRIEND = "Friend"
UI_BUSINESS_CARD_ICON_PATH = "FriendListBg"
UI_BUSINESS_CARD_FRIEND_CHAT_ICON_PATH = "ThemeIcon/ChatBg"
UI_BUSINESS_CARD_FRIEND_DOMAIN_DEPOT_ICON_PATH = "ThemeIcon/DeportBg"
UI_SPRITE_RAID = "Raid"
UI_SPRITE_THEME_ITEM = "ThemeIcon/ThemeItem"
UI_SPRITE_THEME_BG = "ThemeIcon/ThemeChangeBg"
UI_SPRITE_FAC_MACHINE_CRAFTER = "Factory/MachineCrafter"
UI_SPRITE_FAC_WORKSHOP_CRAFT_TYPE_ICON = "Factory/WorkshopCraftTypeIcon"
UI_SPRITE_FAC_MARKER_SETTING_ICON = "Factory/Marker"
UI_SPRITE_MANUAL_CRAFT_TYPE_ICON = "ManualCraft"
UI_SPRITE_FAC_BUILDING_PANEL_ICON = "Factory/BuildingPanelIcon"
UI_SPRITE_FAC_BUILDING_PANEL_ICON_BIG = "Factory/BuildingPanelIconBig"
UI_SPRITE_FAC_SPECIAL_BUILDING_BIG_ICON = "Factory/SpecialBuildingBigIcon"
UI_SPRITE_FAC_CONTRACT = "Factory/Contract"
UI_SPRITE_CONTRACT_TYPE = "Factory/ContractType"
UI_SPRITE_FAC_SKILL_ICON = "Factory/FacSkillIcon"
UI_SPRITE_FAC_HUB_ICON = "Factory/FacHUB"
UI_SPRITE_FAC_MACHINE_BG = "Factory/MachineBG"
UI_SPRITE_FAC_BUILDING_IMAGE = "Factory/BuildingImage"
UI_SPRITE_BATTLE_SKILL_ICON = "SkillIcon"
UI_SPRITE_BATTLE_BUFF_ICON = "BuffIcon"
UI_SPRITE_BATTLE_USP_BG = "UspBg"
UI_SPRITE_KEY_ICON = "KeyIcon"
UI_SPRITE_EQUIP_PART_ICON = "Equip"
UI_SPRITE_DIALOG_OPTION_ICON = "Dialog/Option"
UI_SPRITE_FAC_BUILDING_ICON = "Factory/BuildingIcon"
UI_SPRITE_CHAR_FORMATION_ICON = "CharFormation"
UI_SPRITE_DIALOG_BG = "Dialog/BG"
UI_SPRITE_DIALOG_CENTER_IMAGE = "Dialog/CenterImage"
UI_SPRITE_WALLET = "WalletIcon"
UI_SPRITE_WIKI_MONSTER = "Wiki/MonsterImage"
UI_SPRITE_FAC_MATERIAL_TYPE_ICON = "Factory/FacHUB"
UI_SPRITE_FAC_TECH_ICON = "Factory/TechIcon"
UI_SPRITE_FAC_TOP_VIEW = "Factory/FacTopView"
UI_SPRITE_MONSTER_ICON = "MonsterIcon"
UI_SPRITE_MONSTER_ICON_BIG = "MonsterIconBig"
UI_SPRITE_SPACESHIP_ROOM = "Spaceship/SpaceshipRoom"
UI_SPRITE_CINEMATIC_BIG_LOGO = "Cinematic/BigLogo"
UI_SPRITE_ADVENTURE = "Adventure"
UI_SPRITE_MINIGAME_BLOCK = "MiniGame/Block"
UI_SPRITE_WATCH_NEW_BANNER = "Watch/Banner"
UI_SPRITE_SHOP_WEAPON_BOX = "Shop/ShopWeapon"
UI_SPRITE_SHOP_ROLE_IMAGE = "Shop/ShopEntry"
UI_SPRITE_SHOP_TAG_ICON = "Shop/ShopTagIcon"
UI_SPRITE_SHOP_TRADE_AREA_BG = "Shop/ShopTradeAreaBg"
UI_SPRITE_SHOP_TRADE_AREA_ICON = "Shop/ShopTradeAreaIcon"
UI_SPRITE_SHOP_TRADE_MARKET_ICON_SMALL = "Shop/ShopMarketTabIconSmall"
UI_SPRITE_DOMAIN_DEPOT = "DomainDepot"
UI_SPRITE_WEEKLY_RAID = "DungeonWeeklyRaid"
UI_SPRITE_MEDAL_ICON = "MedalIcon"
UI_SPRITE_MEDAL_ICON_BIG = "MedalIconBig"
UI_SPRITE_MEDAL_ICON_FORMAT = "%s_lv%02d"
UI_SPRITE_MEDAL_ICON_PLATE_FORMAT = "%s_lv%02d_plating"
UI_SPRITE_ACHIEVEMENT = "Achievement"
UI_SPRITE_SHARE_ICON = "CommonShare"
UI_SPRITE_SHIP = "Ship"
UI_SPRITE_CASH_SHOP_CATEGORY = "Shop/CashShopCategory"
UI_SPRITE_CASH_SHOP_GEM = "Shop/CashShopGem"
UI_SPRITE_SHOP_GROUP_BAG = "Shop/ShopGroupBag"
UI_SPRITE_SHOP_MONTHLY_PASS = "Shop/ShopMonthlyPass"
UI_SPRITE_DOMAIN_ICON = "DomainDepot"
UI_SPRITE_SHOP_COMMON = "Shop/ShopCommon"
UI_SPRITE_SPACESHIP_MUSIC_PLAYER = "MusicPlayer"

UI_SPRITE_CONTINGENCY_CONTRACT = "ContingencyContract"
UI_SPRITE_CONTINGENCY_CONTRACT_HUD = "ContingencyContract/Hud"
UI_SPRITE_CONTINGENCY_CONTRACT_BUFF = "ContingencyContract/Buff"

UI_SPRITE_REFLOW = "ReflowFormal"

COMMON_UI_TIME_UPDATE_INTERVAL = 1
FAC_COMMON_UI_UPDATE_INTERVAL = 0.1
FAC_COMMON_UI_MIDDLE_UPDATE_INTERVAL = 0.2
FAC_COMMON_UI_LARGER_UPDATE_INTERVAL = 0.5
FAC_HUB_UPDATE_INTERVAL = 10

UI_PLAYER_RENAME_UPDATE_INTERVAL = 0.1

NUMBER_SELECTOR_COUNT_REFRESH_INTERVAL = 0.5
NUMBER_SELECTOR_COUNT_REFRESH_AMOUNT = 10

NUMBER_SELECTOR_COUNT_CHANGE_GEAR_REFRESH_COUNT = 3
NUMBER_SELECTOR_COUNT_REFRESH_GEAR_PARAM = {
    {
        refreshInterval = 0.5,
        refreshAmount = 1,
    },
    {
        refreshInterval = 0.5,
        refreshAmount = 10,
    },
    {
        refreshInterval = 0.5,
        refreshAmount = 100,
        isFastMode = true,
    },
    {
        refreshInterval = 0.5,
        refreshAmount = 500,
        isFastMode = true,
    },
    {
        refreshInterval = 0.5,
        refreshAmount = 1000,
        isFastMode = true,
    },
    {
        refreshInterval = 0.25,
        refreshAmount = 1000,
        isFastMode = true,
    },
}
AUTO_CLOSE_DELAY = 0.15
CINEMATIC_TWEEN_TIME = 0.5

LOCK_ALPHA = 100 / 255

UI_TIPS_POS_TYPE = {
    MidBottom = 1,
    LeftTop = 2,
    RightTop = 3,
    RightDown = 4,
    LeftDown = 5,
    MidTop = 6,
    LeftMid = 7,
    RightMid = 8,
    GuideTips = 9,
    FacTopViewOption = 10,
    FacTopViewBuildActionIcons = 11,
    FacSmartAlertTop = 12,
    AdaptiveRightTop = 13,
    DailyAbsentRightTop = 14,
    LeftTopOrRightTop = 15,
    RightTopOrLeftTop = 16,
    LeftAlignBottom = 17,
}

UI_TIPS_X_POS_TYPE = {
    Mid = 1,
    Right = 2,
    Left = 3,
}

UI_TIPS_Y_POS_TYPE = {
    Mid = 1,
    Top = 2,
    Bottom = 3,
}

UI_GUIDE_OUT_OF_SCREEN_DISTANCE = 10000

UI_COMMON_MASK_TYPE = CS.Beyond.Gameplay.CommonMaskType
UI_COMMON_MASK_FADE_TYPE = CS.Beyond.Gameplay.CommonMaskFadeType
UI_COMMON_MASK_LAYER_TYPE = CS.Beyond.Gameplay.CommonMaskLayerType

UI_CHAR_FORMATION_STATE = {
    TeamWaitSet = 1,
    TeamHasSet = 2,
    CharChange = 3,
    SingleChar = 4,
}

UI_CHAR_FORMATION_SINGLE_STATE = {
    None = 0,
    Current = 1,
    OtherInTeam = 2,
    OtherDead = 3,
    OtherAvailable = 4,
    CurrentLocked = 5,
    OtherInTeamLocked = 6,
    OtherUnavailable = 7,
}

LAYERS = {
    Enemy = Unity.LayerMask.GetMask("Enemy"),
    UIInteract = Unity.LayerMask.GetMask("UIInteract"),
    WalkAndClimb = Unity.LayerMask.GetMask("Walkable", "Climbable"),
    Building = Unity.LayerMask.GetMask("Building"),
    BuildingAndClimb = Unity.LayerMask.GetMask("Building", "Climbable"),
    UISelect = Unity.LayerMask.GetMask("UISelect"),
    UI = Unity.LayerMask.GetMask("UI"),
    UIPP = Unity.LayerMask.GetMask("UI", "UIPP"),
    Nothing = Unity.LayerMask.GetMask("Nothing"),
    Gacha = Unity.LayerMask.GetMask("Gacha", "WorldUI", "Fog"),
    CharFormation = Unity.LayerMask.GetMask("Default", "WorldUI", "Fog", "UltimateShow"),
}
DEFAULT_LAYER = Unity.LayerMask.NameToLayer("Default")
GACHA_LAYER = Unity.LayerMask.NameToLayer("Gacha")
WORLD_UI_LAYER = Unity.LayerMask.NameToLayer("WorldUI")
UI_LAYER = Unity.LayerMask.NameToLayer("UI")
HIDE_LAYER = Unity.LayerMask.NameToLayer("Hide")
UIPP_LAYER = Unity.LayerMask.NameToLayer("UIPP")
UI_MODEL_LAYER = Unity.LayerMask.NameToLayer("UIModel")

do


    CharInfoTabEnum = {
        Info = 1,
        Unit = 2,
    }

    CharInfoCameraEnum = {
        Default = 0,
        Equip = 1,
        Far = 2,
        SkillUpgrade = 3,
    }

    CAM_TWEEN_TIME = 0.5
end

CharListMode = {
    Single = 1,
    MultiSelect = 2,
}

COUNT_NOT_ENOUGH_COLOR_STR = "FF8080"

INERTIA_VIEW_PAGER_STATE = {
    Idle = 0,
    Dragging = 1,
    Inertia = 2,
    Aligning = 3,
}

COUNT_RED_COLOR_STR = "F71717"

FAC_BUILDING_BUFF_COLOR_STR = "22BBFF"
FAC_BUILDING_DEBUFF_COLOR_STR = "FF7D7D"

COLOR_STRING_FORMAT = "<color=#%s>%s</color>"
COLOR_NUMBER_FORMAT = "<color=#%s>%d</color>"

ITEM_COIN_ID = "item_collection_ether"
ITEM_MOON_ID = "item_collection_instance"

SHOW_MISSION_HIGH_TIPS_HEIGHT_MIN = 1
SHOW_MISSION_HIGH_TIPS_DISTANCE_MAX = 100

SHOW_REWARD_ITEM_MAX_COUNT = 3

FAC_DEPOT_SORT_OPTIONS = {
    {
        name = Language.LUA_FAC_DEPOT_DEFAULT_SORT_NAME,
        keys = { "missionSortId", "sortId1", "sortId2", "rarity", "id" },
        reverseKeys = { "missionReverseSortId", "sortId1", "sortId2", "rarity", "id" },
    },
    {
        name = Language.LUA_FAC_DEPOT_RARITY_SORT_NAME,
        keys = { "missionSortId", "rarity", "sortId1", "sortId2", "id" },
        reverseKeys = { "missionReverseSortId", "rarity", "sortId1", "sortId2", "id" },
    },
}

COMMON_ITEM_SORT_KEYS = { "sortId1", "sortId2", "id", "customSortId" }

RPG_DUNGEON_GOLD_ID = "item_rpgdg_gold"
RPG_DUNGEON_TAB_COUNT = 3

ADVENTURE_DAILY_PROGRESS_ICON = "icon_adventure_box"
ADVENTURE_DAILY_PROGRESS_DOUBLE_ICON = "icon_adventure_box_double"

CHAR_INFO_DEFAULT_SHOW_ATTRIBUTES = { "hp", "atk", "def" }

JumpId = {
    Equip = "char_info_equip",
    LevelUpgrade = "char_info_level_upgrade",
    LevelBreak = "char_info_level_break",
}

AttributeShowNumMode = {
    Default = 0,
    ShowDeltaNum = 1,
    ShowDeltaCurNum = 2,
}

DUNGEON_REWARDS_DEFAULT_ICON = "icon_obtain_task01"



PANEL_ORDER_TO_PANEL_LEVEL = {
    [Types.EPanelOrderTypes.UI3D] = 0,
    [Types.EPanelOrderTypes.BottomScreenEffect] = 0,
    [Types.EPanelOrderTypes.LowerHud] = 0,
    [Types.EPanelOrderTypes.Hud] = 0,
    [Types.EPanelOrderTypes.TopScreenEffect] = 0,

    [Types.EPanelOrderTypes.Window] = 1,

    [Types.EPanelOrderTypes.PopUp] = 2,
    [Types.EPanelOrderTypes.Toast] = 2,
    [Types.EPanelOrderTypes.Guide] = 2,
    [Types.EPanelOrderTypes.Loading] = 2,
    [Types.EPanelOrderTypes.System] = 2,
    [Types.EPanelOrderTypes.Debug] = 2,
}

PANEL_PLAY_ANIMATION_OUT_COMPLETE_ACTION_TYPE = {
    Close = 1,
    Hide = 2,
}

COMMON_TOAST_SHOW_LIGHT_RARITY = 6
COMMON_TOAST_EXP_ICON_ID = "exp"

INVENTORY_MONEY_IDS = { "item_gold" }
DESTROY_ITEM_COUNT_FORMAT = "<color=#ff5458>%s</color>/%s"
REGION_MAP_STAMINA_IDS = { "item_ap" }

INPUT_FIELD_CHARACTER_LIMIT = 20
INPUT_FIELD_NAME_CHARACTER_LIMIT = 16
INPUT_FIELD_PLAYER_NAME_CHARACTER_LIMIT = 15

ITEM_MISSING_TRANSPARENCY = 0.5
ITEM_EXIST_TRANSPARENCY = 1
ITEM_TIP_PRODUCT_NODE_MAX_SHOW_COUNT = 6

COMMON_SERVER_UPDATE_TIME = 4

FAC_BELT_MAX_SHOW_ITEM_COUNT = 3


CHAR_INFO_TAB_TYPE = {
    OVERVIEW = 1,
    WEAPON = 2,
    EQUIP = 3,
    POTENTIAL = 4,
}
CHAR_INFO_TAB_NAME_LANGUAGE_PREFIX = "LUA_CHAR_INFO_MAIN_CONTROL_TAB_"

CHAR_INFO_PAGE_TYPE = {
    OVERVIEW = 1,
    WEAPON = 2,
    EQUIP = 3,
    POTENTIAL = 4,
    TALENT = 5,
    PROFILE = 6,
    UPGRADE = 7,
    PROFILE_SHOW = 10,
}


CHAR_INFO_TRANSITION_BLACK_SCREEN_DURATION = 0.3
CHAR_MAX_SKILL = 4
CHAR_MAX_TALENT = 2
CHAR_MAX_RARITY = 6
CHAR_MAX_POTENTIAL = Tables.globalConst.charMaxPotentialLevel
CHAR_MAX_TALENT_NUM = 2
CHAR_MAX_SKILL_LV = 12
CHAR_MAX_SKILL_NORMAL_LV = 9
CHAR_INFO_SKILL_SHOW_ORDER = {
    GEnums.SkillGroupType.NormalAttack,
    GEnums.SkillGroupType.NormalSkill,
    GEnums.SkillGroupType.ComboSkill,
    GEnums.SkillGroupType.UltimateSkill,
}



PHASE_CHAR_ITEM_ENABLE_SWITCH_FORMATION = "EnableSwitchFormation"
PHASE_CHAR_ITEM_SKIP_PARAM_NAME = "SkipIn"
PHASE_CHAR_ITEM_ENABLE_SWITCH = "EnableSwitch"
PHASE_CHAR_ITEM_FROM_INDEX = "FromIndex"
PHASE_CHAR_ITEM_TO_INDEX = "ToIndex"
PHASE_CHAR_ITEM_ANIMATOR_INDEX_DICT = {
    OVERVIEW = 0,
    WEAPON = 1,
    EQUIP = 2,
    TALENT = 3,
    DOCUMENT = 4,
    UPGRADE = 5,
    PROFILE_SHOW = 6,
    SP_1 = 20,
    SP_2 = 21,
    SP_RELAX = 30,
}

CHAR_INFO_PAGE_2_ANIMATOR_INDEX_DICT = {
    [CHAR_INFO_PAGE_TYPE.OVERVIEW] = PHASE_CHAR_ITEM_ANIMATOR_INDEX_DICT.OVERVIEW,
    [CHAR_INFO_PAGE_TYPE.WEAPON] = PHASE_CHAR_ITEM_ANIMATOR_INDEX_DICT.WEAPON,
    [CHAR_INFO_PAGE_TYPE.EQUIP] = PHASE_CHAR_ITEM_ANIMATOR_INDEX_DICT.EQUIP,
    [CHAR_INFO_PAGE_TYPE.TALENT] = PHASE_CHAR_ITEM_ANIMATOR_INDEX_DICT.TALENT,
    [CHAR_INFO_PAGE_TYPE.PROFILE] = PHASE_CHAR_ITEM_ANIMATOR_INDEX_DICT.OVERVIEW,
    [CHAR_INFO_PAGE_TYPE.UPGRADE] = PHASE_CHAR_ITEM_ANIMATOR_INDEX_DICT.UPGRADE,
    [CHAR_INFO_PAGE_TYPE.PROFILE_SHOW] = PHASE_CHAR_ITEM_ANIMATOR_INDEX_DICT.PROFILE_SHOW,
}

CHAR_INFO_ANIMATOR_INDEX_2_WEAPON_STATE = {
    [PHASE_CHAR_ITEM_ANIMATOR_INDEX_DICT.OVERVIEW] = CS.Beyond.Gameplay.View.CharUIModelMono.WeaponState.HIDE,
    [PHASE_CHAR_ITEM_ANIMATOR_INDEX_DICT.WEAPON] = CS.Beyond.Gameplay.View.CharUIModelMono.WeaponState.SHOW,
    [PHASE_CHAR_ITEM_ANIMATOR_INDEX_DICT.EQUIP] = CS.Beyond.Gameplay.View.CharUIModelMono.WeaponState.HIDE,
    [PHASE_CHAR_ITEM_ANIMATOR_INDEX_DICT.TALENT] = CS.Beyond.Gameplay.View.CharUIModelMono.WeaponState.HIDE,
    [PHASE_CHAR_ITEM_ANIMATOR_INDEX_DICT.DOCUMENT] = CS.Beyond.Gameplay.View.CharUIModelMono.WeaponState.HIDE,
    [PHASE_CHAR_ITEM_ANIMATOR_INDEX_DICT.UPGRADE] = CS.Beyond.Gameplay.View.CharUIModelMono.WeaponState.HIDE,
    [PHASE_CHAR_ITEM_ANIMATOR_INDEX_DICT.PROFILE_SHOW] = CS.Beyond.Gameplay.View.CharUIModelMono.WeaponState.HIDE,
}
CHAR_INFO_TO_OVERVIEW_STATE_NAME = {
    "FromOveview",
    "FromWeapon",
    "FromSkill",
    "FromUpgrade",
    "FromBreak",
    "FromEquip",
}



PHASE_CHAR_ITEM_CAMERA_POSTFIX_DICT = {
    OVERVIEW = "overview",
    WEAPON = "weapon",
    EQUIP = "equip",
    TALENT = "skill",
    TALENT_FOCUS = "skillFocus",
    DOCUMENT = "document",
    UPGRADE = "upgrade",
    POTENTIAL = "potential",
    FORMATION = "formation",
}
CHAR_INFO_MAIN_CONTROL_PAGE_2_CAMERA_POSTFIX = {
    [CHAR_INFO_PAGE_TYPE.OVERVIEW] = PHASE_CHAR_ITEM_CAMERA_POSTFIX_DICT.OVERVIEW,
    [CHAR_INFO_PAGE_TYPE.WEAPON] = PHASE_CHAR_ITEM_CAMERA_POSTFIX_DICT.WEAPON,
    [CHAR_INFO_PAGE_TYPE.EQUIP] = PHASE_CHAR_ITEM_CAMERA_POSTFIX_DICT.EQUIP,
    [CHAR_INFO_PAGE_TYPE.TALENT] = PHASE_CHAR_ITEM_CAMERA_POSTFIX_DICT.TALENT,
    [CHAR_INFO_PAGE_TYPE.PROFILE] = PHASE_CHAR_ITEM_CAMERA_POSTFIX_DICT.DOCUMENT,
    [CHAR_INFO_PAGE_TYPE.PROFILE_SHOW] = PHASE_CHAR_ITEM_CAMERA_POSTFIX_DICT.DOCUMENT,
    [CHAR_INFO_PAGE_TYPE.UPGRADE] = PHASE_CHAR_ITEM_CAMERA_POSTFIX_DICT.UPGRADE,
    [CHAR_INFO_PAGE_TYPE.POTENTIAL] = PHASE_CHAR_ITEM_CAMERA_POSTFIX_DICT.POTENTIAL
}




CHAR_INFO_ATTRIBUTE_SPECIFIC_SYSTEM = {
    CHAR_FULL_ATTR = 1,
    EQUIP_FULL_ATTR = 2,
}

CHAR_INFO_FULL_ATTR_BLACK_MAP = {
    [GEnums.AttributeType.Str] = {
        forbidAllModifier = true,
    },
    [GEnums.AttributeType.Agi] = {
        forbidAllModifier = true,
    },
    [GEnums.AttributeType.Wisd] = {
        forbidAllModifier = true,
    },
    [GEnums.AttributeType.Will] = {
        forbidAllModifier = true,
    },
    [GEnums.AttributeType.MaxHp] = {
        forbidAllModifier = true,
    },
    [GEnums.AttributeType.Def] = {
        forbidAllModifier = true,
    },
    [GEnums.AttributeType.Atk] = {
        forbidAllModifier = true,
    },
}

CHAR_INFO_ALL_SHOW_ATTRIBUTES = {
    GEnums.AttributeType.Str,
    GEnums.AttributeType.Agi,
    GEnums.AttributeType.Wisd,
    GEnums.AttributeType.Will,
    GEnums.AttributeType.Atk,
    GEnums.AttributeType.Def,
    GEnums.AttributeType.MaxHp,
    GEnums.AttributeType.ComboSkillCooldownScalar,
    GEnums.AttributeType.PhysicalDamageTakenScalar,
    GEnums.AttributeType.FireDamageTakenScalar,
    GEnums.AttributeType.PulseDamageTakenScalar,
    GEnums.AttributeType.CrystDamageTakenScalar,
    GEnums.AttributeType.CriticalRate,
    GEnums.AttributeType.CriticalDamageIncrease,
}

CHAR_INFO_UPGRADE_SHOW_ATTRIBUTES = {
    GEnums.AttributeType.Str,
    GEnums.AttributeType.Agi,
    GEnums.AttributeType.Wisd,
    GEnums.AttributeType.Will,
    GEnums.AttributeType.Atk,
    GEnums.AttributeType.MaxHp,
}
CHAR_INFO_FIRST_CLASS_ATTRIBUTES = {
    [GEnums.AttributeType.Str] = true,
    [GEnums.AttributeType.Agi] = true,
    [GEnums.AttributeType.Wisd] = true,
    [GEnums.AttributeType.Will] = true,
}
CHAR_INFO_FIRST_CLASS_ATTRIBUTE_SHOW_ORDER = {
    GEnums.AttributeType.Str,
    GEnums.AttributeType.Agi,
    GEnums.AttributeType.Wisd,
    GEnums.AttributeType.Will,
}

CHAR_INFO_SECOND_CLASS_MAIN_ATTRIBUTE_SHOW_ORDER = {
    GEnums.AttributeType.MaxHp,
    GEnums.AttributeType.Atk,
    GEnums.AttributeType.Def,
}

CHAR_INFO_SECOND_CLASS_SUB_ATTRIBUTE_SHOW_ORDER = {
    GEnums.AttributeType.CriticalRate,
    GEnums.AttributeType.CriticalDamageIncrease,
    GEnums.AttributeType.PhysicalDamageTakenScalar,
    GEnums.AttributeType.FireDamageTakenScalar,
    GEnums.AttributeType.PulseDamageTakenScalar,
    GEnums.AttributeType.CrystDamageTakenScalar,
    GEnums.AttributeType.ComboSkillCooldownScalar,
}

CHAR_INFO_BASIC_ATTRIBUTE_SHOW_ORDER = {
    GEnums.AttributeType.Str,
    GEnums.AttributeType.Agi,
    GEnums.AttributeType.Wisd,
    GEnums.AttributeType.Will,
    GEnums.AttributeType.Atk,
    GEnums.AttributeType.Def,
    GEnums.AttributeType.MaxHp,
}

CHAR_INFO_UPGRADE_ATTRIBUTE_SHOW_ORDER = {
    GEnums.AttributeType.Str,
    GEnums.AttributeType.Agi,
    GEnums.AttributeType.Wisd,
    GEnums.AttributeType.Will,
    GEnums.AttributeType.Atk,
    GEnums.AttributeType.MaxHp,
}

CHAR_INFO_SKILL_GROUP_TYPE_TO_TYPE_NAME = {
    [GEnums.SkillGroupType.NormalAttack] = Language.LUA_CHAR_INFO_NORMAL_ATTACK_NAME,
    [GEnums.SkillGroupType.NormalSkill] = Language.LUA_CHAR_INFO_NORMAL_SKILL_NAME,
    [GEnums.SkillGroupType.UltimateSkill] = Language.LUA_CHAR_INFO_ULTIMATE_SKILL_NAME,
    [GEnums.SkillGroupType.ComboSkill] = Language.LUA_CHAR_INFO_COMBO_SKILL_NAME,
}

CHAR_INFO_ATTRIBUTE_ALL_FILTER_MASK = CS.Beyond.Gameplay.Core.Attributes.FilerTypeMask.All
CHAR_INFO_ATTRIBUTE_NONE_FILTER_MASK = CS.Beyond.Gameplay.Core.Attributes.FilerTypeMask.None
CHAR_INFO_ATTRIBUTE_EQUIP_FILTER_MASK = CS.Beyond.Gameplay.Core.Attributes.FilerTypeMask.Equipment
CHAR_INFO_ATTRIBUTE_WEAPON_FILTER_MASK = CS.Beyond.Gameplay.Core.Attributes.FilerTypeMask.Weapon




CHAR_INFO_TAB_ICON_PREFIX = "icon_mainmenu_0"
CHAR_INFO_TAB_ICON_PREFIX = "icon_mainmenu_0"




CHAR_INFO_EQUIP_SLOT_MAP = {
    BODY = 0,
    HAND = 1,
    EDC_1 = 2,
    EDC_2 = 3,
    TACTICAL = 4,
}
EQUIP_PART_TYPE_2_CELL_CONFIG = {
    [CHAR_INFO_EQUIP_SLOT_MAP.BODY] = {
        equipPostfix = "Body",
        slotPartType = GEnums.PartType.Body,
        equipIndex = 1,
    },
    [CHAR_INFO_EQUIP_SLOT_MAP.HAND] = {
        equipPostfix = "Hand",
        slotPartType = GEnums.PartType.Hand,
        equipIndex = 0,
    },
    [CHAR_INFO_EQUIP_SLOT_MAP.EDC_1] = {
        equipPostfix = "EDC_1",
        slotPartType = GEnums.PartType.EDC,
        equipIndex = 2,

    },
    [CHAR_INFO_EQUIP_SLOT_MAP.EDC_2] = {
        equipPostfix = "EDC_2",
        slotPartType = GEnums.PartType.EDC,
        equipIndex = 3,

    },
    [CHAR_INFO_EQUIP_SLOT_MAP.TACTICAL] = {
        equipPostfix = "Tactical",
        isTacticalItem = true,
        equipIndex = 4,

    },
}



CHAR_INFO_WEAPON_EXHIBIT_TAB_ICON_PREFIX = "icon_weapon_exhibit_0"

WEAPON_INFO_TYPE = {
    CHAR_INFO,
    WEAPON_EXHIBIT,
}







WEAPON_MAX_RARITY = 6
WEAPON_EXHIBIT_TAB_ICON_PREFIX = "icon_weapon_exhibit_0"
WEAPON_EXHIBIT_WEAPON_TYPE_ICON_PREFIX = "icon_weapontype_0"

WEAPON_EXHIBIT_TAB_TYPE = {
    OVERVIEW = 1,
    GEM = 2,
    POTENTIAL = 3,
    DOCUMENT = 4,
}

WEAPON_EXHIBIT_PAGE_TYPE = {
    OVERVIEW = 1,
    UPGRADE = 2,
    GEM = 3,
    DOCUMENT = 4,
    POTENTIAL = 5,
}

WEAPON_EXHIBIT_CAM_INDEX = {
    OVERVIEW = 1,
    UPGRADE = 2,
    GEM = 3,
    GEM_NEAR = 4,
    DOCUMENT = 5,
    POTENTIAL = 6,
}

WEAPON_EXHIBIT_PAGE_TYPE_2_CAM_NAME = {
    [WEAPON_EXHIBIT_PAGE_TYPE.OVERVIEW] = "vcam_wpn_overview",
    [WEAPON_EXHIBIT_PAGE_TYPE.UPGRADE] = "vcam_wpn_upgrade",
    [WEAPON_EXHIBIT_PAGE_TYPE.GEM] = "vcam_wpn_orb",
    [WEAPON_EXHIBIT_PAGE_TYPE.DOCUMENT] = "vcam_wpn_document",
    [WEAPON_EXHIBIT_PAGE_TYPE.POTENTIAL] = "vcam_wpn_potential",
}

WEAPON_EXHIBIT_PAGE_TYPE_2_CAM_INDEX = {
    [WEAPON_EXHIBIT_PAGE_TYPE.OVERVIEW] = WEAPON_EXHIBIT_CAM_INDEX.OVERVIEW,
    [WEAPON_EXHIBIT_PAGE_TYPE.UPGRADE] = WEAPON_EXHIBIT_CAM_INDEX.UPGRADE,
    [WEAPON_EXHIBIT_PAGE_TYPE.GEM] = WEAPON_EXHIBIT_CAM_INDEX.GEM,
    [WEAPON_EXHIBIT_PAGE_TYPE.DOCUMENT] = WEAPON_EXHIBIT_CAM_INDEX.DOCUMENT,
    [WEAPON_EXHIBIT_PAGE_TYPE.POTENTIAL] = WEAPON_EXHIBIT_CAM_INDEX.POTENTIAL,
}

WEAPON_EXHIBIT_UPGRADE_MIN_SLOT_NUM = 5
WEAPON_EXHIBIT_UPGRADE_ITEM_MAX_COUNT = 100
WEAPON_EXHIBIT_REFUND_ICON = "icon_weapon_upgrade"

CHAR_INFO_EQUIP_SLOT_COUNT = 4
CHAR_INFO_EQUIP_TYPE_TILE_PREFIX = "LUA_CHAR_EQUIP_LIST_NAME_"
CHAR_INFO_EQUIP_LIST_SPRITE_NAME_PREFIX = "icon_equipmenttype_0"
CHAR_ICON_DEFAULT = "icon_chr_default"

BLOC_MAX_REWARD_ITEM_COUNT = 4

DEPOT_FILTER_CALC_TYPE = {
    AND = 1,
    OR = 2,
}
COUNT_ICON_ITEM_BAG = "count_icon_01"
COUNT_ICON_DEPOT = "count_icon_02"
COUNT_ICON_ALL = "count_icon_03"

MAP_DETAIL_BTN_ICON_NAME = {
    TELEPORT = "icon_btn_transfer",
    TRACE = "icon_btn_trace",
    REMOVE_TRACE = "icon_btn_cancel",
    DETAIL = "icon_btn_particulars",
    FAST_ENTER = "icon_btn_enter_into",
    CONFIRM = "icon_btn_confirm",
}

GUIDE_STEP_MIN_INTERVAL = 0.5

COMMON_MASK_STATE = {
    None = 0,
    WaitFade = 1,
    FadingIn = 2,
    Masking = 3,
    ShowTextEnd = 4,
    FadingOut = 5,
    WaitEnd = 6,
    End = 7,
}

SKILL_TYPE_2_BTN_INDEX = {
    [Const.SkillTypeEnum.NormalAttack] = 1,
    [Const.SkillTypeEnum.UltimateSkill] = 3,
    [Const.SkillTypeEnum.NormalSkill] = 2,
}

SKILL_BTN_INDEX_2_TYPE = {
    [1] = Const.SkillTypeEnum.NormalAttack,
    [2] = Const.SkillTypeEnum.NormalSkill,
    [3] = Const.SkillTypeEnum.UltimateSkill,
}

CHAR_SKILL_MODE = {
    Default = 1,
    ShowSkillTypeName = 2,
}

TALENT_COLUMN_NUM = 2

RED_DOT_TYPE = {
    New = 1,
    Normal = 2,
    Expire = 3,
}
RED_DOT_TYPE_MAX = RED_DOT_TYPE.Expire
RED_DOT_DEFAULT_PENETRATE = RED_DOT_TYPE.Normal

CHAR_INFO_WEAPON_STATE = {
    Normal = 1,
    Detail = 2,
}

CHAR_INFO_EQUIP_STATE = {
    Normal = 1,
    Detail = 2,
}
ATTRIBUTE_GENERATE_FORCE_PERCENT = {
    DO_NOT_CARE = 0,
    NO_PERCENT = 1,
    HAS_PERCENT = 2,
}
ATTRIBUTE_GENERATE_FORCE_DIFF_FROM_DEFAULT = {
    DO_NOT_CARE = 0,
    NO_PERCENT = 1,
    HAS_PERCENT = 2,
}

EQUIP_TYPE_TO_ICON_NAME = {
    [GEnums.PartType.Body] = "icon_equipmenttype_01",
    [GEnums.PartType.Hand] = "icon_equipmenttype_02",
    [GEnums.PartType.EDC] = "icon_equipmenttype_EDC",
}

EQUIP_TYPE_TO_INVERSE_ICON_NAME = {
    [GEnums.PartType.Body] = "icon_equipmenttype_01new",
    [GEnums.PartType.Hand] = "icon_equipmenttype_02new",
    [GEnums.PartType.EDC] = "icon_equipmenttype_EDCnew",
}

DEFAULT_SORT_OPTION = {
    {
        name = Language.LUA_CHAR_SORT_1,
        keys = { "slotIndex", "level", "templateId" },
        reverseKeys = { "slotReverseIndex", "level", "templateId" },
    }
}

TACTICAL_ITEM_SORT_OPTION = {
    {
        name = Language.LUA_TACTICAL_ITEM_SORT_NUM,
        keys = { "curCount", "rarity", "sortId1", "sortId2" },
    },
    {
        name = Language.LUA_TACTICAL_ITEM_SORT_RARITY,
        keys = { "rarity", "sortId1", "sortId2" },
    },
}

CHAR_FORMATION_LIST_SORT_OPTION = {
    {
        name = Language.LUA_CHAR_SORT_1,


        keys = { "slotIndex", "replaceablePriorityReverse", "level", "rarity", "ownTime", "sortOrder", "templateId" },
        reverseKeys = { "slotReverseIndex", "replaceablePriority", "level", "rarity", "ownTime", "sortOrder", "templateId" },
    },
    {
        name = Language.LUA_CHAR_SORT_2,
        keys = { "slotIndex", "replaceablePriorityReverse", "rarity", "level", "ownTime", "sortOrder", "templateId" },
        reverseKeys = { "slotReverseIndex", "replaceablePriority", "rarity", "level", "ownTime", "sortOrder", "templateId" },
    },
}

CHAR_POSTER_LIST_SORT_OPTION = {
    {
        name = Language.LUA_CHAR_SORT_1,
        keys = { "selectSlot", "level", "rarity", "ownTime", "sortOrder" },
        reverseKeys = { "selectSlotReverse", "level", "rarity", "ownTime", "sortOrder", },
    },
    {
        name = Language.LUA_CHAR_SORT_2,
        keys = { "selectSlot","rarity", "level", "ownTime", "sortOrder" },
        reverseKeys = {"selectSlotReverse", "rarity", "level", "ownTime", "sortOrder" },
    },
}

SS_PICTURE_SORT_OPTION = {
    {
        name = Language.LUA_CHAR_SORT_1,
        keys = {"selectSlot", "charRarity", "charSortOrder", "charPhotoCount","photoLevel", "innerIndexReversal"},
        reverseKeys = {"selectSlotReverse", "charRarity", "charSortOrder", "charPhotoCountReversal", "photoLevelReversal", "innerIndex"},
    },
    {
        name = Language.LUA_POTENTIAL_LEVEL,
        keys = {"selectSlot", "charPhotoCount", "charRarity", "photoLevel", "charSortOrder", "innerIndexReversal"},
        reverseKeys = {"selectSlotReverse", "charPhotoCount", "charRarityReversal", "photoLevel", "charSortOrder", "innerIndex"},
    },
    {
        name = Language.LUA_POTENTIAL_UNLOCK_TIME,
        keys = {"selectSlot", "unlockTime", "charRarity", "charSortOrder", "innerIndexReversal"},
        reverseKeys = {"selectSlotReverse", "unlockTime", "charRarityReversal", "charSortOrder", "innerIndex"},
    },
}

CHAR_SORT_OPTION = {
    {
        name = Language.LUA_CHAR_SORT_1,
        keys = { "slotIndex", "level", "rarity", "ownTime", "sortOrder" },
        reverseKeys = { "slotReverseIndex", "slotIndex", "level", "rarity", "ownTime", "sortOrder" },
    },
    {
        name = Language.LUA_CHAR_SORT_2,
        keys = { "slotIndex", "rarity", "level", "ownTime", "sortOrder" },
        reverseKeys = { "slotReverseIndex", "slotIndex", "rarity", "level", "ownTime", "sortOrder" },
    },





}

ManualCraftSortOptions = {
    {

        name = Language.LUA_FAC_CRAFT_SORT_1,
        sortMode = 1,
        sortKeys = {"sortId"},
    },
    {

        name = Language.LUA_FAC_CRAFT_SORT_2,
        sortMode = 2,
        sortKeys = {"rarity", "sortId"},
    },
}

ManualCraftPopupsSortOptions = {
    {

        name = Language.LUA_FAC_CRAFT_SORT_1,
        sortMode = 1,
        sortKeys = {"sortId1", "sortId2", "rarity", "id"},
    },
    {

        name = Language.LUA_FAC_CRAFT_SORT_2,
        sortMode = 2,
        sortKeys = {"rarity","sortId1", "sortId2","id"},
    },
}

CharacterSummonSortOptions = {
    {

        name = Language.LUA_FAC_CRAFT_SORT_1,
        upKeys = { "upStageSort", "rarity", "friendStageUpSort", "friendValueSort", "sortOrder" },
        downKeys = { "downAllStageSort",  "rarity", "friendStageDownSort", "friendValueSort","sortOrder" },
    },
    {
        name = Language.LUA_SPACESHIP_SUMMON_SORT_FRIEND_LEVEL,
        upKeys = { "upStageSort", "friendStageSpecialUpSort", "friendValueSort", "rarity", "sortOrder" },
        downKeys = { "downAllStageSort", "friendStageSpecialDownSort", "friendValueSort", "rarity", "sortOrder" },
    },
}

EQUIP_SORT_OPTION = {
    {
        name = Language.LUA_EQUIP_SORT_1,
        keys = { "num_canEquip", "minWearLv", "sortId2", "tier", "equipEnhanceLevel" },
        reverseKeys = { "num_canEquip", "minWearLv", "sortId2", "tier", "equipEnhanceLevel" },
    },
}

WEAPON_SORT_OPTION = {
    {
        name = Language.LUA_DEPOT_SORT_OPTION_RARITY,
        keys = { "selectSlot","forceSortKey", "rarity", "weaponLv", "sortId1", "sortId2", "id" },
        reverseKeys = { "selectSlotReverse", "forceSortKeyReverse", "rarity", "weaponLv", "sortId1", "sortId2", "id" },
    },
    {
        name = Language.LUA_DEPOT_SORT_OPTION_WEAPON_LV,
        keys = { "selectSlot", "forceSortKey", "weaponLv", "rarity", "sortId1", "sortId2", "id" },
        reverseKeys = { "selectSlotReverse","forceSortKeyReverse", "weaponLv", "rarity", "sortId1", "sortId2", "id" },
    },
}

WEAPON_POTENTIAL_SORT_OPTION = {
    {
        name = Language.LUA_DEPOT_SORT_OPTION_RARITY,
        keys = { "isItemMarker", "rarity", "weaponLv", "sortId1", "sortId2", "id" },
    },
    {
        name = Language.LUA_DEPOT_SORT_OPTION_WEAPON_LV,
        keys = { "isItemMarker", "weaponLv", "rarity", "sortId1", "sortId2", "id" },
    },
}

WEAPON_UPGRADE_SORT_OPTION = {
    {
        name = Language.LUA_DEPOT_SORT_OPTION_RARITY,
        keys = { "ingredientIndex", "rarity", "sortId1", "sortId2", "id" },
    },
    {
        name = Language.LUA_DEPOT_SORT_OPTION_WEAPON_LV,
        keys = { "ingredientIndex", "weaponLv", "rarity", "sortId1", "sortId2", "id" },
    },
}

WEAPON_GEM_SORT_OPTION = {
    {
        name = Language.LUA_DEPOT_SORT_OPTION_RARITY,
        keys = { "matchWeaponSkillCount", "matchWeaponSkillIndex", "rarity", "lockedIndex", "trashIndexReverse", "equippedWeaponInstIdReverse", "sortId1", "sortId2", "id", "instId" },
    },
}

FRIENDSHIP_PRESENT_GIFT_SORT_OPTION = {
    {
        name = Language.LUA_SPACESHIP_GIFT_SORT_OPTION_CHAR_LIKE,
        keys = { "isLike", "count", "sortId1Reverse" },
    },
    {
        name = Language.LUA_SPACESHIP_GIFT_SORT_OPTION_COUNT,
        keys = { "count", "isLike", "sortId1Reverse" },
    },
    {
        name = Language.LUA_SPACESHIP_GIFT_SORT_OPTION_RARITY,
        keys = { "rarity", "isLike", "sortId1Reverse" },
    },
}

COMMON_ITEM_LIST_TYPE = {
    WEAPON_EXHIBIT_GEM = "WeaponExhibitGem",
    WEAPON_EXHIBIT_UPGRADE = "WeaponExhibitUpgrade",
    CHAR_INFO_WEAPON = "charInfoWeapon",
    CHAR_INFO_EQUIP = "CharInfoEquip",
    WEAPON_EXHIBIT_POTENTIAL = "WeaponExhibitPotential",
    GEM_RECAST = "GemRecast",
    CHAR_INFO_TACTICAL_ITEM = "CharInfoTactical",
    EQUIP_TECH_EQUIP_ENHANCE = "EquipTechEquipEnhance",
    EQUIP_TECH_EQUIP_ENHANCE_MATERIALS = "EquipTechEquipEnhanceMaterials",
    WEAPON_GEM_MATERIAL = "WeaponGemMaterial",
}

ITEM_BG_TYPE_COLORS = {
    [GEnums.ItemBgType.Black] = "2B2B2B",
}

BLOC_LEVEL_COLOR = {
    "ededed",
    "e5ecce",
    "d1ec89",
    "daed26",
    "fdd700",
    "ffae00",
}

CONTROLLER_HINT_POS_TYPE = {
    Center = 1,
    Left = 2,
    Right = 3,
}

EMOJI_PREFAB_PATH = "Assets/Beyond/DynamicAssets/Gameplay/UI/Prefabs/Emoji/%s.prefab"

COMMON_MASK_WAIT_HIDE_TIME_OUT_TIME = 10
COMMON_MASK_TIME_OUT_TIME = 30

NARRATIVE_ANONYMITY_PATTERN = "(.-%b{})"

REMOTE_COMM_CELL_MAX_NUM = 4


MIN_FMV_ASPECT_RATIO = 1.777777777

MAX_FMV_ASPECT_RATIO = 2.166666666

MAX_DIALOG_ASPECT_RATIO = 2.333333333

DIALOG_IMAGE_FEMALE_SUFFIX = "_f"
DIALOG_IMAGE_MALE_SUFFIX = "_m"

UI_ANIMATION_WRAPPER_STATE = CS.Beyond.UI.UIConst.AnimationState

PANEL_ASSET_TYPES = {
    Default = 1,
    PC = 2,
    Controller = 3,
}

MOUSE_ICON_HINT = {
    Default = "icon_mouse",
    Delete = "icon_mouse_frame",
    Frame = "icon_mouse_frame",
    Magnifier = "icon_mouse_magnifier",
    ContinuousBuild = "icon_mouse_continuous_build",
}


DIALOG_OPEN_UI_USE_PANEL = {

}

DIALOG_OPTION_ENHANCE_COLOR_ICON_TYPE = {
    "manualcollect",
}

CHAPTER_ICON_CONFIGS = {
    [CS.Beyond.Gameplay.ChapterType.Main] = {
        icon = "chapter_main_icon_01",
        bgIcon = "chapter_main_bg_icon_01",
    },
    [CS.Beyond.Gameplay.ChapterType.Other] = {
        icon = "chapter_character_icon_01",
        bgIcon = "",
    },
}

WORLD_LEVEL_CONDITION_TAG_ICON_FOLDER = "WorldLevelConditionTag"
WORLD_LEVEL_CONDITION_TAG_ICON_FORMAT = "icon_world_level_condition_chapter_%s"

FAC_TRANS_DOMAIN_ICONS = {
    ["domain_1"] = "icon_transfer_site_valley_iv",
    ["domain_2"] = "icon_transfer_site_kam_lung",
    ["domain_3"] = "icon_transfer_site_kam_lung",
}

ITEM_MAX_RARITY = 6
ITEM_RARITY_DEFAULT_LIGHT_IMG = "bg_item_rarity_bar_common"
ITEM_RARITY_SP_LIGHT_IMG = "bg_item_rarity_bar_sp"

GACHA_CHAR_TIMELINE_PATH = "Assets/Beyond/DynamicAssets/Gameplay/Gacha/%s/Prefab/%s.prefab"

MINI_PUZZLE_GAME_ECOLOR_STR = {
    [CS.Beyond.Gameplay.EColor.Color1] = "B3FF00",
    [CS.Beyond.Gameplay.EColor.Color2] = "3CAAFF",
    [CS.Beyond.Gameplay.EColor.Color3] = "00FFD0",
    [CS.Beyond.Gameplay.EColor.Color4] = "FFAF49",
}

INT_BATTLE_ENABLED_OPT_TYPES = {
    [CS.Beyond.Gameplay.Core.InteractOptionType.Item] = true,
    [CS.Beyond.Gameplay.Core.InteractOptionType.Interactive] = true,
    [CS.Beyond.Gameplay.Core.InteractOptionType.AbandonPack] = true,
}

UI_RICH_CONTENT_IMG_GENDER_DIFF_MATCH = "^fm//(.*)"
UI_RICH_CONTENT_IMG_GENDER_DIFF_FORMAT_MALE = "%s_m"
UI_RICH_CONTENT_IMG_GENDER_DIFF_FORMAT_FEMALE = "%s_f"

CHECK_IN_CONST = {

    CBT2_CHECK_IN_ID = "activity_checkin_1",

    PART_SPLIT_NUM = 7,
}

CHAR_INFO_ATTR_TYPE_2_DETAIL_GROUP = {
    [GEnums.AttributeType.MaxHp] = {
        {
            showNameKey = "LUA_CHAR_INFO_ATTR_HP_DETAIL_BASE",
            valueFuncName = "getHpBase",
            detailListFuncName = "getHpDetailList",
        },
        {
            showNameKey = "LUA_CHAR_INFO_ATTR_EXTRA",
            valueFuncName = "getHpExtra",
            notZero = true,
        },
    },
    [GEnums.AttributeType.Atk] = {
        {
            showNameKey = "LUA_CHAR_INFO_ATTR_ATK_DETAIL_TOTAL_BASE",
            valueFuncName = "getAtkTotalBase",
            detailListFuncName = "getAtkTotalBaseDetailList",
        },
        {
            showNameKey = "LUA_CHAR_INFO_ATTR_ATK_DETAIL_MULTI",
            valueFuncName = "getAtkScalar",
            detailListFuncName = "getAtkScalarDetailList",
            hintInfo = {
                titleKey = "LUA_CHAR_INFO_ATTR_ATK_DETAIL_MULTI",
                mainHintKey = "LUA_CHAR_INFO_ATTR_ATK_DETAIL_MULTI_HINT",
            }
        },
    },
    [GEnums.AttributeType.Def] = {
        {
            showNameKey = "LUA_CHAR_INFO_ATTR_DEF_DETAIL_BASE",
            valueFuncName = "getDefBase",
            detailListFuncName = "getDefDetailList",
        },
        {
            showNameKey = "LUA_CHAR_INFO_ATTR_EXTRA",
            valueFuncName = "getDefExtra",
            notZero = true,
        },
    },
}

GACHA_MUSIC_UI = "au_music_gacha_interface"
GACHA_MUSIC_DROP_BIN = "au_music_gacha_cs"

TRACK_HUD_SCROLL_STATE = {


    AlwaysCantScroll = 1,


    CanScrollWhenFold = 2,


    AlwaysCanScroll = 3,
}


ON_TRACK_HUD_CONST_HEIGHT = 296

JOYSTICK_IN_SCREEN_HEIGHT_PROPORTION = 0.55

TRACK_HUD_UNFOLD_OCCLUSION_JOYSTICK_PROPORTION = 0.333333

AUTO_CLOSE_MOBILE_DRAG_HELPER_DIST = 50

NEXT_POTENTIAL_STAR_COLOR = Color(1, 0.8745, 0.1647, 1)

ACHIEVEMENT_TOAST_DISABLE_KEY = {
    Loading = "loading",
    GachaChar = "gacha_char",
    GachaWeapon = "gacha_weapon",
}

ACHIEVEMENT_MEDAL_SLOT_TYPE = {
    MedalDisplay = 0,
    MedalDepot = 1,
}

ACHIEVEMENT_MEDAL_UPGRADE_LEVEL = {
    Iron = 1,
    Silver = 2,
    Gold = 3,
}

CHAR_TALENT_SPACESHIP_ICON =
{
    ["α"] = "stage_level_01",
    ["β"] = "stage_level_02",
    ["γ"] = "stage_level_03",
}

SPACESHIP_SUMMON_MASK_FADE_IN = 0.3
SPACESHIP_SUMMON_MASK_FADE_OUT = 0.3
SPACESHIP_SUMMON_MASK_FADE_WAIT = 2

COMMON_UI_DRAG_MIN_SQR_DIST = 0.1
CHAR_PHOTO_POTENTIAL_LEVELS = { 1, 3, 5 }
CHAR_POTENTIAL_EFFECT_RARITY = 6


INVENTORY_AREA_ITEM_MOVE_TYPE = {
    DEFAULT = 1,
    BAG_TO_DEPOT = 2,
}


INVENTORY_AREA_LAYOUT_STYLE = {
    ACCORDION = 1,
    SPLIT = 2,
}




GameSettingItemState = {
    Normal = "Normal",
    Disabled = "Disabled",
}

GameSettingChangeReason = {
    Default = 1,
    Gamepad = 2,
}




INPUT_DEVICE_CHANGE_MASK_TIME = 0


DISABLED_USE_ITEM_ID_IN_TOP_VIEW = {
    ["item_proc_bomb_1"] = true
}

DOMAIN_DEPOT_BACKGROUND_STAGES = {
    Pack = 0,
    WaitSelectBuyer = 1,
    SelectBuyer = 2,
    FinishSelectBuyer = 3,
}

ITEM_BAG_QUICK_STASH_USEFUL_ITEMS = {
    ["item_fbottle_glass_water"] = true,
    ["item_fbottle_glass_xiranite"] = true,
    ["item_fbottle_glassenr_water"] = true,
    ["item_fbottle_glassenr_xiranite"] = true,
    ["item_fbottle_iron_water"] = true,
    ["item_fbottle_iron_xiranite"] = true,
    ["item_fbottle_ironenr_water"] = true,
    ["item_fbottle_ironenr_xiranite"] = true,
    ["item_fbottle_copper_water"] = true,
    ["item_fbottle_copper_xiranite"] = true,


    ["item_fbottle_glass_xiranite_enr"] = true,
    ["item_fbottle_glassenr_xiranite_enr"] = true,
    ["item_fbottle_iron_xiranite_enr"] = true,
    ["item_fbottle_ironenr_xiranite_enr"] = true,
    ["item_fbottle_copper_xiranite_enr"] = true,
    ["item_fbottle_copperenr_water"] = true,
    ["item_fbottle_copperenr_xiranite"] = true,
    ["item_fbottle_copperenr_xiranite_enr"] = true,
}

UI_RESTORE_PHASE_BLACKLIST = {
    ["CharFormation"] = true,
    ["Map"] = true,
    ["RegionMap"] = true,
    ["Watch"] = true,
    ["Dialog"] = true,
    ["BalloonInteractive"] = true,
}

UI_TEXT_LINK_TYPE = {
    Default = "default",
    Narrative = "narrative",
    Hyperlink = "hyperlink",
}

UI_TEXT_LINK_MOUSE_ICON = {
    [UI_TEXT_LINK_TYPE.Default] = MOUSE_ICON_HINT.Default,
    [UI_TEXT_LINK_TYPE.Narrative] = MOUSE_ICON_HINT.Magnifier,
    [UI_TEXT_LINK_TYPE.Hyperlink] = MOUSE_ICON_HINT.Magnifier,
}
