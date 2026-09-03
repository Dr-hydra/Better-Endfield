
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ShopTradeItem
local NULL_SYMBOL = "--%"

local PANEL_STATE =
{
    Purchase = "Purchase",
    Sell = "Sell",
    FriendPrice = "FriendPrice"
}
local PRICE_CHART_LINE_MIN = -80
local PRICE_CHART_LINE_MAX = 90

ShopTradeItemCtrl = HL.Class('ShopTradeItemCtrl', uiCtrl.UICtrl)

ShopTradeItemCtrl.m_args = HL.Field(HL.Table)

ShopTradeItemCtrl.m_friendSystem = HL.Field(CS.Beyond.Gameplay.FriendSystem)

ShopTradeItemCtrl.m_shopSystem = HL.Field(CS.Beyond.Gameplay.ShopSystem)


ShopTradeItemCtrl.m_genPriceItemCellCache = HL.Field(HL.Forward('UIListCache'))

ShopTradeItemCtrl.m_hisPrice = HL.Field(HL.Userdata)

ShopTradeItemCtrl.m_domainGoodsData = HL.Field(HL.Userdata)

ShopTradeItemCtrl.m_standardPrice = HL.Field(HL.Number) << -1

ShopTradeItemCtrl.m_shopGroupRemainCount = HL.Field(HL.Number) << -1

ShopTradeItemCtrl.m_normalizedPriceValues = HL.Field(HL.Table)

ShopTradeItemCtrl.m_lastState = HL.Field(HL.String) << ""

ShopTradeItemCtrl.m_curState = HL.Field(HL.String) << ""

ShopTradeItemCtrl.m_itemId = HL.Field(HL.String) << ""

ShopTradeItemCtrl.m_moneyId = HL.Field(HL.String) << ""

ShopTradeItemCtrl.m_friendList = HL.Field(HL.Table)

ShopTradeItemCtrl.m_getFriendPriceCellFunc = HL.Field(HL.Function)

ShopTradeItemCtrl.m_friendDataIsInit = HL.Field(HL.Boolean) << false

ShopTradeItemCtrl.m_nowNaviFriendCell = HL.Field(HL.Table)

ShopTradeItemCtrl.m_friendDetailKey = HL.Field(HL.Number) << -1

ShopTradeItemCtrl.m_waitSellRsp = HL.Field(HL.Boolean) << false





ShopTradeItemCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_FRIEND_INFO_SYNC] = '_UpdateFriend',
    [MessageConst.ON_FRIEND_GOODS_INFO_SYNC] = '_UpdateFriendGoodsList',
    [MessageConst.ON_BUY_ITEM_SUCC] = '_OnBuyItemSucc',
    [MessageConst.ON_SELL_ITEM_SUCC] = '_OnSellItemSucc',
}


ShopTradeItemCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_args = arg
    self.view.closeButton.onClick:AddListener(function()
        self:PlayAnimationOutAndClose()
    end)
    self.view.btnBackLv01.onClick:AddListener(function()
        if self.m_curState == PANEL_STATE.FriendPrice then
            if self.m_lastState == PANEL_STATE.Sell then
                self:_SwitchSellState()
            else
                self:_SwitchPurchaseState()
            end
        else
            self:PlayAnimationOutAndClose()
        end
    end)
    self:_InitData()
    self.view.detailBtn.onClick:AddListener(function()
        Notify(MessageConst.SHOW_ITEM_TIPS, {
            itemId = self.m_itemId,
            itemCount = self.m_domainGoodsData.quantity,
            transform = self.view.detailBtn.transform,
        })
    end)
    self.view.helpBtn.onClick:AddListener(function()
        UIManager:Open(PanelId.InstructionBook, "trade_item")
    end)
    self.view.viewFriendPricesBtn.onClick:AddListener(function()
        self:_SwitchFriendPriceState()
    end)

    local isPurchase = false
    if self.m_args.isSellOnly or self.m_args.isDefaultSell then
        self:_SwitchSellState()
        if self.m_args.isSellOnly then
            self.view.operationTab.gameObject:SetActive(false)
        end
        isPurchase = false
    else
        self:_SwitchPurchaseState()
        isPurchase = true
    end
    self.view.operationToggle:InitCommonToggle(function(isOn)
        if isOn then
            self:_SwitchPurchaseState()
        else
            self:_SwitchSellState()
        end
    end, isPurchase, false)
    if DeviceInfo.usingController and not self.m_args.isSellOnly then
        local setOnActionId = self.view.operationToggle.view.keyHintOn.actionId
        local setOffActionId = self.view.operationToggle.view.keyHintOff.actionId
        UIUtils.bindInputPlayerAction(setOnActionId, function()
            self:_TogglePurchaseState()
            AudioManager.PostEvent("Au_UI_Toggle_FacPower_Switch_On")
        end, self.view.operationToggle.view.inputBindingGroupMonoTarget.groupId)
        UIUtils.bindInputPlayerAction(setOffActionId, function()
            self:_ToggleSellState()
            AudioManager.PostEvent("Au_UI_Toggle_FacPower_Switch_Off")
        end, self.view.operationToggle.view.inputBindingGroupMonoTarget.groupId)
    end

    self.m_friendDetailKey = self:BindInputPlayerAction("shop_item_friend_detail", function()
        if self.m_nowNaviFriendCell then
            self.m_nowNaviFriendCell.headIconImgButton.onClick:Invoke()
        end
    end, self.view.friendPriceScrollListInputBindingGroupMonoTarget.groupId)
    InputManagerInst:ToggleBinding(self.m_friendDetailKey, false)
    InputManagerInst:SetBindingText(self.m_friendDetailKey, Language.LUA_DOMAIN_SHOP_ITEM_FRIEND_DETAIL_KEY_HINT)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })

    GameInstance.player.shopSystem:SetSingleGoodsIdSee(self.m_args.goodsData.goodsTemplateId)
    self:_ApplyResumeState(self.m_args and self.m_args.resumeState or nil)
