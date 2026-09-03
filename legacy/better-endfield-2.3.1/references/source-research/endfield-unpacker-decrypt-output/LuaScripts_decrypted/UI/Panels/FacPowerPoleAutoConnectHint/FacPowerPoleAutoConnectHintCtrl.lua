
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FacPowerPoleAutoConnectHint
FacPowerPoleAutoConnectHintCtrl = HL.Class('FacPowerPoleAutoConnectHintCtrl', uiCtrl.UICtrl)

FacPowerPoleAutoConnectHintCtrl.m_trackerPool = HL.Field(HL.Table)
FacPowerPoleAutoConnectHintCtrl.m_inBuildingMode = HL.Field(HL.Boolean) << false

FacPowerPoleAutoConnectHintCtrl.m_linkWireModeTrackerTickKey = HL.Field(HL.Number) << -1
FacPowerPoleAutoConnectHintCtrl.m_linkWireModeTrackerPool = HL.Field(HL.Table)
FacPowerPoleAutoConnectHintCtrl.m_inLinkWireMode = HL.Field(HL.Boolean) << false

FacPowerPoleAutoConnectHintCtrl.m_buildingModeTrackerTickKey = HL.Field(HL.Number) << -1





FacPowerPoleAutoConnectHintCtrl.s_messages = HL.StaticField(HL.Table) << {

}


FacPowerPoleAutoConnectHintCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_trackerPool = {}
    self.m_linkWireModeTrackerPool = {}
    self.view.tracker.gameObject:SetActive(false)
end

FacPowerPoleAutoConnectHintCtrl.OnEnterBuildingMode = HL.StaticMethod(HL.Opt(HL.Any)) << function()
    local ctrl = FacPowerPoleAutoConnectHintCtrl.AutoOpen(PANEL_ID, nil, false)
    ctrl.m_inBuildingMode = true
    ctrl:_StartBuildingModeTrackerTick()
    ctrl:_UpdateTrackers()
end

FacPowerPoleAutoConnectHintCtrl.OnExitBuildingMode = HL.StaticMethod() << function()
    local ctrl = FacPowerPoleAutoConnectHintCtrl.AutoOpen(PANEL_ID, nil, false)
    ctrl.m_inBuildingMode = false
    ctrl:_StopBuildingModeTrackerTick()
    ctrl:ClearAllHints()
end

FacPowerPoleAutoConnectHintCtrl.OnEnterBlueprintMode = HL.StaticMethod(HL.Opt(HL.Any)) << function()
    local ctrl = FacPowerPoleAutoConnectHintCtrl.AutoOpen(PANEL_ID, nil, false)
    ctrl.m_inBuildingMode = true
    ctrl:_StartBuildingModeTrackerTick()
    ctrl:_UpdateTrackers()
end

FacPowerPoleAutoConnectHintCtrl.OnExitBlueprintMode = HL.StaticMethod() << function()
    local ctrl = FacPowerPoleAutoConnectHintCtrl.AutoOpen(PANEL_ID, nil, false)
    ctrl.m_inBuildingMode = false
    ctrl:_StopBuildingModeTrackerTick()
    ctrl:ClearAllHints()
end

FacPowerPoleAutoConnectHintCtrl.OnBuild = HL.StaticMethod(HL.Table) << function(args)
    local ctrl = FacPowerPoleAutoConnectHintCtrl.AutoOpen(PANEL_ID, nil, false)
    ctrl:OnBuildModeUpdated(args.buildingTypeId, args.position)
end

FacPowerPoleAutoConnectHintCtrl.OnMove = HL.StaticMethod(HL.Table) << function(args)
    local ctrl = FacPowerPoleAutoConnectHintCtrl.AutoOpen(PANEL_ID, nil, false)
    ctrl:OnMoveModeUpdated(args.buildingTypeId, args.position, args.nodeId)
end

FacPowerPoleAutoConnectHintCtrl.OnBlueprint = HL.StaticMethod() << function()
    local ctrl = FacPowerPoleAutoConnectHintCtrl.AutoOpen(PANEL_ID, nil, false)
    ctrl:OnBlueprintModeUpdated()
end

