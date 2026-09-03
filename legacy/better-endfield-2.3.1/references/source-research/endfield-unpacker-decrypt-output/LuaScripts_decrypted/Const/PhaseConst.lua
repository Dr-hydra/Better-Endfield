PHASE_FILE_PATH = "Phase/%s/Phase%s"
PHASE_GAME_OBJECT_FILE_PATH = "Assets/Beyond/DynamicAssets/Gameplay/Prefabs/%s/%s.prefab"

PHASE_MODEL_FILE_PATH = "Assets/Beyond/DynamicAssets/Gameplay/Prefabs/UIModels/"
PHASE_CHAR_MODEL_FILE_PATH = PHASE_MODEL_FILE_PATH .. "%s_uimodel.prefab"
PHASE_CHAR_ANIMATOR_CONTROLLER_FILE_PATH = PHASE_MODEL_FILE_PATH .. "%s_controller.controller"

EPhaseState = {
    Init = 1,
    TransitionIn = 2,
    Activated = 3,
    Deactivated = 4,
    TransitionBehind = 5,
    TransitionOut = 6,
    WaitRelease = 7,
    TransitionBackToTop = 8,
}

PHASE_CHAR_ITEM_INIT_PARAM_NAME = "init"
PHASE_CHAR_ITEM_FORMATION_PARAM_NAME = "formation"


DONT_DESTROY_ON_SEAMLESS_LOADING = {
    "MiniMap",
    "MainHud",
    "Joystick",
    "MissionHud",
    "InteractOption",
    "FacBuildingInteract",
    "HeadBar",
    "BattleDamageText",
    "RadioEmpty",
    "GeneralAbility",
    "CommonItemToast",
    "CommonNewToast",
    "SNSHud",
    "AIBark",
    "Watch",
    "Inventory",
    "ValuableDepot",
    "WalletBar",
    "ControllerHint",
    "CommonPopUp",
    "Guide",
    "FacMiniPowerHud",
    "FacBuildMode",
    "FacDestroyMode",
    "WaterDroneAim",
    "ItemTips",
    "GuideLimited",
    "FacMain",
    "FacMainLeft",
    "FacMainRight",
    "FacQuickBar",
    "BattleAction",
    "SquadIcon",
    "BattleComboSkill",
    "BattleComboSkillUse",
    "BattleBossInfo",
    "BattleBottomScreenEffect",
    "GeneralTracker",
    "Radio",
    "LevelCamera",
}
