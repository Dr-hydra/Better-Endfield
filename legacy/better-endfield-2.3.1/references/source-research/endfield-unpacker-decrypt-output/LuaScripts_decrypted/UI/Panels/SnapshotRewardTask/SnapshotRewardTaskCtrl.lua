local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.SnapshotRewardTask


local missionSystem = GameInstance.player.mission


local snapshotSystem = GameInstance.player.snapshotSystem

SnapshotRewardTaskCtrl = HL.Class('SnapshotRewardTaskCtrl', uiCtrl.UICtrl)





SnapshotRewardTaskCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_MISSION_STATE_CHANGE] = '_OnMissionStateChange',
    [MessageConst.ON_QUEST_STATE_CHANGE] = '_OnQuestStateChange',
    [MessageConst.ON_QUEST_OBJECTIVE_UPDATE] = '_OnQuestObjectiveUpdate',
    [MessageConst.ON_SNAPSHOT_REWARD_TASK_REWARDED] = '_OnRewardTaskRewarded',
}



SnapshotRewardTaskCtrl.m_rewardTaskId = HL.Field(HL.String) << ""
SnapshotRewardTaskCtrl.m_actionIsStatic = HL.Field(HL.Boolean) << false
SnapshotRewardTaskCtrl.m_characterId = HL.Field(HL.String) << ""
SnapshotRewardTaskCtrl.m_itemId = HL.Field(HL.String) << ""
SnapshotRewardTaskCtrl.m_isRewarded = HL.Field(HL.Boolean) << false
SnapshotRewardTaskCtrl.m_taskInfos = HL.Field(HL.Table)
SnapshotRewardTaskCtrl.m_getTaskCellFunc = HL.Field(HL.Function)
SnapshotRewardTaskCtrl.m_titleLanguageKey = HL.Field(HL.String) << ""






SnapshotRewardTaskCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:_InitData(arg)
    self:_InitUI()
    self:_RefreshAllUI()
end

SnapshotRewardTaskCtrl.OnShow = HL.Override() << function(self)
    self:SetNaviTarget(self.view.taskOverviewNaviDeco)
end


SnapshotRewardTaskCtrl.OnPhaseRefresh = HL.Override(HL.Any) << function(self, arg)
    self:_RefreshTaskProgress()
end




SnapshotRewardTaskCtrl._InitData = HL.Method(HL.Any) << function(self, arg)
    arg = arg or {}
    self.m_rewardTaskId = arg.rewardTaskId or ""
    self.m_actionIsStatic = arg.actionIsStatic == true
    self.m_characterId = arg.characterId or ""
    self.m_titleLanguageKey = arg.titleLanguageKey
    self.m_taskInfos = {}

    if string.isEmpty(self.m_rewardTaskId) then
        return
    end

    local hasTaskCfg, taskCfg = Tables.snapshotRewardTaskTable:TryGetValue(self.m_rewardTaskId)
    if not hasTaskCfg or taskCfg == nil then
        return
    end

    self.m_itemId = taskCfg.itemId or ""
    self.m_isRewarded = snapshotSystem:IsTaskRewarded(self.m_rewardTaskId)


    for i = 0, taskCfg.conditionIdList.Count - 1 do
        local conditionId = taskCfg.conditionIdList[i]
        local condCfg = Tables.snapshotRewardTaskConditionTable:GetValue(conditionId)
        local isComplete = false
        local missionId = ""
        local parameter1 = ""
        if condCfg.parameter1 and condCfg.parameter1.valueStringList
            and condCfg.parameter1.valueStringList.Count > 0 then
            parameter1 = condCfg.parameter1.valueStringList[0]
        end
        if condCfg.conditionType == GEnums.ConditionType.MissionStateEqual then
            missionId = parameter1
            isComplete = missionSystem:GetMissionState(parameter1) ==
                CS.Beyond.Gameplay.MissionSystem.MissionState.Completed
        else
            missionId = missionSystem:GetMissionIdByQuestId(parameter1)
            isComplete = not string.isEmpty(missionId) and
                missionSystem:GetMissionState(missionId) ==
                CS.Beyond.Gameplay.MissionSystem.MissionState.Completed
        end
        table.insert(self.m_taskInfos, {
            title = condCfg.taskTitle,
            isComplete = isComplete,
            missionId = missionId,
        })
    end
