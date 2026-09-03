local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

FacCacheSlot = HL.Class('FacCacheSlot', UIWidgetBase)

FacCacheSlot.m_slotIndex = HL.Field(HL.Number) << -1

FacCacheSlot.m_cacheType = HL.Field(HL.Number) << 0

FacCacheSlot.m_itemInfo = HL.Field(HL.Table)

FacCacheSlot.m_isBlocked = HL.Field(HL.Boolean) << false

FacCacheSlot.m_cache = HL.Field(CS.Beyond.Gameplay.RemoteFactory.FBUtil.Cache)

FacCacheSlot.m_isInCache = HL.Field(HL.Boolean) << false

FacCacheSlot.m_onDropCallback = HL.Field(HL.Function)

FacCacheSlot.m_isDropEnabled = HL.Field(HL.Boolean) << true

FacCacheSlot.m_isQuickDropEnabled = HL.Field(HL.Boolean) << true

FacCacheSlot.m_disableDump = HL.Field(HL.Boolean) << false

FacCacheSlot.m_quickDropBindingId = HL.Field(HL.Number) << -1

FacCacheSlot.m_producerInfo = HL.Field(HL.Userdata)



FacCacheSlot._OnFirstTimeInit = HL.Override() << function(self)
    self:_InitSlotDrag()
    self:RegisterMessage(MessageConst.ON_FAC_MOVE_ITEM_BAG_TO_CACHE, function(args)
        self:_OnMoveItemToCache(args)
    end)
    self:RegisterMessage(MessageConst.ON_FAC_MOVE_ITEM_DEPOT_TO_CACHE, function(args)
        self:_OnMoveItemToCache(args)
    end)
end

FacCacheSlot.InitFacCacheSlot = HL.Method(HL.Table) << function(self, slotData)
    self:_FirstTimeInit()

    if slotData == nil then
        return
    end

    self.m_slotIndex = slotData.slotIndex
    self.m_cacheType = slotData.cacheType
    self.m_itemInfo = slotData.itemInfo
    self.m_cache = slotData.cache
    self.m_isInCache = slotData.isInCache
    self.m_onDropCallback = slotData.onDropCallback or function() end
    self.m_lockFormulaId = slotData.lockFormulaId or ""
    self.m_producerInfo = slotData.producerInfo
    self.m_disableDump = slotData.disableDump

    self:_InitItemSlot()
    self:_UpdateContent()
end




FacCacheSlot._OnClickItemSlot = HL.Method(HL.Forward('ItemSlot'), HL.Table) << function(self, itemSlot, info)
    if DeviceInfo.usingController then
        itemSlot.item:ShowActionMenu()
        return
    end

    if DeviceInfo.usingKeyboard then
        local mode = UIUtils.getMovingItemMode()
        if mode and not Utils.isDepotManualInOutLocked() then
            local inventoryArea = self:GetUICtrl().view.inventoryArea
            if inventoryArea then
                if Tables.liquidTable:ContainsKey(itemSlot.item.id) then
                    Notify(MessageConst.SHOW_TOAST, Language.LUA_ITEM_QUICK_DROP_LIQUID_FAIL)
                    return
                end
                if Tables.gasTable:ContainsKey(itemSlot.item.id) then
                    Notify(MessageConst.SHOW_TOAST, Language.LUA_ITEM_QUICK_DROP_GAS_FAIL)
                    return
                end
                local isMoveToItemBag = inventoryArea.m_isItemBag
                if isMoveToItemBag then
                    GameInstance.player.remoteFactory.core:Message_OpMoveItemCacheToBag(Utils.getCurrentChapterId(), self.m_cache.componentId, 0, CSIndex(self.m_slotIndex), mode)
                else
                    GameInstance.player.remoteFactory.core:Message_OpMoveItemCacheToDepot(Utils.getCurrentChapterId(), self.m_cache.componentId, CSIndex(self.m_slotIndex), mode)
                end
                return
            end
        end
    end

    itemSlot.item:SetSelected(true)
    itemSlot.item:ShowTips(nil, function()
        itemSlot.item:SetSelected(false)
    end)
