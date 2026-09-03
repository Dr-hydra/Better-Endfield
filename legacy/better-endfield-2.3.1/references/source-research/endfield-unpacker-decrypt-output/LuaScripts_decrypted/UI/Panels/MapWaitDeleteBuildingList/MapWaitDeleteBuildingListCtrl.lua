
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.MapWaitDeleteBuildingList

MapWaitDeleteBuildingListCtrl = HL.Class('MapWaitDeleteBuildingListCtrl', uiCtrl.UICtrl)






MapWaitDeleteBuildingListCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.FAC_ON_INVALID_BUILDING_INFO_CHANGED] = '_OnDataChanged',
    [MessageConst.SHOW_LEVEL_MAP_MARK_DETAIL] = '_OnLevelMapMarkDetailOpened',
}

MapWaitDeleteBuildingListCtrl.m_getCellFunc = HL.Field(HL.Function)

MapWaitDeleteBuildingListCtrl.m_getTitleFunc = HL.Field(HL.Function)

MapWaitDeleteBuildingListCtrl.m_dataList = HL.Field(HL.Table)

MapWaitDeleteBuildingListCtrl.m_timeGroupList = HL.Field(HL.Table)

MapWaitDeleteBuildingListCtrl.m_dataNumList = HL.Field(HL.Table)

MapWaitDeleteBuildingListCtrl.m_chapterId = HL.Field(HL.Number) << -1

MapWaitDeleteBuildingListCtrl.m_lastRemoveCellIndex = HL.Field(HL.Number) << -1



MapWaitDeleteBuildingListCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_chapterId = arg
    self.view.topBar.closeButton.onClick:AddListener(function()
        self:PlayAnimationOutAndClose()
    end)
    self.view.allStorageBtn.button.onClick:AddListener(function()
        self:_StorageAllBuildings()
    end)

    if not self.m_getCellFunc then
        self.m_getCellFunc = UIUtils.genCachedCellFunction(self.view.scrollList, function(object)
            return UIWidgetManager:Wrap(object)
        end)
        self.view.scrollList.onUpdateCell:AddListener(function(object, csIndex)
            self:_UpdateCell(self.m_getCellFunc(object), LuaIndex(csIndex))
        end)
        self.m_getTitleFunc = UIUtils.genCachedCellFunction(self.view.scrollList, function(object)
            return Utils.wrapLuaNode(object)
        end, true)
        self.view.scrollList.onUpdateGroupTitle:AddListener(function(object, csIndex)
            self:_OnUpdateGroupTitle(self.m_getTitleFunc(object), LuaIndex(csIndex))
        end)
        self.view.scrollList.getCellCountInGroup = function(groupCSIndex)
            return self.m_dataNumList[LuaIndex(groupCSIndex)]
        end
    end

    self:_RefreshList()
    self:_InitController()
end

MapWaitDeleteBuildingListCtrl._RefreshData = HL.Method() << function(self)
    local invalidPlacedBuildings = GameInstance.player.remoteFactory:GetInvalidPlacedBuildings(self.m_chapterId)
    local curServerTime = DateTimeUtils.GetCurrentTimestampBySeconds()
    local dataList = {}
    if invalidPlacedBuildings ~= nil then
        for i = 0, invalidPlacedBuildings.Count - 1 do
            local info = invalidPlacedBuildings[i]
            local instId = GameInstance.player.mapManager:GetFacMarkInstId(self.m_chapterId, info.nodeId)
            table.insert(dataList, {
                nodeId = info.nodeId,
                leftTime = info.invalidTimestamp - curServerTime,
                instId = instId,
                isMarkVisible = MapUtils.isMarkVisible(instId) and 0 or 1
            })
        end
    end

    if #dataList == 0 then
        self.m_dataNumList = {}
        self.m_dataList = dataList
        return
    end

    table.sort(dataList, Utils.genSortFunction({ "leftTime", "isMarkVisible", "nodeId" }, true))

    self.m_dataList = dataList

    local firstInfo = dataList[1]
    local dataNumList = {1}
    self.m_timeGroupList = {firstInfo.leftTime}
    local lastTime = firstInfo.leftTime
    local index = 1
    for i = 2, #dataList do
        local info = dataList[i]
        if info.leftTime ~= lastTime then
            local hour = info.leftTime / 3600
            local min = info.leftTime / 60
            if (hour > 0 and hour ~= lastTime / 3600) or (hour == 0 and min ~= lastTime / 60) then
                index = index + 1
                table.insert(dataNumList, 0)
                table.insert(self.m_timeGroupList, info.leftTime)
            end
            lastTime = info.leftTime
        end
        dataNumList[index] = dataNumList[index] + 1
    end

    self.m_dataNumList = dataNumList
