local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ActivityImportantPopup
local PHASE_ID = PhaseId.ActivityImportantPopup

ActivityImportantPopupCtrl = HL.Class('ActivityImportantPopupCtrl', uiCtrl.UICtrl)

ActivityImportantPopupCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_ACTIVITY_CHECK_IN] = '_OnActivityCheckIn',
    [MessageConst.ON_ACTIVITY_UPDATED] = '_OnActivityUpdated',
}

ActivityImportantPopupCtrl.m_requestKey = HL.Field(HL.String) << ""
ActivityImportantPopupCtrl.m_activityId = HL.Field(HL.String) << ""
ActivityImportantPopupCtrl.m_jumpId = HL.Field(HL.String) << ""
ActivityImportantPopupCtrl.m_autoClaimActivityIds = HL.Field(HL.Table)
ActivityImportantPopupCtrl.m_isFirstDayRewardClaimed = HL.Field(HL.Boolean) << false
ActivityImportantPopupCtrl.m_videoStartTimestamp = HL.Field(HL.Number) << 0
ActivityImportantPopupCtrl.m_monthRewardsCellCache = HL.Field(HL.Forward("UIListCache"))
ActivityImportantPopupCtrl.m_otherActRewardsCellCache = HL.Field(HL.Forward("UIListCache"))
ActivityImportantPopupCtrl.m_firstDayRewardItems = HL.Field(HL.Table)
ActivityImportantPopupCtrl.m_loopAudioPlayingId = HL.Field(HL.Number) << 0
ActivityImportantPopupCtrl.m_introAudioPlayingId = HL.Field(HL.Number) << 0
ActivityImportantPopupCtrl.m_audioStartUnscaledTime = HL.Field(HL.Number) << 0
ActivityImportantPopupCtrl.m_audioSynchronizerUpdateKey = HL.Field(HL.Number) << -1
ActivityImportantPopupCtrl.m_isPlayingIntro = HL.Field(HL.Boolean) << true
ActivityImportantPopupCtrl.m_currentAudioKey = HL.Field(HL.String) << ""

ActivityImportantPopupCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_requestKey = arg.requestKey
    self.m_activityId = ActivityUtils.findImportantCheckinActivity() or ""
    local cellTemplate = self.view.jumpNode.activityImportantItemCell
    self.m_monthRewardsCellCache = UIUtils.genCellCache(cellTemplate, nil, self.view.jumpNode.monthRewardsNode)
    self.m_otherActRewardsCellCache = UIUtils.genCellCache(cellTemplate, nil, self.view.jumpNode.otherActRewardsContent)

    local _, info = Tables.checkInInfoTable:TryGetValue(self.m_activityId)
    self.m_jumpId = info and info.jumpId or ""

    self:_ParseAutoClaimActivityIds()
    self:_CacheFirstDayRewardItems()
    self:_SetupButtons()
    self:_InitJumpNodeFirstDayReward()

    local videoAspectRatio
    if Utils.checkIsPSDevice() then
        videoAspectRatio = self.view.config.VIDEO_ASPECT_RATIO_PS5
    elseif DeviceInfo.isMobile then
        videoAspectRatio = self.view.config.VIDEO_ASPECT_RATIO_MOBILE
    else
        videoAspectRatio = self.view.config.VIDEO_ASPECT_RATIO_PC
    end
    if videoAspectRatio then
        self.view.introVideoPlayer.view.aspectRatioFitter.aspectRatio = videoAspectRatio
        self.view.loopVideoPlayer.view.aspectRatioFitter.aspectRatio = videoAspectRatio
    end

    local activity = GameInstance.player.activitySystem:GetActivity(self.m_activityId)
    if not activity then
        logger.error("ActivityImportantPopupCtrl: activity not found", self.m_activityId)
        return
    end

    local rewardDaysSet = self:_GetRewardDaysSet(activity)
    if rewardDaysSet[1] then
        self.m_isFirstDayRewardClaimed = true
        self:_ShowJumpStage()
    else
        self:_ShowFirstDayStage()
    end

    if DeviceInfo.usingController then
        self.view.jumpNode.rewardNaviNode.getDefaultSelectableFunc = function()
            if self.m_monthRewardsCellCache:GetCount() > 0 then
                local cell = self.m_monthRewardsCellCache:GetItem(1)
                if cell then
                    return cell.view.selectBtn
                end
            end
            if self.m_otherActRewardsCellCache:GetCount() > 0 then
                local cell = self.m_otherActRewardsCellCache:GetItem(1)
                if cell then
                    return cell.view.selectBtn
                end
            end
            return nil
        end
        self.view.jumpNode.rewardNaviNode.onIsFocusedChange:AddListener(function(isFocused)
            if not isFocused then
                Notify(MessageConst.HIDE_ITEM_TIPS)
            end
        end)
    end
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})

    self.m_audioSynchronizerUpdateKey = self:_StartUpdate(function()
        self:_SyncAudio()
    end)
