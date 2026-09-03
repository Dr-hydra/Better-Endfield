local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.MapMarkDetailInvalidBuilding

MapMarkDetailInvalidBuildingCtrl = HL.Class('MapMarkDetailInvalidBuildingCtrl', uiCtrl.UICtrl)

MapMarkDetailInvalidBuildingCtrl.m_nodeId = HL.Field(HL.Number) << -1

MapMarkDetailInvalidBuildingCtrl.m_chapterId = HL.Field(HL.Number) << -1





MapMarkDetailInvalidBuildingCtrl.s_messages = HL.StaticField(HL.Table) << {

}


MapMarkDetailInvalidBuildingCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    local markInstId = arg.markInstId
    local _, markRuntimeData = GameInstance.player.mapManager:GetMarkInstRuntimeData(markInstId)
    self.m_nodeId = markRuntimeData.nodeId
    self.m_chapterId = markRuntimeData.chapterId

    local commonArgs = {}
    commonArgs.bigBtnActive = true
    commonArgs.markInstId = markInstId
    commonArgs.leftBtnActive = false
    commonArgs.hintInfo = {
        importantHint = true,
        hintText = Language["ui_fac_illegal_building_map_detail_tips"]
    }
    self.view.mapMarkDetailCommon:InitMapMarkDetailCommon(commonArgs)
    self.view.mapMarkDetailCommon.view.leftBtn.gameObject:SetActiveIfNecessary(true)
    self.view.mapMarkDetailCommon.view.rightBtn.gameObject:SetActiveIfNecessary(true)

    self.view.mapMarkDetailCommon.view.leftBtn.onClick:AddListener(function()
        self:_DeleteInvalidBuilding()
    end)
end

MapMarkDetailInvalidBuildingCtrl._DeleteInvalidBuilding = HL.Method() << function(self)
    if GameWorld.gameMechManager.travelPoleBrain:CanOpenMiniMap() then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_SYSTEM_FORBIDDEN)
        return
    end

    FactoryUtils.delBuilding(self.m_nodeId, function()
        if self.m_isClosed then
            return
        end
        self:PlayAnimationOutAndClose()
    end, true, nil, self.m_chapterId, true)
end

HL.Commit(MapMarkDetailInvalidBuildingCtrl)
