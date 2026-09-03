local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.RemoteComm
local AUDIO_AMPLITUDE_TICK_INTERVAL = 0.1
local CHAR_CELL_DELAY_ANIM_IN = "remotecomm_charcell_in"
local BG_CELL_DELAY_ANIM_IN = "remotecomm_bannercell_first_in"
RemoteCommCtrl = HL.Class('RemoteCommCtrl', uiCtrl.UICtrl)







RemoteCommCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.REMOTE_COMM_PAUSE] = 'RemoteCommPause',
    [MessageConst.REMOTE_COMM_RESUME] = 'RemoteCommResume',
}

RemoteCommCtrl.m_singleData = HL.Field(Cfg.Types.RemoteCommonSingleData)

RemoteCommCtrl.m_charCellCache = HL.Field(HL.Forward("UIListCache"))

RemoteCommCtrl.m_actorList = HL.Field(HL.Table)

RemoteCommCtrl.m_midId = HL.Field(HL.String) << ""

RemoteCommCtrl.m_updateKey = HL.Field(HL.Number) << -1

RemoteCommCtrl.m_rightList = HL.Field(HL.Table)

RemoteCommCtrl.m_rightNum = HL.Field(HL.Number) << 0

RemoteCommCtrl.m_cellRealRefreshFunc = HL.Field(HL.Table)

RemoteCommCtrl.m_cellTimer = HL.Field(HL.Table)

RemoteCommCtrl.m_playingId = HL.Field(HL.Number) << -1

RemoteCommCtrl.m_voiceHandleId = HL.Field(HL.Number) << -1

RemoteCommCtrl.m_voiceTimer = HL.Field(HL.Number) << -1

RemoteCommCtrl.m_aSynActionHelpers = HL.Field(HL.Table)

RemoteCommCtrl.m_timer = HL.Field(HL.Number) << 0

RemoteCommCtrl.m_inited = HL.Field(HL.Boolean) << false

RemoteCommCtrl.m_rightPosTable = HL.Field(HL.Table)

RemoteCommCtrl.m_audioId = HL.Field(HL.String) << ""

RemoteCommCtrl.m_musicId = HL.Field(HL.String) << ""

RemoteCommCtrl.m_voiceId = HL.Field(HL.String) << ""


RemoteCommCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_actorList = {}
    self.m_rightList = {}
    self.m_cellRealRefreshFunc = {}
    self.m_cellTimer = {}
    self.m_aSynActionHelpers = {}
    self.m_rightPosTable = {}
    self.m_midId = ""
    self.m_charCellCache = UIUtils.genCellCache(self.view.defaultCell)
end

RemoteCommCtrl.RefreshInfo = HL.Method(Cfg.Types.RemoteCommonSingleData) << function(self, singleData)
    if not self.m_inited then
        self:_InitRightPos()
    end

    self.m_singleData = singleData
    self:_RefreshLeft()
    self:_RefreshMiddle()
    self:_RefreshRight()
    self.m_inited = true
end

RemoteCommCtrl._InitRightPos = HL.Method() << function(self)
    for i = 1, self.view.config.RIGHT_CELL_MAX_NUM do
        local cell = self.view[string.format("bannerCell%d", i)]
        local posY = cell.transform.anchoredPosition.y
        table.insert(self.m_rightPosTable, posY)
    end
end

RemoteCommCtrl._TryPlayCellIn = HL.Method(HL.Table, HL.Opt(HL.Number, HL.String)) << function(self, cell, delay, anim)
    local animationWrapper = cell.animationWrapper
    local timer = self.m_cellTimer[cell]
    if timer then
        self:_ClearTimer(timer)
    end

    self.m_cellTimer[cell] = nil

    if delay and delay > 0 then
        if not string.isEmpty(anim) then
            animationWrapper:SampleClipAtPercent(anim, 0)
        end

        self.m_cellTimer[cell] = self:_StartTimer(delay, function()
            if not string.isEmpty(anim) then
                animationWrapper:PlayWithTween(anim)
            else
                animationWrapper:PlayInAnimation()
            end
            self.m_cellTimer[cell] = nil
        end)
    else
        if not string.isEmpty(anim) then
            animationWrapper:PlayWithTween(anim)
        else
            animationWrapper:PlayInAnimation()
        end
    end
