
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FacHUB
local SHOW_TRSBTN_CHECK_DOMAIN_ID = "domain_1"

FacHUBCtrl = HL.Class('FacHUBCtrl', uiCtrl.UICtrl)







FacHUBCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_SYNC_PRODUCT_DATA] = 'OnSyncProductData',
    [MessageConst.ON_SYNC_BOOKMARK_ITEM] = 'OnSyncBookmarkItem',
    [MessageConst.ON_SYNC_POWER_DATA] = 'OnSyncPowerData',
    [MessageConst.FAC_ON_DEL_TIME_LIMITED_FORMULA] = "_OnFormulaDelete",
}

FacHUBCtrl.m_nodeId = HL.Field(HL.Any)

FacHUBCtrl.m_domainId = HL.Field(HL.String) << ""

FacHUBCtrl.m_hubInfo = HL.Field(CS.Beyond.Gameplay.FacSpMachineSystem.HubInfo)

FacHUBCtrl.m_getMaterialCell = HL.Field(HL.Function)

FacHUBCtrl.m_materialList = HL.Field(HL.Table)

FacHUBCtrl.m_showItemCell = HL.Field(HL.Table)

FacHUBCtrl.m_activeAreaList = HL.Field(HL.Forward('UIListCache'))

FacHUBCtrl.m_curDomainDepotStackLimitCount = HL.Field(HL.Number) << 0

FacHUBCtrl.m_waitingForNaviFirstShowCell = HL.Field(HL.Boolean) << true



FacHUBCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.closeBtn.onClick:AddListener(function()
        PhaseManager:PopPhase(PhaseId.FacMachine)
    end)

    local isInBlackbox = Utils.isInBlackbox()
    self.view.main:SetState(isInBlackbox and "BlackBox" or "World")

    local nodeId = arg.uiInfo.nodeId
    self.m_nodeId = nodeId
    self.m_hubInfo = GameInstance.player.facSpMachineSystem:GetCurHubInfo()
    self.view.titleText.text = Tables.factoryBuildingTable:GetValue(arg.uiInfo.buildingId).name
    self.m_domainId = Utils.getCurDomainId()
    self.m_curDomainDepotStackLimitCount = Utils.getDepotItemStackLimitCountInCurrentDomain()

    if not isInBlackbox then
        self.view.domainName.text = Tables.domainDataTable[self.m_domainId].domainName
    end

    self.view.moveBtn.gameObject:SetActive(FactoryUtils.canMoveBuilding(nodeId))
    self.view.moveBtn.onClick:AddListener(function()
        self:_MoveBuilding()
    end)

    local inBlackbox = Utils.isInBlackbox()
    local placeSecondHub = false
    local showTrsBtn = false
    if not inBlackbox then
        placeSecondHub = GameInstance.player.remoteFactory:IsFacTransEntryUnlocked()
        local hasTransCfg, domainTransmissionCfg = Tables.factoryDomainItemTransmissionTable:TryGetValue(SHOW_TRSBTN_CHECK_DOMAIN_ID)
        local hasTransData, domainDevData = GameInstance.player.domainDevelopmentSystem.domainDevDataDic:TryGetValue(SHOW_TRSBTN_CHECK_DOMAIN_ID)
        if hasTransCfg and hasTransData then
            showTrsBtn = domainDevData.lv >= domainTransmissionCfg.unlockLevel
        end
    end
    self.view.transferBtn.gameObject:SetActive(placeSecondHub or showTrsBtn)
    if placeSecondHub or showTrsBtn then
        if placeSecondHub then
            self.view.transferBtn.onClick:AddListener(function()
                PhaseManager:OpenPhase(PhaseId.DomainItemTransfer)
            end)
        else
            self.view.transferBtn.onClick:AddListener(function()
                Notify(MessageConst.SHOW_TOAST, Language.LUA_HUB_TRANSFER_BTN_LOCK_TOAST)
            end)
        end
    end

    local curSceneInfo = GameInstance.remoteFactoryManager.currentSceneInfo

    local craftBtnEnabled = true
    if inBlackbox then
        if curSceneInfo then
            craftBtnEnabled = CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryUtil.IsHubBuildingCraftEnabledInBlackbox(curSceneInfo)
        else
            craftBtnEnabled = false
        end
    end
    self.view.craftNode.button.gameObject:SetActive(craftBtnEnabled)
    self.view.craftNode.empty.gameObject:SetActive(not craftBtnEnabled)
    self.view.craftNode.button.onClick:AddListener(function()
        Notify(MessageConst.OPEN_FAC_BUILD_MODE_SELECT, { onlyCraftNode = true })
    end)


    self.view.facDataNode.moreBtn.onClick:AddListener(function()
        PhaseManager:OpenPhase(PhaseId.FacHUBData, { tabIndex = 1 })
    end)
    self.view.powerStorageNode.powerMoreBtn.onClick:AddListener(function()
        PhaseManager:OpenPhase(PhaseId.FacHUBData, { tabIndex = 2 })
    end)

    self.m_getMaterialCell = UIUtils.genCachedCellFunction(self.view.facDataNode.materialList)
    self.view.facDataNode.materialList.onUpdateCell:AddListener(function(object, csIndex)
        self:_OnUpdateMaterialCell(self.m_getMaterialCell(object), LuaIndex(csIndex))
    end)

    self:_InitPowerStorageNode()
    self:_InitOtherStatisticDataNode()
    self:_InitFacDataNode()
    self:_InitFacHubController()
    self:_InitOfflineTips()

    GameInstance.player.facSpMachineSystem:ReqAllProductData(GEnums.FacStatisticRank_Productivity.Minute10)

    self:_StartCoroutine(function()
        while true do
            coroutine.wait(UIConst.FAC_HUB_UPDATE_INTERVAL)
            if self:IsShow() and self.m_phase.isActive then
                self:_ReqProductData()
                self:_ReqPowerData()
            end
        end
    end)

    self:_StartCoroutine(function()
        while true do
            coroutine.wait(UIConst.FAC_COMMON_UI_LARGER_UPDATE_INTERVAL)
            if self:IsShow() and self.m_phase.isActive then
                self:_RefreshPowerStorageNodeElse()
                self:_RefreshItemCount()
                self:_RefreshPowerStorageNodeProgress()
            end
        end
    end)
