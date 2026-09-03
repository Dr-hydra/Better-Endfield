
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ActivityHighDifficulty

ActivityHighDifficultyCtrl = HL.Class('ActivityHighDifficultyCtrl', uiCtrl.UICtrl)





ActivityHighDifficultyCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_CONDITIONAL_MULTI_STAGE_UPDATE] = 'OnStageChange',
    [MessageConst.ON_CONDITIONAL_MULTI_STAGE_PROGRESS_CHANGE] = 'OnStageChange',
}

ActivityHighDifficultyCtrl.m_activityId = HL.Field(HL.String) << ''

ActivityHighDifficultyCtrl.m_activity = HL.Field(HL.Any)

ActivityHighDifficultyCtrl.m_curTabIndex = HL.Field(HL.Number) << 0

ActivityHighDifficultyCtrl.m_tabCells = HL.Field(HL.Any)

ActivityHighDifficultyCtrl.m_tabTotalCount = HL.Field(HL.Number) << 0

ActivityHighDifficultyCtrl.m_tasks = HL.Field(HL.Table)

ActivityHighDifficultyCtrl.m_getTaskCell = HL.Field(HL.Function)

ActivityHighDifficultyCtrl.m_curShowingTasks = HL.Field(HL.Table)

ActivityHighDifficultyCtrl.m_BgNode = HL.Field(HL.Any)

ActivityHighDifficultyCtrl.m_shownNewTaskIds = HL.Field(HL.Table)


ActivityHighDifficultyCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)

    self.m_activityId = args.activityId
    self.m_curShowingTasks = {}
    self.m_shownNewTaskIds = {}
    self.view.activityCommonInfo:InitActivityCommonInfo(args)
    self:_RefreshInfo()


    self.view.redDotScrollRect.getRedDotStateAt = function(csIndex)
        return self:GetRedDotStateAt(csIndex)
    end


    self.m_getTaskCell = UIUtils.genCachedCellFunction(self.view.scrollList)
    self.view.scrollList.onUpdateCell:AddListener(function(obj, csIndex)
        self:_OnUpdateCell(self.m_getTaskCell(obj), LuaIndex(csIndex))
    end)


    self.m_tabCells = UIUtils.genCellCache(self.view.tabCell)
    self:_ChangeTab(1)
    if self.m_tabTotalCount == 1 then
        self.view.topNode.gameObject:SetActive(false)
    else
        self.m_tabCells:Refresh(self.m_tabTotalCount, function(cell, tabIndex)
            cell.numTxt.text = tabIndex
            cell.clickBtn.onClick:AddListener(function()
                self:_ChangeTab(tabIndex)
            end)
            local tasks = {}
            for _,task in ipairs(self.m_tasks) do
                if (task.openTime > self.m_activity.startTime) == (tabIndex > 1) then
                    table.insert(tasks, task)
                end
            end
            cell.redDot:InitRedDot("ActivityHighDifficultyTaskSeries",{ self.m_activityId, tasks })
            cell.titleTxt.text = #tasks == 0 and Language.LUA_ACTIVITY_HIGH_DIFFICULTY_SERIES_UNLOCKED or Language["LUA_ACTIVITY_HIGH_DIFFICULTY_SERIES_" .. tabIndex]
        end)

        self:BindInputPlayerAction("common_toggle_group_previous_include_pc", function()
            self:_ChangeTab(self.m_curTabIndex == 1 and self.m_tabTotalCount or self.m_curTabIndex - 1 )
        end)
        self:BindInputPlayerAction("common_toggle_group_next_include_pc", function()
            self:_ChangeTab(self.m_curTabIndex % self.m_tabTotalCount + 1)
        end)
    end


    local viewBindingId = self:BindInputPlayerAction("common_view_item", function()
        self:_SetNaviTarget(1)
    end)

    self.view.rightNaviGroup.onIsTopLayerChanged:AddListener(function(active)
        InputManagerInst:ToggleBinding(viewBindingId, not active)
        InputManagerInst:ToggleGroup(self.view.enterNode.groupId, not active)
    end)


    self.view.activityCommonInfo.view.gotoNode.btnDetailRedDot:InitRedDot("ActivityHighDifficultyDetailBtn", self.m_activityId)


    local _, info = Tables.activityHighDifficultyTable:TryGetValue(self.m_activityId)
    if info and info.bgNode then
        local path = string.format(UIConst.UI_ACTIVITY_HIGH_DIFFICULTY_BG_PREFAB_PATH, info.bgNode)
        local prefab = self:LoadGameObject(path)
        if self.m_BgNode then
            CSUtils.ClearUIComponents(self.m_BgNode)
            GameObject.DestroyImmediate(self.m_BgNode)
        end
        self.m_BgNode = CSUtils.CreateObject(prefab, self.view.bgNode)
    end
