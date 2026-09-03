
local phaseBase = require_ex('Phase/Core/PhaseBase')
local PHASE_ID = PhaseId.DomainMain
PhaseDomainMain = HL.Class('PhaseDomainMain', phaseBase.PhaseBase)







PhaseDomainMain.s_messages = HL.StaticField(HL.Table) << {

}

PhaseDomainMain.hasJumpedToOtherPhase = HL.Field(HL.Boolean) << false


PhaseDomainMain._OnInit = HL.Override() << function(self)
    PhaseDomainMain.Super._OnInit(self)
    if self.arg and self.arg.hasJumpedToOtherPhase then
        self.hasJumpedToOtherPhase = self.arg.hasJumpedToOtherPhase
        self.arg.hasJumpedToOtherPhase = nil
    end
end




PhaseDomainMain._DoPhaseTransitionIn = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end

PhaseDomainMain._OnRefresh = HL.Override() << function(self)
end

PhaseDomainMain._DoPhaseTransitionOut = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end

PhaseDomainMain._DoPhaseTransitionBehind = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end

PhaseDomainMain.PrepareTransition = HL.Override(HL.Number, HL.Boolean, HL.Opt(HL.Number)) << function(self, transitionType, fastMode, anotherPhaseId)
    if transitionType == PhaseConst.EPhaseState.TransitionBackToTop and self.hasJumpedToOtherPhase then
        local panelId = self.panels[1]
        if self.m_panel2Item[panelId] ~= nil then
            local uiCtrl = self.m_panel2Item[panelId].uiCtrl
            uiCtrl.animationWrapper:SampleClipAtPercent("domainmain_in_part_0", 1)
        end
    end
end

PhaseDomainMain._DoPhaseTransitionBackToTop = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    if self.hasJumpedToOtherPhase and self.panels[1] ~= nil then
        local panelId = self.panels[1]
        if self.m_panel2Item[panelId] ~= nil then
            local uiCtrl = self.m_panel2Item[panelId].uiCtrl
            if fastMode then

                uiCtrl.animationWrapper:SampleClipAtPercent("domainmain_in_part_1", 1)
                uiCtrl:SetNavi(true)
            else
                uiCtrl:SetNavi(false)
                local wrapper = uiCtrl.animationWrapper
                wrapper:PlayInAnimation(function()
                    uiCtrl:SetNavi(true)
                end)
            end
        end
    end
end

PhaseDomainMain.GetCurStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    if self.panels[1] ~= nil then
        local panelId = self.panels[1]
        if self.m_panel2Item[panelId] ~= nil then

            local uiCtrl = self.m_panel2Item[panelId].uiCtrl
            local arg = uiCtrl:GetCurPhaseStateArg()
            if arg then
                arg.hasJumpedToOtherPhase = self.hasJumpedToOtherPhase
            end
            return arg
        end
    end
    return nil
end






PhaseDomainMain._OnActivated = HL.Override() << function(self)
end

PhaseDomainMain._OnDeActivated = HL.Override() << function(self)
end

PhaseDomainMain._OnDestroy = HL.Override() << function(self)
    PhaseDomainMain.Super._OnDestroy(self)
end




HL.Commit(PhaseDomainMain)