end

RemoteCommCtrl._RefreshCellWithAnim = HL.Method(HL.Table, HL.Boolean, HL.Boolean, HL.Opt(HL.Number, HL.String)) <<
    function(self, cell, outAnim, inAnim, inDelay, anim)
        local animationWrapper = cell.animationWrapper
        if animationWrapper.curState ~= UIConst.UI_ANIMATION_WRAPPER_STATE.Out then
            if outAnim then
                animationWrapper:PlayOutAnimation(function()
                    local refreshFunc = self.m_cellRealRefreshFunc[cell]
                    if refreshFunc then
                        refreshFunc()
                    end
                    self.m_cellRealRefreshFunc[cell] = nil
                    if inAnim then
                        self:_TryPlayCellIn(cell, inDelay, anim)
                    end
                end)
            elseif inAnim then
                local refreshFunc = self.m_cellRealRefreshFunc[cell]
                if refreshFunc then
                    refreshFunc()
                end
                self:_TryPlayCellIn(cell, inDelay, anim)
            end
        end
    end

RemoteCommCtrl._RefreshSingleActor = HL.Method(HL.Table, HL.Any) << function(self, cell, actorId)
    if string.isEmpty(actorId) then
        cell.gameObject:SetActive(false)
    else
        cell.gameObject:SetActive(true)
        local charUtils = CS.Beyond.Gameplay.CharUtils
        local endminVirtualCharTemplateID = charUtils.endminVirtualCharTemplateID
        if actorId == endminVirtualCharTemplateID then
            actorId = charUtils.curEndminCharTemplateId
        end
        local res, actorImageData = Tables.actorImageTable:TryGetValue(actorId)
        if res then
            cell.char:LoadSprite(UIConst.UI_SPRITE_CHAR_REMOTE_ICON, actorImageData.bustPath)
        end
    end
end

RemoteCommCtrl._RefreshSingleBG = HL.Method(HL.Table, HL.Any) << function(self, cell, imageId)
    if imageId then
        local spritePath = UIUtils.getSpritePath(UIConst.UI_SPRITE_VIDEO_COVER, imageId)
        local res = ResourceManager.CheckExists(spritePath)

        if res then
            cell.banner:LoadSprite(UIConst.UI_SPRITE_VIDEO_COVER, imageId)
        else
            cell.banner:LoadSprite(imageId)
        end
    end
end

RemoteCommCtrl._IsVideo = HL.Method().Return(HL.Boolean, HL.Any) << function(self)
    local middleId = self.m_singleData.middleId
    local videoKey = "Narrative/RemoteComm/" .. middleId
    local isVideo, path = CS.Beyond.Gameplay.View.VideoManager.TryGetVideoPlayFullPath(videoKey)
    return isVideo, path
end

