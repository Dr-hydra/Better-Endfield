local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ContingencyContractTask
local PHASE_ID = PhaseId.ContingencyContractTask

ContingencyContractTaskCtrl = HL.Class('ContingencyContractTaskCtrl', uiCtrl.UICtrl)

ContingencyContractTaskCtrl.m_activityId = HL.Field(HL.String) << ""

ContingencyContractTaskCtrl.m_gameId = HL.Field(HL.String) << ""

ContingencyContractTaskCtrl.m_groupToTaskMap = HL.Field(HL.Table)

ContingencyContractTaskCtrl.m_groupData = HL.Field(HL.Table)

ContingencyContractTaskCtrl.m_tabCache = HL.Field(HL.Any)

ContingencyContractTaskCtrl.m_taskListCache = HL.Field(HL.Any)

ContingencyContractTaskCtrl.m_taskCacheTable = HL.Field(HL.Table)

ContingencyContractTaskCtrl.m_rewardCellTable = HL.Field(HL.Table)

ContingencyContractTaskCtrl.m_rewardCacheTable = HL.Field(HL.Table)

ContingencyContractTaskCtrl.m_tabCell = HL.Field(HL.Table)

ContingencyContractTaskCtrl.m_listCell = HL.Field(HL.Table)

ContingencyContractTaskCtrl.m_taskCell = HL.Field(HL.Table)

ContingencyContractTaskCtrl.m_readTasks = HL.Field(HL.Table)

ContingencyContractTaskCtrl.m_taskTypeInitStatus = HL.Field(HL.Table)

ContingencyContractTaskCtrl.m_selectedTabIndex = HL.Field(HL.Number) << 0

ContingencyContractTaskCtrl.m_groupCount = HL.Field(HL.Number) << 0

ContingencyContractTaskCtrl.m_cor = HL.Field(HL.Thread)

ContingencyContractTaskCtrl.m_taskInfoDict = HL.Field(HL.Any)

ContingencyContractTaskCtrl.m_needUpdateRefresh = HL.Field(HL.Any)

ContingencyContractTaskCtrl.m_pendingRecoverPopupState = HL.Field(HL.Any)

ContingencyContractTaskCtrl.m_hasTabInit = HL.Field(HL.Boolean) << false

ContingencyContractTaskCtrl.m_hasTaskInit = HL.Field(HL.Boolean) << false

ContingencyContractTaskCtrl.m_groupHeight = HL.Field(HL.Table)

ContingencyContractTaskCtrl.m_topViewedTaskGroupIndex = HL.Field(HL.Number) << 0






ContingencyContractTaskCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_CONDITIONAL_MULTI_STAGE_TASK_PROGRESS_CHANGE] = '_OnTaskProgress',
}


ContingencyContractTaskCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    local recoverState = arg and arg.recoverState or nil
    self.m_activityId = arg.activityId
    self.view.btnClose.onClick:AddListener(function()
        Notify(MessageConst.HIDE_ITEM_TIPS)
        PhaseManager:PopPhase(PHASE_ID)
    end)

    self.m_taskCacheTable = {}
    self.m_taskCell = {}
    self.m_rewardCellTable = {}
    self.m_rewardCacheTable = {}
    self.m_readTasks = {}
    self.m_groupHeight = {}
    self.m_taskInfoDict = Tables.activityConditionalMultiStageTaskConfigTable[self.m_activityId].TaskConfigMap
    self.m_pendingRecoverPopupState = recoverState and recoverState.popupState or nil
    local activityConfig = Tables.activityTable[self.m_activityId]
    local info = Tables.activityContingencyContractTable[self.m_activityId]
    self.m_gameId = info.gameId
    local moneyId = Tables.activityShopAdditionalTable[info.shopGroupId].activityMoneyId



    self.m_hasTabInit = false
    self.m_hasTaskInit = false
    self.m_taskTypeInitStatus = {}


    self.m_groupData = {}
    for _, groupData in pairs(Tables.activityContingencyContractTaskGroupTable) do
        table.insert(self.m_groupData, groupData)
    end
    table.sort(self.m_groupData, Utils.genSortFunction({ "sortId" }, true))
    self.m_groupCount = #self.m_groupData

    self.m_groupToTaskMap = ContingencyContractUtils.CreateGroupToTaskMap(self.m_activityId)
    self:_InitSelectedIndex(recoverState and recoverState.selectedTabIndex or -1)


    self.view.helpBtn.onClick:AddListener(function()
        local instructionId = activityConfig.instructionId
        UIManager:Open(PanelId.InstructionBook, instructionId)
    end)
    if arg then
        arg.recoverState = nil
    end


    LayoutRebuilder.ForceRebuildLayoutImmediate(self.view.topRightNode)
    self.view.walletBarPlaceholder:InitWalletBarPlaceholder({moneyId})
