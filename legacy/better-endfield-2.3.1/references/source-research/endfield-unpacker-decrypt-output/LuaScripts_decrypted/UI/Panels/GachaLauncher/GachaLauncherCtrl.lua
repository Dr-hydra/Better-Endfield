local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.GachaLauncher
local PHASE_ID = PhaseId.GachaLauncher

GachaLauncherCtrl = HL.Class('GachaLauncherCtrl', uiCtrl.UICtrl)






GachaLauncherCtrl.s_messages = HL.StaticField(HL.Table) << {

}

local timelineStageEnum = {
    Entry = 0,
    CanDrag = 1,
    AutoPlay = 2,
}


GachaLauncherCtrl.m_dirPlayInfo = HL.Field(HL.Table)

GachaLauncherCtrl.m_directors = HL.Field(HL.Table)

GachaLauncherCtrl.m_updateKey = HL.Field(HL.Number) << -1

GachaLauncherCtrl.m_nextShowGuideTime = HL.Field(HL.Number) << 0

GachaLauncherCtrl.m_controllerTriggerSettingHandlerId = HL.Field(HL.Number) << -1




GachaLauncherCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:_InitUI()
end


GachaLauncherCtrl.Start = HL.Method() << function(self)
    self:_InitData()
    self.m_updateKey = LuaUpdate:Add("TailTick", function(deltaTime)
        self:_CheckUpdateStage()
        self:TickMove(deltaTime)

        local dirPlayInfo = self.m_dirPlayInfo
        if IsNull(self.view.guideNode) then
            return
        end
        if not self.view.guideNode.gameObject.activeSelf
            and dirPlayInfo.curStage ~= timelineStageEnum.AutoPlay
            and not dirPlayInfo.curIsDrag
        then
            if Time.time >= self.m_nextShowGuideTime then
                self.view.guideNode.gameObject:SetActive(true)
            end
        end
    end)
    if DeviceInfo.usingController and not DeviceInfo.isMobile and self.m_controllerTriggerSettingHandlerId == -1 then
        self.m_controllerTriggerSettingHandlerId = GameInstance.audioManager.gamePad.scePad:SetTriggerEffect(self.m_phase.m_launcherObjItem.view.psTriggerEffectCfg.commands[0])
    end
    self:_StartCoroutine(function()
        coroutine.waitForRenderDone()
        self:_SetTime(0, true)
    end)

    AudioAdapter.SetRtpc("au_rtpc_gacha_lever_progress", 0, CS.Beyond.Audio.AudioConstants.AUDIO_GLOBAL_GAME_OBJECT);
    AudioAdapter.SetRtpc("au_rtpc_gacha_lever_speed", 0, CS.Beyond.Audio.AudioConstants.AUDIO_GLOBAL_GAME_OBJECT);
end

GachaLauncherCtrl.OnClose = HL.Override() << function(self)
    AudioAdapter.PostEvent("Au_UI_Gacha_Lever_stop")
    self.m_updateKey = LuaUpdate:Remove(self.m_updateKey)
    if self.m_controllerTriggerSettingHandlerId > 0 then
        GameInstance.audioManager.gamePad.scePad:EndTriggerEffect(self.m_controllerTriggerSettingHandlerId)
        self.m_controllerTriggerSettingHandlerId = -1
    end
end



GachaLauncherCtrl._InitData = HL.Method() << function(self)


    local mainDir = self.m_phase.m_launcherDirector

    local actorDir = self.m_phase.m_launcherObjItem.view.actor
    self.m_directors = {
        mainDir = mainDir,
        actorDir = actorDir,
    }

    self.m_dirPlayInfo = {
        startPoint = 0,
        autoPlayPoint = 0,
        rarityLightPoint = 0,
        endPoint = 0,

        curStage = timelineStageEnum.Entry,
        curIsDrag = false,
        targetDirTime = 0,

        firstDragFlag = true,
        firstDragReleaseFlag = false,
        isNotPlayRarityLightAudio = true,
        preMoveDirection = 0,
    }

    local timePoints = {}
    local timeline = actorDir.playableAsset;
    for _, track in cs_pairs(timeline:GetOutputTracks()) do
        if (track.name == "TimePointTrack") then
            for _, timelineClip in cs_pairs(track:GetClips()) do
                table.insert(timePoints, timelineClip.start)
            end
            break
        end
    end
    table.sort(timePoints)
    if #timePoints >= 4 then
        self.m_dirPlayInfo.startPoint = timePoints[1]
        self.m_dirPlayInfo.rarityLightPoint = timePoints[2]
        self.m_dirPlayInfo.autoPlayPoint = timePoints[3]
        self.m_dirPlayInfo.endPoint = timePoints[4]
    else
        if #timePoints <= 0 then
            logger.error("[GachaLauncherCtrl._InitData] time point获取失败！未找到TimePointTrack或TimePointTrack上没有clip！")
        else
            logger.error("[GachaLauncherCtrl._InitData] time point获取失败！TimePointTrack上的clip数量小于3！")
        end
    end

    self.m_directors.mainDir:Stop()
    self:_SetTime(0, false)