RemoteCommCtrl._RefreshMidContent = HL.Method() << function(self)
    local isVideo, path = self:_IsVideo()
    local middleId = self.m_singleData.middleId
    local charUtils = CS.Beyond.Gameplay.CharUtils
    local endminVirtualCharTemplateID = charUtils.endminVirtualCharTemplateID
    if middleId == endminVirtualCharTemplateID then
        middleId = charUtils.curEndminCharTemplateId
    end

    local isActor, actorImageData = Tables.actorImageTable:TryGetValue(middleId)
    self.view.videoImage.gameObject:SetActive(true)
    local image = self.view.videoImage.gameObject:GetComponent("UIImage")
    image.enabled = isVideo
    self.view.charinfoNodeWrapper.gameObject:SetActive(isActor)
    self.view.bgMask.gameObject:SetActive(not isActor and not isVideo)

    if isVideo then
        local isVideoLoop = self.m_singleData.isVideoLoop

        self.view.videoImage.player:EasySetFile(nil, path)
        self.view.videoImage.player:EasyStart()
        self.view.videoImage.player:Loop(isVideoLoop)
        self.view.videoImage.player.applyTargetAlpha = true

        self.view.videoImage.player.statusChangeCallback = nil
        self.view.videoImage.player.statusChangeCallback = function(status)
            local movieInfo = self.view.videoImage.player.movieInfo
            if status == CS.CriWare.CriMana.Player.Status.Playing and movieInfo then
                local imageWidth = self.view.videoImage.transform.rect.width
                local imageHeight = self.view.videoImage.transform.rect.height
                local fmvWidth = movieInfo.dispWidth
                local fmvHeight = movieInfo.dispHeight
                local imageRatio = imageWidth / imageHeight
                local targetRatio = fmvWidth / fmvHeight

                if imageRatio < targetRatio then
                    imageHeight = imageWidth / targetRatio
                    self.view.videoImage.transform.anchorMin = Vector2(0.5, 0.5)
                    self.view.videoImage.transform.anchorMax = Vector2(0.5, 0.5)
                    self.view.videoImage.transform.pivot = Vector2(0.5, 0.5)
                    self.view.videoImage.transform.anchoredPosition = Vector2.zero
                    self.view.videoImage.transform.sizeDelta = Vector2(imageWidth, imageHeight)
                elseif imageRatio > targetRatio then
                    imageWidth = imageHeight * targetRatio
                    self.view.videoImage.transform.anchorMin = Vector2(0.5, 0.5)
                    self.view.videoImage.transform.anchorMax = Vector2(0.5, 0.5)
                    self.view.videoImage.transform.pivot = Vector2(0.5, 0.5)
                    self.view.videoImage.transform.anchoredPosition = Vector2.zero
                    self.view.videoImage.transform.sizeDelta = Vector2(imageWidth, imageHeight)
                end


                if not string.isEmpty(self.m_audioId) or not string.isEmpty(self.m_musicId) then
                    self:_DoPlayAudio("", self.m_audioId, self.m_musicId)
                    self.m_audioId = ""
                    self.m_musicId = ""
                end
            end
        end

        self.view.videoImage.player:SetVolume(0)
    elseif isActor then
        self.view.charImageEx:LoadSprite(UIConst.UI_SPRITE_CHAR_REMOTE_ICON_700, actorImageData.bustPath)
    else
        self.view.bgImage:LoadSprite(middleId)
    end

    local wrapper
    if isVideo then
        wrapper = self.view.videoAnimationWrapper
    elseif isActor then
        wrapper = self.view.charinfoNodeWrapper
    else
        wrapper = self.view.bgAnimationWrapper
    end

    wrapper:PlayInAnimation()

end

RemoteCommCtrl._TryRefreshCell = HL.Method(HL.Table, HL.Any, HL.Any, HL.Function, HL.Opt(HL.Number, HL.String)) << function(self,
                                                                                                                            cell,
                                                                                                                            actorId,
                                                                                                                            lastActorId,
                                                                                                                            refreshFunc,
                                                                                                                            inDelay,
                                                                                                                            anim)
    if lastActorId then
        if actorId then
            if actorId ~= lastActorId then
                self.m_cellRealRefreshFunc[cell] = refreshFunc
                self:_RefreshCellWithAnim(cell, true, true, inDelay, anim)
            end
        else
            self.m_cellRealRefreshFunc[cell] = refreshFunc
            self:_RefreshCellWithAnim(cell, true, false, 0, anim)
        end
    else
        if actorId then
            cell.gameObject:SetActive(true)
            self.m_cellRealRefreshFunc[cell] = refreshFunc
            self:_RefreshCellWithAnim(cell, false, true, inDelay, anim)
        else
            cell.gameObject:SetActive(false)
        end
    end
end

RemoteCommCtrl._RefreshLeft = HL.Method() << function(self)
    local actorList = self.m_singleData.actorList

    if actorList.Count > UIConst.REMOTE_COMM_CELL_MAX_NUM then
        logger.error("RemoteComm: " .. self.m_singleData.singleId .. " actorList Count > 4!!!")
    end

    local selectedCell = self.view.selectedCell
    local defaultCount = math.max(actorList.Count - 1, 0)
    local selectActorId = actorList.Count > 0 and actorList[0]

    local lastSelectActorId = self.m_actorList[1]

    local delayTime
    if not self.m_inited then
        delayTime = self.view.config.CHAR_CELL_ANIM_IN_DELAY
    else
        delayTime = 0
    end

    self:_TryRefreshCell(selectedCell, selectActorId, lastSelectActorId, function()
        self:_RefreshSingleActor(selectedCell, selectActorId)
    end, delayTime)

    for i = 1, self.m_charCellCache:GetCount() do
        local cell = self.m_charCellCache:Get(i)
        local actorId
        local lastActorId = self.m_actorList[i + 1]
        if defaultCount >= i then
            actorId = actorList[i]
        end

        local anim = ""
        if not self.m_inited then
            anim = CHAR_CELL_DELAY_ANIM_IN
            delayTime = self.view.config.CHAR_CELL_ANIM_IN_DELAY + self.view.config.CELL_IN_ANIM_DELAY * i
        else
            delayTime = 0
        end

        self:_TryRefreshCell(cell, actorId, lastActorId, function()
            self:_RefreshSingleActor(cell, actorId)
        end, delayTime, anim)
    end


    self.m_actorList = {}
    for _, actorId in pairs(actorList) do
        table.insert(self.m_actorList, actorId)
    end
