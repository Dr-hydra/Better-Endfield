
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.WatchController

WatchControllerCtrl = HL.Class('WatchControllerCtrl', uiCtrl.UICtrl)






WatchControllerCtrl.s_messages = HL.StaticField(HL.Table) << {

}


WatchControllerCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder(arg.groupId)
end











HL.Commit(WatchControllerCtrl)
