local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')
local LIST_CONFIG = {
    [UIConst.COMMON_ITEM_LIST_TYPE.CHAR_INFO_WEAPON] = {
        infoProcessFunc = "processWeapon",
        sortOption = UIConst.WEAPON_SORT_OPTION,
        getDepotFunc = "_GetWeaponDepot",
        filterTagGroupFunc = "generateConfig_CHAR_INFO_WEAPON",
    },
}
WeaponPosterScrollList = HL.Class('WeaponPosterScrollList', UIWidgetBase)

WeaponPosterScrollList.m_getItemCell = HL.Field(HL.Function)

WeaponPosterScrollList.m_itemInfoList = HL.Field(HL.Table)

WeaponPosterScrollList.m_filteredInfoList = HL.Field(HL.Table)

WeaponPosterScrollList.m_selectedTags = HL.Field(HL.Table)

WeaponPosterScrollList.m_filterTagGroups = HL.Field(HL.Table)

WeaponPosterScrollList.m_arg = HL.Field(HL.Table)

WeaponPosterScrollList.m_curListConfig = HL.Field(HL.Table)

WeaponPosterScrollList.m_curSelectIndex2ItemInfo = HL.Field(HL.Table)

WeaponPosterScrollList.m_curSelectItemInfos = HL.Field(HL.Table)

WeaponPosterScrollList.m_sortIsIncremental = HL.Field(HL.Boolean) << false

WeaponPosterScrollList.m_sortOptData = HL.Field(HL.Table)



WeaponPosterScrollList.m_onClickItem = HL.Field(HL.Function)

WeaponPosterScrollList.m_setItemSelected = HL.Field(HL.Function)

WeaponPosterScrollList.m_getItemBtn = HL.Field(HL.Function)

WeaponPosterScrollList.m_lastListType = HL.Field(HL.String) << ""



WeaponPosterScrollList._OnFirstTimeInit = HL.Override() << function(self)
    self.m_getItemCell = UIUtils.genCachedCellFunction(self.view.scrollList)
    self.view.scrollList.onUpdateCell:AddListener(function(object, csIndex)
        self:_RefreshItemCell(object, LuaIndex(csIndex))
    end)
    self.view.scrollList.onSelectedCell:AddListener(function(obj, csIndex)
        self:SetSelectedIndex(LuaIndex(csIndex), true)
    end)

    self:BindInputPlayerAction("char_list_select_up", function()
        self.view.scrollList:NavigateSelected(CS.UnityEngine.UI.NaviDirection.Up)
    end)

    self:BindInputPlayerAction("char_list_select_down", function()
        self.view.scrollList:NavigateSelected(CS.UnityEngine.UI.NaviDirection.Down)
    end)

    self:BindInputPlayerAction("char_list_select_left", function()
        self.view.scrollList:NavigateSelected(CS.UnityEngine.UI.NaviDirection.Left)
    end)

    self:BindInputPlayerAction("char_list_select_right", function()
        self.view.scrollList:NavigateSelected(CS.UnityEngine.UI.NaviDirection.Right)
    end)
end



























WeaponPosterScrollList.InitWeaponPosterScrollList = HL.Method(HL.Table) << function(self, arg)
    self:_FirstTimeInit()

    self.m_arg = arg

    self.m_curListConfig = LIST_CONFIG[arg.listType]

    self.m_onClickItem = arg.onClickItem

    self.m_setItemSelected = function(cell, selected)
        if selected then
            local itemBtn = self.m_getItemBtn(cell)
            if itemBtn and itemBtn ~= InputManagerInst.controllerNaviManager.curTarget then
                self:SetNaviTarget(itemBtn)
            end
        else
            cell:SetSelectIndex()
        end
        self:RefreshSelectItemsIndex()
    end
    self.m_getItemBtn = arg.getItemBtn or function(cell)
        return cell.view.item.view.button
    end
    self.m_filteredInfoList = {}
    self.m_itemInfoList = {}
    self.m_curSelectIndex2ItemInfo = {}
    self.m_curSelectItemInfos = self.m_curSelectItemInfos or {}
    if self.m_lastListType ~= arg.listType then
        self:_InitFilterNode()
        self:_InitSortNode()
    end
    self.m_lastListType = arg.listType
    self:Refresh(arg)
end

WeaponPosterScrollList.GetItemDepotCount = HL.Method().Return(HL.Number) << function(self)
    if not self.m_itemInfoList then
        return 0
    end

    return #self.m_itemInfoList
end

