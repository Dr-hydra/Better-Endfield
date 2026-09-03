local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

SnapshotChallengeMainInfoCommon = HL.Class('SnapshotChallengeMainInfoCommon', UIWidgetBase)


local activitySystem = GameInstance.player.activitySystem

local snapshotSystem = GameInstance.player.snapshotSystem

local missionSystem = GameInstance.player.mission

local PHASE_ID = PhaseId.SnapshotChallenge
local PANEL_ID = PanelId.SnapshotChallenge



SnapshotChallengeMainInfoCommon.StageStateEnum = HL.Field(HL.Table)

SnapshotChallengeMainInfoCommon.m_snapshotCtrl = HL.Field(HL.Any)

SnapshotChallengeMainInfoCommon.m_info = HL.Field(HL.Table)

SnapshotChallengeMainInfoCommon.m_stageNodeList = HL.Field(HL.Table)

SnapshotChallengeMainInfoCommon.m_stageProgressBarList = HL.Field(HL.Table)

SnapshotChallengeMainInfoCommon.m_updateCor = HL.Field(HL.Thread)

SnapshotChallengeMainInfoCommon.m_rewardCellCache = HL.Field(HL.Forward("UIListCache"))

SnapshotChallengeMainInfoCommon.m_identifyDescCellCache = HL.Field(HL.Forward("UIListCache"))

SnapshotChallengeMainInfoCommon.m_activityId = HL.Field(HL.String) << ""

SnapshotChallengeMainInfoCommon.m_defaultStageId = HL.Field(HL.String) << ""

SnapshotChallengeMainInfoCommon.m_readStageIds = HL.Field(HL.Table)




SnapshotChallengeMainInfoCommon._OnFirstTimeInit = HL.Override() << function(self)
    self.StageStateEnum = {
        Lock = 0,
        NeedCompletePreTask = 1,
        InProgress = 2,
        Complete = 3,
        Rewarded = 4,
    }
    self.m_readStageIds = {}
    self:RegisterMessage(MessageConst.ON_CONDITIONAL_MULTI_STAGE_UPDATE, function(args)
        self:_OnMultiStageUpdate(args)
    end)
    self:RegisterMessage(MessageConst.ON_CONDITIONAL_MULTI_STAGE_PROGRESS_CHANGE, function(args)
        self:_OnMultiStageUpdate(args)
    end)

    self.view.scrollViewSelectableNaviGroup.onIsFocusedChange:AddListener(function(isFocused)
        if not isFocused then
            self.view.descScrollRect.verticalNormalizedPosition = 1
        end
    end)
end


SnapshotChallengeMainInfoCommon.InitSnapshotChallengeMainInfo = HL.Virtual(HL.Any, HL.String, HL.String) << function(self, snapshotCtrl, activityId, stageId)
    self:_FirstTimeInit()
    self.m_snapshotCtrl = snapshotCtrl
    self.m_activityId = activityId
    self.m_defaultStageId = stageId
    self:_InitUI()
    self:_InitData()
    self:_UpdateData(true)
    self:_RefreshAllUI()
    if self.view.config:HasValue("AUDIO_IN_EVENT") then
        AudioAdapter.PostEvent(self.view.config.AUDIO_IN_EVENT)
    end
end

SnapshotChallengeMainInfoCommon.OnClose = HL.Virtual() << function(self)
    if self.view.config:HasValue("AUDIO_OUT_EVENT") then
        AudioAdapter.PostEvent(self.view.config.AUDIO_OUT_EVENT)
    end
    self.m_updateCor = self:_ClearCoroutine(self.m_updateCor)

    for _, stageId in pairs(self.m_readStageIds) do
        if ActivityUtils.isNewActivityConditionalStage(stageId) then
            ActivityUtils.setFalseNewActivityConditionalStage(stageId)
        end
    end
end




