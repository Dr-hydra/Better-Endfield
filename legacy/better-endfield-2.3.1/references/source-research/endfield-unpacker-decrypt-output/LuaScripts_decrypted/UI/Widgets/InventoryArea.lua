local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

InventoryArea = HL.Class('InventoryArea', UIWidgetBase)










InventoryArea.m_opening = HL.Field(HL.Boolean) << false

InventoryArea.m_dataInited = HL.Field(HL.Boolean) << false

InventoryArea.m_isItemBag = HL.Field(HL.Boolean) << true

InventoryArea.m_args = HL.Field(HL.Table)

InventoryArea.m_cellValidStateCache = HL.Field(HL.Table)

InventoryArea.m_isDepotLocked = HL.Field(HL.Boolean) << false

InventoryArea.m_cacheAllQuickDropBindingIds = HL.Field(HL.Table)

InventoryArea.m_allQuickDropBindingSetGray = HL.Field(HL.Boolean) << false

InventoryArea.inSafeZone = HL.Field(HL.Boolean) << false


InventoryArea._OnFirstTimeInit = HL.Override() << function(self)
    self:_InitInventoryAreaController()

    self.view.itemBagBtn.onClick:AddListener(function()
        self:_Show(true)
    end)
    self.view.depotBtn.onClick:AddListener(function()
        self:_Show(false)
    end)
    self.view.itemDropAreaBtn.onClick:AddListener(function()
        self:_Show(true)
    end)
    self.view.depotDropAreaBtn.onClick:AddListener(function()
        self:_Show(false)
    end)

    self:RegisterMessage(MessageConst.ON_START_UI_DRAG, function(dragHelper)
        if UIUtils.isTypeDropValid(dragHelper, UIConst.ITEM_BAG_DROP_ACCEPT_INFO) then
            if not self.m_opening then
                local dataInited = self.m_dataInited
                if not dataInited then
                    self.view.itemBag.view.itemBagContent:OnStartUiDrag(dragHelper)
                    self.view.depot.view.depotContent:OnStartUiDrag(dragHelper)
                end
            end
            self.view.depotNotOpenDropHint.gameObject:SetActive(true)
            self.view.itemBagNotOpenDropHint.gameObject:SetActive(true)
        end
    end)
    self:RegisterMessage(MessageConst.ON_END_UI_DRAG, function(dragHelper)
        self.view.depotNotOpenDropHint.gameObject:SetActive(false)
        self.view.itemBagNotOpenDropHint.gameObject:SetActive(false)
    end)

    self:RegisterMessage(MessageConst.ON_NAVI_INVENTORY_SELECT_FLUID, function(args)
        self:_OnNaviTargetToSelectFluid(args)
    end)

    UIUtils.initUIDropHelper(self.view.itemDropArea, {
        acceptTypes = UIConst.INVENTORY_AREA_ITEM_BAG_DROP_ACCEPT_INFO,
        onToggleHighlight = function(active)
            if active then
                self:_OnDropOnArea(true)
            else

                self.view.itemBag.itemBagContent:_CancelDropHighlight()
            end
        end,
    })
    UIUtils.initUIDropHelper(self.view.depotDropArea, {
        acceptTypes = UIConst.INVENTORY_AREA_FACTORY_DEPOT_DROP_ACCEPT_INFO,
        onToggleHighlight = function(active)
            if active then
                self:_OnDropOnArea(false)
            end
        end,
    })

    self:_RegisterPlayAnimationOut()

    local ctrl = self:GetUICtrl()
    local node = self.view.moveItemMouseHintNode
    node:SetParent(ctrl.view.transform)
    node.pivot = Vector2.zero
    node.anchorMin = Vector2.zero
    node.anchorMax = Vector2.zero
    node.anchoredPosition = Vector2.zero
end

InventoryArea.InitInventoryArea = HL.Method(HL.Opt(HL.Table)) << function(self, args)
    self:_FirstTimeInit()

    self.m_cellValidStateCache = {}
    self.m_cacheAllQuickDropBindingIds = {}
    self.m_opening = false
    self.m_isItemBag = true
    self.m_args = args or {}
    self.m_args.layoutStyle = self.m_args.layoutStyle or UIConst.INVENTORY_AREA_LAYOUT_STYLE.ACCORDION
    self.m_args.itemMoveType = self.m_args.itemMoveType or UIConst.INVENTORY_AREA_ITEM_MOVE_TYPE.DEFAULT

    self.m_lockFormulaId = self.m_args.lockFormulaId or ""

    self.inSafeZone = self.m_args.ignoreInSafeZone or Utils.isInSafeZone()

    self.view.content.gameObject:SetActive(true)
    self.view.itemBagNode.gameObject:SetActive(true)
    self.view.depotNode.gameObject:SetActive(true)
    self:_Show(true, true)

    if self.m_args.noNormalCache then

        self.view.moveItemMouseHintNode.gameObject:SetActive(false)
    end
    self:_InitDepotLockInBlackbox()
end

InventoryArea._Hide = HL.Method() << function(self)
    self.m_opening = false
    self.view.content.gameObject:SetActive(false)

    local uiCtrl = self:GetUICtrl()
    if uiCtrl.view.blockMask then
        uiCtrl.view.blockMask.gameObject:SetActive(false)
    end
