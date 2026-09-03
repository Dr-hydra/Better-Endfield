local phaseBase = require_ex('Phase/Core/PhaseBase')
local PHASE_ID = PhaseId.SNS
PhaseSubmitCollection = HL.Class('PhaseSubmitCollection', phaseBase.PhaseBase)

PhaseSubmitCollection.m_basicPanelItem = HL.Field(HL.Forward("PhasePanelItem"))

PhaseSubmitCollection.m_curPanelItem = HL.Field(HL.Forward("PhasePanelItem"))

PhaseSubmitCollection.m_panelId2Item = HL.Field(HL.Table)





PhaseSubmitCollection.s_messages = HL.StaticField(HL.Table) << {
}

PhaseSubmitCollection.s_prePanelId = HL.StaticField(HL.Number) << -1


PhaseSubmitCollection._OnInit = HL.Override() << function(self)
    PhaseSubmitCollection.Super._OnInit(self)
end



PhaseSubmitCollection._DoPhaseTransitionIn = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    if self.arg and self.arg.isCollectionSubmitInstruction then
        UIManager:Open(PanelId.InstructionBook, "collection_submit")
        self.arg.isCollectionSubmitInstruction = nil
    end
end

PhaseSubmitCollection._DoPhaseTransitionOut = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end

PhaseSubmitCollection._DoPhaseTransitionBehind = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end

PhaseSubmitCollection._DoPhaseTransitionBackToTop = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    Notify(MessageConst.HIDE_ITEM_TIPS)
end






PhaseSubmitCollection._OnActivated = HL.Override() << function(self)
end

PhaseSubmitCollection._OnDeActivated = HL.Override() << function(self)
end

PhaseSubmitCollection._OnDestroy = HL.Override() << function(self)
    PhaseSubmitCollection.Super._OnDestroy(self)
end

PhaseSubmitCollection._OnRefresh = HL.Override() << function(self)
    local item = self.m_panel2Item[PanelId.SubmitCollection]
end



PhaseSubmitCollection.GetCurStateArg = HL.Override().Return(HL.Any) << function(self)
    local arg = self.arg and lume.deepCopy(self.arg) or {}
    local isOpen, instructionCtrl = UIManager:IsOpen(PanelId.InstructionBook)
    if isOpen and instructionCtrl:IsShow() and instructionCtrl.id == "collection_submit" then
        arg.isCollectionSubmitInstruction = true
    else
        arg.isCollectionSubmitInstruction = nil
    end
    return arg
end

HL.Commit(PhaseSubmitCollection)