end

ActivityHighDifficultyCtrl._RefreshInfo = HL.Method() << function(self)
    self.m_activity = GameInstance.player.activitySystem:GetActivity(self.m_activityId)
    self.m_tasks = {}
    self.m_tabTotalCount = 0
    local openTimes = {}


    for stageId, stageInfo in pairs(Tables.activityConditionalMultiStageTable[self.m_activityId].stageList) do
        local total = Tables.activityConditionalMultiStageCompleteConditionTable[stageId].conditionList[0].progressToCompare
        local curProgress
        local isComplete
        local isReceived

        local _, stageData = self.m_activity.stageDataDict:TryGetValue(stageId)
        local openTime = stageData.OpenTimeTs
        if not openTimes[openTime] then
            openTimes[openTime] = true
            self.m_tabTotalCount = self.m_tabTotalCount + 1
        end
        if stageData.Status ~= GEnums.ActivityConditionalStageState.Locked:GetHashCode() then
            if stageData.Conditions then

                for _, info in pairs(stageData.Conditions.Values) do
                    curProgress = info
                end
            else

                curProgress = total
            end
            isComplete = stageData.Status >= GEnums.ActivityConditionalStageState.Completed:GetHashCode()
            isReceived = stageData.Status >= GEnums.ActivityConditionalStageState.Rewarded:GetHashCode()
        else
            curProgress = 0
            isComplete = false
            isReceived = false
        end

        local task = {
            stageId = stageId,
            name = stageInfo.name,
            sortId = stageInfo.sortId,
            openTime = openTime,
            rewardId = stageInfo.rewardId,
            isComplete = isComplete,
            isReceived = isReceived,
            curProgress = curProgress,
            total = total,
        }
        task.statusSortId = task.isReceived and 3 or (task.isComplete and 1 or 2)
        table.insert(self.m_tasks, task)
    end
end

ActivityHighDifficultyCtrl.GetAllCanReceiveStageIds = HL.Method().Return(HL.Table) << function(self)
    local stageIds = {}
    for _, task in ipairs(self.m_curShowingTasks) do
        if task.isComplete and not task.isReceived then
            table.insert(stageIds, task.stageId)
        end
    end
    return stageIds
end

ActivityHighDifficultyCtrl._OnUpdateCell = HL.Method(HL.Table, HL.Number) << function(self, cell, index)

    local task = self.m_curShowingTasks[index]
    local isComplete = task.isComplete
    local isReceived = task.isReceived
    cell.descTxt.text = task.name
    cell.progressTxt.text = task.curProgress .. "/" .. task.total
    cell.fgSlider.fillAmount = task.curProgress / task.total
    cell.redDot:InitRedDot("ActivityHighDifficultyTask", {self.m_activityId, task.stageId}, nil, self.view.redDotScrollRect)
    if HighDifficultyUtils.isNewHighDifficultyTask(self.m_activityId, task.stageId) then
        self.m_shownNewTaskIds[task.stageId] = true
    end
    cell.gameObject.name = "Cell" .. index


    local rewardId = task.rewardId
    local rewardBundles = UIUtils.getRewardItems(rewardId)
    cell.rewardCellCache = cell.rewardCellCache or UIUtils.genCellCache(cell.itemSmallReward)
    cell.rewardCellCache:Refresh(#rewardBundles, function(item, innerIndex)
        local reward = {
            id = rewardBundles[innerIndex].id,
            count = rewardBundles[innerIndex].count,
        }
        item:InitItem(reward, true)
        item:SetExtraInfo({
            tipsPosType = UIConst.UI_TIPS_POS_TYPE.LeftMid,
            tipsPosTransform = self.view.scrollList.transform,
            isSideTips = true,
        })
        item.view.rewardedCover.gameObject:SetActive(isReceived)
    end)


    local state = isReceived and "Received" or (isComplete and "Completed" or "Normal")
    cell.nodeState:SetState(state)
    cell.nodeState:SetState( state .. "Topic")
    local bgName = Tables.activityHighDifficultySpecialStageTable[task.stageId].bgName
    if not string.isEmpty(bgName) then
        cell.bgImage.gameObject:SetActive(true)
        cell.bgImage:LoadSprite(UIConst.UI_SPRITE_ACTIVITY_HIGH_DIFFICULTY, bgName)
    else
        cell.bgImage.gameObject:SetActive(false)
    end


    cell.clickNode.onClick:RemoveAllListeners()
    if isComplete and not isReceived then
        cell.clickNode.onClick:AddListener(function()
            GameInstance.player.activitySystem:SendReceiveRewardConditionMultiStage(self.m_activityId, self:GetAllCanReceiveStageIds())
        end)
    end
