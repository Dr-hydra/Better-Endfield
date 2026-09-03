local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

WeeklyRaidDelegateListCell = HL.Class('WeeklyRaidDelegateListCell', UIWidgetBase)

WeeklyRaidDelegateListCell.m_onClick = HL.Field(HL.Function)

WeeklyRaidDelegateListCell.m_genDifficultyCells = HL.Field(HL.Forward("UIListCache"))

WeeklyRaidDelegateListCell.m_genRewardCells = HL.Field(HL.Forward("UIListCache"))

WeeklyRaidDelegateListCell.m_genRewardBgCells = HL.Field(HL.Forward("UIListCache"))


WeeklyRaidDelegateListCell._OnFirstTimeInit = HL.Override() << function(self)
    self.view.button.onClick:RemoveAllListeners()
    self.view.button.onClick:AddListener(function()
        if self.m_onClick then
            self.m_onClick()
        end
    end)

    self.m_genDifficultyCells = UIUtils.genCellCache(self.view.level)
    self.m_genRewardCells = UIUtils.genCellCache(self.view.itemSmallBlack)
    self.m_genRewardBgCells = UIUtils.genCellCache(self.view.itemBgCell)
end

WeeklyRaidDelegateListCell.InitWeeklyRaidDelegateListCell = HL.Method(HL.String, HL.String, HL.Boolean, HL.Function) << function(self, gameId, missionId, selected, onclick)
    self:_FirstTimeInit()

    self.m_onClick = onclick

    if self.view.currentTrackingNode.gameObject.activeSelf and missionId ~= GameInstance.player.weekRaidSystem.currentPinMission then
        self.view.currentTrackingNode:PlayOutAnimation(function()
            self.view.currentTrackingNode.gameObject:SetActive(missionId == GameInstance.player.weekRaidSystem.currentPinMission)
        end)
    else
        self.view.currentTrackingNode.gameObject:SetActive(missionId == GameInstance.player.weekRaidSystem.currentPinMission)
    end

    if selected and self.view.button.isNaviTarget == false and NotNull(self.view.gameObject) then
        self:SetNaviTarget(self.view.button)
    end
    self.view.uiState:SetState(selected and 'ContentSelect' or 'ContentNormal')

    if string.isEmpty(gameId) or string.isEmpty(missionId) then
        self.view.uiState:SetState('Empty')
        self.view.uiState:SetState(selected and 'AddSelect' or 'AddNormal')
        self.view.redDot.gameObject:SetActive(false)
        return
    end

    self.view.uiState:SetState('Normal')

    local _, config = Tables.weekRaidDelegateTable:TryGetValue(missionId)

    if config == nil then
        logger.error("WeeklyRaidDelegateListCell.InitWeeklyRaidDelegateListCell 失败！因为没有找到对应的配置数据！missionId : " .. missionId)
        self.view.uiState:SetState('Empty')
        return
    end

    local _, rewardCfg = Tables.rewardTable:TryGetValue(config.rewardId)
    if not rewardCfg then
        logger.error("WeeklyRaidDelegateListCell.InitWeeklyRaidDelegateListCell 失败！因为没有找到对应的奖励配置数据！missionId : " .. missionId)
        return
    end
    local textData = WeeklyRaidUtils.GetWeeklyRaidMissionText(missionId)
    self.view.name.text = textData.name
    self.view.desc.text = config.typeDesc

    self.m_genDifficultyCells:Refresh(config.difficulty, function(cell,luaIndex)
        cell.stateController:SetState(selected and 'Select' or 'Normal')
    end)

    self.m_genRewardCells:Refresh(#rewardCfg.itemBundles, function(cell, luaIndex)
        cell:InitItem(rewardCfg.itemBundles[CSIndex(luaIndex)], true)
    end)
    self.m_genRewardBgCells:Refresh(#rewardCfg.itemBundles)


    self.view.uiState:SetState(config.weekRaidMissionType:ToString())
    if config.weekRaidMissionType ~= GEnums.WeekRaidMissionType.MainMission then

        self.view.uiState:SetState("OtherEntrust")
    end



    local missionData = GameInstance.player.mission:GetMissionData(config.missionId)
    if missionData then
        self.view.uiState:SetState(missionData.missionState == CS.Beyond.Gameplay.MissionSystem.MissionState.Completed and 'EntrustFinish' or 'NoHint')
    end



    if config.weekRaidMissionType == GEnums.WeekRaidMissionType.MainMission and not GameInstance.player.weekRaidSystem.IsCurrentMainMissionDependentCompleted then
        self.view.uiState:SetState("EntrustUnFinished")
    end

    self.view.redDot:InitRedDot("WeekRaidDelegate", {
        missionId = missionId
    })

end

HL.Commit(WeeklyRaidDelegateListCell)
return WeeklyRaidDelegateListCell

