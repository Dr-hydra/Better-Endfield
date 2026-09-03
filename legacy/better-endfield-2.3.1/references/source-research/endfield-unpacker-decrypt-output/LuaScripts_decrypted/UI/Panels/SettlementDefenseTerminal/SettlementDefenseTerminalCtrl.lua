local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.SettlementDefenseTerminal
SettlementDefenseTerminalCtrl = HL.Class('SettlementDefenseTerminalCtrl', uiCtrl.UICtrl)

local MAX_DISPLAY_REWARD_COUNT = 3

local DefenseState = CS.Beyond.Gameplay.TowerDefenseSystem.DefenseState

local ENEMY_DETAIL_TITLE_TEXT_ID = "ui_fac_settlement_defence_radar_enemy_review"
local ENEMY_LIST_TITLE_TEXT_ID = "ui_fac_settlement_defence_radar_eny_list_left"
local ENEMY_INFO_TITLE_TEXT_ID = "ui_fac_settlement_defence_radar_eny_list_right"

SettlementDefenseTerminalCtrl.m_getGroupCell = HL.Field(HL.Function)

SettlementDefenseTerminalCtrl.m_getRecommendCell = HL.Field(HL.Function)

SettlementDefenseTerminalCtrl.m_rewardCells = HL.Field(HL.Forward('UIListCache'))

SettlementDefenseTerminalCtrl.m_settlementId = HL.Field(HL.String) << ""

SettlementDefenseTerminalCtrl.m_selectedLevelId = HL.Field(HL.String) << ""

SettlementDefenseTerminalCtrl.m_groupDataList = HL.Field(HL.Userdata)

SettlementDefenseTerminalCtrl.m_selectedGroupIndex = HL.Field(HL.Number) << 0

SettlementDefenseTerminalCtrl.m_maxUnlockedGroupIndex = HL.Field(HL.Number) << 0

SettlementDefenseTerminalCtrl.m_maxUnCompletedGroupIndex = HL.Field(HL.Number) << 0

SettlementDefenseTerminalCtrl.m_towerDefenseSystem = HL.Field(HL.Userdata)

SettlementDefenseTerminalCtrl.m_settlementSystem = HL.Field(HL.Userdata)

SettlementDefenseTerminalCtrl.m_lastFailedGroupIndex = HL.Field(HL.Number) << 0

SettlementDefenseTerminalCtrl.s_lastFailedTdId = HL.StaticField(HL.String) << ''

SettlementDefenseTerminalCtrl.m_enterSelectedTdId = HL.Field(HL.String) << ''






SettlementDefenseTerminalCtrl.s_messages = HL.StaticField(HL.Table) << {

}

SettlementDefenseTerminalCtrl.ShowTerminalPanel = HL.StaticMethod(HL.Any) << function(arg)
    PhaseManager:OpenPhase(PhaseId.SettlementDefenseTerminal, arg)
end


SettlementDefenseTerminalCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_towerDefenseSystem = GameInstance.player.towerDefenseSystem
    self.m_settlementSystem = GameInstance.player.settlementSystem

    if type(arg) == "table" then
        self.m_settlementId = unpack(arg)
    else
        self.m_settlementId = arg
    end

    if PhaseManager.isRecovering then
        self.animationWrapper:SkipInAnimation()
    end

    self:_InitController()
    self:_InitAction()
    self:_RefreshSafetyState()
    self:_InitGroupList()
    self:_HideBuffInfoTips()
    self.view.luaPanel:BlockAllInput()
end

SettlementDefenseTerminalCtrl.OnAnimationInFinished = HL.Override() << function(self)
    self.view.luaPanel:RecoverAllInput()
end