end

InventoryArea._Show = HL.Method(HL.Boolean, HL.Opt(HL.Boolean)) << function(self, isItemBag, forceUpdate)
    if not isItemBag and not self.inSafeZone then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_CANT_OPEN_FAC_DEPOT_IN_NON_SAFE_ZONE)
        return
    end

    if isItemBag == self.m_isItemBag and not forceUpdate then
        return
    end

    local fastMode = not self.m_opening
    local layoutStyle = self.m_args.layoutStyle
    self.m_opening = true
    self:_InitData()
    if layoutStyle == UIConst.INVENTORY_AREA_LAYOUT_STYLE.ACCORDION then
        self:_SwitchTo(isItemBag, fastMode)
    end
    self.view.content.gameObject:SetActive(true)

    local uiCtrl = self:GetUICtrl()
    if uiCtrl.view.blockMask then
        uiCtrl.view.blockMask.gameObject:SetActive(true)
    end

    self.view.itemBag.view.itemBagContent:ToggleCanQuickDrop(true)
    self.view.depot.view.depotContent:ToggleQuickAcceptDrop(true)

    self.m_isItemBag = isItemBag
    if self.m_args.onStateChange then
        self.m_args.onStateChange(isItemBag)
    end
end


InventoryArea._SwitchTo = HL.Method(HL.Boolean, HL.Boolean) << function(self, isItemBag, fastMode)
    local anim
    if self.inSafeZone then
        anim = isItemBag and "ui_storage_area_expand_down" or "ui_storage_area_expand_up"
    else
        anim = "ui_storage_area_expand_only_bag"
    end
    if fastMode then
        self.view.content:SampleClipAtPercent(anim, 1)
    else
        self.view.content:PlayWithTween(anim)
    end
    self.view.itemBag.itemBagContent.view.quickDropButton.enabled = isItemBag

    if self.inSafeZone then
        self.view.depot.depotContent.view.quickDropButton.enabled = not isItemBag
    end

    if isItemBag then
        if self.view.itemBag.view.forbiddenMask.gameObject.activeSelf then
            self.view.itemBag.view.forbiddenMask:PlayInAnimation()
        end
    else
        if self.view.depot.view.forbiddenMask.gameObject.activeSelf then
            self.view.depot.view.forbiddenMask:PlayInAnimation()
        end
    end
    if DeviceInfo.usingController then
        AudioAdapter.PostEvent("Au_UI_Button_SelectSourceBag")
    end
end

InventoryArea._InitData = HL.Method() << function(self)
    if self.m_dataInited then
        return
    end









    self.view.itemBag:InitItemBag(function(itemId, cell, csIndex)
        self:_OnClickItem(itemId, cell, csIndex)
    end, {

        canSplit = true,
        canClear = true,



        customOnUpdateCell = function(cell, itemBundle, csIndex)

            self:_RefreshItemSlotCellValidState(cell, itemBundle)
            if self.m_args.customOnUpdateCell ~= nil then

                self.m_args.customOnUpdateCell(cell, itemBundle, LuaIndex(csIndex), true)
            end


            if DeviceInfo.usingController then
                cell.item:SetExtraInfo({
                    tipsPosType = UIConst.UI_TIPS_POS_TYPE.RightTop,
                    tipsPosTransform = self.view.itemBag.transform,
                })
            end
            cell.item.view.button.onIsNaviTargetChanged = function(active)
                if active then
                    self:_TryDisableHoverBindingOnEmptyItem(cell, itemBundle)

                    if self.m_curNaviSelectComponentId ~= -1 then
                        InputManagerInst:ToggleBinding(self.m_confirmSelectFluidBindingId, self:_CheckBottleDrop(itemBundle.id))
                    end
                end
            end
            self:_TryDisableHoverBindingOnEmptyItem(cell, itemBundle)
            InputManagerInst:SetBindingText(cell.item.view.button.hoverConfirmBindingId, Language["key_hint_item_open_action_menu"])
            if self:_GetIsValidControllerNaviTarget(cell, itemBundle, true) then
                local quickDropId = cell.item:AddHoverBinding("common_quick_drop", function()
                    cell:QuickDrop()
                end)
                if self.m_args.adaptForceQuickDropKeyhintToGray then
                    self.m_cacheAllQuickDropBindingIds["BAG_" .. csIndex] = quickDropId
                    if self.m_allQuickDropBindingSetGray then
                        InputManagerInst:ForceBindingKeyhintToGray(quickDropId, true)
                    end
                end
                if self.m_args.hasFluidInCache or self.m_args.hasGasInCache then
                    local itemId = itemBundle.id
                    if self.m_args.hasFluidInCache then
                        local isEmptyBottle = FactoryUtils.isEmptyBottleOrJarItem(itemId, FacConst.FAC_CACHE_SLOT_TYPE_STATE.Liquid)
                        local isFullBottle = FactoryUtils.isFullBottleOrJarItem(itemId, FacConst.FAC_CACHE_SLOT_TYPE_STATE.Liquid)
                        local isBottle = isEmptyBottle or isFullBottle
                        if isBottle then
                            local quickDropText = isEmptyBottle and Language.LUA_ITEM_ACTION_FILL_LIQUID or Language.LUA_ITEM_ACTION_DUMP_LIQUID
                            InputManagerInst:SetBindingText(quickDropId, quickDropText)
                        end
                    end
                    if self.m_args.hasGasInCache then
                        local isEmptyGasJar = FactoryUtils.isEmptyBottleOrJarItem(itemId, FacConst.FAC_CACHE_SLOT_TYPE_STATE.Gas)
                        local isFullGasJar = FactoryUtils.isFullBottleOrJarItem(itemId, FacConst.FAC_CACHE_SLOT_TYPE_STATE.Gas)
                        local isGasJar = isEmptyGasJar or isFullGasJar
                        if isGasJar then
                            local quickDropText = isEmptyGasJar and Language.LUA_ITEM_ACTION_FILL_GAS or Language.LUA_ITEM_ACTION_DUMP_GAS
                            InputManagerInst:SetBindingText(quickDropId, quickDropText)
                        end
                    end
                else
                    local quickDropBindingText = self.m_args.itemBagQuickDropBindingText
                    if not string.isEmpty(quickDropBindingText) then
                        InputManagerInst:SetBindingText(quickDropId, quickDropBindingText)
                    end
                end
            else
                if self.m_args.adaptForceQuickDropKeyhintToGray and self.m_cacheAllQuickDropBindingIds["BAG_" .. csIndex] then
                    self.m_cacheAllQuickDropBindingIds["BAG_" .. csIndex] = nil
                end
            end
            self:_RefreshForbiddenItemActionMenuArgs(cell, itemBundle)
        end,

        customSetActionMenuArgs = self.m_args.customSetActionMenuArgs,
    })

    if self.inSafeZone then
        self:_InitDepot()
    end

    self.m_dataInited = true
