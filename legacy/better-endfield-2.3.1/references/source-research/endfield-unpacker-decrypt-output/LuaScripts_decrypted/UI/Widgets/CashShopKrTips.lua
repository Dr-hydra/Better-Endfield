local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

CashShopKrTips = HL.Class('CashShopKrTips', UIWidgetBase)


CashShopKrTips._OnFirstTimeInit = HL.Override() << function(self)
    self.view.buyillustrateBtn.onClick:AddListener(function()
        CashShopUtils.ShowKrUrl()
    end)
end

CashShopKrTips.InitCashShopKrTips = HL.Method() << function(self)
    self:_FirstTimeInit()

    local show = CashShopUtils.ShowKrUrlBtn()
    self.view.gameObject:SetActive(show)
end

HL.Commit(CashShopKrTips)
return CashShopKrTips

