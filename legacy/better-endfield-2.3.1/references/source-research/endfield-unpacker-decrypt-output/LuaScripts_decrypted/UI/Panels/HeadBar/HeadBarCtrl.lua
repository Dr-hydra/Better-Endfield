
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.HeadBar
HeadBarCtrl = HL.Class('HeadBarCtrl', uiCtrl.UICtrl)







HeadBarCtrl.s_messages = HL.StaticField(HL.Table) << { }


HeadBarCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)

    self:_CreateWorldObjectRoot(false )
    self.view.csHeadBarCtrl:OnCreate(self.m_worldRoot)
end

HeadBarCtrl.OnClose = HL.Override() << function(self)
    self.view.csHeadBarCtrl:OnClose()
end

HeadBarCtrl.OnHide = HL.Override() << function(self)
    self.view.csHeadBarCtrl:OnHide()
    self.view.csHeadBarCtrl.enabled = false
    self.m_worldRoot.transform.position = Vector3(10000, 10000, 10000)
end

HeadBarCtrl.OnShow = HL.Override() << function(self)
    self.view.csHeadBarCtrl:OnShow()
    self.m_worldRoot.transform.position = Vector3.zero
    self.view.csHeadBarCtrl.enabled = true
end

HL.Commit(HeadBarCtrl)
