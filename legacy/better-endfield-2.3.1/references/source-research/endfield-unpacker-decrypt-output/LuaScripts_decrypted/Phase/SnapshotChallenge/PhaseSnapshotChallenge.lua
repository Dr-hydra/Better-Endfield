
local phaseBase = require_ex('Phase/Core/PhaseBase')
local PHASE_ID = PhaseId.GachaPool
PhaseSnapshotChallenge = HL.Class('PhaseSnapshotChallenge', phaseBase.PhaseBase)





PhaseSnapshotChallenge.s_messages = HL.StaticField(HL.Table) << {

}



PhaseSnapshotChallenge._OnInit = HL.Override() << function(self)
    PhaseSnapshotChallenge.Super._OnInit(self)
end




PhaseSnapshotChallenge.PrepareTransition = HL.Override(HL.Number, HL.Boolean, HL.Opt(HL.Number)) << function(self, transitionType, fastMode, anotherPhaseId)

    if transitionType == PhaseConst.EPhaseState.TransitionIn and not fastMode then
        local activityId = ""
        if type(self.arg) == "string" then
            activityId = self.arg
        else
            activityId = self.arg.activityId
        end

        local path = ActivityUtils.GetSnapshotChallengeMainNodePath(activityId)
        self.m_resourceLoader:LoadGameObjectAsync(path, function()
            logger.info(path, "预载完成")
        end)
        self.arg.phase = self
    end
end

PhaseSnapshotChallenge._DoPhaseTransitionIn = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end

PhaseSnapshotChallenge._DoPhaseTransitionOut = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end

PhaseSnapshotChallenge._DoPhaseTransitionBehind = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end

PhaseSnapshotChallenge._DoPhaseTransitionBackToTop = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end






PhaseSnapshotChallenge._OnActivated = HL.Override() << function(self)
end

PhaseSnapshotChallenge._OnDeActivated = HL.Override() << function(self)
end

PhaseSnapshotChallenge._OnDestroy = HL.Override() << function(self)
    PhaseSnapshotChallenge.Super._OnDestroy(self)
end




HL.Commit(PhaseSnapshotChallenge)
