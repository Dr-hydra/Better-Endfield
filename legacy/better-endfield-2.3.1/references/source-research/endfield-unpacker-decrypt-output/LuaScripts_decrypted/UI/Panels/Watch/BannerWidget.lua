BannerWidget = HL.Class('BannerWidget')



BannerWidget.m_genPageTabCells = HL.Field(HL.Forward("UIListCache"))

BannerWidget.m_getBannerCellFunc = HL.Field(HL.Function)

BannerWidget.m_curPageIndex = HL.Field(HL.Number) << 1

BannerWidget.m_bannerCount = HL.Field(HL.Number) << 0

BannerWidget.m_infos = HL.Field(HL.Table)

BannerWidget.m_scrollHoldTime = HL.Field(HL.Number) << 0

BannerWidget.m_isPause = HL.Field(HL.Boolean) << false

BannerWidget.view = HL.Field(HL.Any)



BannerWidget.BannerWidget = HL.Constructor(HL.Any) << function(self, viewNode)
    self.view = viewNode
    self:_OnFirstTimeInit()
end

BannerWidget._OnFirstTimeInit = HL.Method() << function(self)
    self.m_genPageTabCells = UIUtils.genCellCache(self.view.pageTabCell)
    self.m_getBannerCellFunc = UIUtils.genCachedCellFunction(self.view.bannerList)
    self.view.bannerList.onUpdateCell:AddListener(function(obj, csIndex)
        local cell = self.m_getBannerCellFunc(obj)
        self:_OnRefreshBannerCell(cell, LuaIndex(csIndex))
    end)
    self.view.bannerList.onDrag:AddListener(function(_)
        self.m_scrollHoldTime = 0
    end)
    self.view.bannerList.onCenterIndexChanged:AddListener(function(_, _)
        self:_OnManualScroll()
    end)

    self:_RegisterMessages()
end

BannerWidget.InitBannerWidget = HL.Method() << function(self)
    self:_UpdateData()
    self:_RefreshAllUI()

    self:_StartAutoScroll()
end

BannerWidget.OnDestroy = HL.Method() << function(self)
    self:_StopAutoScroll()
    MessageManager:UnregisterAll(self)
end

BannerWidget.SetPause = HL.Method(HL.Boolean) << function(self, isPause)
    self.m_isPause = isPause
end



BannerWidget._RegisterMessages = HL.Method() << function(self)
    MessageManager:Register(MessageConst.ON_GACHA_POOL_INFO_CHANGED, function()
        self:_UpdateData()
        self:_RefreshAllUI()
    end, self)
end

BannerWidget._UpdateData = HL.Method() << function(self)
    self.m_infos = {}
    for bannerId, bannerCfg in pairs(Tables.activityBannerTable) do
        local canShow = true
        local type = bannerCfg.bannerType

        if not string.isEmpty(bannerCfg.jumpId) then
            canShow = canShow and Utils.canJumpToSystem(bannerCfg.jumpId)
        end
        if canShow then
            if type == GEnums.BannerType.Gacha then

                local dict = GameInstance.player.gacha.poolInfos
                local hasInfo, poolInfo = dict:TryGetValue(bannerCfg.corrSysId)
                if hasInfo then
                    canShow = poolInfo.isOpenValid
                else
                    canShow = false
                end
            elseif type == GEnums.BannerType.GiftPack then
                local cashShopId = Tables.activityBannerGiftPackTable[bannerId].cashShopId
                local cashGoodsId = Tables.activityBannerGiftPackTable[bannerId].cashGoodsId
                if not string.isEmpty(cashGoodsId) then

                    canShow = CashShopUtils.CheckCanBuyCashShopGoods(cashGoodsId)
                elseif not string.isEmpty(cashShopId) then

                    canShow = false
                    for _, groupData in ipairs(CashShopUtils.GetAllGiftPackGoodsByGroup()) do
                        if cashShopId == groupData.cashShopId then
                            for _, goodsInfo in ipairs(groupData.goodsInfos) do
                                if CashShopUtils.CheckCanBuyCashShopGoods(goodsInfo.goodsId) then
                                    canShow = true
                                    break
                                end
                            end
                        end
                    end
                end
            end
        end

        if canShow then
            local info = {
                iconImg = bannerCfg.image,
                jumpId = bannerCfg.jumpId,
                sortId = bannerCfg.sortId,
            }
            local suc, giftPackInfo = Tables.activityBannerGiftPackTable:TryGetValue(bannerId)
            if suc then
                info.cashShopId = giftPackInfo.cashShopId
                info.cashGoodsId = giftPackInfo.cashGoodsId
            end
            table.insert(self.m_infos, info)
        end
    end
    table.sort(self.m_infos, Utils.genSortFunction({"sortId"}, true))

    self.m_bannerCount = #self.m_infos
    self.m_curPageIndex = math.min(1, self.m_bannerCount)
