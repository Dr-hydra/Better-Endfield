local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ShopStar
local PHASE_ID = PhaseId.ShopStar
local DEFAULT_SHOP_GROUP_ID = "shop_trstar"
local SOLD_OUT_GOODS_TAG_ID = "soldOut"
local SCROLL_SYNC_DELAY = 0.3
local shopSystem = GameInstance.player.shopSystem

ShopStarCtrl = HL.Class('ShopStarCtrl', uiCtrl.UICtrl)

ShopStarCtrl.m_shopGroupId = HL.Field(HL.String) << ""

ShopStarCtrl.m_shopId = HL.Field(HL.String) << ""

ShopStarCtrl.m_shopTabs = HL.Field(HL.Table)

ShopStarCtrl.m_goodsGroupList = HL.Field(HL.Table)

ShopStarCtrl.m_tabCellCache = HL.Field(HL.Forward("UIListCache"))

ShopStarCtrl.m_goodsTagCellCache = HL.Field(HL.Forward("UIListCache"))

ShopStarCtrl.m_curSelectTagIndex = HL.Field(HL.Number) << 1

ShopStarCtrl.m_targetGoodsId = HL.Field(HL.String) << ""

ShopStarCtrl.m_targetItemId = HL.Field(HL.String) << ""

ShopStarCtrl.m_getGoodsGroupCellFunc = HL.Field(HL.Function)

ShopStarCtrl.m_getGoodsGroupTitleFunc = HL.Field(HL.Function)

ShopStarCtrl.m_hasBoundGoodsGroupScrollValueChanged = HL.Field(HL.Boolean) << false

ShopStarCtrl.m_waitAutoScrollTagListTime = HL.Field(HL.Number) << -1

ShopStarCtrl.m_isScrollingByCode = HL.Field(HL.Boolean) << false

ShopStarCtrl.m_goodsGroupSortFunc = HL.Field(HL.Function)

ShopStarCtrl.m_goodsSortFunc = HL.Field(HL.Function)

ShopStarCtrl.m_soldOutGoodsSortFunc = HL.Field(HL.Function)

ShopStarCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_SHOP_REFRESH] = '_OnShopDataChanged',
    [MessageConst.ON_SHOP_FREQUENCY_LIMIT_CHANGE] = '_OnShopFrequencyLimitChange',
    [MessageConst.ON_SHOP_GOODS_LOCK_CHANGE] = '_OnShopDataChanged',
    [MessageConst.ON_SHOP_GOODS_CONDITION_REFRESH] = '_OnShopDataChanged',
    [MessageConst.ON_BUY_ITEM_SUCC] = '_OnBuyItemSuccess',
}













ShopStarCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:_InitUI()
    self:_InitData(arg)
    if string.isEmpty(self.m_shopId) then
        return
    end
    self:_StartUpdate(function(deltaTime)
        if self.m_waitAutoScrollTagListTime < 0 then
            return
        end
        if self.m_waitAutoScrollTagListTime >= SCROLL_SYNC_DELAY then
            self:_TrySyncTagSelectionByScroll()
            self.m_waitAutoScrollTagListTime = -1
        else
            self.m_waitAutoScrollTagListTime = self.m_waitAutoScrollTagListTime + deltaTime
        end
    end)
    self:_RefreshAll()
    local targetGoodsId = self.m_targetGoodsId
    local searchInfo
    if not string.isEmpty(targetGoodsId) then
        searchInfo = self:_FindGoodsInfoByGoodsId(targetGoodsId)
    elseif not string.isEmpty(self.m_targetItemId) then
        searchInfo = self:_FindGoodsInfoByItemId(self.m_targetItemId)
        if searchInfo then
            targetGoodsId = searchInfo.goodsInfo.goodsId
        end
    end
    if searchInfo then
        self:_OnClickGoodsTagCell(searchInfo.groupIndex, true)
        local goodsObj = self.view.goodsGroupList:Get(CSIndex(searchInfo.groupIndex), CSIndex(searchInfo.localIndex))
        if goodsObj then
            local goodsCell = self.m_getGoodsGroupCellFunc(goodsObj)
            if goodsCell and goodsCell.view and goodsCell.view.selectBtn then
                self:SetNaviTarget(goodsCell.view.selectBtn)
            end
        end
        local goodsData = shopSystem:GetShopGoodsData(self.m_shopId, targetGoodsId)
        if goodsData then
            CashShopUtils.OpenShopDetailPanel(goodsData, self)
        end
    end
    self.m_targetGoodsId = ""
    self.m_targetItemId = ""

    local resumeOpenPanel = type(arg) == "table" and arg.resumeOpenPanel or nil
    if resumeOpenPanel then
        for _, panelInfo in ipairs(resumeOpenPanel) do
            self:_RestorePopupByResumeState(panelInfo)
        end
    end
end

