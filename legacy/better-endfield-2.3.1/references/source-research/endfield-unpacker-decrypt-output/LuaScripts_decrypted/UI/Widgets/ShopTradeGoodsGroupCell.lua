local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

ShopTradeGoodsGroupCell = HL.Class('ShopTradeGoodsGroupCell', UIWidgetBase)



ShopTradeGoodsGroupCell.m_goodsCellCache = HL.Field(HL.Forward("UIListCache"))




ShopTradeGoodsGroupCell._OnFirstTimeInit = HL.Override() << function(self)
    self:_InitUI()
end

ShopTradeGoodsGroupCell.InitShopTradeGoodsGroupCell = HL.Method() << function(self)
    self:_FirstTimeInit()
end






ShopTradeGoodsGroupCell._InitUI = HL.Method() << function(self)
    self.m_goodsCellCache = UIUtils.genCellCache(self.view.goodsCell)
end

ShopTradeGoodsGroupCell.SetTitleCommonUI = HL.Method(HL.String, HL.String, HL.Boolean) << function(self, titleName, titleIcon, hideDeco)
    self.view.goodsTagTxt.text = titleName
    self.view.goodsTagImg:LoadSprite(UIConst.UI_SPRITE_SHOP_TAG_ICON, titleIcon)
    self.view.titleStateCtrl:SetState(hideDeco and "NoDecoState" or "NormalState")
end


HL.Commit(ShopTradeGoodsGroupCell)
return ShopTradeGoodsGroupCell

