
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.DebugText
DebugTextCtrl = HL.Class('DebugTextCtrl', uiCtrl.UICtrl)





DebugTextCtrl.s_messages = HL.StaticField(HL.Table) << {
}


DebugTextCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
end

DebugTextCtrl.UpdateDebugText = HL.StaticMethod(HL.Any) << function(str)
    if type(str) == "table" then
        str = unpack(str)
    end
    local ctrl = DebugTextCtrl.AutoOpen(PANEL_ID, nil, false)
    ctrl:_UpdateDebugText(str)
end

DebugTextCtrl._UpdateDebugText = HL.Method(HL.Any) << function(self, str)
    self.view.text.text = str
end

HL.Commit(DebugTextCtrl)
