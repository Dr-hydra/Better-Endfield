local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

ItemBagContent = HL.Class('ItemBagContent', UIWidgetBase)




ItemBagContent.m_itemBag = HL.Field(CS.Beyond.Gameplay.InventorySystem.ItemBag)

ItemBagContent.canDrop = HL.Field(HL.Boolean) << true

ItemBagContent.canQuickDrop = HL.Field(HL.Boolean) << true

ItemBagContent.canPlace = HL.Field(HL.Boolean) << false

ItemBagContent.canSplit = HL.Field(HL.Boolean) << false

ItemBagContent.canClear = HL.Field(HL.Boolean) << false

ItemBagContent.m_updateStopped = HL.Field(HL.Boolean) << false

ItemBagContent.goldItemNum = HL.Field(HL.Number) << -1

ItemBagContent.m_itemBundleList = HL.Field(HL.Any)

ItemBagContent.m_getCell = HL.Field(HL.Function)

ItemBagContent.m_customOnUpdateCell = HL.Field(HL.Function)

ItemBagContent.m_customSetActionMenuArgs = HL.Field(HL.Function)

ItemBagContent.m_itemCellExtraInfo = HL.Field(HL.Table)

ItemBagContent.m_onClickItemAction = HL.Field(HL.Function)

ItemBagContent.m_missionItemIds = HL.Field(HL.Table)