end

FacCacheSlot._InitItemSlot = HL.Method() << function(self)
    self.view.itemSlot.gameObject:SetActiveIfNecessary(self.m_cacheType == FacConst.FAC_CACHE_SLOT_TYPE_STATE.Normal)
    self.view.liquidItemSlot.gameObject:SetActiveIfNecessary(self.m_cacheType ~= FacConst.FAC_CACHE_SLOT_TYPE_STATE.Normal)

    if self.m_cacheType ~= FacConst.FAC_CACHE_SLOT_TYPE_STATE.Normal then
        self.view.liquidItemSlot.view.cacheTypeCtrl:SetState(FacConst.FAC_CACHE_SLOT_TYPE_CTRL_STATE[self.m_cacheType])
    end
end

FacCacheSlot._GetCurrentItemSlot = HL.Method().Return(HL.Userdata) << function(self)
    return self.m_cacheType == FacConst.FAC_CACHE_SLOT_TYPE_STATE.Normal and self.view.itemSlot or self.view.liquidItemSlot
end

FacCacheSlot._UpdateContent = HL.Method() << function(self)
    local info = self.m_itemInfo
    local isEmpty = not info or string.isEmpty(info.id)

    local itemSlot = self:_GetCurrentItemSlot()
    local dragEnabled = not isEmpty and info.count ~= nil and info.count > 0
    if isEmpty then
        itemSlot:InitItemSlot()
        itemSlot.gameObject.name = "Item__" .. CSIndex(self.m_slotIndex)
    else
        if not dragEnabled then

            itemSlot.view.dragItem.enabled = false
        end
        itemSlot:InitItemSlot(info, function()
            self:_OnClickItemSlot(itemSlot, info)
        end)
        itemSlot.gameObject.name = "Item_" .. info.id
        itemSlot.item.view.button.clickHintTextId = "virtual_mouse_hint_item_tips"
    end
    if Tables.liquidTable:ContainsKey(info.id) or Tables.gasTable:ContainsKey(info.id) then
        dragEnabled = false
    end
    itemSlot.view.dragItem.enabled = dragEnabled
    itemSlot.supportQuickMovingHalfItem = true
    if self.m_cacheType == FacConst.FAC_CACHE_SLOT_TYPE_STATE.Normal then

        itemSlot.view.dropHintEmptyText.gameObject:SetActiveIfNecessary(isEmpty or info.count <= 0)
        itemSlot.view.dropHintFilledText.gameObject:SetActiveIfNecessary(not isEmpty and info.count > 0)
    else

        self.view.liquidItemSlot:SetItemType(self.m_cacheType)
        self.view.liquidItemSlot.view.emptyIcon.gameObject:SetActiveIfNecessary(isEmpty)
        self:_RefreshLiquidBg()
    end

    self.m_dropHelper = UIUtils.initUIDropHelper(itemSlot.view.dropItem, {
        acceptTypes = UIConst.FACTORY_REPO_DROP_ACCEPT_INFO,
        onDropItem = function(eventData, dragHelper)
            if self:CanDrop(dragHelper) then
                self:_OnDropItem(dragHelper)
            end
        end,
        onToggleHighlight = function(active)
            self:_MobileRefreshDropHighlight(active)
        end
    })

    itemSlot.item.view.button.onIsNaviTargetChanged = function(active)
        if active then
            self:_RefreshSlotHoverBindingState()
        end
    end
    self:_RefreshSlotHoverBindingState()
    if isEmpty then
        return
    end

    if info.count > 0 then
        local data = Tables.itemTable:GetValue(info.id)
        local dragCount = math.min(info.count or 0, info.maxStackCount or 0)

        self.m_dragHelper = UIUtils.initUIDragHelper(itemSlot.view.dragItem, {
            source = UIConst.UI_DRAG_DROP_SOURCE_TYPE.Repository,
            type = data.type,

            repository = self.m_cache,
            isIn = self.m_isInCache,

            itemId = info.id,
            count = dragCount,
            cacheGridIndex = CSIndex(self.m_slotIndex),

            canFacCacheDrop = function(id)
                return self:_CheckIsValidProducerDropBagItem(id)
            end
        })
        itemSlot:InitPressDrag()

        if self.m_cacheType ~= FacConst.FAC_CACHE_SLOT_TYPE_STATE.Normal then
            local isLiquid = Tables.liquidTable:ContainsKey(info.id)
            if isLiquid then
                self.m_quickDropBindingId = itemSlot.item:AddHoverBinding("common_quick_drop", function()
                    Notify(MessageConst.ON_NAVI_INVENTORY_SELECT_FLUID, {
                        componentId = self.m_cache.componentId,
                        slotIndex = CSIndex(self.m_slotIndex),
                        fluidId = "",
                        sourceItem = itemSlot.item,
                        cacheType = FacConst.FAC_CACHE_SLOT_TYPE_STATE.Liquid
                    })
                end)
                InputManagerInst:SetBindingText(self.m_quickDropBindingId, Language.LUA_ITEM_ACTION_CACHE_SELECT_FILL_LIQUID)
            else
                self.m_quickDropBindingId = itemSlot.item:AddHoverBinding("common_quick_drop", function()
                    Notify(MessageConst.ON_NAVI_INVENTORY_SELECT_FLUID, {
                        componentId = self.m_cache.componentId,
                        slotIndex = CSIndex(self.m_slotIndex),
                        fluidId = "",
                        sourceItem = itemSlot.item,
                        cacheType = FacConst.FAC_CACHE_SLOT_TYPE_STATE.Gas
                    })
                end)
                InputManagerInst:SetBindingText(self.m_quickDropBindingId, Language.LUA_ITEM_ACTION_CACHE_SELECT_FILL_GAS)
            end
        else
            self.m_quickDropBindingId = itemSlot.item:AddHoverBinding("common_quick_drop", function()
                itemSlot:QuickDrop()
            end)
        end
    else
        self.m_quickDropBindingId = itemSlot.item:AddHoverBinding("common_quick_drop", function() end)
        if self.m_cacheType == FacConst.FAC_CACHE_SLOT_TYPE_STATE.Liquid then
            InputManagerInst:SetBindingText(self.m_quickDropBindingId, Language.LUA_ITEM_ACTION_CACHE_SELECT_FILL_LIQUID)
        elseif self.m_cacheType == FacConst.FAC_CACHE_SLOT_TYPE_STATE.Gas then
            InputManagerInst:SetBindingText(self.m_quickDropBindingId, Language.LUA_ITEM_ACTION_CACHE_SELECT_FILL_GAS)
        end
        InputManagerInst:ForceBindingKeyhintToGray(self.m_quickDropBindingId, true)
    end
    if not self.m_isQuickDropEnabled then
        InputManagerInst:ToggleBinding(self.m_quickDropBindingId, false)
    end

    itemSlot.item.actionMenuArgs = {
        source = UIConst.UI_DRAG_DROP_SOURCE_TYPE.Repository,
        componentId = self.m_cache.componentId,
        cacheGridIndex = CSIndex(self.m_slotIndex),
        cacheType = self.m_cacheType,
        isInCacheSlot = self.m_isInCache,
        disableDump = self.m_disableDump,
    }
    InputManagerInst:SetBindingText(itemSlot.item.view.button.hoverConfirmBindingId, Language["key_hint_item_open_action_menu"])

    local color = info.count < info.maxStackCount and self.config.COLOR_NORMAL or self.config.COLOR_MAX_STACK
    local colorFormatterPrefix = string.format("<color=#%sff>", color)
    itemSlot.view.item:UpdateCountWithColor(info.count, colorFormatterPrefix .. "%s" .. "</color>")