end

ActivityImportantPopupCtrl._CacheFirstDayRewardItems = HL.Method() << function(self)
    self.m_firstDayRewardItems = {}
    local rewardData = Tables.CheckInRewardTable[self.m_activityId]
    if rewardData and rewardData.stageList and rewardData.stageList.Count > 0 then
        local firstDayReward = rewardData.stageList[CSIndex(1)]
        if firstDayReward then
            self.m_firstDayRewardItems = UIUtils.getRewardItems(firstDayReward.rewardId)
        end
    end
end

ActivityImportantPopupCtrl._ParseAutoClaimActivityIds = HL.Method() << function(self)
    self.m_autoClaimActivityIds = {}
    local _, info = Tables.checkInInfoTable:TryGetValue(self.m_activityId)
    if info and info.autoClaimActivityIds then
        for _, id in pairs(info.autoClaimActivityIds) do
            table.insert(self.m_autoClaimActivityIds, id)
        end
    end
end

ActivityImportantPopupCtrl._SetupButtons = HL.Method() << function(self)
    self.view.getFirstDayRewardBtn.onClick:AddListener(function()
        self:_OnClickGetFirstDayReward()
    end)
    self.view.jumpNode.gachaBtn.onClick:AddListener(function()
        self:_OnJumpToTarget()
    end)
    self.view.jumpNode.closeBtn.onClick:AddListener(function()
        self:_OnClickClose()
    end)
    self.view.getFirstDayRewardBtn.gameObject:SetActive(false)
end

ActivityImportantPopupCtrl._ShowFirstDayStage = HL.Method() << function(self)
    self.view.firstDayNode.gameObject:SetActive(true)
    self.view.jumpNode.gameObject:SetActive(false)
    self.view.firstDayNode:SetState("Normal")

    local firstDayRewardItem = self.view.firstDayRewardNode.firstDayRewardItem
    if #self.m_firstDayRewardItems > 0 then
        firstDayRewardItem.itemIcon:InitItemIcon(self.m_firstDayRewardItems[1].id)
        firstDayRewardItem.countTxt.text = self.m_firstDayRewardItems[1].count
    end

    self.view.firstDayRewardNode.gameObject:SetActive(false)
    self:_InitVideos()
end

ActivityImportantPopupCtrl._ShowJumpStage = HL.Method() << function(self)


    self.view.jumpNode.btnNode:SetState("Btn")
    self:_RefreshTimeTxt()
    self:_RefreshOtherActRewards()
end

ActivityImportantPopupCtrl._RefreshTimeTxt = HL.Method() << function(self)
    local activity = GameInstance.player.activitySystem:GetActivity(self.m_activityId)
    if activity then
        local remainSec = activity.endTime - DateTimeUtils.GetCurrentTimestampBySeconds()
        self.view.jumpNode.timeTxt.text = UIUtils.getLeftTime(math.max(0, remainSec))
    end
end

ActivityImportantPopupCtrl._InitJumpNodeFirstDayReward = HL.Method() << function(self)
    local cell = self.view.jumpNode.firstDayRewardNode.activityImportantItemCell
    if #self.m_firstDayRewardItems > 0 then
        cell:InitItem(self.m_firstDayRewardItems[1], true)
    end
end

ActivityImportantPopupCtrl._RefreshOtherActRewards = HL.Method() << function(self)
    self:_RefreshMonthlyRewards()
    self:_RefreshCheckinRewards()
end

ActivityImportantPopupCtrl._RefreshMonthlyRewards = HL.Method() << function(self)
    local monthItems = {}
    local monthlyPassSystem = GameInstance.player.monthlyPassSystem
    local needShowTimeStamps = monthlyPassSystem:GetNeedShowDailyPopupTimestamps()
    if needShowTimeStamps.Count > 0 then
        local dailyRewardInfoList = CashShopUtils.GetMonthlyPassDailyRewardInfoList()
        for _, info in ipairs(dailyRewardInfoList) do
            table.insert(monthItems, { id = info.rewardId, count = info.number })
        end
    end

    local monthNode = self.view.jumpNode.monthRewardsNode
    if #monthItems > 0 then
        monthNode.gameObject:SetActive(true)
        self.m_monthRewardsCellCache:Refresh(#monthItems, function(cell, index)
            cell:InitItem(monthItems[index])
        end)
        self.view.jumpNode.rewardNode:SetState("Month")
    else
        monthNode.gameObject:SetActive(false)
        self.m_monthRewardsCellCache:Refresh(0)
        self.view.jumpNode.rewardNode:SetState("CheckIn")
    end
