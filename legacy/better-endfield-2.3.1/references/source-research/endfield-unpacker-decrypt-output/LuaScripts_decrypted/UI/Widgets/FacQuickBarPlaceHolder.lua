local PlaceholderBaseWidget = require_ex('UI/Widgets/PlaceholderBaseWidget')

FacQuickBarPlaceHolder = HL.Class('FacQuickBarPlaceHolder', PlaceholderBaseWidget)


FacQuickBarPlaceHolder.m_arg = HL.Field(HL.Table)


FacQuickBarPlaceHolder.InitFacQuickBarPlaceHolder = HL.Method(HL.Opt(HL.Table)) << function(self, extraArgs)
    self.m_arg = extraArgs or {}
    self:_InitPlaceholder()
end

FacQuickBarPlaceHolder._InitPlaceholder = HL.Override(HL.Opt(HL.Table)) << function(self, args)
    self.m_playAnimationOutMsg = MessageConst.PLAY_FAC_QUICK_BAR_OUT_ANIM
    self.m_showMsg = MessageConst.SHOW_FAC_QUICK_BAR
    self.m_hideMsg = MessageConst.HIDE_FAC_QUICK_BAR

    FacQuickBarPlaceHolder.Super._InitPlaceholder(self, args)
end

FacQuickBarPlaceHolder.GetArgs = HL.Override().Return(HL.Table) << function(self)
    return {
        panelId = self.m_panelId,
        offset = self.config.PANEL_ORDER_OFFSET,
        showBelt = self.config.SHOW_BELT,
        showPipe = self.config.SHOW_PIPE,
        useActiveAction = self.config.USE_ACTIVE_ACTION,
        controllerSwitchArgs = self.m_arg.controllerSwitchArgs,
        showBuildBtn = self.config.SHOW_BUILD_BUTTON,
    }
end

FacQuickBarPlaceHolder.GetNaviGroup = HL.Method().Return(HL.Opt(CS.Beyond.UI.UISelectableNaviGroup)) << function(self)
    local succ, panelCtrl = UIManager:IsOpen(PanelId.FacQuickBar)
    if succ then
        return panelCtrl.view.main
    end
end

FacQuickBarPlaceHolder.GetInputBindingGroupId = HL.Method().Return(HL.Opt(HL.Number)) << function(self)
    local succ, panelCtrl = UIManager:IsOpen(PanelId.FacQuickBar)
    if succ then
        return panelCtrl.view.inputGroup.groupId
    end
end

HL.Commit(FacQuickBarPlaceHolder)
return FacQuickBarPlaceHolder