end



GachaLauncherCtrl._InitUI = HL.Method() << function(self)
    self.view.skipBtn.onClick:AddListener(function()
        self:_CloseSelf(true)
    end)


    self.view.touchPlate.onDragBegin:AddListener(function(dragPos)
        local dirPlayInfo = self.m_dirPlayInfo
        if dirPlayInfo.curStage ~= timelineStageEnum.CanDrag then
            return
        end
        dirPlayInfo.targetDirTime = self:_GetTime()
        dirPlayInfo.curIsDrag = true
        if dirPlayInfo.firstDragFlag then
            dirPlayInfo.firstDragFlag = false
            AudioAdapter.PostEvent("Au_UI_Gacha_Lever_Start")
        end
    end)

    self.view.touchPlate.onDrag:AddListener(function(eventData)
        local dirPlayInfo = self.m_dirPlayInfo
        if dirPlayInfo.curStage ~= timelineStageEnum.CanDrag then
            return
        end
        dirPlayInfo.curIsDrag = true

        local launcherConfig = self.m_phase.m_launcherObjItem.view.config
        local curDirTime = self:_GetTime()
        local timeSpan = (dirPlayInfo.autoPlayPoint - dirPlayInfo.startPoint)
        local dirTimeRatio = (curDirTime - dirPlayInfo.startPoint) / timeSpan
        dirTimeRatio = dirTimeRatio, 0, 1
        local draggedScreenRatio = launcherConfig.TIME_POINT_DRAG_RATIO:Evaluate(dirTimeRatio)
        local deltaDragScreenRatio = -eventData.delta.y / (Screen.height * launcherConfig.NEED_DRAG_SCREEN_RATIO)
        local curTotalDragScreenRatio = draggedScreenRatio + deltaDragScreenRatio
        local rawTargetTime = dirPlayInfo.startPoint + launcherConfig.DRAG_RATIO_2_TIME_POINT:Evaluate(curTotalDragScreenRatio) * timeSpan
        dirPlayInfo.targetDirTime = lume.clamp(rawTargetTime, dirPlayInfo.startPoint, dirPlayInfo.autoPlayPoint)
    end)

    self.view.touchPlate.onDragEnd:AddListener(function(dragPos)
        local dirPlayInfo = self.m_dirPlayInfo
        if dirPlayInfo.curStage ~= timelineStageEnum.CanDrag then
            return
        end
        dirPlayInfo.curIsDrag = false
        dirPlayInfo.firstDragReleaseFlag = true
    end)


    local _, isFirstGacha = ClientDataManagerInst:GetBool("IS_FIRST_GACHA", false, true)
    if isFirstGacha then
        ClientDataManagerInst:SetBool("IS_FIRST_GACHA", false, false)
    end
    self.view.guideNode.gameObject:SetActive(isFirstGacha)
    self.m_nextShowGuideTime = Time.time + self.view.config.SHOW_GUIDE_WAIT_SECONDS
end


GachaLauncherCtrl._SetTime = HL.Method(HL.Number, HL.Boolean) << function(self, time, isPlay)
    self.m_directors.mainDir.time = time
    self.m_directors.mainDir:Evaluate()
    if isPlay then
        self.m_directors.mainDir:Play()
    end
    self:_CheckUpdateStage()
end

GachaLauncherCtrl._GetTime = HL.Method().Return(HL.Number) << function(self)
    return self.m_directors.mainDir.time
end

GachaLauncherCtrl._CloseSelf = HL.Method(HL.Boolean) << function(self, isSkip)
    self.m_updateKey = LuaUpdate:Remove(self.m_updateKey)
    local onComplete = self.m_phase.arg.onComplete
    if onComplete then
        local isAllowSkip = isSkip and not GameInstance.player.gacha.hasSuperSurprise
        onComplete(isAllowSkip)
    end
    PhaseManager:ExitPhaseFast(PHASE_ID)
end