SettlementDefenseTerminalCtrl._InitAction = HL.Method() << function(self)
    self.view.leftList.addNode.addInfo.tipsBtn.onClick:AddListener(function()
        if self.view.leftList.addNode.addTipsNode.gameObject.activeSelf then
            self:_HideBuffInfoTips()
        else
            self:_ShowBuffInfoTips()
        end
    end)
    self.view.leftList.addNode.addTipsNode.autoCloseArea.onTriggerAutoClose:AddListener(function()
        self:_HideBuffInfoTips()
    end)
    self.view.leftList.addNode.addTipsNode.tipsBtn.onClick:AddListener(function()
        UIManager:Open(PanelId.SettlementTokenInstruction, self.m_settlementId)
    end)
    self.view.rightDetail.rewardsInfo.normalNode.button.onClick:AddListener(function()
        self:_RefreshLevelContent(false, true)
    end)
    self.view.rightDetail.rewardsInfo.autoNode.button.onClick:AddListener(function()
        local groupData = self.m_groupDataList[CSIndex(self.m_selectedGroupIndex)]
        if groupData and groupData.normalLevel and groupData.normalLevel.isCompleted then
            self:_RefreshLevelContent(true, true)
        else
            Notify(MessageConst.SHOW_TOAST, Language.LUA_TD_NORMAL_LEVEL_NOT_COMPLETED)
        end
    end)
    self.view.rightDetail.normalStartNode.startBtn.onClick:AddListener(function()
        self:_OnConfirmButtonClicked()
    end)
    self.view.rightDetail.autoStartNode.startBtn.onClick:AddListener(function()
        self:_OnConfirmButtonClicked()
    end)
    self.view.topBar.btnClose.onClick:AddListener(function()
        PhaseManager:PopPhase(PhaseId.SettlementDefenseTerminal)
    end)
end

SettlementDefenseTerminalCtrl._OnConfirmButtonClicked = HL.Method() << function(self)
    PhaseManager:PopPhase(PhaseId.SettlementDefenseTerminal, function()
        self.m_towerDefenseSystem:EnterPreparingPhase(self.m_selectedLevelId)
    end)
end

SettlementDefenseTerminalCtrl._InitGroupList = HL.Method() << function(self)
    local isGuideCompleted = Utils.isSettlementDefenseGuideCompleted()

    local groupDataList = self.m_towerDefenseSystem:GetDefenseGroupDataList(self.m_settlementId, not isGuideCompleted)
    if groupDataList == nil or groupDataList.Count == 0 then
        self.view.missionNode.gameObject:SetActive(false)
        return
    end
    local groupCount = groupDataList.Count
    self.m_maxUnlockedGroupIndex = 0
    self.m_maxUnCompletedGroupIndex = 0
    self.m_lastFailedGroupIndex = 0
    self.m_enterSelectedTdId = SettlementDefenseTerminalCtrl.s_lastFailedTdId
    for i = 1, groupDataList.Count do
        local groupData = groupDataList[CSIndex(i)]
        if groupData.isUnlocked then
            self.m_maxUnlockedGroupIndex = i
            if groupData.autoLevel and not groupData.autoLevel.isCompleted then
                self.m_maxUnCompletedGroupIndex = i
            end
            if not string.isEmpty(self.m_enterSelectedTdId) then
                if groupData.normalLevel and groupData.normalLevel.levelId == self.m_enterSelectedTdId then
                    self.m_lastFailedGroupIndex = i
                end
                if groupData.autoLevel and groupData.autoLevel.levelId == self.m_enterSelectedTdId then
                    self.m_lastFailedGroupIndex = i
                end
            end

        end
    end
    if not self.m_getGroupCell then
        self.m_getGroupCell = UIUtils.genCachedCellFunction(self.view.leftList.riskLevelScrollList)
    end
    self.view.missionNode.gameObject:SetActive(true)
    self.m_groupDataList = groupDataList
    if self.m_towerDefenseSystem:GetSettlementDefenseState(self.m_settlementId) == DefenseState.LongSafety then
        if self.m_maxUnlockedGroupIndex > 0 then
            self.m_selectedGroupIndex = self.m_maxUnlockedGroupIndex
        end
    else
        if self.m_maxUnCompletedGroupIndex > 0 then
            self.m_selectedGroupIndex = self.m_maxUnCompletedGroupIndex
        end
    end
    if self.m_lastFailedGroupIndex > 0 then
        self.m_selectedGroupIndex = self.m_lastFailedGroupIndex
    end
    if self.m_selectedGroupIndex <= 0 then
        self.m_selectedGroupIndex = groupDataList.Count
    end
    self.view.leftList.riskLevelScrollList.onUpdateCell:AddListener(function(object, csIndex)
        local luaIndex = LuaIndex(csIndex)
        self:_RefreshGroupCell(self.m_getGroupCell(object), luaIndex)
    end)
    self.view.leftList.riskLevelScrollList:UpdateCount(groupCount, CSIndex(self.m_selectedGroupIndex))
    self:_RefreshLevelContent()
