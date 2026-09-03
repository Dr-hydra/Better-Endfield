local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')




VideoPlayer = HL.Class('VideoPlayer', UIWidgetBase)

local PlayerStatus = CS.CriWare.CriMana.Player.Status


VideoPlayer.m_preparingVideo = HL.Field(HL.Any) << nil

VideoPlayer.m_preparedVideo = HL.Field(HL.Any) << nil

VideoPlayer.m_playingVideo = HL.Field(HL.Any) << nil

VideoPlayer.m_stateChangeListener = HL.Field(HL.Table)

VideoPlayer.m_videoVolume = HL.Field(HL.Number) << 0

VideoPlayer.preloadStartTime = HL.Field(HL.Number) << -1

VideoPlayer.preloadKeepForever = HL.Field(HL.Boolean) << false

VideoPlayer.m_manualUpdateCor = HL.Field(HL.Thread)

VideoPlayer.m_onCanvasChangedClosure = HL.Field(HL.Function)

VideoPlayer.m_videoAudioKey = HL.Field(HL.String) << ""

VideoPlayer.m_videoAudioPlayingId = HL.Field(HL.Number) << 0

VideoPlayer.m_audioStartUnscaledTime = HL.Field(HL.Number) << 0

VideoPlayer.m_audioSyncUpdateKey = HL.Field(HL.Number) << -1


VideoPlayer._OnCreate = HL.Override() << function(self)
    self.m_stateChangeListener = {}
end

VideoPlayer._OnDestroy = HL.Override() << function(self)
    self:StopAudio()
    self:StopAutoKeepAspectRatio()
end

VideoPlayer._OnEnable = HL.Override() << function(self)
    self:_StopManualUpdate()

    if BEYOND_DEBUG or BEYOND_DEBUG_COMMAND then
        self:_StartDisplayDebugInfo()
    end
end

VideoPlayer._OnDisable = HL.Override() << function(self)
    self:StopAudio()

    if self.m_preparingVideo then
        self:_StartManualUpdate()
    end

    if BEYOND_DEBUG or BEYOND_DEBUG_COMMAND then
        self:_StopDisplayDebugInfo()
    end
end

VideoPlayer._StartManualUpdate = HL.Method() << function(self)
    if not self.m_manualUpdateCor then
        self.m_manualUpdateCor = self:_StartCoroutine(function() return self:_ManualUpdate() end)
    end
end

VideoPlayer._StopManualUpdate = HL.Method() << function(self)
    if self.m_manualUpdateCor then
        self:_ClearCoroutine(self.m_manualUpdateCor)
        self.m_manualUpdateCor = nil
    end
end


VideoPlayer._ManualUpdate = HL.Method() << function(self)
    while not self.gameObject.activeInHierarchy do
        self.view.movieController:PlayerManualUpdate()
        coroutine.step()
    end
end


VideoPlayer.SetVideoVolume = HL.Method(HL.Number) << function(self, volume)
    self.m_videoVolume = volume
end

VideoPlayer._LoadAndPlayVideo = HL.Method(HL.String) << function(self, path)
    self.view.movieController.player:EasySetFile(nil, path)
    self.view.movieController.player:EasyStart()
end

VideoPlayer._EnsurePlayer = HL.Method() << function(self)
    if self.view.movieController.player == nil then
        self.view.movieController:PlayerManualInitialize()
    end

    if self.view.movieController.player.statusChangeCallback == nil then

        self.view.movieController.player.statusChangeCallback = function(status) self:OnPlayerStateChange(status) end
    end
end

VideoPlayer.PreloadVideo = HL.Method(HL.String, HL.Opt(HL.Function, HL.Function)) << function(self, path, onPlayerReady, onPlayerError)
    if self.m_preparingVideo == path or self.m_preparedVideo == path then
        return
    end

    self:_EnsurePlayer()

    self:_ClearStateChangeListener()
    self:_AddStateChangeListener(PlayerStatus.Ready, function()
        if onPlayerReady then
            onPlayerReady(self.view.movieController)
        end
        self.m_preparedVideo = path
        self.m_preparingVideo = nil
        self:_StopManualUpdate()

    end)
    self:_AddStateChangeListener(PlayerStatus.Error, function(state)
        if self.m_preparingVideo ~= path then
            return
        end

        self:_HandlePlayerError(path, state)
        if onPlayerError then
            onPlayerError(state, self.view.movieController)
        elseif onPlayerReady then
            self:StopVideo()
            onPlayerReady(self.view.movieController)
        else
            self:StopVideo()
        end
    end)










    self.m_preparingVideo = path
    self.preloadStartTime = Time.unscaledTime
    self.view.movieController.player:SetFile(nil, path);
    self.view.movieController.player:Prepare()
    self.view.canvasGroup.alpha = 0.0

    if not self.gameObject.activeInHierarchy then
        self:_StartManualUpdate()
    end
