local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

DepotContent = HL.Class('DepotContent', UIWidgetBase)




DepotContent.canDrag = HL.Field(HL.Boolean) << false

DepotContent.canPlace = HL.Field(HL.Boolean) << false

DepotContent.canSplit = HL.Field(HL.Boolean) << false

DepotContent.canClear = HL.Field(HL.Boolean) << false

DepotContent.m_showEmptyChoice = HL.Field(HL.Boolean) << false

DepotContent.m_showHistory = HL.Field(HL.Boolean) << false

DepotContent.isIncremental = HL.Field(HL.Boolean) << false

DepotContent.sortKeys = HL.Field(HL.Table)

DepotContent.m_allItemInfoList = HL.Field(HL.Table)

DepotContent.m_allItemInfoMap = HL.Field(HL.Table)

DepotContent.m_curDropHighlightId = HL.Field(HL.String) << ''

DepotContent.m_customItemInfoListPreProcess = HL.Field(HL.Function)

DepotContent.m_customOnUpdateCell = HL.Field(HL.Function)

DepotContent.m_customSetActionMenuArgs = HL.Field(HL.Function)

DepotContent.m_customItemInfoListPostProcess = HL.Field(HL.Function)

DepotContent.m_insertCustomListData = HL.Field(HL.Function)

DepotContent.m_beforeFindAndHighlightForDrop = HL.Field(HL.Function)

DepotContent.m_itemCellExtraInfo = HL.Field(HL.Table)

DepotContent.m_itemInfoCount = HL.Field(HL.Number) << 0

DepotContent.m_itemInfoList = HL.Field(HL.Table)

DepotContent.m_onClickItemAction = HL.Field(HL.Function)

DepotContent.m_valuableDepotType = HL.Field(GEnums.ItemValuableDepotType)

DepotContent.m_acceptDrop = HL.Field(HL.Boolean) << false

DepotContent.m_acceptQuickDrop = HL.Field(HL.Boolean) << false

DepotContent.m_disableAutoHighlightForDrop = HL.Field(HL.Boolean) << false

DepotContent.m_isFactoryDepot = HL.Field(HL.Boolean) << false

DepotContent.m_updateStopped = HL.Field(HL.Boolean) << false

DepotContent.m_dragSourceType = HL.Field(HL.Number) << -1

DepotContent.dropHelper = HL.Field(HL.Forward('UIDropHelper'))

DepotContent.m_getCell = HL.Field(HL.Function)

DepotContent.m_itemIndexMap = HL.Field(HL.Table)

DepotContent.m_nonValidShowTypes = HL.Field(HL.Table)

DepotContent.m_showingTypes = HL.Field(HL.Table)

DepotContent.m_domainId = HL.Field(HL.String) << ""

DepotContent.m_missionItemIds = HL.Field(HL.Table)





DepotContent._OnFirstTimeInit = HL.Override() << function(self)
    self.m_itemIndexMap = {}

    self.m_getCell = UIUtils.genCachedCellFunction(self.view.itemList, function(object)
        return UIWidgetManager:Wrap(object)
    end)
    self.view.itemList.onUpdateCell:AddListener(function(object, csIndex)
        self:_OnUpdateCell(object, LuaIndex(csIndex))
    end)
    self.view.itemList.onCellSelectedChanged:AddListener(function(obj, csIndex, isSelected)
        local cell = self.m_getCell(obj)
        if cell then
            cell.item:SetSelected(isSelected)
        end
    end)


    self.view.dropMask.gameObject:SetActive(false)
    self.view.dragMask.gameObject:SetActive(false)
    self:RegisterMessage(MessageConst.ON_START_UI_DRAG, function(dragHelper)
        self:OnStartUiDrag(dragHelper)
    end)
    self:RegisterMessage(MessageConst.ON_END_UI_DRAG, function(dragHelper)
        self:OnEndUiDrag(dragHelper)
    end)


    self:RegisterMessage(MessageConst.ON_FACTORY_DEPOT_CHANGED, function(args)
        if not self.m_isFactoryDepot then
            return
        end
        local depotChange = unpack(args)
        self:_OnDepotChanged(depotChange)
    end)
    self:RegisterMessage(MessageConst.ON_VALUABLE_DEPOT_CHANGED, function(args)
        if self.m_isFactoryDepot then
            return
        end
        local valuableDepotType, depotChange = unpack(args)
        if valuableDepotType == self.m_valuableDepotType then
            self:_OnDepotChanged(depotChange)
        end
    end)
    self:RegisterMessage(MessageConst.ON_SYNC_INVENTORY, function()
        self:_OnDepotChanged(nil, true)
    end)

    self:RegisterMessage(MessageConst.ON_ITEM_BAG_MOVE_TO_FACTORY_DEPOT, function(args)
        self:_PlayDropAnimation(args)
    end)
    self:RegisterMessage(MessageConst.ON_FAC_MOVE_ITEM_CACHE_TO_DEPOT, function(args)
        self:_PlayDropAnimation(args)
    end)
    self:RegisterMessage(MessageConst.ON_FAC_MOVE_ITEM_GRID_BOX_TO_DEPOT, function(args)
        self:_PlayDropAnimation(args)
    end)
    self:RegisterMessage(MessageConst.ON_FAC_MOVE_ALL_CACHE_OUT_ITEM_TO_DEPOT, function(args)
        self:_PlayDropAnimation(args)
    end)
