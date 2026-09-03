local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ActivityCoinReward
local PHASE_ID = PhaseId.ActivityCoinReward

ActivityCoinRewardCtrl = HL.Class('ActivityCoinRewardCtrl', uiCtrl.UICtrl)

ActivityCoinRewardCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_CONDITIONAL_MULTI_STAGE_TASK_PROGRESS_CHANGE] = '_OnActivityDataChange',
    [MessageConst.ON_CONDITIONAL_MULTI_STAGE_UPDATE] = '_OnActivityDataChange',
    [MessageConst.ON_ACTIVITY_RACING_DUNGEON_MILESTONE_CHANGED] = '_OnActivityDataChange',
}


ActivityCoinRewardCtrl.m_activityId = HL.Field(HL.String) << ''

ActivityCoinRewardCtrl.m_activityData = HL.Field(HL.Any)


ActivityCoinRewardCtrl.m_openInDungeon = HL.Field(HL.Boolean) << false


ActivityCoinRewardCtrl.m_tasks = HL.Field(HL.Table)


ActivityCoinRewardCtrl.m_getCellFunc = HL.Field(HL.Function)


ActivityCoinRewardCtrl.m_countdownCor = HL.Field(HL.Any)


ActivityCoinRewardCtrl.m_readTasks = HL.Field(HL.Table)


ActivityCoinRewardCtrl.m_needNaviToFirst = HL.Field(HL.Boolean) << false


ActivityCoinRewardCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_activityId = arg.activityId
    self.m_openInDungeon = arg.openInDungeon == true
    self.m_readTasks = {}
    self.m_needNaviToFirst = true

    self:_InitData()
    self:_BindUI()
    self:_RefreshUI()
end




ActivityCoinRewardCtrl._BindUI = HL.Method() << function(self)
    self.view.commonTopTitleNode.btnClose.onClick:AddListener(function()
        PhaseManager:PopPhase(PHASE_ID)
    end)

    self.m_getCellFunc = UIUtils.genCachedCellFunction(self.view.taskScroll)
    self.view.taskScroll.onUpdateCell:RemoveAllListeners()
    self.view.taskScroll.onUpdateCell:AddListener(function(object, csIndex)
        self:_OnUpdateCell(object, LuaIndex(csIndex))
    end)

    self.view.allReceiveBtn.onClick:RemoveAllListeners()
    self.view.allReceiveBtn.onClick:AddListener(function()
        local waitToReceiveTaskIds = {}
        for _, taskInfo in ipairs(self.m_tasks) do
            if taskInfo.status == GEnums.ActivityConditionalTaskState.Completed then
                table.insert(waitToReceiveTaskIds, taskInfo.taskId)
            end
        end
        if #waitToReceiveTaskIds > 0 then
            GameInstance.player.activitySystem:SendReceiveTaskRewardConditionMultiStage(self.m_activityId, waitToReceiveTaskIds)
        end
    end)

    local _, achievementData = Tables.activityAchievementDataTable:TryGetValue(self.m_activityId)
    if achievementData ~= nil then
        self.view.dungeonMedalCell:InitCommonMedalNode(achievementData.achievementId)
    end

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
end


ActivityCoinRewardCtrl._InitData = HL.Method() << function(self)
    self.m_activityData = GameInstance.player.activitySystem:GetActivity(self.m_activityId)

    local allTaskInfos = ActivityUtils.GetTaskInfos(self.m_activityId)

    for _, info in ipairs(allTaskInfos) do
        info.isNew = self:_IsNewTask(info)
    end

    self.m_tasks = {}
    for _, info in ipairs(allTaskInfos) do
        if info.status ~= GEnums.ActivityConditionalTaskState.Locked then
            table.insert(self.m_tasks, info)
        end
    end

    for _, info in ipairs(self.m_tasks) do
        if info.status == GEnums.ActivityConditionalTaskState.Completed then
            info._statusSort = 0
        elseif info.status == GEnums.ActivityConditionalTaskState.Unlocked then
            info._statusSort = info.isNew and 1 or 2
        else
            info._statusSort = 3
        end
    end
    table.sort(self.m_tasks, Utils.genSortFunction({ "_statusSort", "sortId" }, true))
