local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.CG

local Status = CS.CriWare.CriMana.Player.Status

CGCtrl = HL.Class('CGCtrl', uiCtrl.UICtrl)

do
    CGCtrl.m_time = HL.Field(HL.Number) << -1

    CGCtrl.m_targetTime = HL.Field(HL.Number) << -1

    CGCtrl.m_lateTickKey = HL.Field(HL.Number) << -1

    CGCtrl.m_isPositivePausing = HL.Field(HL.Boolean) << false

    CGCtrl.m_onCanvasChangedClosure = HL.Field(HL.Function)
end







CGCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.SKIP_FMV] = 'SkipVideo',
    [MessageConst.ON_SHOW_BIG_LOGO_FMV] = '_OnShowBigLogo',
    [MessageConst.ON_APPLICATION_PAUSE] = '_OnApplicationPause',

}

CGCtrl.m_shouldClose = HL.Field(HL.Boolean) << false

CGCtrl.m_afterMaskData = HL.Field(HL.Any) << nil

CGCtrl.m_stopCoroutine = HL.Field(HL.Thread)

CGCtrl.m_isInAfterMask = HL.Field(HL.Boolean) << false

CGCtrl.m_volume = HL.Field(HL.Number) << 1.0


CGCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.exUINode.view.button.onClick:RemoveAllListeners()
    self.view.exUINode.view.button.onClick:AddListener(function()
        self:OnSkipButtonClick()
    end)
end

CGCtrl.OnShow = HL.Override() << function(self)
    self.view.image.gameObject:SetActive(false)
    self.view.exUINode:InitCinematicExUI()
    self:_HideBigLogo()
end

CGCtrl.OnClose = HL.Override() << function(self)
    self.view.exUINode.view.button.onClick:RemoveAllListeners()
    self.view.movieController.player.statusChangeCallback = nil
    self.view.subtitleController:ReleaseAll()
    self.view.subtitleController:UnbindMovieController()
    GameInstance.audioManager.music:ResumeMusic();
    LuaUpdate:Remove(self.m_lateTickKey)
    self.m_lateTickKey = -1
    self.view.exUINode:Clear()

    if self.m_onCanvasChangedClosure then
        UIManager.m_uiCanvasScaleHelper.onCanvasChanged:RemoveListener(self.m_onCanvasChangedClosure)
        self.m_onCanvasChangedClosure = nil
    end

    if self.m_stopCoroutine then
        self.m_stopCoroutine = self:_ClearCoroutine(self.m_stopCoroutine)
    end

    if self.m_isInAfterMask then
        GameAction.ShutdownBlackScreen(UIConst.UI_COMMON_MASK_LAYER_TYPE.High)
        self.m_isInAfterMask = false
    end

    if BEYOND_DEBUG or BEYOND_DEBUG_COMMAND then
        self:_StopDisplayDebugInfo()
    end
end

CGCtrl.OnHide = HL.Override() << function(self)
    self.view.exUINode:Clear()
end

CGCtrl.PlayCG = HL.Method(HL.String, HL.String, HL.Opt(HL.Any, HL.Any)) << function(self, path, fmvId, beforeMaskData, afterMaskData)
    self.m_shouldClose = false
    local image = self.view.image
    image.gameObject:SetActive(true)

    self.m_afterMaskData = afterMaskData
    if beforeMaskData and beforeMaskData.fadeInDuration then
        local dynamicMaskData = UIUtils.genDynamicBlackScreenMaskData("FMV-IN", beforeMaskData.fadeInDuration, 0, function()
            self:LoadAndPlayCG(path, fmvId, beforeMaskData)
        end)
        dynamicMaskData.fadeType = UIConst.UI_COMMON_MASK_FADE_TYPE.FadeIn
        dynamicMaskData.maskType = beforeMaskData.maskType
        dynamicMaskData.audioBlackScreenBehaviour = beforeMaskData.audioBlackScreenBehaviour
        dynamicMaskData.waitHide = true

        GameAction.ShowBlackScreen(dynamicMaskData)
    else
        self:LoadAndPlayCG(path, fmvId, beforeMaskData)
    end

    if BEYOND_DEBUG or BEYOND_DEBUG_COMMAND then
        self:_StartDisplayDebugInfo()
    end
end

