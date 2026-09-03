
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.MapMarkDetailAvailableMission

MapMarkDetailAvailableMissionCtrl = HL.Class('MapMarkDetailAvailableMissionCtrl', uiCtrl.UICtrl)





MapMarkDetailAvailableMissionCtrl.s_messages = HL.StaticField(HL.Table) << {

}


MapMarkDetailAvailableMissionCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    local markInstId = args.markInstId
    local getRuntimeDataSuccess, markRuntimeData = GameInstance.player.mapManager:GetMarkInstRuntimeData(markInstId)
    if getRuntimeDataSuccess == false then
        logger.error("地图详情页获取实例数据失败" .. markInstId)
        return
    end

    local missionId = markRuntimeData.missionInfo.missionId
    local missionRuntimeAsset = GameInstance.player.mission:GetMissionInfo(missionId)
    local commonArgs = {
        titleText = missionRuntimeAsset.missionName:GetText(),
        descText = missionRuntimeAsset.missionDescription:GetText(),
        markInstId = markInstId,
        bigBtnActive = true,
    }
    self.view.detailCommon:InitMapMarkDetailCommon(commonArgs)
end

HL.Commit(MapMarkDetailAvailableMissionCtrl)