ItemBagContent._OnFirstTimeInit = HL.Override() << function(self)
    self.m_getCell = UIUtils.genCachedCellFunction(self.view.itemList)
    self.view.itemList.onUpdateCell:AddListener(function(object, csIndex)
        self:_OnUpdateCell(object, csIndex)
    end)
    self.view.itemList.getCellName = function(csIndex)
        local itemBag = self.m_itemBag
        if csIndex < itemBag.slots.Count then
            local itemBundle = itemBag.slots[csIndex]
            if not itemBundle.isEmpty then
                return "Item_" .. itemBundle.id
            end
        end
        return "Item__" .. csIndex
    end

    self.view.dropHint.gameObject:SetActive(false)
    self.view.dropMask.gameObject:SetActive(false)
    self:RegisterMessage(MessageConst.ON_START_UI_DRAG, function(dragHelper)
        self:OnStartUiDrag(dragHelper)
    end)
    self:RegisterMessage(MessageConst.ON_END_UI_DRAG, function(dragHelper)
        self:OnEndUiDrag(dragHelper)
    end)

    self:RegisterMessage(MessageConst.ON_ITEM_BAG_CHANGED, function(args)
        local changedIndexes = unpack(args)
        self:_OnItemBagChanged(changedIndexes, false)
    end)
    self:RegisterMessage(MessageConst.ON_ITEM_BAG_LIMIT_CHANGED, function()
        self:_OnItemBagChanged(nil, true)
    end)
    self:RegisterMessage(MessageConst.ON_SYNC_INVENTORY, function()
        self:_OnItemBagChanged(nil, true)
    end)

    self:RegisterMessage(MessageConst.ON_FACTORY_DEPOT_MOVE_TO_ITEM_BAG_BY_INDEX, function(args)
        self:_PlayDropAnimation(args)
    end)
    self:RegisterMessage(MessageConst.ON_FACTORY_DEPOT_MOVE_TO_ITEM_BAG_BY_MODE, function(args)
        self:_PlayDropAnimation(args)
    end)
    self:RegisterMessage(MessageConst.ON_FAC_MOVE_ITEM_CACHE_TO_BAG, function(args)
        self:_PlayDropAnimation(args)
    end)
    self:RegisterMessage(MessageConst.ON_FAC_MOVE_ITEM_GRID_BOX_TO_BAG, function(args)
        self:_PlayDropAnimation(args)
    end)
    self:RegisterMessage(MessageConst.ON_FAC_MOVE_ALL_CACHE_OUT_ITEM_TO_BAG, function(args)
        self:_PlayDropAnimation(args)
    end)

    self:RegisterMessage(MessageConst.ON_PUT_ON_RPG_DUNGEON_EQUIP_SUCC, function()
        self:_OnItemBagChanged(nil, true, true)
    end)
    self:RegisterMessage(MessageConst.ON_PUT_OFF_RPG_DUNGEON_EQUIP_SUCC, function()
        self:_OnItemBagChanged(nil, true, true)
    end)

    UIUtils.initUIDropHelper(self.view.dropMask, {
        acceptTypes = UIConst.ITEM_BAG_DROP_MASK_ACCEPT_INFO,
        onDropItem = function(eventData, dragHelper)
            self:_OnDropItem(-1, dragHelper)
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
end




ItemBagContent.OnStartUiDrag = HL.Method(HL.Forward('UIDragHelper')) << function(self, dragHelper)
    if self.m_updateStopped then
        return
    end
    if self.view.itemListAutoScrollArea then
        self.view.itemListAutoScrollArea.gameObject:SetActive(true)
    end
    local isTypeDropValid = UIUtils.isTypeDropValid(dragHelper, UIConst.ITEM_BAG_DROP_ACCEPT_INFO)
    if isTypeDropValid then
        self.m_curDraggingDragHelper = dragHelper
        local isPortable = Utils.isPortableDevice(dragHelper:GetId())
        self:_ToggleAllColoredSlotsDropArrowHint(isPortable)
        self.view.itemBagColoredSlotUniBG:SetDropHilightActive(isPortable)
    end
    if dragHelper.source == UIConst.UI_DRAG_DROP_SOURCE_TYPE.ItemBag then
        return
    end
    if not self.canDrop or not self.canQuickDrop then
        return
    end
    if isTypeDropValid then
        self.view.quickDropButton.onClick:RemoveAllListeners()
        self.view.quickDropButton.onClick:AddListener(function()
            self:_OnDropItem(-1, dragHelper)
        end)
        self.view.dropHint.gameObject:SetActive(true)
        self.view.dropMask.gameObject:SetActive(true)
        self:_ShowMobileDragHelper(dragHelper)
    end
    self.view.itemList:UpdateShowingCells(function(csIndex, obj)
        self:_UpdateItemSlotForDropItem(self.m_getCell(obj), csIndex)
    end)
end

ItemBagContent._ShowMobileDragHelper = HL.Method(HL.Forward('UIDragHelper')) << function(self, dragHelper)
    if not DeviceInfo.usingTouch then
        return
    end

    if not self.view.gameObject.activeInHierarchy then
        return
    end

    local dragInfo = dragHelper.info
    local moveAct, canBatch
    if dragHelper.source == UIConst.UI_DRAG_DROP_SOURCE_TYPE.FactoryDepot then
        if Utils.isDepotManualInOutLocked() then
            return
        end
        moveAct = function(mode)
            GameInstance.player.inventory:FactoryDepotMoveToItemBag(Utils.getCurrentScope(), Utils.getCurrentChapterId(), dragHelper:GetId(), mode)
            dragHelper.uiDragItem:OnEndDrag(nil)
        end
        canBatch = true
    elseif dragHelper.source == UIConst.UI_DRAG_DROP_SOURCE_TYPE.Repository then
        moveAct = function(mode)
            GameInstance.player.remoteFactory.core:Message_OpMoveItemCacheToBag(Utils.getCurrentChapterId(), dragInfo.repository.componentId, 0, dragInfo.cacheGridIndex, mode)
            dragHelper.uiDragItem:OnEndDrag(nil)
        end
    elseif dragHelper.source == UIConst.UI_DRAG_DROP_SOURCE_TYPE.Storage then
        moveAct = function(mode)
            GameInstance.player.remoteFactory.core:Message_OpMoveItemGridBoxToBag(Utils.getCurrentChapterId(), dragInfo.storage.componentId, dragInfo.csIndex, 0, mode)
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
                text = Language.LUA_MOBILE_ITEM_DRAG_GRID_TO_BAG,
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

ItemBagContent._ToggleAllColoredSlotsDropArrowHint = HL.Method(HL.Boolean) << function(self, active)
    local itemBag = self.m_itemBag
    local state = active and "InDrop" or "NotInDrop"
    for k = 1, itemBag.coloredSlotNum do
        local cell = self.m_getCell(k)
        if cell then
            cell.view.coloredSlotStateController:SetState(state)
        end
    end
end

ItemBagContent.OnEndUiDrag = HL.Method(HL.Forward('UIDragHelper')) << function(self, dragHelper)
    if self.m_updateStopped then
        return
    end
    if self.view.itemListAutoScrollArea then
        self.view.itemListAutoScrollArea.gameObject:SetActive(false)
    end
    local isTypeDropValid = UIUtils.isTypeDropValid(dragHelper, UIConst.ITEM_BAG_DROP_ACCEPT_INFO)
    if isTypeDropValid then
        self:_ToggleAllColoredSlotsDropArrowHint(false)
        self.view.itemBagColoredSlotUniBG:SetDropHilightActive(false)
        self.m_curDraggingDragHelper = nil
    end
    if dragHelper.source == UIConst.UI_DRAG_DROP_SOURCE_TYPE.ItemBag then
        return
    end
    if not self.canQuickDrop then
        return
    end
    if isTypeDropValid then
        self.view.dropHint.gameObject:SetActive(false)
        self.view.dropMask.gameObject:SetActive(false)
        Notify(MessageConst.HIDE_ITEM_DRAG_HELPER)
    end
    self.view.itemList:UpdateShowingCells(function(csIndex, obj)
        self:_UpdateItemSlotForDropItem(self.m_getCell(obj), csIndex)
    end)
end




ItemBagContent.InitItemBagContent = HL.Method(HL.Opt(HL.Function, HL.Table)) << function(self, onClickItemAction, otherArgs)
    self:_FirstTimeInit()

    self.m_readItemIds = {}
    self.m_onClickItemAction = onClickItemAction

    otherArgs = otherArgs or {}
    self.canPlace = otherArgs.canPlace == true
    self.canSplit = otherArgs.canSplit == true
    self.canClear = otherArgs.canClear == true
    self.m_itemCellExtraInfo = otherArgs.itemCellExtraInfo
    self.m_customOnUpdateCell = otherArgs.customOnUpdateCell
    self.m_customSetActionMenuArgs = otherArgs.customSetActionMenuArgs
    self.m_missionItemIds = otherArgs.missionItemIds or {}

    self.m_updateStopped = false
    self:Refresh()
end

ItemBagContent.StopUpdate = HL.Method(HL.Boolean) << function(self, cacheAllCell)
    self.m_updateStopped = true
    if cacheAllCell then



        self.view.itemList:UpdateCount(0)
    end
end

ItemBagContent.StartUpdate = HL.Method() << function(self)
    if not self.m_updateStopped then
        return
    end
    self.m_updateStopped = false
    self:Refresh()
end

ItemBagContent.RefreshChangeGold = HL.Method(HL.Table) << function(self, Args)
    self.m_itemBundleList = Args.itemBundleList
    self.goldItemNum = Args.goldItemNum
    self:Refresh()
end

ItemBagContent.Refresh = HL.Method(HL.Opt(HL.Boolean)) << function(self, skipGraduallyShow)
    self.m_itemBag = GameInstance.player.inventory.itemBag:GetOrFallback(Utils.getCurrentScope())
    self.view.itemList:UpdateCount(self.m_itemBag.maxSlotCount, false, false, false, skipGraduallyShow == true)
    self.view.itemBagColoredSlotUniBG:InitItemBagColoredSlotUniBG(self.view.itemList, self.m_itemBag.coloredSlotNum)
end

ItemBagContent._OnUpdateCell = HL.Method(GameObject, HL.Number) << function(self, object, csIndex)
    local cell = self.m_getCell(object)

    local itemBag = self.m_itemBag
    local slotLimit = itemBag.slots.Count
    local item = csIndex < slotLimit and itemBag.slots[csIndex] or nil
    if item then
        self:_UpdateNormalSlot(cell, item, csIndex)
        cell.item:ShowPickUpLogo(true)
        local isPortableDevice = Utils.isPortableDevice(item.id)
        if isPortableDevice then
            local isActive = itemBag:IsColoredSlotActive(item.id, csIndex)
            cell.item.view.spTypeIconNode.stateController:SetState(isActive and "Active" or "Normal")
            local wrapper = cell.item.view.spTypeIconNode.animationWrapper
            local AS = CS.Beyond.UI.UIConst.AnimationState
            if isActive then


                if wrapper.curState ~= AS.In and wrapper.curState ~= AS.InEasing and wrapper.curState ~= AS.Loop then
                    wrapper:PlayLoopAnimation()
                end
            else
                wrapper:ClearTween()
            end
        end
        local isMissionItem = self.m_missionItemIds[item.id] == true
        cell.item.view.missionMark.gameObject:SetActive(isMissionItem)
        cell.item.redDot.gameObject:SetActive(not isMissionItem)
    else
        self:_UpdateLockSlot(cell, csIndex)
        cell.item:ShowPickUpLogo(false)
        cell.item.view.missionMark.gameObject:SetActive(false)
    end
end

ItemBagContent._UpdateNormalSlot = HL.Method(HL.Userdata, HL.Userdata, HL.Number) << function(self, cell, item, csIndex)
    cell:InitItemSlot(item, function()
        self:_OnClickItem(csIndex)
    end)
    cell.item.canPlace = self.canPlace
    cell.item.canSplit = self.canSplit
    cell.item.canClear = self.canClear
    cell.item.slotIndex = csIndex
    cell.supportQuickMovingHalfItem = true

    local id = item.id
    local isEmpty = string.isEmpty(id)
    if isEmpty then
        cell.gameObject.name = "Item__" .. csIndex
    else
        cell.gameObject.name = "Item_" .. id
        cell.item.view.button.clickHintTextId = "virtual_mouse_hint_item_tips"
    end
    cell.item:UpdateRedDot()
    if cell.item.redDot.curIsActive then
        self.m_readItemIds[id] = true
    end

    if self.m_itemCellExtraInfo then
        cell.item:SetExtraInfo(self.m_itemCellExtraInfo)
    end

    cell.item:SetSelected(cell.item.showingTips)
    local isDropHighlighted = csIndex == self.m_curDropHighlightCSIndex
    cell:SetDropHighlighted(isDropHighlighted)
    self:UpdateColoredItemSlotColorBG(cell, csIndex, isDropHighlighted and self.m_curDraggingDragHelper or nil)

    if not isEmpty then
        local data = Tables.itemTable:GetValue(id)
        local count = item.count

        if data.maxBackpackStackCount == 1 then
            cell.item.view.count.gameObject:SetActive(false)
        elseif count >= data.maxBackpackStackCount then
            cell.item.view.count.color = self.view.config.ITEM_SLOT_FULL_COLOR
            cell.item.view.count.fontSharedMaterial = self.view.config.ITEM_SLOT_FULL_MAT
        else
            cell.item.view.count.color = Color.white
            cell.item.view.count.fontSharedMaterial = self.view.config.ITEM_SLOT_NORMAL_MAT
        end

        local canAbandon = GameInstance.player.inventory:CanDestroyItem(Utils.getCurrentScope(), id)
        local dragHelper = UIUtils.initUIDragHelper(cell.view.dragItem, {
            source = UIConst.UI_DRAG_DROP_SOURCE_TYPE.ItemBag,
            type = data.type,
            csIndex = csIndex,
            itemId = id,
            count = count,
            instId = item.instId,
            onBeginDrag = function()
                self.view.itemList:SetCellCanCache(csIndex, false)
                cell.item:Read()
            end,
            onEndDrag = function()
                self.view.itemList:SetCellCanCache(csIndex, true)
            end,
            onDropTargetChanged = function(enterObj, dropHelper)
                local dragObj = cell.view.dragItem.curDragObj
                if not dragObj then
                    return
                end
                local dragItem = dragObj:GetComponent("LuaUIWidget").table[1]
                if not dropHelper or not dropHelper.info.isAbandon then
                    dragItem.view.abandonNode.gameObject:SetActive(false)
                    return
                end
                dragItem.view.abandonNode.gameObject:SetActive(true)
                dragItem.view.canAbandonNode.gameObject:SetActive(canAbandon)
                dragItem.view.cantAbandonNode.gameObject:SetActive(not canAbandon)
            end,
        })
        cell:InitPressDrag()

        if count > 0 then
            cell.item.actionMenuArgs = {
                source = UIConst.UI_DRAG_DROP_SOURCE_TYPE.ItemBag,
                dragHelper = dragHelper,
            }
            if self.m_customSetActionMenuArgs ~= nil then
                self.m_customSetActionMenuArgs(cell.item.actionMenuArgs)
            end
        end
    end

    self:_UpdateItemSlotForDropItem(cell, csIndex)
    UIUtils.initUIDropHelper(cell.view.dropItem, {
        acceptTypes = UIConst.ITEM_BAG_DROP_ACCEPT_INFO,
        onDropItem = function(eventData, dragHelper)
            self:_OnDropItem(csIndex, dragHelper)
        end,
        onToggleHighlight = function(active)
            if active then
                self:UpdateColoredItemSlotColorBG(cell, csIndex, self.m_curDraggingDragHelper)
            else
                self:UpdateColoredItemSlotColorBG(cell, csIndex)
            end
        end,
    })

    if self.m_customOnUpdateCell then
        self.m_customOnUpdateCell(cell, item, csIndex)
    end
end

ItemBagContent._UpdateLockSlot = HL.Method(HL.Userdata, HL.Number) << function(self, cell, csIndex)
    cell.gameObject.name = "Item__" .. csIndex
    cell:InitLockSlot()
    UIUtils.initUIDropHelper(cell.view.dropItem, {
        acceptTypes = UIConst.ITEM_BAG_DROP_ACCEPT_INFO,
        onDropItem = function(eventData, dragHelper)
            Notify(MessageConst.SHOW_TOAST, Language.LUA_ITEM_BAG_SLOT_LOCKED)
        end,
    })

    if self.m_customOnUpdateCell then
        self.m_customOnUpdateCell(cell, nil, csIndex)
    end
end

ItemBagContent._OnDropItem = HL.Method(HL.Number, HL.Forward('UIDragHelper')) << function(self, csIndex, dragHelper)
    local inventory = GameInstance.player.inventory
    local source = dragHelper.source
    local dragInfo = dragHelper.info
    local core = GameInstance.player.remoteFactory.core
    if csIndex < 0 then
        csIndex = self.m_itemBag:GetFirstValidSlotIndex(dragInfo.itemId)
    end
    if csIndex < 0 then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_ITEM_BAG_NO_EMPTY_SLOT)
        return
    end

    local mode = dragHelper.isHalfDragging and CS.Proto.ITEM_MOVE_MODE.HalfGrid or CS.Proto.ITEM_MOVE_MODE.Normal

    if source == UIConst.UI_DRAG_DROP_SOURCE_TYPE.Storage then

        core:Message_OpMoveItemGridBoxToBag(Utils.getCurrentChapterId(), dragInfo.storage.componentId, dragInfo.csIndex, csIndex, mode)
    elseif source == UIConst.UI_DRAG_DROP_SOURCE_TYPE.Repository then

        local itemBundle = self.m_itemBag.slots[csIndex]
        if not dragInfo.isIn then
            if itemBundle ~= nil and not string.isEmpty(itemBundle.id) and itemBundle.id ~= dragInfo.itemId then
                return
            end
        else
            if itemBundle ~= nil and not dragInfo.canFacCacheDrop(itemBundle.id) then
                Notify(MessageConst.SHOW_TOAST, Language["ui_fac_common_bag_drop_same_item"])
                return
            end
        end
        core:Message_OpMoveItemCacheToBag(Utils.getCurrentChapterId(), dragInfo.repository.componentId, csIndex, dragInfo.cacheGridIndex, mode)
    elseif source == UIConst.UI_DRAG_DROP_SOURCE_TYPE.FactoryDepot then
        if Utils.isDepotManualInOutLocked() then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_DEPOT_MANUAL_IN_OUT_LOCKED)
            return
        end

        local itemBundle = self.m_itemBag.slots[csIndex]
        if string.isEmpty(itemBundle.id) or itemBundle.id == dragInfo.itemId then
            inventory:FactoryDepotMoveToItemBag(Utils.getCurrentScope(), Utils.getCurrentChapterId(), dragInfo.itemId, dragHelper:GetCount(), csIndex)
        end
    elseif source == UIConst.UI_DRAG_DROP_SOURCE_TYPE.ItemBag then

        if dragHelper.isHalfDragging then
            inventory:SplitInItemBag(Utils.getCurrentScope(), dragInfo.csIndex, csIndex, dragHelper:GetCount())
        else
            inventory:MoveInItemBag(Utils.getCurrentScope(), dragInfo.csIndex, csIndex)
        end
        UIUtils.playItemDropAudio(dragHelper:GetId())
    end
