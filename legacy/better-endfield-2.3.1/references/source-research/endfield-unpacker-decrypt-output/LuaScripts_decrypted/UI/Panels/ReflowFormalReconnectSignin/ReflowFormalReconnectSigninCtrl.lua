local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ReflowFormalReconnectSignin

ReflowFormalReconnectSigninCtrl = HL.Class('ReflowFormalReconnectSigninCtrl', uiCtrl.UICtrl)






ReflowFormalReconnectSigninCtrl.s_messages = HL.StaticField(HL.Table) << {

}


ReflowFormalReconnectSigninCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
end











HL.Commit(ReflowFormalReconnectSigninCtrl)
