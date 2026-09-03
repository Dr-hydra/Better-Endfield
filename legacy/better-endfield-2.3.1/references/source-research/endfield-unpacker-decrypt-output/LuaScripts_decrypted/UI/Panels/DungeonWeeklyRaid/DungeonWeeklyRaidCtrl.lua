local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.DungeonWeeklyRaid

local OPTIONAL_TEXT_COLOR = "C7EC59"
local tabConfig = WeeklyRaidUtils.TabConfig

DungeonWeeklyRaidCtrl = HL.Class('DungeonWeeklyRaidCtrl', uiCtrl.UICtrl)






DungeonWeeklyRaidCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_WEEK_RAID_MISSION_REFRESH] = '_OnSelectCellChange',
    [MessageConst.ON_WEEK_RAID_MISSION_PIN_CHANGE] = '_OnWeeklyRaidDelegateCellChange',
    [MessageConst.ON_WEEK_RAID_TECH_MODIFY] = '_UpdateTech',
}

DungeonWeeklyRaidCtrl.m_currentTab = HL.Field(HL.Number) << WeeklyRaidUtils.DungeonWeeklyRaidType.Normal

DungeonWeeklyRaidCtrl.m_genTabCells = HL.Field(HL.Forward("UIListCache"))

DungeonWeeklyRaidCtrl.m_genLevelCells = HL.Field(HL.Forward("UIListCache"))

DungeonWeeklyRaidCtrl.m_genObjectiveCells = HL.Field(HL.Forward("UIListCache"))

DungeonWeeklyRaidCtrl.m_genRewardCells = HL.Field(HL.Forward("UIListCache"))

DungeonWeeklyRaidCtrl.m_genRewardBgCells = HL.Field(HL.Forward("UIListCache"))

DungeonWeeklyRaidCtrl.m_genTechCells = HL.Field(HL.Forward("UIListCache"))

DungeonWeeklyRaidCtrl.m_cacheDelegate = HL.Field(HL.Any)

DungeonWeeklyRaidCtrl.m_selectIndex = HL.Field(HL.Number) << 0

DungeonWeeklyRaidCtrl.m_getCell = HL.Field(HL.Function)

DungeonWeeklyRaidCtrl.m_isPreview = HL.Field(HL.Boolean) << false


DungeonWeeklyRaidCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.btnClose.onClick:RemoveAllListeners()
    self.view.btnClose.onClick:AddListener(function()
        self.m_phase:TryCloseTopPanel()
    end)

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })

    local _, refreshCfg = Tables.weekRaidRefreshTable:TryGetValue(GameInstance.player.weekRaidSystem.gameId)
    if refreshCfg == nil then
        logger.error("DungeonWeeklyRaidCtrl.OnCreate: Failed to get week raid refresh config for gameId: " .. GameInstance.player.weekRaidSystem.gameId)
        return
    end

    local refreshAction = function()
        local id = ""
        if self.m_currentTab == WeeklyRaidUtils.DungeonWeeklyRaidType.Week and self.m_selectIndex < self.m_cacheDelegate.Count then
            id = self.m_cacheDelegate[self.m_selectIndex]
        end

        local curGold = Utils.getItemCount(refreshCfg.costGoldId, true)
        local costNum = 0
        local levelMap = Tables.weekRaidRefreshTable[GameInstance.player.weekRaidSystem.gameId].levelMap
        if #levelMap < GameInstance.player.weekRaidSystem.refreshCount + 1 then
            costNum = levelMap[#levelMap].costNum
        else
            costNum = levelMap[GameInstance.player.weekRaidSystem.refreshCount + 1].costNum
        end
        if curGold < costNum then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_WEEKLY_RAID_NOT_ENOUGH_CURRENCY)
            return
        end
        GameInstance.player.weekRaidSystem:WeekRaidRefreshMission(id)
    end

    self.view.contentRight.refreshBtn.onClick:RemoveAllListeners()
    self.view.contentRight.refreshBtn.onClick:AddListener(function()
        refreshAction()
    end)

    self.view.contentRight.addBtn.onClick:RemoveAllListeners()
    self.view.contentRight.addBtn.onClick:AddListener(function()
        refreshAction()
    end)

    self.view.contentRight.needEntrustBtn.onClick:RemoveAllListeners()
    self.view.contentRight.needEntrustBtn.onClick:AddListener(function()
        PhaseManager:OpenPhase(PhaseId.Mission, { autoSelect = GameInstance.player.weekRaidSystem.currentMainMissionDependentId })
    end)

    self.view.detailBtn.onClick:RemoveAllListeners()
    self.view.detailBtn.onClick:AddListener(function()
        UIManager:Open(PanelId.RaidTechPopup)
    end)

    self.view.leftBottom.onClick:RemoveAllListeners()
    self.view.leftBottom.onClick:AddListener(function()
        UIManager:Open(PanelId.RaidTechPopup)
    end)

    self.view.contentRight.finishBtn.onClick:RemoveAllListeners()
    self.view.contentRight.finishBtn.onClick:AddListener(function()
        local delegateId = self.m_cacheDelegate[self.m_selectIndex]
        local cfg = Tables.weekRaidDelegateTable[delegateId]
        if cfg.weekRaidMissionType == GEnums.WeekRaidMissionType.MainMission then

            return
        end

        local allCompleted = GameInstance.player.mission:GetQuestState(cfg.questId) == CS.Beyond.Gameplay.MissionSystem.QuestState.Completed
        local questInfo = GameInstance.player.mission:GetQuestInfo(cfg.questId)

        if allCompleted == false then
            allCompleted = true
            for i = 0, questInfo.objectiveList.Count - 1 do
                local objective = questInfo.objectiveList[i]
                allCompleted = allCompleted and objective.isCompleted
            end
        end

        if allCompleted then
            local submitId = cfg.submitId
            GameInstance.player.weekRaidSystem:SendCompletedWeekRaid(cfg.missionId, cfg.submitQuestId, submitId)
        else
            logger.warn("DungeonWeeklyRaidCtrl.OnCreate: Attempting to finish an uncompleted mission, delegateId: " .. tostring(delegateId))
        end
    end)

    self.view.contentRight.pinBtn.onClick:RemoveAllListeners()
    self.view.contentRight.pinBtn.onClick:AddListener(function()
        if self.m_selectIndex >= self.m_cacheDelegate.Count then
            logger.error("DungeonWeeklyRaidCtrl.OnCreate: Cannot pin a non-existent mission")
            return
        end
        local delegateId = self.m_cacheDelegate[self.m_selectIndex]
        if GameInstance.player.weekRaidSystem.currentPinMission == delegateId then
            GameInstance.player.weekRaidSystem:WeekRaidSetPinMission("")
        else
            GameInstance.player.weekRaidSystem:WeekRaidSetPinMission(delegateId)
        end
    end)

    self.view.contentRight.entrustConditionListNaviGroup.onIsFocusedChange:RemoveAllListeners()
    self.view.contentRight.entrustConditionListNaviGroup.onIsFocusedChange:AddListener(function(isFocused)
        if not isFocused then
            Notify(MessageConst.HIDE_CONTROLLER_NAVI_TEXT_HINT)
        end
    end)

    self.m_isPreview = arg ~= nil and arg.isPreview == true
    if self.m_isPreview then
        self.view.contentRight.finishAreaState:SetState("UnderWay")
    end

    self.view.moneyCell:InitMoneyCell(refreshCfg.costGoldId)

    self.m_genLevelCells = UIUtils.genCellCache(self.view.contentRight.levelIconCell)
    self.m_genObjectiveCells = UIUtils.genCellCache(self.view.contentRight.objectiveCell)
    self.m_genRewardCells = UIUtils.genCellCache(self.view.contentRight.rewardCell)
    self.m_genRewardBgCells = UIUtils.genCellCache(self.view.contentRight.itemBgCell)
    self.m_genTechCells = UIUtils.genCellCache(self.view.techCell)



    self.m_getCell = UIUtils.genCachedCellFunction(self.view.scrollListLeft)
    self.view.scrollList.onUpdateCell:RemoveAllListeners()
    self.view.scrollList.onUpdateCell:AddListener(function(object, csIndex)
        local cell = self.m_getCell(object)
        if csIndex < 0 or csIndex >= self.m_cacheDelegate.Count then

            cell:InitWeeklyRaidDelegateListCell("", "", self.m_selectIndex == csIndex, function()
                self.m_selectIndex = csIndex
                self:_OnSelectCellChange()
            end)
            return
        end
        local delegateId = self.m_cacheDelegate[csIndex]
        cell.gameObject.name = "DelegateCell_" .. delegateId

        if string.isEmpty(delegateId) then
            cell:InitWeeklyRaidDelegateListCell("", "", self.m_selectIndex == csIndex, function()
                self.m_selectIndex = csIndex
                self:_OnSelectCellChange()
            end)
            return
        end

        local _, delegateCfg = Tables.weekRaidDelegateTable:TryGetValue(delegateId)
        if delegateCfg then

            cell:InitWeeklyRaidDelegateListCell(delegateCfg.gameId, delegateCfg.missionId, self.m_selectIndex == csIndex, function()
                self.m_selectIndex = csIndex
                self:_OnSelectCellChange()
            end)
        else
            logger.error("DungeonWeeklyRaidCtrl.OnCreate: Invalid delegateId: " .. tostring(delegateId))
        end
    end)

    local weekRaidCfg = Tables.weekRaidTable:GetValue(GameInstance.player.weekRaidSystem.gameId)
    local tabCount = #tabConfig

    local randomMissionState = GameInstance.player.mission:GetMissionState(weekRaidCfg.unlockRandomStageMission)
    if randomMissionState ~= CS.Beyond.Gameplay.MissionSystem.MissionState.Completed then
        tabCount = 1
    end

    local recoverState = arg and arg.recoverState or nil
    local targetTabIndex = self:_GetDefaultTabIndex()
    if recoverState and type(recoverState.tabIndex) == "number" and recoverState.tabIndex >= 1 and recoverState.tabIndex <= tabCount then
        targetTabIndex = recoverState.tabIndex
    end
    self.m_currentTab = targetTabIndex

    self.view.hintRoot.gameObject:SetActiveIfNecessary(tabCount > 1)
    self.m_genTabCells = UIUtils.genCellCache(self.view.topTabToggleCell)
    self.m_genTabCells:Refresh(tabCount, function(cell, luaIndex)
        cell.gameObject.name = "TabCell_" .. tostring(luaIndex)
        cell.toggle.onValueChanged:RemoveAllListeners()
        cell.toggle.onValueChanged:AddListener(function(isOn)
            if isOn then
                self.m_currentTab = luaIndex
                self:_OnTabChange()
            end
        end)
        local isOn = luaIndex == self.m_currentTab
        if isOn == cell.toggle.isOn then
            self:_OnTabChange()
        else
            cell.toggle.isOn = isOn
        end
        cell.tabCellName.text = Language[tabConfig[luaIndex].title]
        cell.tabCellIcon:LoadSprite(UIConst.UI_SPRITE_WEEKLY_RAID, tabConfig[luaIndex].icon)
        cell.redDot:InitRedDot("WeekRaidDelegate", {
            missionType = luaIndex
        })
        cell.cellSelectColor.color = UIUtils.getColorByString(tabConfig[luaIndex].color)
    end)


    self:_UpdateTech(nil)

    if recoverState ~= nil then
        self:_RecoverState(recoverState)
        arg.recoverState = nil
    end
