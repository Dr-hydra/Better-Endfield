



local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ChangePortableDevice
local PHASE_ID = PhaseId.ChangePortableDevice
local GROUP_BAG = 1
local GROUP_DEPOT = 2


ChangePortableDeviceCtrl = HL.Class('ChangePortableDeviceCtrl', uiCtrl.UICtrl)

ChangePortableDeviceCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_ITEM_BAG_CHANGED] = 'OnItemBagChanged',
    [MessageConst.ON_SYNC_INVENTORY] = 'OnSyncInventory',
    [MessageConst.ON_START_UI_DRAG] = 'OnStartUiDrag',
    [MessageConst.ON_END_UI_DRAG] = 'OnEndUiDrag',
}

ChangePortableDeviceCtrl.m_getCell = HL.Field(HL.Function)

ChangePortableDeviceCtrl.m_getTitle = HL.Field(HL.Function)

ChangePortableDeviceCtrl.m_invDatas = HL.Field(HL.Table)

ChangePortableDeviceCtrl.m_itemBag = HL.Field(HL.Any)


ChangePortableDeviceCtrl.m_moveFrom = HL.Field(HL.Any)

ChangePortableDeviceCtrl.m_cancelMoveBindingId = HL.Field(HL.Number) << -1

ChangePortableDeviceCtrl.m_curDraggingDragHelper = HL.Field(HL.Forward('UIDragHelper'))

ChangePortableDeviceCtrl.m_curDropHighlightGlobalLuaIndex = HL.Field(HL.Number) << -1

ChangePortableDeviceCtrl.m_showDragTempEmptySlot = HL.Field(HL.Boolean) << false

ChangePortableDeviceCtrl.m_showDepotDragTempEmptySlot = HL.Field(HL.Boolean) << false

ChangePortableDeviceCtrl.m_depotDragTempItemId = HL.Field(HL.String) << ""

ChangePortableDeviceCtrl.m_dropCommandHandledInCurrentDrag = HL.Field(HL.Boolean) << false

ChangePortableDeviceCtrl.m_dragTempEmptyBagFastAdded = HL.Field(HL.Boolean) << false

ChangePortableDeviceCtrl.m_dragTempEmptyBagCsIndex = HL.Field(HL.Number) << -1

ChangePortableDeviceCtrl.m_depotDragTempFastAdded = HL.Field(HL.Boolean) << false


ChangePortableDeviceCtrl.m_pendingFocusRequest = HL.Field(HL.Any)


ChangePortableDeviceCtrl.m_suppressTipsUntilRefresh = HL.Field(HL.Boolean) << false




ChangePortableDeviceCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_invDatas = {
        [GROUP_BAG] = {},
        [GROUP_DEPOT] = {},
    }

    self.m_getCell = UIUtils.genCachedCellFunction(self.view.scrollList, function(object)
        return UIWidgetManager:Wrap(object)
    end)
    self.m_getTitle = UIUtils.genCachedCellFunction(self.view.scrollList, function(object)
        return Utils.wrapLuaNode(object)
    end, true)

    self.view.closeBtn.onClick:AddListener(function()
        PhaseManager:PopPhase(PHASE_ID)
    end)
    self.view.scrollList.onUpdateCell:AddListener(function(obj, csIndex)
        self:_OnUpdateCell(obj, csIndex)
    end)
    self.view.scrollList.onUpdateGroupTitle:AddListener(function(obj, csIndex)
        self:_OnUpdateGroupTitle(obj, csIndex)
    end)
    self.view.scrollList.onUpdateGroupBG:AddListener(function(obj, csIndex)
        self:_OnUpdateGroupBG(obj, csIndex)
    end)
    self.view.scrollList.getCellCountInGroup = function(groupCSIndex)
        return #self.m_invDatas[LuaIndex(groupCSIndex)]
    end

    self.m_cancelMoveBindingId = self:BindInputPlayerAction("common_cancel", function()
        self:_ToggleMoveMode(false, nil, true)
    end)
    InputManagerInst:SetBindingText(self.m_cancelMoveBindingId, Language.LUA_EXIT_INV_MOVE_MODE)
    InputManagerInst:ToggleBinding(self.m_cancelMoveBindingId, false)

    self.view.moveHintItem.gameObject:SetActive(false)
    self:_RefreshAll(true)

    UIUtils.initUIDropHelper(self.view.depotDropMask.dropItem, {
        acceptTypes = UIConst.FACTORY_DEPOT_DROP_ACCEPT_INFO,
        onDropItem = function(eventData, dragHelper)
            self:_OnDropToDepot(dragHelper)
        end,
        onToggleHighlight = function(active)
            if active then
                self:_FindAndHighlightForDrop(GROUP_DEPOT)
            else
                self:_CancelDropHighlight()
            end
        end,
        isDropArea = true,
    })

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
end

ChangePortableDeviceCtrl.OnClose = HL.Override() << function(self)
    self:_CancelDropHighlight()
    self:_TryClearDepotDragTempEntry(false)
    self:_ToggleMoveMode(false, nil, true)
end

ChangePortableDeviceCtrl.OnItemBagChanged = HL.Method(HL.Any) << function(self, args)
    local changedIndexes = args and unpack(args)
    local lastColoredSlotStates = self:_CaptureVisibleColoredSlotActiveStates()
    self.m_suppressTipsUntilRefresh = false
    self:_RefreshAll(false)
    self:_TryFocusPendingQuickDropTarget()
    self:_HandlePortableDeviceActivationAfterBagChanged(changedIndexes, lastColoredSlotStates)
end

ChangePortableDeviceCtrl.OnSyncInventory = HL.Method(HL.Any) << function(self, args)
    self.m_suppressTipsUntilRefresh = false
    self:_RefreshAll(false)
    self:_TryFocusPendingQuickDropTarget()
end




ChangePortableDeviceCtrl.OnStartUiDrag = HL.Method(HL.Forward('UIDragHelper')) << function(self, dragHelper)
    self.m_dropCommandHandledInCurrentDrag = false
    local isFromBag = dragHelper.source == UIConst.UI_DRAG_DROP_SOURCE_TYPE.ItemBag
    self.m_curDraggingDragHelper = dragHelper
    self:_RefreshNonDrawingGraphicEnabledForCurrentDrag()
    self.view.depotDropMask.gameObject:SetActive(isFromBag)
    self.view.depotDropMask.transform:SetAsLastSibling()
    self:_RefreshColoredSlotDragState(true)
    local shouldShowTempEmptySlot = false
    if isFromBag then
        local itemBag = self.m_itemBag
        local sourceCsIndex = dragHelper.info.csIndex
        shouldShowTempEmptySlot = sourceCsIndex >= 0 and sourceCsIndex < itemBag.coloredSlotNum
        self.view.listAutoScrollArea.gameObject:SetActive(true)
    else
        self.view.scrollList:ScrollToIndex(0, true, CS.Beyond.UI.UIScrollList.ScrollAlignType.Top)
        self.view.listAutoScrollArea.gameObject:SetActive(false)
    end
    if shouldShowTempEmptySlot and not self.m_showDragTempEmptySlot then
        self.m_showDragTempEmptySlot = true
        if not self:_TryAppendDragTempEmptyBagEntryFast() then
            self:_RefreshAll(false)
        end
    end
end

ChangePortableDeviceCtrl.OnEndUiDrag = HL.Method(HL.Opt(HL.Forward('UIDragHelper'))) << function(self, dragHelper)
    self.m_dropCommandHandledInCurrentDrag = false
    self.m_curDraggingDragHelper = nil
    self:_RefreshNonDrawingGraphicEnabledForCurrentDrag()
    self.view.depotDropMask.gameObject:SetActive(false)
    self:_ResetShowingCellsCanCache()
    self:_RefreshColoredSlotDragState(false)
    if self.m_showDragTempEmptySlot then
        self.m_showDragTempEmptySlot = false
        if not self:_TryRemoveDragTempEmptyBagEntryFast() then
            self:_RefreshAll(false)
        end
    end
    self:_CancelDropHighlight()
    self:_TryClearDepotDragTempEntry(true)
    self.view.listAutoScrollArea.gameObject:SetActive(false)
end

