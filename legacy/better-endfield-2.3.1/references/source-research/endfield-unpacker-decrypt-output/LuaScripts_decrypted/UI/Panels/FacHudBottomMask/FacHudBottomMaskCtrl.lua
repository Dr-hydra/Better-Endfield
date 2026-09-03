
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FacHudBottomMask
FacHudBottomMaskCtrl = HL.Class('FacHudBottomMaskCtrl', uiCtrl.UICtrl)





FacHudBottomMaskCtrl.s_messages = HL.StaticField(HL.Table) << {

}


FacHudBottomMaskCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
end











HL.Commit(FacHudBottomMaskCtrl)
