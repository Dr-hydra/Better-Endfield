local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.DomainDepotGoodsPack
local DomainDepotDeliverItemType = GEnums.DomainDepotDeliverItemType
local DeliverPackType = GEnums.DeliverPackType

DomainDepotGoodsPackCtrl = HL.Class('DomainDepotGoodsPackCtrl', uiCtrl.UICtrl)






DomainDepotGoodsPackCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_DOMAIN_DEPOT_PACK_FAILED] = '_OnPackFailed',
}

DomainDepotGoodsPackCtrl.m_domainId = HL.Field(HL.String) << ""

DomainDepotGoodsPackCtrl.m_depotId = HL.Field(HL.String) << ""

DomainDepotGoodsPackCtrl.m_minLimitValue = HL.Field(HL.Number) << 0

DomainDepotGoodsPackCtrl.m_maxLimitValue = HL.Field(HL.Number) << 0

DomainDepotGoodsPackCtrl.m_itemType = HL.Field(DomainDepotDeliverItemType)

DomainDepotGoodsPackCtrl.m_packType = HL.Field(DeliverPackType)

DomainDepotGoodsPackCtrl.m_itemValueGetter = HL.Field(HL.Table)

DomainDepotGoodsPackCtrl.m_itemCellGetFunc = HL.Field(HL.Function)

DomainDepotGoodsPackCtrl.m_itemInfoList = HL.Field(HL.Table)

DomainDepotGoodsPackCtrl.m_selectedItemList = HL.Field(HL.Table)

DomainDepotGoodsPackCtrl.m_currSelectedItemIndex = HL.Field(HL.Number) << -1

DomainDepotGoodsPackCtrl.m_lastSelectedItemId = HL.Field(HL.String) << ""

DomainDepotGoodsPackCtrl.m_incomeRatio = HL.Field(HL.Number) << 1

DomainDepotGoodsPackCtrl.m_pack = HL.Field(HL.Forward("DomainDepotPack"))

DomainDepotGoodsPackCtrl.m_fillTween = HL.Field(HL.Userdata)

DomainDepotGoodsPackCtrl.m_resumeState = HL.Field(HL.Table)


DomainDepotGoodsPackCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_depotId = arg.depotId
    self.m_domainId = arg.domainId
    self.m_itemType = arg.itemType
    self.m_packType = arg.packType
    self.m_minLimitValue = arg.minLimitValue
    self.m_maxLimitValue = arg.maxLimitValue
    local itemFactor = Tables.domainDepotDeliverItemTypeTable[self.m_itemType].priceFactor
    local domainRatio = Tables.domainDataTable[self.m_domainId].domainDepotOfferPriceRatio
    self.m_incomeRatio = domainRatio * itemFactor
    self.m_pack = arg.pack
    self.m_resumeState = arg.resumeState

    self.view.backBtn.onClick:AddListener(function()
        Notify(MessageConst.ON_DOMAIN_DEPOT_BACK_TO_PACK_TYPE_SELECT_PANEL)
        self.m_pack:PlayRandomItemDropAnim(self.m_itemType)
    end)
    self.view.nextBtn.onClick:AddListener(function()
        self:_OnNextBtnClick()
    end)

    self:_InitBasicNodes()
    self:_InitPackItemList()
    self:_InitValueNode()
    self.m_pack:ClearPackItemCount()

    self:_InitPackController()
    self:_ApplyResumeState(self.m_resumeState)


end


DomainDepotGoodsPackCtrl.GetCurStateArg = HL.Method().Return(HL.Table) << function(self)
    local currentSelectedItemId = self.m_lastSelectedItemId
    return {
        depotId = self.m_depotId,
        domainId = self.m_domainId,
        itemType = self.m_itemType,
        packType = self.m_packType,
        minLimitValue = self.m_minLimitValue,
        maxLimitValue = self.m_maxLimitValue,
        resumeState = {
            selectedItemList = lume.deepCopy(self.m_selectedItemList),
            currentSelectedItemId = currentSelectedItemId,
        }
    }
