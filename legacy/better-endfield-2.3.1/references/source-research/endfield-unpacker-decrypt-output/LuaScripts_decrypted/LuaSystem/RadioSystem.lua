local LuaSystemBase = require_ex('LuaSystem/LuaSystemBase')
RadioSystem = HL.Class('RadioSystem', LuaSystemBase.LuaSystemBase)

RADIO_DATA_SORT_KEYS = { "priorityKey", "needResumeKey", "addTimeKey" }
CONTINUE_RADIO_PRIORITY = -1000


local EMOTION_VOICE_PLAY_INTERVAL = 2









































RadioSystem.s_radioIndexCache = HL.StaticField(HL.Table) << {}

RadioSystem.m_curShow = HL.Field(HL.Any)

RadioSystem.m_waitingQueue = HL.Field(HL.Table)

RadioSystem.m_queueSortFunc = HL.Field(HL.Function)

RadioSystem.m_pauseRefCount = HL.Field(HL.Number) << 0

RadioSystem.inMainHud = HL.Field(HL.Boolean) << false

RadioSystem.m_forcePlayRadio = HL.Field(HL.Boolean) << false

RadioSystem.m_radioOnlySound = HL.Field(HL.Boolean) << false

RadioSystem.m_enableDebugLog = HL.Field(HL.Boolean) << false

RadioSystem.m_lastPlayedEmotionVoiceId = HL.Field(HL.String) << ""

RadioSystem.m_globalTagHandle = HL.Field(CS.Beyond.Gameplay.Core.GlobalTagHandle)

RadioSystem.RadioSystem = HL.Constructor() << function(self)
    self.inMainHud = GameWorld.worldInfo.inMainHud


    self:RegisterMessage(MessageConst.ON_SCENE_LOAD_START, function(arg)
        self:_FlushAll()
    end)

    self:RegisterMessage(MessageConst.ALL_CHARACTER_DEAD, function(arg)
        self:_FlushAll()
    end)

    self:RegisterMessage(MessageConst.ON_LOADING_START, function(arg)
        self:_TryFlushAllWhenLoading()
    end)
    self:RegisterMessage(MessageConst.OPEN_LOADING_PANEL, function(arg)
        self:_FlushAll()
    end)
    self:RegisterMessage(MessageConst.OPEN_TELEPORT_LOADING_PANEL, function(arg)
        self:_FlushAll()
    end)

    self:RegisterMessage(MessageConst.PLAY_CG, function(arg)
        self:_FlushAll()
    end)

    self:RegisterMessage(MessageConst.ON_PLAY_CUTSCENE, function(arg)
        if not self.m_forcePlayRadio then
            self:_FlushAll()
        end
    end)

    self:RegisterMessage(MessageConst.REMOTE_COMM_START, function(arg)
        self:_FlushAll()
    end)

    self:RegisterMessage(MessageConst.ON_SNS_FORCE_DIALOG_START, function(arg)
        self:_FlushAll()
    end)

    self:RegisterMessage(MessageConst.EXIT_ALL_PHASE, function(arg)
        self:_FlushAll()
    end)




    self:RegisterMessage(MessageConst.ON_GUIDE_STOPPED, function(arg)
        if arg then
            local isForceGuide = unpack(arg)
            if isForceGuide then
                self:_TryResumeAndShow()
            end
        end
    end)

    self:RegisterMessage(MessageConst.START_GUIDE_GROUP, function(arg)
        local info = unpack(arg)
        if info.type == CS.Beyond.Gameplay.GuideGroupType.Force then
            self:_TryPauseAndHide()
        end
    end)


    self:RegisterMessage(MessageConst.ON_ULTIMATE_SKILL_START, function(arg)
        local info = unpack(arg)
        if info.type == CS.Beyond.Gameplay.GuideGroupType.Force then
            self:_TryPauseAndHide()
        end
    end)
    self:RegisterMessage(MessageConst.ON_ULTIMATE_SKILL_END, function(arg)
        self:_TryResumeAndShow()
    end)


    self:RegisterMessage(MessageConst.ON_NARRATIVE_BLACK_SCREEN_END, function(arg)
        self:_TryResumeAndShow()
    end)


    self:RegisterMessage(MessageConst.ON_DIALOG_START, function(arg)
        self:_OnDialogStart(arg)
    end)




    self:RegisterMessage(MessageConst.ON_FORCE_PLAY_RADIO_CHANGED, function(arg)
        local forcePlayRadio = unpack(arg)
        if self.m_forcePlayRadio ~= forcePlayRadio then
            self.m_forcePlayRadio = forcePlayRadio

            if not forcePlayRadio and not self:_CheckCanPlay() then
                if self.m_curShow and not string.isEmpty(self.m_curShow.radioId) then
                    local errorLog = string.format(
                    "RadioSystem forcePlayRadio changed to FALSE but CheckCanPlay FALSE, CutCurRadio: %s!!!!",
                        self.m_curShow.radioId)
                    logger.warn(errorLog)
                    if BEYOND_DEBUG or BEYOND_DEBUG_COMMAND then
                        local toast = string.format("【EditorOnly】Radio:【%s】 被UI打断，目前效果可以找负责策划反馈是否符合预期!!!",
                            self.m_curShow.radioId)
                        Notify(MessageConst.SHOW_TOAST, toast)
                    end
                end
                self:_CutCurRadio(true)
                self:_Exit()
            end
        end
    end)

    self:RegisterMessage(MessageConst.ON_RADIO_ONLY_SOUND_CHANGED, function(arg)
        local onlySound = unpack(arg)
        if self.m_radioOnlySound ~= onlySound then
            self.m_radioOnlySound = onlySound

            if self.m_curShow then
                local panel = self:_GetUICtrl()
                if onlySound then
                    panel:HideSelf()
                else
                    panel:ShowSelf()
                    panel.animationWrapper:ClearTween(false)
                    panel:PlayAnimationIn()
                end
            end
        end
    end)

    self:RegisterMessage(MessageConst.ON_RADIO_EMPTY_SHOW, function(arg)
        self:_TryResumeAndShow()
    end)
    self:RegisterMessage(MessageConst.ON_RADIO_EMPTY_HIDE, function(arg)
        if not self.m_forcePlayRadio then
            self:_TryPauseAndHide()
        end
    end)

    self:RegisterMessage(MessageConst.ON_IN_MAIN_HUD_CHANGED, function(arg)
        local inMainHud = unpack(arg)
        self.inMainHud = inMainHud
        if inMainHud then
            self:_TryResumeAndShow()
        else
            if not self.m_forcePlayRadio then
                self:_TryPauseAndHide()
            end
        end
    end)

    self:RegisterMessage(MessageConst.ON_NARRATIVE_STATE_CHANGED, function(arg)
        local inNarrative = unpack(arg)

        if not inNarrative then
            self:_TryResumeAndShow()
        end
    end)

    self:RegisterMessage(MessageConst.SHOW_RADIO, function(arg)
        local data = unpack(arg)
        self:_TryPlayRadio(data)
    end)
    self:RegisterMessage(MessageConst.MANUAL_STOP_RADIO, function(arg)
        local radioId = unpack(arg)
        self:ManualStopRadio(radioId)
    end)

    self:RegisterMessage(MessageConst.FLUSH_RADIO, function(arg)
        local radioId = unpack(arg)
        self:_DoFlushRadio()
        if not string.isEmpty(radioId) then
            self:_TryPlayRadio(radioId)
        else
            self:_Exit()
        end
    end)


    self:RegisterMessage(MessageConst.ENABLE_RADIO_QUEUE_LOG, function(enable)
        self.m_enableDebugLog = enable
    end)