end

InventoryArea._GetRecoverDepotState = HL.Method().Return(HL.Opt(HL.Any)) << function(self)
    local recoverState = self.m_args and self.m_args.recoverState
    if recoverState == nil then
        return nil
    end
    return recoverState.depotState
end

InventoryArea._GetDepotInitArgs = HL.Method().Return(HL.Table) << function(self)
    return {

        canClear = true,



        itemMoveTarget = UIConst.ITEM_MOVE_TARGET.FacMachine,
        recoverState = self:_GetRecoverDepotState(),

        customItemInfoListPostProcess = function(allItemInfoList)

            for _, info in ipairs(allItemInfoList) do

                local isValid = self:_IsItemValid(info.id)
                if not isValid then
                    info.missionSortId = info.missionSortId + 100
                    info.missionReverseSortId = -info.missionSortId
                end
            end
            return allItemInfoList
        end,

        customOnUpdateCell = function(cell, itemBundle, luaIndex)

            self:_RefreshItemSlotCellValidState(cell, itemBundle)
            if self.m_args.customOnUpdateCell ~= nil then
                self.m_args.customOnUpdateCell(cell, itemBundle, luaIndex, false)
            end


            cell.item.view.button.onIsNaviTargetChanged = function(active)
                if active then
                    self:_TryDisableHoverBindingOnEmptyItem(cell, itemBundle)

                    if self.m_curNaviSelectComponentId ~= -1 then
                        InputManagerInst:ToggleBinding(self.m_confirmSelectFluidBindingId, self:_CheckBottleDrop(itemBundle.id))
                    end
                end
            end
            self:_TryDisableHoverBindingOnEmptyItem(cell, itemBundle)
            if self:_GetIsValidControllerNaviTarget(cell, itemBundle, true) then
                local quickDropBindingId = cell.item:AddHoverBinding("common_quick_drop", function()
                    cell:QuickDrop()
                end)
                if self.m_args.adaptForceQuickDropKeyhintToGray then
                    self.m_cacheAllQuickDropBindingIds["DEPOT_" .. itemBundle.id] = quickDropBindingId
                    if self.m_allQuickDropBindingSetGray then
                        InputManagerInst:ForceBindingKeyhintToGray(quickDropBindingId, true)
                    end
                end
                if self.m_args.hasFluidInCache or self.m_args.hasGasInCache then
                    local itemId = itemBundle.id
                    if self.m_args.hasFluidInCache then
                        local isEmptyBottle = FactoryUtils.isEmptyBottleOrJarItem(itemId, FacConst.FAC_CACHE_SLOT_TYPE_STATE.Liquid)
                        local isFullBottle = FactoryUtils.isFullBottleOrJarItem(itemId, FacConst.FAC_CACHE_SLOT_TYPE_STATE.Liquid)
                        local isBottle = isEmptyBottle or isFullBottle
                        if isBottle then
                            local quickDropText = isEmptyBottle and Language.LUA_ITEM_ACTION_FILL_LIQUID or Language.LUA_ITEM_ACTION_DUMP_LIQUID
                            InputManagerInst:SetBindingText(quickDropBindingId, quickDropText)
                        end
                    end
                    if self.m_args.hasGasInCache then
                        local isEmptyGasJar = FactoryUtils.isEmptyBottleOrJarItem(itemId, FacConst.FAC_CACHE_SLOT_TYPE_STATE.Gas)
                        local isFullGasJar = FactoryUtils.isFullBottleOrJarItem(itemId, FacConst.FAC_CACHE_SLOT_TYPE_STATE.Gas)
                        local isGasJar = isEmptyGasJar or isFullGasJar
                        if isGasJar then
                            local quickDropText = isEmptyGasJar and Language.LUA_ITEM_ACTION_FILL_GAS or Language.LUA_ITEM_ACTION_DUMP_GAS
                            InputManagerInst:SetBindingText(quickDropBindingId, quickDropText)
                        end
                    end
                else
                    local quickDropBindingText = self.m_args.depotQuickDropBindingText
                    if not string.isEmpty(quickDropBindingText) then
                        InputManagerInst:SetBindingText(quickDropBindingId, quickDropBindingText)
                    end
                end
            else
                if self.m_args.adaptForceQuickDropKeyhintToGray and self.m_cacheAllQuickDropBindingIds["DEPOT_" .. itemBundle.id] then
                    self.m_cacheAllQuickDropBindingIds["DEPOT_" .. itemBundle.id] = nil
                end
            end
            self:_RefreshForbiddenItemActionMenuArgs(cell, itemBundle)
        end,

        customSetActionMenuArgs = self.m_args.customSetActionMenuArgs
    }
