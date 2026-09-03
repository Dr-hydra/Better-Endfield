
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.GachaCharResultBG

GachaCharResultBGCtrl = HL.Class('GachaCharResultBGCtrl', uiCtrl.UICtrl)






GachaCharResultBGCtrl.s_messages = HL.StaticField(HL.Table) << {

}


GachaCharResultBGCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
end











HL.Commit(GachaCharResultBGCtrl)