end


VideoPlayer.StartAutoKeepAspectRatio = HL.Method(HL.Opt(HL.Boolean)) << function(self, disableSafeZone)
    if self.m_onCanvasChangedClosure then
        return
    end

    if disableSafeZone == nil then
        disableSafeZone = false
    end

    self:SetVideoImageOffset(disableSafeZone)
    self.m_onCanvasChangedClosure = function() self:SetVideoImageOffset(disableSafeZone) end
    UIManager.m_uiCanvasScaleHelper.onCanvasChanged:AddListener(self.m_onCanvasChangedClosure)
end

VideoPlayer.StopAutoKeepAspectRatio = HL.Method() << function(self)
    if self.m_onCanvasChangedClosure then
        UIManager.m_uiCanvasScaleHelper.onCanvasChanged:RemoveListener(self.m_onCanvasChangedClosure)
        self.m_onCanvasChangedClosure = nil
    end
end

VideoPlayer.SetUserTimeCorrectionThreshold = HL.Method(HL.Int) << function(self, threshold)
    if threshold ~= nil and threshold >= 0 then
        self.view.movieController.player:SetUserTimeCorrectionThreshold(threshold)
    end
end



VideoPlayer.PlayVideo = HL.Method(HL.String, HL.Opt(HL.Function, HL.Function, HL.Function)).Return(HL.Boolean) <<
function(self, path, onPlayStart, onPlayEnd, onPlayError)
    if not self.gameObject.activeInHierarchy then
        logger.error("Set GameObject Active Before Play Video !!", path)
        return false
    end

    self:_EnsurePlayer()

    local onPlayerPlaying = function(state, movieController)
        if onPlayStart then
            onPlayStart(state, movieController)
        end
    end
    local onPlayerPlayEnd = function(state, movieController)
        self.m_stateChangeListener[PlayerStatus.Error] = nil
        if onPlayEnd then
            onPlayEnd(state, movieController)
        end
    end
    local onPlayerError = function(state, movieController)
        self:_HandlePlayerError(path, state)
        if onPlayError then
            onPlayError(state, movieController)
        else
            self:StopVideo()
        end
    end

    self.m_playingVideo = path
    if self.m_preparedVideo == path then
        self:_AddStateChangeListener(PlayerStatus.Playing, onPlayerPlaying)
        self:_AddStateChangeListener(PlayerStatus.PlayEnd, onPlayerPlayEnd)
        self:_AddStateChangeListener(PlayerStatus.Error, onPlayerError)
        self:_TriggerPlay()
        return true
    elseif self.m_preparingVideo == path then

        self:_AddStateChangeListener(PlayerStatus.Ready, function() self:_TriggerPlay() end)
        self:_AddStateChangeListener(PlayerStatus.Playing, onPlayerPlaying)
        self:_AddStateChangeListener(PlayerStatus.PlayEnd, onPlayerPlayEnd)
        self:_AddStateChangeListener(PlayerStatus.Error, onPlayerError)
        return false
    else
        self:_ClearStateChangeListener()
        self:_AddStateChangeListener(PlayerStatus.Playing, onPlayerPlaying)
        self:_AddStateChangeListener(PlayerStatus.PlayEnd, onPlayerPlayEnd)
        self:_AddStateChangeListener(PlayerStatus.Error, onPlayerError)
        self:_LoadAndPlayVideo(path)
        return false
    end
end




VideoPlayer.ReplayVideo = HL.Method(HL.Opt(HL.Function, HL.Function, HL.Function)) <<
function(self, onPlayStart, onPlayEnd, onPlayError)
    local path = self.m_playingVideo

    local onPlayerPlaying = function(state, movieController)
        if onPlayStart then
            onPlayStart(state, movieController)
        end
    end
    local onPlayerPlayEnd = function(state, movieController)
        self.m_stateChangeListener[PlayerStatus.Error] = nil
        if onPlayEnd then
            onPlayEnd(state, movieController)
        end
    end
    local onPlayerError = function(state, movieController)
        self:_HandlePlayerError(path, state)
        if onPlayError then
            onPlayError(state, movieController)
        else
            self:StopVideo()
        end
    end

    self:_AddStateChangeListener(PlayerStatus.Playing, onPlayerPlaying)
    self:_AddStateChangeListener(PlayerStatus.PlayEnd, onPlayerPlayEnd)
    self:_AddStateChangeListener(PlayerStatus.Error, onPlayerError)
    self:_TriggerPlay()