ChangePortableDeviceCtrl._ResetShowingCellsCanCache = HL.Method() << function(self)
    self.view.scrollList:UpdateShowingCells(function(globalCsIndex, obj)
        self.view.scrollList:SetCellCanCache(globalCsIndex, true)
    end)
end




ChangePortableDeviceCtrl._TryAppendDragTempEmptyBagEntryFast = HL.Method().Return(HL.Boolean) << function(self)
    if self.m_moveFrom then
        return false
    end
    local bagData = self.m_invDatas[GROUP_BAG]
    if not bagData then
        return false
    end
    for _, entry in ipairs(bagData) do
        if entry.isTempEmpty then
            return true
        end
    end
    local hiddenEmptyCsIndex = self:_GetFirstHiddenEmptyBagSlotIndex()
    table.insert(bagData, {
        group = GROUP_BAG,
        csIndex = hiddenEmptyCsIndex,
        item = {
            id = "",
            count = 0,
        },
        isColoredSlot = false,
        isTempEmpty = true,
        isInvalidTemp = hiddenEmptyCsIndex < 0,
    })
    self.m_dragTempEmptyBagFastAdded = true
    self.m_dragTempEmptyBagCsIndex = hiddenEmptyCsIndex
    self.view.scrollList:AppendCellToGroup(CSIndex(GROUP_BAG))
    return true
end

ChangePortableDeviceCtrl._TryRemoveDragTempEmptyBagEntryFast = HL.Method().Return(HL.Boolean) << function(self)
    if not self.m_dragTempEmptyBagFastAdded then
        return false
    end
    local bagData = self.m_invDatas[GROUP_BAG]
    if not bagData then
        self.m_dragTempEmptyBagFastAdded = false
        self.m_dragTempEmptyBagCsIndex = -1
        return false
    end
    local removeIndex = -1
    for i = #bagData, 1, -1 do
        local entry = bagData[i]
        if entry and entry.isTempEmpty and
            (self.m_dragTempEmptyBagCsIndex < 0 or entry.csIndex == self.m_dragTempEmptyBagCsIndex) then
            removeIndex = i
            break
        end
    end
    if removeIndex < 0 then
        for i = #bagData, 1, -1 do
            local entry = bagData[i]
            if entry and entry.isTempEmpty then
                removeIndex = i
                break
            end
        end
    end
    self.m_dragTempEmptyBagFastAdded = false
    self.m_dragTempEmptyBagCsIndex = -1
    if removeIndex < 0 then
        return false
    end
    if removeIndex ~= #bagData then
        return false
    end
    table.remove(bagData, removeIndex)
    return self.view.scrollList:RemoveLastCellFromGroup(CSIndex(GROUP_BAG))
end

ChangePortableDeviceCtrl._TryAppendDepotDragTempEntryFast = HL.Method(HL.String).Return(HL.Boolean) <<
    function(self, itemId)
        if string.isEmpty(itemId) then
            return false
        end
        local depotData = self.m_invDatas[GROUP_DEPOT]
        if not depotData then
            return false
        end
        for _, entry in ipairs(depotData) do
            if entry.isDepotTempEmpty then
                self.m_showDepotDragTempEmptySlot = true
                self.m_depotDragTempItemId = itemId
                self.m_depotDragTempFastAdded = true
                return true
            end
        end
        table.insert(depotData, {
            group = GROUP_DEPOT,
            item = {
                id = "",
                count = 0,
            },
            itemId = itemId,
            instId = 0,
            isTempEmpty = true,
            isDepotTempEmpty = true,
        })
        self.m_showDepotDragTempEmptySlot = true
        self.m_depotDragTempItemId = itemId
        self.m_depotDragTempFastAdded = true
        self.view.scrollList:AppendCellToGroup(CSIndex(GROUP_DEPOT))
        return true
    end

ChangePortableDeviceCtrl._TryRemoveDepotDragTempEntryFast = HL.Method().Return(HL.Boolean) << function(self)
    if not self.m_depotDragTempFastAdded then
        return false
    end
    local depotData = self.m_invDatas[GROUP_DEPOT]
    self.m_depotDragTempFastAdded = false
    if not depotData or #depotData <= 0 then
        return false
    end
    local removeIndex = -1
    for i = #depotData, 1, -1 do
        local entry = depotData[i]
        if entry and entry.isDepotTempEmpty then
            removeIndex = i
            break
        end
    end
    if removeIndex <= 0 or removeIndex ~= #depotData then
        return false
    end
    table.remove(depotData, removeIndex)
    return self.view.scrollList:RemoveLastCellFromGroup(CSIndex(GROUP_DEPOT))
end




ChangePortableDeviceCtrl._RefreshAll = HL.Method(HL.Opt(HL.Boolean)) << function(self, isInit)
    self:_CollectData()

    local isDepotEmpty = #self.m_invDatas[GROUP_DEPOT] == 0
    self.view.scrollList:SetPaddingBottom(isDepotEmpty and 120 or 30)

    self.view.scrollList:UpdateGroup(2, isInit == true, false, false, not isInit)
    if isInit then
        self:_TryFocusFirstValidCell()
    end

    local itemBag = self.m_itemBag
    local hasNonColoredEmptySlot = false
    for csIndex = itemBag.coloredSlotNum, itemBag.slots.Count - 1 do
        local itemBundle = itemBag.slots[csIndex]
        if string.isEmpty(itemBundle.id) then
            hasNonColoredEmptySlot = true
            break
        end
    end
    self.view.bagIsFullHint.gameObject:SetActive(not hasNonColoredEmptySlot)

    local offsetY = self.view.groupTitle.transform.sizeDelta.y + self.view.scrollList.space.y
    self.view.itemBagColoredSlotUniBG:InitItemBagColoredSlotUniBG(self.view.scrollList, self.m_itemBag.coloredSlotNum, offsetY)
end

ChangePortableDeviceCtrl._CollectData = HL.Method() << function(self)
    self.m_invDatas[GROUP_BAG] = {}
    self.m_invDatas[GROUP_DEPOT] = {}
    self:_BuildBagData()
    self:_BuildDepotData()
end

ChangePortableDeviceCtrl._BuildBagData = HL.Method() << function(self)
    local itemBag = GameInstance.player.inventory.itemBag:GetOrFallback(Utils.getCurrentScope())
    self.m_itemBag = itemBag
    if not itemBag then
        return
    end

    local bagData = self.m_invDatas[GROUP_BAG]
    for csIndex = 0, itemBag.slots.Count - 1 do
        local itemBundle = itemBag.slots[csIndex]
        local itemId = itemBundle.id
        local isColoredSlot = csIndex < itemBag.coloredSlotNum
        local shouldShow = isColoredSlot or (not string.isEmpty(itemId) and Utils.isPortableDevice(itemId))
        if shouldShow then
            table.insert(bagData, {
                group = GROUP_BAG,
                csIndex = csIndex,
                item = itemBundle,
                isColoredSlot = isColoredSlot,
                isTempEmpty = false,
            })
        end
    end

    local shouldShowMoveModeTempEmpty = self.m_moveFrom and
        self.m_moveFrom.source == UIConst.UI_DRAG_DROP_SOURCE_TYPE.ItemBag and
        self.m_moveFrom.csIndex < itemBag.coloredSlotNum
    if shouldShowMoveModeTempEmpty or self.m_showDragTempEmptySlot then
        local hiddenEmptyCsIndex = self:_GetFirstHiddenEmptyBagSlotIndex()
        if hiddenEmptyCsIndex >= 0 then
            table.insert(bagData, {
                group = GROUP_BAG,
                csIndex = hiddenEmptyCsIndex,
                item = {
                    id = "",
                    count = 0,
                },
                isColoredSlot = false,
                isTempEmpty = true,
            })
        elseif self.m_showDragTempEmptySlot then
            table.insert(bagData, {
                group = GROUP_BAG,
                csIndex = -1,
                item = {
                    id = "",
                    count = 0,
                },
                isColoredSlot = false,
                isTempEmpty = true,
                isInvalidTemp = true,
            })
        end
    end
end