ShopStarCtrl._InitUI = HL.Method() << function(self)
    local closeFunc = function()
        if self.view.goodsGroupListSelectableNaviGroup.IsTopLayer then
            self.view.goodsGroupListSelectableNaviGroup:ManuallyStopFocus()
            self:_SetCurSelectTag()
            return
        end
        Notify(MessageConst.HIDE_ITEM_TIPS)
        PhaseManager:PopPhase(PHASE_ID)
    end
    self.view.btnClose.onClick:AddListener(closeFunc)
    self.view.btnBack.onClick:AddListener(closeFunc)
    self.view.closeButton.onClick:AddListener(closeFunc)
    self.view.helpBtn.gameObject:SetActive(false)
    self.m_tabCellCache = UIUtils.genCellCache(self.view.marketTabLayout)
    self.m_goodsTagCellCache = UIUtils.genCellCache(self.view.goodsTagCell)
    self.m_getGoodsGroupCellFunc = self.m_getGoodsGroupCellFunc or UIUtils.genCachedCellFunction(self.view.goodsGroupList, function(object)
        return UIWidgetManager:Wrap(object)
    end)
    self.m_getGoodsGroupTitleFunc = self.m_getGoodsGroupTitleFunc or UIUtils.genCachedCellFunction(self.view.goodsGroupList, nil, true)
    self.view.goodsGroupList.onUpdateCell:RemoveAllListeners()
    self.view.goodsGroupList.onUpdateCell:AddListener(function(object, csIndex)
        self:_OnUpdateGoodsCell(self.m_getGoodsGroupCellFunc(object), LuaIndex(csIndex))
    end)
    self.view.goodsGroupList.onUpdateGroupTitle:RemoveAllListeners()
    self.view.goodsGroupList.onUpdateGroupTitle:AddListener(function(object, csIndex)
        self:_RefreshGoodsGroupTitle(self.m_getGoodsGroupTitleFunc(object), LuaIndex(csIndex))
    end)
    self.view.goodsGroupList.getCellCountInGroup = function(groupCSIndex)
        return self:_GetGoodsGroupCellCount(LuaIndex(groupCSIndex))
    end
    if not self.m_hasBoundGoodsGroupScrollValueChanged then
        self.m_hasBoundGoodsGroupScrollValueChanged = true
        self.view.goodsGroupList.onEndDrag:RemoveAllListeners()
        self.view.goodsGroupList.onEndDrag:AddListener(function()
            self.m_waitAutoScrollTagListTime = 0
            self.m_isScrollingByCode = false
        end)
        self.view.goodsGroupScrollRect.onValueChanged:AddListener(function(_)
            if not self.m_isScrollingByCode and self.m_waitAutoScrollTagListTime  < 0 then
                self.m_waitAutoScrollTagListTime = 0
            end
        end)
        self.view.goodsGroupScrollRect.OnScrollStart:AddListener(function()
            self.m_isScrollingByCode = false
            self.m_waitAutoScrollTagListTime = 0
        end)
    end
    self.m_goodsGroupSortFunc = Utils.genSortFunction({ "sortId" }, true)
    self.m_goodsSortFunc = Utils.genSortFunction({ "remainLimitSort", "sortId", "raritySort", "priceRatioSort", "goodsId" }, true)
    self.m_soldOutGoodsSortFunc = Utils.genSortFunction({ "refreshTypeSort", "tagSort", "sortId", "raritySort", "priceRatioSort", "goodsId" }, true)
    self.view.goodsGroupListSelectableNaviGroup.getDefaultSelectableFunc = function()
        return self:_GetGoodsGroupListDefaultSelectable()
    end
    self.view.goodsGroupListSelectableNaviGroup.onSetLayerSelectedTarget:AddListener(function(target)
        if not DeviceInfo.usingController or not target then
            return
        end
        self:_SyncTagBySelectedGoods(target)
    end)

    self.view.focusHelperGoodsGroupList.onIsNaviTargetChanged = function(isTarget)
        if isTarget then
            self:_SetCurSelectTag()
        end
    end
    self.view.focusHelperGoodsTagList.onIsNaviTargetChanged = function(isTarget)
        if isTarget then
            self.view.goodsGroupListSelectableNaviGroup:ManuallyFocus()
        end
    end

    self.view.goodsGroupListSelectableNaviGroup.onIsFocusedChange:AddListener(function(isFocused)
        if not isFocused then
            self:_SetCurSelectTag()
        end
    end)

    self.view.goodsTagList.gameObject:SetActive(false)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
end

ShopStarCtrl._InitData = HL.Method(HL.Any) << function(self, arg)
    local shopId
    local curSelectTagIndex
    if type(arg) == "table" then
        shopId = arg.shopId
        curSelectTagIndex = arg.curSelectTagIndex
        self.m_targetGoodsId = arg.targetGoodsId or ""
        self.m_targetItemId = arg.itemId or ""
    end
    self.m_curSelectTagIndex = type(curSelectTagIndex) == "number" and curSelectTagIndex > 0 and curSelectTagIndex or 1
    self.m_shopGroupId = DEFAULT_SHOP_GROUP_ID
    if not shopSystem:CheckShopGroupUnlocked(self.m_shopGroupId) then
        Notify(MessageConst.SHOW_POP_UP, {
            content = Language.LUA_SHOP_NOT_UNLOCK,
            hideCancel = true,
            onConfirm = function()
                PhaseManager:PopPhase(PHASE_ID)
            end
        })
        self.m_shopGroupId = ""
        self.m_shopId = ""
        return
    end
    self.m_shopTabs = self:_BuildShopTabs()
    if #self.m_shopTabs <= 0 then
        logger.error("[ShopStarCtrl] No available shop tabs for group: " .. self.m_shopGroupId)
        self.m_shopId = ""
        return
    end
    self.m_shopId = self:_ResolveShopId(shopId)
