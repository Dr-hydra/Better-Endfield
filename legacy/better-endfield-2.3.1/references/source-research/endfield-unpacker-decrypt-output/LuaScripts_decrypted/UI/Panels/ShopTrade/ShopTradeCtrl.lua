local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ShopTrade
local PHASE_ID = PhaseId.ShopTrade

local shopSystem = GameInstance.player.shopSystem

ShopTradeCtrl = HL.Class('ShopTradeCtrl', uiCtrl.UICtrl)






ShopTradeCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_BUY_ITEM_SUCC] = '_OnBuyItemSuccess',
    [MessageConst.ON_SELL_ITEM_SUCC] = '_OnSellItemSuccess',
    [MessageConst.ON_SHOP_REFRESH] = '_OnShopRefresh',
    [MessageConst.ON_FRIEND_SHOP_INFO_SYNC] = '_OnFriendShopRefresh',
    [MessageConst.ON_SCREEN_SIZE_CHANGED] = '_OnScreenSizeChanged',
}


ShopTradeCtrl.m_isLocalShop = HL.Field(HL.Boolean) << false

ShopTradeCtrl.m_showFriendRecord = HL.Field(HL.Boolean) << false

ShopTradeCtrl.m_goodsTagCellCache = HL.Field(HL.Forward("UIListCache"))

ShopTradeCtrl.m_getGoodsGroupCellFunc = HL.Field(HL.Function)

ShopTradeCtrl.m_getGoodsGroupTitleFunc = HL.Field(HL.Function)

ShopTradeCtrl.m_goodsGroupSortFunc = HL.Field(HL.Function)

ShopTradeCtrl.m_goodsSortFunc = HL.Field(HL.Function)

ShopTradeCtrl.m_soldOutGoodsSortFunc = HL.Field(HL.Function)

ShopTradeCtrl.m_goodsFriendSortFunc = HL.Field(HL.Function)

ShopTradeCtrl.m_myPositionGoodsSortFunc = HL.Field(HL.Function)

ShopTradeCtrl.m_bindIdPreTab = HL.Field(HL.Number) << 0

ShopTradeCtrl.m_bindIdNextTab = HL.Field(HL.Number) << 0

ShopTradeCtrl.m_waitAutoScrollTagListTime = HL.Field(HL.Number) << -1

ShopTradeCtrl.m_getCellSizeHelperInfo = HL.Field(HL.Table)



ShopTradeCtrl.m_domainId = HL.Field(HL.String) << ""

ShopTradeCtrl.m_onCloseCallBack = HL.Field(HL.Function)

ShopTradeCtrl.m_domainInfo = HL.Field(HL.Table)

ShopTradeCtrl.m_localShopInfo = HL.Field(HL.Table)

ShopTradeCtrl.m_isSelectCommonShop = HL.Field(HL.Boolean) << true

ShopTradeCtrl.m_curSelectTagIndex = HL.Field(HL.Number) << 0

ShopTradeCtrl.m_commonShopRefreshGoodsSearchMap = HL.Field(HL.Table)

ShopTradeCtrl.m_targetLocalJumpArg = HL.Field(HL.Table)

ShopTradeCtrl._updateLimitCountTimeKey = HL.Field(HL.Number) << 0

ShopTradeCtrl._nextUpdateLimitCountTime = HL.Field(HL.Number) << 0



ShopTradeCtrl.m_friendRoleId = HL.Field(HL.Number) << 0

ShopTradeCtrl.m_friendShopInfoList = HL.Field(HL.Table)

ShopTradeCtrl.m_friendTabCellCache = HL.Field(HL.Forward("UIListCache"))

ShopTradeCtrl.m_curSelectFriendShopIndex = HL.Field(HL.Number) << 0

ShopTradeCtrl.m_nextRefreshTs = HL.Field(HL.Number) << 0



ShopTradeCtrl.m_getBulkSellGoodsCellFunc = HL.Field(HL.Function)

ShopTradeCtrl.m_onClickBulkSellGoods = HL.Field(HL.Function)

ShopTradeCtrl.m_onSelectorNumberChanged = HL.Field(HL.Function)

ShopTradeCtrl.m_bulkSellInfo = HL.Field(HL.Table)

ShopTradeCtrl.m_waitBulkSellResp = HL.Field(HL.Boolean) << false


ShopTradeCtrl.m_resumeArg = HL.Field(HL.Table)

ShopTradeCtrl.m_resumeOpenPanel = HL.Field(HL.Table)




ShopTradeCtrl.OpenDomainFriendShop = HL.StaticMethod(HL.Any) << function(arg)
    if arg == nil then
        return
    end
    local roleId = unpack(arg)
    if roleId == nil then
        return
    end
    DomainShopUtils.openDomainFriendShop(roleId)
end

ShopTradeCtrl.OpenDomainShop = HL.StaticMethod(HL.Table) << function(args)
    local domainId, shopId, goodsId = unpack(args)
    PhaseManager:OpenPhase(PHASE_ID, {
        domainId = domainId,
        showFriendRecord = true,
        shopId = shopId == nil and "" or shopId,
        goodsId = goodsId == nil and "" or goodsId,
    })
end




ShopTradeCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:_InitUI()
    self:_InitData(arg)
    self:_UpdateData()
    if arg and type(arg) == "table" then
        self.m_resumeArg = arg.resumeState
        self.m_resumeOpenPanel = arg.resumeOpenPanel
    end
    self:_RefreshAllUI()

    self:_StartUpdate(function(deltaTime)

        local curTs = DateTimeUtils.GetCurrentTimestampBySeconds()
        if curTs >= self.m_nextRefreshTs then
            self.m_nextRefreshTs = Utils.getNextCommonServerRefreshTime()
            if not self.m_isLocalShop then

                GameInstance.player.shopSystem:SendQueryFriendShop(self.m_friendRoleId, DomainShopUtils.getAllLocalUnlockRandomShopIds())
                UIManager:Close(PanelId.ShopTradeItem)
            else


                local isOpen, ctrl = UIManager:IsOpen(PanelId.ShopDetail)
                if isOpen then
                    ctrl:TryClose()
                end
            end
        end

        if DeviceInfo.usingController then
            return
        end
        if self.m_waitAutoScrollTagListTime < 0 then
            return
        end
        if self.m_waitAutoScrollTagListTime >= 1 then
            local showRange = self.view.goodsNode.goodsGroupList:GetGroupRangeInView()
            if showRange.x < 0 then
                self.m_waitAutoScrollTagListTime = -1
                return
            end
            local csIndex = math.floor((showRange.x + showRange.y) / 2)
            local newIndex = LuaIndex(csIndex)
            if self.m_curSelectTagIndex ~= newIndex then
                self:_OnChangeSelectTagUI(newIndex)
            end
            self.m_waitAutoScrollTagListTime = -1
        else
            self.m_waitAutoScrollTagListTime = self.m_waitAutoScrollTagListTime + deltaTime
        end
    end)

    self.m_nextRefreshTs = Utils.getNextCommonServerRefreshTime()
    if self.m_targetLocalJumpArg.locateInfo then
        self:_TryOpenTargetLocalGoodsPopup()
    end
    if self.m_resumeOpenPanel then
        for _, panelInfo in ipairs(self.m_resumeOpenPanel) do
            self:_RestorePopupByResumeState(panelInfo)
        end
    end
    self.m_resumeArg = nil
    self.m_resumeOpenPanel = nil
end

ShopTradeCtrl.OnClose = HL.Override() << function(self)
    local succ, level = GameUtil.SpaceshipUtils.TryGetSpaceshipLevel()
    if succ then
        level:TriggerDomainSellStatus()
    end
    shopSystem:SetGoodsIdSee()

    self._updateLimitCountTimeKey = LuaUpdate:Remove(self._updateLimitCountTimeKey)
    UIManager:ToggleBlockObtainWaysJump("VISIT_SPACESHIP", false)
end

ShopTradeCtrl.OnAnimationInFinished = HL.Override() << function(self)
    self:_TryNaviToTargetLocalGoods()
end

ShopTradeCtrl.OnShow = HL.Override() << function(self)
    self:_NotifyOpenDomainShopCondition()
    self:_NotifyClientCondition()
end

ShopTradeCtrl.OnPhaseRefresh = HL.Override(HL.Opt(HL.Any)) << function(self, arg)
    self:_NotifyOpenDomainShopCondition()
end