ChangePortableDeviceCtrl._BuildDepotData = HL.Method() << function(self)
    local allInfos = {}
    local inventory = GameInstance.player.inventory
    local depotInChapter = inventory.factoryDepot:GetOrFallback(Utils.getCurrentScope())
    local depot = depotInChapter[Utils.getCurrentChapterId()]
    if not depot then
        return
    end

    for itemId, bundle in pairs(depot.normalItems) do
        if Utils.isPortableDevice(itemId) then
            table.insert(allInfos, self:_CreateDepotItemInfo(itemId, bundle.count, 0))
        end
    end
    for instId, bundle in pairs(depot.instItems) do
        if Utils.isPortableDevice(bundle.id) then
            table.insert(allInfos, self:_CreateDepotItemInfo(bundle.id, bundle.count, instId))
        end
    end

    local sortKeys = UIConst.FAC_DEPOT_SORT_OPTIONS[1].keys
    table.sort(allInfos, Utils.genSortFunction(sortKeys, true))

    local depotData = self.m_invDatas[GROUP_DEPOT]
    for _, info in ipairs(allInfos) do
        table.insert(depotData, {
            group = GROUP_DEPOT,
            item = info,
            itemId = info.id,
            instId = info.instId,
            isTempEmpty = false,
        })
    end

    if self.m_showDepotDragTempEmptySlot then
        table.insert(depotData, {
            group = GROUP_DEPOT,
            item = { id = "", count = 0, },
            itemId = self.m_depotDragTempItemId,
            instId = 0,
            isTempEmpty = true,
            isDepotTempEmpty = true,
        })
    end
end

ChangePortableDeviceCtrl._CreateDepotItemInfo = HL.Method(HL.String, HL.Number, HL.Opt(HL.Number)).Return(HL.Table) <<
    function(self, itemId, count, instId)
        local data = Tables.itemTable:GetValue(itemId)
        local info = {
            id = itemId,
            instId = instId or 0,
            isInst = instId ~= nil and instId > 0,
            count = count,
            maxStackCount = data.maxBackpackStackCount,
            data = data,
            showingType = data.showingType,
            rarity = data.rarity,
            sortId1 = data.sortId1,
            sortId1Neg = -data.sortId1,
            sortId2 = data.sortId2,
            isMissionItem = false,
        }
        info.missionSortId = 1
        info.missionReverseSortId = -1
        return info
    end




ChangePortableDeviceCtrl._TryFocusFirstValidCell = HL.Method() << function(self)
    local targetCell = self.m_getCell(1)
    if targetCell then
        targetCell:SetAsNaviTarget()
    end
end

ChangePortableDeviceCtrl._GetEntryByGlobalLuaIndex = HL.Method(HL.Number).Return(HL.Opt(HL.Table)) <<
    function(self, globalLuaIndex)
        local bagCount = #self.m_invDatas[GROUP_BAG]
        if globalLuaIndex <= bagCount then
            return self.m_invDatas[GROUP_BAG][globalLuaIndex]
        end
        local depotLuaIndex = globalLuaIndex - bagCount
        return self.m_invDatas[GROUP_DEPOT][depotLuaIndex]
    end

ChangePortableDeviceCtrl._OnUpdateCell = HL.Method(HL.Any, HL.Number) << function(self, obj, globalCsIndex)
    obj.name = string.format("Cell_%d", globalCsIndex)
    local cell = self.m_getCell(obj)
    local entry = self:_GetEntryByGlobalLuaIndex(LuaIndex(globalCsIndex))
    self:_RefreshCell(cell, entry, globalCsIndex)
    self.view.depotDropMask.transform:SetAsLastSibling()
end

ChangePortableDeviceCtrl._OnUpdateGroupTitle = HL.Method(HL.Any, HL.Number) << function(self, obj, groupCsIndex)
    obj.name = string.format("Title_%d", groupCsIndex)
    local title = self.m_getTitle(obj)
    local groupLuaIndex = LuaIndex(groupCsIndex)
    local titleText
    if groupLuaIndex == GROUP_BAG then
        titleText = Language.LUA_ITEM_BAG_PORTABLE_DEVICE_BAG_TITLE
    else
        local domainId = Utils.getCurDomainId()
        local succ, domainData = Tables.domainDataTable:TryGetValue(domainId)
        titleText = string.format(Language.LUA_ITEM_BAG_PORTABLE_DEVICE_DEPOT_TITLE, domainData.storageName)
    end
    title.text.text = titleText
    self.view.depotDropMask.transform:SetAsLastSibling()
end




ChangePortableDeviceCtrl._IsMoveSourceEntry = HL.Method(HL.Table).Return(HL.Boolean) << function(self, entry)
    if not self.m_moveFrom or not entry then
        return false
    end
    if self.m_moveFrom.source == UIConst.UI_DRAG_DROP_SOURCE_TYPE.ItemBag then
        return entry.group == GROUP_BAG and entry.csIndex == self.m_moveFrom.csIndex
    end
    if self.m_moveFrom.source == UIConst.UI_DRAG_DROP_SOURCE_TYPE.FactoryDepot then
        return entry.group == GROUP_DEPOT and entry.itemId == self.m_moveFrom.itemId
    end
    return false
end

ChangePortableDeviceCtrl._IsInMoveMode = HL.Method().Return(HL.Boolean) << function(self)
    return self.m_moveFrom ~= nil
end

ChangePortableDeviceCtrl._UpdateItemBlockMask = HL.Method(HL.Forward("ItemSlot"), HL.Table) <<
    function(self, cell, entry)
        local inMoveMode = self:_IsInMoveMode()
        local confirmTextId
        if inMoveMode then
            local isMoveSource = self:_IsMoveSourceEntry(entry)
            cell.view.blockMask.gameObject:SetActiveIfNecessary(isMoveSource)
            confirmTextId = isMoveSource and "LUA_ITEM_ACTION_MOVE_IN_ITEM_BAG_PLACE" or "LUA_ITEM_ACTION_MOVE_IN_ITEM_BAG_SWAP"
        else
            cell.view.blockMask.gameObject:SetActiveIfNecessary(false)
            confirmTextId = "key_hint_item_open_action_menu"
        end
        InputManagerInst:SetBindingText(cell.item.view.button.hoverConfirmBindingId, Language[confirmTextId])
    end