end

FacCacheSlot._RefreshSlotHoverBindingState = HL.Method() << function(self)
    local itemInfo = self.m_itemInfo
    local itemSlot = self:_GetCurrentItemSlot()
    if not itemInfo or string.isEmpty(itemInfo.id) or not self.m_isQuickDropEnabled then
        InputManagerInst:ToggleBinding(itemSlot.item.view.button.hoverConfirmBindingId, false)
    end
end

FacCacheSlot._RefreshLiquidBg = HL.Method() << function(self)
    if self.m_cacheType == FacConst.FAC_CACHE_SLOT_TYPE_STATE.Normal then
        return
    end

    local count = 0
    local height = 0
    if not string.isEmpty(self.m_itemInfo.id) then
        count = self.m_itemInfo.count
        local maxCount = self.m_itemInfo.maxStackCount
        if maxCount > 0 then
            height = count / maxCount
        end
    end

    self.view.liquidItemSlot:RefreshLiquidHeight(height)
end






FacCacheSlot.m_dragHelper = HL.Field(HL.Forward('UIDragHelper'))

FacCacheSlot._InitSlotDrag = HL.Method() << function(self)
    self:RegisterMessage(MessageConst.ON_START_UI_DRAG, function(dragHelper)
        self:_OnStartUiDrag(dragHelper)
    end)
    self:RegisterMessage(MessageConst.ON_END_UI_DRAG, function(dragHelper)
        self:_OnEndUiDrag(dragHelper)
    end)
