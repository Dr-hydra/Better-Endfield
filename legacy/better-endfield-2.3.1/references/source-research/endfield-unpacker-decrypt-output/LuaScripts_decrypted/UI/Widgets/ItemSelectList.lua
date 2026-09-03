local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

ItemSelectList = HL.Class('ItemSelectList', UIWidgetBase)

ItemSelectList.m_sortOptions = HL.Field(HL.Table)

ItemSelectList.items = HL.Field(HL.Table)

ItemSelectList.getItemCell = HL.Field(HL.Function)

ItemSelectList.m_onClickItem = HL.Field(HL.Function)

ItemSelectList.m_selectedItems = HL.Field(HL.Table)

ItemSelectList.m_onUnlockItem = HL.Field(HL.Function)


ItemSelectList._OnFirstTimeInit = HL.Override() << function(self)
    self.getItemCell = UIUtils.genCachedCellFunction(self.view.itemList)
    self.view.itemList.onUpdateCell:AddListener(function(object, csIndex)
        self:_OnUpdateItemList(object, LuaIndex(csIndex))
    end)

    self.view.mask.onClick:AddListener(function()
        self.gameObject:SetActiveIfNecessary(false)
    end)

    self.m_sortOptions = {
        {
            name = Language.LUA_FAC_CRAFT_SORT_1,
            keys = {"notSelected", "count", "sortId1", "sortId2", "id", "instId"},
        },
        {
            name = Language.LUA_FAC_CRAFT_SORT_2,
            keys = {"notSelected", "count", "rarity", "sortId1", "sortId2", "id", "instId"},
        },
    }

    self:RegisterMessage(MessageConst.ON_ITEM_LOCKED_STATE_CHANGED, function(arg)
        self:_OnItemLockedStateChanged(arg)
    end)
end

