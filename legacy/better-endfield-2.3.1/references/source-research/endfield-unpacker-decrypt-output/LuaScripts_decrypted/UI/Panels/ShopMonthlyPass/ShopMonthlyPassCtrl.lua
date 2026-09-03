
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ShopMonthlyPass

ShopMonthlyPassCtrl = HL.Class('ShopMonthlyPassCtrl', uiCtrl.UICtrl)

ShopMonthlyPassCtrl.m_shopGoodsId = HL.Field(HL.String) << ""

ShopMonthlyPassCtrl.m_cashShopId = HL.Field(HL.String) << ""


ShopMonthlyPassCtrl.m_isRecommend = HL.Field(HL.Boolean) << true

ShopMonthlyPassCtrl.m_recommendId = HL.Field(HL.String) << ""





ShopMonthlyPassCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_SYNC_MONTHLY_CARD_DATA] = '_OnSyncData',
}


ShopMonthlyPassCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    if arg == nil then
        return
    end

    if arg.goodsId ~= nil then
        self.m_shopGoodsId = arg.goodsId
    end

    if arg.cashShopId ~= nil then
        self.m_cashShopId = arg.cashShopId
    end

    if arg.recommendId ~= nil then
        self.m_recommendId = arg.recommendId
    end

    if arg.isRecommend ~= nil and arg.isRecommend == true then
        self.m_isRecommend = true
        self.view.contentState:SetState("Recommend")
    else
        self.m_isRecommend = false
        self.view.contentState:SetState("Combination")

        EventLogManagerInst:GameEvent_GoodsViewClick(
            "1",
            self.m_cashShopId,
            CashShopConst.CashShopCategoryType.Pack,
            self.m_shopGoodsId
        )
    end

    self.view.advertisingTxt.text = Language.LUA_MONTHLYPASS_RECOMMEND_TEXT
    self.view.checkBtnNode.onClick:AddListener(function()
        self:_OnDetailBtnClick()
    end)

    self.view.descriptionBtn.onClick:RemoveAllListeners()
    self.view.descriptionBtn.onClick:AddListener(function()
        UIManager:Open(PanelId.InstructionBook, "ShopPackage_ShopMonthlyPass")
    end)

    self.view.buyBtn.onClick:RemoveAllListeners()
    self.view.buyBtn.onClick:AddListener(function()
        self:_OnBuyBtnClick()
    end)

    self:_RefreshUI()


    if not self.m_isRecommend then

        self:_StartCoroutine(function()
            local cashShopCtrl = self.m_phase.m_panel2Item[PanelId.CashShop].uiCtrl
            self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({
                self.view.inputGroup.groupId,
                cashShopCtrl.view.inputGroup.groupId,
            })
        end)
    end
end

ShopMonthlyPassCtrl.OnShow = HL.Override() << function(self)
    GameInstance.player.cashShopSystem:ReadCashGoods(self.m_shopGoodsId)
end









ShopMonthlyPassCtrl._RefreshUI = HL.Method() << function(self)
    local remainDay = GameInstance.player.monthlyPassSystem:GetRemainValidDays()

    self.view.buyNumTxt.text = CashShopUtils.getGoodsPriceText(self.m_shopGoodsId)
    self.view.btnNameTxt.text = remainDay <= 0 and
        Language.LUA_MONTHLY_PASS_BUY_BTN_TEXT_1 or Language.LUA_MONTHLY_PASS_BUY_BTN_TEXT_2

    self:_StartCoroutine(function()
        self.view.buyBtn.customBindingViewLabelText = remainDay <= 0 and
            Language.LUA_MONTHLY_PASS_BUY_BTN_KEYHINT_TEXT_1 or Language.LUA_MONTHLY_PASS_BUY_BTN_KEYHINT_TEXT_2
    end)

    local monthlyPassId = Tables.CashShopConst.currentMonthlycardId
    self.view.monthlyPassNameTxt.text = CashShopUtils.GetMonthlyPassName()

    if remainDay > 0 then
        self.view.remainingDaysLayout:SetState("HavePass")
        self.view.dayNumTxt.text = remainDay
        if remainDay <= self.view.config.DAY_SHORT_THRESHOLD then
            self.view.dayNumTxt.color = self.view.config.DAY_COLOR_SHORT_DATED
        else
            self.view.dayNumTxt.color = self.view.config.DAY_COLOR_NORMAL
        end
    else
        self.view.remainingDaysLayout:SetState("NoPass")
    end
end

ShopMonthlyPassCtrl._OnDetailBtnClick = HL.Method() << function(self)
    self.m_phase:OpenGiftpackCategoryAndOpenDetailPanel(self.m_shopGoodsId, self.m_recommendId, false)
end

ShopMonthlyPassCtrl._OnBuyBtnClick = HL.Method() << function(self)
    if self.m_isRecommend then

        self.m_phase:OpenGiftpackCategoryAndOpenDetailPanel(self.m_shopGoodsId, self.m_recommendId)
    else

        CashShopUtils.TryBuyMonthlyPass(self.m_shopGoodsId, self.m_cashShopId)
    end

end

ShopMonthlyPassCtrl._OnSyncData = HL.Method() << function(self)
    self:_RefreshUI()
end

ShopMonthlyPassCtrl.OnSyncMonthlyCardData = HL.StaticMethod() << function()

    logger.info("[cashshop] ShopMonthlyPassCtrl.OnSyncMonthlyCardData")
    CashShopUtils.TryAddMonthlyPassToMainHUDQueue()
end

HL.Commit(ShopMonthlyPassCtrl)