end

RadioSystem.ManualStopRadio = HL.Method(HL.Any) << function(self, radioId)
    if self.m_curShow == nil then
        return
    end

    if self.m_curShow.radioId ~= radioId then
        return
    end

    RadioSystem.s_radioIndexCache[radioId] = self.m_curShow.curIndex - 1
    self:_CutCurRadio(true)
    if not self.m_curShow then
        if not self:_TryShowNextRadio() then
            self:_Exit()
        end
    end
end

RadioSystem._ClearCurTimer = HL.Method() << function(self)
    if self.m_curShow and self.m_curShow.timerId and self.m_curShow.timerId > 0 then
        self:_ClearTimer(self.m_curShow.timerId)
    end
end

RadioSystem._AddCurExitTimer = HL.Method(HL.Number) << function(self, existTime)
    self:_ClearCurTimer()
    local timerId = self:_StartTimer(existTime, function()
        self:_ShowSingleRadio()
    end)
    self.m_curShow.timerId = timerId
end

RadioSystem._StopCurAudioEvent = HL.Method() << function(self)
    if self.m_curShow and self.m_curShow.audioEventPlayingId and self.m_curShow.audioEventPlayingId > 0 then
        AudioAdapter.StopByPlayingId(self.m_curShow.audioEventPlayingId, 0)
        self.m_curShow.audioEventPlayingId = 0
    end
end

RadioSystem._PauseCurAudioEvent = HL.Method() << function(self)
    if self.m_curShow and self.m_curShow.audioEventPlayingId and self.m_curShow.audioEventPlayingId > 0 then
        AudioAdapter.PauseByPlayingId(self.m_curShow.audioEventPlayingId, 0)
    end
end