end


ItemBagContent._OnClickItem = HL.Method(HL.Number) << function(self, csIndex)
    local item = self.m_itemBag.slots[csIndex]
    local cell = self.m_getCell(LuaIndex(csIndex))
    if self.m_onClickItemAction then
        self.m_onClickItemAction(item.id, cell, csIndex)
    elseif not string.isEmpty(item.id) then
        cell.item:ShowTips()
        cell.item:Read()
    end
end

ItemBagContent._OnItemBagChanged = HL.Method(HL.Opt(HL.Userdata, HL.Boolean, HL.Boolean)) << function(self, changedIndexes, refreshAll, skipGraduallyShow)
    if self.m_updateStopped then
        return
    end

    if refreshAll then
        self:Refresh(skipGraduallyShow)
        return
    end

    local itemBag = self.m_itemBag
    local changeIndexDic = {}
    local needUpdateColoredSlot
    for _, slotIndex in pairs(changedIndexes) do
        local itemId = itemBag[slotIndex].id
        local obj = self.view.itemList:Get(slotIndex)
        local isColorActive
        if slotIndex < itemBag.coloredSlotNum then
            needUpdateColoredSlot = true
            if not string.isEmpty(itemId) then
                local isLowerLv
                isColorActive, isLowerLv = itemBag:IsColoredSlotActive(itemId, slotIndex)
                if (not isColorActive) and isLowerLv then
                    local iData = Tables.itemTable[itemId]
                    Notify(MessageConst.SHOW_TOAST, string.format(Language.LUA_ITEM_BAG_PORTABLE_DEVICE_NOT_ACTIVE, iData.name))
                end
            end
        end
        changeIndexDic[slotIndex] = true
        if obj then
            self:_OnUpdateCell(obj, slotIndex)




            if isColorActive then
                local cell = self.m_getCell(obj)
                cell:PlayColoredSlotActivatedAnimation()
            end
        end
    end
    if needUpdateColoredSlot then
        for slotIndex = 0, itemBag.coloredSlotNum - 1 do
            if not changeIndexDic[slotIndex] then
                local obj = self.view.itemList:Get(slotIndex)
                if obj then
                    local cell = self.m_getCell(obj)
                    local lastIsActive = cell.m_colorSlotActivated
                    self:_OnUpdateCell(obj, slotIndex)
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
                                if otherPdData.type == pdData.type and pdData.isMainDevice == otherPdData.isMainDevice then
                                    if otherPdData.lv > pdData.lv then
                                        local iData = Tables.itemTable[itemId]
                                        Notify(MessageConst.SHOW_TOAST, string.format(Language.LUA_ITEM_BAG_PORTABLE_DEVICE_NOT_ACTIVE, iData.name))
                                        break
                                    end
                                end
                            end
                        end
                    end
                end
            end
        end
    end