end

SettlementDefenseTerminalCtrl._RefreshGroupCell = HL.Method(HL.Table, HL.Number) << function(self, cell, luaIndex)
    local groupData = self.m_groupDataList[CSIndex(luaIndex)]
    if groupData == nil then
        return
    end
    local groupId = groupData.groupId
    local _, groupTableData = Tables.towerDefenseGroupTable:TryGetValue(groupId)
    if not groupTableData then
        return
    end
    local isSelected = self.m_selectedGroupIndex == luaIndex
    self:_SetGroupSelected(luaIndex, isSelected)
    if isSelected and cell.button ~= InputManagerInst.controllerNaviManager.curTarget then
        self:SetNaviTarget(cell.button)
    end

    cell.gameObject.name = string.format("Level_%d", luaIndex)
    cell.button.onClick:AddListener(function()
        self:_OnLevelSelected(luaIndex)
    end)
    cell.nameText.text = groupTableData.name
    local isAllCompleted = true
    if groupData.normalLevel and groupData.normalLevel.isCompleted then
        cell.manualDefenseCell:SetState("Completed")
    else
        isAllCompleted = false
    end
    if groupData.autoLevel and groupData.autoLevel.isCompleted then
        cell.defensePlanCell:SetState("Completed")
    else
        isAllCompleted = false
    end
    cell.lockStateController:SetState(groupData.isUnlocked and "Normal" or "Locked")
    local targetTipStateName = "Empty"
    if self.m_maxUnlockedGroupIndex == luaIndex then
        if groupData.normalLevel and not groupData.normalLevel.isCompleted then
            targetTipStateName = "Normal"
        elseif groupData.autoLevel and not groupData.autoLevel.isCompleted then
            targetTipStateName = "Auto"
        end
    end
    cell.targetNode:SetState(targetTipStateName)

    if self.m_selectedGroupIndex <= 0 and not isAllCompleted then
        self:_OnLevelSelected(luaIndex)
    end
end

SettlementDefenseTerminalCtrl._OnLevelSelected = HL.Method(HL.Number) << function(self, luaIndex)
    if luaIndex == self.m_selectedGroupIndex then
        return
    end
    self:_SetGroupSelected(luaIndex, true, true)
    if self.m_selectedGroupIndex > 0 then
        self:_SetGroupSelected(self.m_selectedGroupIndex, false, true)
    end
    self.m_selectedGroupIndex = luaIndex
    self:_RefreshLevelContent()
end