end

RemoteCommCtrl._StopVoice = HL.Method() << function(self)
    if self.m_voiceHandleId > 0 then
        VoiceManager:StopVoice(self.m_voiceHandleId)
        self.m_voiceHandleId = -1
    end

    self:_SwitchSelectedCellAnim(false)
    self:_ClearVoiceTimer()
end

RemoteCommCtrl._StopAudio = HL.Method() << function(self)
    if self.m_playingId > 0 then
        GameAction.StopAudio(self.m_playingId)
        self.m_playingId = -1
    end
end

RemoteCommCtrl._SwitchSelectedCellAnim = HL.Method(HL.Boolean) << function(self, enable)
    local talkLight = self.view.selectedCell.talkLight
    talkLight.gameObject:SetActive(enable)
end

RemoteCommCtrl._ClearVoiceTimer = HL.Method() << function(self)
    if self.m_voiceTimer > 0 then
        self:_ClearTimer(self.m_voiceTimer)
    end
    self.m_voiceTimer = -1
end

RemoteCommCtrl._DoPlayAudio = HL.Method(HL.String, HL.String, HL.String) << function(self, voiceId, audioId, musicId)
    if not string.isEmpty(voiceId) then
        local res, duration = VoiceUtils.TryGetVoiceDuration(voiceId)
        if res then
            self:_SwitchSelectedCellAnim(true)
            self.m_voiceHandleId = VoiceManager:SpeakNarrative(voiceId, nil, CS.Beyond.Gameplay.Audio.NarrativeVoiceConfig.DEFAULT_CONFIG)
            self.m_voiceTimer = self:_StartTimer(duration, function()
                self:_SwitchSelectedCellAnim(false)
            end)
        end
    end


    if not string.isEmpty(audioId) then
        if self.m_playingId > 0 then
            GameAction.StopAudio(self.m_playingId)
        end

        self.m_playingId = GameAction.PlayAudio(audioId)
    end

    if not string.isEmpty(musicId) then
        GameAction.PostAudioCue(musicId)
    end
end

RemoteCommCtrl._TryPlayAudio = HL.Method() << function(self)

    self:_StopVoice()

    local voiceId = self.m_singleData.voiceId
    local audioId = self.m_singleData.audioId
    local musicId = self.m_singleData.musicId

    local isVideo, path = self:_IsVideo()

    if not self.m_inited then
        self.m_voiceId = voiceId
        self.m_audioId = audioId
    else
        if isVideo then
            self.m_audioId = audioId
            self:_DoPlayAudio(voiceId, "", "")
        else
            self:_DoPlayAudio(voiceId, audioId, musicId)
        end
    end
end

