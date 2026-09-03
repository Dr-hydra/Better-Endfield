local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.MissionHud

local missionCompletePopCtrl = require_ex('UI/Panels/MissionCompletePop/MissionCompletePopCtrl')

local QuestState = CS.Beyond.Gameplay.MissionSystem.QuestState
local MissionState = CS.Beyond.Gameplay.MissionSystem.MissionState
local MissionImportance = CS.Beyond.GEnums.MissionImportance

local TrackAction = CS.Beyond.Gameplay.MissionSystem.TrackAction

local MissionAnimType = CS.Beyond.Gameplay.MissionShowData.AnimType
local QuestAnimType = CS.Beyond.Gameplay.QuestShowData.AnimType
local ObjectiveAnimType = CS.Beyond.Gameplay.ObjectiveShowData.AnimType

local QUEST_CELL_PADDING_TOP = 10

local OPTIONAL_TEXT_COLOR = "C7EC59"

local IMPORTANCE_KEY_HIGH = "ImportanceHigh"
local IMPORTANCE_KEY_MID = "ImportanceMid"
local IMPORTANCE_KEY_LOW = "ImportanceLow"

local CONTENT_ARROW_IN = "commontasktrackhud_arrowbottom_in"
local CONTENT_ARROW_OUT = "commontasktrackhud_arrowbottom_out"

local BTN_FOLD_ANIM_FOLD = "tasktrackhud_btnfold_fold"
local BTN_FOLD_ANIM_UNFOLD = "tasktrackhud_btnfold_unfold"

local LEFT_HASH_BASE = 4

local LEFT_HASH_OBJ_NO_DIST_NODE = 1
local LEFT_HASH_OBJ_DIST_NUMBER = 2
local LEFT_HASH_OBJ_BTN = 3

MissionHudCtrl = HL.Class('MissionHudCtrl', uiCtrl.UICtrl)







MissionHudCtrl.s_messages = HL.StaticField(HL.Table) << {

    [MessageConst.ON_SYNC_ALL_MISSION] = 'OnSyncAllMission',
    [MessageConst.ON_MISSION_STATE_CHANGE] = '_OnMissionStateChange',
    [MessageConst.ON_HUD_ORDER_ENQUEUE] = 'OnMissionHudOrderEnqueue',
    [MessageConst.ON_TRACKING_SNS] = '_OnTrackingSns',
    [MessageConst.CHANGE_SKIP_BOSS_BATTLE_BUTTON_STATE] = '_ChangeSkipBossBattleButtonState',
    [MessageConst.ON_TOGGLE_FAC_TOP_VIEW] = '_OnFacTopViewChange',
    [MessageConst.ON_TRACKING_TO_UI] = '_OnTrackingToUI',
    [MessageConst.ON_MISSION_HUD_EXCLUSIVE_MODE_CHANGE] = "_OnExclusiveModeChange",
    [MessageConst.ON_MISSION_HUD_SHOW_OPEN_BTN_CHANGE] = "_OnOpenBtnAvailableChange",
    [MessageConst.ON_TOGGLE_PHASE_FORBID] = "_OnTogglePhaseForbid",
}

MissionHudCtrl.m_missionSystem = HL.Field(HL.Any)

MissionHudCtrl.m_questCellCache = HL.Field(HL.Forward("UIListCache"))

MissionHudCtrl.m_currentMissionShowData = HL.Field(HL.Any)

MissionHudCtrl.m_missionStateChangeSignal = HL.Field(HL.Table)

MissionHudCtrl.m_updateDistanceTimerHandler = HL.Field(HL.Any)

MissionHudCtrl.m_leftContentHash = HL.Field(HL.Number) << 0

MissionHudCtrl.m_lastRebuildFrame = HL.Field(HL.Number) << 0




MissionHudCtrl.m_canScrollContent = HL.Field(HL.Boolean) << false

MissionHudCtrl.m_showArrow = HL.Field(HL.Boolean) << true

MissionHudCtrl.m_modifyingContentDontShowProg = HL.Field(HL.Boolean) << false

MissionHudCtrl.m_canFold = HL.Field(HL.Boolean) << false

MissionHudCtrl.m_isFold = HL.Field(HL.Boolean) << true

MissionHudCtrl.m_skipBossBattleButtonVisible = HL.StaticField(HL.Boolean) << false

MissionHudCtrl.m_scrollState = HL.Field(HL.Number) << -1

MissionHudCtrl.m_rectFoldHeight = HL.Field(HL.Number) << 0

MissionHudCtrl.m_rectUnfoldHeight = HL.Field(HL.Number) << 0

MissionHudCtrl.m_tween = HL.Field(HL.Any)

MissionHudCtrl.m_isOrderPlaying = HL.Field(HL.Boolean) << false

MissionHudCtrl.m_customLevelScriptBtnClearScreenKey = HL.Field(HL.Number) << -1

MissionHudCtrl.m_switcherCoroutine = HL.Field(HL.Thread)

MissionHudCtrl.m_blockedByMainHudActionQueueSys = HL.Field(HL.Boolean) << false

MissionHudCtrl.m_blockingMainHudActionQueueSys = HL.Field(HL.Boolean) << false



local MISSION_ANIM_TYPE = {
    None = 0,
    NewMission = 1,
    CompleteMission = 2,
    Track = 3,
    CompleteObjective = 4,
    RollbackObjective = 5,
    NewQuest = 6,
    CompleteQuest = 7,
}

local MissionViewType = CS.Beyond.GEnums.MissionViewType
local MissionTypeConfig = {
    [MissionViewType.MissionViewMain] = {
        missionDecoIcon = "main_mission_icon_gray",
    },
    [MissionViewType.MissionViewActivity] = {
        missionDecoIcon = "activity_mission_icon_gray",
    },
    [MissionViewType.MissionViewSide] = {
        missionDecoIcon = "char_mission_icon_gray",
    },
    [MissionViewType.MissionViewDiscovery] = {
        missionDecoIcon = "fac_mission_icon_gray",
    },
    [MissionViewType.MissionViewOther] = {
        missionDecoIcon = "misc_mission_icon_gray",
    },
}


MissionHudCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.openMissionPanelBtnWrapper:ClearTween()
    self.view.openMissionPanelBtnWrapper:PlayInAnimation()

    self.m_missionSystem = GameInstance.player.mission

    self.m_missionSystem:EnableMissionTrackData()

    self.m_questCellCache = self.m_questCellCache or UIUtils.genCellCache(self.view.questCell)

    self.view.trackCurrentDisplayMissionBtn.onClick:RemoveAllListeners()
    self.view.trackCurrentDisplayMissionBtn.onClick:AddListener(function()
        self:_SkipAnimationAndTrackMission()
    end)

    self.view.closeBtn.onClick:AddListener(function()
        self:_ShrinkPanel()
    end)

    self.view.objectiveList.onValueChanged:AddListener(function(normalizedPosition)
        self:_OnScrollValueChanged(normalizedPosition)
    end)

    if DeviceInfo.usingTouch then
        self.view.triggerTrackBtn.onClick:AddListener(function()
            self:_TriggerTrack()
        end)

        self.view.btnFold.onClick:AddListener(function()
            self:_OnBtnFoldClick()
        end)
    else
        self.view.triggerTrackBtn.gameObject:SetActiveIfNecessary(false)
        self.view.trackCurrentDisplayMissionBtn.gameObject:SetActiveIfNecessary(true)
    end

    self:_RefreshOpenBtn()
    self:_DisplayCurrentTrackingMission()

    self.view.openMissionUI.onClick:AddListener(function()
        local missionId = ""
        if self.m_currentMissionShowData and not string.isEmpty(self.m_currentMissionShowData.missionId) then
            missionId = self.m_currentMissionShowData.missionId
        end
        PhaseManager:OpenPhase(PhaseId.Mission, {autoSelect = missionId, useBlackMask = true})
    end)

    self:_ModifyBtnStateByFacTopView()
    self:_HandleSkipBossBattleButton(MissionHudCtrl.m_skipBossBattleButtonVisible)
    local lastGetDistanceTime = 0
    self.m_updateDistanceTimerHandler = LuaUpdate:Add("Tick", function()
        if not self:IsShow() then
            return
        end

        if Time.unscaledTime - lastGetDistanceTime <= 0.1 then
            return
        end

        lastGetDistanceTime = Time.unscaledTime
        self:_UpdateAllObjectiveDistance()
    end, true)

    self:_TryStartSwitcherCoroutine()
    if self.m_lastRebuildFrame ~= CS.Beyond.DLogger.FrameCountThreadSafe then
        self:_RebuildLayoutAndAdaptByDevice()
    end
end

MissionHudCtrl.OnMissionCompleteOrderEnqueue = HL.StaticMethod() << function()

    local open, self = UIManager:IsOpen(PanelId.MissionHud)
    if not open or self == nil then
        return
    end
    self:_TryBlockMainHudActionQueueSys()
end

