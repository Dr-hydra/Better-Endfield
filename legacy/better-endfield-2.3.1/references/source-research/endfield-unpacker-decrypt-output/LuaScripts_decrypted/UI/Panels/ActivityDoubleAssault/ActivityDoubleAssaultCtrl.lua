local uiCtrl = require_ex('UI/Panels/Base/UICtrl')


ActivityDoubleAssaultCtrl = HL.Class('ActivityDoubleAssaultCtrl', uiCtrl.UICtrl)






ActivityDoubleAssaultCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_ACTIVITY_UPDATED] = 'OnActivityUpdated',
    [MessageConst.ON_SC_MULTI_STAGE_ACTIVITY_GAIN_TASK_REWARD] = 'OnSync',
    [MessageConst.ON_SC_MULTI_STAGE_ACTIVITY_GAIN_REWARD] = 'OnStageRewardSync',
}

ActivityDoubleAssaultCtrl.m_activityId = HL.Field(HL.String) << ''

ActivityDoubleAssaultCtrl.m_refreshFunc = HL.Field(HL.Function)

ActivityDoubleAssaultCtrl.m_rewardList = HL.Field(HL.Table)

ActivityDoubleAssaultCtrl.m_genCell = HL.Field(HL.Any)

ActivityDoubleAssaultCtrl.m_rewardCellCacheList = HL.Field(HL.Table)


ActivityDoubleAssaultCtrl.m_missionInputGroupId = HL.Field(HL.Number) << -1

ActivityDoubleAssaultCtrl.m_receiveAllBindingId = HL.Field(HL.Number) << -1

ActivityDoubleAssaultCtrl._GetLastStageDungeonName = HL.Method().Return(HL.String) << function(self)
    local hasStageCfg, stageCfg = Tables.activityConditionalMultiStageTable:TryGetValue(self.m_activityId)
    if not hasStageCfg then
        logger.error("ActivityDoubleAssaultCtrl 找不到多阶段活动配置，活动id为：" .. self.m_activityId)
        return ""
    end

    local hasDungeonCfg, activityDungeonCfg = Tables.activityDungeonTable:TryGetValue(self.m_activityId)
    if not hasDungeonCfg then
        logger.error("ActivityDoubleAssaultCtrl 找不到活动副本配置，活动id为：" .. self.m_activityId)
        return ""
    end

    local activityData = GameInstance.player.activitySystem:GetActivity(self.m_activityId)
    if not activityData then
        logger.error("ActivityDoubleAssaultCtrl 找不到活动运行时数据，活动id为：" .. self.m_activityId)
        return ""
    end


    local now = DateTimeUtils.GetCurrentTimestampBySeconds()
    local lockedStateHash = GEnums.ActivityConditionalStageState.Locked:GetHashCode()

    local lastStageDungeonName = ""
    local maxSortId = -math.huge
    for dungeonId, _ in pairs(activityDungeonCfg.gameMap) do
        local isNormalDungeon = Tables.dungeonNormal2RaidTable:ContainsKey(dungeonId)
        if isNormalDungeon then
            local hasActivityDungeonStateCfg, activityDungeonStateCfg = Tables.activityDungeonState:TryGetValue(dungeonId)
            local hasDungeonData, dungeonData = Tables.dungeonTable:TryGetValue(dungeonId)
            if hasActivityDungeonStateCfg and hasDungeonData then
                local stageId = activityDungeonStateCfg.activityStage
                local hasStageInfo, stageInfo = stageCfg.stageList:TryGetValue(stageId)
                local hasStageData, stageData = activityData.stageDataDict:TryGetValue(stageId)
                local isStageUnlocked = hasStageData
                    and stageData.Status ~= lockedStateHash
                    and now >= stageData.OpenTimeTs
                if hasStageInfo and isStageUnlocked and stageInfo.sortId > maxSortId then
                    maxSortId = stageInfo.sortId
                    lastStageDungeonName = dungeonData.dungeonName
                end
            end
        end
    end
    return lastStageDungeonName
end


ActivityDoubleAssaultCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    self.m_activityId = args.activityId
    self.view.activityCommonInfo:InitActivityCommonInfo(args)
    self:_RefreshActivityCommonInfoRewardState()
    local lastStageDungeonName = self:_GetLastStageDungeonName()
    local hasUnlockedStage = not string.isEmpty(lastStageDungeonName)
    self.view.regionNode.gameObject:SetActiveIfNecessary(hasUnlockedStage)
    self.view.regionTxt.text = hasUnlockedStage and string.format(Language.LUA_ACTIVITY_DOUBLE_ASSAULT_REGION_TEXT, lastStageDungeonName) or ""

    local activityData = GameInstance.player.activitySystem:GetActivity(self.m_activityId)


    local taskMap = ActivityUtils.CreateGroupToTaskMap(self.m_activityId)









    self.m_rewardList = {}

    for _, taskList in pairs(taskMap) do
        for _, taskId in ipairs(taskList) do

            local config = Tables.activityConditionalMultiStageTaskConfigTable[self.m_activityId].TaskConfigMap[taskId]
            if #config.completeConditionId == 1 then
                local conditionConfig = Tables.activityConditionalMultiStageTaskCompleteConditionTable[config.completeConditionId[0]]
                local taskData = activityData:GetTaskData(taskId)
                local currentProgress = 0
                if taskData ~= nil then


                    if taskData.Status >= GEnums.ActivityConditionalTaskState.Completed:GetHashCode() then
                        currentProgress = conditionConfig.progressToCompare
                    elseif taskData.Conditions ~= nil then
                        local _, data = taskData.Conditions.Values:TryGetValue(conditionConfig.conditionId)
                        currentProgress = data or 0
                    end
                    table.insert(self.m_rewardList, {
                        sortId = config.sortId,
                        desc = conditionConfig.desc,
                        currentProgress = currentProgress,
                        maxProgress = conditionConfig.progressToCompare,
                        rewardId = config.rewardId,
                        taskData = taskData
                    })
                else
                    logger.error("ActivityDoubleAssaultCtrl 找到单条件任务，但没有找到任务数据，任务id为：" .. taskId)
                end

            else
                logger.error("ActivityDoubleAssaultCtrl 找到多条件任务，任务id为：" .. taskId)
            end

        end
    end
    table.sort(self.m_rewardList, function(a, b)
        return a.sortId < b.sortId
    end)
    self.m_genCell = UIUtils.genCellCache(self.view.doubleAssaultMissionCell1)
    self.m_rewardCellCacheList = {}

    self.m_refreshFunc = function(cell , luaIndex)
        local taskData = self.m_rewardList[luaIndex].taskData
        local taskState = GEnums.ActivityConditionalTaskState.__CastFrom(taskData.Status)
        cell.stateController:SetState(taskState:ToString())

        local succ, data = Tables.rewardTable:TryGetValue(self.m_rewardList[luaIndex].rewardId)

        if not succ then
            logger.error("找不到奖励配置，id为：" .. self.m_rewardList[luaIndex].rewardId)
            return
        end

        local isRewarded = taskState == GEnums.ActivityConditionalTaskState.Rewarded
        self.m_rewardCellCacheList[luaIndex] = self.m_rewardCellCacheList[luaIndex] or UIUtils.genCellCache(cell.rewardItem)
        self.m_rewardCellCacheList[luaIndex]:Refresh(data.itemBundles.Count, function(rewardCell, rewardLuaIndex)
            local itemInfo = data.itemBundles[CSIndex(rewardLuaIndex)]
            rewardCell:InitItem(itemInfo, function()
                UIUtils.showItemSideTips(rewardCell, UIConst.UI_TIPS_POS_TYPE.LeftTop, self.view.missionNode, function(phaseId)
                    if phaseId == PhaseId.ActivityCenter then
                        self:_SetItemTipBlockOthers(false)
                    end
                end)
            end)
            rewardCell:SetExtraInfo({ isSideTips = DeviceInfo.usingController })
            rewardCell.view.getNode.gameObject:SetActive(isRewarded)
        end)
        cell.numberTxt.text = string.format("%d/%d", self.m_rewardList[luaIndex].currentProgress, self.m_rewardList[luaIndex].maxProgress)
        cell.missionTxt.text = self.m_rewardList[luaIndex].desc
        cell.barImage.fillAmount = self.m_rewardList[luaIndex].maxProgress == 0 and 0 or self.m_rewardList[luaIndex].currentProgress / self.m_rewardList[luaIndex].maxProgress

        cell.clickBtn.onClick:RemoveAllListeners()
        if taskState == GEnums.ActivityConditionalTaskState.Completed then
            cell.clickBtn.onClick:AddListener(function()
                local completedTaskIds = {}
                for _, reward in ipairs(self.m_rewardList) do
                    if GEnums.ActivityConditionalTaskState.__CastFrom(reward.taskData.Status) == GEnums.ActivityConditionalTaskState.Completed then
                        table.insert(completedTaskIds, reward.taskData.Id)
                    end
                end
                if #completedTaskIds > 0 then
                    GameInstance.player.activitySystem:SendReceiveTaskRewardConditionMultiStage(self.m_activityId, completedTaskIds)
                end
            end)
        end

    end

    self.m_genCell:Refresh(#self.m_rewardList, self.m_refreshFunc)

    if DeviceInfo.usingController then
        local missionNaviGroup = self.view.missionNaviGroup
        local missionInputGroup = self.view.missionInputGroup
        if missionInputGroup ~= nil then
            self.m_missionInputGroupId = missionInputGroup.groupId
        end
        missionNaviGroup.onIsFocusedChange:AddListener(function(isFocused)



            self:_SetItemTipBlockOthers(isFocused)
            if not isFocused then
                Notify(MessageConst.HIDE_ITEM_TIPS)
            end
        end)

        self.m_receiveAllBindingId = self:BindInputPlayerAction("activity_stage_reward_receive_all", function()
            self:_ClaimAllCompletedRewards()
        end)
        self:_UpdateReceiveAllBinding()
    end





    self.view.activityCommonInfo.view.gotoNode.btnDetailRedDot:InitRedDot("ActivityDoubleAssaultDungeon", { activityId = self.m_activityId })
end

ActivityDoubleAssaultCtrl.OnSync = HL.Method(HL.Any) << function(self, args)
    local activityId = unpack(args)
    if activityId ~= self.m_activityId then
        return
    end
    self.m_genCell:Refresh(#self.m_rewardList, self.m_refreshFunc)
    self:_UpdateReceiveAllBinding()
    self:_RefreshActivityCommonInfoRewardState()
end

ActivityDoubleAssaultCtrl.OnStageRewardSync = HL.Method(HL.Any) << function(self, args)
    local activityId = unpack(args)
    if activityId ~= self.m_activityId then
        return
    end
    self:_RefreshActivityCommonInfoRewardState()
end

ActivityDoubleAssaultCtrl.OnActivityUpdated = HL.Method(HL.Any) << function(self, args)
    local activityId = unpack(args)
    if activityId ~= self.m_activityId then
        return
    end
    self:_StartTimer(0, function()
        self:_RefreshActivityCommonInfoRewardState()
    end)
end



ActivityDoubleAssaultCtrl.OnHide = HL.Override() << function(self)

    self:_SetItemTipBlockOthers(false)
end
ActivityDoubleAssaultCtrl.OnClose = HL.Override() << function(self)
    self:_SetItemTipBlockOthers(false)
end

ActivityDoubleAssaultCtrl._HasCompletedTasks = HL.Method().Return(HL.Boolean) << function(self)
    for _, reward in ipairs(self.m_rewardList) do
        if GEnums.ActivityConditionalTaskState.__CastFrom(reward.taskData.Status) == GEnums.ActivityConditionalTaskState.Completed then
            return true
        end
    end
    return false
end

ActivityDoubleAssaultCtrl._RefreshActivityCommonInfoRewardState = HL.Method() << function(self)
    local receiveAll = self:_HasReceivedAllDungeonRewards()
    local gotoNode = self.view.activityCommonInfo.view.gotoNode
    gotoNode.receiveAllNode.gameObject:SetActive(receiveAll)
    gotoNode.notReceiveAllNode.gameObject:SetActive(not receiveAll)
end

ActivityDoubleAssaultCtrl._HasReceivedAllDungeonRewards = HL.Method().Return(HL.Boolean) << function(self)
    local activityData = GameInstance.player.activitySystem:GetActivity(self.m_activityId)
    if not activityData then
        return false
    end

    local hasStageCfg, stageCfg = Tables.activityConditionalMultiStageTable:TryGetValue(self.m_activityId)
    local hasDungeonCfg, activityDungeonCfg = Tables.activityDungeonTable:TryGetValue(self.m_activityId)
    if not hasStageCfg or not hasDungeonCfg then
        return false
    end

    local rewardedHash = GEnums.ActivityConditionalStageState.Rewarded:GetHashCode()
    local completedHash = GEnums.ActivityConditionalStageState.Completed:GetHashCode()
    local hasDungeonReward = false
    for dungeonId, _ in pairs(activityDungeonCfg.gameMap) do
        local normalDungeonId = tostring(dungeonId)
        if Tables.dungeonNormal2RaidTable:ContainsKey(normalDungeonId) then
            local hasActivityDungeonStateCfg, activityDungeonStateCfg = Tables.activityDungeonState:TryGetValue(normalDungeonId)
            if not hasActivityDungeonStateCfg then
                return false
            end

            local stageId = activityDungeonStateCfg.activityStage
            local hasStageData, stageData = activityData.stageDataDict:TryGetValue(stageId)
            local stageInfo = stageCfg.stageList[stageId]
            if not hasStageData or not stageInfo then
                return false
            end

            local isStageRewarded = stageData.Status == rewardedHash

            local treatCompletedAsRewarded = stageData.Status == completedHash and string.isEmpty(stageInfo.rewardId)
            if not isStageRewarded and not treatCompletedAsRewarded then
                return false
            end
            hasDungeonReward = true
        end
    end
    return hasDungeonReward
end

ActivityDoubleAssaultCtrl._ClaimAllCompletedRewards = HL.Method() << function(self)
    local completedTaskIds = {}
    for _, reward in ipairs(self.m_rewardList) do
        if GEnums.ActivityConditionalTaskState.__CastFrom(reward.taskData.Status) == GEnums.ActivityConditionalTaskState.Completed then
            table.insert(completedTaskIds, reward.taskData.Id)
        end
    end
    if #completedTaskIds > 0 then
        GameInstance.player.activitySystem:SendReceiveTaskRewardConditionMultiStage(self.m_activityId, completedTaskIds)
    end
end

ActivityDoubleAssaultCtrl._UpdateReceiveAllBinding = HL.Method() << function(self)
    if self.m_receiveAllBindingId < 0 then
        return
    end
    InputManagerInst:ToggleBinding(self.m_receiveAllBindingId, self:_HasCompletedTasks())
end

ActivityDoubleAssaultCtrl._SetItemTipBlockOthers = HL.Method(HL.Boolean) << function(self, block)
    if not self.view or self.m_missionInputGroupId <= 0 then
        return
    end
    if block then


        InputManagerInst:IgnoreBindingGroupParent(self.m_missionInputGroupId, true)
        self.view.inputGroup.enabled = false

        if NotNull(self.view.promptBox) then
            self.view.promptBox.gameObject:SetActive(false)
        end



        Notify(MessageConst.SHOW_AS_CONTROLLER_SMALL_MENU, {
            panelId = self.panelId,
            isGroup = true,
            id = self.m_missionInputGroupId,
            rectTransform = self.view.missionNode,
            useNormalFrame = true,
            noHighlight = true,
        })
    else


        Notify(MessageConst.CLOSE_CONTROLLER_SMALL_MENU, self.m_missionInputGroupId)
        if NotNull(self.view.promptBox) then
            self.view.promptBox.gameObject:SetActive(true)
        end
        self.view.inputGroup.enabled = true
        InputManagerInst:IgnoreBindingGroupParent(self.m_missionInputGroupId, false)
    end
end

HL.Commit(ActivityDoubleAssaultCtrl)