ShopTradeCtrl._InitData = HL.Method(HL.Any) << function(self, arg)
    self.m_domainId = arg.domainId or ""
    self.m_onCloseCallBack = arg.onCloseCB
    self.m_friendRoleId = arg.friendRoleId or 0
    self.m_showFriendRecord = (arg.showFriendRecord == true) or Utils.isInSpaceShip()
    self.m_isLocalShop = not string.isEmpty(self.m_domainId)

    local targetJumpShopId = arg.shopId == nil and "" or arg.shopId
    if string.isEmpty(targetJumpShopId) then
        self.m_targetLocalJumpArg = {}
    else
        self.m_targetLocalJumpArg = {
            shopId = targetJumpShopId,
            goodsId = arg.goodsId == nil and "" or arg.goodsId,
            itemId = arg.itemId == nil and "" or arg.itemId,
            locateInfo = nil,
        }
        if not string.isEmpty(self.m_targetLocalJumpArg.goodsId) then
            self.m_targetLocalJumpArg.itemId = ""
        end
    end


    if self.m_isLocalShop then

        local _, domainCfg = Tables.domainDataTable:TryGetValue(self.m_domainId)
        local moneyId = domainCfg.domainGoldItemId
        local moneyItemCfg = Utils.tryGetTableCfg(Tables.itemTable, moneyId)
        self.m_domainInfo = {
            moneyId = moneyId,
            moneyIcon = moneyItemCfg and moneyItemCfg.iconId or "",
            shopGroupId = domainCfg.domainShopGroupId,
        }

        local shopGroupId = self.m_domainInfo.shopGroupId
        local _, shopGroupCfg = Tables.shopGroupTable:TryGetValue(shopGroupId)
        local _, domainShopGroupCfg = Tables.shopGroupDomainTable:TryGetValue(shopGroupId)
        local commonShopId, randomShopId
        for _, shopId in pairs(shopGroupCfg.shopIds) do
            local _, shopCfg = Tables.shopTable:TryGetValue(shopId)
            if shopCfg.shopRefreshType == GEnums.ShopRefreshType.None then
                commonShopId = shopId
            elseif shopCfg.shopRefreshType == GEnums.ShopRefreshType.RefreshRandom then
                randomShopId = shopId
            end
        end

        self.m_localShopInfo = {
            commonShopInfo = {
                shopId = commonShopId,
            },
            randomShopInfo = {
                shopId = randomShopId,
            },
            myPositionGoodsGroup = ShopTradeCtrl._CreateGoodsGroup(Tables.shopDomainConst.goodsTagIdMyPosition),
            soldOutGoodsGroup = ShopTradeCtrl._CreateGoodsGroup(Tables.shopDomainConst.goodsTagIdSoldOut),

            bgImg = domainShopGroupCfg.bgImg,
            icon = shopGroupCfg.icon,
        }
    else

    end

    self.m_onClickBulkSellGoods = function(luaIndex)
        self:_OnSelectBulkSellGoods(luaIndex)
    end
    self.m_onSelectorNumberChanged = function(curNumber, isChangeByBtn)
        self:_OnChangeBulkSellSelectCount(lume.round(curNumber), not isChangeByBtn)
    end

    self.m_goodsGroupSortFunc = Utils.genSortFunction({ "sortId" }, true)






    self.m_goodsSortFunc = Utils.genSortFunction({ "remainLimitSort", "sortId", "raritySort", "priceRatioSort", "goodsId" }, true)







    self.m_soldOutGoodsSortFunc = Utils.genSortFunction({ "refreshTypeSort", "tagSort", "sortId", "raritySort", "priceRatioSort", "goodsId" }, true)






    self.m_goodsFriendSortFunc = Utils.genSortFunction({ "hasPosition", "sortId", "raritySort", "priceRatioSort", "goodsId" }, true)





    self.m_myPositionGoodsSortFunc = Utils.genSortFunction({ "sortId", "raritySort", "profitRatioSort", "goodsId" }, true)

    if GameInstance.player.spaceship.isViewingFriend then
        UIManager:ToggleBlockObtainWaysJump("VISIT_SPACESHIP", true)
    end
end

ShopTradeCtrl._UpdateData = HL.Method() << function(self)
    self.m_commonShopRefreshGoodsSearchMap = {}
    if self.m_isLocalShop then

        self:_UpdateCommonShopData()
        self:_UpdateRandomShopData()
    else


        local shopData = shopSystem:GetFriendShopData(self.m_friendRoleId)
        self.m_friendShopInfoList = {}
        for shopId, goodsMap in cs_pairs(shopData.shopGoodsDic) do
            local domainId = DomainShopUtils.getDomainIdByDomainShopId(shopId)
            local hasCfg, domainCfg = Tables.domainDataTable:TryGetValue(domainId)
            local hasData = GameInstance.player.domainDevelopmentSystem.domainDevDataDic:TryGetValue(domainId)
            if hasCfg and hasData then
                local moneyId = domainCfg.domainGoldItemId
                local moneyItemCfg = Utils.tryGetTableCfg(Tables.itemTable, moneyId)

                local shopInfo = {
                    shopId = shopId,
                    domainId = domainId,
                    domainName = domainCfg.domainName,
                    domainIcon = domainCfg.domainIcon,
                    moneyId = domainCfg.domainGoldItemId,
                    moneyIcon = moneyItemCfg and moneyItemCfg.iconId or "",
                    myPositionGoodsGroup = ShopTradeCtrl._CreateGoodsGroup(Tables.shopDomainConst.goodsTagIdMyPosition),

                    sortId = domainCfg.sortId,
                }
                self:_UpdateFriendShopData(shopInfo, goodsMap)
                table.insert(self.m_friendShopInfoList, shopInfo)
            end
        end
        table.sort(self.m_friendShopInfoList, function(a, b)
            return a.sortId < b.sortId
        end)
    end
end

ShopTradeCtrl._CreateGoodsGroup = HL.StaticMethod(HL.String).Return(HL.Table) << function(tagId)
    local _, tagCfg = Tables.shopGoodsTagTable:TryGetValue(tagId)
    local goodsGroup = {
        tagId = tagId,
        tagName = tagCfg.tagName,
        tagIcon = tagCfg.tagIcon,
        goodsList = {},

        sortId = tagCfg.sortId,
    }
    return goodsGroup
end

ShopTradeCtrl._TryGetLocalShopTabByShopId = HL.Method(HL.String).Return(HL.Boolean, HL.Boolean) << function(self, shopId)
    if not self.m_isLocalShop or string.isEmpty(shopId) or self.m_localShopInfo == nil then
        return false, true
    end
    if shopId == self.m_localShopInfo.commonShopInfo.shopId then
        return true, true
    end
    local hasRandomTab = #self.m_localShopInfo.randomShopInfo.goodsGroupList > 0
    if hasRandomTab and shopId == self.m_localShopInfo.randomShopInfo.shopId then
        return true, false
    end
    return false, true
end

ShopTradeCtrl._ResolveLocalShopLocateArg = HL.Method().Return(HL.Opt(HL.Table)) << function(self)
    local jumpArg = self.m_targetLocalJumpArg
    jumpArg.locateInfo = nil
    if not self.m_isLocalShop or string.isEmpty(jumpArg.shopId) then
        return nil
    end
    local hasShop, isCommonShop = self:_TryGetLocalShopTabByShopId(jumpArg.shopId)
    if not hasShop then
        self.m_targetLocalJumpArg = {}
        return nil
    end
    local goodsGroupList = isCommonShop
        and self.m_localShopInfo.commonShopInfo.goodsGroupList
        or self.m_localShopInfo.randomShopInfo.goodsGroupList
    local groupOffset = isCommonShop and 0 or 1

    if not string.isEmpty(jumpArg.goodsId) then
        for groupIndex, goodsGroup in ipairs(goodsGroupList) do
            for goodsIndex, goodsInfo in ipairs(goodsGroup.goodsList) do
                if goodsInfo.goodsId == jumpArg.goodsId then
                    jumpArg.locateInfo = {
                        goodsInfo = goodsInfo,
                        isCommonShop = isCommonShop,
                        groupIndex = groupIndex + groupOffset,
                        goodsIndex = goodsIndex,
                    }
                    break
                end
            end
            if jumpArg.locateInfo ~= nil then
                break
            end
        end
        if jumpArg.locateInfo == nil then
            jumpArg.goodsId = ""
        end
    elseif not string.isEmpty(jumpArg.itemId) then
        local soldOutLocateInfo
        for groupIndex, goodsGroup in ipairs(goodsGroupList) do
            for goodsIndex, goodsInfo in ipairs(goodsGroup.goodsList) do
                if goodsInfo.itemId == jumpArg.itemId and
                    (goodsInfo.remainLimitCount > 0 or soldOutLocateInfo == nil)
                then
                    local locateInfo = {
                        goodsInfo = goodsInfo,
                        isCommonShop = isCommonShop,
                        groupIndex = groupIndex + groupOffset,
                        goodsIndex = goodsIndex,
                    }
                    if goodsInfo.remainLimitCount > 0 then
                        jumpArg.locateInfo = locateInfo
                        break
                    elseif soldOutLocateInfo == nil then
                        soldOutLocateInfo = locateInfo
                    end
                end
            end
            if jumpArg.locateInfo ~= nil then
                break
            end
        end
        if jumpArg.locateInfo == nil then
            jumpArg.locateInfo = soldOutLocateInfo
        end
        if jumpArg.locateInfo == nil then
            jumpArg.itemId = ""
        end
    end
    if jumpArg.locateInfo == nil then
        self.m_targetLocalJumpArg = {}
    end
    return {
        isCommonShop = isCommonShop,
    }
end


ShopTradeCtrl._UpdateCommonShopData = HL.Method() << function(self)
    local shopInfo = self.m_localShopInfo.commonShopInfo
    local shopId = shopInfo.shopId
    shopInfo.goodsGroupDic = {}
    shopInfo.goodsGroupList = {}
    local soldOutGoodsGroup = self.m_localShopInfo.soldOutGoodsGroup
    soldOutGoodsGroup.goodsList = {}
    local shopData = shopSystem:GetShopData(shopId)
    if not shopSystem:CheckShopUnlocked(shopId) then
        return
    end

    for _, goodsData in cs_pairs(shopData.goodList) do
        local goodsTplId = goodsData.goodsTemplateId
        local goodsId = goodsData.goodsId
        local _, goodsCfg = Tables.shopGoodsTable:TryGetValue(goodsTplId)
        local tagId = goodsCfg.goodsTagId
        local unlock = shopSystem:CheckGoodsUnlocked(goodsId)
        if unlock or goodsCfg.isShowWhenLock then

            local goodsGroup = shopInfo.goodsGroupDic[tagId]
            if goodsGroup == nil then
                goodsGroup = ShopTradeCtrl._CreateGoodsGroup(tagId)
                shopInfo.goodsGroupDic[tagId] = goodsGroup
            end

            local itemBundle = UIUtils.getRewardFirstItem(goodsCfg.rewardId)
            local itemId = itemBundle.id
            local hasCfg, itemCfg = Tables.itemTable:TryGetValue(itemId)
            if not hasCfg then
                logger.error("itemTable表数据缺失，itemId为：", itemId)
            else
                local remainLimitCount = shopSystem:GetRemainCountByGoodsId(shopId, goodsTplId)
                local discount = 1 - goodsData.discount
                local refreshType = goodsCfg.limitCountRefreshType
                local isSoldOut = remainLimitCount == 0
                local goodsInfo = {

                    shopId = shopId,
                    goodsId = goodsId,

                    originPrice = goodsCfg.price,
                    discount = discount,
                    curPrice = CashShopUtils.GetDisplayPrice(goodsCfg.price, goodsData.discount),
                    remainLimitCount = remainLimitCount,
                    refreshType = refreshType,

                    itemId = itemId,
                    itemName = itemCfg.name,
                    itemCount = Utils.getItemCount(itemId, true, true),
                    itemBundleCount = itemBundle.count,
                    itemIcon = itemCfg.iconId,
                    itemRarity = itemCfg.rarity,
                    moneyId = self.m_domainInfo.moneyId,
                    moneyIcon = self.m_domainInfo.moneyIcon,

                    refreshTypeSort = refreshType == GEnums.ShopFrequencyLimitType.Forever and 999 or refreshType:GetHashCode(),
                    remainLimitSort = remainLimitCount == 0 and 1 or 0,
                    raritySort = -itemCfg.rarity,
                    priceRatioSort = -discount,
                    sortId = goodsCfg.sortId,
                }
                if isSoldOut then
                    goodsInfo.tagSort = goodsGroup.sortId
                    table.insert(soldOutGoodsGroup.goodsList, goodsInfo)
                else
                    table.insert(goodsGroup.goodsList, goodsInfo)
                end
            end
        end
    end

    for _, goodsGroup in pairs(shopInfo.goodsGroupDic) do
        if #goodsGroup.goodsList > 0 then
            table.insert(shopInfo.goodsGroupList, goodsGroup)
        end
    end
    table.sort(shopInfo.goodsGroupList, self.m_goodsGroupSortFunc)

    local groupIndex = 1
    for _, goodsGroup in pairs(shopInfo.goodsGroupList) do
        table.sort(goodsGroup.goodsList, self.m_goodsSortFunc)
        local goodsIndex = 1
        for _, goodsInfo in pairs(goodsGroup.goodsList) do
            self.m_commonShopRefreshGoodsSearchMap[goodsInfo.goodsId] = {
                goodsInfo = goodsInfo,
                groupIndex = groupIndex,
                goodsIndex = goodsIndex,
            }
            goodsIndex = goodsIndex + 1
        end
        groupIndex = groupIndex + 1
    end

    if #soldOutGoodsGroup.goodsList > 0 then
        table.insert(shopInfo.goodsGroupList, soldOutGoodsGroup)
        table.sort(soldOutGoodsGroup.goodsList, self.m_soldOutGoodsSortFunc)
    end
