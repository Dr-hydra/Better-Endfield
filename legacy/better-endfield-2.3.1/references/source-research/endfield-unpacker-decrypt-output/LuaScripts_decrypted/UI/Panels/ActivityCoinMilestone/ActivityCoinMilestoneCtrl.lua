local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ActivityCoinMilestone
local PHASE_ID = PhaseId.ActivityCoinMilestone

local NodeStatus = {
    Nrl = 1,
    Receive = 2,
    Done = 3,
    WaitOpen = 4,
}


local LOCK_NODE_DEFAULT_WIDTH = 500

ActivityCoinMilestoneCtrl = HL.Class('ActivityCoinMilestoneCtrl', uiCtrl.UICtrl)

ActivityCoinMilestoneCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_RACING_DUNGEON_GET_MILESTONE_REWARD] = '_OnDataChange',
    [MessageConst.ON_CONDITIONAL_MULTI_STAGE_UPDATE] = '_OnDataChange',
}

ActivityCoinMilestoneCtrl.m_activityId = HL.Field(HL.String) << ''

ActivityCoinMilestoneCtrl.m_activityData = HL.Field(HL.Any)

ActivityCoinMilestoneCtrl.m_nodes = HL.Field(HL.Table)

ActivityCoinMilestoneCtrl.m_getCellFunc = HL.Field(HL.Function)

ActivityCoinMilestoneCtrl.m_countdownCor = HL.Field(HL.Thread)

ActivityCoinMilestoneCtrl.m_lockNodeLayoutCor = HL.Field(HL.Thread)

ActivityCoinMilestoneCtrl.m_scoreAnimLuaUpdateKey = HL.Field(HL.Number) << -1

ActivityCoinMilestoneCtrl.m_hasPlayedScoreAnim = HL.Field(HL.Boolean) << false

ActivityCoinMilestoneCtrl.m_lockedStageOpenTime = HL.Field(HL.Any)

ActivityCoinMilestoneCtrl.m_readNodes = HL.Field(HL.Table)

ActivityCoinMilestoneCtrl.m_lockNodeWidth = HL.Field(HL.Number) << LOCK_NODE_DEFAULT_WIDTH


ActivityCoinMilestoneCtrl.m_rewardScrollBasePaddingRight = HL.Field(HL.Number) << 0


ActivityCoinMilestoneCtrl.m_totalMilestoneNodeCount = HL.Field(HL.Number) << 0


ActivityCoinMilestoneCtrl.m_sceneContentStartPos = HL.Field(Vector2)


ActivityCoinMilestoneCtrl.m_sceneContentStartPosInited = HL.Field(HL.Boolean) << false


ActivityCoinMilestoneCtrl.m_sceneContentMaxMoveWidth = HL.Field(HL.Number) << 0


ActivityCoinMilestoneCtrl.m_currRewardScrollableWidth = HL.Field(HL.Number) << 0


ActivityCoinMilestoneCtrl.m_fullRewardScrollableWidth = HL.Field(HL.Number) << 0


ActivityCoinMilestoneCtrl.m_needNaviToFirst = HL.Field(HL.Boolean) << false


ActivityCoinMilestoneCtrl.m_naviTargetIndex = HL.Field(HL.Number) << 0


ActivityCoinMilestoneCtrl.m_needRestoreNaviByNodeId = HL.Field(HL.Boolean) << false

ActivityCoinMilestoneCtrl.m_restoreNaviNodeId = HL.Field(HL.Any)

ActivityCoinMilestoneCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_activityId = arg.activityId
    self.m_readNodes = {}
    self.m_needNaviToFirst = true
    self:_InitData()
    self:_BindUI()
    self:_RefreshUI()
end