end

DepotContent._GetDepot = HL.Method().Return(CS.Beyond.Gameplay.InventorySystem.ItemDepot) << function(self)
    if self.m_isFactoryDepot then
        local factoryDepot = GameInstance.player.inventory.factoryDepot
        local depotInChapter = factoryDepot:GetOrFallback(Utils.getCurrentScope())
        local actualDepot = depotInChapter[Utils.getCurrentChapterId()]
        if self.m_domainId ~= "" then
            actualDepot = depotInChapter[ScopeUtil.ChapterIdStr2Int(self.m_domainId)]
        end
        return actualDepot
    else
        return GameInstance.player.inventory.valuableDepots[self.m_valuableDepotType]:GetOrFallback(Utils.getCurrentScope())
    end
end

DepotContent.InitDepotContent = HL.Method(GEnums.ItemValuableDepotType, HL.Opt(HL.Function, HL.Table)) <<
function(self, valuableDepotType, onClickItemAction, otherArgs)
    self.m_valuableDepotType = valuableDepotType
    self.m_isFactoryDepot = not valuableDepotType or valuableDepotType == GEnums.ItemValuableDepotType.Factory
    if otherArgs.domainId ~= nil then
        self.m_domainId = otherArgs.domainId
    end

    self:_FirstTimeInit()

    self.m_readItemIds = {}

    self.view.dropMask.onDropEvent:RemoveAllListeners()
    self.view.dropMask.onToggleHighlight:RemoveAllListeners()
    if self.m_isFactoryDepot then
        self.m_dragSourceType = UIConst.UI_DRAG_DROP_SOURCE_TYPE.FactoryDepot
        if Utils.isDepotManualInOutLocked() then
            self.m_acceptDrop = false
            self.m_acceptQuickDrop = false
            self.canDrag = false
        else
            self.dropHelper = UIUtils.initUIDropHelper(self.view.dropMask, {
                acceptTypes = UIConst.FACTORY_DEPOT_DROP_ACCEPT_INFO,
                onDropItem = function(eventData, dragHelper)
                    self:OnDropItem(dragHelper)
                end,
                onToggleHighlight = function(active)
                    if active then
                        self:_FindAndHighlightForDrop()
                    else
                        self:_CancelDropHighlight()
                    end
                end,
                isDropArea = true,
                quickDropCheckGameObject = self.gameObject,
            })
            self.m_acceptDrop = true
            self.m_acceptQuickDrop = true
            self.canDrag = not otherArgs.disableDrag
        end
    else
        self.m_dragSourceType = nil
        self.m_acceptDrop = false
        self.m_acceptQuickDrop = false
        self.canDrag = false
    end

    self.m_onClickItemAction = onClickItemAction

    otherArgs = otherArgs or {}
    self.m_showingTypes = otherArgs.showingTypes
    self.m_customOnUpdateCell = otherArgs.customOnUpdateCell
    self.m_customSetActionMenuArgs = otherArgs.customSetActionMenuArgs
    self.m_customItemInfoListPreProcess = otherArgs.customItemInfoListPreProcess
    self.m_customItemInfoListPostProcess = otherArgs.customItemInfoListPostProcess
    self.m_insertCustomListData = otherArgs.insertCustomListData
    self.m_beforeFindAndHighlightForDrop = otherArgs.beforeFindAndHighlightForDrop
    self.m_disableAutoHighlightForDrop = otherArgs.disableAutoHighlightForDrop == true
    self.m_showHistory = otherArgs.showHistory == true
    if self.m_insertCustomListData and self.m_showHistory then
        logger.error("showHistory 会导致 insertCustomListData 无效", depotType)
    end
    self.m_showEmptyChoice = otherArgs.showEmptyChoice == true
    self.canPlace = otherArgs.canPlace == true
    self.canClear = otherArgs.canClear == true
    self.canSplit = false
    self.m_nonValidShowTypes = otherArgs.nonValidShowTypes
    self.m_itemCellExtraInfo = otherArgs.itemCellExtraInfo
    self.m_missionItemIds = otherArgs.missionItemIds or {}

    self.isIncremental = otherArgs.isIncremental == nil and true or otherArgs.isIncremental
    if otherArgs.sortKeys then
        self.sortKeys = otherArgs.sortKeys
    else
        self.sortKeys = self.isIncremental and UIConst.FAC_DEPOT_SORT_OPTIONS[1].keys or UIConst.FAC_DEPOT_SORT_OPTIONS[1].reverseKeys
    end

    self.m_updateStopped = false
    self.m_itemIndexMap = {}
    self:RefreshAll()
