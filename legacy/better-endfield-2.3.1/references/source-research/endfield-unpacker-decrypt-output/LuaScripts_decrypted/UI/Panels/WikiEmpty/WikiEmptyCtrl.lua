
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.WikiEmpty

WikiEmptyCtrl = HL.Class('WikiEmptyCtrl', uiCtrl.UICtrl)






WikiEmptyCtrl.s_messages = HL.StaticField(HL.Table) << {

}


WikiEmptyCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)



    if DeviceInfo.usingController then
        self:SetNaviTarget(self.view.dummyNaviBtn)
    end
end

HL.Commit(WikiEmptyCtrl)
