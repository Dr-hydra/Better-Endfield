local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

Depot = HL.Class('Depot', UIWidgetBase)




Depot.depotContent = HL.Field(HL.Forward("DepotContent"))

Depot.m_extraArgs = HL.Field(HL.Table)

Depot.m_inInit = HL.Field(HL.Boolean) << false

Depot.m_inited = HL.Field(HL.Boolean) << false

Depot.m_curTypeIndex = HL.Field(HL.Number) << -1

Depot.m_itemShowingTypeInfos = HL.Field(HL.Table)

Depot.m_sortOptions = HL.Field(HL.Table)

Depot.m_nonValidShowTypes = HL.Field(HL.Table)

Depot.m_typeCells = HL.Field(HL.Forward('UIListCache'))

Depot.m_valuableDepotType = HL.Field(GEnums.ItemValuableDepotType)

Depot.m_onClickItemAction = HL.Field(HL.Function)

Depot.m_onToggleDestroyMode = HL.Field(HL.Function)

Depot.m_onChangeTypeFunction = HL.Field(HL.Function)

Depot.m_oriPaddingBottom = HL.Field(HL.Number) << 0

Depot.m_waitInitNaviTarget = HL.Field(HL.Boolean) << false





Depot._OnFirstTimeInit = HL.Override() << function(self)
    self.m_typeCells = UIUtils.genCellCache(self.view.typeCell)
    self.depotContent = self.view.depotContent

    local canDes = self.view.config.CAN_DESTROY
    self.view.destroyBtn.gameObject:SetActive(canDes)
    self.view.destroyNode.gameObject:SetActive(false)
    if canDes then
        self:_InitDestroyNode()
    else
        self.m_destroyInfo = {}
    end

    self.m_oriPaddingBottom = self.depotContent.view.itemList:GetPadding().bottom


    self.view.sortNode:InitSortNode(self.m_sortOptions, function(optData, isIncremental)
        self:_OnSortChanged(optData, isIncremental)
    end, nil, nil, true)
end

Depot.InitDepot = HL.Method(GEnums.ItemValuableDepotType, HL.Opt(HL.Function, HL.Table)) << function(self, valuableDepotType, onClickItemAction, extraArgs)
    self.m_sortOptions = extraArgs.sortOptions or UIConst.FAC_DEPOT_SORT_OPTIONS
    self:_FirstTimeInit()

    self.m_inInit = true

    extraArgs = extraArgs or {}
    self.m_extraArgs = extraArgs
    self.m_nonValidShowTypes = extraArgs.nonValidShowTypes

    self.m_valuableDepotType = valuableDepotType
    self:_RefreshShowingTypeData()
    self:_TryRecoverStateBeforeInit(extraArgs.recoverState)
    local domainId = Utils.getCurDomainId()
    if extraArgs.domainId ~= nil then
        domainId = extraArgs.domainId
    end
    local succ, domainData = Tables.domainDataTable:TryGetValue(domainId)
    if succ then
        self.view.titleTxt.text = domainData.storageName
        self.view.titleBg.color = UIUtils.getColorByString(domainData.domainColor)
    else

        self.view.titleTxt.text = Language.LUA_BLACK_BOX_DEPOT_NAME
        self.view.titleBg.color = self.view.config.BLACK_BOX_TITLE_BG_COLOR
    end

    self.m_onClickItemAction = onClickItemAction
    self.m_onToggleDestroyMode = extraArgs.onToggleDestroyMode
    self.m_onChangeTypeFunction = extraArgs.onChangeTypeFunction
    self.m_waitInitNaviTarget = extraArgs.needInitNaviTarget == true
    extraArgs.showingTypes = self.m_itemShowingTypeInfos[self.m_curTypeIndex].types
    extraArgs.isIncremental = self.view.sortNode.isIncremental
    extraArgs.sortKeys = self.view.sortNode:GetCurSortKeys()

    extraArgs.beforeFindAndHighlightForDrop = function(id)
        local itemData = Tables.itemTable[id]
        local showingTypes = self.m_itemShowingTypeInfos[self.m_curTypeIndex].types
        if showingTypes and not lume.find(showingTypes, itemData.showingType) then
            for k, info in ipairs(self.m_itemShowingTypeInfos) do
                if info.types and lume.find(info.types, itemData.showingType) then
                    self.m_typeCells:Get(k).toggle.isOn = true
                    return
                end
            end
        end
    end

    local externalCustomOnUpdateCell = extraArgs.customOnUpdateCell
    extraArgs.customOnUpdateCell = function(cell, info, luaIndex)
        self:_CustomOnUpdateCell(cell, info, luaIndex)
        if externalCustomOnUpdateCell then
            externalCustomOnUpdateCell(cell, info, luaIndex)
        end
    end

    self.depotContent:InitDepotContent(valuableDepotType, function(itemId, cell)
        self:_OnClickItem(itemId, cell)
    end, extraArgs)
    self:_RefreshShowingTypeList(extraArgs.blockEmptyType)

    self.m_inInit = false
    self.m_inited = true
