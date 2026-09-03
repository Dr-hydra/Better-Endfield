local phaseBase = require_ex('Phase/Core/PhaseBase')
local PHASE_ID = PhaseId.KiteStation

PhaseKiteStation = HL.Class('PhaseKiteStation', phaseBase.PhaseBase)

PhaseKiteStation.s_messages = HL.StaticField(HL.Table) << {
}

PhaseKiteStation.m_collectionRewardItem = HL.Field(HL.Forward("PhasePanelItem"))

PhaseKiteStation._OnInit = HL.Override() << function(self)
    PhaseKiteStation.Super._OnInit(self)
end

PhaseKiteStation.OpenCollectionReward = HL.Method(HL.String) << function(self, insId)
    if self.m_collectionRewardItem then
        return
    end
    self.m_collectionRewardItem = self:CreatePhasePanelItem(PanelId.KiteStationCollectionReward, {
        insId = insId,
    })
end

PhaseKiteStation.CloseCollectionReward = HL.Method() << function(self)
    if self.m_collectionRewardItem then
        self:RemovePhasePanelItem(self.m_collectionRewardItem)
        self.m_collectionRewardItem = nil
    end
end




PhaseKiteStation.PrepareTransition = HL.Override(HL.Number, HL.Boolean, HL.Opt(HL.Number)) << function(self, transitionType, fastMode, anotherPhaseId)
end

PhaseKiteStation._DoPhaseTransitionIn = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    if self.arg and self.arg.collectionRewardArg then
        self:OpenCollectionReward(self.arg.collectionRewardArg.insId)
    end
end

PhaseKiteStation._DoPhaseTransitionOut = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end

PhaseKiteStation._DoPhaseTransitionBehind = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end

PhaseKiteStation._DoPhaseTransitionBackToTop = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end






PhaseKiteStation._OnActivated = HL.Override() << function(self)
end

PhaseKiteStation._OnDeActivated = HL.Override() << function(self)
end

PhaseKiteStation._OnDestroy = HL.Override() << function(self)
    PhaseKiteStation.Super._OnDestroy(self)
end




PhaseKiteStation.GetCurStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local kiteStationItem = self:_GetPanelPhaseItem(PanelId.KiteStation)
    local arg
    if kiteStationItem and kiteStationItem.uiCtrl then
        arg = kiteStationItem.uiCtrl:GetCurPhaseStateArg()
    end
    arg = arg or (self.arg and lume.deepCopy(self.arg) or {})

    if self.m_collectionRewardItem and self.m_collectionRewardItem.uiCtrl then
        arg.collectionRewardArg = {
            insId = arg.kiteStationId or "",
        }
    else
        arg.collectionRewardArg = nil
    end

    return arg
end


HL.Commit(PhaseKiteStation)
