
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.MapMarkDetailSettlement

MapMarkDetailSettlementCtrl = HL.Class('MapMarkDetailSettlementCtrl', uiCtrl.UICtrl)






MapMarkDetailSettlementCtrl.s_messages = HL.StaticField(HL.Table) << {

}

MapMarkDetailSettlementCtrl.m_markRuntimeData = HL.Field(HL.Any)


MapMarkDetailSettlementCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    local markInstId = args.markInstId
    local getRuntimeDataSuccess, markRuntimeData = GameInstance.player.mapManager:GetMarkInstRuntimeData(markInstId)

    if getRuntimeDataSuccess == false then
        logger.LogError("地图详情页获取实例数据失败" .. self.m_instId)
        return
    end
    self.m_markRuntimeData = markRuntimeData

    local forbidMapTp = Utils.isForbiddenMapTeleport()
    local commonArgs = {}
    commonArgs.markInstId = markInstId
    self.view.forbidTeleportTips.gameObject:SetActive(forbidMapTp)
    if forbidMapTp then
        commonArgs.bigBtnActive = true
        commonArgs.bigBtnText = Language["ui_mapmarkdetail_button_info"]
        commonArgs.bigBtnIconName = UIConst.MAP_DETAIL_BTN_ICON_NAME.DETAIL
        commonArgs.bigBtnCallback = function()
            self:_OpenDetailPanel()
        end
    else
        commonArgs.leftBtnActive = true
        commonArgs.leftBtnText = Language["ui_mapmarkdetail_button_info"]
        commonArgs.leftBtnIconName = UIConst.MAP_DETAIL_BTN_ICON_NAME.DETAIL
        commonArgs.leftBtnCallback = function()
            self:_OpenDetailPanel()
        end

        commonArgs.rightBtnActive = true
        commonArgs.rightBtnText = Language["ui_mapmarkdetail_button_teleport"]
        commonArgs.rightBtnIconName = UIConst.MAP_DETAIL_BTN_ICON_NAME.TELEPORT
        commonArgs.rightBtnCallback = function()
            GameWorld.dialogManager:SkipCurrentDialog(true)
            self:_Teleport()
        end
    end

    local settlementId = markRuntimeData.settlementId
    local _, settlementData = Tables.settlementBasicDataTable:TryGetValue(settlementId)
    commonArgs.titleText = settlementData.settlementName
    commonArgs.descText = GameInstance.player.settlementSystem:GetSettlementDescription(settlementId)


    self.view.detailCommon:InitMapMarkDetailCommon(commonArgs)
end

MapMarkDetailSettlementCtrl._Teleport = HL.Method() << function(self)
    local markRuntimeData = self.m_markRuntimeData
    MapUtils.teleportToHubByHubMark(markRuntimeData, markRuntimeData.settlementHubNodeId)
end

MapMarkDetailSettlementCtrl._OpenDetailPanel = HL.Method() << function(self)
    PhaseManager:OpenPhase(PhaseId.SettlementMain,self.m_markRuntimeData.levelId)
end

HL.Commit(MapMarkDetailSettlementCtrl)