end

DomainDepotGoodsPackCtrl.OnShow = HL.Override() << function(self)
    GameInstance.player.remoteFactory.core:Message_HSFB(Utils.getCurrentChapterId(), false, {1})
end

DomainDepotGoodsPackCtrl.OnHide = HL.Override() << function(self)
    self:_ClearFillTween()
    GameInstance.player.remoteFactory.core:Message_HSFB(Utils.getCurrentChapterId(), true, {1})
end

DomainDepotGoodsPackCtrl.OnClose = HL.Override() << function(self)
    self:_ClearFillTween()
    GameInstance.player.remoteFactory.core:Message_HSFB(Utils.getCurrentChapterId(), true, {1})
end

DomainDepotGoodsPackCtrl._InitBasicNodes = HL.Method() << function(self)
    local packTypeCfg = Tables.domainDepotDeliverItemTypeTable[self.m_itemType]
    self.view.titleTxt.text = string.format(Language.LUA_DOMAIN_DEPOT_PACK_ITEM_SELECT_TITLE, packTypeCfg.typeDesc)

    DomainDepotUtils.InitTopMoneyTitle(self.view.domainTopMoneyTitle, self.m_domainId, function()
        Notify(MessageConst.ON_CLOSE_DOMAIN_DEPOT_PACK_ITEM_SELECT_PANEL)
    end)

    DomainDepotUtils.RefreshMoneyIconWithDomain(self.view.moneyIconImg, self.m_domainId)

    DomainDepotUtils.SetDomainColorToDepotNodes(self.m_domainId, { self.view.bgColorMask })
end

DomainDepotGoodsPackCtrl._OnNextBtnClick = HL.Method() << function(self)
    local currValue = self:_GetItemTotalValue()
    GameInstance.player.domainDepotSystem:PackDomainDepotItems(
        self.m_depotId,
        self.m_itemType,
        self.m_packType,
        self.m_selectedItemList,
        currValue
    )
    self.view.nextBtn.enabled = false
end

DomainDepotGoodsPackCtrl._RefreshNextBtnState = HL.Method() << function(self)
    local currValue = self:_GetItemTotalValue()
    local enough = currValue >= self.m_minLimitValue
    self.view.nextBtn.interactable = enough
    self.view.nextBtnStateController:SetState(enough and "NormalState" or "DisableState")
    if DeviceInfo.usingController then
        local viewText = enough and self.view.nextBtnText.text or self.view.nextBtnNotEnoughText.text
        self.view.nextBtn.customBindingViewLabelText = viewText
    end
end

DomainDepotGoodsPackCtrl._OnPackFailed = HL.Method() << function(self)
    self:_UpdatePackItemDataList()
    self:_UpdateSelectItemListAfterDataChange()
    if DeviceInfo.usingController then
        self.view.leftNaviGroup:ManuallyStopFocus()
    end
    self:_RefreshPackItemList()
    self:_RefreshAllValueState()
    self:_RefreshNextBtnState()
    self.view.nextBtn.enabled = true
end


