
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ShopRecommendEmptyGiftPack

ShopRecommendEmptyGiftPackCtrl = HL.Class('ShopRecommendEmptyGiftPackCtrl', uiCtrl.UICtrl)

ShopRecommendEmptyGiftPackCtrl.m_tabData = HL.Field(HL.Table)

ShopRecommendEmptyGiftPackCtrl.m_go = HL.Field(HL.Any)





ShopRecommendEmptyGiftPackCtrl.s_messages = HL.StaticField(HL.Table) << {

}


ShopRecommendEmptyGiftPackCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_tabData = arg

    local cashGoodsId = self.m_tabData.cashGoodsIds[1]
    if cashGoodsId == nil then
        logger.error("表格中缺少配置:" .. self.m_tabData.id)
        return
    end


    if not string.isEmpty(self.m_tabData.prefabName) then
        self.m_go = self:_CreateGO(self.m_tabData.prefabName)
    end


    self:_SetupUI()

    self.view.cashShopKrTips:InitCashShopKrTips()
end








ShopRecommendEmptyGiftPackCtrl._CreateGO = HL.Method(HL.String).Return(GameObject) << function(self, prefabName)
    local path = string.format(UIConst.UI_CASH_SHOP_CUSTOM_GIFT_PANEL_WIDGETS_PATH, prefabName)
    local goAsset = self:LoadGameObject(path)
    local go = CSUtils.CreateObject(goAsset, self.view.widgetRoot.transform)
    return go
end

ShopRecommendEmptyGiftPackCtrl._SetupUI = HL.Method() << function(self)
    local widget = {}
    local luaRef = self.m_go.transform:GetComponent("LuaReference")
    luaRef:BindToLua(widget)
    local cashGoodsId = self.m_tabData.cashGoodsIds[1]

    local button = widget.button
    if button then
        button.onClick:RemoveAllListeners()
        button.onClick:AddListener(function()
            self.m_phase:OpenGiftpackCategoryAndOpenDetailPanel(cashGoodsId, self.m_tabData.id)
        end)
    end

    local name = widget.txtName
    if name then
        name:SetAndResolveTextStyle(CashShopUtils.GetCashGoodsName(cashGoodsId))
    end

    local priceTxt = widget.priceTxt
    if priceTxt then
        priceTxt:SetAndResolveTextStyle(CashShopUtils.getGoodsPriceText(cashGoodsId))
    end

    local endTimeTxt = widget.endTimeTxt
    local endDate, endTime = CashShopUtils.GetCashGoodsEndDateAndTime(cashGoodsId)
    if endTimeTxt and endDate then
        endTimeTxt:SetAndResolveTextStyle(endDate .. " " .. Utils.appendUTC(endTime))
    end
end




HL.Commit(ShopRecommendEmptyGiftPackCtrl)