end

InventoryArea._InitDepot = HL.Method() << function(self)
    self.view.depot:InitDepot(GEnums.ItemValuableDepotType.Factory, function(itemId, cell)
        self:_OnClickItem(itemId, cell)
    end, self:_GetDepotInitArgs())
end

InventoryArea._OnValueChanged = HL.Method(HL.Boolean) << function(self, isOn)
    if isOn == self.m_opening then
        return
    end

    if isOn then
        self:_Show(self.m_isItemBag)
    else
        self:_Hide()
    end
end

InventoryArea._OnDropOnArea = HL.Method(HL.Boolean) << function(self, isItemBag)
    if isItemBag ~= self.m_isItemBag then
        self:_Show(isItemBag)
        if isItemBag then
            self.view.itemBag.itemBagContent:_FindAndHighlightForDrop()
        else
            self.view.depot.depotContent:_FindAndHighlightForDrop()
        end
    end
end

InventoryArea._OnDestroy = HL.Override() << function(self)
    if not self.m_dataInited then
        return
    end
    if self.m_curNaviSelectComponentId ~= -1 then
        self:_ClearSelectFluidBinding(true)
    end
    self.view.itemBag.itemBagContent:ReadCurShowingItems()
    if self.inSafeZone then
        self.view.depot.depotContent:ReadCurShowingItems()
    end
end

InventoryArea.PlayAnimationOut = HL.Override() << function(self)
    self:_Hide()
end

InventoryArea._OnClickItem = HL.Method(HL.String, HL.Forward('ItemSlot'), HL.Opt(HL.Number)) << function(self, itemId, cell, csIndex)
    cell.item:Read()
    if DeviceInfo.usingController then
        cell.item:ShowActionMenu(true)
        return
    elseif DeviceInfo.usingKeyboard then

        local itemMoveCheckFunc = self.m_args.itemMoveCheckFunc
        local valid = itemMoveCheckFunc == nil or itemMoveCheckFunc()
        if valid then
            local mode = UIUtils.getMovingItemMode()
            if mode then
                local dragHelper = cell.view.dragItem.luaTable
                if not cell.view.dragItem.enabled then

                    Notify(MessageConst.SHOW_TOAST, Language.LUA_QUICK_DROP_INVALID)
                    return
                end
                if dragHelper then
                    dragHelper = dragHelper[1]
                    local uiCtrl = self:GetUICtrl()
                    local itemMoveType = self.m_args.itemMoveType
                    if itemMoveType == UIConst.INVENTORY_AREA_ITEM_MOVE_TYPE.BAG_TO_DEPOT then
                        local scope = Utils.getCurrentScope()
                        local chapterId = Utils.getCurrentChapterId()
                        if csIndex then
                            GameInstance.player.inventory:ItemBagMoveToFactoryDepot(scope, chapterId, csIndex, mode)
                        else
                            GameInstance.player.inventory:FactoryDepotMoveToItemBag(scope, chapterId, itemId, mode)
                        end
                        return
                    end
                    local facCacheArea = uiCtrl.view.cacheArea
                    local core = GameInstance.player.remoteFactory.core
                    local chapterId = Utils.getCurrentChapterId()
                    if facCacheArea then
                        if not self.m_args.noNormalCache then
                            local cptId = facCacheArea:GetDropToComponentId(dragHelper)
                            if self:_CheckIsValidItemInLockFormula(itemId) then
                                if self.m_isItemBag then
                                    core:Message_OpMoveItemBagToCache(chapterId, csIndex, cptId, 0, mode)
                                else
                                    core:Message_OpMoveItemDepotToCache(chapterId, itemId, cptId, 0, mode)
                                end
                            end
                        end
                        return
                    else
                        local storageContent = uiCtrl.view.storageContent
                        if storageContent then
                            local cptId = storageContent.m_storage.componentId
                            if self.m_isItemBag then
                                core:Message_OpMoveItemBagToGridBox(chapterId, csIndex, cptId, 0, mode)
                            else
                                core:Message_OpMoveItemDepotToGridBox(chapterId, itemId, cptId, 0, mode)
                            end
                            return
                        else

                            local facCacheRepository = uiCtrl.view.facCacheRepository
                            if not facCacheRepository then
                                return
                            end
                            if facCacheRepository:GetRepoCacheType() ~= FacConst.FAC_CACHE_SLOT_TYPE_STATE.Normal then
                                Notify(MessageConst.SHOW_TOAST, Language.LUA_QUICK_DROP_INVALID)
                                return
                            end
                            local cptId = facCacheRepository.m_cache.componentId
                            if self.m_isItemBag then
                                core:Message_OpMoveItemBagToCache(chapterId, csIndex, cptId, 0, mode)
                            else
                                core:Message_OpMoveItemDepotToCache(chapterId, itemId, cptId, 0, mode)
                            end
                            return
                        end
                    end
                end
            end
        end
    end
    cell.item:ShowTips()