SettlementDefenseTerminalCtrl._SetGroupSelected = HL.Method(HL.Number, HL.Boolean, HL.Opt(HL.Boolean)) << function(
    self, index, isSelected, playAnim)

    local cell = self.m_getGroupCell(self.view.leftList.riskLevelScrollList:Get(CSIndex(index)))
    if not cell then
        return
    end
    local function setSelected()
        local groupData = self.m_groupDataList[CSIndex(index)]
        if not (groupData.normalLevel and groupData.normalLevel.isCompleted) then
            cell.manualDefenseCell:SetState(isSelected and "Selected" or "UnSelected")
        end
        if not (groupData.autoLevel and groupData.autoLevel.isCompleted) then
            cell.defensePlanCell:SetState(isSelected and "Selected" or "UnSelected")
        end
        cell.selectionStateController:SetState(isSelected and "Selected" or "UnSelected")
    end

    if playAnim then
        cell.animationWrapper:ClearTween()
        if isSelected then
            setSelected()
            cell.animationWrapper:Play("defense_terminal_risklevelcell_slcin")
        else
            cell.animationWrapper:Play("defense_terminal_risklevelcell_slcout", setSelected)
        end
    else
        setSelected()
    end

end

SettlementDefenseTerminalCtrl._RefreshLevelContent = HL.Method(HL.Opt(HL.Boolean, HL.Boolean)) << function(self, isAuto, playAnim)
    local groupData = self.m_groupDataList[CSIndex(self.m_selectedGroupIndex)]
    if groupData == nil then
        return
    end

    if isAuto == nil then
        if groupData.normalLevel and groupData.normalLevel.isCompleted and groupData.autoLevel then
            isAuto = true
        else
            isAuto = false
        end
        if not string.isEmpty(self.m_enterSelectedTdId) then
            if groupData.normalLevel and groupData.normalLevel.levelId == self.m_enterSelectedTdId then
                isAuto = false
            end
            if groupData.autoLevel and groupData.autoLevel.levelId == self.m_enterSelectedTdId then
                isAuto = true
            end
            self.m_enterSelectedTdId = ''
        end
    end

    local levelData = isAuto and groupData.autoLevel or groupData.normalLevel
    if levelData == nil then
        return
    end

    local levelId = levelData.levelId
    local _, levelTableData = Tables.towerDefenseTable:TryGetValue(levelId)
    if not levelTableData then
        return
    end

    local mapSuccess, mapTableData = Tables.towerDefenseMapTable:TryGetValue(self.m_settlementId)
    if mapSuccess then
        self:_RefreshMapContent(mapTableData.mapImage, levelTableData.detailImage)
    end

    self:_RefreshRecommendBuildingContent(levelTableData)
    self:_RefreshEnemyContent(levelTableData)
    self:_RefreshRewardContent(levelData, levelTableData)
    self:_RefreshLevelBtnState(isAuto, playAnim)

    self.m_selectedLevelId = levelId
end

