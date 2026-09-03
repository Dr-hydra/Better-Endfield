
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.WeaponExhibitEmpty
WeaponExhibitEmptyCtrl = HL.Class('WeaponExhibitEmptyCtrl', uiCtrl.UICtrl)






WeaponExhibitEmptyCtrl.s_messages = HL.StaticField(HL.Table) << {

}


WeaponExhibitEmptyCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
end

HL.Commit(WeaponExhibitEmptyCtrl)
