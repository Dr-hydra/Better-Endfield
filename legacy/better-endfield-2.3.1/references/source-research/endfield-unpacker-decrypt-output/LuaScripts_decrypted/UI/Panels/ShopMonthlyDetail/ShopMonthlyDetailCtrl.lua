
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ShopMonthlyDetail

ShopMonthlyDetailCtrl = HL.Class('ShopMonthlyDetailCtrl', uiCtrl.UICtrl)

ShopMonthlyDetailCtrl.m_goodsId = HL.Field(HL.String) << ""

ShopMonthlyDetailCtrl.m_goodsInfo = HL.Field(HL.Any)

ShopMonthlyDetailCtrl.m_getCellFunc = HL.Field(HL.Function)






ShopMonthlyDetailCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_CASH_SHOP_PACK_SET_TOP] = '_OnSetTop',
    [MessageConst.ON_CASH_SHOP_OPEN_CATEGORY] = '_OnCashShopOpenCategory',
    [MessageConst.ON_CASH_SHOP_RECEIVE_REFRESH_MSG] = '_OnCashShopReceiveRefreshMsg',
}


ShopMonthlyDetailCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_goodsId = arg.goodsId
    self.m_goodsInfo = arg.goodsInfo

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })

    self:_BindUICallback()
    self:_RefreshView()
end

ShopMonthlyDetailCtrl.OnShow = HL.Override() << function(self)
    Notify(MessageConst.ON_OPEN_CASH_SHOP_DETAILS)
end

ShopMonthlyDetailCtrl.OnClose = HL.Override() << function(self)
    Notify(MessageConst.ON_CLOSE_CASH_SHOP_DETAILS)
end

ShopMonthlyDetailCtrl._BindUICallback = HL.Method() << function(self)
    self.view.closeButton.onClick:RemoveAllListeners()
    self.view.closeButton.onClick:AddListener(function()
        self:PlayAnimationOut()
    end)

    self.view.btnCommonYellow.onClick:RemoveAllListeners()
    self.view.btnCommonYellow.onClick:AddListener(function()
        self:_TryBuyShop()
    end)

    self.m_getCellFunc = UIUtils.genCachedCellFunction(self.view.titleScrollList)
    self.view.titleScrollList.onUpdateCell:AddListener(function(obj, index)
        local cell = self.m_getCellFunc(obj)
        cell.titleTxt.text = Language["LUA_CASHSHOP_MONTHLYCARD_DETAILPANEL_REWARD_TITLE_" .. LuaIndex(index)]
        local itemCells = UIUtils.genCellCache(cell.itemBlack)
        local rewardInfoList = nil
        if index == 0 then

            rewardInfoList = CashShopUtils.GetMonthlyPassImmediateRewardInfoList()
        else

            rewardInfoList = CashShopUtils.GetMonthlyPassDailyRewardInfoList()
        end
        itemCells:Refresh(#rewardInfoList, function(itemCell, itemIndex)
            local rewardInfo = rewardInfoList[itemIndex]
            itemCell:InitItem({ id = rewardInfo.rewardId, count = rewardInfo.number }, true)
            itemCell:SetExtraInfo({
                tipsPosType = UIConst.UI_TIPS_POS_TYPE.LeftMid,
                tipsPosTransform = self.view.titleScrollList.transform,
                isSideTips = DeviceInfo.usingController,
            })
        end)
    end)

    if DeviceInfo.usingController then
        self.view.titleScrollListSelectableNaviGroup.onIsFocusedChange:AddListener(function(isFocused)
            if not isFocused then
                Notify(MessageConst.HIDE_ITEM_TIPS)
            end
        end)
    end
end

ShopMonthlyDetailCtrl._RefreshView = HL.Method() << function(self)
    self.view.numeberTxt.text = 1
    self.view.costTotalTxt.text = CashShopUtils.getGoodsPriceText(self.m_goodsId)
    self.view.nameTxt.text = CashShopUtils.GetCashGoodsName(self.m_goodsId)
    self.view.subTitleTxt.text = Language.LUA_CASHSHOP_GIRFPACK_DETAILPANEL_SUBTITLE
    local buttonStateCtrl = self.view.bottomNode
    local amountNodeStateCtrl = self.view.amountNode
    if CashShopUtils.CheckCanBuyMonthlyPass() then
        buttonStateCtrl:SetState("normal")
        amountNodeStateCtrl:SetState("normal")
    else
        buttonStateCtrl:SetState("lock")
        amountNodeStateCtrl:SetState("replensh")
        self.view.replenishTxt.text = Language.LUA_CASH_SHOP_MONTHLY_DETAIL_AMOUNT_SOLD_OUT_TEXT
    end

    local inventory = self.view.inventory
    local remainDay = GameInstance.player.monthlyPassSystem:GetRemainValidDays()
    if remainDay > 0 then
        inventory.simpleStateController:SetState("HaveBuy")
        if CashShopUtils.MonthlyCardRemainDayIsShort() then
            inventory.inventoryTxt:SetAndResolveTextStyle(string.format(
                Language.LUA_CASHSHOP_MONTHLYCARD_DETAILPANEL_REMAINDAYTEXT_RED, remainDay))
        else
            inventory.inventoryTxt:SetAndResolveTextStyle(string.format(
                Language.LUA_CASHSHOP_MONTHLYCARD_DETAILPANEL_REMAINDAYTEXT, remainDay))
        end
    else
        inventory.simpleStateController:SetState("NoBuy")
    end

    self.view.titleScrollList:UpdateCount(2)
end











ShopMonthlyDetailCtrl._TryBuyShop = HL.Method() << function(self)
    EventLogManagerInst:GameEvent_GoodsViewClick(
        "2",
        self.m_goodsInfo.cashShopId,
        CashShopConst.CashShopCategoryType.Pack,
        self.m_goodsId
    )
    CashShopUtils.createOrder(self.m_goodsId, CashShopUtils.GetCashShopIdByGoodsId(self.m_goodsId), 1)
    self:Close()
end

ShopMonthlyDetailCtrl._OnSetTop = HL.Method() << function(self)
    UIManager:SetTopOrder(PanelId.ShopMonthlyDetail)
end

ShopMonthlyDetailCtrl._OnCashShopOpenCategory = HL.Method() << function(self)
    self:Close()
end

ShopMonthlyDetailCtrl._OnCashShopReceiveRefreshMsg = HL.Method() << function(self)
    GameInstance.player.guide:OnShopRefreshItemInfo()
    Notify(MessageConst.SHOW_TOAST, Language.LUA_REFRESH_CLOSE_SHOP_TOAST)
    self:Close()
end

ShopMonthlyDetailCtrl.GetGoodsId = HL.Method().Return(HL.String) << function(self)
    return self.m_goodsId
end

HL.Commit(ShopMonthlyDetailCtrl)