end


ActivityCoinRewardCtrl._RefreshUI = HL.Method() << function(self)
    self.view.warningNode.gameObject:SetActive(self.m_openInDungeon)

    self.view.taskScroll:UpdateCount(#self.m_tasks)

    local hasCompletedTask = false
    for _, taskInfo in ipairs(self.m_tasks) do
        if taskInfo.status == GEnums.ActivityConditionalTaskState.Completed then
            hasCompletedTask = true
            break
        end
    end
    self.view.allReceiveBtn.gameObject:SetActive(not self.m_openInDungeon and hasCompletedTask)
    self.view.disBtn.gameObject:SetActive(not self.m_openInDungeon and not hasCompletedTask)

    self:_RefreshScore()
    self:_RefreshReward()
    self:_RefreshCountdown()
end


ActivityCoinRewardCtrl._OnUpdateCell = HL.Method(HL.Userdata, HL.Number) << function(self, object, index)
    local cell = self.m_getCellFunc(object)
    local taskInfo = self.m_tasks[index]
    if not taskInfo then
        return
    end

    cell.taskTxt.text = taskInfo.desc

    cell.progressTxt.text = string.format("%d/%d", taskInfo.progress, taskInfo.target)
    cell.progressImg.fillAmount = taskInfo.target > 0 and taskInfo.progress / taskInfo.target or 1

    local rewardItems = UIUtils.getRewardItems(taskInfo.rewardId)
    if #rewardItems >= 1 then
        if #rewardItems > 1 then
            logger.error("ActivityCoinReward: task " .. taskInfo.taskId .. " has more than 1 reward item, only showing first")
        end
        cell.itemReward:InitItem(rewardItems[1], true)
    end

    local state = "Nrl"
    if taskInfo.status == GEnums.ActivityConditionalTaskState.Completed then
        if self.m_openInDungeon then
            state = "ReceiveInDungeon"
        else
            state = "Receive"
        end
    elseif taskInfo.status == GEnums.ActivityConditionalTaskState.Rewarded then
        state = "Done"
    end
    cell.stateController:SetState(state)

    cell.receiveBtn.onClick:RemoveAllListeners()
    if taskInfo.status == GEnums.ActivityConditionalTaskState.Completed then
        cell.receiveBtn.onClick:AddListener(function()
            GameInstance.player.activitySystem:SendReceiveTaskRewardConditionMultiStage(self.m_activityId, { taskInfo.taskId })
        end)
    end

    local redDotArgs = {
        activityId = self.m_activityId,
        taskId = taskInfo.taskId,
    }
    cell.redDot:InitRedDot("ActivityRacingDungeonSingleTask", redDotArgs)

    self.m_readTasks[taskInfo.taskId] = true

    if index == 1 and self.m_needNaviToFirst then
        self.m_needNaviToFirst = false
        self:SetNaviTarget(cell.inputBindingGroupNaviDecorator)
    end
end


ActivityCoinRewardCtrl._RefreshScore = HL.Method() << function(self)
    local currScore = ActivityUtils.GetRacingDungeonMilestoneCurrScore(self.m_activityId)
    local maxScore = ActivityUtils.GetRacingDungeonMilestoneMaxScore(self.m_activityId)
    self.view.rewardInfoNode.topNode.integralTxt.text = string.format("%d/%d", currScore, maxScore)
    self.view.rewardInfoNode.topNode.progressImg.fillAmount = maxScore > 0 and currScore / maxScore or 0
end


ActivityCoinRewardCtrl._RefreshReward = HL.Method() << function(self)
    local _, cfg = Tables.activityRacingDungeonTable:TryGetValue(self.m_activityId)
    if cfg == nil then
        return
    end

    local itemId = cfg.headItemId
    local itemData = Tables.itemTable:GetValue(itemId)
    local bgReward = self.view.rewardInfoNode.bgReward

    bgReward.awardTxt.text = itemData.name

    if itemData.type == GEnums.ItemType.UserAvatar then
        local avatarIcon = UIUtils.getAvatarIconByItemId(itemId)
        if avatarIcon then
            bgReward.commonPlayerHead:UpdateHideLevelTxt(true)
            bgReward.commonPlayerHead:UpdateHideSignature(true)
            bgReward.commonPlayerHead:InitCommonPlayerHead(avatarIcon, "", false)
        end
    end

    local rewardTask = nil
    for _, taskInfo in ipairs(self.m_tasks) do
        local rewardItems = UIUtils.getRewardItems(taskInfo.rewardId)
        for _, rewardItem in ipairs(rewardItems) do
            if rewardItem.id == itemId then
                rewardTask = taskInfo
                break
            end
        end
        if rewardTask ~= nil then
            break
        end
    end

    if rewardTask == nil then
        logger.info("ActivityCoinReward: no visible task reward contains head item " .. itemId .. ", activityId: " .. self.m_activityId)
    end
    self.view.rewardInfoNode.stateController:SetState(rewardTask ~= nil and rewardTask.status == GEnums.ActivityConditionalTaskState.Rewarded and "Done" or "Nrl")
end


ActivityCoinRewardCtrl._RefreshCountdown = HL.Method() << function(self)
    if self.m_countdownCor then
        self:_ClearCoroutine(self.m_countdownCor)
        self.m_countdownCor = nil
    end

    local haveCfg, multiStageCfg = Tables.activityConditionalMultiStageTable:TryGetValue(self.m_activityId)
    if not haveCfg then
        self.view.countdownNode.gameObject:SetActive(false)
        return
    end

    local activityData = GameInstance.player.activitySystem:GetActivity(self.m_activityId)
    local stages = {}
    for stageId, stageCfg in pairs(multiStageCfg.stageList) do
        table.insert(stages, { stageId = stageId, cfg = stageCfg })
    end
    table.sort(stages, function(a, b) return a.cfg.sortId < b.cfg.sortId end)

    local lockedStage = nil
    for _, stage in ipairs(stages) do
        local _, stageData = activityData.stageDataDict:TryGetValue(stage.stageId)
        local status = stageData and GEnums.ActivityConditionalStageState.__CastFrom(stageData.Status)
        if status == GEnums.ActivityConditionalStageState.Locked then
            lockedStage = stageData
            break
        end
    end

    if not lockedStage then
        self.view.countdownNode.gameObject:SetActive(false)
        return
    end

    self.view.countdownNode.gameObject:SetActive(true)
    local openTime = lockedStage.OpenTimeTs

    self.m_countdownCor = self:_StartCoroutine(function()
        while true do
            local remaining = openTime - DateTimeUtils.GetCurrentTimestampBySeconds()
            if remaining <= 0 then
                self.view.countdownNode.gameObject:SetActive(false)
                break
            end
            self.view.countdownNode.timeText.text = string.format(Language.LUA_ACTIVITY_RACING_DUNGEON_REWARD_STAGE_TWO_COUNTDOWN_TEXT, UIUtils.getLeftTime(remaining))
            coroutine.wait(1)
        end
    end)
end

ActivityCoinRewardCtrl._IsNewTask = HL.Method(HL.Table).Return(HL.Boolean) << function(self, taskInfo)
    return ActivityUtils.isNewTask(self.m_activityId, taskInfo.taskId)
end

ActivityCoinRewardCtrl.OnClose = HL.Override() << function(self)
    self:_UpdateReadInfo()
end

ActivityCoinRewardCtrl._UpdateReadInfo = HL.Method() << function(self)
    for taskId, _ in pairs(self.m_readTasks) do
        ActivityUtils.setTaskRead(self.m_activityId, taskId)
    end
end

ActivityCoinRewardCtrl._OnActivityDataChange = HL.Method(HL.Any) << function(self, arg)
    local activityId = unpack(arg)
    if activityId ~= self.m_activityId then
        return
    end
    self:_InitData()
    self:_RefreshUI()
end



HL.Commit(ActivityCoinRewardCtrl)