end

ShopStarCtrl._BuildShopTabs = HL.Method().Return(HL.Table) << function(self)
    local tabs = {}
    local shopGroupData = shopSystem:GetShopGroupData(self.m_shopGroupId)
    if not shopGroupData then
        return tabs
    end
    for i = 0, shopGroupData.shopIdList.Count - 1 do
        local tabShopId = shopGroupData.shopIdList[i]
        local hasShopCfg, shopCfg = Tables.shopTable:TryGetValue(tabShopId)
        if hasShopCfg then
            local isUnlocked = shopSystem:CheckShopUnlocked(tabShopId)
            if isUnlocked or shopCfg.isShowWhenLock then
                table.insert(tabs, {
                    shopId = tabShopId,
                    isUnlocked = isUnlocked,
                    shopCfg = shopCfg,
                })
            end
        end
    end
    return tabs
end

ShopStarCtrl._ResolveShopId = HL.Method(HL.Opt(HL.String)).Return(HL.String) << function(self, shopId)
    if not string.isEmpty(shopId) then
        for _, tabInfo in ipairs(self.m_shopTabs) do
            if tabInfo.shopId == shopId and tabInfo.isUnlocked then
                return shopId
            end
        end
    end
    if not string.isEmpty(self.m_targetGoodsId) then
        for _, tabInfo in ipairs(self.m_shopTabs) do
            if tabInfo.isUnlocked then
                local shopData = shopSystem:GetShopData(tabInfo.shopId)
                if shopData and shopData.goodsDic:ContainsKey(self.m_targetGoodsId) then
                    return tabInfo.shopId
                end
            end
        end
    end
    if string.isEmpty(self.m_targetGoodsId) and not string.isEmpty(self.m_targetItemId) then
        for _, tabInfo in ipairs(self.m_shopTabs) do
            if tabInfo.isUnlocked and self:_ShopHasGoodsWithItemId(tabInfo.shopId, self.m_targetItemId) then
                return tabInfo.shopId
            end
        end
    end
    for _, tabInfo in ipairs(self.m_shopTabs) do
        if tabInfo.isUnlocked then
            return tabInfo.shopId
        end
    end
    return self.m_shopTabs[1].shopId
end

ShopStarCtrl._RefreshAll = HL.Method() << function(self)
    self:_RefreshTitle()
    self:_RefreshTabs()
    self:_RefreshGoodsGroupData()
    self:_RefreshGoodsGroupList()
    self:_RefreshGoodsTagList()
    self:_RefreshControllerTarget()
    self:_RefreshWalletBar()
end

ShopStarCtrl._RefreshTitle = HL.Method() << function(self)
    local _, shopGroupCfg = Tables.shopGroupTable:TryGetValue(self.m_shopGroupId)
    if not shopGroupCfg then
        return
    end
    self.view.titleTxt.text = shopGroupCfg.shopGroupName
    self.view.titleTxt02.text = shopGroupCfg.shopGroupName
end

ShopStarCtrl._RefreshWalletBar = HL.Method() << function(self)
    local moneyId = ""
    local hasShopCfg, shopCfg = Tables.shopTable:TryGetValue(self.m_shopId)
    if hasShopCfg then
        for i = 0, shopCfg.shopGoodsIds.Count - 1 do
            local goodsId = shopCfg.shopGoodsIds[i]
            local hasGoodsCfg, goodsCfg = Tables.shopGoodsTable:TryGetValue(goodsId)
            if hasGoodsCfg and not string.isEmpty(goodsCfg.moneyId) then
                moneyId = goodsCfg.moneyId
                break
            end
        end
    end
    self.view.walletBarPlaceholder:InitWalletBarPlaceholder(string.isEmpty(moneyId) and {} or { moneyId })
end

ShopStarCtrl._RefreshTabs = HL.Method() << function(self)
    local tabCount = #self.m_shopTabs
    local showTabHint = tabCount > 1
    self.view.tabKeyHintPrev.gameObject:SetActive(showTabHint)
    self.view.tabKeyHintPost.gameObject:SetActive(showTabHint)
    self.m_tabCellCache:Refresh(tabCount, function(cell, luaIndex)
        self:_RefreshTabCell(cell, luaIndex)
    end)
end