end

Depot.GetRecoverStateArg = HL.Method().Return(HL.Opt(HL.Any)) << function(self)
    return {
        typeIndex = self.m_curTypeIndex,
        showingType = self:_GetRecoverShowingType(),
    }
end

Depot.TryRecoverState = HL.Method(HL.Opt(HL.Any)) << function(self, recoverState)

    if recoverState == nil or not self.m_inited then
        return
    end
    local recoverTypeIndex = self:_GetRecoverTypeIndex(recoverState)
    if recoverTypeIndex == self.m_curTypeIndex then
        return
    end
    local typeCell = self.m_typeCells:Get(recoverTypeIndex)
    if typeCell ~= nil then
        typeCell.toggle.isOn = true
    end
end


Depot._TryRecoverStateBeforeInit = HL.Method(HL.Opt(HL.Any)) << function(self, recoverState)
    if recoverState == nil then
        return
    end
    self.m_curTypeIndex = self:_GetRecoverTypeIndex(recoverState)
    self:_RefreshShowingTypeTitle()
end

Depot._GetRecoverShowingType = HL.Method().Return(HL.Opt(HL.Any)) << function(self)
    local info = self.m_itemShowingTypeInfos and self.m_itemShowingTypeInfos[self.m_curTypeIndex]
    if info == nil or info.types == nil then
        return nil
    end
    return info.types[1]
end

Depot._GetRecoverTypeIndex = HL.Method(HL.Any).Return(HL.Number) << function(self, recoverState)
    local typeCount = self.m_itemShowingTypeInfos and #self.m_itemShowingTypeInfos or 0
    if typeCount <= 0 then
        return 1
    end
    local recoverShowingType = recoverState.showingType
    if recoverShowingType ~= nil then
        for index, info in ipairs(self.m_itemShowingTypeInfos) do
            if info.types ~= nil and info.types[1] == recoverShowingType then
                return index
            end
        end
    end
    local recoverTypeIndex = recoverState.typeIndex
    if type(recoverTypeIndex) == "number" then
        return math.min(math.max(recoverTypeIndex, 1), typeCount)
    end
    return 1
end

Depot._RefreshShowingTypeList = HL.Method(HL.Opt(HL.Boolean)) << function(self, blockEmptyType)
    if blockEmptyType then
        local allItemList = self.depotContent:GetAllItemInfo()
        local typeMap = {}
        for _, info in ipairs(allItemList) do
            if not typeMap[info.showingType] then
                typeMap[info.showingType] = true
            end
        end
        local resultTypeInfos = {}
        for _, info in ipairs(self.m_itemShowingTypeInfos) do
            if info.types ~= nil then
                for _, type in ipairs(info.types) do
                    if typeMap[type] == true then
                        table.insert(resultTypeInfos, info)
                        break
                    end
                end
            else
                table.insert(resultTypeInfos, info)
            end
        end
        self.m_itemShowingTypeInfos = resultTypeInfos
    end

    self.view.typesNode.gameObject:SetActive(false)
    self.m_typeCells:Refresh(#self.m_itemShowingTypeInfos, function(cell, index)
        local info = self.m_itemShowingTypeInfos[index]

        cell.dimIcon:LoadSprite(UIConst.UI_SPRITE_INVENTORY, info.icon)
        cell.lightIcon:LoadSprite(UIConst.UI_SPRITE_INVENTORY, info.icon)

        if cell.name then
            cell.name.text = info.name
        end

        local isSelected = index == self.m_curTypeIndex
        cell.decoLine.gameObject:SetActive(not (isSelected or index == 1 or index == (self.m_curTypeIndex + 1)))
        self:_RefreshTypeCellSelectedState(cell, isSelected)

        cell.toggle.onValueChanged:RemoveAllListeners()
        cell.toggle.isOn = isSelected
        cell.toggle.onValueChanged:AddListener(function(isOn)
            if isOn and self.m_curTypeIndex ~= index then
                self.depotContent:ReadCurShowingItems()
                self:_OnClickShowingType(cell, index)
            end
        end)

        cell.gameObject.name = "TypeCell_" .. index
    end)
    self.view.typesNode.gameObject:SetActive(true)
    LayoutRebuilder.ForceRebuildLayoutImmediate(self.view.typesNode.transform)
