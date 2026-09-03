
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ActivityCenter
local PHASE_ID = PhaseId.ActivityCenter


ActivityCenterCtrl = HL.Class('ActivityCenterCtrl', uiCtrl.UICtrl)





ActivityCenterCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_ACTIVITY_UPDATED] = 'OnActivityUpdated',
    [MessageConst.ON_ACTIVITY_NAVI_FAILED] = 'OnActivityNaviFailed',
    [MessageConst.ON_ACTIVITY_CENTER_BACK_TO_TOP] = '_OnBackToTop',
    [MessageConst.ON_UNREAD_ACTIVITY_PUSH] = '_OnServerUnreadActivityPush',
}

ActivityCenterCtrl.s_debugDelay = HL.StaticField(HL.Number) << -1

ActivityCenterCtrl.m_selectedPanel = HL.Field(HL.Any)

ActivityCenterCtrl.m_fromDialog = HL.Field(HL.Boolean) << false

ActivityCenterCtrl.m_allActivities = HL.Field(HL.Table)

ActivityCenterCtrl.m_selectedTabIndex = HL.Field(HL.Number) << 0

ActivityCenterCtrl.m_activityDict = HL.Field(HL.Table)

ActivityCenterCtrl.m_tabCells = HL.Field(HL.Any)

ActivityCenterCtrl.m_cells = HL.Field(HL.Any)

ActivityCenterCtrl.m_activityId = HL.Field(HL.String) << ""

ActivityCenterCtrl.m_initialActivityId = HL.Field(HL.String) << ""

ActivityCenterCtrl.m_enterType = HL.Field(HL.String) << ""






ActivityCenterCtrl.m_optimisticReadPushIds = HL.Field(HL.Table)




ActivityCenterCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)




    self:_InitInfoAndButtons(arg)
    self:_RefreshTabList()
    self:_InitController()
    self:GoToActivity(self.m_initialActivityId)
end

ActivityCenterCtrl.OnHide = HL.Override() << function(self)
    if self.m_phase then
        self.m_phase:_ClearCoroutine(self.m_phase.m_delayShowActivityCo)
    end
end

ActivityCenterCtrl.OnClose = HL.Override() << function(self)

    GameInstance.player.activitySystem:ClearInviteBackCodeCache()
end

ActivityCenterCtrl._InitInfoAndButtons = HL.Method(HL.Table) << function(self, arg)
    self.view.btnClose.onClick:AddListener(function()
        self:_Close()
    end)
    self:BindInputPlayerAction("activity_center_f7_close", function()
        self:_Close()
    end)
    self.m_tabCells = UIUtils.genCellCache(self.view.tabCell)
    self.m_initialActivityId = (arg and arg.gotoCenter) and arg.activityId or ""
    self.m_enterType = arg and arg.openFrom or "SomewhereElse"
end

ActivityCenterCtrl._InitController = HL.Method() << function(self)
    if DeviceInfo.usingController then
        if #self.m_allActivities > 0 and not self.m_initialActivityId then
            self:_SetNaviTarget(1)
        end
    end
end

