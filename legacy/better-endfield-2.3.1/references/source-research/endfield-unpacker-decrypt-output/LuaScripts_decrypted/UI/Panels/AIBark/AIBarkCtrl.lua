local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.AIBark
local TIMEOUT_TIME = 15

local SINGLE_CELL_TEXT_HEIGHT_LIMIT = 92

AIBarkCtrl = HL.Class('AIBarkCtrl', uiCtrl.UICtrl)







AIBarkCtrl.s_messages = HL.StaticField(HL.Table) << {

    [MessageConst.STOP_AI_BARK] = 'AudioStopSingleAIBark',
    [MessageConst.ON_SCENE_LOAD_START] = 'StopAllAIBark',
    [MessageConst.ALL_CHARACTER_DEAD] = 'StopAllAIBark',
    [MessageConst.ON_TELEPORT_SQUAD] = 'StopAllAIBark',
    [MessageConst.PLAY_CG] = 'StopAllAIBark',
    [MessageConst.ON_PLAY_CUTSCENE] = 'StopAllAIBark',
    [MessageConst.ON_DIALOG_START] = 'StopAllAIBark',
    [MessageConst.SHOW_RADIO] = 'StopAllAIBark',
    [MessageConst.HIDE_GUIDE_STEP] = 'StopAllAIBark',
    [MessageConst.START_GUIDE_GROUP] = 'StopAllAIBark',
    [MessageConst.ON_ULTIMATE_SKILL_START] = 'StopAllAIBark',
    [MessageConst.ON_ULTIMATE_SKILL_END] = 'StopAllAIBark',
}

AIBarkCtrl.s_inMainHud = HL.StaticField(HL.Boolean) << false

AIBarkCtrl.s_uniqueId = HL.StaticField(HL.Number) << 0

AIBarkCtrl.m_cellCache = HL.Field(HL.Table)

AIBarkCtrl.m_waitingCacheCells = HL.Field(HL.Table)

AIBarkCtrl.m_waitingTable = HL.Field(HL.Table)

AIBarkCtrl.m_timeoutTimer = HL.Field(HL.Number) << -1

AIBarkCtrl.m_tween = HL.Field(HL.Any)

AIBarkCtrl.s_showByExecute = HL.StaticField(HL.Boolean) << false











AIBarkCtrl.m_curPlayingBarks = HL.Field(HL.Table)

AIBarkCtrl.OnInMainHudChanged = HL.StaticMethod(HL.Table) << function(arg)
    local inMainHud = unpack(arg)
    if inMainHud then
        AIBarkCtrl.OnEnterMainHud()
    else
        AIBarkCtrl.OnLeaveMainHud()
    end
end


AIBarkCtrl.OnEnterMainHud = HL.StaticMethod() << function()
    AIBarkCtrl.s_inMainHud = true
end

AIBarkCtrl.OnLeaveMainHud = HL.StaticMethod() << function()
    AIBarkCtrl.s_inMainHud = false
    local res, ctrl = UIManager:IsOpen(PANEL_ID)
    if res then
        ctrl:StopAllAIBark()
    end
end

AIBarkCtrl.ShowAIBark = HL.StaticMethod(HL.Table) << function(arg)
    AIBarkCtrl.s_showByExecute = true
    local ctrl = AIBarkCtrl.AutoOpen(PANEL_ID, {}, true)
    AIBarkCtrl.s_showByExecute = false
    local data = unpack(arg)
    ctrl:DoShowAIBark(data)
end


AIBarkCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_cellCache = {}
    self.m_curPlayingBarks = {}
    self.m_waitingCacheCells = {}
    self.m_waitingTable = {}
    self.m_tween = nil
end