end

ContingencyContractTaskCtrl.OnShow = HL.Override() << function(self)

    self:_OnUpdate()


    self.view.taskListNode.scrollView.onValueChanged:RemoveAllListeners()
    self.view.taskListNode.scrollView.onValueChanged:AddListener(function(pos)

        local hasVelocity = math.abs(self.view.taskListNode.scrollView.velocity.y) + math.abs(Input.mouseScrollDelta.y) > 0.1
        if hasVelocity then
            self:_UpdateTabByScrollPos()
        end

        for _,list in pairs(self.m_taskCell) do
            for _,task in pairs(list) do
                if self.view.taskListNode.scrollView:IsCellViewed(task.cell.rectTransform) then
                    self.m_readTasks[task.id] = true
                end
            end
        end
    end)

end

ContingencyContractTaskCtrl.OnClose = HL.Override() << function(self)
    self:_UpdateReadInfo()
end

ContingencyContractTaskCtrl._InitSelectedIndex = HL.Method(HL.Number) << function(self, index)
    self:_UpdateContentHeight()
    if index > 0 then

        self.m_selectedTabIndex = index
    else

        local found
        local activityData = GameInstance.player.activitySystem:GetActivity(self.m_activityId)
        for index, groupData in ipairs(self.m_groupData) do
            if found then
                break
            end
            local taskIds = self.m_groupToTaskMap[groupData.taskGroupId]
            for _, taskId in ipairs(taskIds) do
                local taskData = activityData:GetTaskData(taskId)
                if taskData then
                    local status = GEnums.ActivityConditionalTaskState.__CastFrom(taskData.Status)
                    if status == GEnums.ActivityConditionalTaskState.Unlocked or status == GEnums.ActivityConditionalTaskState.Completed then
                        found = true
                        self.m_selectedTabIndex = index
                        break
                    end
                end
            end
        end
        if not found then
            self.m_selectedTabIndex = 1
        end
    end

    local height = 0
    for i=2, self.m_selectedTabIndex do
        height = height + self.m_groupHeight[i-1] + self.view.taskListNode.contentVerticalLayoutGroup.spacing
    end
    self:_ScrollToTaskList(height, true, true)

    local currentTopHeight = self.view.taskListNode.content.anchoredPosition.y
    local h = 0
    for index, height in ipairs(self.m_groupHeight) do
        if h + height > currentTopHeight then
            self.m_topViewedTaskGroupIndex = index
            break
        else
            h = h + height + self.view.taskListNode.contentVerticalLayoutGroup.spacing
        end
    end

end

ContingencyContractTaskCtrl._UpdateContentHeight = HL.Method() << function(self)
    local activityData = GameInstance.player.activitySystem:GetActivity(self.m_activityId)
    local outerPadding = self.view.taskListNode.contentVerticalLayoutGroup.spacing
    local titleHeight = self.view.taskListNode.taskTypeCell.titleNodeLayoutElement.minHeight
    local cellHeight = self.view.taskListNode.taskTypeCell.taskCellLayoutElement.minHeight
    local innerPadding = self.view.taskListNode.taskTypeCell.taskLayout.spacing

    local height = 0
    for index, groupData in ipairs(self.m_groupData) do
        local taskCount = 0
        local taskIds = self.m_groupToTaskMap[groupData.taskGroupId]
        for _, taskId in ipairs(taskIds) do
            local taskData = activityData:GetTaskData(taskId)
            if taskData then
                local status = GEnums.ActivityConditionalTaskState.__CastFrom(taskData.Status)
                if status ~= GEnums.ActivityConditionalTaskState.Locked then
                    taskCount = taskCount + 1
                end
            end
        end
        self.m_groupHeight[index] = titleHeight + (cellHeight + innerPadding) * taskCount
        height = height +  self.m_groupHeight[index]
    end
    height = (self.m_groupCount - 1) * outerPadding + height

    UIUtils.setSizeDeltaY(self.view.taskListNode.content, height)