end

ActivityImportantPopupCtrl._RefreshCheckinRewards = HL.Method() << function(self)
    local checkinItems = {}
    for _, claimId in ipairs(self.m_autoClaimActivityIds) do
        local claimActivity = GameInstance.player.activitySystem:GetActivity(claimId)
        if claimActivity then
            local unclaimedDays = ActivityUtils.getUnclaimedDaysForCheckin(claimId)
            if #unclaimedDays > 0 then
                local rewardData = Tables.CheckInRewardTable[claimId]
                if rewardData and rewardData.stageList then
                    for _, day in ipairs(unclaimedDays) do
                        if rewardData.stageList.Count >= day then
                            local stageData = rewardData.stageList[CSIndex(day)]
                            local items = UIUtils.getRewardItems(stageData.rewardId)
                            for _, item in ipairs(items) do
                                table.insert(checkinItems, item)
                            end
                        end
                    end
                end
            end
        end
    end

    local otherActRewards = self.view.jumpNode.otherActRewards
    if #checkinItems > 0 then
        otherActRewards.gameObject:SetActive(true)
        self.m_otherActRewardsCellCache:Refresh(#checkinItems, function(cell, index)
            cell:InitItem(checkinItems[index])
        end)
    else
        otherActRewards.gameObject:SetActive(false)
        self.m_otherActRewardsCellCache:Refresh(0)
    end
end

ActivityImportantPopupCtrl._SetAllRewardCellsState = HL.Method(HL.Function) << function(self, callback)
    local callbackCell
    for i = 1, self.m_monthRewardsCellCache:GetCount() do
        local cell = self.m_monthRewardsCellCache:GetItem(i)
        if cell then
            cell:SetDoneState()
        end
    end
    for i = 1, self.m_otherActRewardsCellCache:GetCount() do
        local cell = self.m_otherActRewardsCellCache:GetItem(i)
        if cell then
            if not callbackCell then
                callbackCell = cell
                cell:SetDoneState(function()
                    callback()
                end)
            else
                cell:SetDoneState()
            end
        end
    end
    if not callbackCell then
        callback()
    end
end

ActivityImportantPopupCtrl._OnClickGetFirstDayReward = HL.Method() << function(self)
    if self.m_isFirstDayRewardClaimed then
        return
    end
    local activity = GameInstance.player.activitySystem:GetActivity(self.m_activityId)
    if not activity then
        return
    end
    if activity.loginDays < 1 then
        return
    end
    activity:GainReward({ 1 })
    self:_RefreshOtherActRewards()

end

ActivityImportantPopupCtrl._OnActivityCheckIn = HL.Method(HL.Table) << function(self, args)
    local id = unpack(args)
    if id == self.m_activityId then
        self.m_isFirstDayRewardClaimed = true
        self.view.firstDayNode:SetState("Receive")
        self.view.getFirstDayRewardBtn.gameObject:SetActive(false)
        self:_ShowJumpStage()
        if self.view.config.CHECK_IN_AUDIO_DONE_KEY then
            AudioAdapter.PostEvent(self.view.config.CHECK_IN_AUDIO_DONE_KEY)
        end
        self.view.animationWrapper:PlayWithTween("activityimportant_done", function()
            self:_SwitchToJumpStage()
        end)
    end
end

ActivityImportantPopupCtrl._SwitchToJumpStage = HL.Method() << function(self)
    self:_StopIntroVideos()
end

ActivityImportantPopupCtrl._OnActivityUpdated = HL.Method(HL.Table) << function(self, args)
    local id = unpack(args)
    if id == self.m_activityId and not GameInstance.player.activitySystem:GetActivity(id) then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_ACTIVITY_FORBIDDEN)
        self:_OnClickClose()
    end
end

ActivityImportantPopupCtrl._OnJumpToTarget = HL.Method() << function(self)
    if string.isEmpty(self.m_jumpId) then
        logger.error("ActivityImportantPopupCtrl: jumpId is empty", self.m_activityId)
        return
    end
    self.view.jumpNode.closeBtn.gameObject:SetActive(false)
    self.view.jumpNode.rewardNaviNode.enabled = false
    self.view.raycastMask.gameObject:SetActive(true)
    EventLogManagerInst:GameEvent_UIClick("sign_gacha")
    self:_AutoClaimMonthlyPass()
    self:_AutoClaimCheckinRewards()
    self:_CleanupMainHudActionQueue()
    self.view.jumpNode.btnNode:SetState("Receive")
    local needPlay = true
    self:_SetAllRewardCellsState(function()
        self.view.animationWrapper:PlayOutAnimation(function()
            if not needPlay then
                return
            end
            needPlay = false
            local requestKey = self.m_requestKey
            self:_StopVideos()
            Utils.jumpToSystem(self.m_jumpId, function()
                PhaseManager:ExitPhaseFast(PHASE_ID)
                Notify(MessageConst.ON_ONE_MAIN_HUD_ACTION_FINISHED, requestKey)
            end)
        end)
    end)
