
local phaseBase = require_ex('Phase/Core/PhaseBase')
local PHASE_ID = PhaseId.ActivityImportantPopup
PhaseActivityImportantPopup = HL.Class('PhaseActivityImportantPopup', phaseBase.PhaseBase)

PhaseActivityImportantPopup.s_messages = HL.StaticField(HL.Table) << {
}

PhaseActivityImportantPopup._OnInit = HL.Override() << function(self)
    PhaseActivityImportantPopup.Super._OnInit(self)
    UIManager:ToggleBlockObtainWaysJump("PhaseActivityImportantPopup", true, {})
end

PhaseActivityImportantPopup._OnDestroy = HL.Override() << function(self)
    PhaseActivityImportantPopup.Super._OnDestroy(self)
    UIManager:ToggleBlockObtainWaysJump("PhaseActivityImportantPopup", false)
    LuaSystemManager.inputDeviceChangeSystem:SetForbidInputDeviceChange("ActivityImportantPopup", false)
end

HL.Commit(PhaseActivityImportantPopup)
