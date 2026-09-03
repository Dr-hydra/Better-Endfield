
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.WorldLevelTipsPopup
local MISSION_STATE = CS.Beyond.Gameplay.MissionSystem.MissionState

WorldLevelTipsPopupCtrl = HL.Class('WorldLevelTipsPopupCtrl', uiCtrl.UICtrl)






WorldLevelTipsPopupCtrl.s_messages = HL.StaticField(HL.Table) << {

}

WorldLevelTipsPopupCtrl.isTipsMode = HL.Field(HL.Boolean) << false

WorldLevelTipsPopupCtrl._GetMaxConfigWorldLevel = HL.Method().Return(HL.Number) << function(self)
    local maxWorldLevel = math.max(1, GameInstance.player.adventure.currentMaxWorldLevel)
    while true do
        local success = Tables.adventureWorldLevelTable:TryGetValue(maxWorldLevel + 1)
        if not success then
            break
        end
        maxWorldLevel = maxWorldLevel + 1
    end
    return maxWorldLevel
end

WorldLevelTipsPopupCtrl._GetWorldLevelUpgradeMissionId = HL.Method().Return(HL.String) << function(self)
    local success, worldLevelData = Tables.adventureWorldLevelTable:TryGetValue(GameInstance.player.adventure.currentMaxWorldLevel + 1)
    if not success or worldLevelData == nil or string.isEmpty(worldLevelData.missionId) then
        return ""
    end
    local missionData = GameInstance.player.mission:GetMissionData(worldLevelData.missionId, false)
    if missionData and (missionData.missionState == MISSION_STATE.Available or missionData.missionState == MISSION_STATE.Processing) then
        return worldLevelData.missionId
    end
    return ""
end

WorldLevelTipsPopupCtrl._GotoWorldLevelUpgradeMission = HL.Method() << function(self)
    if GameInstance.player.adventure.currentMaxWorldLevel == 1 then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_WORLD_LEVEL_SYSTEM_LOCK)
        return
    end
    local missionId = self:_GetWorldLevelUpgradeMissionId()
    if not string.isEmpty(missionId) then
        PhaseManager:OpenPhase(PhaseId.Mission, { autoSelect = missionId })
    end
end


WorldLevelTipsPopupCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    arg = arg or {}
    self.isTipsMode = arg.isTipsMode == true
    local isTipsMode = self.isTipsMode
    local gotoMissionId = arg.isFromCharUpgrade == true and self:_GetWorldLevelUpgradeMissionId() or ""
    local showGotoBtn = not string.isEmpty(gotoMissionId)
    local currentMaxWorldLevel = GameInstance.player.adventure.currentMaxWorldLevel
    local maxConfigWorldLevel = self:_GetMaxConfigWorldLevel()
    local hasNextWorldLevel = currentMaxWorldLevel < maxConfigWorldLevel
    local showUpgradeBtn = hasNextWorldLevel and not showGotoBtn
    local nextTargetWorldLevel = math.min(currentMaxWorldLevel + 1, maxConfigWorldLevel)
    self.view.closeBtn.onClick:RemoveAllListeners()
    self.view.closeBtn.onClick:AddListener(function()
        self.m_phase:CloseSelf()
    end)

    self.view.bg.onClick:RemoveAllListeners()
    self.view.bg.onClick:AddListener(function()
        self.m_phase:CloseSelf()
    end)

    self.view.confirmBtn.onClick:RemoveAllListeners()
    self.view.confirmBtn.onClick:AddListener(function()
        self.m_phase:OpenWorldLevelPopup()
    end)
    self.view.confirmBtn.gameObject:SetActive(not isTipsMode)

    self.view.gotoBtn.onClick:RemoveAllListeners()
    self.view.gotoBtn.gameObject:SetActive(showGotoBtn)
    if showGotoBtn then
        self.view.gotoBtn.onClick:AddListener(function()
            self:_GotoWorldLevelUpgradeMission()
        end)
    end

    self.view.upgradeBtn.onClick:RemoveAllListeners()
    self.view.upgradeBtn.gameObject:SetActive(showUpgradeBtn)
    self.view.upgradeBtn.onClick:AddListener(function()
        self.m_phase:OpenWorldLevelUp(nextTargetWorldLevel)
    end)
    self.view.upgradeBtn.interactable = showUpgradeBtn

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
end











HL.Commit(WorldLevelTipsPopupCtrl)
