
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.MapMarkDetailSSControlCenter

MapMarkDetailSSControlCenterCtrl = HL.Class('MapMarkDetailSSControlCenterCtrl', uiCtrl.UICtrl)






MapMarkDetailSSControlCenterCtrl.s_messages = HL.StaticField(HL.Table) << {

}


MapMarkDetailSSControlCenterCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    local markInstId = args.markInstId
    local commonArgs = {
        markInstId = markInstId,
        bigBtnActive = true,
    }
    self.view.detailCommon:InitMapMarkDetailCommon(commonArgs)
    local getRuntimeDataSuccess, markRuntimeData = GameInstance.player.mapManager:GetMarkInstRuntimeData(markInstId)
    local roomId = Tables.spaceshipConst.controlCenterRoomId
    local succ, roomInfo = GameInstance.player.spaceship:TryGetRoom(roomId)
    self.view.lvDotNode:InitLvDotNode(roomInfo.lv, roomInfo.maxLv)

end


HL.Commit(MapMarkDetailSSControlCenterCtrl)
