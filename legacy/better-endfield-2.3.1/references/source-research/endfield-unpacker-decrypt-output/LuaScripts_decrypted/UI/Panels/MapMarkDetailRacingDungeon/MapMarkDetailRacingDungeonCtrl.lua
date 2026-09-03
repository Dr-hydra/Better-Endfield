
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.MapMarkDetailRacingDungeon

MapMarkDetailRacingDungeonCtrl = HL.Class('MapMarkDetailRacingDungeonCtrl', uiCtrl.UICtrl)






MapMarkDetailRacingDungeonCtrl.s_messages = HL.StaticField(HL.Table) << {

}


MapMarkDetailRacingDungeonCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    local commonArgs = {}
    commonArgs.markInstId = args.markInstId

    commonArgs.leftBtnActive = true
    commonArgs.leftBtnText = Language["ui_mapmarkdetail_button_info"]
    commonArgs.leftBtnIconName = UIConst.MAP_DETAIL_BTN_ICON_NAME.DETAIL
    commonArgs.leftBtnCallback = function()
        self:_OpenRacingDungeonPanel()
    end
    commonArgs.rightBtnActive = true

    self.view.detailCommon:InitMapMarkDetailCommon(commonArgs)
end

MapMarkDetailRacingDungeonCtrl._OpenRacingDungeonPanel = HL.Method() << function(self)
    PhaseManager:OpenPhase(PhaseId.RacingDungeonEntry)
end

HL.Commit(MapMarkDetailRacingDungeonCtrl)