end


ShopTradeItemCtrl.OnShow = HL.Override() << function(self)

end

ShopTradeItemCtrl.OnHide = HL.Override() << function(self)

end

ShopTradeItemCtrl.OnClose = HL.Override() << function(self)

end


ShopTradeItemCtrl._InitData = HL.Method() << function(self)
    self.m_shopSystem = GameInstance.player.shopSystem
    self.m_friendSystem = GameInstance.player.friendSystem
    self.view.purchaseNumeberTxt.text = 1
    local goodsId = self.m_args.goodsData.goodsTemplateId
    local goodsTableData = Tables.shopGoodsTable:GetValue(goodsId)
    self.m_shopGroupRemainCount = self.m_shopSystem:GetRemainLimitCountByShopId(self.m_args.goodsData.shopId)
    local shopGoodsData = self.m_shopSystem:GetShopGoodsData(self.m_args.goodsData.shopId, self.m_args.goodsData.goodsId)
    self.m_domainGoodsData = shopGoodsData.domainRandomGoodsData

    if self.m_args.goodsData.roleId then
        self.m_hisPrice = self.m_args.goodsData.historyPrice
    else
        self.m_hisPrice = self.m_domainGoodsData.historyPrice
    end
    self.m_standardPrice = goodsTableData.randomGoodsStandardPrice
    local curPrice = self.m_hisPrice[CSIndex(1)]
    local moneyId = goodsTableData.moneyId
    self.m_moneyId = moneyId

    self.view.costTxt.text = curPrice
    self.view.moneyCell:InitMoneyCell(self.m_moneyId)
    local moneyItemData = Tables.itemTable:GetValue(moneyId)
    self.view.costIconImg1:LoadSprite(UIConst.UI_SPRITE_WALLET, moneyItemData.iconId)
    self.view.costIconImg2:LoadSprite(UIConst.UI_SPRITE_WALLET, moneyItemData.iconId)
    self.view.costMoneyTxt.text = moneyItemData.name

    local displayItem = UIUtils.getRewardFirstItem(goodsTableData.rewardId)
    local itemId = displayItem.id
    local itemData = Tables.itemTable[itemId]
    self.view.itemNameTxt.text = itemData.name
    self.m_itemId = itemId


    UIUtils.setItemRarityImage(self.view.rarityLine, itemData.rarity)
    self.view.itemIconImg1:LoadSprite(UIConst.UI_SPRITE_ITEM_BIG, itemData.iconId)
    self.view.itemIconImg2:LoadSprite(UIConst.UI_SPRITE_ITEM_BIG, itemData.iconId)

    local itemCount = self.m_domainGoodsData.quantity
    self.view.ownNumberTxt.text = itemCount
    self.view.averageCostPrice.gameObject:SetActive(itemCount ~= 0)
    self.view.averageCostPriceTxt.text = itemCount == 0 and 0 or self.m_domainGoodsData.avgPrice

    local itemTypeName = UIUtils.getItemTypeName(itemId)
    self.view.itemTypeTxt.text = itemTypeName


    self.m_genPriceItemCellCache = UIUtils.genCellCache(self.view.priceItem)
    self.m_genPriceItemCellCache:Refresh(self.m_hisPrice.Count)


    if self.m_domainGoodsData.quantity > 0 then
        local percentage = curPrice * 100 / self.m_domainGoodsData.avgPrice - 100
        self.view.profitRatioTxtLayout.gameObject:SetActive(percentage ~= 0)
        self.view.profitArrow.gameObject:SetActive(percentage ~= 0)
        percentage = lume.round(percentage,0.1)
        percentage = tonumber(string.format("%.1f", percentage))
        self.view.profitRatioTxt.text = percentage
        local totalPrice = self.m_domainGoodsData.avgPrice * self.m_domainGoodsData.quantity
        self.view.priceTxt.text = totalPrice
        self.view.profitTxt.text = curPrice * self.m_domainGoodsData.quantity - totalPrice
        self.view.profitArrow:SetState(DomainShopUtils.getProfitArrowStateName(percentage))
        self.view.moneyIconImg:LoadSprite(UIConst.UI_SPRITE_WALLET, moneyItemData.iconId)
    end
