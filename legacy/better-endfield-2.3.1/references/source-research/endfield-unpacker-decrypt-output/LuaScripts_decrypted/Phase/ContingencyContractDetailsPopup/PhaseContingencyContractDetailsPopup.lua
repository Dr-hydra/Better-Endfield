
local phaseBase = require_ex('Phase/Core/PhaseBase')
local PHASE_ID = PhaseId.ContingencyContractDetailsPopup
PhaseContingencyContractDetailsPopup = HL.Class('PhaseContingencyContractDetailsPopup', phaseBase.PhaseBase)





PhaseContingencyContractDetailsPopup.s_messages = HL.StaticField(HL.Table) << {

}


PhaseContingencyContractDetailsPopup._OnInit = HL.Override() << function(self)
    PhaseContingencyContractDetailsPopup.Super._OnInit(self)
end




PhaseContingencyContractDetailsPopup.PrepareTransition = HL.Override(HL.Number, HL.Boolean, HL.Opt(HL.Number)) << function(self, transitionType, fastMode, anotherPhaseId)

end

PhaseContingencyContractDetailsPopup._DoPhaseTransitionIn = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end

PhaseContingencyContractDetailsPopup._DoPhaseTransitionOut = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end

PhaseContingencyContractDetailsPopup._DoPhaseTransitionBehind = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end

PhaseContingencyContractDetailsPopup._DoPhaseTransitionBackToTop = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end

PhaseContingencyContractDetailsPopup.GetCurStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local arg = self.arg and lume.deepCopy(self.arg) or {}
    local panelItem = self.m_panel2Item and self.m_panel2Item[PanelId.ContingencyContractDetailsPopup]
    local uiCtrl = panelItem and panelItem.uiCtrl
    if uiCtrl and HL.TryGet(uiCtrl, "GetRecoverStateArg") and PhaseManager:GetTopPhaseId() == PHASE_ID then

        arg.recoverState = uiCtrl:GetRecoverStateArg()
    else
        arg.recoverState = nil
    end
    return arg
end






PhaseContingencyContractDetailsPopup._OnActivated = HL.Override() << function(self)
end

PhaseContingencyContractDetailsPopup._OnDeActivated = HL.Override() << function(self)
end

PhaseContingencyContractDetailsPopup._OnDestroy = HL.Override() << function(self)
    PhaseContingencyContractDetailsPopup.Super._OnDestroy(self)
end




HL.Commit(PhaseContingencyContractDetailsPopup)