RadioSystem._ResumeCurAudioEvent = HL.Method() << function(self)
    if self.m_curShow and self.m_curShow.audioEventPlayingId and self.m_curShow.audioEventPlayingId > 0 then
        AudioAdapter.ResumeByPlayingId(self.m_curShow.audioEventPlayingId, 0)
    end
end

RadioSystem._TryPauseAndHide = HL.Method() << function(self)
    self:_RemoveGlobalTag()

    if self.m_curShow then
        if self.m_pauseRefCount == 0 then

            if self.m_curShow.voiceHandleId and self.m_curShow.voiceHandleId > 0 then
                VoiceManager:PauseVoice(self.m_curShow.voiceHandleId)
                self:_ClearCurTimer()
            elseif self.m_curShow.timerId then
                local triggerTime = TimerManager:GetTimerTriggerTime(self.m_curShow.timerId)
                if triggerTime > 0 then
                    self.m_curShow.resumeTime = triggerTime - Time.time
                end
                self:_ClearCurTimer()
            end
            self:_PauseCurAudioEvent()
        end

        self.m_pauseRefCount = self.m_pauseRefCount + 1
    end
    NarrativeUtils.SetRadioId("")

    self:_Exit()
end

RadioSystem._IsSoundOnly = HL.Method().Return(HL.Boolean) << function(self)
    return GameWorld.narrativeManager.radioOnlySound
end

RadioSystem._TryResumeAndShow = HL.Method() << function(self)
    local panel = self:_GetUICtrl()
    if not panel then
        return
    end

    local showRadio = true
    local resume = false
    local isShow = panel:IsShow()
    if self.m_curShow and self.m_pauseRefCount > 0 then
        resume = true
        self.m_pauseRefCount = self.m_pauseRefCount - 1
        if self.m_curShow and self.m_curShow.voiceHandleId and
            self.m_pauseRefCount == 0 then
            VoiceManager:ResumeVoice(self.m_curShow.voiceHandleId)
            local exitTime = self:_GetExistTime(self.m_curShow.voiceId, self.m_curShow.voiceHandleId)
            self:_AddCurExitTimer(exitTime)
        end


        if self.m_curShow and self.m_pauseRefCount == 0 then
            self:_ResumeCurAudioEvent()
        end
    end
    if self:_CheckCanPlay() then
        local needShow = not self:_IsSoundOnly()
        if needShow then
            panel:ShowSelf()
            if not isShow then
                panel.animationWrapper:ClearTween(false)
                panel:PlayAnimationIn()
            end
        end

        if resume then
            self:_ContinueCurRadio()
        elseif not self.m_curShow then
            if not self:_TryShowNextRadio() then
                showRadio = false
                self:_Exit()
            end
        elseif not isShow then

            showRadio = false
            self:_Exit()
        end
    else
        showRadio = false
        self:_Exit()
    end

    if showRadio then
        self:_AddGlobalTag()
    end
end

RadioSystem._Exit = HL.Method(HL.Opt(HL.Boolean)) << function(self, useAnim)
    self:_RemoveGlobalTag()
    self:_HideUI(useAnim)
end

RadioSystem._HideUI = HL.Method(HL.Opt(HL.Boolean)) << function(self, useAnim)
    local panel = self:_GetUICtrl()
    if panel then
        panel:HideSelf(useAnim)
    end
end

