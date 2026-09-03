local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

PinBtn = HL.Class('PinBtn', UIWidgetBase)

PinBtn.m_pinSystem = HL.Field(HL.Any)

PinBtn.m_pinId = HL.Field(HL.String) << ""

PinBtn.m_pinType = HL.Field(HL.Any)

PinBtn.m_pinChangedCallback = HL.Field(HL.Function)

PinBtn.pinIsOn = HL.Field(HL.Boolean) << false


PinBtn._OnFirstTimeInit = HL.Override() << function(self)
    self:RegisterMessage(MessageConst.ON_PIN_STATE_CHANGED, function(arg)
        local pinId, pinType = unpack(arg)
        self:_RefreshPinBtnState(pinId, pinType)
    end)

    self.view.pinToggle.onValueChanged:AddListener(function(isOn)
        self:_OnPinBtnToggleValueChanged(isOn)
    end)
end

PinBtn.InitPinBtn = HL.Method(HL.String, HL.Any, HL.Opt(HL.Function)) << function(self, pinId, pinType, pinChangedCallback)
    self:_FirstTimeInit()

    if string.isEmpty(pinId) then
        self.view.gameObject:SetActive(false)
    end

    self.m_pinSystem = GameInstance.player.pin
    self.m_pinId = pinId
    self.m_pinType = pinType
    self.m_pinChangedCallback = pinChangedCallback

    local chapterInfo = FactoryUtils.getCurChapterInfo()
    if chapterInfo == nil then
        return
    end

    local pinIdFormula = CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryUtil.GetPinBoardStrId(chapterInfo.pinBoard, GEnums.FCPinPosition.Formula:GetHashCode())
    local pinIdBuilding = CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryUtil.GetPinBoardStrId(chapterInfo.pinBoard, GEnums.FCPinPosition.Building:GetHashCode())
    local isPinnedById = self.m_pinId == pinIdFormula or self.m_pinId == pinIdBuilding

    self.view.pinToggle:SetIsOnWithoutNotify(isPinnedById)
    self.pinIsOn = self.view.pinToggle.isOn
end

PinBtn._RefreshPinBtnState = HL.Method(HL.String, HL.Any) << function(self, pinId, pinType)
    if pinId ~= self.m_pinId and self.view.pinToggle.isOn and self.m_pinType == pinType then
        self.view.pinToggle:SetIsOnWithoutNotify(false)
        self.pinIsOn = false
        self:_InvokePinChangedCallback()
    end
    if pinId == self.m_pinId and not self.view.pinToggle.isOn and self.m_pinType == pinType then
        self.view.pinToggle:SetIsOnWithoutNotify(true)
        self.pinIsOn = true
        self:_InvokePinChangedCallback()
    end
end

PinBtn._OnPinBtnToggleValueChanged = HL.Method(HL.Boolean) << function(self, isOn)
    local curScopeIndex = ScopeUtil.GetCurrentScope():GetHashCode()
    if curScopeIndex == 0 then
        return
    end
    if isOn then
        GameInstance.player.remoteFactory.core:Message_PinSet(curScopeIndex, self.m_pinType, self.m_pinId, 0, false)
    else
        GameInstance.player.remoteFactory.core:Message_PinSet(curScopeIndex, self.m_pinType, "", 0, true)
    end


    local pinWay = ''
    if UIManager:IsShow(PanelId.ItemTips) then
        pinWay = 'way_1'
    elseif UIManager:IsOpen(PanelId.Formula) then
        pinWay = 'way_3'
    elseif PhaseManager:GetTopPhaseId() == PhaseId.Wiki then
        pinWay = 'way_2'
    end
    EventLogManagerInst:GameEvent_FactoryPinSetting(isOn, self.m_pinId, pinWay)

    self.pinIsOn = isOn
    self:_InvokePinChangedCallback()
end

PinBtn._InvokePinChangedCallback = HL.Method() << function(self)
    if self.m_pinChangedCallback ~= nil then
        self.m_pinChangedCallback()
    end
end

PinBtn.TogglePinBtn = HL.Method() << function(self)
    self.view.pinToggle.isOn = not self.view.pinToggle.isOn
end

HL.Commit(PinBtn)
return PinBtn