SettlementDefenseTerminalCtrl._RefreshRecommendBuildingContent = HL.Method(HL.Userdata) << function(self, levelTableData)
    local recommendNode = self.view.rightDetail.recommendNode
    if not self.m_getRecommendCell then
        self.m_getRecommendCell = UIUtils.genCachedCellFunction(recommendNode.recommendScrollList)

    end
    recommendNode.recommendScrollList.onUpdateCell:RemoveAllListeners()
    recommendNode.recommendScrollList.onUpdateCell:AddListener(function(object, csIndex)
        local cell = self.m_getRecommendCell(object)
        cell:InitItem({id = levelTableData.recommendBuildingItemIds[csIndex]}, true)
        if DeviceInfo.usingController then
            cell:SetExtraInfo({
                tipsPosType = UIConst.UI_TIPS_POS_TYPE.LeftMid,
                tipsPosTransform = recommendNode.transform,
                isSideTips = true,
            })
        end
    end)
    recommendNode.recommendScrollList:UpdateCount(#levelTableData.recommendBuildingItemIds)
end

SettlementDefenseTerminalCtrl._RefreshEnemyContent = HL.Method(HL.Userdata) << function(self, levelTableData)
    self.view.rightDetail.enemyInfoBtn.onClick:RemoveAllListeners()
    self.view.rightDetail.enemyInfoBtn.onClick:AddListener(function()
        UIManager:AutoOpen(PanelId.CommonEnemyPopup, {
            title = Language[ENEMY_DETAIL_TITLE_TEXT_ID],
            enemyListTitle = Language[ENEMY_LIST_TITLE_TEXT_ID],
            enemyInfoTitle = Language[ENEMY_INFO_TITLE_TEXT_ID],
            enemyIds = levelTableData.enemyIds,
            enemyLevels = levelTableData.enemyLevels
        })
    end)
end

SettlementDefenseTerminalCtrl._RefreshRewardContent = HL.Method(HL.Userdata, HL.Userdata) << function(self, levelData, levelTableData)
    if levelData == nil or levelTableData == nil then
        return
    end

    if not self.m_rewardCells then
        self.m_rewardCells = UIUtils.genCellCache(self.view.rightDetail.rewardsInfo.itemSmallRewardBlack)
    end

    local rewardId = levelTableData.rewardId
    local _, rewardData = Tables.rewardTable:TryGetValue(rewardId)
    if rewardData then
        local rewardItems = rewardData.itemBundles
        local rewardItemDataList = UIUtils.convertRewardItemBundlesToDataList(rewardItems, false)

        local rewardCount = #rewardItemDataList
        self.m_rewardCells:GraduallyRefresh(rewardCount, 0.02, function(cell, luaIndex)
            local itemData = rewardItemDataList[luaIndex]
            cell:InitItem({
                id = itemData.id,
                count = itemData.count,
            }, true)
            if DeviceInfo.usingController then
                cell:SetExtraInfo({
                    tipsPosType = UIConst.UI_TIPS_POS_TYPE.LeftMid,
                    tipsPosTransform = self.view.rightDetail.rewardsInfo.transform,
                    isSideTips = true,
                })
            end
            cell.view.rewardedCover.gameObject:SetActive(levelData.isCompleted)
        end)
    end
end

SettlementDefenseTerminalCtrl._RefreshMapContent = HL.Method(HL.String, HL.String) << function(self, mapImage, detailImage)
    if not string.isEmpty(mapImage) then
        self.view.mapNode.mapImage:LoadSprite(UIConst.UI_SPRITE_SETTLEMENT_DEFENSE_MAP, mapImage)
    end

    if not string.isEmpty(detailImage) then
        self.view.mapNode.detailImage:LoadSprite(UIConst.UI_SPRITE_SETTLEMENT_DEFENSE_DETAIL, detailImage)
    end
end

SettlementDefenseTerminalCtrl._RefreshLevelBtnState = HL.Method(HL.Boolean, HL.Opt(HL.Boolean)) << function(self, isAuto, playAnim)
    local rightDetail = self.view.rightDetail
    local stateName = isAuto and "Auto" or "Normal"
    if playAnim and rightDetail.stateController.currentStateName ~= stateName then
        if isAuto then
            rightDetail.rewardsInfo.normalNode.animationWrapper:PlayOutAnimation()
            rightDetail.rewardsInfo.autoNode.animationWrapper:PlayInAnimation()
        else
            rightDetail.rewardsInfo.normalNode.animationWrapper:PlayInAnimation()
            rightDetail.rewardsInfo.autoNode.animationWrapper:PlayOutAnimation()
        end
    end
    rightDetail.stateController:SetState(stateName)
    local groupData = self.m_groupDataList[CSIndex(self.m_selectedGroupIndex)]
    local isMaxUnlockedGroup = self.m_maxUnlockedGroupIndex == self.m_selectedGroupIndex
    local isNormalCompleted = groupData.normalLevel and groupData.normalLevel.isCompleted
    local isAutoCompleted = groupData.autoLevel and groupData.autoLevel.isCompleted
    if isNormalCompleted then
        rightDetail.rewardsInfo.normalNode.manualDefenseCell:SetState("Completed")
    end
    if isAutoCompleted then
        rightDetail.rewardsInfo.autoNode.defensePlanCell:SetState("Completed")
    end
    rightDetail.rewardsInfo.normalNode.lock2Image.gameObject:SetActive(not groupData.isUnlocked)
    rightDetail.rewardsInfo.autoNode.lock2Image.gameObject:SetActive(not groupData.isUnlocked or not isNormalCompleted)
    rightDetail.rewardsInfo.normalNode.targetCell.gameObject:SetActive(isMaxUnlockedGroup and not isNormalCompleted)
    rightDetail.rewardsInfo.autoNode.targetCell.gameObject:SetActive(isMaxUnlockedGroup
        and isNormalCompleted and not isAutoCompleted)

    local settlementData = self.m_settlementSystem:GetUnlockSettlementData(self.m_settlementId)
    local isDuringBuff = settlementData and settlementData.timeLimitTdGainEffectByTdId == groupData.normalLevel.levelId
        and DateTimeUtils.GetCurrentTimestampBySeconds() < settlementData.tdGainEffectExpirationTs
    rightDetail.normalStartNode.tipsNode:StopCountDown()
    if not isAuto then
        local stateName = ""
        if groupData.normalLevel and groupData.normalLevel.isUnlocked then
            if isDuringBuff and isMaxUnlockedGroup then
                stateName = "Timing"
                rightDetail.normalStartNode.tipsNode:InitCountDownText(settlementData.tdGainEffectExpirationTs, function()
                    self:_RefreshLevelBtnState(isAuto)
                end, UIUtils.getLeftTimeToSecond)
            elseif not isNormalCompleted or (isMaxUnlockedGroup and not isAutoCompleted) then
                stateName = "Hint"
            else
                stateName = "Completed"
            end
        else
            stateName = "Locked"
        end
        rightDetail.normalStartNode.stateController:SetState(stateName)
    else
        local stateName = ""
        if isAutoCompleted then
            stateName = "Completed"
        else
            if isMaxUnlockedGroup then
                stateName = "Hint"
            else
                stateName = "UnCompleted"
            end
        end
        rightDetail.autoStartNode.stateController:SetState(stateName)
    end
end

SettlementDefenseTerminalCtrl._RefreshSafetyState = HL.Method() << function(self)
    self.view.topNode:SetState(self.m_towerDefenseSystem:GetSettlementDefenseState(self.m_settlementId):ToString())
end

SettlementDefenseTerminalCtrl._RefreshBuffState = HL.Method() << function(self)

    local settlementData = self.m_settlementSystem:GetUnlockSettlementData(self.m_settlementId)
    if settlementData == nil then
        return
    end
    local addInfoView = self.view.leftList.addNode.addInfo
    local totalAddNum = settlementData.tdGainEffect
    if DateTimeUtils.GetCurrentTimestampBySeconds() < settlementData.tdGainEffectExpirationTs then
        totalAddNum = totalAddNum + settlementData.timeLimitTdGainEffect
    end
    addInfoView.addNumberTxt.text = string.format("+%d", math.floor(totalAddNum))
    if totalAddNum == 0 then
        addInfoView.completeOperationNode.gameObject:SetActive(true)
        addInfoView.additionDurationCell.gameObject:SetActive(false)
    elseif settlementData.timeLimitTdGainEffect > 0 and
        DateTimeUtils.GetCurrentTimestampBySeconds() < settlementData.tdGainEffectExpirationTs then
        addInfoView.completeOperationNode.gameObject:SetActive(false)
        addInfoView.additionDurationCell.gameObject:SetActive(true)
        addInfoView.additionDurationCell:InitCountDownText(settlementData.tdGainEffectExpirationTs, function()
            self:_RefreshBuffState()
        end, UIUtils.getLeftTimeToSecond)
    else
        addInfoView.completeOperationNode.gameObject:SetActive(false)
        addInfoView.additionDurationCell.gameObject:SetActive(false)
    end
end

SettlementDefenseTerminalCtrl._ShowBuffInfoTips = HL.Method() << function(self)

    local settlementData = self.m_settlementSystem:GetUnlockSettlementData(self.m_settlementId)
    if settlementData == nil then
        return
    end
    self.view.leftList.addNode.stateController:SetState("Selected")
    local addTipsNodeView = self.view.leftList.addNode.addTipsNode
    local timeLimitAddValue = math.floor(settlementData.timeLimitTdGainEffect)
    local addValue = math.floor(settlementData.tdGainEffect)
    local isEmpty = true
    if timeLimitAddValue == 0 and addValue == 0 then
        addTipsNodeView.emptyNode.gameObject:SetActive(true)
        addTipsNodeView.detailsNode.gameObject:SetActive(false)
        return
    end
    if settlementData.tdGainEffectExpirationTs == 0 then
        addTipsNodeView.manualDefenseNode.gameObject:SetActive(false)
    else
        isEmpty = false
        addTipsNodeView.manualDefenseNode.gameObject:SetActive(true)
        local levelName = self:_GetLevelGroupName(settlementData.timeLimitTdGainEffectByTdId)
        addTipsNodeView.manualDefenseNode.decoTxt.text = string.format(Language.LUA_TD_LEVEL_COMPLETED_FORMAT, levelName)
        if DateTimeUtils.GetCurrentTimestampBySeconds() > settlementData.tdGainEffectExpirationTs then
            addTipsNodeView.manualDefenseNode.stateController:SetState("Expired")
        else
            addTipsNodeView.manualDefenseNode.stateController:SetState("During")
            addTipsNodeView.manualDefenseNode.numberTxt.text = string.format("%d%%", timeLimitAddValue)
            addTipsNodeView.manualDefenseNode.additionDurationNode:InitCountDownText(settlementData.tdGainEffectExpirationTs, function()
                self:_ShowBuffInfoTips()
            end, UIUtils.getLeftTimeToSecond)
        end
    end
    if addValue > 0 then
        isEmpty = false
        addTipsNodeView.defensePlanNode.gameObject:SetActive(true)
        local levelName = self:_GetLevelGroupName(settlementData.tdGainEffectByTdId)
        addTipsNodeView.defensePlanNode.decoTxt.text = string.format(Language.LUA_TD_LEVEL_COMPLETED_FORMAT, levelName)
        addTipsNodeView.defensePlanNode.numberTxt.text = string.format("%d%%", addValue)
    else
        addTipsNodeView.defensePlanNode.gameObject:SetActive(false)
    end
    addTipsNodeView.emptyNode.gameObject:SetActive(isEmpty)
    addTipsNodeView.detailsNode.gameObject:SetActive(not isEmpty)
end

SettlementDefenseTerminalCtrl._HideBuffInfoTips = HL.Method() << function(self)
    self.view.leftList.addNode.stateController:SetState("UnSelected")
    self:_RefreshBuffState()
end

SettlementDefenseTerminalCtrl._GetLevelGroupName = HL.Method(HL.String).Return(HL.String) << function(self, tdId)
    local levelName = ""
    local _, tdLevelData = Tables.towerDefenseTable:TryGetValue(tdId)
    if tdLevelData then
        local _, groupTableData = Tables.towerDefenseGroupTable:TryGetValue(tdLevelData.tdGroup)
        if groupTableData then
            levelName = groupTableData.name
        end
    end
    return levelName
end



SettlementDefenseTerminalCtrl._InitController = HL.Method() << function(self)
    if not DeviceInfo.usingController then
        return
    end
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
    self.view.rightDetail.recommendNode.naviGroup.onIsFocusedChange:AddListener(UIUtils.hideItemTipsOnLoseFocus)
    self.view.rightDetail.rewardsInfo.naviGroup.onIsFocusedChange:AddListener(UIUtils.hideItemTipsOnLoseFocus)
    self.view.leftList.addNode.naviGroup.onIsFocusedChange:AddListener(function(isFocused)
        if isFocused then
            self:_ShowBuffInfoTips()
        else
            self:_HideBuffInfoTips()
        end
    end)
end



HL.Commit(SettlementDefenseTerminalCtrl)