MissionHudCtrl._TryBlockMainHudActionQueueSys = HL.Method() << function(self)
    if self.m_blockingMainHudActionQueueSys then
        return
    end



    if self.m_blockedByMainHudActionQueueSys then
        return
    end


    if self.m_currentMissionShowData ~= nil and self.m_currentMissionShowData.animType == MissionAnimType.Complete then
        return
    end

    self.m_blockingMainHudActionQueueSys = true
    logger.info(ELogChannel.Mission, string.format("[MissionHud] 加入奖励队列阻塞器，MainHudActionQueue停止工作"))
    LuaSystemManager.mainHudActionQueue:AddRequest("MissionHudCompleteBlocker", function()


        local missionHudOpen, missionHud = UIManager:IsOpen(PanelId.MissionHud)
        if (not missionHudOpen) or (not missionHud.m_blockingMainHudActionQueueSys) then
            logger.info(ELogChannel.Mission, string.format("[MissionHud] 移除奖励队列阻塞器，MainHudActionQueue恢复工作"))
            Notify(MessageConst.ON_ONE_MAIN_HUD_ACTION_FINISHED, "MissionHudCompleteBlocker")
            return
        end
    end)
end

MissionHudCtrl.OnMissionHudOrderEnqueue = HL.Method() << function(self)
    self:_TryStartSwitcherCoroutine()
end

MissionHudCtrl._TryStartSwitcherCoroutine = HL.Method() << function(self)
    if self.m_switcherCoroutine ~= nil then
        return
    end

    if self:IsHide() then
        return
    end

    if self.m_blockedByMainHudActionQueueSys then
        return
    end

    if self:_GetNextOrderIndex() > 0 then
        self:_StartSwitcherCoroutine()
        return
    end

    self:_DisplayCurrentTrackingMission()
end