WeaponPosterScrollList.GetItemInfoByIndex = HL.Method(HL.Number).Return(HL.Opt(HL.Table)) << function(self, index)
    if not self.m_filteredInfoList then
        return
    end
    return self.m_filteredInfoList[index]
end

WeaponPosterScrollList.GetItemInfoByIndexId = HL.Method(HL.Any).Return(HL.Opt(HL.Table)) << function(self, indexId)
    if not self.m_filteredInfoList then
        return
    end
    for _, itemInfo in pairs(self.m_filteredInfoList) do
        if itemInfo.indexId == indexId then
            return itemInfo
        end
    end
end


WeaponPosterScrollList.Refresh = HL.Method(HL.Table) << function(self, arg)
    local skipGraduallyShow = arg.skipGraduallyShow == true
    local itemInfoList = self:_CollectItemInfoList()
    if not itemInfoList then
        return
    end
    self.m_curSelectItemInfos = arg.defaultSelected
    local filteredList = itemInfoList
    if not self.m_curListConfig.hideSort then
        filteredList = self:_ApplySort(filteredList, self.view.sortNode:GetCurSortData(), self.view.sortNode.isIncremental)
    end
    if not self.m_curListConfig.hideFilter then
        filteredList = self:_ApplyFilter(itemInfoList, self.m_selectedTags)
    end
    self.m_itemInfoList = itemInfoList
    self.m_filteredInfoList = filteredList

    if arg.onlyRefreshData then
        return
    end
    self:_RefreshItemList(filteredList, skipGraduallyShow ,0,false, arg.defaultSelected)
end

WeaponPosterScrollList.RefreshSelectItemsIndex = HL.Method() << function(self)
    for index, info in pairs(self.m_curSelectIndex2ItemInfo) do
        for selectIndex, itemInfo in ipairs(self.m_curSelectItemInfos) do
            if info.instId == itemInfo.instId then
                local curGo = self.view.scrollList:Get(CSIndex(index))
                if curGo then
                    local listCell = self.m_getItemCell(curGo)
                    listCell:SetSelectIndex(selectIndex)
                end
                break
            end
        end
    end
end


WeaponPosterScrollList.RefreshCellById = HL.Method(HL.Any) << function(self, id)
    for index, filteredInfo in pairs(self.m_filteredInfoList) do
        if type(filteredInfo.indexId) == type(id) and filteredInfo.indexId == id then
            local curGo = self.view.scrollList:Get(CSIndex(index))
            if curGo then
                self:_RefreshItemCell(curGo, index)
            end
        end
    end
end

WeaponPosterScrollList.RefreshCellByIndex = HL.Method(HL.Number) << function(self, index)
    local curGo = self.view.scrollList:Get(CSIndex(index))
    if curGo then
        self:_RefreshItemCell(curGo, index)
    end
end

WeaponPosterScrollList.RefreshAllCells = HL.Method() << function(self)
    for index, _ in pairs(self.m_filteredInfoList) do
        local curGo = self.view.scrollList:Get(CSIndex(index))
        if curGo then
            self:_RefreshItemCell(curGo, index)
        end
    end
end

WeaponPosterScrollList.SetSelectedIndex = HL.Method(HL.Number, HL.Boolean, HL.Opt(HL.Boolean))
    << function(self, luaIndex, realClick, noScroll)
    if luaIndex == nil then
        return
    end

    local selectInfo = self.m_curSelectIndex2ItemInfo[luaIndex]

    if not selectInfo and #self.m_curSelectItemInfos >= self.m_arg.select_num then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_WEAPON_WALL_LIMIT_MAX)
        return
    end

    if not noScroll and CSIndex(luaIndex) >= 0 then
        self.view.scrollList:ScrollToIndex(CSIndex(luaIndex), true)
    end
    if luaIndex and selectInfo and selectInfo.indexId then
        local selectItemInfos = {}
        for i, itemInfo in ipairs(self.m_curSelectItemInfos) do
            if itemInfo.indexId ~= selectInfo.indexId then
                table.insert(selectItemInfos, itemInfo)
            end
        end
        self.m_curSelectItemInfos = selectItemInfos
        self.m_curSelectIndex2ItemInfo[luaIndex] = nil

        local curCell
        local curGo = self.view.scrollList:Get(CSIndex(luaIndex))
        if curGo then
            curCell = self.m_getItemCell(curGo)
            if curCell then
                self.m_setItemSelected(curCell, false)
            end
        end
    elseif luaIndex then
        table.insert(self.m_curSelectItemInfos, self.m_filteredInfoList[luaIndex])
        self.m_curSelectIndex2ItemInfo[luaIndex] = self.m_filteredInfoList[luaIndex]
        local curGo = self.view.scrollList:Get(CSIndex(luaIndex))
        local curCell
        if curGo then
            curCell = self.m_getItemCell(curGo)
            if curCell then
                self.m_setItemSelected(curCell, true)
            end
        end
    end

    if UNITY_EDITOR or DEVELOPMENT_BUILD then
        logger.info(string.format("WeaponPosterScrollList->选中物品TemplateId [   %s   ]", self.m_filteredInfoList[luaIndex].id))
    end

    if self.m_onClickItem then
        self.m_onClickItem(self.m_curSelectItemInfos)
    end