end

DepotContent.StopUpdate = HL.Method(HL.Boolean) << function(self, cacheAllCell)
    self.m_updateStopped = true
    if cacheAllCell then



        self.view.itemList:UpdateCount(0)
    end
end

DepotContent.StartUpdate = HL.Method() << function(self)
    if not self.m_updateStopped then
        return
    end
    self.m_updateStopped = false
    self:RefreshAll()
end

DepotContent._CreateItemInfo = HL.Method(HL.String, HL.Opt(HL.Number, HL.Number, HL.Boolean)).Return(HL.Table) <<
function(self, id, count, instId, isInfinite)
    local data = Tables.itemTable:GetValue(id)
    local info = {
        id = id,
        instId = instId or 0,
        isInst = instId ~= nil,
        count = isInfinite and data.maxBackpackStackCount or count,
        maxStackCount = data.maxBackpackStackCount,
        data = data,
        showingType = data.showingType,
        rarity = data.rarity,
        sortId1 = data.sortId1,
        sortId1Neg = -data.sortId1,
        sortId2 = data.sortId2,
        isInfinite = isInfinite,
        isMissionItem = self.m_missionItemIds[id] == true
    }
    info.missionSortId = info.isMissionItem and 0 or 1
    info.missionReverseSortId = -info.missionSortId
    return info
end

DepotContent._IsInfiniteItemInDepot = HL.Method(HL.String, HL.Any).Return(HL.Boolean) << function(self, itemId, depot)
    if not self.m_isFactoryDepot then
        return false
    end

    if depot == nil then
        return false
    end

    local success, isInfinite = depot.infiniteItemIds:TryGetValue(itemId)
    if not success then
        return false
    end

    return isInfinite
end

DepotContent.ToggleAcceptDrop = HL.Method(HL.Boolean) << function(self, active)
    self.m_acceptDrop = active
end

DepotContent.ToggleQuickAcceptDrop = HL.Method(HL.Boolean) << function(self, active)
    self.m_acceptQuickDrop = active
end

DepotContent.m_curDraggingDragHelper = HL.Field(HL.Forward('UIDragHelper'))

