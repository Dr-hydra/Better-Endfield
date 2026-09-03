
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ActivityWeeklyTask

ActivityWeeklyTaskCtrl = HL.Class('ActivityWeeklyTaskCtrl', uiCtrl.UICtrl)

ActivityWeeklyTaskCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_CONDITIONAL_MULTI_STAGE_UPDATE] = 'OnUpdate',
    [MessageConst.ON_ACTIVITY_UPDATED] = 'OnUpdate',
    [MessageConst.ON_WEEKLY_TASK_CHANGE] = 'OnUpdate',
}

ActivityWeeklyTaskCtrl.m_activityId = HL.Field(HL.String) << ''

ActivityWeeklyTaskCtrl.m_activity = HL.Field(HL.Any)

ActivityWeeklyTaskCtrl.m_tasks = HL.Field(HL.Table)

ActivityWeeklyTaskCtrl.m_mileStones = HL.Field(HL.Table)

ActivityWeeklyTaskCtrl.m_taskCells = HL.Field(HL.Any)

ActivityWeeklyTaskCtrl.m_mileStoneCells = HL.Field(HL.Any)

ActivityWeeklyTaskCtrl.m_maxScore = HL.Field(HL.Number) << 0

ActivityWeeklyTaskCtrl.m_viewBindingId = HL.Field(HL.Number) << 0

ActivityWeeklyTaskCtrl.m_score = HL.Field(HL.Number) << 0

local TaskColorTable = {
    [5] = "High",
    [2] = "Middle",
    [1] = "Low",
}

ActivityWeeklyTaskCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    self.m_score = -1
    self.m_activityId = args.activityId
    self.m_activity = GameInstance.player.activitySystem:GetActivity(self.m_activityId)
    self.m_taskCells = UIUtils.genCellCache(self.view.taskCell)
    self.m_mileStoneCells = UIUtils.genCellCache(self.view.mileStoneCell)
    self.view.activityCommonInfo:InitActivityCommonInfo(args)
    self.view.activityCommonInfo.view.infoNode.countDownWidget:InitCountDownText(Utils.getNextWeeklyServerRefreshTime())
    self.m_viewBindingId = -1
    self:_Refresh()
end

ActivityWeeklyTaskCtrl._SetNaviTarget = HL.Method(HL.Number) << function(self, index)
    if index == 0 or not DeviceInfo.usingController  then
        return
    end
    local cell = self.m_taskCells:Get(index)
    if cell then
        self:SetNaviTarget(cell.naviDecorator)
    end
end

ActivityWeeklyTaskCtrl._Refresh = HL.Method() << function(self)
    local activityUnlocked = ActivityUtils.isActivityUnlocked(self.m_activityId)
    self.view.unlockedNode.gameObject:SetActive(activityUnlocked)
    self.view.activityCommonInfo.view.gotoNode.gameObject:SetActive(not activityUnlocked)
    if not activityUnlocked then
        return
    end
    self:_GetTaskInfo()
    self:_GetMileStoneInfo()
    self:_RefreshTaskView()
    self:_RefreshMileStoneView()
    if DeviceInfo.usingController and self.m_viewBindingId < 0 then
        self.m_viewBindingId = self:BindInputPlayerAction("common_view_item", function()
            self:_SetNaviTarget(1)
        end)

        self.view.rightNaviGroup.onIsTopLayerChanged:AddListener(function(active)
            InputManagerInst:ToggleBinding(self.m_viewBindingId, not active)
            InputManagerInst:ToggleGroup(self.view.enterNode.groupId, not active)
        end)
    end
end