end

FacCacheSlot._OnStartUiDrag = HL.Method(HL.Forward('UIDragHelper')) << function(self, dragHelper)
    if dragHelper == nil then
        return
    end

    local itemSlot = self:_GetCurrentItemSlot()
    if self:_ShouldAcceptDrop(dragHelper) then
        if self.m_cacheType == FacConst.FAC_CACHE_SLOT_TYPE_STATE.Normal then
            itemSlot.view.dropItem.enabled = true
            itemSlot.view.dropHintNode.gameObject:SetActiveIfNecessary(true)
        else
            itemSlot:SetAcceptDrop(true, dragHelper.info.itemId, self.m_cacheType)
        end
    else
        if self.m_cacheType == FacConst.FAC_CACHE_SLOT_TYPE_STATE.Normal then
            itemSlot.view.dropItem.enabled = false
        else
            itemSlot:SetAcceptDrop(false)
        end
    end
end

FacCacheSlot._OnEndUiDrag = HL.Method(HL.Forward('UIDragHelper')) << function(self, dragHelper)
    if dragHelper == nil then
        return
    end

    local itemSlot = self:_GetCurrentItemSlot()
    if self.m_cacheType == FacConst.FAC_CACHE_SLOT_TYPE_STATE.Normal then
        itemSlot.view.dropItem.enabled = false
    else
        itemSlot:SetAcceptDrop(false)
    end
    if self:_ShouldAcceptDrop(dragHelper) then
        if self.m_cacheType == FacConst.FAC_CACHE_SLOT_TYPE_STATE.Normal then
            itemSlot.view.dropHintNode.gameObject:SetActiveIfNecessary(false)
        else
            itemSlot:SetHintActive(false)
        end
    end
end






FacCacheSlot.m_dropHelper = HL.Field(HL.Forward('UIDropHelper'))

FacCacheSlot._ShouldAcceptDrop = HL.Method(HL.Forward('UIDragHelper')).Return(HL.Boolean) << function(self, dragHelper)
    if not self.m_isDropEnabled then
        return false
    end

    if not self.m_dropHelper:Accept(dragHelper) then
        return false
    end

    if dragHelper.info.repository == self.m_cache then
        return false
    end

    local itemId = dragHelper.info.itemId
    local success, data = Tables.factoryItemTable:TryGetValue(itemId)
    if not success then
        return false
    end


    if data.phaseType ~= GEnums.FCItemCacheType.Normal then
        return false
    end

    if self.m_cacheType ~= FacConst.FAC_CACHE_SLOT_TYPE_STATE.Normal then
        local isEmptyBottleOrJar, isFullBottleOrJar = self:_IsEmptyBottleDrop(itemId), self:_IsFullBottleDrop(itemId)


        if not isEmptyBottleOrJar and not isFullBottleOrJar then
            return false
        end


        if isEmptyBottleOrJar and string.isEmpty(self.view.liquidItemSlot.view.item.id) then
            return false
        end


        if not self.m_isInCache and not isEmptyBottleOrJar then
            return false
        end
    else

        if data.buildingBufferStackLimit <= 0 then
            return false
        end


        if not self.m_isInCache then
            return false
        end
    end

    return true