DepotContent.OnStartUiDrag = HL.Method(HL.Forward('UIDragHelper')) << function(self, dragHelper)
    if self.m_updateStopped then
        return
    end
    if not self.m_isFactoryDepot then
        return
    end
    if dragHelper.source == self.m_dragSourceType then

        if self.view.itemListAutoScrollArea then
            self.view.itemListAutoScrollArea.gameObject:SetActive(true)
        end
        return
    end

    if self.m_acceptDrop and self.m_acceptQuickDrop and self.dropHelper:Accept(dragHelper) then
        self.m_curDraggingDragHelper = dragHelper
        self.view.quickDropButton.onClick:RemoveAllListeners()
        self.view.quickDropButton.onClick:AddListener(function()
            self:OnDropItem(dragHelper)
        end)
        self.view.dropMask.gameObject:SetActive(true)
        self.view.dropMask:ToggleHighlight(false)
        self:_ShowMobileDragHelper(dragHelper)
    end
end

DepotContent.OnEndUiDrag = HL.Method(HL.Forward('UIDragHelper')) << function(self, dragHelper)
    self.view.dragMask.gameObject:SetActive(false)

    if self.m_updateStopped then
        return
    end
    if not self.m_isFactoryDepot then
        return
    end
    if dragHelper.source == self.m_dragSourceType then
        if self.view.itemListAutoScrollArea then
            self.view.itemListAutoScrollArea.gameObject:SetActive(false)
        end
        return
    end
    if self.m_acceptDrop and self.m_acceptQuickDrop and self.dropHelper:Accept(dragHelper) then
        self.m_curDraggingDragHelper = nil
        self.view.dropMask.gameObject:SetActive(false)
        self:_CancelDropHighlight()
        Notify(MessageConst.HIDE_ITEM_DRAG_HELPER)
    end
end


DepotContent.RefreshAll = HL.Method(HL.Opt(HL.Boolean)) << function(self, skipGraduallyShow)
    local allItemInfoList = {}

    local depot = self:_GetDepot()
    local processItem = function(id, count, instId, isInfinite)
        local valid = true
        if self.m_nonValidShowTypes then
            local data = Tables.itemTable:GetValue(id)
            valid = not lume.find(self.m_nonValidShowTypes, data.showingType)
        end
        if valid then
            local info = self:_CreateItemInfo(id, count, instId, isInfinite)
            table.insert(allItemInfoList, info)
        end
    end
    for id, bundle in pairs(depot.normalItems) do
        processItem(id, bundle.count, nil, self:_IsInfiniteItemInDepot(id, depot))
    end
    if self.m_isFactoryDepot and self.m_itemIndexMap and not self.m_showHistory then
        for id in pairs(self.m_itemIndexMap) do
            if type(id) == 'string' and id ~= '' and not depot.normalItems:ContainsKey(id) then
                processItem(id, 0, nil, false)
            end
        end
    end
    for instId, bundle in pairs(depot.instItems) do
        processItem(bundle.id, bundle.count, instId)
    end
    if self.m_customItemInfoListPreProcess ~= nil then
        self:m_customItemInfoListPreProcess(allItemInfoList, depot)
    end
    if self.m_showHistory then
        for _, id in pairs(GameInstance.player.inventory.gotItems) do
            local succ, itemData = Tables.itemTable:TryGetValue(id)
            if succ then
                local facFound, facItemData = Tables.factoryItemTable:TryGetValue(id)
                local valid = true
                valid = GameInstance.player.inventory:IsPlaceInBag(itemData.type)
                if facFound then
                    valid = valid and facItemData ~= nil
                end
                if valid and self.m_nonValidShowTypes then
                    valid = not lume.find(self.m_nonValidShowTypes, itemData.showingType)
                end
                if valid then
                    if not depot.normalItems:ContainsKey(id) then
                        local info = self:_CreateItemInfo(id, 0)
                        table.insert(allItemInfoList, info)
                    end
                end
            end
        end
    elseif self.m_insertCustomListData then
        self.m_insertCustomListData(allItemInfoList, depot)
    end

    if self.m_customItemInfoListPostProcess ~= nil then
        allItemInfoList = self.m_customItemInfoListPostProcess(allItemInfoList)
    end
    self.m_allItemInfoList = allItemInfoList

    local allItemInfoMap = {}
    for _, v in ipairs(allItemInfoList) do
        allItemInfoMap[v.id] = v
    end
    self.m_allItemInfoMap = allItemInfoMap

    self:ChangeShowingType(self.m_showingTypes, skipGraduallyShow)