CGCtrl.LoadAndPlayCG = HL.Method(HL.String, HL.String, HL.Opt(HL.Any)) << function(self, path, fmvId, maskData)
    local canSkip = Utils.checkCGCanSkip(fmvId)
    if BEYOND_DEBUG then
        canSkip = canSkip or CS.Beyond.DebugSettings.instance:LuaGetBool(CS.Beyond.DebugDefines.USE_CINEMATIC_DEBUG) == true
    end

    local res, cgConfig = DataManager.cgConfig.data:TryGetValue(fmvId)
    local isInitComplete = false
    self.view.exUINode.view.button.gameObject:SetActive(canSkip)
    self.view.subtitleController:PreloadFMVConfig(fmvId, function()
        self.view.subtitleController:BindMovieController(self.view.movieController)
        self.view.movieController.player:SetFile(nil, path)
        self.view.movieController.player:Start()

        self.view.movieController.player:SetVolume(0)






        self.view.movieController.player.statusChangeCallback = nil
        self.view.movieController.player.statusChangeCallback = function(status)
            local movieInfo = self.view.movieController.player.movieInfo

            if status == Status.Playing and movieInfo then
                if not isInitComplete then
                    local fadeOutTime = 0
                    if maskData and maskData.fadeOutDuration then
                        fadeOutTime = maskData.fadeOutDuration
                    end
                    local dynamicMaskData = UIUtils.genDynamicBlackScreenMaskData("FMV-OUT", 0, fadeOutTime, function()
                        self.view.image.gameObject:SetActive(true)
                    end)
                    if maskData then
                        dynamicMaskData.maskType = maskData.maskType
                    end
                    dynamicMaskData.fadeType = UIConst.UI_COMMON_MASK_FADE_TYPE.FadeOut
                    GameAction.ShowBlackScreen(dynamicMaskData)
                    isInitComplete = true

                    local noSafeZone = false
                    if cgConfig then
                        noSafeZone = cgConfig.noSafeZone
                    end

                    self:SetVideoImageOffset(noSafeZone)
                    if self.m_onCanvasChangedClosure == nil then
                        self.m_onCanvasChangedClosure = function() self:SetVideoImageOffset(false) end
                        UIManager.m_uiCanvasScaleHelper.onCanvasChanged:AddListener(self.m_onCanvasChangedClosure)
                    end
                    self.view.subtitleController:Play()
                end
            end

            if status == Status.PlayEnd then
                self:OnVideoEnd()
            end
        end
        self.m_time = 0
        self.m_targetTime = 1
    end)
end

CGCtrl.SetVideoImageOffset = HL.Method(HL.Boolean) << function(self, noSafeZone)
    if noSafeZone == nil then
        noSafeZone = false
    end

    local player = self.view.movieController.player
    if player == nil then
        return
    end

    local movieInfo = player.movieInfo
    if not movieInfo then
        return
    end

    local screenWidth = self.view.image.transform.rect.width
    local screenHeight = self.view.image.transform.rect.height
    local w = movieInfo.dispWidth
    local h = movieInfo.dispHeight

    local offsetMin, offsetMax = FMVUtils.GetSuitableFMVImageOffset(screenWidth, screenHeight, w, h, noSafeZone)
    self.view.movieController.transform.offsetMin = offsetMin
    self.view.movieController.transform.offsetMax = offsetMax
end

CGCtrl.SkipVideo = HL.Method() << function(self)
    self:OnVideoEnd(true)
end

CGCtrl.OnVideoEnd = HL.Method(HL.Opt(HL.Boolean)) << function(self, isSkip)
    if isSkip == nil then
        isSkip = false
    end

    self.m_shouldClose = true






    local fmvDirector = self.view.subtitleController.fmvDirector
    if isSkip then
        CS.Beyond.Gameplay.Core.TimelineUtils.HandleDirectorSkipAudio(fmvDirector)
    end
    self.view.subtitleController:Stop()

    if self.m_afterMaskData then
        local maskData = self.m_afterMaskData
        self.m_stopCoroutine = self:_StartCoroutine(function() self:AsyncStop(maskData, isSkip) end)
    else
        self:Close()
        VideoManager:OnPlayCGEnd(isSkip, true)
    end
end

