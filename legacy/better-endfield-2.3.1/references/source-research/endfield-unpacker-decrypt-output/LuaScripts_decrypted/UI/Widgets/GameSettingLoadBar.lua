local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

GameSettingLoadBar = HL.Class('GameSettingLoadBar', UIWidgetBase)












GameSettingLoadBar.m_args = HL.Field(HL.Table)

GameSettingLoadBar.m_currentProgress = HL.Field(HL.Number) << -1

GameSettingLoadBar.m_progressTween = HL.Field(HL.Userdata)


GameSettingLoadBar._OnFirstTimeInit = HL.Override() << function(self)

end

GameSettingLoadBar._OnDisable = HL.Override() << function(self)
    self:_StopProgressAnimation()
end

GameSettingLoadBar.InitGameSettingLoadBar = HL.Method(HL.Table) << function(self, args)
    self:_FirstTimeInit()
    self.m_args = args
end

GameSettingLoadBar.Refresh = HL.Method(HL.Boolean, HL.Number) << function(self, playAnim, duration)
    local currentProgress = self.m_args.currentProgressGetter()
    if currentProgress < 0 then
        logger.error(ELogChannel.GameSetting, "Invalid current progress: " .. tostring(currentProgress))
        currentProgress = 0
    end

    if currentProgress == self.m_currentProgress then
        return
    end

    local previousProgress = self.m_currentProgress
    self.m_currentProgress = currentProgress
    if self.m_args.onCurrentProgressChanged then
        self.m_args.onCurrentProgressChanged(previousProgress, currentProgress)
    end

    local currentProgressConfig
    local newRatio = math.max(0, currentProgress / self.m_args.maxProgress)
    for i, progressConfig in ipairs(self.m_args.progressConfigs) do
        if newRatio > progressConfig.threshold then
            currentProgressConfig = progressConfig
            break
        end
    end

    if currentProgressConfig then
        self.view.stateCtrl:SetState(currentProgressConfig.stateName)
        local stateText = currentProgressConfig.stateText
        if UNITY_EDITOR then
            stateText = string.format("%s ( %.2f / %.2f )", stateText, currentProgress, self.m_args.maxProgress)
        end
        self.view.stateText.text = stateText
    else
        logger.error(ELogChannel.GameSetting, "Matched progress config not found for current progress: " .. tostring(currentProgress))
    end

    if playAnim then
        self:_PlayProgressAnimation(newRatio, duration)
    else
        self:_StopProgressAnimation()
        self.view.progressImage.fillAmount = lume.clamp(newRatio, 0, 1)
    end
end

GameSettingLoadBar._PlayProgressAnimation = HL.Method(HL.Number, HL.Number) << function(self, endValue, duration)
    self:_StopProgressAnimation()

    self.m_progressTween = DOTween.To(function()
        return self.view.progressImage.fillAmount
    end, function(value)
        self.view.progressImage.fillAmount = value
    end, endValue, duration)
end

GameSettingLoadBar._StopProgressAnimation = HL.Method() << function(self)
    if self.m_progressTween then
        self.m_progressTween:Kill()
        self.m_progressTween = nil
    end
end

HL.Commit(GameSettingLoadBar)
return GameSettingLoadBar
