ROOM_COLOR_STR = {
    [GEnums.SpaceshipRoomType.ControlCenter] = { "4F91BF", "303030" },
    [GEnums.SpaceshipRoomType.ManufacturingStation] = { "FBF12A", "303030" },
    [GEnums.SpaceshipRoomType.GrowCabin] = { "B4F72F", "303030" },
    [GEnums.SpaceshipRoomType.GuestRoomClueExtension] = { "9f8bff", "303030" },
}

NO_ROOM_COLOR_STR = { "313131", "DEDEDE" }
NO_ROOM_SPECIAL_COLOR_STR = { "DEDEDE", "262626" }

GROW_CABIN_MAX_FILED = 9

ROOM_PHASE_ID_NAME_MAP_BY_TYPE = {
    [GEnums.SpaceshipRoomType.ControlCenter] = "SpaceshipControlCenter",
    [GEnums.SpaceshipRoomType.ManufacturingStation] = "SpaceshipManufacturingStation",
    [GEnums.SpaceshipRoomType.GrowCabin] = "SpaceshipGrowCabin",
    [GEnums.SpaceshipRoomType.GuestRoomClueExtension] = "SpaceshipGuestRoomClue",
}

FORMULA_CELL_OWN_COUNT_COLOR_STR = { "4A4A4A", "BD2631" }
SOW_FORMULA_COUNT_STATE_COLOR_STR = { "DEDEDE", "FF0000" }


TYPE_TXT_MAP = {
    [GEnums.SpaceshipRoomType.ControlCenter] = Language["ui_spaceship_dailyreport_roomcell_typetxt_controlcenter"],
    [GEnums.SpaceshipRoomType.GrowCabin] = Language.LUA_SPACESHIP_COLLECT_HINT_TYPE_GROW_CABIN,
    [GEnums.SpaceshipRoomType.ManufacturingStation] = Language.LUA_SPACESHIP_COLLECT_HINT_TYPE_MANUFACTURING_STATION,
    [GEnums.SpaceshipRoomType.GuestRoomClueExtension] = Language.LUA_SPACESHIP_CLUE_COLLECTION,
}

SCENE_UI_PREFAB_PATH = "Assets/Beyond/DynamicAssets/Gameplay/UI/SceneUIPrefabs/Interactives/SpaceShip/%s.prefab"
CHAR_POSTER_UI_NAME = "ReceptionRoomCharPosterUI"
CHAR_POSTER_TEXTURE_PATH = "Assets/Beyond/DynamicAssets/Gameplay/UI/Textures/SpaceShip/ReceptionRoom/poster_%s.png"

SHOWCASE_LOWER_SLOT_EFFECT_PREFAB = "Assets/Beyond/DynamicAssets/Gameplay/Spaceship/ShowcaseItems/spaceship_showcase_lower_slot_effect.prefab"
SHOWCASE_UPPER_SLOT_EFFECT_PREFAB = "Assets/Beyond/DynamicAssets/Gameplay/Spaceship/ShowcaseItems/spaceship_showcase_upper_slot_effect.prefab"

SHOWCASE_SLOT_EFFECT_INFO = {

    [1] = { true, Vector3(5.2940, 2.0011, 14.5940), Vector3(271.3740, 144.7488, 120.9140) },
    [2] = { false, Vector3(6.1150, 2.4120, 12.3920), Vector3(270.0000, 175.8215, 0.0000) },
    [3] = { true, Vector3(5.3470, 2.0600, 10.6280), Vector3(270.5356, 216.0156, 329.0623) },
    [4] = { false, Vector3(8.1880, 2.3490, 11.1430), Vector3(270.0000, 75.9170, 0.0000) },
    [5] = { true, Vector3(10.2980, 1.9651, 11.0540), Vector3(270.0280, 78.2874, 0.0000) },
    [6] = { false, Vector3(9.0370, 2.5810, 13.4860), Vector3(270.0000, 334.1375, 0.0000) },
    [7] = { true, Vector3(8.4150, 2.2421, 15.6060), Vector3(273.0993, 210.7621, 120.0839) },
}
GUEST_ROOM_CLUE_PANEL_TYPE = {
    Overview = 1,
    Collect = 2,
    Receive = 3,
    GiftClues = 4,
    Inventory = 5,
    Settlement = 6,
}


SPACESHIP_ATTR_COMBINE_TYPE =
{
    [GEnums.SpaceshipRoomAttrType.GuestRoomClue1ProbabilityIncrease] = GEnums.SpaceshipRoomAttrType.GuestRoomClueProbabilityIncreaseShow,
    [GEnums.SpaceshipRoomAttrType.GuestRoomClue2ProbabilityIncrease] = GEnums.SpaceshipRoomAttrType.GuestRoomClueProbabilityIncreaseShow,
    [GEnums.SpaceshipRoomAttrType.GuestRoomClue3ProbabilityIncrease] = GEnums.SpaceshipRoomAttrType.GuestRoomClueProbabilityIncreaseShow,
    [GEnums.SpaceshipRoomAttrType.GuestRoomClue4ProbabilityIncrease] = GEnums.SpaceshipRoomAttrType.GuestRoomClueProbabilityIncreaseShow,
    [GEnums.SpaceshipRoomAttrType.GuestRoomClue5ProbabilityIncrease] = GEnums.SpaceshipRoomAttrType.GuestRoomClueProbabilityIncreaseShow,
    [GEnums.SpaceshipRoomAttrType.GuestRoomClue6ProbabilityIncrease] = GEnums.SpaceshipRoomAttrType.GuestRoomClueProbabilityIncreaseShow,
    [GEnums.SpaceshipRoomAttrType.GuestRoomClue7ProbabilityIncrease] = GEnums.SpaceshipRoomAttrType.GuestRoomClueProbabilityIncreaseShow,
}


SPACESHIP_CLUE_INDEX_2_ATTR_TYPE =
{
    [1] = GEnums.SpaceshipRoomAttrType.GuestRoomClue1ProbabilityIncrease,
    [2] = GEnums.SpaceshipRoomAttrType.GuestRoomClue2ProbabilityIncrease,
    [3] = GEnums.SpaceshipRoomAttrType.GuestRoomClue3ProbabilityIncrease,
    [4] = GEnums.SpaceshipRoomAttrType.GuestRoomClue4ProbabilityIncrease,
    [5] = GEnums.SpaceshipRoomAttrType.GuestRoomClue5ProbabilityIncrease,
    [6] = GEnums.SpaceshipRoomAttrType.GuestRoomClue6ProbabilityIncrease,
    [7] = GEnums.SpaceshipRoomAttrType.GuestRoomClue7ProbabilityIncrease,
}