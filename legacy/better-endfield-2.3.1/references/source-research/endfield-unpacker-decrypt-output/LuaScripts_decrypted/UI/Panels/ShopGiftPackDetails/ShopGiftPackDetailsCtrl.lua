
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ShopGiftPackDetails

ShopGiftPackDetailsCtrl = HL.Class('ShopGiftPackDetailsCtrl', uiCtrl.UICtrl)

ShopGiftPackDetailsCtrl.m_goodsId = HL.Field(HL.String) << ""

ShopGiftPackDetailsCtrl.m_cashShopId = HL.Field(HL.String) << ""

ShopGiftPackDetailsCtrl.m_getCellFunc = HL.Field(HL.Function)

ShopGiftPackDetailsCtrl.m_rewardItemBundles = HL.Field(HL.Table)

ShopGiftPackDetailsCtrl.m_targetTime = HL.Field(HL.Number) << 0





ShopGiftPackDetailsCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_CASH_SHOP_PACK_SET_TOP] = '_OnSetTop',
    [MessageConst.ON_CASH_SHOP_OPEN_CATEGORY] = '_OnCashShopOpenCategory',
    [MessageConst.ON_CASH_SHOP_RECEIVE_REFRESH_MSG] = '_OnCashShopReceiveRefreshMsg',
}


ShopGiftPackDetailsCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_goodsId = arg.goodsId
    self.m_cashShopId = arg.cashShopId

    self:_BindUICallback()
    self:_InitRewardData()
    self:_RefreshView()

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
end

ShopGiftPackDetailsCtrl.OnShow = HL.Override() << function(self)
    Notify(MessageConst.ON_OPEN_CASH_SHOP_DETAILS)
end

ShopGiftPackDetailsCtrl.OnClose = HL.Override() << function(self)
    Notify(MessageConst.ON_CLOSE_CASH_SHOP_DETAILS)
end

ShopGiftPackDetailsCtrl._BindUICallback = HL.Method() << function(self)
    self.view.closeButton.onClick:RemoveAllListeners()
    self.view.closeButton.onClick:AddListener(function()
        self:PlayAnimationOut()
    end)

    self.view.btnCommonYellow.onClick:RemoveAllListeners()
    self.view.btnCommonYellow.onClick:AddListener(function()
        self:_TryBuyShop()
    end)

    self.m_getCellFunc = UIUtils.genCachedCellFunction(self.view.info.scrollList)
    self.view.info.scrollList.onUpdateCell:AddListener(function(obj, index)
        local cell = self.m_getCellFunc(obj)
        local itemBundle = self.m_rewardItemBundles[LuaIndex(index)]
        cell:InitItem({ id = itemBundle.id, count = itemBundle.count, forceHidePotentialStar = true }, true)
        cell:SetExtraInfo({
            tipsPosType = UIConst.UI_TIPS_POS_TYPE.LeftMid,
            tipsPosTransform = self.view.info.scrollList.transform,
            isSideTips = DeviceInfo.usingController,
        })
    end)

    if DeviceInfo.usingController then
        self.view.info.scrollListSelectableNaviGroup.onIsFocusedChange:AddListener(function(isFocused)
            if not isFocused then
                Notify(MessageConst.HIDE_ITEM_TIPS)
            end
        end)
    end
end

ShopGiftPackDetailsCtrl._InitRewardData = HL.Method() << function(self)
    self.m_rewardItemBundles = {}
    local succ, cfg = Tables.CashShopGoodsTable:TryGetValue(self.m_goodsId)
    if not succ then
        return
    end
    local rewardId = cfg.rewardId
    local getRewardCfgSucc, rewardsCfg = Tables.rewardTable:TryGetValue(rewardId)
    if getRewardCfgSucc then
        for _, itemBundle in pairs(rewardsCfg.itemBundles) do
            table.insert(self.m_rewardItemBundles, itemBundle)
        end
    end
end