end

Depot._RefreshShowingTypeData = HL.Method() << function(self)
    self.m_curTypeIndex = 1
    self.m_itemShowingTypeInfos = {
        {
            name = Language.LUA_FAC_ALL,
            icon = "icon_item_type_all",
        }
    }
    local showingTypes = UIConst.FACTORY_DEPOT_SHOWING_TYPES
    for _, v in ipairs(showingTypes) do
        local isLocked
        if v == GEnums.ItemShowingType.PortableDevice then
            isLocked = not Utils.isSystemUnlocked(GEnums.UnlockSystemType.ColoredBagItem)
        end
        if not isLocked and (not self.m_nonValidShowTypes or not lume.find(self.m_nonValidShowTypes, v)) then
            local data = Tables.itemShowingTypeTable:GetValue(v:ToInt())
            table.insert(self.m_itemShowingTypeInfos, {
                name = data.name,
                icon = data.icon,
                types = {data.type},
            })
        end
    end
    self:_RefreshShowingTypeTitle()
end

Depot._OnClickShowingType = HL.Method(HL.Table, HL.Number) << function(self, typeCell, index)
    if DeviceInfo.usingController and InputManagerInst.controllerNaviManager:IsTopLayer(self.depotContent.view.itemListSelectableNaviGroup) then
        self.depotContent.view.itemList:ScrollToIndex(0, true)
        self.m_waitInitNaviTarget = true
    end

    self.m_curTypeIndex = index
    local showingTypes = self.m_itemShowingTypeInfos[index].types
    self.depotContent:ChangeShowingType(showingTypes)

    if self.m_onChangeTypeFunction ~= nil then
        self.m_onChangeTypeFunction()
    end

    self.m_typeCells:Update(function(cell, k)
        cell.decoLine.gameObject:SetActive(not (k == self.m_curTypeIndex or k == 1 or k == (self.m_curTypeIndex + 1)))
        self:_RefreshTypeCellSelectedState(cell, k == index)
    end)

    self:_RefreshShowingTypeTitle()

    LayoutRebuilder.ForceRebuildLayoutImmediate(self.view.typesNode.transform)

    if DeviceInfo.usingController and self.view.typesScrollRect.enabled then
        self.view.typesScrollRect:AutoScrollToRectTransform(typeCell.rectTransform)
    end
end

Depot._RefreshTypeCellSelectedState = HL.Method(HL.Any, HL.Boolean) << function(self, cell, isSelected)
    if cell == nil then
        return
    end

    cell.dimIcon.gameObject:SetActive(not isSelected)
    cell.lightNode.gameObject:SetActive(isSelected)
end

Depot._RefreshShowingTypeTitle = HL.Method() << function(self, index)
    local info = self.m_itemShowingTypeInfos[self.m_curTypeIndex]
    if info == nil then
        return
    end
    self.view.typeTitle.text = info.name
end

Depot._OnSortChanged = HL.Method(HL.Table, HL.Boolean) << function(self, optData, isIncremental)
    if self.m_inInit then
        return
    end
    self.depotContent.isIncremental = isIncremental
    if optData.reverseKeys and not isIncremental then
        self.depotContent.sortKeys = optData.reverseKeys
    else
        self.depotContent.sortKeys = optData.keys
    end
    self.depotContent:OnSortChanged()

    self.depotContent.view.itemList:ScrollToIndex(0, true)
    if DeviceInfo.usingController then
        local firstCell = self.depotContent:GetCell(1)
        if firstCell then
            self:SetNaviTarget(firstCell.item.view.button)
        end
    end
end