end

ActivityImportantPopupCtrl._AutoClaimMonthlyPass = HL.Method() << function(self)
    local monthlyPassSystem = GameInstance.player.monthlyPassSystem
    local needShowTimeStamps = monthlyPassSystem:GetNeedShowDailyPopupTimestamps()
    for _, ts in pairs(needShowTimeStamps) do
        if monthlyPassSystem:CheckIsValidShowTimeStamp(ts) then
            monthlyPassSystem:SendConfirm(ts)
        end
    end
end

ActivityImportantPopupCtrl._AutoClaimCheckinRewards = HL.Method() << function(self)
    for _, claimId in ipairs(self.m_autoClaimActivityIds) do
        local claimActivity = GameInstance.player.activitySystem:GetActivity(claimId)
        if claimActivity then
            local unclaimedDays = ActivityUtils.getUnclaimedDaysForCheckin(claimId)
            if #unclaimedDays > 0 then
                claimActivity:GainReward(unclaimedDays)
            end
        end
    end
end

ActivityImportantPopupCtrl._CleanupMainHudActionQueue = HL.Method() << function(self)
    local q = LuaSystemManager.mainHudActionQueue
    q:RemoveActionsOfType("LoginCheck_MonthlypassPopup")
    q:RemoveActionsOfType("MonthlyPassPopup")





    if q:HasRequest("LoginCheck_CashShopOrderSettle") then
        q:RemoveActionsOfType("LoginCheck_CashShopOrderSettle")
        local PhaseLevel = require_ex('Phase/Level/PhaseLevel').PhaseLevel
        PhaseLevel.s_LoginCheckFinishedInfo.CashShopOrderSettleDeferredToGachaPool = true
    end
end

ActivityImportantPopupCtrl._InitVideos = HL.Method() << function(self)
    local introVideoKey = self.view.config.INTRO_VIDEO_KEY
    local loopVideoKey = self.view.config.LOOP_VIDEO_KEY
    local introExist, introPath = UIUtils.getUIVideoFullPath(introVideoKey)
    local loopExist, loopPath = UIUtils.getUIVideoFullPath(loopVideoKey)
    if not introExist then
        logger.error("ActivityImportantPopupCtrl: intro video not found", introVideoKey)
        self.view.firstDayRewardNode.gameObject:SetActive(true)
        return
    end

    self.view.introMask.gameObject:SetActive(true)
    self.view.introVideoPlayer.view.canvasGroup.alpha = 0
    self.view.loopVideoPlayer.view.canvasGroup.alpha = 0

    local onIntroVideoError = function(state)
        logger.error("ActivityImportantPopupCtrl: intro video error", introPath, state)
        self.view.introMask.gameObject:SetActive(false)
        self.m_isPlayingIntro = false
        self:_OnIntroVideoFinished()
    end

    self.m_videoStartTimestamp = DateTimeUtils.GetCurrentTimestampBySeconds()
    self.view.introVideoPlayer:PlayVideo(introPath, function()
        self.view.introVideoPlayer.view.canvasGroup.alpha = 1
        self.view.introMask.gameObject:SetActive(false)
        self.m_isPlayingIntro = true
        local introAudioKey = self.view.config.INTRO_VIDEO_AUDIO_KEY
        self.m_currentAudioKey = introAudioKey
        self.m_introAudioPlayingId = AudioAdapter.PostEvent(introAudioKey)
        self.m_audioStartUnscaledTime = Time.unscaledTime
        if loopExist then
            self.view.loopVideoPlayer:PreloadVideo(loopPath, nil, function(state)
                logger.error("ActivityImportantPopupCtrl: loop video preload error", loopPath, state)
                self.view.loopVideoPlayer:StopVideo()
            end)
        end
    end, function()
        self:_OnIntroVideoFinished()
    end, function(state)
        onIntroVideoError(state)
    end)
end