ShopGiftPackDetailsCtrl._RefreshView = HL.Method() << function(self)
    local goodsData = GameInstance.player.cashShopSystem:GetGoodsData(self.m_goodsId)

    self.view.numeberTxt.text = 1
    self.view.costTotalTxt.text = CashShopUtils.getGoodsPriceText(self.m_goodsId)
    self.view.info.nameTxt.text = CashShopUtils.GetCashGoodsName(self.m_goodsId)
    self.view.info.subTitleTxt.text = Language.LUA_CASHSHOP_GIRFPACK_DETAILPANEL_SUBTITLE

    local canBuy = CashShopUtils.CheckCanBuyCashShopGoods(self.m_goodsId)
    local buttonStateCtrl = self.view.bottomNode
    if canBuy and CashShopUtils.CheckCashShopGoodsIsOpen(self.m_goodsId) then
        buttonStateCtrl:SetState("normal")
    else
        buttonStateCtrl:SetState("lock")
    end

    local amountNodeStateCtrl = self.view.amountNode
    if canBuy then
        amountNodeStateCtrl:SetState("normal")
    else
        amountNodeStateCtrl:SetState("replensh")
        self.view.replenishTxt.text = Language.LUA_CASH_SHOP_GIFTPACK_DETAIL_AMOUNT_SOLD_OUT_TEXT
    end

    local inventory = self.view.info.inventory

    local closeTimeStamp = goodsData.closeTimeStamp
    self.m_targetTime = closeTimeStamp
    local leftTime = closeTimeStamp - DateTimeUtils.GetCurrentTimestampBySeconds()
    if closeTimeStamp ~= 0 and leftTime > -1 then
        self:UpdateTimeByTargetTs()
        self:_StartCoroutine(function()
            coroutine.wait(1)
            self:UpdateTimeByTargetTs()
        end)
    else
        inventory.timeTag.gameObject:SetActive(false)
    end

    local haveRestriction = false
    local limitGoodsData = GameInstance.player.cashShopSystem:GetPlatformLimitGoodsData(self.m_goodsId)
    if limitGoodsData ~= nil and limitGoodsData.limitType == CS.Beyond.Gameplay.CashShopSystem.EPlatformLimitGoodsType.Common then
        local limitCount = limitGoodsData.limitCount
        local purchaseCount = limitGoodsData.purchaseCount
        local remain = limitCount - purchaseCount
        inventory.inventoryTag.inventoryTagTxt01.text = remain
        haveRestriction = true
    else
        inventory.inventoryTag.inventoryText.text = Language.LUA_SHOP_GIFTPACK_DETAIL_NO_RESTRICTION_TAG
    end
    inventory.inventoryTag.lineImage.gameObject:SetActive(haveRestriction)
    inventory.inventoryTag.inventoryTagTxt01.gameObject:SetActive(haveRestriction)

    local itemBtnCell = self.view.itemBtn
    local itemImage = itemBtnCell.itemIconImg3
    local path, imageName = CashShopUtils.GetGiftpackBigIcon(self.m_goodsId)
    itemImage:LoadSprite(path, imageName)
    local bg = itemBtnCell.itemIconImg3Bg
    local bgPath, bgIcon = CashShopUtils.GetGiftpackBigBg(self.m_goodsId)
    bg:LoadSprite(bgPath, bgIcon)

    local tagWidget = itemBtnCell.cashShopItemTag
    tagWidget:InitCashShopItemTag({
        isCashShop = true,
        shopGoodsInfo = {
            goodsId = self.m_goodsId,
            goodsData = goodsData,
        },
        hideRestriction = true,
        hideTime = true,
    })

    self.view.info.scrollList:UpdateCount(#self.m_rewardItemBundles)
end

ShopGiftPackDetailsCtrl.UpdateTimeByTargetTs = HL.Method() << function(self)
    local closeTimeStamp = self.m_targetTime
    local leftTime = closeTimeStamp - DateTimeUtils.GetCurrentTimestampBySeconds()
    if leftTime > -1 then
        local stateName
        if leftTime > 3600 * 24 * 3 then
            stateName = "Green"
        elseif leftTime <= 3600 * 24 * 3 and leftTime > 3600 * 24 then
            stateName = "Yellow"
        else
            stateName = "Red"
        end
        self.view.info.inventory.timeTag.stateController:SetState(stateName)
        self.view.info.inventory.timeTag.timeTagTxt.text = UIUtils.getShortLeftTime(leftTime)
    else
        self.view.info.inventory.timeTag.gameObject:SetActive(false)
    end
end

ShopGiftPackDetailsCtrl._TryBuyShop = HL.Method() << function(self)
    EventLogManagerInst:GameEvent_GoodsViewClick(
        "2",
        self.m_cashShopId,
        CashShopConst.CashShopCategoryType.Pack,
        self.m_goodsId
    )
    CashShopUtils.createOrder(self.m_goodsId, CashShopUtils.GetCashShopIdByGoodsId(self.m_goodsId), 1)
    self:Close()
end

ShopGiftPackDetailsCtrl._OnSetTop = HL.Method() << function(self)
    UIManager:SetTopOrder(PanelId.ShopGiftPackDetails)
end







ShopGiftPackDetailsCtrl._OnCashShopOpenCategory = HL.Method() << function(self)
    self:Close()
end

ShopGiftPackDetailsCtrl._OnCashShopReceiveRefreshMsg = HL.Method() << function(self)
    GameInstance.player.guide:OnShopRefreshItemInfo()
    Notify(MessageConst.SHOW_TOAST, Language.LUA_REFRESH_CLOSE_SHOP_TOAST)
    self:Close()
end

ShopGiftPackDetailsCtrl.GetGoodsId = HL.Method().Return(HL.String) << function(self)
    return self.m_goodsId
end

HL.Commit(ShopGiftPackDetailsCtrl)