end

ItemBagContent.GetCell = HL.Method(HL.Any).Return(HL.Opt(HL.Forward("ItemSlot"))) << function(self, objOrIndex)
    return self.m_getCell(objOrIndex)
end

ItemBagContent.CheckAndNaviToTargetCell = HL.Method(HL.Function) << function(self, checkFunc)
    local findTag = false
    for csIndex, itemBundle in cs_pairs(self.m_itemBag.slots) do
        if checkFunc(itemBundle.id) then
            self.view.itemList:ScrollToIndex(csIndex, true)
            local slot = self.m_getCell(LuaIndex(csIndex))
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

ItemBagContent.GetCurNaviTargetSlot = HL.Method().Return(HL.Number, HL.Opt(HL.Forward("ItemSlot"))) << function(self)
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

ItemBagContent.m_readItemIds = HL.Field(HL.Table)

ItemBagContent.ReadCurShowingItems = HL.Method() << function(self)
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

ItemBagContent.ToggleCanDrop = HL.Method(HL.Boolean) << function(self, active)
    self.canDrop = active
end

ItemBagContent.ToggleCanQuickDrop = HL.Method(HL.Boolean) << function(self, active)
    self.canQuickDrop = active
end




ItemBagContent.m_curDropHighlightCSIndex = HL.Field(HL.Number) << -1