end

MapWaitDeleteBuildingListCtrl._SetEmpty = HL.Method() << function(self)
    self.view.main:SetState("Empty")
    self.view.allStorageBtn.root:SetState("DisableState")
    self.view.allStorageBtn.button.enabled = false
    self.view.allStorageBtn.text.text = Language.LUA_FACTORY_STORAGE_ALL_INVALID_BUILDING_BTN_INACTIVE
end

MapWaitDeleteBuildingListCtrl._RefreshList = HL.Method() << function(self)
    self:_RefreshData()
    local dataCount = #self.m_dataList
    if dataCount == 0 then
        self:_SetEmpty()
    else
        self.view.main:SetState("Normal")
        self.view.allStorageBtn.root:SetState("NormalState")
        self.view.allStorageBtn.text.text = Language.LUA_FACTORY_STORAGE_ALL_INVALID_BUILDING_BTN
        self.view.scrollList:UpdateGroup(#self.m_dataNumList)
    end

    local lastRemoveIndex = self.m_lastRemoveCellIndex
    if DeviceInfo.usingController and lastRemoveIndex >= 0 then
        if lastRemoveIndex >= dataCount then
            lastRemoveIndex = CSIndex(dataCount)
        end
        if lastRemoveIndex >= 0 then
            self.view.scrollList:ScrollToIndex(lastRemoveIndex)
        end
        local cell = self.view.scrollList:Get(lastRemoveIndex)
        if cell then
            cell = self.m_getCellFunc(cell)
            self:SetNaviTarget(cell.view.inputBindingGroupNaviDecorator)
        end
    end
end

MapWaitDeleteBuildingListCtrl._OnUpdateGroupTitle = HL.Method(HL.Any, HL.Number) << function(self, cell, index)
    local leftTime = self.m_timeGroupList[index]
    cell.timeTxt.text = string.format(Language.LUA_FACTORY_INVALID_BUILDING_TIME_TITLE, UIUtils.getLeftTime(leftTime))
end

MapWaitDeleteBuildingListCtrl._UpdateCell = HL.Method(HL.Any, HL.Number) << function(self, cell, index)
    local info = self.m_dataList[index]
    local nodeId = info.nodeId
    local chapterId = self.m_chapterId
    cell.view.isStorage = false
    cell.view.isTracking = false
    local nodeHandler = FactoryUtils.getBuildingNodeHandler(nodeId, chapterId)
    if nodeHandler == nil then
        return
    end

    local templateId = nodeHandler.templateId
    local buildingName, itemId = self:_GetBuildingDisplayInfo(templateId)
    cell.view.nameTxt.text = buildingName

    local levelId = nodeHandler.belongScene.sceneIdStr
    local levelDescExisted, levelDescData = Tables.levelDescTable:TryGetValue(levelId)
    local levelName = ""
    if levelDescExisted then
        levelName = levelDescData.showName
    end

    cell.view.addressLauoyt.text.text = levelName

    if itemId then
        cell.view.itemBlack:InitItem({ id = itemId }, true)
    end

    cell.view.markBtn.onClick:RemoveAllListeners()
    if info.isMarkVisible == 0 then
        cell.view.markBtn.gameObject:SetActiveIfNecessary(true)
        cell.view.markBtn.onClick:AddListener(function()
            if not cell.view.isStorage then
                cell.view.isTracking = true
                self:_TrackBuilding(info.instId)
                self:PlayAnimationOutAndClose()
            end
        end)
    else
        cell.view.markBtn.gameObject:SetActiveIfNecessary(false)
    end
    cell.view.storageBtn.onClick:RemoveAllListeners()
    cell.view.storageBtn.onClick:AddListener(function()
        if not cell.view.isTracking then
            cell.view.isStorage = true
            self.m_lastRemoveCellIndex = CSIndex(index)
            self:_StorageBuilding(nodeId, buildingName)
        end
    end)
end


MapWaitDeleteBuildingListCtrl._GetBuildingDisplayInfo = HL.Method(HL.String).Return(HL.String, HL.String) << function(self, templateId)
    local succ, buildingData = Tables.factoryBuildingTable:TryGetValue(templateId)
    if succ then
        local itemId = FactoryUtils.getBuildingItemId(templateId)
        return buildingData.name, itemId
    end

    local success, pipeData = Tables.factoryLiquidPipeTable:TryGetValue(templateId)
    if success then
        return pipeData.pipeData.name, pipeData.pipeData.itemId
    end

    local logisticData = FactoryUtils.getLogisticData(templateId)
    if logisticData then
        return logisticData.name, logisticData.itemId
    end

    return "", ""
end

MapWaitDeleteBuildingListCtrl._TrackBuilding = HL.Method(HL.String) << function(self, instId)
    local success, markData = GameInstance.player.mapManager:GetMarkInstRuntimeData(instId)
    if success then
        PhaseManager:GoToPhase(PhaseId.Map, {
            instId = instId,
            levelId = markData.levelId
        })
    end
    GameInstance.player.mapManager:TrackMark(instId, true)
end

MapWaitDeleteBuildingListCtrl._StorageBuilding = HL.Method(HL.Number, HL.String) << function(self, nodeId, name)
    if GameWorld.gameMechManager.travelPoleBrain:CanOpenMiniMap() then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_SYSTEM_FORBIDDEN)
        return
    end

    FactoryUtils.delBuilding(nodeId, function()
        Notify(MessageConst.SHOW_TOAST, string.format(Language.LUA_FACTORY_STORAGE_BUILDING_TOAST, name))
    end, true, nil, self.m_chapterId, true)