GachaLauncherCtrl.TickMove = HL.Method(HL.Number) << function(self, deltaTime)
    local dirPlayInfo = self.m_dirPlayInfo
    if dirPlayInfo.curStage ~= timelineStageEnum.CanDrag then
        return
    end

    local curDirTime = self:_GetTime()
    local curIsDrag = dirPlayInfo.curIsDrag
    local gamepadInputValue = InputManagerInst:GetGamepadTriggerValue(false)
    local launcherConfig = self.m_phase.m_launcherObjItem.view.config

    if DeviceInfo.usingController then
        if gamepadInputValue >= launcherConfig.GAMEPAD_TRIGGER_THRESHOLD then
            if dirPlayInfo.firstDragFlag then
                dirPlayInfo.firstDragFlag = false
                AudioAdapter.PostEvent("Au_UI_Gacha_Lever_Start")
            end
            curIsDrag = true
            local dirTimeRatio = (curDirTime - dirPlayInfo.startPoint) / (dirPlayInfo.autoPlayPoint - dirPlayInfo.startPoint)
            dirTimeRatio = lume.clamp(dirTimeRatio, 0, 1)
            local speed = launcherConfig.GAMEPAD_TL_2_SPEED:Evaluate(dirTimeRatio) * launcherConfig.GAMEPAD_INPUT_2_SPEED:Evaluate(gamepadInputValue)
            local rawTargetTime = curDirTime + speed * deltaTime
            dirPlayInfo.targetDirTime = lume.clamp(rawTargetTime, dirPlayInfo.startPoint, dirPlayInfo.autoPlayPoint)
        else
            if not dirPlayInfo.firstDragFlag then
                dirPlayInfo.firstDragReleaseFlag = true
            end
        end
    end

    local curMoveDir = 0

    local targetTime = -1
    local isStopMove = false
    if curIsDrag then
        targetTime = dirPlayInfo.targetDirTime

        self.m_nextShowGuideTime = Time.time + self.view.config.SHOW_GUIDE_WAIT_SECONDS
        if self.view.guideNode.gameObject.activeSelf then
            self.view.guideNode.gameObject:SetActive(false)
        end

        if targetTime >= dirPlayInfo.autoPlayPoint then
            targetTime = dirPlayInfo.autoPlayPoint
            dirPlayInfo.targetDirTime = targetTime
            dirPlayInfo.curStage = timelineStageEnum.AutoPlay
            self:_SetTime(dirPlayInfo.autoPlayPoint, true)
            logger.info("[GachaLauncherCtrl] AutoPlay")
            self:_SetAudioValue(1, 0)
            isStopMove = true
            curMoveDir = 0
        else
            self:_SetTime(targetTime, false)
            curMoveDir = 1
        end
    else

        if curDirTime > dirPlayInfo.startPoint then
            local dirTimeRatio = (curDirTime - dirPlayInfo.startPoint) / (dirPlayInfo.autoPlayPoint - dirPlayInfo.startPoint)
            local speed = launcherConfig.RETURN_SPEED:Evaluate(dirTimeRatio)
            targetTime = lume.clamp(curDirTime - deltaTime * speed, dirPlayInfo.startPoint, dirPlayInfo.autoPlayPoint)
            self:_SetTime(targetTime, false)
            self:_SetAudioValue(0, 0)
            curMoveDir = -1
            isStopMove = true
        else
            curMoveDir = 0
        end
    end


    if targetTime > 0 and not isStopMove then
        local curProgress = (targetTime - dirPlayInfo.startPoint) / (dirPlayInfo.autoPlayPoint - dirPlayInfo.startPoint)
        self:_SetAudioValue(curProgress, (targetTime - curDirTime) / deltaTime)
    end

    if dirPlayInfo.firstDragReleaseFlag and curMoveDir ~= dirPlayInfo.preMoveDirection then
        dirPlayInfo.preMoveDirection = curMoveDir
        if curMoveDir ~= 0 then
            AudioAdapter.PostEvent("Au_UI_Gacha_Lever_Movement")
        end
    end

end

GachaLauncherCtrl._CheckUpdateStage = HL.Method() << function(self)
    local dirPlayInfo = self.m_dirPlayInfo
    local curDirTime = self:_GetTime()
    if dirPlayInfo.curStage == timelineStageEnum.Entry then
        if curDirTime >= dirPlayInfo.startPoint then
            dirPlayInfo.curStage = timelineStageEnum.CanDrag
            self.m_directors.mainDir:Stop()
            self:_SetTime(dirPlayInfo.startPoint, false)
            AudioAdapter.PostEvent("Au_UI_Gacha_Lever_loop")
        end
        return
    end

    if dirPlayInfo.isNotPlayRarityLightAudio then
        if curDirTime >= dirPlayInfo.rarityLightPoint then
            dirPlayInfo.isNotPlayRarityLightAudio = false
            AudioAdapter.PostEvent("Au_UI_Gacha_Lever_rare")
        end
    end

    if curDirTime >= dirPlayInfo.endPoint then
        self:_CloseSelf(false)
        return
    end

    if curDirTime >= dirPlayInfo.autoPlayPoint then
        dirPlayInfo.curStage = timelineStageEnum.AutoPlay
        return
    end

    if curDirTime >= dirPlayInfo.startPoint then
        dirPlayInfo.curStage = timelineStageEnum.CanDrag
    end
end

GachaLauncherCtrl._SetAudioValue = HL.Method(HL.Number, HL.Number) << function(self, progress, speed)
    AudioAdapter.SetRtpc("au_rtpc_gacha_lever_progress", progress, CS.Beyond.Audio.AudioConstants.AUDIO_GLOBAL_GAME_OBJECT);
    AudioAdapter.SetRtpc("au_rtpc_gacha_lever_speed", speed, CS.Beyond.Audio.AudioConstants.AUDIO_GLOBAL_GAME_OBJECT);


end


HL.Commit(GachaLauncherCtrl)