end



ShopTradeCtrl._UpdateRandomShopData = HL.Method() << function(self)
    local shopInfo = self.m_localShopInfo.randomShopInfo
    local shopId = shopInfo.shopId
    local myPositionGroup = self.m_localShopInfo.myPositionGoodsGroup
    myPositionGroup.totalPrice = 0
    myPositionGroup.totalTodayPrice = 0
    myPositionGroup.totalProfit = 0
    myPositionGroup.totalProfitRatio = 0
    shopInfo.goodsGroupDic = {}
    shopInfo.goodsGroupList = {}
    myPositionGroup.goodsList = {}
    if not shopSystem:CheckShopUnlocked(shopId) then
        return
    end

    local shopData = shopSystem:GetShopData(shopId)
    shopInfo.remainLimitCount = shopSystem:GetRemainLimitCountByShopId(shopId)


    for _, goodsData in cs_pairs(shopData.goodList) do
        local goodsTplId = goodsData.goodsTemplateId
        local goodsId = goodsData.goodsId
        local _, goodsCfg = Tables.shopGoodsTable:TryGetValue(goodsTplId)
        local unlock = shopSystem:CheckGoodsUnlocked(goodsId)
        if unlock or goodsCfg.isShowWhenLock then
            local tagId = goodsCfg.goodsTagId

            local goodsGroup = shopInfo.goodsGroupDic[tagId]
            if goodsGroup == nil then
                goodsGroup = ShopTradeCtrl._CreateGoodsGroup(tagId)
                shopInfo.goodsGroupDic[tagId] = goodsGroup
                table.insert(shopInfo.goodsGroupList, goodsGroup)
            end


            local itemBundle = UIUtils.getRewardFirstItem(goodsCfg.rewardId)
            local itemId = itemBundle.id
            local _, itemCfg = Tables.itemTable:TryGetValue(itemId)


            local randomGoodsData = goodsData.domainRandomGoodsData
            local originPrice = goodsCfg.randomGoodsStandardPrice
            local todayPrice = randomGoodsData.historyPrice[0]
            local avgPrice = randomGoodsData.avgPrice
            local profit = todayPrice - avgPrice
            local priceDiffRatio = lume.round((todayPrice - originPrice) * 100 / originPrice, 0.1)
            local remainLimitCount = shopSystem:GetRemainCountByGoodsId(shopId, goodsTplId)
            local profitRatio = avgPrice == 0 and 0 or lume.round(profit * 100 / avgPrice, 0.1)

            local goodsInfo = {

                shopId = shopId,
                goodsId = goodsId,

                originPrice = originPrice,
                todayPrice = todayPrice,
                priceDiffRatio = priceDiffRatio,
                remainLimitCount = remainLimitCount,
                refreshType = goodsCfg.limitCountRefreshType,

                itemId = itemId,
                itemName = itemCfg.name,
                itemCount = randomGoodsData.quantity,
                itemBundleCount = itemBundle.count,
                itemIcon = itemCfg.iconId,
                itemRarity = itemCfg.rarity,
                moneyId = self.m_domainInfo.moneyId,
                moneyIcon = self.m_domainInfo.moneyIcon,

                positionAvgPrice = avgPrice,
                profit = profit,
                profitRatio = profitRatio,

                remainLimitSort = remainLimitCount == 0 and 1 or 0,
                raritySort = -itemCfg.rarity,
                priceRatioSort = -priceDiffRatio,
                profitRatioSort = -profitRatio,
                sortId = goodsCfg.sortId,
            }
            table.insert(goodsGroup.goodsList, goodsInfo)

            if goodsInfo.itemCount > 0 then
                table.insert(myPositionGroup.goodsList, goodsInfo)
                myPositionGroup.totalPrice = myPositionGroup.totalPrice + goodsInfo.positionAvgPrice * goodsInfo.itemCount
                myPositionGroup.totalTodayPrice = myPositionGroup.totalTodayPrice + goodsInfo.todayPrice * goodsInfo.itemCount
            end
        end
    end
    myPositionGroup.totalProfit = myPositionGroup.totalTodayPrice - myPositionGroup.totalPrice
    myPositionGroup.totalProfitRatio = myPositionGroup.totalProfit == 0 and 0 or
        lume.round(myPositionGroup.totalProfit * 100 / myPositionGroup.totalPrice, 0.1)

    table.sort(shopInfo.goodsGroupList, self.m_goodsGroupSortFunc)

    for _, goodsGroup in pairs(shopInfo.goodsGroupList) do
        table.sort(goodsGroup.goodsList, self.m_goodsSortFunc)
    end

    table.sort(myPositionGroup.goodsList, self.m_myPositionGoodsSortFunc)
end



ShopTradeCtrl._UpdateFriendShopData = HL.Method(HL.Table, HL.Any) << function(self, shopInfo, goodsMap)
    local shopId = shopInfo.shopId
    local myPositionGroup = shopInfo.myPositionGoodsGroup
    shopInfo.goodsGroupDic = {}
    shopInfo.goodsGroupList = {}
    myPositionGroup.goodsList = {}
    myPositionGroup.totalPrice = 0
    myPositionGroup.totalTodayPrice = 0
    myPositionGroup.totalProfit = 0
    myPositionGroup.totalProfitRatio = 0

    for goodsId, friendGoodsData in cs_pairs(goodsMap) do
        local unlock = shopSystem:CheckGoodsUnlocked(goodsId)
        if unlock then
            local goodsData = shopSystem:GetShopGoodsData(shopId, goodsId)
            local randomGoodsData = goodsData.domainRandomGoodsData
            local goodsTplId = friendGoodsData.goodsTemplateId
            local _, goodsCfg = Tables.shopGoodsTable:TryGetValue(goodsTplId)

            local tagId = goodsCfg.goodsTagId
            local goodsGroup = shopInfo.goodsGroupDic[tagId]
            if goodsGroup == nil then
                goodsGroup = ShopTradeCtrl._CreateGoodsGroup(tagId)
                shopInfo.goodsGroupDic[tagId] = goodsGroup
                table.insert(shopInfo.goodsGroupList, goodsGroup)
            end


            local itemBundle = UIUtils.getRewardFirstItem(goodsCfg.rewardId)
            local itemId = itemBundle.id
            local _, itemCfg = Tables.itemTable:TryGetValue(itemId)

            local originPrice = goodsCfg.randomGoodsStandardPrice
            local todayPrice = friendGoodsData.historyPrice[0]
            local avgPrice = randomGoodsData.avgPrice
            local profit = todayPrice - avgPrice
            local priceDiffRatio = lume.round((todayPrice - originPrice) * 100 / originPrice, 0.1)
            local profitRatio = avgPrice == 0 and 0 or lume.round(profit * 100 / avgPrice, 0.1)
            local goodsInfo = {

                shopId = shopId,
                goodsId = goodsId,
                friendGoodsData = friendGoodsData,

                originPrice = originPrice,
                todayPrice = todayPrice,
                priceDiffRatio = priceDiffRatio,

                itemId = itemId,
                itemName = itemCfg.name,
                itemCount = randomGoodsData.quantity,
                itemBundleCount = itemBundle.count,
                itemIcon = itemCfg.iconId,
                itemRarity = itemCfg.rarity,
                moneyId = shopInfo.moneyId,
                moneyIcon = shopInfo.moneyIcon,

                positionAvgPrice = avgPrice,
                profit = profit,
                profitRatio = profitRatio,

                hasPosition = randomGoodsData.quantity > 0 and 0 or 1,
                raritySort = -itemCfg.rarity,
                priceRatioSort = -priceDiffRatio,
                profitRatioSort = -profitRatio,
                sortId = goodsCfg.sortId,
            }
            table.insert(goodsGroup.goodsList, goodsInfo)

            if goodsInfo.itemCount > 0 then
                table.insert(myPositionGroup.goodsList, goodsInfo)
                myPositionGroup.totalPrice = myPositionGroup.totalPrice + goodsInfo.positionAvgPrice * goodsInfo.itemCount
                myPositionGroup.totalTodayPrice = myPositionGroup.totalTodayPrice + goodsInfo.todayPrice * goodsInfo.itemCount
            end
        end
    end
    myPositionGroup.totalProfit = myPositionGroup.totalTodayPrice - myPositionGroup.totalPrice
    myPositionGroup.totalProfitRatio = myPositionGroup.totalProfit == 0 and 0 or
        lume.round(myPositionGroup.totalProfit * 100 / myPositionGroup.totalPrice, 0.1)

    table.sort(shopInfo.goodsGroupList, self.m_goodsGroupSortFunc)

    for _, goodsGroup in pairs(shopInfo.goodsGroupList) do
        table.sort(goodsGroup.goodsList, self.m_goodsFriendSortFunc)
    end

    table.sort(myPositionGroup.goodsList, self.m_myPositionGoodsSortFunc)