end

DepotContent.ChangeShowingType = HL.Method(HL.Table, HL.Opt(HL.Boolean)) <<
function(self, showingTypes, skipGraduallyShow)
    local itemInfoList = {}
    self.m_showingTypes = showingTypes
    for _, info in ipairs(self.m_allItemInfoList) do
        if not showingTypes or lume.find(showingTypes, info.showingType) then

            table.insert(itemInfoList, info)
        end
    end

    self.m_itemInfoList = itemInfoList
    self:OnSortChanged(skipGraduallyShow)
end

DepotContent.OnSortChanged = HL.Method(HL.Opt(HL.Boolean)) << function(self, skipGraduallyShow)
    local list = self.m_itemInfoList
    if list[1] and list[1].isEmptyChoice then
        table.remove(list, 1)
    end

    table.sort(list, Utils.genSortFunction(self.sortKeys, self.isIncremental))

    if self.m_showEmptyChoice or (DeviceInfo.usingController and next(list) == nil and self.config.SHOW_EMPTY_CHOICE_ON_CONTROLLER) then
        table.insert(list, 1, { id = "", count = 0, extraOrder = 1, isEmptyChoice = true, })
    end

    self.m_itemIndexMap = {}
    for index, info in ipairs(list) do
        if info.instId and info.instId > 0 then
            self.m_itemIndexMap[info.instId] = index
        else
            self.m_itemIndexMap[info.id] = index
        end
    end

    self.m_itemInfoCount = #list

    if string.isEmpty(self.m_curDropHighlightId) then
        self.view.itemList:UpdateCount(self.m_itemInfoCount, false, false, false, skipGraduallyShow == true)
    else
        local index
        for k, v in ipairs(self.m_itemInfoList) do
            if v.id == self.m_curDropHighlightId then
                index = k
                break
            end
        end
        if index then
            self.view.itemList:UpdateCount(self.m_itemInfoCount, false, false, false, true)
        else
            self.view.itemList:UpdateCount(self.m_itemInfoCount + 1, false, false, false, true)
        end
    end

    self.view.emptyTarget.gameObject:SetActive(self.m_itemInfoCount == 0)
end

DepotContent.GetItemIndex = HL.Method(HL.String).Return(HL.Opt(HL.Number)) << function(self, itemId)
    return self.m_itemIndexMap[itemId]
end

DepotContent.GetItemCount = HL.Method(HL.String).Return(HL.Number) << function(self, itemId)
    local index = self.m_itemIndexMap[itemId]
    local info = self.m_itemInfoList[index]
    return info.count
end

DepotContent.GetCell = HL.Method(HL.Any).Return(HL.Opt(HL.Forward("ItemSlot"))) << function(self, objOrIndex)
    return self.m_getCell(objOrIndex)
end

DepotContent.CheckAndNaviToTargetCell = HL.Method(HL.Function) << function(self, checkFunc)
    local findTag = false
    for index, itemBundle in ipairs(self.m_itemInfoList) do
        if checkFunc(itemBundle.id) then
            self.view.itemList:ScrollToIndex(CSIndex(index), true)
            local slot = self.m_getCell(index)
            slot:SetAsNaviTarget()
            findTag = true
            break
        end
    end
    if not findTag then
        self.view.itemList:ScrollToIndex(CSIndex(1), true)
        local slot = self.m_getCell(1)
        slot:SetAsNaviTarget()
    end
end

DepotContent.GetCurNaviTargetSlot = HL.Method().Return(HL.Number, HL.Opt(HL.Forward("ItemSlot"))) << function(self)
    local count = self.view.itemList.count
    for i = 1, count do
        local object = self.view.itemList:Get(CSIndex(i))
        if object then
            local cell = self.m_getCell(object)
            if cell.item.view.button.isNaviTarget then
                return CSIndex(i), cell
            end
        end
    end
    return -1
end

DepotContent.GetAllItemInfo = HL.Method().Return(HL.Opt(HL.Table)) << function(self)
    if self.m_allItemInfoList ~= nil then
        return self.m_allItemInfoList
    end