end

FacCacheSlot._OnDropItem = HL.Method(HL.Forward('UIDragHelper'), HL.Opt(CS.Proto.ITEM_MOVE_MODE)) << function(self, dragHelper, mode)
    local source = dragHelper.source
    local dragInfo = dragHelper.info
    local core = GameInstance.player.remoteFactory.core
    local componentId = self.m_cache.componentId
    mode = mode or (dragHelper.isHalfDragging and CS.Proto.ITEM_MOVE_MODE.HalfGrid or CS.Proto.ITEM_MOVE_MODE.Normal)
    if source == UIConst.UI_DRAG_DROP_SOURCE_TYPE.Storage then

        logger.error("未实现: 从工厂 Storage Drop 过来")
        return
    elseif source == UIConst.UI_DRAG_DROP_SOURCE_TYPE.Repository then

        if dragInfo.itemId then

            core:Message_OpMoveItemCacheToCache(Utils.getCurrentChapterId(), dragInfo.repository.componentId, dragInfo.itemId, componentId, mode)
        else

            logger.error("未实现: 从工厂 Repo Drop 过来, 尝试转移整个Repo")
            return
        end
    elseif source == UIConst.UI_DRAG_DROP_SOURCE_TYPE.FactoryDepot then

        if self:_IsFluidModeBottleDrop(dragInfo.itemId) then
            if self:_CheckIsValidBottleInLockFormula(dragInfo.itemId) then
                if not self:_CheckIsValidFluidDrop(dragInfo.itemId) then
                    Notify(MessageConst.SHOW_TOAST, Language["ui_fac_common_bag_drop_same_item"])
                    return
                end

                core:Message_OpFillingFluidComWithDepot(Utils.getCurrentChapterId(), componentId, dragInfo.itemId, CSIndex(self.m_slotIndex))
                FactoryUtils.playAudioWhenFillingItem(dragInfo.itemId, self.m_itemInfo.id, self.m_itemInfo.count)
            end
        else
            if self:_CheckIsValidItemInLockFormula(dragInfo.itemId) then
                if not self:_CheckIsValidProducerDropBagItem(dragInfo.itemId) then
                    Notify(MessageConst.SHOW_TOAST, Language["ui_fac_common_bag_drop_same_item"])
                    return
                end

                core:Message_OpMoveItemDepotToCache(Utils.getCurrentChapterId(), dragInfo.itemId, componentId, CSIndex(self.m_slotIndex), mode)
            end
        end
    elseif source == UIConst.UI_DRAG_DROP_SOURCE_TYPE.ItemBag then

        if self:_IsFluidModeBottleDrop(dragInfo.itemId) then
            if self:_CheckIsValidBottleInLockFormula(dragInfo.itemId) then
                if not self:_CheckIsValidFluidDrop(dragInfo.itemId) then
                    Notify(MessageConst.SHOW_TOAST, Language["ui_fac_common_bag_drop_same_item"])
                    return
                end

                core:Message_OpFillingFluidComWithBag(Utils.getCurrentChapterId(), componentId, dragInfo.csIndex, CSIndex(self.m_slotIndex))
                FactoryUtils.playAudioWhenFillingItem(dragInfo.itemId, self.m_itemInfo.id, self.m_itemInfo.count)
            end
        else
            if self:_CheckIsValidItemInLockFormula(dragInfo.itemId) then
                if not self:_CheckIsValidProducerDropBagItem(dragInfo.itemId) then
                    Notify(MessageConst.SHOW_TOAST, Language["ui_fac_common_bag_drop_same_item"])
                    return
                end

                core:Message_OpMoveItemBagToCache(Utils.getCurrentChapterId(), dragInfo.csIndex, componentId, CSIndex(self.m_slotIndex), mode)
            end
        end
    end

    if dragInfo.itemId then
        local cptHandler = FactoryUtils.getBuildingComponentHandler(componentId)
        local buildingNode = cptHandler.belongNode
        local worldPos = GameInstance.remoteFactoryManager.visual:BuildingGridToWorld(
                Vector2(buildingNode.transform.position.x, buildingNode.transform.position.z))
        EventLogManagerInst:GameEvent_FactoryItemPush(buildingNode.nodeId, buildingNode.templateId,
                GameInstance.remoteFactoryManager.currentSceneName, worldPos,
                dragInfo.itemId, dragInfo.count, self.m_cache.items)
    end
