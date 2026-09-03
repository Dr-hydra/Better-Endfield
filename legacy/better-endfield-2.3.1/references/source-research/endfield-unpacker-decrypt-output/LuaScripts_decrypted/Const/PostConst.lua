


Const.BATTLE_MODE_ONLY_PANELS = {
    PanelId.BattleBottomScreenEffect,
    PanelId.BattleAction,
    PanelId.SquadIcon,
    PanelId.OutOfScreenTargets,
    PanelId.BattleBossInfo,
    PanelId.BattleComboSkill,
    PanelId.BattleComboSkillUse,
    PanelId.BattleDamageText,
}

Const.FACTORY_MODE_ONLY_PANELS = {
    PanelId.FacHudBottomMask,
    PanelId.FacMainLeft,
    PanelId.FacMainRight,
    PanelId.FacMain,
}

Const.RESERVE_PANEL_IDS_FOR_FAC_BUILD_MODE = {
    PanelId.FacBuildMode,
    PanelId.FacDestroyMode,

    PanelId.MainHud,
    PanelId.FacHudBottomMask,
    PanelId.Joystick,
    PanelId.LevelCamera,
    PanelId.FacPowerPoleLinkingLabel,
    PanelId.FacPowerPoleTravelHint,
    PanelId.FacPowerPoleAutoConnectHint,
    PanelId.HeadLabel,
    PanelId.FacMiniPowerHud,
    PanelId.FacTopView,
    PanelId.Radio,
    PanelId.FacTopViewBuildingInfo,
}

Const.RESERVE_PANEL_IDS_FOR_FAC_DESTROY_MODE = {
    PanelId.FacDestroyMode,
    PanelId.FacBuildMode,

    PanelId.MainHud,
    PanelId.FacHudBottomMask,
    PanelId.LevelCamera,
    PanelId.FacBuildingInteract,
    PanelId.InteractOption,
    PanelId.Joystick,
    PanelId.FacPowerPoleLinkingLabel,
    PanelId.FacPowerPoleTravelHint,
    PanelId.FacPowerPoleAutoConnectHint,
    PanelId.HeadLabel,
    PanelId.FacTopView,
    PanelId.FacTopViewBuildingInfo,
    PanelId.FacSaveBlueprint,
}

do
    local tmp = {}
    for _, id in ipairs(Const.RESERVE_PANEL_IDS_FOR_FAC_BUILD_MODE) do
        if id ~= PanelId.MainHud then
            tmp[id] = true
        end
    end
    for _, id in ipairs(Const.RESERVE_PANEL_IDS_FOR_FAC_DESTROY_MODE) do
        if id ~= PanelId.MainHud then
            tmp[id] = true
        end
    end
    Const.ALL_RESERVE_PANEL_IDS_FOR_FAC_MODE_IN_TOP_VIEW = {}
    for id, _ in pairs(tmp) do
        table.insert(Const.ALL_RESERVE_PANEL_IDS_FOR_FAC_MODE_IN_TOP_VIEW, id)
    end
end
