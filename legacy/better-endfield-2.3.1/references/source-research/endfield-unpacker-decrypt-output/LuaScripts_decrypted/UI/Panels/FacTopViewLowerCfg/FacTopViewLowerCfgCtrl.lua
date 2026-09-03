




local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FacTopViewLowerCfg
FacTopViewLowerCfgCtrl = HL.Class('FacTopViewLowerCfgCtrl', uiCtrl.UICtrl)







FacTopViewLowerCfgCtrl.s_messages = HL.StaticField(HL.Table) << {

}


FacTopViewLowerCfgCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
end

FacTopViewLowerCfgCtrl.OnToggleFacTopView = HL.StaticMethod(HL.Boolean) << function(active)
    if active then
        UIManager:AutoOpen(PANEL_ID)
    else
        UIManager:Hide(PANEL_ID)
    end
end

HL.Commit(FacTopViewLowerCfgCtrl)
