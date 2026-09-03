
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.MapTransitionMask

MapTransitionMaskCtrl = HL.Class('MapTransitionMaskCtrl', uiCtrl.UICtrl)






MapTransitionMaskCtrl.s_messages = HL.StaticField(HL.Table) << {

}


MapTransitionMaskCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
end











HL.Commit(MapTransitionMaskCtrl)
