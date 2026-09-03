QUICK_MENU_ITEM_ID_GETTER = {
    none = "none",
    character = "character",
    valuableDepot = "valuableDepot",
    inventory = "inventory",
    adventureBook = "adventureBook",
    gacha = "gacha",
    sns = "sns",
    mission = "mission",
    formation = "formation",
    techTree = "techTree",
    mail = "mail",
    hubReport = "hubReport",
    dungeonInfo = "dungeonInfo",
    hub = "hub",
    racingEffect = "racingEffect",
    controlCenter = "controlCenter",
    activity = "activity",
    domain = "domain",
    weekRaid = "weekRaid",
    wikiGuide = "wikiGuide",
    battlePass = "battlePass",
    cashShop = "cashShop",
    weekRaidTipInfo = "weekRaidTipInfo",
    dungeonRacing = "dungeonRacing",
    hubData = "hubData",
}
























QUICK_MENU_ITEM_CONFIG = {

    [QUICK_MENU_ITEM_ID_GETTER.character] = {
        mainHudId = "character",
        needExtraDelayRecoverScreen = true,
    },
    [QUICK_MENU_ITEM_ID_GETTER.valuableDepot] = {
        mainHudId = "valuableDepot",
    },
    [QUICK_MENU_ITEM_ID_GETTER.inventory] = {
        mainHudId = "inventory",
        nameTextId = function()
            if WeeklyRaidUtils.IsInWeeklyRaid() then
                return "LUA_WEEK_RAID_INVENTORY_QUICK_MENU_NAME"
            end
            return nil
        end,
        iconId = function()



            if Utils.isInSafeZone() then
                return "btn_inventory_safezone"
            end
            return nil
        end,
        refreshMessageList = { MessageConst.ON_SET_IN_SAFE_ZONE }
    },
    [QUICK_MENU_ITEM_ID_GETTER.adventureBook] = {
        mainHudId = "adventureBook",
    },
    [QUICK_MENU_ITEM_ID_GETTER.domain] = {
        mainHudId = "domain",
    },
    [QUICK_MENU_ITEM_ID_GETTER.sns] = {
        phaseId = "SNS",
    },
    [QUICK_MENU_ITEM_ID_GETTER.mission] = {
        phaseId = "Mission",
        getIsForbidden = function()
            if GameInstance.player.forbidSystem:IsForbidden(ForbidType.ForbidMissionHudShowNonTracking) then
                return true
            end
            if GameInstance.player.forbidSystem:IsForbidden(ForbidType.ForbidJumpToMissionPanelFromHud) then
                return true
            end
            return false
        end
    },
    [QUICK_MENU_ITEM_ID_GETTER.formation] = {
        mainHudId = "formation",
        needExtraDelayRecoverScreen = true,
    },
    [QUICK_MENU_ITEM_ID_GETTER.weekRaid] = {
        mainHudId = "weekRaid",
        nameTextId = "LUA_WEEK_RAID_MISSION_QUICK_MENU_NAME",
        iconId = "btn_week_raid",
    },


    [QUICK_MENU_ITEM_ID_GETTER.techTree] = {
        mainHudId = "techTree",
    },
    [QUICK_MENU_ITEM_ID_GETTER.mail] = {
        phaseId = "Mail",
        mainHudId = "mail",
    },
    [QUICK_MENU_ITEM_ID_GETTER.dungeonInfo] = {
        mainHudId = "dungeonInfo",
        nameTextId = "LUA_QUICK_MENU_ITEM_DUNGEON_INFO_NAME",
        iconId = "btn_dungeon",
    },
    [QUICK_MENU_ITEM_ID_GETTER.weekRaidTipInfo] = {
        mainHudId = "weekRaidTipInfo",
        nameTextId = "ui_weekraid_main_intro",
        iconId = "racing_answer_btn",
    },
    [QUICK_MENU_ITEM_ID_GETTER.dungeonRacing] = {
        mainHudId = "dungeonRacing",
        nameTextId = "ui_activity_racingdungeon_center_task",
        iconId = "btn_coin_task",
    },
    [QUICK_MENU_ITEM_ID_GETTER.hub] = {
        mainHudId = "hub",
        nameTextId = "LUA_QUICK_MENU_ITEM_HUB_NAME",
        iconId = "btn_hub",
    },
    [QUICK_MENU_ITEM_ID_GETTER.hubData] = {
        mainHudId = "hubData",
        nameTextId = "LUA_QUICK_MENU_ITEM_HUB_DATA_NAME",
        iconId = "btn_hubData",
    },
    [QUICK_MENU_ITEM_ID_GETTER.controlCenter] = {
        mainHudId = "controlCenter",
        nameTextId = "LUA_QUICK_MENU_ITEM_CONTROL_CENTER_NAME",
        iconId = "btn_airship",
    },
    [QUICK_MENU_ITEM_ID_GETTER.wikiGuide] = {
        mainHudId = "wikiGuide",
        nameTextId = "LUA_QUICK_MENU_ITEM_WIKI_GUIDE_NAME",
        iconId = "btn_wiki_guide",
    },


    [QUICK_MENU_ITEM_ID_GETTER.gacha] = {
        mainHudId = "gacha",
    },
    [QUICK_MENU_ITEM_ID_GETTER.activity] = {
        mainHudId = "activityCenter",
    },
    [QUICK_MENU_ITEM_ID_GETTER.battlePass] = {
        mainHudId = "battlePass",
        nameTextId = "LUA_QUICK_MENU_ITEM_BATTLE_PASS_NAME",
        iconId = "btn_battlepass",

        ignorePhaseOpenCheck = true,
    },
    [QUICK_MENU_ITEM_ID_GETTER.cashShop] = {
        mainHudId = "cashShop",
    },
}