end

MapWaitDeleteBuildingListCtrl._StorageAllBuildings = HL.Method() << function(self)
    if GameWorld.gameMechManager.travelPoleBrain:CanOpenMiniMap() then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_SYSTEM_FORBIDDEN)
        return
    end

    local dataList = self.m_dataList
    if not dataList or #dataList == 0 then
        return
    end

    local nodeList = {}
    for _, data in ipairs(dataList) do
        table.insert(nodeList, data.nodeId)
    end

    GameInstance.player.remoteFactory.core:Message_OpDismantleBatch(self.m_chapterId, nodeList, {}, {}, true, function()
        Notify(MessageConst.SHOW_TOAST, Language.LUA_FACTORY_STORAGE_ALL_BUILDING_TOAST)
    end)
end

MapWaitDeleteBuildingListCtrl._OnDataChanged = HL.Method() << function(self)
    self:_RefreshList()
end

MapWaitDeleteBuildingListCtrl._InitController = HL.Method() << function(self)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
    self.view.scrollListSelectableNaviGroup:NaviToThisGroup()
end

MapWaitDeleteBuildingListCtrl._OnLevelMapMarkDetailOpened = HL.Method(HL.Table) << function(self, args)
    self:Close()
end

HL.Commit(MapWaitDeleteBuildingListCtrl)