end




FacHUBCtrl._InitPowerStorageNode = HL.Method() << function(self)
    self:_RefreshPowerStorageNodeProgress()
    self:_RefreshPowerStorageNodeElse()

    local node = self.view.powerStorageNode
    node.genLine:InitBrokenLine()
    node.costLine:InitBrokenLine()
    node.notEnoughCostLine:InitBrokenLine()
    self:_ReqPowerData()
end

FacHUBCtrl._RefreshPowerStorageNodeElse = HL.Method() << function(self)
    local powerInfo = FactoryUtils.getCurRegionPowerInfo()
    local node = self.view.powerStorageNode

    local curPowerSave = powerInfo.powerSaveCurrent
    local maxPowerSave = powerInfo.powerSaveMax
    node.curPowerStorageValue.text = curPowerSave
    node.maxPowerStorageValue.text = maxPowerSave

    local powerCost = powerInfo.powerCost
    local powerGen = powerInfo.powerGen
    node.genPowerNode.text.text = powerGen
    node.usePowerNode.text.text = powerCost

    local isEnough = powerGen >= powerCost
    node.simpleStateController:SetState(isEnough and "Normal" or "NotEnough")

    if powerCost > powerGen then
        local time = curPowerSave / (powerCost - powerGen)
        node.time.text = UIUtils.getRemainingText(time)
    elseif powerCost < powerGen then
        local time = (maxPowerSave - curPowerSave) / (powerGen - powerCost)
        node.time.text = UIUtils.getRemainingText(time)
    else
        node.time.text = "--:--:--"
    end
end

FacHUBCtrl._RefreshPowerStorageNodeProgress = HL.Method() << function(self)
    local powerInfo = FactoryUtils.getCurRegionPowerInfo()
    local node = self.view.powerStorageNode
    local curPowerSave = powerInfo.powerSaveCurrent
    local maxPowerSave = powerInfo.powerSaveMax
    local fillAmount = 0
    if maxPowerSave > 0 then
        fillAmount = curPowerSave / maxPowerSave
    end
    node.fill.fillAmount = fillAmount
end






FacHUBCtrl._MoveBuilding = HL.Method() << function(self)
    local nodeId = self.m_nodeId
    if not FactoryUtils.canMoveBuilding(nodeId) then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_FACTORY_BUILDING_MOVE_NOT_ALLOWED)
        return
    end
    PhaseManager:ExitPhaseFast(PhaseId.FacMachine)
    Notify(MessageConst.FAC_ENTER_BUILDING_MODE, {
        nodeId = nodeId
    })
end

FacHUBCtrl._InitOtherStatisticDataNode = HL.Method() << function(self)
    if Utils.isInBlackbox() then
        return
    end

    self.m_activeAreaList = UIUtils.genCellCache(self.view.otherStatisticDataNode.areaCell)
    local areaIdList = FactoryUtils.getActiveChapterIdList()
    self.m_activeAreaList:Refresh(#areaIdList, function(cell, index)
        local id = areaIdList[index]
        cell.nameTxt.text = Tables.domainDataTable[id].domainName
    end)
end

