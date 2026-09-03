
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FacPowerPoleTravelHint
FacPowerPoleTravelHintCtrl = HL.Class('FacPowerPoleTravelHintCtrl', uiCtrl.UICtrl)







FacPowerPoleTravelHintCtrl.s_messages = HL.StaticField(HL.Table) << {

}

FacPowerPoleTravelHintCtrl.m_inBuildingMode = HL.Field(HL.Boolean) << false

FacPowerPoleTravelHintCtrl.m_buildingTypeId = HL.Field(HL.String) << ""

FacPowerPoleTravelHintCtrl.m_buildModePosition = HL.Field(Vector3)

FacPowerPoleTravelHintCtrl.m_moveModeNodeId = HL.Field(HL.Any)

FacPowerPoleTravelHintCtrl.m_moveModePosition = HL.Field(Vector3)

FacPowerPoleTravelHintCtrl.m_isShown = HL.Field(HL.Boolean) << false

FacPowerPoleTravelHintCtrl.m_tempTargetInfoList = HL.Field(HL.Userdata) << nil

FacPowerPoleTravelHintCtrl.m_trackers = HL.Field(HL.Table)

FacPowerPoleTravelHintCtrl.m_trackersCache = HL.Field(HL.Table)

FacPowerPoleTravelHintCtrl.m_currentLogicId = HL.Field(HL.Any) << 0

FacPowerPoleTravelHintCtrl.m_lateTickKey = HL.Field(HL.Number) << -1


FacPowerPoleTravelHintCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_trackers = {}
    self.m_trackersCache = {}
    self.m_tempTargetInfoList = nil

    self.view.tracker.gameObject:SetActive(false)
    self.m_lateTickKey = LuaUpdate:Add("LateTick", function(deltaTime)
        self:_UpdateTrackers()
    end)
end

FacPowerPoleTravelHintCtrl.OnEnterBuildingMode = HL.StaticMethod(HL.Opt(HL.Any)) << function()
    local ctrl = FacPowerPoleTravelHintCtrl.AutoOpen(PANEL_ID, nil, false)
    ctrl.m_inBuildingMode = true

end

FacPowerPoleTravelHintCtrl.OnExitBuildingMode = HL.StaticMethod() << function()
    local ctrl = FacPowerPoleTravelHintCtrl.AutoOpen(PANEL_ID, nil, false)
    ctrl.m_inBuildingMode = false


    ctrl.m_isShown = false

    if ctrl.m_tempTargetInfoList ~= nil then
        for _, poleInfo in pairs(ctrl.m_tempTargetInfoList) do
            poleInfo.lineComponent:HideTempLinkLine()
        end
        ctrl.m_tempTargetInfoList = nil
    end

    for i = #ctrl.m_trackers, 1, -1 do
        ctrl.m_trackers[i].obj:SetActive(false)
        table.insert(ctrl.m_trackersCache, ctrl.m_trackers[i])
        table.remove(ctrl.m_trackers, i)
    end
end

FacPowerPoleTravelHintCtrl.OnBuild = HL.StaticMethod(HL.Table) << function(args)
    local ctrl = FacPowerPoleTravelHintCtrl.AutoOpen(PANEL_ID, nil, false)
    ctrl:OnBuildModeUpdated(args.buildingTypeId, args.position)
end

FacPowerPoleTravelHintCtrl.OnBuildModeUpdated = HL.Method(HL.String, Vector3) << function(self, buildingTypeId, position)
    if string.isEmpty(self.m_buildingTypeId) then
        self:OnBuildModeChanged(buildingTypeId, position)
        return
    else
        if self.m_buildingTypeId ~= buildingTypeId then
            self:OnBuildModeChanged(buildingTypeId, position)
            return
        end
    end

    if self.m_buildModePosition == nil then
        self:OnBuildModeChanged(buildingTypeId, position)
    else
        if self.m_buildModePosition ~= position then
            self:OnBuildModeChanged(buildingTypeId, position)
        end
    end
end

FacPowerPoleTravelHintCtrl.OnBuildModeChanged = HL.Method(HL.String, Vector3) << function(self, buildingTypeId, position)
    self.m_buildingTypeId = buildingTypeId
    self.m_buildModePosition = position





    if not self.m_inBuildingMode then
        return
    end

    if self.m_tempTargetInfoList ~= nil then
        for _, poleInfo in pairs(self.m_tempTargetInfoList) do
            poleInfo.lineComponent:HideTempLinkLine()
        end
        self.m_tempTargetInfoList = nil
    end

    self.m_isShown = true
    self.m_tempTargetInfoList = GameWorld.gameMechManager.travelPoleBrain:GetSurroundingTravelPoleBuildHintInfoList(self.m_buildingTypeId, self.m_buildModePosition)
end

FacPowerPoleTravelHintCtrl.OnMove = HL.StaticMethod(HL.Table) << function(args)
    local ctrl = FacPowerPoleTravelHintCtrl.AutoOpen(PANEL_ID, nil, false)
    ctrl:OnMoveModeUpdated(args.buildingTypeId, args.position, args.nodeId)
end