ShopStarCtrl._RefreshTabCell = HL.Method(HL.Any, HL.Number) << function(self, cell, luaIndex)
    local tabInfo = self.m_shopTabs[luaIndex]
    cell.toggle.onValueChanged:RemoveAllListeners()
    cell.toggle.onValueChanged:AddListener(function(isOn)
        if isOn and self.m_shopId ~= tabInfo.shopId and tabInfo.isUnlocked then
            shopSystem:SetGoodsIdSee()
            self.m_shopId = tabInfo.shopId
            self.m_curSelectTagIndex = 1
            self:_RefreshAll()
            self:_ScrollToGoodsGroupByTagIndex(self.m_curSelectTagIndex, true)
        end
    end)
    cell.button.onClick:RemoveAllListeners()
    cell.button.onClick:AddListener(function()
        if not tabInfo.isUnlocked then
            return
        end
        self:_SelectTab(luaIndex)
    end)
    cell.toggle.isOn = self.m_shopId == tabInfo.shopId
    cell.tabNameText.text = tabInfo.shopCfg.shopName
    cell.tabNameDecoText.text = tabInfo.shopCfg.shopEnName or tabInfo.shopCfg.shopName
    cell.tabIconImg:LoadSprite(UIConst.UI_SPRITE_INVENTORY, tabInfo.shopCfg.iconId)
    cell.tabIconDecoImg:LoadSprite(UIConst.UI_SPRITE_INVENTORY, tabInfo.shopCfg.iconId)
end

ShopStarCtrl._SelectTab = HL.Method(HL.Number) << function(self, index)
    local tabCount = self.m_tabCellCache:GetCount()
    for luaIndex = 1, tabCount do
        local cell = self.m_tabCellCache:Get(luaIndex)
        if cell then
            cell.toggle.isOn = false
        end
    end
    local curCell = self.m_tabCellCache:Get(index)
    if curCell then
        curCell.toggle.isOn = true
    end
end

ShopStarCtrl._RefreshGoodsGroupData = HL.Method() << function(self)
    self.m_goodsGroupList = {}
    local _, shopCfg = Tables.shopTable:TryGetValue(self.m_shopId)
    local shopData = shopSystem:GetShopData(self.m_shopId)
    if not shopCfg or not shopData then
        return
    end
    local groupDict = {}
    local soldOutGoodsGroup = self:_GetOrCreateGoodsGroup(groupDict, shopCfg, SOLD_OUT_GOODS_TAG_ID)
    for goodsId, goodsData in pairs(shopData.goodsDic) do
        local isUnlocked = shopSystem:CheckGoodsUnlocked(goodsId)
        local hasGoodsCfg, goodsCfg = Tables.shopGoodsTable:TryGetValue(goodsData.goodsTemplateId)
        if hasGoodsCfg and (isUnlocked or goodsCfg.isShowWhenLock) then
            local groupInfo = self:_GetOrCreateGoodsGroup(groupDict, shopCfg, goodsCfg.goodsTagId)
            local goodsInfo = self:_BuildGoodsInfo(goodsId, goodsData, goodsCfg, isUnlocked)
            if goodsInfo then
                if goodsInfo.remainLimitCount == 0 then
                    goodsInfo.tagSort = groupInfo.sortId
                    table.insert(soldOutGoodsGroup.goodsList, goodsInfo)
                else
                    table.insert(groupInfo.goodsList, goodsInfo)
                end
            end
        end
    end
    for _, groupInfo in pairs(groupDict) do
        if groupInfo ~= soldOutGoodsGroup and #groupInfo.goodsList > 0 then
            table.sort(groupInfo.goodsList, self.m_goodsSortFunc)
            table.insert(self.m_goodsGroupList, groupInfo)
        end
    end
    table.sort(self.m_goodsGroupList, self.m_goodsGroupSortFunc)
    if #soldOutGoodsGroup.goodsList > 0 then
        table.sort(soldOutGoodsGroup.goodsList, self.m_soldOutGoodsSortFunc)
        table.insert(self.m_goodsGroupList, soldOutGoodsGroup)
    end
end

ShopStarCtrl._GetOrCreateGoodsGroup = HL.Method(HL.Table, HL.Any, HL.Any).Return(HL.Table) << function(self, groupDict, shopCfg, tagId)
    local normalizedTagId = tagId
    if normalizedTagId ~= nil and type(normalizedTagId) ~= "string" then
        normalizedTagId = tostring(normalizedTagId)
    end
    local groupKey = string.isEmpty(normalizedTagId) and "__default__" or normalizedTagId
    local groupInfo = groupDict[groupKey]
    if groupInfo then
        return groupInfo
    end
    groupInfo = {
        groupKey = groupKey,
        tagId = groupKey,
        tagIcon = "",
        titleName = shopCfg.shopName,
        titleIcon = "",
        sortId = 0,
        hideDeco = true,
        goodsList = {},
    }
    if not string.isEmpty(normalizedTagId) then
        local hasTagCfg, tagCfg = Tables.shopGoodsTagCommonTable:TryGetValue(normalizedTagId)
        if not hasTagCfg then
            hasTagCfg, tagCfg = Tables.shopGoodsTagTable:TryGetValue(normalizedTagId)
        end
        if hasTagCfg then
            groupInfo.tagId = normalizedTagId
            groupInfo.tagIcon = tagCfg.tagIcon
            groupInfo.titleName = tagCfg.tagName
            groupInfo.titleIcon = tagCfg.tagIcon
            groupInfo.sortId = tagCfg.sortId
            groupInfo.hideDeco = false
        end
    end
    groupDict[groupKey] = groupInfo
    return groupInfo