end

ShopTradeItemCtrl._ApplyResumeState = HL.Method(HL.Opt(HL.Any)) << function(self, resumeState)
    if not resumeState then
        return
    end
    local tradeState = resumeState.tradeState
    if tradeState == PANEL_STATE.Sell or self.m_args.isSellOnly then
        if not self.m_args.isSellOnly and self.view.operationToggle.view.toggle.isOn ~= false then
            self.view.operationToggle.view.toggle.isOn = false
        else
            self:_SwitchSellState()
        end
    else
        if self.view.operationToggle.view.toggle.isOn ~= true then
            self.view.operationToggle.view.toggle.isOn = true
        else
            self:_SwitchPurchaseState()
        end
    end

    local selectedCount = math.max(resumeState.selectedCount or 1, 1)
    self.view.numberSelector:RefreshNumber(selectedCount)
    if resumeState.isFriendPriceShown == true then

        self:_SwitchFriendPriceState()
    end
end

ShopTradeItemCtrl._TogglePurchaseState = HL.Method() << function(self)
    self.view.operationToggle.view.toggle.isOn = true
end

ShopTradeItemCtrl._ToggleSellState = HL.Method() << function(self)
    self.view.operationToggle.view.toggle.isOn = false
end

ShopTradeItemCtrl._SwitchPurchaseState = HL.Method() << function(self)
    if self.m_curState == PANEL_STATE.Purchase  then
        return
    end
    self:OnFriendCellNaviTargetChange(nil)
    self.view.btnBackLv01.gameObject:SetActive(false)
    self.view.closeButton.gameObject:SetActive(true)
    self.view.profitAndPriceInfo.gameObject:SetActive(false)
    self.m_lastState = self.m_curState
    self.m_curState = PANEL_STATE.Purchase
    self.view.main:SetState(self.m_curState)
    local remainCount = self.m_shopSystem:GetRemainCountByGoodsId(self.m_args.goodsData.shopId, self.m_args.goodsData.goodsId)
    local curPrice = self.m_hisPrice[CSIndex(1)]
    local shopGoodsData = self.m_shopSystem:GetShopGoodsData(self.m_args.goodsData.shopId, self.m_args.goodsData.goodsId)
    local limitCount = shopGoodsData.limitCount

    local haveMoney = Utils.getItemCount(self.m_moneyId)
    local maxBuy = math.floor(haveMoney / curPrice)
    maxBuy = math.max(maxBuy, 1)
    maxBuy = math.min(maxBuy, remainCount == -1 and self.m_shopGroupRemainCount or remainCount)
    self.view.inventoryTagTxt.text = remainCount == -1 and "∞" or string.format("%d/%d", remainCount / limitCount)


    if self.m_shopGroupRemainCount == 0 or remainCount == 0 then
        self.view.operationContent:SetState("LimitUnablePurchaseState")
    elseif haveMoney < curPrice then
        self.view.operationContent:SetState("CurrencyUnablePurchaseState")
    else
        self.view.purchaseBtn.onClick:RemoveAllListeners()
        self.view.purchaseBtn.onClick:AddListener(function()
            self:_OnClickBuyConfirm()
        end)
        self.view.operationContent:SetState("PurchaseState")
    end

    self.view.numberSelector:InitNumberSelector(1, 1, maxBuy, function(newNum)
        self.view.purchaseNumeberTxt.text = math.floor(newNum)
        self.view.costNumberTxt.text = math.floor(newNum * curPrice)
    end)

    local selectorActive = (remainCount <= 1 and remainCount ~= -1) or haveMoney < curPrice or maxBuy <= 1
    self.view.numberSelector.gameObject:SetActive(not selectorActive)



    self.view.suggestedPriceTxt.text = self.m_standardPrice
    local baseNormalized, avgValue
    self.m_normalizedPriceValues, baseNormalized, avgValue = self:_NormalizePriceValues(self.m_standardPrice, self.m_hisPrice)
    self:_SetPriceChartLinePos(baseNormalized)
    self.m_genPriceItemCellCache:Update(function(cell, index)
        self:_InitSinglePriceItem(cell, index, baseNormalized)
    end)


    local percentage = self.m_hisPrice[CSIndex(1)] *100 / self.m_standardPrice - 100
    self.view.priceArrow:SetState(DomainShopUtils.getProfitArrowStateName(percentage))
    local suggestPrice = math.abs(self.m_hisPrice[CSIndex(1)] - self.m_standardPrice)
    self.view.compareSuggestPriceTxt.text = suggestPrice
    self.view.compareSuggestPrice.gameObject:SetActive(true)
    percentage = lume.round(percentage,0.1)
    local format = percentage >= 0 and string.format("(+%.1f%%)", percentage) or string.format("(%.1f%%)", percentage)
    self.view.priceFluctuationTxt.text = format
    self:_RefreshLimitCount()
