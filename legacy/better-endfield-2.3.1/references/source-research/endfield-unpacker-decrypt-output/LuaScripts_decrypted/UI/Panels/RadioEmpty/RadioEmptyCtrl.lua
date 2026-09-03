
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.RadioEmpty

RadioEmptyCtrl = HL.Class('RadioEmptyCtrl', uiCtrl.UICtrl)






RadioEmptyCtrl.s_messages = HL.StaticField(HL.Table) << {

}

RadioEmptyCtrl.m_inited = HL.Field(HL.Boolean) << false


RadioEmptyCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)

end

RadioEmptyCtrl.OnShow = HL.Override() << function(self)
    if self.m_inited  then
        self:Notify(MessageConst.ON_RADIO_EMPTY_SHOW)
    end
    self.m_inited = true
end
RadioEmptyCtrl.OnHide = HL.Override() << function(self)
    self:Notify(MessageConst.ON_RADIO_EMPTY_HIDE)
end






HL.Commit(RadioEmptyCtrl)
