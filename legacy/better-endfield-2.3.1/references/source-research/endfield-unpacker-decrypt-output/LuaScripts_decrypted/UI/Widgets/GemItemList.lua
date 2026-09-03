local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')
local LIST_CONFIG = {
    [GEnums.ItemType.WeaponGem] = {
        filterTagGroupFunc = "generateConfig_WEAPON_EXHIBIT_GEM",
        infoProcessFunc = "processWeaponGem",
        sortOption = UIConst.WEAPON_GEM_SORT_OPTION,
        getDepotFunc = "_GetWeaponGemDepot"
    },
}
GemItemList = HL.Class('GemItemList', UIWidgetBase)

GemItemList.m_getItemCell = HL.Field(HL.Function)

GemItemList.m_itemInfoList = HL.Field(HL.Table)

GemItemList.m_filteredInfoList = HL.Field(HL.Table)

GemItemList.m_selectedTags = HL.Field(HL.Table)

GemItemList.m_curSelectIndex = HL.Field(HL.Number) << 0

GemItemList.m_curSelectId = HL.Field(HL.Any) << 0

GemItemList.m_filterTagGroups = HL.Field(HL.Table)


GemItemList.m_onClickItem = HL.Field(HL.Function)

GemItemList.m_onLongPressItem = HL.Field(HL.Function)

GemItemList.m_refreshItemAddOn = HL.Field(HL.Function)

GemItemList.m_isItemSelected = HL.Field(HL.Function)



GemItemList._OnFirstTimeInit = HL.Override() << function(self)

    self.m_getItemCell = UIUtils.genCachedCellFunction(self.view.itemList)
    self.view.itemList.onUpdateCell:AddListener(function(object, csIndex)
        self:_RefreshItemCell(object, LuaIndex(csIndex))
    end)

    self.view.filterBtn.normal.onClick:AddListener(function()
        self.view.commonFilterList.gameObject:SetActive(true)
    end)
    self.view.filterBtn.haveSelected.onClick:AddListener(function()
        self.view.commonFilterList.gameObject:SetActive(true)
    end)
end

GemItemList.InitGemItemList = HL.Method(HL.Table) << function(self, arg)
    self:_FirstTimeInit()

    local listConfig = LIST_CONFIG[arg.itemType]
    local skipGraduallyShow = arg.skipGraduallyShow

    self.m_onClickItem = arg.onClickItem
    self.m_onLongPressItem = arg.onLongPressItem
    self.m_refreshItemAddOn = arg.refreshItemAddOn
    self.m_isItemSelected = arg.isItemSelected
    self.m_curSelectIndex = 0
    self.m_curSelectId = 0

    self:_InitSortNode(listConfig)
    self:_InitFilterNode(listConfig)

    local itemInfoList = self:_CollectItemInfoList(listConfig)
    if not itemInfoList then
        return
    end

    local filteredList = self:_ApplyFilter(itemInfoList, self.m_selectedTags)
    filteredList = self:_ApplySort(filteredList, self.view.sortNode:GetCurSortData(), self.view.sortNode.isIncremental)

    self.m_itemInfoList = itemInfoList
    self.m_filteredInfoList = filteredList

    self:_RefreshItemList(filteredList, skipGraduallyShow)
end

GemItemList.SetSelectedId = HL.Method(HL.Opt(HL.Any)) << function(self, id)
    if not id then
        self:SetSelectedIndex(1)
        return
    end

    for index, filteredInfo in pairs(self.m_filteredInfoList) do
        if type(filteredInfo.indexId) == type(id) and  filteredInfo.indexId == id then
            self:SetSelectedIndex(index)
            return
        end
    end
    self:SetSelectedIndex(1)
end

GemItemList.SetSelectedIndex = HL.Method(HL.Opt(HL.Number)) << function(self, luaIndex)
    if luaIndex == nil then
        return
    end

    if CSIndex(luaIndex) >= 0 then
        self.view.itemList:ScrollToIndex(CSIndex(luaIndex))
    end

    local curGo = self.view.itemList:Get(CSIndex(self.m_curSelectIndex))
    if curGo then
        local curCell = self.m_getItemCell(curGo)
        if curCell then
            curCell.itemBig.view.selectedBG.gameObject:SetActive(false)
        end
    end

    local nextGo = self.view.itemList:Get(CSIndex(luaIndex))
    if nextGo then
        local nextCell = self.m_getItemCell(nextGo)
        if nextCell then
            nextCell.itemBig.view.selectedBG.gameObject:SetActive(true)
        end
    end

    local selectedInfo = self.m_filteredInfoList[luaIndex]
    self.m_curSelectIndex = luaIndex
    self.m_curSelectId = selectedInfo and self.m_filteredInfoList[luaIndex].indexId or 0

    if self.m_onClickItem then
        self.m_onClickItem(selectedInfo)
    end
end

