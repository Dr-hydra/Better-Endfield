local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local shopSystem = GameInstance.player.shopSystem
SpaceshipShopBaseCtrl = HL.Class('SpaceshipShopBaseCtrl', uiCtrl.UICtrl)

SpaceshipShopBaseCtrl.m_shopGroupId = HL.Field(HL.String) << ""

SpaceshipShopBaseCtrl.m_shopId = HL.Field(HL.String) << ""

SpaceshipShopBaseCtrl.m_goodsInfos = HL.Field(HL.Table)

SpaceshipShopBaseCtrl.m_goods = HL.Field(HL.Table)

SpaceshipShopBaseCtrl.m_soldOut = HL.Field(HL.Table)

SpaceshipShopBaseCtrl.m_needPlaySoldOut = HL.Field(HL.Table)

SpaceshipShopBaseCtrl.m_needPlayUnlock = HL.Field(HL.Table)

SpaceshipShopBaseCtrl.m_waitAnimation = HL.Field(HL.Boolean) << false

SpaceshipShopBaseCtrl.m_getCellFunc = HL.Field(HL.Function)

SpaceshipShopBaseCtrl.m_isInitSortNode = HL.Field(HL.Boolean) << false
SpaceshipShopBaseCtrl.m_needShowUnlock = HL.Field(HL.Boolean) << false

SpaceshipShopBaseCtrl.m_lastBuyGoods = HL.Field(HL.Table)






SpaceshipShopBaseCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_SHOP_REFRESH] = '_OnShopRefresh',
    [MessageConst.ON_SHOP_FREQUENCY_LIMIT_CHANGE] = 'OnLimitChange',
    [MessageConst.ON_SHOP_GOODS_LOCK_CHANGE] = 'OnConditionChange',
    [MessageConst.ON_BUY_ITEM_SUCC] = 'OnBuyItemSucc',
    [MessageConst.AFTER_ON_BUY_ITEM_SUCC] = 'OnAfterBuyItemSucc',
    [MessageConst.SHOP_WAIT_ANIMATION] = 'WaitAnimation',
    [MessageConst.SHOW_SHOP_ITEM_POP_UP] = 'SetMoneyCell',
    [MessageConst.ON_SHOP_GOODS_CONDITION_REFRESH] = '_OnShopRefresh',
    [MessageConst.ON_SHOP_GOODS_MANUAL_REFRESH] = '_OnShopGoodsManualRefresh',
    [MessageConst.ON_SPACESHIP_GUEST_ROOM_RECV_VISIT_LIST_REWARD] = '_OnGetCredit',
    [MessageConst.ON_WALLET_CHANGED] = '_OnWalletChanged',
    [MessageConst.ON_SPACESHIP_RECV_QUERY_VISIT_INFO] = '_OnReceiveVisitInfo',
}

SpaceshipShopBaseCtrl._OnShopRefresh = HL.Virtual() << function(self)
    logger.info("[shop] _OnShopRefresh")
    if self.m_waitAnimation then
        return
    end
    self:_RefreshSheetTabs(self.m_shopId)
    self.view.scrollList:SkipGraduallyShow()
end



SpaceshipShopBaseCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)

end

SpaceshipShopBaseCtrl._RefreshSheetTabs = HL.Virtual(HL.String) << function(self, curShopId)

end

SpaceshipShopBaseCtrl._ApplySortOption = HL.Method(HL.Opt(HL.Table, HL.Boolean)) << function(self, sortData, isIncremental)
    sortData = sortData or self.view.sortNode:GetCurSortData()
    if isIncremental == nil then
        isIncremental = self.view.sortNode.isIncremental
    end

    table.sort(self.m_goods, Utils.genSortFunction(sortData.keys, isIncremental))

    self.m_goodsInfos = {}
    for i, v in ipairs(self.m_goods) do
        table.insert(self.m_goodsInfos, v)
    end
    for i, v in ipairs(self.m_soldOut) do
        table.insert(self.m_goodsInfos, v)
    end
    self:_RefreshContent()
end

