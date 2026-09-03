
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.TestNavi
TestNaviCtrl = HL.Class('TestNaviCtrl', uiCtrl.UICtrl)







TestNaviCtrl.s_messages = HL.StaticField(HL.Table) << {

}


TestNaviCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:BindInputPlayerAction("common_cancel", function()
        self:Close()
    end)

    self:_InitLayoutTestNode()
end

TestNaviCtrl._InitLayoutTestNode = HL.Method() << function(self)
    self.view.shopScrollList:UpdateCount(30)





























end

HL.Commit(TestNaviCtrl)