FacHUBCtrl._InitOfflineTips = HL.Method() << function(self)
    if Utils.isInBlackbox() then
        return
    end

    local node = self.view.offlineTipsNode
    node.guideBtn.onClick:AddListener(function()
        UIManager:Open(PanelId.InstructionBook, "fac_hub_offline")
    end)
    local info = GameInstance.player.facSpMachineSystem.offlineInfo
    node.timeTxt.text = UIUtils.getLeftTime(info.offlineCalcDuration)

    if info.offlineMissCalcDuration > 0 then
        local lastNotifyTS = CS.Beyond.PlayerLocalData.GetUserLong("FacLastOfflineTimeStamp")
        if lastNotifyTS < info.endOfflineCalcTimestamp then
            CS.Beyond.PlayerLocalData.SetUserLong("FacLastOfflineTimeStamp", info.endOfflineCalcTimestamp)
            UIManager:Open(PanelId.FacHUBNotify)
            return
        end
    end

    CS.Beyond.Gameplay.Conditions.OnOpenFacHubPanelWithoutNotify.Trigger()
end






FacHUBCtrl._InitFacDataNode = HL.Method() << function(self)
    local materialList = {}
    local itemIdList
    if Utils.isInBlackbox() then
        local bData = GameWorld.worldInfo.curLevel.levelData.blackbox
        itemIdList = bData.statistics.limitedStatisticItemIds
    else
        local succ, showingItemsData = Tables.factoryItemShowingHubTable:TryGetValue(self.m_domainId)
        itemIdList = showingItemsData.list
    end

    local scopeInfo = GameInstance.player.remoteFactory.core:GetCurrentScopeInfo()
    for _, itemId in pairs(itemIdList) do
        if GameInstance.player.inventory:IsItemFound(itemId) then
            local showItem = false
            if FactoryUtils.isTimeLimitedItem(itemId) then
                local craftInfoList, canCraft = FactoryUtils.getItemCrafts(itemId)
                showItem = next(craftInfoList) ~= nil
            else
                showItem = true
            end

            if showItem then
                local itemData = Tables.itemTable[itemId]
                local showCount = FactoryUtils.isFactoryItemNormal(itemId)
                local isBookmark = scopeInfo:IsBookmarkItem(itemId, self.m_domainId)
                local order = 1
                if isBookmark then
                    order = 0
                end
                table.insert(materialList, {
                    itemId = itemId,
                    showCount = showCount,
                    isBookmark = isBookmark,
                    order = order,
                    showingType = itemData.showingType:GetHashCode(),
                    sortId1 = -itemData.sortId1,
                    sortId2 = itemData.sortId2,
                    rarity = itemData.rarity
                })
            end
        end
    end
    local keys = { "order", "sortId1", "sortId2", "id" }
    table.sort(materialList, Utils.genSortFunction(keys, true))

    self.m_materialList = materialList
    self.m_showItemCell = {}
    self.view.facDataNode.materialNaviGroup.enabled = next(materialList) ~= nil
    self.view.facDataNode.materialList:UpdateCount(#materialList)
    self:_RefreshItemCount()
end

FacHUBCtrl._OnUpdateMaterialCell = HL.Method(HL.Table, HL.Number) << function(self, cell, index)
    local itemInfo = self.m_materialList[index]

    if not lume.find(self.m_showItemCell, cell) then
        table.insert(self.m_showItemCell, cell)
    end
    cell.itemId = itemInfo.itemId

    if itemInfo.isBookmark then
        cell.mainController:SetState("BookMark")
    else
        cell.mainController:SetState("Normal")
    end

    if DeviceInfo.usingController then
        cell.item:InitItem( { id = itemInfo.itemId }, function()
            if not self.m_waitingForNaviFirstShowCell then
                cell.item:ShowTips()
            end
        end)
        cell.item:SetExtraInfo({
            tipsPosType = UIConst.UI_TIPS_POS_TYPE.LeftTop,
            tipsPosTransform = self.view.facDataNode.materialList.transform,
            isSideTips = true,
        })
    else
        cell.item:InitItem( { id = itemInfo.itemId }, true )
    end

    if itemInfo.showCount then
        local _, _, count = Utils.getItemCount(itemInfo.itemId, true)
        cell.count.text = string.format("%s/%s", count, self.m_curDomainDepotStackLimitCount)
    else
        cell.count.text = "-"
    end

    cell.productivity.text = string.format(Language.LUA_FAC_HUB_ITEM_GEN_RATE, 0)
    self:_ReqOneData(itemInfo.itemId)
end

FacHUBCtrl._RefreshItemCount = HL.Method() << function(self)
    self.view.facDataNode.materialList:UpdateShowingCells(function(csIndex, obj)
        local cell = self.m_getMaterialCell(obj)
        local index = LuaIndex(csIndex)
        local itemInfo = self.m_materialList[index]
        if itemInfo.showCount then
            local _, _, count = Utils.getItemCount(itemInfo.itemId, true)
            cell.count.text = string.format("%s/%s", count, self.m_curDomainDepotStackLimitCount)
        else
            cell.count.text = "-"
        end
    end)
end

FacHUBCtrl._ReqProductData = HL.Method() << function(self)
    local itemIds = {}
    for i = 1, #self.m_showItemCell do
        if self.m_showItemCell[i].itemId then
            table.insert(itemIds, self.m_showItemCell[i].itemId)
        end
    end
    GameInstance.player.facSpMachineSystem:ReqProductData(GEnums.FacStatisticRank_Productivity.Minute10, itemIds)
end

FacHUBCtrl.OnSyncProductData = HL.Method() << function(self)
    for i = 1, #self.m_showItemCell do
        local cell = self.m_showItemCell[i]
        local info = GameInstance.player.facSpMachineSystem:GetItemData(cell.itemId)
        if info then
            local type = info.productDataType
            if type == GEnums.FacStatisticRank_Productivity.Minute10:GetHashCode() then
                local genValue = info.productGen
                local count = Tables.factoryProductivityDataTypeTable:GetValue(type).count
                if genValue.Count >= count then
                    cell.productivity.text = string.format(Language.LUA_FAC_HUB_ITEM_GEN_RATE, genValue[genValue.Count - 1])
                else
                    cell.productivity.text = string.format(Language.LUA_FAC_HUB_ITEM_GEN_RATE, 0)
                end
            end
        end
    end
end

FacHUBCtrl.OnSyncBookmarkItem = HL.Method() << function(self)
    self:_InitFacDataNode()
end

FacHUBCtrl._ReqOneData = HL.Method(HL.String) << function(self, itemId)
    GameInstance.player.facSpMachineSystem:ReqOneProductData(GEnums.FacStatisticRank_Productivity.Minute10, itemId)
end

FacHUBCtrl._OnFormulaDelete = HL.Method(HL.Any) << function(self, args)
    self:_InitFacDataNode()
end





FacHUBCtrl._InitFacHubController = HL.Method() << function(self)
    self.view.facDataNode.materialNaviGroup.onIsFocusedChange:AddListener(function(isFocused)
        self.m_waitingForNaviFirstShowCell = not isFocused
        if isFocused then
            local firstIndex = self.view.facDataNode.materialList:GetShowingCellsIndexRange()
            local cell = self.m_getMaterialCell(LuaIndex(firstIndex))
            if cell ~= nil then
                if cell.item.view.button.isNaviTarget then
                    cell.item:ShowTips()
                else
                    self:SetNaviTarget(cell.item.view.button)
                end
            end
        end
    end)

    if FactoryUtils.canMoveBuilding(self.m_nodeId) then
        self.view.controllerSideMenuBtn.gameObject:SetActiveIfNecessary(true)
        self.view.controllerSideMenuBtn:InitControllerSideMenuBtn({
            extraBtnInfos = {
                {
                    button = self.view.moveBtn,
                    sprite = self.view.moveBtnIcon.sprite,
                    textId = "ui_fac_common_move",
                    priority = 1,
                }
            },
        })
    else
        self.view.controllerSideMenuBtn.gameObject:SetActiveIfNecessary(false)
    end

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
end



FacHUBCtrl._ReqPowerData = HL.Method() << function(self)
    GameInstance.player.facSpMachineSystem:ReqPowerData(GEnums.FacStatisticRank_Power.Minute10)
end

FacHUBCtrl.OnSyncPowerData = HL.Method(HL.Any) << function(self, args)
    logger.info("FacHUBCtrl.OnSyncPowerData")

    local type, genValue, costValue, domainId = unpack(args)
    if domainId ~= self.m_domainId then
        return
    end
    if type ~= GEnums.FacStatisticRank_Power.Minute10:GetHashCode() then
        return
    end

    local node = self.view.powerStorageNode
    local count = Tables.factoryPowerDataTypeTable:GetValue(type).count
    local maxValue = 1
    for _, value in pairs(genValue) do
        if value > maxValue then
            maxValue = value
        end
    end
    for _, value in pairs(costValue) do
        if value > maxValue then
            maxValue = value
        end
    end
    local genPoints = {}
    for _, value in pairs(genValue) do
        table.insert(genPoints, value / maxValue)
    end
    genPoints = lume.reverse(genPoints)
    local costPoints = {}
    for _, value in pairs(costValue) do
        table.insert(costPoints, value / maxValue)
    end
    costPoints = lume.reverse(costPoints)

    local curGen = genValue.Count >= count and genValue[genValue.Count - 1] or 0
    local curCost = costValue.Count >= count and costValue[costValue.Count - 1] or 0

    local isEnough = curGen >= curCost
    node.genLine:InitBrokenLine(genPoints, count)
    node.costLine:InitBrokenLine(costPoints, count)
    node.notEnoughCostLine:InitBrokenLine(costPoints, count)
end


HL.Commit(FacHUBCtrl)