end

ContingencyContractTaskCtrl._OnTaskProgress = HL.Method(HL.Any) << function(self, arg)
    local activityId = unpack(arg)
    if activityId ~= self.m_activityId then
        return
    end

    self:_OnUpdate()
    if self.m_needUpdateRefresh then
        self.m_needUpdateRefresh = false
        if PhaseManager:GetTopPhaseId()==PHASE_ID then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_ACTIVITY_MODIFY_QUIT_TO_MENU)

            self:_OnClickTab(1,true, true)
            if DeviceInfo.usingController then

                local firstTab = self.m_tabCell[1]
                UIUtils.setAsNaviTarget(firstTab.button)
            end
        end
    end
end

ContingencyContractTaskCtrl._OnUpdate = HL.Method() << function(self)
    self:_UpdateContentHeight()


    local achievementId = Tables.activityAchievementDataTable[self.m_activityId].achievementId
    self.view.groupListNode.dungeonMedalCell:InitCommonMedalNode(achievementId)


    self.m_tabCache = self.m_tabCache or UIUtils.genCellCache(self.view.groupListNode.tabCell)
    self.m_tabCell = {}
    if not self.m_hasTabInit then
        self.m_tabCache:GraduallyRefresh(self.m_groupCount, self.view.config.TAB_SHOW_INTERVAL, function(cell, index)
            self:_OnUpdateTaskGroupTab(cell, index)
            if index==self.m_groupCount then
                self.m_hasTabInit = true
                self:_UpdateInit()
            end
        end)
    else
        self.m_tabCache:Refresh(self.m_groupCount, function(cell, index)
            self:_OnUpdateTaskGroupTab(cell, index)
        end)
    end


    self.m_taskListCache = self.m_taskListCache or UIUtils.genCellCache(self.view.taskListNode.taskTypeCell)
    self.m_listCell = {}

    if not self.m_hasTaskInit then

        self.m_taskListCache:Refresh(self.m_groupCount, function(cell, index)
            self:_StartCoroutine(function()
                coroutine.waitCondition(function()
                    return index <= self.m_topViewedTaskGroupIndex or self.m_taskTypeInitStatus[index - 1]
                end)
                self:_OnUpdateTaskList(cell, index, index >= self.m_topViewedTaskGroupIndex)
                cell.gameObject:SetActive(true)
            end)
        end, true)
    else
        self.m_taskListCache:Refresh(self.m_groupCount, function(cell, index)
            self:_OnUpdateTaskList(cell, index)
        end)
    end


    local waitToReceiveTasks = {}
    local activityData = GameInstance.player.activitySystem:GetActivity(self.m_activityId)
    local taskDataDict = activityData.taskDataDict
    for _, taskData in pairs(taskDataDict) do
        local status = GEnums.ActivityConditionalTaskState.__CastFrom(taskData.Status)
        if status == GEnums.ActivityConditionalTaskState.Completed then
            table.insert(waitToReceiveTasks, taskData.Id)
        end
    end
    local receiveAllState
    if #waitToReceiveTasks > 0 then
        receiveAllState = "NormalState"
        self.view.taskListNode.receiveAll.interactable = true
        self.view.taskListNode.receiveAll.onClick:RemoveAllListeners()
        self.view.taskListNode.receiveAll.onClick:AddListener(function()
            GameInstance.player.activitySystem:SendReceiveTaskRewardConditionMultiStage(self.m_activityId, waitToReceiveTasks)
        end)
    else
        receiveAllState = "DisableState"
        self.view.taskListNode.receiveAll.interactable = false
    end
    self.view.taskListNode.receiveAllStateCtrl:SetState(receiveAllState)
    if DeviceInfo.usingController then
        InputManagerInst:ToggleGroup(self.view.taskListNode.receiveAllBindingGroup.groupId, #waitToReceiveTasks > 0)
        self.view.taskListNode.receiveAllKeyHint.gameObject:SetActive(#waitToReceiveTasks > 0)
    end
end

ContingencyContractTaskCtrl._OnUpdateTaskGroupTab = HL.Method(HL.Any, HL.Number) << function(self, cell, index)

    self.m_tabCell[index] = cell
    cell.button.onClick:RemoveAllListeners()
    cell.button.onClick:AddListener(function()
        self:_OnClickTab(index, true)
    end)
    local groupData = self.m_groupData[index]
    cell.iconImg:LoadSprite(UIConst.UI_SPRITE_CONTINGENCY_CONTRACT, groupData.icon)
    cell.tabNameText.text = groupData.name
    cell.gameObject.name = "taskGroupTab_" .. groupData.taskGroupId
    self:_SetTabSelected(cell, index == self.m_selectedTabIndex)


    local activityData = GameInstance.player.activitySystem:GetActivity(self.m_activityId)
    local completedTaskCount = 0
    local unlockedTaskCount = 0
    for _, taskId in ipairs(self.m_groupToTaskMap[groupData.taskGroupId]) do
        local taskData = activityData:GetTaskData(taskId)
        if taskData then
            local status = GEnums.ActivityConditionalTaskState.__CastFrom(taskData.Status)
            if status ~= GEnums.ActivityConditionalTaskState.Locked then
                unlockedTaskCount = unlockedTaskCount + 1
            end
            if status == GEnums.ActivityConditionalTaskState.Completed or
                status == GEnums.ActivityConditionalTaskState.Rewarded then
                completedTaskCount = completedTaskCount + 1
            end
        end
    end
    cell.tabTaskNumTxt.text = string.format("%d/%d", completedTaskCount, unlockedTaskCount)
    local allTaskCount = self.m_groupData[index].canUpdate and self.m_groupData[index].totalTaskNum or unlockedTaskCount
    cell.completeNode.gameObject:SetActive(completedTaskCount == allTaskCount and true or false)


    local redDotArgs = {
        activityId = self.m_activityId,
        groupId = groupData.taskGroupId,
    }
    cell.redDot:InitRedDot("ActivityContingencyContractTaskTab", redDotArgs)

end

ContingencyContractTaskCtrl._OnUpdateTaskList = HL.Method(HL.Any, HL.Number, HL.Opt(HL.Boolean)) << function(self, cell, index, graduallyShow)
    local activityData = GameInstance.player.activitySystem:GetActivity(self.m_activityId)


    self.m_listCell[index] = cell
    local groupData = self.m_groupData[index]
    cell.listNameText.text = groupData.name
    cell.iconImg:LoadSprite(UIConst.UI_SPRITE_CONTINGENCY_CONTRACT, groupData.icon)
    cell.gameObject.name = "taskList_" .. groupData.taskGroupId
    self.m_rewardCacheTable[index] = self.m_rewardCacheTable[index] or {}


    local currentTaskCount = 0
    local currentTasks = {}
    local allTasks = self.m_groupToTaskMap[groupData.taskGroupId]
    local countdownState
    local latestUnlockTime
    for _, taskId in ipairs(allTasks) do
        local taskData = activityData:GetTaskData(taskId)
        if taskData then
            local status = GEnums.ActivityConditionalTaskState.__CastFrom(taskData.Status)
            if status == GEnums.ActivityConditionalTaskState.Locked then
                local unlockTime = taskData.UnlockTimestamp
                latestUnlockTime = latestUnlockTime and (latestUnlockTime <= unlockTime and latestUnlockTime or unlockTime) or unlockTime
            else
                table.insert(currentTasks, {
                    id = taskId,
                    completed = status == GEnums.ActivityConditionalTaskState.Completed and 0 or 1,
                    inProgress = status == GEnums.ActivityConditionalTaskState.Unlocked and 0 or 1,
                    sortId = self.m_taskInfoDict[taskId].sortId
                })
                currentTaskCount = currentTaskCount + 1
            end
        end
    end
    if groupData.canUpdate then
        local allTaskCount = self.m_groupData[index].totalTaskNum

        local hideTip = currentTaskCount ~= allTaskCount and not latestUnlockTime
        countdownState = hideTip and "None" or (latestUnlockTime and "Countdown" or "Updated")
        if latestUnlockTime then
            cell.countDownText:InitCountDownText(latestUnlockTime, function()
                self.m_needUpdateRefresh = true
            end)
        end
    else
        countdownState = "None"
    end
    cell.titleNode:SetState(countdownState)
    table.sort(currentTasks, Utils.genSortFunction({ "completed", "inProgress", "sortId" }, true))


    local taskCache = self.m_taskCacheTable[index]
    taskCache = taskCache or UIUtils.genCellCache(cell.taskCell)
    self.m_taskCacheTable[index] = taskCache
    self.m_taskCell[index] = {}
    if graduallyShow then
        taskCache:GraduallyRefresh(currentTaskCount, self.view.config.TASK_SHOW_INTERVAL, function(innerCell, innerIndex)
            self:_OnUpdateTask(innerCell, innerIndex, currentTasks, index)
            table.insert(self.m_taskCell[index], {cell = innerCell, id = currentTasks[innerIndex].id})

            if innerIndex == currentTaskCount then
                self.m_taskTypeInitStatus[index] = true

                if index == self.m_groupCount then
                    self.m_hasTaskInit = true
                    self:_UpdateInit()
                end
            end
        end)
    else
        taskCache:Refresh(currentTaskCount, function(innerCell, innerIndex)
            self:_OnUpdateTask(innerCell, innerIndex, currentTasks, index)
            table.insert(self.m_taskCell[index], {cell = innerCell, id = currentTasks[innerIndex].id})
        end)
    end
end

ContingencyContractTaskCtrl._OnUpdateTask = HL.Method(HL.Any, HL.Number, HL.Table, HL.Number) << function(self, cell, index, currentTasks, tabIndex)
    local activityData = GameInstance.player.activitySystem:GetActivity(self.m_activityId)
    local taskId = currentTasks[index].id
    local taskData = activityData:GetTaskData(taskId)

    cell.gameObject.name = "TaskCell" .. taskId
    cell.missionTxt.text = self.m_taskInfoDict[taskId].desc

    local rewardId = self.m_taskInfoDict[taskId].rewardId
    local rewardBundles = UIUtils.getRewardItems(rewardId)
    local rewardCellCache = self.m_rewardCacheTable[tabIndex][index] or UIUtils.genCellCache(cell.rewardItem)
    self.m_rewardCacheTable[tabIndex][index] = rewardCellCache
    local rewardCell = {}
    rewardCellCache:Refresh(#rewardBundles, function(innerCell, innerIndex)
        table.insert(rewardCell, innerCell)
        innerCell.view.stateCtrl:SetState("Normal")
        local reward = {
            id = rewardBundles[innerIndex].id,
            count = rewardBundles[innerIndex].count,
            forceHidePotentialStar = true,
        }
        innerCell:InitItem(reward, function()
            innerCell:ShowTips()
        end)
        innerCell:SetExtraInfo({
            tipsPosTransform = innerCell.view.content,
            isSideTips = true,
        })
    end)

    cell.rewardKeyHintRect:SetAsLastSibling()
    self.m_rewardCellTable[taskId] = rewardCell


    local state = "InProgress"


    local status = GEnums.ActivityConditionalTaskState.__CastFrom(taskData.Status)
    if status == GEnums.ActivityConditionalTaskState.Unlocked then
        local jumpId = self.m_taskInfoDict[taskId].jumpId
        if not string.isEmpty(jumpId) then
            state = "Goto"
            cell.btnGoto.onClick:RemoveAllListeners()
            cell.btnGoto.onClick:AddListener(function()

                ActivityUtils.setCcNewTaskRead(self.m_activityId, taskId)

                local cfg = Tables.systemJumpTable[jumpId]
                local phaseArgs = Json.decode(cfg.phaseArgs)
                local jumpTagIds = phaseArgs.tagIds
                local jumpShareCode = phaseArgs.shareCode
                local isValid = jumpTagIds and ContingencyContractUtils.CheckJoinedTagListValid(self.m_gameId, jumpTagIds, true) or ContingencyContractUtils.AnalyzeShareCode(self.m_gameId, jumpShareCode, true)

                if isValid then

                    Notify(MessageConst.SHOW_POP_UP, {
                        content = Language.LUA_ACTIVITY_CONTINGENCY_CONTRACT_JUMP_CONFIRM_POP_UP,
                        warningContent = Language.LUA_CONTINGENCY_CONTRACT_SHARE_CONFIRM_POP_UP_WARN_TITLE,
                        onConfirm = function()
                            Utils.jumpToSystem(jumpId)
                            self.m_readTasks[taskId] = true
                            self:_UpdateReadInfo()
                        end
                    })
                else

                    Notify(MessageConst.SHOW_POP_UP, {
                        content = Language.LUA_ACTIVITY_CONTINGENCY_CONTRACT_JUMP_HAS_LOCKED_TAG_CONFIRM_POP_UP,
                        onConfirm = function()
                            PhaseManager:OpenPhase(PhaseId.ContingencySelectTag, {
                                gameId = self.m_gameId
                            })
                            self.m_readTasks[taskId] = true
                            self:_UpdateReadInfo()
                        end
                    })
                end
            end)
        else
            state = "InProgress"
        end

    elseif status == GEnums.ActivityConditionalTaskState.Completed then
        state = "Complete"
        cell.completedNode.onClick:RemoveAllListeners()
        cell.completedNode.onClick:AddListener(function()
            GameInstance.player.activitySystem:SendReceiveTaskRewardConditionMultiStage(self.m_activityId, { taskId })
        end)

    else
        state = "Received"
        for _, rewardCell in ipairs(self.m_rewardCellTable[taskId]) do
            rewardCell.view.stateCtrl:SetState("Received")
        end
    end

    cell.stateController:SetState(state)


    local conditionIdList = self.m_taskInfoDict[taskId].completeConditionId
    local isInProgress = status == GEnums.ActivityConditionalTaskState.Unlocked
    local progress = 0
    local target = 0
    for i = 1, conditionIdList.Count do
        local conditionId = conditionIdList[CSIndex(i)]
        if isInProgress then
            local _, val = taskData.Conditions.Values:TryGetValue(conditionId)
            progress = progress + val
        end
        target = target + Tables.activityConditionalMultiStageTaskCompleteConditionTable[conditionId].progressToCompare
    end
    if not isInProgress then
        progress = target
    end
    cell.numberTxt.text = string.format("%d/%d", progress, target)
    cell.handle.fillAmount = target ~= 0 and progress / target or 1


    local redDotArgs = {
        activityId = self.m_activityId,
        taskId = taskId,
    }
    cell.redDot:InitRedDot("ActivityContingencyContractSingleTask", redDotArgs, nil, self.view.taskListNode.redDotScrollRect)

    if self.view.taskListNode.scrollView:IsCellViewed(cell.rectTransform) then
       self.m_readTasks[taskId] = true
    end


    if DeviceInfo.usingController then

        cell.rewardsKeyHint.gameObject:SetActive(false)
        cell.naviDecorator.onIsNaviTargetChanged = function(isTarget)
            cell.rewardsKeyHint.gameObject:SetActive(isTarget)
            if isTarget then

                ActivityUtils.setCcNewTaskRead(self.m_activityId, taskId)

                self:_OnClickTab(tabIndex, false)
            end
        end
    end
end

ContingencyContractTaskCtrl._GetTab = HL.Method(HL.Number).Return(HL.Any) << function(self, index)
    return self.m_tabCell[index]
end

ContingencyContractTaskCtrl._GetList = HL.Method(HL.Number).Return(HL.Any) << function(self, index)
    return self.m_listCell[index]
end

ContingencyContractTaskCtrl._OnClickTab = HL.Method(HL.Number, HL.Boolean, HL.Opt(HL.Boolean)) << function(self, index, needScroll, noTween)

    if index ~= self.m_selectedTabIndex then
        local oldTab = self:_GetTab(self.m_selectedTabIndex)
        if oldTab then
            self:_SetTabSelected(oldTab, false)
        end
        self.m_selectedTabIndex = index
        local newTab = self:_GetTab(index)
        if newTab then
            self:_SetTabSelected(newTab, true)
        end
    end
    if needScroll then
        self:_ScrollToTaskList(index, noTween)
    end

end

ContingencyContractTaskCtrl._SetTabSelected = HL.Method(HL.Any, HL.Boolean) << function(self, tabCell, isSelected)
    if tabCell.stateController.currentStateName == "Select" and not isSelected then
        tabCell.animationWrapper:Play(self.view.config.TAB_SLC_OUT_ANIM)
    elseif tabCell.stateController.currentStateName ~= "Select" and isSelected then
        tabCell.animationWrapper:Play(self.view.config.TAB_SLC_ANIM)
    end
    tabCell.stateController:SetState(isSelected and "Select" or "Unselect")
end

ContingencyContractTaskCtrl._ScrollToTaskList = HL.Method(HL.Number, HL.Opt(HL.Boolean, HL.Boolean)) << function(self, value, noTween, isPos)
    if not isPos then
        local index = value
        local listCell = self:_GetList(index)
        if not listCell then
            return
        end
        value = -listCell.rectTransform.anchoredPosition.y
    end
    local scrollView = self.view.taskListNode.scrollView
    local scrollViewRect = self.view.taskListNode.scrollViewRectTransform
    local contentRect = self.view.taskListNode.content


    local maxScrollDistance = contentRect.rect.height - scrollViewRect.rect.height
    if maxScrollDistance <= 0 then
        return
    end

    local targetLocalY = value
    if targetLocalY > maxScrollDistance then
        targetLocalY = maxScrollDistance
    end
    local targetPos = contentRect.anchoredPosition
    targetPos.y = targetLocalY

    scrollView:ScrollTo(targetPos, noTween or false)
end



ContingencyContractTaskCtrl._UpdateTabByScrollPos = HL.Method() << function(self)
    local scrollViewRect = self.view.taskListNode.scrollViewRectTransform
    local content = self.view.taskListNode.content
    LayoutRebuilder.ForceRebuildLayoutImmediate(content)
    local judgeLineLocalY = -420

    local closestIndex = 1
    local minDistance = math.huge
    local maxScrollDistance = content.rect.height - scrollViewRect.rect.height
    for i, cell in ipairs(self.m_listCell) do
        local cellLocalY = cell.rectTransform.anchoredPosition.y



        local cellToViewLocalY = cellLocalY + content.anchoredPosition.y
        local distToJudgeLine = -judgeLineLocalY + cellToViewLocalY
        if distToJudgeLine >= 0 and distToJudgeLine < minDistance then
            minDistance = distToJudgeLine
            closestIndex = i
        end


    end
    self:_OnClickTab(closestIndex, false)
end

ContingencyContractTaskCtrl._CheckCanScrollTo = HL.Method(HL.Number, HL.Number).Return(HL.Boolean) << function(self, index, judgeLineLocalY)
    local scrollViewRect = self.view.taskListNode.scrollViewRectTransform
    local content = self.view.taskListNode.content
    LayoutRebuilder.ForceRebuildLayoutImmediate(content)
    local maxDistance = content.rect.height - scrollViewRect.rect.height
    local listCell = self:_GetList(index)
    if not listCell then
        return false
    end
    local cellLocalY = listCell.rectTransform.anchoredPosition.y
    if cellLocalY + maxDistance < judgeLineLocalY then
        return false
    else
        return true
    end
end

ContingencyContractTaskCtrl._SetNavi = HL.Method() << function(self)

    if DeviceInfo.usingController then
        local selectedTab = self.m_tabCell[self.m_selectedTabIndex]
        self:SetNaviTarget(selectedTab.button)


        self.view.groupListNode.naviGroup.onDefaultNaviFailed:AddListener(function(dir)
            if dir == Unity.UI.NaviDirection.Right then
                self:SetNaviTarget(self.m_taskCell[self.m_selectedTabIndex][1].cell.naviDecorator)
            end
        end)

        self.view.taskListNode.scrollNaviGroup.onDefaultNaviFailed:AddListener(function(dir)
            if dir == Unity.UI.NaviDirection.Left then
                local targetTab = self.m_tabCell[self.m_selectedTabIndex]


                self:SetNaviTarget(targetTab.button)

            end
        end)

        local viewBindingId = self:BindInputPlayerAction("common_confirm", function()
            self:SetNaviTarget(self.m_taskCell[self.m_selectedTabIndex][1].cell.naviDecorator)
        end)
        local backToTabBindingId = self:BindInputPlayerAction("common_back", function()
            self:SetNaviTarget(self.m_tabCell[self.m_selectedTabIndex].button)
        end)
        InputManagerInst:ToggleBinding(viewBindingId, true)
        InputManagerInst:ToggleBinding(backToTabBindingId, false)
        self.view.taskListNode.scrollNaviGroup.onIsTopLayerChanged:AddListener(function(active)
            InputManagerInst:ToggleBinding(viewBindingId, not active)
            InputManagerInst:ToggleBinding(backToTabBindingId, active)
        end)


        self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.bindingGroup.groupId })
    end
