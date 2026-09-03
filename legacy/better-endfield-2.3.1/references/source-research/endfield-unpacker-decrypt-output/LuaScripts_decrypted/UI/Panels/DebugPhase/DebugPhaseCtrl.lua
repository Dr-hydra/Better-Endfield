
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.DebugPhase
DebugPhaseCtrl = HL.Class('DebugPhaseCtrl', uiCtrl.UICtrl)







DebugPhaseCtrl.s_messages = HL.StaticField(HL.Table) << {

    [MessageConst.HIDE_DEBUG_PHASE] = 'HideDebugPhase',
    [MessageConst.UPDATE_DEBUG_PHASE_TEXT] = 'UpdateDebugText',
}


DebugPhaseCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
end

DebugPhaseCtrl.ShowDebugPhase = HL.StaticMethod() << function()
    if not BEYOND_DEBUG_COMMAND then
        return
    end
    local ctrl = DebugPhaseCtrl.AutoOpen(PANEL_ID, nil, false)
    ctrl:UpdateDebugText()
end

DebugPhaseCtrl.HideDebugPhase = HL.Method() << function()
    UIManager:Close(PANEL_ID)
end

DebugPhaseCtrl.UpdateDebugText = HL.Method() << function(self)
    self.view.text.text = self:_GetPhaseStackInfo()
end

DebugPhaseCtrl._GetPhaseStackInfo = HL.Method().Return(HL.String) << function(self)
    local phaseStack = PhaseManager:GetPhaseStack()
    if phaseStack:Empty() then
        return "Empty PhaseStack"
    end

    local phaseCfgs = PhaseManager:GetPhaseCfgs()
    local str = "PhaseStack如下:\n"

    for i = phaseStack:TopIndex(), phaseStack:BottomIndex(), -1 do
        local phase = phaseStack:Get(i)
        local cfg = phaseCfgs[phase.phaseId]
        if i == phaseStack:TopIndex() then
            str = str .. "[栈顶] "
        end
        str = str..cfg.name.."    "..phase.phaseId.."\n"
    end

    return str
end











HL.Commit(DebugPhaseCtrl)
