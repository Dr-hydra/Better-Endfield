


local stateBehaviour = require_ex('Phase/Core/StateBehaviour')
PhaseSubItem = HL.Class("PhaseSubItem", stateBehaviour.StateBehaviour)




PhaseSubItem.phase = HL.Field(HL.Forward("PhaseBase"))

PhaseSubItem.phaseId = HL.Field(HL.Number) << -1






PhaseSubItem._OnInit = HL.Override() << function(self)
end

PhaseSubItem.OnPhaseRefresh = HL.Virtual(HL.Opt(HL.Any)) << function(self, arg)
end

PhaseSubItem.BindBasicInfos = HL.Method(HL.Forward('PhaseBase'), HL.Number) << function(self, phase, phaseId)
    self.phaseId = phaseId
    self.phase = phase
end




PhaseSubItem._DoTransitionInCoroutine = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end


PhaseSubItem._OnActivated = HL.Override() << function(self)
end


PhaseSubItem._OnDeActivated = HL.Override() << function(self)
end


PhaseSubItem._DoTransitionBehindCoroutine = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end

PhaseSubItem._DoTransitionOutCoroutine = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end

PhaseSubItem._CheckAllTransitionDone = HL.Override().Return(HL.Boolean) << function(self)
    return true
end


PhaseSubItem._OnDestroy = HL.Override() << function(self)
end


PhaseSubItem.Notify = HL.Method(HL.Number, HL.Opt(HL.Any)) << function(self, msg, arg)
    Notify(msg, arg)
end

HL.Commit(PhaseSubItem)