ActivityWeeklyTaskCtrl._GetTaskInfo = HL.Method() << function(self)
    local tasks = {}
    for id, task in pairs(self.m_activity.taskInfo) do
        local success, taskInfo = Tables.activityWeeklyTaskTable:TryGetValue(id)

        local progress = math.floor(task.Item2 * taskInfo.displayFactor + 1e-6)
        local progressToCompare = math.floor(taskInfo.progressToCompare * taskInfo.displayFactor + 1e-6)
        if success then
            local statusSortId = task.Item1 == 2 and -1 or task.Item1
            table.insert(tasks, {
                taskId = taskInfo.taskId,
                score = taskInfo.score,
                desc = string.format(taskInfo.desc, progressToCompare),
                jumpId = taskInfo.jumpId,
                progressToCompare = progressToCompare,
                sortId = taskInfo.sortId,
                status = task.Item1,
                statusSortId = statusSortId,
                progress = task.Item1 >= GEnums.ActivityConditionalStageState.Completed:GetHashCode() and progressToCompare or progress,
            })
        end
    end
    table.sort(tasks, Utils.genSortFunction({ "statusSortId", "sortId", "taskId" }, true))
    self.m_tasks = tasks
end

ActivityWeeklyTaskCtrl._GetMileStoneInfo = HL.Method() << function(self)
    self.m_maxScore = 0
    local mileStones = {}
    for mileStoneId, received in pairs(self.m_activity.mileStoneInfo) do
        local success, taskInfo = Tables.activityWeeklyTaskMileStoneTable[self.m_activityId].mileStones:TryGetValue(mileStoneId)
        if success then
            table.insert(mileStones, {
                id = mileStoneId,
                received = received,
                completed = self.m_activity.score >= taskInfo.score,
                score = taskInfo.score,
                rewardId = taskInfo.rewardId,
            })
            self.m_maxScore = math.max(self.m_maxScore, taskInfo.score)
        end
    end
    table.sort(mileStones, Utils.genSortFunction({ "score" }, true))
    local playVoice = false


    if self.m_mileStones then
        for i, mileStone in ipairs(self.m_mileStones) do
            if not mileStone.completed and mileStones[i].completed then
                playVoice = true
                mileStones[i].justComplete = true
            end
        end
    end
    if playVoice then
        AudioManager.PostEvent("Au_UI_Event_WeekMissionReward")
    end
    self.m_mileStones = mileStones


    if self.m_activity.score > self.m_score and self.m_score >= 0 then
        self.view.weeklyStageNode:PlayInAnimation()
        AudioManager.PostEvent("Au_UI_Event_WeekMissionLevelUp")
    end
    self.m_score = self.m_activity.score
end

ActivityWeeklyTaskCtrl._RefreshTaskView = HL.Method() << function(self)
    self.m_taskCells:GraduallyRefresh(#self.m_tasks, self.view.config.TASK_CELL_GRADUALLY_SHOW_TIME, function(cell, index)

        local task = self.m_tasks[index]
        cell.gameObject.name = "TaskCell_" .. index
        cell.scoreTxt.text = task.score
        cell.progressTxt.text = task.progress .. "/" .. task.progressToCompare
        cell.scrollbar.size = lume.clamp(task.progress/task.progressToCompare, 0, 1)
        cell.descTxt.text = task.desc


        cell.stateController:SetState(task.status == GEnums.ActivityConditionalStageState.Completed:GetHashCode() and "Completed" or "Normal")
        cell.stateController:SetState(TaskColorTable[task.score] or "Low")
        cell.inProgressNode.gameObject:SetActive(task.status == GEnums.ActivityConditionalStageState.Unlocked:GetHashCode() and string.isEmpty(task.jumpId))
        cell.receivedNode.gameObject:SetActive(task.status == GEnums.ActivityConditionalStageState.Rewarded:GetHashCode())
        cell.progressNode:SetState(task.status == GEnums.ActivityConditionalStageState.Rewarded:GetHashCode() and "Gray" or "Yellow")


        if self.m_activity.score >= self.m_maxScore then
            cell.stateController:SetState("Max")
            return
        else
            cell.maxNode.gameObject:SetActive(false)
        end


        cell.btnGoto.onClick:RemoveAllListeners()
        if task.status == GEnums.ActivityConditionalStageState.Unlocked:GetHashCode() and not string.isEmpty(task.jumpId) then
            cell.btnGoto.gameObject:SetActive(true)
            cell.btnGoto.onClick:AddListener(function()
                Utils.jumpToSystem(task.jumpId)
            end)
        else
            cell.btnGoto.gameObject:SetActive(false)
        end


        cell.btnAvailable.onClick:RemoveAllListeners()
        cell.btnAvailable.onClick:AddListener(function()
            self:_ReceiveAllTaskReward()
        end)
    end)