FacPowerPoleTravelHintCtrl.OnMoveModeUpdated = HL.Method(HL.String, Vector3, HL.Any) << function(self, buildingTypeId, position, nodeId)
    if string.isEmpty(self.m_moveModeNodeId) then
        self:OnMoveModeChanged(buildingTypeId, position, nodeId)
        return
    else
        if self.m_moveModeNodeId ~= nodeId then
            self:OnMoveModeChanged(buildingTypeId, position, nodeId)
            return
        end
    end

    if self.m_moveModePosition == nil then
        self:OnMoveModeChanged(buildingTypeId, position, nodeId)
    else
        if self.m_moveModePosition ~= position then
            self:OnMoveModeChanged(buildingTypeId, position, nodeId)
        end
    end
end

FacPowerPoleTravelHintCtrl.OnMoveModeChanged = HL.Method(HL.String, Vector3, HL.Any) << function(self, buildingTypeId, position, nodeId)
    self.m_buildingTypeId = buildingTypeId
    self.m_moveModePosition = position
    self.m_moveModeNodeId = nodeId





    if not self.m_inBuildingMode then
        return
    end

    if self.m_tempTargetInfoList ~= nil then
        for _, poleInfo in pairs(self.m_tempTargetInfoList) do
            poleInfo.lineComponent:HideTempLinkLine()
        end
        self.m_tempTargetInfoList = nil
    end

    self.m_isShown = true
    self.m_tempTargetInfoList = GameWorld.gameMechManager.travelPoleBrain:GetSurroundingTravelPoleBuildHintInfoListExclude(self.m_buildingTypeId, self.m_moveModePosition, self.m_moveModeNodeId)
end


FacPowerPoleTravelHintCtrl._UpdateTrackers = HL.Method() << function(self)
    if not self.m_isShown or not GameInstance.playerController.mainCharacter then
        return
    end

    local targetScrPosDict = {}
    local targetDistanceDict = {}
    local targetIconStatusDict = {}
    local targetStatusDict = {}
    local targetLineDict = {}
    local targetEntityDict = {}
    local logicIdList = {}
    local poleInfoList = {}
    local targetIsOtherSocialTravelDict = {}
    local mainCharacterPos = GameUtil.playerPos

    if self.m_tempTargetInfoList ~= nil then
        for _, poleInfo in pairs(self.m_tempTargetInfoList) do
            if poleInfo.entity.isValid then
                local screenPos, isInside = UIUtils.objectPosToUI(poleInfo.targetPos, self.uiCamera)
                table.insert(targetScrPosDict, screenPos)
                table.insert(targetDistanceDict, poleInfo.distance)
                table.insert(targetIconStatusDict, poleInfo.iconStatus)
                table.insert(targetStatusDict, poleInfo.status)
                table.insert(targetLineDict, poleInfo.line)
                table.insert(logicIdList, poleInfo.logicId)
                table.insert(poleInfoList, poleInfo)
                table.insert(targetIsOtherSocialTravelDict, FactoryUtils.isOthersSocialBuilding(CSFactoryUtil.GetNodeIdByLogicId(poleInfo.logicId)))
            end
        end
    end

    if #self.m_trackers > #targetScrPosDict then
        for i = #self.m_trackers, #targetScrPosDict + 1, -1 do
            self.m_trackers[i].obj:SetActive(false)
            table.insert(self.m_trackersCache, self.m_trackers[i])
            table.remove(self.m_trackers, i)
        end
    end

    if #self.m_trackers < #targetScrPosDict then
        for i = #self.m_trackers + 1, #targetScrPosDict do
            table.insert(self.m_trackers, self:_CreateNewTracker())
        end
    end

    for i = 1, #targetScrPosDict do
        local item = self.m_trackers[i]
        if item then
            local uiPos, uiAngle, isOutBound = UIUtils.mapScreenPosToEllipseEdge(targetScrPosDict[i], self.view.config.ELLIPSE_X_RADIUS, self.view.config.ELLIPSE_Y_RADIUS)


            item.tracker:UpdateIsOtherSocial(targetIsOtherSocialTravelDict[i])
            item.tracker:UpdatePosition(uiPos, uiAngle, isOutBound)
            item.tracker:UpdateDistance(targetDistanceDict[i])
            item.tracker:UpdateStatus(targetStatusDict[i])
            item.tracker:UpdateIconStatus(targetIconStatusDict[i])

            local line = targetLineDict[i]
            if line ~= nil and line.isValid then
                line:SetIsDot(true)
            end
        end
    end
end

FacPowerPoleTravelHintCtrl._CreateNewTracker = HL.Method().Return(HL.Table) << function(self)
    local cacheCount = #self.m_trackersCache
    if cacheCount > 0 then
        local cacheObj = self.m_trackersCache[cacheCount]
        cacheObj.obj:SetActive(true)
        table.remove(self.m_trackersCache, cacheCount)
        return cacheObj
    end
    local obj = CSUtils.CreateObject(self.view.tracker.gameObject, self.view.main.transform)
    obj:SetActive(true)
    local item = {}
    item.obj = obj
    item.tracker = obj:GetComponent(typeof(CS.Beyond.UI.UIPowerPoleFastTravelTracker))
    return item
end

HL.Commit(FacPowerPoleTravelHintCtrl)