MissionHudCtrl._StartSwitcherCoroutine = HL.Method() << function(self)
    local animThread = function(animInfo)
        self:_ObjAnimThreadFunc(animInfo)
    end

    self.m_switcherCoroutine = self:_StartCoroutine(function()

        local panelAnimWrapper = self.animationWrapper


        while self:_GetNextOrderIndex() > 0 do


            if self:IsHide() then
                self.m_switcherCoroutine = nil
                return
            end


            local missionShowData = self:_FetchNextOrderShowData()


            if not DeviceInfo.usingTouch then
                self.view.triggerTrackBtn.gameObject:SetActiveIfNecessary(false)
                self.view.trackCurrentDisplayMissionBtn.gameObject:SetActiveIfNecessary(true)
            else
                self.view.triggerTrackBtn.gameObject:SetActiveIfNecessary(not self.m_isOrderPlaying)
                self.view.trackCurrentDisplayMissionBtn.gameObject:SetActiveIfNecessary(self.m_isOrderPlaying)
            end

            if not missionShowData then
                break
            end

            while not (self:IsShow() and (panelAnimWrapper.curState ~= CS.Beyond.UI.UIConst.AnimationState.In or
                (panelAnimWrapper.curState == CS.Beyond.UI.UIConst.AnimationState.In and
                    missionShowData.availableInAnimationIn))) do
                coroutine.step()
            end
            self.view.leftNode.gameObject:SetActive(true)


            repeat

                if missionShowData.animType == MissionAnimType.Transparent then
                    break
                end

                if missionShowData.animType == MissionAnimType.ChapterStart then
                    missionCompletePopCtrl.MissionCompletePopCtrl.OnChapterStart({missionShowData.chapterId})
                    while missionCompletePopCtrl.MissionCompletePopCtrl.IsOpen() do

                        coroutine.step()
                    end

                    break
                end

                if missionShowData.animType == MissionAnimType.ChapterComplete then
                    missionCompletePopCtrl.MissionCompletePopCtrl.OnChapterCompleted({missionShowData.chapterId, missionShowData.chapterPanelTbc})
                    while missionCompletePopCtrl.MissionCompletePopCtrl.IsOpen() do

                        coroutine.step()
                    end

                    break
                end

                self:_InitMissionShowData(missionShowData)
                missionShowData.modifyContent = false
                local clipName
                if missionShowData.animType == MissionAnimType.New then

                    clipName = self.view.config.NEW_MISSION_CLIP_NAME
                elseif missionShowData.animType == MissionAnimType.Complete then

                    self:_TryRemoveMainHudActionQueueBlocker()
                    clipName = self.view.config.MISSION_COMPLETE_CLIP_NAME
                elseif missionShowData.animType == MissionAnimType.Track then

                    clipName = self.view.config.TRACK_MISSION_CLIP_NAME
                elseif missionShowData.animType == MissionAnimType.TrackOut then

                    clipName = self.view.config.TRACK_MISSION_OUT_CLIP_NAME
                end

                if clipName then
                    if missionShowData.animType == MissionAnimType.New then
                        AudioManager.PostEvent("Au_UI_Mission_New")
                    elseif missionShowData.animType == MissionAnimType.Complete then
                        AudioManager.PostEvent("Au_UI_Mission_Complete")
                    end

                    local animationWrapper = self.view.animationWrapper
                    local clipLength = animationWrapper:GetClipLength(clipName)
                    animationWrapper:SampleClipAtPercent(clipName, 0)
                    animationWrapper:PlayWithTween(clipName)
                    local startTime = Time.time
                    while Time.time - startTime <= clipLength do

                        self:_RefreshQuestContentIfNecessary(self.m_currentMissionShowData)
                        if missionShowData.skipAnimSignal then
                            break
                        end
                        coroutine.step()
                    end
                    self:_RefreshQuestContentIfNecessary(self.m_currentMissionShowData)
                    animationWrapper:SampleClipAtPercent(clipName, 1)

                    if missionShowData.animType == MissionAnimType.New then

                        clipName = self.view.config.NEW_MISSION_OUT_CLIP_NAME

                        startTime = Time.time
                        while Time.time - startTime <= self.view.config.NEW_ANIM_HOLD_TIME do
                            if missionShowData.skipAnimSignal then
                                self.view.trackButtonNode:PlayWithTween("missionhud_trackbutton_press")
                                local clickLength = self.view.trackButtonNode:GetClipLength("missionhud_trackbutton_press")
                                coroutine.wait(clickLength)
                                self.view.trackButtonNode:SampleClipAtPercent("missionhud_trackbutton_press", 1)
                                break
                            end
                            coroutine.step()
                        end
                        clipLength = animationWrapper:GetClipLength(clipName)
                        animationWrapper:SampleClipAtPercent(clipName, 0)
                        animationWrapper:PlayWithTween(clipName)
                        startTime = Time.time
                        local totalTime = clipLength
                        while Time.time - startTime <= totalTime do
                            if missionShowData.skipAnimSignal then
                                break
                            end
                            if self:IsHide() then
                                break
                            end
                            coroutine.step()
                        end
                        animationWrapper:SampleClipAtPercent(clipName, 1)
                    elseif missionShowData.animType == MissionAnimType.Complete then
                        self.m_blockedByMainHudActionQueueSys = true
                        logger.info(ELogChannel.Mission, string.format("[MissionHud] 加入自己阻塞器，MissionHud停止工作"))

                        LuaSystemManager.mainHudActionQueue:AddRequest("MissionHudResumeInfo", function()

                            logger.info(ELogChannel.Mission, string.format("[MissionHud] 移除自己阻塞器，MissionHud恢复工作"))
                            local missionHudOpen, missionHud = UIManager:IsOpen(PanelId.MissionHud)
                            if (not missionHudOpen) then
                                return
                            end
                            missionHud.m_blockedByMainHudActionQueueSys = false
                            missionHud:_TryStartSwitcherCoroutine()
                        end)

                    end

                    missionShowData.skipAnimSignal = false
                    break
                end



                local newQuestAnim = false
                for _, questShowData in pairs(missionShowData.questShowDataList) do
                    if questShowData.animType == QuestAnimType.New then
                        newQuestAnim = true
                    end
                end

                if newQuestAnim then
                    local skipNewQuestAnim = false
                    if self.m_missionSystem.trackMissionId ~= missionShowData.missionId then
                        local hasHudUpdateTag = false
                        for _, questShowData in pairs(missionShowData.questShowDataList) do
                            if questShowData.needHudUpdateTag then
                                hasHudUpdateTag = true
                            end
                        end
                        if not hasHudUpdateTag then
                            skipNewQuestAnim = true;
                        end
                    end
                    if not skipNewQuestAnim then




                        AudioManager.PostEvent("Au_UI_Mission_Step_Update")
                        AudioManager.PostEvent("Au_UI_Mission_Step_New")
                        local rootAnimationWrapper = self.view.animationWrapper
                        local newQuestClipLength = rootAnimationWrapper:GetClipLength(self.view.config.NEW_QUEST_CLIP_NAME)
                        rootAnimationWrapper:SampleClipAtPercent(self.view.config.NEW_QUEST_CLIP_NAME, 0)
                        rootAnimationWrapper:PlayWithTween(self.view.config.NEW_QUEST_CLIP_NAME)
                        local moveRightCurve = self.view.config.QUEST_MOVE_RIGHT_CURVE
                        local moveRightLength = CSUtils.GetAnimationCurveLength(moveRightCurve)
                        local questCellNewClipLength = self.view.questCell.animationWrapper:GetClipLength(self.view.config.QUEST_CELL_NEW_CLIP_NAME)
                        local totalTime = math.max(questCellNewClipLength, moveRightLength, newQuestClipLength)
                        local startTime = Time.time
                        local hasSkipAnim = false
                        local function processSkipAnim()
                            if missionShowData.skipAnimSignal and not hasSkipAnim then
                                hasSkipAnim = true
                                missionShowData.skipAnimSignal = false
                                self.view.trackButtonNode:PlayWithTween("missionhud_trackbutton_press")
                                local clickLength = self.view.trackButtonNode:GetClipLength("missionhud_trackbutton_press")
                                totalTime = Time.time + clickLength - startTime
                            end
                        end
                        while Time.time - startTime < totalTime do
                            self:_RefreshQuestContentIfNecessary(missionShowData)

                            local needHudUpdateTag = false
                            for _, questShowData in pairs(missionShowData.questShowDataList) do
                                if questShowData.needHudUpdateTag then
                                    needHudUpdateTag = true
                                end
                            end
                            self.view.updateTags.gameObject:SetActiveIfNecessary(needHudUpdateTag)
                            if self:IsHide() then
                                break
                            end
                            processSkipAnim()
                            for _, questShowData in pairs(missionShowData.questShowDataList) do
                                if questShowData.animType == QuestAnimType.New then
                                    local questCell = self:_GetQuestCell(questShowData.questId)
                                    if questCell then
                                        local verticalLayoutGroup = questCell.gameObject:GetComponent(typeof(CS.UnityEngine.UI.VerticalLayoutGroup))
                                        local questCellRectTransform = questCell.gameObject:GetComponent(typeof(CS.UnityEngine.RectTransform))
                                        verticalLayoutGroup.padding.left = self.view.config.QUEST_CELL_PADDING_LEFT
                                        LayoutRebuilder.MarkLayoutForRebuild(questCellRectTransform)
                                        local questCellWidth = questCellRectTransform.sizeDelta.x

                                        questCell.animationWrapper:SampleClip(self.view.config.QUEST_CELL_NEW_CLIP_NAME, math.min(Time.time - startTime, questCellNewClipLength))
                                        local ratio = moveRightCurve:Evaluate(math.min(Time.time - startTime, moveRightLength))
                                        verticalLayoutGroup.padding.left = math.floor(self.view.config.QUEST_CELL_PADDING_LEFT - questCellWidth * (1 - math.min(ratio, 1)))
                                        LayoutRebuilder.MarkLayoutForRebuild(questCellRectTransform)
                                        questCell.left.anchoredPosition = Vector2(-(questCellWidth * (1 - math.min(ratio, 1))), 0)
                                    end
                                end
                            end
                            coroutine.step()
                        end
                        self:_RefreshQuestContentIfNecessary(missionShowData)
                        rootAnimationWrapper:SampleClipAtPercent(self.view.config.NEW_QUEST_CLIP_NAME, 1)
                        for _, questShowData in pairs(missionShowData.questShowDataList) do
                            if questShowData.animType == QuestAnimType.New then
                                local questCell = self:_GetQuestCell(questShowData.questId)
                                local verticalLayoutGroup = questCell.gameObject:GetComponent(typeof(CS.UnityEngine.UI.VerticalLayoutGroup))
                                local questCellRectTransform = questCell.gameObject:GetComponent(typeof(CS.UnityEngine.RectTransform))
                                questCell.animationWrapper:SampleClipAtPercent(self.view.config.QUEST_CELL_NEW_CLIP_NAME, 1)
                                verticalLayoutGroup.padding.left = self.view.config.QUEST_CELL_PADDING_LEFT
                                LayoutRebuilder.MarkLayoutForRebuild(questCellRectTransform)
                                questCell.left.anchoredPosition = Vector2(0, 0)
                            end
                        end
                        if not hasSkipAnim then
                            if missionShowData.missionId ~= self.m_missionSystem.trackMissionId then

                                startTime = Time.time
                                totalTime = self.view.config.NEW_QUEST_ANIM_HOLD_TIME
                                while Time.time - startTime < totalTime do
                                    processSkipAnim()
                                    coroutine.step();
                                end
                            end
                        end
                        if not hasSkipAnim then

                            totalTime = rootAnimationWrapper:GetClipLength(self.view.config.NEW_QUEST_OUT_CLIP_NAME)
                            rootAnimationWrapper:SampleClipAtPercent(self.view.config.NEW_QUEST_OUT_CLIP_NAME, 0)
                            rootAnimationWrapper:PlayWithTween(self.view.config.NEW_QUEST_OUT_CLIP_NAME)
                            startTime = Time.time
                            while Time.time - startTime < totalTime do
                                processSkipAnim()
                                if self:IsHide() then
                                    break
                                end
                                coroutine.step()
                            end
                        end
                        rootAnimationWrapper:SampleClipAtPercent(self.view.config.NEW_QUEST_OUT_CLIP_NAME, 1)
                        self.view.trackButtonNode:SampleClipAtPercent("missionhud_trackbutton_press", 1)
                    end


                    break
                end


                for _, questShowData in pairs(missionShowData.questShowDataList) do
                    local questCell
                    if questShowData.animType == QuestAnimType.Complete then
                        questCell = self:_GetQuestCell(questShowData.questId)
                        if questCell then
                            AudioManager.PostEvent("Au_UI_Mission_Step_Complete")
                            questCell.animationWrapper:SampleClipAtPercent(self.view.config.QUEST_CELL_COMPLETE_CLIP_NAME, 0)
                            local clipLength = questCell.animationWrapper:GetClipLength(self.view.config.QUEST_CELL_COMPLETE_CLIP_NAME)
                            questCell.animationWrapper:PlayWithTween(self.view.config.QUEST_CELL_COMPLETE_CLIP_NAME)
                            coroutine.wait(clipLength)
                            questCell.animationWrapper:SampleClipAtPercent(self.view.config.QUEST_CELL_COMPLETE_CLIP_NAME, 1)
                            local verticalLayoutGroup = questCell.gameObject:GetComponent(typeof(CS.UnityEngine.UI.VerticalLayoutGroup))
                            local questCellRectTransform = questCell.gameObject:GetComponent(typeof(CS.UnityEngine.RectTransform))
                            local questCellHeight = questCellRectTransform.sizeDelta.y
                            local moveUpCurve = self.view.config.QUEST_MOVE_UP_CURVE
                            local moveUpLength = CSUtils.GetAnimationCurveLength(moveUpCurve)
                            local startTime = Time.time
                            while Time.time - startTime <= moveUpLength do
                                local ratio = moveUpCurve:Evaluate(math.min(Time.time - startTime, moveUpLength))
                                local moveLength = (questCellHeight + self.view.objContentLayout.spacing) * ratio
                                verticalLayoutGroup.padding.top = math.floor(QUEST_CELL_PADDING_TOP - moveLength)
                                LayoutRebuilder.MarkLayoutForRebuild(questCellRectTransform)
                                questCell.left.anchoredPosition = Vector2(0, moveLength)
                                if self:IsHide() then
                                    break
                                end
                                coroutine.step()
                            end
                            questCell.left.anchoredPosition = Vector2(0, 0)
                        end
                        break
                    else
                        for _, objectiveShowData in pairs(questShowData.objectiveShowDataList) do
                            local objectiveAnimInfo
                            if objectiveShowData.animType == ObjectiveAnimType.Complete then
                                objectiveAnimInfo = self:_GetObjectiveCellAnimInfo(0, objectiveShowData.questId, objectiveShowData.objectiveIdx)
                            elseif objectiveShowData.animType == ObjectiveAnimType.Rollback then
                                objectiveAnimInfo = self:_GetObjectiveCellAnimInfo(1, objectiveShowData.questId, objectiveShowData.objectiveIdx)
                            end
                            if objectiveAnimInfo then
                                local co = coroutine.create(animThread)
                                while coroutine.status(co) ~= "dead" do
                                    local status, _ = coroutine.resume(co, objectiveAnimInfo)
                                    if self:IsHide() then
                                        break
                                    end
                                    coroutine.step()
                                end
                                break
                            end
                        end
                    end
                end
            until true

            if self.m_isOrderPlaying then
                self.m_isOrderPlaying = false
                self.m_missionSystem:OnOrderFinish()
            end

            if self.m_blockedByMainHudActionQueueSys then
                self.m_switcherCoroutine = nil
                return
            end
        end


        self:_DisplayCurrentTrackingMission()
        self.m_switcherCoroutine = nil
        return

    end)
end

