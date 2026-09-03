
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.CharInfoCursor
CharInfoCursorCtrl = HL.Class('CharInfoCursorCtrl', uiCtrl.UICtrl)





CharInfoCursorCtrl.s_messages = HL.StaticField(HL.Table) << {

}


CharInfoCursorCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
end











HL.Commit(CharInfoCursorCtrl)