SnapshotChallengeMainInfoCommon._InitData = HL.Virtual() << function(self)
    local activityId = self.m_activityId
    local _, activityCfg = Tables.activityTable:TryGetValue(activityId)

    local activityData = activitySystem:GetActivity(activityId)


    self.m_info = {
        activityId = activityId,
        activityName = activityCfg.name,
        instructionId = activityCfg.instructionId,

        startTime = activityData and activityData.startTime or -1,
        nextStageUnlockTime = -1,
        stageInfoList = {},

        curSelectStage = 1,
        curStageInfo = nil,
    }

    local _, multiStageCfg = Tables.activityConditionalMultiStageTable:TryGetValue(activityId)
    for stageId, stageCfg in pairs(multiStageCfg.stageList) do
        local _, activitySnapshotCfg = Tables.activitySnapshotChallengeTable:TryGetValue(stageId)
        local _, activitySnapshotStageCfg = Tables.activitySnapShotStageTable:TryGetValue(stageId)
        local levelId = activitySnapshotStageCfg.levelId
        local hasLevelCfg, levelCfg = Tables.levelDescTable:TryGetValue(levelId)
        local levelName = hasLevelCfg and levelCfg.showName or ""
        local missionId = stageCfg.missionId
        local questId = activitySnapshotStageCfg.questId

        local rewardId = stageCfg.rewardId

        local identifyGroupId = snapshotSystem:GetQuestIdentifyGroupId(missionId, questId)
        local identifyDescInfos = {}
        local hasCfg, identifyGroupCfg = Tables.snapshotIdentifyGroupTable:TryGetValue(identifyGroupId)
        if hasCfg then
            for _, identifyId in pairs(identifyGroupCfg.identifyIds) do
                local desc = GameInstance.player.snapshotSystem:GetIdentifyDesc(identifyId)
                table.insert(identifyDescInfos, desc)
            end
        end

        local preTaskId = activitySnapshotCfg.missionId
        local preTaskState = missionSystem:GetMissionState(preTaskId)
        local isPreTaskComplete = preTaskState == CS.Beyond.Gameplay.MissionSystem.MissionState.Completed

        local canShowPreTaskDetail = preTaskState == CS.Beyond.Gameplay.MissionSystem.MissionState.Processing

        local _, stageData = activityData.stageDataDict:TryGetValue(stageId)
        local stageInfo = {
            stageId = stageId,
            missionId = missionId,
            questId = questId,
            questTitle = stageCfg.name,
            questDesc = stageCfg.desc,
            jumpId = stageCfg.jumpId,
            levelName = levelName,
            openTime = stageData.OpenTimeTs,
            rewardList = UIUtils.getRewardItems(rewardId),
            sortId = stageCfg.sortId,

            preTaskMissionId = preTaskId,
            isPreTaskComplete = isPreTaskComplete,
            canShowPreTaskDetail = canShowPreTaskDetail,
            normalImg = activitySnapshotCfg.normalImg,
            completeImg = activitySnapshotCfg.completeImg,
            normalDesc = activitySnapshotCfg.normalStoryDesc,
            completeDesc = activitySnapshotCfg.completeStoryDesc,

            identifyGroupId = identifyGroupId,
            identifyDescInfos = identifyDescInfos,

            state = self.StageStateEnum.Lock,
            identifyComplete = false,
        }
        table.insert(self.m_info.stageInfoList, stageInfo)
    end
    table.sort(self.m_info.stageInfoList, function(a, b)
        return a.sortId < b.sortId
    end)
end

SnapshotChallengeMainInfoCommon._UpdateData = HL.Virtual(HL.Boolean) << function(self, isInit)
    self.m_info.nextStageUnlockTime = -1
    local firstCompleteStageIndex = -1
    local firstInProgressStageIndex = -1
    local initArgDefaultIndex = -1
    for i, stageInfo in pairs(self.m_info.stageInfoList) do
        self:_UpdateStageInfo(stageInfo)
        if stageInfo.stageId == self.m_defaultStageId and stageInfo.state ~= self.StageStateEnum.Lock then
            initArgDefaultIndex = i
        end
        if self.m_info.startTime > 0 and self.m_info.nextStageUnlockTime < 0 and stageInfo.state == self.StageStateEnum.Lock then
            self.m_info.nextStageUnlockTime = stageInfo.unlockTime
        end
        if firstCompleteStageIndex < 0 and stageInfo.state == self.StageStateEnum.Complete then
            firstCompleteStageIndex = i
        end
        if firstInProgressStageIndex < 0 and (stageInfo.state == self.StageStateEnum.InProgress or stageInfo.state == self.StageStateEnum.NeedCompletePreTask) then
            firstInProgressStageIndex = i
        end
    end

    if isInit then
        local defaultIndex = 1
        if initArgDefaultIndex > 0 then
            defaultIndex = initArgDefaultIndex
        elseif firstCompleteStageIndex > 0 then
            defaultIndex = firstCompleteStageIndex
        elseif firstInProgressStageIndex > 0 then
            defaultIndex = firstInProgressStageIndex
        end
        defaultIndex = math.max(defaultIndex, 1)
        self.m_info.curSelectStage = defaultIndex
    end
