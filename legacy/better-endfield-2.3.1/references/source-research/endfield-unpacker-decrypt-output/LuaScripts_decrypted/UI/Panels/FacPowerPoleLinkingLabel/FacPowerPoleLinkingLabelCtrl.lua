
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FacPowerPoleLinkingLabel
FacPowerPoleLinkingLabelCtrl = HL.Class('FacPowerPoleLinkingLabelCtrl', uiCtrl.UICtrl)







FacPowerPoleLinkingLabelCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.UPDATE_POWER_POLE_LINKING_LABEL] = '_UpdateWireLengthLabel',
    [MessageConst.ON_MOVE_POWER_POLE_TRAVEL_HINT] = '_OnMove',
    [MessageConst.SHOW_POWER_POLE_TOAST] = '_ShowLinkToast',
    [MessageConst.ON_CONTROLLER_INDICATOR_CHANGE] = 'OnToggleControllerSkillIndicator',


}

FacPowerPoleLinkingLabelCtrl.m_nodeId = HL.Field(HL.Any)

FacPowerPoleLinkingLabelCtrl.m_linkType = HL.Field(HL.Number) << 0

FacPowerPoleLinkingLabelCtrl.m_toastType = HL.Field(HL.Number) << 0

FacPowerPoleLinkingLabelCtrl.m_timerId = HL.Field(HL.Number) << -1

FacPowerPoleLinkingLabelCtrl.m_showToast = HL.Field(HL.Boolean) << false


FacPowerPoleLinkingLabelCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.main.gameObject:SetActive(false)
    self.view.buttonCancel.onClick:AddListener(function()
        GameWorld.gameMechManager.linkWireBrain:EndLinkWithCancel(true)
    end)
end

FacPowerPoleLinkingLabelCtrl.ShowLabel = HL.StaticMethod(HL.Table) << function(args)
    local ctrl = FacPowerPoleLinkingLabelCtrl.AutoOpen(PANEL_ID, nil, false)
    ctrl:_ShowLabel(args)
    LuaSystemManager.factory:AddFactoryModeRequest({ true, "FacPowerPole" })
end

FacPowerPoleLinkingLabelCtrl._ShowLabel = HL.Method(HL.Table) << function(self, args)
    self.m_showToast = true
    if self.m_timerId >= 0 then
        self:_ClearTimer(self.m_timerId)
        self.m_timerId = -1
    end
    local nodeId, linkType = unpack(args)
    self.m_nodeId = nodeId
    self.m_linkType = linkType

    if linkType == 1 then
        self.view.textPrefixPowerPole.gameObject:SetActiveIfNecessary(true)
        self.view.textPrefixUdpipe.gameObject:SetActiveIfNecessary(false)
    elseif linkType == 2 then
        self.view.textPrefixPowerPole.gameObject:SetActiveIfNecessary(false)
        self.view.textPrefixUdpipe.gameObject:SetActiveIfNecessary(true)
        Notify(MessageConst.LINK_WIRE_MODE_UDPIPE_TRACKER_SHOW)
    end

    self.view.main.gameObject:SetActiveIfNecessary(true)
    self.view.tipsTxtNode.gameObject:SetActiveIfNecessary(true)
    self.view.wireLengthNode.gameObject:SetActiveIfNecessary(false)

    if linkType == 1 then
        self:_ShowToast(FacConst.FAC_LINK_WIRE_TOAST_TYPE.Start)
    elseif linkType == 2 then
        self:_ShowToast(FacConst.FAC_LINK_WIRE_TOAST_TYPE.UdpipeStart)
    end

    self.view.animationWrapper:PlayWithTween("facpowerpolelink_in", function()
        self:_PlayToastAnimation(function()
            self:_HideToast()
        end)
    end)
end

FacPowerPoleLinkingLabelCtrl.HideLabel = HL.StaticMethod(HL.Table) << function(args)
    local ctrl = FacPowerPoleLinkingLabelCtrl.AutoOpen(PANEL_ID, nil, false)
    ctrl:_HideLabel(args)
    LuaSystemManager.factory:RemoveFactoryModeRequest("FacPowerPole")
    if ctrl.m_linkType == 2 then
        Notify(MessageConst.LINK_WIRE_MODE_UDPIPE_TRACKER_HIDE)
    end
end