end

WeaponPosterScrollList._GetIndexByIndexId = HL.Method(HL.Any).Return(HL.Opt(HL.Number)) << function(self, id)
    for index, filteredInfo in pairs(self.m_filteredInfoList) do
        if type(filteredInfo.indexId) == type(id) and  filteredInfo.indexId == id then
            return index
        end
    end
end

WeaponPosterScrollList._InitSortNode = HL.Method() << function(self)
    local listConfig = self.m_curListConfig
    self.view.sortNode.gameObject:SetActive(listConfig.hideSort ~= true)
    if listConfig.hideSort then
        return
    end

    local sortOption = listConfig.sortOption or {}
    self.view.sortNode:InitSortNode(sortOption, function(optData, isIncremental)
        local filteredList = self.m_filteredInfoList
        if not filteredList then
            return
        end
        filteredList = self:_ApplySort(filteredList, optData, isIncremental)
        self:_RefreshItemList(filteredList, false, 0, false)

        if #filteredList <= 0 then
            return
        end
    end, nil, false, true, self.view.filterBtn)
end

WeaponPosterScrollList._InitFilterNode = HL.Method() << function(self)
    local listConfig = self.m_curListConfig
    local filterTagGroups = {}
    local filterTagGroupFunc = listConfig.filterTagGroupFunc
    if filterTagGroupFunc and FilterUtils[filterTagGroupFunc] then
        filterTagGroups, self.m_selectedTags = FilterUtils[filterTagGroupFunc](self.m_arg.selectedTagArgs)
    end

    self.m_selectedTags = self.m_selectedTags or {}
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
    self.view.filterBtn.gameObject:SetActive(showFilter)
    self.view.filterBtn:InitFilterBtn(filterArgs)
end

