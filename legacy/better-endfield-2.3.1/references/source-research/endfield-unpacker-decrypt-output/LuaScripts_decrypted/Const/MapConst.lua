LEVEL_MAP_CONTROLLER_MODE = {
    DEBUG = 0,
    FIXED = 1,
    LEVEL_SWITCH = 2,
    FOLLOW_CHARACTER = 3,
}

LEVEL_MAP_ID_GETTER = {
    MAP01_LV001 = "map01_lv001",
    MAP01_LV002 = "map01_lv002",
    MAP01_LV003 = "map01_lv003",
    MAP01_LV005 = "map01_lv005",
    MAP01_LV006 = "map01_lv006",
    MAP01_LV007 = "map01_lv007",
    MAP02_LV001 = "map02_lv001",
    MAP02_LV002 = "map02_lv002",
    MAP02_LV003 = "map02_lv003",
    MAP02_LV004 = "map02_lv004",
    MAP02_LV005 = "map02_lv005",
    BASE01_LV001 = "base01_lv001",
    BASE01_LV003 = "base01_lv003",
    DUNG02_DG005 = "dung02_dg005",
}

UI_DOMAIN_MAP_PATH = "Assets/Beyond/DynamicAssets/Gameplay/UI/Prefabs/Map/Region3D/%s.prefab"
UI_MAP_SWITCH_MASK_PATH = "LevelMap/SwitchMask"
UI_SPACESHIP_MAP = "Spaceship"

MAP_BUILDING_COLLECTION_INFO_NUM_TEXT_FORMAT = "%d/%d"

MAP_BUILDING_COLLECTION_INFO_POPUP_TOTAL_NUM_TEXT_FORMAT = "/%d"

CUSTOM_MARK_SELECT_TEMPLATE = "mark_cus_select"

MAP_3D_NAVI_THREAD_WAIT_TIME = 0.3

LOADER_POWER_LINE_MAX_COUNT = 1024

RESOURCE_RELATED_MARK_TYPE = {
    [GEnums.MarkType.DoodadGroup] = true,
    [GEnums.MarkType.MinePointTeam] = true,
    [GEnums.MarkType.GasMinePointTeam] = true,
}




BASE_TIER_CONTAINER_ID = 0
BASE_TIER_ID = 0
BASE_TIER_INDEX = 0






MapMistLoadType = {
    Normal = 0,
    Animation = 1,
    LOD = 2,
}













MapPanelNodeType = {
    Remind = 1,
    Tracking = 2,
    SpaceshipJump = 3,
    Zoom = 4,
    DomainSwitch = 5,
    LevelInfo = 6,
    Filter = 7,
    TierSwitch = 8,
    WalletBar = 9,
}

POWER_RELATED_COMBINED_TEMPLATE_ID = "mark_fac_power_related"
TRAVEL_RELATED_COMBINED_TEMPLATE_ID = "mark_fac_travel_related"

GAMEPLAY_STATE_CUSTOM_GETTER = function()
    if Utils.isInWeekRaid() then
        return {
            expectedPanelNodes = MapConst.WEEK_RAID_MAP_EXPECTED_PANEL_NODES,
            expectedStaticElementTypes = MapConst.WEEK_RAID_MAP_EXPECTED_STATIC_ELEMENT_TYPES,
        }
    end

    if GameInstance.player.spaceship.isViewingFriend then
        return {
            expectedPanelNodes = MapConst.SPACESHIP_FRIEND_VIEW_MAP_EXPECTED_PANEL_NODES,
        }
    end

    return nil
end



DOMAIN_DEPOT_MAP_EXPECTED_PANEL_NODES = {
    [MapPanelNodeType.Zoom] = true,
    [MapPanelNodeType.TierSwitch] = true,
}

WEEK_RAID_MAP_EXPECTED_PANEL_NODES = {
    [MapPanelNodeType.Zoom] = true,
    [MapPanelNodeType.TierSwitch] = true,
}

SPACESHIP_FRIEND_VIEW_MAP_EXPECTED_PANEL_NODES = {
    [MapPanelNodeType.Zoom] = true,
    [MapPanelNodeType.TierSwitch] = true,
}

SPACESHIP_MAP_EXPECTED_PANEL_NODES = {
    [MapPanelNodeType.Zoom] = true,
    [MapPanelNodeType.TierSwitch] = true,
    [MapPanelNodeType.SpaceshipJump] = true,
    [MapPanelNodeType.Tracking] = true,
    [MapPanelNodeType.Remind] = true,
    [MapPanelNodeType.WalletBar] = true,
}

ZFY_OFFICE_MAP_EXPECTED_PANEL_NODES = {
    [MapPanelNodeType.Zoom] = true,
    [MapPanelNodeType.TierSwitch] = true,
    [MapPanelNodeType.SpaceshipJump] = true,
    [MapPanelNodeType.Tracking] = true,
    [MapPanelNodeType.Remind] = true,
    [MapPanelNodeType.DomainSwitch] = true,
    [MapPanelNodeType.WalletBar] = true,
    [MapPanelNodeType.Filter] = true,
}

DELETE_MODE_MAP_EXPECTED_NODES = {
    [MapPanelNodeType.Zoom] = true,
}

LEVEL_EXPECTED_PANEL_NODES_GETTER = {
    [LEVEL_MAP_ID_GETTER.BASE01_LV001] = SPACESHIP_MAP_EXPECTED_PANEL_NODES,
    [LEVEL_MAP_ID_GETTER.BASE01_LV003] = SPACESHIP_MAP_EXPECTED_PANEL_NODES,
    [LEVEL_MAP_ID_GETTER.DUNG02_DG005] = ZFY_OFFICE_MAP_EXPECTED_PANEL_NODES,
}