end



ShopTradeCtrl._UpdateBulkSellData = HL.Method() << function(self)
    local myPositionGroup
    local moneyId
    local moneyIcon
    if self.m_isLocalShop then

        myPositionGroup = self.m_localShopInfo.myPositionGoodsGroup
        moneyId = self.m_domainInfo.moneyId
        moneyIcon = self.m_domainInfo.moneyIcon
    else

        local shopInfo = self.m_friendShopInfoList[self.m_curSelectFriendShopIndex]
        myPositionGroup = shopInfo.myPositionGoodsGroup
        moneyId = shopInfo.moneyId
        moneyIcon = shopInfo.moneyIcon
    end
    self.m_bulkSellInfo = {
        goodsList = myPositionGroup.goodsList,
        profitInfo = {
            moneyId = moneyId,
            moneyIcon = moneyIcon,
            moneyCount = myPositionGroup.totalPrice,
            profit = myPositionGroup.totalProfit,
            profitRatio = myPositionGroup.totalProfitRatio,
        },
        selectCountList = {},
        totalReward = 0,
        curFocusGoodsIndex = 0,
    }
end

ShopTradeCtrl._CollectBulkSellResumeState = HL.Method().Return(HL.Opt(HL.Table)) << function(self)
    if not self.view.bulkSellNode.gameObject.activeSelf or self.m_bulkSellInfo == nil then
        return nil
    end
    local bulkSellState = {
        selectedGoodsList = {},
    }

    for index, goodsInfo in ipairs(self.m_bulkSellInfo.goodsList) do
        local selectCount = self.m_bulkSellInfo.selectCountList[index]
        if goodsInfo and selectCount and selectCount > 0 then
            table.insert(bulkSellState.selectedGoodsList, {
                goodsId = goodsInfo.goodsId,
                count = selectCount,
            })
        end
    end
    local curFocusGoodsInfo = self.m_bulkSellInfo.goodsList[self.m_bulkSellInfo.curFocusGoodsIndex]
    bulkSellState.curFocusGoodsId = curFocusGoodsInfo and curFocusGoodsInfo.goodsId or nil
    return bulkSellState
end

ShopTradeCtrl._GetBulkSellGoodsIndexByGoodsId = HL.Method(HL.String).Return(HL.Number) << function(self, goodsId)
    if self.m_bulkSellInfo == nil or string.isEmpty(goodsId) then
        return 0
    end
    for index, goodsInfo in ipairs(self.m_bulkSellInfo.goodsList) do
        if goodsInfo.goodsId == goodsId then
            return index
        end
    end
    return 0
end

ShopTradeCtrl._RestoreBulkSellByResumeState = HL.Method(HL.Opt(HL.Any)) << function(self, bulkSellState)
    if bulkSellState == nil then
        return
    end
    self:_ShowBulkSellNode(true)
    self:_UpdateBulkSellData()
    local bulkInfo = self.m_bulkSellInfo
    if #bulkInfo.goodsList <= 0 then
        self:_ShowBulkSellNode(false)
        return
    end

    for _, selectedGoodsInfo in ipairs(bulkSellState.selectedGoodsList or {}) do
        local goodsIndex = self:_GetBulkSellGoodsIndexByGoodsId(selectedGoodsInfo.goodsId)
        if goodsIndex > 0 then
            local goodsInfo = bulkInfo.goodsList[goodsIndex]
            local selectCount = lume.clamp(selectedGoodsInfo.count or 1, 1, goodsInfo.itemCount)
            bulkInfo.selectCountList[goodsIndex] = selectCount
            bulkInfo.totalReward = bulkInfo.totalReward + goodsInfo.todayPrice * selectCount
        end
    end
    local focusIndex = string.isEmpty(bulkSellState.curFocusGoodsId)
        and 0
        or self:_GetBulkSellGoodsIndexByGoodsId(bulkSellState.curFocusGoodsId)
    if focusIndex > 0 and bulkInfo.selectCountList[focusIndex] == nil then
        focusIndex = 0
    end
    if focusIndex == 0 then

        for index, _ in ipairs(bulkInfo.goodsList) do
            if bulkInfo.selectCountList[index] ~= nil then
                focusIndex = index
                break
            end
        end
    end
    bulkInfo.curFocusGoodsIndex = focusIndex
    self:_RefreshBulkSellUI()
    if focusIndex > 0 then
        self.view.bulkSellNode.goodsList:ScrollToIndex(CSIndex(focusIndex))
        local focusObj = self.view.bulkSellNode.goodsList:Get(CSIndex(focusIndex))
        local focusCell = self.m_getBulkSellGoodsCellFunc(focusObj)
        if focusCell then
            self:SetNaviTarget(focusCell.view.selectBtn)
        end
    end
end





ShopTradeCtrl._InitUI = HL.Method() << function(self)

    self.view.domainTopMoneyTitle.view.closeBtn.onClick:AddListener(function()
        PhaseManager:PopPhase(PHASE_ID)
        if self.m_onCloseCallBack then
            self.m_onCloseCallBack()
            self.m_onCloseCallBack = nil
        end
    end)
    self.view.goodsNode.bulkSellBtn.onClick:AddListener(function()
        self:_ShowBulkSellNode(true)
        self:_UpdateBulkSellData()
        self:_RefreshBulkSellUI()
    end)
    self.m_goodsTagCellCache = UIUtils.genCellCache(self.view.goodsNode.goodsTagCell)
    self.m_getGoodsGroupCellFunc = UIUtils.genCachedCellFunction(self.view.goodsNode.goodsGroupList)
    self.m_getGoodsGroupTitleFunc = UIUtils.genCachedCellFunction(self.view.goodsNode.goodsGroupList, nil, true)
    self.view.goodsNode.goodsGroupList.onUpdateCell:AddListener(function(obj, csIndex)
        local cell = self.m_getGoodsGroupCellFunc(obj)
        self:_OnRefreshGoodsGroupCell(cell, LuaIndex(csIndex))
    end)
    self.view.goodsNode.goodsGroupList.onUpdateGroupTitle:AddListener(function(obj, csIndex)
        local title = self.m_getGoodsGroupTitleFunc(obj)
        self:_OnRefreshGoodsGroupTitle(title, LuaIndex(csIndex))
    end)
    self.view.goodsNode.goodsGroupList.getCellName = function(csIndex)
        local result = self:_GlobalToGroupLocal(LuaIndex(csIndex))
        local groupIndex = result.groupIndex
        local goodsIndex = result.goodsIndex
        local groupInfo = self:_GetGoodsGroupInfo(groupIndex)
        local goodsInfo = groupInfo and groupInfo.goodsList[goodsIndex]
        if goodsInfo then
            return "GoodsCell_" .. groupIndex .. "_" .. goodsIndex
        else
            return "GoodsGroupTitle_" .. groupIndex .. "_" .. goodsIndex
        end
    end
    self.view.goodsNode.goodsGroupList.getCellCountInGroup = function(groupCSIndex)
        return self:_GetGoodsGroupCellCount(LuaIndex(groupCSIndex))
    end
    self.view.goodsNode.goodsGroupList.getGroupTitleSize = function(groupCSIndex)
        return self:_GetGoodsGroupTitleHeight(LuaIndex(groupCSIndex))
    end
    self:m_InitGetCellSizeHelperInfo()

    local preActionId = self.view.tabNode.previousKeyHint.actionId
    local nextActionId = self.view.tabNode.nextKeyHint.actionId
    self.m_bindIdPreTab = UIUtils.bindInputPlayerAction(preActionId, function()
        if self.m_isLocalShop then
            local hasRandomTab = #self.m_localShopInfo.randomShopInfo.goodsGroupList > 0
            if not hasRandomTab then
                return
            end
            self:_OnChangeSelectLocalShop(not self.m_isSelectCommonShop)
            AudioManager.PostEvent("Au_UI_Toggle_Tab_On")
            self:_NotifyClientCondition()
        else
            local count = #self.m_friendShopInfoList
            local newIndex = (self.m_curSelectFriendShopIndex + count - 2) % count + 1
            if newIndex ~= self.m_curSelectFriendShopIndex then
                self:_OnChangeSelectFriendShop(newIndex)
                AudioManager.PostEvent("Au_UI_Toggle_Tab_On")
            end
        end
    end, self.view.goodsNode.inputGroup.groupId)
    self.m_bindIdNextTab = UIUtils.bindInputPlayerAction(nextActionId, function()
        if self.m_isLocalShop then
            local hasRandomTab = #self.m_localShopInfo.randomShopInfo.goodsGroupList > 0
            if not hasRandomTab then
                return
            end
            self:_OnChangeSelectLocalShop(not self.m_isSelectCommonShop)
            AudioManager.PostEvent("Au_UI_Toggle_Tab_On")
            self:_NotifyClientCondition()
        else
            local count = #self.m_friendShopInfoList
            local newIndex = self.m_curSelectFriendShopIndex % count + 1
            if newIndex ~= self.m_curSelectFriendShopIndex then
                self:_OnChangeSelectFriendShop(newIndex)
                AudioManager.PostEvent("Au_UI_Toggle_Tab_On")
            end
        end
    end, self.view.goodsNode.inputGroup.groupId)

    self.view.tabNode.shopCommonTab.selectBtn.onClick:AddListener(function()
        if self.m_isSelectCommonShop == false then
            self:_OnChangeSelectLocalShop(true)
            self:_NotifyClientCondition()
        end
    end)
    self.view.tabNode.shopRandomTab.selectBtn.onClick:AddListener(function()
        if self.m_isSelectCommonShop == true then
            self:_OnChangeSelectLocalShop(false)
            self:_NotifyClientCondition()
        end
    end)

    self.m_friendTabCellCache = UIUtils.genCellCache(self.view.tabNode.shopFriendTabCell)
    self.view.tabNode.friendRecordBtn.onClick:AddListener(function()
        PhaseManager:OpenPhase(PhaseId.SpaceshipSalesRecords)
    end)

    local bulkSellNode = self.view.bulkSellNode
    bulkSellNode.closeBtn.onClick:AddListener(function()
        self:_ShowBulkSellNode(false)
    end)
    bulkSellNode.confirmBtn.onClick:AddListener(function()
        self:_OnConfirmBulkSellGoods()
    end)
    self.m_getBulkSellGoodsCellFunc = UIUtils.genCachedCellFunction(bulkSellNode.goodsList)
    bulkSellNode.goodsList.onUpdateCell:AddListener(function(obj, csIndex)
        local cell = self.m_getBulkSellGoodsCellFunc(obj)
        self:_OnRefreshBulkSellGoodsCell(cell, LuaIndex(csIndex))
    end)
    self:_ShowBulkSellNode(false)

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
    self.view.goodsNode.goodsGroupListNaviGroup.getDefaultSelectableFunc = function()
        return self:_GetGoodsGroupListDefaultSelectable()
    end
    self.view.goodsNode.goodsGroupListNaviGroup.onIsFocusedChange:AddListener(function(isFocused)
        if not isFocused then
            self:_SetNaviTargetToCurGoodsTag()
        end
    end)
    self.view.goodsNode.focusHelperGoodsGroupList.onIsNaviTargetChanged = function(isTarget)
        if isTarget then
            self:_SetNaviTargetToCurGoodsTag()
        end
    end
    self.view.goodsNode.focusHelperGoodsTagList.onIsNaviTargetChanged = function(isTarget)
        if isTarget then
            self.view.goodsNode.goodsGroupListNaviGroup:ManuallyFocus()
        end
    end
