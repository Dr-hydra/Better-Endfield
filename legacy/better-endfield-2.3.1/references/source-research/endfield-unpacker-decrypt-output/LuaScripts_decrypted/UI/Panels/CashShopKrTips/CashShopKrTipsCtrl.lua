local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.CashShopKrTips

CashShopKrTipsCtrl = HL.Class('CashShopKrTipsCtrl', uiCtrl.UICtrl)






CashShopKrTipsCtrl.s_messages = HL.StaticField(HL.Table) << {

}


CashShopKrTipsCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.cashShopKrTips:InitCashShopKrTips()
end











HL.Commit(CashShopKrTipsCtrl)
