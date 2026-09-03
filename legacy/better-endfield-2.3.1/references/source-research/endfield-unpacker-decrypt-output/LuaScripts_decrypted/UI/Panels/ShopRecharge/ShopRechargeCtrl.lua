
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ShopRecharge

ShopRechargeCtrl = HL.Class('ShopRechargeCtrl', uiCtrl.UICtrl)






ShopRechargeCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_SDK_PRODUCT_INFO_UPDATE] = '_Refresh',
    [MessageConst.ON_CASH_SHOP_PLATFORM_DATA_REFRESH] = '_Refresh',
}

ShopRechargeCtrl.m_cashShopId = HL.Field(HL.String) << ''

ShopRechargeCtrl.m_cashShopSystem = HL.Field(HL.Userdata)

ShopRechargeCtrl.m_gemCellCache = HL.Field(HL.Forward("UIListCache"))

ShopRechargeCtrl.m_haveSetNaviTarget = HL.Field(HL.Boolean) << false


ShopRechargeCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_phase:ShowPsStore()

    if CashShopUtils.IsPS() then
        self.view.stateCtrl:SetState("Controller")
    else
        self.view.stateCtrl:SetState("Mobile")
    end

    local _, shopGroupData = Tables.cashShopGroupTable:TryGetValue(CashShopConst.CashShopCategoryType.Recharge)
    if not shopGroupData or shopGroupData.cashShopIds.Count == 0 then
        logger.error("ShopRechargeCtrl.OnCreate no shop for id:", CashShopConst.CashShopCategoryType.Recharge)
        return
    end

    self.m_cashShopId = shopGroupData.cashShopIds[0]
    self.m_cashShopSystem = GameInstance.player.cashShopSystem
    self:_InitAction()
    self:_Refresh()


    self:_StartCoroutine(function()
        self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({
            self.view.inputGroup.groupId,
            self.m_phase.m_panel2Item[PanelId.CashShop].uiCtrl.view.inputGroup.groupId,
        })
    end)
end

ShopRechargeCtrl.OnShow = HL.Override() << function(self)
    Notify(MessageConst.CASH_SHOP_SHOW_WALLET_BAR, {
        moneyIds = {Tables.globalConst.originiumItemId, Tables.globalConst.diamondItemId},
    })

    if self.m_phase.m_needGameEvent then
        self.m_phase.m_needGameEvent = false
        EventLogManagerInst:GameEvent_ShopEnter(
            self.m_phase.m_enterButton,
            self.m_phase.m_enterPanel,
            self.m_cashShopId,
            CashShopConst.CashShopCategoryType.Recharge,
            ""
        )
    else
        EventLogManagerInst:GameEvent_ShopPageView(
            self.m_cashShopId,
            CashShopConst.CashShopCategoryType.Recharge,
            ""
        )
    end
end

ShopRechargeCtrl.OnClose = HL.Override() << function(self)
end

ShopRechargeCtrl.SetCashShopStateArg = HL.Method(HL.Table) << function(self, arg)

end



ShopRechargeCtrl.OnAfterCategoryTopOrdered = HL.Method() << function(self)
end

ShopRechargeCtrl._InitAction = HL.Method() << function(self)
    self.m_gemCellCache = UIUtils.genCellCache(self.view.shopGemCell)

    local isJP = I18nUtils.curEnvLang == GEnums.EnvLang.JP and CS.Beyond.SDK.SDKConsts.IsOverseaVersion()
    local isKR = I18nUtils.curEnvLang == GEnums.EnvLang.KR and CS.Beyond.SDK.SDKConsts.IsOverseaVersion()
    self.view.btnNodeJP.gameObject:SetActive(isJP)
    self.view.btnNodeKR.gameObject:SetActive(isKR)
    if isJP then
        self.view.jP_PSA.functionBtn.onClick:AddListener(function()
            Utils.openURL(Tables.cashShopConst.urlPaymentServicesAct)
        end)
        self.view.jP_SCTA.functionBtn.onClick:AddListener(function()
            Utils.openURL(Tables.cashShopConst.urlSpecifiedCommercialTransactionsAct)
        end)
        self.view.btnNodeJP.onIsFocusedChange:AddListener(function(isFocused)
            self.view.controllerFocusHintNode.gameObject:SetActive(not isFocused)
        end)
    end
    if isKR then
        self.view.kR_CP.functionBtn.onClick:AddListener(function()
            CashShopUtils.ShowKrUrl()
        end)
    end