MissionHudCtrl._IsPlayingMissionAnim = HL.Method().Return(HL.Boolean) << function(self)
    return self.m_isOrderPlaying;
end

MissionHudCtrl._TryRemoveMainHudActionQueueBlocker = HL.Method() << function(self)
    if self.m_blockingMainHudActionQueueSys then
        LuaSystemManager.mainHudActionQueue:RemoveActionsOfType("MissionHudCompleteBlocker")
        logger.info(ELogChannel.Mission, string.format("[MissionHud] 移除了奖励队列阻塞器，奖励队列恢复工作"))
        self.m_blockingMainHudActionQueueSys = false
    end
end

MissionHudCtrl._ObjAnimThreadFunc = HL.Method(HL.Any) << function(self, animInfo)
    local animWrapper = animInfo.animWrapper
    local inClipName = animInfo.inClipName
    if not string.isEmpty(inClipName) then
        local clipLength = animWrapper:GetClipLength(inClipName)
        animWrapper:SampleClipAtPercent(inClipName, 0.0)
        animWrapper:PlayWithTween(inClipName)
        local startTime = Time.time
        while true do
            if self:IsHide() then
                break
            end
            if Time.time - startTime > clipLength then
                break
            end
            coroutine.yield()
        end
        animWrapper:SampleClipAtPercent(inClipName, 1.0)
    end
    local outClipName = animInfo.outClipName
    if not string.isEmpty(outClipName) then
        local clipLength = animWrapper:GetClipLength(outClipName)
        animWrapper:SampleClipAtPercent(outClipName, 0.0)
        animWrapper:PlayWithTween(outClipName)
        local startTime = Time.time
        while true do
            if self:IsHide() then
                break
            end
            if Time.time - startTime > clipLength then
                break
            end
            coroutine.yield()
        end
        animWrapper:SampleClipAtPercent(outClipName, 1.0)
    end
end

MissionHudCtrl._FetchNextOrderShowData = HL.Method().Return(HL.Any) << function(self)
    if self.m_missionSystem:MissionHudExclusiveMode() then
        local trackMissionId = self.m_missionSystem:GetTrackMissionId()
        if string.isEmpty(trackMissionId) then
            return nil
        end
        local ret = self.m_missionSystem:ApplyNextOrderByMissionId(trackMissionId)
        self.m_isOrderPlaying = (ret ~= nil)
        return ret
    end

    local ret = self.m_missionSystem:ApplyNextOrder()
    self.m_isOrderPlaying = (ret ~= nil)
    return ret
end

MissionHudCtrl._GetNextOrderIndex = HL.Method().Return(HL.Number) << function(self)
    if self.m_missionSystem:MissionHudExclusiveMode() then
        local trackMissionId = self.m_missionSystem:GetTrackMissionId()
        if string.isEmpty(trackMissionId) then
            return 0
        end
        local ret = self.m_missionSystem:GetNextOrderIndex(trackMissionId)
        return ret
    end

    local ret = self.m_missionSystem:GetNextOrderIndex("")
    return ret
end

MissionHudCtrl._InitMissionShowData = HL.Method(HL.Any) << function(self, missionShowData)
    self.m_currentMissionShowData = missionShowData
    self:_SetImportanceView(missionShowData)
    self.view.missionName.text = missionShowData.missionName:GetText()

    if DeviceInfo.usingTouch then
        self.view.trackCurrentDisplayMissionBtn.gameObject:SetActive(self.m_isOrderPlaying)
        self.view.triggerTrackBtn.gameObject:SetActive(not self.m_isOrderPlaying)
    else
        self.view.trackCurrentDisplayMissionBtn.gameObject:SetActiveIfNecessary(true)
    end

    if MissionTypeConfig[missionShowData.missionViewType] then
        self.view.missionIconBg:LoadSprite(UIConst.UI_SPRITE_MISSION_TYPE_ICON,
            MissionTypeConfig[missionShowData.missionViewType].missionDecoIcon)
    else
        self.view.missionIconBg.sprite = nil
    end

    self.view.newMissionTags.gameObject:SetActive(false)
    self.view.updateTags.gameObject:SetActive(false)

    self.view.finishGlow.gameObject:SetActive(false)
    self.view.trackButtonNode.gameObject:SetActive(false)

    self.view.missionIconBg.gameObject:SetActive(true)
    if missionShowData.animType == MissionAnimType.New then
        self.view.newMissionTags.gameObject:SetActive(true)
        self.view.trackButtonNode.gameObject:SetActive(true)
        self.view.missionIcon.gameObject:SetActive(true)
    elseif missionShowData.animType == MissionAnimType.Complete then
        self.view.finishGlow.gameObject:SetActive(true)
        self.view.missionIcon.gameObject:SetActive(true)
    end

    local animType = self:_GetMissionShowDataAnimType(missionShowData)

    if animType ~= MISSION_ANIM_TYPE.NewQuest and
        animType ~= MISSION_ANIM_TYPE.NewMission and
        animType ~= MISSION_ANIM_TYPE.CompleteMission then
        self.view.trackingIcon.gameObject:SetActive(true)
        self.view.missionIcon.gameObject:SetActive(false)
    else
        self.view.trackingIcon.gameObject:SetActive(false)
        self.view.missionIcon.gameObject:SetActive(true)
    end

    self.m_questCellCache:Refresh(missionShowData.questShowDataList.Count, function(questCell, questLuaIdx)
        local questIdx = CSIndex(questLuaIdx)
        local questShowData = missionShowData.questShowDataList[questIdx]
        if questShowData.animType == QuestAnimType.New then
            self.view.updateTags.gameObject:SetActive(true)
            self.view.trackButtonNode.gameObject:SetActive(true)
        end
        questCell.questId = questShowData.questId
        local verticalLayoutGroup = questCell.gameObject:GetComponent(typeof(CS.UnityEngine.UI.VerticalLayoutGroup))
        local questCellRectTransform = questCell.gameObject:GetComponent(typeof(CS.UnityEngine.RectTransform))
        verticalLayoutGroup.padding.top = QUEST_CELL_PADDING_TOP
        verticalLayoutGroup.padding.left = self.view.config.QUEST_CELL_PADDING_LEFT
        LayoutRebuilder.MarkLayoutForRebuild(questCellRectTransform)
        questCell.left.anchoredPosition = Vector2(0, 0)


        questCell.multiObjectiveDeco.gameObject:SetActive(questShowData.objectiveShowDataList.Count > 1)
        questCell.objectiveCellCache = questCell.objectiveCellCache or UIUtils.genCellCache(questCell.objectiveCell)
        questCell.objectiveCellCache:Refresh(questShowData.objectiveShowDataList.Count, function(objectiveCell, objectiveLuaIdx)
            local objectiveIdx = CSIndex(objectiveLuaIdx)
            local objectiveShowData = questShowData.objectiveShowDataList[objectiveIdx]
            objectiveCell.questId = questShowData.questId
            objectiveCell.objectiveIdx = objectiveIdx
            objectiveCell.questAnimState = questShowData.animType
            objectiveCell.missionAnimState = missionShowData.animType


            self:_UpdateObjectiveCellDesc(objectiveCell, objectiveShowData)

            self:_UpdateObjectiveCellProgress(objectiveCell, objectiveShowData)

            self:_UpdateObjectiveCellDistance(objectiveCell, objectiveShowData)

            self:_UpdateObjectiveCellComplete(objectiveCell, objectiveShowData)

            if objectiveShowData.isCompleted then
                objectiveCell.animationWrapper:SampleClipAtPercent(self.view.config.OBJECTIVE_CELL_COMPLETE_CLIP_NAME, 1)
            else
                objectiveCell.animationWrapper:SampleClipAtPercent(self.view.config.OBJECTIVE_CELL_COMPLETE_CLIP_NAME, 0)
            end
        end)
        questCell.animationWrapper:SampleClipAtPercent(self.view.config.QUEST_CELL_NEW_CLIP_NAME, 1)
    end)
    self.view.animationWrapper:SampleClipAtPercent(self.view.config.TRACK_MISSION_CLIP_NAME, 1)
    self:_UpdateAllObjectiveDistance()

    if self.m_lastRebuildFrame ~= CS.Beyond.DLogger.FrameCountThreadSafe then
        self:_RebuildLayoutAndAdaptByDevice()
    end
end

MissionHudCtrl._RefreshQuestContentIfNecessary = HL.Method(HL.Any) << function(self, missionShowData)
    if missionShowData ~= nil and missionShowData.modifyContent then
        missionShowData.modifyContent = false
        local animType = self:_GetMissionShowDataAnimType(missionShowData)
        if animType == MissionAnimType.New or animType == MISSION_ANIM_TYPE.NewQuest then
            self.m_modifyingContentDontShowProg = true
        end
        self:_RefreshQuestContent(missionShowData)
        self.m_modifyingContentDontShowProg = false
        self:_RebuildLayoutAndAdaptByDevice()
    end
end

