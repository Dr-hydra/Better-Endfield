
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.BattlePassTask

BattlePassTaskCtrl = HL.Class('BattlePassTaskCtrl', uiCtrl.UICtrl)






BattlePassTaskCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_BATTLE_PASS_TASK_UPDATE] = '_OnTaskUpdate',
    [MessageConst.ON_BATTLE_PASS_TASK_BASIC_INFO_UPDATE] = '_OnBasicInfoUpdate',
    [MessageConst.ON_DAILY_ACTIVATION_MODIFY] = '_OnDailyActivationModify',
}


BattlePassTaskCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:_InitViews(arg)
    self:_LoadData(true)
    self:_RecoverState(arg and arg.panelArgs)
    self:_RenderViews()
    self:_RecoverScrollPosition()
end

BattlePassTaskCtrl.m_labelInfos = HL.Field(HL.Any)

BattlePassTaskCtrl.m_subLabelInfos = HL.Field(HL.Any)

BattlePassTaskCtrl.m_showInGroup = HL.Field(HL.Boolean) << false

BattlePassTaskCtrl.m_taskGroups = HL.Field(HL.Any)

BattlePassTaskCtrl.m_taskViewModels = HL.Field(HL.Any)

BattlePassTaskCtrl.m_taskForecastTipId = HL.Field(HL.String) << ''

BattlePassTaskCtrl.m_taskForecastTipPriority = HL.Field(HL.Number) << -99999

BattlePassTaskCtrl.m_selectedLabelIndex = HL.Field(HL.Number) << -1

BattlePassTaskCtrl.m_selectedSubLabelIndex = HL.Field(HL.Number) << -1

BattlePassTaskCtrl.m_isLifeTime = HL.Field(HL.Boolean) << false

BattlePassTaskCtrl.m_isLifeTimeComplete = HL.Field(HL.Boolean) << false

BattlePassTaskCtrl.m_labelCacheFunc = HL.Field(HL.Function)

BattlePassTaskCtrl.m_subLabelCacheFunc = HL.Field(HL.Function)

BattlePassTaskCtrl.m_subLabelNaviGroupId = HL.Field(HL.Number) << 1

BattlePassTaskCtrl.m_taskCacheFunc = HL.Field(HL.Function)

BattlePassTaskCtrl.m_viewedTasks = HL.Field(HL.Any) << nil

BattlePassTaskCtrl.m_subLabelFocus = HL.Field(HL.Boolean) << false

BattlePassTaskCtrl.m_naviTaskTarget = HL.Field(HL.Any) << nil

BattlePassTaskCtrl.m_naviTaskId = HL.Field(HL.String) << ""

BattlePassTaskCtrl.OnShow = HL.Override() << function(self)
    self:_NaviResume()
end

BattlePassTaskCtrl.OnHide = HL.Override() << function(self)
    self:_ReadShowingTasks()
end

BattlePassTaskCtrl.OnClose = HL.Override() << function(self)
    self:_ReadShowingTasks()
end

BattlePassTaskCtrl.OnPhaseRefresh = HL.Override(HL.Any) << function(self, arg)

end

local TASK_VIEW_TYPE = {
    GROUP_TITLE = 1,
    TASK = 2,
    FORECAST = 3,
}

BattlePassTaskCtrl._InitViews = HL.Method(HL.Any) << function(self, arg)
    local naviGroupIds = {}
    table.insert(naviGroupIds, self.view.inputGroup.groupId)
    if arg ~= nil and arg.baseNaviGroupId ~= nil then
        table.insert(naviGroupIds, arg.baseNaviGroupId)
    end
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder(naviGroupIds)
    UIUtils.bindHyperlinkPopup(self, "bpTask", self.view.inputGroup.groupId)
    self.m_labelCacheFunc = UIUtils.genCachedCellFunction(self.view.labelScrollList)
    self.view.labelScrollList.onUpdateCell:RemoveAllListeners()
    self.view.labelScrollList.onUpdateCell:AddListener(function(obj, csIndex)
        self:_RenderLabel(self.m_labelCacheFunc(obj), LuaIndex(csIndex))
    end)
    self.m_subLabelCacheFunc = UIUtils.genCachedCellFunction(self.view.subLabelScrollList)
    self.view.subLabelScrollList.onUpdateCell:RemoveAllListeners()
    self.view.subLabelScrollList.onUpdateCell:AddListener(function(obj, csIndex)
        self:_RenderSubLabel(self.m_subLabelCacheFunc(obj), LuaIndex(csIndex))
    end)
    self.m_taskCacheFunc = UIUtils.genCachedCellFunction(self.view.taskScrollList)
    self.view.taskScrollList.onUpdateCell:RemoveAllListeners()
    self.view.taskScrollList.onUpdateCell:AddListener(function(obj, csIndex)
        self:_RenderTask(self.m_taskCacheFunc(obj), LuaIndex(csIndex))
    end)
    self.view.taskScrollList.getCellSize = function(csIndex)
        local luaIndex = LuaIndex(csIndex)
        local viewModel = self.m_taskViewModels[luaIndex]
        if viewModel == nil then
            return 0
        end
        if viewModel.viewType == TASK_VIEW_TYPE.GROUP_TITLE then
            return self.view.config.TASK_GROUP_TITLE_HEIGHT
        elseif viewModel.viewType == TASK_VIEW_TYPE.TASK then
            return self.view.config.TASK_CELL_HEIGHT
        elseif viewModel.viewType == TASK_VIEW_TYPE.FORECAST then
            return self.view.config.TASK_FORECAST_HEIGHT
        end
        return 0
    end
    self.view.goBtn.onClick:RemoveAllListeners()
    self.view.goBtn.onClick:AddListener(function()
        PhaseManager:GoToPhase(PhaseId.AdventureBook, {
            panelId = 'AdventureDaily',
        })
    end)
    self.view.receiveBtn.onClick:RemoveAllListeners()
    self.view.receiveBtn.onClick:AddListener(function()
        self:_TakeAllTaskReward()
    end)
    self.view.redDotScrollRect.getRedDotStateAt = function(csIndex)
        local taskViewModel = self.m_taskViewModels[LuaIndex(csIndex)]
        if taskViewModel == nil or taskViewModel.viewType ~= TASK_VIEW_TYPE.TASK then
            return 0
        end
        local taskId = taskViewModel.taskInfo.taskId
        local hasRedDot = false
        local redDotType = 0
        if BattlePassUtils.CheckTaskUnread(taskId) then
            hasRedDot = true
            redDotType = UIConst.RED_DOT_TYPE.New
        end
        if BattlePassUtils.CheckTaskCompleted(taskId) then
            hasRedDot = true
            redDotType = (redDotType == UIConst.RED_DOT_TYPE.New) and UIConst.RED_DOT_TYPE.New or UIConst.RED_DOT_TYPE.Normal
        end
        if not hasRedDot then
            return 0
        end
        return redDotType
    end
    self:_InitSubLabelNavi()