ItemSelectList.InitItemSelectList = HL.Method(
    HL.Table, HL.Table, HL.Function, HL.Opt(HL.String, HL.Function)) << function(
    self, itemIds, selectedItems, onClickItem, title, onUnlockItem)
    self:_FirstTimeInit()

    self.m_onClickItem = onClickItem

    self.m_selectedItems = selectedItems

    self.m_onUnlockItem = onUnlockItem

    if title then
        self.view.titleTxt:SetAndResolveTextStyle(title)
    else
        self.view.titleTxt.text = Language.LUA_SELECT_ITEM_LIST_TITLE
    end

    local items = {}
    for i = 1, #itemIds do
        local itemId = itemIds[i]
        local itemData = Tables.itemTable:GetValue(itemId)
        local typeData = Tables.itemTypeTable:GetValue(itemData.type:GetHashCode())
        local itemCount = GameInstance.player.inventory:GetItemCountInBag(Utils.getCurrentScope(), itemId)
        if typeData.storageSpace == GEnums.ItemStorageSpace.ValuableDepot then
            local depot = GameInstance.player.inventory.valuableDepots[typeData.valuableTabType]:GetOrFallback(Utils.getCurrentScope())
            for instId,itemBundle in pairs(depot.instItems) do
                if itemBundle.id == itemId and
                    not GameInstance.player.inventory:IsEquipped(Utils.getCurrentScope(), itemId, instId) and
                    typeData.valuableTabType ~= GEnums.ItemValuableDepotType.Weapon then
                    local itemData = Tables.itemTable:GetValue(itemId)
                    local isLocked = GameInstance.player.inventory:IsItemLocked(Utils.getCurrentScope(), itemId, instId)
                    local order = 0
                    if isLocked then
                        order = 1
                    end
                    local notSelected = 1
                    if self.m_selectedItems then
                        for i=1,#self.m_selectedItems do
                            if itemBundle.instId == self.m_selectedItems[i].instId then
                                notSelected = 0
                            end
                        end
                    end
                    table.insert(items,{
                        id = itemId,
                        count = 1,
                        instId = instId,
                        order = order,
                        sortId1 = itemData.sortId1,
                        sortId2 = itemData.sortId2,
                        rarity = itemData.rarity,
                        isLocked = isLocked,
                        notSelected = notSelected,
                    })
                end
            end
        else

            local itemData = Tables.itemTable:GetValue(itemId)
            local notSelected = 1
            if self.m_selectedItems then
                for i=1,#self.m_selectedItems do
                    if itemId == self.m_selectedItems[i].itemId then
                        notSelected = 0
                    end
                end
            end
            table.insert(items,{
                id = itemId,
                count = itemCount,
                sortId1 = itemData.sortId1,
                sortId2 = itemData.sortId2,
                rarity = itemData.rarity,
                notSelected = notSelected,
            })
        end
    end
    self.items = items

    self.view.emptyNode.gameObject:SetActiveIfNecessary(#items <= 0)

    self.view.itemList:UpdateCount(#self.items)

    self.view.sortNode:InitSortNode(self.m_sortOptions, function(optData, isIncremental)
        self:_OnSortChanged(optData, isIncremental)
    end, nil, false)

    self.view.sortNode:SortCurData()

end

ItemSelectList._OnSortChanged = HL.Method(HL.Table, HL.Boolean) << function(self, optData, isIncremental)
    Notify(MessageConst.HIDE_ITEM_TIPS)
    self:_SortData(optData.keys, isIncremental)
    self.view.itemList:UpdateCount(#self.items)
end

ItemSelectList._SortData = HL.Method(HL.Table, HL.Boolean) << function(self, keys, isIncremental)
    table.sort(self.items, Utils.genSortFunctionWithIgnore(keys, isIncremental,{"notSelected"}))
end

ItemSelectList._OnUpdateItemList = HL.Method(HL.Userdata, HL.Number) << function(self, object, index)
    local cell = self.getItemCell(object)
    local itemBundle = self.items[index]
    if self.m_selectedItems then
        local find = false
        for i=1,#self.m_selectedItems do
            if itemBundle.instId == self.m_selectedItems[i].instId then
                find = true
            end
        end
        cell.view.toggle.gameObject:SetActiveIfNecessary(find)
    else
        cell.view.toggle.gameObject:SetActiveIfNecessary(false)
    end


    cell:InitItem(self.items[index],function()
        self:_OnClickItem(object,index)
    end)

    cell:SetEnableHoverTips(not DeviceInfo.usingController)
    local itemId = cell.id
    local isInst = Utils.isItemInstType(itemId)
    if not isInst then
        if cell.count > 0 then
            cell:UpdateCountWithColor(cell.count, string.format(UIConst.COLOR_STRING_FORMAT, "A4A4A4", "%s"))
        else
            cell:UpdateCountWithColor(cell.count, string.format(UIConst.COLOR_STRING_FORMAT, UIConst.COUNT_NOT_ENOUGH_COLOR_STR, "%s"))
        end

        cell.view.countNode.gameObject:SetActive(true)
    else
        if itemBundle.isLocked then
            cell:SetIconTransparent(UIConst.ITEM_MISSING_TRANSPARENCY)
        else
            cell:SetIconTransparent(UIConst.ITEM_EXIST_TRANSPARENCY)
        end
        cell.view.countNode.gameObject:SetActive(false)
    end
end

ItemSelectList._OnClickItem = HL.Method(HL.Userdata, HL.Number) << function(self, object, index)
    local cell = self.getItemCell(object)

    local posInfo = {
        tipsPosType = UIConst.UI_TIPS_POS_TYPE.RightTop,
        tipsPosTransform = self.view.tipsPos.transform,
        isSideTips = true,
    }
    cell:ShowTips(posInfo, function() end)

    local itemBundle = self.items[index]
    if self.m_onClickItem then
        self.m_onClickItem(itemBundle, cell, index)
    end
end

ItemSelectList._OnItemLockedStateChanged = HL.Method(HL.Table) << function(self, arg)
    local itemId, instId, isLock = unpack(arg)

    for i = 1,#self.items do
        local cell = self.getItemCell(i)
        if cell then
            if self.items[i].instId == instId then
                if isLock then
                    cell.view.toggle.gameObject:SetActiveIfNecessary(false)
                end
                if isLock then
                    cell:SetIconTransparent(UIConst.ITEM_MISSING_TRANSPARENCY)
                else
                    cell:SetIconTransparent(UIConst.ITEM_EXIST_TRANSPARENCY)
                end
            end
        end
    end

    if isLock and self.m_onUnlockItem then
        self.m_onUnlockItem(itemId, instId)
    end

end


HL.Commit(ItemSelectList)
return ItemSelectList