end

SnapshotChallengeMainInfoCommon._UpdateStageInfo = HL.Virtual(HL.Table) << function(self, stageInfo)
    local activityId = self.m_info.activityId
    local stageId = stageInfo.stageId

    local activityData = activitySystem:GetActivity(activityId)
    local stageData = activityData:GetStageData(stageId)
    stageInfo.unlockTime = stageInfo.openTime

    local state = self.StageStateEnum.Lock
    if stageData ~= nil then
        local status = GEnums.ActivityConditionalStageState.__CastFrom(stageData.Status)
        local nowTime = DateTimeUtils.GetCurrentTimestampBySeconds()
        if status == GEnums.ActivityConditionalStageState.Locked and nowTime >= stageInfo.unlockTime then
            local canForceShow, failDesc = activityData:CanShowStage(stageInfo.stageId)
            if canForceShow then
                status = GEnums.ActivityConditionalStageState.Unlocked
                stageInfo.preTaskTipsDesc = failDesc
            end
        end
        if status == GEnums.ActivityConditionalStageState.Locked then
            state = self.StageStateEnum.Lock
        elseif status == GEnums.ActivityConditionalStageState.Unlocked then
            state = stageInfo.isPreTaskComplete and self.StageStateEnum.InProgress or self.StageStateEnum.NeedCompletePreTask
        elseif status == GEnums.ActivityConditionalStageState.Completed then
            state = self.StageStateEnum.Complete
        elseif status == GEnums.ActivityConditionalStageState.Rewarded then
            state = self.StageStateEnum.Rewarded
        end
    end
    stageInfo.state = state
    stageInfo.identifyComplete = state == self.StageStateEnum.Complete or state == self.StageStateEnum.Rewarded
end



SnapshotChallengeMainInfoCommon._InitUI = HL.Virtual() << function(self)
    local viewNode = self.view
    viewNode.commonTitle.btnClose.onClick:AddListener(function()
        self:_Close()
    end)
    viewNode.commonTitle.helpBtn.onClick:AddListener(function()
        UIManager:Open(PanelId.InstructionBook, self.m_info.instructionId)
    end)
    viewNode.preTaskBtn.onClick:AddListener(function()
        PhaseManager:OpenPhase(PhaseId.Mission, {
            autoSelect = self.m_info.curStageInfo.preTaskMissionId
        })
    end)
    viewNode.goToBtn.onClick:AddListener(function()

        local stageInfo = self.m_info.curStageInfo
        Utils.jumpToSystem(stageInfo.jumpId)
    end)
    viewNode.rewardBtn.onClick:AddListener(function()
        local stageInfo = self.m_info.curStageInfo
        activitySystem:SendReceiveRewardConditionMultiStage(self.m_info.activityId, stageInfo.stageId)
    end)

    self.m_rewardCellCache = UIUtils.genCellCache(viewNode.itemRewardCell)
    self.m_identifyDescCellCache = UIUtils.genCellCache(viewNode.identifyDescCell)

    self.m_stageNodeList = {}
    self.m_stageProgressBarList = {}
    for i = 1, viewNode.config.MAX_STAGE_NUM do
        table.insert(self.m_stageNodeList, viewNode.stageProgress["stageNode" .. i])
        table.insert(self.m_stageProgressBarList, viewNode.stageProgress["stageProgressBar" .. i])
    end
    table.insert(self.m_stageProgressBarList, viewNode.stageProgress.stageProgressBarFinal)


    local preActionId = viewNode.stageProgress.keyHintLeft.actionId
    local nextActionId = viewNode.stageProgress.keyHintRight.actionId
    self.m_snapshotCtrl:BindInputPlayerAction(preActionId, function()
        if self.view.config:HasValue("AUDIO_SWITCH_TAB_EVENT") then
            AudioAdapter.PostEvent(self.view.config.AUDIO_SWITCH_TAB_EVENT)
        end
        local count = #self.m_info.stageInfoList
        local newIndex = (self.m_info.curSelectStage + count - 2) % count + 1
        if newIndex ~= self.m_info.curSelectStage then
            self:_OnClickStageBtn(newIndex)
        end
    end)
    self.m_snapshotCtrl:BindInputPlayerAction(nextActionId, function()
        if self.view.config:HasValue("AUDIO_SWITCH_TAB_EVENT") then
            AudioAdapter.PostEvent(self.view.config.AUDIO_SWITCH_TAB_EVENT)
        end
        local count = #self.m_info.stageInfoList
        local newIndex = self.m_info.curSelectStage % count + 1
        if newIndex ~= self.m_info.curSelectStage then
            self:_OnClickStageBtn(newIndex)
        end
    end)