end

BattlePassTaskCtrl._InitSubLabelNavi = HL.Method() << function(self)
    self.m_subLabelNaviGroupId = InputManagerInst:CreateGroup(self.view.inputGroup.groupId)
    UIUtils.bindInputPlayerAction("bp_task_sublabel_next", function()
        local subLabelCount = #self.m_subLabelInfos
        if subLabelCount <= 1 then
            return
        end
        local targetIndex = math.min(self.m_selectedSubLabelIndex + 1, subLabelCount)
        if targetIndex == self.m_selectedSubLabelIndex then
            return
        end
        self:_NaviToTargetIndex(targetIndex)
    end, self.m_subLabelNaviGroupId)
    UIUtils.bindInputPlayerAction("bp_task_sublabel_prev", function()
        local subLabelCount = #self.m_subLabelInfos
        if subLabelCount <= 1 then
            return
        end
        local targetIndex = math.max(self.m_selectedSubLabelIndex - 1, 1)
        if targetIndex == self.m_selectedSubLabelIndex then
            return
        end
        self:_NaviToTargetIndex(targetIndex)
    end, self.m_subLabelNaviGroupId)
    InputManagerInst:ToggleGroup(self.m_subLabelNaviGroupId, true)
    self.view.taskScrollListSelectableNaviGroup.onIsFocusedChange:AddListener(function(isFocused)
        self.m_subLabelFocus = isFocused
    end)
    self.view.taskScrollListSelectableNaviGroup.onSetLayerSelectedTarget:AddListener(function(target)
        self.m_subLabelFocus = true
        self.m_naviTaskTarget = target
        self.m_naviTaskId = self:_FindTaskIdByNaviTarget(target) or ""
    end)
    self.view.taskScrollListSelectableNaviGroup.getDefaultSelectableFunc = function()
        self.view.taskScrollList:ScrollToIndex(0, true)
        for index, viewModel in ipairs(self.m_taskViewModels) do
            if viewModel.viewType == TASK_VIEW_TYPE.TASK then
                local firstCell = self.m_taskCacheFunc(index)
                if firstCell ~= nil then
                    return firstCell.naviDecorator
                end
            end
        end
    end
    self.view.labelScrollListSelectableNaviGroup.onSetLayerSelectedTarget:AddListener(function(target)
        self.m_subLabelFocus = false
        self.m_naviTaskTarget = nil
        self.m_naviTaskId = ""
    end)
    self:BindInputPlayerAction("common_horizontal_focus_right", function()
        self.view.taskScrollListSelectableNaviGroup:ManuallyFocus()
    end)
    self:BindInputPlayerAction("common_horizontal_stop_focus_left", function()
        self.view.taskScrollListSelectableNaviGroup:ManuallyStopFocus()
        if DeviceInfo.usingController then
            local curTarget = InputManagerInst.controllerNaviManager.curTarget
            if curTarget == nil or curTarget.naviGroup ~= self.view.labelScrollListSelectableNaviGroup then
                local selectedLabelCell = self.m_labelCacheFunc(self.m_selectedLabelIndex)
                if selectedLabelCell ~= nil and selectedLabelCell.button ~= nil and selectedLabelCell.button.gameObject.activeInHierarchy then
                    self:SetNaviTarget(selectedLabelCell.button)
                end
            end
        end
    end, self.view.taskScrollListInputBindingGroupMonoTarget.groupId)
end

BattlePassTaskCtrl._NaviToTargetIndex = HL.Method(HL.Number) << function(self, targetIndex)
    self:_OnSelectSubTab(targetIndex)

    AudioAdapter.PostEvent("Au_UI_Button_Common")
    if not self.m_subLabelFocus then
        return
    end
    local firstTaskIndex = -1
    for index, viewModel in ipairs(self.m_taskViewModels) do
        if viewModel.viewType == TASK_VIEW_TYPE.TASK then
            firstTaskIndex = index
            break
        end
    end
    if firstTaskIndex > 0 then
        self.view.taskScrollList:UpdateShowingCells(function(csIndex, obj)
            local luaIndex = LuaIndex(csIndex)
            local cell = self.m_taskCacheFunc(obj)
            self:_RenderTask(cell, luaIndex)
            if luaIndex == firstTaskIndex then
                self:SetNaviTarget(cell.naviDecorator)
            end
        end)
    end
end



BattlePassTaskCtrl._EnsureLabelNaviLayer = HL.Method() << function(self)
    if not DeviceInfo.usingController then
        return
    end
    local labelNaviGroup = self.view.labelScrollListSelectableNaviGroup
    if labelNaviGroup == nil or InputManagerInst.controllerNaviManager:IsLayerInStack(labelNaviGroup) then
        return
    end
    local taskNaviGroup = self.view.taskScrollListSelectableNaviGroup
    if taskNaviGroup ~= nil and InputManagerInst.controllerNaviManager:IsLayerInStack(taskNaviGroup) then


        InputManagerInst.controllerNaviManager:TryRemoveLayer(taskNaviGroup)
    end
    if self.m_selectedLabelIndex <= 0 then
        return
    end
    local selectedLabelCell = self.m_labelCacheFunc(self.m_selectedLabelIndex)
    if selectedLabelCell ~= nil
        and selectedLabelCell.button ~= nil
        and selectedLabelCell.button.gameObject.activeInHierarchy then
        self:SetNaviTarget(selectedLabelCell.button)
    end
end

