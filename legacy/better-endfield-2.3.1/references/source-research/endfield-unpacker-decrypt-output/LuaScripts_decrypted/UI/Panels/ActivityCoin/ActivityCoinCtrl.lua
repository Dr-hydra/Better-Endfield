
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ActivityCoin
local REWARD_PHASE_ID = PhaseId.ActivityCoinReward
local MILESTONE_PHASE_ID = PhaseId.ActivityCoinMilestone

ActivityCoinCtrl = HL.Class('ActivityCoinCtrl', uiCtrl.UICtrl)

ActivityCoinCtrl.m_calendarCountdownCor = HL.Field(HL.Any)

ActivityCoinCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_CONDITIONAL_MULTI_STAGE_UPDATE] = 'OnConditionalMultiStageChange',
    [MessageConst.ON_CONDITIONAL_MULTI_STAGE_PROGRESS_CHANGE] = 'OnConditionalMultiStageChange',
    [MessageConst.ON_CONDITIONAL_MULTI_STAGE_TASK_PROGRESS_CHANGE] = 'OnConditionalMultiStageChange',
    [MessageConst.ON_ACTIVITY_RACING_DUNGEON_MILESTONE_CHANGED] = 'OnConditionalMultiStageChange',
    [MessageConst.ON_RACING_DUNGEON_GET_MILESTONE_REWARD] = 'OnConditionalMultiStageChange',
}

ActivityCoinCtrl.m_activityId = HL.Field(HL.String) << ''

ActivityCoinCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    self.m_activityId = args.activityId
    args.showCalendar = true
    args.showMedal = true
    args.calendarInstructionId = self.view.config.CALENDAR_ID
    args.jumpBtnCallBack = function()
        local dungeonId = ActivityUtils.RacingDungeonGetGameId(self.m_activityId)
        PhaseManager:GoToPhase(PhaseId.CharFormation, {
            dungeonId = dungeonId,
            enterDungeonCallback = function(enterDungeonId)
                LuaSystemManager.uiRestoreSystem:AddRequest(enterDungeonId)
            end,
            infoBtnCallback = function()
                Notify(MessageConst.SHOW_INTRO, "dungeon_race")
            end,
        })
    end
    args.skipReceive = true
    self.view.activityCommonInfo:InitActivityCommonInfo(args)

    self.view.taskBtn.button.onClick:RemoveAllListeners()
    self.view.taskBtn.button.onClick:AddListener(function()
        PhaseManager:OpenPhase(REWARD_PHASE_ID, {
            activityId = self.m_activityId,
            openInDungeon = false,
        })
    end)
    self.view.taskBtn.redDot:InitRedDot("ActivityRacingDungeonTaskDetail", self.m_activityId)

    self.view.milestoneBtn.button.onClick:RemoveAllListeners()
    self.view.milestoneBtn.button.onClick:AddListener(function()
        PhaseManager:OpenPhase(MILESTONE_PHASE_ID, {
            activityId = self.m_activityId,
        })
    end)
    self.view.milestoneBtn.redDot:InitRedDot("ActivityRacingDungeonMilestoneDetail", self.m_activityId)

    self.view.leftInfoNode.rankingBtn.button.onClick:RemoveAllListeners()
    self.view.leftInfoNode.rankingBtn.button.onClick:AddListener(function()
        PhaseManager:OpenPhase(PhaseId.ActivityCoinRanking, {
            activityId = self.m_activityId,
            rankRelatedId = ActivityUtils.RacingDungeonGetGameId(self.m_activityId),
        })
    end)

    self:_RefreshUI()
end

ActivityCoinCtrl._RefreshUI = HL.Method() << function(self)
    local activityData = GameInstance.player.activitySystem:GetActivity(self.m_activityId)

    if activityData.status ~= GEnums.ActivityStatus.InProgress and
        activityData.status ~= GEnums.ActivityStatus.Completed then
        self.view.leftInfoNode.gameObject:SetActive(false)
    else
        self.view.leftInfoNode.gameObject:SetActive(true)
    end

    local completedCount, totalCount = ActivityUtils.GetTaskCompletionCount(self.m_activityId)
    self.view.taskBtn.taskNumTxt.text = string.format("%d/%d", completedCount, totalCount)

    local currScore = ActivityUtils.GetRacingDungeonMilestoneCurrScore(self.m_activityId)
    local maxScore = ActivityUtils.GetRacingDungeonMilestoneMaxScore(self.m_activityId)
    self.view.milestoneBtn.milestoneNumTxt.text = string.format("%s/%s", currScore, maxScore)


    local succ, rankValue = GameInstance.player.activitySystem:TryGetActivityOwnRankValue(
        self.m_activityId,
        ActivityUtils.RacingDungeonGetGameId(self.m_activityId))
    self.view.leftInfoNode.rankingBtn.milestoneNumTxt.text = tostring(rankValue)


    self:_RefreshUIOpenText()


    self:_RefreshActivityCommonInfoRewardState()
