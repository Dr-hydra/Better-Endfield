
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FacConditioner

FacConditionerCtrl = HL.Class('FacConditionerCtrl', uiCtrl.UICtrl)

local NAVI_STATE = {
    SELECTED = "Selected",
    FIRST = "First",
    NONE = "None",
}





FacConditionerCtrl.s_messages = HL.StaticField(HL.Table) << {

    [MessageConst.ON_FACTORY_DEPOT_CHANGED] = "OnDepotChange",
}

FacConditionerCtrl.m_curTypeIndex = HL.Field(HL.Number) << -1

FacConditionerCtrl.m_itemShowingTypeInfos = HL.Field(HL.Table)

FacConditionerCtrl.m_materialMap = HL.Field(HL.Table)

FacConditionerCtrl.m_showItemList = HL.Field(HL.Table)

FacConditionerCtrl.m_showItemMap = HL.Field(HL.Table)

FacConditionerCtrl.m_onClickItem = HL.Field(HL.Function)

FacConditionerCtrl.m_selectItemId = HL.Field(HL.String) << ""

FacConditionerCtrl.m_typeCells = HL.Field(HL.Forward('UIListCache'))

FacConditionerCtrl.m_getCell = HL.Field(HL.Function)

FacConditionerCtrl.m_isFluid = HL.Field(HL.Boolean) << false

FacConditionerCtrl.m_sortOptions = HL.Field(HL.Table)

FacConditionerCtrl.m_sortData = HL.Field(HL.Table)

FacConditionerCtrl.m_sortIncremental = HL.Field(HL.Boolean) << false

FacConditionerCtrl.m_waitingNaviState = HL.Field(HL.String) << NAVI_STATE.NONE


FacConditionerCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    if arg.onClickItem then
        self.m_onClickItem = arg.onClickItem
    end
    if arg.selectItemId then
        self.m_selectItemId = arg.selectItemId
    end
    self.m_isFluid = arg.isFluid or false

    self.view.closeBtn.onClick:AddListener(function()
        Notify(MessageConst.HIDE_COMMON_HOVER_TIP, { noAnimation = true })
        self:PlayAnimationOutAndClose()
    end)

    self.view.typesNode.gameObject:SetActiveIfNecessary(not self.m_isFluid)
    self:_InitItemList()
    self:_InitSortNode()
    if self.m_isFluid then
        self:_InitFluidData()
    else
        self:_InitAllItemData()
        self:_InitTypeData()
        self:_InitTypeList()
    end

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
end

FacConditionerCtrl.OnDepotChange = HL.Method(HL.Table) << function(self, args)
    local depotChange = unpack(args)
    for _, itemId in pairs(depotChange.normalItemIds) do
        if self.m_materialMap[itemId] ~= nil then
            local _, _, itemCount = Utils.getItemCount(itemId, true)
            self.m_materialMap[itemId].count = itemCount
        end
        local index = self.m_showItemMap[itemId]
        if index ~= nil then
            local obj = self.view.itemList:Get(CSIndex(index))
            if obj then
                self:_OnUpdateCell(obj, index)
            end
        end
    end
end

FacConditionerCtrl._InitSortNode = HL.Method() << function(self)
    self.m_sortOptions = UIConst.FAC_DEPOT_SORT_OPTIONS
    self.view.sortNode:InitSortNode(self.m_sortOptions, function(optData, isIncremental)
        self:_OnSortChanged(optData, isIncremental)
    end, nil, nil, true)
    self.m_sortData = self.m_sortOptions[1]
    self.m_sortIncremental = self.view.sortNode.isIncremental
end

