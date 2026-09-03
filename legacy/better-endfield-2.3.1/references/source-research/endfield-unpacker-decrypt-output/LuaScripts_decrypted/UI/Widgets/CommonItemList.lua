local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

local LIST_CONFIG = {
    [UIConst.COMMON_ITEM_LIST_TYPE.WEAPON_EXHIBIT_GEM] = {
        infoProcessFunc = "processWeaponGem",
        filterTagGroupFunc = "generateConfig_WEAPON_EXHIBIT_GEM",
        getSortOption = function()
            return UIConst.WEAPON_GEM_SORT_OPTION
        end,
        getDepotFunc = "_GetWeaponGemDepot",
        applyFilterFunc = "_ApplyFilterGem",
        hideSort = true,
    },
    [UIConst.COMMON_ITEM_LIST_TYPE.GEM_RECAST] = {
        infoProcessFunc = "processWeaponGem",
        filterTagGroupFunc = "generateConfig_DEPOT_GEM",
        getSortOption = function()
            return UIConst.WEAPON_GEM_SORT_OPTION
        end,
        getDepotFunc = "_GetWeaponGemDepot",
    },
    [UIConst.COMMON_ITEM_LIST_TYPE.WEAPON_GEM_MATERIAL] = {
        infoProcessFunc = "processWeaponGem",
        filterTagGroupFunc = "generateConfig_WEAPON_EXHIBIT_GEM",
        getSortOption = function()
            return UIConst.WEAPON_GEM_SORT_OPTION
        end,
        getDepotFunc = "_GetWeaponGemMaterialDepot",
        applyFilterFunc = "_ApplyFilterGem",
        hideSort = true,
    },
    [UIConst.COMMON_ITEM_LIST_TYPE.WEAPON_EXHIBIT_UPGRADE] = {
        infoProcessFunc = "processWeaponUpgradeIngredient",
        getSortOption = function()
            return UIConst.WEAPON_SORT_OPTION
        end,
        getDepotFunc = "_GetWeaponUpgradeDepot",
        filterTagGroupFunc = "generateConfig_CHAR_INFO_WEAPON",
    },
    [UIConst.COMMON_ITEM_LIST_TYPE.WEAPON_EXHIBIT_POTENTIAL] = {
        infoProcessFunc = "processWeaponPotential",
        getSortOption = function()
            return UIConst.WEAPON_POTENTIAL_SORT_OPTION
        end,
        hideSort = true,
        hideFilter = true,
        getDepotFunc = "_GetWeaponPotentialDepot",
    },
    [UIConst.COMMON_ITEM_LIST_TYPE.CHAR_INFO_WEAPON] = {
        infoProcessFunc = "processWeapon",
        getSortOption = function()
            return UIConst.WEAPON_SORT_OPTION
        end,
        getDepotFunc = "_GetWeaponDepot",
        filterTagGroupFunc = "generateConfig_CHAR_INFO_WEAPON",
    },
    [UIConst.COMMON_ITEM_LIST_TYPE.CHAR_INFO_EQUIP] = {
        infoProcessFunc = "processEquip",
        getSortOption = function()
            return UIConst.EQUIP_SORT_OPTION
        end,
        filterTagGroupFunc = "generateConfig_EQUIP_ENHANCE",
        getDepotFunc = "_GetEquipDepot"
    },
    [UIConst.COMMON_ITEM_LIST_TYPE.CHAR_INFO_TACTICAL_ITEM] = {
        infoProcessFunc = "processTacticalItem",
        getSortOption = function()
            return UIConst.TACTICAL_ITEM_SORT_OPTION
        end,
        filterTagGroupFunc = "generateConfig_TACTICAL_ITEM",
        getDepotFunc = "_GetTacticalItemDepot"
    },
    [UIConst.COMMON_ITEM_LIST_TYPE.EQUIP_TECH_EQUIP_ENHANCE] = {
        infoProcessFunc = "processEquipEnhance",
        filterTagGroupFunc = "generateConfig_EQUIP_ENHANCE",
        getSortOption = function()
            return EquipTechConst.EQUIP_ENHANCE_SORT_OPTION
        end,

        getDepotFunc = "_GetEquipEnhanceDepot"
    },
    [UIConst.COMMON_ITEM_LIST_TYPE.EQUIP_TECH_EQUIP_ENHANCE_MATERIALS] = {
        infoProcessFunc = "processEquipEnhanceMaterial",
        filterTagGroupFunc = "generateConfig_EQUIP_ENHANCE_MATERIALS",
        getSortOption = function()
            return EquipTechConst.EQUIP_ENHANCE_MATERIALS_SORT_OPTION
        end,

        getDepotFunc = "_GetEquipEnhanceMaterialsDepot"
    },
}
CommonItemList = HL.Class('CommonItemList', UIWidgetBase)