end

ShopTradeCtrl._SetNaviTargetToCurGoodsTag = HL.Method() << function(self)
    local tagCell = self.m_goodsTagCellCache:Get(self.m_curSelectTagIndex)
    if tagCell then
        self:SetNaviTarget(tagCell.tagBtn)
    end
end

ShopTradeCtrl._GetGoodsGroupListDefaultSelectable = HL.Method().Return(HL.Opt(HL.Any)) << function(self)
    local range = self.view.goodsNode.goodsGroupList:GetRangeInView()
    if range.x < 0 then
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
    local obj = self.view.goodsNode.goodsGroupList:Get(targetCSIndex)
    local cell = self.m_getGoodsGroupCellFunc(obj)
    if cell then
        return cell.view.selectBtn
    else
        return nil
    end
end

ShopTradeCtrl._TryScrollToTargetLocalGoods = HL.Method().Return(HL.Boolean) << function(self)
    local locateInfo = self.m_targetLocalJumpArg.locateInfo
    if locateInfo == nil or locateInfo.groupIndex <= 0 or locateInfo.goodsIndex <= 0 then
        return false
    end
    if self.m_curSelectTagIndex ~= locateInfo.groupIndex then
        self:_OnChangeSelectTagUI(locateInfo.groupIndex)
    end
    self.view.goodsNode.goodsGroupList:ScrollToIndex(
        CSIndex(locateInfo.groupIndex),
        CSIndex(locateInfo.goodsIndex),
        true,
        CS.Beyond.UI.UIScrollList.ScrollAlignType.Center
    )
    self.m_curSelectTagIndex = locateInfo.groupIndex
    return true
end

ShopTradeCtrl._GetTargetLocalGoodsCell = HL.Method().Return(HL.Opt(HL.Any)) << function(self)
    local locateInfo = self.m_targetLocalJumpArg.locateInfo
    if locateInfo == nil then
        return nil
    end
    local obj = self.view.goodsNode.goodsGroupList:Get(CSIndex(locateInfo.groupIndex), CSIndex(locateInfo.goodsIndex))
    if obj == nil then
        return nil
    end
    return self.m_getGoodsGroupCellFunc(obj)
end

ShopTradeCtrl._TryOpenTargetLocalGoodsPopup = HL.Method() << function(self)
    if not self:_TryScrollToTargetLocalGoods() then
        self.m_targetLocalJumpArg = {}
        return
    end
    local locateInfo = self.m_targetLocalJumpArg.locateInfo
    local goodsInfo = locateInfo.goodsInfo
    if goodsInfo == nil then
        return
    end
    self:_UpdateReadInfo(goodsInfo.goodsId)
    local goodsData = shopSystem:GetShopGoodsData(goodsInfo.shopId, goodsInfo.goodsId)
    if locateInfo.isCommonShop then
        CashShopUtils.OpenShopDetailPanel(goodsData, self)
    else
        UIManager:Open(PanelId.ShopTradeItem, {
            goodsData = goodsData,
            isDefaultSell = locateInfo.groupIndex == 1,
        })
    end
end

ShopTradeCtrl._TryNaviToTargetLocalGoods = HL.Method() << function(self)
    if self.m_targetLocalJumpArg.locateInfo == nil then
        return
    end
    local cell = self:_GetTargetLocalGoodsCell()
    if cell then
        self:SetNaviTarget(cell.view.selectBtn)
    end
    self.m_targetLocalJumpArg = {}
end

ShopTradeCtrl.m_InitGetCellSizeHelperInfo = HL.Method() << function(self)
    local groupTitle = self.view.goodsNode.goodsGroupTitle

    local titlePadding = groupTitle.verticalLayoutGroup.padding
    local titleHeight = groupTitle.shopItemTitle.transform.rect.height + titlePadding.top + titlePadding.bottom
    local titleHeightHasPosition = titleHeight + groupTitle.myPositionDetail.transform.rect.height
    local titleHeightEmptyPosition = titleHeight + groupTitle.myPositionEmpty.transform.rect.height

    self.m_getCellSizeHelperInfo = {
        titleHeight = titleHeight,
        titleHeightHasPosition = titleHeightHasPosition,
        titleHeightEmptyPosition = titleHeightEmptyPosition,
    }
end

ShopTradeCtrl._CollectPopupResumeState = HL.Method().Return(HL.Table) << function(self)
    local resumeOpenPanel = {}
    if PhaseManager:GetTopPhaseId() ~= PHASE_ID then
        return resumeOpenPanel
    end

    local isTradeItemOpen, tradeItemCtrl = UIManager:IsOpen(PanelId.ShopTradeItem)
    if isTradeItemOpen then
        local popupArg = tradeItemCtrl:GetCurPhaseStateArg()
        if popupArg then
            table.insert(resumeOpenPanel, {
                panelId = PanelId.ShopTradeItem,
                arg = popupArg,
            })
        end
        return resumeOpenPanel
    end

    local isDetailOpen, openedDetailCtrl = UIManager:IsOpen(PanelId.ShopDetail)
    if isDetailOpen then
        local popupArg = openedDetailCtrl:GetCurPhaseStateArg()
        if popupArg then
            table.insert(resumeOpenPanel, {
                panelId = PanelId.ShopDetail,
                arg = popupArg,
            })
        end
    end
    return resumeOpenPanel
end

ShopTradeCtrl._RestorePopupByResumeState = HL.Method(HL.Table) << function(self, panelInfo)
    if not panelInfo or panelInfo.panelId == nil then
        return
    end

    if panelInfo.panelId == PanelId.ShopDetail and self.m_phase then
        self.m_phase:CreatePhasePanelItem(panelInfo.panelId, panelInfo.arg)
        return
    end
    UIManager:Open(panelInfo.panelId, panelInfo.arg)
end

ShopTradeCtrl._RefreshAllUI = HL.Method() << function(self)
    local resumeState = self.m_resumeArg
    if self.m_isLocalShop then
        local localShopLocateArg = self:_ResolveLocalShopLocateArg()
        self.view.shopStateCtrl:SetState("LocalShop")
        self:_RefreshLocalShopTabUI()
        local hasRandomTab = #self.m_localShopInfo.randomShopInfo.goodsGroupList > 0
        local isCommonShop
        if localShopLocateArg then
            isCommonShop = localShopLocateArg.isCommonShop
        else
            isCommonShop = resumeState == nil or resumeState.isSelectCommonShop ~= false
        end
        if not hasRandomTab then
            isCommonShop = true
        end

        self:_OnChangeSelectLocalShop(isCommonShop, true)
        local tabNode = self.view.tabNode
        tabNode.titleBgImg:LoadSprite(UIConst.UI_SPRITE_SHOP_TRADE_AREA_BG, self.m_localShopInfo.bgImg)
        if not string.isEmpty(self.m_localShopInfo.icon) and #self.m_localShopInfo.icon > 0 and not self.m_showFriendRecord then
            tabNode.locationIconImg.gameObject:SetActive(true)
            tabNode.locationIconImg:LoadSprite(UIConst.UI_SPRITE_SHOP_TRADE_AREA_ICON, self.m_localShopInfo.icon)
        else
            tabNode.locationIconImg.gameObject:SetActive(false)
        end

        tabNode.friendRecordBtn.gameObject:SetActive(self.m_showFriendRecord)

        self.view.goodsNode.goodsGroupList.onEndDrag:RemoveAllListeners()
        self.view.goodsNode.goodsGroupList.onEndDrag:AddListener(function()
            self.m_waitAutoScrollTagListTime = 0
        end)
        local domainInfo = self.m_domainInfo
        self:_RefreshTitleMoneyUI(self.m_domainId, domainInfo.moneyId)
    else
        self.view.shopStateCtrl:SetState("FriendShop")
        local shopCount = #self.m_friendShopInfoList

        self.m_friendTabCellCache:Refresh(shopCount, function(cell, luaIndex)
            self:_OnUpdateFriendTabCell(cell, luaIndex)
        end)
        self.view.tabNode.previousKeyHintParent.gameObject:SetActive(shopCount > 1)
        self.view.tabNode.nextKeyHintParent.gameObject:SetActive(shopCount > 1)
        self.view.tabNode.friendPreviousKeyHint.gameObject:SetActive(shopCount > 1)
        self.view.tabNode.friendNextKeyHint.gameObject:SetActive(shopCount > 1)

        local targetIndex = 1
        if shopCount > 0 and resumeState then
            targetIndex = lume.clamp(resumeState.selectedFriendShopIndex or 1, 1, shopCount)
        end

        self:_OnChangeSelectFriendShop(targetIndex)
    end

    if resumeState and resumeState.bulkSellState then
        self:_RestoreBulkSellByResumeState(resumeState.bulkSellState)
    end
end

ShopTradeCtrl._RefreshTitleMoneyUI = HL.Method(HL.String, HL.String) << function(self, domainId, moneyId)
    local hasCfg, domainCfg = Tables.domainDataTable:TryGetValue(domainId)
    self.view.domainTopMoneyTitle:InitDomainTopMoneyTitle(domainId)
    if self.m_isLocalShop then
        self.view.domainTopMoneyTitle.view.titleTxt.text = string.format(Language.LUA_DOMAIN_SHOP_TITLE, domainCfg.domainName)
    else
        self.view.domainTopMoneyTitle.view.titleTxt.text = Language.LUA_DOMAIN_SHOP_FRIEND_TITLE
    end