MissionHudCtrl._RefreshQuestContent = HL.Method(HL.Any) << function(self, missionShowData)
    self.m_questCellCache:Refresh(missionShowData.questShowDataList.Count, function(questCell, questLuaIdx)
        local questIdx = CSIndex(questLuaIdx)
        local questShowData = missionShowData.questShowDataList[questIdx]
        if questShowData.animType == QuestAnimType.New then
            self.view.updateTags.gameObject:SetActive(true)
            self.view.trackButtonNode.gameObject:SetActive(true)
        end
        questCell.questId = questShowData.questId
        local verticalLayoutGroup = questCell.gameObject:GetComponent(typeof(CS.UnityEngine.UI.VerticalLayoutGroup))
        local questCellRectTransform = questCell.gameObject:GetComponent(typeof(CS.UnityEngine.RectTransform))
        verticalLayoutGroup.padding.top = QUEST_CELL_PADDING_TOP
        verticalLayoutGroup.padding.left = self.view.config.QUEST_CELL_PADDING_LEFT
        LayoutRebuilder.MarkLayoutForRebuild(questCellRectTransform)
        questCell.multiObjectiveDeco.gameObject:SetActive(questShowData.objectiveShowDataList.Count > 1)

        questCell.objectiveCellCache = questCell.objectiveCellCache or UIUtils.genCellCache(questCell.objectiveCell)
        questCell.objectiveCellCache:Refresh(questShowData.objectiveShowDataList.Count, function(objectiveCell, objectiveLuaIdx)
            local objectiveIdx = CSIndex(objectiveLuaIdx)
            local objectiveShowData = questShowData.objectiveShowDataList[objectiveIdx]
            objectiveCell.questId = questShowData.questId
            objectiveCell.objectiveIdx = objectiveIdx
            objectiveCell.questAnimState = questShowData.animType
            objectiveCell.missionAnimState = missionShowData.animType


            self:_UpdateObjectiveCellDesc(objectiveCell, objectiveShowData)

            self:_UpdateObjectiveCellProgress(objectiveCell, objectiveShowData)

            self:_UpdateObjectiveCellDistance(objectiveCell, objectiveShowData)

            self:_UpdateObjectiveCellComplete(objectiveCell, objectiveShowData)

            if objectiveShowData.isCompleted then
                objectiveCell.animationWrapper:SampleClipAtPercent(self.view.config.OBJECTIVE_CELL_COMPLETE_CLIP_NAME, 1)
            else
                objectiveCell.animationWrapper:SampleClipAtPercent(self.view.config.OBJECTIVE_CELL_COMPLETE_CLIP_NAME, 0)
            end
        end)
        questCell.animationWrapper:SampleClipAtPercent(self.view.config.QUEST_CELL_NEW_CLIP_NAME, 1)
    end)

    self:_RebuildLayoutAndAdaptByDevice()
end

MissionHudCtrl._OnQuestObjectiveUpdate = HL.Method(HL.Any) << function(self, arg)

    if (not self.m_currentMissionShowData) then
        return
    end

    local questId = unpack(arg)
    local questCellCount = self.m_questCellCache:GetCount()
    for questIdx = 1, questCellCount do
        local questCell = self.m_questCellCache:GetItem(questIdx)
        local questInfo = self.m_missionSystem:GetQuestInfo(questCell.questId)
        local objectiveCellCount = questCell.objectiveCellCache:GetCount()
        for objectiveIdx = 1, objectiveCellCount do
            local objectiveCell = questCell.objectiveCellCache:GetItem(objectiveIdx)
            local objective = questInfo.objectiveList[objectiveCell.objectiveIdx]
            if objective.isShowProgress then
                objectiveCell.progress.gameObject:SetActive(true)
                if objective.isCompleted then
                    objectiveCell.progress.text = string.format("%d/%d", objective.progressToCompareForShow, objective.progressToCompareForShow)
                else
                    objectiveCell.progress.text = string.format("%d/%d", objective.progressForShow, objective.progressToCompareForShow)
                end
            else
                objectiveCell.progress.gameObject:SetActive(false)
            end
        end
    end
end

MissionHudCtrl._GetMissionShowDataAnimType = HL.Method(HL.Any).Return(HL.Number) << function(self, missionShowData)
    if missionShowData.animType == MissionAnimType.New then
        return MISSION_ANIM_TYPE.NewMission
    elseif missionShowData.animType == MissionAnimType.Complete then
        return MISSION_ANIM_TYPE.CompleteMission
    elseif missionShowData.animType == MissionAnimType.Track then
        return MISSION_ANIM_TYPE.Track
    end
    for _, questShowData in pairs(missionShowData.questShowDataList) do
        if questShowData.animType == QuestAnimType.New then
            return MISSION_ANIM_TYPE.NewQuest
        elseif questShowData.animType == QuestAnimType.Complete then
            return MISSION_ANIM_TYPE.CompleteQuest
        end
        for _, objectiveShowData in pairs(questShowData.objectiveShowDataList) do
            if objectiveShowData.animType == ObjectiveAnimType.Complete then
                return MISSION_ANIM_TYPE.CompleteObjective
            elseif objectiveShowData.animType == ObjectiveAnimType.Rollback then
                return MISSION_ANIM_TYPE.RollbackObjective
            end
        end
    end
    return MISSION_ANIM_TYPE.None
end

MissionHudCtrl._SkipAnimationAndTrackMission = HL.Method() << function(self)
    if self:_IsPlayingMissionAnim() then
        local missionShowData = self.m_missionSystem:GetCurrentOrderShowData()
        if missionShowData then
            local animType = self:_GetMissionShowDataAnimType(missionShowData)
            if animType == MISSION_ANIM_TYPE.NewMission or animType == MISSION_ANIM_TYPE.NewQuest then
                self.m_currentMissionShowData.skipAnimSignal = true
                local toBeTrackedMissionId = missionShowData.missionId or ""
                if not string.isEmpty(toBeTrackedMissionId) then
                    self.m_missionSystem:TrackMission(toBeTrackedMissionId)
                end
            end
        end
    else


        if not DeviceInfo.usingTouch then
            self:_TriggerTrack()
        end
    end
end



MissionHudCtrl._OnScrollValueChanged = HL.Method(Vector2) << function(self, normalizedPosition)
    if not self.m_canScrollContent then
        return
    end

    local bottom = normalizedPosition.y < 0
    if bottom and self.m_showArrow then
        self.m_showArrow = false





        self.view.arrowBottomNode.gameObject:SetActiveIfNecessary(false)
    elseif not bottom and not self.m_showArrow then
        self.m_showArrow = true
        self.view.arrowBottomNode.gameObject:SetActiveIfNecessary(true)

    end
end

MissionHudCtrl._RebuildLayoutAndAdaptByDevice = HL.Method(HL.Opt(HL.Boolean)) << function(self, doNotReCalcWidth)
    self.m_lastRebuildFrame = CS.Beyond.DLogger.FrameCountThreadSafe
    if doNotReCalcWidth ~= true then
        self:_RebuildLayout()
    end

    local deviceAdapterParams = {
        headNode = self.view.missionHeadNode,
        objectiveContent = self.view.objectiveContent,
        contentScrollView = self.view.objectiveList,
        verticalLayoutGroup = self.view.leftNodeVerticalLayoutGroup,
        rectTransform = self.view.leftNode,
        arrowBottomNode = self.view.arrowBottomNode,
    }

    local ignoreScrollMask, scrollState, rectFoldHeight, rectUnfoldHeight = UIUtils.commonAdaptHudTrackV2(deviceAdapterParams,
        self.view.config)
    local canFold = scrollState ~= UIConst.TRACK_HUD_SCROLL_STATE.AlwaysCantScroll
    self.m_scrollState = scrollState
    self.m_rectFoldHeight = rectFoldHeight
    self.m_rectUnfoldHeight = rectUnfoldHeight
    self.m_canFold = canFold

    self.m_isFold = self.m_canFold and self.m_isFold or true
    self.m_canScrollContent = self:_GetCanScrollState()
    self.m_showArrow = self.m_canScrollContent

    self.view.objListNonDrawingGraphic.raycastTarget = self.m_canScrollContent

    local width = self.view.leftNode.rect.width
    local defaultRectHeight = self.m_isFold and rectFoldHeight or rectUnfoldHeight
    local percent = self.m_isFold and 1 or 0
    self.view.leftNode.sizeDelta = Vector2(width, defaultRectHeight)
    self.view.btnFoldAnimationWrapper:SampleClip(BTN_FOLD_ANIM_FOLD, percent)

    self.view.objectiveList:ScrollTo(Vector2(0, 1), true)
    self.view.objectiveList.enabled = self.m_canScrollContent

    self.view.objectiveListRectMask2D.enabled = not ignoreScrollMask

    self.view.btnFold.gameObject:SetActiveIfNecessary(self.m_canFold)

    self:_UpdateScrollContentWightsState(true)
end