ItemBagContent.m_curDraggingDragHelper = HL.Field(HL.Forward('UIDragHelper'))


ItemBagContent._FindAndHighlightForDrop = HL.Method() << function(self)
    if not self.m_curDraggingDragHelper then
        return
    end
    local itemId = self.m_curDraggingDragHelper:GetId()
    local csIndex = self.m_itemBag:GetFirstEmptySlotIndex()
    if csIndex == -1 then
        csIndex = self.m_itemBag:GetFirstValidSlotIndex(itemId)
    end
    if csIndex == -1 then
        return
    end




    self.m_curDropHighlightCSIndex = csIndex
    local object = self.view.itemList:Get(csIndex)
    logger.info("_FindAndHighlightForDrop", csIndex, self.m_curDraggingDragHelper)
    if object then
        local cell = self.m_getCell(object)
        cell:SetDropHighlighted(true)
        self:UpdateColoredItemSlotColorBG(cell, csIndex, self.m_curDraggingDragHelper)
    end
end

ItemBagContent._CancelDropHighlight = HL.Method() << function(self)
    local csIndex = self.m_curDropHighlightCSIndex
    self.m_curDropHighlightCSIndex = -1
    if csIndex >= 0 then
        local cell = self.m_getCell(LuaIndex(csIndex))
        if cell then
            cell:SetDropHighlighted(false)
            self:UpdateColoredItemSlotColorBG(cell, csIndex)
        end
    end
