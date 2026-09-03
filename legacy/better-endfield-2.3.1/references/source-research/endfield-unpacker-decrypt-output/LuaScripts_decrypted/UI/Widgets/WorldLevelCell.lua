local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

WorldLevelCell = HL.Class('WorldLevelCell', UIWidgetBase)


WorldLevelCell._OnFirstTimeInit = HL.Override() << function(self)

end

WorldLevelCell.InitWorldLevelCell = HL.Method() << function(self)
    self:_FirstTimeInit()

    self.view.exploreBtn.onClick:RemoveAllListeners()
    self.view.exploreBtn.onClick:AddListener(function()
        if GameInstance.player.adventure.currentMaxWorldLevel == 1 then

            PhaseManager:OpenPhase(PhaseId.WorldLevelPopup, { isTipsMode = true })
            return
        end
        PhaseManager:OpenPhase(PhaseId.WorldLevelPopup)
    end)

    self.view.moreBtn.onClick:RemoveAllListeners()
    self.view.moreBtn.onClick:AddListener(function()
        self:OnClickMoreBtn()
    end)
    self.view.lvTxt.text = string.format("%02d", GameInstance.player.adventure.currentWorldLevel)

    local success, worldLevelData = Tables.adventureWorldLevelTable:TryGetValue(GameInstance.player.adventure.currentMaxWorldLevel + 1)
    if not success or worldLevelData == nil or string.isEmpty(worldLevelData.missionId) then
        self.view.stateController:SetState(GameInstance.player.adventure.isCurWorldLvMax and "Nrl" or "Lower")
        return
    end
    local missionData = GameInstance.player.mission:GetMissionData(worldLevelData.missionId, false)
    if missionData and (missionData.missionState == CS.Beyond.Gameplay.MissionSystem.MissionState.Available or missionData.missionState == CS.Beyond.Gameplay.MissionSystem.MissionState.Processing) then
        self.view.stateController:SetState("Up")
    else
        self.view.stateController:SetState(GameInstance.player.adventure.isCurWorldLvMax and "Nrl" or "Lower")
    end

end

WorldLevelCell.OnClickMoreBtn = HL.Method() << function(self)
    if GameInstance.player.adventure.currentMaxWorldLevel == 1 then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_WORLD_LEVEL_SYSTEM_LOCK)
        return
    end

    local success, worldLevelData = Tables.adventureWorldLevelTable:TryGetValue(GameInstance.player.adventure.currentMaxWorldLevel + 1)
    if not success or worldLevelData == nil or string.isEmpty(worldLevelData.missionId) then
        return
    end
    local missionId = worldLevelData.missionId
    local missionData = GameInstance.player.mission:GetMissionData(missionId, false)
    if missionData and (missionData.missionState == CS.Beyond.Gameplay.MissionSystem.MissionState.Available or missionData.missionState == CS.Beyond.Gameplay.MissionSystem.MissionState.Processing) then
        PhaseManager:OpenPhase(PhaseId.Mission, { autoSelect = missionId })
    end

end

HL.Commit(WorldLevelCell)
return WorldLevelCell

