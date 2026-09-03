local phaseBase = require_ex('Phase/Core/PhaseBase')
local PHASE_ID = PhaseId.PowerPoleFastTravel
PhasePowerPoleFastTravel = HL.Class('PhasePowerPoleFastTravel', phaseBase.PhaseBase)

local ReservePanelIds = {
    PanelId.Joystick,
    PanelId.LevelCamera,
    PanelId.GeneralTracker,
    PanelId.WeeklyRaidTaskTrackHud,
    PanelId.MiniMap,
}

PhasePowerPoleFastTravel.m_fastTravelPanelItem = HL.Field(HL.Forward("PhasePanelItem"))

PhasePowerPoleFastTravel.m_currentLogicId = HL.Field(HL.Any) << 0





PhasePowerPoleFastTravel.s_messages = HL.StaticField(HL.Table) << {

    [MessageConst.TRAVEL_POLE_ENTER_TRAVEL_MODE] = { '_OnEnterPowerPoleFastTravelMode', false },
    [MessageConst.TRAVEL_POLE_TRIGGER_FORCE_CLOSE_PANEL] = { 'TriggerForceClosePanel', true },
}


PhasePowerPoleFastTravel._OnInit = HL.Override() << function(self)
    PhasePowerPoleFastTravel.Super._OnInit(self)
end

PhasePowerPoleFastTravel.TriggerForceClosePanel = HL.Method(HL.Opt(HL.Any)) << function(self)
    if PhaseManager:IsOpen(PhaseId.PowerPoleFastTravel) then
        GameWorld.gameMechManager.travelPoleBrain:OnClosePanelFast()
        PhaseManager:ExitPhaseFast(PhaseId.PowerPoleFastTravel)
    end
end



PhasePowerPoleFastTravel.PrepareTransition = HL.Override(HL.Number, HL.Boolean, HL.Opt(HL.Number)) << function(self, transitionType, fastMode, anotherPhaseId)
    if transitionType == PhaseConst.EPhaseState.TransitionIn then
        if anotherPhaseId == PhaseId.Level then
            Notify(MessageConst.SET_PHASE_LEVEL_TRANSITION_RESERVE_PANELS, self:_GetReservePanelIds())
        end
    end
end

PhasePowerPoleFastTravel._DoPhaseTransitionIn = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)

end

PhasePowerPoleFastTravel._DoPhaseTransitionOut = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end

PhasePowerPoleFastTravel._DoPhaseTransitionBehind = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end

PhasePowerPoleFastTravel._DoPhaseTransitionBackToTop = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args) end





PhasePowerPoleFastTravel._OnActivated = HL.Override() << function(self)
    if self.m_fastTravelPanelItem == nil then
        self.m_fastTravelPanelItem = self:CreatePhasePanelItem(PanelId.PowerPoleFastTravel, self.arg)
        GameWorld.gameMechManager.travelPoleBrain:OnPanelOpened(self.arg[1])
    elseif self.m_fastTravelPanelItem ~= nil and self.m_fastTravelPanelItem.uiCtrl ~= nil then
        self.m_fastTravelPanelItem.uiCtrl:Show()
    end
end

PhasePowerPoleFastTravel._OnDeActivated = HL.Override() << function(self)
    if self.m_fastTravelPanelItem and self.m_fastTravelPanelItem.uiCtrl then
        self.m_fastTravelPanelItem.uiCtrl:Hide()
    end
end

PhasePowerPoleFastTravel._OnDestroy = HL.Override() << function(self)
    PhasePowerPoleFastTravel.Super._OnDestroy(self)
    self:RemovePhasePanelItem(self.m_fastTravelPanelItem)
    self.m_fastTravelPanelItem = nil
end



PhasePowerPoleFastTravel._GetReservePanelIds = HL.Method().Return(HL.Table) << function(self)
    local ids = {}
    for _, panelId in ipairs(ReservePanelIds) do
        table.insert(ids, panelId)
    end
    if self:_ShouldReserveCommonTaskCountdown() then
        table.insert(ids, PanelId.CommonTaskTrackCountdown)
    end
    return ids
end

PhasePowerPoleFastTravel._ShouldReserveCommonTaskCountdown = HL.Method().Return(HL.Boolean) << function(self)
    local dungeonId = GameInstance.dungeonManager.curDungeonId
    return DungeonUtils.isDungeonRacingDungeon(dungeonId)
end

PhasePowerPoleFastTravel._OnEnterPowerPoleFastTravelMode = HL.StaticMethod(HL.Table) << function(args)
    local logicId, openFailedCallback = unpack(args)
    if PhaseManager:GetTopPhaseId() ~= PhaseId.Level then
        openFailedCallback()
        return
    end

    local openSuccess = PhaseManager:OpenPhase(PhaseId.PowerPoleFastTravel, args)
    if not openSuccess then
        openFailedCallback()
    end
end

PhasePowerPoleFastTravel.SaveCurrentLogicId = HL.Method() << function(self)
    if self.m_fastTravelPanelItem and self.m_fastTravelPanelItem.uiCtrl then
        self.m_currentLogicId = self.m_fastTravelPanelItem.uiCtrl.m_currentLogicId
    else
        self.m_currentLogicId = nil
    end
end

HL.Commit(PhasePowerPoleFastTravel)

