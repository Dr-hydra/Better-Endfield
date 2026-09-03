local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

CircularProgressBar = HL.Class('CircularProgressBar', UIWidgetBase)

local TEMP_VALUE_TWEEN_TIME<const> = 0.3

CircularProgressBar.m_totalValue = HL.Field(HL.Number) << 100

CircularProgressBar.m_currentValue = HL.Field(HL.Number) << 0

CircularProgressBar.m_tempValue = HL.Field(HL.Number) << 0

CircularProgressBar.m_tempValueTweener = HL.Field(HL.Userdata) << nil

CircularProgressBar.InitCircularProgressBar = HL.Method(HL.Number) << function(self, totalValue)
    self:_FirstTimeInit()
    if totalValue > 0 then
        self.m_totalValue = totalValue
    else
        logger.error("CircularProgressBar totalValue must be greater than 0!! ")
    end

    self.view.currentValue.fillAmount = 0
    self.view.tempValue.fillAmount = 0
end

CircularProgressBar.SetCurrentValue = HL.Method(HL.Number) << function(self, value)
    if value > 0 then

        self.view.displayNode.gameObject:SetActive(true)
    end

    self.m_currentValue = value
    local progress = lume.clamp(self.m_currentValue / self.m_totalValue, 0, 1)
    self.view.currentValue.fillAmount = progress

    if self.m_currentValue >= self.m_totalValue then

        self.view.increaseImage.gameObject:SetActive(false)
        self.view.increaseBg.gameObject:SetActive(false)
    else
        self.view.increaseImage.gameObject:SetActive(true)
        self.view.increaseBg.gameObject:SetActive(true)
    end
end

CircularProgressBar.SetTempValue = HL.Method(HL.Number, HL.Boolean) << function(self, value, keepActive)
    if self.m_currentValue == self.m_totalValue then
        return
    end

    if value > self.m_tempValue then

        self.view.animationWrapper:PlayInAnimation()
    end

    self.m_tempValue = value
    local displayValue = value + lume.clamp(self.m_currentValue, 0, self.m_totalValue)
    local progress = lume.clamp(displayValue / self.m_totalValue, 0, 1)

    local function onTweenComplete()
        if self.m_tempValue == 0 and not keepActive then

            self.view.displayNode.gameObject:SetActive(false)
        end

        self.m_tempValueTweener = nil
    end

    if progress ~= self.view.tempValue.fillAmount then

        self.view.displayNode.gameObject:SetActive(true)

        if self.m_tempValueTweener then
            self.m_tempValueTweener:Kill(false)
            self.m_tempValueTweener = nil
        end

        self.m_tempValueTweener = self.view.tempValue:DOFillAmount(progress, TEMP_VALUE_TWEEN_TIME);
        self.m_tempValueTweener:SetEase(CS.DG.Tweening.Ease.InOutQuad)
        self.m_tempValueTweener:OnComplete(onTweenComplete)
    end
end


CircularProgressBar.RefreshDisplay = HL.Method() << function(self)


    self:SetCurrentValue(self.m_currentValue)
    self:SetTempValue(self.m_tempValue)
end

HL.Commit(CircularProgressBar)
return CircularProgressBar