Depot._OnClickItem = HL.Method(HL.String, HL.Opt(HL.Forward("ItemSlot"))) << function(self, itemId, cell)
    if self.m_onClickItemAction then
        self.m_onClickItemAction(itemId, cell)
        return
    end

    if string.isEmpty(itemId) then
        return
    end

    cell.item:Read()
    if not self.m_inDestroyMode then
        if DeviceInfo.usingController then
            cell.item:ShowActionMenu()
            return
        elseif DeviceInfo.usingKeyboard and self.m_extraArgs.itemMoveTarget then
            local mode = UIUtils.getMovingItemMode()
            if mode then
                if self.m_extraArgs.itemMoveTarget == UIConst.ITEM_MOVE_TARGET.ItemBag and not Utils.isDepotManualInOutLocked() then
                    GameInstance.player.inventory:FactoryDepotMoveToItemBag(Utils.getCurrentScope(), Utils.getCurrentChapterId(), itemId, mode)
                    return
                elseif self.m_extraArgs.itemMoveTarget == UIConst.ITEM_MOVE_TARGET.FacMachine then
                    local dragHelper = cell.view.dragItem.luaTable
                    if dragHelper and cell.view.dragItem.enabled then
                        dragHelper = dragHelper[1]
                        local uiCtrl = self:GetUICtrl()
                        local facCacheArea = uiCtrl.view.cacheArea
                        local core = GameInstance.player.remoteFactory.core
                        if facCacheArea then
                            core:Message_OpMoveItemDepotToCache(Utils.getCurrentChapterId(), itemId, facCacheArea:GetDropToComponentId(dragHelper), 0, mode)
                            return
                        else
                            local storageContent = uiCtrl.view.storageContent
                            if storageContent then
                                core:Message_OpMoveItemDepotToGridBox(Utils.getCurrentChapterId(), itemId, storageContent.m_storage.componentId, 0, mode)
                                return
                            end
                        end
                    end
                end
            end
        end
        cell.item.canPlace = true
        cell.item:ShowTips()
        return
    end

    local count = self.depotContent:GetItemCount(itemId)
    if count == 0 then
        if not DeviceInfo.usingController then
            cell.item:ShowTips()
        end
        return
    end

    local showTips = not self.m_destroyInfo[itemId] and not DeviceInfo.usingController
    self:_ClickItemInDestroyMode(itemId)
    if showTips then
        local posInfo = {
            tipsPosType = UIConst.UI_TIPS_POS_TYPE.RightTop,
            tipsPosTransform = self.view.transform,
            safeArea = self.view.destroyNode.numberSelector.rectTransform,
            isSideTips = true,
        }
        cell.item.canPlace = false
        cell.item:ShowTips(posInfo, function()
            if self.m_curSelectedItemIdInDesMode == itemId then
                self.view.destroyNode.selectorNode:PlayOutAnimation()
                self.m_curSelectedItemIdInDesMode = ""
            end
        end)
    end
end




Depot.m_inDestroyMode = HL.Field(HL.Boolean) << false

Depot.m_destroyInfo = HL.Field(HL.Table)

Depot.m_curSelectedItemIdInDesMode = HL.Field(HL.String) << ""


Depot._InitDestroyNode = HL.Method() << function(self)
    self.view.destroyBtn.onClick:AddListener(function()
        self:ToggleDestroyMode(true, false)
    end)

    local node = self.view.destroyNode
    node.gameObject:SetActive(false)
    node.backBtn.onClick:AddListener(function()
        self:ToggleDestroyMode(false, false)
    end)
    node.confirmBtn.onClick:AddListener(function()
        self:_ConfirmDestroy()
    end)
    self.m_destroyInfo = {}
    self:ToggleDestroyMode(false, true)
end