FacConditionerCtrl._OnSortChanged = HL.Method(HL.Table, HL.Boolean) << function(self, optData, isIncremental)
    if self.m_showItemList == nil or #self.m_showItemList == 0 then
        return
    end
    self.m_sortData = optData
    self.m_sortIncremental = isIncremental
    local sortKeys
    if optData.reverseKeys and not isIncremental then
        sortKeys = optData.reverseKeys
    else
        sortKeys = optData.keys
    end
    if self.m_showItemList[1] and self.m_showItemList[1].isEmptyChoice then
        table.remove(self.m_showItemList, 1)
    end
    table.sort(self.m_showItemList, Utils.genSortFunction(sortKeys, isIncremental))
    table.insert(self.m_showItemList, 1, { id = "", count = 0, extraOrder = 1, isEmptyChoice = true, })
    self.m_showItemMap = {}
    for index, item in ipairs(self.m_showItemList) do
        self.m_showItemMap[item.id] = index
    end
    if (self.m_waitingNaviState == NAVI_STATE.SELECTED and self.m_showItemMap[self.m_selectItemId] == nil) or self.m_waitingNaviState == NAVI_STATE.NONE then
        self.m_waitingNaviState = NAVI_STATE.FIRST
    end
    self.view.itemList:UpdateCount(#self.m_showItemList)
    if self.m_waitingNaviState == NAVI_STATE.FIRST then
        self.view.itemList:ScrollToIndex(0, true)
    elseif self.m_waitingNaviState == NAVI_STATE.SELECTED and self.m_selectItemId ~= nil and self.m_showItemMap[self.m_selectItemId] ~= nil then
        local targetIndex = self.m_showItemMap[self.m_selectItemId]
        self.view.itemList:ScrollToIndex(CSIndex(targetIndex), true)
    end
end

FacConditionerCtrl._InitTypeData = HL.Method() << function(self)
    self.m_curTypeIndex = 1
    self.m_itemShowingTypeInfos = {
        {
            name = Language.LUA_FAC_ALL,
            icon = "icon_item_type_all",
        }
    }
    local showingTypes = UIConst.FACTORY_DEPOT_SHOWING_TYPES
    for _, v in ipairs(showingTypes) do
        local data = Tables.itemShowingTypeTable:GetValue(v:ToInt())
        table.insert(self.m_itemShowingTypeInfos, {
            name = data.name,
            icon = data.icon,
            type = data.type:GetHashCode(),
        })
    end

    local typeMap = {}
    for _, info in pairs(self.m_materialMap) do
        if not typeMap[info.showingType] then
            typeMap[info.showingType] = true
        end
    end
    local resultTypeInfos = {}
    for _, info in ipairs(self.m_itemShowingTypeInfos) do
        if info.type ~= nil then
            if typeMap[info.type] == true then
                table.insert(resultTypeInfos, info)
            end
        else
            table.insert(resultTypeInfos, info)
        end
    end
    self.m_itemShowingTypeInfos = resultTypeInfos
end

FacConditionerCtrl._InitAllItemData = HL.Method() << function(self)
    local materialMap = {}
    local itemIdList
    if Utils.isInBlackbox() then
        local bData = GameWorld.worldInfo.curLevel.levelData.blackbox
        itemIdList = bData.statistics.limitedStatisticItemIds
    else
        local succ, showingItemsData = Tables.factoryItemShowingHubTable:TryGetValue(Utils.getCurDomainId())
        itemIdList = showingItemsData.list
    end

    for _, itemId in pairs(itemIdList) do
        if not Tables.liquidTable:ContainsKey(itemId) and not Tables.gasTable:ContainsKey(itemId) and GameInstance.player.inventory:IsItemFound(itemId) then
            local showItem = false
            if FactoryUtils.isTimeLimitedItem(itemId) then
                local craftInfoList, canCraft = FactoryUtils.getItemCrafts(itemId)
                showItem = next(craftInfoList) ~= nil
            else
                showItem = true
            end
            if showItem then
                local itemData = Tables.itemTable[itemId]
                local _, _, itemCount = Utils.getItemCount(itemId, true)
                materialMap[itemId] = {
                    id = itemId,
                    count = itemCount,
                    data = itemData,
                    showingType = itemData.showingType:GetHashCode(),
                    sortId1 = -itemData.sortId1,
                    sortId2 = itemData.sortId2,
                    rarity = itemData.rarity
                }
            end
        end
    end

    self.m_materialMap = materialMap
end

FacConditionerCtrl._InitTypeList = HL.Method() << function(self)
    self.m_typeCells = UIUtils.genCellCache(self.view.typeCell)
    self.m_typeCells:Refresh(#self.m_itemShowingTypeInfos, function(cell, index)
        local info = self.m_itemShowingTypeInfos[index]

        cell.dimIcon:LoadSprite(UIConst.UI_SPRITE_INVENTORY, info.icon)
        cell.lightIcon:LoadSprite(UIConst.UI_SPRITE_INVENTORY, info.icon)

        if cell.name then
            cell.name.text = info.name
        end

        local isSelected = index == self.m_curTypeIndex
        cell.decoLine.gameObject:SetActive(not (isSelected or index == 1 or index == (self.m_curTypeIndex + 1)))
        cell.dimIcon.gameObject:SetActive(not isSelected)
        cell.lightNode.gameObject:SetActive(isSelected)

        cell.toggle.onValueChanged:RemoveAllListeners()
        cell.toggle.isOn = isSelected
        cell.toggle.onValueChanged:AddListener(function(isOn)
            if isOn and self.m_curTypeIndex ~= index then
                self.m_waitingNaviState = NAVI_STATE.FIRST
                self:_OnClickShowingType(index)
            end
        end)

        cell.gameObject.name = "TypeCell_" .. index
    end)
    self.m_waitingNaviState = string.isEmpty(self.m_selectItemId) and NAVI_STATE.FIRST or NAVI_STATE.SELECTED
    self:_OnClickShowingType(self.m_curTypeIndex)
    LayoutRebuilder.ForceRebuildLayoutImmediate(self.view.typesNode.transform)
end

FacConditionerCtrl._InitItemList = HL.Method() << function(self)
    self.m_getCell = UIUtils.genCachedCellFunction(self.view.itemList)
    self.view.itemList.onUpdateCell:AddListener(function(object, csIndex)
        self:_OnUpdateCell(object, LuaIndex(csIndex))
    end)
end

FacConditionerCtrl._OnClickShowingType = HL.Method(HL.Number) << function(self, index)
    self.m_curTypeIndex = index
    local showingType = self.m_itemShowingTypeInfos[index].type

    local showItemList = {}
    for _, info in pairs(self.m_materialMap) do
        if not showingType or showingType == info.showingType then
            table.insert(showItemList, info)
        end
    end
    self.m_showItemList = showItemList
    self:_OnSortChanged(self.m_sortData, self.m_sortIncremental)

    self.m_typeCells:Update(function(cell, k)
        cell.decoLine.gameObject:SetActive(not (k == self.m_curTypeIndex or k == 1 or k == (self.m_curTypeIndex + 1)))
        cell.dimIcon.gameObject:SetActive(k ~= index)
        cell.lightNode.gameObject:SetActive(k == index)
    end)

    LayoutRebuilder.ForceRebuildLayoutImmediate(self.view.typesNode.transform)
end

FacConditionerCtrl._OnUpdateCell = HL.Method(GameObject, HL.Number) << function(self, object, luaIndex)
    local cell = self.m_getCell(object)
    local info = self.m_showItemList[luaIndex]
    cell:InitItem(info, function()
        Notify(MessageConst.HIDE_COMMON_HOVER_TIP, { noAnimation = true })
        if self.m_onClickItem ~= nil then
            self.m_onClickItem(info.id)
        end
        self:PlayAnimationOutAndClose()
    end, nil, true)
    cell.view.normalBG.gameObject:SetActiveIfNecessary(true)
    local isSelected = not string.isEmpty(info.id) and info.id == self.m_selectItemId
    cell.view.destroySelectNode.gameObject:SetActiveIfNecessary(isSelected)
    if string.isEmpty(info.id) then
        cell.gameObject.name = "Item__" .. CSIndex(luaIndex)
    else
        cell.gameObject.name = "Item_" .. info.id
        cell:OpenLongPressTips()
        cell.view.countNode.gameObject:SetActiveIfNecessary(not self.m_isFluid)
    end
    if DeviceInfo.usingController then
        if (self.m_waitingNaviState == NAVI_STATE.SELECTED and isSelected)
            or (self.m_waitingNaviState == NAVI_STATE.FIRST and luaIndex == 1) then
            self:SetNaviTarget(cell.view.button)
            self.m_waitingNaviState = NAVI_STATE.NONE
        end
        local confirmTextId = isSelected and "key_hint_fac_unloader_cancel_select" or "key_hint_fac_unloader_confirm_select"
        InputManagerInst:SetBindingText(cell.view.button.hoverConfirmBindingId, Language[confirmTextId])
    end

end

FacConditionerCtrl._InitFluidData = HL.Method() << function(self)
    local materialMap = {}
    self.m_showItemList = {}
    for liquidId, liquidData in pairs(Tables.liquidTable) do
        if GameInstance.player.inventory:IsItemFound(liquidId) then
            local itemData = Tables.itemTable[liquidId]
            local itemInfo = {
                id = liquidId,
                data = itemData,
                showingType = itemData.showingType:GetHashCode(),
                sortId1 = -itemData.sortId1,
                sortId2 = itemData.sortId2,
                rarity = itemData.rarity
            }
            materialMap[liquidId] = itemInfo
            table.insert(self.m_showItemList, itemInfo)
        end
    end
    for gasId, gasData in pairs(Tables.gasTable) do
        if GameInstance.player.inventory:IsItemFound(gasId) then
            local itemData = Tables.itemTable[gasId]
            local itemInfo = {
                id = gasId,
                data = itemData,
                showingType = itemData.showingType:GetHashCode(),
                sortId1 = -itemData.sortId1,
                sortId2 = itemData.sortId2,
                rarity = itemData.rarity
            }
            materialMap[gasId] = itemInfo
            table.insert(self.m_showItemList, itemInfo)
        end
    end

    self.m_waitingNaviState = string.isEmpty(self.m_selectItemId) and NAVI_STATE.FIRST or NAVI_STATE.SELECTED
    self.m_materialMap = materialMap
    self:_OnSortChanged(self.m_sortData, self.m_sortIncremental)
end











HL.Commit(FacConditionerCtrl)