end

ShopTradeItemCtrl._RefreshLimitCount = HL.Method() << function(self)
    local diff = DomainShopUtils.getNextServerRefreshTimeLeftSecByType(GEnums.ShopFrequencyLimitType.Daily)
    if diff >= 0 then
        self.view.refreshCountdownTxt.text = string.format(Language.LUA_TRADE_ITEM_REFRESH_LEFT_TIME, UIUtils.getShortLeftTime(diff))
        local shopId = self.m_args.goodsData.shopId

        local shopGroupId = self.m_shopSystem:GetShopGroupIdByShopId(shopId)
        local groupData = self.m_shopSystem:GetShopGroupData(shopGroupId)
        local limitUpCount = groupData.domainChannelData.buyRandomGoodsLimitUpCount
        local maxLimitCount = groupData.domainChannelData.buyRandomGoodsLimitCount
        if groupData.domainChannelData then
            self.view.refreshCountdownText.text = string.format("+%d", limitUpCount)

            local overflowPromptNodeActive = (self.m_shopGroupRemainCount + limitUpCount) > maxLimitCount
            self.view.overflowPromptNode.gameObject:SetActive(overflowPromptNodeActive)
            self.view.remainingOrderTxt.text = string.format("%d/%d",self.m_shopGroupRemainCount, maxLimitCount)
        else
            self.view.overflowPromptNode.gameObject:SetActive(false)
            self.view.refreshCountdownText.gameObject:SetActive(false)
        end
    else
        self.view.supplementPurchaseInfo.gameObject:SetActive(false)
    end
end

ShopTradeItemCtrl._SwitchSellState = HL.Method() << function(self)
    if self.m_curState == PANEL_STATE.Sell  then
        return
    end
    self:OnFriendCellNaviTargetChange(nil)
    self.view.btnBackLv01.gameObject:SetActive(false)
    self.view.closeButton.gameObject:SetActive(true)
    self.view.profitAndPriceInfo.gameObject:SetActive(self.m_domainGoodsData.quantity > 0)
    self.m_lastState = self.m_curState
    self.m_curState = PANEL_STATE.Sell
    self.view.main:SetState(self.m_curState)
    local avgPrice = self.m_domainGoodsData.avgPrice

    local itemCount = self.m_domainGoodsData.quantity
    self.view.inventoryTagTxt.text = itemCount
    self.view.ownNumberTxt.text = itemCount
    self.view.averageCostPrice.gameObject:SetActive(itemCount ~= 0)


    if itemCount > 0 then
        self.view.sellBtn.onClick:RemoveAllListeners()
        self.view.sellBtn.onClick:AddListener(function()
            self:_OnClickSellConfirm()
        end)
        self.view.operationContent:SetState("SellState")
    else
        self.view.operationContent:SetState("NotSellableState")
    end
    self.view.numberSelector:InitNumberSelector(1, 1, itemCount, function(newNum)
        self.view.sellNumberTxt.text = math.floor(newNum)
        self.view.costNumberTxt.text = math.floor(newNum * self.m_hisPrice[CSIndex(1)])
    end)

    local selectorActive = itemCount <= 1 and itemCount ~= -1
    self.view.numberSelector.gameObject:SetActive(not selectorActive)


    local disPlayPrice
    if self.m_domainGoodsData.quantity ~= 0 then
        disPlayPrice = avgPrice
        self.view.costPriceLine.text = Language.LUA_TRADE_ITEM_COST_PRICE_LINE
    else
        disPlayPrice = self.m_standardPrice
        self.view.costPriceLine.text = Language.LUA_TRADE_ITEM_SUGGESTED_PRICE_LINE
    end
    self.view.suggestedPriceTxt.text = disPlayPrice
    local baseNormalized, hisAvgValue
    self.m_normalizedPriceValues, baseNormalized, hisAvgValue = self:_NormalizePriceValues(disPlayPrice, self.m_hisPrice)
    self:_SetPriceChartLinePos(baseNormalized)
    self.m_genPriceItemCellCache:Update(function(cell, index)
        self:_InitSinglePriceItem(cell, index, baseNormalized)
    end)


    local percentage = self.m_hisPrice[CSIndex(1)] * 100 / self.m_domainGoodsData.avgPrice - 100
    self.view.priceArrow:SetState(DomainShopUtils.getProfitArrowStateName(percentage))
    local suggestPrice = math.abs(self.m_hisPrice[CSIndex(1)] - self.m_domainGoodsData.avgPrice)
    self.view.compareSuggestPriceTxt.text = suggestPrice
    self.view.compareSuggestPrice.gameObject:SetActive(self.m_domainGoodsData.quantity ~= 0)
    if self.m_domainGoodsData.quantity == 0 then
        self.view.priceFluctuationTxt.gameObject:SetActive(false)
    else
        self.view.priceFluctuationTxt.gameObject:SetActive(true)
        percentage = lume.round(percentage,0.1)
        local format = percentage >= 0 and string.format("(+%.1f%%)", percentage) or string.format("(%.1f%%)", percentage)
        self.view.priceFluctuationTxt.text = format
    end