MissionHudCtrl._RebuildLayout = HL.Method() << function(self)




    self.view.leftLayoutGroup.childForceExpandWidth = false
    LayoutRebuilder.ForceRebuildLayoutImmediate(self.view.objectiveContent.gameObject.transform)
    LayoutRebuilder.ForceRebuildLayoutImmediate(self.view.objectiveList.gameObject.transform)
    LayoutRebuilder.ForceRebuildLayoutImmediate(self.view.leftLayoutTransform)

    local log = ""

    local questCellCount = self.m_questCellCache:GetCount()
    local finalWidth = 0

    for questIdx = 1, questCellCount do
        local questCell = self.m_questCellCache:GetItem(questIdx)
        local questTransform = questCell.rectTransform
        local width = questTransform.sizeDelta.x
        log = log .. string.format("\n quest %f", width)
        local objectiveCellCount = questCell.objectiveCellCache:GetCount()
        for objectiveIdx = 1, objectiveCellCount do
            local objectiveCell = questCell.objectiveCellCache:GetItem(objectiveIdx)
            log = log .. string.format("\n    objective %f %s %f", objectiveCell.rectTransform.sizeDelta.x,
                objectiveCell.desc.text, objectiveCell.desc.rectTransform.sizeDelta.x)
        end
        finalWidth = math.max(finalWidth, width)
    end

    local headNodeTrans = self.view.missionHeadNode
    finalWidth = math.max(math.min(headNodeTrans.sizeDelta.x, self.view.objectiveContent.sizeDelta.x - self.view.objContentLayout.padding.left), finalWidth)
    log = log .. string.format("\n HEAD %f", headNodeTrans.sizeDelta.x)
    finalWidth = finalWidth + self.view.objContentLayout.padding.left

    local size = self.view.leftLayoutTransform.sizeDelta
    size.x = finalWidth
    self.view.leftLayoutTransform.sizeDelta = size

    log = string.format("[EnhancedMissionHudLog] Final %f\n", finalWidth) .. log


    self.view.leftLayoutGroup.childForceExpandWidth = true
    LayoutRebuilder.ForceRebuildLayoutImmediate(self.view.objectiveContent.gameObject.transform)
    LayoutRebuilder.ForceRebuildLayoutImmediate(self.view.objectiveList.gameObject.transform)
    LayoutRebuilder.ForceRebuildLayoutImmediate(self.view.leftLayoutTransform)

    local progressLayoutX = 0
    local maxQuestCellX = 0
    for questIdx = 1, questCellCount do
        local questCell = self.m_questCellCache:GetItem(questIdx)
        local questCellX = questCell.rectTransform.sizeDelta.x
        progressLayoutX = math.max(progressLayoutX, questCellX)
        maxQuestCellX = math.max(maxQuestCellX, questCellX)
    end

    for questIdx = 1, questCellCount do
        local questCell = self.m_questCellCache:GetItem(questIdx)
        local objectiveCellCount = questCell.objectiveCellCache:GetCount()
        for objectiveIdx = 1, objectiveCellCount do
            local objectiveCell = questCell.objectiveCellCache:GetItem(objectiveIdx)
            local pos = objectiveCell.progressRectTransform.anchoredPosition
            pos.x = progressLayoutX - self.view.config.QUEST_CELL_PADDING_LEFT
            objectiveCell.progressRectTransform.anchoredPosition = pos

            local completeBgSize = objectiveCell.completeSingleBg.sizeDelta
            completeBgSize.x = maxQuestCellX
            objectiveCell.completeSingleBg.sizeDelta = completeBgSize
        end
        LayoutRebuilder.ForceRebuildLayoutImmediate(questCell.gameObject.transform)
    end
    LayoutRebuilder.ForceRebuildLayoutImmediate(self.view.objectiveContent.gameObject.transform)
    LayoutRebuilder.ForceRebuildLayoutImmediate(self.view.objectiveList.gameObject.transform)
    LayoutRebuilder.ForceRebuildLayoutImmediate(self.view.leftLayoutTransform)

    for questIdx = 1, questCellCount do
        local questCell = self.m_questCellCache:GetItem(questIdx)
        if questCell.objectiveCellCache:GetCount() then
            local multiObjDecoSizeDelta = questCell.multiObjectiveDeco.sizeDelta
            local frontObjCellYSum = 0
            local objCount = questCell.objectiveCellCache:GetCount()
            for i = 1, objCount - 1 do
                local cell = questCell.objectiveCellCache:Get(i)
                frontObjCellYSum = frontObjCellYSum + cell.rectTransform.sizeDelta.y
            end

            multiObjDecoSizeDelta.y = frontObjCellYSum + self.view.config.MULTI_OBJ_DECO_Y_CORRECTION
            questCell.multiObjectiveDeco.sizeDelta = multiObjDecoSizeDelta
        end
    end
end

MissionHudCtrl._UpdateScrollContentWightsState = HL.Method(HL.Boolean) << function(self, ignoreAnim)
    self.view.scrollbarNode.gameObject:SetActiveIfNecessary(not self.m_isFold and self.m_scrollState == UIConst.TRACK_HUD_SCROLL_STATE.AlwaysCanScroll)




















    self.view.arrowBottomNode.gameObject:SetActiveIfNecessary(self.m_canScrollContent)
end

MissionHudCtrl._OnBtnFoldClick = HL.Method() << function(self)
    local config = self.view.config
    if self.m_isFold then

        self.view.btnFoldAnimationWrapper:Play(BTN_FOLD_ANIM_UNFOLD)
        self.m_isFold = false


        self.m_tween = DOTween.To(function()
            return self.view.leftNode.sizeDelta
        end, function(value)
            self.view.leftNode.sizeDelta = value
        end, Vector2(self.view.leftNode.sizeDelta.x, self.m_rectUnfoldHeight), config.FOLD_TIME):SetEase(config.FOLD_ANIM_CURVE)
    else

        self.view.btnFoldAnimationWrapper:Play(BTN_FOLD_ANIM_FOLD)
        self.m_isFold = true


        self.m_tween = DOTween.To(function()
            return self.view.leftNode.sizeDelta
        end, function(value)
            self.view.leftNode.sizeDelta = value
        end, Vector2(self.view.leftNode.sizeDelta.x, self.m_rectFoldHeight), config.UNFOLD_TIME):SetEase(config.UNFOLD_ANIM_CURVE)
    end


    self.m_canScrollContent = self:_GetCanScrollState()
    self.view.objectiveList:ScrollTo(Vector2(0, 1), true)
    self.view.objectiveList.enabled = self.m_canScrollContent

    self.view.objListNonDrawingGraphic.raycastTarget = self.m_canScrollContent

    self.m_tween:OnComplete(function()
        self:_UpdateScrollContentWightsState(false)
    end)
end

MissionHudCtrl._GetCanScrollState = HL.Method().Return(HL.Boolean) << function(self)
    return self.m_scrollState == UIConst.TRACK_HUD_SCROLL_STATE.AlwaysCanScroll and not self.m_isFold
end



MissionHudCtrl._TriggerTrack = HL.Method() << function(self)


    Notify(MessageConst.SHOW_MISSION_TRACKER)

    local trackMissionId = self.m_missionSystem:GetTrackMissionId()
    if string.isEmpty(trackMissionId) then
        return
    end


    local displayQuestIds = self.m_missionSystem:GetDisplayQuestIdsByMissionId(trackMissionId)
    if not displayQuestIds then
        return
    end

    for _, questId in pairs(displayQuestIds) do
        local questInfo = self.m_missionSystem:GetQuestInfo(questId)
        for _, objective in pairs(questInfo.objectiveList) do
            local _, _, trackAction, trackerId = self.m_missionSystem:GetObjectiveDistanceTextForMissionHud(objective)
            if trackAction == TrackAction.OpenMap then
                MapUtils.openMapByMissionId(trackerId)
                break
            elseif trackAction == TrackAction.Special then

                self.m_missionSystem:SpecialTracking(objective)
                break
            end
        end
    end
end

MissionHudCtrl.OnSyncAllMission = HL.Method() << function(self)
    self:_DisplayCurrentTrackingMission()
end

MissionHudCtrl._DisplayCurrentTrackingMission = HL.Method() << function(self)

    local trackMissionId = self.m_missionSystem.trackMissionId
    if not string.isEmpty(trackMissionId) then
        self.view.leftNode.gameObject:SetActive(true)

        local trackMissionShowData = self.m_missionSystem:GetMissionStableShowDataByMissionId(trackMissionId)
        self:_InitMissionShowData(trackMissionShowData)
    else
        self.m_currentMissionShowData = nil
        self.view.leftNode.gameObject:SetActive(false)
    end
end

MissionHudCtrl._SetImportanceView = HL.Method(HL.Any) << function(self, showData)
    local importance = showData.importance
    if importance == MissionImportance.High then
        self.view.importanceStateCtrl:SetState(IMPORTANCE_KEY_HIGH)
    end
    if importance == MissionImportance.Mid then
        self.view.importanceStateCtrl:SetState(IMPORTANCE_KEY_MID)
    end
    if importance == MissionImportance.Low then
        self.view.importanceStateCtrl:SetState(IMPORTANCE_KEY_LOW)
    end