ActivityImportantPopupCtrl._OnIntroVideoFinished = HL.Method() << function(self)
    EventLogManagerInst:GameEvent_ActingEnd(self.view.config.INTRO_VIDEO_KEY, CS.Beyond.SDK.EventLogDefine.EventLogActingType.CutScene, true, self.m_videoStartTimestamp, false, false, false)
    self.view.introVideoPlayer.view.image.raycastTarget = false
    self.view.introVideoPlayer:StopVideo()
    self.view.introVideoPlayer.gameObject:SetActive(false)
    local loopExist, loopPath = UIUtils.getUIVideoFullPath(self.view.config.LOOP_VIDEO_KEY)
    if loopExist then
        self.view.loopVideoPlayer:PlayVideo(loopPath, function()
            self.m_isPlayingIntro = false
            local loopAudioKey = self.view.config.LOOP_VIDEO_AUDIO_KEY
            self.m_currentAudioKey = loopAudioKey
            self.m_loopAudioPlayingId = AudioAdapter.PostEvent(loopAudioKey)
            self.m_audioStartUnscaledTime = Time.unscaledTime
        end, nil, function(state)
            logger.error("ActivityImportantPopupCtrl: loop video error", loopPath, state)
            self.view.loopVideoPlayer:StopVideo()
        end)
        self.view.loopVideoPlayer.view.canvasGroup.alpha = 1
    end

    self.view.firstDayRewardNode.gameObject:SetActive(true)
    self.view.getFirstDayRewardBtn.gameObject:SetActive(true)
    if self.view.config.CHECK_IN_AUDIO_IN_KEY then
        AudioAdapter.PostEvent(self.view.config.CHECK_IN_AUDIO_IN_KEY)
    end
    self.view.animationWrapper:PlayWithTween("activityimportant_in_part_1")
end

ActivityImportantPopupCtrl._StopVideos = HL.Method() << function(self)
    self:_StopIntroVideos()
    self:_StopAllAudio()
    self.view.loopVideoPlayer:StopVideo(true)
    self.view.loopVideoPlayer.gameObject:SetActive(false)
end

ActivityImportantPopupCtrl._StopIntroVideos = HL.Method() << function(self)
    self.view.introVideoPlayer:StopVideo(true)
    self.view.introVideoPlayer.gameObject:SetActive(false)
end

ActivityImportantPopupCtrl._StopAllAudio = HL.Method() << function(self)
    if self.m_loopAudioPlayingId ~= 0 then
        AudioAdapter.StopByPlayingId(self.m_loopAudioPlayingId, 2000)
        self.m_loopAudioPlayingId = 0
    end
    if self.m_introAudioPlayingId ~= 0 then
        AudioAdapter.StopByPlayingId(self.m_introAudioPlayingId, 2000)
        self.m_introAudioPlayingId = 0
    end
end



local AUDIO_VIDEO_SEEK_THRESHOLD = 0.7
ActivityImportantPopupCtrl._SyncAudio = HL.Method() << function(self)
    local audioPlayingId = self.m_isPlayingIntro and self.m_introAudioPlayingId or self.m_loopAudioPlayingId
    if audioPlayingId == 0 then
        return
    end
    local videoPlayer = self.m_isPlayingIntro and self.view.introVideoPlayer or self.view.loopVideoPlayer
    local videoPos = videoPlayer:GetTime()
    if videoPos < 0 then
        return
    end
    local audioUnscaledPlayedTime = Time.unscaledTime - self.m_audioStartUnscaledTime
    if math.abs(audioUnscaledPlayedTime - videoPos) > AUDIO_VIDEO_SEEK_THRESHOLD then
        local seekTimeMs = math.ceil(videoPos * 1000)
        AudioAdapter.SeekOnEvent(self.m_currentAudioKey, seekTimeMs, false, audioPlayingId)
        self.m_audioStartUnscaledTime = Time.unscaledTime - videoPos
    end
end

ActivityImportantPopupCtrl._OnClickClose = HL.Method() << function(self)
    PhaseManager:PopPhase(PHASE_ID)
    Notify(MessageConst.ON_ONE_MAIN_HUD_ACTION_FINISHED, self.m_requestKey)
end

ActivityImportantPopupCtrl.OnClose = HL.Override() << function(self)
    self.m_audioSynchronizerUpdateKey = self:_RemoveUpdate(self.m_audioSynchronizerUpdateKey)
    self:_StopVideos()
end

ActivityImportantPopupCtrl._GetRewardDaysSet = HL.Method(HL.Any).Return(HL.Table) << function(self, activity)
    local set = {}
    for i = 1, activity.rewardDays.Count do
        set[activity.rewardDays[CSIndex(i)]] = true
    end
    return set
end

HL.Commit(ActivityImportantPopupCtrl)