end

SnapshotChallengeMainInfoCommon._RefreshAllUI = HL.Virtual() << function(self)
    self:_RefreshTitle()
    self:_RefreshStageCell()
    self:_RefreshContentUI(self.m_info.curSelectStage)
end

SnapshotChallengeMainInfoCommon._RefreshTitle = HL.Virtual() << function(self)
    local viewNode = self.view
    viewNode.commonTitle.titleTxt.text = self.m_info.activityName
    local nowTime = DateTimeUtils.GetCurrentTimestampBySeconds()
    local hasUnlockTime = self.m_info.nextStageUnlockTime > nowTime
    viewNode.commonTitle.timeTxtNode.gameObject:SetActive(hasUnlockTime)
    if hasUnlockTime then
        self.m_updateCor = self:_ClearCoroutine(self.m_updateCor)
        self.m_updateCor = self:_StartCoroutine(function()
            viewNode.commonTitle.timeTxtNode.gameObject:SetActive(true)
            local curTime = DateTimeUtils.GetCurrentTimestampBySeconds()
            while curTime <= self.m_info.nextStageUnlockTime do
                local leftTime = self.m_info.nextStageUnlockTime - curTime
                viewNode.commonTitle.timeTxt.text = UIUtils.getLeftTime(leftTime)
                coroutine.wait(1)
            end
            viewNode.commonTitle.timeTxtNode.gameObject:SetActive(false)
        end)
    else
        viewNode.commonTitle.timeTxtNode.gameObject:SetActive(false)
    end
end

