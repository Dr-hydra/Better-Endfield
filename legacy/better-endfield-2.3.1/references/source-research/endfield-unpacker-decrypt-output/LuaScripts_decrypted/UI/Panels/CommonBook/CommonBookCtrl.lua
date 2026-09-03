
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.CommonBook

CommonBookCtrl = HL.Class('CommonBookCtrl', uiCtrl.UICtrl)

CommonBookCtrl.m_onCloseCallback = HL.Field(HL.Function)





CommonBookCtrl.s_messages = HL.StaticField(HL.Table) << {

}


CommonBookCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.closeButton.onClick:AddListener(function()
        self:PlayAnimationOut()
    end)
    self.view.maskBtn.onClick:AddListener(function()
        self:PlayAnimationOut()
    end)

    if arg.onConfirm then
        self:BindInputPlayerAction("common_confirm", function()
            self:PlayAnimationOut()
            arg.onConfirm()
        end)
    end

    if arg.onCloseCallback then
        self.m_onCloseCallback = arg.onCloseCallback
    end

    self.view.titleText:SetAndResolveTextStyle(arg.title)
    self.view.contentTxt:SetAndResolveTextStyle(arg.content)

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
end






CommonBookCtrl.OnClose = HL.Override() << function(self)
    if self.m_onCloseCallback then
        self.m_onCloseCallback()
    end
end




HL.Commit(CommonBookCtrl)
