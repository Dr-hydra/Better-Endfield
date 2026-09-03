local phaseBase = require_ex('Phase/Core/PhaseBase')
local PHASE_ID = PhaseId.GameSetting
PhaseGameSetting = HL.Class('PhaseGameSetting', phaseBase.PhaseBase)





PhaseGameSetting.s_messages = HL.StaticField(HL.Table) << {

    [MessageConst.ON_LOADING_PANEL_OPENED] = { "ExitSelfFast", true },
    [MessageConst.ON_TELEPORT_LOADING_PANEL_OPENED] = { "ExitSelfFast", true },
}



PhaseGameSetting._OnInit = HL.Override() << function(self)
    PhaseGameSetting.Super._OnInit(self)
end




PhaseGameSetting.PrepareTransition = HL.Override(HL.Number, HL.Boolean, HL.Opt(HL.Number)) << function(self, transitionType, fastMode, anotherPhaseId)
end

PhaseGameSetting._DoPhaseTransitionIn = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end

PhaseGameSetting._DoPhaseTransitionOut = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end

PhaseGameSetting._DoPhaseTransitionBehind = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end

PhaseGameSetting._DoPhaseTransitionBackToTop = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end






PhaseGameSetting._OnActivated = HL.Override() << function(self)

    local isLoading = UIManager:IsOpen(PanelId.Loading) or UIManager:IsOpen(PanelId.TeleportLoading)
    if isLoading then
        self:_StartCoroutine(function()
            if PhaseManager:IsOpen(PHASE_ID) then
                self:ExitSelfFast()
            end
        end)
    end
end

PhaseGameSetting._OnDeActivated = HL.Override() << function(self)
end

PhaseGameSetting._OnDestroy = HL.Override() << function(self)
    PhaseGameSetting.Super._OnDestroy(self)
end




HL.Commit(PhaseGameSetting)
