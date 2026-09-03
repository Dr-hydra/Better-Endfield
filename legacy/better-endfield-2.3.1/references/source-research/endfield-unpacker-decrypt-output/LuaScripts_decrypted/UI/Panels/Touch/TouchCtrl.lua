local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.Touch
TouchCtrl = HL.Class('TouchCtrl', uiCtrl.UICtrl)





TouchCtrl.s_messages = HL.StaticField(HL.Table) << {

}


TouchCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    logger.info("TouchCtrl.OnCreate")
end

HL.Commit(TouchCtrl)