RemoteCommCtrl._RefreshMiddle = HL.Method() << function(self)
    local lastMid = self.m_midId

    if string.isEmpty(lastMid) then
        self:_TryPlayAudio()
        self:_RefreshMidContent()
    elseif lastMid ~= self.m_singleData.middleId then
        local videoKey = "Narrative/RemoteComm/" .. lastMid
        local lastIsVideo, _ = CS.Beyond.Gameplay.View.VideoManager.TryGetVideoPlayFullPath(videoKey)
        local lastIsActor, _ = Tables.actorImageTable:TryGetValue(lastMid)
        local lastWrapper
        if lastIsVideo then
            lastWrapper = self.view.videoAnimationWrapper
        elseif lastIsActor then
            lastWrapper = self.view.charinfoNodeWrapper
        else
            lastWrapper = self.view.bgAnimationWrapper
        end

        if lastWrapper.curState ~= UIConst.UI_ANIMATION_WRAPPER_STATE.Out then
            local needPlayVoice = self.m_singleData.index > 0
            local curAudioId = self.m_singleData.audioId
            local curMusicId = self.m_singleData.musicId
            lastWrapper:PlayOutAnimation(function()
                if needPlayVoice then
                    self:_TryPlayAudio()
                elseif not string.isEmpty(curAudioId) or not string.isEmpty(curMusicId) then

                    self:_DoPlayAudio("", curAudioId, curMusicId)
                end
                self:_RefreshMidContent()
            end)
        end
    else
        self:_TryPlayAudio()
    end
    self.m_midId = self.m_singleData.middleId
end

RemoteCommCtrl._GetTargetPos = HL.Method(HL.Number).Return(HL.Number) << function(self, i)
    local firstNum = self.m_rightNum - #self.m_rightList + 1
    local firstPos = self.view.bannerCell1.transform.anchoredPosition
    local targetPosY = firstPos.y - (i - firstNum - 1) * self.view.config.RIGHT_CELL_HEIGHT
    return targetPosY
end

RemoteCommCtrl._RightNext = HL.Method() << function(self)
    local imageList = self.m_singleData.imageList
    local count = imageList.Count

    local firstNum = self.m_rightNum - #self.m_rightList + 1
    local firstCell = self.view[string.format("bannerCell%d", firstNum)]

    local animationWrapper = firstCell.animationWrapper
    animationWrapper:PlayWithTween("remotecomm_bannercell_out")


    for i = firstNum + 1, self.view.config.RIGHT_CELL_MAX_NUM do
        local delayTime = self.view.config.CELL_IN_ANIM_DELAY * (i - 1)
        local cell = self.view[string.format("bannerCell%d", i)]
        cell.transform:DOKill()
        cell.transform:DOAnchorPosY(self:_GetTargetPos(i), self.view.config.RIGHT_CELL_ANIM_TIME):SetDelay(delayTime):SetEase(self.view.config.RIGHT_CELL_ANIM_CURVE)
    end

    if count >= self.view.config.RIGHT_CELL_REVEAL_NUM then
        local delayTime = self.view.config.CELL_IN_ANIM_DELAY * (firstNum + self.view.config.RIGHT_CELL_REVEAL_NUM - 1)
        local lastCell = self.view[string.format("bannerCell%d", firstNum + self.view.config.RIGHT_CELL_REVEAL_NUM)]
        self:_RefreshCellWithAnim(lastCell, false, true, delayTime, "remotecomm_bannercell_in")
    end

    if count == 0 then
        self.m_rightNum = 0
    end
end

RemoteCommCtrl._ClearRight = HL.Method() << function(self)
    for i = 1, self.view.config.RIGHT_CELL_MAX_NUM do
        local cell = self.view[string.format("bannerCell%d", i)]
        cell.transform:DOKill()
    end
end

RemoteCommCtrl._InitRight = HL.Method() << function(self)
    local imageList = self.m_singleData.imageList
    local count = imageList.Count
    for i = 1, self.view.config.RIGHT_CELL_MAX_NUM do
        local cell = self.view[string.format("bannerCell%d", i)]
        local imageId
        if i <= count then
            imageId = imageList[CSIndex(i)]
        end

        local delayTime = self.view.config.CELL_IN_ANIM_DELAY * (i - 1)
        local anim = "remotecomm_bannercell_first_in"
        if not self.m_inited then
            delayTime = self.view.config.CHAR_CELL_ANIM_IN_DELAY + delayTime
        end
        cell.transform.anchoredPosition = Vector2(cell.transform.anchoredPosition.x, self.m_rightPosTable[i])

        self:_TryRefreshCell(cell, imageId, nil, function()
            self:_RefreshSingleBG(cell, imageId)
        end, delayTime, anim)
    end
    self.m_rightNum = count
end

