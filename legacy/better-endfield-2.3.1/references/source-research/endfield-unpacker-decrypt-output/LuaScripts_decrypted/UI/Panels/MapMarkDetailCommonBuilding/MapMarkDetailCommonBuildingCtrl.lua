local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.MapMarkDetailCommonBuilding

MapMarkDetailCommonBuildingCtrl = HL.Class('MapMarkDetailCommonBuildingCtrl', uiCtrl.UICtrl)

MapMarkDetailCommonBuildingCtrl.m_nodeId = HL.Field(HL.Number) << -1

MapMarkDetailCommonBuildingCtrl.m_chapterId = HL.Field(HL.Number) << -1





MapMarkDetailCommonBuildingCtrl.s_messages = HL.StaticField(HL.Table) << {

}


MapMarkDetailCommonBuildingCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    local markInstId = arg.markInstId
    local _, markRuntimeData = GameInstance.player.mapManager:GetMarkInstRuntimeData(markInstId)
    self.m_nodeId = markRuntimeData.nodeId
    self.m_chapterId = markRuntimeData.chapterId

    local commonArgs =
    {
        bigBtnActive = false,
        markInstId = markInstId,
        rightBtnActive = true,
    }
    self.view.detailCommon:InitMapMarkDetailCommon(commonArgs)
    self.view.detailCommon.view.leftBtn.gameObject:SetActiveIfNecessary(true)
    self.view.detailCommon.view.leftBtn.onClick:AddListener(function()
        self:_DeleteBuilding()
    end)
end

MapMarkDetailCommonBuildingCtrl._DeleteBuilding = HL.Method() << function(self)
    if GameWorld.gameMechManager.travelPoleBrain:CanOpenMiniMap() then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_SYSTEM_FORBIDDEN)
        return
    end
    local nodeHandler = FactoryUtils.getBuildingNodeHandler(self.m_nodeId, self.m_chapterId)
    local isDeco = nodeHandler and FactoryUtils.isDecoBuilding(nodeHandler.templateId)
    FactoryUtils.delBuilding(self.m_nodeId, function()
        if isDeco then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_FACTORY_DECO_BUILDING_DEL_TOAST)
        else
            Notify(MessageConst.SHOW_TOAST, Language.LUA_FACTORY_BUILDING_DEL_TOAST)
        end

        if self.m_isClosed then
            return
        end
        self:PlayAnimationOutAndClose()
    end, true, nil, self.m_chapterId, true)
end

HL.Commit(MapMarkDetailCommonBuildingCtrl)