FacPowerPoleLinkingLabelCtrl._HideLabel = HL.Method(HL.Table) << function(self, args)
    self.m_showToast = false
    if self.m_timerId >= 0 then
        self:_ClearTimer(self.m_timerId)
        self.m_timerId = -1
    end
    local type = unpack(args)
    self:_ShowToast(type)
    self:_PlayAudio(type)
    self:_PlayToastAnimation(function()
        self.view.animationWrapper:PlayWithTween("facpowerpolelink_out", function()
            self.view.main.gameObject:SetActiveIfNecessary(false)
        end)
    end)
end

FacPowerPoleLinkingLabelCtrl._UpdateWireLengthLabel = HL.Method(HL.Table) << function(self, args)
    local curLength, maxLength, textColor = unpack(args)
    self.view.textMeter.text = UIUtils.ceilToTenthStr(curLength)
    self.view.textMeter.color = textColor
    self.view.textMeterTotal.text = UIUtils.floorToTenthStr(maxLength)
end

FacPowerPoleLinkingLabelCtrl._OnMove = HL.Method(HL.Table) << function(self, args)
    self:OnMoveModeUpdated(args.buildingTypeId, args.position, args.nodeId)
end

FacPowerPoleLinkingLabelCtrl.OnMoveModeUpdated = HL.Method(HL.String, Vector3, HL.Any) << function(self, buildingTypeId, position, nodeId)
    if self.m_nodeId == nodeId then
        GameWorld.gameMechManager.linkWireBrain:EndLinkWithCancel(true)
    end
end

FacPowerPoleLinkingLabelCtrl._ShowToast = HL.Method(HL.Number) << function(self, type)
    self.view.animationWrapper:ClearTween(false)
    self.view.animationWrapper:SampleClipAtPercent("facpowerpolelink_text",0)
    self.view.tipsTxtNode.gameObject:SetActiveIfNecessary(true)
    self.view.tipsTxtNode.beginText.gameObject:SetActiveIfNecessary(type == FacConst.FAC_LINK_WIRE_TOAST_TYPE.Start)
    self.view.tipsTxtNode.beginUdpipeText.gameObject:SetActiveIfNecessary(type == FacConst.FAC_LINK_WIRE_TOAST_TYPE.UdpipeStart)
    self.view.tipsTxtNode.breakText.gameObject:SetActiveIfNecessary(type == FacConst.FAC_LINK_WIRE_TOAST_TYPE.Failed)
    self.view.tipsTxtNode.disconnectText.gameObject:SetActiveIfNecessary(type == FacConst.FAC_LINK_WIRE_TOAST_TYPE.Cancel)
    self.view.tipsTxtNode.failedSourceNoPowerDiffuserText.gameObject:SetActiveIfNecessary(type == FacConst.FAC_LINK_WIRE_TOAST_TYPE.FailedSourceNoPowerDiffuser)
    self.view.tipsTxtNode.failedSourceNoPowerPoleText.gameObject:SetActiveIfNecessary(type == FacConst.FAC_LINK_WIRE_TOAST_TYPE.FailedSourceNoPowerPole)
    self.view.tipsTxtNode.linkAlreadyText.gameObject:SetActiveIfNecessary(type == FacConst.FAC_LINK_WIRE_TOAST_TYPE.LinkAlready)
    self.view.tipsTxtNode.linkText.gameObject:SetActiveIfNecessary(type == FacConst.FAC_LINK_WIRE_TOAST_TYPE.Success)
    self.view.tipsTxtNode.powerNotEnoughText.gameObject:SetActiveIfNecessary(type == FacConst.FAC_LINK_WIRE_TOAST_TYPE.PowerNotEnough)
    self.view.tipsTxtNode.tooFarText.gameObject:SetActiveIfNecessary(type == FacConst.FAC_LINK_WIRE_TOAST_TYPE.TooFar)
    self.view.tipsTxtNode.udpipeLoader2LoaderText.gameObject:SetActiveIfNecessary(type == FacConst.FAC_LINK_WIRE_TOAST_TYPE.UdpipeLoader2Loader)
    self.view.tipsTxtNode.udpipeUnLoader2UnLoaderText.gameObject:SetActiveIfNecessary(type == FacConst.FAC_LINK_WIRE_TOAST_TYPE.UdpipeUnloader2Unloader)

    self.view.wireLengthNode.gameObject:SetActiveIfNecessary(false)

    if type == FacConst.FAC_LINK_WIRE_TOAST_TYPE.TooFar then
        GameInstance.mobileMotionManager:PostEventCommonOperateFailure()
    end
end