end


SnapshotRewardTaskCtrl._InitUI = HL.Method() << function(self)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })

    self.view.closeBtn.onClick:RemoveAllListeners()
    self.view.closeBtn.onClick:AddListener(function()
        self:PlayAnimationOutAndClose()
    end)

    self.view.tipsBtn.onClick:RemoveAllListeners()
    self.view.tipsBtn.onClick:AddListener(function()
        if string.isEmpty(self.m_itemId) then
            return
        end
        Notify(MessageConst.SHOW_ITEM_TIPS, {
            transform = self.view.rewardImg.transform,
            itemId = self.m_itemId,
        })
    end)

    self.view.getRewardBtn.onClick:RemoveAllListeners()
    self.view.getRewardBtn.onClick:AddListener(function()
        self:_OnClickGetRewardBtn()
    end)

    self.m_getTaskCellFunc = UIUtils.genCachedCellFunction(self.view.taskScrollList)
    self.view.taskScrollList.onUpdateCell:RemoveAllListeners()
    self.view.taskScrollList.onUpdateCell:AddListener(function(obj, csIndex)
        self:_RefreshTaskCell(self.m_getTaskCellFunc(obj), LuaIndex(csIndex))
    end)
end

SnapshotRewardTaskCtrl._OnClickGetRewardBtn = HL.Method() << function(self)
    snapshotSystem:SendGainReward(self.m_rewardTaskId)
end


SnapshotRewardTaskCtrl._UpdateTaskInfos = HL.Method() << function(self)
    for _, info in ipairs(self.m_taskInfos) do
        info.isComplete = not string.isEmpty(info.missionId)
            and missionSystem:GetMissionState(info.missionId) ==
            CS.Beyond.Gameplay.MissionSystem.MissionState.Completed
    end
end


SnapshotRewardTaskCtrl._GetCompletedCount = HL.Method().Return(HL.Number) << function(self)
    local completedCount = 0
    for _, info in ipairs(self.m_taskInfos) do
        if info.isComplete then
            completedCount = completedCount + 1
        end
    end
    return completedCount
end


SnapshotRewardTaskCtrl._IsAllComplete = HL.Method().Return(HL.Boolean) << function(self)
    return self:_GetCompletedCount() >= #self.m_taskInfos
end


SnapshotRewardTaskCtrl._GetOverviewState = HL.Method().Return(HL.String) << function(self)
    if self.m_isRewarded then
        return "Rewarded"
    elseif self:_IsAllComplete() then
        return "CanGetReward"
    else
        return "InProgress"
    end
end


SnapshotRewardTaskCtrl._RefreshAllUI = HL.Method() << function(self)
    if not string.isEmpty(self.m_titleLanguageKey) then
        self.view.titleTxt.text = Language[self.m_titleLanguageKey]
    end
    self:_RefreshRewardUI()
    self:_RefreshTaskList()
    self:_RefreshOverview()
end


SnapshotRewardTaskCtrl._RefreshRewardUI = HL.Method() << function(self)
    local hasItemCfg, itemCfg = Tables.itemTable:TryGetValue(self.m_itemId)
    if hasItemCfg and itemCfg ~= nil then
        self.view.rewardImg:LoadSprite(UIConst.UI_SPRITE_ITEM_BIG, itemCfg.iconId)
        self.view.rewardNameTxt.text = itemCfg.name or ""
        self.view.rewardDescTxt.text = itemCfg.desc or ""
        self.view.itemRarityImg.color = UIUtils.getItemRarityColor(itemCfg.rarity)
    else
        self.view.rewardNameTxt.text = ""
        self.view.rewardDescTxt.text = ""
    end

    local isExclusive = not string.isEmpty(self.m_characterId)
        and self.m_characterId ~= "common"
    self.view.avatarNode.gameObject:SetActive(isExclusive)
    self.view.spRewardDescTxt.gameObject:SetActive(isExclusive)
    if isExclusive then
        local charCfg = Tables.characterTable:GetValue(self.m_characterId)
        self.view.avatarNode.avatarImg:LoadSprite(
            UIConst.UI_SPRITE_ROUND_CHAR_HEAD,
            UIConst.UI_ROUND_CHAR_HEAD_PREFIX .. self.m_characterId)
        self.view.spRewardDescTxt.text = string.format(
            Language.LUA_SNAPSHOT_REWARD_SP_ACTION_DESC,
            charCfg.name)
    end

    local showPlayIcon = self.m_actionIsStatic
    self.view.actionPlayIcon.gameObject:SetActive(showPlayIcon)
    self.view.tipsBtn.gameObject:SetActive(not string.isEmpty(self.m_itemId))