end

DungeonWeeklyRaidCtrl._GetDefaultTabIndex = HL.Method().Return(HL.Number) << function(self)
    for i = 1, #tabConfig do
        local delegates = tabConfig[i].getDelegate()
        if delegates and delegates.Count > 0 then
            return i
        end
    end
    return WeeklyRaidUtils.DungeonWeeklyRaidType.Normal
end

DungeonWeeklyRaidCtrl._GetRecoverSelectIndex = HL.Method(HL.Table).Return(HL.Number) << function(self, recoverState)
    if recoverState == nil then
        return 0
    end

    local recoverSelectIndex = type(recoverState.selectIndex) == "number" and recoverState.selectIndex or 0
    local selectedDelegateId = recoverState.selectedDelegateId
    if not string.isEmpty(selectedDelegateId) then
        for i = 0, self.m_cacheDelegate.Count - 1 do
            if self.m_cacheDelegate[i] == selectedDelegateId then
                recoverSelectIndex = i
                break
            end
        end
    end

    local currentCellCount = self:_GetCurrentCellCount()
    if recoverSelectIndex < 0 then
        return 0
    end
    if recoverSelectIndex >= currentCellCount then
        return math.max(0, currentCellCount - 1)
    end
    return recoverSelectIndex
end

DungeonWeeklyRaidCtrl._RecoverState = HL.Method(HL.Table) << function(self, recoverState)
    self.m_selectIndex = self:_GetRecoverSelectIndex(recoverState)
    self:_OnSelectCellChange()
    if self:_GetCurrentCellCount() > 0 then
        self.view.scrollList:ScrollToIndex(self.m_selectIndex)
    end
