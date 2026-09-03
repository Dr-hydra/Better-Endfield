
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ActivityDevelopReturn

ActivityDevelopReturnCtrl = HL.Class('ActivityDevelopReturnCtrl', uiCtrl.UICtrl)

ActivityDevelopReturnCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_CONDITIONAL_MULTI_STAGE_UPDATE] = 'OnStageChange',
    [MessageConst.ON_CONDITIONAL_MULTI_STAGE_PROGRESS_CHANGE] = 'OnStageChange',
}

ActivityDevelopReturnCtrl.m_activityId = HL.Field(HL.String) << ''

ActivityDevelopReturnCtrl.m_activity = HL.Field(HL.Any)

ActivityDevelopReturnCtrl.m_curTabIndex = HL.Field(HL.Number) << 0

ActivityDevelopReturnCtrl.m_tabCells = HL.Field(HL.Any)

ActivityDevelopReturnCtrl.m_tabTotalCount = HL.Field(HL.Number) << 0

ActivityDevelopReturnCtrl.m_tasks = HL.Field(HL.Table)

ActivityDevelopReturnCtrl.m_getTaskCell = HL.Field(HL.Function)

ActivityDevelopReturnCtrl.m_curShowingTasks = HL.Field(HL.Table)

ActivityDevelopReturnCtrl.m_canReward = HL.Field(HL.Boolean) << false

ActivityDevelopReturnCtrl.m_allRewarded = HL.Field(HL.Boolean) << true

ActivityDevelopReturnCtrl.m_rewardTab = HL.Field(HL.Table)

ActivityDevelopReturnCtrl.m_rewardAll = HL.Field(HL.Table)

ActivityDevelopReturnCtrl.m_receiveAllBindingId = HL.Field(HL.Number) << -1

ActivityDevelopReturnCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    self.m_activityId = args.activityId
    self.m_curShowingTasks = {}
    self.view.activityCommonInfo:InitActivityCommonInfo(args)
    self:_RefreshInfo()

    self.m_receiveAllBindingId = self:BindInputPlayerAction("activity_develop_receive_all", function()
        logger.info("activity_develop_receive_all 被触发, canReward=" .. tostring(self.m_canReward) .. ", rewardTab count=" .. #self.m_rewardTab)
        if self.m_canReward then
            GameInstance.player.activitySystem:SendReceiveRewardConditionMultiStage(self.m_activityId, self.m_rewardTab)
        end
    end)









    self.m_getTaskCell = UIUtils.genCachedCellFunction(self.view.scrollList)
    self.view.scrollList.onUpdateCell:AddListener(function(obj, csIndex)
        self:_OnUpdateCell(self.m_getTaskCell(obj), LuaIndex(csIndex))
    end)


    self.m_tabCells = UIUtils.genCellCache(self.view.tabCell)
    self:_ChangeTab(1)
    self.m_tabCells:Refresh(self.m_tabTotalCount, function(cell, tabIndex)
        cell.clickBtn.onClick:AddListener(function()
            self:_ChangeTab(tabIndex)
        end)


        if cell.iconImg then
            local path = UIConst.UI_SPRITE_DEVELOP_RETURN_TAB_PATH
            local name = Tables.activityConst["ActivityCultivationRefundTabIcon" .. tabIndex]
            cell.iconImg:LoadSprite(path, name)
        end

        local tasks = {}
        for _,task in ipairs(self.m_tasks[tabIndex]) do
            table.insert(tasks, task)
        end
        cell.redDot:InitRedDot("ActivityDevelopReturnTaskSeries",{ self.m_activityId, tasks })

        cell.titleTxt.text = Language["LUA_ACTIVITY_CULTIVATION_REFUND_SERIES_" .. tabIndex]
    end)


    self:BindInputPlayerAction("common_toggle_group_previous_include_pc", function()
        self:_ChangeTab(self.m_curTabIndex == 1 and self.m_tabTotalCount or self.m_curTabIndex - 1 )
    end)
    self:BindInputPlayerAction("common_toggle_group_next_include_pc", function()
        self:_ChangeTab(self.m_curTabIndex % self.m_tabTotalCount + 1)
    end)


    local viewBindingId = self:BindInputPlayerAction("common_view_item", function()
        self:_SetNaviTarget(1)
    end)

    self.view.rightNaviGroup.onIsTopLayerChanged:AddListener(function(active)
        InputManagerInst:ToggleBinding(viewBindingId, not active)
        InputManagerInst:ToggleGroup(self.view.enterNode.groupId, not active)
    end)

end

ActivityDevelopReturnCtrl._RefreshInfo = HL.Method() << function(self)
    self.m_activity = GameInstance.player.activitySystem:GetActivity(self.m_activityId)
    self.m_tasks = {}
    self.m_rewardTab = {}
    self.m_rewardAll = {}
    self.m_canReward = false
    self.m_allRewarded = true

    local groupMap = {}


    for stageId, stageInfo in pairs(Tables.activityCultivationRefundStageTable) do
        local total = Tables.activityConditionalMultiStageCompleteConditionTable[stageId].conditionList[0].progressToCompare

        local curProgress = 0
        local isComplete = false
        local isReceived = false

        local suc, stageData = self.m_activity.stageDataDict:TryGetValue(stageId)

        if stageData.Conditions then

            for _, info in pairs(stageData.Conditions.Values) do
                curProgress = info
            end
        else

            curProgress = total
        end
        isComplete = stageData.Status >= GEnums.ActivityConditionalStageState.Completed:GetHashCode()
        isReceived = stageData.Status >= GEnums.ActivityConditionalStageState.Rewarded:GetHashCode()

        local task = {
            stageId = stageId,
            name = stageInfo.name,
            sortId = stageInfo.sortId,
            stageGroupId = stageInfo.stageGroupId,
            rewardId = stageInfo.rewardId,
            jumpId = stageInfo.jumpId,
            isComplete = isComplete,
            isReceived = isReceived,
            curProgress = curProgress,
            total = total,
        }
        task.statusSortId = task.isReceived and 3 or (task.isComplete and 1 or 2)

        if task.isComplete and not task.isReceived and not self.m_canReward then
            self.m_canReward = true
        end
        if not task.isComplete then
            self.m_allRewarded = false
        end
        if task.isComplete and not task.isReceived then
            table.insert(self.m_rewardAll, task.stageId)
            self.m_allRewarded = false
        end

        local groupId = stageInfo.stageGroupId
        local groupData = groupMap[groupId]
        if not groupData then
            groupData = {
                groupId = groupId,
                minSortId = stageInfo.sortId,
                tasks = {},
            }
            groupMap[groupId] = groupData
        else
            if stageInfo.sortId < groupData.minSortId then
                groupData.minSortId = stageInfo.sortId
            end
        end

        table.insert(groupData.tasks, task)
    end


    local groupList = {}
    for _, groupData in pairs(groupMap) do

        table.sort(groupData.tasks, function(a, b)
            if a.sortId ~= b.sortId then
                return a.sortId < b.sortId
            end
            return a.stageId < b.stageId
        end)
        table.insert(groupList, groupData)
    end


    table.sort(groupList, function(a, b)
        if a.minSortId ~= b.minSortId then
            return a.minSortId < b.minSortId
        end
        return a.groupId < b.groupId
    end)


    for i, groupData in ipairs(groupList) do
        self.m_tasks[i] = groupData.tasks
    end
    self.m_tabTotalCount = #groupList


    if self.m_allRewarded then
        self.view.downNode:SetState("Finish")
    else
        self.view.downNode:SetState("Receive")
    end



















end

ActivityDevelopReturnCtrl._OnUpdateRewardCell = HL.Method(HL.Table, HL.Any, HL.Number) << function(self, taskCell, rewardCell, rewardIndex)
    local rewardBundles = taskCell.m_rewardBundles
    if not rewardBundles or not rewardBundles[rewardIndex] then
        return
    end

    local bundle = rewardBundles[rewardIndex]
    local reward = {
        id = bundle.id,
        count = bundle.count,
    }

    rewardCell:InitItem(reward, true)
    rewardCell:SetExtraInfo({
        tipsPosType = UIConst.UI_TIPS_POS_TYPE.LeftMid,
        tipsPosTransform = self.view.scrollList.transform,
        isSideTips = true,
    })
    rewardCell.view.rewardedCover.gameObject:SetActive(taskCell.m_isReceived)
    rewardCell.view.selectedBG.gameObject:SetActive(false)
    rewardCell.view.button.onIsNaviTargetChanged = function(isTarget)
        rewardCell.view.selectedBG.gameObject:SetActive(isTarget)
    end
end

ActivityDevelopReturnCtrl._OnUpdateCell = HL.Method(HL.Table, HL.Number) << function(self, cell, index)

    local task = self.m_curShowingTasks[index]
    if not task then
        return
    end
    local isComplete = task.isComplete
    local isReceived = task.isReceived
    cell.m_isReceived = isReceived
    cell.descTxt.text = task.name
    cell.progressTxt.text = task.curProgress .. "/" .. task.total
    cell.fgSlider.fillAmount = task.curProgress / task.total
    cell.redDot:InitRedDot("ActivityDevelopReturnTask", {self.m_activityId, task.stageId}, nil, self.view.redDotScrollRect)
    cell.gameObject.name = "Cell" .. index


    local rewardId = task.rewardId
    local rewardBundles = UIUtils.getRewardItems(rewardId) or {}

    cell.m_rewardBundles = rewardBundles
    cell.m_getRewardCell = cell.m_getRewardCell or UIUtils.genCachedCellFunction(cell.rewardScrollList)
    cell.rewardScrollList.onUpdateCell:RemoveAllListeners()
    cell.rewardScrollList.onUpdateCell:AddListener(function(obj, csRewardIndex)
        self:_OnUpdateRewardCell(cell, cell.m_getRewardCell(obj), LuaIndex(csRewardIndex))
    end)
    cell.rewardScrollList:UpdateCount(#cell.m_rewardBundles)


    local state = isReceived and "Finish" or (isComplete and "Receive" or "Normal")
    cell.nodeState:SetState(state)


    cell.clickBtn.onClick:RemoveAllListeners()
    if isComplete and not isReceived then
        cell.clickBtn.onClick:AddListener(function()
            GameInstance.player.activitySystem:SendReceiveRewardConditionMultiStage(self.m_activityId, self.m_rewardTab)

            self:_RefreshInfo()
        end)
    end


    cell.goBtn.onClick:RemoveAllListeners()
    cell.goBtn.onClick:AddListener(function()
        Utils.jumpToSystem(task.jumpId)
    end)
end

ActivityDevelopReturnCtrl._SetNaviTarget = HL.Method(HL.Number) << function(self,index)
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

ActivityDevelopReturnCtrl._ChangeTab = HL.Method(HL.Number, HL.Opt(HL.Boolean)) << function(self, newIndex, forceRefresh)

    if self.m_curTabIndex == newIndex and not forceRefresh then
        return
    end


    AudioAdapter.PostEvent("Au_UI_Toggle_Tag_On")


    local curTasks = {}
    self.m_rewardTab = {}
    for _,task in ipairs(self.m_tasks[newIndex]) do
        table.insert(curTasks, task)

        if task.isComplete and not task.isReceived then
            table.insert(self.m_rewardTab, task.stageId)
        end
    end


    if #curTasks == 0 then
        Notify(MessageConst.SHOW_TOAST,Language.LUA_ACTIVITY_HIGH_DIFFICULTY_SERIES_UNLOCKED)
        return
    end


    self.m_curTabIndex = newIndex
    table.sort(curTasks, Utils.genSortFunction({"statusSortId", "sortId"}, true))
    self.m_curShowingTasks = curTasks


    if self.m_receiveAllBindingId >= 0 then
        local hasReward = #self.m_rewardTab > 0
        logger.info("ToggleBinding receiveAll, bindingId=" .. self.m_receiveAllBindingId .. ", hasReward=" .. tostring(hasReward) .. ", rewardTab count=" .. #self.m_rewardTab)
        InputManagerInst:ToggleBinding(self.m_receiveAllBindingId, #self.m_rewardTab > 0)
    end


    self.m_tabCells:Refresh(self.m_tabTotalCount, function(cell, tabIndex)
        if tabIndex == newIndex then
            cell.stateController:SetState("Selected")
        else
            cell.stateController:SetState("NotSelected")
        end
    end)


    self.view.scrollList:ScrollToIndex(1, true)
    self.view.scrollList:UpdateCount(#self.m_curShowingTasks)
    if DeviceInfo.usingController and self.view.rightNaviGroup.IsTopLayer then
        self:_SetNaviTarget(1)
    end
end

ActivityDevelopReturnCtrl.OnActivityCenterNaviFailed = HL.Method() << function(self)
    local firstCell = self.view.scrollList:GetRangeInView().x
    self:_SetNaviTarget(LuaIndex(firstCell))
end

ActivityDevelopReturnCtrl.OnStageChange = HL.Method(HL.Any) << function(self, args)
    local id = unpack(args)
    if id ~= self.m_activityId then
        return
    end
    self:_RefreshInfo()
    self:_ChangeTab(self.m_curTabIndex, true)
end

HL.Commit(ActivityDevelopReturnCtrl)
