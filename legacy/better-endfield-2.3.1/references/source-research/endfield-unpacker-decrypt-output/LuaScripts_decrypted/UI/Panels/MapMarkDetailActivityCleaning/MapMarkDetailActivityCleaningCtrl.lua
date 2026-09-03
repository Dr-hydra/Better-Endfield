
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.MapMarkDetailActivityCleaning

MapMarkDetailActivityCleaningCtrl = HL.Class('MapMarkDetailActivityCleaningCtrl', uiCtrl.UICtrl)






MapMarkDetailActivityCleaningCtrl.s_messages = HL.StaticField(HL.Table) << {

}


MapMarkDetailActivityCleaningCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    local markInstId = arg.markInstId
    local _, markRuntimeData = GameInstance.player.mapManager:GetMarkInstRuntimeData(markInstId)
    local success, cfg = Tables.activityTable:TryGetValue(markRuntimeData.detail.activityId)
    if not success then
        logger.error("[MapMarkCleaning] Invalid activityId " .. tostring(markRuntimeData.detail.activityId))
        return
    end

    local commonArgs = {}
    commonArgs.markInstId = markInstId
    commonArgs.bigBtnActive = true
    commonArgs.titleText = cfg.name
    commonArgs.descText = cfg.desc

    self.view.mapMarkDetailCommon:InitMapMarkDetailCommon(commonArgs)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
end











HL.Commit(MapMarkDetailActivityCleaningCtrl)
