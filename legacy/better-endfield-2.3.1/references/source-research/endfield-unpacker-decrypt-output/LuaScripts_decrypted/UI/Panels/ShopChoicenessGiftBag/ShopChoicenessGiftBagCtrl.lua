
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ShopChoicenessGiftBag

ShopChoicenessGiftBagCtrl = HL.Class('ShopChoicenessGiftBagCtrl', uiCtrl.UICtrl)

ShopChoicenessGiftBagCtrl.m_tabData = HL.Field(HL.Table)





ShopChoicenessGiftBagCtrl.s_messages = HL.StaticField(HL.Table) << {

}


ShopChoicenessGiftBagCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_tabData = arg

    local cashGoodsId = self.m_tabData.cashGoodsIds[1]
    if cashGoodsId == nil then
        logger.error("表格中缺少配置:" .. self.m_tabData.id)
        return
    end

    self.view.button.onClick:RemoveAllListeners()
    self.view.button.onClick:AddListener(function()
        self.m_phase:OpenGiftpackCategoryAndOpenDetailPanel(cashGoodsId, self.m_tabData.id)
    end)

    self.view.txtName:SetAndResolveTextStyle(CashShopUtils.GetCashGoodsName(cashGoodsId))
    local startDate, startTime = CashShopUtils.GetCashGoodsStartDateAndTime(cashGoodsId)
    local endDate, endTime = CashShopUtils.GetCashGoodsEndDateAndTime(cashGoodsId)
    if startDate and endDate then
        self.view.allottedTimeNode.gameObject:SetActive(true)
        self.view.startDateTxt:SetAndResolveTextStyle(startDate)
        self.view.startTimeTxt:SetAndResolveTextStyle(Utils.appendUTC(startTime))
        self.view.endDateTxt:SetAndResolveTextStyle(endDate)
        self.view.endTimeTxt:SetAndResolveTextStyle(Utils.appendUTC(endTime))
    else
        self.view.allottedTimeNode.gameObject:SetActive(false)
    end

    self.view.priceTxt:SetAndResolveTextStyle(CashShopUtils.getGoodsPriceText(cashGoodsId))

    self.view.cashShopKrTips:InitCashShopKrTips()
end

ShopChoicenessGiftBagCtrl.OnShow = HL.Override() << function(self)
    GameInstance.player.cashShopSystem:ReadCashGoods(self.m_tabData.cashGoodsIds[1])
end








HL.Commit(ShopChoicenessGiftBagCtrl)