BattlePassTaskCtrl._NaviResume = HL.Method() << function(self)
    local lastNaviTarget = self.m_naviTaskTarget
    local lastNaviTaskId = self.m_naviTaskId
    self.view.labelScrollList:UpdateShowingCells(function(csIndex, obj)
        self:_RenderLabel(self.m_labelCacheFunc(obj), LuaIndex(csIndex), true)
    end)

    if not string.isEmpty(lastNaviTaskId) and self.m_taskViewModels ~= nil then
        local targetIndex = nil
        for index, viewModel in ipairs(self.m_taskViewModels) do
            if viewModel.viewType == TASK_VIEW_TYPE.TASK
                and viewModel.taskInfo ~= nil
                and viewModel.taskInfo.taskId == lastNaviTaskId then
                targetIndex = index
                break
            end
        end
        if targetIndex ~= nil then
            self.view.taskScrollList:ScrollToIndex(CSIndex(targetIndex), true)
            local cell = self.m_taskCacheFunc(targetIndex)
            if cell ~= nil and cell.naviDecorator ~= nil then
                self:_EnsureLabelNaviLayer()
                self:SetNaviTarget(cell.naviDecorator)
                self.m_naviTaskTarget = cell.naviDecorator
                return
            end
        end
    end

    if lastNaviTarget ~= nil then
        self:_EnsureLabelNaviLayer()
        self:SetNaviTarget(lastNaviTarget)
        return
    end



    if DeviceInfo.usingController and self.m_selectedLabelIndex > 0 then
        local selectedLabelCell = self.m_labelCacheFunc(self.m_selectedLabelIndex)
        if selectedLabelCell ~= nil
            and selectedLabelCell.button ~= nil
            and selectedLabelCell.button.gameObject.activeInHierarchy then
            self:SetNaviTarget(selectedLabelCell.button)
        end
    end
end




BattlePassTaskCtrl._FindTaskIdByNaviTarget = HL.Method(HL.Any).Return(HL.String) << function(self, target)
    if target == nil or self.m_taskViewModels == nil then
        return ""
    end
    local foundTaskId = ""
    self.view.taskScrollList:UpdateShowingCells(function(csIndex, obj)
        if not string.isEmpty(foundTaskId) then
            return
        end
        local cell = self.m_taskCacheFunc(obj)
        if cell == nil or cell.naviDecorator ~= target then
            return
        end
        local viewModel = self.m_taskViewModels[LuaIndex(csIndex)]
        if viewModel ~= nil
            and viewModel.viewType == TASK_VIEW_TYPE.TASK
            and viewModel.taskInfo ~= nil then
            foundTaskId = viewModel.taskInfo.taskId
        end
    end)
    return foundTaskId
end



BattlePassTaskCtrl._LoadData = HL.Method(HL.Opt(HL.Boolean)) << function(self, needReset)
    self:_LoadLabelData()
    if needReset == true then
        self:_ResetSelectLabel()
    end
    self:_LoadSubLabelData()
    self:_LoadGroupData()
end

BattlePassTaskCtrl._LoadLabelData = HL.Method() << function(self)
    self.m_labelInfos = {}
    local labelTable = Tables.battlePassTaskLabelTable
    local bpSystem = GameInstance.player.battlePassSystem
    for labelId, playerLabel in pairs(bpSystem.taskData.taskLabels) do
        local isSub = Tables.battlePassTaskSubLabelMapTable:ContainsKey(labelId)
        if not isSub then
            local hasLabel, labelData = labelTable:TryGetValue(labelId)
            if hasLabel then
                local labelInfo = self:_LoadLabelInfo(labelData, playerLabel)
                if labelInfo ~= nil then
                    local hasSub, subLabelMapInfo = Tables.battlePassTaskLabelMapTable:TryGetValue(labelId)
                    if hasSub then
                        local subLabelIds = {}
                        for _, subLabel in pairs(subLabelMapInfo.subLabels) do
                            table.insert(subLabelIds, subLabel.taskLabelID)
                        end
                        labelInfo.subLabelIds = subLabelIds


                        local subLabelInfos = {}
                        local subEnableGroupCount = 0
                        for _, subLabelId in ipairs(subLabelIds) do
                            local hasData, subLabelData = labelTable:TryGetValue(subLabelId)
                            local hasSubLabel, playerSubLabel = bpSystem.taskData.taskLabels:TryGetValue(subLabelId)
                            if hasData and hasSubLabel then
                                local subLabelInfo = self:_LoadLabelInfo(subLabelData, playerSubLabel)
                                if subLabelInfo ~= nil and subLabelInfo.enableGroupCount > 0 then
                                    subEnableGroupCount = subEnableGroupCount + subLabelInfo.enableGroupCount
                                    table.insert(subLabelInfos, subLabelInfo)
                                end
                            end
                        end
                        table.sort(subLabelInfos, Utils.genSortFunction({"statusSortId", "sortId"}, true))
                        local visibleTime = -1
                        local overrideParentLabelIndex = -1
                        if labelInfo.isTimeVisible and labelInfo.isConditionVisible then
                            for index, subLabelInfo in ipairs(subLabelInfos) do
                                if subLabelInfo.isTimeVisible and subLabelInfo.isConditionVisible and subLabelInfo.visibleTime > visibleTime then
                                    visibleTime = subLabelInfo.visibleTime
                                    overrideParentLabelIndex = index
                                end
                            end
                        end
                        if overrideParentLabelIndex > 0 then
                            labelInfo.subName = subLabelInfos[overrideParentLabelIndex].subName
                        end
                        labelInfo.subLabelInfos = subLabelInfos
                        labelInfo.enableGroupCount = subEnableGroupCount
                    end
                    labelInfo.hasSub = hasSub
                    if labelInfo.enableGroupCount > 0 then
                        table.insert(self.m_labelInfos, labelInfo)
                    end
                end
            end
        end
    end
    table.sort(self.m_labelInfos, Utils.genSortFunction({"statusSortId", "sortId"}, true))
end

