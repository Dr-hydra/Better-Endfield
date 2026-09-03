
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ShopChoicenessGroupBag

ShopChoicenessGroupBagCtrl = HL.Class('ShopChoicenessGroupBagCtrl', uiCtrl.UICtrl)

ShopChoicenessGroupBagCtrl.m_tabData = HL.Field(HL.Table)

ShopChoicenessGroupBagCtrl.m_cashGoodsIds = HL.Field(HL.Table)

ShopChoicenessGroupBagCtrl.m_getTabCellFunc = HL.Field(HL.Function)

ShopChoicenessGroupBagCtrl.m_scroll = HL.Field(HL.Any)

ShopChoicenessGroupBagCtrl.m_canBuyCount = HL.Field(HL.Number) << 0

ShopChoicenessGroupBagCtrl.m_currNaviIndex = HL.Field(HL.Int) << 1





ShopChoicenessGroupBagCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_READ_CASH_SHOP_GOODS] = '_RefreshUI',
}


ShopChoicenessGroupBagCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_tabData = arg
    self.m_cashGoodsIds = self.m_tabData.cashGoodsIds

    self:_RefreshUI()

    self:_InitShortCut()
end











ShopChoicenessGroupBagCtrl._RefreshUI = HL.Method() << function(self)
    local canBuyGoodsIds = {}
    for _, goodsId in ipairs(self.m_cashGoodsIds) do
        local canBuy = CashShopUtils.CheckCanBuyCashShopGoods(goodsId) and
            CashShopUtils.CheckCashShopGoodsIsOpen(goodsId)
        if canBuy then

            if #canBuyGoodsIds == 3 then
                logger.info("[cashshop] ShopChoicenessGroupBagCtrl 中可显示超过3个，不显示超出部分。")
            else
                table.insert(canBuyGoodsIds, goodsId)
            end
        end
    end
    local canBuyCount = #canBuyGoodsIds
    self.m_canBuyCount = canBuyCount
    if canBuyCount > 3 then
        logger.error("请检查配置：" .. self.m_tabData.id)
        return
    end
    local stateCtrl = self.view.contentState
    if canBuyCount == 2 or canBuyGoodsIds == 3 then
        stateCtrl:SetState("OneSellOutOrAllSell")
        self:_RefreshUIThree(canBuyGoodsIds)
    elseif canBuyCount == 1 then
        stateCtrl:SetState("TwoSellOut")
        self:_RefreshUITwo(canBuyGoodsIds)
    elseif canBuyCount == 0 then
        stateCtrl:SetState("AllSellOut")
        self:_RefreshUIOne(canBuyGoodsIds)
    end


    if canBuyCount > 0 then
        self.view.cashShopKrTips.gameObject:SetActive(true)
        self.view.cashShopKrTips:InitCashShopKrTips()
    else
        self.view.cashShopKrTips.gameObject:SetActive(false)
    end

    GameInstance.player.cashShopSystem:ReadCashGoods(canBuyGoodsIds)
end


ShopChoicenessGroupBagCtrl._RefreshUIOne = HL.Method(HL.Table) << function(self, canBuyGoodsIds)
    local root = self.view.content1
    root.contentBtn.onClick:RemoveAllListeners()
    root.contentBtn.onClick:AddListener(function()

        self.m_phase:OpenGiftpackCategoryByCashShopId("All")
    end)


    root.tagLayout.tagNew.gameObject:SetActive(false)
end


ShopChoicenessGroupBagCtrl._RefreshUITwo = HL.Method(HL.Table) << function(self, canBuyGoodsIds)
    self.m_scroll = self.view.content2Scroll

    self.m_getTabCellFunc = UIUtils.genCachedCellFunction(self.m_scroll)
    self.m_scroll.onUpdateCell:AddListener(function(obj, index)
        local cell = self.m_getTabCellFunc(obj)
        index = LuaIndex(index)
        if index == 2 then

            self:_SetDefaultCell(cell)
        else

            self:_SetGiftCell(cell, canBuyGoodsIds[1])
        end
    end)

    self.m_scroll:UpdateCount(2)
end


ShopChoicenessGroupBagCtrl._RefreshUIThree = HL.Method(HL.Table) << function(self, canBuyGoodsIds)
    self.m_scroll = self.view.content3Scroll

    self.m_getTabCellFunc = UIUtils.genCachedCellFunction(self.m_scroll)
    self.m_scroll.onUpdateCell:AddListener(function(obj, index)
        local cell = self.m_getTabCellFunc(obj)
        index = LuaIndex(index)
        if index == 3 and #canBuyGoodsIds < 3 then

            self:_SetDefaultCell(cell)
        else

            self:_SetGiftCell(cell, canBuyGoodsIds[index])
        end
    end)

    self.m_scroll:UpdateCount(3)
end

ShopChoicenessGroupBagCtrl._SetDefaultCell = HL.Method(HL.Any) << function(self, cell)
    local stateCtrl = cell.stateController
    stateCtrl:SetState("SellOut")

    cell.contentBtn.onClick:RemoveAllListeners()
    cell.contentBtn.onClick:AddListener(function()

        self.m_phase:OpenGiftpackCategoryByCashShopId("All", self.m_tabData.id)
    end)


    cell.tagLayout.tagNew.gameObject:SetActive(false)