CommonItemList.m_getItemCell = HL.Field(HL.Function)

CommonItemList.m_itemInfoList = HL.Field(HL.Table)

CommonItemList.m_filteredInfoList = HL.Field(HL.Table)

CommonItemList.m_selectedTags = HL.Field(HL.Table)

CommonItemList.m_curSelectIndex = HL.Field(HL.Number) << 0

CommonItemList.m_curSelectId = HL.Field(HL.Any) << 0

CommonItemList.m_filterTagGroups = HL.Field(HL.Table)

CommonItemList.m_arg = HL.Field(HL.Table)

CommonItemList.m_curListConfig = HL.Field(HL.Table)


CommonItemList.m_onClickItem = HL.Field(HL.Function)

CommonItemList.m_onFinishGraduallyShow = HL.Field(HL.Function)

CommonItemList.m_onLongPressItem = HL.Field(HL.Function)

CommonItemList.m_onPressItem = HL.Field(HL.Function)

CommonItemList.m_onReleaseItem = HL.Field(HL.Function)

CommonItemList.m_refreshItemAddOn = HL.Field(HL.Function)

CommonItemList.m_setItemSelected = HL.Field(HL.Function)

CommonItemList.m_getItemBtn = HL.Field(HL.Function)

CommonItemList.m_lastListType = HL.Field(HL.String) << ""



CommonItemList._OnFirstTimeInit = HL.Override() << function(self)
    self.m_getItemCell = UIUtils.genCachedCellFunction(self.view.itemList)
    self.view.itemList.onUpdateCell:AddListener(function(object, csIndex)
        self:_RefreshItemCell(object, LuaIndex(csIndex))
    end)
    self.view.itemList.onSelectedCell:AddListener(function(obj, csIndex)
        self:SetSelectedIndex(LuaIndex(csIndex), true)
    end)
    self.view.itemList.onGraduallyShowFinish:AddListener(function()
        if self.m_onFinishGraduallyShow then
            self.m_onFinishGraduallyShow()
        end
    end)
    self.view.itemList.getCurSelectedIndex = function()
        return CSIndex(self.m_curSelectIndex)
    end

    if self.m_arg.enableKeyboardNavi then
        self:BindInputPlayerAction("char_list_select_up", function()
            self.view.itemList:NavigateSelected(CS.UnityEngine.UI.NaviDirection.Up)
        end)

        self:BindInputPlayerAction("char_list_select_down", function()
            self.view.itemList:NavigateSelected(CS.UnityEngine.UI.NaviDirection.Down)
        end)

        self:BindInputPlayerAction("char_list_select_left", function()
            self.view.itemList:NavigateSelected(CS.UnityEngine.UI.NaviDirection.Left)
        end)

        self:BindInputPlayerAction("char_list_select_right", function()
            self.view.itemList:NavigateSelected(CS.UnityEngine.UI.NaviDirection.Right)
        end)
    end
end




































CommonItemList.InitCommonItemList = HL.Method(HL.Table) << function(self, arg)
    self.m_arg = arg

    self:_FirstTimeInit()

    self.m_curListConfig = LIST_CONFIG[arg.listType]

    self.m_onClickItem = arg.onClickItem
    self.m_onFinishGraduallyShow = arg.onFinishGraduallyShow
    self.m_onLongPressItem = arg.onLongPressItem
    self.m_onPressItem = arg.onPressItem
    self.m_onReleaseItem = arg.onReleaseItem
    self.m_refreshItemAddOn = arg.refreshItemAddOn
    self.m_setItemSelected = function(cell, selected)
        if arg.setItemSelected then
            arg.setItemSelected(cell, selected)
        else
            if cell and cell.item then
                self:SetSelectedAppearance(cell, selected and not DeviceInfo.usingController)
            end
        end

        if selected then
            local itemBtn = self.m_getItemBtn(cell)



            if not arg.suppressAutoNaviTarget and itemBtn and itemBtn ~= InputManagerInst.controllerNaviManager.curTarget then
                if self.view.scrollRect and self.view.scrollRect.naviGroup then
                    self:SetNaviTarget(itemBtn)
                else
                    self:SetNaviTarget(itemBtn)
                end
            end

            if arg.afterSetItemSelected then
                arg.afterSetItemSelected(cell, selected)
            end
        end
    end
    self.m_getItemBtn = arg.getItemBtn or function(cell)
        return cell.item.view.button
    end
    self.m_curSelectIndex = 0
    self.m_curSelectId = 0
    self.m_filteredInfoList = {}
    self.m_itemInfoList = {}

    if self.m_lastListType ~= arg.listType then
        self:_InitFilterNode()
        self:_InitSortNode()
    end
    self.m_lastListType = arg.listType

    self:Refresh(arg)