end

DungeonWeeklyRaidCtrl._UpdateTech = HL.Method(HL.Opt(HL.Table)) << function(self, args)
    local showTypeTable = {}
    local biasIndex = 0
    self.m_genTechCells:Refresh(WeeklyRaidUtils.ShowTechCount, function(cell, luaIndex)
        local csIndex = CSIndex(luaIndex)
        cell.gameObject.name = "TechCell_" .. tostring(luaIndex)
        cell.button.onHoverChange:RemoveAllListeners()
        cell.button.onClick:RemoveAllListeners()
        cell.button.onClick:AddListener(function()

            UIManager:Open(PanelId.RaidTechPopup)
        end)
        if csIndex + biasIndex < GameInstance.player.weekRaidSystem.unlockedTechIds.Count then
            local techId = GameInstance.player.weekRaidSystem.unlockedTechIds[csIndex + biasIndex]
            local _, cfg = Tables.weekRaidTechTable:TryGetValue(techId)
            if not cfg then
                logger.error("DungeonWeeklyRaidCtrl.OnCreate: Invalid techId: " .. tostring(techId))
                cell.stateController:SetState('Empty')
                return
            end

            while true do
                techId = GameInstance.player.weekRaidSystem.unlockedTechIds[csIndex + biasIndex]
                _, cfg = Tables.weekRaidTechTable:TryGetValue(techId)
                if not cfg then
                    logger.error("DungeonWeeklyRaidCtrl.OnCreate: Invalid techId: " .. tostring(techId))
                    cell.stateController:SetState('Empty')
                    return
                end
                if not showTypeTable[cfg.techType] or cfg.techType == GEnums.WeekRaidTechType.Buff then
                    showTypeTable[cfg.techType] = true
                    break
                else
                    biasIndex = biasIndex + 1
                    if csIndex + biasIndex >= GameInstance.player.weekRaidSystem.unlockedTechIds.Count then
                        cell.stateController:SetState('Empty')
                        return
                    end
                end
            end

            cell.stateController:SetState('Normal')
            cell.attributeItemIcon:LoadSprite(UIConst.UI_SPRITE_ITEM, cfg.techTypeData.icon)
            cell.button.onHoverChange:AddListener(function(isHover)
                if isHover then
                    Notify(MessageConst.SHOW_COMMON_HOVER_TIP, {
                        mainText = cfg.techTypeData.name,
                        delay = self.view.config.TECH_HOVER_DELAY,
                    })
                else
                    Notify(MessageConst.HIDE_COMMON_HOVER_TIP)
                end
            end)
        else
            cell.stateController:SetState('Empty')
        end
    end)
end

DungeonWeeklyRaidCtrl._OnWeeklyRaidDelegateCellChange = HL.Method() << function(self)

    for i = 0, self.m_cacheDelegate.Count - 1 do
        local delegateId = self.m_cacheDelegate[i]
        if delegateId == GameInstance.player.weekRaidSystem.currentPinMission then
            self.m_selectIndex = i
            break
        end
    end
    self:_OnSelectCellChange()
end

DungeonWeeklyRaidCtrl._OnSelectCellChange = HL.Method() << function(self)

    if self.m_selectIndex < 0 or self.m_selectIndex >= self:_GetCurrentCellCount() then
        self.m_selectIndex = 0
    end

    self.view.scrollList:UpdateCount(self:_GetCurrentCellCount())
    self:_UpdateDetailInfo()
end

DungeonWeeklyRaidCtrl._GetCurrentCellCount = HL.Method().Return(HL.Number) << function(self)
    if self.m_currentTab == WeeklyRaidUtils.DungeonWeeklyRaidType.Week and self.m_isPreview ~= true and self.m_cacheDelegate.Count ~= WeeklyRaidUtils.MAX_WEEKLY_ENTRUST_COUNT then
        return self.m_cacheDelegate.Count + 1
    else
        return self.m_cacheDelegate.Count
    end
end

