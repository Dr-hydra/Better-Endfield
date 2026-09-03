local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ActivityStaminaDiscount

ActivityStaminaDiscountCtrl = HL.Class('ActivityStaminaDiscountCtrl', uiCtrl.UICtrl)






ActivityStaminaDiscountCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_CONDITIONAL_MULTI_STAGE_UPDATE] = 'OnStageChange',
    [MessageConst.ON_CONDITIONAL_MULTI_STAGE_PROGRESS_CHANGE] = 'OnStageChange',
    [MessageConst.ON_ACTIVITY_UPDATED] = 'OnStageChange',
}

ActivityStaminaDiscountCtrl.m_activityId = HL.Field(HL.String) << ''

ActivityStaminaDiscountCtrl.m_activity = HL.Field(HL.Any)

ActivityStaminaDiscountCtrl.m_tasks = HL.Field(HL.Table)

ActivityStaminaDiscountCtrl.m_getRewardCell = HL.Field(HL.Function)

ActivityStaminaDiscountCtrl.m_canReward = HL.Field(HL.Boolean) << false

ActivityStaminaDiscountCtrl.m_rewardStageIds = HL.Field(HL.Table)

ActivityStaminaDiscountCtrl.m_receiveAllBindingId = HL.Field(HL.Number) << -1

ActivityStaminaDiscountCtrl.m_refreshDirty = HL.Field(HL.Boolean) << false

ActivityStaminaDiscountCtrl.OnBeforePanelActive = HL.Method(HL.Any) << function(self, args)
    self:_ApplyAudioOnOpen(args.activityId)
end

ActivityStaminaDiscountCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    self.m_activityId = args.activityId

    self:_ApplyAudioOnOpen(self.m_activityId)
    if Tables.activityStaminaRefundBgStateTable then
        local has, bgStateData = Tables.activityStaminaRefundBgStateTable:TryGetValue(self.m_activityId)
        if has and not string.isEmpty(bgStateData.bgStateName) then
            self.view.bgLayout:SetState(bgStateData.bgStateName)
        end
    end


    self.m_receiveAllBindingId = self:BindInputPlayerAction("activity_stamina_receive_all", function()
        if self.m_canReward then
            GameInstance.player.activitySystem:SendReceiveRewardConditionMultiStage(self.m_activityId, self.m_rewardStageIds)
        end
    end)


    self.view.activityCommonInfo:InitActivityCommonInfo(args)


    self.m_getRewardCell = UIUtils.genCachedCellFunction(self.view.rewardList)
    self.view.rewardList.onUpdateCell:AddListener(function(obj, csIndex)
        self:_OnUpdateCell(self.m_getRewardCell(obj), LuaIndex(csIndex))
    end)


    local viewBindingId = self:BindInputPlayerAction("common_view_item", function()
        self:_SetNaviTarget(1)
    end)

    self.view.rightNaviGroup.onIsTopLayerChanged:AddListener(function(active)
        InputManagerInst:ToggleBinding(viewBindingId, not active)
    end)
    self:_RefreshInfo()
end

ActivityStaminaDiscountCtrl._ApplyAudioOnOpen = HL.Method(HL.String) << function(self, activityId)
    local audioOnOpen = self:_GetAudioOnOpen(activityId)
    if not string.isEmpty(audioOnOpen) and self.animationWrapper then
        self.animationWrapper:SetAudioOnOpen(audioOnOpen)
    end
end

ActivityStaminaDiscountCtrl._GetAudioOnOpen = HL.Method(HL.String).Return(HL.String) << function(self, activityId)
    if Tables.activityStaminaRefundBgStateTable then
        local has, bgStateData = Tables.activityStaminaRefundBgStateTable:TryGetValue(activityId)
        if has then
            return bgStateData and bgStateData.audioOnOpen or ""
        end
    end
    return ""
end

ActivityStaminaDiscountCtrl.OnClose = HL.Override() << function(self)





    ActivityUtils.setActivityDayAsRead(self.m_activityId)
end

