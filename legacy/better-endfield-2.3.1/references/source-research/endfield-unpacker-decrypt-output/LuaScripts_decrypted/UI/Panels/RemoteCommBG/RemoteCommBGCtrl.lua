
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.RemoteCommBG
RemoteCommBGCtrl = HL.Class('RemoteCommBGCtrl', uiCtrl.UICtrl)





RemoteCommBGCtrl.s_messages = HL.StaticField(HL.Table) << {

}


RemoteCommBGCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
end











HL.Commit(RemoteCommBGCtrl)