end

DepotContent._OnUpdateCell = HL.Method(GameObject, HL.Number) << function(self, object, luaIndex)
    local cell = self.m_getCell(object)

    if luaIndex > self.m_itemInfoCount then

        cell:InitItemSlot()
        cell.item:SetSelected(false)
        cell:SetDropHighlighted(true)
        cell.item.view.missionMark.gameObject:SetActive(false)
        return
    end

    local info = self.m_itemInfoList[luaIndex]
    if not string.isEmpty(info.id) and info.count <= 0 and cell.view.dragItem.inDragging then


        cell.view.dragItem:OnEndDrag()
    end
    cell:InitItemSlot(info, function()
        self:_OnClickItem(luaIndex)
    end, nil, true)
    cell.item.canPlace = self.canPlace
    cell.item.canSplit = self.canSplit
    cell.item.canClear = self.canClear
    cell.item.fromDepot = true
    cell.item.canUse = false
    cell.supportQuickMovingHalfItem = true

    local isDropHighlight = (not string.isEmpty(self.m_curDropHighlightId)) and (self.m_curDropHighlightId == info.id)
    cell.item:SetSelected(cell.item.showingTips or cell.item.showingActionMenu)
    cell:SetDropHighlighted(isDropHighlight)

    if self.m_itemCellExtraInfo then
        cell.item:SetExtraInfo(self.m_itemCellExtraInfo)
    end

    local isEmpty = string.isEmpty(info.id)
    if isEmpty or not self.view.config.USE_ITEM_ID_NAME then
        cell.gameObject.name = "Item__" .. CSIndex(luaIndex)
    else
        cell.gameObject.name = "Item_" .. info.id
        cell.item.view.button.clickHintTextId = "virtual_mouse_hint_item_tips"
    end
    cell.item:ShowPickUpLogo(true)
    cell.item:UpdateRedDot()
    if cell.item.redDot.curIsActive then
        self.m_readItemIds[info.id] = true
    end

    cell.item.view.missionMark.gameObject:SetActive(info.isMissionItem)
    cell.item.redDot.gameObject:SetActive(not info.isMissionItem)

    cell.item.actionMenuArgs = {
        source = UIConst.UI_DRAG_DROP_SOURCE_TYPE.FactoryDepot,
        moveCount = info.isInfinite and info.maxStackCount or math.min(info.count or 0, info.maxStackCount or 0),
    }

    cell.view.dropItem.enabled = false
    if not isEmpty and self.canDrag and info.count > 0 then
        cell.view.dragItem.enabled = true
        local data = Tables.itemTable:GetValue(info.id)
        local dragHelper = UIUtils.initUIDragHelper(cell.view.dragItem, {
            source = self.m_dragSourceType,
            type = data.type,

            itemId = info.id,
            instId = info.instId,
            count = info.isInfinite and info.maxStackCount or math.min(info.count or 0, info.maxStackCount or 0),

            onBeginDrag = function()
                self.view.itemList:SetCellCanCache(CSIndex(luaIndex), false)
                self.view.dragMask.gameObject:SetActive(true)
                cell.item:Read()
            end,
            onEndDrag = function()
                self.view.itemList:SetCellCanCache(CSIndex(luaIndex), true)
                self.view.dragMask.gameObject:SetActive(false)
            end,
        })
        cell:InitPressDrag()

        cell.item.actionMenuArgs.dragHelper = dragHelper

        if self.m_customSetActionMenuArgs ~= nil then
            self.m_customSetActionMenuArgs(cell.item.actionMenuArgs)
        end
    else
        cell.view.dragItem.enabled = false
        cell.item.view.button.longPressHintTextId = ""
    end

    if self.m_customOnUpdateCell then
        self.m_customOnUpdateCell(cell, info, luaIndex)
    end
end

DepotContent._OnClickItem = HL.Method(HL.Number) << function(self, luaIndex)
    local item = self.m_itemInfoList[luaIndex]
    if not item then
        return
    end

    local cell = self.m_getCell(luaIndex)
    if self.m_onClickItemAction then
        self.m_onClickItemAction(item.id, cell)
    elseif not string.isEmpty(item.id) then
        cell.item:ShowTips()
        cell.item:Read()
    end
