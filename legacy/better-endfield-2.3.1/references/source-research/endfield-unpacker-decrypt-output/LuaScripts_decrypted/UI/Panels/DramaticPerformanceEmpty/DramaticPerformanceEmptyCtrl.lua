
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.DramaticPerformanceEmpty
local PHASE_ID = PhaseId.DramaticPerformanceEmpty
DramaticPerformanceEmptyCtrl = HL.Class('DramaticPerformanceEmptyCtrl', uiCtrl.UICtrl)






DramaticPerformanceEmptyCtrl.s_messages = HL.StaticField(HL.Table) << {

}



DramaticPerformanceEmptyCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
end


DramaticPerformanceEmptyCtrl.ExecuteShow = HL.StaticMethod() << function()
    PhaseManager:OpenPhaseFast(PHASE_ID)
end

DramaticPerformanceEmptyCtrl.ExecuteClose = HL.StaticMethod() << function()
    PhaseManager:ExitPhaseFast(PHASE_ID)
end

HL.Commit(DramaticPerformanceEmptyCtrl)
