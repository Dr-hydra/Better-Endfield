
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.RaceModuleRank

RaceModuleRankCtrl = HL.Class('RaceModuleRankCtrl', uiCtrl.UICtrl)





RaceModuleRankCtrl.s_messages = HL.StaticField(HL.Table) << {

}


RaceModuleRankCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
end

RaceModuleRankCtrl.ShowRaceModuleRankUI = HL.StaticMethod() << function(self)
    local isOpen, ctrl = UIManager:IsOpen(PANEL_ID)
    if isOpen then
        UIManager:Close(PANEL_ID)
    end
    UIManager:Open(PANEL_ID)
end

RaceModuleRankCtrl.CloseRaceModuleRankUI = HL.StaticMethod() << function(self)
    UIManager:Close(PANEL_ID)
end













HL.Commit(RaceModuleRankCtrl)