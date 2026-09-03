
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.MapMarkDetailDefault

MapMarkDetailDefaultCtrl = HL.Class('MapMarkDetailDefaultCtrl', uiCtrl.UICtrl)






MapMarkDetailDefaultCtrl.s_messages = HL.StaticField(HL.Table) << {

}


MapMarkDetailDefaultCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    local commonArgs = {}
    commonArgs.bigBtnActive = true
    commonArgs.markInstId = args.markInstId
    self.view.detailCommon:InitMapMarkDetailCommon(commonArgs)
end

HL.Commit(MapMarkDetailDefaultCtrl)