WeaponPosterScrollList._RefreshItemList = HL.Method(HL.Table, HL.Boolean, HL.Number, HL.Opt(HL.Boolean, HL.Table))
    << function(self, filteredList, skipGraduallyShow, realIndex, realClick, selects)
    local isEmpty = filteredList == nil or #filteredList == 0
    self.view.emptyNode.gameObject:SetActive(isEmpty)
    self.view.scrollList:UpdateCount(#filteredList, CSIndex(realIndex), false, false, skipGraduallyShow == true, CS.Beyond.UI.UIScrollList.ScrollAlignType.Top)
    self.view.scrollList:SetTop()
    if isEmpty then
        if self.m_arg.onFilterNone then
            self.m_arg.onFilterNone()
        end
    end
    if selects then
        self:ShowSelectItems(selects)
    end
end

WeaponPosterScrollList._RefreshItemCell = HL.Method(HL.Userdata, HL.Number) << function(self, object, index)
    local listCell = self.m_getItemCell(object)
    local itemInfo = self.m_filteredInfoList[index]
    if itemInfo == nil then
        return
    end

    local cellArgs = {
        itemInfo = itemInfo,
        enableControllerHoverTips = self.m_arg.enableControllerHoverTips,
        selectIndex = self.m_curSelectIndex2ItemInfo[index] and index
    }
    listCell:InitSpaceShipCharPosterWeaponCell(cellArgs)

    local itemBtn = self.m_getItemBtn(listCell)
    if itemBtn then
        itemBtn.onClick:RemoveAllListeners()
        itemBtn.onClick:AddListener(function()
            self:SetSelectedIndex(index, true, true)
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
    local selected = index == 1
    self.m_setItemSelected(listCell, selected)
end

WeaponPosterScrollList._OnFilterConfirm = HL.Method(HL.Table) << function(self, tags)
    local itemInfoList = self.m_itemInfoList
    local filteredList = self:_ApplyFilter(itemInfoList, tags)
    if not self.m_curListConfig.hideSort then
        filteredList = self:_ApplySort(filteredList, self.view.sortNode:GetCurSortData(), self.view.sortNode.isIncremental)
    end

    self.m_selectedTags = tags
    self.m_filteredInfoList = filteredList

    self:_RefreshItemList(filteredList, false, 1, false)
end

WeaponPosterScrollList._OnFilterGetCount = HL.Method(HL.Table).Return(HL.Number) << function(self, tags)
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

WeaponPosterScrollList._ApplyFilter = HL.Method(HL.Table, HL.Table).Return(HL.Table) << function(self, itemInfoList, selectedTags)
    local filteredList = {}

    local tempInstList = {}
    for i, v in pairs(self.m_curSelectItemInfos) do
        tempInstList[v.indexId] = true
    end

    if self.m_curListConfig.applyFilterFunc and self[self.m_curListConfig.applyFilterFunc] then
        filteredList = self[self.m_curListConfig.applyFilterFunc](self, itemInfoList, selectedTags)
    elseif self.m_curListConfig.hideFilter or not selectedTags or not next(selectedTags) then
        filteredList = itemInfoList
    else
        for _, itemInfo in pairs(itemInfoList) do
            if FilterUtils.checkIfPassFilter(itemInfo, selectedTags) or tempInstList[itemInfo.indexId] then
                table.insert(filteredList, itemInfo)
            end
        end
    end
    self:_SortData(itemInfoList, self.m_sortOptData, self.m_sortIsIncremental)

    self.m_curSelectIndex2ItemInfo = {}
    for i, info in pairs(filteredList) do
        if tempInstList[info.indexId] then
            self.m_curSelectIndex2ItemInfo[i] = info
        end
    end

    return filteredList
end

WeaponPosterScrollList._ApplySort = HL.Method(HL.Table, HL.Table, HL.Boolean).Return(HL.Table) << function(self, itemInfoList, optData, isIncremental)
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
    local tempInstList = {}
    for i, v in ipairs(self.m_curSelectItemInfos) do
        tempInstList[v.indexId] = i
    end
    for _, itemInfo in pairs(itemInfoList) do
        if tempInstList[itemInfo.instId] then
            itemInfo.selectSlot = -tempInstList[itemInfo.instId]
            itemInfo.selectSlotReverse = tempInstList[itemInfo.instId]
        else
            itemInfo.selectSlot = math.mininteger
            itemInfo.selectSlotReverse = math.maxinteger
        end
    end

    self:_SortData(itemInfoList, sortKeys, isIncremental)
    self.m_curSelectIndex2ItemInfo = {}
    for i, info in pairs(itemInfoList) do
        if tempInstList[info.indexId] then
            self.m_curSelectIndex2ItemInfo[i] = info
        end
    end
    return itemInfoList
end

WeaponPosterScrollList._SortData = HL.Method(HL.Table, HL.Table, HL.Boolean)
    << function(self, itemList, keys, isIncremental)
    if itemList then
        table.sort(itemList, Utils.genSortFunction(keys, isIncremental))
        self.m_sortIsIncremental = isIncremental
        self.m_sortOptData = keys
    end
end

WeaponPosterScrollList._CollectItemInfoList = HL.Method().Return(HL.Table) << function(self)
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
            logger.error("WeaponPosterScrollList-> Can't get itemCfg for templateId: " .. templateId)
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

WeaponPosterScrollList.ShowSelectItems = HL.Method(HL.Table) << function(self, selectIdList)
    local selectedInstIds = {}
    for _, itemInfo in ipairs(selectIdList) do
        selectedInstIds[itemInfo.instId] = true
    end

    for index, info in pairs(self.m_curSelectIndex2ItemInfo) do
        local curGo = self.view.scrollList:Get(CSIndex(index))
        if curGo then
            local listCell = self.m_getItemCell(curGo)
            listCell:SetSelectIndex()
        end
    end

    self.m_curSelectItemInfos = selectIdList

    local indexMap = {}
    local filteredInfoLookup = {}
    for _, v in ipairs(self.m_filteredInfoList) do
        filteredInfoLookup[v.indexId] = v
    end

    for i, info in ipairs(selectIdList) do
        local filteredInfo = filteredInfoLookup[info.indexId]
        if filteredInfo then
            for index, v in ipairs(self.m_filteredInfoList) do
                if v == filteredInfo then
                    indexMap[index] = info
                    break
                end
            end
        end
    end

    self.m_curSelectIndex2ItemInfo = indexMap
    self:RefreshSelectItemsIndex()
end



WeaponPosterScrollList._GetWeaponDepot = HL.Method(HL.Table).Return(HL.Table) << function(self, arg)
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

HL.Commit(WeaponPosterScrollList)
return WeaponPosterScrollList
