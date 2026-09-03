
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ShopCredit

ShopCreditCtrl = HL.Class('ShopCreditCtrl', uiCtrl.UICtrl)






ShopCreditCtrl.s_messages = HL.StaticField(HL.Table) << {

}


ShopCreditCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
end











HL.Commit(ShopCreditCtrl)
