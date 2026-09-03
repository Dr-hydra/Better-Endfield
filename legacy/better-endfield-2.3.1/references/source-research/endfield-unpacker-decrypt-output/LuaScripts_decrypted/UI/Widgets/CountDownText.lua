local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

CountDownText = HL.Class('CountDownText', UIWidgetBase)


CountDownText._OnFirstTimeInit = HL.Override() << function(self)

end


CountDownText.m_cor = HL.Field(HL.Thread)

CountDownText.m_onComplete = HL.Field(HL.Function)

CountDownText.m_timeFormatFunc = HL.Field(HL.Function)

CountDownText.m_targetTime = HL.Field(HL.Number) << -1


CountDownText.InitCountDownText = HL.Method(HL.Number, HL.Opt(HL.Function, HL.Function)) << function(self, targetTime, onComplete, timeFormatFunc)
    self:_FirstTimeInit()

    self.m_targetTime = targetTime
    self.m_onComplete = onComplete
    self.m_timeFormatFunc = timeFormatFunc or UIUtils.getLeftTime
    self:_Update()

    self.m_cor = self:_ClearCoroutine(self.m_cor)
    self.m_cor = self:_StartCoroutine(function()
        while true do
            coroutine.wait(1)
            self:_Update()
        end
    end)
end

CountDownText.StopCountDown = HL.Method() << function(self)
    if self.m_cor then
        self.m_cor = self:_ClearCoroutine(self.m_cor)
    end
end

CountDownText._Update = HL.Method() << function(self)
    local curTime = DateTimeUtils.GetCurrentTimestampBySeconds()
    local leftTime = self.m_targetTime - curTime
    self.view.text:SetAndResolveTextStyle(self.m_timeFormatFunc(leftTime))
    if leftTime <= 0 then
        self.m_cor = self:_ClearCoroutine(self.m_cor)
        if self.m_onComplete then
            self.m_onComplete()
        end
    end
end

HL.Commit(CountDownText)
return CountDownText