DungeonWeeklyRaidCtrl._OnTabChange = HL.Method() << function(self)
    self.view.contentLeftState:SetState(self.m_currentTab == WeeklyRaidUtils.DungeonWeeklyRaidType.Week and 'IsWeekEntrust' or 'NotWeekEntrust')

    if self.m_currentTab == WeeklyRaidUtils.DungeonWeeklyRaidType.Week then
        self.view.countDownText:InitCountDownText(Utils.getNextWeeklyServerRefreshTime(), nil , function(sec)
            return string.format(Language.LUA_WEEKLY_RAID_DELEGATE_REFRESH_TIME, UIUtils.getLeftTime(sec))
        end)
    end


    self.m_selectIndex = 0

    self.m_cacheDelegate = tabConfig[self.m_currentTab].getDelegate()

    self.view.contentLeftState:SetState(self:_GetCurrentCellCount() > 0 and 'Normal' or 'Empty')

    self.view.scrollList:UpdateCount(self:_GetCurrentCellCount())


    self:_UpdateDetailInfo()
end

DungeonWeeklyRaidCtrl.GetRecoverStateArg = HL.Method().Return(HL.Table) << function(self)
    local recoverState = {
        tabIndex = self.m_currentTab,
        selectIndex = self.m_selectIndex,
    }
    if self.m_cacheDelegate and self.m_selectIndex >= 0 and self.m_selectIndex < self.m_cacheDelegate.Count then
        recoverState.selectedDelegateId = self.m_cacheDelegate[self.m_selectIndex]
    end
    return recoverState
end

DungeonWeeklyRaidCtrl._SetFinishAreaState = HL.Method(HL.String) << function(self, state)
    if self.m_isPreview then
        return
    end
    self.view.contentRight.finishAreaState:SetState(state)
end