SnapshotChallengeMainInfoCommon._RefreshStageCell = HL.Virtual() << function(self)
    local viewNode = self.view
    local infoCount = #self.m_info.stageInfoList
    local maxUICount = viewNode.config.MAX_STAGE_NUM

    for i = 1, maxUICount do
        local isShow = i <= infoCount
        self.m_stageNodeList[i].gameObject:SetActive(isShow)
        self.m_stageProgressBarList[i + 1].gameObject:SetActive(isShow)
    end

    local completeCount = 0
    for i = 1, infoCount do
        local stageInfo = self.m_info.stageInfoList[i]
        local stageNode = self.m_stageNodeList[i]
        local stageBar = self.m_stageProgressBarList[i]

        local isComplete = stageInfo.state == self.StageStateEnum.Complete
        local isRewarded = stageInfo.state == self.StageStateEnum.Rewarded
        if stageInfo.state == self.StageStateEnum.Lock then
            stageNode.stateCtrl:SetState("NotUnlocked")
            stageBar.progressImg.fillAmount = 0
        else
            if isRewarded then
                stageNode.stateCtrl:SetState("Complete")
                stageBar.progressImg.fillAmount = 1
                completeCount = completeCount + 1
            elseif isComplete then
                stageBar.progressImg.fillAmount = 1
                completeCount = completeCount + 1
                stageNode.stateCtrl:SetState("Normal")
            else
                stageNode.stateCtrl:SetState("Normal")
                stageBar.progressImg.fillAmount = 0
            end
        end

        stageNode.stateCtrl:SetState(self.m_info.curSelectStage == i and "Select" or "UnSelect")

        stageNode.btn.onClick:RemoveAllListeners()
        stageNode.btn.onClick:AddListener(function()
            if self.m_info.curSelectStage == i then
                return
            end
            self:_OnClickStageBtn(i)
        end)

        local isNewStage = ActivityUtils.isNewActivityConditionalStage(stageInfo.stageId)
        if self.m_info.curSelectStage == i and ActivityUtils.isNewActivityConditionalStage(stageInfo.stageId) then

            ActivityUtils.setFalseNewActivityConditionalStage(stageInfo.stageId)
            isNewStage = false
        end
        local isFirstUnlock = stageInfo.state ~= self.StageStateEnum.Lock
            and stageInfo.state ~= self.StageStateEnum.Rewarded
            and isNewStage
        if isFirstUnlock or stageInfo.state == self.StageStateEnum.Complete then
            stageNode.stateCtrl:SetState("HasRedDot")
        else
            stageNode.stateCtrl:SetState("NoRedDot")
        end
    end
    local finalBar = self.m_stageProgressBarList[infoCount + 1]
    finalBar.progressImg.fillAmount = completeCount == infoCount and 1 or 0
end

SnapshotChallengeMainInfoCommon._RefreshContentUI = HL.Virtual(HL.Number) << function(self, stageIndex)
    local viewNode = self.view
    self.m_info.curSelectStage = stageIndex
    local stageInfo = self.m_info.stageInfoList[stageIndex]
    local isComplete = stageInfo.state == self.StageStateEnum.Complete
    local isRewarded = stageInfo.state == self.StageStateEnum.Rewarded
    local isCompleteOrRewarded = isComplete or isRewarded
    self.m_info.curStageInfo = stageInfo

    viewNode.normalImg:LoadSprite(UIConst.UI_SPRITE_SNAPSHOT_CHALLENGE, stageInfo.normalImg)
    viewNode.completeImg:LoadSprite(UIConst.UI_SPRITE_SNAPSHOT_CHALLENGE, stageInfo.completeImg)
    viewNode.normalImg.gameObject:SetActive(not isCompleteOrRewarded)
    viewNode.completeImg.gameObject:SetActive(isCompleteOrRewarded)
    viewNode.descTxt.text = isCompleteOrRewarded and stageInfo.completeDesc or stageInfo.normalDesc
    LayoutRebuilder.ForceRebuildLayoutImmediate(viewNode.descTxt.transform.parent)
    local canFocus = viewNode.descTxt.transform.rect.height > viewNode.descScrollRect.transform.rect.height
    self.view.scrollViewSelectableNaviGroup.enabled = canFocus
    viewNode.questTitleTxt.text = stageInfo.questTitle
    viewNode.levelNameTxt.text = stageInfo.levelName
    viewNode.questDescTxt.text = stageInfo.questDesc

    self.m_identifyDescCellCache:Refresh(#stageInfo.identifyDescInfos, function(cell, luaIndex)
        local curStageInfo = self.m_info.curStageInfo
        local desc = curStageInfo.identifyDescInfos[luaIndex]
        cell.descTxt.text = desc
        cell.stateController:SetState(curStageInfo.identifyComplete and "Complete" or "Normal")
        cell.finishedIcon.gameObject:SetActive(curStageInfo.identifyComplete)
        cell.gameObject.name = "identifyDescCell_" .. luaIndex
    end)


    self.m_rewardCellCache:Refresh(#stageInfo.rewardList, function(rewardCell, luaIndex)
        rewardCell:InitItem(stageInfo.rewardList[luaIndex], function()
            UIUtils.showItemSideTips(rewardCell)
        end)
        rewardCell:SetExtraInfo({ isSideTips = DeviceInfo.usingController })
        rewardCell.view.rewardedCover.gameObject:SetActive(isRewarded)
    end)
    viewNode.rewardedImg.gameObject:SetActive(isRewarded)
    viewNode.rewardListNaviGroup.onIsFocusedChange:RemoveAllListeners()
    viewNode.rewardListNaviGroup.onIsFocusedChange:AddListener(function(isFocused)
        if not isFocused then
            Notify(MessageConst.HIDE_ITEM_TIPS)
        end
    end)

    if isComplete then
        viewNode.btnNodeStateCtrl:SetState("CanGetReward")
    elseif stageInfo.state == self.StageStateEnum.NeedCompletePreTask then
        if stageInfo.canShowPreTaskDetail then
            viewNode.btnNodeStateCtrl:SetState("HasPreTask")
        else
            viewNode.btnNodeStateCtrl:SetState("HasPreTaskNoJump")
        end
    else
        viewNode.btnNodeStateCtrl:SetState("Normal")
    end
    if stageInfo.preTaskTipsDesc then
        viewNode.preTaskTipsTxt.text = stageInfo.preTaskTipsDesc
    end

    table.insert(self.m_readStageIds, stageInfo.stageId)