end

ActivityWeeklyTaskCtrl.OnActivityCenterNaviFailed = HL.Method() << function(self)
    self:_SetNaviTarget(1)
end

ActivityWeeklyTaskCtrl._RefreshMileStoneView = HL.Method() << function(self)
    local canReceive = false
    local allReceived = true


    self.m_mileStoneCells:Refresh(#self.m_mileStones, function(cell, index)
        cell.gameObject.name = "MileStoneCell_" .. index
        local mileStone = self.m_mileStones[index]
        cell.receivedNode.gameObject:SetActive(mileStone.received)
        cell.stageNumTxt.text = mileStone.score


        local lastMileStone = index > 1 and self.m_mileStones[index - 1]
        local canReceiveBoth = lastMileStone and mileStone.completed and not mileStone.received and lastMileStone.completed and not lastMileStone.received
        cell.stateController:SetState(index == 1 and "NoLine" or (canReceiveBoth and "DarkLine" or "NormalLine"))


        local rewardBundles = UIUtils.getRewardItems(mileStone.rewardId)
        for i = 1,2 do
            local item = cell["item" .. i]
            item:InitItem(rewardBundles[i], function()
                if mileStone.completed and not mileStone.received and not DeviceInfo.usingController then
                    self:_ReceiveAllMileStoneReward()
                else
                    item:ShowTips()
                end
            end)
            item:SetExtraInfo({
                tipsPosTransform = item.transform,
                isSideTips = true,
            })
        end


        cell.stateController:SetState(mileStone.received and "Received" or (mileStone.completed and "Completed" or "InProgress"))
        if mileStone.completed and not mileStone.received then
            canReceive = true
        end
        if not mileStone.received then
            allReceived = false
        end

        if mileStone.received then
            cell.lightNodeAnim:ClearTween()
        end


        if mileStone.justComplete then
            cell.animeNode:PlayInAnimation()
            mileStone.justComplete = nil
        end
    end)


    self.view.lvTxt.text = self.m_activity.score
    self.view.scoreScrollbar.size = lume.clamp(self.m_activity.score/self.m_maxScore, 0, 1)
    self.view.levelNode:SetState(self.m_activity.score >= self.m_maxScore and "Max" or (self.m_activity.score == 0 and "Zero" or "Number"))
    self.view.mileStoneStateNode.stateController:SetState(allReceived and "Received" or (canReceive and "Completed" or "InProgress"))
    self.view.mileStoneStateNode.simpleStateController:SetState(allReceived and "Received" or (canReceive and "Completed" or "InProgress"))
    self.view.mileStoneStateNode.button.onClick:RemoveAllListeners()
    if canReceive then
        self.view.mileStoneStateNode.button.onClick:AddListener(function()
            self:_ReceiveAllMileStoneReward()
        end)
    end
    self.view.mileStoneStateNode.button.gameObject:SetActive(canReceive)
end

ActivityWeeklyTaskCtrl._ReceiveAllMileStoneReward = HL.Method() << function(self)
    local ids = {}
    for _, mileStone in ipairs(self.m_mileStones) do
        if mileStone.completed and not mileStone.received then
            table.insert(ids, mileStone.id)
        end
    end
    if #ids > 0 then
        self.m_activity:GainReward(ids)
    end
end

ActivityWeeklyTaskCtrl._ReceiveAllTaskReward = HL.Method() << function(self)
    local ids = {}
    for _, task in ipairs(self.m_tasks) do
        if task.status == GEnums.ActivityConditionalStageState.Completed:GetHashCode() then
            table.insert(ids, task.taskId)
        end
    end
    if #ids > 0 then
        self.m_activity:CompleteTask(ids)
    end
end

ActivityWeeklyTaskCtrl.OnUpdate = HL.Method(HL.Table) << function(self, args)
    local id = unpack(args)
    if id ~= self.m_activityId then
        return
    end
    self:_Refresh()
end

HL.Commit(ActivityWeeklyTaskCtrl)