end

ActivityHighDifficultyCtrl._SetNaviTarget = HL.Method(HL.Number) << function(self,index)
    if index == 0 or not DeviceInfo.usingController  then
        return
    end
    local oriCell = self.view.scrollList:Get(CSIndex(index))
    if not oriCell then
        self.view.scrollList:ScrollToIndex(index, true)
        oriCell = self.view.scrollList:Get(CSIndex(index))
    end
    local cell = oriCell and self.m_getTaskCell(oriCell)
    if cell then
        self:SetNaviTarget(cell.naviDecorator)
    end
end

ActivityHighDifficultyCtrl._ChangeTab = HL.Method(HL.Number, HL.Opt(HL.Boolean)) << function(self, newIndex, forceRefresh)

    if self.m_curTabIndex == newIndex and not forceRefresh then
        return
    end


    AudioAdapter.PostEvent("Au_UI_Toggle_Tag_On")


    local curTasks = {}
    for _,task in ipairs(self.m_tasks) do
        if (task.openTime > self.m_activity.startTime) == (newIndex > 1) then
            table.insert(curTasks, task)
        end
    end


    if #curTasks == 0 then
        Notify(MessageConst.SHOW_TOAST,Language.LUA_ACTIVITY_HIGH_DIFFICULTY_SERIES_UNLOCKED)
        return
    end


    self.m_curTabIndex = newIndex
    table.sort(curTasks, Utils.genSortFunction({"statusSortId", "sortId"}, true))
    self.m_curShowingTasks = curTasks


    self.m_tabCells:Refresh(self.m_tabTotalCount, function(cell, tabIndex)
        if tabIndex == newIndex then
            cell.stateController:SetState("Selected")
        else
            cell.stateController:SetState("UnSelected")
        end
    end)


    self.view.scrollList:ScrollToIndex(1, true)
    self.view.scrollList:UpdateCount(#self.m_curShowingTasks)
    if DeviceInfo.usingController and self.view.rightNaviGroup.IsTopLayer then
        self:_SetNaviTarget(1)
    end
end

ActivityHighDifficultyCtrl.OnStageChange = HL.Method(HL.Any) << function(self, args)
    local id = unpack(args)
    if id ~= self.m_activityId then
        return
    end
    self:_RefreshInfo()
    self.m_tabCells:Refresh(self.m_tabTotalCount, function(cell, tabIndex)
        local notOpen = true
        for _,task in ipairs(self.m_tasks) do
            if (task.openTime > self.m_activity.startTime) == (tabIndex > 1) then
                notOpen = false
                break
            end
        end
        cell.titleTxt.text = notOpen and Language.LUA_ACTIVITY_HIGH_DIFFICULTY_SERIES_UNLOCKED or Language["LUA_ACTIVITY_HIGH_DIFFICULTY_SERIES_" .. tabIndex]
    end)
    self:_ChangeTab(self.m_curTabIndex, true)
end

ActivityHighDifficultyCtrl.GetRedDotStateAt = HL.Method(HL.Number).Return(HL.Number) << function(self, index)
    local luaIndex = LuaIndex(index)
    if luaIndex < 1 or luaIndex > #self.m_curShowingTasks then
        return 0
    end
    local task = self.m_curShowingTasks[luaIndex]
    local hasRedDot, redDotType = RedDotManager:GetRedDotState("ActivityHighDifficultyTask", {self.m_activityId, task.stageId})
    if hasRedDot then
        return redDotType or UIConst.RED_DOT_TYPE.Normal
    else
        return 0
    end
end


ActivityHighDifficultyCtrl.OnActivityCenterNaviFailed = HL.Method() << function(self)
    local firstCell = self.view.scrollList:GetRangeInView().x
    self:_SetNaviTarget(LuaIndex(firstCell))
end

ActivityHighDifficultyCtrl.OnClose = HL.Override() << function(self)
    local taskIds = {}
    for taskId,_ in pairs(self.m_shownNewTaskIds) do
        table.insert(taskIds, taskId)
    end
    HighDifficultyUtils.setFalseNewHighDifficultyTask(self.m_activityId, taskIds)
end

HL.Commit(ActivityHighDifficultyCtrl)