end

ShopTradeItemCtrl._SetPriceChartLinePos = HL.Method(HL.Number) << function(self, baseNormalized)
    local basePosition = baseNormalized * (math.abs(PRICE_CHART_LINE_MAX) + math.abs(PRICE_CHART_LINE_MIN)) / 2 + (PRICE_CHART_LINE_MAX + PRICE_CHART_LINE_MIN) / 2
    self.view.priceChartLine.anchoredPosition = Vector2(self.view.priceChartLine.anchoredPosition.x, basePosition)
end


ShopTradeItemCtrl._SwitchFriendPriceState = HL.Method() << function(self)
    if self.m_curState == PANEL_STATE.FriendPrice  then
        return
    end
    InputManagerInst:ToggleBinding(self.m_friendDetailKey, true)

    self.view.btnBackLv01.gameObject:SetActive(true)
    self.view.closeButton.gameObject:SetActive(false)
    self.m_lastState = self.m_curState
    self.m_curState = PANEL_STATE.FriendPrice
    self.view.main:SetState(self.m_curState)
    if not self.m_friendDataIsInit then
        self.view.loadingNode.gameObject:SetActive(true)
    end


    local moneyItemData = Tables.itemTable:GetValue(self.m_moneyId)
    local myPriceItem = self.view.myPriceItem
    myPriceItem.compareHoldingsArrowLayout.gameObject:SetActive(false)
    myPriceItem.compareLocalArrowLayout.gameObject:SetActive(false)
    myPriceItem.compareLocalTxt.text = NULL_SYMBOL
    myPriceItem.compareHoldingsTxt.text = NULL_SYMBOL
    myPriceItem.purchasePriceIcon:LoadSprite(UIConst.UI_SPRITE_WALLET, moneyItemData.iconId)
    myPriceItem.purchasePriceTxt.text = self.m_domainGoodsData.avgPrice
    local roleId = GameInstance.player.roleId
    local nameStr, avatarPath, avatarFramePath, psName = FriendUtils.getFriendInfoByRoleId(roleId)
    myPriceItem.friendNameTxt.text = Utils.getPlayerName()
    if not string.isEmpty(avatarPath) then
        myPriceItem.headIconImg:LoadSprite(avatarPath)
    end
    myPriceItem.headIconImgButton.enabled = false
    myPriceItem.emptyNode.gameObject:SetActive(string.isEmpty(avatarPath))
    myPriceItem.headIconImg.gameObject:SetActive(not string.isEmpty(avatarPath))
    if not self.m_friendDataIsInit then
        self.view.noFriendState.gameObject:SetActive(false)
        self.view.noFriendHaveDomainShopState.gameObject:SetActive(false)
        self.view.friendPriceScrollList.gameObject:SetActive(false)
        self.m_friendSystem:SyncFriendSimpleInfo()
    end
end

ShopTradeItemCtrl._UpdateFriend = HL.Method() << function(self)
    if self.m_friendDataIsInit then
        return
    end
    self.m_friendDataIsInit = true


    self.m_friendList = {}
    local friendList = {}
    local index = 1
    for roleId, friendInfo in cs_pairs(self.m_friendSystem.friendInfoDic) do
        if friendInfo.guestRoomUnlock then
            self.m_friendList[index] = FriendUtils.friendInfo2SortInfo(friendInfo)
            table.insert(friendList, self.m_friendList[index].roleId)
            index = index + 1
        end
    end
    local noFriend = self.m_friendSystem.friendInfoDic.Count == 0
    local noFriendUnlockGuestRoom = index == 1

    if noFriend or noFriendUnlockGuestRoom then
        self.view.loadingNode.gameObject:SetActive(false)
        self.view.friendPriceScrollList.gameObject:SetActive(false)
        if noFriend then
            self.view.noFriendState.gameObject:SetActive(true)
        else
            self.view.noFriendHaveDomainShopState.gameObject:SetActive(true)
        end
    else
        self.m_shopSystem:SendQueryFriendGoodsPrice(self.m_args.goodsData.shopId, self.m_args.goodsData.goodsId, friendList)
    end
end