ChangePortableDeviceCtrl._RefreshCell = HL.Method(HL.Forward("ItemSlot"), HL.Table, HL.Number)
        << function(self, cell, entry, globalCsIndex)


    local itemBundle = entry.item
    local itemId = itemBundle.id
    local isEmpty = string.isEmpty(itemId)
    local clickableEvenEmpty = self.m_moveFrom and entry.group == GROUP_BAG
    cell:InitItemSlot(itemBundle, function()
        self:_OnClickEntry(entry, cell)
    end, nil, clickableEvenEmpty)
    cell.item.fromDepot = entry.group == GROUP_DEPOT
    cell.item.slotIndex = entry.csIndex
    cell.item:ShowPickUpLogo(true)
    cell.item:SetEnableHoverTips(not DeviceInfo.usingController)
    if DeviceInfo.usingController then
        cell.item.view.button.onIsNaviTargetChanged = function(active)
            self:_RefreshCellColoredSlotBG(cell, entry, active)
            if not active then
                return
            end
            self.m_suppressTipsUntilRefresh = false
            if self.m_moveFrom then
                self.view.moveHintItem.view.followerObject.target = cell.gameObject.transform
            else
                self:_ShowTipsOnNaviTarget(cell, itemBundle.id, false)
            end
        end

        if not isEmpty or clickableEvenEmpty then
            cell.item.view.button:ChangeActionOnSetNaviTarget(CS.Beyond.Input.ActionOnSetNaviTarget.PressConfirmTriggerOnClick)
        else
            cell.item.view.button:ChangeActionOnSetNaviTarget(CS.Beyond.Input.ActionOnSetNaviTarget.None)
        end

        local enableControllerNavi = true
        if self.m_moveFrom then

            if entry.group == GROUP_DEPOT then

                enableControllerNavi = false
            elseif self.m_moveFrom.source == UIConst.UI_DRAG_DROP_SOURCE_TYPE.ItemBag and self.m_moveFrom.csIndex < self.m_itemBag.coloredSlotNum then

                enableControllerNavi = true
            else

                enableControllerNavi = entry.csIndex < self.m_itemBag.coloredSlotNum
            end
        end
        cell.item.view.button.enabled = enableControllerNavi
        cell.item.view.button.enableControllerNavi = enableControllerNavi
    end

    self:_RefreshHoverBindings(cell, entry, isEmpty)
    if DeviceInfo.usingController and cell.item.view.button.isNaviTarget then
        if self.m_moveFrom then
            self.view.moveHintItem.view.followerObject.target = cell.gameObject.transform
        else
            self:_ShowTipsOnNaviTarget(cell, itemId, true)
        end
    end


    self:_UpdateItemBlockMask(cell, entry)
    if DeviceInfo.usingController then
        self:_RefreshCellColoredSlotBG(cell, entry, cell.item.view.button.isNaviTarget)
    else
        self:_RefreshCellColoredSlotBG(cell, entry, false)
    end

    local isDropHighlighted = self.m_curDropHighlightGlobalLuaIndex == LuaIndex(globalCsIndex)
    cell:SetDropHighlighted(isDropHighlighted)
    self.view.depotDropMask.transform:SetAsLastSibling()
    cell.view.invalidHint.gameObject:SetActive(entry.isInvalidTemp == true)


    if isEmpty then
        if not clickableEvenEmpty then
            InputManagerInst:ToggleBinding(cell.item.view.button.hoverConfirmBindingId, false)
        end
        if entry.group == GROUP_BAG then
            UIUtils.initUIDropHelper(cell.view.dropItem, {
                acceptTypes = UIConst.ITEM_BAG_DROP_ACCEPT_INFO,
                onDropItem = function(eventData, dragHelper)
                    self:_OnDropToBag(entry, dragHelper)
                end,
                onToggleHighlight = function(active)
                    self:_RefreshCellColoredSlotBG(cell, entry, active)
                end,
            })
        else
            UIUtils.initUIDropHelper(cell.view.dropItem, {
                acceptTypes = UIConst.FACTORY_DEPOT_DROP_ACCEPT_INFO,
                onDropItem = function(eventData, dragHelper)
                    self:_OnDropToDepot(dragHelper)
                end,
            })
        end
        local draggingFromDepot = self.m_curDraggingDragHelper and
                self.m_curDraggingDragHelper.source == UIConst.UI_DRAG_DROP_SOURCE_TYPE.FactoryDepot
        local keepEnabled = not draggingFromDepot or (entry.group == GROUP_BAG and entry.isColoredSlot == true)
        cell.view.nonDrawingGraphic.enabled = keepEnabled
        cell.item.view.nonDrawingGraphic.enabled = keepEnabled
        return
    end

    if entry.group == GROUP_BAG then
        self:_BindBagDragAndDrop(cell, entry, globalCsIndex)
    else
        self:_BindDepotDragAndDrop(cell, entry, globalCsIndex)
    end
    local draggingFromDepot = self.m_curDraggingDragHelper and
            self.m_curDraggingDragHelper.source == UIConst.UI_DRAG_DROP_SOURCE_TYPE.FactoryDepot
    local keepEnabled = not draggingFromDepot or (entry.group == GROUP_BAG and entry.isColoredSlot == true)
    cell.view.nonDrawingGraphic.enabled = keepEnabled
    cell.item.view.nonDrawingGraphic.enabled = keepEnabled
end

ChangePortableDeviceCtrl._RefreshHoverBindings = HL.Method(HL.Forward("ItemSlot"), HL.Table, HL.Boolean) <<
    function(self, cell, entry, isEmpty)
        if isEmpty or self.m_moveFrom then
            return
        end

        local quickDropBindingId = cell.item:AddHoverBinding("common_quick_drop", function()
            self:_QuickDropEntry(entry)
        end)
        local startMoveBindingId = cell.item:AddHoverBinding("inv_item_bag_start_move_item", function()
            self:_EnterMoveModeWithEntry(entry)
        end)
        local quickDropText = entry.isColoredSlot and Language.LUA_ITEM_ACTION_MOVE_TO_ITEM_BAG or
            Language.LUA_ITEM_BAG_PORTABLE_DEVICE_MOVE_TO_COLORED_SLOT
        InputManagerInst:SetBindingText(quickDropBindingId, quickDropText)
        InputManagerInst:SetBindingText(startMoveBindingId, Language.LUA_ITEM_BAG_PORTABLE_DEVICE_START_MOVE_MODE)
    end

ChangePortableDeviceCtrl._ShowTipsOnNaviTarget = HL.Method(HL.Forward("ItemSlot"), HL.String, HL.Opt(HL.Boolean)) <<
    function(self, cell, itemId, forceRefresh)
        if self.m_moveFrom then
            return
        end
        if self.m_suppressTipsUntilRefresh then
            return
        end
        if string.isEmpty(itemId) then
            Notify(MessageConst.HIDE_ITEM_TIPS)
            return
        end
        if cell.item.showingTips then
            if not forceRefresh then
                return
            end
            Notify(MessageConst.HIDE_ITEM_TIPS)
            cell.item:_OnTipsClosed()
        end
        cell.item:ShowTips({
            tipsPosType = UIConst.UI_TIPS_POS_TYPE.RightTop,
            padding = { left = self.view.scrollList.transform.rect.size.x, bottom = 100, },
            isSideTips = true,
        })
    end

ChangePortableDeviceCtrl._QuickDropEntry = HL.Method(HL.Table) << function(self, entry)
    if not entry then
        return
    end
    if entry.isColoredSlot then
        self:_MoveBagEntryToNormalBag(entry)
        return
    end
    self:_MoveEntryToColoredSlot(entry)
end

ChangePortableDeviceCtrl._QueueQuickDropFocus = HL.Method(HL.Table, HL.Opt(HL.Table)) <<
    function(self, target, fallback)
        self.m_pendingFocusRequest = { target = target, fallback = fallback }
    end

ChangePortableDeviceCtrl._TryFocusPendingQuickDropTarget = HL.Method() << function(self)
    local req = self.m_pendingFocusRequest
    if not req then
        return
    end
    self.m_pendingFocusRequest = nil
    if self:_TryFocusLocator(req.target) then
        return
    end
    if req.fallback then
        self:_TryFocusLocator(req.fallback)
    end
end

ChangePortableDeviceCtrl._MoveEntryToColoredSlot = HL.Method(HL.Table) << function(self, entry)
    local targetEntry = self:_FindColoredSlotTargetEntry()
    if not targetEntry then
        return
    end
    local targetCsIndex = targetEntry.csIndex
    if entry.group == GROUP_BAG then
        self:_QueueQuickDropFocus({ group = GROUP_BAG, csIndex = targetCsIndex })
        self:_DoMoveInItemBag(Utils.getCurrentScope(), entry.csIndex, targetCsIndex)
    elseif entry.group == GROUP_DEPOT then
        local itemId = entry.itemId or (entry.item and entry.item.id) or ""
        local count = math.min((entry.item and entry.item.count) or 0, (entry.item and entry.item.maxStackCount) or 0)
        if string.isEmpty(itemId) or count <= 0 then
            return
        end
        self:_QueueQuickDropFocus({ group = GROUP_DEPOT, itemId = itemId },
            { group = GROUP_BAG, csIndex = targetCsIndex })
        self:_DoDepotToBagWithTargetDisplace(Utils.getCurrentScope(), Utils.getCurrentChapterId(), targetCsIndex, itemId,
            count)
    end
end

ChangePortableDeviceCtrl._OnClickEntry = HL.Method(HL.Table, HL.Forward("ItemSlot")) << function(self, entry, cell)
    local itemId = entry.item.id
    if self.m_moveFrom then
        if entry.group ~= GROUP_BAG then
            return
        end
        local targetIndex = self:_ResolveMoveTargetIndex(entry)
        if targetIndex < 0 then
            return
        end

        local scope = Utils.getCurrentScope()
        local mf = self.m_moveFrom
        if mf.source == UIConst.UI_DRAG_DROP_SOURCE_TYPE.ItemBag and mf.csIndex >= 0 then
            self:_DoMoveInItemBag(scope, mf.csIndex, targetIndex)
            self:_ToggleMoveMode(false, nil, false)
        elseif mf.source == UIConst.UI_DRAG_DROP_SOURCE_TYPE.FactoryDepot and
            not string.isEmpty(mf.itemId) and mf.count > 0 then
            self:_DoDepotToBagWithTargetDisplace(scope, Utils.getCurrentChapterId(), targetIndex,
                mf.itemId, mf.count)
            self:_ToggleMoveMode(false, nil, false)
        end
        return
    end

    if string.isEmpty(itemId) then
        return
    end

    cell.item:Read()

    if DeviceInfo.usingController then
        cell.item:ShowActionMenu()
        return
    end

    cell.item:ShowTips({
        tipsPosType = UIConst.UI_TIPS_POS_TYPE.RightTop,
        padding = { left = self.view.scrollList.transform.rect.size.x, bottom = 100, },
        isSideTips = DeviceInfo.usingController,
    })