end

VideoPlayer.SetLoop = HL.Method(HL.Boolean) << function(self, enable)
    if self.view.movieController.player then
        self.view.movieController.player:Loop(enable)
    end
end


VideoPlayer.SetVideoImageOffset = HL.Method(HL.Boolean) << function(self, noSafeZone)
    if noSafeZone == nil then
        noSafeZone = false
    end

    local player = self.view.movieController.player
    if player == nil then
        return
    end

    local movieInfo = player.movieInfo
    if movieInfo then
        local screenWidth = self.view.image.transform.rect.width
        local screenHeight = self.view.image.transform.rect.height
        local w = movieInfo.dispWidth
        local h = movieInfo.dispHeight

        local offsetMin, offsetMax = FMVUtils.GetSuitableFMVImageOffset(screenWidth, screenHeight, w, h, noSafeZone)
        self.view.movieController.transform.offsetMin = offsetMin
        self.view.movieController.transform.offsetMax = offsetMax
    end
end

VideoPlayer.StopVideo = HL.Method(HL.Opt(HL.Boolean)) << function(self, dispose)
    self:StopAudio()
    self:_StopManualUpdate()
    self:_ClearStateChangeListener()
    self.view.movieController:Stop()
    self.m_preparedVideo = nil
    self.m_preparingVideo = nil
    self.m_playingVideo = nil

    if dispose == true then
        self:Dispose()
    end
end

VideoPlayer.GetVideoTotalTime = HL.Method().Return(HL.Number) << function(self)
    local player = self.view.movieController.player
    if player == nil then
        return -1
    end

    local movieInfo = player.movieInfo
    if movieInfo == nil then
        return -1
    end

    if movieInfo.framerateD == 0 or movieInfo.framerateN == 0 then
        return -1
    end

    local totalFrame = movieInfo.totalFrames
    local frameRate = movieInfo.framerateN / movieInfo.framerateD

    return totalFrame / frameRate
end

VideoPlayer.GetTime = HL.Method().Return(HL.Number) << function(self)
    local player = self.view.movieController.player
    if player == nil then
        return -1
    end
    local frameInfo = player.frameInfo
    if frameInfo == nil then
        return -1
    end
    return frameInfo.time / frameInfo.tunit
end

VideoPlayer.Dispose = HL.Method() << function(self)

    self:StopAutoKeepAspectRatio()
    self.view.movieController:RenderTargetManualFinalize()
    self.view.movieController:PlayerManualFinalize()
end

VideoPlayer._TriggerPlay = HL.Method() << function(self)
    self.m_preparedVideo = nil
    self.view.canvasGroup.alpha = 1.0
    self.view.movieController.player:SetVolume(self.m_videoVolume)
    self.view.movieController:Play()
end

VideoPlayer._HandlePlayerError = HL.Method(HL.String, HL.Any) << function(self, path, state)
    logger.error("VideoPlayer player error", path, state)
    self:StopAudio()
    self:_StopManualUpdate()
    self:_ClearStateChangeListener()
    if self.m_preparingVideo == path then
        self.m_preparingVideo = nil
    end
    if self.m_preparedVideo == path then
        self.m_preparedVideo = nil
    end
    if self.m_playingVideo == path then
        self.m_playingVideo = nil
    end

    self.view.movieController:Stop()
end

VideoPlayer.OnPlayerStateChange = HL.Method(HL.Any) << function(self, state)
    local listenerList = self.m_stateChangeListener[state]
    if listenerList then
        self.m_stateChangeListener[state] = nil
        for _, listener in ipairs(listenerList) do
            listener(state, self.view.movieController)
        end
    end

    if state == PlayerStatus.Error then
        return
    end



    listenerList = self.m_stateChangeListener[state]
    if listenerList then
        self.m_stateChangeListener[state] = nil
        for _, listener in ipairs(listenerList) do
            listener(state, self.view.movieController)
        end
    end
end

VideoPlayer._AddStateChangeListener = HL.Method(HL.Any, HL.Function) << function(self, state, listener)
    if listener == nil then
        return
    end

    local listenerList = self.m_stateChangeListener[state]
    if not listenerList then
        listenerList = {}
        self.m_stateChangeListener[state] = listenerList
    end
    table.insert(listenerList, listener)