DomainDepotGoodsPackCtrl._ApplyResumeState = HL.Method(HL.Opt(HL.Any)) << function(self, resumeState)
    if resumeState == nil or resumeState.selectedItemList == nil then
        return
    end

    self.m_selectedItemList = lume.deepCopy(resumeState.selectedItemList or {})
    self.m_lastSelectedItemId = resumeState.currentSelectedItemId or ""
    self:_UpdateSelectItemListAfterDataChange()
    self.m_currSelectedItemIndex = -1
    self.view.numberSelector.view.gameObject:SetActive(false)

    local currentSelectedItemId = self.m_lastSelectedItemId
    if not string.isEmpty(currentSelectedItemId) and self.m_selectedItemList[currentSelectedItemId] ~= nil then
        local selectedIndex = self:_FindItemIndexById(currentSelectedItemId)
        if selectedIndex > 0 then
            self.m_currSelectedItemIndex = selectedIndex
            local maxCount = self:_GetItemNumberSelectorMaxCount(selectedIndex)
            if maxCount < 1 then
                maxCount = self.m_selectedItemList[currentSelectedItemId]
            end
            self.view.numberSelector.view.gameObject:SetActive(true)
            self.view.numberSelector:InitNumberSelector(
                self.m_selectedItemList[currentSelectedItemId],
                1,
                maxCount,
                function(newCount)
                    self.m_selectedItemList[currentSelectedItemId] = math.tointeger(newCount)
                    local cell = self.m_itemCellGetFunc(selectedIndex)
                    if cell ~= nil then
                        self:_RefreshItemCellSelectNode(currentSelectedItemId, cell)
                    end
                    self:_RefreshValueDisplayNode()
                end
            )
        end
    end

    self:_RefreshAllValueState()
    if self.m_currSelectedItemIndex > 0 then
        local currentCell = self.m_itemCellGetFunc(self.m_currSelectedItemIndex)
        if currentCell ~= nil then
            currentCell.item:SetSelected(true)
        end
    end

    if self.m_currSelectedItemIndex > 0 and currentSelectedItemId ~= nil then
        self:_RestoreControllerNavi(currentSelectedItemId)
    end
end


DomainDepotGoodsPackCtrl._RestoreControllerNavi = HL.Method(HL.String) << function(self, itemId)
    if string.isEmpty(itemId) then
        return
    end

    local targetIndex = self:_FindItemIndexById(itemId)
    if targetIndex <= 0 then
        return
    end

    self.view.itemList:ScrollToIndex(CSIndex(targetIndex), true)
    if DeviceInfo.usingController then
        self.view.leftNaviGroup:ManuallyFocus()
        local cell = self.m_itemCellGetFunc(targetIndex)
        if cell ~= nil then
            self:SetNaviTarget(cell.item.view.button)
        end
    end
end


DomainDepotGoodsPackCtrl._FindItemIndexById = HL.Method(HL.String).Return(HL.Number) << function(self, itemId)
    for index, itemInfo in ipairs(self.m_itemInfoList) do
        if itemInfo.id == itemId then
            return index
        end
    end
    return -1
end




DomainDepotGoodsPackCtrl._InitPackItemList = HL.Method() << function(self)
    self.m_itemCellGetFunc = UIUtils.genCachedCellFunction(self.view.itemList)

    self.view.itemList.onUpdateCell:AddListener(function(object, csIndex)
        self:_OnUpdateItemCell(self.m_itemCellGetFunc(object), LuaIndex(csIndex))
    end)

    self.m_selectedItemList = {}

    self:_UpdatePackItemDataList()
    self:_RefreshPackItemList()
end

DomainDepotGoodsPackCtrl._UpdatePackItemDataList = HL.Method() << function(self)
    local factoryDepot = GameInstance.player.inventory.factoryDepot
    local depotInChapter = factoryDepot:GetOrFallback(Utils.getCurrentScope())
    local actualDepot = depotInChapter[ScopeUtil.ChapterIdStr2Int(self.m_domainId)]
    local containsInItemTypeList = function(typeList)
        for index = 0, typeList.Count - 1 do
            if typeList[index] == self.m_itemType then
                return true
            end
        end
        return false
    end

    self.m_itemInfoList = {}
    self.m_itemValueGetter = {}
    for id, bundle in cs_pairs(actualDepot.normalItems) do
        local success, itemData = Tables.itemTable:TryGetValue(id)
        local facSuccess, facItemData = Tables.factoryItemTable:TryGetValue(id)
        if success and facSuccess then
            if containsInItemTypeList(facItemData.deliverItemTypeList) then
                table.insert(self.m_itemInfoList, {
                    id = id,
                    count = bundle.count,
                    value = facItemData.value,
                    sortId1 = itemData.sortId1,
                    sortId2 = itemData.sortId2,
                })
                self.m_itemValueGetter[id] = facItemData.value
            end
        end
    end
    table.sort(self.m_itemInfoList, Utils.genSortFunction({ "count", "value", "sortId1", "sortId2", "id" }, false))