end





ChangePortableDeviceCtrl._OnUpdateGroupBG = HL.Method(HL.Any, HL.Number) << function(self, obj, groupCsIndex)
    obj.name = string.format("GroupBG_%d", groupCsIndex)
    local emptyNode = obj.transform:Find("EmptyNode")
    local groupIndex = LuaIndex(groupCsIndex)
    if groupIndex == GROUP_BAG then
        emptyNode.gameObject:SetActive(false)
        return
    end

    local depotIsEmpty = #self.m_invDatas[GROUP_DEPOT] == 0
    emptyNode.gameObject:SetActive(depotIsEmpty)

    self.view.depotDropMask.followerObject.target = obj.transform
    self.view.depotDropMask.transform.sizeDelta = obj.transform.sizeDelta + Vector2(0, depotIsEmpty and 130 or 0)
    self.view.depotDropMask.transform:SetAsLastSibling()
end




ChangePortableDeviceCtrl._BindBagDragAndDrop = HL.Method(HL.Forward("ItemSlot"), HL.Table, HL.Number) <<
    function(self, cell, entry, globalCsIndex)
        local itemBundle = entry.item
        local itemId = itemBundle.id
        local data = Tables.itemTable:GetValue(itemId)

        local dragHelper = UIUtils.initUIDragHelper(cell.view.dragItem, {
            source = UIConst.UI_DRAG_DROP_SOURCE_TYPE.ItemBag,
            type = data.type,
            csIndex = entry.csIndex,
            itemId = itemId,
            count = itemBundle.count,
            instId = itemBundle.instId,
            onBeginDrag = function()
                self.view.scrollList:SetCellCanCache(globalCsIndex, false)
                cell.item:Read()
            end,
            onEndDrag = function()
                self:_ResetShowingCellsCanCache()
            end,
        })
        cell:InitPressDrag()
        UIUtils.initUIDropHelper(cell.view.dropItem, {
            acceptTypes = UIConst.ITEM_BAG_DROP_ACCEPT_INFO,
            onDropItem = function(eventData, helper)
                self:_OnDropToBag(entry, helper)
            end,
        })

        cell.item.actionMenuArgs = {}
        cell.item.customChangeActionMenuFunc = function(actionMenuInfos)
            self:_CustomizePortableDeviceActionMenuInfos(actionMenuInfos, entry, dragHelper)
        end
    end

ChangePortableDeviceCtrl._BindDepotDragAndDrop = HL.Method(HL.Forward("ItemSlot"), HL.Table, HL.Number) <<
    function(self, cell, entry, globalCsIndex)
        local info = entry.item
        local data = Tables.itemTable:GetValue(entry.itemId)
        local moveCount = math.min(info.count or 0, info.maxStackCount or 0)
        local dragHelper = UIUtils.initUIDragHelper(cell.view.dragItem, {
            source = UIConst.UI_DRAG_DROP_SOURCE_TYPE.FactoryDepot,
            type = data.type,
            itemId = entry.itemId,
            instId = entry.instId,
            count = moveCount,
            onBeginDrag = function()
                self.view.scrollList:SetCellCanCache(globalCsIndex, false)
                cell.item:Read()
            end,
            onEndDrag = function()
                self:_ResetShowingCellsCanCache()
            end,
        })
        cell:InitPressDrag()
        UIUtils.initUIDropHelper(cell.view.dropItem, {
            acceptTypes = UIConst.FACTORY_DEPOT_DROP_ACCEPT_INFO,
            onDropItem = function(eventData, helper)
                self:_OnDropToDepot(helper)
            end,
        })

        cell.item.actionMenuArgs = {}
        cell.item.customChangeActionMenuFunc = function(actionMenuInfos)
            self:_CustomizePortableDeviceActionMenuInfos(actionMenuInfos, entry, dragHelper)
        end
    end




ChangePortableDeviceCtrl._DoMoveInItemBag = HL.Method(HL.Any, HL.Number, HL.Number) <<
    function(self, scope, fromIndex, toIndex)
        GameInstance.player.inventory:MoveInItemBag(scope, fromIndex, toIndex)
    end

ChangePortableDeviceCtrl._DoDepotToBagWithTargetDisplace = HL.Method(HL.Any, HL.Number, HL.Number, HL.String, HL.Number) <<
    function(self, scope, chapterId, targetIndex, itemId, count)
        local inventory = GameInstance.player.inventory
        local targetBundle = self.m_itemBag.slots[targetIndex]
        if not string.isEmpty(targetBundle.id) then

            local emptyCsIndex = self:_GetFirstHiddenEmptyBagSlotIndex()
            if emptyCsIndex >= 0 then
                inventory:MoveInItemBag(scope, targetIndex, emptyCsIndex)
                local targetItemData = Tables.itemTable[targetBundle.id]
                Notify(MessageConst.SHOW_TOAST,
                    string.format(Language.LUA_ITEM_BAG_PORTABLE_DEVICE_REPLACED_TO_BAG, targetItemData.name))
            else
                inventory:ItemBagMoveToFactoryDepot(scope, chapterId, targetIndex, CS.Proto.ITEM_MOVE_MODE.Normal)
                local targetItemData = Tables.itemTable[targetBundle.id]
                Notify(MessageConst.SHOW_TOAST,
                    string.format(Language.LUA_ITEM_BAG_PORTABLE_DEVICE_TARGET_BACK_TO_DEPOT, targetItemData.name))
            end
        end
        inventory:FactoryDepotMoveToItemBag(scope, chapterId, itemId, count, targetIndex)
    end
ChangePortableDeviceCtrl._OnDropToBag = HL.Method(HL.Table, HL.Forward('UIDragHelper')) <<
    function(self, targetEntry, dragHelper)
        if self.m_dropCommandHandledInCurrentDrag then
            return
        end
        local scope = Utils.getCurrentScope()
        local chapterId = Utils.getCurrentChapterId()
        local targetIndex = self:_ResolveMoveTargetIndex(targetEntry)
        if targetIndex < 0 then
            if targetEntry and targetEntry.isInvalidTemp then
                Notify(MessageConst.SHOW_TOAST, Language.LUA_ITEM_BAG_PORTABLE_DEVICE_NO_EXTRA_SLOT)
            end
            return
        end

        if dragHelper.source == UIConst.UI_DRAG_DROP_SOURCE_TYPE.ItemBag then
            self.m_dropCommandHandledInCurrentDrag = true
            self:_DoMoveInItemBag(scope, dragHelper.info.csIndex, targetIndex)
            UIUtils.playItemDropAudio(dragHelper:GetId())
            return
        end

        if dragHelper.source == UIConst.UI_DRAG_DROP_SOURCE_TYPE.FactoryDepot then
            self.m_dropCommandHandledInCurrentDrag = true
            self:_DoDepotToBagWithTargetDisplace(scope, chapterId, targetIndex, dragHelper:GetId(), dragHelper:GetCount())
        end
    end

ChangePortableDeviceCtrl._OnDropToDepot = HL.Method(HL.Forward('UIDragHelper')) << function(self, dragHelper)
    if self.m_dropCommandHandledInCurrentDrag then
        return
    end
    if dragHelper.source ~= UIConst.UI_DRAG_DROP_SOURCE_TYPE.ItemBag then
        return
    end
    local mode = CS.Proto.ITEM_MOVE_MODE.AutoBatch
    self.m_dropCommandHandledInCurrentDrag = true
    GameInstance.player.inventory:ItemBagMoveToFactoryDepot(Utils.getCurrentScope(), Utils.getCurrentChapterId(),
        dragHelper.info.csIndex, mode)
end

ChangePortableDeviceCtrl._ResolveMoveTargetIndex = HL.Method(HL.Table).Return(HL.Number) << function(self, targetEntry)
    if not targetEntry then
        return -1
    end
    if targetEntry.isInvalidTemp then
        return -1
    end
    return targetEntry.csIndex