Depot.ToggleDestroyMode = HL.Method(HL.Boolean, HL.Opt(HL.Boolean)) << function(self, active, noAnimation)
    if not self.m_inited then
        return
    end

    local node = self.view.destroyNode
    local rootAni = self.view.content
    if active then
        if noAnimation then
            rootAni:SampleToInAnimationEnd()
        else
            rootAni:PlayInAnimation()
        end
    else
        if noAnimation then
            rootAni:SampleToOutAnimationEnd()
        else
            rootAni:PlayOutAnimation()
        end
    end

    self.depotContent.view.itemListSelectableNaviGroup.enablePartner = not active
    self.depotContent.view.itemList:SetPaddingBottom(active and self.m_oriPaddingBottom + 100 or self.m_oriPaddingBottom)

    self.m_typeCells:Update(function(cell, _)
        if active then
            cell.animation:PlayOutAnimation()
        else
            cell.animation:PlayInAnimation()
        end
    end)

    self.m_inDestroyMode = active
    local panelId = self:GetPanelId()
    if not active then
        local info = self.m_destroyInfo
        self.m_destroyInfo = {}
        self.m_curSelectedItemIdInDesMode = ""
        if info then
            for itemId, _ in pairs(info) do
                self:_UpdateItemDestroySelect(itemId)
            end
        end

        if DeviceInfo.usingController then
            self:_RefreshShowingCellHoverTipsEnabledState()

            Notify(MessageConst.CLOSE_CONTROLLER_SMALL_MENU, self.view.inputBindingGroupMonoTarget.groupId)
            Notify(MessageConst.HIDE_ITEM_TIPS)
        end
    else
        self.m_destroyInfo = {}
        self.m_curSelectedItemIdInDesMode = ""
        self.view.destroyNode.selectorNode:SampleToOutAnimationEnd()

        self.view.destroyNode.confirmBtn.gameObject:SetActive(false)

        if DeviceInfo.usingController then
            self:_RefreshShowingCellHoverTipsEnabledState()

            Notify(MessageConst.SHOW_AS_CONTROLLER_SMALL_MENU, {
                panelId = panelId,
                isGroup = true,
                id = self.view.inputBindingGroupMonoTarget.groupId,
                hintPlaceholder = self.view.destroyNode.controllerHintPlaceholder,
                rectTransform = node.transform,
                noHighlight = true,
            })
            if not self.depotContent.view.itemListSelectableNaviGroup.IsTopLayer then
                self:SetAsNaviTarget()
            end
        end
    end


    local content = self.depotContent
    content.view.itemList:UpdateShowingCells(function(csIndex, obj)
        local cell = content.m_getCell(obj)
        local info = content.m_itemInfoList[LuaIndex(csIndex)]
        if info then
            self:_UpdateItemBlockMask(cell, info)
        end
    end)

    if self.m_onToggleDestroyMode then
        self.m_onToggleDestroyMode(active)
    end
end

Depot._UpdateItemBlockMask = HL.Method(HL.Any, HL.Table) << function(self, cell, info)
    cell.view.dragItem.disableDrag = self.m_inDestroyMode
    local showMask = false
    local id = info.id
    local button = cell.item.view.button
    if self.m_inDestroyMode then
        local inventory = GameInstance.player.inventory
        if not string.isEmpty(id) then

            showMask = not inventory:CanDestroyItem(Utils.getCurrentScope(), id) or FactoryUtils.isItemInfiniteInFactoryDepot(id)
        end
        button.clickHintTextId = self.m_destroyInfo[id] and "key_hint_common_unselect" or "key_hint_common_select"
        button.longPressHintTextId = nil
    else
        button.clickHintTextId = DeviceInfo.usingController and "key_hint_item_open_action_menu" or "key_hint_common_item_tips"
        button.longPressHintTextId = "virtual_mouse_hint_drag"
        cell.view.destroySelectNode.gameObject:SetActive(false)
    end
    InputManagerInst:SetBindingText(button.hoverConfirmBindingId, Language[button.clickHintTextId])
    cell.view.blockMask.gameObject:SetActiveIfNecessary(showMask)
end