ActivityStaminaDiscountCtrl._RefreshInfo = HL.Method() << function(self)
    self.m_activity = GameInstance.player.activitySystem:GetActivity(self.m_activityId)
    self.m_tasks = {}
    self.m_rewardStageIds = {}
    self.m_canReward = false

    if not self.m_activity then
        return
    end


    local nextRefreshTime = Utils.getNextCommonServerRefreshTime()
    local isLastDay = self.m_activity.endTime > 0 and self.m_activity.endTime <= nextRefreshTime


    if isLastDay then
        self.view.timeNode.gameObject:SetActive(false)
    else
        self.view.countDownText.gameObject:SetActive(true)
        self.view.countDownText:InitCountDownText(Utils.getNextCommonServerRefreshTime(), nil, function(leftSec)
            return string.format(Language["LUA_WEEK_RAID_MAIN_BATTLE_PASS_REFRESH_TIPS"], UIUtils.getLeftTime(leftSec))
        end)
    end

    local hasMultiStageCfg, multiStageCfg = Tables.activityConditionalMultiStageTable:TryGetValue(self.m_activityId)
    if hasMultiStageCfg and multiStageCfg and multiStageCfg.stageList then
        for stageId, stageInfo in pairs(multiStageCfg.stageList) do
            local total = Tables.activityConditionalMultiStageCompleteConditionTable[stageId].conditionList[0].progressToCompare

            local curProgress = 0
            local isComplete = false
            local isReceived = false

            local suc, stageData = self.m_activity.stageDataDict:TryGetValue(stageId)

            if suc then
                if stageData.Conditions then
                    for _, info in pairs(stageData.Conditions.Values) do
                        curProgress = info
                    end
                else
                    curProgress = total
                end
                isComplete = stageData.Status >= GEnums.ActivityConditionalStageState.Completed:GetHashCode()
                isReceived = stageData.Status >= GEnums.ActivityConditionalStageState.Rewarded:GetHashCode()
            end

            local task = {
                stageId = stageId,
                desc = stageInfo.desc,
                sortId = stageInfo.sortId,
                rewardId = stageInfo.rewardId,
                mapJumpId = stageInfo.jumpId,
                isComplete = isComplete,
                isReceived = isReceived,
                curProgress = curProgress,
                total = total,
            }
            task.statusSortId = task.isReceived and 3 or (task.isComplete and 1 or 2)

            if task.isComplete and not task.isReceived then
                self.m_canReward = true
                table.insert(self.m_rewardStageIds, task.stageId)
            end

            table.insert(self.m_tasks, task)
        end
    else
        logger.warn("ActivityStaminaDiscountCtrl._RefreshInfo: multi stage cfg not found", self.m_activityId)
    end

    table.sort(self.m_tasks, Utils.genSortFunction({"statusSortId", "sortId"}, true))

    if self.m_getRewardCell then
        self.view.rewardList:UpdateCount(#self.m_tasks)
    end

    if self.m_receiveAllBindingId >= 0 then
        InputManagerInst:ToggleBinding(self.m_receiveAllBindingId, self.m_canReward)
    end
end

ActivityStaminaDiscountCtrl._OnUpdateRewardCell = HL.Method(HL.Table, HL.Any, HL.Number) << function(self, taskCell, rewardCell, rewardIndex)
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
        tipsPosTransform = self.view.rewardList.transform,
        isSideTips = true,
    })
    rewardCell.view.rewardedCover.gameObject:SetActive(taskCell.m_isReceived)
    rewardCell.view.selectedBG.gameObject:SetActive(false)
    rewardCell.view.button.onIsNaviTargetChanged = function(isTarget)
        rewardCell.view.selectedBG.gameObject:SetActive(isTarget)
    end
end

ActivityStaminaDiscountCtrl._OnUpdateCell = HL.Method(HL.Table, HL.Number) << function(self, cell, index)
    local task = self.m_tasks[index]
    if not task then
        return
    end
    local isComplete = task.isComplete
    local isReceived = task.isReceived
    cell.descTxt.text = task.desc
    cell.progressTxt.text = task.curProgress .. "/" .. task.total
    cell.scrollbar.size = lume.clamp(task.curProgress/task.total, 0, 1)
    cell.redDot:InitRedDot("ActivityStaminaDiscountTask", {self.m_activityId, task.stageId})
    cell.m_isReceived = isReceived
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


    local state = isReceived and "Received" or (isComplete and "Completed" or "NotCompleted")
    cell.nodeState:SetState(state)


    cell.completeBtn.onClick:RemoveAllListeners()
    if isComplete and not isReceived then
        cell.completeBtn.onClick:AddListener(function()
            GameInstance.player.activitySystem:SendReceiveRewardConditionMultiStage(self.m_activityId, self.m_rewardStageIds)
        end)
    end


    cell.notCompleteBtn.onClick:RemoveAllListeners()
    cell.notCompleteBtn.onClick:AddListener(function()
        ActivityUtils.setActivityDayAsRead(self.m_activityId)
        Utils.jumpToSystem(task.mapJumpId)
    end)
end

ActivityStaminaDiscountCtrl._SetNaviTarget = HL.Method(HL.Number) << function(self, index)
    if index == 0 or not DeviceInfo.usingController then
        return
    end
    local oriCell = self.view.rewardList:Get(CSIndex(index))
    if not oriCell then
        self.view.rewardList:ScrollToIndex(index, true)
        oriCell = self.view.rewardList:Get(CSIndex(index))
    end
    local cell = oriCell and self.m_getRewardCell(oriCell)
    if cell then
        self:SetNaviTarget(cell.naviDecorator)
    end
end

ActivityStaminaDiscountCtrl.OnActivityCenterNaviFailed = HL.Method() << function(self)
    local firstCell = self.view.rewardList:GetRangeInView().x
    self:_SetNaviTarget(LuaIndex(firstCell))
end

ActivityStaminaDiscountCtrl.OnStageChange = HL.Method(HL.Any) << function(self, args)
    local id = unpack(args)
    if id ~= self.m_activityId then
        return
    end
    if self.m_refreshDirty then
        return
    end
    self.m_refreshDirty = true
    TimerManager:StartFrameTimer(1, function()
        self.m_refreshDirty = false
        self:_RefreshInfo()
    end)
end

HL.Commit(ActivityStaminaDiscountCtrl)