end

FacCacheSlot._IsFluidModeBottleDrop = HL.Method(HL.String).Return(HL.Boolean) << function(self, itemId)
    return (self.m_cacheType ~= FacConst.FAC_CACHE_SLOT_TYPE_STATE.Normal) and (self:_IsEmptyBottleDrop(itemId) or self:_IsFullBottleDrop(itemId))
end

FacCacheSlot._IsEmptyBottleDrop = HL.Method(HL.String).Return(HL.Boolean) << function(self, itemId)
    return FactoryUtils.isEmptyBottleOrJarItem(itemId, self.m_cacheType)
end

FacCacheSlot._IsFullBottleDrop = HL.Method(HL.String).Return(HL.Boolean) << function(self, itemId)
    return FactoryUtils.isFullBottleOrJarItem(itemId, self.m_cacheType)
end

FacCacheSlot._CheckIsValidProducerDropBagItem = HL.Method(HL.String).Return(HL.Boolean) << function(self, itemId)
    if self.m_producerInfo == nil then
        return true
    end

    local checkCacheList = {
        self.m_producerInfo.cacheIn1,
        self.m_producerInfo.cacheIn2,
        self.m_producerInfo.cacheIn3,
        self.m_producerInfo.cacheIn4,
        self.m_producerInfo.cacheFluidIn1,
        self.m_producerInfo.cacheFluidIn2,
        self.m_producerInfo.cacheFluidIn3,
        self.m_producerInfo.cacheFluidIn4,
    }

    local checkFunc = function(cache)
        if cache == nil then
            return true
        end
        for id, _ in cs_pairs(cache.items) do
            if id == itemId then
                local success, order = cache.itemOrderMap:TryGetValue(id)
                if success and LuaIndex(order) ~= self.m_slotIndex then
                    return false
                end
            end
        end
        return true
    end

    for _, cache in pairs(checkCacheList) do
        local result = checkFunc(cache)
        if not checkFunc(cache) then
            return false
        end
    end

    return true
end

FacCacheSlot._CheckIsValidFluidDrop = HL.Method(HL.String).Return(HL.Boolean) << function(self, bottleOrJarId)
    if self.m_producerInfo == nil then
        return true
    end

    local itemId
    if Tables.fullBottleTable:ContainsKey(bottleOrJarId) then
        itemId = Tables.fullBottleTable[bottleOrJarId].liquidId
    end
    if Tables.fullGasJarTable:ContainsKey(bottleOrJarId) then
        itemId = Tables.fullGasJarTable[bottleOrJarId].gasId
    end
    if not itemId then
        return true
    end

    local checkCacheList = {
        self.m_producerInfo.cacheIn1,
        self.m_producerInfo.cacheIn2,
        self.m_producerInfo.cacheIn3,
        self.m_producerInfo.cacheIn4,
        self.m_producerInfo.cacheFluidIn1,
        self.m_producerInfo.cacheFluidIn2,
        self.m_producerInfo.cacheFluidIn3,
        self.m_producerInfo.cacheFluidIn4,
    }

    local checkFunc = function(cache)
        if cache == nil then
            return true
        end
        for id, _ in cs_pairs(cache.items) do
            if id == itemId then
                local success, order = cache.itemOrderMap:TryGetValue(id)
                if success and LuaIndex(order) ~= self.m_slotIndex then
                    return false
                end
            end
        end
        return true
    end

    for _, cache in pairs(checkCacheList) do
        local result = checkFunc(cache)
        if not checkFunc(cache) then
            return false
        end
    end

    return true
end

