
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.MapMarkDetailUndergroundPipe

MapMarkDetailUndergroundPipeCtrl = HL.Class('MapMarkDetailUndergroundPipeCtrl', uiCtrl.UICtrl)






MapMarkDetailUndergroundPipeCtrl.s_messages = HL.StaticField(HL.Table) << {

}

MapMarkDetailUndergroundPipeCtrl.m_markInstId = HL.Field(HL.String) << ""

MapMarkDetailUndergroundPipeCtrl.m_connectHandler = HL.Field(HL.Any)
MapMarkDetailUndergroundPipeCtrl.m_nodeId = HL.Field(HL.Number) << -1

MapMarkDetailUndergroundPipeCtrl.m_chapterId = HL.Field(HL.Number) << -1


MapMarkDetailUndergroundPipeCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_markInstId = arg.markInstId

    local commonArgs =
    {
        bigBtnActive = false,
        markInstId = self.m_markInstId,
        rightBtnActive = true,
    }
    self.view.detailCommon:InitMapMarkDetailCommon(commonArgs)

    local getRuntimeDataSuccess, markRuntimeData = GameInstance.player.mapManager:GetMarkInstRuntimeData(self.m_markInstId)

    if getRuntimeDataSuccess then
        self.m_nodeId = markRuntimeData.nodeId
        self.m_chapterId = markRuntimeData.chapterId
        local nodeHandler = FactoryUtils.getBuildingNodeHandler(markRuntimeData.nodeId, markRuntimeData.chapterId)
        local cpt = nodeHandler:GetComponentInPosition(GEnums.FCComponentPos.FluidUdPipe:GetHashCode())
        local chapterInfo = GameInstance.remoteFactoryManager.system.core:GetChapterInfoById(markRuntimeData.chapterId)
        local component = chapterInfo:GetComponent(cpt.componentId)
        local isLoader = FacConst.UDPIPE_PORT_LOAD_TYPE_MAP[nodeHandler.templateId]
        self.m_connectHandler = component.udPipe.connectComponent
        if self.m_connectHandler == nil then
            self.view.stateController:SetState(isLoader and "DisconnectOutlet" or "DisconnectEntrance")
        else
            local isDstAdvanced = not FacConst.UDPIPE_PORT_LAYOUT_STATE_MAP[self.m_connectHandler.belongNode.templateId]
            if isDstAdvanced then
                self.view.stateController:SetState(isLoader and "ConnectedAdvancedOutlet" or "ConnectedAdvancedEntrance")
            else
                self.view.stateController:SetState(isLoader and "ConnectedOutlet" or "ConnectedEntrance")
            end
        end

        self.view.navigationBtn.onClick:AddListener(function()
            if self.m_connectHandler == nil then
                return
            end
            local success, mapInstId = GameInstance.player.mapManager:GetFacMarkInstIdByNodeId(self.m_connectHandler.belongNode.belongChapter.chapterId, self.m_connectHandler.belongNode.nodeId)
            if success then
                MapUtils.openMap(mapInstId)
            end
        end)

        self.view.detailCommon.view.leftBtn.gameObject:SetActiveIfNecessary(true)
        self.view.detailCommon.view.leftBtn.onClick:AddListener(function()
            self:_DeleteBuilding()
        end)
    end
end

MapMarkDetailUndergroundPipeCtrl._DeleteBuilding = HL.Method() << function(self)
    if GameWorld.gameMechManager.travelPoleBrain:CanOpenMiniMap() then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_SYSTEM_FORBIDDEN)
        return
    end

    FactoryUtils.delBuilding(self.m_nodeId, function()
        Notify(MessageConst.SHOW_TOAST, Language.LUA_FACTORY_BUILDING_DEL_TOAST)
        if self.m_isClosed then
            return
        end
        self:PlayAnimationOutAndClose()
    end, true, nil, self.m_chapterId, true)
end

HL.Commit(MapMarkDetailUndergroundPipeCtrl)