end




VideoPlayer._ClearStateChangeListener = HL.Method() << function(self)
    self.m_stateChangeListener = {}
end




local AUDIO_VIDEO_SEEK_THRESHOLD = 0.7

VideoPlayer.PlayAudio = HL.Method(HL.String) << function(self, audioKey)
    self:StopAudio()
    self.m_videoAudioKey = audioKey
    self.m_videoAudioPlayingId = AudioAdapter.PostEvent(audioKey)
    self.m_audioStartUnscaledTime = Time.unscaledTime
    self:_StartAudioSync()
end

VideoPlayer.StopAudio = HL.Method() << function(self)
    self:_StopAudioSync()
    if self.m_videoAudioPlayingId ~= 0 then
        AudioAdapter.StopByPlayingId(self.m_videoAudioPlayingId, 0)
        self.m_videoAudioPlayingId = 0
    end
    self.m_videoAudioKey = ""
    self.m_audioStartUnscaledTime = 0
end

VideoPlayer._StartAudioSync = HL.Method() << function(self)
    if self.m_audioSyncUpdateKey ~= -1 then
        return
    end
    self.m_audioSyncUpdateKey = LuaUpdate:Add("Tick", function()
        self:_SyncAudio()
    end)
end

VideoPlayer._StopAudioSync = HL.Method() << function(self)
    if self.m_audioSyncUpdateKey ~= -1 then
        LuaUpdate:Remove(self.m_audioSyncUpdateKey)
        self.m_audioSyncUpdateKey = -1
    end
end

VideoPlayer._SyncAudio = HL.Method() << function(self)
    if self.m_videoAudioPlayingId == 0 then
        return
    end
    local videoPos = self:GetTime()
    if videoPos < 0 then
        return
    end

    local audioUnscaledPlayedTime = Time.unscaledTime - self.m_audioStartUnscaledTime
    if math.abs(audioUnscaledPlayedTime - videoPos) > AUDIO_VIDEO_SEEK_THRESHOLD then
        local seekTimeMs = math.ceil(videoPos * 1000)
        AudioAdapter.SeekOnEvent(self.m_videoAudioKey, seekTimeMs, false, self.m_videoAudioPlayingId)

        self.m_audioStartUnscaledTime = Time.unscaledTime - videoPos
    end
end



if BEYOND_DEBUG or BEYOND_DEBUG_COMMAND then
    VideoPlayer.m_debugCor = HL.Field(HL.Thread)

    VideoPlayer._StartDisplayDebugInfo = HL.Method() << function(self)
        if DISABLE_VIDEO_DEBUG_INFO then
            return
        end

        self.view.videoDebugNode.gameObject:SetActive(true)
        if self.m_debugCor == nil then
            self.m_debugCor = self:_StartCoroutine(function()
                while true do
                    if self.view.movieController.player then
                        local info = VideoPlayer.GetDebugInfoStrFromPlayer(self.view.movieController.player)
                        self.view.videoDebugNode.debugText.text = info
                    end
                    coroutine.step()
                end
            end)
        end
    end

    VideoPlayer._StopDisplayDebugInfo = HL.Method() << function(self)
        if self.m_debugCor then
            self:_ClearCoroutine(self.m_debugCor)
            self.m_debugCor = nil
        end
        self.view.videoDebugNode.gameObject:SetActive(false)
    end

    VideoPlayer.GetDebugInfoStrFromPlayer = HL.StaticMethod(HL.Userdata).Return(HL.String) << function(player)
        if player == nil then
            return "Player is nil"
        end

        local movieInfo = player.movieInfo
        local frameInfo = player.frameInfo

        if movieInfo == nil or frameInfo == nil then
            return "MovieInfo or FrameInfo is Not Available"
        end

        local frameNo = frameInfo.frameNo
        local totalFrame = movieInfo.totalFrames
        local frameRate = movieInfo.framerateN / movieInfo.framerateD
        local width = movieInfo.dispWidth
        local height = movieInfo.dispHeight
        local time = frameInfo.time / frameInfo.tunit

        local formatterStr = "FProgress: %d/%d,\nFrameRate: %.2f,\nResolution: %dx%d,\nTime: %.2f \n"
        local info = string.format(formatterStr,
            frameNo, totalFrame,
            frameRate,
            width, height,
            time
        )

        return info
    end
end


HL.Commit(VideoPlayer)
return VideoPlayer