ShopTradeItemCtrl._UpdateFriendGoodsList = HL.Method() << function(self)

    if not self.m_friendList then
        return
    end
    self.m_getFriendPriceCellFunc = UIUtils.genCachedCellFunction(self.view.friendPriceScrollList)

    local friendList = {}
    for i, v in ipairs(self.m_friendList) do
        local friendGoodsData = self.m_shopSystem:GetFriendGoodsData(self.m_args.goodsData.goodsId, v.roleId)
        if friendGoodsData then
            local friendGoodsList = {}
            friendGoodsList.friendInfo = v
            friendGoodsList.goodsData = friendGoodsData
            friendGoodsList.curPrice = friendGoodsData.historyPrice[CSIndex(1)]
            table.insert(friendList, friendGoodsList)
        end
    end
    table.sort(friendList, Utils.genSortFunction({"curPrice"}, false))
    self.m_friendList = friendList
    local friendCount = #self.m_friendList
    local ids = {}
    for i, info in pairs(self.m_friendList) do
        local roleId = info.friendInfo.roleId
        table.insert(ids, roleId)
    end
    if friendCount > 0 then
        self.view.friendPriceScrollList.onUpdateCell:AddListener(function(object, csIndex)
            local cell = self.m_getFriendPriceCellFunc(object)
            self:_InitSingleFriendItem(cell, LuaIndex(csIndex))
        end)
        self.view.friendPriceScrollList:UpdateCount(friendCount)
    end
    self.view.noFriendHaveDomainShopState.gameObject:SetActive(friendCount == 0)
    self.view.friendPriceScrollList.gameObject:SetActive(friendCount ~= 0)
    self.view.loadingNode.gameObject:SetActive(false)
end

ShopTradeItemCtrl._InitSingleFriendItem = HL.Method(HL.Table, HL.Number) << function(self, cell, index)
    local fiendData = self.m_friendList[index]
    local moneyItemData = Tables.itemTable:GetValue(self.m_moneyId)

    local curPrice = self.m_domainGoodsData.historyPrice[CSIndex(1)]
    local friendCurPrice = fiendData.curPrice
    local localPriceRate = (friendCurPrice  - curPrice) * 100 / curPrice
    local localPercentage = math.abs(localPriceRate)
    localPercentage = lume.round(localPercentage, 0.1)
    cell.compareLocalTxt.text = string.format("%.1f%%", localPercentage)
    cell.compareLocalArrowLayout.gameObject:SetActive(localPercentage ~= 0)
    cell.compareLocalArrow:SetState(DomainShopUtils.getProfitArrowStateName(localPriceRate))

    local itemCount = self.m_domainGoodsData.quantity
    local holdingPriceRate = (friendCurPrice - self.m_domainGoodsData.avgPrice) * 100 / self.m_domainGoodsData.avgPrice
    local holdingPercentage = math.abs(holdingPriceRate )
    holdingPercentage = lume.round(holdingPercentage,0.1)
    if itemCount == 0 then
        cell.compareHoldingsTxt.text = NULL_SYMBOL
    else
        cell.compareHoldingsTxt.text = string.format("%.1f%%", holdingPercentage)
    end
    cell.compareHoldingsArrowLayout.gameObject:SetActive(holdingPercentage ~= 0 and itemCount ~= 0)
    cell.compareHoldingsArrow:SetState(DomainShopUtils.getProfitArrowStateName(holdingPriceRate))
    local friendRoleId = fiendData.friendInfo.roleId
    local friendInfo = GameInstance.player.spaceship:GetFriendRoleInfo()
    if GameInstance.player.spaceship.isViewingFriend and friendInfo and friendRoleId == friendInfo.roleId then
        cell.currentTag.gameObject:SetActive(true)
    else
        cell.currentTag.gameObject:SetActive(false)
    end
    cell.purchasePriceIcon:LoadSprite(UIConst.UI_SPRITE_WALLET, moneyItemData.iconId)
    cell.purchasePriceTxt.text = fiendData.curPrice
    local nameStr, avatarPath, avatarFramePath, psName = FriendUtils.getFriendInfoByRoleId(friendRoleId)
    cell.friendNameTxt.text = nameStr
    if not string.isEmpty(avatarPath) then
        cell.headIconImg:LoadSprite(avatarPath)
    end
    cell.psIcon.gameObject:SetActive(not string.isEmpty(psName))
    cell.emptyNode.gameObject:SetActive(string.isEmpty(avatarPath))
    cell.headIconImgButton.onClick:RemoveAllListeners()
    cell.headIconImgButton.onClick:AddListener(function()
        local args = {
            transform = cell.detailTarget.transform,
            actions = {}
        }
        table.insert(args.actions, {
            text = Language.LUA_VISIT_FRIEND_SPACESHIP_TEXT,
            action = function()
                local isBuild = GameInstance.player.spaceship:IsRoomBuild(Tables.spaceshipConst.guestRoomId)
                if not isBuild then
                    Notify(MessageConst.SHOW_TOAST, Language.LUA_FRIEND_UNABLE_VISIT_GUEST_ROOM_TIPS)
                    return
                end

                if not Utils.isSystemUnlocked(GEnums.UnlockSystemType.SpaceshipSystem) then
                    Notify(MessageConst.SHOW_TOAST, Language.LUA_VISIT_OWN_SPACESHIP_LOCKED_TOAST)
                    return
                end
                local success, info = self.m_friendSystem.friendInfoDic:TryGetValue(friendRoleId)
                if not success then
                    return
                end
                if success and not info.guestRoomUnlock then
                    Notify(MessageConst.SHOW_TOAST, Language.LUA_VISIT_FRIEND_SPACESHIP_LOCKED_TOAST)
                    return
                end
                if GameInstance.player.spaceship.isViewingFriend and friendInfo and friendRoleId == friendInfo.roleId then
                    Notify(MessageConst.SHOW_TOAST, Language.ui_spaceship_shoptradeitem_friendpricedetails_toast_visitcurrent)
                    return
                end
                GameInstance.player.spaceship:VisitFriendSpaceShip(friendRoleId)
                self:Close()
            end})
        table.insert(args.actions, {
            text = Language.LUA_FRIEND_TIP_SHOW_BUSINESS_CARD,
            action = function()
                Notify(MessageConst.ON_OPEN_BUSINESS_CARD_PREVIEW, { roleId = friendRoleId, isPhase = true })
            end
        })
        Notify(MessageConst.SHOW_NAVI_TARGET_ACTION_MENU, args)
    end)
    cell.inputBindingGroupNaviDecorator.onGroupSetAsNaviTarget:AddListener(function(select)
        if select then
            self:OnFriendCellNaviTargetChange(cell)
        end
    end)
    if not self.m_nowNaviFriendCell and index == 1 then
        self:SetNaviTarget(cell.inputBindingGroupNaviDecorator)
        self:OnFriendCellNaviTargetChange(cell)
    end