end

CommonItemList.PlayGraduallyShow = HL.Method(HL.Opt(HL.Number, HL.Boolean)) << function(self, selectIndex, realClick)
    selectIndex = selectIndex or self:_GetDefaultSelectIndex()
    self:_RefreshItemList(self.m_filteredInfoList, false, selectIndex, realClick)
end

CommonItemList.GetItemDepotCount = HL.Method().Return(HL.Number) << function(self)
    if not self.m_itemInfoList then
        return 0
    end

    return #self.m_itemInfoList
end

CommonItemList.GetFilteredItemDepotCount = HL.Method().Return(HL.Number) << function(self)
    if not self.m_filteredInfoList then
        return 0
    end

    return #self.m_filteredInfoList
end

CommonItemList.GetItemInfoByIndex = HL.Method(HL.Number).Return(HL.Opt(HL.Table)) << function(self, index)
    if not self.m_filteredInfoList then
        return
    end
    return self.m_filteredInfoList[index]
end

CommonItemList.GetItemInfoByIndexId = HL.Method(HL.Any).Return(HL.Opt(HL.Table)) << function(self, indexId)
    if not self.m_filteredInfoList then
        return
    end
    for _, itemInfo in pairs(self.m_filteredInfoList) do
        if itemInfo.indexId == indexId then
            return itemInfo
        end
    end
end


CommonItemList.Refresh = HL.Method(HL.Table) << function(self, arg)
    local skipGraduallyShow = arg.skipGraduallyShow == true
    local itemInfoList = self:_CollectItemInfoList()
    if not itemInfoList then
        return
    end

    local filteredList = itemInfoList
    if not self.m_curListConfig.hideFilter then
        filteredList = self:_ApplyFilter(itemInfoList, self.m_selectedTags)
    end
    if not self.m_curListConfig.hideSort then
        filteredList = self:_ApplySort(filteredList, self.view.sortNode:GetCurSortData(), self.view.sortNode.isIncremental)
    end

    self.m_itemInfoList = itemInfoList
    self.m_filteredInfoList = filteredList

    if arg.onlyRefreshData then
        return
    end

    local selectIndex = self:_GetDefaultSelectIndex()
    self:_RefreshItemList(filteredList, skipGraduallyShow, selectIndex, false)
end

CommonItemList.GetCurSelectedItem = HL.Method().Return(HL.Opt(HL.Any)) << function(self)
    local curGo = self.view.itemList:Get(CSIndex(self.m_curSelectIndex))
    if curGo then
        return curGo
    end
end

CommonItemList.GetCurSelectedItemCell = HL.Method().Return(HL.Any) << function(self)
    local curGo = self:GetCurSelectedItem()
    if curGo then
        return self.m_getItemCell(curGo)
    end
    return nil
end

CommonItemList.SetSelectedAppearance = HL.Method(HL.Any, HL.Boolean) << function(self, cell, selected)
    if not cell then
        return
    end
    cell.item.view.selectedBG.gameObject:SetActive(selected)
end

CommonItemList.IsAnyItemSelecting = HL.Method().Return(HL.Boolean) << function(self)
    return self.m_curSelectIndex > 0
end

CommonItemList.GetCurSelectIndex = HL.Method().Return(HL.Number) << function(self)
    return self.m_curSelectIndex
end

CommonItemList.RefreshCellById = HL.Method(HL.Any) << function(self, id)

    for index, filteredInfo in pairs(self.m_filteredInfoList) do
        if type(filteredInfo.indexId) == type(id) and filteredInfo.indexId == id then
            local curGo = self.view.itemList:Get(CSIndex(index))
            if curGo then
                self:_RefreshItemCell(curGo, index)
            end
        end
    end
end

CommonItemList.RefreshCellByIndex = HL.Method(HL.Number) << function(self, index)
    local curGo = self.view.itemList:Get(CSIndex(index))
    if curGo then
        self:_RefreshItemCell(curGo, index)
    end
