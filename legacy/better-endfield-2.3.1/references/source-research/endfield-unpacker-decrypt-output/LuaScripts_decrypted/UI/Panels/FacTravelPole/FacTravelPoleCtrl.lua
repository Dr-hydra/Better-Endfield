local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FacTravelPole
FacTravelPoleCtrl = HL.Class('FacTravelPoleCtrl', uiCtrl.UICtrl)





FacTravelPoleCtrl.s_messages = HL.StaticField(HL.Table) << {

}

FacTravelPoleCtrl.m_nodeId = HL.Field(HL.Any)

FacTravelPoleCtrl.m_isUpgraded = HL.Field(HL.Boolean) << false

FacTravelPoleCtrl.m_hasDefaultNext = HL.Field(HL.Boolean) << false

FacTravelPoleCtrl.m_currPoleMarkInstId = HL.Field(HL.String) << ""

FacTravelPoleCtrl.m_uiInfo = HL.Field(CS.Beyond.Gameplay.RemoteFactory.BuildingUIInfo_TravelPole)


FacTravelPoleCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_uiInfo = arg.uiInfo
    local nodeId = self.m_uiInfo.nodeId
    self.m_nodeId = nodeId

    self.view.buildingCommon:InitBuildingCommon(self.m_uiInfo)

    self:_InitMapNode()
    self:_RefreshBasicContent()
    self:_RefreshTravelPoleMapOrder()

    self.view.buildingCommon.view.buttonsNaviGroup:NaviToThisGroup()
end

FacTravelPoleCtrl._InitMapNode = HL.Method() << function(self)
    self.view.mapMask.gameObject:SetActive(false)
    self.view.emptyNode.gameObject:SetActive(true)

    GameInstance.player.mapManager.forceShowFacMarkInRegion = true
    if GameInstance.player.mapManager:IsLevelLoaderDataExists(self.m_uiInfo.nodeHandler.belongScene.sceneIdStr) then
        local success, markInstId = GameInstance.player.mapManager:GetFacMarkInstIdByNodeId(self.m_uiInfo.chapterId, self.m_uiInfo.nodeId)
        if success then
            self.m_currPoleMarkInstId = markInstId
            self.view.levelMapController:InitLevelMapController(MapConst.LEVEL_MAP_CONTROLLER_MODE.FIXED, {
                fixedMarkInstId = markInstId,
                hidePlayer = true,
                gridsMaskFollowTarget = self.view.gridsMaskFollowTarget,
                customRefreshMark = function(markViewData)
                    self:_RefreshTravelPoleMapMark(markViewData)
                end
            })
            self.view.mapMask.gameObject:SetActive(true)
            self.view.emptyNode.gameObject:SetActive(false)
        end
    end
    GameInstance.player.mapManager.forceShowFacMarkInRegion = false


    self.view.levelMapController.view.levelMapLoader.view.element.frontLineRoot.udPipeLine.gameObject:SetActive(false)
end

FacTravelPoleCtrl._GetAllRegionTravelPolesCount = HL.Method(HL.Any, HL.Any)
                                                    .Return(HL.Number)
    << function(self, isInFacRegion, isOthersSocialBuilding)
    local levelId = self.m_uiInfo.nodeHandler.belongScene.sceneIdStr
    local nodeType = self.m_uiInfo.nodeHandler.nodeType
    local travelPoleDataList = GameInstance.player.mapManager:GetFacMarkDataListByLevelIdAndType(levelId, nodeType)
    local count = 0
    for _, travelPoleData in cs_pairs(travelPoleDataList) do
        local valid = (isInFacRegion == nil or travelPoleData.isInFacRegion == isInFacRegion) and
            (isOthersSocialBuilding == nil or FactoryUtils.isOthersSocialBuilding(travelPoleData.nodeId) == isOthersSocialBuilding) and
            not FactoryUtils.isPendingBuildingNode(travelPoleData.nodeId)
        if valid then
            count = count + 1
        end
    end
    return count