end

ShopTradeCtrl._OnRefreshGoodsGroupCell = HL.Method(HL.Any, HL.Number) << function(self, cell, globalLuaIndex)
    local result = self:_GlobalToGroupLocal(globalLuaIndex)
    local groupIndex = result.groupIndex
    local goodsIndex = result.goodsIndex
    if not groupIndex then
        return
    end
    local groupInfo = self:_GetGoodsGroupInfo(groupIndex)
    local goodsInfo = groupInfo and groupInfo.goodsList[goodsIndex]
    if not goodsInfo then
        return
    end
    if self.m_isLocalShop then
        if self.m_isSelectCommonShop then
            cell:InitShopTradeGoodsCellCommonMode(goodsInfo)
        else
            cell:InitShopTradeGoodsCellRandomMode(goodsInfo, groupIndex == 1)
        end
    else
        cell:InitShopTradeGoodsCellFriendMode(goodsInfo)
    end
    cell.view.selectBtn.onIsNaviTargetChanged = function(isTarget)
        if isTarget then
            if self.m_curSelectTagIndex ~= groupIndex then
                self:_OnChangeSelectTagUI(groupIndex)
            end
        end
    end
end

ShopTradeCtrl._OnRefreshGoodsGroupTitle = HL.Method(HL.Any, HL.Number) << function(self, title, luaIndex)
    local groupInfo = self:_GetGoodsGroupInfo(luaIndex)
    if not title or not groupInfo then
        return
    end
    title.goodsTagTxt.text = groupInfo.tagName
    title.goodsTagImg:LoadSprite(UIConst.UI_SPRITE_SHOP_TAG_ICON, groupInfo.tagIcon)
    local stateName = "Normal"
    if self.m_isLocalShop and not self.m_isSelectCommonShop and luaIndex == 1 then
        local goodsCount = #groupInfo.goodsList
        if goodsCount <= 0 then
            stateName = "EmptyPosition"
        else
            stateName = "HasPosition"
            local profitInfo = {
                moneyIcon = self.m_domainInfo.moneyIcon,
                moneyCount = self.m_localShopInfo.myPositionGoodsGroup.totalPrice,
                profit = self.m_localShopInfo.myPositionGoodsGroup.totalProfit,
                profitRatio = self.m_localShopInfo.myPositionGoodsGroup.totalProfitRatio,
            }
            DomainShopUtils.refreshTotalMyPositionDetail(title.myPositionDetail, profitInfo)
        end
    elseif not self.m_isLocalShop then
        local shopInfo = self.m_friendShopInfoList[self.m_curSelectFriendShopIndex]
        stateName = "HasPosition"
        local profitInfo = {
            moneyIcon = shopInfo.moneyIcon,
            moneyCount = shopInfo.myPositionGoodsGroup.totalPrice,
            profit = shopInfo.myPositionGoodsGroup.totalProfit,
            profitRatio = shopInfo.myPositionGoodsGroup.totalProfitRatio,
        }
        DomainShopUtils.refreshTotalMyPositionDetail(title.myPositionDetail, profitInfo)
    end
    title.groupTitleStateCtrl:SetState(stateName)
end

ShopTradeCtrl._GetGoodsGroupInfo = HL.Method(HL.Number).Return(HL.Opt(HL.Table)) << function(self, luaIndex)
    if self.m_isLocalShop then
        if self.m_isSelectCommonShop then
            return self.m_localShopInfo.commonShopInfo.goodsGroupList[luaIndex]
        end
        if luaIndex == 1 then
            return self.m_localShopInfo.myPositionGoodsGroup
        end
        return self.m_localShopInfo.randomShopInfo.goodsGroupList[luaIndex - 1]
    end
    local shopInfo = self.m_friendShopInfoList[self.m_curSelectFriendShopIndex]
    return shopInfo and shopInfo.goodsGroupList[luaIndex]
end

ShopTradeCtrl._GetGoodsGroupCount = HL.Method().Return(HL.Number) << function(self)
    if self.m_isLocalShop then
        if self.m_isSelectCommonShop then
            return #self.m_localShopInfo.commonShopInfo.goodsGroupList
        end
        return #self.m_localShopInfo.randomShopInfo.goodsGroupList + 1
    end
    local shopInfo = self.m_friendShopInfoList[self.m_curSelectFriendShopIndex]
    return shopInfo and #shopInfo.goodsGroupList or 0
end

ShopTradeCtrl._GetGoodsGroupCellCount = HL.Method(HL.Number).Return(HL.Number) << function(self, luaIndex)
    local groupInfo = self:_GetGoodsGroupInfo(luaIndex)
    return groupInfo and #groupInfo.goodsList or 0
end

ShopTradeCtrl._GetGoodsGroupTitleHeight = HL.Method(HL.Number).Return(HL.Number) << function(self, luaIndex)
    if self.m_isLocalShop and not self.m_isSelectCommonShop and luaIndex == 1 then
        local groupInfo = self.m_localShopInfo.myPositionGoodsGroup
        return #groupInfo.goodsList <= 0 and self.m_getCellSizeHelperInfo.titleHeightEmptyPosition or self.m_getCellSizeHelperInfo.titleHeightHasPosition
    end
    if not self.m_isLocalShop then
        return self.m_getCellSizeHelperInfo.titleHeightHasPosition
    end
    return self.m_getCellSizeHelperInfo.titleHeight
end

ShopTradeCtrl._GlobalToGroupLocal = HL.Method(HL.Number).Return(HL.Table) << function(self, globalLuaIndex)
    local remaining = globalLuaIndex
    for groupIndex = 1, self:_GetGoodsGroupCount() do
        local cellCount = self:_GetGoodsGroupCellCount(groupIndex)
        if remaining <= cellCount then
            return { groupIndex = groupIndex, goodsIndex = remaining }
        end
        remaining = remaining - cellCount
    end
    return {}
end

ShopTradeCtrl._OnRefreshGoodsTagCell = HL.Method(HL.Any, HL.Number) << function(self, cell, luaIndex)
    local groupInfo = self:_GetGoodsGroupInfo(luaIndex)
    if not groupInfo then
        return
    end
    local isLastIndex = self:_GetGoodsGroupCount() == luaIndex
    cell.connectLineImg.enabled = not isLastIndex
    cell.tagIconImg:LoadSprite(UIConst.UI_SPRITE_SHOP_TAG_ICON, groupInfo.tagIcon)
    cell.animationWrapper:ClearTween()
    cell.animationWrapper:Play(self.m_curSelectTagIndex == luaIndex and "shoptrade_goodstagcell_select" or "shoptrade_goodstagcell_noselect")
    cell.tagBtn.onClick:RemoveAllListeners()
    cell.tagBtn.onClick:AddListener(function()
        self:_OnClickGoodsTagCell(luaIndex)
    end)
    cell.tagBtn.onIsNaviTargetChanged = function(isTarget)
        if isTarget then
            self:_OnClickGoodsTagCell(luaIndex)
        end
    end

    cell.gameObject.name = "tag_" .. groupInfo.tagId
end

ShopTradeCtrl._OnClickGoodsTagCell = HL.Method(HL.Number) << function(self, luaIndex)
    if self.m_curSelectTagIndex ~= luaIndex then
        self:_OnChangeSelectTagUI(luaIndex)
        self.view.goodsNode.goodsGroupList:ScrollToGroup(CSIndex(luaIndex), true, CS.Beyond.UI.UIScrollList.ScrollAlignType.Top)
    end
end


ShopTradeCtrl._RefreshLocalShopTabUI = HL.Method() << function(self)
    local randomTab = self.view.tabNode.shopRandomTab
    local hasRandomTab = #self.m_localShopInfo.randomShopInfo.goodsGroupList > 0
    self.view.tabNode.previousKeyHintParent.gameObject:SetActive(hasRandomTab)
    self.view.tabNode.nextKeyHintParent.gameObject:SetActive(hasRandomTab)
    if not hasRandomTab then
        randomTab.gameObject:SetActive(false)
    else
        randomTab.gameObject:SetActive(true)

        local profitRatio = self.m_localShopInfo.myPositionGoodsGroup.totalProfitRatio
        local stateName = DomainShopUtils.getProfitArrowStateName(profitRatio)
        randomTab.profitTxt.text = math.abs(profitRatio)
        randomTab.profitArrowStateCtrl:SetState(stateName)
        local count = #self.m_localShopInfo.myPositionGoodsGroup.goodsList
        randomTab.profitLossInfo.gameObject:SetActive(count > 0)
    end
end

ShopTradeCtrl._RefreshLocalShopGoodsUI = HL.Method(HL.Boolean) << function(self, isChangeTab)
    local count
    local isCommonShop = self.m_isSelectCommonShop
    self.view.goodsNode.remainLimitCountNode.gameObject:SetActive(false)
    self._updateLimitCountTimeKey = LuaUpdate:Remove(self._updateLimitCountTimeKey)
    if isCommonShop then
        count = #self.m_localShopInfo.commonShopInfo.goodsGroupList
    else
        count = #self.m_localShopInfo.randomShopInfo.goodsGroupList + 1
        self:_RefreshLimitCount()
        self._updateLimitCountTimeKey = LuaUpdate:Add("LateTick", function(deltaTime)
            if self._nextUpdateLimitCountTime <= Time.time then
                self._nextUpdateLimitCountTime = Time.time + 1
                self:_RefreshLimitCount()
            end
        end)
    end
    local hasPosition = #self.m_localShopInfo.myPositionGoodsGroup.goodsList > 0
    self.view.goodsNode.bulkSellBtnNode.gameObject:SetActive(not isCommonShop and hasPosition)
    self.view.goodsNode.goodsGroupList:UpdateGroup(count, isChangeTab, false, false, true)
    if isChangeTab then
        if self.m_targetLocalJumpArg.locateInfo then
            self.m_curSelectTagIndex = lume.clamp(self.m_targetLocalJumpArg.locateInfo.groupIndex, 1, math.max(count, 1))
        elseif self.m_resumeArg and self.m_resumeArg.curSelectTagIndex then
            self.m_curSelectTagIndex = lume.clamp(self.m_resumeArg.curSelectTagIndex, 1, math.max(count, 1))
        else
            self.m_curSelectTagIndex = 1
        end
    end
    if count <= 0 then
        self.m_curSelectTagIndex = 0
    elseif self.m_curSelectTagIndex < 1 or self.m_curSelectTagIndex > count then
        self.m_curSelectTagIndex = 1
    end
    if isChangeTab and self.m_curSelectTagIndex > 0 then
        self.view.goodsNode.goodsGroupList:ScrollToGroup(CSIndex(self.m_curSelectTagIndex), true, CS.Beyond.UI.UIScrollList.ScrollAlignType.Top)
    end

    self.m_goodsTagCellCache:Refresh(count, function(cell, luaIndex)
        self:_OnRefreshGoodsTagCell(cell, luaIndex)
    end)