end

CommonItemList.RefreshAllCells = HL.Method() << function(self)
    for index, itemInfo in pairs(self.m_filteredInfoList) do
        local curGo = self.view.itemList:Get(CSIndex(index))
        if curGo then
            self:_RefreshItemCell(curGo, index)
        end
    end
end

CommonItemList.RefreshAllCellsItemAddOn = HL.Method() << function(self)
    for index, itemInfo in pairs(self.m_filteredInfoList) do
        local curGo = self.view.itemList:Get(CSIndex(index))
        if curGo then
            local listCell = self.m_getItemCell(curGo)
            if self.m_refreshItemAddOn then
                self.m_refreshItemAddOn(listCell, itemInfo, index)
            end
        end
    end
end

CommonItemList.SetSelectedId = HL.Method(HL.Any, HL.Boolean, HL.Opt(HL.Boolean)) << function(self, id, realClick, noScroll)

    if not id then
        self:SetSelectedIndex(1, realClick, noScroll)
        return
    end

    local index = self:_GetIndexByIndexId(id)
    if index then
        self:SetSelectedIndex(index, realClick, noScroll)
        return
    end

    self:SetSelectedIndex(1, realClick, noScroll)
end

CommonItemList.SetSelectedIndex = HL.Method(HL.Number, HL.Boolean, HL.Opt(HL.Boolean)) << function(self, luaIndex, realClick, noScroll)
    if luaIndex == nil then
        return
    end

    if not noScroll and CSIndex(luaIndex) >= 0 then
        self.view.itemList:ScrollToIndex(CSIndex(luaIndex), true)
    end

    local curCell
    local curGo = self.view.itemList:Get(CSIndex(self.m_curSelectIndex))
    if curGo then
        curCell = self.m_getItemCell(curGo)
        if curCell then
            self.m_setItemSelected(curCell, false)
        end
    end

    local nextCell
    local nextGo = self.view.itemList:Get(CSIndex(luaIndex))
    if nextGo then
        nextCell = self.m_getItemCell(nextGo)
        if nextCell then
            self.m_setItemSelected(nextCell, true)
        end
    end

    local selectedInfo = self.m_filteredInfoList[luaIndex]
    if not selectedInfo then
        return
    end

    self.m_curSelectIndex = luaIndex
    self.m_curSelectId = selectedInfo and self.m_filteredInfoList[luaIndex].indexId or 0


    if UNITY_EDITOR or DEVELOPMENT_BUILD then
        logger.info(string.format("CommonItemList->选中物品TemplateId [   %s   ]", selectedInfo.id))
    end

    if self.m_onClickItem then
        self.m_onClickItem({
            itemInfo = selectedInfo,
            realClick = realClick,
            nextCell = nextCell,
            curCell = curCell
        })
    end
end

CommonItemList._GetIndexByIndexId = HL.Method(HL.Any).Return(HL.Opt(HL.Number)) << function(self, id)
    for index, filteredInfo in pairs(self.m_filteredInfoList) do
        if type(filteredInfo.indexId) == type(id) and  filteredInfo.indexId == id then
            return index
        end
    end
end

CommonItemList._GetIndexByItemId = HL.Method(HL.String).Return(HL.Opt(HL.Number)) << function(self, id)
    for index, filteredInfo in pairs(self.m_filteredInfoList) do
        if not string.isEmpty(filteredInfo.id) and filteredInfo.id == id then
            return index
        end
    end
end

CommonItemList._InitSortNode = HL.Method() << function(self)
    local listConfig = self.m_curListConfig
    self.view.sortNode.gameObject:SetActive(listConfig.hideSort ~= true)
    if listConfig.hideSort then
        return
    end

    local isIncremental = self.m_arg.curSortIsIncremental or false
    local curSortCSIndex = self.m_arg.curSortCSIndex
    local sortOption = listConfig.getSortOption() or {}
    self.view.sortNode:InitSortNode(sortOption, function(optData, isIncremental)
        local filteredList = self.m_filteredInfoList
        if not filteredList then
            return
        end
        local lastSelectedIndexId
        if self.m_curSelectIndex > 0 then
            local info = self.m_filteredInfoList[self.m_curSelectIndex]
            if info then
                lastSelectedIndexId = info.indexId
            end
        end

        filteredList = self:_ApplySort(filteredList, optData, isIncremental)

        if self.m_arg.keepSelectionOnSort and lastSelectedIndexId then
            local newSelectIndex = self:_GetIndexByIndexId(lastSelectedIndexId)
            if newSelectIndex then
                self:_RefreshItemList(filteredList, false, newSelectIndex, false)
            else
                self:_RefreshItemList(filteredList, false, self.m_curSelectIndex > 0 and 1 or 0, false)
            end
        else
            self:_RefreshItemList(filteredList, false, self.m_curSelectIndex > 0 and 1 or 0, false)
        end

        if #filteredList <= 0 then
            return
        end
    end, curSortCSIndex, isIncremental, true, self.view.filterBtn)