DungeonWeeklyRaidCtrl._UpdateDetailInfo = HL.Method() << function(self)
    local missionNode = self.view.contentRight

    missionNode.detailNullState:SetState('UnderWay')

    self.view.entrustOverviewNumber.text = string.format("%d/%d", self.m_cacheDelegate.Count, WeeklyRaidUtils.MAX_WEEKLY_ENTRUST_COUNT)

    if self.m_currentTab == WeeklyRaidUtils.DungeonWeeklyRaidType.Week then

        local levelMap = Tables.weekRaidRefreshTable[GameInstance.player.weekRaidSystem.gameId].levelMap
        local costNum = 0
        if #levelMap < GameInstance.player.weekRaidSystem.refreshCount + 1 then
            costNum = levelMap[#levelMap].costNum
        else
            costNum = levelMap[GameInstance.player.weekRaidSystem.refreshCount + 1].costNum
        end
        missionNode.currencyNumber.text = tostring(costNum)

        local refreshCfg = Tables.weekRaidRefreshTable:GetValue(GameInstance.player.weekRaidSystem.gameId)
        local curGold = Utils.getItemCount(refreshCfg.costGoldId, true)
        missionNode.currencyNumber.color = curGold < costNum
            and self.view.config.NOT_ENOUGH_CURRENCY_COLOR or self.view.config.ENOUGH_CURRENCY_COLOR
        missionNode.needCurrencyStateStateController:SetState('RefreshEntrust')

        missionNode.countDownTextLuaUIWidget:InitCountDownText(Utils.getNextWeeklyServerRefreshTime(), nil, function(sec)
            return string.format(Language.LUA_WEEKLY_RAID_DELEGATE_REFRESH_TIME, UIUtils.getLeftTime(sec))
        end)
    end



    if self.m_selectIndex < 0 or self.m_selectIndex >= self.m_cacheDelegate.Count then

        if self.m_currentTab == WeeklyRaidUtils.DungeonWeeklyRaidType.Week and self.m_selectIndex == self.m_cacheDelegate.Count then
            missionNode.detailNullState:SetState('NewEntrust')
            missionNode.countDownText:InitCountDownText(Utils.getNextWeeklyServerRefreshTime())
            missionNode.contentRightState:SetState(self.m_isPreview and 'UnderWay' or 'Empty')
            missionNode.needCurrencyStateStateController:SetState('GetEntrust')
            self:_SetFinishAreaState('AddEntrust')
            return
        end
        missionNode.contentRightState:SetState(self.m_isPreview and 'UnderWay' or 'Empty')
        self:_SetFinishAreaState('Null')
        return
    end

    local delegateId = self.m_cacheDelegate[self.m_selectIndex]
    if string.isEmpty(delegateId) then
        missionNode.contentRightState:SetState(self.m_isPreview and 'UnderWay' or 'Empty')
        self:_SetFinishAreaState('Null')
        logger.error("DungeonWeeklyRaidCtrl._UpdateDetailInfo: Invalid delegateId at index: " .. tostring(self.m_selectIndex))
        return
    end

    local success, delegateCfg = Tables.weekRaidDelegateTable:TryGetValue(delegateId)
    if not success then
        logger.error("DungeonWeeklyRaidCtrl._UpdateDetailInfo: Invalid delegateId: " .. tostring(delegateId))
        missionNode.contentRightState:SetState(self.m_isPreview and 'UnderWay' or 'Empty')
    end


    missionNode.contentRightState:SetState(self.m_currentTab == WeeklyRaidUtils.DungeonWeeklyRaidType.Week and 'WeekContent' or 'NormalContent')


    local textData = WeeklyRaidUtils.GetWeeklyRaidMissionText(delegateId)
    missionNode.nameText.text = textData.name
    missionNode.typeDescText.text = delegateCfg.typeDesc
    missionNode.descText.text = textData.desc

    self.m_genLevelCells:Refresh(delegateCfg.difficulty)

    local questInfo = nil
    if delegateCfg.weekRaidMissionType == GEnums.WeekRaidMissionType.MainMission then
        local list = GameInstance.player.mission:GetMissionData(delegateCfg.missionId):GetProcessingQuests()
        if list.Count ~= 1 then
            logger.error("DungeonWeeklyRaidCtrl.InitWeeklyRaidDelegateListCell 失败！因为主线任务的Quest不止一个！")
            return
        end
        questInfo = GameInstance.player.mission:GetQuestInfo(list[0])
    else
        questInfo = GameInstance.player.mission:GetQuestInfo(delegateCfg.questId)
    end
    local questCompleted = GameInstance.player.mission:GetQuestState(delegateCfg.questId) == CS.Beyond.Gameplay.MissionSystem.QuestState.Completed
    local optional = questInfo.optional

    local allObjectivesCompleted = true

    self.m_genObjectiveCells:Refresh(questInfo.objectiveList.Count, function(objectiveCell, objectiveLuaIdx)
        local objective = questInfo.objectiveList[CSIndex(objectiveLuaIdx)]
        local type = objective.condition:GetType()
        local cfg = WeeklyRaidUtils.DelegateObjectiveConfig[type]
        if cfg ~= nil then
            local itemId = cfg.GetItemId(objective.condition)
            objectiveCell.desc:SetAndResolveTextStyle(WeeklyRaidUtils.DelegateObjectiveConfig[objective.condition:GetType()].GetObjectiveDesc(objective.condition))
            objectiveCell.desc.onClickLink:RemoveAllListeners()
            objectiveCell.desc.onClickLink:AddListener(function(linkId)
                Notify(MessageConst.SHOW_ITEM_TIPS, {
                    transform = self.view.contentRight.descText.transform,
                    itemId = itemId,
                    posType = UIConst.UI_TIPS_POS_TYPE.LeftTop,
                })
            end)
        else
            if not objective.useStrDesc then
                if optional then
                    local tempText = string.format("<color=#%s>%s</color> %s", OPTIONAL_TEXT_COLOR, Language.ui_optional_quest, objective.runtimeDescription:GetText())
                    objectiveCell.desc:SetAndResolveTextStyle(tempText)
                else
                    objectiveCell.desc:SetAndResolveTextStyle(objective.runtimeDescription:GetText())
                end
            else
                objectiveCell.desc:SetAndResolveTextStyle(objective.descStr)
            end
        end

        if objective.isShowProgress then
            objectiveCell.progress.gameObject:SetActive(true)
            if questCompleted or objective.isCompleted then
                if cfg ~= nil then
                    local itemId = cfg.GetItemId(objective.condition)
                    local count = WeeklyRaidUtils.GetWeekRaidItemCount(itemId)
                    objectiveCell.progress.text = string.format("%d/%d", count, objective.progressToCompareForShow)
                else
                    objectiveCell.progress.text = string.format("%d/%d", objective.progressToCompareForShow, objective.progressToCompareForShow)
                end
            else
                objectiveCell.progress.text = string.format("%d/%d", objective.progressForShow, objective.progressToCompareForShow)
            end
        else
            objectiveCell.progress.gameObject:SetActive(false)
        end





        objectiveCell.desc.color = self.view.config.OBJECTIVE_NORMAL_FONT_COLOR
        objectiveCell.progress.color = self.view.config.OBJECTIVE_NORMAL_FONT_COLOR


        allObjectivesCompleted = allObjectivesCompleted and objective.isCompleted



        objectiveCell.inputBindingGroupNaviDecorator.enableControllerNavi = objectiveCell.desc.textInfo.linkCount > 0
        objectiveCell.stateController:SetState(objective.isCompleted and 'Complete' or 'Normal')
        objectiveCell.inputBindingGroupNaviDecorator.onGroupSetAsNaviTarget:RemoveAllListeners()
        objectiveCell.inputBindingGroupNaviDecorator.onGroupSetAsNaviTarget:AddListener(function(select)
            if select then

                local itemId = cfg.GetItemId(objective.condition)
                local linkData = objectiveCell.desc.textInfo.linkInfo[0]
                local arg = {
                    uiText = objectiveCell.desc,
                    startCharIndex = linkData.linkTextfirstCharacterIndex - 1,
                    endCharIndex = linkData.linkTextfirstCharacterIndex + linkData.linkTextLength - 1,
                }
                Notify(MessageConst.SHOW_CONTROLLER_NAVI_TEXT_HINT, arg)
                Notify(MessageConst.SHOW_ITEM_TIPS, {
                    transform = self.view.contentRight.descText.transform,
                    itemId = itemId,
                    posType = UIConst.UI_TIPS_POS_TYPE.LeftTop,
                    isSideTips = true,
                })
            end
        end)

    end)


    local _, rewardCfg = Tables.rewardTable:TryGetValue(delegateCfg.rewardId)
    if not rewardCfg then
        logger.error("WeeklyRaidDelegateListCell.InitWeeklyRaidDelegateListCell 失败！因为没有找到对应的奖励配置数据！")
        return
    end
    self.m_genRewardCells:Refresh(#rewardCfg.itemBundles, function(cell, luaIndex)
        cell:InitItem(rewardCfg.itemBundles[CSIndex(luaIndex)], true)
        cell:SetExtraInfo({ isSideTips = DeviceInfo.usingController })
    end)
    self.m_genRewardBgCells:Refresh(#rewardCfg.itemBundles)


    local isSubmit = delegateCfg.isSubmit
    if not allObjectivesCompleted then
        missionNode.finishBtnState:SetState('UnFinished')
        missionNode.finishBtn.interactable = false
    else
        missionNode.finishBtnState:SetState(isSubmit and 'FinishAndSubmit' or 'Finish')
        missionNode.finishBtn.interactable = true
    end
    self:_SetFinishAreaState(self.m_currentTab == WeeklyRaidUtils.DungeonWeeklyRaidType.Week and 'WeekFinish' or 'NormalFinish')

    missionNode.pinBtnState:SetState(GameInstance.player.weekRaidSystem.currentPinMission == delegateId and 'Pin' or 'UnPin')

    missionNode.contentRightState:SetState(delegateCfg.weekRaidMissionType == GEnums.WeekRaidMissionType.MainMission and 'MainMission' or 'OtherMission')
    if delegateCfg.weekRaidMissionType == GEnums.WeekRaidMissionType.MainMission then

        if not GameInstance.player.weekRaidSystem.IsCurrentMainMissionDependentCompleted then
            missionNode.finishAreaState:SetState("NeedEntrustFinish")
            local missionInfo = GameInstance.player.mission:GetMissionInfo(GameInstance.player.weekRaidSystem.currentMainMissionDependentId)
            missionNode.needEntrustTxt.text = string.format(Language.LUA_WEEKLY_RAID_NEED_ENTRUST, missionInfo.missionName:GetText())
        end
    end

    Notify(MessageConst.REFRESH_CONTROLLER_HINT)
end

DungeonWeeklyRaidCtrl.OnShow = HL.Override() << function(self)
    self.view.scrollListNaviGroup:NaviToThisGroup()
end


DungeonWeeklyRaidCtrl.OnClose = HL.Override() << function(self)
    Notify(MessageConst.HIDE_CONTROLLER_NAVI_TEXT_HINT)
end




HL.Commit(DungeonWeeklyRaidCtrl)