RadioSystem._TryPlayRadio = HL.Method(CS.Beyond.Gameplay.Actions.GameAction.RadioRuntimeData) << function(self, data)
    local radioId = data.radioId
    local fromBegin = data.fromBegin
    local index = data.index
    local callback = data.callback
    local entity = data.entity
    local interruptFinish = data.interruptFinish
    local enableAdvancedOptions = data.enableAdvancedOptions
    local voOffset = data.voOffset
    local reverbOffset = data.reverbOffset
    local attenuationType = data.attenuationType
    local noFlushAfterLoading = data.noFlushAfterLoading

    local res, radioData = Tables.radioTable:TryGetValue(radioId)
    if not res then
        logger.error("Radio play fail, no radioId in table: " .. radioId)
        return
    end

    local extraData = {
        callback = callback,
        entity = entity,
        interruptFinish = interruptFinish,
        enableAdvancedOptions = enableAdvancedOptions,
        voOffset = voOffset,
        reverbOffset = reverbOffset,
        attenuationType = attenuationType,
        noFlushAfterLoading = noFlushAfterLoading,
    }
    local tmpIndex
    index = index == nil and 0 or index

    if not fromBegin then
        if index >= 0 then
            tmpIndex = index
        elseif RadioSystem.s_radioIndexCache[radioId] then

            tmpIndex = RadioSystem.s_radioIndexCache[radioId]
            RadioSystem.s_radioIndexCache[radioId] = -1
        end
    end

    if tmpIndex and tmpIndex >= 0 then
        extraData.curIndex = tmpIndex
    end


    if not self.m_curShow then
        self:_TryAddRadio2Queue(radioId, extraData)
        self:_TryResumeAndShow()
    else
        local curExtraData = {
            callback = self.m_curShow.callback,
            entity = self.m_curShow.entity,
            curIndex = self.m_curShow.curIndex - 1,
            needResume = true,
        }


        if self.m_curShow.isContinueExRadio then
            local continueSourceRadioId = self.m_curShow.continueSourceRadioId
            if #self.m_waitingQueue > 0 and self.m_waitingQueue[1].radioId == continueSourceRadioId then
                local res, continueSourceRadioData = Tables.radioTable:TryGetValue(continueSourceRadioId)

                if res and not continueSourceRadioData.continueAfterRadio then
                    table.remove(self.m_waitingQueue, 1)
                end
            end

            self:_TryAddRadio2Queue(radioId, extraData)
            self:_CutCurRadio(true)
            if not self.m_curShow then
                self:_TryShowNextRadio()
            end
        elseif self.m_curShow.priority > radioData.priority then

            if radioData.continueAfterRadio then
                self:_TryAddRadio2Queue(radioId, extraData)
            end
        elseif self.m_curShow.priority < radioData.priority then
            self:_TryAddRadio2Queue(radioId, extraData)

            if self.m_curShow.continueAfterRadio then
                self:_TryAddRadio2Queue(self.m_curShow.radioId, curExtraData)
            end
            self:_CutCurRadio(true)
            if not self.m_curShow then
                self:_TryShowNextRadio()
            end
        else
            self:_TryAddRadio2Queue(radioId, extraData)

            if self.m_curShow.radioId ~= radioId and self.m_curShow.continueAfterRadio then
                self:_TryAddRadio2Queue(self.m_curShow.radioId, curExtraData, true)
            end
            self:_CutCurRadio(true)
            if not self.m_curShow then
                self:_TryShowNextRadio()
            end
        end
    end
end

RadioSystem._ContinueCurRadio = HL.Method() << function(self)
    if self.m_pauseRefCount == 0 then
        local panel = self:_GetUICtrl()
        local needShow = not self:_IsSoundOnly()
        if self.m_curShow then
            if self.m_curShow.voiceHandleId then
                VoiceManager:ResumeVoice(self.m_curShow.voiceHandleId)
                local existTime = self:_GetExistTime(self.m_curShow.voiceId, self.m_curShow.voiceHandleId)
                self:_AddCurExitTimer(existTime)
            elseif self.m_curShow.resumeTime > 0 then
                self:_AddCurExitTimer(self.m_curShow.resumeTime)
            else
                self:_ShowSingleRadio()
            end

            if needShow then
                panel.view.textTalk:Play()
                panel.view.textTalkCenter:Play()
            end


            if self.m_curShow then
                NarrativeUtils.SetRadioId(self.m_curShow.radioId)
            end
        end
    end
end

RadioSystem._TryGetContinueExRadio = HL.Method(HL.Table).Return(HL.Any) << function(self, curData)
    local interruptedVoiceId = curData.interruptedVoiceId
    local data
    if not string.isEmpty(interruptedVoiceId) then
        local res, continueRadioId = VoiceUtils.TryGetRadioContinueId(interruptedVoiceId,
            VoiceUtils.GetDefaultContinueRadioSpeakers())
        if res then
            data = {
                radioId = continueRadioId,
                curIndex = 0,
                priority = CONTINUE_RADIO_PRIORITY,
                addTime = TimeManagerInst.unscaledTime,
                isContinueExRadio = true,
            }
        end
    end
    return data
end

RadioSystem._TryShowNextRadio = HL.Method().Return(HL.Boolean) << function(self)
    if #self.m_waitingQueue <= 0 then
        return false
    end

    local data = self.m_waitingQueue[1]
    local radioId = data.radioId

    if not self:_CheckCanPlay(radioId) then
        return false
    end


    if data.isContinueExRadio then
        table.remove(self.m_waitingQueue, 1)
        data = self.m_waitingQueue[1]
        radioId = data.radioId
    end


    local continueRadioData = self:_TryGetContinueExRadio(data)
    if continueRadioData then
        continueRadioData.continueSourceRadioId = data.radioId
        self:_DoShowRadio(continueRadioData)

        if self.m_curShow then

            self.m_curShow.interruptedVoiceId = self.m_waitingQueue[1].interruptedVoiceId
            self.m_waitingQueue[1].interruptedVoiceId = nil
            self.m_waitingQueue[1].continuedRadio = continueRadioData.radioId
            return true
        else
            return false
        end
    end






    table.remove(self.m_waitingQueue, 1)
    self:_DoShowRadio(data)

    if BEYOND_DEBUG then
        self:_UpdateLog()
    end

    return true
