
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ShopDynamicGift

ShopDynamicGiftCtrl = HL.Class('ShopDynamicGiftCtrl', uiCtrl.UICtrl)

ShopDynamicGiftCtrl.m_tabData = HL.Field(HL.Table)

ShopDynamicGiftCtrl.m_go = HL.Field(HL.Any)





ShopDynamicGiftCtrl.s_messages = HL.StaticField(HL.Table) << {

}


ShopDynamicGiftCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
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
    local endDate, endTime = CashShopUtils.GetCashGoodsEndDateAndTime(cashGoodsId)
    if endDate then
        self.view.detailHorizontal01:SetState("haveTime")
        self.view.endDateTxt:SetAndResolveTextStyle(endDate)
        self.view.endTimeTxt:SetAndResolveTextStyle(Utils.appendUTC(endTime))
    else
        self.view.detailHorizontal01:SetState("noTime")
    end

    self.view.priceTxt:SetAndResolveTextStyle(CashShopUtils.getGoodsPriceText(cashGoodsId))


    if not string.isEmpty(self.m_tabData.prefabName) then
        self.m_go = self:_CreateGO(self.m_tabData.prefabName)
    end

    self.view.cashShopKrTips:InitCashShopKrTips()

    if CashShopUtils.IsPS() and CashShopUtils.TabDataShowPsStore(self.m_tabData) then
        self.view.stateController:SetState("InPsStore")
    else
        self.view.stateController:SetState("NoPsStore")
    end
end

ShopDynamicGiftCtrl.OnShow = HL.Override() << function(self)
    GameInstance.player.cashShopSystem:ReadCashGoods(self.m_tabData.cashGoodsIds[1])
end

ShopDynamicGiftCtrl._OnPlayAnimationOut = HL.Override() << function(self)
    ShopDynamicGiftCtrl.Super._OnPlayAnimationOut(self)
    if self.m_go then
        local animationWrapper = self.m_go.gameObject:GetComponent("UIAnimationWrapper")
        if animationWrapper then
            animationWrapper:PlayOutAnimation()
        end
    end
end









ShopDynamicGiftCtrl._CreateGO = HL.Method(HL.String).Return(GameObject) << function(self, prefabName)
    local path = string.format(UIConst.UI_CASH_SHOP_DYNAMIC_GIFT_PANEL_WIDGETS_PATH, prefabName)
    local goAsset = self:LoadGameObject(path)
    local go = CSUtils.CreateObject(goAsset, self.view.seasonalGiftpackRoot.transform)
    return go
end

HL.Commit(ShopDynamicGiftCtrl)