end

ShopStarCtrl._BuildGoodsInfo = HL.Method(HL.String, HL.Any, HL.Any, HL.Boolean).Return(HL.Opt(HL.Table)) << function(self, goodsId, goodsData, goodsCfg, isUnlocked)
    if string.isEmpty(goodsCfg.rewardId) then
        return nil
    end
    local itemBundle = UIUtils.getRewardFirstItem(goodsCfg.rewardId)
    if itemBundle == nil then
        return nil
    end
    local itemId = itemBundle.id
    local hasItemCfg, itemCfg = Tables.itemTable:TryGetValue(itemId)
    if not hasItemCfg then
        logger.error("[ShopStarCtrl] Missing item cfg for reward item: " .. tostring(itemId))
        return nil
    end
    local moneyId = goodsCfg.moneyId
    local moneyItemCfg = Utils.tryGetTableCfg(Tables.itemTable, moneyId)
    local discount = 1 - goodsData.discount
    local remainLimitCount = shopSystem:GetRemainCountByGoodsId(self.m_shopId, goodsId)
    return {
        shopId = self.m_shopId,
        goodsId = goodsId,
        originPrice = goodsCfg.price,
        discount = discount,
        curPrice = CashShopUtils.GetDisplayPrice(goodsCfg.price, goodsData.discount),
        remainLimitCount = remainLimitCount,
        refreshType = goodsCfg.limitCountRefreshType,
        itemId = itemId,
        itemName = itemCfg.name,
        itemCount = Utils.getItemCount(itemId, true, true),
        itemBundleCount = itemBundle.count,
        itemRarity = itemCfg.rarity,
        moneyId = moneyId,
        moneyIcon = moneyItemCfg and moneyItemCfg.iconId or "",
        isLocked = not isUnlocked,
        refreshTag = "normal",
        refreshTypeSort = goodsCfg.limitCountRefreshType == GEnums.ShopFrequencyLimitType.Forever and 999 or goodsCfg.limitCountRefreshType:GetHashCode(),
        remainLimitSort = remainLimitCount == 0 and 1 or 0,
        raritySort = -itemCfg.rarity,
        priceRatioSort = -discount,
        sortId = goodsCfg.sortId,
    }
end