end

MissionHudCtrl._UpdateObjectiveCellDistance = HL.Method(HL.Any, HL.Any) << function(self, objectiveCell, objectiveShowData)
    if (not string.isEmpty(objectiveShowData.distanceText)) and
        (not self:_IsPlayingMissionAnim()) and (not self.m_modifyingContentDontShowProg) then
        objectiveCell.distanceNode.gameObject:SetActive(true)
        objectiveCell.distance.text = objectiveShowData.distanceText
        objectiveCell.hotKeyIcon.gameObject:SetActive(objectiveShowData.needHotKeyIcon and not DeviceInfo.usingTouch)
    else
        objectiveCell.distanceNode.gameObject:SetActive(false)
    end
end

MissionHudCtrl._UpdateAllObjectiveDistance = HL.Method() << function(self)
    if self:_IsPlayingMissionAnim() then

        local questCellCount = self.m_questCellCache:GetCount()
        local currentHash = 0
        for questIdx = 1, questCellCount do
            local questCell = self.m_questCellCache:GetItem(questIdx)
            local objectiveCellCount = questCell.objectiveCellCache:GetCount()
            for objLuaIdx = 1, objectiveCellCount do
                local objectiveCell = questCell.objectiveCellCache:GetItem(objLuaIdx)
                objectiveCell.distanceNode.gameObject:SetActive(false)
                currentHash = currentHash * LEFT_HASH_BASE + LEFT_HASH_OBJ_NO_DIST_NODE
            end
        end
        if self.m_leftContentHash ~= currentHash then
            self.m_leftContentHash = currentHash
            self:_RebuildLayoutAndAdaptByDevice()
        end
        return
    end
    if not self.m_currentMissionShowData then
        return
    end

    local trackMissionId = self.m_missionSystem.trackMissionId
    if trackMissionId ~= self.m_currentMissionShowData.missionId then
        return
    end

    local hasHotKeyIcon = false
    local questCellCount = self.m_questCellCache:GetCount()

    local currentHash = 0
    for questIdx = 1, questCellCount do
        local questCell = self.m_questCellCache:GetItem(questIdx)
        local questInfo = self.m_missionSystem:GetQuestInfo(questCell.questId)
        local objectiveCellCount = questCell.objectiveCellCache:GetCount()
        for objLuaIdx = 1, objectiveCellCount do
            local objectiveCell = questCell.objectiveCellCache:GetItem(objLuaIdx)
            local objective = questInfo.objectiveList[CSIndex(objLuaIdx)]
            local missionSystem = self.m_missionSystem
            local objTextInfo = missionSystem:GetObjectiveDistanceTextForMissionHudWrap(objective)
            local distanceText, thisCellNeedHotKey = objTextInfo.distanceText, objTextInfo.needHotKeyIcon

            if hasHotKeyIcon or DeviceInfo.usingTouch then
                thisCellNeedHotKey = false
            end

            if thisCellNeedHotKey then
                hasHotKeyIcon = true
            end

            if not string.isEmpty(distanceText) then

                objectiveCell.distanceNode.gameObject:SetActiveIfNecessary(true)
                objectiveCell.distanceLayouter.gameObject:SetActiveIfNecessary(true)
                if objectiveCell.distance.text ~= distanceText then
                    objectiveCell.distance.text = distanceText
                end
                objectiveCell.hotKeyIcon.gameObject:SetActiveIfNecessary(thisCellNeedHotKey and not DeviceInfo.usingTouch)
                if thisCellNeedHotKey then
                    currentHash = currentHash * LEFT_HASH_BASE + LEFT_HASH_OBJ_BTN
                else
                    currentHash = currentHash * LEFT_HASH_BASE + LEFT_HASH_OBJ_DIST_NUMBER
                end
            else

                objectiveCell.distanceNode.gameObject:SetActiveIfNecessary(false)
                objectiveCell.distanceLayouter.gameObject:SetActiveIfNecessary(true)
                currentHash = currentHash * LEFT_HASH_BASE + LEFT_HASH_OBJ_NO_DIST_NODE
            end
        end
    end

    if self.m_leftContentHash ~= currentHash then
        self.m_leftContentHash = currentHash
        self:_RebuildLayoutAndAdaptByDevice()
    end
end

MissionHudCtrl._UpdateObjectiveCellDesc = HL.Method(HL.Any, HL.Any) << function(self, objectiveCell, objectiveShowData)
    local objectiveDesc
    if objectiveShowData.useStrDesc then
        objectiveDesc = objectiveShowData.descStr
    else
        if objectiveShowData.description.isEmpty then
            objectiveDesc = " "
        else
            objectiveDesc = objectiveShowData.description:GetText()
        end
    end

    if objectiveShowData.optional then
        local tempText = string.format("<color=#%s>%s</color> %s", OPTIONAL_TEXT_COLOR, Language.ui_optional_quest, objectiveDesc)
        objectiveCell.desc:SetAndResolveTextStyle(tempText)
    else
        objectiveCell.desc:SetAndResolveTextStyle(objectiveDesc)
    end
end

MissionHudCtrl._UpdateObjectiveCellProgress = HL.Method(HL.Any, HL.Any) << function(self, objectiveCell, objectiveShowData)

    if (not self.m_modifyingContentDontShowProg) and objectiveShowData.isShowProgress and
        objectiveCell.questAnimState ~= QuestAnimType.New and
        objectiveCell.missionAnimState ~= MissionAnimType.New then

        objectiveCell.progress.gameObject:SetActive(true)
        local left = self:_ProgNumToText(objectiveShowData.progress)
        local right = self:_ProgNumToText(objectiveShowData.progressToCompare)
        objectiveCell.progress.text = string.format("%s/%s", left, right)
    else
        objectiveCell.progress.gameObject:SetActive(false)
    end
end

MissionHudCtrl._ProgNumToText = HL.Method(HL.Any).Return(HL.Any) << function(self, num)
    if num < 10000 then
        return string.format("%d", num)
    end
    local curLang = CS.Beyond.I18n.I18nUtils.curEnvLang
    local isChineseStyleLang = curLang == GEnums.EnvLang.CN
    if isChineseStyleLang then
        local wan = num / 10000
        if wan < 1 then
            return string.format("%d", num)
        end
        if wan < 10000 then
            return self:_CombineProgToText(wan, Language.LUA_NUM_UNIT_WAN)
        end
        local yi = wan / 10000
        return self:_CombineProgToText(yi, Language.LUA_NUM_UNIT_YI)
    else
        local m = num / 1000 / 1000
        local k = num / 1000
        if m >= 1 then
            return self:_CombineProgToText(m, Language.LUA_NUM_UNIT_MILLION)
        elseif k >= 1 then
            return self:_CombineProgToText(k, Language.LUA_NUM_UNIT_THOUSAND)
        else
            return string.format("%d", num)
        end
    end
end

MissionHudCtrl._CombineProgToText = HL.Method(HL.Any, HL.Any).Return(HL.Any) << function(self, num, text)
    local carryFunc = function(x)

        return math.floor(x + 1e-10)
    end

    if num < 100 then
        if carryFunc(num * 10) % 10 > 0 then
            return string.format("%.1f%s", carryFunc(num * 10) / 10, text)
        end
    end

    return string.format("%d%s", carryFunc(num), text)
end

MissionHudCtrl._UpdateObjectiveCellComplete = HL.Method(HL.Any, HL.Any) << function(self, objectiveCell, objectiveShowData)
    if objectiveShowData.isCompleted then
        local clipName = self.view.config.OBJECTIVE_CELL_COMPLETE_CLIP_NAME
        objectiveCell.animationWrapper:SampleClipAtPercent(clipName, 1)
    else
        local clipName = self.view.config.OBJECTIVE_CELL_COMPLETE_CLIP_NAME
        objectiveCell.animationWrapper:SampleClipAtPercent(clipName, 0)
    end
end



MissionHudCtrl._GetObjectiveCellAnimInfo = HL.Method(HL.Number, HL.String, HL.Number).Return(HL.Table) << function(self, type, questId, objectiveCSIdx)
    local ret = {}
    local questCellCount = self.m_questCellCache:GetCount()
    for i = 1, questCellCount do
        local questCell = self.m_questCellCache:GetItem(i)
        if questCell.questId == questId then
            local objectiveCount = questCell.objectiveCellCache:GetCount()
            if objectiveCSIdx < objectiveCount then
                local objectiveCell = questCell.objectiveCellCache:GetItem(LuaIndex(objectiveCSIdx))
                ret.animWrapper = objectiveCell.animationWrapper
                if type == 0 then
                    ret.inClipName = self.view.config.OBJECTIVE_CELL_COMPLETE_CLIP_NAME
                else
                    ret.inClipName = self.view.config.OBJECTIVE_CELL_ROLLBACK_CLIP_NAME
                end
            end
            break
        end
    end
    return ret