FacPowerPoleAutoConnectHintCtrl._UpdateTrackers = HL.Method() << function(self)
    local buildingMode = GameInstance.remoteFactoryManager.interact.currentBuildingMode
    if buildingMode then
        local infos = buildingMode.autoConnectCandidateList
        local panelRect = self.view.rectTransform.rect
        local xRadius = panelRect.width * 0.5 - self.view.config.ELLIPSE_X_MARGIN
        local yRadius = panelRect.height * 0.5 - self.view.config.ELLIPSE_Y_MARGIN
        if infos then
            for i = 0, infos.Count - 1 do
                local info = infos[i]
                local screenPos, isInside = UIUtils.objectPosToUI(info.to, self.uiCamera)
                local uiPos, uiAngle, isOutBound = UIUtils.mapScreenPosToEllipseEdge(screenPos, xRadius, yRadius)
                local trackKey = i + 1
                if not self.m_trackerPool[trackKey] then
                    self.m_trackerPool[trackKey] = self:_CreateNewTracker()
                end
                local item = self.m_trackerPool[trackKey]
                item.obj:SetActive(true)
                item.tracker:UpdatePosition(uiPos, uiAngle, isOutBound)
                item.tracker:UpdateDistance(info.dist)
                item.tracker:UpdateNodeType(info.targetNode.nodeType)
                item.tracker:UpdateStatus(info.status)
            end
        end
        local infoCount = infos and infos.Count or 0
        local udpipeEntries = {}
        if buildingMode then
            local udpipeEntry = buildingMode.udpipeConnectEntry
            if udpipeEntry and udpipeEntry.showIcon then
                table.insert(udpipeEntries, udpipeEntry)
            end
        end
        local yRadiusUdpipe = panelRect.height * 0.5 - (self.view.config.ELLIPSE_Y_MARGIN + self.view.config.ELLIPSE_BOTTOM_MARGIN_UDPIPE) * 0.5
        local yOffsetUdpipe = (self.view.config.ELLIPSE_BOTTOM_MARGIN_UDPIPE - self.view.config.ELLIPSE_Y_MARGIN) * 0.5
        for i, udpipeEntry in ipairs(udpipeEntries) do
            local trackKey = i + infoCount
            if not self.m_trackerPool[trackKey] then
                self.m_trackerPool[trackKey] = self:_CreateNewTracker()
            end
            local screenPos, isInside = UIUtils.objectPosToUI(udpipeEntry.to, self.uiCamera)
            local uiPos, uiAngle, isOutBound = UIUtils.mapScreenPosToEllipseEdge(screenPos, xRadius, yRadiusUdpipe, yOffsetUdpipe)
            local item = self.m_trackerPool[trackKey]
            item.obj:SetActive(true)
            item.tracker:UpdatePosition(uiPos, uiAngle, isOutBound)
            item.tracker:UpdateNodeType(udpipeEntry.node.nodeType)
            item.tracker:UpdateDistance(udpipeEntry.dist)
            item.tracker:UpdateUdpipeError(udpipeEntry.outOfRange)
        end
        for i = infoCount + #udpipeEntries + 1, #self.m_trackerPool do
            self.m_trackerPool[i].obj:SetActive(false)
        end
    end
end

FacPowerPoleAutoConnectHintCtrl.OnBuildModeUpdated = HL.Method(HL.String, Vector3) << function(self, buildingTypeId, position)
    if not self.m_inBuildingMode then
        return
    end
    self:_UpdateTrackers()
end

FacPowerPoleAutoConnectHintCtrl.OnBlueprintModeUpdated = HL.Method() << function(self)
    if not self.m_inBuildingMode then
        return
    end
    self:_UpdateTrackers()
end

FacPowerPoleAutoConnectHintCtrl.OnMoveModeUpdated = HL.Method(HL.String, Vector3, HL.Any) << function(self, buildingTypeId, position, nodeId)
    if not self.m_inBuildingMode then
        return
    end
    self:_UpdateTrackers()
end

FacPowerPoleAutoConnectHintCtrl.ClearAllHints = HL.Method() << function(self)
    for i, v in ipairs(self.m_trackerPool) do
        v.obj:SetActive(false)
    end
end

FacPowerPoleAutoConnectHintCtrl._StartBuildingModeTrackerTick = HL.Method() << function(self)
    if self.m_buildingModeTrackerTickKey ~= -1 then
        return
    end
    self.m_buildingModeTrackerTickKey = LuaUpdate:Add("LateTick", function(deltaTime)
        if self.m_inBuildingMode then
            self:_UpdateTrackers()
        end
    end)
end

FacPowerPoleAutoConnectHintCtrl._StopBuildingModeTrackerTick = HL.Method() << function(self)
    if self.m_buildingModeTrackerTickKey == -1 then
        return
    end
    LuaUpdate:Remove(self.m_buildingModeTrackerTickKey)
    self.m_buildingModeTrackerTickKey = -1
end

FacPowerPoleAutoConnectHintCtrl._StartLinkWireModeTrackerTick = HL.Method() << function(self)
    if self.m_linkWireModeTrackerTickKey ~= -1 then
        return
    end
    self.m_linkWireModeTrackerTickKey = LuaUpdate:Add("LateTick", function(deltaTime)
        if self.m_inLinkWireMode then
            self:LinkWireModeUpdateTrackers()
        end
    end)
end

FacPowerPoleAutoConnectHintCtrl._StopLinkWireModeTrackerTick = HL.Method() << function(self)
    if self.m_linkWireModeTrackerTickKey == -1 then
        return
    end
    LuaUpdate:Remove(self.m_linkWireModeTrackerTickKey)
    self.m_linkWireModeTrackerTickKey = -1