end

RadioSystem._CheckRadioInQueue = HL.Method(HL.String).Return(HL.Number) << function(self, radioId)
    for index, v in pairs(self.m_waitingQueue) do
        if v.radioId == radioId then
            return index
        end
    end
    return -1
end

RadioSystem._TryAddRadio2Queue = HL.Method(HL.String, HL.Opt(HL.Table, HL.Boolean)) <<
function(self, radioId, extraData, resume)
    if self:_CheckRadioInQueue(radioId) <= 0 then
        local res, radioData = Tables.radioTable:TryGetValue(radioId)
        if res then
            local data = {
                radioId = radioId,
                curIndex = 0,
                priority = radioData.priority,
                addTime = TimeManagerInst.unscaledTime,
            }

            if extraData then
                for k, v in pairs(extraData) do
                    data[k] = v
                end
            end

            data["priorityKey"] = -radioData.priority
            data["needResumeKey"] = resume and 0 or -1
            data["addTimeKey"] = resume and data.addTime or -data.addTime
            table.insert(self.m_waitingQueue, data)

            table.sort(self.m_waitingQueue, self.m_queueSortFunc)

            if BEYOND_DEBUG then
                self:_UpdateLog()
            end
        end
    end
end

RadioSystem._CutCurRadio = HL.Method(HL.Opt(HL.Boolean, HL.Boolean)) << function(self, doCallback, tryFinishRadio)
    local curShow = self.m_curShow
    local callback
    local radioId
    local interruptFinish

    if tryFinishRadio == nil then
        tryFinishRadio = true
    end
    if curShow then
        callback = curShow.callback
        radioId = curShow.radioId
        interruptFinish = curShow.interruptFinish
        if curShow.timerId then
            self:_ClearTimer(curShow.timerId)
        end

        if tryFinishRadio and interruptFinish then
            self:_SetLastFinishRadio(self.m_curShow.radioId, "_CutCurRadio")
        end

        if not string.isEmpty(curShow.voiceId) then
            if curShow.voiceHandleId then
                VoiceManager:StopVoice(curShow.voiceHandleId)
            end
        end
        self:_StopCurAudioEvent()
    end
    self:_ClearCurTimer()
    self.m_curShow = nil
    self.m_pauseRefCount = 0


    if doCallback and callback and not string.isEmpty(radioId) then
        callback(radioId)
    end

    NarrativeUtils.RadioFinish({ radioId })

    NarrativeUtils.SetRadioId("")
end

RadioSystem._UpdateVoiceInfo = HL.Method() << function(self)
    self.m_curShow.voiceDurations = {}
    self.m_curShow.voiceTotalDuration = 0
    local radioSingleDataList = self.m_curShow.radioSingleDataList
    for i = 1, radioSingleDataList.Count do
        local singleData = radioSingleDataList[CSIndex(i)]
        local voiceId = singleData.audioOverride
        local res, duration = VoiceUtils.TryGetVoiceDuration(voiceId)
        if not res then
            voiceId = ""
        end
        if string.isEmpty(voiceId) then
            self.m_curShow.voiceDurations = {}
            self.m_curShow.voiceTotalDuration = 0
            break
        end

        table.insert(self.m_curShow.voiceDurations, duration)
        self.m_curShow.voiceTotalDuration = self.m_curShow.voiceTotalDuration + duration
    end
end



RadioSystem._IsPureAudioEventRow = HL.Method(HL.Userdata).Return(HL.Boolean) << function(self, singleData)
    if singleData == nil then
        return false
    end
    if string.isEmpty(singleData.audioEvent) then
        return false
    end
    return string.isEmpty(singleData.radioText)
end

