
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.MapMarkDetailHub

MapMarkDetailHubCtrl = HL.Class('MapMarkDetailHubCtrl', uiCtrl.UICtrl)






MapMarkDetailHubCtrl.s_messages = HL.StaticField(HL.Table) << {

}

MapMarkDetailHubCtrl.m_markInstId = HL.Field(HL.String) << ""


MapMarkDetailHubCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    self.m_markInstId = args.markInstId
    local getRuntimeDataSuccess, markRuntimeData = GameInstance.player.mapManager:GetMarkInstRuntimeData(self.m_markInstId)

    if getRuntimeDataSuccess == false then
        logger.LogError("地图详情页获取实例数据失败" .. self.m_instId)
        return
    end

    local forbidMapTp = Utils.isForbiddenMapTeleport()
    local commonArgs = {}
    commonArgs.markInstId = self.m_markInstId
    self.view.forbidTeleportTips.gameObject:SetActive(forbidMapTp)
    if not forbidMapTp then
        commonArgs.bigBtnActive = true
        commonArgs.bigBtnText = Language["ui_mapmarkdetail_button_teleport"]
        commonArgs.bigBtnIconName = UIConst.MAP_DETAIL_BTN_ICON_NAME.TELEPORT
        commonArgs.bigBtnCallback = function()
            GameWorld.dialogManager:SkipCurrentDialog(true)
            MapUtils.teleportToHubByHubMark(markRuntimeData)
        end
    end
    self.view.detailCommon:InitMapMarkDetailCommon(commonArgs)
end

HL.Commit(MapMarkDetailHubCtrl)