CGCtrl.AsyncStop = HL.Method(HL.Any, HL.Boolean) << function(self, maskData, isSkip)
    local dynamicMaskData = UIUtils.genDynamicBlackScreenMaskData(
        "FMV-END", maskData.fadeInDuration, maskData.fadeOutDuration
    )
    dynamicMaskData.fadeType = UIConst.UI_COMMON_MASK_FADE_TYPE.FadeIn
    dynamicMaskData.maskType = maskData.maskType
    dynamicMaskData.callback = maskData.endCallback
    dynamicMaskData.audioBlackScreenBehaviour = maskData.audioBlackScreenBehaviour

    dynamicMaskData.waitHide = true
    dynamicMaskData.enableEntitySyncLoadMode = true
    GameAction.ShowBlackScreen(dynamicMaskData)
    self.m_isInAfterMask = true

    coroutine.wait(maskData.fadeInDuration)

    VideoManager:SendFMVWatchedAndFlushSync()

    local waitStartTime = Time.unscaledTime
    coroutine.waitCondition(function()
        return VideoManager.isFlushSyncDone or Time.unscaledTime - waitStartTime > 5.0
    end)

    if not maskData.fadeOutWaitHide then

        local shotDownMaskData = UIUtils.genDynamicBlackScreenMaskData(
            "FMV-END", maskData.fadeInDuration, maskData.fadeOutDuration
        )
        shotDownMaskData.fadeType = UIConst.UI_COMMON_MASK_FADE_TYPE.FadeOut
        shotDownMaskData.maskType = maskData.maskType
        shotDownMaskData.audioBlackScreenBehaviour = maskData.audioBlackScreenBehaviour
        GameAction.ShowBlackScreen(shotDownMaskData)
    end

    self.m_isInAfterMask = false
    self:Close()
    VideoManager:OnPlayCGEnd(isSkip, false)
end

CGCtrl.OnSkipButtonClick = HL.Method() << function(self)
    self:_PauseEveryThing(true)
    self.m_isPositivePausing = true

    self:Notify(MessageConst.SHOW_POP_UP, {
        content = Language.LUA_CONFIRM_SKIP_DIALOG,
        onConfirm = function()
            self.m_isPositivePausing = false
            self:OnVideoEnd(true)
        end,
        onCancel = function()
            self.m_isPositivePausing = false
            self:_PauseEveryThing(false)
        end,
    })
end

CGCtrl.OnPlayVideo = HL.StaticMethod(HL.Opt(HL.Any)) << function(arg)
    local ctrl = CGCtrl.AutoOpen(PANEL_ID, arg, true)
    local path, rawName, beforeMaskData, afterMaskData = unpack(arg)
    ctrl:PlayCG(path, rawName, beforeMaskData, afterMaskData)
end

CGCtrl._HideBigLogo = HL.Method() << function(self)
    self.view.bigLogoMain.gameObject:SetActive(false)
    self.view.stretchImageMain.gameObject:SetActive(false)
end

CGCtrl._OnShowBigLogo = HL.Method(HL.Table) << function(self, args)
    local spritePath, useStretchImage, showOnTop, hideBackground = unpack(args)
    self.view.subtitleController:SetBigLogoImage(spritePath, useStretchImage, showOnTop, hideBackground)
end

CGCtrl._OnApplicationPause = HL.Method(HL.Any) << function(self, arg)

    local isPause = unpack(arg)
    if isPause == nil then
        isPause = false
    end


    if self.m_isPositivePausing then
        return
    end

    if not isPause then


        self.view.subtitleController.fmvDirector.time = self.view.subtitleController.fmvDirector.time
    end
    self:_PauseEveryThing(isPause)
end


CGCtrl._PauseEveryThing = HL.Method(HL.Boolean) << function(self, isPause)
    self.view.movieController:Pause(isPause)
    self.view.exUINode:SetPause(isPause)
    self.view.subtitleController:Pause(isPause)
    if isPause then
        AudioAdapter.PostEvent("au_global_contr_fmv_pause")
        GameInstance.audioManager.music:PauseMusic();
    else
        GameInstance.audioManager.music:ResumeMusic();
        AudioAdapter.PostEvent("au_global_contr_fmv_resume")
    end


end



if BEYOND_DEBUG or BEYOND_DEBUG_COMMAND then
    CGCtrl.m_debugCor = HL.Field(HL.Thread)

    CGCtrl._StartDisplayDebugInfo = HL.Method() << function(self)
        if DISABLE_VIDEO_DEBUG_INFO then
            return
        end

        local VideoPlayer = require_ex("UI/Widgets/VideoPlayer")
        self.view.videoDebugNode.gameObject:SetActive(true)
        if self.m_debugCor == nil then
            self.m_debugCor = self:_StartCoroutine(function()
                while true do
                    if self.view.movieController.player then
                        local info = VideoPlayer.GetDebugInfoStrFromPlayer(self.view.movieController.player)
                        info = info .. string.format("DirectorTime: %.2f\n GameTime: %.2f", self.view.subtitleController.fmvDirector.time, Time.unscaledTime)
                        self.view.videoDebugNode.debugText.text = info
                    end
                    coroutine.step()
                end
            end)
        end
    end

    CGCtrl._StopDisplayDebugInfo = HL.Method() << function(self)
        if self.m_debugCor then
            self:_ClearCoroutine(self.m_debugCor)
            self.m_debugCor = nil
        end
        self.view.videoDebugNode.gameObject:SetActive(false)
    end
end




HL.Commit(CGCtrl)