BattlePassTaskCtrl._LoadLabelInfo = HL.Method(HL.Any, HL.Any).Return(HL.Table) << function(self, labelData, playerLabel)
    local labelId = labelData.labelId
    local isLabelValid, isTimeVisible, isConditionVisible = BattlePassUtils.CheckLabelVisible(labelId)
    if not isLabelValid then
        return nil
    end
    if labelData.forecastType == GEnums.BPLabelForecastType.None and (not isTimeVisible or not isConditionVisible) then
        return nil
    end
    local enableGroupCount = 0
    for _, groupId in pairs(playerLabel.groupIds) do
        if BattlePassUtils.CheckGroupEnable(groupId) then
            enableGroupCount = enableGroupCount + 1
        end
    end
    local subName = labelData.subName
    local forecastDesc = ''
    local curServerTime = DateTimeUtils.GetCurrentTimestampBySeconds()
    if not isTimeVisible and (labelData.forecastType == GEnums.BPLabelForecastType.Time or
            (isConditionVisible and labelData.forecastType == GEnums.BPLabelForecastType.Condition)) then
        local leftTime = UIUtils.getShortLeftTime(playerLabel.visibleTime - curServerTime)
        local leftLongTime = UIUtils.getLeftTime(playerLabel.visibleTime - curServerTime)
        subName = string.format(Language.LUA_BATTLEPASS_TASK_LABEL_TIME_LOCK_FORMAT, leftTime)
        forecastDesc = string.format(Language.LUA_BATTLEPASS_TASK_TIME_LOCK_FORMAT, leftLongTime)
    elseif not isConditionVisible and (labelData.forecastType == GEnums.BPLabelForecastType.Condition or
            (isTimeVisible and labelData.forecastType == GEnums.BPLabelForecastType.Time)) then
        subName = Language.LUA_BATTLEPASS_TASK_LABEL_CONDITION_LOCK_TIP
        forecastDesc = labelData.conditionHint
    elseif isTimeVisible and isConditionVisible and string.isEmpty(subName) then
        local playerSeason = GameInstance.player.battlePassSystem.seasonData
        local leftLongTime = UIUtils.getLeftTime(playerSeason.closeTime - curServerTime)
        subName = string.format(Language.LUA_BATTLEPASS_TASK_LABEL_TIME_LEFT_SUB_NAME_FORMAT, leftLongTime)
    end
    local statusSortId = 0
    if not isTimeVisible or not isConditionVisible then
        statusSortId = 1
    end

    local labelInfo = {
        labelId = labelId,
        name = labelData.name,
        subName = subName,
        sortId = labelData.sortId,
        statusSortId = statusSortId,
        forecastType = labelData.forecastType,
        forecastDesc = forecastDesc,
        isTimeVisible = isTimeVisible,
        isConditionVisible = isConditionVisible,
        visibleTime = playerLabel.visibleTime,
        groupIds = playerLabel.groupIds,
        isLifeTime = labelData.isLifeTime,
        enableGroupCount = enableGroupCount,
    }
    return labelInfo
end

BattlePassTaskCtrl._ResetSelectLabel = HL.Method() << function(self)
    if self.m_labelInfos ~= nil and #self.m_labelInfos > 0 then
        self.m_selectedLabelIndex = 1
    else
        self.m_selectedLabelIndex = -1
    end
    self.m_selectedSubLabelIndex = 1
end

BattlePassTaskCtrl._RecoverState = HL.Method(HL.Opt(HL.Any)) << function(self, recoverState)
    if recoverState == nil then
        return
    end
    local labelCount = self.m_labelInfos ~= nil and #self.m_labelInfos or 0
    if labelCount <= 0 then
        self.m_selectedLabelIndex = -1
        self.m_selectedSubLabelIndex = -1
        return
    end
    if recoverState.selectedLabelIndex ~= nil then
        self.m_selectedLabelIndex = math.max(1, math.min(recoverState.selectedLabelIndex, labelCount))
    end
    self:_LoadSubLabelData()
    local subLabelCount = self.m_subLabelInfos ~= nil and #self.m_subLabelInfos or 0
    if subLabelCount <= 0 then
        self.m_selectedSubLabelIndex = 1
    elseif recoverState.selectedSubLabelIndex ~= nil then
        self.m_selectedSubLabelIndex = math.max(1, math.min(recoverState.selectedSubLabelIndex, subLabelCount))
    else
        self.m_selectedSubLabelIndex = math.max(1, math.min(self.m_selectedSubLabelIndex, subLabelCount))
    end
    self:_LoadGroupData()
end

BattlePassTaskCtrl._RecoverScrollPosition = HL.Method() << function(self)
    if self.m_selectedLabelIndex > 0 then
        self.view.labelScrollList:ScrollToIndex(CSIndex(self.m_selectedLabelIndex), true)
    end
    if self.m_subLabelInfos ~= nil and #self.m_subLabelInfos > 0 and self.m_selectedSubLabelIndex > 0 then
        self.view.subLabelScrollList:ScrollToIndex(CSIndex(self.m_selectedSubLabelIndex), true)
    end
end

BattlePassTaskCtrl._LoadSubLabelData = HL.Method() << function(self)
    self.m_subLabelInfos = {}
    if self.m_selectedLabelIndex <= 0 then
        return
    end
    local parentLabelInfo = self.m_labelInfos[self.m_selectedLabelIndex]
    if parentLabelInfo == nil or parentLabelInfo.subLabelInfos == nil then
        return
    end
    self.m_subLabelInfos = parentLabelInfo.subLabelInfos
end

BattlePassTaskCtrl._LoadGroupData = HL.Method() << function(self)
    self.m_taskGroups = {}
    self.m_taskViewModels = {}
    self.m_taskForecastTipId = ''
    self.m_taskForecastTipPriority = -99999
    if self.m_selectedLabelIndex <= 0 then
        return
    end
    local labelInfo = self.m_labelInfos[self.m_selectedLabelIndex]
    if self.m_subLabelInfos ~= nil and #self.m_subLabelInfos > 0 then
        labelInfo = self.m_subLabelInfos[self.m_selectedSubLabelIndex]
    end

    if labelInfo == nil or (not labelInfo.isTimeVisible or not labelInfo.isConditionVisible) then
        self:_GenerateTaskViewModels()
        return
    end
    self.m_taskGroups, self.m_taskForecastTipId, self.m_taskForecastTipPriority = self:_LoadGroupDataImpl(labelInfo)
    self.m_isLifeTime = false
    self.m_isLifeTimeComplete = false
    local selectLabelInfo = self.m_labelInfos[self.m_selectedLabelIndex]
    if selectLabelInfo ~= nil then
        self.m_isLifeTime = selectLabelInfo.isLifeTime
        if self.m_isLifeTime then
            self.m_isLifeTimeComplete = true
        end
        for _, groupInfo in pairs(self.m_taskGroups) do
            self.m_isLifeTimeComplete = self.m_isLifeTimeComplete and groupInfo.isAllComplete
        end
    end
    self:_GenerateTaskViewModels()
