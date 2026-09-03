local BattlePassNodeUnlockState = CS.Beyond.Gameplay.WeekRaidSystem.BattlePassNodeUnlockState

local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.RaidReward

RaidRewardCtrl = HL.Class('RaidRewardCtrl', uiCtrl.UICtrl)






RaidRewardCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_WEEK_RAID_BATTLE_PASS_UPDATE] = "_UpdateView",
}

RaidRewardCtrl.m_getRewardCell = HL.Field(HL.Function)

RaidRewardCtrl.m_focusRewardIndex = HL.Field(HL.Number) << -1

RaidRewardCtrl.m_system = HL.Field(CS.Beyond.Gameplay.WeekRaidSystem)


RaidRewardCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.btnBack.onClick:AddListener(function()
        self.m_phase:TryCloseTopPanel()
    end)

    self.view.helpBtn.onClick:AddListener(function()
        UIManager:Open(PanelId.InstructionBook, "week_raid_battle_pass")
    end)

    self.m_getRewardCell = UIUtils.genCachedCellFunction(self.view.mainScrollView)
    self.view.mainScrollView.onUpdateCell:AddListener(function(obj, csIndex)
        self:_UpdateNodeCell(obj, csIndex)
    end)
    self.view.mainScrollView.onGraduallyShowFinish:AddListener(function()
        self:_SetNaviTarget()
    end)

    self.view.receiveBtn.onClick:AddListener(function()
        self:_ReceiveAllRewards()
    end)

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })

    self.m_system = GameInstance.player.weekRaidSystem


    self.m_system:SetBattlePassVersionRead()
end

RaidRewardCtrl.OnShow = HL.Override() << function(self)
    self:_UpdateView()
end

RaidRewardCtrl._UpdateView = HL.Method() << function(self)

    self.view.scoreTxt.text = self.m_system.battlePassScore
    self.view.maxScoreTxt.text = self.m_system.battlePassMaxScore


    local currentTime = DateTimeUtils.GetCurrentTimestampBySeconds()
    local refreshTime = Utils.getNextWeeklyServerRefreshTime()
    local deltaSeconds = refreshTime - currentTime
    self.view.refreshTimeTxt.text = UIUtils.getLeftTime(deltaSeconds)


    self:_UpdateNodes()
    self.view.mainScrollRect.naviGroup:SetLayerSelectedTarget(nil, false)


    local isAnyReceivable = self.m_system:IsAnyBattlePassRewardReceivable()
    self.view.receiveBtn.gameObject:SetActive(isAnyReceivable)
    if isAnyReceivable then
        self.view.receiveNodeStateCtrl:SetState("ReceiveAll")
    else
        local areAllReceived = self.m_system:AreAllBattlePassRewardsReceived()
        if areAllReceived then
            self.view.receiveNodeStateCtrl:SetState("AllReceived")
        else
            self.view.receiveNodeStateCtrl:SetState("NotReceivable")
        end
    end
end

RaidRewardCtrl._UpdateNodes = HL.Method() << function(self)
    local nodeCount = self.m_system.battlePassNodeCount
    self.view.mainScrollView:UpdateCount(nodeCount)

    self.m_focusRewardIndex = self:_CalculateFocusIndex()
    if self.m_focusRewardIndex >= 0 then
        self.view.mainScrollView:ScrollToIndex(self.m_focusRewardIndex, true, CS.Beyond.UI.UIScrollList.ScrollAlignType.Center, true)
    end
end

RaidRewardCtrl._CalculateFocusIndex = HL.Method().Return(HL.Number) << function(self)
    local nodeCount = self.m_system.battlePassNodeCount
    if nodeCount == 0 then
        return -1
    end

    local focusCSIndex
    for luaIndex = 1, nodeCount do
        local csIndex = CSIndex(luaIndex)
        local node = self.m_system:GetBattlePassNode(csIndex)
        local isReceivable = self.m_system:IsBattlePassNodeRewardReceivable(node)
        if isReceivable then
            focusCSIndex = csIndex
            break
        elseif not focusCSIndex then
            local reached = self.m_system:IsBattlePassNodeReached(node)
            if not reached then
                focusCSIndex = csIndex
            end
        end
    end
    return focusCSIndex or 0
end

RaidRewardCtrl._SetNaviTarget = HL.Method() << function(self)
    if self.m_focusRewardIndex >= 0 then
        local cellObject = self.view.mainScrollView:Get(self.m_focusRewardIndex)
        local cell = self.m_getRewardCell(cellObject)
        if cell then
            self:SetNaviTarget(cell.contentNode.inputBindingGroupNaviDecorator)
        end
    end
end