end

ShopTradeCtrl._RefreshLocalShopTabAni = HL.Method(HL.Boolean, HL.Boolean) << function(self, isCommonShop, isReset)
    if isReset then
        self.view.tabNode.shopCommonTab.animationWrapper:Play(isCommonShop and "shopmarkettab_in" or "shopmarkettab_reset")
        self.view.tabNode.shopRandomTab.animationWrapper:Play(not isCommonShop and "shopmarkettab_in" or "shopmarkettab_reset")
    else
        self.view.tabNode.shopCommonTab.animationWrapper:Play(isCommonShop and "shopmarkettab_in" or "shopmarkettab_out")
        self.view.tabNode.shopRandomTab.animationWrapper:Play(not isCommonShop and "shopmarkettab_in" or "shopmarkettab_out")
    end
end

ShopTradeCtrl._RefreshLimitCount = HL.Method() << function(self)
    self.view.goodsNode.remainLimitCountNode.gameObject:SetActive(true)
    local shopId = self.m_localShopInfo.randomShopInfo.shopId
    local shopGroupId = shopSystem:GetShopGroupIdByShopId(shopId)
    local groupData = shopSystem:GetShopGroupData(shopGroupId)
    local remainLimitCount = self.m_localShopInfo.randomShopInfo.remainLimitCount
    local limitUpCount = groupData.domainChannelData.buyRandomGoodsLimitUpCount
    local maxLimitCount = groupData.domainChannelData.buyRandomGoodsLimitCount
    local limitCountNode = self.view.goodsNode.remainLimitCountNode
    limitCountNode.limitCountTxt.text = remainLimitCount .. "/" .. maxLimitCount

    local diff = DomainShopUtils.getNextServerRefreshTimeLeftSecByType(GEnums.ShopFrequencyLimitType.Daily)
    if diff >= 0 then
        limitCountNode.refreshCountdown.gameObject:SetActive(true)
        limitCountNode.refreshTimeTxt.text = string.format(Language.LUA_TRADE_ITEM_REFRESH_LEFT_TIME, UIUtils.getShortLeftTime(diff))
        limitCountNode.refreshCountTxt.text = string.format("+%d", limitUpCount)

        local willOverflow = (remainLimitCount + limitUpCount) > maxLimitCount
        limitCountNode.refreshCountdownState:SetState(willOverflow and "Overflow" or "Normal")
    else
        limitCountNode.refreshCountdown.gameObject:SetActive(false)
    end
end





ShopTradeCtrl._OnUpdateFriendTabCell = HL.Method(HL.Any, HL.Number) << function(self, cell, luaIndex)
    local info = self.m_friendShopInfoList[luaIndex]
    cell.tabNameTxt.text = info.domainName
    cell.tabIconImg:LoadSprite(UIConst.UI_SPRITE_SETTLEMENT, info.domainIcon)
    cell.tabBigIconImg:LoadSprite(UIConst.UI_SPRITE_SETTLEMENT_ICON_BIG, info.domainIcon)
    cell.animationWrapper:Play("friendmarkettab_reset")

    cell.selectBtn.onClick:RemoveAllListeners()
    cell.selectBtn.onClick:AddListener(function()
        if self.m_curSelectFriendShopIndex ~= luaIndex then
            self:_OnChangeSelectFriendShop(luaIndex)
        end
    end)
end

ShopTradeCtrl._RefreshFriendShopGoodsUI = HL.Method(HL.Boolean) << function(self, isInit)

    local shopInfo = self.m_friendShopInfoList[self.m_curSelectFriendShopIndex]
    local count = #shopInfo.goodsGroupList
    self.view.goodsNode.goodsGroupList:UpdateGroup(count, isInit, false, false, true)

    local hasPosition = #shopInfo.myPositionGoodsGroup.goodsList > 0
    self.view.goodsNode.bulkSellBtnNode.gameObject:SetActive(hasPosition)

    local groupObj = self.view.goodsNode.goodsGroupList:Get(0)
    local cell = self.m_getGoodsGroupCellFunc(groupObj)
    if cell then
        self:SetNaviTarget(cell.view.selectBtn)
    end
end




ShopTradeCtrl._RefreshBulkSellUI = HL.Method() << function(self)

    local bulkSellNode = self.view.bulkSellNode
    DomainShopUtils.refreshTotalMyPositionDetail(bulkSellNode.myPositionDetail, self.m_bulkSellInfo.profitInfo)

    local goodsCount = #self.m_bulkSellInfo.goodsList
    bulkSellNode.goodsList:UpdateCount(goodsCount, true)

    self:_RefreshBulkSellSelectState()
    local targetIndex = self.m_bulkSellInfo.curFocusGoodsIndex
    if targetIndex <= 0 then
        targetIndex = 1
    end
    local obj = bulkSellNode.goodsList:Get(CSIndex(targetIndex))
    local cell = self.m_getBulkSellGoodsCellFunc(obj)
    if cell then
        self:SetNaviTarget(cell.view.selectBtn)
    end
end

ShopTradeCtrl._OnSelectBulkSellGoods = HL.Method(HL.Number) << function(self, luaIndex)
    local bulkInfo = self.m_bulkSellInfo
    local oldIndex = bulkInfo.curFocusGoodsIndex
    local oldObj = self.view.bulkSellNode.goodsList:Get(CSIndex(oldIndex))
    local newObj = self.view.bulkSellNode.goodsList:Get(CSIndex(luaIndex))

    local oldCell = self.m_getBulkSellGoodsCellFunc(oldObj)

    local newCell = self.m_getBulkSellGoodsCellFunc(newObj)

    if oldIndex == luaIndex then

        bulkInfo.curFocusGoodsIndex = 0
        local count = bulkInfo.selectCountList[oldIndex]
        local reward = bulkInfo.goodsList[oldIndex].todayPrice * count
        bulkInfo.totalReward = bulkInfo.totalReward - reward
        bulkInfo.selectCountList[oldIndex] = nil
        if oldCell then
            oldCell:SetSelectCount(0)
            oldCell:SetSelectState(false)
        end
    else

        bulkInfo.curFocusGoodsIndex = luaIndex
        local curCount = bulkInfo.selectCountList[luaIndex]
        if curCount == nil then

            curCount = 1
            bulkInfo.selectCountList[luaIndex] = 1
            bulkInfo.totalReward = bulkInfo.totalReward + bulkInfo.goodsList[luaIndex].todayPrice
        end
        if oldCell then
            oldCell:SetSelectState(false)
        end
        if newCell then
            newCell:SetSelectCount(curCount)
            newCell:SetSelectState(true)
        end
    end

    self.view.bulkSellNode.goodsList:ScrollToIndex(CSIndex(luaIndex))
    self:_RefreshBulkSellSelectState()
end

ShopTradeCtrl._RefreshBulkSellSelectState = HL.Method() << function(self)
    local bulkSellNode = self.view.bulkSellNode
    local curIndex = self.m_bulkSellInfo.curFocusGoodsIndex
    if curIndex == 0 then
        bulkSellNode.selectStateCtrl:SetState("noSelect")
        return
    end

    bulkSellNode.selectStateCtrl:SetState("hasSelect")
    local goodsInfo = self.m_bulkSellInfo.goodsList[curIndex]
    bulkSellNode.goodsName.text = goodsInfo.itemName
    bulkSellNode.avgPriceTxt.text = goodsInfo.positionAvgPrice
    bulkSellNode.curPriceTxt.text = goodsInfo.todayPrice
    bulkSellNode.curPriceRatioTxt.text = goodsInfo.profitRatio
    bulkSellNode.profitArrowStateCtrl:SetState(DomainShopUtils.getProfitArrowStateName(goodsInfo.profitRatio))
    bulkSellNode.moneyIcon1:LoadSprite(UIConst.UI_SPRITE_WALLET, goodsInfo.moneyIcon)
    bulkSellNode.moneyIcon2:LoadSprite(UIConst.UI_SPRITE_WALLET, goodsInfo.moneyIcon)
    bulkSellNode.moneyIcon3:LoadSprite(UIConst.UI_SPRITE_WALLET, goodsInfo.moneyIcon)

    bulkSellNode.totalRewardMoneyTxt.text = self.m_bulkSellInfo.totalReward
    local selectCount = self.m_bulkSellInfo.selectCountList[curIndex]
    bulkSellNode.numberSelector:InitNumberSelector(selectCount, 1, goodsInfo.itemCount, self.m_onSelectorNumberChanged)
end

ShopTradeCtrl._OnRefreshBulkSellGoodsCell = HL.Method(HL.Forward("ShopTradeGoodsCell"), HL.Number) << function(self, cell, luaIndex)
    local info = self.m_bulkSellInfo.goodsList[luaIndex]
    cell:InitShopTradeGoodsCellBulkSellMode(info, luaIndex, self.m_onClickBulkSellGoods)
    cell:SetSelectCount(self.m_bulkSellInfo.selectCountList[luaIndex] or 0)
    cell:SetSelectState(self.m_bulkSellInfo.curFocusGoodsIndex == luaIndex)
    cell.gameObject.name = "GoodsCell_" .. luaIndex
end





ShopTradeCtrl._OnChangeSelectLocalShop = HL.Method(HL.Boolean, HL.Opt(HL.Boolean)) << function(self, isCommonShop, isInit)
    shopSystem:SetGoodsIdSee()

    self.m_isSelectCommonShop = isCommonShop
    if isCommonShop then
        self.view.goodsNode.stateController:SetState("ShopCommon")
        self:_UpdateCommonShopData()
    else
        self.view.goodsNode.stateController:SetState("ShopRandom")
        self:_UpdateRandomShopData()
    end

    self:_RefreshLocalShopGoodsUI(true)
    self:_RefreshLocalShopTabAni(isCommonShop, not not isInit)
    if self.m_targetLocalJumpArg.locateInfo == nil then
        local cell = self.m_goodsTagCellCache:Get(self.m_curSelectTagIndex)
        if cell then
            self:SetNaviTarget(cell.tagBtn)
        end
    end