Depot._ClickItemInDestroyMode = HL.Method(HL.String) << function(self, itemId)
    if self.m_destroyInfo[itemId] then

        self.m_destroyInfo[itemId] = nil
        self:_UpdateItemDestroySelect(itemId)

        if not string.isEmpty(self.m_curSelectedItemIdInDesMode) then
            self.view.destroyNode.selectorNode:PlayOutAnimation()
            self.m_curSelectedItemIdInDesMode = ""
        end
    else
        local inventory = GameInstance.player.inventory
        if inventory:CanDestroyItem(Utils.getCurrentScope(), itemId) then
            local count = self.depotContent:GetItemCount(itemId)
            self.m_destroyInfo[itemId] = 1
            self:_UpdateItemDestroySelect(itemId)

            self.view.destroyNode.numberSelector:InitNumberSelector(1, 1, count, function(newCount)
                self:_OnChangeItemDestroyCount(itemId, newCount)
            end)

            if string.isEmpty(self.m_curSelectedItemIdInDesMode) then
                self.view.destroyNode.selectorNode:PlayInAnimation()
            end
            self.m_curSelectedItemIdInDesMode = itemId
        else
            if not string.isEmpty(self.m_curSelectedItemIdInDesMode) then
                self.view.destroyNode.selectorNode:PlayOutAnimation()
                self.m_curSelectedItemIdInDesMode = ""
            end
            Notify(MessageConst.SHOW_TOAST, Language.LUA_ITEM_CANT_DESTROY_BECAUSE_TYPE)
        end
    end

    self.view.destroyNode.confirmBtn.gameObject:SetActive(next(self.m_destroyInfo) ~= nil)
end

Depot._UpdateItemDestroySelect = HL.Method(HL.String, HL.Opt(HL.Forward("ItemSlot"))) << function(self, itemId, cell)
    local index = self.depotContent:GetItemIndex(itemId)
    if not cell then
        cell = self.depotContent:GetCell(index)
        if not cell then
            return
        end
    end
    local totalCount = self.depotContent:GetItemCount(itemId)
    local selectCount = self.m_destroyInfo[itemId]
    if selectCount then
        cell.view.destroySelectNode.gameObject:SetActive(true)
        cell.item.view.count.text = string.format(UIConst.COLOR_STRING_FORMAT, UIConst.COUNT_RED_COLOR_STR, UIUtils.getNumString(selectCount))
        cell.item.view.button.clickHintTextId = "virtual_mouse_hint_unselect"
    else
        cell.view.destroySelectNode.gameObject:SetActive(false)
        cell.item:UpdateCount(totalCount)
        cell.item.view.button.clickHintTextId = "virtual_mouse_hint_select"
    end
    InputManagerInst:SetBindingText(cell.item.view.button.hoverConfirmBindingId, Language[cell.item.view.button.clickHintTextId])
end

Depot._OnChangeItemDestroyCount = HL.Method(HL.String, HL.Number) << function(self, itemId, newCount)
    self.m_destroyInfo[itemId] = newCount
    self:_UpdateItemDestroySelect(itemId)
end

Depot._CustomOnUpdateCell = HL.Method(HL.Forward("ItemSlot"), HL.Table, HL.Number) << function(self, cell, info, luaIndex)
    if DeviceInfo.usingController then
        cell.item.view.button.onIsNaviTargetChanged = function(active)
            if active then
                self:_RefreshNaviTargetItemState(cell, info)
            end
        end
        self:_TryDisableHoverBindingOnEmptyItem(cell, info)


        if self.depotContent.view.config.IS_SET_TIPS_POS then
            local tipsPosType = self.depotContent.view.config.IS_IN_SCREEN_LEFT
                and UIConst.UI_TIPS_POS_TYPE.RightTop
                or UIConst.UI_TIPS_POS_TYPE.LeftTop
            cell.item:SetExtraInfo({
                tipsPosType = tipsPosType,
                tipsPosTransform = self.view.transform,
            })
        end

        if luaIndex == 1 and self.m_waitInitNaviTarget then
            cell:SetAsNaviTarget()

            self:_RefreshNaviTargetItemState(cell, info)
            self.m_waitInitNaviTarget = false
        end
    end

    if self.view.config.CAN_DESTROY then
        cell.item.canDestroy = true
        if cell.item.actionMenuArgs then
            cell.item.actionMenuArgs.extraButtons = { self.view.destroyBtn }
        end
    end

    self:_UpdateItemBlockMask(cell, info)
    if not self.m_inDestroyMode then
        return
    end
    self:_UpdateItemDestroySelect(info.id, cell)
end

Depot._TryDisableHoverBindingOnEmptyItem = HL.Method(HL.Forward("ItemSlot"), HL.Opt(HL.Any)) << function(self, cell, itemBundle)
    if self.m_extraArgs.emptyItemClickable then
        return
    end
    if itemBundle == nil or string.isEmpty(itemBundle.id) then
        InputManagerInst:ToggleBinding(cell.item.view.button.hoverConfirmBindingId, false)
    else

        InputManagerInst:ToggleBinding(cell.item.view.button.hoverConfirmBindingId, cell.item.view.button.isNaviTarget)
    end