RadioSystem._DoShowRadio = HL.Method(HL.Table) << function(self, data)
    local panel = self:_GetUICtrl()
    local needShow = not self:_IsSoundOnly()
    local radioId = data.radioId
    local curIndex = data.curIndex
    local callback = data.callback
    local entity = data.entity
    local isContinueExRadio = data.isContinueExRadio
    local continueSourceRadioId = data.continueSourceRadioId
    local res, radioData = Tables.radioTable:TryGetValue(radioId)


    local firstIsPureAudioEvent = false
    if res and radioData and radioData.radioSingleDataList then
        local firstIndex = (curIndex or 0) + 1
        if firstIndex <= radioData.radioSingleDataList.Count then
            local firstSingle = radioData.radioSingleDataList[CSIndex(firstIndex)]
            firstIsPureAudioEvent = self:_IsPureAudioEventRow(firstSingle)
        end
    end

    if needShow and not firstIsPureAudioEvent then
        panel.animationWrapper:ClearTween()
        panel:PlayAnimationIn()
    elseif not needShow then
        self:_HideUI()
    end

    if res then
        local canStop = false
        local radioType = radioData.radioType


        self.m_lastPlayedEmotionVoiceId = ""

        if radioType == GEnums.RadioType.Wireless then
            self.m_curShow = {
                radioId = radioId,
                priority = radioData.priority,
                continueAfterRadio = radioData.continueAfterRadio,
                continueAfterDialog = radioData.continueAfterDialog,
                radioSingleDataList = radioData.radioSingleDataList,
                callback = callback,
                curIndex = curIndex,
                timerId = nil,
                voiceHandleId = nil,
                entity = entity,
                icon = nil,
                spriteName = nil,
                cacheCallbackVoiceHandleId = -1,
                resumeTime = -1,
                isContinueExRadio = isContinueExRadio,
                continueSourceRadioId = continueSourceRadioId,
                interruptFinish = data.interruptFinish,
                enableAdvancedOptions = data.enableAdvancedOptions,
                voOffset = data.voOffset,
                reverbOffset = data.reverbOffset,
                attenuationType = data.attenuationType,
                noFlushAfterLoading = data.noFlushAfterLoading,
                audioEventPlayingId = 0,
                emotionVoiceSkipRemaining = 0,
            }
            self:_UpdateVoiceInfo()
            self:_ShowSingleRadio()
            NarrativeUtils.SetRadioId(self.m_curShow.radioId)
        else
            logger.error("_DoShowRadio radioType error, only wireless supported, radioId: " .. radioId .. "!!!")
        end
    else
        logger.error("_DoShowRadio data error, radioId: " .. radioId .. "!!!")
    end
end

RadioSystem._GetExistTime = HL.Method(HL.String, HL.Number).Return(HL.Number) << function(self, voiceId, voiceHandleId)
    local existTime = -1
    local res, duration = VoiceUtils.TryGetVoiceDuration(voiceId)
    local hasPlayed, positionMs = VoiceUtils.TryGetVoicePlayPosition(voiceHandleId)
    if res then
        existTime = duration
        if hasPlayed then
            existTime = duration - positionMs / 1000
        end
    end
    return existTime
end