DOMAIN_DEPOT_MAP_EXPECTED_PANEL_MARKS = {
    [TRAVEL_RELATED_COMBINED_TEMPLATE_ID] = true,
    ["mark_sp_campfire"] = true,
    ["mark_p_domain_depot"] = true,
    ["mark_p_domain_depot_deliver_target"] = true,
}

DOMAIN_DEPOT_MAP_TOP_ORDER_PANEL_MARKS = {
    ["mark_p_domain_depot"] = true,
    ["mark_p_domain_depot_deliver_target"] = true,
}



DOMAIN_DEPOT_MAP_EXPECTED_STATIC_ELEMENT_TYPES = {
    [CS.Beyond.Gameplay.UILevelMapStaticElementType.FacMainRegion] = true,
    [CS.Beyond.Gameplay.UILevelMapStaticElementType.SettlementRegion] = true,
    [CS.Beyond.Gameplay.UILevelMapStaticElementType.Crane] = true,
    [CS.Beyond.Gameplay.UILevelMapStaticElementType.Misty] = true,
    [CS.Beyond.Gameplay.UILevelMapStaticElementType.NarrativeAreaText] = true,
    [CS.Beyond.Gameplay.UILevelMapStaticElementType.Common] = true,
}

WEEK_RAID_MAP_EXPECTED_STATIC_ELEMENT_TYPES = {
    [CS.Beyond.Gameplay.UILevelMapStaticElementType.FacMainRegion] = true,
    [CS.Beyond.Gameplay.UILevelMapStaticElementType.SettlementRegion] = true,
    [CS.Beyond.Gameplay.UILevelMapStaticElementType.Crane] = true,
    [CS.Beyond.Gameplay.UILevelMapStaticElementType.Misty] = true,
    [CS.Beyond.Gameplay.UILevelMapStaticElementType.NarrativeAreaText] = true,
    [CS.Beyond.Gameplay.UILevelMapStaticElementType.Common] = true,
}

MINI_MAP_EXPECTED_STATIC_ELEMENT_TYPES = {
    [CS.Beyond.Gameplay.UILevelMapStaticElementType.FacMainRegion] = true,
    [CS.Beyond.Gameplay.UILevelMapStaticElementType.SettlementRegion] = true,
    [CS.Beyond.Gameplay.UILevelMapStaticElementType.Crane] = true,
    [CS.Beyond.Gameplay.UILevelMapStaticElementType.Misty] = true,
    [CS.Beyond.Gameplay.UILevelMapStaticElementType.Common] = true,
}






COMBINED_TEMPLATE_ID_TO_LINE_TYPE = {
    [POWER_RELATED_COMBINED_TEMPLATE_ID] = CS.Beyond.Gameplay.MarkLineType.Power,
    [TRAVEL_RELATED_COMBINED_TEMPLATE_ID] = CS.Beyond.Gameplay.MarkLineType.Travel,
}

FILTER_TYPE_TO_LINE_TYPE = {
    [CS.Beyond.GEnums.MarkInfoType.HUB] = CS.Beyond.Gameplay.MarkLineType.Power,
    [CS.Beyond.GEnums.MarkInfoType.PowerPole] = CS.Beyond.Gameplay.MarkLineType.Power,
    [CS.Beyond.GEnums.MarkInfoType.TravelPole] = CS.Beyond.Gameplay.MarkLineType.Travel,
    [CS.Beyond.GEnums.MarkInfoType.PipeBuilding] = CS.Beyond.Gameplay.MarkLineType.UdPipe,
}

LINE_TYPE_TO_VISIBLE_LAYER_FIELD_NAME = {
    [CS.Beyond.Gameplay.MarkLineType.Travel] = "travelLineVisibleLayer",
    [CS.Beyond.Gameplay.MarkLineType.Power] = "powerLineVisibleLayer",
    [CS.Beyond.Gameplay.MarkLineType.UdPipe] = "udPipeLineVisibleLayer",
}

FAC_LINE_TYPES = {
    [CS.Beyond.Gameplay.MarkLineType.Power] = true,
    [CS.Beyond.Gameplay.MarkLineType.Travel] = true,
    [CS.Beyond.Gameplay.MarkLineType.UdPipe] = true,
}






GENERAL_TRACK_OTHER_LEVEL_ICON_NAME = "general_track_other_level"
MISSION_HIGH_IMPORTANCE_TRACK_OTHER_LEVEL_ICON_NAME = "mission_high_track_other_level"
MISSION_MID_IMPORTANCE_TRACK_OTHER_LEVEL_ICON_NAME = "mission_mid_track_other_level"
MISSION_LOW_IMPORTANCE_TRACK_OTHER_LEVEL_ICON_NAME = "mission_low_track_other_level"






MARK_DYNAMIC_NODE_PREFAB_ROOT_PATH = "Assets/Beyond/DynamicAssets/Gameplay/UI/Prefabs/Map/MarkDynamicNode/"





MARK_DYNAMIC_NODE_PREFAB_PATH_CONFIG = {
    DomainDepotHint = "DomainDepotHint",
    SocialBuildingHint = "SocialBuildingHint",
    SettlementLevelNode = "SettlementLevelNode",
    TierStateNode = "TierStateNode",
    DetectorNode = "DetectorNode",
    SettlementDefenseHint = "SettlementDefenseHint",
    ResourceStateNode = "ResourceStateNode",
}