end

InventoryArea._RefreshItemSlotCellValidState = HL.Method(HL.Userdata, HL.Any) << function(self, cell, itemBundle)
    if itemBundle == nil then
        return
    end

    local itemId = itemBundle.id
    local isEmpty = string.isEmpty(itemId)
    if isEmpty then
        cell.view.forbiddenMask.gameObject:SetActive(false)
        cell.view.dragItem.enabled = false
        cell.view.dropItem.enabled = true
        return
    end

    local isValid = self:_IsItemValid(itemId)

    if self.m_curNaviSelectComponentId ~= -1 then
        isValid = isValid and self:_CheckBottleDrop(itemId)
    end

    cell.view.forbiddenMask.gameObject:SetActive(not isValid)
    cell.view.dragItem.enabled = cell.view.dragItem.enabled and isValid
    cell.view.dropItem.enabled = isValid
end

InventoryArea._IsItemValid = HL.Method(HL.String).Return(HL.Boolean) << function(self, itemId)
    if string.isEmpty(itemId) then
        return true
    end
    if self.m_cellValidStateCache[itemId] == nil then
        local itemValid
        local customCheckItemValid = self.m_args.customCheckItemValid
        if customCheckItemValid then
            itemValid = customCheckItemValid(itemId)
        else
            local success, factoryItemData = Tables.factoryItemTable:TryGetValue(itemId)
            if not success then
                logger.error("Can't find in factoryItemTable", itemId)
                return false
            end
            itemValid = factoryItemData.buildingBufferStackLimit > 0
        end
        self.m_cellValidStateCache[itemId] = itemValid
    end
    return self.m_cellValidStateCache[itemId]
end


InventoryArea._InitDepotLockInBlackbox = HL.Method() << function(self)
    local isLocked = Utils.isDepotManualInOutLocked()
    self.view.depot.view.forbiddenMask.gameObject:SetActive(isLocked)
    self.view.depot.view.depotContent:ToggleAcceptDrop(not isLocked)
    self.m_isDepotLocked = isLocked
    if isLocked then

        self.view.moveItemMouseHintNode.gameObject:SetActive(false)
    end
end

InventoryArea.LockInventoryArea = HL.Method(HL.Boolean) << function(self, isLocked)
    local isDepotLocked = isLocked or self.m_isDepotLocked

    self.view.itemBag.view.forbiddenMask.gameObject:SetActive(isLocked)
    self.view.depot.view.forbiddenMask.gameObject:SetActive(isDepotLocked)

    self.view.itemBag.view.itemBagContent:ToggleCanDrop(not isLocked)
    self.view.depot.view.depotContent:ToggleAcceptDrop(not isDepotLocked)

    self.m_itemBagNaviGroup.enabled = not isLocked
    self.m_depotNaviGroup.enabled = not isDepotLocked
end

InventoryArea.SetState = HL.Method(HL.String) << function(self, stateName)
    self.view.stateCtrl:SetState(stateName)
end




InventoryArea.m_lockFormulaId = HL.Field(HL.String) << ""

InventoryArea._CheckIsValidItemInLockFormula = HL.Method(HL.String).Return(HL.Boolean) << function(self, itemId)
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






InventoryArea.m_itemBagNaviGroup = HL.Field(HL.Userdata)

InventoryArea.m_depotNaviGroup = HL.Field(HL.Userdata)

InventoryArea.m_confirmSelectFluidBindingId = HL.Field(HL.Number) << -1

InventoryArea.m_cancelSelectFluidBindingId = HL.Field(HL.Number) << -1

InventoryArea.m_curNaviSelectComponentId = HL.Field(HL.Number) << -1

InventoryArea.m_curNaviSelectSlotIndex = HL.Field(HL.Number) << -1

InventoryArea.m_curNaviSelectFluidId = HL.Field(HL.String) << ""

InventoryArea.m_curNaviSourceItem = HL.Field(HL.Any)

InventoryArea.m_curNaviCacheType = HL.Field(HL.Number) << -1

