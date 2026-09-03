
local phaseBase = require_ex('Phase/Core/PhaseBase')
local PHASE_ID = PhaseId.GachaWeaponResult
PhaseGachaWeaponResult = HL.Class('PhaseGachaWeaponResult', phaseBase.PhaseBase)





PhaseGachaWeaponResult.s_messages = HL.StaticField(HL.Table) << {

}



PhaseGachaWeaponResult._OnInit = HL.Override() << function(self)
    PhaseGachaWeaponResult.Super._OnInit(self)
end




PhaseGachaWeaponResult.PrepareTransition = HL.Override(HL.Number, HL.Boolean, HL.Opt(HL.Number)) << function(self, transitionType, fastMode, anotherPhaseId)
end

PhaseGachaWeaponResult._DoPhaseTransitionIn = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end

PhaseGachaWeaponResult._DoPhaseTransitionOut = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end

PhaseGachaWeaponResult._DoPhaseTransitionBehind = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end

PhaseGachaWeaponResult._DoPhaseTransitionBackToTop = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end






PhaseGachaWeaponResult._OnActivated = HL.Override() << function(self)
    LuaSystemManager.gachaSystem:UpdateGachaWeaponSettingState()
end

PhaseGachaWeaponResult._OnDeActivated = HL.Override() << function(self)
    LuaSystemManager.gachaSystem:UpdateGachaWeaponSettingState()
end

PhaseGachaWeaponResult._OnDestroy = HL.Override() << function(self)
    PhaseGachaWeaponResult.Super._OnDestroy(self)
end




HL.Commit(PhaseGachaWeaponResult)