RaidRewardCtrl._UpdateNodeCell = HL.Method(GameObject, HL.Number) << function(self, obj, csIndex)
    local cell = self.m_getRewardCell(obj)

    local node = self.m_system:GetBattlePassNode(csIndex)
    local nodeData = Tables.weekRaidBattlePassTable:GetValue(node.id)

    local received = node.rewardReceived
    local reached = self.m_system:IsBattlePassNodeReached(node)
    local unlocked, unlockState = self.m_system:IsBattlePassNodeUnlocked(node)
    local rewardItems = UIUtils.getRewardItems(nodeData.rewardId)


    local progress = cell.progressNode
    local nodeScore = nodeData.score
    local currentScore = self.m_system.battlePassScore
    local first = csIndex == 0
    local last = csIndex == self.m_system.battlePassNodeCount - 1


    progress.unlockLeftEndpoint.gameObject:SetActive(first)
    progress.unlockRightEndpoint.gameObject:SetActive(last)
    if unlocked then

        progress.unlockLeftProgress.fillAmount = 1

        if last then
            progress.unlockRightProgress.fillAmount = 1
        else
            local nextNode = self.m_system:GetBattlePassNode(csIndex + 1)
            local nextNodeUnlocked = self.m_system:IsBattlePassNodeUnlocked(nextNode)
            progress.unlockRightProgress.fillAmount = nextNodeUnlocked and 1 or 0
        end
    else

        progress.unlockLeftProgress.fillAmount = 0

        progress.unlockRightProgress.fillAmount = 0
    end


    progress.scoreLeftEndpoint.gameObject:SetActive(first)
    progress.scoreRightEndpoint.gameObject:SetActive(reached and last)
    if reached then

        progress.scoreLeftProgress.fillAmount = 1

        if last then
            progress.scoreRightProgress.fillAmount = 1
        else
            local nextNode = self.m_system:GetBattlePassNode(csIndex + 1)
            local nextNodeData = Tables.weekRaidBattlePassTable:GetValue(nextNode.id)
            local rightScore = (nodeScore + nextNodeData.score) * 0.5
            progress.scoreRightProgress.fillAmount = (currentScore - nodeScore) / (rightScore - nodeScore)
        end
    else

        local leftScore
        if first then
            leftScore = 0
        else
            local prevNode = self.m_system:GetBattlePassNode(csIndex - 1)
            local prevNodeData = Tables.weekRaidBattlePassTable:GetValue(prevNode.id)
            leftScore = (prevNodeData.score + nodeScore) * 0.5
        end
        progress.scoreLeftProgress.fillAmount = (currentScore - leftScore) / (nodeScore - leftScore)

        progress.scoreRightProgress.fillAmount = 0
    end


    progress.unlockReached.gameObject:SetActive(unlocked)
    progress.scoreReached.gameObject:SetActive(reached)
    progress.receivable.gameObject:SetActive(reached and unlocked and not received)
    progress.locked.gameObject:SetActive(not unlocked)
    progress.scoreTxt.text = nodeScore


    local content = cell.contentNode
    local contentState
    if not unlocked then
        contentState = "Disabled"
    elseif received then
        contentState = "Received"
    elseif reached then
        contentState = "Receivable"
    else
        contentState = "Normal"
    end
    content.stateCtrl:SetState(contentState)


    local rewardItem
    if string.isEmpty(nodeData.rewardItemId) then
        rewardItem = rewardItems[1]
        table.remove(rewardItems, 1)
    else
        for i, itemBundle in ipairs(rewardItems) do
            if itemBundle.id == nodeData.rewardItemId then
                rewardItem = itemBundle
                table.remove(rewardItems, i)
                break
            end
        end
    end
    local hasRewardItem = rewardItem ~= nil
    content.titleNode.gameObject:SetActive(hasRewardItem)
    if hasRewardItem then
        local itemData = Tables.itemTable:GetValue(rewardItem.id)
        content.titleIcon:LoadSprite(UIConst.UI_SPRITE_ITEM, itemData.iconId)
        content.titleTxt.text = itemData.name
        content.countTxt.text = string.format("×%s", rewardItem.count)
    else
        logger.error("[WeekRaid] Reward item not found, nodeId: " .. tostring(node.id))
    end


    local extraArgs
    if DeviceInfo.usingController then
        extraArgs = {
            onClickItem = function(itemCell)
                UIUtils.showItemSideTips(itemCell, UIConst.UI_TIPS_POS_TYPE.AdaptiveRightTop, content.transform)
            end,
            enableItemHoverTips = not DeviceInfo.usingController,
            onPostInitItem = function(itemCell, itemBundle)
                itemCell:SetExtraInfo({ isSideTips = DeviceInfo.usingController })
            end
        }
    end
    content.rewardItems:InitRewardItems(rewardItems, received, extraArgs)
    content.rewardItems.view.rewardListNaviGroup.onIsFocusedChange:AddListener(function(isFocused)
        if not isFocused then
            Notify(MessageConst.HIDE_ITEM_TIPS)
        end
    end)


    content.receiveBtn.onClick:RemoveAllListeners()
    content.receiveBtn.onClick:AddListener(function()
        self:_ReceiveReward(node.id)
    end)


    local disableTips
    if not unlocked then
        if unlockState == BattlePassNodeUnlockState.LockedByTech then
            local exists, techData = Tables.weekRaidTechTable:TryGetValue(nodeData.conditionTech)
            if exists then
                local techTypeData = Tables.weekRaidTechTypeTable:GetValue(techData.techType)
                disableTips = string.format(Language["LUA_WEEK_RAID_BATTLE_PASS_NODE_TECH_LOCKED_TIPS"], techTypeData.name)
            else
                logger.error("[WeekRaid] Invalid battle pass node tech id: " .. nodeData.conditionTech)
            end
        elseif unlockState == BattlePassNodeUnlockState.LockedByAdventureLevel then
            disableTips = string.format(Language["LUA_WEEK_RAID_BATTLE_PASS_NODE_ADVENTURE_LEVEL_LOCKED_TIPS"],
                nodeData.conditionAdventureLevel)
        end
    end
    content.disableTxt.text = disableTips
end

RaidRewardCtrl._ReceiveReward = HL.Method(HL.Number) << function(self, nodeId)
    self.m_system:WeekRaidBattlePassReceiveReward(nodeId)
end

RaidRewardCtrl._ReceiveAllRewards = HL.Method() << function(self)
    self.m_system:WeekRaidBattlePassReceiveAllRewards()
end

HL.Commit(RaidRewardCtrl)