end


SnapshotRewardTaskCtrl._RefreshTaskList = HL.Method() << function(self)
    self.view.taskScrollList:UpdateCount(#self.m_taskInfos, true)
end


SnapshotRewardTaskCtrl._RefreshTaskCell = HL.Method(HL.Any, HL.Number) << function(self, cell, luaIndex)
    local info = self.m_taskInfos[luaIndex]
    if not info then
        return
    end

    cell.titleTxt.text = info.title or ""
    cell.goToBtn.onClick:RemoveAllListeners()

    if info.isComplete then
        cell.taskStateCtrl:SetState("Complete")
        cell.goToBtn.gameObject:SetActive(false)
        cell.descTxt.gameObject:SetActive(false)
    else
        cell.taskStateCtrl:SetState("InProgress")
        cell.descTxt.gameObject:SetActive(true)
        local _, progressDesc = Utils.getCurMissionIdAndDesc("activity")
        cell.descTxt.text = string.format(
            Language.LUA_SNAPSHOT_REWARD_TASK_CUR_PROGRESS,
            progressDesc or "")
        cell.goToBtn.gameObject:SetActive(true)
        cell.goToBtn.onClick:AddListener(function()
            if not string.isEmpty(info.missionId)
                and missionSystem:GetMissionState(info.missionId) ==
                CS.Beyond.Gameplay.MissionSystem.MissionState.Processing then
                PhaseManager:OpenPhase(PhaseId.Mission, { autoSelect = info.missionId })
            end
        end)
    end
end


SnapshotRewardTaskCtrl._RefreshOverview = HL.Method() << function(self)
    local total = #self.m_taskInfos
    local completed = self:_GetCompletedCount()
    self.view.progressSlider.value = total > 0 and completed / total or 0
    self.view.progressTxt.text = string.format("%d/%d", completed, total)

    local overviewState = self:_GetOverviewState()
    self.view.taskOverviewState:SetState(overviewState)

    local canGetReward = overviewState == "CanGetReward"
    self.view.getRewardBtn.gameObject:SetActive(canGetReward)
    self.view.redDot.gameObject:SetActive(canGetReward)
end


SnapshotRewardTaskCtrl._RefreshTaskProgress = HL.Method() << function(self)
    self.m_isRewarded = snapshotSystem:IsTaskRewarded(self.m_rewardTaskId)
    self:_UpdateTaskInfos()
    self:_RefreshTaskList()
    self:_RefreshOverview()
end


SnapshotRewardTaskCtrl._OnMissionStateChange = HL.Method(HL.Any) << function(self, arg)
    self:_RefreshTaskProgress()
end


SnapshotRewardTaskCtrl._OnQuestStateChange = HL.Method(HL.Any) << function(self, arg)
    self:_RefreshTaskProgress()
end


SnapshotRewardTaskCtrl._OnQuestObjectiveUpdate = HL.Method(HL.Any) << function(self, arg)
    self:_RefreshTaskProgress()
end


SnapshotRewardTaskCtrl._OnRewardTaskRewarded = HL.Method(HL.Any) << function(self, id)
    if id ~= self.m_rewardTaskId then
        return
    end
    self.m_isRewarded = true
    self:_RefreshOverview()
end

HL.Commit(SnapshotRewardTaskCtrl)