end


DepotContent.OnDropItem = HL.Method(HL.Forward('UIDragHelper')) << function(self, dragHelper)
    if Utils.isDepotManualInOutLocked() then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_DEPOT_MANUAL_IN_OUT_LOCKED)
        return
    end

    local source = dragHelper.source
    local dragInfo = dragHelper.info
    local core = GameInstance.player.remoteFactory.core
    local mode = dragHelper.isHalfDragging and CS.Proto.ITEM_MOVE_MODE.HalfGrid or CS.Proto.ITEM_MOVE_MODE.Normal
    if source == UIConst.UI_DRAG_DROP_SOURCE_TYPE.Storage then

        core:Message_OpMoveItemGridBoxToDepot(Utils.getCurrentChapterId(), dragInfo.storage.componentId, dragInfo.csIndex, mode)
    elseif source == UIConst.UI_DRAG_DROP_SOURCE_TYPE.Repository then

        core:Message_OpMoveItemCacheToDepot(Utils.getCurrentChapterId(), dragInfo.repository.componentId, dragInfo.cacheGridIndex, mode)
    elseif source == UIConst.UI_DRAG_DROP_SOURCE_TYPE.ItemBag then

        GameInstance.player.inventory:ItemBagMoveToFactoryDepot(Utils.getCurrentScope(), Utils.getCurrentChapterId(), dragInfo.csIndex, mode)
    end
end

DepotContent._OnDepotChanged = HL.Method(HL.Opt(CS.Beyond.Gameplay.InventorySystem.DepotCachedChange, HL.Boolean)) <<
function(self, depotChange, refreshAll)
    if self.m_updateStopped then
        return
    end

    if refreshAll or depotChange.instIds.Count > 0 then

        self:RefreshAll(true)
        return
    end

    if not self.m_isFactoryDepot then
        if depotChange.hasAdd or depotChange.hasRemove then

            self:RefreshAll(true)
            return
        end
    else


        for _, itemId in pairs(depotChange.normalItemIds) do
            if not self.m_itemIndexMap[itemId] then

                self:RefreshAll(true)
                return
            end
        end
    end

    local depot = self:_GetDepot()
    for _, itemId in pairs(depotChange.normalItemIds) do
        local index = self.m_itemIndexMap[itemId]
        self:_RefreshSingleItemCount(index, depot:GetCount(itemId))
    end
end

DepotContent._RefreshSingleItemCount = HL.Method(HL.Number, HL.Number) << function(self, slotIndex, count)

    local info = self.m_itemInfoList[slotIndex]
    if info.instId == 0 then
        if info.count == count then
            return
        end
        info.count = count
    end

    local obj = self.view.itemList:Get(CSIndex(slotIndex))
    if obj then
        self:_OnUpdateCell(obj, slotIndex)
    end
end

DepotContent._FindAndHighlightForDrop = HL.Method() << function(self)
    if self.m_disableAutoHighlightForDrop or not self.m_curDraggingDragHelper then
        return
    end

    local id = self.m_curDraggingDragHelper:GetId()
    self.m_curDropHighlightId = id
    if string.isEmpty(id) then
        return
    end

    if self.m_beforeFindAndHighlightForDrop then
        self.m_beforeFindAndHighlightForDrop(id)
    end

    local index = self.m_itemIndexMap[id]
    if index then
        local object = self.view.itemList:Get(CSIndex(index))
        if object then
            local cell = self.m_getCell(object)
            cell:SetDropHighlighted(true)
        end
    else
        index = self.m_itemInfoCount + 1
        self.view.itemList:UpdateCount(index, false, false, true, true)
    end
    self.view.itemList:ScrollToIndex(CSIndex(index))
end

DepotContent._CancelDropHighlight = HL.Method() << function(self)
    if self.m_disableAutoHighlightForDrop then
        return
    end

    local id = self.m_curDropHighlightId
    self.m_curDropHighlightId = ''

    local index = self.m_itemIndexMap[id]
    local needRefresh = false
    if not index then
        needRefresh = true
        index = self.m_itemInfoCount + 1
    end
    local object = self.view.itemList:Get(CSIndex(index))
    if object then
        local cell = self.m_getCell(object)
        cell.item:SetSelected(false)
        cell:SetDropHighlighted(false)
    end
    if needRefresh then
        self.view.itemList:UpdateCount(self.m_itemInfoCount, false, false, true, true)
    end
