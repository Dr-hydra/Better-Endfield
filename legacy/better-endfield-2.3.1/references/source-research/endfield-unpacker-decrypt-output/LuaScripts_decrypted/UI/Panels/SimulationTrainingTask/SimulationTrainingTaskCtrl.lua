local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.SimulationTrainingTask
local PHASE_ID = PhaseId.SimulationTrainingTask
SimulationTrainingTaskCtrl = HL.Class('SimulationTrainingTaskCtrl', uiCtrl.UICtrl)






SimulationTrainingTaskCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_CONDITIONAL_MULTI_STAGE_TASK_PROGRESS_CHANGE] = '_OnTaskProgress',
    [MessageConst.ON_ACTIVITY_UPDATED] = '_OnLimitedActivityEnd',
}

SimulationTrainingTaskCtrl.m_activityId = HL.Field(HL.String) << ""
SimulationTrainingTaskCtrl.m_enterFromActivityCenter = HL.Field(HL.Boolean) << false
SimulationTrainingTaskCtrl.m_taskCellCache = HL.Field(HL.Any)
SimulationTrainingTaskCtrl.m_taskStatusInfo = HL.Field(HL.Table)
SimulationTrainingTaskCtrl.m_taskConfigInfo = HL.Field(HL.Table)
SimulationTrainingTaskCtrl.m_waitToReceiveTasks = HL.Field(HL.Table)
SimulationTrainingTaskCtrl.m_rewardCacheTable = HL.Field(HL.Table)
SimulationTrainingTaskCtrl.m_taskCells = HL.Field(HL.Table)
SimulationTrainingTaskCtrl.m_readTasks = HL.Field(HL.Table)
SimulationTrainingTaskCtrl.m_getListCell = HL.Field(HL.Function)
SimulationTrainingTaskCtrl.m_isInit = HL.Field(HL.Boolean) << false



SimulationTrainingTaskCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_isInit = true
    self:_InitData(arg)
    self:_InitUI()
    self:_UpdateData()
    self:_RefreshAllUIs(true)
    self.m_isInit = false
end

SimulationTrainingTaskCtrl._InitData = HL.Method(HL.Any) << function(self, arg)
    self.m_enterFromActivityCenter = arg and arg.enterFromActivityCenter or false
    self.m_activityId = Tables.simulationTrainingConst.simulationTrainingLimitedActivityId
    self.m_taskConfigInfo = {}
    self.m_rewardCacheTable = {}
    self.m_taskCells = {}
    self.m_readTasks = {}
end

SimulationTrainingTaskCtrl._InitUI = HL.Method() << function(self)
    self.view.btnBack.onClick:AddListener(function()
        PhaseManager:PopPhase(PHASE_ID)
    end)

    self.m_getListCell = UIUtils.genCachedCellFunction(self.view.typhoeaArcheryTaskScrollList)
    self.view.typhoeaArcheryTaskScrollList.onUpdateCell:AddListener(function(obj, csIndex)
        self:_UpdateTaskCells(self.m_getListCell(obj), LuaIndex(csIndex))
    end)

    self.m_taskCellCache = UIUtils.genCellCache(self.view.taskCell)

    local activityData = GameInstance.player.activitySystem:GetActivity(self.m_activityId)
    if activityData then
        self.view.countDownText:InitCountDownText(activityData.endTime)
    end
end

SimulationTrainingTaskCtrl._UpdateData = HL.Method() << function(self)

    self.m_taskStatusInfo = {}
    self.m_waitToReceiveTasks = {}
    local activityData = GameInstance.player.activitySystem:GetActivity(self.m_activityId)
    if activityData == nil then
        return
    end

    local _, taskConfig = Tables.activityConditionalMultiStageTaskConfigTable:TryGetValue(self.m_activityId)
    for id, singleTaskConfig in pairs(taskConfig.TaskConfigMap) do
        self.m_taskConfigInfo[id] = singleTaskConfig
        local taskStatusData = activityData:GetTaskData(id)
        local status
        if taskStatusData ~= nil then

            status = GEnums.ActivityConditionalTaskState.__CastFrom(taskStatusData.Status)

            if status == GEnums.ActivityConditionalTaskState.Completed then
                table.insert(self.m_waitToReceiveTasks, id)
            end
            table.insert(self.m_taskStatusInfo, {
                id = id,
                toReceive = status == GEnums.ActivityConditionalTaskState.Completed and 0 or 1,
                inProgress = status == GEnums.ActivityConditionalTaskState.Unlocked and 0 or 1,
                rewarded = status == GEnums.ActivityConditionalTaskState.Rewarded and 0 or 1,
                sortId = singleTaskConfig.sortId
            })
        end
    end

    table.sort(self.m_taskStatusInfo, Utils.genSortFunction({ "toReceive", "inProgress", "rewarded", "sortId"}, true))
end

SimulationTrainingTaskCtrl._RefreshAllUIs = HL.Method(HL.Boolean) << function(self, isInit)


    self.view.typhoeaArcheryTaskScrollList:UpdateCount(#self.m_taskStatusInfo)





    local hasWaitToReceiveTasks = #self.m_waitToReceiveTasks > 0
    self.view.receiveAllNode.stateController:SetState(hasWaitToReceiveTasks and "NormalState" or "DisableState")
    self.view.receiveAllNode.button.interactable = hasWaitToReceiveTasks
    if hasWaitToReceiveTasks then
        self.view.receiveAllNode.button.onClick:RemoveAllListeners()
        self.view.receiveAllNode.button.onClick:AddListener(function()
            GameInstance.player.activitySystem:SendReceiveTaskRewardConditionMultiStage(self.m_activityId, self.m_waitToReceiveTasks)
        end)
    end


    if DeviceInfo.usingController then
        if self.m_isInit then
            self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.bindingGroup.groupId })
            self:SetNaviTarget(self.m_taskCells[1].cell.naviDecorator)
        end

        InputManagerInst:ToggleGroup(self.view.receiveAllNode.bindingGroup.groupId, hasWaitToReceiveTasks)
        self.view.receiveAllNode.keyHint.gameObject:SetActive(hasWaitToReceiveTasks)
    end