end

BattlePassTaskCtrl._LoadGroupDataImpl = HL.Method(HL.Any).Return(HL.Table, HL.String, HL.Number) << function(self, labelInfo)
    local taskGroups = {}
    local taskForecastTipId = ''
    local taskForecastTipPriority = -99999
    local groupTable = Tables.battlePassTaskGroupTable
    local bpSystem = GameInstance.player.battlePassSystem
    for _, groupId in pairs(labelInfo.groupIds) do
        local hasGroup, groupData = groupTable:TryGetValue(groupId)
        local hasPlayer, playerGroup = bpSystem.taskData.taskGroups:TryGetValue(groupId)
        if hasGroup and hasPlayer then
            local groupInfo, forecastId, forecastPriority = self:_LoadGroupInfo(groupData, playerGroup)
            if groupInfo ~= nil then
                table.insert(taskGroups, groupInfo)
            end
            if not string.isEmpty(forecastId) then
                if forecastPriority > taskForecastTipPriority then
                    taskForecastTipId = forecastId
                    taskForecastTipPriority = forecastPriority
                end
            end
        end
    end
    table.sort(taskGroups, Utils.genSortFunction({"defaultSortId", "sortId"}, true))
    return taskGroups, taskForecastTipId, taskForecastTipPriority
end

BattlePassTaskCtrl._LoadGroupInfo = HL.Method(HL.Any, HL.Any).Return(HL.Table, HL.String, HL.Number) << function(self, groupData, playerGroup)
    local groupId = groupData.groupId
    local isGroupValid, isTimeVisible, isConditionVisible = BattlePassUtils.CheckGroupVisible(groupId)
    local taskForecastTipId = ''
    local taskForecastTipPriority = -99999
    if not isGroupValid then
        return nil, taskForecastTipId, taskForecastTipPriority
    end
    local groupTask = {}
    local taskTable = Tables.battlePassTaskTable
    local bpSystem = GameInstance.player.battlePassSystem
    local isAllComplete = true
    for _, taskId in pairs(playerGroup.taskIds) do
        local hasTask, taskData = taskTable:TryGetValue(taskId)
        local hasPlayer, playerTask = bpSystem.taskData.taskInfos:TryGetValue(taskId)
        if hasTask and hasPlayer then
            local taskInfo, forecastId = self:_LoadTaskInfo(taskData, playerTask)
            if taskInfo ~= nil then
                table.insert(groupTask, taskInfo)
                isAllComplete = isAllComplete and (taskInfo.taskState == CS.Proto.BP_TASK_STATE.TakeReward)
            elseif not string.isEmpty(forecastId) then
                local hasForecast, forecastTipData = Tables.battlePassForecastTipTable:TryGetValue(forecastId)
                if hasForecast and forecastTipData.priority > taskForecastTipPriority then
                    taskForecastTipId = forecastId
                    taskForecastTipPriority = forecastTipData.priority
                end
            end
        end
    end
    if not isTimeVisible or not isConditionVisible then
        return nil, taskForecastTipId, taskForecastTipPriority
    end
    table.sort(groupTask, Utils.genSortFunction({"statusSortId", "sortId"}, true))
    local groupInfo = {
        groupId = groupId,
        name = groupData.name,
        sortId = groupData.sortId,
        defaultSortId = groupData.isDefault and 0 or 1,
        showAsDefault = groupData.isDefault,
        taskInfos = groupTask,
        isAllComplete = isAllComplete,
        disableTime = playerGroup.disableTime,
    }
    return groupInfo, taskForecastTipId, taskForecastTipPriority
end

BattlePassTaskCtrl._LoadTaskInfo = HL.Method(HL.Any, HL.Any).Return(HL.Table, HL.String) << function(self, taskData, playerTask)
    local taskId = taskData.taskId
    local isValid, isTimeVisible, isConditionVisible = BattlePassUtils.CheckTaskVisible(taskId)
    if not isValid then
        return nil, ''
    end
    if not isTimeVisible or not isConditionVisible then
        return nil, taskData.forecastTipId
    end
    local statusSortId = 99
    if playerTask.taskState == CS.Proto.BP_TASK_STATE.HasCompleted then
        statusSortId = 1
    elseif playerTask.taskState == CS.Proto.BP_TASK_STATE.InProcessing or playerTask.taskState == CS.Proto.BP_TASK_STATE.NotAccept then
        statusSortId = 2
    elseif playerTask.taskState == CS.Proto.BP_TASK_STATE.TakeReward then
        statusSortId = 3
    end
    local taskVal, taskTarget = self:_CheckTaskCondition(taskData, playerTask)
    local formatConditionId = taskData.conditionIds.Count >= 1 and taskData.conditionIds[0] or nil
    local formatVal = 0
    if formatConditionId ~= nil then
        local hasCondition, conditionData = Tables.battlePassConditionTable:TryGetValue(formatConditionId)
        if hasCondition then
            formatVal = conditionData.progressToCompare
        end
    end
    local taskInfo = {
        taskId = taskId,
        name = taskData.formatType and string.format(taskData.name, formatVal) or taskData.name,
        sortId = taskData.sortId,
        statusSortId = statusSortId,
        taskState = playerTask.taskState,
        jumpId = taskData.jumpId,
        addExp = taskData.addexp,
        taskVal = taskVal,
        taskTarget = taskTarget,
    }
    return taskInfo, ''
end

BattlePassTaskCtrl._GenerateTaskViewModels = HL.Method() << function(self)
    self.m_taskViewModels = {}
    for _, groupInfo in ipairs(self.m_taskGroups) do
        if not groupInfo.showAsDefault then
            table.insert(self.m_taskViewModels, {
                viewType = TASK_VIEW_TYPE.GROUP_TITLE,
                groupInfo = groupInfo,
            })
        end
        for _, taskInfo in ipairs(groupInfo.taskInfos) do
            table.insert(self.m_taskViewModels, {
                viewType = TASK_VIEW_TYPE.TASK,
                taskInfo = taskInfo,
            })
        end
    end
    if not string.isEmpty(self.m_taskForecastTipId) then
        local hasForecast, forecastData = Tables.battlePassForecastTipTable:TryGetValue(self.m_taskForecastTipId)
        if hasForecast then
            table.insert(self.m_taskViewModels, {
                viewType = TASK_VIEW_TYPE.FORECAST,
                forecastInfo = {
                    desc = forecastData.text1,
                    subDesc = forecastData.text2
                },
            })
        end
    end