end

ContingencyContractTaskCtrl._UpdateReadInfo = HL.Method() << function(self)
    for id,_ in pairs(self.m_readTasks) do
        if ActivityUtils.isCcNewTask(self.m_activityId, id) then
           ActivityUtils.setCcNewTaskRead(self.m_activityId, id)
        end
    end
end

ContingencyContractTaskCtrl.GetCurPhaseStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    return {
        activityId = self.m_activityId,
        recoverState = {

            selectedTabIndex = self.m_selectedTabIndex,

            popupState = self:GetRecoverPopupStateArg(),
        },
    }
end

ContingencyContractTaskCtrl.GetRecoverPopupStateArg = HL.Method().Return(HL.Opt(HL.Any)) << function(self)
    if PhaseManager:GetTopPhaseId() ~= PHASE_ID then
        return
    end
    local isOpen, instructionBookCtrl = UIManager:IsOpen(PanelId.InstructionBook)
    if not isOpen or not instructionBookCtrl or not instructionBookCtrl:IsShow() then
        return
    end
    local _, activityCfg = Tables.activityTable:TryGetValue(self.m_activityId)
    local instructionId = activityCfg and activityCfg.instructionId or nil
    if string.isEmpty(instructionId) or instructionBookCtrl.id ~= instructionId then
        return
    end
    return {
        popupType = "InstructionBook",
    }