end

ItemBagContent._UpdateItemSlotForDropItem = HL.Method(HL.Forward('ItemSlot'), HL.Number) << function(self, itemSlot, csIndex)
    local activeRaycast
    if not self.m_curDraggingDragHelper then
        activeRaycast = true
    elseif self.m_curDraggingDragHelper.source == UIConst.UI_DRAG_DROP_SOURCE_TYPE.ItemBag then
        activeRaycast = true
    else

        if csIndex < self.m_itemBag.slots.Count then
            local itemBundle = self.m_itemBag.slots[csIndex]
            if itemBundle.isEmpty then
                activeRaycast = true
            else
                activeRaycast = itemBundle.id == self.m_curDraggingDragHelper:GetId()
            end
        else
            activeRaycast = false
        end
    end
    itemSlot.view.nonDrawingGraphic.raycastTarget = activeRaycast
    itemSlot.item.view.nonDrawingGraphic.raycastTarget = activeRaycast
    if itemSlot.view.forbiddenMask then


        itemSlot.view.forbiddenMask.raycastTarget = activeRaycast
    end
end

ItemBagContent._PlayDropAnimation = HL.Method(HL.Table) << function(self, args)
    if self.m_updateStopped then
        return
    end
    local value = args[1]
    local index
    if type(value) == "number" then

        self:_PlayDropAnimationAt(value)
        index = value
    else
        for _,v in cs_pairs(value) do
            if type(v) == "number" then

                self:_PlayDropAnimationAt(v)
                index = v
            else

                self:_PlayDropAnimationAt(v.GridIndex)
                index = v.GridIndex
            end
        end
    end
    if not index then
        return
    end
    local item = self.m_itemBag.slots[index]
    if item then
        UIUtils.playItemDropAudio(item.id)
    end
end

ItemBagContent._PlayDropAnimationAt = HL.Method(HL.Number) << function(self, index)

    local cell = self.m_getCell(LuaIndex(index))
    if cell then
        cell:PlayDropAnimation()
    end
end







ItemBagContent.UpdateColoredItemSlotColorBG = HL.Method(HL.Any, HL.Number, HL.Opt(HL.Any)) << function(self, cell, csIndex, dragHelper)
    local itemId
    if dragHelper then

        itemId = dragHelper:GetId()
    else

        itemId = self.m_itemBag.slots[csIndex].id
    end
    cell:UpdateSlotColorBGByItemId(self.m_itemBag, csIndex, itemId, self.m_curDraggingDragHelper ~= nil and Utils.isPortableDevice(self.m_curDraggingDragHelper:GetId()))
end




HL.Commit(ItemBagContent)
return ItemBagContent