end

CommonItemList._InitFilterNode = HL.Method() << function(self)
    local listConfig = self.m_curListConfig
    local filterTagGroups = {}
    local filterTagGroupFunc = listConfig.filterTagGroupFunc
    local selectedTags = {}
    if filterTagGroupFunc and FilterUtils[filterTagGroupFunc] then
        filterTagGroups, selectedTags = FilterUtils[filterTagGroupFunc](self.m_arg.selectedTagArgs)
    end

    if self.m_arg.selectedTags then
        self.m_selectedTags = self.m_arg.selectedTags
    else
        self.m_selectedTags = selectedTags
    end
    self.m_filterTagGroups = filterTagGroups

    local showFilter = (filterTagGroups ~= nil) and (next(filterTagGroups) ~= nil) and (listConfig.hideFilter ~= true)
    self.view.filterBtn.gameObject:SetActive(showFilter)
    if not showFilter then
        return
    end


    local filterArgs = {
        tagGroups = filterTagGroups,
        selectedTags = self.m_selectedTags,
        onConfirm = function(tags)
            self:_OnFilterConfirm(tags)
        end,
        getResultCount = function(tags)
            return self:_OnFilterGetCount(tags)
        end,
        sortNodeWidget = self.view.sortNode,
    }

    if self.view.filterBtnWithText then
        self.view.filterBtn.gameObject:SetActive(false)
        self.view.filterBtnWithText.gameObject:SetActive(showFilter)
        filterArgs.sortNodeWidget = nil
        self.view.filterBtnWithText:InitFilterBtn(filterArgs)
    else
        self.view.filterBtn.gameObject:SetActive(showFilter)

        self.view.filterBtn:InitFilterBtn(filterArgs)
    end
end

