local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.MapMarkDetailActivitySnapShot

MapMarkDetailActivitySnapShotCtrl = HL.Class('MapMarkDetailActivitySnapShotCtrl', uiCtrl.UICtrl)






MapMarkDetailActivitySnapShotCtrl.s_messages = HL.StaticField(HL.Table) << {

}


MapMarkDetailActivitySnapShotCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    local commonArgs = {
        markInstId = arg.markInstId,
    }
    local _, markRuntimeData = GameInstance.player.mapManager:GetMarkInstRuntimeData(commonArgs.markInstId)

    local success, cfg = Tables.activityConditionalMultiStageTable:TryGetValue(markRuntimeData.detail.activityId)

    if not success then
        logger.error("MapMarkDetailActivitySnapShotCtrl.OnCreate: Invalid activityId " .. tostring(markRuntimeData.detail.activityId))
        self.view.mapMarkDetailCommon:InitMapMarkDetailCommon(commonArgs)
        return
    end

    commonArgs.bigBtnActive = true
    commonArgs.titleText = cfg.stageList[markRuntimeData.detail.activityStageId].name
    commonArgs.descText = cfg.stageList[markRuntimeData.detail.activityStageId].desc

    self.view.mapMarkDetailCommon:InitMapMarkDetailCommon(commonArgs)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
end











HL.Commit(MapMarkDetailActivitySnapShotCtrl)