ShopStarCtrl._RefreshGoodsGroupList = HL.Method() << function(self)
    self.view.goodsGroupList:UpdateGroup(#self.m_goodsGroupList, false, false, false, true)
end

ShopStarCtrl._RefreshGoodsTagList = HL.Method() << function(self)
    local count = #self.m_goodsGroupList
    self.view.goodsTagList.gameObject:SetActive(count > 0)
    if count <= 0 then
        return
    end
    if self.m_curSelectTagIndex < 1 or self.m_curSelectTagIndex > count then
        self.m_curSelectTagIndex = 1
    end
    self.m_goodsTagCellCache:Refresh(count, function(cell, luaIndex)
        self:_RefreshGoodsTagCell(cell, luaIndex)
    end)
    local selectedTagCell = self.m_goodsTagCellCache:Get(self.m_curSelectTagIndex)
    if selectedTagCell then
        self.view.goodsTagScrollRect:AutoScrollToRectTransform(selectedTagCell.transform, true)
        self:_SyncFocusHelperToTagCell(selectedTagCell)
    end
end

ShopStarCtrl._RefreshGoodsTagCell = HL.Method(HL.Any, HL.Number) << function(self, cell, luaIndex)
    local groupInfo = self.m_goodsGroupList[luaIndex]
    cell.connectLineImg.enabled = luaIndex < #self.m_goodsGroupList
    cell.tagIconImg.gameObject:SetActiveIfNecessary(not string.isEmpty(groupInfo.tagIcon))
    if not string.isEmpty(groupInfo.tagIcon) then
        cell.tagIconImg:LoadSprite(UIConst.UI_SPRITE_SHOP_TAG_ICON, groupInfo.tagIcon)
    end
    cell.animationWrapper:ClearTween()
    local isSelected = self.m_curSelectTagIndex == luaIndex
    cell.animationWrapper:Play(isSelected and "shoptrade_goodstagcell_select" or "shoptrade_goodstagcell_noselect")
    if cell.stateController then
        cell.stateController:SetState(isSelected and "SelectState" or "NormalState")
    end
    cell.tagBtn.onClick:RemoveAllListeners()
    cell.tagBtn.onClick:AddListener(function()
        self:_OnClickGoodsTagCell(luaIndex)
    end)
    cell.tagBtn.onIsNaviTargetChanged = function(isTarget)
        if isTarget then
            self:_OnClickGoodsTagCell(luaIndex)
        end
    end
    cell.gameObject.name = "Tag_" .. tostring(groupInfo.tagId)
end

ShopStarCtrl._OnClickGoodsTagCell = HL.Method(HL.Number, HL.Opt(HL.Boolean)) << function(self, luaIndex, noTween)
    if self.m_curSelectTagIndex ~= luaIndex then
        self:_OnChangeSelectTagUI(luaIndex)
    end
    self:_ScrollToGoodsGroupByTagIndex(luaIndex, noTween or false)
end

ShopStarCtrl._OnChangeSelectTagUI = HL.Method(HL.Number) << function(self, newIndex)
    self.m_curSelectTagIndex = newIndex
    local count = self.m_goodsTagCellCache:GetCount()
    for i = 1, count do
        local cell = self.m_goodsTagCellCache:Get(i)
        if cell then
            local isSelected = i == newIndex
            cell.animationWrapper:ClearTween()
            cell.animationWrapper:Play(isSelected and "shoptrade_goodstagcell_select" or "shoptrade_goodstagcell_noselect")
            if cell.stateController then
                cell.stateController:SetState(isSelected and "SelectState" or "NormalState")
            end
        end
    end
    local newCell = self.m_goodsTagCellCache:Get(newIndex)
    if newCell then
        self.view.goodsTagScrollRect:AutoScrollToRectTransform(newCell.transform, true)
        self:_SyncFocusHelperToTagCell(newCell)
    end
end

ShopStarCtrl._OnUpdateGoodsCell = HL.Method(HL.Any, HL.Number) << function(self, cell, globalLuaIndex)
    local result = self:_GlobalToGroupLocal(globalLuaIndex)
    local groupIndex = result.groupIndex
    local localIndex = result.localIndex
    if not groupIndex then
        return
    end
    local groupInfo = self.m_goodsGroupList[groupIndex]
    local goodsInfo = groupInfo and groupInfo.goodsList[localIndex]
    if not goodsInfo then
        return
    end
    cell:InitShopStarGoodsCell(goodsInfo)
    cell.gameObject.name = "GoodsCell_" .. groupIndex .. "_" .. localIndex
end

ShopStarCtrl._RefreshGoodsGroupTitle = HL.Method(HL.Any, HL.Number) << function(self, title, luaIndex)
    local groupInfo = self.m_goodsGroupList[luaIndex]
    if not groupInfo or not title then
        return
    end
    title.gameObject.name = "GoodsGroupTitle_" .. luaIndex
    self:_SetGroupTitleVisual(title, groupInfo.titleName, groupInfo.titleIcon, groupInfo.hideDeco)
end

ShopStarCtrl._GetGoodsGroupCellCount = HL.Method(HL.Number).Return(HL.Number) << function(self, luaIndex)
    local groupInfo = self.m_goodsGroupList[luaIndex]
    return groupInfo and #groupInfo.goodsList or 0
end

ShopStarCtrl._GlobalToGroupLocal = HL.Method(HL.Number).Return(HL.Table) << function(self, globalLuaIndex)
    local remaining = globalLuaIndex
    for g = 1, #self.m_goodsGroupList do
        local count = #self.m_goodsGroupList[g].goodsList
        if remaining <= count then
            return { groupIndex = g, localIndex = remaining }
        end
        remaining = remaining - count
    end
    return {}
end

ShopStarCtrl._IsTopPanel = HL.Method().Return(HL.Boolean) << function(self)
    if PhaseManager:GetTopPhaseId() ~= PHASE_ID or UIManager:IsOpen(PanelId.ShopDetail)  then
        return false
    end
    return true
end

ShopStarCtrl._RefreshControllerTarget = HL.Method() << function(self)
    if not DeviceInfo.usingController then
        return
    end
    if not self:_IsTopPanel() then
        return
    end
    local selectedTagCell = self.m_goodsTagCellCache and self.m_goodsTagCellCache:Get(self.m_curSelectTagIndex)
    if selectedTagCell then
        self:SetNaviTarget(selectedTagCell.tagBtn)
        return
    end
    local firstCellObj = self.view.goodsGroupList:Get(0, 0)
    if not firstCellObj then
        return
    end
    local firstCell = self.m_getGoodsGroupCellFunc(firstCellObj)
    if firstCell and firstCell.view and firstCell.view.selectBtn then
        self:SetNaviTarget(firstCell.view.selectBtn)
    end
end

ShopStarCtrl._OnShopDataChanged = HL.Method(HL.Opt(HL.Any)) << function(self, _)
    if string.isEmpty(self.m_shopId) then
        return
    end
    self:_RefreshAll()
end

ShopStarCtrl._OnBuyItemSuccess = HL.Method(HL.Any) << function(self, msg)
    shopSystem:SetGoodsIdSee()
    if string.isEmpty(self.m_shopId) then
        return
    end
    local unpackMsg = unpack(msg)
    self:_RefreshSingleGoods(unpackMsg.GoodsId, unpackMsg.ShopId)
end

ShopStarCtrl._OnShopFrequencyLimitChange = HL.Method(HL.Any) << function(self, msg)
    local limitId, remainLimitCount
    if type(msg) == "table" then
        limitId, remainLimitCount = unpack(msg)
    end
    local goodsId = self:_GetGoodsIdFromFrequencyLimitId(limitId)
    self:_RefreshSingleGoods(goodsId, self.m_shopId, remainLimitCount)
end

ShopStarCtrl._RefreshSingleGoods = HL.Method(HL.String, HL.Opt(HL.String, HL.Number)) << function(self, goodsId, shopId, remainLimitCount)
    local searchInfo = self:_FindGoodsInfoByGoodsId(goodsId)
    if not searchInfo then
        return
    end
    local goodsInfo = searchInfo.goodsInfo
    goodsInfo.remainLimitCount = remainLimitCount or shopSystem:GetRemainCountByGoodsId(shopId or self.m_shopId, goodsId)
    goodsInfo.itemCount = Utils.getItemCount(goodsInfo.itemId, true, true)
    goodsInfo.remainLimitSort = goodsInfo.remainLimitCount == 0 and 1 or 0

    local goodsObj = self.view.goodsGroupList:Get(CSIndex(searchInfo.groupIndex), CSIndex(searchInfo.localIndex))
    if not goodsObj then
        return
    end
    local goodsCell = self.m_getGoodsGroupCellFunc(goodsObj)
    if goodsCell then
        goodsCell:InitShopStarGoodsCell(goodsInfo)
    end
end

ShopStarCtrl._GetGoodsIdFromFrequencyLimitId = HL.Method(HL.Opt(HL.String)).Return(HL.String) << function(self, limitId)
    if string.isEmpty(limitId) then
        return ""
    end
    local goodsId = string.match(limitId, "^[^#]+#(.+)$")
    return goodsId or limitId
end

ShopStarCtrl._FindGoodsInfoByGoodsId = HL.Method(HL.String).Return(HL.Opt(HL.Table)) << function(self, goodsId)
    if string.isEmpty(goodsId) then
        return nil
    end
    for groupIndex, groupInfo in ipairs(self.m_goodsGroupList) do
        for localIndex, goodsInfo in ipairs(groupInfo.goodsList) do
            if goodsInfo.goodsId == goodsId then
                return {
                    goodsInfo = goodsInfo,
                    groupIndex = groupIndex,
                    localIndex = localIndex,
                }
            end
        end
    end
    return nil
end

ShopStarCtrl._FindGoodsInfoByItemId = HL.Method(HL.String).Return(HL.Opt(HL.Table)) << function(self, itemId)
    if string.isEmpty(itemId) then
        return nil
    end
    for groupIndex, groupInfo in ipairs(self.m_goodsGroupList) do
        for localIndex, goodsInfo in ipairs(groupInfo.goodsList) do
            if goodsInfo.itemId == itemId then
                return {
                    goodsInfo = goodsInfo,
                    groupIndex = groupIndex,
                    localIndex = localIndex,
                }
            end
        end
    end
    return nil
end

ShopStarCtrl._ShopHasGoodsWithItemId = HL.Method(HL.String, HL.String).Return(HL.Boolean) << function(self, shopId, itemId)
    local shopData = shopSystem:GetShopData(shopId)
    if not shopData then
        return false
    end
    for _, goodsData in pairs(shopData.goodsDic) do
        local hasGoodsCfg, goodsCfg = Tables.shopGoodsTable:TryGetValue(goodsData.goodsTemplateId)
        if hasGoodsCfg and not string.isEmpty(goodsCfg.rewardId) then
            local itemBundle = UIUtils.getRewardFirstItem(goodsCfg.rewardId)
            if itemBundle and itemBundle.id == itemId then
                return true
            end
        end
    end
    return false
end

ShopStarCtrl._CollectPopupResumeState = HL.Method().Return(HL.Table) << function(self)
    local resumeOpenPanel = {}
    if PhaseManager:GetTopPhaseId() ~= PHASE_ID then
        return resumeOpenPanel
    end
    local isDetailOpen, shopDetailCtrl = UIManager:IsOpen(PanelId.ShopDetail)
    if isDetailOpen and shopDetailCtrl then
        local popupArg = shopDetailCtrl:GetCurPhaseStateArg()
        if popupArg then
            table.insert(resumeOpenPanel, {
                panelId = PanelId.ShopDetail,
                arg = popupArg,
            })
        end
    end
    return resumeOpenPanel
end

ShopStarCtrl._RestorePopupByResumeState = HL.Method(HL.Table) << function(self, panelInfo)
    if not panelInfo or panelInfo.panelId == nil then
        return
    end
    if self.m_phase then
        self.m_phase:CreatePhasePanelItem(panelInfo.panelId, panelInfo.arg)
    end
end

ShopStarCtrl._SyncFocusHelperToTagCell = HL.Method(HL.Any) << function(self, tagCell)
    local helperRT = self.view.focusHelperGoodsTagList.transform
    local worldPos = tagCell.transform.position
    local localPos = helperRT.parent:InverseTransformPoint(worldPos)
    helperRT.anchoredPosition = CS.UnityEngine.Vector2(helperRT.anchoredPosition.x, localPos.y)
end

ShopStarCtrl._SetCurSelectTag = HL.Method() << function(self)
    self:_OnClickGoodsTagCell(self.m_curSelectTagIndex)
    local selectedTagCell = self.m_goodsTagCellCache and self.m_goodsTagCellCache:Get(self.m_curSelectTagIndex)
    if selectedTagCell then
        self:SetNaviTarget(selectedTagCell.tagBtn)
    end
end

ShopStarCtrl._GetGoodsGroupListDefaultSelectable = HL.Method().Return(HL.Opt(HL.Any)) << function(self)
    local range = self.view.goodsGroupList:GetRangeInView()
    if not range or range.x < 0 then
        return nil
    end
    local targetCSIndex = range.x
    for csIndex = range.x, range.y do
        local result = self:_GlobalToGroupLocal(LuaIndex(csIndex))
        if result.groupIndex == self.m_curSelectTagIndex then
            targetCSIndex = csIndex
            break
        end
    end
    local obj = self.view.goodsGroupList:Get(targetCSIndex)
    if not obj then
        return nil
    end
    local cell = self.m_getGoodsGroupCellFunc(obj)
    if cell and cell.view and cell.view.selectBtn then
        self.view.goodsTagListSelectableNaviGroup:SetLayerSelectedTarget(nil, false)
        return cell.view.selectBtn
    end
    return nil
end

ShopStarCtrl._ScrollToGoodsGroupByTagIndex = HL.Method(HL.Number, HL.Opt(HL.Boolean)) << function(self, luaIndex, noTween)
    self.m_isScrollingByCode = true
    self.m_waitAutoScrollTagListTime = -1
    if luaIndex > 0 and luaIndex <= #self.m_goodsGroupList then
        self.view.goodsGroupList:ScrollToIndex(CSIndex(luaIndex), 0, noTween or false, CS.Beyond.UI.UIScrollList.ScrollAlignType.Top)
    end
end

ShopStarCtrl._SyncTagBySelectedGoods = HL.Method(HL.Any) << function(self, target)
    if not target then
        return
    end
    local totalCount = self.view.goodsGroupList.totalCellCount
    if totalCount <= 0 then
        return
    end
    for csIndex = 0, totalCount - 1 do
        local obj = self.view.goodsGroupList:Get(csIndex)
        if obj then
            local cell = self.m_getGoodsGroupCellFunc(obj)
            if cell and cell.view and cell.view.selectBtn == target then
                local result = self:_GlobalToGroupLocal(LuaIndex(csIndex))
                if result.groupIndex and self.m_curSelectTagIndex ~= result.groupIndex then
                    self:_OnChangeSelectTagUI(result.groupIndex)
                end
                return
            end
        end
    end
end

ShopStarCtrl._TrySyncTagSelectionByScroll = HL.Method() << function(self)
    local viewRange = self.view.goodsGroupList:GetRangeInView()
    if not viewRange or viewRange.x < 0 or viewRange.y < 0 then
        return
    end
    local firstResult = self:_GlobalToGroupLocal(LuaIndex(viewRange.x))
    local firstGroup = firstResult.groupIndex
    if not firstGroup then
        return
    end
    local newIndex = firstGroup
    if newIndex < 1 then
        newIndex = 1
    elseif newIndex > #self.m_goodsGroupList then
        newIndex = #self.m_goodsGroupList
    end
    if self.m_curSelectTagIndex ~= newIndex then
        self:_OnChangeSelectTagUI(newIndex)
    end
end

ShopStarCtrl._SetGroupTitleVisual = HL.Method(HL.Table, HL.String, HL.String, HL.Boolean) << function(self, title, titleName, titleIcon, hideDeco)
    if title.goodsTagTxt then
        title.goodsTagTxt.text = titleName
    end
    if title.goodsTagImg then
        title.goodsTagImg.gameObject:SetActiveIfNecessary(not string.isEmpty(titleIcon))
        if not string.isEmpty(titleIcon) then
            title.goodsTagImg:LoadSprite(UIConst.UI_SPRITE_SHOP_TAG_ICON, titleIcon)
        end
    end
    if title.stateController then
        title.stateController:SetState(hideDeco and "NoDecoState" or "NormalState")
    end
end

ShopStarCtrl.GetCurPhaseStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    return {
        shopGroupId = self.m_shopGroupId,
        shopId = self.m_shopId,
        curSelectTagIndex = self.m_curSelectTagIndex,
        resumeOpenPanel = self:_CollectPopupResumeState(),
    }
end

ShopStarCtrl.OnClose = HL.Override() << function(self)
    shopSystem:SetGoodsIdSee()
end

HL.Commit(ShopStarCtrl)