ActivityCoinMilestoneCtrl._BindUI = HL.Method() << function(self)
    self.view.commonTopTitleNode.btnClose.onClick:AddListener(function()
        PhaseManager:PopPhase(PHASE_ID)
    end)

    local helpInstructionId = self.m_activityId .. "_milestone"
    self.view.commonTopTitleNode.helpBtn.onClick:AddListener(function()
        UIManager:Open(PanelId.InstructionBook, {
            id = helpInstructionId,
        })
    end)

    self.m_getCellFunc = UIUtils.genCachedCellFunction(self.view.rewardScroll)
    self.m_rewardScrollBasePaddingRight = self.view.rewardScroll:GetPadding().right
    self.view.rewardScroll.onUpdateCell:RemoveAllListeners()
    self.view.rewardScroll.onUpdateCell:AddListener(function(object, csIndex)
        self:_OnUpdateCell(object, LuaIndex(csIndex))
    end)
    self.view.rewardScrollScrollRect.onValueChanged:AddListener(function(normalizedPosition)
        self:_OnRewardScrollValueChanged(normalizedPosition)
    end)

    self.view.allReceiveBtn.onClick:RemoveAllListeners()
    self.view.allReceiveBtn.onClick:AddListener(function()
        local hasReceivable = false
        for _, node in ipairs(self.m_nodes) do
            if node.status == NodeStatus.Receive then
                hasReceivable = true
                break
            end
        end
        if hasReceivable then
            GameInstance.player.racingDungeonSystem:SendReceiveMilestoneReward(self.m_activityId, true)
        end
    end)

    if self.view.redDotScrollRect then
        self.view.redDotScrollRect.getRedDotStateAt = function(csIndex)
            return self:GetRedDotStateAt(csIndex)
        end
    end

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
end

ActivityCoinMilestoneCtrl._InitData = HL.Method() << function(self)
    self.m_activityData = GameInstance.player.activitySystem:GetActivity(self.m_activityId)

    local haveCfg, milestoneCfg = Tables.activityRacingDungeonMilestoneTable:TryGetValue(self.m_activityId)
    if not haveCfg then
        self.m_nodes = {}
        self.m_totalMilestoneNodeCount = 0
        return
    end

    local milestoneScore = self.m_activityData.milestoneScore
    local receivedNodes = self.m_activityData.receivedMilestoneNodes

    local nodes = {}
    local totalNodeCount = 0
    local haveLockedNode = false
    for nodeId, nodeData in pairs(milestoneCfg.milestoneMap) do
        totalNodeCount = totalNodeCount + 1
        local unlockStageId = nodeData.unlockStageId
        local _, stageData = self.m_activityData.stageDataDict:TryGetValue(unlockStageId)
        local stageStatus = stageData and GEnums.ActivityConditionalStageState.__CastFrom(stageData.Status)
        if stageStatus and stageStatus ~= GEnums.ActivityConditionalStageState.Locked then
            local status = NodeStatus.Nrl
            if receivedNodes:Contains(nodeId) then
                status = NodeStatus.Done
            elseif milestoneScore >= nodeData.completeScore then
                status = NodeStatus.Receive
            end
            table.insert(nodes, {
                nodeId = nodeData.nodeId,
                completeScore = nodeData.completeScore,
                maxScore = nodeData.maxScore,
                rewardId = nodeData.rewardId,
                isBig = nodeData.isBig,
                status = status,
            })
        end
        if stageStatus == GEnums.ActivityConditionalStageState.Locked then
            haveLockedNode = true
            if not self.m_lockedStageOpenTime then
                self.m_lockedStageOpenTime = stageData.OpenTimeTs
            end
        end
    end

    table.sort(nodes, function(a, b) return a.completeScore < b.completeScore end)
    if haveLockedNode then
        table.insert(nodes, {
            status = NodeStatus.WaitOpen,
        })
    end
    self.m_nodes = nodes
    self.m_totalMilestoneNodeCount = totalNodeCount
end

ActivityCoinMilestoneCtrl._CalcInitialTargetIndex = HL.Method().Return(HL.Number) << function(self)
    local firstNormalIndex = 0
    local lastDoneIndex = 0
    for i, node in ipairs(self.m_nodes) do
        if node.status == NodeStatus.Receive then
            return i
        elseif node.status == NodeStatus.Nrl and firstNormalIndex <= 0 then
            firstNormalIndex = i
        elseif node.status == NodeStatus.Done then
            lastDoneIndex = i
        end
    end

    if firstNormalIndex > 0 then
        return firstNormalIndex
    end
    return lastDoneIndex
end