end



MissionHudCtrl._GetQuestCell = HL.Method(HL.String).Return(HL.Any) << function(self, questId)
    local questCellCount = self.m_questCellCache:GetCount()
    for i = 1, questCellCount do
        local questCell = self.m_questCellCache:GetItem(i)
        if questCell.questId == questId then
            return questCell
        end
    end
    return nil
end

MissionHudCtrl._GetObjectiveCell = HL.Method(HL.String, HL.Number).Return(HL.Any) << function(self, questId, objectiveIdx)
    local questCellCount = self.m_questCellCache:GetCount()
    for i = 1, questCellCount do
        local questCell = self.m_questCellCache:GetItem(i)
        if questCell.questId == questId then
            local objectiveCellCount = questCell.objectiveCellCache:GetCount()
            for j = 1, objectiveCellCount do
                local objectiveCell = questCell.objectiveCellCache:GetItem(j)
                if objectiveCell.objectiveIdx == objectiveIdx then
                    return objectiveCell
                end
            end
        end
    end
    return nil
end

MissionHudCtrl._OnTogglePhaseForbid = HL.Method(HL.Opt(HL.Any)) << function(self, arg)
    self:_RefreshOpenBtn()
end

MissionHudCtrl._RefreshOpenBtn = HL.Method() << function(self)
    local showMissionBtn = not DeviceInfo.usingController and
        not PhaseManager:IsPhaseForbidden(PhaseId.Mission) and
        not self.m_missionSystem:MissionHudExclusiveMode() and
        not GameInstance.player.forbidSystem:IsForbidden(ForbidType.ForbidJumpToMissionPanelFromHud)
    self.view.openMissionUI.gameObject:SetActive(showMissionBtn)
end

MissionHudCtrl._OnTrackingSns = HL.Method(HL.Any) << function(self, args)
    local dialogId = unpack(args)
    PhaseManager:OpenPhase(PhaseId.SNS, {dialogId = dialogId})
end

MissionHudCtrl._ModifyBtnStateByFacTopView = HL.Method() << function(self)
    local isTopView = LuaSystemManager.factory.inTopView
    if isTopView then
        self.view.trackCurrentDisplayMissionBtn.gameObject:GetComponent("NonDrawingGraphic").raycastTarget = false
    else
        self.view.trackCurrentDisplayMissionBtn.gameObject:GetComponent("NonDrawingGraphic").raycastTarget = true
    end
end

MissionHudCtrl._OnFacTopViewChange = HL.Method(HL.Boolean) << function(self, useless)
    self:_ModifyBtnStateByFacTopView()
    self:_RefreshCloseBtn()
end

MissionHudCtrl._ChangeSkipBossBattleButtonState = HL.Method(HL.Table) << function(self, args)
    local show = unpack(args)
    MissionHudCtrl.m_skipBossBattleButtonVisible = show
    self:_HandleSkipBossBattleButton(show)
end

MissionHudCtrl._HandleSkipBossBattleButton = HL.Method(HL.Boolean) << function(self, show)
    if show then
        self.view.customLevelScriptNode.gameObject:SetActive(true)
        self.view.customLevelScriptNodeAnimationWrapper:PlayInAnimation()
        self.view.customLevelScriptBtn.onClick:RemoveAllListeners()
        self.view.customLevelScriptBtn.onClick:AddListener(function()
            self.m_customLevelScriptBtnClearScreenKey = UIManager:ClearScreen()
            Notify(MessageConst.SHOW_POP_UP, {
                content = Language.LUA_SKIP_BOSS_BATTLE_POPUP_TITLE,
                subContent = "",
                onConfirm = function()
                    if self.m_customLevelScriptBtnClearScreenKey ~= -1 then
                        UIManager:RecoverScreen(self.m_customLevelScriptBtnClearScreenKey)
                        self.m_customLevelScriptBtnClearScreenKey = -1
                    end

                    GameWorld.eventManager:RaiseLevelEvent(GameLevelEvent.ON_SKIP_BATTLE_POPUP_CONFIRM)
                    local battleId = GameWorld.battle.inFightBattleId
                    local curUtcTms = DateTimeUtils.GetCurrentTimestampByMilliseconds()
                    local disTime = 0
                    if curUtcTms >= battleId then
                        disTime = curUtcTms - battleId
                    end
                    EventLogManagerInst:GameEvent_SkipBossBattle(battleId, disTime)
                end,
                onCancel = function()
                    if self.m_customLevelScriptBtnClearScreenKey ~= -1 then
                        UIManager:RecoverScreen(self.m_customLevelScriptBtnClearScreenKey)
                        self.m_customLevelScriptBtnClearScreenKey = -1
                    end
                end,
                confirmText = Language.LUA_SKIP_BOSS_BATTLE_POPUP_CONFIRM_TEXT,
                cancelText = Language.LUA_SKIP_BOSS_BATTLE_POPUP_CANCEL_TEXT,
                freezeWorld = true,
                pauseGame = true,
            })
        end)
    else
        self.view.customLevelScriptBtn.onClick:RemoveAllListeners()
        self.view.customLevelScriptNodeAnimationWrapper:PlayOutAnimation(function()
            self.view.customLevelScriptNode.gameObject:SetActive(false)
        end)
    end
end

MissionHudCtrl.SkipMissionBtnAnimIn = HL.Method() << function(self)
    self.view.openMissionPanelBtnWrapper:SkipInAnimation()
end

MissionHudCtrl._RefreshCloseBtn = HL.Method() << function(self)
    if not DeviceInfo.usingTouch then
        return
    end

    local isTopView = LuaSystemManager.factory.inTopView
    if not isTopView then
        self.view.closeBtn.gameObject:SetActive(false)
        return
    end
    self.view.closeBtn.gameObject:SetActive(true)
end

MissionHudCtrl._ShrinkPanel = HL.Method() << function(self)
    UIManager:AutoOpen(PanelId.MissionHudMini, {needAnimationIn = true})
    UIManager:HideWithKey(PANEL_ID, "MiniPanel")
end

MissionHudCtrl._OnMissionStateChange = HL.Method(HL.Any) << function(self, args)
    if not self.m_missionStateChangeSignal then
        self.m_missionStateChangeSignal = {
            time = Time.unscaledTime
        }
    end
end

MissionHudCtrl._OnTrackingToUI = HL.Method(HL.Any) << function(self, args)
    local jumpId = unpack(args)
    Utils.jumpToSystem(jumpId)
end

MissionHudCtrl._OnExclusiveModeChange = HL.Method() << function(self, args)
    self:_RefreshOpenBtn()
    self:_TryStartSwitcherCoroutine()
end

MissionHudCtrl._OnOpenBtnAvailableChange = HL.Method() << function(self, args)
    self:_RefreshOpenBtn()
end

MissionHudCtrl.OnShow = HL.Override() << function(self)
    self:_TryStartSwitcherCoroutine()
    if self.m_lastRebuildFrame ~= CS.Beyond.DLogger.FrameCountThreadSafe then
        self:_RebuildLayoutAndAdaptByDevice()
    end
    self:_RefreshCloseBtn()
    self.view.openMissionPanelBtnWrapper:ClearTween()
    self.view.openMissionPanelBtnWrapper:PlayInAnimation()
end

MissionHudCtrl.OnClose = HL.Override() << function(self)
    self.m_missionSystem:DisableMissionTrackData()
    self:_TryRemoveMainHudActionQueueBlocker()
    LuaSystemManager.mainHudActionQueue:RemoveActionsOfType("MissionHudResumeInfo")
    self.m_blockedByMainHudActionQueueSys = false
    self.m_blockingMainHudActionQueueSys = false
    self.m_currentMissionShowData = nil
    if self.m_questCellCache then
        self.m_questCellCache:Refresh(0)
    end
    self.view.leftNode.gameObject:SetActive(false)
    if self.m_updateDistanceTimerHandler then
        LuaUpdate:Remove(self.m_updateDistanceTimerHandler)
        self.m_updateDistanceTimerHandler = nil
    end
end

MissionHudCtrl.ExtractHotSwitchRuntimeState = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    if not self.m_questCellCache then
        return nil
    end
    local questCellCache = self.m_questCellCache
    self.m_questCellCache = nil
    return {
        questCellCache = questCellCache,
    }
end

MissionHudCtrl.RestoreHotSwitchRuntimeState = HL.Override(HL.Opt(HL.Any)) << function(self, state)
    if not state then
        return
    end
    self.m_questCellCache = state.questCellCache
end

MissionHudCtrl.PlayAnimationOutWithCallback = HL.Override(HL.Opt(HL.Function)) << function(self, action)
    MissionHudCtrl.Super.PlayAnimationOutWithCallback(self, action)
    self.view.openMissionPanelBtnWrapper:ClearTween()
    self.view.openMissionPanelBtnWrapper:PlayOutAnimation()
end

HL.Commit(MissionHudCtrl)