end

ActivityCoinCtrl._RefreshActivityCommonInfoRewardState = HL.Method() << function(self)
    local activity = GameInstance.player.activitySystem:GetActivity(self.m_activityId)
    local receiveAll = false
    if activity then
        receiveAll = activity.receiveAllReward
    end
    receiveAll = receiveAll and self:_HasReceivedAllMilestoneRewards() and
            not self.view.activityCommonInfo.view.config.HIDE_RECEIVE_ALL
    local gotoNode = self.view.activityCommonInfo.view.gotoNode
    gotoNode.receiveAllNode.gameObject:SetActive(receiveAll)
    gotoNode.notReceiveAllNode.gameObject:SetActive(not receiveAll)
end

ActivityCoinCtrl._HasReceivedAllMilestoneRewards = HL.Method().Return(HL.Boolean) << function(self)
    local activity = GameInstance.player.activitySystem:GetActivity(self.m_activityId)
    if not activity then
        return false
    end

    local haveCfg, milestoneCfg = Tables.activityRacingDungeonMilestoneTable:TryGetValue(self.m_activityId)
    if not haveCfg then
        return false
    end

    local hasMilestoneReward = false
    local receivedNodes = activity.receivedMilestoneNodes
    for nodeId, _ in pairs(milestoneCfg.milestoneMap) do
        if not receivedNodes:Contains(nodeId) then
            return false
        end
        hasMilestoneReward = true
    end
    return hasMilestoneReward
end


ActivityCoinCtrl._RefreshUIOpenText = HL.Method() << function(self)
    if self.m_calendarCountdownCor then
        self.m_calendarCountdownCor = self:_ClearCoroutine(self.m_calendarCountdownCor)
    end

    local activityData = GameInstance.player.activitySystem:GetActivity(self.m_activityId)

    if activityData.status ~= GEnums.ActivityStatus.InProgress then
        self.view.stateNewNode.gameObject:SetActive(false)
        return
    end

    local stages = {}
    local haveCfg, multiStageCfg = Tables.activityConditionalMultiStageTable:TryGetValue(self.m_activityId)
    for stageId, stageCfg in pairs(multiStageCfg.stageList) do
        table.insert(stages, { stageId = stageId, cfg = stageCfg })
    end
    table.sort(stages, function(a, b) return a.cfg.sortId < b.cfg.sortId end)

    if #stages < 2 then
        self.view.stateNewNode.gameObject:SetActive(false)
        return
    end

    local stage1 = stages[1]
    local stage2 = stages[2]
    local _, stage1Data = activityData.stageDataDict:TryGetValue(stage1.stageId)
    local _, stage2Data = activityData.stageDataDict:TryGetValue(stage2.stageId)
    local status1 = stage1Data and GEnums.ActivityConditionalStageState.__CastFrom(stage1Data.Status)
    local status2 = stage2Data and GEnums.ActivityConditionalStageState.__CastFrom(stage2Data.Status)
    local isLocked1 = status1 == GEnums.ActivityConditionalStageState.Locked
    local isLocked2 = status2 == GEnums.ActivityConditionalStageState.Locked

    if isLocked1 then
        self.view.stateNewNode.gameObject:SetActive(false)
        return
    end

    if isLocked2 then
        self.view.stateNewNode.stateController:SetState("Stage01")
        local openTime = stage2Data.OpenTimeTs
        local curTime = DateTimeUtils.GetCurrentTimestampBySeconds()
        local leftSec = openTime - curTime
        if leftSec < 0 then leftSec = 0 end
        self.view.stateNewNode.newOpenTxt.text = string.format(Language.LUA_ACTIVITY_RACING_DUNGEON_STAGE_TWO_REMAIN_TEXT, UIUtils.getLeftTime(leftSec))

        self.m_calendarCountdownCor = self:_StartCoroutine(function()
            while true do
                coroutine.wait(1)
                local remaining = openTime - DateTimeUtils.GetCurrentTimestampBySeconds()
                if remaining < 0 then remaining = 0 end
                self.view.stateNewNode.newOpenTxt.text = string.format(Language.LUA_ACTIVITY_RACING_DUNGEON_STAGE_TWO_REMAIN_TEXT, UIUtils.getLeftTime(remaining))
                if remaining <= 0 then
                    self.m_calendarCountdownCor = nil
                    break
                end
            end
        end)
    else
        if ActivityUtils.RacingDungeonStageTwoIsNew(self.m_activityId) then
            self.view.stateNewNode.gameObject:SetActive(true)
            self.view.stateNewNode.stateController:SetState("Stage02")
        else
            self.view.stateNewNode.gameObject:SetActive(false)
        end
    end
end

ActivityCoinCtrl.OnConditionalMultiStageChange = HL.Method(HL.Any) << function(self, args)
    local id = unpack(args)
    if id ~= self.m_activityId then
        return
    end
    self:_RefreshUI()
end

HL.Commit(ActivityCoinCtrl)