end

Depot._TryDisableItemHoverBindingOnDestroyMode = HL.Method(HL.Forward("ItemSlot")) << function(self, cell)
    InputManagerInst:ToggleGroup(cell.item.view.button.hoverBindingGroupId, not self.m_inDestroyMode)
end

Depot._RefreshDestroySelectorNodeOnHover = HL.Method(HL.Any) << function(self, itemBundle)
    local itemId = itemBundle ~= nil and itemBundle.id or ""
    if self.m_destroyInfo[itemId] then
        self.view.destroyNode.numberSelector:InitNumberSelector(self.m_destroyInfo[itemId], 1, itemBundle.count, function(newCount)
            self:_OnChangeItemDestroyCount(itemId, newCount)
        end)

        if string.isEmpty(self.m_curSelectedItemIdInDesMode) then
            self.view.destroyNode.selectorNode:PlayInAnimation()
        end
        self.m_curSelectedItemIdInDesMode = itemId
    else
        if not string.isEmpty(self.m_curSelectedItemIdInDesMode) then
            self.view.destroyNode.selectorNode:PlayOutAnimation()
            self.m_curSelectedItemIdInDesMode = ""
        end
    end
end

Depot._ShowTipsOnNaviTargetInDestroyMode = HL.Method(HL.Forward("ItemSlot"), HL.Opt(HL.Any)) << function(self, cell, itemBundle)
    if not self.m_inDestroyMode then
        return
    end

    local itemId = itemBundle ~= nil and itemBundle.id or ""
    local showTips = not string.isEmpty(itemId)
    if showTips then
        if not cell.item.showingTips then
            local posInfo = {
                tipsPosType = UIConst.UI_TIPS_POS_TYPE.RightTop,
                tipsPosTransform = self.view.transform,
                safeArea = self.view.destroyNode.numberSelector.rectTransform,
                isSideTips = true,
            }
            cell.item.canPlace = false
            cell.item.canSplit = false
            cell.item.canUse = false
            cell.item.canClear = false
            cell.item:ShowTips(posInfo)
        end
    else
        Notify(MessageConst.HIDE_ITEM_TIPS)
    end
end

Depot._ConfirmDestroy = HL.Method() << function(self)
    local items = {}
    for id, count in pairs(self.m_destroyInfo) do
        table.insert(items, {
            id = id,
            count = count,
        })
    end
    table.sort(items, Utils.genSortFunction({ "id" }, true))
    Notify(MessageConst.SHOW_POP_UP, {
        content = Language.LUA_DESTROY_ITEM_CONFIRM_TEXT,
        warningContent = Language.LUA_DESTROY_ITEM_CONFIRM_WARNING_TEXT,
        items = items,
        onConfirm = function()
            GameInstance.player.inventory:DestroyInFactoryDepot(Utils.getCurrentScope(), Utils.getCurrentChapterId(), self.m_destroyInfo)
            self:ToggleDestroyMode(false, false)
        end,
    })
end






Depot._RefreshNaviTargetItemState = HL.Method(HL.Forward("ItemSlot"), HL.Table) << function(self, cell, info)
    self:_TryDisableHoverBindingOnEmptyItem(cell, info)
    self:_TryDisableItemHoverBindingOnDestroyMode(cell)
    self:_ShowTipsOnNaviTargetInDestroyMode(cell, info)
    self:_RefreshDestroySelectorNodeOnHover(info)
    self:_RefreshCellHoverTipsEnabledState(cell)
end

Depot._RefreshShowingCellHoverTipsEnabledState = HL.Method() << function(self)
    self.depotContent.view.itemList:UpdateShowingCells(function(csIndex, obj)
        local cell = self.depotContent.m_getCell(obj)
        self:_RefreshCellHoverTipsEnabledState(cell)
    end)
end

Depot._RefreshCellHoverTipsEnabledState = HL.Method(HL.Forward("ItemSlot")) << function(self, cell)
    cell.view.item:SetEnableHoverTips(not self.m_inDestroyMode)
end

Depot.SetAsNaviTarget = HL.Method() << function(self)
    local cell = self.depotContent:GetCell(1)
    if cell then
        cell:SetAsNaviTarget()
    end
end



HL.Commit(Depot)
return Depot