FacCacheSlot._MobileRefreshDropHighlight = HL.Method(HL.Boolean) << function(self, active)
    if not DeviceInfo.usingTouch then
        return
    end

    local itemSlot = self:_GetCurrentItemSlot()
    if not itemSlot.view.dropItem.enabled then
        return
    end

    itemSlot.view.dropHintDeco.gameObject:SetActiveIfNecessary(not active)
end

FacCacheSlot._OnMoveItemToCache = HL.Method(HL.Table) << function(self, args)
    local toComponentId, toGridIndex, itemId = unpack(args)
    if self.m_cache.componentId ~= toComponentId then
        return
    end
    if toGridIndex == CSIndex(self.m_slotIndex) then
        self.view.itemSlot:PlayDropAnimation()
        UIUtils.playItemDropAudio(itemId)
    end
end






FacCacheSlot.m_lockFormulaId = HL.Field(HL.String) << ""

FacCacheSlot._CheckIsValidItemInLockFormula = HL.Method(HL.String).Return(HL.Boolean) << function(self, itemId)
    if string.isEmpty(self.m_lockFormulaId) then
        return true
    end

    local success, craftData = Tables.factoryMachineCraftTable:TryGetValue(self.m_lockFormulaId)
    if not success then
        return true
    end

    local isValid = false
    for _, itemBundleGroup in pairs(craftData.ingredients) do
        for _, itemBundle in pairs(itemBundleGroup.group) do
            if itemBundle.id == itemId then
                isValid = true
                break
            end
        end
    end

    if not isValid then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_FAC_FORBID_CHANGE_FORMULA)
    end

    return isValid
end

FacCacheSlot._CheckIsValidBottleInLockFormula = HL.Method(HL.String).Return(HL.Boolean) << function(self, bottleId)
    if string.isEmpty(self.m_lockFormulaId) then
        return true
    end

    local success, craftData = Tables.factoryMachineCraftTable:TryGetValue(self.m_lockFormulaId)
    if not success then
        return true
    end

    if self:_IsEmptyBottleDrop(bottleId) then
        return false
    end

    local succ, bottleData = Tables.fullBottleTable:TryGetValue(bottleId)
    if not succ then
        return true
    end
    local itemId = bottleData.liquidId
    local isValid = false
    for _, itemBundleGroup in pairs(craftData.ingredients) do
        for _, itemBundle in pairs(itemBundleGroup.group) do
            if itemBundle.id == itemId then
                isValid = true
                break
            end
        end
    end

    if not isValid then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_FAC_FORBID_CHANGE_FORMULA)
    end

    return isValid
end






FacCacheSlot.ShowCacheSlotHint = HL.Method(HL.Boolean) << function(self, needShow)
    local slot = self:_GetCurrentItemSlot()
    slot.view.controllerMoveHintNode.gameObject:SetActive(needShow)
end






FacCacheSlot.RefreshSlotBlockState = HL.Method(HL.Boolean) << function(self, isBlocked)
    if self.m_isBlocked == isBlocked then
        return
    end

    local itemSlot = self:_GetCurrentItemSlot()
    local itemSlotView = itemSlot.view
    local itemView = itemSlotView.item.view
    UIUtils.PlayAnimationAndToggleActive(itemView.blockMaskAnimationWrapper, isBlocked)
    self.m_isBlocked = isBlocked
end

FacCacheSlot.GetNormalSlotLine = HL.Method().Return(HL.Userdata) << function(self)
    return self.view.itemSlot.view.facLineCell
end

FacCacheSlot.GetCurrentNormalSlotItemId = HL.Method().Return(HL.String) << function(self)
    return self.view.itemSlot.view.item.id
end

FacCacheSlot.PlaySlotAnimation = HL.Method(HL.String, HL.Opt(HL.Function)) << function(self, animationName, callback)
    if self.m_isBlocked then
        return
    end

    local slot = self:_GetCurrentItemSlot()
    slot.view.animationWrapper:PlayWithTween(animationName, callback)
end


