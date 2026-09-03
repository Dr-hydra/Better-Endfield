



local phaseSubItem = require_ex('Phase/Core/PhaseSubItem')
PhasePanelItem = HL.Class("PhasePanelItem", phaseSubItem.PhaseSubItem)




PhasePanelItem.uiCtrl = HL.Field(HL.Forward("UICtrl"))

PhasePanelItem.hideOnDestroy = HL.Field(HL.Boolean) << false





PhasePanelItem._OnInit = HL.Override() << function(self)
    self.uiCtrl = nil
end

PhasePanelItem.OnPhaseRefresh = HL.Override(HL.Opt(HL.Any)) << function(self, arg)
    self.uiCtrl:OnPhaseRefresh(arg)
end

PhasePanelItem.BindUICtrl = HL.Method(HL.Any) << function(self, uiCtrl)
    self.uiCtrl = uiCtrl
    self.uiCtrl:SetPhaseItem(self)
end





PhasePanelItem._DoTransitionInCoroutine = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)

end

PhasePanelItem._DoTransitionOutCoroutine = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    if not self.uiCtrl or fastMode or UIUtils.usingBlockTransition() then
        return
    end
    if UIManager:IsHide(self.uiCtrl.panelId) then
        return
    end
    local t = self.hideOnDestroy and UIConst.PANEL_PLAY_ANIMATION_OUT_COMPLETE_ACTION_TYPE.Hide or UIConst.PANEL_PLAY_ANIMATION_OUT_COMPLETE_ACTION_TYPE.Close
    self.uiCtrl:PlayAnimationOut(t)
end

PhasePanelItem._CheckAllTransitionDone = HL.Override().Return(HL.Boolean) << function(self)
    if self.uiCtrl then

        if IsNull(self.uiCtrl.view.gameObject) then
            return true
        end
        if UIManager:IsHide(self.uiCtrl.panelId) then
            return true
        end
        local wrapper = self.uiCtrl.animationWrapper
        if wrapper then
            return (wrapper.curState ~= CS.Beyond.UI.UIConst.AnimationState.Out and wrapper.curState ~= CS.Beyond.UI.UIConst.AnimationState.In)
        else
            return true
        end
    else
        return false
    end
end





PhasePanelItem.SetNaviTarget = HL.Method(HL.Userdata) << function(self, selectable)
    if self.uiCtrl then
        self.uiCtrl:SetNaviTarget(selectable)
    end
end

PhasePanelItem.ClearNaviTarget = HL.Method() << function(self)
    if self.uiCtrl then
        self.uiCtrl:ClearNaviTarget()
    end
end



PhasePanelItem._OnDestroy = HL.Override() << function(self)
    if self.uiCtrl then
        if self.hideOnDestroy then
            self.uiCtrl:Hide()
            self.uiCtrl:SetPhaseItem(nil)
        else
            self.uiCtrl:Close()
        end
        self.uiCtrl = nil
    end
end

HL.Commit(PhasePanelItem)
