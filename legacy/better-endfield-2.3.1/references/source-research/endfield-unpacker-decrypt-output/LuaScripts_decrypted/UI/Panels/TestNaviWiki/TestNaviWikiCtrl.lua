
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.TestNaviWiki
TestNaviWikiCtrl = HL.Class('TestNaviWikiCtrl', uiCtrl.UICtrl)







TestNaviWikiCtrl.s_messages = HL.StaticField(HL.Table) << {

}


TestNaviWikiCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:BindInputPlayerAction("common_confirm", function()
        self:SetNaviTarget(self.view.leftBtn)
    end)
    self:BindInputPlayerAction("common_cancel", function()
        self:Close()
    end)
end











HL.Commit(TestNaviWikiCtrl)