AIBarkCtrl.StopAllAIBark = HL.Method(HL.Opt(HL.Any)) << function(self, _)
    logger.info("StopAllAIBark, count: ", #self.m_curPlayingBarks)
    local count = #self.m_curPlayingBarks
    if count > 0 then
        for i = 1, count do
            local targetIndex = #self.m_curPlayingBarks
            if targetIndex <= 0 then
                break
            end
            self:_DoStopSingleAIBark(targetIndex)
        end
    end
end

AIBarkCtrl.OnShow = HL.Override() << function(self)

    if DeviceInfo.usingTouch then
        local pos = self.view.mask.transform.anchoredPosition
        self.view.mask.transform.anchoredPosition = Vector2(self.view.config.MOBILE_MASK_POS_X, pos.y)
    end

    if AIBarkCtrl.s_showByExecute then
        GameWorld.aiBarkManager:UpdateFullScreenUICD()
    end
end

AIBarkCtrl.OnHide = HL.Override() << function(self)
    self:_Reset()
end



AIBarkCtrl._ClearStopTimer = HL.Method(HL.Number) << function(self, index)
    self:_ClearTimer(index)
end

AIBarkCtrl._StartStopTimer = HL.Method(HL.Number, HL.Number).Return(HL.Number) << function(self, duration, uniqueId)
    return self:_StartTimer(duration, function()
        self:_StopLastAIBark(uniqueId)
    end)
end

AIBarkCtrl._StartTimeoutTimer = HL.Method() << function(self)
    self:_ClearTimeoutTimer()
    self.m_timeoutTimer = self:_StartTimer(TIMEOUT_TIME, function()
        logger.info("AIBark timeout StopAllAIBark")
        self:StopAllAIBark()
    end)
end

AIBarkCtrl._ClearTimeoutTimer = HL.Method() << function(self)
    if self.m_timeoutTimer > 0 then
        self:_ClearTimer(self.m_timeoutTimer)
    end

    self.m_timeoutTimer = -1
end

AIBarkCtrl.StopAIBarkUI = HL.Method(HL.Table) << function(self, arg)
    local barkTextId, audioStop = unpack(arg)
    self:StopSingleAIBark(barkTextId, audioStop)
end

AIBarkCtrl.AudioStopSingleAIBark = HL.Method(HL.Table) << function(self, arg)
    local barkTextId, audioStop, audioInterrupt = unpack(arg)
    self:StopSingleAIBark(barkTextId, audioInterrupt)
end

AIBarkCtrl.StopSingleAIBark = HL.Method(HL.Any, HL.Opt(HL.Boolean)) << function(self, voId, audioInterrupt)
    local removeIndex = 0
    local barkTextId
    if type(voId) == 'table' then
        barkTextId = unpack(voId)
    else
        barkTextId = voId
    end
    for index, bark in pairs(self.m_curPlayingBarks) do
        if bark.barkTextId == barkTextId then
            removeIndex = index

            self.m_curPlayingBarks[index].needStopVoice = false
            if not bark.isLastSentence then
                self.m_curPlayingBarks[index].isLastSentence = true
                local existTime = Time.time - bark.startTime - bark.duration
                if existTime > 0 then
                    self.m_curPlayingBarks[index].timerId = self:_StartStopTimer(existTime, bark.uniqueId)
                else
                    self:_StopLastAIBark(bark.uniqueId, audioInterrupt)
                end

            else
                self:_StopLastAIBark(bark.uniqueId, audioInterrupt)
            end
            break
        end
    end
end

AIBarkCtrl.OnBarkStopped = HL.StaticMethod(HL.String) << function(barkId)
    GameWorld.aiBarkManager:UpdateBarkEndTime(barkId)
end

AIBarkCtrl._CheckHeight = HL.Method().Return(HL.Boolean) << function(self)
    return self.view.content.transform.rect.height <= self.view.mask.rect.height
end

AIBarkCtrl._TryShowSingleBark = HL.Method(CS.Beyond.Gameplay.AIBarkManager.AIBarkRuntimeData) << function(self, aiBarkData)
    local barkId = aiBarkData.barkId
    local barkTextId = aiBarkData.barkTextId
    local charTemplateId = aiBarkData.charTemplateId
    local tDuration = aiBarkData.duration
    local voiceHandleId = aiBarkData.voiceHandleId
    local isLastSentence = aiBarkData.isLastSentence
    local res, data = Tables.aIBarkText:TryGetValue(barkTextId)
    if res then
        self:Show()
        local cell = self:_GenCell()
        local barkText = UIUtils.resolveTextCinematic(data.barkText)
        local duration = tDuration
        if duration <= 0 then
            local originText = UIUtils.resolveOriginalText(barkText)
            duration = UIUtils.getTextShowDuration(originText, 1)
        end

        AIBarkCtrl.s_uniqueId = AIBarkCtrl.s_uniqueId + 1

        local bark = {
            barkId = barkId,
            cell = cell,
            voiceHandleId = voiceHandleId,
            barkTextId = barkTextId,
            barkText = barkText,
            isLastSentence = isLastSentence,
            needStopVoice = true,
            timerId = -1,
            uniqueId = AIBarkCtrl.s_uniqueId,
            startTime = Time.time,
            duration = duration,
        }

        cell.text:SetAndResolveTextStyle(barkText)
        local sizeDelta = cell.text.rectTransform.sizeDelta
        if cell.text.preferredHeight > SINGLE_CELL_TEXT_HEIGHT_LIMIT then
            sizeDelta.y = SINGLE_CELL_TEXT_HEIGHT_LIMIT
        else
            sizeDelta.y = cell.text.preferredHeight
        end
        cell.text.rectTransform.sizeDelta = sizeDelta

        local spriteName = UIConst.UI_AI_BARK_CHAR_HEAD_PREFIX .. "_" .. charTemplateId
        cell.chrIcon:LoadSprite(UIConst.UI_SPRITE_AI_BARK_CHAR_HEAD, spriteName)

        LayoutRebuilder.ForceRebuildLayoutImmediate(self.view.content.transform)


        for i = 1, #self.m_curPlayingBarks do
            if self:_CheckHeight() then
                break
            end
            self:_DoStopSingleAIBarkByUniqueId(self.m_curPlayingBarks[1].uniqueId, true)
            LayoutRebuilder.ForceRebuildLayoutImmediate(self.view.content.transform)
        end
        if isLastSentence then
            bark.timerId = self:_StartStopTimer(duration, bark.uniqueId)
            logger.info("PlaySingleAIBark: ", barkText, ", duration: ", duration, "uniqueId: ", bark.uniqueId)
            self:_ClearTimeoutTimer()
        else
            logger.info("PlaySingleAIBark: ", barkText, ", start timeout timer, uniqueId: ", bark.uniqueId)
            self:_StartTimeoutTimer()
        end

        table.insert(self.m_curPlayingBarks, bark)
    end
end

AIBarkCtrl._StopLastAIBark = HL.Method(HL.Number, HL.Opt(HL.Boolean)) << function(self, uniqueId, audioInterrupt)
    local index = self:_GetIndexByUniqueId(uniqueId)
    logger.info("_StopLastAIBark: ", uniqueId, ", index: ", index, ", totalCount: ", #self.m_curPlayingBarks)
    if index > 0 and #self.m_curPlayingBarks >= index then
        for i = index, 1, -1 do
            local count = #self.m_curPlayingBarks
            if i <= count and count > 0 then
                self:_DoStopSingleAIBark(i, false, audioInterrupt)
            end
        end
    end
end

AIBarkCtrl._GetIndexByUniqueId = HL.Method(HL.Number).Return(HL.Number) << function(self, uniqueId)
    local index = -1
    for i, cell in pairs(self.m_curPlayingBarks) do
        if cell.uniqueId == uniqueId then
            index = i
            break
        end
    end
    return index
end

AIBarkCtrl._DoStopSingleAIBarkByUniqueId = HL.Method(HL.Number, HL.Opt(HL.Boolean)) << function(self, uniqueId, heightRemove)
    local index = self:_GetIndexByUniqueId(uniqueId)
    if index > 0 then
        self:_DoStopSingleAIBark(index, heightRemove)
    end
end

AIBarkCtrl._DoStopSingleAIBark = HL.Method(HL.Number, HL.Opt(HL.Boolean, HL.Boolean)) << function(self, index, heightRemove, audioInterrupt)
    logger.info("_DoStopSingleAIBark start: index: ", index, ", totalCount: ", #self.m_curPlayingBarks)
    local bark = self.m_curPlayingBarks[index]
    if not bark then
        return
    end

    local voiceHandleId = bark.voiceHandleId
    local needStopVoice = bark.needStopVoice
    local timerId = bark.timerId
    local cell = bark.cell

    if needStopVoice and voiceHandleId >= 0 then
        VoiceManager:StopAIBarkNoNotify(voiceHandleId)
    end

    if timerId and timerId > 0 then
        self:_ClearStopTimer(timerId)
    end

    local lastCell = not heightRemove and index == #self.m_curPlayingBarks

    table.remove(self.m_curPlayingBarks, index)

    self:_CacheCell(cell, lastCell)

    if not audioInterrupt then
        local barkId = bark.barkId
        AIBarkCtrl.OnBarkStopped(barkId)
    end
    logger.info("_DoStopSingleAIBark finish: index: ", index, ", totalCount: ", #self.m_curPlayingBarks)
end

AIBarkCtrl._CacheCell = HL.Method(HL.Table, HL.Boolean) << function(self, cell, lastCell)
    if not self.m_waitingCacheCells[cell] then
        self.m_waitingCacheCells[cell] = true
        table.insert(self.m_waitingTable, cell)
    end

    self:_StartTween(lastCell)
end

AIBarkCtrl._CacheAllWaiting = HL.Method() << function(self)
    if #self.m_waitingTable > 0 then
        for _, cell in pairs(self.m_waitingTable) do
            cell.transform:SetParent(self.view.cacheRoot)
            cell.gameObject:SetActive(false)
            table.insert(self.m_cellCache, cell)
        end
    end
    self.m_waitingCacheCells = {}
    self.m_waitingTable = {}
end

AIBarkCtrl._ClearTween = HL.Method() << function(self)
    if self.m_tween then
        self.m_tween:Kill()
    end
    self.m_tween = nil
end

AIBarkCtrl._StartTween = HL.Method(HL.Boolean) << function(self, lastCell)
    self:_ClearTween()
    if lastCell then
        for index, cell in pairs(self.m_waitingTable) do
            cell.animationWrapper:ClearTween(false)
            cell.animationWrapper:SampleToInAnimationEnd()
            cell.animationWrapper:PlayOutAnimation(function()
                if index == #self.m_waitingTable then
                    self:_CacheAllWaiting()
                end
            end)
        end
    else
        local totalHeight = 0
        for _, cell in pairs(self.m_waitingTable) do
            cell.animationWrapper:ClearTween(false)
            cell.animationWrapper:SampleToInAnimationEnd()
            totalHeight = totalHeight + cell.transform.rect.height
        end

        self.m_tween = DOTween.To(function()
            return self.view.content.transform.anchoredPosition.y
        end, function(y)
            self.view.content.transform.anchoredPosition = Vector2(0, y)
        end, totalHeight, 0.2)

        self.m_tween:SetEase(CS.DG.Tweening.Ease.OutSine)
        self.m_tween:OnComplete(function()
            self:_Reset()
        end)
    end
end

AIBarkCtrl._Reset = HL.Method() << function(self)
    self:_ClearTween()
    self:_CacheAllWaiting()
    self.view.content.transform.anchoredPosition = Vector2.zero
end

AIBarkCtrl._GenCell = HL.Method().Return(HL.Table) << function(self)
    local cell
    local cacheCount = #self.m_cellCache
    local parent = self.view.content
    if cacheCount > 0 then
        cell = self.m_cellCache[cacheCount]
        table.remove(self.m_cellCache, cacheCount)
        cell.gameObject:SetActive(true)
        cell.transform:SetParent(parent)
    else
        local go = UIUtils.addChild(parent, self.view.cell.gameObject)
        cell = Utils.wrapLuaNode(go)
        cell.gameObject:SetActive(true)
    end
    return cell
end

AIBarkCtrl.DoShowAIBark = HL.Method(CS.Beyond.Gameplay.AIBarkManager.AIBarkRuntimeData) << function(self, aiBarkData)
    if not self:_CheckCanPlay() then
        self:Hide()
        return
    end

    self:_TryShowSingleBark(aiBarkData)
end

AIBarkCtrl._CheckCanPlay = HL.Method().Return(HL.Boolean) << function(self)
    if Utils.isInNarrative() then
        return false
    end

    if Utils.isRadioPlaying() then
        return false
    end

    return AIBarkCtrl.s_inMainHud
end

HL.Commit(AIBarkCtrl)