end

SnapshotChallengeMainInfoCommon._ChangeSelectStage = HL.Virtual(HL.Number) << function(self, stageIndex)
    local oldIndex = self.m_info.curSelectStage
    local oldStageNode = self.m_stageNodeList[oldIndex]
    oldStageNode.stateCtrl:SetState("UnSelect")
    local oldStageInfo = self.m_info.stageInfoList[oldIndex]

    if ActivityUtils.isNewActivityConditionalStage(oldStageInfo.stageId) then
        ActivityUtils.setFalseNewActivityConditionalStage(oldStageInfo.stageId)
    end
    if oldStageInfo.state ~= self.StageStateEnum.Complete then
        oldStageNode.stateCtrl:SetState("NoRedDot")
    end

    local curStageNode = self.m_stageNodeList[stageIndex]
    curStageNode.stateCtrl:SetState("Select")
    local curStageInfo = self.m_info.stageInfoList[stageIndex]
    self.view.animationWrapper:Play("activity_snapshotchallenge_switch")
    self:_RefreshContentUI(stageIndex)

    if ActivityUtils.isNewActivityConditionalStage(curStageInfo.stageId) then
        ActivityUtils.setFalseNewActivityConditionalStage(curStageInfo.stageId)
    end
    if curStageInfo.state == self.StageStateEnum.Complete then
        curStageNode.stateCtrl:SetState("HasRedDot")
    else
        curStageNode.stateCtrl:SetState("NoRedDot")
    end
end



SnapshotChallengeMainInfoCommon._OnMultiStageUpdate = HL.Virtual(HL.Any) << function(self, arg)
    local activityId = unpack(arg)
    if self.m_info.activityId ~= activityId then
        return
    end
    self:_UpdateData(false)
    self:_RefreshAllUI()
end

SnapshotChallengeMainInfoCommon._OnClickStageBtn = HL.Virtual(HL.Number) << function(self, luaIndex)
    local stageInfo = self.m_info.stageInfoList[luaIndex]
    if stageInfo.state ~= self.StageStateEnum.Lock then
        self:_ChangeSelectStage(luaIndex)
    else
        local nowTime = DateTimeUtils.GetCurrentTimestampBySeconds()
        if stageInfo.unlockTime > nowTime then

            Notify(MessageConst.SHOW_TOAST, Language.LUA_ACTIVITY_LOCK_TOAST_TIME)
        else

            Notify(MessageConst.SHOW_TOAST, Language.LUA_ACTIVITY_LOCK_TOAST_PRECONDITION)
        end
    end
end

SnapshotChallengeMainInfoCommon._Close = HL.Virtual() << function(self)
    PhaseManager:PopPhase(PHASE_ID)
end



HL.Commit(SnapshotChallengeMainInfoCommon)
return SnapshotChallengeMainInfoCommon

