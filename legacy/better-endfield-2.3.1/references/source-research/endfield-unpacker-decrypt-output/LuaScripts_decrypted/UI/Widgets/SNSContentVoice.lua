local SNSContentBase = require_ex('UI/Widgets/SNSContentBase')

SNSContentVoice = HL.Class('SNSContentVoice', SNSContentBase)

SNSContentVoice.m_timerId = HL.Field(HL.Number) << -1

SNSContentVoice.m_voiceHandleId = HL.Field(HL.Number) << -1

SNSContentVoice.m_showText = HL.Field(HL.Boolean) << false

SNSContentVoice.s_playingVoice = HL.StaticField(HL.Forward("SNSContentVoice"))


SNSContentVoice._OnDisable = HL.Override() << function(self)
    self:_ClearVoice()
end

SNSContentVoice._OnDestroy = HL.Override() << function(self)
    self:_ClearVoice()
end

SNSContentVoice._OnSNSContentInit = HL.Override() << function(self)
    self.view.voiceTextNode.gameObject:SetActiveIfNecessary(false)

    self.view.timeText.text = self:_GetTimeText()
    self.view.voiceText.text = self.m_contentCfg.content

    self.view.voiceNodeBtn.onClick:RemoveAllListeners()
    self.view.voiceNodeBtn.onClick:AddListener(function()
        self:_PlayVoice()
    end)

    self.view.showTextBtn.onClick:RemoveAllListeners()
    self.view.showTextBtn.onClick:AddListener(function()
        self:_OnClickShowTextBtn()
    end)

    self.view.controllerShowTextBtn.onClick:RemoveAllListeners()
    self.view.controllerShowTextBtn.onClick:AddListener(function()
        self:_OnClickShowTextBtn()
    end)

    self.m_showText = false
    self:_UpdateHintText()

    self.view.voiceNodeBtn.customBindingViewLabelText = Language.LUA_SNS_CONTENT_CELL_VOICE_START_PLAY_DESC
end

SNSContentVoice._PlayVoice = HL.Method() << function(self)

    if SNSContentVoice.s_playingVoice ~= nil then
        SNSContentVoice.s_playingVoice:_ClearVoice()
    end

    local contentParam = self.m_contentCfg.contentParam
    local voiceId = contentParam.Count > 0 and contentParam[0] or ""
    local duration = contentParam.Count > 1 and tonumber(contentParam[1]) or 1
    if not string.isEmpty(voiceId) then
        self.m_timerId = self:_StartTimer(duration, function()
            self:_ClearVoice()
        end)

        self.m_voiceHandleId = AudioAdapter.PostEvent(voiceId)
        self.view.voiceProgress:PlayLoopAnimation()
        SNSContentVoice.s_playingVoice = self
    end
end

SNSContentVoice._ClearVoice = HL.Method() << function(self)
    if self.m_timerId >= 0 then
        self:_ClearTimer(self.m_timerId)
        self.m_timerId = -1
    end

    if self.m_voiceHandleId >= 0 then
        self.view.voiceProgress:SampleClipAtPercent(self.view.voiceProgress.animationLoop.name, 0)
        AudioAdapter.StopByPlayingId(self.m_voiceHandleId)
        self.m_voiceHandleId = -1
        SNSContentVoice.s_playingVoice = nil
    end
end

SNSContentVoice._OnClickShowTextBtn = HL.Method() << function(self)
    local isShow = not self.m_showText
    self.view.voiceTextNode.gameObject:SetActiveIfNecessary(isShow)

    if self.m_notifyCellSizeChange then
        self.m_notifyCellSizeChange()
    end

    self.m_showText = isShow
    self:_UpdateHintText()
end

SNSContentVoice._GetTimeText = HL.Method().Return(HL.String) << function(self)
    local contentParam = self.m_contentCfg.contentParam
    local duration = contentParam.Count > 1 and contentParam[1] or 0
    duration = math.ceil(duration)
    local min = duration // 60
    local sec = duration % 60
    if min > 0 then
        return tostring(min) .. "'" .. tostring(sec) .. "''"
    else
        return tostring(sec) .. "''"
    end
end

SNSContentVoice._UpdateHintText = HL.Method() << function(self)
    local showText = self.m_showText and Language.LUA_SNS_CONTENT_CELL_VOICE_HIDE_TEXT_DESC or
            Language.LUA_SNS_CONTENT_CELL_VOICE_SHOW_TEXT_DESC
    self.view.showTextBtn.text = showText

    self.view.controllerShowTextBtn.customBindingViewLabelText = showText
end



SNSContentVoice.CanSetTarget = HL.Override().Return(HL.Boolean) << function(self)
    return true
end

SNSContentVoice.GetNaviTarget = HL.Override().Return(HL.Any) << function(self)
    return self.view.textVoiceRoot
end



HL.Commit(SNSContentVoice)
return SNSContentVoice