GemItemList._InitSortNode = HL.Method(HL.Table) << function(self, listConfig)
    local sortOption = listConfig.sortOption or {}
    self.view.sortNode:InitSortNode(sortOption, function(optData, isIncremental)
        local filteredList = self.m_filteredInfoList
        if not filteredList then
            return
        end

        filteredList = self:_ApplySort(filteredList, optData, isIncremental)

        self.view.emptyNode.gameObject:SetActive(#filteredList == 0)
        self.view.itemList:UpdateCount(#filteredList, false, false, false, false)
        self:SetSelectedId(self.m_curSelectId)
    end)
end

GemItemList._InitFilterNode = HL.Method(HL.Table) << function(self, listConfig)
    local filterTagGroups = {}
    local filterTagGroupFunc = listConfig.filterTagGroupFunc
    if filterTagGroupFunc and FilterUtils[filterTagGroupFunc] then
        filterTagGroups = FilterUtils[filterTagGroupFunc]() or {}
    end

    self.m_selectedTags = {}
    self.m_filterTagGroups = filterTagGroups

    local hasFilter = filterTagGroups and next(filterTagGroups)
    self.view.filterBtn.gameObject:SetActive(hasFilter)
    self.view.commonFilterList:InitFilterListWithTagGroups(filterTagGroups, function(tags)
        local itemInfoList = self.m_itemInfoList
        local filteredList = self:_ApplyFilter(itemInfoList, tags)
        filteredList = self:_ApplySort(filteredList, self.view.sortNode:GetCurSortData(), self.view.sortNode.isIncremental)

        self.m_selectedTags = tags
        self.m_filteredInfoList = filteredList

        self:_RefreshItemList(filteredList, false)
        self:SetSelectedId(self.m_curSelectId)
        self.view.commonFilterList.gameObject:SetActive(false)
    end, self.m_selectedTags)
end

GemItemList._RefreshItemList = HL.Method(HL.Table, HL.Opt(HL.Boolean)) << function(self, filteredList, skipGraduallyShow)
    self.view.emptyNode.gameObject:SetActive(#filteredList == 0)
    self.view.itemList:UpdateCount(#filteredList, false, false, false, skipGraduallyShow == true)
end

GemItemList._RefreshItemCell = HL.Method(HL.Userdata, HL.Number) << function(self, object, index)
    local itemCell = self.m_getItemCell(object)
    local itemInfo = self.m_filteredInfoList[index]

    itemCell.itemBig:InitItem({
        id = itemInfo.itemCfg.id,
        instId = itemInfo.itemInst.instId,
        count = 1,
    }, true)

    itemCell.itemBig.view.button.onClick:RemoveAllListeners()
    itemCell.itemBig.view.button.onClick:AddListener(function()
        self:SetSelectedIndex(index)
    end)

    itemCell.itemBig.view.button.onLongPress:RemoveAllListeners()
    itemCell.itemBig.view.button.onLongPress:AddListener(function()
        if self.m_onLongPressItem then
            self.m_onLongPressItem(itemInfo)
        end
    end)

    if self.m_refreshItemAddOn then
        self.m_refreshItemAddOn(itemCell, itemInfo)
    end

    if self.m_isItemSelected then
        itemCell.itemBig.view.selectedBG.gameObject:SetActive(self.m_isItemSelected(itemCell, itemInfo))
    end
end

GemItemList._ApplyFilter = HL.Method(HL.Table, HL.Table).Return(HL.Table) << function(self, itemInfoList, selectedTags)
    if not selectedTags or not next(selectedTags) then
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

GemItemList._ApplySort = HL.Method(HL.Table, HL.Table, HL.Boolean).Return(HL.Table) << function(self, itemInfoList, optData, isIncremental)
    if not optData or not next(optData) then
        return itemInfoList
    end

    if isIncremental == nil then
        isIncremental = true
    end
    table.sort(itemInfoList, Utils.genSortFunction(optData.keys, isIncremental))
    return itemInfoList
end

GemItemList._CollectItemInfoList = HL.Method(HL.Table).Return(HL.Table) << function(self, listConfig)
    local itemInfoList = {}

    local index = 1
    local listItems
    local depotFunc = listConfig.getDepotFunc
    listItems = self[depotFunc](self)
    if not listItems then
        return
    end

    for _, itemBundle in pairs(listItems) do
        local templateId = itemBundle.id
        local instId = itemBundle.instId or 0
        local instData = itemBundle.instData
        local _, itemCfg = Tables.itemTable:TryGetValue(templateId)

        if not itemCfg then
            logger.error("GemItemList-> Can't get itemCfg for templateId: " .. templateId)
        else
            local infoProcessFunc = listConfig.infoProcessFunc
            local itemInfo = FilterUtils[infoProcessFunc](templateId, instId)

            itemInfo.itemCfg = itemCfg
            itemInfo.itemInst = instData

            table.insert(itemInfoList, itemInfo)
            index = index + 1
        end
    end

    return itemInfoList
end

GemItemList._GetWeaponGemDepot = HL.Method().Return(HL.Userdata) << function(self)
    local gemDepot = GameInstance.player.inventory.valuableDepots[GEnums.ItemValuableDepotType.WeaponGem]:GetOrFallback(Utils.getCurrentScope())
    if not gemDepot then
        return
    end

    return gemDepot.instItems
end


HL.Commit(GemItemList)
return GemItemList

