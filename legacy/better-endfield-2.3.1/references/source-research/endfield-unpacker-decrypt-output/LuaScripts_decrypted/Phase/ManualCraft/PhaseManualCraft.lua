
local phaseBase = require_ex('Phase/Core/PhaseBase')
local PHASE_ID = PhaseId.ManualCraft
PhaseManualCraft = HL.Class('PhaseManualCraft', phaseBase.PhaseBase)


PhaseManualCraft.m_basicPanelItem = HL.Field(HL.Forward("PhasePanelItem"))

PhaseManualCraft.m_curPanelItem = HL.Field(HL.Forward("PhasePanelItem"))

PhaseManualCraft.m_panelId2Item = HL.Field(HL.Table)





PhaseManualCraft.s_messages = HL.StaticField(HL.Table) << {
}

PhaseManualCraft.s_prePanelId = HL.StaticField(HL.Number) << -1


PhaseManualCraft._OnInit = HL.Override() << function(self)
   PhaseManualCraft.Super._OnInit(self)
end



PhaseManualCraft._DoPhaseTransitionIn = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end

PhaseManualCraft._DoPhaseTransitionOut = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end

PhaseManualCraft._DoPhaseTransitionBehind = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end

PhaseManualCraft._DoPhaseTransitionBackToTop = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end






PhaseManualCraft._OnActivated = HL.Override() << function(self)
end

PhaseManualCraft._OnDeActivated = HL.Override() << function(self)
end

PhaseManualCraft._OnDestroy = HL.Override() << function(self)
   PhaseManualCraft.Super._OnDestroy(self)
end

PhaseManualCraft._OnRefresh = HL.Override() << function(self)
    local item = self.m_panel2Item[PanelId.ManualCraft]

    if item ~= nil and self.arg ~= nil and self.arg.jumpId ~= nil then
        item.uiCtrl:PhaseRefresh(self.arg.jumpId)
    end

end



PhaseManualCraft.GetCurStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local item = self.m_panel2Item[PanelId.ManualCraft]
    if item and item.uiCtrl then
        local recoverState = item.uiCtrl:GetRecoverStateArg()
        if recoverState then
            return { recoverState = recoverState }
        end
    end
    if self.arg ~= nil then
        return lume.deepCopy(self.arg)
    end
end

HL.Commit(PhaseManualCraft)