InventoryArea._InitInventoryAreaController = HL.Method() << function(self)
    self.m_itemBagNaviGroup = self.view.itemBag.view.itemBagContent.view.itemListSelectableNaviGroup
    self.m_depotNaviGroup = self.view.depot.view.depotContent.view.itemListSelectableNaviGroup

    self:_RefreshItemBagGetDefaultNaviTargetFunc()

    self:BindInputPlayerAction("fac_inventory_area_switch", function()
        local nextIsItemBag
        if self.m_isItemBag and self.m_itemBagNaviGroup.IsTopLayer then
            nextIsItemBag = false
        elseif not self.m_isItemBag and self.m_depotNaviGroup.IsTopLayer then
            nextIsItemBag = true
        end
        self:_Show(not self.m_isItemBag, false)
        if nextIsItemBag ~= nil then
            self:NaviToPart(nextIsItemBag, true)
        end
    end)


    self.m_confirmSelectFluidBindingId = UIUtils.bindInputPlayerAction("fac_move_to_in_cache_slot_confirm", function()
        self:_OnNaviTargetToSelectFluidConfirm()
    end, self.view.inputGroup.groupId)
    self.m_cancelSelectFluidBindingId = UIUtils.bindInputPlayerAction("common_cancel", function()
        self:_ClearSelectFluidBinding()
    end, self.view.inputGroup.groupId)
    InputManagerInst:ToggleBinding(self.m_confirmSelectFluidBindingId, false)
    InputManagerInst:ToggleBinding(self.m_cancelSelectFluidBindingId, false)
end

InventoryArea._OnNaviTargetToSelectFluidConfirm = HL.Method() << function(self)
    if self.m_isItemBag then
        local csIndex, slot = self.view.itemBag.itemBagContent:GetCurNaviTargetSlot()
        if csIndex ~= -1 and self:_CheckBottleDrop(slot.item.id) then
            local core = GameInstance.player.remoteFactory.core
            core:Message_OpFillingFluidComWithBag(Utils.getCurrentChapterId(), self.m_curNaviSelectComponentId, csIndex, self.m_curNaviSelectSlotIndex)
            if self.m_curNaviSourceItem ~= nil then
                FactoryUtils.playAudioWhenFillingItem(slot.item.id, self.m_curNaviSourceItem.id, self.m_curNaviSourceItem.count)
            end
            self:_ClearSelectFluidBinding()
            return
        end
    else
        local csIndex, slot = self.view.depot.depotContent:GetCurNaviTargetSlot()
        if csIndex ~= -1 and self:_CheckBottleDrop(slot.item.id) then
            local core = GameInstance.player.remoteFactory.core
            core:Message_OpFillingFluidComWithDepot(Utils.getCurrentChapterId(), self.m_curNaviSelectComponentId, slot.item.id, self.m_curNaviSelectSlotIndex)
            if self.m_curNaviSourceItem ~= nil then
                FactoryUtils.playAudioWhenFillingItem(slot.item.id, self.m_curNaviSourceItem.id, self.m_curNaviSourceItem.count)
            end
            self:_ClearSelectFluidBinding()
            return
        end
    end
end

InventoryArea._ClearSelectFluidBinding = HL.Method(HL.Opt(HL.Boolean)) << function(self, destroy)
    self.m_curNaviSelectComponentId = -1
    self.m_curNaviSelectFluidId = ""
    self.view.naviTargetToSelectDeco.gameObject:SetActiveIfNecessary(false)
    if self.m_curNaviSourceItem ~= nil then
        self.m_curNaviSourceItem:SetSelected(false)
        if not destroy then
            self:SetNaviTarget(self.m_curNaviSourceItem.view.button)
        end
        self.m_curNaviSourceItem = nil
    end
    self.m_curNaviCacheType = -1
    self.m_itemBagNaviGroup.enablePartner = true
    self.m_depotNaviGroup.enablePartner = true
    InputManagerInst:ToggleBinding(self.m_confirmSelectFluidBindingId, false)
    InputManagerInst:ToggleBinding(self.m_cancelSelectFluidBindingId, false)
    self.view.itemBag.itemBagContent:Refresh(true)
    if self.inSafeZone then
        self.view.depot.depotContent:RefreshAll(true)
    end
    Notify(MessageConst.CLOSE_CONTROLLER_SMALL_MENU, self.view.inputGroup.groupId)
    Notify(MessageConst.FAC_ON_MOVE_HIDE_CONTROLLER_MODE_HINT)
end

InventoryArea._CheckBottleDrop = HL.Method(HL.String).Return(HL.Boolean) << function(self, itemId)
    if string.isEmpty(self.m_curNaviSelectFluidId) then
        return FactoryUtils.isEmptyBottleOrJarItem(itemId, self.m_curNaviCacheType)
    else
        if self.m_curNaviCacheType == FacConst.FAC_CACHE_SLOT_TYPE_STATE.Liquid then
            local fullBottleSuccess, fullBottleData = Tables.fullBottleTable:TryGetValue(itemId)
            if fullBottleSuccess then
                return fullBottleData.liquidId == self.m_curNaviSelectFluidId
            end
        elseif self.m_curNaviCacheType == FacConst.FAC_CACHE_SLOT_TYPE_STATE.Gas then
            local fullGasJarSuccess, fullGasJarData = Tables.fullGasJarTable:TryGetValue(itemId)
            if fullGasJarSuccess then
                return fullGasJarData.gasId == self.m_curNaviSelectFluidId
            end
        end
    end
    return false