RemoteCommCtrl._RefreshRight = HL.Method() << function(self)
    local imageList = self.m_singleData.imageList
    local count = imageList.Count

    if count > UIConst.REMOTE_COMM_CELL_MAX_NUM then
        logger.error("RemoteComm: " .. self.m_singleData.singleId .. " imageList Count > 6!!!")
        return
    end


    if #self.m_rightList <= 0 and count >= 0 then
        self:_InitRight()
    elseif #self.m_rightList == count + 1 then
        self:_RightNext()
    end


    self.m_rightList = {}
    for _, imageId in pairs(imageList) do
        table.insert(self.m_rightList, imageId)
    end
end

RemoteCommCtrl.OnShow = HL.Override() << function(self)
    self.m_charCellCache:Refresh(UIConst.REMOTE_COMM_CELL_MAX_NUM - 1, nil, true, function(cell, index)
        cell.transform:DOKill()
    end)

    local material = self.loader:LoadMaterial("Assets/Beyond/DynamicAssets/Gameplay/UI/Materials/RemoteCommVideo.mat")
    self.view.videoImage.material = material
    self:_ClearRegisters()
    self:_AddRegisters()

    self.animationWrapper:PlayInAnimation(function()

        self.m_voiceId = self.m_voiceHandleId <= 0 and self.m_voiceId or ""
        if not string.isEmpty(self.m_audioId) or not string.isEmpty(self.m_voiceId) or not string.isEmpty(self.m_musicId) then
            self:_DoPlayAudio(self.m_voiceId, self.m_audioId, self.m_musicId)
            self.m_voiceId = ""
            self.m_audioId = ""
            self.m_musicId = ""
        end
    end)
end

RemoteCommCtrl.RemoteCommPause = HL.Method(HL.Opt(HL.Any)) << function(self)
    if self.m_playingId >= 0 then
        GameAction.PauseAudio(self.m_playingId)
    end

    if self.m_voiceHandleId > 0 then
        VoiceManager:PauseVoice(self.m_voiceHandleId)
    end

    local middleId = self.m_singleData.middleId
    local videoKey = "Narrative/RemoteComm/" .. middleId
    local isVideo, _ = CS.Beyond.Gameplay.View.VideoManager.TryGetVideoPlayFullPath(videoKey)
    if isVideo then
        self.view.videoImage.player:Pause(true)
    end
end

RemoteCommCtrl.RemoteCommResume = HL.Method(HL.Opt(HL.Any)) << function(self)
    if self.m_playingId >= 0 then
        GameAction.ResumeAudio(self.m_playingId)
    end

    if self.m_voiceHandleId > 0 then
        VoiceManager:ResumeVoice(self.m_voiceHandleId)
    end

    local middleId = self.m_singleData.middleId
    local videoKey = "Narrative/RemoteComm/" .. middleId
    local isVideo, _ = CS.Beyond.Gameplay.View.VideoManager.TryGetVideoPlayFullPath(videoKey)
    if isVideo then
        self.view.videoImage.player:Pause(false)
    end
end

RemoteCommCtrl._AddRegisters = HL.Method() << function(self)
    self.m_updateKey = LuaUpdate:Add("Tick", function(deltaTime)
        self.m_timer = self.m_timer + deltaTime
        if self.m_timer >= AUDIO_AMPLITUDE_TICK_INTERVAL then
            self:_Update()
            self.m_timer = 0
        end
        local player = self.view.videoImage.player
        if player and player.movieInfo and player:GetDisplayedFrameNo() >= player.movieInfo.totalFrames - player.maxFrameDrop then
            player:EasyPause()
        end
    end)
end

RemoteCommCtrl._ClearRegisters = HL.Method() << function(self)
    self.m_updateKey = LuaUpdate:Remove(self.m_updateKey)
end

RemoteCommCtrl._Update = HL.Method() << function(self)
    self:_UpdateAudioAmplitude()
end

RemoteCommCtrl._UpdateAudioAmplitude = HL.Method() << function(self)
    local res, amplitude = VoiceUtils.GetNarratingVoiceVolume()
    if res then
        self.view.voice.material:SetFloat("_GramTilling", amplitude * 10 + 5)
        self.view.voice.material:SetFloat("_GramScale", amplitude + 0.1)
    end
end

RemoteCommCtrl.OnClose = HL.Override() << function(self)
    self:_StopVoice()
    self:_StopAudio()
    self:_ClearRight()
    self:_ClearRegisters()
end

HL.Commit(RemoteCommCtrl)
