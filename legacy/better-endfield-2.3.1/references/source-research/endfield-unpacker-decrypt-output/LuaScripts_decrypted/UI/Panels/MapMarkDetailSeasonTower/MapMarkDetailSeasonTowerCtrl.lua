
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.MapMarkDetailSeasonTower

MapMarkDetailSeasonTowerCtrl = HL.Class('MapMarkDetailSeasonTowerCtrl', uiCtrl.UICtrl)






MapMarkDetailSeasonTowerCtrl.s_messages = HL.StaticField(HL.Table) << {

}


MapMarkDetailSeasonTowerCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    local markInstId = arg.markInstId
    local commonArgs = {
        markInstId = markInstId,
        leftBtnActive = true,
        leftBtnText = Language["ui_mapmarkdetail_button_info"],
        leftBtnIconName = UIConst.MAP_DETAIL_BTN_ICON_NAME.DETAIL,
        leftBtnCallback = function()
            PhaseManager:GoToPhase(PhaseId.SeasonTowerMainHud)
        end,
        rightBtnActive = true,
    }

    self.view.mapMarkDetailCommon:InitMapMarkDetailCommon(commonArgs)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
    local system = GameInstance.player.seasonTowerSystem

    self.view.countDownText:InitCountDownText(system.seasonRecord.endTime)
end











HL.Commit(MapMarkDetailSeasonTowerCtrl)