SpaceshipShopBaseCtrl._RefreshContent = HL.Method() << function(self)
    self.view.scrollList:UpdateCount(#self.m_goodsInfos)
end

SpaceshipShopBaseCtrl._RefreshContentCell = HL.Virtual(HL.Any, HL.Number) << function(self, cell, luaIndex)
    local goodsId = self.m_goodsInfos[luaIndex].id
    local goodsData = shopSystem:GetShopGoodsData(self.m_shopId, goodsId)
    cell:InitCashShopItem(goodsData, true, true)
end

SpaceshipShopBaseCtrl.CheckGoodsUnlocked = HL.Virtual(HL.String).Return(HL.Boolean) << function(self, goodsId)
    return not shopSystem:CheckGoodsUnlocked(goodsId)
end


SpaceshipShopBaseCtrl._RefreshTimeCountDown = HL.Virtual() << function(self)
end



SpaceshipShopBaseCtrl._CalculateTargetTime = HL.Method(GEnums.ShopRefreshCycleType).Return(HL.Number) << function(self, refreshCycleType)
    local time = self:_CalculateServerTargetTime(refreshCycleType)
    return time
end

SpaceshipShopBaseCtrl._CalculateServerTargetTime = HL.Method(GEnums.ShopRefreshCycleType).Return(HL.Number) << function(self, refreshCycleType)
    if refreshCycleType == GEnums.ShopRefreshCycleType.Daily then
        return Utils.getNextCommonServerRefreshTime()
    elseif refreshCycleType == GEnums.ShopRefreshCycleType.Weekly then
        return Utils.getNextWeeklyServerRefreshTime()
    elseif refreshCycleType == GEnums.ShopRefreshCycleType.Monthly then
        return Utils.getNextMonthlyServerRefreshTime()
    end
end

SpaceshipShopBaseCtrl.OnLimitChange = HL.Method(HL.Any) << function(self, data)
    local goods, left
    if type(data) == "table" then
        goods,left = unpack(data)
    end
    for i, v in ipairs(self.m_goodsInfos) do
        if v.id == goods then
            if left == 0 then
                table.insert(self.m_needPlaySoldOut, goods)
            end
            break
        end
    end
end

SpaceshipShopBaseCtrl.OnConditionChange = HL.Method(HL.Any) << function(self, data)
    local goods, unlock
    if type(data) == "table" then
        goods,unlock = unpack(data)
    end

    for i, v in ipairs(self.m_goodsInfos) do
        if v.id == goods then
            if unlock then
                table.insert(self.m_needPlayUnlock, goods)
            end
            break
        end
    end
end

SpaceshipShopBaseCtrl.OnBuyItemSucc = HL.Virtual(HL.Any) << function(self, arg)
    local goodsId = arg[1].GoodsId
    table.insert(self.m_lastBuyGoods, goodsId)
end

SpaceshipShopBaseCtrl.OnAfterBuyItemSucc = HL.Virtual() << function(self)
    for i, v in ipairs(self.m_goodsInfos) do
        for j, id in ipairs(self.m_needPlaySoldOut) do
            if v.id == id then
                local cell = self.m_getCellFunc(i)
                if cell then
                    cell:PlaySoldOutAnimation()

                end
                break
            end
        end

        for j, id in ipairs(self.m_needPlayUnlock) do
            if v.id == id then
                local cell = self.m_getCellFunc(i)
                if cell then
                    cell:PlayUnlockAnimation()
                end
            end
            break
        end
    end

    for i, v in ipairs(self.m_needPlaySoldOut) do
        for j, data in ipairs(self.m_goodsInfos) do
            if v == data.id then
                local cell = self.m_getCellFunc(j)
                if cell then
                    self:_RefreshContentCell(cell, j)
                end
            end
        end
    end

    for i, v in ipairs(self.m_needPlayUnlock) do
        for j, data in ipairs(self.m_goodsInfos) do
            if v == data.id then
                local cell = self.m_getCellFunc(j)
                if cell then
                    self:_RefreshContentCell(cell, j)
                end
            end
        end
    end
    self.m_needPlaySoldOut = {}
    self.m_needPlayUnlock = {}
    for i, v in ipairs(self.m_lastBuyGoods) do
        for j, data in ipairs(self.m_goodsInfos) do
            if v == data.id then
                local cell = self.m_getCellFunc(j)
                if cell then
                    self:_RefreshContentCell(cell, j)
                end
            end
        end
    end
    self.m_lastBuyGoods = {}
end

SpaceshipShopBaseCtrl.WaitAnimation = HL.Method(HL.Boolean) << function(self, state)
    self.m_waitAnimation = state
end

SpaceshipShopBaseCtrl.SetMoneyCell = HL.Virtual(HL.Boolean) << function(self, arg)

end

SpaceshipShopBaseCtrl._OnShopGoodsManualRefresh = HL.Virtual() << function(self)

end

SpaceshipShopBaseCtrl._OnGetCredit = HL.Virtual(HL.Table) << function(self, args)

end

SpaceshipShopBaseCtrl._OnWalletChanged = HL.Virtual(HL.Table) << function(self, args)

end

SpaceshipShopBaseCtrl._OnReceiveVisitInfo = HL.Virtual() << function(self)
end

SpaceshipShopBaseCtrl.OnClose = HL.Override() << function(self)
end
HL.Commit(SpaceshipShopBaseCtrl)