end

ContingencyContractTaskCtrl.TryRecoverPopupState = HL.Method(HL.Any) << function(self, popupState)
    if popupState == nil or string.isEmpty(popupState.popupType) then
        return
    end
    if PhaseManager:GetTopPhaseId() ~= PHASE_ID then
        return
    end
    if popupState.popupType ~= "InstructionBook" then
        return
    end
    local _, activityCfg = Tables.activityTable:TryGetValue(self.m_activityId)
    local instructionId = activityCfg and activityCfg.instructionId or nil
    if string.isEmpty(instructionId) then
        return
    end
    local isOpen, instructionBookCtrl = UIManager:IsOpen(PanelId.InstructionBook)
    if isOpen and instructionBookCtrl and instructionBookCtrl.id == instructionId and instructionBookCtrl:IsShow() then
        return
    end
    UIManager:Open(PanelId.InstructionBook, instructionId)
end

ContingencyContractTaskCtrl._UpdateInit = HL.Method() << function(self)
    if self.m_hasTabInit and self.m_hasTaskInit then

        self:_SetNavi()

        if self.m_pendingRecoverPopupState ~= nil then
            local popupState = self.m_pendingRecoverPopupState
            self.m_pendingRecoverPopupState = nil
            self:TryRecoverPopupState(popupState)
        end
    end
end

HL.Commit(ContingencyContractTaskCtrl)