end

DomainDepotGoodsPackCtrl._RefreshPackItemList = HL.Method() << function(self)
    if #self.m_itemInfoList > 0 then
        self.view.itemList:UpdateCount(#self.m_itemInfoList, true)
        self.view.main:SetState("Normal")
    else
        self.view.main:SetState("Empty")
    end
end

DomainDepotGoodsPackCtrl._OnUpdateItemCell = HL.Method(HL.Any, HL.Number) << function(self, cell, index)
    local itemInfo = self.m_itemInfoList[index]
    local id, count = itemInfo.id, itemInfo.count
    cell.item:InitItem({ id = itemInfo.id }, function()
        self:_OnSelectItem(index)
    end)
    cell.item:UpdateCountSimple(itemInfo.value)
    cell.depotCountTxt.text = tostring(count)
    cell.selectNode.gameObject:SetActive(false)
    cell.item:OpenLongPressTips()
    self:_RefreshItemCellSelectNode(id, cell)
    cell.item:SetSelected(index == self.m_currSelectedItemIndex)
    if DeviceInfo.usingController then
        local isAdded = self.m_selectedItemList[id]
        local bindingText = isAdded and
            Language.LUA_DOMAIN_DEPOT_CONTROLLER_PACK_REMOVE_ITEM or
            Language.LUA_DOMAIN_DEPOT_CONTROLLER_PACK_ADD_ITEM
        InputManagerInst:SetBindingText(cell.item.view.button.hoverConfirmBindingId, bindingText)

        cell.item:AddHoverBinding("show_item_tips", function()
            self.view.numberSelector.view.addKeyHint.gameObject:SetActive(false)
            self.view.numberSelector.view.reduceKeyHint.gameObject:SetActive(false)
            cell.item:ShowTips(nil, function()
                self.view.numberSelector.view.addKeyHint.gameObject:SetActive(true)
                self.view.numberSelector.view.reduceKeyHint.gameObject:SetActive(true)
            end)
        end)
    end
end

DomainDepotGoodsPackCtrl._UpdateSelectItemListAfterDataChange = HL.Method() << function(self)
    local waitUpdateItemList = {}
    for itemId, _ in pairs(self.m_selectedItemList) do
        waitUpdateItemList[itemId] = true
    end

    for _, itemInfo in ipairs(self.m_itemInfoList) do
        local id, count = itemInfo.id, itemInfo.count
        if waitUpdateItemList[id] then


            local selectCount = math.min(count, self.m_selectedItemList[id])
            self.m_selectedItemList[id] = selectCount
            waitUpdateItemList[id] = nil
        end
    end

    if next(waitUpdateItemList) then
        for itemId, _ in pairs(waitUpdateItemList) do
            self.m_selectedItemList[itemId] = nil
        end
    end


    if not string.isEmpty(self.m_lastSelectedItemId) and self.m_selectedItemList[self.m_lastSelectedItemId] == nil then
        self.m_lastSelectedItemId = ""
    end
end

DomainDepotGoodsPackCtrl._OnSelectItem = HL.Method(HL.Number) << function(self, index)
    local itemInfo = self.m_itemInfoList[index]
    local cell = self.m_itemCellGetFunc(index)
    local id, count = itemInfo.id, itemInfo.count

    if self.m_currSelectedItemIndex > 0 then
        local lastCell = self.m_itemCellGetFunc(self.m_currSelectedItemIndex)
        if lastCell ~= nil then
            lastCell.item:SetSelected(false)
        end
    end

    if self.m_selectedItemList[id] then

        self.m_selectedItemList[id] = nil
        self.m_currSelectedItemIndex = -1
        if self.m_lastSelectedItemId == id then
            self.m_lastSelectedItemId = ""
            for selectedItemId, _ in pairs(self.m_selectedItemList) do
                self.m_lastSelectedItemId = selectedItemId
                break
            end
        end

        self.view.numberSelector.view.gameObject:SetActive(false)

        if DeviceInfo.usingController then
            if cell ~= nil then
                InputManagerInst:SetBindingText(
                    cell.item.view.button.hoverConfirmBindingId,
                    Language.LUA_DOMAIN_DEPOT_CONTROLLER_PACK_ADD_ITEM
                )
            end
        end
    else

        local singleValue = self.m_itemInfoList[index].value
        local currTotalValue = self:_GetItemTotalValue()
        if singleValue + currTotalValue > self.m_maxLimitValue then
            return
        end

        local maxCount = self:_GetItemNumberSelectorMaxCount(index)
        self.view.numberSelector.view.gameObject:SetActive(true)
        self.view.numberSelector:InitNumberSelector(1, 1, maxCount, function(newCount)
            self.m_selectedItemList[id] = math.tointeger(newCount)
            self.m_lastSelectedItemId = id
            if cell.item.id == id then
                self:_RefreshItemCellSelectNode(id, cell)
            end
            self:_RefreshValueDisplayNode()
        end)

        self.m_selectedItemList[id] = 1
        self.m_currSelectedItemIndex = index
        self.m_lastSelectedItemId = id

        cell.item:SetSelected(true)

        if DeviceInfo.usingController then
            InputManagerInst:SetBindingText(cell.item.view.button.hoverConfirmBindingId, Language.LUA_DOMAIN_DEPOT_CONTROLLER_PACK_REMOVE_ITEM)
        end
    end

    self:_RefreshItemCellSelectNode(id, cell)
    self:_RefreshValueDisplayNode()
end

DomainDepotGoodsPackCtrl._GetItemNumberSelectorMaxCount = HL.Method(HL.Number).Return(HL.Number) << function(self, index)
    local id = self.m_itemInfoList[index].id
    local count = self.m_itemInfoList[index].count
    local singleValue = self.m_itemInfoList[index].value
    local currTotalValue = self:_GetItemTotalValue()
    local currItemSelectValue = 0
    if self.m_selectedItemList[id] ~= nil then
        currItemSelectValue = self.m_selectedItemList[id] * singleValue
    end
    currTotalValue = currTotalValue - currItemSelectValue
    return math.min(count, math.floor((self.m_maxLimitValue - currTotalValue) / singleValue))
end

DomainDepotGoodsPackCtrl._RefreshItemCellSelectNode = HL.Method(HL.String, HL.Any) << function(self, id, itemCell)
    if self.m_selectedItemList[id] then
        itemCell.selectNode.selectCountTxt.text = string.format("%d", self.m_selectedItemList[id])
        itemCell.selectNode.gameObject:SetActive(true)
    else
        itemCell.selectNode.gameObject:SetActive(false)
    end
end

DomainDepotGoodsPackCtrl._GetItemTotalValue = HL.Method().Return(HL.Number) << function(self)
    local totalValue = 0
    for id, count in pairs(self.m_selectedItemList) do
        totalValue = totalValue + self.m_itemValueGetter[id] * count
    end
    return totalValue
end






DomainDepotGoodsPackCtrl._InitValueNode = HL.Method() << function(self)
    self.view.fillMinimumBtn.onClick:AddListener(function()
        self:_OnMinFillBtnClick()
    end)
    self.view.fillMaxBtn.onClick:AddListener(function()
        self:_OnMaxFillBtnClick()
    end)
    self.view.resetBtn.onClick:AddListener(function()
        self:_OnResetBtnClick()
    end)

    self:_RefreshValueDisplayNode()
    self:_InitValueFillMinimumNode()
end

DomainDepotGoodsPackCtrl._InitValueFillMinimumNode = HL.Method() << function(self)
    local sliderHeight = self.view.fillSliderRectTransform.rect.height
    local minPercentage = self.m_minLimitValue / self.m_maxLimitValue
    local bottomHeight = minPercentage * sliderHeight
    self.view.fillMinimumNode.anchoredPosition = Vector2(0, -sliderHeight / 2 + bottomHeight)
end

DomainDepotGoodsPackCtrl._RefreshValueDisplayNode = HL.Method() << function(self)
    local currTotalValue = self:_GetItemTotalValue()
    self.view.currValueTxt.text = string.format("%d", math.floor(currTotalValue))
    self.view.maxValueTxt.text = string.format("/%d", math.floor(self.m_maxLimitValue))

    local percent = currTotalValue / self.m_maxLimitValue

    if percent ~= self.view.fillSlider.value then
        self:_ClearFillTween()
        self.m_fillTween = DOTween.To(function()
            return self.view.fillSlider.value
        end, function(amount)
            self.view.fillSlider.value = amount
        end, percent, self.view.config.FILL_TWEEN_DURATION):OnComplete(function()
            self:_ClearFillTween()
        end):SetEase(self.view.config.FILL_TWEEN_CURVE)
    end


    self.m_pack:ChangePackItemCount(math.ceil(percent * 9))
    self.view.incomeNumTxt.text = string.format("%d", math.floor(currTotalValue * self.m_incomeRatio))

    self:_RefreshNextBtnState()
end

DomainDepotGoodsPackCtrl._ClearFillTween = HL.Method() << function(self)
    if self.m_fillTween == nil then
        return
    end
    self.m_fillTween:Kill(false)
    self.m_fillTween = nil
end

DomainDepotGoodsPackCtrl._RefreshAllValueState = HL.Method() << function(self)
    self.view.itemList:UpdateShowingCells(function(csIndex, obj)
        local cell = self.m_itemCellGetFunc(obj)
        local id = self.m_itemInfoList[LuaIndex(csIndex)].id
        self:_RefreshItemCellSelectNode(id, cell)
    end)

    if self.m_currSelectedItemIndex > 0 then

        local id = self.m_itemInfoList[self.m_currSelectedItemIndex].id
        local maxCount = self:_GetItemNumberSelectorMaxCount(self.m_currSelectedItemIndex)
        if maxCount < 1 then

            maxCount = self.m_selectedItemList[id]
        end
        self.view.numberSelector:RefreshNumber(self.m_selectedItemList[id], 1, maxCount)
    end

    self:_RefreshValueDisplayNode()
end

DomainDepotGoodsPackCtrl._AddSelectedItemToTargetValue = HL.Method(HL.Number, HL.Boolean) << function(self, fillValue, useFloor)
    local fillItemInfoList = {}
    for _, itemInfo in ipairs(self.m_itemInfoList) do
        table.insert(fillItemInfoList, {
            id = itemInfo.id,
            count = itemInfo.count,
            value = itemInfo.value,
            sortId1 = itemInfo.sortId1,
            sortId2 = itemInfo.sortId2,
        })
    end
    table.sort(fillItemInfoList, Utils.genSortFunction({ "count", "value", "sortId1", "sortId2", "id" }, false))

    for _, itemInfo in ipairs(fillItemInfoList) do
        local id, count = itemInfo.id, itemInfo.count
        local singleValue = itemInfo.value
        if self.m_selectedItemList[id] then
            local selectedCount = self.m_selectedItemList[id]
            if selectedCount < count then
                local unselectedCount = count - selectedCount
                local itemValue = unselectedCount * singleValue
                local fillCount
                if itemValue <= fillValue then
                    fillCount = unselectedCount
                else
                    if useFloor then
                        fillCount = math.floor(fillValue / singleValue)
                    else
                        fillCount = math.ceil(fillValue / singleValue)
                    end
                    fillCount = math.min(unselectedCount, fillCount)
                end
                fillValue = fillValue - fillCount * singleValue
                self.m_selectedItemList[id] = fillCount + selectedCount
            end
        else
            local itemValue = count * singleValue
            local fillCount
            if itemValue <= fillValue then
                fillCount = count
            else
                if useFloor then
                    fillCount = math.floor(fillValue / singleValue)
                else
                    fillCount = math.ceil(fillValue / singleValue)
                end
                fillCount = math.min(count, fillCount)
            end
            fillValue = fillValue - fillCount * singleValue
            if fillCount > 0 then
                self.m_selectedItemList[id] = fillCount
            end
        end

        if fillValue <= 0 then
            break
        end
    end

    self:_RefreshAllValueState()
end

DomainDepotGoodsPackCtrl._RemoveSelectedItemToTargetValue = HL.Method(HL.Number) << function(self, removeValue)
    local removeItemInfoList = {}
    for _, itemInfo in ipairs(self.m_itemInfoList) do
        if self.m_selectedItemList[itemInfo.id] then
            table.insert(removeItemInfoList, {
                id = itemInfo.id,
                count = itemInfo.count,
                value = itemInfo.value,
                sortId1 = itemInfo.sortId1,
                sortId2 = itemInfo.sortId2,
            })
        end
    end
    table.sort(removeItemInfoList, Utils.genSortFunction({ "count", "value", "sortId1", "sortId2", "id" }, true))

    for _, itemInfo in ipairs(removeItemInfoList) do
        local id, selectCount = itemInfo.id, self.m_selectedItemList[itemInfo.id]
        local singleValue = itemInfo.value
        local selectValue = selectCount * singleValue
        local removeCount
        if selectValue <= removeValue then
            removeCount = selectCount
        else
            removeCount = math.min(selectCount, math.floor(removeValue / singleValue))
        end
        removeValue = removeValue - removeCount * singleValue
        self.m_selectedItemList[id] = selectCount - removeCount
        if self.m_selectedItemList[id] <= 0 then
            self.m_selectedItemList[id] = nil
        end
        if removeValue <= 0 then
            break
        end
    end

    self:_RefreshAllValueState()
end

DomainDepotGoodsPackCtrl._OnMinFillBtnClick = HL.Method() << function(self)
    local currValue = self:_GetItemTotalValue()
    if currValue == self.m_minLimitValue then
        return
    end
    if currValue > self.m_minLimitValue then
        self:_RemoveSelectedItemToTargetValue(currValue - self.m_minLimitValue)
    else
        self:_AddSelectedItemToTargetValue(self.m_minLimitValue - currValue, false)
    end
end

DomainDepotGoodsPackCtrl._OnMaxFillBtnClick = HL.Method() << function(self)
    local currValue = self:_GetItemTotalValue()
    if currValue == self.m_maxLimitValue then
        return
    end
    self:_AddSelectedItemToTargetValue(self.m_maxLimitValue - currValue, true)
end

DomainDepotGoodsPackCtrl._OnResetBtnClick = HL.Method() << function(self)
    if self.m_currSelectedItemIndex > 0 then
        self:_OnSelectItem(self.m_currSelectedItemIndex)
    end
    self.m_selectedItemList = {}
    self.m_lastSelectedItemId = ""
    self:_RefreshAllValueState()
end






DomainDepotGoodsPackCtrl._InitPackController = HL.Method() << function(self)
    self.view.leftNaviGroup.onIsFocusedChange:AddListener(function(isFocused)
        if not isFocused then
            if self.m_currSelectedItemIndex > 0 then
                local currCell = self.m_itemCellGetFunc(self.m_currSelectedItemIndex)
                if currCell ~= nil then
                    currCell.item:SetSelected(false)
                end
                self.view.numberSelector.view.gameObject:SetActive(false)
                self.m_currSelectedItemIndex = -1
            end
        end

        self.view.nextBtnKeyHint.gameObject:SetActive(not isFocused)
    end)
    self.view.leftNaviGroup.getDefaultSelectableFunc = function()
        local firstCellIndex = self.view.itemList:GetShowingCellsIndexRange()
        return self.m_itemCellGetFunc(LuaIndex(firstCellIndex)).item.view.button
    end

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
end




HL.Commit(DomainDepotGoodsPackCtrl)