end

FacPowerPoleAutoConnectHintCtrl._CreateNewTracker = HL.Method().Return(HL.Table) << function(self)
    local obj = CSUtils.CreateObject(self.view.tracker.gameObject, self.view.main.transform)
    obj:SetActive(true)
    local item = {}
    item.obj = obj
    item.tracker = obj:GetComponent(typeof(CS.Beyond.UI.UIPowerPoleAutoConnectTracker))
    return item
end


FacPowerPoleAutoConnectHintCtrl.LinkWireModeUdpipeTrackerShow = HL.StaticMethod() << function()
    local ctrl = FacPowerPoleAutoConnectHintCtrl.AutoOpen(PANEL_ID, nil, false)
    ctrl.m_inLinkWireMode = true
    ctrl:_StartLinkWireModeTrackerTick()
    ctrl:LinkWireModeUpdateTrackers()
end

FacPowerPoleAutoConnectHintCtrl.LinkWireModeUdpipeTrackerHide = HL.StaticMethod() << function()
    local ctrl = FacPowerPoleAutoConnectHintCtrl.AutoOpen(PANEL_ID, nil, false)
    ctrl.m_inLinkWireMode = false
    ctrl:_StopLinkWireModeTrackerTick()
    ctrl:LinkWireModeClearAllHints()
end

FacPowerPoleAutoConnectHintCtrl.LinkWireModeClearAllHints = HL.Method() << function(self)
    for i, v in ipairs(self.m_linkWireModeTrackerPool) do
        v.obj:SetActive(false)
    end
end

FacPowerPoleAutoConnectHintCtrl.LinkWireModeUpdateTrackers = HL.Method() << function(self)
    local linkWireBrain = GameWorld.gameMechManager.linkWireBrain
    local node = linkWireBrain:GetLinkSourceNode()
    if node == nil then
        return
    end

    local linkStartPos = linkWireBrain.linkStartPos
    local closeEnoughToConnect = not linkWireBrain.closeEnoughToConnect

    local panelRect = self.view.rectTransform.rect
    local xRadius = panelRect.width * 0.5 - self.view.config.ELLIPSE_X_MARGIN
    local yRadius = panelRect.height * 0.5 - (self.view.config.ELLIPSE_Y_MARGIN + self.view.config.ELLIPSE_BOTTOM_MARGIN_UDPIPE) * 0.5
    local yOffset = (self.view.config.ELLIPSE_BOTTOM_MARGIN_UDPIPE - self.view.config.ELLIPSE_Y_MARGIN) * 0.5

    local commonTrackingSystem = GameInstance.player.commonTrackingSystem
    local uiPos, uiAngle, isOutBound = commonTrackingSystem:WorldPosToTrackerUIData(linkStartPos)
    local trackingRoot = commonTrackingSystem.rootTransform
    if trackingRoot ~= nil then
        local trackingRect = trackingRoot.rect
        local csXRadius = commonTrackingSystem.xRadiusRate * trackingRect.width
        local csYRadius = commonTrackingSystem.yRadiusRate * trackingRect.height
        if csXRadius ~= 0 and csYRadius ~= 0 then
            uiPos = Vector2(uiPos.x / csXRadius * xRadius, uiPos.y / csYRadius * yRadius + yOffset)
        end
    end

    if not self.m_linkWireModeTrackerPool[1] then
        self.m_linkWireModeTrackerPool[1] = self:_CreateNewTracker()
    end
    local item = self.m_linkWireModeTrackerPool[1]
    item.obj:SetActiveIfNecessary(true)
    item.tracker:UpdatePosition(uiPos, uiAngle, isOutBound)
    item.tracker:SetDistanceTextVisible(false)
    item.tracker:UpdateNodeType(node.nodeType)
    if closeEnoughToConnect then
        item.tracker:UpdateStatusTooFar()
    else
        item.tracker:UpdateStatusNormal()
    end
end

FacPowerPoleAutoConnectHintCtrl.OnShow = HL.Override() << function(self)
    if self.m_inLinkWireMode then
        self:_StartLinkWireModeTrackerTick()
        self:LinkWireModeUpdateTrackers()
    end
    if self.m_inBuildingMode then
        self:_StartBuildingModeTrackerTick()
        self:_UpdateTrackers()
    end
end

FacPowerPoleAutoConnectHintCtrl.OnClose = HL.Override() << function(self)
    self:_StopLinkWireModeTrackerTick()
    self:_StopBuildingModeTrackerTick()
    self.m_linkWireModeTrackerPool = {}
    self.m_trackerPool = {}
    self.m_inLinkWireMode = false
    self.m_inBuildingMode = false
end

HL.Commit(FacPowerPoleAutoConnectHintCtrl)