end

ShopTradeItemCtrl.OnFriendCellNaviTargetChange = HL.Method(HL.Opt(HL.Table)) << function(self, cell)
    if cell ~= nil then
        self.m_nowNaviFriendCell = cell
    end
    if not cell then
        InputManagerInst:ToggleBinding(self.m_friendDetailKey, false)
    end
end

ShopTradeItemCtrl._InitSinglePriceItem = HL.Method(HL.Table, HL.Number, HL.Number)
    << function(self, cell, index, baseNormalized)
    local hisCount = self.m_hisPrice.Count
    cell.stateController:SetState(index == hisCount and "CurrentState" or "HistoryState")
    local price = self.m_hisPrice[hisCount - index]
    local sliderValue = math.abs(self.m_normalizedPriceValues[hisCount - CSIndex(index)])
    local isUpPrice = self.m_normalizedPriceValues[hisCount - CSIndex(index)] > 0
    cell.belowSlider.gameObject:SetActive(not isUpPrice)
    cell.upSlider.gameObject:SetActive(isUpPrice)
    local midY = (math.abs(PRICE_CHART_LINE_MAX) + math.abs(PRICE_CHART_LINE_MIN)) / 2
    if isUpPrice then
        cell.upSlider.value = sliderValue
        cell.upSliderValueTxt.text = price
        UIUtils.setSizeDeltaY(cell.upSliderRectTransform, midY + midY * (-baseNormalized))
    else
        cell.belowSlider.value = sliderValue
        cell.belowSliderValueTxt.text = price
        UIUtils.setSizeDeltaY(cell.belowSliderRectTransform, midY + midY * baseNormalized)
    end
end

ShopTradeItemCtrl._OnClickSellConfirm = HL.Method() << function(self)
    if self.m_waitSellRsp then
        return
    end
    local sellCount = self.view.numberSelector.curNumber
    local info = self.m_args.goodsData
    local roleId = GameInstance.player.roleId
    local isSellLocal = true
    if self.m_args.goodsData.roleId then
        roleId = self.m_args.goodsData.roleId
        isSellLocal = false
    end
    local success = self.m_shopSystem:SendSellGoods(roleId, info.shopId, info.goodsId, sellCount, isSellLocal)
    if success then
        self.m_waitSellRsp = true
    end
end


ShopTradeItemCtrl._OnClickBuyConfirm = HL.Method() << function(self)
    local buyCount = self.view.numberSelector.curNumber
    local info = self.m_args.goodsData


    local inventorySystem = GameInstance.player.inventory
    if inventorySystem:IsPlaceInBag(self.m_itemId) then

        local itemTableData = Tables.itemTable[self.m_itemId]
        local stackCount = itemTableData.maxBackpackStackCount
        local oneCount = tonumber(string.sub(self.view.amountTxt.text,2))

        local totalCount = oneCount * buyCount

        local itemBag = inventorySystem.itemBag:GetOrFallback(Utils.getCurrentScope())
        local emptySlotCount = itemBag.slotCount - itemBag:GetUsedSlotCount()
        local sameItemCount = itemBag:GetCount(info.itemId)
        local itemSlotCount = math.ceil(sameItemCount / stackCount)

        local capacity
        if itemSlotCount > 0 then
            capacity = (emptySlotCount + itemSlotCount) * stackCount - sameItemCount
        else
            capacity = stackCount * emptySlotCount
        end

        if capacity < totalCount then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_SHOP_BACKPACK_FULL)
            return
        end
    end
    self.m_shopSystem:BuyGoods(info.shopId, info.goodsId, buyCount)
end