end




ChangePortableDeviceCtrl._ToggleMoveMode = HL.Method(HL.Boolean, HL.Opt(HL.Table, HL.Boolean))
        << function(self, active, moveFrom, isCancel)

    if (self.m_moveFrom ~= nil) == active then
        return
    end
    local prevMoveFrom = self.m_moveFrom
    if active then
        self.m_moveFrom = moveFrom
        local moveItemBundle
        if moveFrom.source == UIConst.UI_DRAG_DROP_SOURCE_TYPE.ItemBag and moveFrom.csIndex >= 0 then
            moveItemBundle = self.m_itemBag.slots[moveFrom.csIndex]
        elseif moveFrom.source == UIConst.UI_DRAG_DROP_SOURCE_TYPE.FactoryDepot and not string.isEmpty(moveFrom.itemId) then
            moveItemBundle = { id = moveFrom.itemId, count = moveFrom.count or 0 }
        end
        self.view.moveHintItem:InitItem(moveItemBundle)

        local isFromColoredSlot = moveFrom.source == UIConst.UI_DRAG_DROP_SOURCE_TYPE.ItemBag and moveFrom.csIndex < self.m_itemBag.coloredSlotNum
        self.view.coloredSlotsAreaHintForMoveMode.gameObject:SetActive(not isFromColoredSlot)
    else
        self.m_moveFrom = nil
        self.view.moveHintItem.view.followerObject.target = nil
        self.view.coloredSlotsAreaHintForMoveMode.gameObject:SetActive(false)
    end
    self.view.moveHintItem.gameObject:SetActive(active)
    InputManagerInst:ToggleBinding(self.m_cancelMoveBindingId, active)
    if active then
        self:_RefreshMoveSourceCell()
    end
    if not active and isCancel then
        self.m_suppressTipsUntilRefresh = true
    end

    self:_RefreshAll(false)

    if not active and isCancel then
        self.m_suppressTipsUntilRefresh = false
        self:_TryFocusMoveSource(prevMoveFrom)
    end
end

ChangePortableDeviceCtrl._TryFocusMoveSource = HL.Method(HL.Opt(HL.Table)) <<
    function(self, moveFrom)
        if not moveFrom then
            return
        end
        if moveFrom.source == UIConst.UI_DRAG_DROP_SOURCE_TYPE.ItemBag then
            self:_TryFocusLocator({ group = GROUP_BAG, csIndex = moveFrom.csIndex })
        elseif moveFrom.source == UIConst.UI_DRAG_DROP_SOURCE_TYPE.FactoryDepot then
            self:_TryFocusLocator({ group = GROUP_DEPOT, itemId = moveFrom.itemId })
        end
    end

ChangePortableDeviceCtrl._RefreshMoveSourceCell = HL.Method() << function(self)
    local mf = self.m_moveFrom
    if not mf then
        return
    end
    local sourceEntry
    if mf.source == UIConst.UI_DRAG_DROP_SOURCE_TYPE.ItemBag and mf.csIndex >= 0 then
        local bagLuaIndex = self:_GetBagLuaIndexByCsIndex(mf.csIndex, true)
        if bagLuaIndex > 0 then
            sourceEntry = self.m_invDatas[GROUP_BAG][bagLuaIndex]
        end
    elseif mf.source == UIConst.UI_DRAG_DROP_SOURCE_TYPE.FactoryDepot then
        for _, entry in ipairs(self.m_invDatas[GROUP_DEPOT]) do
            if entry.itemId == mf.itemId then
                sourceEntry = entry
                break
            end
        end
    end
    if not sourceEntry then
        return
    end
    local globalLuaIndex = self:_GetGlobalLuaIndexByEntry(sourceEntry)
    if globalLuaIndex <= 0 then
        return
    end
    local obj = self.view.scrollList:Get(CSIndex(globalLuaIndex))
    if not obj then
        return
    end
    local cell = self.m_getCell(obj)
    if not cell then
        return
    end
    self:_RefreshCell(cell, sourceEntry, CSIndex(globalLuaIndex))
    self.view.depotDropMask.transform:SetAsLastSibling()
end
ChangePortableDeviceCtrl._GetFirstHiddenEmptyBagSlotIndex = HL.Method().Return(HL.Number) << function(self)
    for csIndex = self.m_itemBag.coloredSlotNum, self.m_itemBag.slots.Count - 1 do
        local itemBundle = self.m_itemBag.slots[csIndex]
        if string.isEmpty(itemBundle.id) and not self:_IsBagSlotShownInNormalMode(csIndex) then
            return csIndex
        end
    end
    return -1
end

ChangePortableDeviceCtrl._IsBagSlotShownInNormalMode = HL.Method(HL.Number).Return(HL.Boolean) << function(self, csIndex)
    if csIndex < self.m_itemBag.coloredSlotNum then
        return true
    end
    local itemBundle = self.m_itemBag.slots[csIndex]
    return not string.isEmpty(itemBundle.id) and Utils.isPortableDevice(itemBundle.id)
end

ChangePortableDeviceCtrl._TryFocusBagSlot = HL.Method(HL.Number) << function(self, csIndex)
    self:_TryFocusLocator({ group = GROUP_BAG, csIndex = csIndex })
end

ChangePortableDeviceCtrl._TryFocusLocator = HL.Method(HL.Table).Return(HL.Boolean) << function(self, locator)
    if not locator then
        return false
    end
    local globalLuaIndex
    if locator.group == GROUP_BAG then
        if not locator.csIndex or locator.csIndex < 0 then
            return false
        end
        local bagLuaIndex = self:_GetBagLuaIndexByCsIndex(locator.csIndex, true)
        if bagLuaIndex <= 0 then
            return false
        end
        globalLuaIndex = bagLuaIndex
    elseif locator.group == GROUP_DEPOT then
        if not locator.itemId or string.isEmpty(locator.itemId) then
            return false
        end
        for _, entry in ipairs(self.m_invDatas[GROUP_DEPOT]) do
            if entry.itemId == locator.itemId then
                globalLuaIndex = self:_GetGlobalLuaIndexByEntry(entry)
                break
            end
        end
        if not globalLuaIndex or globalLuaIndex <= 0 then
            return false
        end
    else
        return false
    end
    local globalCsIndex = CSIndex(globalLuaIndex)
    local obj = self.view.scrollList:Get(globalCsIndex)
    if not obj then
        self.view.scrollList:ScrollToIndex(globalCsIndex, true)
        obj = self.view.scrollList:Get(globalCsIndex)
    end
    if not obj then
        return false
    end
    local cell = self.m_getCell(obj)
    if cell and cell.SetAsNaviTarget then
        cell:SetAsNaviTarget()
        return true
    end
    return false
end

ChangePortableDeviceCtrl._GetVisibleBagCellByCsIndex = HL.Method(HL.Number).Return(HL.Opt(HL.Forward("ItemSlot"))) <<
    function(self, csIndex)
        local bagLuaIndex = self:_GetBagLuaIndexByCsIndex(csIndex, true)
        if bagLuaIndex <= 0 then
            return nil
        end
        local obj = self.view.scrollList:Get(CSIndex(bagLuaIndex))
        if not obj then
            return nil
        end
        return self.m_getCell(obj)
    end

ChangePortableDeviceCtrl._CaptureVisibleColoredSlotActiveStates = HL.Method().Return(HL.Table) << function(self)
    local states = {}
    local itemBag = self.m_itemBag
    if not itemBag then
        return states
    end
    for csIndex = 0, itemBag.coloredSlotNum - 1 do
        local cell = self:_GetVisibleBagCellByCsIndex(csIndex)
        if cell then
            states[csIndex] = cell.m_colorSlotActivated == true
        end
    end
    return states
end