end

BattlePassTaskCtrl._CheckTaskCondition = HL.Method(HL.Any, HL.Any).Return(HL.Number, HL.Number) << function(self, taskData, playerTask)
    local taskTarget = 0
    if taskData.opType == GEnums.BoolOperator.And then
        if taskData.conditionIds.Count > 1 then
            for _, conditionId in pairs(taskData.conditionIds) do
                local hasCondition, conditionData = Tables.battlePassConditionTable:TryGetValue(conditionId)
                if hasCondition then
                    taskTarget = taskTarget + 1
                end
            end
        elseif taskData.conditionIds.Count == 1 then
            local conditionId = taskData.conditionIds[0]
            local hasCondition, conditionData = Tables.battlePassConditionTable:TryGetValue(conditionId)
            if hasCondition then
                taskTarget = conditionData.progressToCompare
            end
        end
    elseif taskData.opType == GEnums.BoolOperator.Or then
        taskTarget = 1
    end

    local taskVal = 0

    if playerTask.taskState == CS.Proto.BP_TASK_STATE.InProcessing then
        if taskData.opType == GEnums.BoolOperator.And then
            taskVal = playerTask.completeConditions:GetAndConditionVal()
        elseif taskData.opType == GEnums.BoolOperator.Or then
            taskVal = playerTask.completeConditions:GetOrConditionVal()
        end
    elseif playerTask.taskState == CS.Proto.BP_TASK_STATE.HasCompleted or playerTask.taskState == CS.Proto.BP_TASK_STATE.TakeReward then
        taskVal = taskTarget
    end

    return taskVal, taskTarget
end



