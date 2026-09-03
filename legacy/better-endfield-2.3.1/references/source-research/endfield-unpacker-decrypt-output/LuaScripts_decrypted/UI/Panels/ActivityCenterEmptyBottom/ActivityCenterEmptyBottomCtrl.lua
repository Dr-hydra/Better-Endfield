
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ActivityCenterEmptyBottom

ActivityCenterEmptyBottomCtrl = HL.Class('ActivityCenterEmptyBottomCtrl', uiCtrl.UICtrl)






ActivityCenterEmptyBottomCtrl.s_messages = HL.StaticField(HL.Table) << {

}


ActivityCenterEmptyBottomCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
end











HL.Commit(ActivityCenterEmptyBottomCtrl)