CommonItemList._RefreshItemList = HL.Method(HL.Table, HL.Boolean, HL.Number, HL.Opt(HL.Boolean)) << function(self, filteredList, skipGraduallyShow, realIndex, realClick)
    local isEmpty = filteredList == nil or #filteredList == 0
    self.view.emptyNode.gameObject:SetActive(isEmpty)

    self.view.itemList:UpdateCount(#filteredList, CSIndex(realIndex), false, false, skipGraduallyShow == true, CS.Beyond.UI.UIScrollList.ScrollAlignType.Top)

    if realIndex > 0 then
        if realClick == nil then
            realClick = true
        end
        self:SetSelectedIndex(realIndex, realClick, true)
    end

    if isEmpty then
        if self.m_arg.onFilterNone then
            self.m_arg.onFilterNone()
        end
    end
end

CommonItemList._GetDefaultSelectIndex = HL.Method(HL.Opt(HL.Number)).Return(HL.Number) << function(self, forceSelectIndex)
    local defaultSelectIndex = -1
    if self.m_arg.defaultSelectedIndex ~= nil then
        defaultSelectIndex = self.m_arg.defaultSelectedIndex
    end

    if self.m_arg.selectedIndexId ~= nil then
        local tryIndex = self:_GetIndexByIndexId(self.m_arg.selectedIndexId)
        if tryIndex then
            defaultSelectIndex = tryIndex
        end
    end

    if not string.isEmpty(self.m_arg.selectedItemId) then
        local tryIndex = self:_GetIndexByItemId(self.m_arg.selectedItemId)
        if tryIndex then
            defaultSelectIndex = tryIndex
        end
    end

    if forceSelectIndex then
        defaultSelectIndex = forceSelectIndex
    end

    return defaultSelectIndex
end

CommonItemList._RefreshItemCell = HL.Method(HL.Userdata, HL.Number) << function(self, object, index)
    local listCell = self.m_getItemCell(object)
    local item = listCell.item
    local itemInfo = self.m_filteredInfoList[index]
    if itemInfo == nil then
        return
    end

    local count
    if itemInfo.itemInst ~= nil then
        count = 1
    elseif self.m_arg.itemCount_onlyBag then
        count = Utils.getBagItemCount(itemInfo.id)
    else
        count = itemInfo.itemInst ~= nil and 1 or Utils.getItemCount(itemInfo.id)
    end
    local instId
    if itemInfo.itemInst then
        instId = itemInfo.itemInst.instId
    end
    item:InitItem({
        id = itemInfo.itemCfg.id,
        instId = instId,
        count = count,
    }, true)
    if self.m_arg.enableControllerHoverTips ~= true then
        item:SetEnableHoverTips(not DeviceInfo.usingController)
    end

    local itemBtn = self.m_getItemBtn(listCell)
    if itemBtn then
        itemBtn.onClick:RemoveAllListeners()
        itemBtn.onClick:AddListener(function()
            self:SetSelectedIndex(index, true, true)
        end)

        itemBtn.onLongPress:RemoveAllListeners()
        itemBtn.onLongPress:AddListener(function()
            if self.m_onLongPressItem then
                self.m_onLongPressItem(itemInfo)
            end
        end)

        itemBtn.onPressStart:RemoveAllListeners()
        itemBtn.onPressStart:AddListener(function()
            if self.m_onPressItem then
                self.m_onPressItem(itemInfo)
            end
        end)

        itemBtn.onPressEnd:RemoveAllListeners()
        itemBtn.onPressEnd:AddListener(function()
            if self.m_onReleaseItem then
                self.m_onReleaseItem(itemInfo)
            end
        end)

        if self.m_arg.onItemIsNaviTargetChanged then
            itemBtn.onIsNaviTargetChanged = function(isTarget)
                self.m_arg.onItemIsNaviTargetChanged(listCell, itemInfo, isTarget)
            end
        end

        if self.m_arg.clickItemControllerHintText then
            itemBtn.customBindingViewLabelText = self.m_arg.clickItemControllerHintText
        end
    end

    if self.m_refreshItemAddOn then
        self.m_refreshItemAddOn(listCell, itemInfo, index)
    end

    self.m_setItemSelected(listCell, self.m_curSelectId == itemInfo.indexId)
end

CommonItemList._OnFilterConfirm = HL.Method(HL.Table) << function(self, tags)
    local itemInfoList = self.m_itemInfoList
    local filteredList = self:_ApplyFilter(itemInfoList, tags)
    if not self.m_curListConfig.hideSort then
        filteredList = self:_ApplySort(filteredList, self.view.sortNode:GetCurSortData(), self.view.sortNode.isIncremental)
    end

    self.m_selectedTags = tags
    self.m_filteredInfoList = filteredList


    self:_RefreshItemList(filteredList, false, 1, false)
end

CommonItemList._OnFilterGetCount = HL.Method(HL.Table).Return(HL.Number) << function(self, tags)
    local getCountFunc = self.m_curListConfig.getFilterResultCountFunc
    if getCountFunc and self[getCountFunc] then
        return self[getCountFunc](self, self.m_itemInfoList, tags)
    end
    local resultCount = 0
    if not tags or not next(tags) then
        return resultCount
    end
    for _, itemInfo in pairs(self.m_itemInfoList) do
        if FilterUtils.checkIfPassFilter(itemInfo, tags) then
            resultCount = resultCount + 1
        end
    end
    return resultCount
end

CommonItemList._ApplyFilter = HL.Method(HL.Table, HL.Table).Return(HL.Table) << function(self, itemInfoList, selectedTags)
    if self.m_curListConfig.applyFilterFunc and self[self.m_curListConfig.applyFilterFunc] then
        return self[self.m_curListConfig.applyFilterFunc](self, itemInfoList, selectedTags)
    end

    if self.m_curListConfig.hideFilter or not selectedTags or not next(selectedTags) then
        return itemInfoList
    end

    local filteredList = {}
    for _, itemInfo in pairs(itemInfoList) do
        if FilterUtils.checkIfPassFilter(itemInfo, selectedTags) then
            table.insert(filteredList, itemInfo)
        end
    end

    return filteredList
end

CommonItemList._ApplySort = HL.Method(HL.Table, HL.Table, HL.Boolean).Return(HL.Table) << function(self, itemInfoList, optData, isIncremental)
    if self.m_curListConfig.hideSort or not optData or not next(optData) then
        return itemInfoList
    end

    if isIncremental == nil then
        isIncremental = true
    end

    local sortKeys = optData.keys
    if isIncremental and optData.reverseKeys ~= nil then
        sortKeys = optData.reverseKeys
    end
    table.sort(itemInfoList, Utils.genSortFunction(sortKeys, isIncremental))
    return itemInfoList
end

CommonItemList._CollectItemInfoList = HL.Method().Return(HL.Table) << function(self)
    local listConfig = self.m_curListConfig
    local arg = self.m_arg
    local itemInfoList = {}

    local index = 1
    local depotFunc = listConfig.getDepotFunc
    local itemDepot = self[depotFunc](self, arg)
    if not itemDepot then
        return
    end

    for _, itemBundle in pairs(itemDepot) do
        local templateId = itemBundle.id
        local instId = itemBundle.instId or 0
        local instData = itemBundle.instData
        local _, itemCfg = Tables.itemTable:TryGetValue(templateId)

        if not itemCfg then
            logger.error("CommonItemList-> Can't get itemCfg for templateId: " .. templateId)
        else
            local infoProcessFunc = listConfig.infoProcessFunc
            local itemInfo = FilterUtils[infoProcessFunc](templateId, instId, arg)
            itemInfo.itemCfg = itemCfg
            itemInfo.itemInst = instData

            table.insert(itemInfoList, itemInfo)
            index = index + 1
        end
    end

    return itemInfoList
end



CommonItemList._GetWeaponGemDepot = HL.Method(HL.Table).Return(HL.Table) << function(self, arg)
    local filteredInstItems = {}
    local gemDepot = GameInstance.player.inventory.valuableDepots[GEnums.ItemValuableDepotType.WeaponGem]:GetOrFallback(Utils.getCurrentScope())

    local filter_rarity = arg.filter_rarity
    local exclusiveInstId = arg.exclusiveInstId

    for _, gemInst in cs_pairs(gemDepot.instItems) do
        local gemCfg = Tables.itemTable:GetValue(gemInst.id)
        if gemCfg then
            local passRarityFilter = ((not filter_rarity) or (gemCfg.rarity == filter_rarity)) and
                                      ((not exclusiveInstId) or (gemInst.instId ~= exclusiveInstId))
            if passRarityFilter then
                table.insert(filteredInstItems, gemInst)
            end
        end
    end

    return filteredInstItems
end

CommonItemList._GetWeaponGemMaterialDepot = HL.Method(HL.Table).Return(HL.Table) << function(self, arg)
    local filteredInstItems = {}
    local gemDepot = GameInstance.player.inventory.valuableDepots[GEnums.ItemValuableDepotType.WeaponGem]:GetOrFallback(Utils.getCurrentScope())

    local filter_rarity = arg.filter_rarity
    local exclusiveInstId = arg.exclusiveInstId

    for _, gemInst in cs_pairs(gemDepot.instItems) do
        local gemCfg = Tables.itemTable:GetValue(gemInst.id)
        if gemCfg then
            local passFilter = ((not filter_rarity) or (gemCfg.rarity == filter_rarity)) and
                ((not exclusiveInstId) or (gemInst.instId ~= exclusiveInstId))
            if passFilter then
                table.insert(filteredInstItems, gemInst)
            end
        end
    end

    return filteredInstItems
end

CommonItemList._GetWeaponUpgradeDepot = HL.Method(HL.Table).Return(HL.Table) << function(self, arg)
    local weaponDepot = self:_GetWeaponDepot(arg)


    for i = 1, Tables.characterConst.weaponExpItem.Count do
        local itemId = Tables.characterConst.weaponExpItem[CSIndex(i)]
        table.insert(weaponDepot, {
            id = itemId
        })
    end

    return weaponDepot
end

CommonItemList._GetWeaponPotentialDepot = HL.Method(HL.Table).Return(HL.Table) << function(self, arg)
    local weaponDepot = self:_GetWeaponDepot(arg)


    local weaponId = arg.filter_templateId
    local _, weaponData = Tables.weaponBasicTable:TryGetValue(weaponId)
    if weaponData then
        for _, itemId in pairs(weaponData.potentialUpItemList) do
            table.insert(weaponDepot, {
                id = itemId
            })
        end
    end

    return weaponDepot
end

CommonItemList._GetTacticalItemDepot = HL.Method(HL.Table).Return(HL.Table) << function(self, arg)
    local useItems = {}
    local filter_isFound = arg.filter_isFound

    for i, cfg in pairs(Tables.equipItemTable) do
        local passFoundFilter = (not filter_isFound) or GameInstance.player.inventory:IsItemFound(cfg.itemId)
        if passFoundFilter then
            table.insert(useItems, {
                id = cfg.itemId
            })
        end
    end
    return useItems
end

CommonItemList._GetEquipDepot = HL.Method(HL.Table).Return(HL.Table) << function(self, arg)
    local filteredInstItems = {}
    local filter_equipType = arg.filter_equipType

    local charInst
    local charInstId = arg.charInstId
    if charInstId then
        charInst = CharInfoUtils.getPlayerCharInfoByInstId(charInstId)
    end
    local equipDepot = GameInstance.player.inventory.valuableDepots[GEnums.ItemValuableDepotType.Equip]:GetOrFallback(Utils.getCurrentScope())
    if equipDepot then
        for _, itemBundle in cs_pairs(equipDepot.instItems) do
            local equipInst = itemBundle.instData
            local templateId = equipInst.templateId

            local _, itemCfg = Tables.itemTable:TryGetValue(templateId)
            if not itemCfg then
                logger.error(ELogChannel.Cfg, "EquipTemplateId: " .. templateId .. " not in equipBasicTable!!!")
                return filteredInstItems
            end

            local _, equipTemplateCfg = Tables.equipTable:TryGetValue(templateId)
            if not equipTemplateCfg then
                logger.error(ELogChannel.Cfg, "EquipTemplateId: " .. templateId .. " not in equipTable!!!")
                return filteredInstItems
            end

            local passEquipTypeFilter = (not filter_equipType) or (equipTemplateCfg.partType == filter_equipType)
            if passEquipTypeFilter then
                table.insert(filteredInstItems, itemBundle)
            end
        end
    end

    return filteredInstItems
end

CommonItemList._GetWeaponDepot = HL.Method(HL.Table).Return(HL.Table) << function(self, arg)
    local filteredInstItems = {}

    local filter_weaponType = arg.filter_weaponType
    local filter_not_equipped = arg.filter_not_equipped
    local filter_templateId = arg.filter_templateId
    local filter_not_instId = arg.filter_not_instId
    local filter_not_maxPotential = arg.filter_not_maxPotential

    local res, weaponInstDict = GameInstance.player.inventory:TryGetAllWeaponInstItems(Utils.getCurrentScope())
    if res then
        for _, itemBundle in cs_pairs(weaponInstDict) do
            local _, weaponCfg = Tables.weaponBasicTable:TryGetValue(itemBundle.id)
            if weaponCfg then
                local passWeaponTypeFilter = (not filter_weaponType) or (weaponCfg.weaponType == filter_weaponType)
                local passEquippedFilter = (not filter_not_equipped) or (itemBundle.instData.equippedCharServerId == 0)
                local passPotentialFilter = (not filter_not_maxPotential) or (itemBundle.instData.refineLv < UIConst.CHAR_MAX_POTENTIAL)
                local passInstIdFilter = (not filter_not_instId) or (itemBundle.instId ~= filter_not_instId)
                local passTemplateIdFilter = (not filter_templateId) or (itemBundle.id == filter_templateId)

                if passWeaponTypeFilter
                    and passEquippedFilter
                    and passInstIdFilter
                    and passTemplateIdFilter
                    and passPotentialFilter
                then
                    table.insert(filteredInstItems, itemBundle)
                end
            end
        end
    end
    return filteredInstItems
end

CommonItemList._GetEquipEnhanceDepot = HL.Method(HL.Table).Return(HL.Table) << function(self, args)
    return EquipTechUtils.getEquipEnhanceItemList(args.filter_equipType)
end

CommonItemList._GetEquipEnhanceMaterialsDepot = HL.Method(HL.Table).Return(HL.Table) << function(self, args)
    return EquipTechUtils.getEquipEnhanceMaterialsItemList(args.filter_equipType, args.attrShowInfo, args.equipInstId)
end





CommonItemList._ApplyFilterGem = HL.Method(HL.Table, HL.Table).Return(HL.Table) << function(self, itemInfoList, tagInfoList)
    local filteredList
    if not tagInfoList or not next(tagInfoList) then
        filteredList = itemInfoList
    else
        filteredList = {}
        for _, itemInfo in pairs(itemInfoList) do
            if FilterUtils.checkIfPassFilter(itemInfo, tagInfoList) then
                table.insert(filteredList, itemInfo)
            end
        end
    end

    local sortKeys = self.m_arg.sortKeys or UIConst.WEAPON_GEM_SORT_OPTION[1].keys
    table.sort(filteredList, Utils.genSortFunction(sortKeys, false))
    return filteredList
end



HL.Commit(CommonItemList)
return CommonItemList