BattlePassTaskCtrl._RenderViews = HL.Method(HL.Opt(HL.Boolean, HL.Boolean, HL.Boolean)) << function(self, refreshLabel, refreshSubLabel, refreshTask)
    local hasSub = self.m_subLabelInfos ~= nil and #self.m_subLabelInfos > 0
    local showTop = hasSub or self.m_isLifeTime
    local canTakeTaskIds = self:_QueryAllCanTakeTaskIds()
    refreshLabel = refreshLabel == true
    refreshSubLabel = refreshSubLabel == true
    refreshTask = refreshTask == true
    if not refreshLabel then
        self:_UpdateLabels()
    else
        self:_RefreshLabels()
    end
    if not refreshSubLabel then
        self:_UpdateSubLabels()
    else
        self:_RefreshSubLabels()
    end
    if not refreshTask then
        self:_UpdateTasks()
    else
        self:_RefreshTasks()
    end
    local labelInfo = self.m_labelInfos[self.m_selectedLabelIndex]
    local hasLabel = labelInfo ~= nil
    local isLabelLocked = hasLabel and (not labelInfo.isTimeVisible or not labelInfo.isConditionVisible)
    local showSubLabel = hasSub and not isLabelLocked
    if hasLabel and not isLabelLocked and self.m_subLabelInfos ~= nil and #self.m_subLabelInfos > 0 then
        labelInfo = self.m_subLabelInfos[self.m_selectedSubLabelIndex]
    end
    local isForecast = hasLabel and (not labelInfo.isTimeVisible or not labelInfo.isConditionVisible)
    if isForecast then
        self.view.promptTxt.text = labelInfo.forecastDesc
    end
    InputManagerInst:ToggleGroup(self.m_subLabelNaviGroupId, hasSub and #self.m_subLabelInfos > 1)
    self.view.rightNode:SetState(not isForecast and "Exist" or (showSubLabel and "SubLabelEmpty" or "Empty"))
    self.view.topNode:SetState((not showTop) and "Empty" or (showSubLabel and "SubLabel" or (self.m_isLifeTimeComplete and "LifeTimeComplete" or "LifeTime")))
    self.view.receiveBtn.gameObject:SetActive(not isLabelLocked and not isForecast and #canTakeTaskIds > 0)
    self:_RenderDailyPart()


    if DeviceInfo.usingController and refreshTask and self.m_subLabelFocus then
        if PhaseManager:GetTopPhaseId() ~= PhaseId.BattlePass or not self:IsShow() then
            return
        end
        self.view.taskScrollList:ScrollToIndex(0, true)
        for index, viewModel in ipairs(self.m_taskViewModels) do
            if viewModel.viewType == TASK_VIEW_TYPE.TASK then
                local firstCell = self.m_taskCacheFunc(index)
                if firstCell ~= nil then
                    self:SetNaviTarget(firstCell.naviDecorator)
                    break
                end
            end
        end
    end
end

BattlePassTaskCtrl._RenderDailyPart = HL.Method() << function(self)
    local bpSystem = GameInstance.player.battlePassSystem
    local bpAbsentCount = bpSystem.seasonData.absentCount
    local hasAbsent = bpAbsentCount > 0
    local dailyPoint = GameInstance.player.adventure.adventureBookData.dailyRewardedActivation
    local dailyMax = 0
    local expBase = 0
    local expBaseGot = 0
    for _, rewardData in pairs(Tables.dailyActivationRewardTable) do
        dailyMax = math.max(dailyMax, rewardData.activation)
        local expReward = BattlePassUtils.GetAdventureDailyBpExpBaseCount(rewardData.rewardId)
        expBase = expBase + expReward
        if dailyPoint >= rewardData.activation then
            expBaseGot = expBaseGot + expReward
        end
    end
    local expBoostRate = (Tables.battlePassConst.absentFlagBpExpRate / 1000)
    local expBoost = expBoostRate * expBase
    local expBoostGot = expBoostRate * expBaseGot
    local expFinal = hasAbsent and expBoost or expBase
    local expGotFinal = hasAbsent and expBoostGot or expBaseGot
    self.view.goBtnStateController:SetState(dailyPoint >= dailyMax and "Full" or "Progress")
    self.view.dailyTxt.text = string.format(Language.LUA_BATTLEPASS_TASK_DAILY_FORMAT, math.tointeger(expGotFinal), math.tointeger(expFinal))
    self.view.goBtn.gameObject:SetActive(Utils.isSystemUnlocked(GEnums.UnlockSystemType.AdventureBook))
end

BattlePassTaskCtrl._UpdateLabels = HL.Method() << function(self)
    self.view.labelScrollList:UpdateCount(#self.m_labelInfos, true)
end

BattlePassTaskCtrl._RefreshLabels = HL.Method() << function(self)
    self.view.labelScrollList:UpdateShowingCells(function(csIndex, obj)
        self:_RenderLabel(self.m_labelCacheFunc(obj), LuaIndex(csIndex), true)
    end)
end

BattlePassTaskCtrl._UpdateSubLabels = HL.Method() << function(self)
    self.view.subLabelScrollList:UpdateCount(#self.m_subLabelInfos, true)
end

BattlePassTaskCtrl._RefreshSubLabels = HL.Method() << function(self)
    self.view.subLabelScrollList:UpdateShowingCells(function(csIndex, obj)
        local luaIndex = LuaIndex(csIndex)
        local cell = self.m_subLabelCacheFunc(obj)
        self:_RenderSubLabel(cell, luaIndex)
        if self.m_selectedSubLabelIndex == luaIndex then
            self.view.subLabelScrollRect:AutoScrollToRectTransform(cell.transform)
        end
    end)
end

BattlePassTaskCtrl._UpdateTasks = HL.Method() << function(self)
    self.view.taskScrollList:UpdateCount(#self.m_taskViewModels, true, true)
end

BattlePassTaskCtrl._RefreshTasks = HL.Method() << function(self)
    self.view.taskScrollList:UpdateCount(#self.m_taskViewModels, false, true)
end

BattlePassTaskCtrl._RenderLabel = HL.Method(HL.Any, HL.Number, HL.Opt(HL.Boolean)) << function(self, cell, luaIndex, isRefresh)
    local isSelected = self.m_selectedLabelIndex == luaIndex
    cell.stateController:SetState(isSelected and "Select" or "Another")
    local labelInfo = self.m_labelInfos[luaIndex]
    if labelInfo ~= nil then
        local isUnlock = labelInfo.isTimeVisible and labelInfo.isConditionVisible
        cell.titleText.text = labelInfo.name
        cell.describeText.text = labelInfo.subName
        cell.lockImage.gameObject:SetActive(not isUnlock)
        cell.redDot:InitRedDot("BattlePassTaskLabel", labelInfo.labelId)
    else
        logger.error("[battlepass] labelInfo is nil!!!")
    end
    cell.lineImage.gameObject:SetActive(luaIndex ~= #self.m_labelInfos)
    cell.button.onClick:RemoveAllListeners()
    cell.button.onClick:AddListener(function()
        if self.m_selectedLabelIndex == luaIndex then
            return
        end
        self.m_selectedLabelIndex = luaIndex
        self.m_selectedSubLabelIndex = 1
        self:_ReadShowingTasks()
        self:_LoadData()
        self:_RenderViews(true)
    end)
    if isSelected and DeviceInfo.usingController and not isRefresh == true then
        self:SetNaviTarget(cell.button)
    end
end

BattlePassTaskCtrl._RenderSubLabel = HL.Method(HL.Any, HL.Number) << function(self, cell, luaIndex)
    local subLabelInfo = self.m_subLabelInfos[luaIndex]
    if subLabelInfo ~= nil then
        cell.titleTxt.text = subLabelInfo.name
        cell.redDot:InitRedDot("BattlePassTaskLabel", subLabelInfo.labelId)
    end

    cell.stateController:SetState(luaIndex == self.m_selectedSubLabelIndex and "On" or "Off")
    cell.button.onClick:RemoveAllListeners()
    cell.button.onClick:AddListener(function()
        self:_OnSelectSubTab(luaIndex)
    end)
end

BattlePassTaskCtrl._RenderTask = HL.Method(HL.Any, HL.Number) << function(self, cell, luaIndex)
    local viewModel = self.m_taskViewModels[luaIndex]
    if viewModel == nil then
        return
    end
    if viewModel.viewType == TASK_VIEW_TYPE.GROUP_TITLE then
        cell.stateController:SetState("GroupTitle")
        self:_RenderTaskGroupTitle(cell, viewModel.groupInfo)
    elseif viewModel.viewType == TASK_VIEW_TYPE.TASK then
        cell.stateController:SetState("Task")
        self:_RenderTaskCell(cell, viewModel.taskInfo)
        cell.redDot:InitRedDot("BattlePassTaskItem", viewModel.taskInfo.taskId, nil, self.view.redDotScrollRect)
        self:_CollectShowedRedDot(viewModel.taskInfo.taskId)
    elseif viewModel.viewType == TASK_VIEW_TYPE.FORECAST then
        cell.stateController:SetState("Forecast")
        self:_RenderTaskForecast(cell, viewModel.forecastInfo)
    end
end

BattlePassTaskCtrl._RenderTaskGroupTitle = HL.Method(HL.Any, HL.Any) << function(self, cell, groupInfo)
    cell.groupNameTxt.text = groupInfo.name
    if self.m_isLifeTime then
        cell.groupTimeTxt.text = Language.LUA_BATTLEPASS_TASK_GROUP_LIFE_TIME_FORMAT
    else
        local curServerTime = DateTimeUtils.GetCurrentTimestampBySeconds()
        local leftSec = groupInfo.disableTime - curServerTime
        cell.groupTimeTxt.text = string.format(Language.LUA_BATTLEPASS_TASK_GROUP_TIME_LEFT_FORMAT, UIUtils.getLeftTime(leftSec))
    end
end


BattlePassTaskCtrl._RenderTaskCell = HL.Method(HL.Any, HL.Any) << function(self, cell, taskInfo)
    local taskVal = taskInfo.taskVal
    taskVal = math.min(taskVal, taskInfo.taskTarget)
    local progress = taskInfo.taskTarget > 0 and (taskVal / taskInfo.taskTarget) or 0
    local hasJump = not string.isEmpty(taskInfo.jumpId)
    local state = hasJump and "GoTo" or "NotGoTo"
    if taskInfo.taskState == CS.Proto.BP_TASK_STATE.HasCompleted then
        state = "Receive"
    elseif taskInfo.taskState == CS.Proto.BP_TASK_STATE.TakeReward then
        state = "End"
    end
    cell.rewardNumberTxt.text = string.format(Language.LUA_BATTLEPASS_TASK_ADD_EXP_FORMAT, taskInfo.addExp)
    cell.itemSmallRewardBlack:InitItem({id = Tables.battlePassConst.bpExpItem}, true)
    cell.descTxt:SetAndResolveTextStyle(taskInfo.name)
    cell.progressBar.value = progress
    cell.progressTxt.text = string.format(Language.LUA_BATTLEPASS_PLAN_EXP_PROGRESS_FORMAT, taskVal, taskInfo.taskTarget)
    cell.takCell:SetState(state)
    cell.goToBtn.onClick:RemoveAllListeners()
    if hasJump then
        cell.goToBtn.onClick:AddListener(function()
            if not BattlePassUtils.CheckBattlePassJumpAvail(taskInfo.jumpId) then
                Notify(MessageConst.SHOW_TOAST, Language.LUA_SYSTEM_FORBIDDEN)
                return
            end
            if not Utils.canJumpToSystem(taskInfo.jumpId) then
                Notify(MessageConst.SHOW_TOAST, Language.LUA_SYSTEM_LOCK)
                return
            end
            Utils.jumpToSystem(taskInfo.jumpId)
        end)
    end
    cell.receiveBtn.onClick:RemoveAllListeners()
    cell.receiveBtn.onClick:AddListener(function()
        self:_TakeTaskReward(taskInfo.taskId)
    end)
end


BattlePassTaskCtrl._RenderTaskForecast = HL.Method(HL.Any, HL.Any) << function(self, cell, forecastInfo)
    cell.descText.text = forecastInfo.desc
    cell.subDescTxt.text = forecastInfo.subDesc
end

BattlePassTaskCtrl._CollectShowedRedDot = HL.Method(HL.String) << function(self, id)
    if self.m_viewedTasks == nil then
        self.m_viewedTasks = {}
    end
    self.m_viewedTasks[id] = true
end

BattlePassTaskCtrl._ClearShowedRedDot = HL.Method() << function(self)
    if self.m_viewedTasks == nil then
        return
    end
    local taskIds = {}
    local bpSystem = GameInstance.player.battlePassSystem
    for id, flag in pairs(self.m_viewedTasks) do
        if flag == true and bpSystem:IsTaskUnread(id) then
            table.insert(taskIds, id)
        end
    end
    self.m_viewedTasks = {}
    if #taskIds <= 0 then
        return
    end
    bpSystem:ReadTasks(taskIds)
end

BattlePassTaskCtrl._OnSelectSubTab = HL.Method(HL.Number) << function(self, luaIndex)
    if self.m_selectedSubLabelIndex == luaIndex then
        return
    end
    self.m_selectedSubLabelIndex = luaIndex
    self:_ReadShowingTasks()
    self:_LoadGroupData()
    self:_RenderViews(true, true)
end

BattlePassTaskCtrl._TakeTaskReward = HL.Method(HL.String) << function(self, taskId)
    if string.isEmpty(taskId) then
        return
    end
    local taskIds = {}
    local bpSystem = GameInstance.player.battlePassSystem
    local hasPlayerTask, playerTask = bpSystem.taskData.taskInfos:TryGetValue(taskId)
    if not hasPlayerTask or not playerTask.taskState == CS.Proto.BP_TASK_STATE.HasCompleted then
        return
    end
    table.insert(taskIds, taskId)
    self:_TakeTaskRewards(taskIds)
end

BattlePassTaskCtrl._QueryAllCanTakeTaskIds = HL.Method().Return(HL.Table) << function(self)
    local taskIds = {}
    if #self.m_subLabelInfos > 0 then
        for _, labelInfo in ipairs(self.m_subLabelInfos) do
            local groups = self:_LoadGroupDataImpl(labelInfo)
            for _, groupInfo in ipairs(groups) do
                for _, taskInfo in ipairs(groupInfo.taskInfos) do
                    if taskInfo.taskState == CS.Proto.BP_TASK_STATE.HasCompleted then
                        table.insert(taskIds, taskInfo.taskId)
                    end
                end
            end
        end
    else
        for _, groupInfo in ipairs(self.m_taskGroups) do
            for _, taskInfo in ipairs(groupInfo.taskInfos) do
                if taskInfo.taskState == CS.Proto.BP_TASK_STATE.HasCompleted then
                    table.insert(taskIds, taskInfo.taskId)
                end
            end
        end
    end
    return taskIds
end

BattlePassTaskCtrl._ReadShowingTasks = HL.Method() << function(self)
    if self.m_taskViewModels == nil or #self.m_taskViewModels <= 0 then
        return
    end
    self:_ClearShowedRedDot()
end

BattlePassTaskCtrl._OnTaskUpdate = HL.Method() << function(self)
    self:_LoadData()
    self:_RenderViews(true, true, true)
end

BattlePassTaskCtrl._OnBasicInfoUpdate = HL.Method() << function(self)
    Notify(MessageConst.SHOW_POP_UP, {
        content = Language.LUA_BATTLE_PASS_TASK_INFO_REFRESH_POPUP,
        onConfirm = function()
            self:_LoadData(true)
            self:_RenderViews()
        end,
        hideCancel = true,
    })
end

BattlePassTaskCtrl._OnDailyActivationModify = HL.Method() << function(self)
    self:_RenderDailyPart()
end

BattlePassTaskCtrl._TakeAllTaskReward = HL.Method() << function(self)
    local taskIds = self:_QueryAllCanTakeTaskIds()
    self:_TakeTaskRewards(taskIds)
end

BattlePassTaskCtrl._TakeTaskRewards = HL.Method(HL.Table) << function(self, taskIds)
    local bpSystem = GameInstance.player.battlePassSystem
    bpSystem:SendTakeTaskRewards(taskIds)
    local needReadTasks = {}
    for _, taskId in ipairs(taskIds) do
        if bpSystem:IsTaskUnread(taskId) then
            table.insert(needReadTasks, taskId)
        end
    end
    if #needReadTasks > 0 then
        bpSystem:ReadTasks(needReadTasks)
    end
end

BattlePassTaskCtrl.GetRecoverStateArg = HL.Method().Return(HL.Table) << function(self)
    return {
        selectedLabelIndex = self.m_selectedLabelIndex,
        selectedSubLabelIndex = self.m_selectedSubLabelIndex,
    }
end

HL.Commit(BattlePassTaskCtrl)
