local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.MapMarkDetailWeekRaid

MapMarkDetailWeekRaidCtrl = HL.Class('MapMarkDetailWeekRaidCtrl', uiCtrl.UICtrl)






MapMarkDetailWeekRaidCtrl.s_messages = HL.StaticField(HL.Table) << {

}


MapMarkDetailWeekRaidCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    local markInstId = arg.markInstId
    local commonArgs = {
        markInstId = markInstId,
        leftBtnActive = true,
        leftBtnText = Language["ui_mapmarkdetail_button_info"],
        leftBtnIconName = UIConst.MAP_DETAIL_BTN_ICON_NAME.DETAIL,
        leftBtnCallback = function()
            PhaseManager:OpenPhase(PhaseId.DungeonWeeklyRaid)
        end,
        rightBtnActive = true,
    }
    self.view.mapMarkDetailCommon:InitMapMarkDetailCommon(commonArgs)
end

HL.Commit(MapMarkDetailWeekRaidCtrl)
