local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ReflowFormalReconnectTask

ReflowFormalReconnectTaskCtrl = HL.Class('ReflowFormalReconnectTaskCtrl', uiCtrl.UICtrl)






ReflowFormalReconnectTaskCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.SHOW_REFLOW_MILESTONE_TASK_REWARD] = '_OnShowTaskReward',
    [MessageConst.ON_REFLOW_MILESTONE_UPDATE] = '_OnMilestoneUpdate',
    [MessageConst.ON_CONDITIONAL_MULTI_STAGE_TASK_PROGRESS_CHANGE] = '_OnTaskProgress',
}

ReflowFormalReconnectTaskCtrl.m_activityId = HL.Field(HL.String) << ''

ReflowFormalReconnectTaskCtrl.m_milestoneId = HL.Field(HL.String) << ''

ReflowFormalReconnectTaskCtrl.m_milestoneData = HL.Field(HL.Any)

ReflowFormalReconnectTaskCtrl.m_stagesCfg = HL.Field(HL.Table)

ReflowFormalReconnectTaskCtrl.m_taskGroupCfg = HL.Field(HL.Table)

ReflowFormalReconnectTaskCtrl.m_taskCfgDict = HL.Field(HL.Table)

ReflowFormalReconnectTaskCtrl.m_stageCellCache = HL.Field(HL.Any)

ReflowFormalReconnectTaskCtrl.m_genTaskCellFunction = HL.Field(HL.Function)

ReflowFormalReconnectTaskCtrl.m_taskGroupCellCache = HL.Field(HL.Any)

ReflowFormalReconnectTaskCtrl.m_rewardCacheTable = HL.Field(HL.Table)

ReflowFormalReconnectTaskCtrl.m_selectedGroupIndex = HL.Field(HL.Number) << -1

ReflowFormalReconnectTaskCtrl.m_selectedTaskIndex = HL.Field(HL.Number) << -1

ReflowFormalReconnectTaskCtrl.m_receivedTaskScore = HL.Field(HL.Number) << -1

ReflowFormalReconnectTaskCtrl.m_waitToReceiveTasks = HL.Field(HL.Table)

ReflowFormalReconnectTaskCtrl.m_isFirstShow = HL.Field(HL.Boolean) << true


ReflowFormalReconnectTaskCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:_InitData(arg)
    self:_InitUI()
    self:_RefreshAllUIs(true)
end