QUICK_MENU_CENTER_ITEM_LIST = {
    QUICK_MENU_ITEM_ID_GETTER.character,
    QUICK_MENU_ITEM_ID_GETTER.valuableDepot,
    QUICK_MENU_ITEM_ID_GETTER.inventory,
    QUICK_MENU_ITEM_ID_GETTER.adventureBook,
    QUICK_MENU_ITEM_ID_GETTER.domain,
    QUICK_MENU_ITEM_ID_GETTER.sns,
    { QUICK_MENU_ITEM_ID_GETTER.weekRaid, QUICK_MENU_ITEM_ID_GETTER.dungeonRacing, QUICK_MENU_ITEM_ID_GETTER.mission },
    QUICK_MENU_ITEM_ID_GETTER.formation,
}


QUICK_MENU_LEFT_ITEM_CELLS_LIST = {
    {
        QUICK_MENU_ITEM_ID_GETTER.mail,
    },
    {
        QUICK_MENU_ITEM_ID_GETTER.dungeonInfo,
        QUICK_MENU_ITEM_ID_GETTER.wikiGuide,
        QUICK_MENU_ITEM_ID_GETTER.weekRaidTipInfo,
        QUICK_MENU_ITEM_ID_GETTER.hubData,
    },
    {
        QUICK_MENU_ITEM_ID_GETTER.hub,
        QUICK_MENU_ITEM_ID_GETTER.controlCenter,
    },
    {
        QUICK_MENU_ITEM_ID_GETTER.techTree,
    },
}


QUICK_MENU_RIGHT_ITEM_CELLS_LIST = {
    {
        QUICK_MENU_ITEM_ID_GETTER.activity
    },
    {
        QUICK_MENU_ITEM_ID_GETTER.battlePass
    },
    {
        QUICK_MENU_ITEM_ID_GETTER.cashShop
    },
    {
        QUICK_MENU_ITEM_ID_GETTER.gacha,
    },
}

QUICK_MENU_AUDIO = {
    [QUICK_MENU_ITEM_ID_GETTER.dungeonInfo] = "Au_UI_Button_Dungeon",
}