RadioSystem._ShowSingleRadio = HL.Method() << function(self)
    local panel = self:_GetUICtrl()
    local isShow = panel:IsShow()
    local needShow = not self:_IsSoundOnly()


    self:_StopCurAudioEvent()

    if self.m_curShow then
        local nextIndex = self.m_curShow.curIndex + 1

        if nextIndex <= self.m_curShow.radioSingleDataList.Count then
            local nextSingleData = self.m_curShow.radioSingleDataList[CSIndex(nextIndex)]
            local isPureAudioEvent = self:_IsPureAudioEventRow(nextSingleData)


            if isPureAudioEvent then
                self:_ClearCurTimer()


                if isShow then
                    panel:TryPlayInfoNodeOut()
                    panel:HideSelf(true)
                end

                local audioEvent = nextSingleData.audioEvent
                local playingId = AudioAdapter.PostEvent(audioEvent)
                if playingId and playingId > 0 then
                    self.m_curShow.audioEventPlayingId = playingId
                end

                local duration = nextSingleData.audioEventDuration
                if not duration or duration <= 0 then

                    duration = Tables.cinematicConst.radioTextMinDuration
                end
                self:_AddCurExitTimer(duration)

                self.m_curShow.curIndex = nextIndex
                self.m_curShow.cacheCallbackVoiceHandleId = -1
                return
            end


            if isShow and not needShow then
                self:_HideUI()
            elseif not isShow and needShow then
                panel:ShowSelf()
            end
            panel.view.infoNode:ClearTween()
            local num = panel:ShowRadioUI(self.m_curShow, nextSingleData, nextIndex)
            num = num / I18nUtils.GetTextSpeedFactor()
            local voiceId = nextSingleData.audioOverride
            local res, _ = VoiceUtils.TryGetVoiceDuration(voiceId)
            if not res then
                voiceId = ""
            end

            local audioEffect = nextSingleData.audioEffect
            local is3D = nextSingleData.is3D

            self:_ClearCurTimer()

            local durationOnText = lume.clamp(num * Tables.cinematicConst.textShowDurationPerWord,
                Tables.cinematicConst.radioMinWaitTime, Tables.cinematicConst.radioMaxWaitTime)

            local entity = self.m_curShow.entity
            local cfg = CS.Beyond.Gameplay.Audio.NarrativeVoiceConfig(audioEffect, 1)

            if self.m_curShow.enableAdvancedOptions then
                cfg.voOffset = self.m_curShow.voOffset
                cfg.reverbOffset = self.m_curShow.reverbOffset
                cfg.attenuationType = self.m_curShow.attenuationType
            end

            if not string.isEmpty(voiceId) then
                local voiceHandleId

                if is3D and entity and not entity.markReleased then
                    voiceHandleId = VoiceManager:SpeakNarrative(voiceId, entity, cfg)
                else
                    voiceHandleId = VoiceManager:SpeakNarrative(voiceId, nil, cfg)
                end
                local exitTime
                if voiceHandleId > 0 and VoiceManager:IsVoicePlaying(voiceHandleId) then
                    self.m_curShow.voiceHandleId = voiceHandleId
                    self.m_curShow.voiceId = voiceId
                    exitTime = self:_GetExistTime(voiceId, voiceHandleId)
                else
                    exitTime = durationOnText
                end
                self:_AddCurExitTimer(exitTime)
            else

                local nameId = DialogUtils.GetRealActorNameId(nextSingleData.actorNameId)
                local emotionType = nextSingleData.emotionType
                local voiceHandleId = -1

                if self.m_curShow.emotionVoiceSkipRemaining > 0 then

                    self.m_curShow.emotionVoiceSkipRemaining = self.m_curShow.emotionVoiceSkipRemaining - 1
                else
                    local getRes, emotionVoiceId = VoiceUtils.TryGetCommonEmotionVoiceId(
                        nameId, emotionType, CSUtils.GetStringHash(nextSingleData.id), { self.m_lastPlayedEmotionVoiceId }
                    )
                    if getRes then
                        if is3D and entity then
                            voiceHandleId = VoiceManager:SpeakNarrative(emotionVoiceId, entity, cfg)
                        else
                            voiceHandleId = VoiceManager:SpeakNarrative(emotionVoiceId, nil, cfg)
                        end


                        if voiceHandleId > 0 and VoiceManager:IsVoicePlaying(voiceHandleId) then
                            self.m_curShow.emotionVoiceSkipRemaining = EMOTION_VOICE_PLAY_INTERVAL - 1
                        end
                    end

                    self.m_lastPlayedEmotionVoiceId = emotionVoiceId
                end


                local minDuration = Tables.cinematicConst.radioTextMinDuration
                local finalTime = math.max(minDuration, durationOnText)
                self:_AddCurExitTimer(finalTime)
            end


            if not string.isEmpty(nextSingleData.audioEvent) then
                local playingId = AudioAdapter.PostEvent(nextSingleData.audioEvent)
                if playingId and playingId > 0 then
                    self.m_curShow.audioEventPlayingId = playingId
                end
            end

            self.m_curShow.curIndex = nextIndex
            self.m_curShow.cacheCallbackVoiceHandleId = -1
        else
            self:_SetLastFinishRadio(self.m_curShow.radioId, "Play End")
            self:_CutCurRadio(true, false)
            if not self.m_curShow then
                if not self:_TryShowNextRadio() then
                    local panel = self:_GetUICtrl()
                    panel:TryPlayInfoNodeOut()
                    self:_Exit()
                end
            end
        end
    end
end

RadioSystem._CheckCanPlay = HL.Method(HL.Opt(HL.String)).Return(HL.Boolean) << function(self, radioId)

    if GameInstance.player.guide.isInForceGuide and not GameInstance.player.guide.isInHelperGuideStep then
        return false
    end

    if self.m_forcePlayRadio then
        return true
    end

    if Utils.isInNarrative() then
        return false
    end

    return self.inMainHud
end

RadioSystem._UpdateLog = HL.Method() << function(self)
    if self.m_enableDebugLog then
        local text = ""
        for i, debugData in pairs(self.m_waitingQueue) do
            if i ~= 1 then
                text = text .. "\n"
            end
            text = text .. string.format("{%s : %d}", debugData.radioId, debugData.curIndex)
        end
        Notify(MessageConst.UPDATE_DEBUG_TEXT, text)
    end
end

RadioSystem._GetUICtrl = HL.Method().Return(HL.Forward("UICtrl")) << function(self)
    local _, panel = UIManager:IsOpen(PanelId.Radio)
    return panel
end



RadioSystem._AddGlobalTag = HL.Method() << function(self)
    self:_RemoveGlobalTag()
    self.m_globalTagHandle = GameInstance.player.globalTagsSystem:AddGlobalTag(CS.Beyond.Gameplay.Core.GameplayTag(CS
    .Beyond.GlobalTagConsts.TAG_RADIO_PATH))
end

RadioSystem._RemoveGlobalTag = HL.Method() << function(self)
    if self.m_globalTagHandle then
        self.m_globalTagHandle:RemoveTag()
    end
end