ActivityCenterCtrl.OnActivityNaviFailed = HL.Method(HL.Userdata) << function(self, dir)
    if dir == Unity.UI.NaviDirection.Down then
        self:_SetNaviTarget(1)
    elseif dir == Unity.UI.NaviDirection.Up then
        self:_SetNaviTarget(#self.m_allActivities)
    end
end




ActivityCenterCtrl._RefreshTabList = HL.Method() << function(self)

    local lastCount = 0
    local lastActivityId
    if self.m_allActivities then
        lastCount = #self.m_allActivities
        if lastCount > 0 then
            lastActivityId = self.m_allActivities[self.m_selectedTabIndex].id
        end
    end


    self.m_allActivities = {}
    local activities = GameInstance.player.activitySystem:GetAllActivities()


    for _, activity in cs_pairs(activities) do
        local _, activityData = Tables.activityTable:TryGetValue(activity.id)
        if activityData then



            local tabPush, allActives = self:_PickActiveTabPush(activity.id, activity)
            local normalState, selectedState = self:_ResolveTabCellStateNames(tabPush, activity)
            table.insert(self.m_allActivities, {
                id = activity.id,
                sortId = -activityData.sortId,
                activity = activity,
                activityData = activityData,
                completed = activity:GetCompleteSortId(),
                type = activityData.type,
                status = activity.status,

                placeAtBottom = activity.placeAtBottom,
                normalState = normalState,
                selectedState = selectedState,
                tabPush = tabPush,
                tabPushAllActives = allActives,
            })
        end
    end
    table.sort(self.m_allActivities, Utils.genSortFunction({"completed","sortId", "id"}, true))


    self.m_activityDict = {}
    for index = 1,#self.m_allActivities do
        local activity = self.m_allActivities[index]
        self.m_activityDict[activity.id] = {
            type = activity.type,
            index = index,
        }
    end





    do
        local activitySystem = GameInstance.player.activitySystem
        for _, entry in ipairs(self.m_allActivities) do
            local tabPush = entry.tabPush
            if tabPush
                and tabPush.tabType == "End"
                and not string.isEmpty(tabPush.pushID)
                and not activitySystem:IsActivityPushRead(tabPush.pushID)
                and ActivityUtils.isActivityEndTabRedDotSeen(tabPush.pushID)
            then
                ActivityUtils.clearActivityEndTabRedDotSeen(tabPush.pushID)
            end
        end
    end


    self.m_cells = {}
    self.m_tabCells:Refresh(#self.m_allActivities, function(cell, index)
        self:_OnUpdateCell(cell, index)
    end)


    for _, info in ipairs(self.m_allActivities) do
        if info.activity.isCompleted then
            if ActivityUtils.isNewActivity(info.id) then
                ActivityUtils.setFalseNewActivity(info.id)
            end
            if ActivityUtils.isNewUnlockActivity(info.id) then
                ActivityUtils.setFalseNewUnlockActivity(info.id)
            end
        end
    end

    if self.m_selectedTabIndex == 0 then

        if #self.m_allActivities > 0 then
            local targetActivityId = not string.isEmpty(self.m_initialActivityId) and self.m_initialActivityId or self.m_allActivities[1].id
            ActivityUtils.GameEventLogActivityEnter(self.m_enterType, targetActivityId)

            local targetIndex = 1
            if not string.isEmpty(self.m_initialActivityId) and self.m_activityDict[self.m_initialActivityId] then
                targetIndex = self.m_activityDict[self.m_initialActivityId].index or 1
            end
            self:_OnTabClicked(targetIndex, nil, nil, true)
        end
    else

        self:GoToActivity(lastActivityId)
    end
end

ActivityCenterCtrl._OnUpdateCell = HL.Method(HL.Any, HL.Number) << function(self, cell, index)
    self.m_cells[index] = cell

    cell.button.onClick:RemoveAllListeners()
    cell.button.onClick:AddListener(function()
        self:_OnTabClicked(index, nil, DeviceInfo.usingController)
    end)
    self:_SetTabCellSelected(cell, index == self.m_selectedTabIndex, index)
    local activityData = self.m_allActivities[index].activityData

    local nodes = {
        cell.selectNode,
        cell.normalNode,
    }
    for i = 1, #nodes do
        local innerCell = nodes[i]
        innerCell.txtName.text = activityData.name
        if activityData.tabImg ~= "" then
            if activityData.tabImgGender then
                local suffix = Utils.getPlayerGender() == CS.Proto.GENDER.GenMale and "_boy" or "_girl"
                innerCell.tabImg:LoadSprite(UIConst.UI_SPRITE_ACTIVITY,activityData.tabImg .. suffix)
            else
                innerCell.tabImg:LoadSprite(UIConst.UI_SPRITE_ACTIVITY,activityData.tabImg)
            end
        end


        if activityData.tabImgColor then
            local suc, color = CS.UnityEngine.ColorUtility.TryParseHtmlString(activityData.tabImgColor)
            if suc then
                innerCell.selectedBg.color = color
                innerCell.decoLine.color = color
                innerCell.decoArrow.color = color
            end
        end


        local activity = GameInstance.player.activitySystem:GetActivity(activityData.id)
        innerCell.completedIconNode.gameObject:SetActive(activity.isCompleted or activity.placeAtBottom)
    end






    self:_RefreshTabPriorityIndicators(cell, self.m_allActivities[index])





    self:_RefreshTabEndCountDown(cell, self.m_allActivities[index])
end



ActivityCenterCtrl._RefreshTabEndCountDown = HL.Method(HL.Any, HL.Table) << function(self, cell, entry)
    local countDownText = cell and cell.countDownText
    if not countDownText then
        return
    end
    local tabPush = entry and entry.tabPush
    local activity = entry and entry.activity
    local needCountDown = tabPush
        and tabPush.tabType == "End"
        and activity
        and not activity.isCompleted


        and not activity.placeAtBottom
    if needCountDown then




        local endTime = self:_GetServerActivityEndTime(tabPush, activity)
        if not endTime then
            if tabPush.isWeeklyRefresh then
                endTime = Utils.getNextWeeklyServerRefreshTime()
            else
                endTime = activity.endTime or 0
            end
        end





        local curTime = DateTimeUtils.GetCurrentTimestampBySeconds()
        if endTime <= curTime then
            countDownText:StopCountDown()
            countDownText.gameObject:SetActive(false)
            return
        end

        countDownText.gameObject:SetActive(true)
        countDownText:InitCountDownText(endTime, function()


            if self and self.m_allActivities then
                self:_RefreshTabList()
            end
        end)
    else
        countDownText:StopCountDown()
        countDownText.gameObject:SetActive(false)
    end
end

ActivityCenterCtrl._OnTabClicked = HL.Method(HL.Number, HL.Opt(HL.Boolean, HL.Boolean, HL.Boolean)) << function(self, index, forceRefresh, isFromNavi, isInit)

    if #self.m_allActivities == 0 then
        ActivityUtils.backToMainHud(true)
        return
    end



    if not isInit and self.m_selectedTabIndex ~= index and GameInstance.player.guide.isInForceGuide then
        return
    end


    if self.m_selectedTabIndex == index and not forceRefresh then
        return
    end

    local lastSelectedCell = self:_GetCell(self.m_selectedTabIndex)
    if lastSelectedCell then
        self:_SetTabCellSelected(lastSelectedCell, false, self.m_selectedTabIndex)
    end
    self.m_selectedTabIndex = index
    local selectedCell = self:_GetCell(index)
    if selectedCell then
        self:_SetTabCellSelected(selectedCell, true, index)
    end


    local id = self.m_allActivities[index].id
    ActivityUtils.GameEventLogActivityVisit(id, "ActivityTabCellButton", "visit_center")

    local entry = self.m_allActivities[index]






    local tabPush = entry and entry.tabPush


    if tabPush
        and tabPush.tabType == "Update"
        and not string.isEmpty(tabPush.pushID)
        and self:_IsConditionListSatisfied(tabPush.pushID, entry and entry.activity)
        and not GameInstance.player.activitySystem:IsActivityPushRead(tabPush.pushID)
    then
        local clickedCell = self:_GetCell(index)
        if clickedCell and clickedCell.normalNode and not IsNull(clickedCell.normalNode.updateNode) then
            local markReadIds = self:_CollectTabBatchReadIds(tabPush, entry)
            if #markReadIds > 0 then
                if not self.m_optimisticReadPushIds then
                    self.m_optimisticReadPushIds = {}
                end
                for _, pushID in ipairs(markReadIds) do
                    self.m_optimisticReadPushIds[pushID] = true
                end
                GameInstance.player.activitySystem:MarkActivityPushReadBatch(markReadIds)
            end
            clickedCell.normalNode.updateNode.gameObject:SetActive(false)
        end
    end





    if entry.activity then
        local bubbleReadIds = self:_CollectActivityUnreadBubblePushIds(id, entry.activity)
        if #bubbleReadIds > 0 then
            if not self.m_optimisticReadPushIds then
                self.m_optimisticReadPushIds = {}
            end
            for _, pushID in ipairs(bubbleReadIds) do
                self.m_optimisticReadPushIds[pushID] = true
            end
            GameInstance.player.activitySystem:MarkActivityPushReadBatch(bubbleReadIds)
        end
    end










    if tabPush
        and tabPush.tabType == "End"
        and not string.isEmpty(tabPush.pushID)
        and self:_IsConditionListSatisfied(tabPush.pushID, entry and entry.activity)
    then
        if not ActivityUtils.isActivityEndTabRedDotSeen(tabPush.pushID) then
            ActivityUtils.setActivityEndTabRedDotSeen(tabPush.pushID)
        end
        if not GameInstance.player.activitySystem:IsActivityPushRead(tabPush.pushID) then

            local markReadIds = self:_CollectTabBatchReadIds(tabPush, entry)
            if #markReadIds > 0 then
                if not self.m_optimisticReadPushIds then
                    self.m_optimisticReadPushIds = {}
                end
                for _, pushID in ipairs(markReadIds) do
                    self.m_optimisticReadPushIds[pushID] = true
                end
                GameInstance.player.activitySystem:MarkActivityPushReadBatch(markReadIds)
            end
        end
    end
    if ActivityUtils.isNewActivity(id) then
        ActivityUtils.setFalseNewActivity(id)
    end
    if ActivityUtils.isNewUnlockActivity(id) then
        ActivityUtils.setFalseNewUnlockActivity(id)
    end


    if ActivityUtils.isNewActivityBubble(id) and entry.activity.isUnlocked then
        ActivityUtils.setFalseNewActivityBubble(id)
    end

    local pushPopupId = ActivityUtils.getActivityPushPopupId(id)
    if pushPopupId and not GameInstance.player.activitySystem:IsActivityPushRead(pushPopupId) then
        GameInstance.player.activitySystem:MarkActivityPushReadOne(pushPopupId)
    end


    local _clickedCell = self:_GetCell(index)
    if _clickedCell then
        for _, _innerCell in ipairs({ _clickedCell.selectNode, _clickedCell.normalNode }) do
            if _innerCell and _innerCell.redDot then
                self:_ApplyRedDotPriority(_innerCell.redDot, entry)
            end
        end
    end

    if self.m_activityId == id then
        return
    end


    self.m_activityId = id
    local msg = isFromNavi and MessageConst.SHOW_ACTIVITY_PANEL_FROM_NAVI or MessageConst.SHOW_ACTIVITY_PANEL
    Notify(msg, {
        activityId = id,
        controllerHintPlaceholder = self.view.controllerHintPlaceholder,
        groupId = self.view.inputBindingGroupMonoTarget.groupId,
        naviGroup = self.view.tabScrollRectSelectableNaviGroup,
        getReturnTargetFunc = function()
            return self:_GetCell(self.m_activityDict[id].index).button
        end,
        btnClose = self.view.btnClose,
        delay = ActivityCenterCtrl.s_debugDelay >= 0 and ActivityCenterCtrl.s_debugDelay or self.view.config.SHOW_ACTIVITY_FROM_NAVI_DELAY,
        isInit = isInit or false,
    })
end



ActivityCenterCtrl.GoToActivity = HL.Method(HL.Any, HL.Opt(HL.Boolean)) << function(self, activityId, forceRefresh)
    local index
    if not string.isEmpty(activityId) and self.m_activityDict[activityId] then
        index = self.m_activityDict[activityId].index or 1
    else
        index = 1
    end
    if DeviceInfo.usingController then
        InputManagerInst.controllerNaviManager:TryRemoveLayer(self.view.tabScrollRectSelectableNaviGroup)
    end
    self:_OnTabClicked(index, forceRefresh)
    LayoutRebuilder.ForceRebuildLayoutImmediate(self.view.contentRectTransform)
    local cell = self:_GetCell(index)
    if cell then
        self.view.tabScrollRect:ScrollToNaviTarget(cell.button)
    end
    self:_SetNaviTarget(index)
end

ActivityCenterCtrl._GetShowingCellStartEnd = HL.Method().Return(HL.Number,HL.Number) << function(self)
    local redDotSize = self.view.tabCell.selectNode.redDot.rectTransform.rect.height
    local totalCount = #self.m_allActivities

    local low, high = 1, totalCount
    local start = totalCount
    while low <= high do
        local mid = math.floor((low + high) / 2)
        local cell = self:_GetCell(mid)
        local top = -self.view.contentRectTransform.anchoredPosition.y - cell.gameObject:GetComponent("RectTransform").anchoredPosition.y - self.view.tabCellRectTransform.rect.height / 2 - redDotSize / 2
        local bottom = top + redDotSize

        if bottom > 0 then
            start = mid
            high = mid - 1
        else
            low = mid + 1
        end
    end

    low, high = 1, totalCount
    local final = 1
    while low <= high do
        local mid = math.floor((low + high) / 2)
        local cell = self:_GetCell(mid)
        local top = -self.view.contentRectTransform.anchoredPosition.y - cell.gameObject:GetComponent("RectTransform").anchoredPosition.y - self.view.tabCellRectTransform.rect.height / 2 - redDotSize / 2

        if top < self.view.tabScrollRectRectTransform.rect.height then
            final = mid
            low = mid + 1
        else
            high = mid - 1
        end
    end

    return start, final
end

ActivityCenterCtrl._GetCell = HL.Method(HL.Number).Return(HL.Any) << function(self, index)
    local target
    self.m_tabCells:Refresh(#self.m_allActivities, function(cell, tabIndex)
        if index == tabIndex then
            target = cell
        end
    end)
    return target
end

ActivityCenterCtrl._SetNaviTarget = HL.Method(HL.Number) << function(self, index)
    local cell = self:_GetCell(index)
    if DeviceInfo.usingController and cell and not IsNull(cell.button) then
        self.view.tabScrollRect:ScrollToNaviTarget(cell.button)
        self:SetNaviTarget(cell.button)
    end
end


local DEFAULT_TAB_NORMAL_STATE = "normal"
local DEFAULT_TAB_SELECTED_STATE = "selected"

local TAB_END_NORMAL_STATE = "NrlHint"
local TAB_END_SELECTED_STATE = "SelHint"

ActivityCenterCtrl._SetTabCellSelected = HL.Method(HL.Table, HL.Boolean, HL.Opt(HL.Number)) << function(self, tabCell, isSelected, index)
    local normalState = DEFAULT_TAB_NORMAL_STATE
    local selectedState = DEFAULT_TAB_SELECTED_STATE
    if index and self.m_allActivities and self.m_allActivities[index] then
        normalState = self.m_allActivities[index].normalState or DEFAULT_TAB_NORMAL_STATE
        selectedState = self.m_allActivities[index].selectedState or DEFAULT_TAB_SELECTED_STATE
    end
    tabCell.stateController:SetState(isSelected and selectedState or normalState)
end




ActivityCenterCtrl._GetServerOffsetHours = HL.Method(HL.Any).Return(HL.Number) << function(self, pushData)
    if not pushData or not pushData.offset or not pushData.offset.Count or pushData.offset.Count <= 0 then
        return 0
    end
    local serverType = Utils.getServerAreaType():GetHashCode()
    if serverType <= 0 then
        serverType = 1
    end
    local idx = CSIndex(serverType)
    if idx > pushData.offset.Count - 1 then
        idx = CSIndex(1)
    end
    return pushData.offset[idx] or 0
end





ActivityCenterCtrl._GetServerActivityEndTime = HL.Method(HL.Any, HL.Any).Return(HL.Opt(HL.Number)) << function(self, pushData, activity)
    if not pushData or not activity then
        return nil
    end
    local arr = pushData.activityEndTime
    if not arr or not arr.Count or arr.Count <= 0 then
        return nil
    end
    local serverType = Utils.getServerAreaType():GetHashCode()
    if serverType <= 0 then
        serverType = 1
    end
    local idx = CSIndex(serverType)
    if idx > arr.Count - 1 then
        idx = CSIndex(1)
    end
    local offsetHours = arr[idx]
    if not offsetHours then
        return nil
    end
    return (activity.startTime or 0) + offsetHours * Const.SEC_PER_HOUR
end










ActivityCenterCtrl._IsConditionListSatisfied = HL.Method(HL.String, HL.Opt(HL.Any)).Return(HL.Boolean) << function(self, pushID, activity)
    if string.isEmpty(pushID) or not Tables.activityPushConditionTable then
        return true
    end
    local hasCondition, conditionData = Tables.activityPushConditionTable:TryGetValue(pushID)

    local conditionListMissing = (not hasCondition)
        or (not conditionData)
        or (not conditionData.conditionList)
        or (not conditionData.conditionList.Count)
        or (conditionData.conditionList.Count <= 0)
    if conditionListMissing then
        if activity and activity.status == GEnums.ActivityStatus.Locked then
            return false
        end
        return true
    end
    local list = conditionData.conditionList
    for i = 1, list.Count do
        local cond = list[CSIndex(i)]
        if cond then
            local ok, value = LuaGameConditionUtils.getConditionValueByParameters(cond.conditionType, cond.parameters)
            if not ok then
                return false
            end
            if not Utils.compareInt(value, cond.progressToCompare, cond.compareOperator) then
                return false
            end
        end
    end
    return true
end






ActivityCenterCtrl._IsPushActivated = HL.Method(HL.Any, HL.Any, HL.Number, HL.Number).Return(HL.Boolean) << function(self, pushData, activity, curTs, curWeekday)


    local realEndTime = self:_GetServerActivityEndTime(pushData, activity)
    if realEndTime and curTs >= realEndTime then
        return false
    end
    if pushData.isWeeklyRefresh then
        if activity.isCompleted or activity.placeAtBottom then
            return false
        end
        if curWeekday < (pushData.pushInWeekday or 0) then
            return false
        end
        return self:_IsConditionListSatisfied(pushData.pushID)
    end
    local offsetHours = self:_GetServerOffsetHours(pushData)
    local activationTime = (activity.startTime or 0) + offsetHours * Const.SEC_PER_HOUR
    return curTs >= activationTime
end





ActivityCenterCtrl._GetTabPushActivationOrder = HL.Method(HL.Any, HL.Any).Return(HL.Number) << function(self, pushData, activity)
    return pushData.bubbleSortId or 0
end






ActivityCenterCtrl._CollectActivityUnreadBubblePushIds = HL.Method(HL.String, HL.Any).Return(HL.Table) << function(self, activityId, activity)
    local result = {}
    if not Tables.activityPushBubbleTable or not activity then
        return result
    end
    local activitySystem = GameInstance.player.activitySystem

    local optimisticRead = self.m_optimisticReadPushIds
    local curTs = DateTimeUtils.GetCurrentTimestampBySeconds()
    local curWeekday = Utils.getServerWeekdayISOAt4AM()
    for _, pushData in pairs(Tables.activityPushBubbleTable) do
        if pushData.activityId == activityId
            and pushData.pushType == "Bubble"
            and not string.isEmpty(pushData.pushID)
            and self:_IsPushActivated(pushData, activity, curTs, curWeekday)
            and self:_IsConditionListSatisfied(pushData.pushID, activity)
            and not activitySystem:IsActivityPushRead(pushData.pushID)
            and not (optimisticRead and optimisticRead[pushData.pushID])
        then
            table.insert(result, pushData.pushID)
        end
    end
    return result
end




ActivityCenterCtrl._GatherActiveTabPushes = HL.Method(HL.String, HL.Any).Return(HL.Any) << function(self, activityId, activity)
    local result = {}
    if not Tables.activityPushBubbleTable or not activity then
        return result
    end
    local curTs = DateTimeUtils.GetCurrentTimestampBySeconds()

    local curWeekday = Utils.getServerWeekdayISOAt4AM()
    for _, pushData in pairs(Tables.activityPushBubbleTable) do
        if pushData.activityId == activityId
            and pushData.pushType == "Tab"
            and self:_IsPushActivated(pushData, activity, curTs, curWeekday)
        then
            table.insert(result, {
                pushData = pushData,
                activationOrder = self:_GetTabPushActivationOrder(pushData, activity),
            })
        end
    end
    return result
end






ActivityCenterCtrl._PickActiveTabPush = HL.Method(HL.String, HL.Any).Return(HL.Opt(HL.Any, HL.Any)) << function(self, activityId, activity)
    if not activity then
        return nil, {}
    end
    local actives = self:_GatherActiveTabPushes(activityId, activity)
    if #actives == 0 then
        return nil, actives
    end
    local best = nil
    for _, c in ipairs(actives) do
        if not best or c.activationOrder < best.activationOrder then
            best = c
        end
    end
    return best and best.pushData or nil, actives
end













ActivityCenterCtrl._ResolveTabCellStateNames = HL.Method(HL.Opt(HL.Any, HL.Any)).Return(HL.String, HL.String) << function(self, tabPush, activity)
    if tabPush and tabPush.tabType == "End" and activity
        and not activity.isCompleted
        and not activity.placeAtBottom
    then
        local endTime = self:_GetServerActivityEndTime(tabPush, activity)
        if not endTime then
            if tabPush.isWeeklyRefresh then
                endTime = Utils.getNextWeeklyServerRefreshTime()
            else
                endTime = activity.endTime or 0
            end
        end
        if endTime and endTime > 0 and endTime > DateTimeUtils.GetCurrentTimestampBySeconds() then
            return TAB_END_NORMAL_STATE, TAB_END_SELECTED_STATE
        end
    end
    return DEFAULT_TAB_NORMAL_STATE, DEFAULT_TAB_SELECTED_STATE
end




ActivityCenterCtrl._RefreshTabUpdateNode = HL.Method(HL.Any, HL.Table) << function(self, cell, entry)
    if IsNull(cell) or not cell.normalNode or IsNull(cell.normalNode.updateNode) then
        return
    end
    local hasNew, hasUpdate = self:_GetTabIndicatorPriority(entry)
    cell.normalNode.updateNode.gameObject:SetActive(hasUpdate and not hasNew)
end






ActivityCenterCtrl._GetTabIndicatorPriority = HL.Method(HL.Table).Return(HL.Boolean, HL.Boolean, HL.Boolean, HL.Boolean) << function(self, entry)
    local activity = entry and entry.activity
    local activityData = entry and entry.activityData


    if not activity or not activityData or activity.isCompleted or activity.placeAtBottom then
        return false, false, false, false
    end
    local id = activityData.id


    local hasNew = ActivityUtils.isNewActivity(id) and true or false


    local hasNormal = false
    local redDotName = ActivityUtils.getActivityRedDotName(id)
    if not string.isEmpty(redDotName) then
        local active, rdType = RedDotManager:GetRedDotState(redDotName, id)
        if active and rdType == UIConst.RED_DOT_TYPE.Normal then
            hasNormal = true
        end
    end

    local tabPush = entry.tabPush



    local optimisticRead = self.m_optimisticReadPushIds
        and tabPush
        and tabPush.pushID
        and self.m_optimisticReadPushIds[tabPush.pushID]
    local conditionSatisfied = tabPush
        and not string.isEmpty(tabPush.pushID)
        and self:_IsConditionListSatisfied(tabPush.pushID, activity)
    local hasUpdate = (
        tabPush
        and tabPush.tabType == "Update"
        and not string.isEmpty(tabPush.pushID)
        and conditionSatisfied
        and not GameInstance.player.activitySystem:IsActivityPushRead(tabPush.pushID)
        and not optimisticRead
    ) and true or false



    local hasEndOnce = (
        tabPush
        and tabPush.tabType == "End"
        and not string.isEmpty(tabPush.pushID)
        and conditionSatisfied
        and not GameInstance.player.activitySystem:IsActivityPushRead(tabPush.pushID)
        and not optimisticRead
        and not ActivityUtils.isActivityEndTabRedDotSeen(tabPush.pushID)
    ) and true or false
    return hasNew, hasUpdate, hasNormal, hasEndOnce
end







ActivityCenterCtrl._RefreshTabPriorityIndicators = HL.Method(HL.Any, HL.Table) << function(self, cell, entry)
    if IsNull(cell) or not entry then
        return
    end
    local activityData = entry.activityData
    local activity = entry.activity
    if not activityData or not activity then
        return
    end

    local redDotName = ActivityUtils.getActivityRedDotName(activityData.id)
    local nodes = { cell.selectNode, cell.normalNode }
    for _, innerCell in ipairs(nodes) do
        if innerCell and innerCell.redDot then

            if not activity.isCompleted and not activity.placeAtBottom and not string.isEmpty(redDotName) then


                innerCell.redDot:InitRedDot(redDotName, activityData.id, function(rd)
                    self:_ApplyRedDotPriority(rd, entry)
                end, self.view.redDotScrollRect)
                self.view.redDotScrollRect.gameObject:SetActive(true)
                self:_ApplyRedDotPriority(innerCell.redDot, entry)
            else
                innerCell.redDot.gameObject:SetActive(false)
            end
        end
    end

    self:_RefreshTabUpdateNode(cell, entry)
end








ActivityCenterCtrl._ApplyRedDotPriority = HL.Method(HL.Any, HL.Table) << function(self, redDot, entry)
    if not redDot or IsNull(redDot.gameObject) then
        return
    end
    local hasNew, hasUpdate, hasNormal, hasEndOnce = self:_GetTabIndicatorPriority(entry)


    local showNew = hasNew
    local showNormal = (not hasNew) and (not hasUpdate) and (hasNormal or hasEndOnce)
    local active = showNew or showNormal

    if redDot.view.content and not IsNull(redDot.view.content.gameObject) then
        redDot.view.content.gameObject:SetActive(active)
    end
    if not active then
        return
    end
    if redDot.view.new and not IsNull(redDot.view.new.gameObject) then
        redDot.view.new.gameObject:SetActiveIfNecessary(showNew)
    end
    if redDot.view.normal and not IsNull(redDot.view.normal.gameObject) then
        redDot.view.normal.gameObject:SetActiveIfNecessary(showNormal)
    end
end






ActivityCenterCtrl._CollectTabBatchReadIds = HL.Method(HL.Any, HL.Table).Return(HL.Any) << function(self, currentTabPush, entry)
    local result = {}
    if not currentTabPush then
        return result
    end
    local activitySystem = GameInstance.player.activitySystem



    local optimisticRead = self.m_optimisticReadPushIds

    if not (optimisticRead and optimisticRead[currentTabPush.pushID]) then
        table.insert(result, currentTabPush.pushID)
    end
    local actives = entry and entry.tabPushAllActives
    if not actives then
        return result
    end
    local curOrder = self:_GetTabPushActivationOrder(currentTabPush, entry.activity)
    for _, info in ipairs(actives) do
        local p = info.pushData
        if p.pushID ~= currentTabPush.pushID
            and info.activationOrder > curOrder
            and not activitySystem:IsActivityPushRead(p.pushID)
            and not (optimisticRead and optimisticRead[p.pushID])
        then
            table.insert(result, p.pushID)
        end
    end
    return result
end

ActivityCenterCtrl._Close = HL.Method() << function(self)
    PhaseManager:PopPhase(PHASE_ID)
end

ActivityCenterCtrl.OnActivityUpdated = HL.Method(HL.Any) << function(self, arg)
    local id = unpack(arg)
    local activity = GameInstance.player.activitySystem:GetActivity(id)


    if not activity then
        ActivityUtils.backToMainHud(true, id)
        return
    end


    if not self.m_activityDict[id] then
        self:_RefreshTabList()
        return
    end


    local oldEntry = self.m_allActivities[self.m_activityDict[id].index]
    if activity.placeAtBottom ~= oldEntry.placeAtBottom or activity.status ~= oldEntry.status then
        self:_RefreshTabCompleteState()
        return
    end
end

ActivityCenterCtrl._RefreshTabCompleteState = HL.Method() << function(self)
    self.m_tabCells:Refresh(#self.m_allActivities, function(cell, index)
        local activityData = self.m_allActivities[index].activityData
        local activity = GameInstance.player.activitySystem:GetActivity(activityData.id)
        if activity then
            cell.selectNode.completedIconNode.gameObject:SetActive(activity.isCompleted or activity.placeAtBottom)
            cell.normalNode.completedIconNode.gameObject:SetActive(activity.isCompleted or activity.placeAtBottom)
        end
    end)
end

ActivityCenterCtrl._OnServerUnreadActivityPush = HL.Method() << function(self)
    self.m_optimisticReadPushIds = {}
    self:_RefreshTabList()
end

ActivityCenterCtrl._IsActivityChanged = HL.Method().Return(HL.Boolean) << function(self)
    local old = self.m_activityDict
    local new = {}
    local activities = GameInstance.player.activitySystem:GetAllActivities()
    for _, activity in cs_pairs(activities) do
        new[activity.id] = true
    end
    for key, _ in pairs(new) do
        if old[key] == nil then
            return true
        end
    end
    for key, _ in pairs(old) do
        if new[key] == nil then
            return true
        end
    end
    return false
end

ActivityCenterCtrl.OnPhaseRefresh = HL.Override(HL.Any) << function(self, arg)
    self:GoToActivity(arg and arg.gotoCenter and arg.activityId, true)
end

ActivityCenterCtrl._OnBackToTop = HL.Method() << function(self)

    if UIManager:IsInternalHidden(PANEL_ID) then
        return
    end
    self.view.animationWrapper:PlayInAnimation()
    for _,cell in pairs(self.m_cells) do
        cell.normalNode.animationWrapper:PlayInAnimation()
        cell.selectNode.animationWrapper:PlayInAnimation()
    end
end


HL.Commit(ActivityCenterCtrl)