FacCacheSlot.TryDropItem = HL.Method(HL.Forward('UIDragHelper'), HL.Boolean, HL.Opt(CS.Proto.ITEM_MOVE_MODE)).Return(HL.Boolean, HL.Boolean) << function(self, dragHelper, tryAllSlot, mode)
    if not self:CanDrop(dragHelper) then
        return false, false
    end


    if DeviceInfo.usingController and tryAllSlot then
        local source = dragHelper.source
        local dragInfo = dragHelper.info
        if source == UIConst.UI_DRAG_DROP_SOURCE_TYPE.FactoryDepot or
            source == UIConst.UI_DRAG_DROP_SOURCE_TYPE.ItemBag then
            if not self:_IsFluidModeBottleDrop(dragInfo.itemId) then
                if self:_CheckIsValidItemInLockFormula(dragInfo.itemId) then
                    if not self:_CheckIsValidProducerDropBagItem(dragInfo.itemId) then
                        return false, false
                    end
                    if not string.isEmpty(self.m_itemInfo.id) then
                        if (self.m_itemInfo.count > 0 and self.m_itemInfo.id ~= dragInfo.itemId) or
                            self.m_itemInfo.count >= self.m_itemInfo.maxStackCount then
                            return false, true
                        end
                    end
                end
            end
        end
    end

    self:_OnDropItem(dragHelper, mode)
    return true, false
end





FacCacheSlot.CanDrop = HL.Method(HL.Forward('UIDragHelper')).Return(HL.Boolean) << function(self, dragHelper)
    if not self:_ShouldAcceptDrop(dragHelper) then
        return false
    end

    if self.m_cacheType == FacConst.FAC_CACHE_SLOT_TYPE_STATE.GasLiquid then
        local itemId = dragHelper.info.itemId
        local cacheItemId = self.view.liquidItemSlot.view.item.id
        if Tables.liquidTable:ContainsKey(cacheItemId) then
            if FactoryUtils.isEmptyBottleOrJarItem(itemId, FacConst.FAC_CACHE_SLOT_TYPE_STATE.Gas) or FactoryUtils.isFullBottleOrJarItem(itemId, FacConst.FAC_CACHE_SLOT_TYPE_STATE.Gas) then
                return false
            end
        end
        if Tables.gasTable:ContainsKey(cacheItemId) then
            if FactoryUtils.isEmptyBottleOrJarItem(itemId, FacConst.FAC_CACHE_SLOT_TYPE_STATE.Liquid) or FactoryUtils.isFullBottleOrJarItem(itemId, FacConst.FAC_CACHE_SLOT_TYPE_STATE.Liquid) then
                return false
            end
        end
    end

    return true
end

FacCacheSlot.SetSlotDropEnabled = HL.Method(HL.Boolean) << function(self, isEnabled)
    self.m_isDropEnabled = isEnabled
end

FacCacheSlot.IsNaviTarget = HL.Method().Return(HL.Boolean) << function(self)
    local slot = self:_GetCurrentItemSlot()
    return slot.item.view.button.isNaviTarget
end

FacCacheSlot.SetNaviTargetToThis = HL.Method() << function(self)
    local slot = self:_GetCurrentItemSlot()
    self:SetNaviTarget(slot.item.view.button)
end

FacCacheSlot.GetNaviTarget = HL.Method().Return(HL.Userdata) << function(self)
    local slot = self:_GetCurrentItemSlot()
    return slot.item.view.button
end

FacCacheSlot.SetSlotBtnEnabled = HL.Method(HL.Boolean) << function(self, enable)
    local slot = self:_GetCurrentItemSlot()
    slot.item.view.button.enabled = enable
end

FacCacheSlot.SetSlotBtnHoverBindingEnabled = HL.Method(HL.Boolean) << function(self, enable)
    if self.m_quickDropBindingId ~= -1 then
        InputManagerInst:ToggleBinding(self.m_quickDropBindingId, enable)
    end
    self.m_isQuickDropEnabled = enable
end

FacCacheSlot.SetState = HL.Method(HL.String) << function(self, stateName)
    self.view.stateCtrl:SetState(stateName)
end




HL.Commit(FacCacheSlot)
return FacCacheSlot