end





BannerWidget._RefreshAllUI = HL.Method() << function(self)
    self.view.bannerList:UpdateCount(self.m_bannerCount, true)
    self.m_genPageTabCells:Refresh(self.m_bannerCount, function(cell, luaIndex)
        self:_OnRefreshPageTabCell(cell, luaIndex)
    end)
end

BannerWidget._OnRefreshPageTabCell = HL.Method(HL.Any, HL.Number) << function(self, cell, luaIndex)
    cell.toggle.isOn = luaIndex == self.m_curPageIndex
end

BannerWidget._OnRefreshBannerCell = HL.Method(HL.Any, HL.Number) << function(self, cell, luaIndex)
    local info = self.m_infos[luaIndex]
    cell.bannerImg:LoadSprite(UIConst.UI_SPRITE_WATCH_NEW_BANNER, info.iconImg)
    cell.button.onClick:RemoveAllListeners()
    cell.button.onClick:AddListener(function()
        self:JumpOut(luaIndex)
    end)
end

BannerWidget.JumpOut = HL.Method(HL.Opt(HL.Number)) << function(self, luaIndex)
    luaIndex = luaIndex or self.m_curPageIndex
    local info = self.m_infos[luaIndex]
    if not string.isEmpty(info.jumpId) and Utils.canJumpToSystem(info.jumpId) then
        Utils.jumpToSystem(info.jumpId)
    elseif info.cashShopId then
        PhaseManager:GoToPhase(PhaseId.CashShop, {
            shopGroupId = CashShopConst.CashShopCategoryType.Pack,
            cashShopId = info.cashShopId,
            goodsId = info.cashGoodsId
        })
    end
end

BannerWidget._OnManualScroll = HL.Method() << function(self)
    self.m_scrollHoldTime = 0
    local newLuaIndex = LuaIndex(self.view.bannerList.centerIndex)
    if self.m_curPageIndex ~= newLuaIndex then
        self.m_curPageIndex = newLuaIndex
        self:_ScrollPageTabToIndex(newLuaIndex)
    end
end

BannerWidget._ScrollPageTabToIndex = HL.Method(HL.Number) << function(self, index)
    self.m_scrollHoldTime = 0
    for idx, cell in pairs(self.m_genPageTabCells:GetItems()) do
        cell.toggle.isOn = index == idx
    end
end





BannerWidget.m_updateKey = HL.Field(HL.Number) << -1

BannerWidget._StartAutoScroll = HL.Method() << function(self)
    if self.m_updateKey > 0 then
        return
    end

    self.m_scrollHoldTime = 0
    self.m_updateKey = LuaUpdate:Add("Tick", function(deltaTime)
        self:_UpdateAutoScroll(deltaTime)
    end)
end

BannerWidget._UpdateAutoScroll = HL.Method(HL.Number) << function(self, deltaTime)
    if self.m_isPause or self.m_bannerCount == 0 then
        return
    end
    self.m_scrollHoldTime = self.m_scrollHoldTime + deltaTime
    if self.m_scrollHoldTime < self.view.config.AUTO_SCROLL_TIME then
        return
    end

    self.m_scrollHoldTime = 0
    self.m_curPageIndex = self.m_curPageIndex % self.m_bannerCount + 1

    self.view.bannerList:ScrollToIndex(CSIndex(self.m_curPageIndex))
    self:_ScrollPageTabToIndex(self.m_curPageIndex)
end

BannerWidget._StopAutoScroll = HL.Method() << function(self)
    if self.m_updateKey <= 0 then
        return
    end

    LuaUpdate:Remove(self.m_updateKey)
    self.m_updateKey = -1
end

BannerWidget.GetInfo = HL.Method().Return(HL.Any) << function(self)
    local info = self.m_infos[self.m_curPageIndex]
    return info
end

BannerWidget.PageUpOrDown = HL.Method(HL.Boolean) << function(self, pageDown)
    if self.m_isPause then
        return
    end
    if pageDown then
        self.m_curPageIndex = self.m_curPageIndex % self.m_bannerCount + 1
    else
        self.m_curPageIndex = self.m_curPageIndex == 1 and self.m_bannerCount or (self.m_curPageIndex - 1)
    end
    self.view.bannerList:ScrollToIndex(CSIndex(self.m_curPageIndex))
    self:_ScrollPageTabToIndex(self.m_curPageIndex)
end



HL.Commit(BannerWidget)
return BannerWidget