end

FacTravelPoleCtrl._RefreshBasicContent = HL.Method() << function(self)
    self.m_isUpgraded = GameWorld.gameMechManager.travelPoleBrain:CheckTravelPoleIsUpgraded(self.m_nodeId)
    self.m_hasDefaultNext = GameWorld.gameMechManager.travelPoleBrain:CheckTravelPoleHasDefaultNext(self.m_nodeId)

    self.view.defaultNextTipsNode.gameObject:SetActiveIfNecessary(self.m_isUpgraded and self.m_hasDefaultNext)

    local sceneMsg = FactoryUtils.getCurSceneHandler()
    if sceneMsg ~= nil then
        local isInFacRegion = CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryUtil.IsNodeIncludedInMainRegion(self.m_uiInfo.nodeHandler)
        local isOthersSocialBuilding = FactoryUtils.isOthersSocialBuilding(self.m_nodeId)
        if isInFacRegion then
            self.view.regionNumberTxt.text = string.format("%d", self:_GetAllRegionTravelPolesCount(true, nil))
        elseif isOthersSocialBuilding then
            self.view.othersSocialBuildingNumberTxt.text = string.format("%d", self:_GetAllRegionTravelPolesCount(nil, true))
        else
            local bandwidth = self.m_uiInfo.nodeHandler.belongBandwidth
            self.view.currentNumberTxt.text = string.format("%d", bandwidth.travelPoleCurrent)
            self.view.maxNumberTxt.text = string.format("%d", bandwidth.travelPoleMax)

            local textColor = bandwidth.travelPoleCurrent >= bandwidth.travelPoleMax and
                self.view.config.COLOR_MAX or
                self.view.config.COLOR_NORMAL
            self.view.currentNumberTxt.color = textColor
        end

        local showOutRegion = not isInFacRegion and not isOthersSocialBuilding
        local showOthersSocialBuilding = not isInFacRegion and isOthersSocialBuilding
        self.view.regionNumber.gameObject:SetActive(isInFacRegion)
        self.view.outRegionNumber.gameObject:SetActive(showOutRegion)
        self.view.othersSocialBuildingNumber.gameObject:SetActive(showOthersSocialBuilding)
        self.view.regionTitle.gameObject:SetActive(isInFacRegion)
        self.view.outRegionTitle.gameObject:SetActive(showOutRegion)
        self.view.othersSocialBuildingTitle.gameObject:SetActive(showOthersSocialBuilding)
    end
end

FacTravelPoleCtrl._RefreshTravelPoleMapOrder = HL.Method() << function(self)

    self.view.levelMapController.view.travelLine:SetParent(self.view.levelMapController.view.staticElementBackRoot)
end

FacTravelPoleCtrl._RefreshTravelPoleMapMark = HL.Method(HL.Table) << function(self, markViewData)
    if markViewData == nil then
        return
    end

    local isVisible = markViewData.filterType == GEnums.MarkInfoType.TravelPole:GetHashCode() or
        markViewData.filterType == GEnums.MarkInfoType.HUB:GetHashCode()
    isVisible = isVisible and not FactoryUtils.isPendingBuildingNode(markViewData.runtimeData.nodeId)
    if isVisible then
        markViewData.markObj:ToggleForceShowMark("TravelPoleForceShow", isVisible)
    else
        markViewData.markObj:ToggleMarkHiddenState("TravelPoleHide", true)
    end

    if markViewData.instId == self.m_currPoleMarkInstId then
        local isOthers = FactoryUtils.isOthersSocialBuilding(markViewData.runtimeData.nodeId)
        if not isOthers then
            markViewData.markObj.view.iconImg.color = self.view.config.CURRENT_MARK_COLOR
        end
        self.view.currentHintNode.position = markViewData.mark.rectTransform.position
        markViewData.markObj.view.rectTransform:SetParent(self.view.levelMapController.view.currentMarkRoot)
    end
end

HL.Commit(FacTravelPoleCtrl)