end

SimulationTrainingTaskCtrl._UpdateTaskCells = HL.Method(HL.Any, HL.Number) << function(self, cell, luaIndex)

    local activityData = GameInstance.player.activitySystem:GetActivity(self.m_activityId)
    local taskId = self.m_taskStatusInfo[luaIndex].id
    local taskStatusData = activityData:GetTaskData(taskId)
    local taskConfigInfo = self.m_taskConfigInfo[taskId]


    cell.taskNameTxt.text = taskConfigInfo.desc
    cell.gameObject.name = "ArcheryTask_" .. taskId
    self.m_taskCells[luaIndex] = {cell = cell, id = taskId}


    local state = "Underway"
    local status = GEnums.ActivityConditionalTaskState.__CastFrom(taskStatusData.Status)

    if status == GEnums.ActivityConditionalTaskState.Locked then
        state = "Lock"

    elseif status == GEnums.ActivityConditionalTaskState.Completed then
        state = "Receive"
        cell.completeBtn.onClick:RemoveAllListeners()
        cell.completeBtn.onClick:AddListener(function()
            GameInstance.player.activitySystem:SendReceiveTaskRewardConditionMultiStage(self.m_activityId, { taskId })
        end)

    elseif status == GEnums.ActivityConditionalTaskState.Rewarded then
        state = "Finish"
    end
    cell.stateController:SetState(state)


    local rewardId = taskConfigInfo.rewardId


    local rewardBundles = UIUtils.getRewardItems(rewardId)


    for innerIndex = 1, 3 do
        local innerCell = nil
        if innerIndex == 1 then
            innerCell = cell.rewardItem1
        end
        if innerIndex == 2 then
            innerCell = cell.rewardItem2
        end
        if innerIndex == 3 then
            innerCell = cell.rewardItem3
        end

        if innerIndex > #rewardBundles then
            innerCell.stateController:SetState("Null")
            return
        end

        innerCell.stateController:SetState("Normal")
        local itemCell = innerCell.itemCell
        itemCell.view.simpleStateController:SetState("Normal")
        itemCell.view.rewardedCover.gameObject:SetActive(status == GEnums.ActivityConditionalTaskState.Rewarded)
        local reward = {
            id = rewardBundles[innerIndex].id,
            count = rewardBundles[innerIndex].count,
            forceHidePotentialStar = true,
        }
        itemCell:InitItem(reward, function()
            itemCell:ShowTips()
        end)
        itemCell:SetExtraInfo({
            tipsPosTransform = itemCell.view.content,
            isSideTips = true,
        })
    end


    local conditionIdList = taskConfigInfo.completeConditionId
    local isInProgress = status == GEnums.ActivityConditionalTaskState.Unlocked
    local isLocked = status == GEnums.ActivityConditionalTaskState.Locked
    local progress = 0
    local target = 0
    for i = 1, conditionIdList.Count do
        local conditionId = conditionIdList[CSIndex(i)]
        if isInProgress and taskStatusData.Conditions then
            local _, val = taskStatusData.Conditions.Values:TryGetValue(conditionId)
            progress = progress + val
        end
        target = target + Tables.activityConditionalMultiStageTaskCompleteConditionTable[conditionId].progressToCompare
    end
    if not isLocked and not isInProgress then
        progress = target
    end
    cell.taskBarPercentTxt.text = string.format("%d/%d", progress, target)
    cell.taskBar.fillAmount = target ~= 0 and progress / target or 1


    local redDotArgs = {
        activityId = self.m_activityId,
        taskId = taskId,
    }
    cell.redDot:InitRedDot("ActivitySimulationTrainingSingleTask", redDotArgs)
    self.m_readTasks[taskId] = true


    if DeviceInfo.usingController then

        cell.rewardsKeyHint.gameObject:SetActive(false)
        cell.naviDecorator.onIsNaviTargetChanged = function(isTarget)
            cell.rewardsKeyHint.gameObject:SetActive(isTarget)
        end
    end
end

SimulationTrainingTaskCtrl._OnTaskProgress = HL.Method(HL.Any) << function(self, arg)
    local activityId = unpack(arg)
    if activityId ~= self.m_activityId then
        return
    end

    self:_UpdateData()
    self:_RefreshAllUIs(false)
end

SimulationTrainingTaskCtrl.OnClose = HL.Override() << function(self)
    self:_UpdateReadInfo()
end

SimulationTrainingTaskCtrl._UpdateReadInfo = HL.Method() << function(self)
    for id, _ in pairs(self.m_readTasks) do
        ActivityUtils.setSimulationTrainingTaskRead(self.m_activityId, id)
    end
end

SimulationTrainingTaskCtrl._OnLimitedActivityEnd = HL.Method(HL.Any) << function(self, arg)

    if self.m_enterFromActivityCenter then
        return
    end

    local activityId = unpack(arg)
    if activityId ~= self.m_activityId then
        return
    end

    local activity = GameInstance.player.activitySystem:GetActivity(activityId)

    if not activity then
        Notify(MessageConst.SHOW_POP_UP, {
            content = Language.LUA_SIMULATION_TRAINING_LIMITED_ACTIVITY_END_POP_UP,
            hideCancel = true,
            onConfirm = function()
                PhaseManager:PopPhase(PHASE_ID)
            end
        })
    end
end

HL.Commit(SimulationTrainingTaskCtrl)