ChangePortableDeviceCtrl._EnterMoveModeWithEntry = HL.Method(HL.Table) << function(self, entry)
    Notify(MessageConst.HIDE_ITEM_TIPS)
    local moveFrom
    if entry.group == GROUP_BAG then
        moveFrom = { source = UIConst.UI_DRAG_DROP_SOURCE_TYPE.ItemBag, csIndex = entry.csIndex, itemId = self.m_itemBag.slots[entry.csIndex].id }
    elseif entry.group == GROUP_DEPOT then
        local itemId = entry.itemId or (entry.item and entry.item.id) or ""
        local count = math.min((entry.item and entry.item.count) or 0, (entry.item and entry.item.maxStackCount) or 0)
        moveFrom = { source = UIConst.UI_DRAG_DROP_SOURCE_TYPE.FactoryDepot, itemId = itemId, count = count }
    end
    self:_ToggleMoveMode(true, moveFrom, false)
    self:_TryFocusPickupMoveTarget()
end

ChangePortableDeviceCtrl._HandlePortableDeviceActivationAfterBagChanged = HL.Method(HL.Any, HL.Table) <<
    function(self, changedIndexes, lastColoredSlotStates)
        local itemBag = self.m_itemBag
        if not itemBag or not changedIndexes then
            return
        end

        local changeIndexDic = {}
        local needUpdateColoredSlot = false
        for _, slotIndex in pairs(changedIndexes) do
            if slotIndex < itemBag.coloredSlotNum then
                needUpdateColoredSlot = true
                local itemId = itemBag[slotIndex].id
                if not string.isEmpty(itemId) then
                    local isColorActive, isLowerLv = itemBag:IsColoredSlotActive(itemId, slotIndex)
                    if isColorActive then
                        local cell = self:_GetVisibleBagCellByCsIndex(slotIndex)
                        if cell then
                            cell:PlayColoredSlotActivatedAnimation()
                        end
                    elseif isLowerLv then
                        local iData = Tables.itemTable[itemId]
                        Notify(MessageConst.SHOW_TOAST,
                            string.format(Language.LUA_ITEM_BAG_PORTABLE_DEVICE_NOT_ACTIVE, iData.name))
                    end
                end
            end
            changeIndexDic[slotIndex] = true
        end

        if not needUpdateColoredSlot then
            return
        end

        for slotIndex = 0, itemBag.coloredSlotNum - 1 do
            if not changeIndexDic[slotIndex] then
                local cell = self:_GetVisibleBagCellByCsIndex(slotIndex)
                if cell and cell.view then
                    local lastIsActive = lastColoredSlotStates[slotIndex] == true
                    if not lastIsActive and cell.m_colorSlotActivated then

                        cell:PlayColoredSlotActivatedAnimation()
                    end
                end
                local itemId = itemBag[slotIndex].id
                if Utils.isPortableDevice(itemId) then
                    local pdData = Tables.itemPortableDeviceTable[itemId]
                    for k = 0, itemBag.coloredSlotNum - 1 do
                        if k ~= slotIndex and changeIndexDic[k] then
                            local otherItem = itemBag.slots[k]
                            if Utils.isPortableDevice(otherItem.id) then
                                local otherPdData = Tables.itemPortableDeviceTable[otherItem.id]
                                if otherPdData.type == pdData.type and pdData.isMainDevice == otherPdData.isMainDevice and
                                    otherPdData.lv > pdData.lv then
                                    local iData = Tables.itemTable[itemId]
                                    Notify(MessageConst.SHOW_TOAST,
                                        string.format(Language.LUA_ITEM_BAG_PORTABLE_DEVICE_NOT_ACTIVE, iData.name))
                                    break
                                end
                            end
                        end
                    end
                end
            end
        end
    end

ChangePortableDeviceCtrl._CustomizePortableDeviceActionMenuInfos = HL.Method(HL.Table, HL.Table,
        HL.Forward('UIDragHelper')) <<
    function(self, actionMenuInfos, entry, dragHelper)
        for i = #actionMenuInfos, 1, -1 do
            table.remove(actionMenuInfos, i)
        end
        local isColoredSlot = entry.isColoredSlot
        if isColoredSlot then
            table.insert(actionMenuInfos, {
                text = Language.LUA_ITEM_ACTION_MOVE_TO_ITEM_BAG,
                action = function()
                    Notify(MessageConst.HIDE_ITEM_TIPS, { skipAnim = true })
                    self:_QuickDropEntry(entry)
                end,
                beforeCloseAction = function()

                    self.m_suppressTipsUntilRefresh = true
                end,
            })
        else
            table.insert(actionMenuInfos, {
                text = Language.LUA_ITEM_BAG_PORTABLE_DEVICE_MOVE_TO_COLORED_SLOT,
                action = function()
                    Notify(MessageConst.HIDE_ITEM_TIPS, { skipAnim = true })
                    self:_MoveEntryToColoredSlot(entry)
                end,
                beforeCloseAction = function()

                    self.m_suppressTipsUntilRefresh = true
                end,
            })
        end
        if entry.group == GROUP_BAG then
            table.insert(actionMenuInfos, {
                text = Language.LUA_ITEM_ACTION_MOVE_TO_DEPOT,
                action = function()
                    Notify(MessageConst.HIDE_ITEM_TIPS, { skipAnim = true })
                    self.m_dropCommandHandledInCurrentDrag = false
                    local oldLuaIndex = self:_GetBagLuaIndexByCsIndex(entry.csIndex, true)
                    self:_OnDropToDepot(dragHelper)
                    if not isColoredSlot and oldLuaIndex == #self.m_invDatas[GROUP_BAG] then
                        self:_TryFocusBagSlot(self.m_invDatas[GROUP_BAG][oldLuaIndex - 1].csIndex)
                    end
                end,
                beforeCloseAction = function()

                    self.m_suppressTipsUntilRefresh = true
                end,
            })
        end
        table.insert(actionMenuInfos, {
            text = Language.LUA_ITEM_BAG_PORTABLE_DEVICE_START_MOVE_MODE,
            action = function()
                self:_EnterMoveModeWithEntry(entry)
                self.m_suppressTipsUntilRefresh = false
            end,
            beforeCloseAction = function()

                self.m_suppressTipsUntilRefresh = true
            end,
        })
    end

ChangePortableDeviceCtrl._FindColoredSlotTargetEntry = HL.Method().Return(HL.Opt(HL.Table)) << function(self)
    local bagData = self.m_invDatas[GROUP_BAG]
    for _, entry in ipairs(bagData) do
        if entry.isColoredSlot then
            local id = entry.item.id
            if string.isEmpty(id) then
                return entry
            end
        end
    end
    return bagData[1]
end

ChangePortableDeviceCtrl._MoveBagEntryToNormalBag = HL.Method(HL.Table) << function(self, entry)
    local targetIndex = self:_GetFirstHiddenEmptyBagSlotIndex()
    if targetIndex < 0 then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_ITEM_BAG_PORTABLE_DEVICE_NO_EXTRA_SLOT)
        return
    end
    self:_QueueQuickDropFocus({ group = GROUP_BAG, csIndex = entry.csIndex })
    GameInstance.player.inventory:MoveInItemBag(Utils.getCurrentScope(), entry.csIndex, targetIndex)
end

ChangePortableDeviceCtrl._TryFocusPickupMoveTarget = HL.Method() << function(self)
    local bagData = self.m_invDatas[GROUP_BAG]
    if not bagData or #bagData <= 0 then
        return
    end

    for _, entry in ipairs(bagData) do
        local itemId = entry.item and entry.item.id or ""
        if entry.isColoredSlot and string.isEmpty(itemId) then
            self:_TryFocusBagSlot(entry.csIndex)
            return
        end
    end

    for _, entry in ipairs(bagData) do
        local itemId = entry.item and entry.item.id or ""
        if not string.isEmpty(itemId) and not Utils.isPortableDevice(itemId) then
            self:_TryFocusBagSlot(entry.csIndex)
            return
        end
    end

    self:_TryFocusBagSlot(bagData[1].csIndex)
end
ChangePortableDeviceCtrl._GetBagLuaIndexByCsIndex = HL.Method(HL.Number, HL.Opt(HL.Boolean)).Return(HL.Number) <<
    function(self, csIndex, includeTemp)
        local bagData = self.m_invDatas[GROUP_BAG]
        for luaIndex, entry in ipairs(bagData) do
            if entry.csIndex == csIndex and (includeTemp or not entry.isTempEmpty) then
                return luaIndex
            end
        end
        return -1
    end