end

ShopTradeCtrl._OnChangeSelectTagUI = HL.Method(HL.Number) << function(self, newIndex)
    local oldIndex = self.m_curSelectTagIndex
    self.m_curSelectTagIndex = newIndex
    local oldCell = self.m_goodsTagCellCache:Get(oldIndex)
    if oldCell then
        oldCell.animationWrapper:Play("shoptrade_goodstagcell_noselect")
    end
    local newCell = self.m_goodsTagCellCache:Get(newIndex)
    if newCell then
        newCell.animationWrapper:Play("shoptrade_goodstagcell_select")
    end
end

ShopTradeCtrl._OnChangeSelectFriendShop = HL.Method(HL.Number) << function(self, luaIndex)

    local oldIndex = self.m_curSelectFriendShopIndex
    self.m_curSelectFriendShopIndex = luaIndex

    local oldCell = self.m_friendTabCellCache:Get(oldIndex)
    if oldCell then
        oldCell.animationWrapper:Play("friendmarkettab_out")
    end
    local newCell = self.m_friendTabCellCache:Get(luaIndex)
    if newCell then
        newCell.animationWrapper:Play("friendmarkettab_in")
    end

    local shopInfo = self.m_friendShopInfoList[luaIndex]
    self:_RefreshFriendShopGoodsUI(true)

    self:_RefreshTitleMoneyUI(shopInfo.domainId, shopInfo.moneyId)
end

ShopTradeCtrl._OnChangeBulkSellSelectCount = HL.Method(HL.Number, HL.Boolean) << function(self, curNumber, changeFromCode)
    local bulkInfo = self.m_bulkSellInfo
    local curIndex = bulkInfo.curFocusGoodsIndex
    local goodsInfo = bulkInfo.goodsList[curIndex]
    local oldCount = bulkInfo.selectCountList[curIndex]

    if oldCount == nil then
        oldCount = 0
        bulkInfo.selectCountList[curIndex] = 0
    end
    local countDiff = curNumber - oldCount
    bulkInfo.selectCountList[curIndex] = curNumber
    bulkInfo.totalReward = bulkInfo.totalReward + goodsInfo.todayPrice * countDiff

    self.view.bulkSellNode.totalRewardMoneyTxt.text = bulkInfo.totalReward
    if changeFromCode then
        self.view.bulkSellNode.numberSelector:RefreshNumber(
            curNumber,
            1,
            goodsInfo.itemCount
        )
    end
    local obj = self.view.bulkSellNode.goodsList:Get(CSIndex(curIndex))

    local cell = self.m_getBulkSellGoodsCellFunc(obj)
    if cell then
        cell:SetSelectCount(curNumber)
    end
end

ShopTradeCtrl._OnConfirmBulkSellGoods = HL.Method() << function(self)
    if self.m_waitBulkSellResp then
        return
    end
    local roleId
    local shopId
    local isSellLocal
    if self.m_isLocalShop then
        roleId = GameInstance.player.roleId
        shopId = self.m_localShopInfo.randomShopInfo.shopId
        isSellLocal = true
    else
        roleId = self.m_friendRoleId
        shopId = self.m_friendShopInfoList[self.m_curSelectFriendShopIndex].shopId
        isSellLocal = false
    end

    local goodsIds = {}
    local goodsCountList = {}
    for index, count in pairs(self.m_bulkSellInfo.selectCountList) do
        table.insert(goodsIds, self.m_bulkSellInfo.goodsList[index].goodsId)
        table.insert(goodsCountList, count)
    end

    local success = shopSystem:SendSellGoods(roleId, shopId, goodsIds, goodsCountList, isSellLocal)
    if success then
        self.m_waitBulkSellResp = true
    end
end

ShopTradeCtrl._OnBuyItemSuccess = HL.Method(HL.Any) << function(self, msg)
    shopSystem:SetGoodsIdSee()
    if self.m_isLocalShop then
        if self.m_isSelectCommonShop then
            self:_RefreshSingleGoods(msg)
        else
            self:_UpdateData()
            self:_RefreshLocalShopGoodsUI(false)
            self:_RefreshLocalShopTabUI()
        end
    else
        self:_UpdateData()
        self:_RefreshFriendShopGoodsUI(false)
    end
end

ShopTradeCtrl._RefreshSingleGoods = HL.Method(HL.Any) << function(self, msg)
    local unpackMsg = unpack(msg)
    local goodsId = unpackMsg.GoodsId

    local searchInfo = self.m_commonShopRefreshGoodsSearchMap[goodsId]
    if not searchInfo then
        return
    end
    local goodsInfo = searchInfo.goodsInfo
    local _, goodsCfg = Tables.shopGoodsTable:TryGetValue(goodsId)
    if not goodsCfg then
        return
    end
    local itemBundle = UIUtils.getRewardFirstItem(goodsCfg.rewardId)
    local itemId = itemBundle.id
    goodsInfo.remainLimitCount = shopSystem:GetRemainCountByGoodsId(unpackMsg.ShopId, goodsId)
    goodsInfo.itemCount = Utils.getItemCount(itemId, true, true)
    goodsInfo.remainLimitSort = goodsInfo.remainLimitCount == 0 and 1 or 0

    local goodsObj = self.view.goodsNode.goodsGroupList:Get(CSIndex(searchInfo.groupIndex), CSIndex(searchInfo.goodsIndex))
    local goodsCell = self.m_getGoodsGroupCellFunc(goodsObj)
    if goodsCell then
        goodsCell:InitShopTradeGoodsCellCommonMode(goodsInfo)
    end
end

ShopTradeCtrl._OnSellItemSuccess = HL.Method(HL.Any) << function(self, msg)
    shopSystem:SetGoodsIdSee()
    if self.m_waitBulkSellResp then
        self.m_waitBulkSellResp = false
        self:_ShowBulkSellNode(false)
        local items = {
            {
                id = self.m_bulkSellInfo.profitInfo.moneyId,
                count = self.m_bulkSellInfo.totalReward,
            }
        }

        Notify(MessageConst.SHOW_SYSTEM_REWARDS, {
            title = Language.LUA_SELL_ITEM_SUCC_TITLE,
            icon = "icon_common_rewards",
            items = items,
            onComplete = function()
                Notify(MessageConst.AFTER_ON_BUY_ITEM_SUCC)
            end,
        })
    end
    self:_UpdateData()
    if self.m_isLocalShop then
        self:_RefreshLocalShopGoodsUI(false)
        self:_RefreshLocalShopTabUI()
        self:_NotifyClientCondition()
    else
        self:_RefreshFriendShopGoodsUI(false)
    end
end

ShopTradeCtrl._OnShopRefresh = HL.Method() << function(self)
    if self.m_isLocalShop then
        shopSystem:SetGoodsIdSee()
        self:_UpdateData()
        self:_RefreshAllUI()
    	self:_NotifyClientCondition()
    end
end

ShopTradeCtrl._OnFriendShopRefresh = HL.Method() << function(self)
    if not self.m_isLocalShop then
        shopSystem:SetGoodsIdSee()

        local shopData = shopSystem:GetFriendShopData(self.m_friendRoleId)
        if shopData then
            self:_UpdateData()
            self:_RefreshAllUI()
        else

            PhaseManager:PopPhase(PHASE_ID)
            if self.m_onCloseCallBack then
                self.m_onCloseCallBack()
                self.m_onCloseCallBack = nil
            end
        end
    end
end

ShopTradeCtrl._ShowBulkSellNode = HL.Method(HL.Boolean) << function(self, isShow)
    self.view.bulkSellNode.gameObject:SetActive(isShow)
    InputManagerInst:ToggleGroup(self.view.goodsNode.inputGroup.groupId, not isShow)
    AudioManager.PostEvent(isShow and "Au_UI_Popup_Common_Large_Open" or "Au_UI_Popup_Common_Large_Close")
end

ShopTradeCtrl._UpdateReadInfo = HL.Method(HL.String) << function(self, goodsId)
    shopSystem:RecordSeeGoodsId(goodsId)
end

ShopTradeCtrl._NotifyClientCondition = HL.Method() << function(self)
    local hasSoldOutGroup = self.m_isLocalShop and self.m_isSelectCommonShop and #self.m_localShopInfo.soldOutGoodsGroup.goodsList > 0
    CS.Beyond.Gameplay.Conditions.CheckDomainShopPanelHasSoldOutGroup.Trigger(hasSoldOutGroup)
end

ShopTradeCtrl._NotifyOpenDomainShopCondition = HL.Method() << function(self)
    if self.m_isLocalShop then
        CS.Beyond.Gameplay.Conditions.CheckIsOpenDomainShop.Trigger(self.m_domainId)
    end
end

ShopTradeCtrl.GetCurPhaseStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    return {
        domainId = self.m_domainId,
        friendRoleId = self.m_friendRoleId,
        showFriendRecord = self.m_showFriendRecord,
        resumeState = {

            isSelectCommonShop = self.m_isSelectCommonShop,
            selectedFriendShopIndex = self.m_curSelectFriendShopIndex,
            curSelectTagIndex = self.m_isLocalShop and self.m_curSelectTagIndex or nil,

            bulkSellState = self:_CollectBulkSellResumeState(),
        },
        resumeOpenPanel = self:_CollectPopupResumeState(),
    }
end

ShopTradeCtrl._OnScreenSizeChanged = HL.Method() << function(self)
    self:m_InitGetCellSizeHelperInfo()
    local count = 0
    if self.m_isLocalShop then
        if self.m_isSelectCommonShop then
            count = #self.m_localShopInfo.commonShopInfo.goodsGroupList
        else
            count = #self.m_localShopInfo.randomShopInfo.goodsGroupList + 1
        end
    else
        local shopInfo = self.m_friendShopInfoList[self.m_curSelectFriendShopIndex]
        if shopInfo == nil then
            return
        end
        count = #shopInfo.goodsGroupList
    end
    self.view.goodsNode.goodsGroupList:UpdateGroup(count, false)
end


HL.Commit(ShopTradeCtrl)