end

DepotContent.m_readItemIds = HL.Field(HL.Table)

DepotContent.ReadCurShowingItems = HL.Method() << function(self)
    if not self.m_readItemIds or not next(self.m_readItemIds) then
        return
    end
    local ids = {}
    for k, _ in pairs(self.m_readItemIds) do
        table.insert(ids, k)
    end
    self.m_readItemIds = {}
    GameInstance.player.inventory:ReadNewItems(ids)
end


DepotContent._ShowMobileDragHelper = HL.Method(HL.Forward('UIDragHelper')) << function(self, dragHelper)
    if not DeviceInfo.usingTouch then
        return
    end
    if not self.view.gameObject.activeInHierarchy then
        return
    end
    if Utils.isDepotManualInOutLocked() then
        return
    end

    local dragInfo = dragHelper.info

    if Tables.liquidTable:ContainsKey(dragHelper:GetId()) then
        return
    end

    local moveAct, canBatch
    if dragHelper.source == UIConst.UI_DRAG_DROP_SOURCE_TYPE.ItemBag then
        moveAct = function(mode)
            GameInstance.player.inventory:ItemBagMoveToFactoryDepot(Utils.getCurrentScope(), Utils.getCurrentChapterId(), dragInfo.csIndex, mode)
            dragHelper.uiDragItem:OnEndDrag(nil)
        end
        canBatch = true
    elseif dragHelper.source == UIConst.UI_DRAG_DROP_SOURCE_TYPE.Repository then
        moveAct = function(mode)
            GameInstance.player.remoteFactory.core:Message_OpMoveItemCacheToDepot(Utils.getCurrentChapterId(), dragInfo.repository.componentId, dragInfo.cacheGridIndex, mode)
            dragHelper.uiDragItem:OnEndDrag(nil)
        end
    elseif dragHelper.source == UIConst.UI_DRAG_DROP_SOURCE_TYPE.Storage then
        moveAct = function(mode)
            GameInstance.player.remoteFactory.core:Message_OpMoveItemGridBoxToDepot(Utils.getCurrentChapterId(), dragInfo.storage.componentId, dragInfo.csIndex, mode)
            dragHelper.uiDragItem:OnEndDrag(nil)
        end
        canBatch = true
    else
        return
    end

    local args = {
        isLeft = self.view.config.IS_IN_SCREEN_LEFT,
        actions = {
            {
                text = Language.LUA_MOBILE_ITEM_DRAG_GRID_TO_DEPOT,
                icon = "icon_common_move_to_bag",
                action = function()
                    moveAct(CS.Proto.ITEM_MOVE_MODE.Grid)
                end
            },
            {
                text = Language.LUA_MOBILE_ITEM_DRAG_HALF,
                icon = "icon_common_move_half",
                action = function()
                    moveAct(CS.Proto.ITEM_MOVE_MODE.HalfGrid)
                end
            },
        }
    }

    if canBatch then
        table.insert(args.actions, 2, {
            text = Language.LUA_MOBILE_ITEM_DRAG_ALL,
            icon = "icon_common_move_all",
            action = function()
                moveAct(CS.Proto.ITEM_MOVE_MODE.BatchItemId)
            end
        })
    end
    Notify(MessageConst.SHOW_ITEM_DRAG_HELPER, args)
end


DepotContent._PlayDropAnimation = HL.Method(HL.Table) << function(self, args)
    if self.m_updateStopped then
        return
    end
    local itemId = args[1]
    local index = self.m_itemIndexMap[itemId]
    if not index then


        self:RefreshAll(true)
        index = self.m_itemIndexMap[itemId]
    end

    local cell = self.m_getCell(index)
    if cell then
        cell:PlayDropAnimation()
    end
    UIUtils.playItemDropAudio(itemId)
end



HL.Commit(DepotContent)
return DepotContent