ReflowFormalReconnectTaskCtrl.OnShow = HL.Override() << function(self)
    if not self.m_isFirstShow then
        self.view.taskScrollList:UpdateCount(#self.m_taskGroupCfg[self.m_selectedGroupIndex].taskSortedList, true)
    else
        self.m_isFirstShow = false
    end
    if DeviceInfo.usingController then
        local firstCell = self.m_genTaskCellFunction(1)
        if firstCell then
            self:SetNaviTarget(firstCell.naviDecorator)
        end
        UIUtils.bindHyperlinkPopup(self, "reflowTask",self.view.inputGroup.groupId, "hyperlink_show_popup")
    end
end

ReflowFormalReconnectTaskCtrl._InitData = HL.Method(HL.Any) << function(self,arg)
    self.m_activityId = arg.activityId
    local activity = GameInstance.player.activitySystem:GetActivity(self.m_activityId)
    local _, milestoneCfg = Tables.activityReflowTable:TryGetValue(self.m_activityId)
    local _, taskGroupCfg = Tables.activityTaskGroupTable:TryGetValue(self.m_activityId)
    local _, taskDetailCfg = Tables.activityConditionalMultiStageTaskConfigTable:TryGetValue(self.m_activityId)
    self.m_milestoneId = milestoneCfg.activityMilestoneId
    local _, milestoneData = activity.milestones:TryGetValue(self.m_milestoneId)
    self.m_milestoneData = milestoneData
    self.m_stagesCfg = {}
    self.m_taskGroupCfg = {}
    self.m_taskCfgDict = {}
    self.m_rewardCacheTable = {}
    self.m_selectedTaskIndex = 1

    for _, stage in pairs(milestoneCfg.stages) do
        table.insert(self.m_stagesCfg, stage)
        table.sort(self.m_stagesCfg, Utils.genSortFunction({ "pointRequired"}, true))
    end
    for _, group in pairs(taskGroupCfg.TaskGroupConfig) do
        table.insert(self.m_taskGroupCfg, group)
        table.sort(self.m_taskGroupCfg, Utils.genSortFunction({ "sortId"}, true))
    end
    for _, task in pairs(taskDetailCfg.TaskConfigMap) do
        local id = task.taskId
        local _, taskInfo = milestoneCfg.tasks:TryGetValue(id)
        self.m_taskCfgDict[id] = {
            task = task,
            score = taskInfo.point,
        }
    end

    self.view.titleTxt:SetAndResolveTextStyle(milestoneCfg.reflowCfg.taskTip)
end

ReflowFormalReconnectTaskCtrl._InitUI = HL.Method() << function(self)
    self.m_stageCellCache = UIUtils.genCellCache(self.view.milestoneNode.stageCell)
    self.m_taskGroupCellCache = UIUtils.genCellCache(self.view.groupCell)
    self.view.taskScrollList.onUpdateCell:AddListener(function(go, csIndex)
        self:_OnTaskCellUpdate(go, csIndex)
    end)
    self.m_genTaskCellFunction = UIUtils.genCachedCellFunction(self.view.taskScrollList)
end

ReflowFormalReconnectTaskCtrl._RefreshAllUIs = HL.Method(HL.Boolean) << function(self, isInit)
    self:_RefreshMilestoneUIs(isInit)
    self:_RefreshTaskUIs(isInit)
end

ReflowFormalReconnectTaskCtrl._RefreshMilestoneUIs = HL.Method(HL.Boolean) << function(self, isInit)

    local milestoneNode = self.view.milestoneNode
    local totalScore = self.m_milestoneData.totalPoint
    local highestScore = self.m_stagesCfg[#self.m_stagesCfg].pointRequired
    milestoneNode.totalScore.text = totalScore
    milestoneNode.stateController:SetState(totalScore>=highestScore and "Max" or "Num")

    local occupied = 0
    local firstStageScore = self.m_stagesCfg[1].pointRequired
    local firstStageOccupied = milestoneNode.itemLayout.padding.left
    local length = firstStageOccupied + milestoneNode.itemLayout.spacing * (#self.m_stagesCfg-1)
    if totalScore < firstStageScore then
        occupied = totalScore/firstStageScore * firstStageOccupied
    else
        occupied = firstStageOccupied + (totalScore-firstStageScore)/(highestScore-firstStageScore) * milestoneNode.itemLayout.spacing * (#self.m_stagesCfg-1)
    end
    milestoneNode.progressBar.value = occupied/length


    self.m_stageCellCache:Refresh(#self.m_stagesCfg, function(cell, luaIndex)
        self:_OnStageCellUpdate(cell, luaIndex, isInit)
    end)

    if DeviceInfo.usingController then
         if isInit then
            milestoneNode.virtualReceiveBtn.onClick:AddListener(function()
                GameInstance.player.activitySystem:SendGainReflowMilestoneReward(self.m_activityId, self.m_milestoneId, "", true)
            end)
         end

         local canReceive = false
         for _, stageData in pairs(self.m_milestoneData.stages) do
             if stageData.reached and not stageData.rewarded then
                 canReceive = true
                 break
             end
         end
         milestoneNode.virtualReceiveBtn.gameObject:SetActive(canReceive)
         milestoneNode.barNodeNaviGroup.enabled = not canReceive
         milestoneNode.focusKeyHint.gameObject:SetActive(not canReceive)
    end
end

ReflowFormalReconnectTaskCtrl._RefreshTaskUIs = HL.Method(HL.Boolean) << function(self, isInit)

    local found = not isInit
    for index, cfg in ipairs(self.m_taskGroupCfg) do
        local group = isInit and cfg or cfg.group
        local taskSortedList = {}
        local completedTaskCount = 0
        local receivedTaskCount = 0
        for _, taskId in pairs(group.tasks) do
            local _, taskData = self.m_milestoneData.tasks:TryGetValue(taskId)
            if taskData then
                local status = GEnums.ActivityConditionalTaskState.__CastFrom(taskData.status)
                local isCompleted = status == GEnums.ActivityConditionalTaskState.Completed
                local isRewarded = status == GEnums.ActivityConditionalTaskState.Rewarded
                local isInProgress = status == GEnums.ActivityConditionalTaskState.Unlocked

                if not found and not isRewarded then
                    found = true
                    self.m_selectedGroupIndex = index
                end

                if isCompleted or isRewarded then
                    completedTaskCount = completedTaskCount + 1
                end

                if isRewarded then
                    receivedTaskCount = receivedTaskCount + 1
                end
                table.insert(taskSortedList, {
                    id = taskId,
                    completed = isCompleted and 0 or 1,
                    inProgress = isInProgress and 0 or 1,
                    sortId = self.m_taskCfgDict[taskId].task.sortId,
                })
            end
        end
        table.sort(taskSortedList, Utils.genSortFunction({ "completed", "inProgress", "sortId" }, true))
        self.m_taskGroupCfg[index] = {
            group = group,
            taskSortedList = taskSortedList,
            completedTaskCount = completedTaskCount,
            receivedTaskCount = receivedTaskCount,
        }
    end
    if not found then
        self.m_selectedGroupIndex = 1
    end


    self.m_taskGroupCellCache:Refresh(#self.m_taskGroupCfg, function(cell, luaIndex)
        self:_OnGroupCellUpdate(cell, luaIndex, isInit)
    end)
    self.view.taskScrollList:UpdateCount(#self.m_taskGroupCfg[self.m_selectedGroupIndex].taskSortedList)
end

ReflowFormalReconnectTaskCtrl._OnStageCellUpdate = HL.Method(HL.Any, HL.Number, HL.Boolean) << function(self, cell, luaIndex, isInit)
    local stageCfg = self.m_stagesCfg[luaIndex]
    local stageId = stageCfg.milestoneStageId
    local _, stageData = self.m_milestoneData.stages:TryGetValue(stageId)

    cell.score.text = stageCfg.pointRequired
    cell.stateController:SetState(stageData.reached and "Reached" or "Nrl")
    cell.reward.view.rewardedCover.gameObject:SetActive(stageData.rewarded)
    cell.bgReceive.gameObject:SetActive(stageData.reached and not stageData.rewarded)
    if isInit then

        local rewardId = stageCfg.rewardId
        local rewardBundles = UIUtils.getRewardItems(rewardId)
        local reward = {
            id = rewardBundles[1].id,
            count = rewardBundles[1].count,
            forceHidePotentialStar = true,
        }
        cell.reward:InitItem(reward, function()
            if stageData.reached and not stageData.rewarded then

                GameInstance.player.activitySystem:SendGainReflowMilestoneReward(self.m_activityId, self.m_milestoneId, stageId, true)
            else
                cell.reward:ShowTips()
            end
        end)
        cell.reward:SetExtraInfo({
            tipsPosTransform = cell.reward.view.content,
            isSideTips = true,
        })

        cell.redDot:InitRedDot("ActivityReflowSingleMilestoneStage", {activityId = self.m_activityId, milestoneId = self.m_milestoneId, stageId = stageId})
    end
end

ReflowFormalReconnectTaskCtrl._OnGroupCellUpdate = HL.Method(HL.Any, HL.Number, HL.Boolean) << function(self, cell, luaIndex, isInit)
    local groupCfg = self.m_taskGroupCfg[luaIndex].group

    local allTasksCount = #groupCfg.tasks
    local completedTaskCount =  self.m_taskGroupCfg[luaIndex].completedTaskCount
    local receivedTaskCount =  self.m_taskGroupCfg[luaIndex].receivedTaskCount
    local allReceived = allTasksCount == receivedTaskCount
    cell.progressTxt.text = string.format("%d/%d", completedTaskCount, #groupCfg.tasks)
    if allReceived then
        cell.stateController:SetState(self.m_selectedGroupIndex == luaIndex and "SelectFinish" or "NorFinish")
    else
        cell.stateController:SetState(self.m_selectedGroupIndex == luaIndex and "SelectUnfinish" or "NorUnfinish")
    end
    if isInit then
        cell.nameTxt.text = groupCfg.name
        cell.iconSortImg:LoadSprite(UIConst.UI_SPRITE_REFLOW, groupCfg.icon)
        cell.redDot:InitRedDot("ActivityReflowSingleTaskGroup", {activityId = self.m_activityId, milestoneId = self.m_milestoneId, taskGroupId = groupCfg.taskGroupId})
        cell.toggle.isOn = self.m_selectedGroupIndex == luaIndex
        cell.toggle.onValueChanged:AddListener(function(isOn)
            if isOn then
                self:_OnTabClicked(luaIndex)
            end
            local completedTaskCount =  self.m_taskGroupCfg[luaIndex].completedTaskCount
            local receivedTaskCount =  self.m_taskGroupCfg[luaIndex].receivedTaskCount
            local allReceived = allTasksCount == receivedTaskCount
            if allReceived then
                cell.stateController:SetState(isOn and "SelectFinish" or "NorFinish")
            else
                cell.stateController:SetState(isOn and "SelectUnfinish" or "NorUnfinish")
            end
        end)
    end
end

ReflowFormalReconnectTaskCtrl._OnTabClicked = HL.Method(HL.Number) << function(self, groupIndex)
    if groupIndex == self.m_selectedGroupIndex then
        return
    end
    self.m_selectedGroupIndex = groupIndex
    self.view.taskScrollList:UpdateCount(#self.m_taskGroupCfg[self.m_selectedGroupIndex].taskSortedList)
end

ReflowFormalReconnectTaskCtrl._RefreshWaitToReceiveTasks = HL.Method() << function(self)
    self.m_waitToReceiveTasks = {}
    self.m_receivedTaskScore = 0
    for _, taskId in pairs(self.m_taskGroupCfg[self.m_selectedGroupIndex].group.tasks) do
        local _, taskData = self.m_milestoneData.tasks:TryGetValue(taskId)
        if taskData then
            local status = GEnums.ActivityConditionalTaskState.__CastFrom(taskData.status)
            local isCompleted = status == GEnums.ActivityConditionalTaskState.Completed
            if isCompleted then
                table.insert(self.m_waitToReceiveTasks, taskId)
                self.m_receivedTaskScore = self.m_receivedTaskScore + self.m_taskCfgDict[taskId].score
            end
        end
    end
end

ReflowFormalReconnectTaskCtrl._OnTaskCellUpdate = HL.Method(HL.Any, HL.Number) << function(self, go, csIndex)
    local cell = self.m_genTaskCellFunction(go)
    local index = LuaIndex(csIndex)
    local taskId = self.m_taskGroupCfg[self.m_selectedGroupIndex].taskSortedList[index].id
    local taskCfg = self.m_taskCfgDict[taskId].task
    local _, taskData = self.m_milestoneData.tasks:TryGetValue(taskId)
    cell.descTxt:SetAndResolveTextStyle(taskCfg.desc)
    cell.scoreTxt.text = self.m_taskCfgDict[taskId].score



    local state
    local status = GEnums.ActivityConditionalTaskState.__CastFrom(taskData.status)
    local isInProgress = status == GEnums.ActivityConditionalTaskState.Unlocked
    local isCompleted = status == GEnums.ActivityConditionalTaskState.Completed
    local isRewarded = status == GEnums.ActivityConditionalTaskState.Rewarded
    if isInProgress then
        local jumpId = taskCfg.jumpId
        if not string.isEmpty(jumpId) then
            state = "Goto"
            cell.btnGoto.onClick:RemoveAllListeners()
            cell.btnGoto.onClick:AddListener(function()
                Utils.jumpToSystem(jumpId)
            end)
        else
            state = "InProgress"
        end

    elseif isCompleted then
        state = "Complete"
        cell.receiveBtnState:SetState("YellowState")
        cell.receiveBtn.onClick:RemoveAllListeners()
        cell.receiveBtn.onClick:AddListener(function()

            self:_RefreshWaitToReceiveTasks()
            GameInstance.player.activitySystem:SendReceiveTaskRewardConditionMultiStage(self.m_activityId, self.m_waitToReceiveTasks)
        end)

    else
        state = "Received"
    end
    cell.nodeState:SetState(state)


    local rewardId = taskCfg.rewardId
    local rewardBundles = UIUtils.getRewardItems(rewardId)
    local rewardCellCache = self.m_rewardCacheTable[go] or UIUtils.genCellCache(cell.rewardItem)
    self.m_rewardCacheTable[go] = rewardCellCache
    rewardCellCache:Refresh(#rewardBundles, function(innerCell, innerIndex)
        innerCell.view.rewardedCover.gameObject:SetActive(isRewarded)
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


    local conditionIdList = taskCfg.completeConditionId
    local progress = 0
    local target = 0
    for i = 1, conditionIdList.Count do
        local conditionId = conditionIdList[CSIndex(i)]
        if isInProgress then
            local _, val = taskData.conditions.Values:TryGetValue(conditionId)
            progress = progress + val
        end
        target = target + Tables.activityConditionalMultiStageTaskCompleteConditionTable[conditionId].progressToCompare
    end
    if not isInProgress then
        progress = target
    end
    cell.progressTxt.text = string.format("%d/%d", progress, target)
    cell.progressBar.value = target ~= 0 and progress / target or 1


    cell.redDot:InitRedDot("ActivityReflowSingleTask", {activityId = self.m_activityId, milestoneId = self.m_milestoneId ,taskId = taskId})

    if DeviceInfo.usingController then

        cell.rewardKeyHint.gameObject:SetActive(false)
        cell.naviDecorator.onIsNaviTargetChanged = function(isTarget)
            cell.rewardKeyHint.gameObject:SetActive(isTarget)
            if isTarget then
                self.m_selectedTaskIndex = index
            end
        end
    end
end

ReflowFormalReconnectTaskCtrl._OnMilestoneUpdate = HL.Method(HL.Any) << function(self, arg)
    local activityId = unpack(arg)
    if activityId ~= self.m_activityId then
        return
    end

    self:_RefreshMilestoneUIs(false)
    self:_RefreshTaskUIs(false)
end

ReflowFormalReconnectTaskCtrl._OnTaskProgress = HL.Method(HL.Any) << function(self, arg)
    local activityId = unpack(arg)
    if activityId ~= self.m_activityId then
        return
    end

    self:_RefreshTaskUIs(false)
end

ReflowFormalReconnectTaskCtrl._OnShowTaskReward = HL.Method(HL.Any) << function(self, arg)
    local _, items, chars = unpack(arg)
    Notify(MessageConst.SHOW_SYSTEM_REWARDS, {
        items = items,
        chars = chars,
        extraScore = self.m_receivedTaskScore,
        hideDecoArrow = true,
    })

    if DeviceInfo.usingController then
        local firstCell = self.m_genTaskCellFunction(1)
        if firstCell then
            self:SetNaviTarget(firstCell.naviDecorator)
        end
    end
end

HL.Commit(ReflowFormalReconnectTaskCtrl)
