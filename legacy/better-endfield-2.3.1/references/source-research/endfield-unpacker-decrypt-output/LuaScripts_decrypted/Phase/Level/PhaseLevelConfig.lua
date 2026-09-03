local PhaseLevelConfig = {}

PhaseLevelConfig.ConditionType = {
    DEFAULT = 1,
    SPACESHIP_VISITOR = 2,
    WEEKLY_RAID = 3,
    WEEKLY_RAID_INTRO = 4,
}
local ConditionFunctions = {
    [PhaseLevelConfig.ConditionType.SPACESHIP_VISITOR] = function()
        return GameWorld.worldInfo.curLevelId == Tables.spaceshipConst.visitSceneName
    end,
    [PhaseLevelConfig.ConditionType.WEEKLY_RAID] = function()
        return WeeklyRaidUtils.IsInWeeklyRaid()
    end,
    [PhaseLevelConfig.ConditionType.WEEKLY_RAID_INTRO] = function()
        return WeeklyRaidUtils.IsInWeeklyRaidIntro()
    end,
}


local DefaultConfig = {
    open = {
        PanelId.MiniMap,
        PanelId.MainHud,
        PanelId.Joystick,
        PanelId.MissionHud,
        PanelId.InteractOption,
        PanelId.FacBuildingInteract,
        PanelId.HeadBar,
        PanelId.BattleDamageText,
        PanelId.RadioEmpty,
        PanelId.GeneralAbility,
        PanelId.CommonItemToast,
        PanelId.CommonNewToast,
        PanelId.SNSHud,
    },
    preload = {
        PanelId.AIBark,
        PanelId.Watch,
        PanelId.Inventory,
        PanelId.ValuableDepot,

        PanelId.WalletBar,
        PanelId.ControllerHint,
        PanelId.CommonPopUp,
        PanelId.Guide,

        PanelId.FacMiniPowerHud,
        PanelId.FacBuildMode,
        PanelId.FacDestroyMode,

        PanelId.WaterDroneAim,
    },
    preOpen = {
        PanelId.ItemTips,
        PanelId.GuideLimited,

        PanelId.FacMain,
        PanelId.FacMainLeft,
        PanelId.FacMainRight,
        PanelId.FacQuickBar,
        PanelId.BattleAction,
        PanelId.SquadIcon,
        PanelId.BattleComboSkill,
        PanelId.BattleComboSkillUse,
        PanelId.BattleBossInfo,
        PanelId.BattleBottomScreenEffect,
        PanelId.EnvironmentalPrompt,
    },
    specialPanels = {
        PanelId.GeneralTracker,
        PanelId.Radio,
    }
}


local Configurations = {
    [PhaseLevelConfig.ConditionType.SPACESHIP_VISITOR] = {
        open = {
            PanelId.MiniMap,
            PanelId.MainHud,
            PanelId.Joystick,
            PanelId.InteractOption,
            PanelId.HeadBar,
            PanelId.BattleDamageText,
            PanelId.RadioEmpty,
            PanelId.SocializeVisitTips,
            PanelId.SocializeVisitMission,
            PanelId.CommonItemToast,
            PanelId.CommonNewToast,
        },
        preload = {
            PanelId.WalletBar,
            PanelId.ControllerHint,
            PanelId.CommonPopUp,
        },
        specialPanels = {
            PanelId.GeneralTracker,
            PanelId.Radio,
        }
    },
    [PhaseLevelConfig.ConditionType.WEEKLY_RAID] = {
        open = {
            PanelId.MiniMap,
            PanelId.MainHud,
            PanelId.Joystick,
            PanelId.WeeklyRaidTaskTrackHud,
            PanelId.InteractOption,
            PanelId.FacBuildingInteract,
            PanelId.HeadBar,
            PanelId.BattleDamageText,
            PanelId.RadioEmpty,
            PanelId.GeneralAbility,
            PanelId.CommonItemToast,
            PanelId.CommonNewToast,
        },
        preload = {
            PanelId.WalletBar,
            PanelId.ControllerHint,
            PanelId.CommonPopUp,
            PanelId.Guide,
            PanelId.GuideLimited,
        },
        specialPanels = {
            PanelId.GeneralTracker,
            PanelId.Radio,
        }
    },
    [PhaseLevelConfig.ConditionType.WEEKLY_RAID_INTRO] = {
        open = {
            PanelId.MiniMap,
            PanelId.MainHud,
            PanelId.Joystick,
            PanelId.InteractOption,
            PanelId.FacBuildingInteract,
            PanelId.WeeklyRaidTaskTrackHud,
            PanelId.HeadBar,
            PanelId.BattleDamageText,
            PanelId.MissionHud,
            PanelId.RadioEmpty,
            PanelId.GeneralAbility,
            PanelId.CommonItemToast,
            PanelId.CommonNewToast,
        },
        preload = {
            PanelId.WalletBar,
            PanelId.ControllerHint,
            PanelId.CommonPopUp,
            PanelId.Guide,
            PanelId.GuideLimited,
        },
        specialPanels = {
            PanelId.GeneralTracker,
            PanelId.Radio,
        }
    },
}

function PhaseLevelConfig.GetCurrentConfig()
    for conditionType, conditionFunc in pairs(ConditionFunctions) do
        if conditionFunc() then
            return Configurations[conditionType] or DefaultConfig
        end
    end
    return DefaultConfig
end

function PhaseLevelConfig.GetWhitelistPanelSet(config)
    local set = {}
    for _, pid in ipairs(config.open or {}) do set[pid] = true end
    for _, pid in ipairs(config.preload or {}) do set[pid] = true end
    for _, pid in ipairs(config.preOpen or {}) do set[pid] = true end
    for _, pid in ipairs(config.specialPanels or {}) do set[pid] = true end
    return set
end


local ConfigMetatable = {
    __index = function(t, key)
        return rawget(Configurations, key) or DefaultConfig
    end,
}

PhaseLevelConfig.Config = setmetatable({}, ConfigMetatable)

_G.PhaseLevelConfig = PhaseLevelConfig
return PhaseLevelConfig