ChangePortableDeviceCtrl._FindAndHighlightForDrop = HL.Method(HL.Number) << function(self, groupIndex)
    local dragHelper = self.m_curDraggingDragHelper
    if not dragHelper then
        return
    end
    local itemId = dragHelper:GetId()
    if string.isEmpty(itemId) then
        return
    end

    local entry
    if groupIndex == GROUP_BAG then
        entry = self:_FindBagDropTargetEntry(itemId)
    elseif groupIndex == GROUP_DEPOT then
        entry = self:_FindDepotDropTargetEntry(itemId)
    end
    if not entry then
        self:_CancelDropHighlight()
        return
    end

    self:_CancelDropHighlight(false)
    local globalLuaIndex = self:_GetGlobalLuaIndexByEntry(entry)
    if globalLuaIndex <= 0 then
        return
    end
    self.m_curDropHighlightGlobalLuaIndex = globalLuaIndex
    self:_SetDropHighlightByGlobalLuaIndex(globalLuaIndex, true)
end

ChangePortableDeviceCtrl._CancelDropHighlight = HL.Method(HL.Opt(HL.Boolean)) << function(self, clearDepotTemp)
    if clearDepotTemp == nil then
        clearDepotTemp = true
    end
    local oldIndex = self.m_curDropHighlightGlobalLuaIndex
    self.m_curDropHighlightGlobalLuaIndex = -1
    if oldIndex > 0 then
        self:_SetDropHighlightByGlobalLuaIndex(oldIndex, false)
    end
    if clearDepotTemp then
        self:_TryClearDepotDragTempEntry(true)
    end
end

ChangePortableDeviceCtrl._SetDropHighlightByGlobalLuaIndex = HL.Method(HL.Number, HL.Boolean) <<
    function(self, globalLuaIndex, active)
        local globalCsIndex = CSIndex(globalLuaIndex)
        local obj = self.view.scrollList:Get(globalCsIndex)
        if not obj then
            self.view.scrollList:ScrollToIndex(globalCsIndex, true)
            obj = self.view.scrollList:Get(globalCsIndex)
        end
        if not obj then
            return
        end

        local cell = self.m_getCell(obj)
        local entry = self:_GetEntryByGlobalLuaIndex(globalLuaIndex)
        if not cell or not entry then
            return
        end
        cell:SetDropHighlighted(active)
        self.view.depotDropMask.transform:SetAsLastSibling()

        if entry.group == GROUP_BAG then
            local itemId = entry.item and entry.item.id or ""
            local draggingHelper = active and self.m_curDraggingDragHelper or nil
            if draggingHelper then
                itemId = draggingHelper:GetId()
            end
            cell:UpdateSlotColorBGByItemId(self.m_itemBag, entry.csIndex, itemId, active)
        end
    end

ChangePortableDeviceCtrl._FindBagDropTargetEntry = HL.Method(HL.String).Return(HL.Opt(HL.Table)) <<
    function(self, itemId)
        local bagData = self.m_invDatas[GROUP_BAG]
        for _, entry in ipairs(bagData) do
            local id = entry.item and entry.item.id or ""
            if string.isEmpty(id) or id == itemId then
                return entry
            end
        end
        local fallbackCsIndex = self.m_itemBag:GetFirstValidSlotIndex(itemId)
        if fallbackCsIndex >= 0 then
            local luaIndex = self:_GetBagLuaIndexByCsIndex(fallbackCsIndex, true)
            if luaIndex > 0 then
                return bagData[luaIndex]
            end
        end
        return nil
    end

ChangePortableDeviceCtrl._FindDepotDropTargetEntry = HL.Method(HL.String).Return(HL.Opt(HL.Table)) <<
    function(self, itemId)
        local depotData = self.m_invDatas[GROUP_DEPOT]
        for _, entry in ipairs(depotData) do
            if entry.itemId == itemId then
                return entry
            end
        end
        if self.m_showDepotDragTempEmptySlot and self.m_depotDragTempItemId ~= itemId then
            if not self:_TryRemoveDepotDragTempEntryFast() then
                self.m_showDepotDragTempEmptySlot = false
                self.m_depotDragTempItemId = ""
            end
        end
        if not self.m_showDepotDragTempEmptySlot or self.m_depotDragTempItemId ~= itemId then
            if not self:_TryAppendDepotDragTempEntryFast(itemId) then
                self.m_showDepotDragTempEmptySlot = true
                self.m_depotDragTempItemId = itemId
                self:_RefreshAll(false)
            end
            depotData = self.m_invDatas[GROUP_DEPOT]
        end
        for _, entry in ipairs(depotData) do
            if entry.isDepotTempEmpty then
                return entry
            end
        end
        return nil
    end

ChangePortableDeviceCtrl._GetGlobalLuaIndexByEntry = HL.Method(HL.Table).Return(HL.Number) << function(self, targetEntry)
    if not targetEntry then
        return -1
    end
    local bagData = self.m_invDatas[GROUP_BAG]
    for i, entry in ipairs(bagData) do
        if entry == targetEntry then
            return i
        end
    end
    local depotData = self.m_invDatas[GROUP_DEPOT]
    for i, entry in ipairs(depotData) do
        if entry == targetEntry then
            return #bagData + i
        end
    end
    return -1
end
ChangePortableDeviceCtrl._RefreshNonDrawingGraphicEnabledForCurrentDrag = HL.Method() << function(self)
    local draggingFromDepot = self.m_curDraggingDragHelper and
        self.m_curDraggingDragHelper.source == UIConst.UI_DRAG_DROP_SOURCE_TYPE.FactoryDepot
    self.view.scrollList:UpdateShowingCells(function(globalCsIndex, obj)
        local cell = self.m_getCell(obj)
        local entry = self:_GetEntryByGlobalLuaIndex(LuaIndex(globalCsIndex))
        local keepEnabled = not draggingFromDepot or (entry.group == GROUP_BAG and entry.isColoredSlot == true)
        cell.view.nonDrawingGraphic.enabled = keepEnabled
        cell.item.view.nonDrawingGraphic.enabled = keepEnabled
    end)
end




ChangePortableDeviceCtrl._RefreshColoredSlotDragState = HL.Method(HL.Boolean) << function(self, active)
    self.view.scrollList:UpdateShowingCells(function(globalCsIndex, obj)
        local globalLuaIndex = LuaIndex(globalCsIndex)
        local entry = self:_GetEntryByGlobalLuaIndex(globalLuaIndex)
        if not entry or entry.group ~= GROUP_BAG or not entry.isColoredSlot then
            return
        end
        local cell = self.m_getCell(obj)
        cell:UpdateSlotColorBGByItemId(self.m_itemBag, entry.csIndex, entry.item.id, active)
    end)
end

ChangePortableDeviceCtrl._RefreshCellColoredSlotBG = HL.Method(HL.Forward("ItemSlot"), HL.Table, HL.Boolean) << function(self, cell, entry, cellIsDropTarget)
    if entry.group == GROUP_DEPOT then
        cell:UpdateSlotColorBGByItemId(self.m_itemBag, -1, "", false)
        return
    end
    local isInDrop = self.m_curDraggingDragHelper ~= nil or self.m_moveFrom ~= nil
    local itemId
    if isInDrop and cellIsDropTarget then
        if self.m_moveFrom then
            itemId = self.m_moveFrom.itemId
        else
            itemId = self.m_curDraggingDragHelper:GetId()
        end
    else
        itemId = entry.item.id
    end

    local showDropHint = isInDrop
    if self.m_moveFrom and entry.isColoredSlot and not Utils.isPortableDevice(self.m_moveFrom.itemId) then
        showDropHint = false
    end
    cell:UpdateSlotColorBGByItemId(self.m_itemBag, entry.csIndex, itemId, showDropHint)
end





ChangePortableDeviceCtrl._TryClearDepotDragTempEntry = HL.Method(HL.Opt(HL.Boolean)) << function(self, refresh)
    if refresh == nil then
        refresh = true
    end
    if not self.m_showDepotDragTempEmptySlot then
        return
    end
    self.m_showDepotDragTempEmptySlot = false
    self.m_depotDragTempItemId = ""
    if refresh then
        if not self:_TryRemoveDepotDragTempEntryFast() then
            self:_RefreshAll(false)
        end
    else
        self.m_depotDragTempFastAdded = false
    end
end


HL.Commit(ChangePortableDeviceCtrl)