end

InventoryArea._OnNaviTargetToSelectFluid = HL.Method(HL.Table) << function(self, args)
    self.m_curNaviSelectComponentId = args.componentId
    self.m_curNaviSelectSlotIndex = args.slotIndex
    self.m_curNaviSelectFluidId = args.fluidId
    self.m_curNaviSourceItem = args.sourceItem
    self.m_curNaviCacheType = args.cacheType
    self.m_curNaviSourceItem:SetSelected(true)
    self.view.naviTargetToSelectDeco.gameObject:SetActiveIfNecessary(true)
    InputManagerInst:ToggleBinding(self.m_confirmSelectFluidBindingId, true)
    InputManagerInst:ToggleBinding(self.m_cancelSelectFluidBindingId, true)
    local ctrl = self:GetUICtrl()
    Notify(MessageConst.SHOW_AS_CONTROLLER_SMALL_MENU, {
        panelId = ctrl.panelId,
        isGroup = true,
        id = self.view.inputGroup.groupId,
        hintPlaceholder = ctrl.view.controllerHintPlaceholder,
        noHighlight = true,
        rectTransform = self.view.rectTransform,
    })
    local textId
    if self.m_curNaviCacheType == FacConst.FAC_CACHE_SLOT_TYPE_STATE.Liquid then
        textId = string.isEmpty(args.fluidId) and "LUA_ITEM_ACTION_CACHE_SELECT_FILL_LIQUID" or "LUA_ITEM_ACTION_CACHE_SELECT_DUMP_LIQUID"
    elseif self.m_curNaviCacheType == FacConst.FAC_CACHE_SLOT_TYPE_STATE.Gas then
        textId = string.isEmpty(args.fluidId) and "LUA_ITEM_ACTION_CACHE_SELECT_FILL_GAS" or "LUA_ITEM_ACTION_CACHE_SELECT_DUMP_GAS"
    end
    Notify(MessageConst.FAC_ON_MOVE_SHOW_CONTROLLER_MODE_HINT, Language[textId])

    self.view.itemBag.itemBagContent:Refresh(true)
    if self.inSafeZone then
        self.view.depot.depotContent:RefreshAll(true)
    end

    if self.m_isItemBag then
        self.view.itemBag.itemBagContent:CheckAndNaviToTargetCell(function(itemId)
            return self:_CheckBottleDrop(itemId)
        end)
    else
        self.view.depot.depotContent:CheckAndNaviToTargetCell(function(itemId)
            return self:_CheckBottleDrop(itemId)
        end)
    end
    self.m_itemBagNaviGroup.enablePartner = false
    self.m_depotNaviGroup.enablePartner = false
end

InventoryArea.AddNaviGroupSwitchInfo = HL.Method(HL.Table) << function(self, naviGroupInfos)
    if self.m_isItemBag then
        table.insert(naviGroupInfos, self:GetItemBagNaviGroupSwitchInfo())
    else
        if self.inSafeZone then
            table.insert(naviGroupInfos, self:GetDepotNaviGroupSwitchInfo())
        end
    end
end

InventoryArea.GetItemBagNaviGroupSwitchInfo = HL.Method().Return(HL.Table) << function(self)
    return {
        naviGroup = self.m_itemBagNaviGroup,
        text = Language.LUA_INV_NAVI_SWITCH_TO_ITEM_BAG,
        forceDefault = true,
        beforeSwitch = function()
            self:_Show(true)
        end
    }
end

InventoryArea.GetDepotNaviGroupSwitchInfo = HL.Method().Return(HL.Table) << function(self)
    return {
        naviGroup = self.m_depotNaviGroup,
        text = Language.LUA_INV_NAVI_SWITCH_TO_DEPOT,
        forceDefault = true,
        beforeSwitch = function()
            self:_Show(false)
        end
    }
end

InventoryArea.GetRecoverStateArg = HL.Method().Return(HL.Opt(HL.Any)) << function(self)
    local recoverState = {
        isItemBag = self.m_isItemBag,
    }
    if self.inSafeZone then
        recoverState.depotState = self.view.depot:GetRecoverStateArg()
    end
    return recoverState
end

InventoryArea.TryRecoverState = HL.Method(HL.Opt(HL.Any)) << function(self, recoverState)
    if recoverState == nil then
        return
    end

    local recoverIsItemBag = recoverState.isItemBag
    if type(recoverIsItemBag) ~= "boolean" then
        recoverIsItemBag = true
    end
    if not recoverIsItemBag and not self.inSafeZone then
        recoverIsItemBag = true
    end

    if self.inSafeZone and recoverState.depotState ~= nil then
        self.view.depot:TryRecoverState(recoverState.depotState)
    end

    local stateCtrl = self.view.stateCtrl
    if stateCtrl ~= nil and stateCtrl.currentStateName == "Disabled" then
        self.m_isItemBag = recoverIsItemBag
        return
    end

    self:_Show(recoverIsItemBag, true)