RadioSystem._OnDialogStart = HL.Method(HL.Opt(HL.Table)) << function(self, data)
    local _, dialogType = unpack(data)
    if dialogType == Const.DialogType.Cinematic then
        self:_FlushAll()
    else
        if self.m_curShow then
            if self.m_curShow.continueAfterDialog then
                local needContinue, nextIndex = self:_TryGetContinueIndex()
                if needContinue then
                    local extraData = {
                        curIndex = nextIndex - 1,
                        interruptedVoiceId = self.m_curShow.voiceId,
                    }
                    self:_TryAddRadio2Queue(self.m_curShow.radioId, extraData)
                end
            elseif self.m_curShow.isContinueExRadio then

                if #self.m_waitingQueue > 0 and self.m_waitingQueue[1].continuedRadio == self.m_curShow.radioId then
                    self.m_waitingQueue[1].interruptedVoiceId = self.m_curShow.interruptedVoiceId
                end
            end
        end
        self:_CutCurRadio(true)
    end
end

RadioSystem._TryGetContinueIndex = HL.Method().Return(HL.Boolean, HL.Number) << function(self)
    local nextIndex = self.m_curShow.curIndex
    if self.m_curShow and self.m_curShow.interruptFinish then

        return false, -1
    elseif self.m_curShow and self.m_curShow.continueAfterDialog then

        if self.m_curShow.voiceTotalDuration <= 0 then
            return true, nextIndex
        end

        local playedTime = 0
        if self.m_curShow.curIndex - 1 > 1 then
            for i = 1, self.m_curShow.curIndex - 1 do
                playedTime = playedTime + self.m_curShow.voiceDurations[i]
            end
        end
        local res, positionMs = VoiceUtils.TryGetVoicePlayPosition(self.m_curShow.voiceHandleId)
        local curVoiceDuration = self.m_curShow.voiceDurations[self.m_curShow.curIndex]
        if res then
            curVoiceDuration = positionMs / 1000
            playedTime = playedTime + curVoiceDuration
        elseif self.m_curShow.voiceHandleId and self.m_curShow.voiceHandleId > 0 then

            playedTime = self.m_curShow.voiceTotalDuration
        end

        local totalPercent = playedTime / self.m_curShow.voiceTotalDuration


        if totalPercent > 0.9 then
            return false, -1
        end


        local singlePercent = curVoiceDuration / self.m_curShow.voiceDurations[self.m_curShow.curIndex]
        if singlePercent > 0.9 then
            nextIndex = self.m_curShow.curIndex + 1
        end


        if nextIndex > self.m_curShow.radioSingleDataList.Count then
            return false, -1
        end
        return true, nextIndex
    else
        return false, -1
    end
end




RadioSystem._TryFlushAllWhenLoading = HL.Method(HL.Opt(HL.Any)) << function(self, _)

    if self.m_curShow and self.m_curShow.noFlushAfterLoading then
        return
    end

    self:_FlushAll()
end


RadioSystem._FlushAll = HL.Method(HL.Opt(HL.Any)) << function(self, _)
    self:_DoFlushRadio()
    self:_Exit(true)
end

RadioSystem._SetLastFinishRadio = HL.Method(HL.String, HL.String) << function(self, radioId, reason)
    GameWorld.narrativeManager:SetLastFinishRadio(radioId)
    logger.info(string.format("RadioSystem _SetLastFinishRadio: %s, Reason: %s", radioId, reason))
end

RadioSystem._DoFlushRadio = HL.Method() << function(self)

    local radioIds = {}
    for _, data in pairs(self.m_waitingQueue) do
        table.insert(radioIds, data.radioId)
        if data.callback then
            data.callback(data.radioId)
        end


        if data.interruptFinish then
            self:_SetLastFinishRadio(data.radioId, "_DoFlushRadio")
        end
    end

    if #radioIds > 0 then
        NarrativeUtils.RadioFinish(radioIds)
    end

    self.m_waitingQueue = {}
    if self.m_curShow then
        self:_CutCurRadio(true)
    end

    if BEYOND_DEBUG then
        self:_UpdateLog()
    end
end



RadioSystem.OnInit = HL.Override() << function(self)
    RadioSystem.s_radioIndexCache = {}
    self.m_waitingQueue = {}
    self.m_queueSortFunc = Utils.genSortFunction(RADIO_DATA_SORT_KEYS, true)
    self.m_curShow = nil
end

RadioSystem.OnRelease = HL.Override() << function(self)



    RadioSystem.s_radioIndexCache = {}
    self.m_waitingQueue = {}
    self.m_curShow = nil
    self.m_queueSortFunc = nil
    self.m_pauseRefCount = 0
end

HL.Commit(RadioSystem)
return RadioSystem