ActivityCoinMilestoneCtrl._TryGetCurNaviNodeId = HL.Method().Return(HL.Opt(HL.Any)) << function(self)
    if not DeviceInfo.usingController then
        return nil
    end

    local getNodeIdByTarget = function(target)
        if target == nil then
            return nil
        end
        for index, node in ipairs(self.m_nodes) do
            if node.status ~= NodeStatus.WaitOpen then
                local cellObj = self.view.rewardScroll:Get(CSIndex(index))
                if cellObj ~= nil then
                    local cell = self.m_getCellFunc(cellObj)
                    if cell ~= nil and cell.inputBindingGroupNaviDecorator == target then
                        return node.nodeId
                    end
                end
            end
        end
        return nil
    end

    local curTarget = InputManagerInst.controllerNaviManager.curTarget
    local curTargetNodeId = getNodeIdByTarget(curTarget)
    if curTargetNodeId ~= nil then
        return curTargetNodeId
    end

    local needTopDummyLayerKey = self:_GetDummyNaviLayerKey()
    if not string.isEmpty(needTopDummyLayerKey) then
        local needTopDummyLayer = LuaSystemManager.dummyNaviLayerSystem:GetDummyNaviLayerByKey(needTopDummyLayerKey)
        if needTopDummyLayer ~= nil and InputManagerInst.controllerNaviManager:GetTopDummyLayerGroup() ~= needTopDummyLayer then
            local rewardScrollNaviGroup = self.view.rewardScroll.gameObject:GetComponent("UISelectableNaviGroup")
            return rewardScrollNaviGroup ~= nil and getNodeIdByTarget(rewardScrollNaviGroup.LayerSelectedTarget) or nil
        end
    end

    return nil
end

ActivityCoinMilestoneCtrl._HasWaitOpenNode = HL.Method().Return(HL.Boolean) << function(self)
    local nodeCount = #self.m_nodes
    return nodeCount > 0 and self.m_nodes[nodeCount].status == NodeStatus.WaitOpen
end

ActivityCoinMilestoneCtrl._RefreshLockNodeLayout = HL.Method() << function(self)
    local nodeCount = #self.m_nodes
    local hasWaitOpenNode = self:_HasWaitOpenNode()
    local scrollRect = self.view.rewardScroll:GetComponent(typeof(CS.UnityEngine.UI.ScrollRect))


    local cellW = self.view.cell.rectTransform.rect.width
    local padding = self.view.rewardScroll:GetPadding()
    local basePaddingRight = self.m_rewardScrollBasePaddingRight
    local extraPaddingRight = 0
    if hasWaitOpenNode then

        local viewportW = self.view.rewardScroll.gameObject:GetComponent("RectTransform").rect.width
        local spaceX = self.view.rewardScroll.space.x
        local otherCount = nodeCount - 1
        local otherWidth = cellW * otherCount + spaceX * math.max(0, otherCount)
        local available = viewportW - padding.left - basePaddingRight - otherWidth
        if available >= LOCK_NODE_DEFAULT_WIDTH then

            self.m_lockNodeWidth = available
            scrollRect.horizontal = false
        else

            self.m_lockNodeWidth = LOCK_NODE_DEFAULT_WIDTH
            scrollRect.horizontal = true
        end
        extraPaddingRight = math.max(0, self.m_lockNodeWidth - cellW)
    else

        self.m_lockNodeWidth = LOCK_NODE_DEFAULT_WIDTH
        scrollRect.horizontal = true
    end
    self.view.rewardScroll:SetPaddingRight(basePaddingRight + extraPaddingRight)
end