end

ShopChoicenessGroupBagCtrl._SetGiftCell = HL.Method(HL.Any, HL.String) << function(self, cell, goodsId)
    local stateCtrl = cell.stateController
    stateCtrl:SetState("Sell")

    cell.contentBtn.onClick:RemoveAllListeners()
    cell.contentBtn.onClick:AddListener(function()

        self.m_phase:OpenGiftpackCategoryAndOpenDetailPanel(goodsId, self.m_tabData.id)
    end)

    local _, recommendTextCfg = Tables.CashShopRecommendTextTable:TryGetValue(goodsId)
    cell.groupBagExplainTxt.gameObject:SetActive(recommendTextCfg ~= nil)
    if recommendTextCfg then
        cell.groupBagExplainTxt:SetAndResolveTextStyle(recommendTextCfg.text)
    end

    local name = CashShopUtils.GetCashGoodsName(goodsId)
    cell.groupBagTxt:SetAndResolveTextStyle(name)

    local price = CashShopUtils.getGoodsPriceText(goodsId)
    cell.sellNumberTxt:SetAndResolveTextStyle(price)


    local isNew = GameInstance.player.cashShopSystem:IsNewGoods(goodsId)
    cell.tagLayout.tagNew.gameObject:SetActive(isNew)


    if cell.contentPattern then
        local path, icon = CashShopUtils.GetGiftpackBigIcon(goodsId)
        cell.contentPattern:LoadSprite(path, icon)
    end
    if cell.bg then
        local path, bg = CashShopUtils.GetGiftpackBigBg(goodsId)
        cell.bg:LoadSprite(path, bg)
    end
end

ShopChoicenessGroupBagCtrl._InitShortCut = HL.Method() << function(self)
    if not DeviceInfo.usingController then
        return
    end

    if self:CheckRecommendSetNaviTarget() then
        self:BindInputPlayerAction("cashshop_giftpack_goto_left", function()
            InputManagerInst:ToggleGroup(self.view.contentStateInputBindingGroupMonoTarget.groupId, false)
            local leftCtrl = self.m_phase.m_panel2Item[PanelId.ShopRecommend].uiCtrl
            leftCtrl:NaviTargetCurrTab()
        end, self.view.contentStateInputBindingGroupMonoTarget.groupId)

        self:BindInputPlayerAction("cashshop_navigation_4_dir_left", function()
            self:_OnGoLeft()
        end, self.view.contentStateInputBindingGroupMonoTarget.groupId)

        self:BindInputPlayerAction("cashshop_navigation_4_dir_right", function()
            self:_OnGoRight()
        end, self.view.contentStateInputBindingGroupMonoTarget.groupId)

        InputManagerInst:ToggleGroup(self.view.contentStateInputBindingGroupMonoTarget.groupId, false)
    end
end

ShopChoicenessGroupBagCtrl._OnGoLeft = HL.Method() << function(self)
    local currIndex = self.m_currNaviIndex

    if currIndex and currIndex == 1 then
        InputManagerInst:ToggleGroup(self.view.contentStateInputBindingGroupMonoTarget.groupId, false)
        local leftCtrl = self.m_phase.m_panel2Item[PanelId.ShopRecommend].uiCtrl
        leftCtrl:NaviTargetCurrTab()
    else
        local targetCell = self.m_getTabCellFunc(self.m_scroll:Get(CSIndex(currIndex - 1)))
        self:SetNaviTarget(targetCell.inputBindingGroupNaviDecorator)
        self.m_currNaviIndex = self.m_currNaviIndex - 1
    end
end

ShopChoicenessGroupBagCtrl._OnGoRight = HL.Method() << function(self)
    local targetIndex = self.m_currNaviIndex + 1
    if targetIndex > self.m_canBuyCount + 1 then
        return
    end

    local targetCell = self.m_getTabCellFunc(self.m_scroll:Get(CSIndex(targetIndex)))
    self:SetNaviTarget(targetCell.inputBindingGroupNaviDecorator)
    self.m_currNaviIndex = targetIndex
end

ShopChoicenessGroupBagCtrl.OnRecommendSetNaviTarget = HL.Method().Return(HL.Boolean) << function(self)
    if self.m_canBuyCount == 0 then
        return false
    end

    InputManagerInst:ToggleGroup(self.view.contentStateInputBindingGroupMonoTarget.groupId, true)
    local firstCell = self.m_getTabCellFunc(self.m_scroll:Get(0))
    self:SetNaviTarget(firstCell.inputBindingGroupNaviDecorator)
    self.m_currNaviIndex = 1

    return true
end

ShopChoicenessGroupBagCtrl.CheckRecommendSetNaviTarget = HL.Method().Return(HL.Boolean) << function(self)
    if self.m_canBuyCount == 0 then
        return false
    end

    return true
end

HL.Commit(ShopChoicenessGroupBagCtrl)