end

InventoryArea._RefreshItemBagGetDefaultNaviTargetFunc = HL.Method() << function(self)
    self.m_itemBagNaviGroup.getDefaultSelectableFunc = function()
        local itemList = self.view.itemBag.view.itemBagContent.view.itemList
        local firstIndex = itemList:GetShowingCellsIndexRange()
        local itemSlot = self.view.itemBag.view.itemBagContent:GetCell(LuaIndex(firstIndex))
        return itemSlot.view.item.view.button
    end
    self.m_depotNaviGroup.getDefaultSelectableFunc = function()
        local itemList = self.view.depot.view.depotContent.view.itemList
        local firstIndex = itemList:GetShowingCellsIndexRange()
        local itemSlot = self.view.depot.view.depotContent:GetCell(LuaIndex(firstIndex))
        return itemSlot.view.item.view.button
    end
end

InventoryArea._TryDisableHoverBindingOnEmptyItem = HL.Method(HL.Forward("ItemSlot"), HL.Opt(HL.Any)) << function(self, cell, itemBundle)
    if self:_GetIsValidControllerNaviTarget(cell, itemBundle, true) then
        return
    end
    InputManagerInst:ToggleBinding(cell.item.view.button.hoverConfirmBindingId, false)
end

InventoryArea._GetIsValidControllerNaviTarget = HL.Method(HL.Forward("ItemSlot"), HL.Any, HL.Boolean).Return(HL.Boolean) << function(self, cell, itemBundle, considerForbidden)

    if self.m_curNaviSelectComponentId ~= -1 then
        return false
    end
    if itemBundle ~= nil and not string.isEmpty(itemBundle.id) then
        if considerForbidden then
            local isForbidden = not self.m_cellValidStateCache[itemBundle.id] or cell.view.forbiddenMask.gameObject.activeSelf
            if isForbidden then
                return false
            end
        end
        if cell:_GetQuickDropTarget() then
            return true
        end
    end
    return false
end

InventoryArea._RefreshForbiddenItemActionMenuArgs = HL.Method(HL.Forward("ItemSlot"), HL.Any) << function(self, cell, itemBundle)

    if itemBundle == nil then
        return
    end
    local isForbidden = not self.m_cellValidStateCache[itemBundle.id] or cell.view.forbiddenMask.gameObject.activeSelf
    if isForbidden then
        cell.item.actionMenuArgs = {}
    end
end

InventoryArea.NaviToPart = HL.Method(HL.Boolean, HL.Boolean) << function(self, toItemBag, toTop)
    if toItemBag then
        if toTop then
            self.view.itemBag.itemBagContent.view.itemList:SetTop()
        end
        local cell = self.view.itemBag.itemBagContent:GetCell(1)
        if cell then
            cell:SetAsNaviTarget()
        end
    elseif self.inSafeZone then
        if toTop then
            self.view.depot.depotContent.view.itemList:SetTop()
        end
        self.view.depot:SetAsNaviTarget()
    end
end

InventoryArea.IsNaviGroupTopLayer = HL.Method(HL.Boolean).Return(HL.Boolean) << function(self, isItemBag)
    if isItemBag then
        return self.m_itemBagNaviGroup.IsTopLayer
    end
    return self.m_depotNaviGroup.IsTopLayer
end

InventoryArea.GetNaviGroup = HL.Method(HL.Boolean).Return(HL.Userdata) << function(self, isItemBag)
    if isItemBag then
        return self.m_itemBagNaviGroup
    end
    return self.m_depotNaviGroup
end

InventoryArea.ToggleAllQuickDropBindings = HL.Method(HL.Boolean) << function(self, active)
    if not self.m_cacheAllQuickDropBindingIds then
        return
    end
    for i, bindingId in pairs(self.m_cacheAllQuickDropBindingIds) do
        InputManagerInst:ToggleBinding(bindingId, active)
    end
end

InventoryArea.SetAllQuickDropBindingGray = HL.Method(HL.Boolean) << function(self, grayState)
    if not self.m_args.adaptForceQuickDropKeyhintToGray then
        return
    end
    self.m_allQuickDropBindingSetGray = grayState
    for index, bingdingId in pairs(self.m_cacheAllQuickDropBindingIds) do
        InputManagerInst:ForceBindingKeyhintToGray(bingdingId, grayState)
    end
end


InventoryArea.SetBuildingHasFluidCache = HL.Method(HL.Opt(HL.Boolean)) << function(self, hasFluidCache)
    self.m_args.hasFluidInCache = hasFluidCache

    self.view.itemBag.itemBagContent:Refresh(true)
    if self.inSafeZone then
        self.view.depot.depotContent:RefreshAll(true)
    end
end

InventoryArea.SetBuildingNoNormalCache = HL.Method(HL.Opt(HL.Boolean)) << function(self, noNormalCache)
    self.m_args.noNormalCache = noNormalCache
    self.view.moveItemMouseHintNode.gameObject:SetActive(not noNormalCache)
end




HL.Commit(InventoryArea)
return InventoryArea