ShopTradeItemCtrl._NormalizePriceValues = HL.Method(HL.Number, HL.Any)
                                            .Return(HL.Table, HL.Number, HL.Number)
    << function(self, base, numbers)
    local minValue = math.huge
    local maxValue = -math.huge
    local avgValue
    local result = {}
    for i = 1, numbers.Count do
        local num = numbers[CSIndex(i)]
        if num < minValue then
            minValue = num
        end
        if num > maxValue then
            maxValue = num
        end
    end
    local basePosition = (base - minValue) / (maxValue - minValue) * 2 - 1
    if maxValue == minValue then
        basePosition = 0
    end
    if basePosition < -1 then
        basePosition = -1
    end
    if basePosition > 1 then
        basePosition = 1
    end

    avgValue = math.floor((minValue + maxValue) / 2)
    for i = 1, numbers.Count do
        local num = numbers[CSIndex(i)]
        if maxValue == minValue then
            if base == num then
                result[i] = 0
            else
                result[i] = base > num and -1 or 1
            end
        else
            local interpolatedValue
            local denominator = 0
            if num > base then
                denominator = maxValue - base
                if denominator == 0 then
                    interpolatedValue = 1
                else
                    interpolatedValue = (num - base) / (maxValue - base)
                end
            else
                denominator = base - minValue
                if denominator == 0 then
                    interpolatedValue = (num == base) and 0 or -1
                else
                    interpolatedValue = ((num - minValue) / (base - minValue)) - 1
                end

            end
            result[i] = interpolatedValue
        end
    end

    return result, basePosition, avgValue
end

ShopTradeItemCtrl._GetItemBuyItems = HL.Method().Return(HL.Table) << function(self)
    local goodsTableData = Tables.shopGoodsTable:GetValue(self.m_args.goodsData.goodsTemplateId)
    local allDisplayItems = UIUtils.getRewardItems(goodsTableData.rewardId)
    local items = {}

    for i = 1, #allDisplayItems do
        local displayItem = allDisplayItems[i]
        local itemId = displayItem.id
        local itemData = Tables.itemTable[itemId]
        local totalCount = displayItem.count * self.view.numberSelector.curNumber
        if itemData.maxStackCount <= 1 and Utils.isItemInstType(itemId) then
            for i = 1, totalCount do
                local item = {
                    id = displayItem.id,
                    count = 1,
                    type = itemData.type,
                }
                table.insert(items, item)
            end
        else
            local item = {
                id = displayItem.id,
                count = totalCount,
            }
            table.insert(items, item)
        end
    end
    return items
end

ShopTradeItemCtrl._OnBuyItemSucc = HL.Method(HL.Any) << function(self, arg)
    local items = self:_GetItemBuyItems()
    Notify(MessageConst.SHOW_SYSTEM_REWARDS, {
        title = Language.LUA_BUY_ITEM_SUCC_TITLE,
        icon = "icon_common_rewards",
        items = items,
        onComplete = function()
            Notify(MessageConst.AFTER_ON_BUY_ITEM_SUCC)
        end,
    })
    self:Close()
end

ShopTradeItemCtrl._OnSellItemSucc = HL.Method(HL.Any) << function(self, arg)
    self.m_waitSellRsp = false
    local sellCost = math.floor(self.view.numberSelector.curNumber * self.m_hisPrice[CSIndex(1)])
    local items = {}
    local item = {
        id = self.m_moneyId,
        count = sellCost,
    }
    table.insert(items, item)
    Notify(MessageConst.SHOW_SYSTEM_REWARDS, {
        title = Language.LUA_SELL_ITEM_SUCC_TITLE,
        icon = "icon_common_rewards",
        items = items,
        onComplete = function()
            Notify(MessageConst.AFTER_ON_BUY_ITEM_SUCC)
        end,
    })
    self:Close()
end

ShopTradeItemCtrl.GetCurPhaseStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local goodsData = self.m_args and self.m_args.goodsData or nil
    if not goodsData then
        return nil
    end

    local latestGoodsData
    if goodsData.roleId then
        latestGoodsData = self.m_shopSystem:GetFriendGoodsData(goodsData.goodsId, goodsData.roleId)
    else
        latestGoodsData = self.m_shopSystem:GetShopGoodsData(goodsData.shopId, goodsData.goodsId)
    end
    if latestGoodsData == nil then
        latestGoodsData = goodsData
    end
    local tradeState = self.m_curState == PANEL_STATE.FriendPrice and self.m_lastState or self.m_curState
    if tradeState ~= PANEL_STATE.Sell then
        tradeState = PANEL_STATE.Purchase
    end
    return {
        goodsData = latestGoodsData,
        isSellOnly = self.m_args.isSellOnly,
        isDefaultSell = self.m_args.isDefaultSell,
        resumeState = {
            tradeState = tradeState,
            selectedCount = self.view.numberSelector.curNumber,
            isFriendPriceShown = self.m_curState == PANEL_STATE.FriendPrice,
        }
    }
end




HL.Commit(ShopTradeItemCtrl)