ActivityCoinMilestoneCtrl._ScheduleDeferredLockNodeLayoutRefresh = HL.Method() << function(self)
    if self.m_lockNodeLayoutCor then
        self.m_lockNodeLayoutCor = self:_ClearCoroutine(self.m_lockNodeLayoutCor)
    end

    if not self:_HasWaitOpenNode() then
        return
    end

    self.m_lockNodeLayoutCor = self:_StartCoroutine(function()
        coroutine.step()
        self.m_lockNodeLayoutCor = nil
        if IsNull(self.view.gameObject) then
            return
        end

        self:_RefreshLockNodeLayout()
        self:_RefreshShowingWaitOpenNodeLayout()
        self.view.rewardScroll:UpdateCount(#self.m_nodes)
        self:_RefreshShowingWaitOpenNodeLayout()
        self:_RefreshSceneScrollMetrics()
        self:_RefreshSceneContentPosition()
        self:_RefreshDecomanRootPosition()
    end)
end

ActivityCoinMilestoneCtrl._RefreshUI = HL.Method() << function(self)

    local nodeCount = #self.m_nodes
    local hasWaitOpenNode = self:_HasWaitOpenNode()


    if self.m_needNaviToFirst then
        self.m_naviTargetIndex = self:_CalcInitialTargetIndex()
    end


    self:_RefreshLockNodeLayout()

    self.view.rewardScroll:UpdateCount(nodeCount)
    self:_ScheduleDeferredLockNodeLayoutRefresh()
    self:_RefreshSceneScrollMetrics()
    self:_RefreshSceneContentPosition()
    self:_RefreshDecomanRootPosition()



    if self.m_needNaviToFirst and self.m_naviTargetIndex > 0 then
        self.view.rewardScroll:ScrollToIndex(
            CSIndex(self.m_naviTargetIndex), true,
            CS.Beyond.UI.UIScrollList.ScrollAlignType.Center)
    end

    local currScore = ActivityUtils.GetRacingDungeonMilestoneCurrScore(self.m_activityId)
    local maxScore = ActivityUtils.GetRacingDungeonMilestoneMaxScore(self.m_activityId)
    if not self.m_hasPlayedScoreAnim then
        self:_StartScoreAnim(currScore, maxScore)
    else
        self:_StopScoreAnim()
        self:_SetScoreText(currScore, maxScore)
    end

    local hasReceivable = false
    for _, node in ipairs(self.m_nodes) do
        if node.status == NodeStatus.Receive then
            hasReceivable = true
            break
        end
    end
    self.view.allReceiveBtn.gameObject:SetActive(hasReceivable)
    self.view.disBtn.gameObject:SetActive(not hasReceivable)

    if self.m_countdownCor then
        self.m_countdownCor = self:_ClearCoroutine(self.m_countdownCor)
    end

    local countdownNode = self.view.countdownNode
    local countdownText = countdownNode.timeText
    local endTime = self.m_activityData.endTime
    local curTime = DateTimeUtils.GetCurrentTimestampBySeconds()
    local leftSec = endTime - curTime
    if leftSec < 0 then leftSec = 0 end
    countdownText.text = string.format(Language.LUA_ACTIVITY_RACING_DUNGEON_MILESTONE_PANEL_COUNTDOWN_TEXT, UIUtils.getLeftTime(leftSec))
    if leftSec > 0 or hasWaitOpenNode then
        self.m_countdownCor = self:_StartCoroutine(function()
            while true do
                coroutine.wait(60)
                local remaining = endTime - DateTimeUtils.GetCurrentTimestampBySeconds()
                if remaining < 0 then remaining = 0 end
                countdownText.text = string.format(Language.LUA_ACTIVITY_RACING_DUNGEON_MILESTONE_PANEL_COUNTDOWN_TEXT, UIUtils.getLeftTime(remaining))
                if hasWaitOpenNode then
                    self:_UpdateShowingWaitOpenNodeCountdown()
                end
                if remaining <= 0 and not hasWaitOpenNode then
                    break
                end
                if hasWaitOpenNode then
                    local stageRemaining = self.m_lockedStageOpenTime - DateTimeUtils.GetCurrentTimestampBySeconds()
                    if stageRemaining <= 0 and remaining <= 0 then
                        break
                    end
                end
            end
        end)
    end
end

ActivityCoinMilestoneCtrl._RefreshWaitOpenNodeCountdown = HL.Method(HL.Any) << function(self, cell)
    if self.m_lockedStageOpenTime then
        local remaining = self.m_lockedStageOpenTime - DateTimeUtils.GetCurrentTimestampBySeconds()
        if remaining < 0 then remaining = 0 end
        cell.lockNode.timeNumTxt.text = UIUtils.getLeftTime(remaining)
    else
        cell.lockNode.timeNumTxt.text = "--"
    end
end

ActivityCoinMilestoneCtrl._RefreshShowingWaitOpenNodeLayout = HL.Method() << function(self)
    local waitOpenIndex = #self.m_nodes
    if waitOpenIndex <= 0 or self.m_nodes[waitOpenIndex].status ~= NodeStatus.WaitOpen then
        return
    end

    self.view.rewardScroll:UpdateShowingCells(function(csIndex, obj)
        if LuaIndex(csIndex) ~= waitOpenIndex then
            return
        end

        local cell = self.m_getCellFunc(obj)
        local lockRect = cell.lockNode.rectTransform
        lockRect.sizeDelta = Vector2(self.m_lockNodeWidth, lockRect.sizeDelta.y)
        self:_RefreshWaitOpenNodeCountdown(cell)
    end)
end

ActivityCoinMilestoneCtrl._UpdateShowingWaitOpenNodeCountdown = HL.Method() << function(self)
    local waitOpenIndex = #self.m_nodes
    if waitOpenIndex <= 0 or self.m_nodes[waitOpenIndex].status ~= NodeStatus.WaitOpen then
        return
    end

    self.view.rewardScroll:UpdateShowingCells(function(csIndex, obj)
        if LuaIndex(csIndex) ~= waitOpenIndex then
            return
        end

        local cell = self.m_getCellFunc(obj)
        self:_RefreshWaitOpenNodeCountdown(cell)
    end)
end




ActivityCoinMilestoneCtrl._RefreshSceneScrollMetrics = HL.Method() << function(self)
    if not self.m_sceneContentStartPosInited then
        local startPos = self.view.sceneContentNode.transform.anchoredPosition
        local sceneParent = self.view.sceneContentNode.parent:GetComponent("RectTransform")
        if sceneParent ~= nil then
            startPos.x = sceneParent.rect.xMin - self.view.sceneContentNode.rect.xMin
        end
        self.m_sceneContentStartPos = startPos
        self.m_sceneContentStartPosInited = true
    end

    local cellW = self.view.cell.rectTransform.rect.width
    local viewportW = self.view.rewardScroll.gameObject:GetComponent("RectTransform").rect.width
    local padding = self.view.rewardScroll:GetPadding()
    local basePaddingRight = self.m_rewardScrollBasePaddingRight
    local spaceX = self.view.rewardScroll.space.x
    local nodeCount = #self.m_nodes
    local hasWaitOpenNode = nodeCount > 0 and self.m_nodes[nodeCount].status == NodeStatus.WaitOpen
    local normalNodeCount = hasWaitOpenNode and nodeCount - 1 or nodeCount

    local currContentWidth = padding.left + basePaddingRight + cellW * normalNodeCount + spaceX * math.max(0, nodeCount - 1)
    if hasWaitOpenNode then
        currContentWidth = currContentWidth + self.m_lockNodeWidth
    end

    local fullContentWidth = padding.left + basePaddingRight + cellW * self.m_totalMilestoneNodeCount + spaceX * math.max(0, self.m_totalMilestoneNodeCount - 1)
    self.m_currRewardScrollableWidth = math.max(0, currContentWidth - viewportW)
    self.m_fullRewardScrollableWidth = math.max(0, fullContentWidth - viewportW)

    local sceneParent = self.view.sceneContentNode.parent:GetComponent("RectTransform")
    local sceneViewportW = sceneParent ~= nil and sceneParent.rect.width or 0
    self.m_sceneContentMaxMoveWidth = math.max(0, self.view.sceneContentNode.rect.width - sceneViewportW)
end


ActivityCoinMilestoneCtrl._OnRewardScrollValueChanged = HL.Method(Vector2) << function(self, normalizedPosition)
    self:_RefreshSceneContentPosition()
    self:_RefreshDecomanRootPosition()
end




ActivityCoinMilestoneCtrl._RefreshSceneContentPosition = HL.Method() << function(self)
    if not self.m_sceneContentStartPosInited then
        return
    end

    local progress = 0
    if self.m_fullRewardScrollableWidth > 0 then


        local currScrollDistance = self.view.rewardScrollScrollRect.horizontalNormalizedPosition * self.m_currRewardScrollableWidth
        progress = math.max(0, math.min(currScrollDistance / self.m_fullRewardScrollableWidth, 1))
    end

    self.view.sceneContentNode.transform.anchoredPosition = Vector2(
        self.m_sceneContentStartPos.x - self.m_sceneContentMaxMoveWidth * progress,
        self.m_sceneContentStartPos.y)
end

ActivityCoinMilestoneCtrl._CalcNodeProgressFillAmount = HL.Method(HL.Number, HL.Number).Return(HL.Number) << function(self, currScore, index)
    local node = self.m_nodes[index]
    if not node or node.status == NodeStatus.WaitOpen then
        return 0
    end

    local prevScore = 0
    if index > 1 then
        prevScore = self.m_nodes[index - 1].completeScore
    end
    local range = node.completeScore - prevScore
    if range <= 0 then
        return 1
    end

    local progress = math.max(0, math.min(currScore - prevScore, range))
    return progress / range
end

ActivityCoinMilestoneCtrl._GetMilestoneProgressTargetInfo = HL.Method(HL.Number).Return(HL.Number, HL.Number) << function(self, currScore)
    local lastNormalIndex = 0
    for index, node in ipairs(self.m_nodes) do
        if node.status ~= NodeStatus.WaitOpen then
            lastNormalIndex = index
            if currScore <= node.completeScore then
                return index, self:_CalcNodeProgressFillAmount(currScore, index)
            end
        end
    end

    if lastNormalIndex > 0 then
        return lastNormalIndex, 1
    end
    return 0, 0
end

ActivityCoinMilestoneCtrl._RefreshDecomanRootPosition = HL.Method() << function(self)
    local targetIndex, fillAmount = self:_GetMilestoneProgressTargetInfo(
        ActivityUtils.GetRacingDungeonMilestoneCurrScore(self.m_activityId))
    if targetIndex <= 0 then
        return
    end

    local parent = self.view.decomanRoot.parent
    local targetParentX
    local targetCellObj = self.view.rewardScroll:Get(CSIndex(targetIndex))
    if targetCellObj ~= nil then
        local targetCell = self.m_getCellFunc(targetCellObj)
        if targetCell ~= nil then
            local progressRect = targetCell.progressImg.rectTransform
            local localTargetPos = Vector3(
                progressRect.rect.xMin + progressRect.rect.width * fillAmount, 0, 0)
            local targetWorldPos = progressRect:TransformPoint(localTargetPos)
            targetParentX = parent:InverseTransformPoint(targetWorldPos).x
        end
    end

    if targetParentX == nil then
        local scrollRect = self.view.rewardScroll:GetComponent(typeof(CS.UnityEngine.UI.ScrollRect))
        local contentRect = scrollRect and scrollRect.content
        if contentRect == nil then
            return
        end

        local cellRect = self.view.cell.rectTransform
        local cellW = cellRect.rect.width
        local padding = self.view.rewardScroll:GetPadding()
        local spaceX = self.view.rewardScroll.space.x
        local progressRect = self.view.cell.progressImg.rectTransform
        local templateLocalTargetPos = Vector3(
            progressRect.rect.xMin + progressRect.rect.width * fillAmount, 0, 0)
        local templateTargetWorldPos = progressRect:TransformPoint(templateLocalTargetPos)
        local targetOffsetX = cellRect:InverseTransformPoint(templateTargetWorldPos).x + cellW * cellRect.pivot.x
        local targetContentX = padding.left + (targetIndex - 1) * (cellW + spaceX) + targetOffsetX
        local targetWorldPos = contentRect:TransformPoint(Vector3(targetContentX, 0, 0))
        targetParentX = parent:InverseTransformPoint(targetWorldPos).x
    end

    local decoRect = self.view.decomanRoot
    local decoW = decoRect.rect.width
    local finalX = targetParentX - decoW * (1 - decoRect.pivot.x)

    self.view.decomanRoot.transform.anchoredPosition = Vector2(
        finalX, self.view.decomanRoot.transform.anchoredPosition.y)
end

ActivityCoinMilestoneCtrl._OnUpdateCell = HL.Method(HL.Userdata, HL.Number) << function(self, object, index)
    local cell = self.m_getCellFunc(object)
    local node = self.m_nodes[index]
    if not node then
        return
    end

    local currScore = ActivityUtils.GetRacingDungeonMilestoneCurrScore(self.m_activityId)


    local isFirst = index == 1
    local isLast = index == #self.m_nodes
    cell.decoLeft.gameObject:SetActive(isFirst)
    cell.decoRight.gameObject:SetActive(isLast)


    if node.status == NodeStatus.WaitOpen then
        cell.rewardStateNode.gameObject:SetActive(false)
        cell.lockNode.gameObject:SetActive(true)
        cell.progressNode:SetState("Lock")

        cell.inputBindingGroupNaviDecorator.enabled = false

        local lockRect = cell.lockNode.rectTransform
        lockRect.sizeDelta = Vector2(self.m_lockNodeWidth, lockRect.sizeDelta.y)
        self:_RefreshWaitOpenNodeCountdown(cell)

        local progressBG = cell.lockNode.transform:Find("ProgressBG"):GetComponent("UIImage")
        if currScore > self.m_nodes[#self.m_nodes - 1].completeScore then
            cell.progressImg.fillAmount = 1
            progressBG.color = UIUtils.getColorByString("FFDE00", 255)
        else
            cell.progressImg.fillAmount = 0
            progressBG.color = UIUtils.getColorByString("FFFFFF", 46)
        end
        return
    end


    cell.rewardStateNode.gameObject:SetActive(true)
    cell.lockNode.gameObject:SetActive(false)

    cell.inputBindingGroupNaviDecorator.enabled = true


    cell.progressImg.fillAmount = self:_CalcNodeProgressFillAmount(currScore, index)
    self:_RefreshDecomanRootPosition()

    if currScore >= node.completeScore then
        cell.progressNode:SetState("Receive")
    else
        cell.progressNode:SetState("Nrl")
    end
    cell.rewardTxt.text = node.completeScore
    local decoColor = currScore >= node.completeScore
        and UIUtils.getColorByString("FFDE00", 255)
        or UIUtils.getColorByString("FFFFFF", 0.18 * 255)
    if isFirst then
        cell.decoLeft.color = decoColor
    end
    if isLast then
        cell.decoRight.color = decoColor
    end


    local rewardItems = UIUtils.getRewardItems(node.rewardId)
    if #rewardItems >= 1 then
        cell.itemReward01.gameObject:SetActive(true)
        cell.itemReward01:InitItem(rewardItems[1], function()
            cell.itemReward01:ShowTips()
        end)
        if DeviceInfo.usingController then
            cell.itemReward01:SetExtraInfo({
                isSideTips = true,
            })
        end
    else
        cell.itemReward01.gameObject:SetActive(false)
    end
    if #rewardItems >= 2 then
        cell.itemReward02.gameObject:SetActive(true)
        cell.itemReward02:InitItem(rewardItems[2], function()
            cell.itemReward02:ShowTips()
        end)
        if DeviceInfo.usingController then
            cell.itemReward02:SetExtraInfo({
                isSideTips = true,
            })
        end
    else
        cell.itemReward02.gameObject:SetActive(false)
    end

    local stateStr = "Nrl"
    if node.status == NodeStatus.Receive then
        stateStr = "Receive"
    elseif node.status == NodeStatus.Done then
        stateStr = "Done"
    end
    cell.rewardStateNode.stateController:SetState(stateStr)

    cell.rewardStateNode.stateController:SetState(node.isBig and "Big" or "Small")

    cell.rewardStateNode.receiveBtn.onClick:RemoveAllListeners()
    if node.status == NodeStatus.Receive then
        cell.rewardStateNode.receiveBtn.onClick:AddListener(function()
            GameInstance.player.racingDungeonSystem:SendReceiveMilestoneReward(self.m_activityId, false, node.nodeId)
        end)
    end

    local redDotArgs = {
        activityId = self.m_activityId,
        nodeId = node.nodeId,
    }
    cell.itemReward01.view.redDot:InitRedDot("ActivityRacingDungeonSingleMilestone", redDotArgs, nil, self.view.redDotScrollRect)


    self.m_readNodes[node.nodeId] = true


    if self.m_needNaviToFirst and index == self.m_naviTargetIndex then
        self.m_needNaviToFirst = false
        self:SetNaviTarget(cell.inputBindingGroupNaviDecorator)
    end
    if self.m_needRestoreNaviByNodeId and node.nodeId == self.m_restoreNaviNodeId then
        self.m_needRestoreNaviByNodeId = false
        self.m_restoreNaviNodeId = nil
        self:SetNaviTarget(cell.inputBindingGroupNaviDecorator)
    end
end

ActivityCoinMilestoneCtrl._SetScoreText = HL.Method(HL.Number, HL.Number) << function(self, score, maxScore)
    self.view.integralNumTxt.text = string.format("%d/%d", score, maxScore)
end

ActivityCoinMilestoneCtrl._StopScoreAnim = HL.Method() << function(self)
    if self.m_scoreAnimLuaUpdateKey > 0 then
        self.m_scoreAnimLuaUpdateKey = LuaUpdate:Remove(self.m_scoreAnimLuaUpdateKey)
    end
end

ActivityCoinMilestoneCtrl._StartScoreAnim = HL.Method(HL.Number, HL.Number) << function(self, currScore, maxScore)
    self:_StopScoreAnim()
    self.m_hasPlayedScoreAnim = true

    local curve = self.view.config.SCORE_CURVE
    local startTime = self.view.config.SCORE_START_TIME
    local duration = self.view.config.SCORE_TIME
    local endTime = startTime + duration
    local timeCount = 0
    self:_SetScoreText(0, maxScore)

    if duration <= 0 then
        self:_SetScoreText(currScore, maxScore)
        return
    end

    self.m_scoreAnimLuaUpdateKey = LuaUpdate:Add("Tick", function(deltaTime)
        timeCount = timeCount + deltaTime
        if timeCount < startTime then
            self:_SetScoreText(0, maxScore)
        elseif timeCount < endTime then
            local normalizedX = (timeCount - startTime) / duration
            local normalizedY = curve:Evaluate(normalizedX)
            self:_SetScoreText(lume.round(currScore * normalizedY), maxScore)
        else
            self:_SetScoreText(currScore, maxScore)
            self:_StopScoreAnim()
        end
    end)
end

ActivityCoinMilestoneCtrl.OnClose = HL.Override() << function(self)
    self.view.rewardScroll:SetPaddingRight(self.m_rewardScrollBasePaddingRight)
    if self.m_countdownCor then
        self.m_countdownCor = self:_ClearCoroutine(self.m_countdownCor)
    end
    if self.m_lockNodeLayoutCor then
        self.m_lockNodeLayoutCor = self:_ClearCoroutine(self.m_lockNodeLayoutCor)
    end
    self:_StopScoreAnim()
    self:_UpdateReadInfo()
end

ActivityCoinMilestoneCtrl._UpdateReadInfo = HL.Method() << function(self)
    for nodeId, _ in pairs(self.m_readNodes) do
        ActivityUtils.setMilestoneNodeRead(self.m_activityId, nodeId)
    end
end

ActivityCoinMilestoneCtrl._OnDataChange = HL.Method(HL.Any) << function(self, arg)
    local activityId = unpack(arg)
    if activityId ~= self.m_activityId then
        return
    end
    local restoreNaviNodeId = self:_TryGetCurNaviNodeId()
    self.m_needRestoreNaviByNodeId = restoreNaviNodeId ~= nil
    self.m_restoreNaviNodeId = restoreNaviNodeId

    self:_InitData()
    self:_RefreshUI()
end

ActivityCoinMilestoneCtrl.GetRedDotStateAt = HL.Method(HL.Number).Return(HL.Number) << function(self, index)
    local luaIndex = LuaIndex(index)
    if luaIndex < 1 or luaIndex > #self.m_nodes then
        return 0
    end

    local node = self.m_nodes[luaIndex]
    local redDotArgs = {
        activityId = self.m_activityId,
        nodeId = node.nodeId,
    }

    local hasRedDot, redDotType, expireTs = RedDotManager:GetRedDotState(
        "ActivityRacingDungeonSingleMilestone", redDotArgs)
    if hasRedDot then
        return redDotType or UIConst.RED_DOT_TYPE.Normal
    else
        return 0
    end
end

HL.Commit(ActivityCoinMilestoneCtrl)