FacPowerPoleLinkingLabelCtrl._HideToast = HL.Method() << function(self)
    self.view.wireLengthNode.gameObject:SetActiveIfNecessary(true)
    self.view.animationWrapper:PlayWithTween("facpowerpolelink_text",function()
        self.view.tipsTxtNode.beginText.gameObject:SetActiveIfNecessary(false)
        self.view.tipsTxtNode.beginUdpipeText.gameObject:SetActiveIfNecessary(false)
        self.view.tipsTxtNode.breakText.gameObject:SetActiveIfNecessary(false)
        self.view.tipsTxtNode.disconnectText.gameObject:SetActiveIfNecessary(false)
        self.view.tipsTxtNode.failedSourceNoPowerDiffuserText.gameObject:SetActiveIfNecessary(false)
        self.view.tipsTxtNode.failedSourceNoPowerPoleText.gameObject:SetActiveIfNecessary(false)
        self.view.tipsTxtNode.linkAlreadyText.gameObject:SetActiveIfNecessary(false)
        self.view.tipsTxtNode.linkText.gameObject:SetActiveIfNecessary(false)
        self.view.tipsTxtNode.powerNotEnoughText.gameObject:SetActiveIfNecessary(false)
        self.view.tipsTxtNode.tooFarText.gameObject:SetActiveIfNecessary(false)
        self.view.tipsTxtNode.udpipeLoader2LoaderText.gameObject:SetActiveIfNecessary(false)
        self.view.tipsTxtNode.udpipeUnLoader2UnLoaderText.gameObject:SetActiveIfNecessary(false)
    end)
end

FacPowerPoleLinkingLabelCtrl._ShowLinkToast = HL.Method(HL.Table) << function(self, args)
    if self.m_showToast == false then
        return
    end
    if self.m_timerId >= 0 then
        self:_ClearTimer(self.m_timerId)
        self.m_timerId = -1
    end
    local type = unpack(args)
    self:_ShowToast(type)
    self:_PlayToastAnimation(function()
        self:_HideToast()
    end)
end

FacPowerPoleLinkingLabelCtrl._PlayAudio = HL.Method(HL.Number) << function(self, type)
    if type == FacConst.FAC_LINK_WIRE_TOAST_TYPE.Success then
        AudioManager.PostEvent("Au_UI_HUD_PowerTower_Connect")
    elseif type == FacConst.FAC_LINK_WIRE_TOAST_TYPE.Failed then
        AudioManager.PostEvent("Au_UI_HUD_PowerTower_Disconnect")
    end
end

FacPowerPoleLinkingLabelCtrl._PlayToastAnimation = HL.Method(HL.Function) << function(self, onAnimationOut)
    self.m_timerId = self:_StartTimer(self.view.config.TOAST_TIME, function()
        if onAnimationOut then
            onAnimationOut()
        end
    end)
end

FacPowerPoleLinkingLabelCtrl._IsWireLengthLabelShow = HL.Method().Return(HL.Boolean) << function(self)
    return self.view.wireLengthNode.gameObject.activeSelf
end

FacPowerPoleLinkingLabelCtrl.OnHide = HL.Override() << function(self)
    if self.m_timerId >= 0 then
        self:_ClearTimer(self.m_timerId)
        self.m_timerId = -1
    end
    self.view.main.gameObject:SetActiveIfNecessary(false)
end

FacPowerPoleLinkingLabelCtrl.OnShow = HL.Override() << function(self)
    if self.m_timerId >= 0 then
        self:_ClearTimer(self.m_timerId)
        self.m_timerId = -1
    end
    if self.m_showToast then
        self.view.main.gameObject:SetActiveIfNecessary(true)
        self.view.wireLengthNode.gameObject:SetActiveIfNecessary(true)
        self.view.tipsTxtNode.gameObject:SetActiveIfNecessary(false)
        self.view.animationWrapper:PlayWithTween("facpowerpolelink_in", function()
            self.view.animationWrapper:SampleClipAtPercent("facpowerpolelink_text", 1)
            self.view.tipsTxtNode.gameObject:SetActiveIfNecessary(true)
        end)
    end
end

FacPowerPoleLinkingLabelCtrl.OnToggleControllerSkillIndicator = HL.Method(HL.Boolean) << function(self, active)
    if active then
        self.view.buttonCancel.onClick:ChangeBindingPlayerAction("")
    else
        self.view.buttonCancel.onClick:ChangeBindingPlayerAction("fac_cancel_power_link")
    end
end

HL.Commit(FacPowerPoleLinkingLabelCtrl)