end

ShopRechargeCtrl._Refresh = HL.Method() << function(self)

    local csShopData = self.m_cashShopSystem:GetShopData(self.m_cashShopId)
    if not csShopData then
        self.m_gemCellCache:Refresh(0)
        return
    end

    local goodsList = GameInstance.player.cashShopSystem:GetGoodsList(csShopData)

    local goodsTable = {}
    for i = 0, goodsList.Count - 1 do
        local csGoodData = goodsList[i]
        local goodsId = csGoodData.goodsId
        local _, tblGoodData = Tables.cashShopGoodsTable:TryGetValue(goodsId)
        if tblGoodData then
            table.insert(goodsTable, tblGoodData)
        end
    end
    table.sort(goodsTable, Utils.genSortFunction({"priceCNY"}, true))

    self.m_gemCellCache:Refresh(#goodsTable, function(cell, index)
        if index == 1 and not self.m_haveSetNaviTarget then
            self.m_haveSetNaviTarget = true
            self:SetNaviTarget(cell.button)
        end
        local tblGoodData = goodsTable[index]
        local goodsId = tblGoodData.cashGoodsId
        local _, rechargeData = Tables.cashShopRechargeTable:TryGetValue(goodsId)
        if not tblGoodData or not rechargeData then
            logger.error("ShopRechargeCtrl._Refresh Table no GoodData for id:", goodsId)
            return
        end

        local limitData = self.m_cashShopSystem:GetPlatformLimitGoodsData(goodsId)
        local isBonus = limitData and limitData.limitType == CS.Beyond.Gameplay.CashShopSystem.EPlatformLimitGoodsType.OnceBonus
                            and limitData.purchaseCount == 0
        local stateName = "Normal"
        local isSpecialBonus = rechargeData.rewardTimes > 2
        if isBonus then
            stateName = isSpecialBonus and "Special" or "Double"
        end
        cell.stateController:SetState(stateName)
        cell.bgImg:LoadSprite(UIConst.UI_SPRITE_CASH_SHOP_GEM, tblGoodData.iconId)
        local rewardItem = UIUtils.getRewardFirstItem(tblGoodData.rewardId)
        cell.txtCount.text = tostring(rewardItem.count)
        cell.paidOriginium.gameObject:SetActive(true)
        if isBonus then
            if isSpecialBonus then
                cell.paidOriginium.gameObject:SetActive(false)
                cell.specialNode.txtTimesCount.text = tostring(rewardItem.count * rechargeData.rewardTimes)
            else
                cell.doubleNode.txtTimesCount.text = tostring(rewardItem.count * (rechargeData.rewardTimes - 1))
            end
        else
            local bonusRewardItem = UIUtils.getRewardFirstItem(rechargeData.bonusRewardId)
            cell.txtBonusCount.text = tostring(bonusRewardItem.count)
        end
        cell.txtName.text = tblGoodData.goodsName
        cell.txtPrice.text = CashShopUtils.getGoodsPriceText(goodsId)
        cell.button.onClick:RemoveAllListeners()
        cell.button.onClick:AddListener(function()
            EventLogManagerInst:GameEvent_GoodsViewClick(
                "2",
                self.m_cashShopId,
                CashShopConst.CashShopCategoryType.Recharge,
                goodsId
            )
            CashShopUtils.createOrder(goodsId, self.m_cashShopId, 1)
        end)
        cell.button.customBindingViewLabelText = Language.LUA_CASH_SHOP_RECHARGE_PANEL_BUTTON_BUTTON_KEY_HINT

        local animWrapper = cell.gameObject:GetComponent("UIAnimationWrapper")
        if index == 1 then
            animWrapper:PlayInAnimation()
        else
            animWrapper:SampleToInAnimationBegin()
            local diff = self.view.config.CELL_FADE_IN_DIFF_TIME
            local time = (index - 1) * diff
            self:_StartCoroutine(function()
                coroutine.wait(time)
                animWrapper:PlayInAnimation()
            end)
        end
    end)

end

HL.Commit(ShopRechargeCtrl)
